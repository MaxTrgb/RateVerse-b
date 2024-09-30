using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Modules
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get("/", _ => "{\"message\": \"Hello World!\"}");
        }
    }
}
