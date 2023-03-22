using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
	public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Author Menu");
            //Console.WriteLine(" 1) List PostS");
            Console.WriteLine(" 2) Add Posts");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                //case "1":
                //    List();
                //    return this;
                case "2":
                    AddByAuthorBlog();
                    return this;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        //private void List()
        //{
        //    List<Post> posts = _postRepository.GetAll();
        //    foreach (Post post in posts)
        //    {
        //        Console.WriteLine(post);
        //    }
        //}

        private void AddByAuthorBlog()
        {
            Console.WriteLine("New Post");
            Post post = new Post();
            Author author = new Author();


            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("Url: ");
            post.Url = Console.ReadLine();

            Console.Write("Publish YYYY-MM-DD : ");
            post.PublishDateTime = DateTime.Parse(Console.ReadLine());

            List<Author> authors = _authorRepository.GetAll();
            foreach (Author a in authors)
            {
                Console.WriteLine(a);
            }
            Console.Write("AuthorId: ");
            post.Author = new Author();
            post.Author.Id = int.Parse(Console.ReadLine());

            Console.Write("BlogId: ");
            post.Blog.Id = int.Parse(Console.ReadLine());


            _postRepository.Insert(post);
        }
    }
}

