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
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Posts");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    AddByAuthorBlog();
                    return this;
                case "3":
                    Remove();
                    return this;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine(post);
            }
        }

        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }


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
                Console.WriteLine($"{a.Id} {a.FullName}");
            }
            Console.Write("AuthorId: ");
            post.Author = new Author();
            post.Author.Id = int.Parse(Console.ReadLine());

            Console.Write("BlogId: ");
            post.Blog.Id = int.Parse(Console.ReadLine());


            _postRepository.Insert(post);
        }

        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New URL (blank to leave unchanged: ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }
            Console.Write("New Publish Date and Time (blank to leave unchanged: ");
            DateTime publishDateTime = DateTime.Parse(Console.ReadLine());
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.PublishDateTime = publishDateTime;
            }
            Console.Write("New Author ID (blank to leave unchanged: ");
            string author = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(author))
            {
                postToEdit.Author.Id = int.Parse(author);
            }
            Console.Write("New Blog ID (blank to leave unchanged: ");
            string blog = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(blog))
            {
                postToEdit.Blog.Id = int.Parse(blog);
            }

            _postRepository.Update(postToEdit);
        }

        private void Remove()
        {
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }

        }
    }
}

