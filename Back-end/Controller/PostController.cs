using DENMAP_SERVER.Entity.dto;
using DENMAP_SERVER.Entity;
using DENMAP_SERVER.Service;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Controller
{
    internal class PostController : NancyModule
    {
        private UserService _userService = new UserService();
        private PostService _postService = new PostService();
        private CommentService _commentService = new CommentService();

        private readonly string _basePath = "/post";

        public PostController()
        {
            Get(_basePath + "/", _ => 
            {

                try
                {
                    List<Post> posts = _postService.GetAllPosts();
                    List<int> userIds = posts.Select(x => x.UserId).ToList();

                    List<User> users = _userService.GetUsersByIds(userIds);

                    List<PostDTO> postsDTO = new List<PostDTO>();



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
