using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private string _connectionString;
        private int _postId;

        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _connectionString = connectionString;
            _postId = postId;
        }

        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) Note Management");
            Console.WriteLine(" 5) View Post's Tags");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    AddTag();
                    return this;
                case "3":
                    RemoveTag();
                    return this;
                case "4":
                    return new NoteManager(this, _connectionString, _postId);
                case "5":
                    ViewPostTags();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }

        }

        private void View()
        {
            Console.WriteLine();
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"Post's Title: {post.Title} \n\t URL: {post.Url} \n\t Publication Date: {post.PublishDateTime}");
            Console.WriteLine($"Post's Author: \t{post.Author.FullName}");
            Console.WriteLine($"Post's Blog: \t{post.Blog.Title}");

            Console.WriteLine();
        }

        private void ViewPostTags()
        {
            Post post = _postRepository.Get(_postId);
            List<Tag> tags = _tagRepository.GetTagsByPost(_postId);
            Console.WriteLine($" Post Name: {post.Title} \n\t Post Details: {post.Url}\n\t Publish Date: {post.PublishDateTime}");
            Console.WriteLine($"Here is the list of Tags for this Post");
            if (tags.Count == 0)
            {
                Console.WriteLine("This Post currently has no tags assigned to it");
            }
            else
            {
                for (int i = 1; i <= tags.Count; i++)
                {
                    Tag tag = tags[i-1];
                    {
                        Console.WriteLine($" {i}) Tag Name:{tag.Name}");
                    }
                }
            }

          


        }

        private void AddTag ()
        {
            Post post = _postRepository.Get(_postId);

            Console.WriteLine("Here is a list of available Tags");
            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");
            int tagChoise = int.Parse (Console.ReadLine());

            _postRepository.InsertPostTag(post, tags[tagChoise - 1]);
            Console.WriteLine("The tag has been added to your post");
            Console.WriteLine();
        }

        private void RemoveTag()
        {
            Post post = _postRepository.Get(_postId);

            Console.WriteLine("Here is a list of available Tags");
            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");
            int tagChoise = int.Parse(Console.ReadLine());

            _postRepository.DeletePostTag(post, tags[tagChoise - 1]);
            Console.WriteLine("The tag has been removed from your post");
            Console.WriteLine();
        }
    }
}