using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private string _connectionString;
        private PostRepository _postRepository;
        private int _postId;

        public NoteManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
            _postId = postId;
            
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Note Menu");
            Console.WriteLine(" 1) Add Note");
            Console.WriteLine(" 2) List Notes");
            Console.WriteLine(" 3) Remove a Note");
            Console.WriteLine(" 0) Return");


            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Add();
                    return this;
                case "2":
                    List();
                    return this;
                case "3":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void Add()
        {
            Post post = _postRepository.Get(_postId);

            Console.WriteLine("New Note");
            Note note = new Note();

            Console.WriteLine("What would you like to Title your Note?");
            Console.Write("> ");
            string titleEntered = Console.ReadLine();
            note.Title = titleEntered;
            Console.WriteLine();

            Console.WriteLine("What content do you want to add to your Note?");
            Console.Write("> ");
            string contentEntered = Console.ReadLine();
            note.Content = contentEntered;
            

            _postRepository.InsertNote(post,note);
            Console.WriteLine("Note Added");
            Console.WriteLine();


        }

        private void List()
        {
            Console.WriteLine("Your Current Notes");
            List<Note> notes = _noteRepository.GetAllLinkedToPost(_postId);

            for (int i = 0; i < notes.Count; i++)
            {
                Note note = notes[i];
                Console.WriteLine($"{i + 1}: {note.Title} \n\t{note.CreateDateTime}\n\t{note.Content}");
            }
            Console.WriteLine();
        }

        private void Remove()
        {
            Console.WriteLine("Which Note would you like to remove from your post?");
            List<Note> notes = _noteRepository.GetAllLinkedToPost( _postId);

            for (int i = 0; i < notes.Count; i++)
            {
                Note note = notes[i];
                Console.WriteLine($"{i + 1}: {note.Title}");
            }
            Console.Write("> ");
            int deleteChoice = int.Parse(Console.ReadLine());
            Console.WriteLine();
            _postRepository.DeletNote(_postId, deleteChoice);
        }
    }
}
