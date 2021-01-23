using API.Context;
using Lib.Entites;
using Lib.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.Posts
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {

        private AssignmentDbContext dbContext;

        public PostsController(AssignmentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PostDetailsResponse>> Get(int id)
        {
            var post = await dbContext.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(new PostDetailsResponse
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
            });
        }

        // POST api/<PostsController>
        [HttpPost]
        public async Task<ActionResult<AddPostResponse>> Post([FromBody] AddPostRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            int.TryParse(User.FindFirst(ClaimTypes.Name).Value, out int userId);
            if (userId == 0) return Unauthorized();

            Story story = await dbContext.Stories.FindAsync(request.StoryId);
            if (story == null) return NotFound();

            Post post = new Post()
            {
                Title = request.Title,
                Content = request.Content,
                AuthorId = userId,
                PostForStoryId = story.Id,
                IsTopic = true
            };


            try
            {
                dbContext.Posts.Add(post);
                await dbContext.SaveChangesAsync();
                return Ok(new AddPostResponse { Message = "Post created successfully", PostId = post.Id });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<EditPostResponse>> Put(int id, [FromBody] EditPostRequest request)
        {
            if (id != request.Id) return BadRequest(new EditPostResponse { Message = "Id does not match", HasError = true });
            if (!ModelState.IsValid) return BadRequest(ModelState);

            int.TryParse(User.FindFirst(ClaimTypes.Name).Value, out int userId);
            if (userId == 0) return Unauthorized();

            string userRole = User.FindFirst(ClaimTypes.Role).Value;

            Post post = await dbContext.Posts.FindAsync(id);

            if (post == null) return NotFound(new EditPostResponse { Message = "Post does not exist", HasError = true });

            // if user is not the post author or the user is not an admin
            if (post.AuthorId != userId || userRole != UserRole.Admin)
                return Unauthorized(new EditPostResponse { Message = "You do no t have permissions to edit this post", HasError = true });

            post.Title = request.Title.Trim();
            post.Description = request.Description.Trim();
            post.Content = request.Content;

            try
            {
                dbContext.Entry(post).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return Ok(new EditPostResponse { Message = "Post updated successfully" });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeletePostResponse>> Delete(int id)
        {
            int.TryParse(User.FindFirst(ClaimTypes.Name).Value, out int userId);
            if (userId == 0) return Unauthorized();

            string userRole = User.FindFirst(ClaimTypes.Role).Value;

            Post post = await dbContext.Posts.FindAsync(id);

            if (post == null) return NotFound(new DeletePostResponse { Message = "Post does not exist", HasError = true });

            // if user is not the story author or the user is not an admin
            if (post.AuthorId != userId || userRole != UserRole.Admin)
                return Unauthorized(new DeletePostResponse { Message = "You do no t have permissions to delete this post", HasError = true });

            if (post.TopicForStoryId != null || post.IsTopic)
                return BadRequest(new DeletePostResponse { Message = "This post is a story topic, you must delete the story first", HasError = true });

            try
            {
                dbContext.Posts.Remove(post);
                await dbContext.SaveChangesAsync();
                return Ok(new DeletePostResponse { Message = "Post deleted successfully" });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
