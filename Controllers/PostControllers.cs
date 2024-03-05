using System.Data;
using Dapper;
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

        [HttpGet("Posts/{postId}/{userId}/{searchParam}")]
        public IEnumerable<Post> GetPosts(int postId = 0, int userId = 0, string searchParam = "None")
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Get";
            string stringParameters = "";
            DynamicParameters sqlParameters = new DynamicParameters();

            if (postId != 0)
            {
                stringParameters += ", @PostId=@PostIdParameter";
                sqlParameters.Add("PostIdParameter", postId, DbType.Int32);
            }
            if (userId != 0)
            {
                stringParameters += ", @userId=@UserIdParameter";
                sqlParameters.Add("UserIdParameter", userId, DbType.Int32);
            }
            if (searchParam.ToLower() != "none")
            {
                stringParameters += ", @SearchValue=@SearchValueParam";
                sqlParameters.Add("SearchValueParam", searchParam, DbType.String);
            }

            if (stringParameters.Length > 0)
            {
                sql += stringParameters.Substring(1);
            }

            return _dapper.LoadDataWithParameters<Post>(sql, sqlParameters);
        }

        [HttpGet("MyPosts")]
        public IEnumerable<Post> GetMyPosts(Post user) // Route Paramaters
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Get @UserId =@UserIdParameter";

            DynamicParameters sqlParameters = new DynamicParameters();

            sqlParameters.Add("@UserIdParameter", user.UserId, DbType.Int32);

            return _dapper.LoadDataWithParameters<Post>(sql, sqlParameters);
        }

        [HttpPut("UpsertPost")]
        public IActionResult UpsertPost(Post post)
        {   // PostId is removed as it is auto generated.
            string sql = @"
            EXEC TutorialAppSchema.spPosts_Upsert
                @UserId =@UserIdParameter, 
                @PostTitle =@PostTitleParameter, 
                @PostContent =@PostContentParameter";


            DynamicParameters sqlParameters = new DynamicParameters();

            sqlParameters.Add("UserIdParameter", post.UserId, DbType.Int32);
            sqlParameters.Add("PostTitleParameter", post.PostTitle, DbType.String);
            sqlParameters.Add("PostContentParameter", post.PostContent, DbType.String);

            // if (post.PostId > 0)
            // {
            //     sql += ", @PostId  = " + post.PostId;
            // }
            if (_dapper.ExecuteSqlWithParameters(sql, sqlParameters))
            {
                return Ok();
            }

            throw new Exception("Failed to create new post");
        }

        [HttpDelete("Post/{postId}")]
        public IActionResult DeletePost(int postId, Post user)
        {
            string sql = @" EXEC TutorialAppSchema.spPost_Delete
                 @PostId =@PostIdParameter, @UserId =@UserIdParameter";

            DynamicParameters sqlParameters = new DynamicParameters();
            sqlParameters.Add("@PostIdParameter", postId, DbType.Int32);
            sqlParameters.Add("@UserIdParameter", user.UserId, DbType.Int32);
            if (_dapper.ExecuteSqlWithParameters(sql, sqlParameters))
            {
                return Ok();
            }
            throw new Exception("Failed to delete post");
        }
    }
}