using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Controller.request
{
    internal class CommentRequest
    {
        public int userId { get; set; }
        public int postId { get; set; }
        public double rating { get; set; }
        public string message { get; set; }
    }
}
