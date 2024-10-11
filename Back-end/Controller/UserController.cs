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
    public class UserController : NancyModule
    {
        private UserService _userService = new UserService();
        private PostService _postService = new PostService();
        private CommentService _commentService = new CommentService();

        private string _basePath = "/api/v1/user";

        public UserController()
        {
            //Get(_basePath + "/", _ => "{\"message\": \"Hello World!\"}");

            Get(_basePath + "/{id}", parameters =>
            {
                int id = parameters.id;

                try
                {
                    User user = _userService.GetUserById(id);
                    Console.WriteLine("user: " + user);

                    List<Comment> comments = _commentService.GetCommentsByUserId(id);
                    Console.WriteLine("comments: " + comments);

                    List<Post> posts = _postService.GetPostsByUserId(id);
                    Console.WriteLine("posts: " + posts);

                    return Response.AsJson(new UserDTO(user, comments, posts));
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { message = ex.Message }, HttpStatusCode.NotFound);
                }
            });
        }

    }
}
