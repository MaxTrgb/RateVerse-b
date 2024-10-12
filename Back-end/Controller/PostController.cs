
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
    public class PostController : NancyModule
    {
        private UserService _userService = new UserService();
        private PostService _postService = new PostService();
        private CommentService _commentService = new CommentService();
        private GenreService _genreService = new GenreService();

        private readonly string _basePath = "/api/v1/post";
        private Response GetAllPosts()
        {
            try
            {
                List<Post> posts = _postService.GetAllPosts();
                
                List<int> userIds = posts.Select(x => x.UserId).ToList();
                List<User> users = _userService.GetUsersByIds(userIds);


                List<int> genreIds = posts.Select(x => x.GenreId).ToList();
                List<Genre> genres = _genreService.GetGenresByIds(genreIds);
                
                List<PostDTO> postsDTO = new List<PostDTO>();

                //Dictionary<int, Post> userPosts = new Dictionary<int, Post>();
                //posts.ForEach(x => userPosts.Add(x.UserId, x));

                foreach (Post post in posts)
                {
                    postsDTO.Add(new PostDTO(post, users.Find(x => x.Id == post.UserId), genres.Find(x => x.Id == post.GenreId)));
                }

                return Response.AsJson(postsDTO);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new { message = ex.Message }, HttpStatusCode.NotFound);
            }
        }

        public PostController()
        {
            Options("/*", args =>
            {
                return new Response()
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS")
                    .WithHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            });

            After.AddItemToEndOfPipeline(ctx =>
            {
                ctx.Response
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS")
                    .WithHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            });



            Get(_basePath + "/{id}", parameters =>
            {
                int id = parameters.id;

                try
                {
                    Post post = _postService.GetPostById(id);
                    if (post == null)
                        return Response.AsJson(new { message = "Post with id " + id + " not found" }, HttpStatusCode.NotFound);
                    Console.WriteLine("post: " + post);

                    Genre genre = _genreService.GetGenreById(post.GenreId);
                    if (genre == null)
                        return Response.AsJson(new { message = "Genre with id " + post.GenreId + " not found" }, HttpStatusCode.NotFound);

                    User user = _userService.GetUserById(post.UserId);
                    if (user == null)
                        return Response.AsJson(new { message = "User with id " + post.UserId + " not found" }, HttpStatusCode.NotFound);
                    Console.WriteLine("user: " + user);

                    List<Comment> comments = _commentService.GetCommentsByPostId(id);
                    Console.WriteLine("comments: " + comments);

                    List<User> commentUsers = new List<User>();
                    if (comments != null)
                        commentUsers = _userService.GetUsersByIds(comments.Select(x => x.UserId).ToList());



                    List<CommentDTO> commentDTOs = new List<CommentDTO>();

                    foreach (Comment comment in comments)
                    {

                        commentDTOs.Add(new CommentDTO(comment, commentUsers.Find(x => x.Id == comment.UserId)));

                    }


                    PostDTO postDTO = new PostDTO(post, user, commentDTOs, genre);

                    return Response.AsJson(postDTO);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { message = ex.Message }, HttpStatusCode.NotFound);
                }
            });

            Post(_basePath + "/", args =>
            {


                PostRequest request = null;

                try
                {
                    request = this.Bind<PostRequest>();
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { message = e.Message }, HttpStatusCode.BadRequest);
                }

                try
                {
                    User user = _userService.GetUserById(request.userId);
                    Genre genre = _genreService.GetGenreById(request.genreId);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { message = e.Message }, HttpStatusCode.BadRequest);
                }


                try
                {
                    int postId = _postService.AddPost(request.userId, request.title, request.image, request.content, request.genreId);
                    return Response.AsJson(new { message = postId }, HttpStatusCode.Created);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { message = e.Message }, HttpStatusCode.BadRequest);
                }
            });

            Get(_basePath + "/", args =>
            {
                int? id = (int?)this.Request.Query["userId"];
                List<PostDTO> postDTOs = new List<PostDTO>();
                List<Post> posts = new List<Post>();
                User user = null;
                List<Genre> genres = new List<Genre>();

                try
                {
                    if (!id.HasValue)
                        return GetAllPosts();

                    user = _userService.GetUserById(id.Value);
                    if (user == null)
                        return Response.AsJson(new { message = "User not found" }, HttpStatusCode.NotFound);

                    posts = _postService.GetPostsByUserId(id.Value);
                    if (posts == null)
                        return Response.AsJson(new { message = "Posts not found" }, HttpStatusCode.NotFound);

                    genres = _genreService.GetGenresByIds(posts.Select(x => x.GenreId).ToList());
                    if (genres == null)
                        return Response.AsJson(new { message = "Genres not found" }, HttpStatusCode.NotFound);

                    foreach (Post post in posts)
                        postDTOs.Add(new PostDTO(post, user, genres.Find(x => x.Id == post.GenreId)));

                }
                catch (Exception e)
                {
                    return Response.AsJson(new { message = e.Message }, HttpStatusCode.InternalServerError);
                }

                return Response.AsJson(postDTOs);
            });
        }
    }
}
