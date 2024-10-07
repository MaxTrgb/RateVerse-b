using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DENMAP_SERVER.Entity;
using DENMAP_SERVER.Repository;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace DENMAP_SERVER.Service
{
    internal class CommentService
    {
        string connectionString = "Server=34.116.253.154;Port=3306;Database=chat_database;Uid=app_user;Pwd=&X9fT#7vYqZ$4LpR;";

        private CommentRepository commentRepository = new CommentRepository();

        public int AddComment(int userId, double rating, string message, int postId)
        {
            int id = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    id = commentRepository.addComment(connection, userId, rating, message, postId, DateTime.Now);
                }
                catch (Exception ex)
                {
                    throw new Exception("Database error.\nError:" + ex.Message);
                }
            }
            return id;
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            List<Comment> comments = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    comments = commentRepository.getCommentsByPostID(connection, postId);
                }
                catch (Exception ex)
                {
                    throw new Exception("Database error.\nError:" + ex.Message);
                }
            }
            return comments;
        }

        public List<Comment> GetCommentsByUserId(int userId)
        {
            List<Comment> comments = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    comments = commentRepository.getCommentsByUserID(connection, userId);
                }
                catch (Exception ex)
                {
                    throw new Exception("Database error.\nError:" + ex.Message);
                }
            }
            return comments;
        }
    }
}
