using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Entity.dto
{
    internal class CommentDTO
    {
        public int Id { get; set; }
        public User User { get; set; }
        public double Rating { get; set; }
        public string Message { get; set; }
        public Post Post { get; set; }
        public DateTime CreatedAt { get; set; }

        public CommentDTO(int id, User user, double rating, string message, Post post, DateTime createdAt)
        {
            Id = id;
            User = user;
            Rating = rating;
            Message = message;
            Post = post;
            CreatedAt = createdAt;
        }
    }
}
