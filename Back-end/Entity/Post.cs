using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Entity
{
    internal class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        public Post(int id, int user, string title, byte[] image, string content, double rating, DateTime createdAt)
        {
            Id = id;
            UserId = user;
            Title = title;
            Image = image;
            Content = content;
            Rating = rating;
            CreatedAt = createdAt;
        }
    }
}
