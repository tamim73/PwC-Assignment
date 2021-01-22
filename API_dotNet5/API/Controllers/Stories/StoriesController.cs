using API.Context;
using Lib.Entites;
using Lib.Enums;
using Lib.Helpers;
using Lib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers.Stories
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoriesController : ControllerBase
    {

        private AssignmentDbContext dbContext;

        public StoriesController(AssignmentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/<StoriesController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<SearchStoriesResponse>> Search([FromQuery] PageBatch batch, [FromQuery] SearchStoriesRequest request)
        {
            IQueryable<Story> storiesQueryable = dbContext.Stories
               .Where(story =>
                   // general search 
                   string.IsNullOrEmpty(request.General) ||
                       story.Topic.Title.Contains(request.General) ||
                       story.Topic.Description.Contains(request.General) ||
                       story.Topic.Author.Name.Contains(request.General)
                   );

            int count = storiesQueryable.Count();

            var result = await SearchHelper.QuerySearchBatch(storiesQueryable, batch)
               .Include(x => x.Topic).ThenInclude(x => x.Author)
               .Select(story => new StoriesListModel
               {
                   Id = story.Id,
                   Title = story.Topic.Title,
                   Description = story.Topic.Description,
                   AuthorName = story.Topic.Author.Name,
                   CreationDateTime = story.CreationDateTime
               })
               .ToListAsync();

            return Ok(new SearchStoriesResponse
            {
                Rows = result,
                Count = count
            });
        }

        // GET api/<StoriesController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<StoryDetailsRespone>> Get(int id)
        {
            var story = await dbContext.Stories
                .Include(x => x.Topic).ThenInclude(x => x.Author)
                .Include(x => x.Posts).ThenInclude(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (story == null)
            {
                return NotFound();
            }

            return new StoryDetailsRespone
            {
                Id = story.Id,
                Title = story.Topic.Title,
                Description = story.Topic.Description,
                Content = story.Topic.Content,
                AuthorId = story.Topic.AuthorId,
                AuthorName = story.Topic.Author.Name,
                CreationDateTime = story.CreationDateTime,
                Posts = story.Posts.Select(x => new StoryPostItem
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    CreationDateTime = x.CreationDateTime,
                    AuthorId = x.AuthorId,
                    AuthorName = x.Author.Name
                }).ToList()
            };
        }

        // POST api/<StoriesController>
        [HttpPost]
        public async Task<ActionResult<AddStoryResponse>> Post([FromBody] AddStoryRequest request)
        {
            int.TryParse(User.FindFirst(ClaimTypes.Name).Value, out int userId);
            if (userId == 0) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Story story = new Story()
            {
                Topic = new Post
                {
                    Title = request.Title.Trim(),
                    Description = request.Description.Trim(),
                    Content = request.Content,
                    AuthorId = userId,
                },
            };

            try
            {
                dbContext.Stories.Add(story);
                await dbContext.SaveChangesAsync();
                return Ok(new AddStoryResponse { Message = "Story created successfully", StoryId = story.Id });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        // PUT api/<StoriesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<EditStoryResponse>> Put(int id, [FromBody] EditStoryRequest request)
        {
            if (id != request.Id) return BadRequest(new EditStoryResponse { Message = "Id does not match", HasError = true });
            if (!ModelState.IsValid) return BadRequest(ModelState);

            int.TryParse(User.FindFirst(ClaimTypes.Name).Value, out int userId);
            if (userId == 0) return Unauthorized();

            string userRole = User.FindFirst(ClaimTypes.Role).Value;


            Story story = await dbContext.Stories.Include(x => x.Topic).FirstOrDefaultAsync(x => x.Id == id);

            if (story == null) return NotFound(new EditStoryResponse { Message = "Story does not exist", HasError = true });

            // if user is not the story author or the user is not an admin
            if (story.Topic.AuthorId != userId || userRole != UserRole.Admin)
                return Unauthorized(new EditStoryResponse { Message = "You do no t have permissions to edit this story", HasError = true });

            story.Topic.Title = request.Title.Trim();
            story.Topic.Description = request.Description.Trim();
            story.Topic.Content = request.Content;

            try
            {
                dbContext.Entry(story).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return Ok(new EditStoryResponse { Message = "Story updated successfully" });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        // DELETE api/<StoriesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteStoryResponse>> Delete(int id)
        {
            int.TryParse(User.FindFirst(ClaimTypes.Name).Value, out int userId);
            if (userId == 0) return Unauthorized();

            string userRole = User.FindFirst(ClaimTypes.Role).Value;

            Story story = await dbContext.Stories.Include(x => x.Topic).Include(x => x.Posts).FirstOrDefaultAsync(x => x.Id == id);

            if (story == null) return NotFound(new DeleteStoryResponse { Message = "Story does not exist", HasError = true });

            // if user is not the story author or the user is not an admin
            if (story.Topic.AuthorId != userId || userRole != UserRole.Admin)
                return Unauthorized(new DeleteStoryResponse { Message = "You do no t have permissions to delete this story", HasError = true });

            try
            {
                dbContext.Stories.Remove(story);
                await dbContext.SaveChangesAsync();
                return Ok(new DeleteStoryResponse { Message = "Story deleted successfully" });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
