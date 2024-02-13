using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute; // ??

namespace DotnetAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")] // replaces class name => route becomes /Post
    public class PostController : ControllerBase
    {
        private readonly DataContextDapper _dapper;

        public PostController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        [HttpGet("Post")]
        public IEnumerable<Post> GetPosts()
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts";
            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("PostSingle/{postId}")]
        public IEnumerable<Post> GetPostSingle(int postId) // Route Paramaters
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts
                    WHERE PostId = " + postId.ToString();
            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("PostsByUser/{userId}")] // All posts for a single user
        public IEnumerable<Post> GetPostsByUser(int userId) // Route Paramaters
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts
                    WHERE PostId = " + userId.ToString();
            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("MyPosts")]
        public IEnumerable<Post> GetMyPosts() // Route Paramaters
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts
                    WHERE PostId = " + this.User.FindFirst("userId")?.Value;

            return _dapper.LoadData<Post>(sql);
        }

        [HttpPost("Post")]
        public IActionResult AddPost(PostToAddDTO postToAdd)
        {   // PostId is removed as it is auto generated.
            string sql = @"
            INSERT INTO TutorialAppSchmea.Posts( 
                [UserId],
                [PostTitle],
                [PostContent],
                [PostCreated],
                [PostUpdated]) VALUES (" + this.User.FindFirst("userId")?.Value
                + ",'" + postToAdd.PostTitle
                + "','" + postToAdd.PostContent
                + "', GETDATE(), GETDATE() )";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Failed to create new post");
        }

        [HttpPut("Post")]
        public IActionResult EditPost(PostToEditDTO postToEdit)
        {   // PostId is removed as it is auto generated.
            string sql = @"
            UPDATE TutorialAppSchema.Posts 
                SET PostTitle = '" + postToEdit.PostTitle +
                "', PostContent = '" + postToEdit.PostContent +
                @"', PostUpdated GETDATE()
                WHERE PostId = " + postToEdit.PostId.ToString() +
                "AND UserId =" + this.User.FindFirst("userId")?.Value;

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Failed to edit post");
        }

        [HttpDelete("Post/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            string sql = @"DELETE FROM TutorialAppSchema.Posts
                WHERE PostId = " + postId.ToString();

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to delete post");
        }
    }
}