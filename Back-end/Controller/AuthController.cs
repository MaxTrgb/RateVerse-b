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
using DENMAP_SERVER.Controller.request;

namespace DENMAP_SERVER.Controller
{
    public class AuthController : NancyModule
    {
        private UserService _userService = new UserService();

        private readonly string _basePath = "/auth";

        public AuthController()
        {
            Post(_basePath + "/register", args =>
            {

                AuthRequest request = null;

                try
                {
                    Console.WriteLine("start binding parameters");
                   request = this.Bind<AuthRequest>();
                    Console.WriteLine(request);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { message = e.Message }, HttpStatusCode.BadRequest);
                }

                try
                {
                    int userId = _userService.RegisterUser(request.Name, request.Password, request.Image, request.Description);
                    return Response.AsJson(new { message = userId }, HttpStatusCode.Created);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { message = e.Message }, HttpStatusCode.BadRequest);
                }

            });

            Post(_basePath + "/login", args =>
            {
                AuthRequest request = null;

                try
                {
                   request = this.Bind<AuthRequest>();
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { message = e.Message }, HttpStatusCode.BadRequest);
                }

                try
                {
                    int userId = _userService.loginUser(request.Name, request.Password);
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
