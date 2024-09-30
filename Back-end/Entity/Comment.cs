using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Entity
{
    internal class Comment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public double Rating { get; set; }
        public string Message { get; set; }
        public Post Post { get; set; }

        public Comment(int id, User user, double rating, string message, Post post)
        {
            Id = id;
            User = user;
            Rating = rating;
            Message = message;
            Post = post;
        }
    }
}
