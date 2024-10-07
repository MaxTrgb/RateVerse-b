using DENMAP_SERVER.Entity;
using DENMAP_SERVER.Entity.dto;
using DENMAP_SERVER.Service;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Controller
{
    internal class UserController : NancyModule
    {
        private UserService _userService = new UserService();
        private PostService _postService = new PostService();
        private CommentService _commentService = new CommentService();

        private readonly string _basePath = "/api/user";

        public UserController()
        {
            //Get(_basePath + "/", _ => "{\"message\": \"Hello World!\"}");

            Get(_basePath + "/{id}", parameters =>
            {
                int id = parameters.id;

                try
                {
                    User user = _userService.GetUserById(id);

                    List<Comment> comments = _commentService.GetCommentsByUserId(id);

                    List<Post> posts = _postService.GetPostsByUserId(id);

                    return Response.AsJson(new UserDTO(user, comments, posts));
                }
                catch(Exception ex)
                {
                    return Response.AsJson(new { message = ex.Message }, HttpStatusCode.NotFound);
                }
            });
        }

    }
}
