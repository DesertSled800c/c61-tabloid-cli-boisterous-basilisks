using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace TabloidCLI.UserInterfaceManagers
{
    public class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private string _connectionString;

        public NoteManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
            _connectionString = connectionString;     
        }

        public IUserInterfaceManager Exectute()
        {
            Console.WriteLine("Note Menu");
            Console.WriteLine(" 1) Add Note");
            Console.WriteLine(" 2) List Notes");
            Console.WriteLine(" 3) Remove Note");
            Console.WriteLine(" 0) Return");


            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case 1:
                    Add();
                    return this;
                case 2:
                    List();
                    return this;
                case 3:
                    Remove();
                    return this;
                case 4:
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void Add()
        {

        }

        private void List()
        {

        }

        private void Remove()
        {

        }
    }
}
