using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class NoteRepository : DatabaseConnector
    {
        public NoteRepository(string connectionString) : base(connectionString) { }

        public List<Note> GetAllLinkedToPost(int id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT n.Id, n.Title, n.Content, n.CreateDateTime
                                        FROM Note n JOIN Post p ON n.postId = p.Id
                                        WHERE p.Id = @id";
                    cmd.Parameters.AddWithValue("id", id);

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Note> notes = new List<Note>();
                        while (reader.Read())
                        {
                            Note note = new Note()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Content = reader.GetString(reader.GetOrdinal("Content")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))  
                            };
                            notes.Add(note);
                        }
                        return notes;
                    }
                }
            }
        }



        /*for post detail manager
         add private int _postId;
        add it to contructor as well
        // add choose method to postManager
        // private AddNote() to postManager
        //private RemoveNOte() to postManager
        */

        public void InsertNote(Post post, Note note) //add to PostRepo
        {
            using(SqlConnection conn= Connection)
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO Note ( Title, Content, CreateDateTime, PostId)
                                    VALUES (@title, @content, GETDATE(), @postId)";
                    cmd.Parameters.AddWithValue("@title", note.Title);
                    cmd.Parameters.AddWithValue("@content", note.Content);
                    cmd.Parameters.AddWithValue("@postId", post.Id);

                    cmd.ExecuteNonQuery();
                };
            }

        }

        public void DeletNote(int postId, int id ) //Add to PostRepo
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"DELETE FROM Note 
                                        WHERE PostId = @postId AND Id = @id";
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
