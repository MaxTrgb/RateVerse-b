using DENMAP_SERVER.Entity.dto;
using DENMAP_SERVER.Entity;
using DENMAP_SERVER.Service;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.ModelBinding;

namespace DENMAP_SERVER.Controller
{
    internal class AuthController : NancyModule
    {
        private UserService _userService = new UserService();

        private readonly string _basePath = "/auth";

        public AuthController()
        {
            Post(_basePath + "/register", args =>
            {
                string name;
                string password;
                byte[] image;
                string description;

                try
                {
                    name = this.Bind<string>("name");
                    password = this.Bind<string>("password");
                    image = this.Bind<byte[]>("image");
                    description = this.Bind<string>("description");
                }
                catch(Exception e)
                {
                    return Response.AsJson(new { message = e.Message }, HttpStatusCode.BadRequest);
                }

                try
                {
                    int userId = _userService.RegisterUser(name, password, image, description);
                    return Response.AsJson(new { message = userId }, HttpStatusCode.Created);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { message = e.Message }, HttpStatusCode.BadRequest);
                }
                
            });

            Post(_basePath + "/login", args =>
            {
                string name;
                string password;

                try
                {
                    name = this.Bind<string>("name");
                    password = this.Bind<string>("password");
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { message = e.Message }, HttpStatusCode.BadRequest);
                }

                try
                {
                    int userId = _userService.loginUser(name, password);
                    return Response.AsJson(new { message = userId }, HttpStatusCode.OK);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { message = e.Message }, HttpStatusCode.BadRequest);
                }
            });
        }
    }
}
