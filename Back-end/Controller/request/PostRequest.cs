using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Controller.request
{
    internal class PostRequest
    {
        public int userId { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string content { get; set; }
        public int genreId { get; set; }
    }
}
