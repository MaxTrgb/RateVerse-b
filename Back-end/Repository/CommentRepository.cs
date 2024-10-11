using DENMAP_SERVER.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Repository
{
    internal class CommentRepository
    {
        public int addComment(MySqlConnection connection, int userId, double rating, string message, int postId, DateTime createdAt)
        {
            string query = $"INSERT INTO comments (post_id, user_id, rating, message, created_at) VALUES ( @postId, @userId, @rating, @message, @createdAt); SELECT LAST_INSERT_ID();";
            int id = 0;
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@rating", rating);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.Parameters.AddWithValue("@postId", postId);
                cmd.Parameters.AddWithValue("@createdAt", createdAt);
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return id;
        }


        public List<Comment> getCommentsByUserID(MySqlConnection connection, int userId)
        {
            List<Comment> comments = new List<Comment>();

            string query = $"SELECT * FROM comments WHERE user_id = {userId}";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Comment comment = new Comment(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["user_id"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToString(reader["message"]),
                            Convert.ToInt32(reader["post_id"]),
                            Convert.ToDateTime(reader["created_at"])
                        );
                        comments.Add(comment);
                    }
                }
            }

            return comments;
        }

        public List<Comment> getCommentsByPostID(MySqlConnection connection, int postId)
        {
            List<Comment> comments = new List<Comment>();

            string query = $"SELECT * FROM comments WHERE post_id = {postId}";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Comment comment = new Comment(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["user_id"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToString(reader["message"]),
                            Convert.ToInt32(reader["post_id"]),
                            Convert.ToDateTime(reader["created_at"])
                        );
                        comments.Add(comment);
                    }
                }
            }

            return comments;
        }
    }
}
