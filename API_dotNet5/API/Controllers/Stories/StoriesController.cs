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
                       story.Title.Contains(request.General) ||
                       story.Description.Contains(request.General) ||
                       story.Topic.Author.Name.Contains(request.General)
                   );

            int count = storiesQueryable.Count();

            var result = await SearchHelper.QuerySearchBatch(storiesQueryable, batch)
               .Select(story => new StoriesListModel
               {
                   Title = story.Title,
                   Description = story.Description,
                   AuthorName = story.OrginalAuthorName,
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
        public async Task<ActionResult<Story>> Get(int id)
        {
            var story = await dbContext.Stories
                .Include(x => x.Topic).ThenInclude(x => x.Author)
                .Include(x => x.Posts)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (story == null)
            {
                return NotFound();
            }

            return story;
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
                Title = request.StoryTitle.Trim(),
                Description = request.StoryDescription.Trim(),
                Topic = new Post
                {
                    Title = request.TopicTitle.Trim(),
                    Content = request.TopicContent,
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
            int.TryParse(User.FindFirst(ClaimTypes.Name).Value, out int userId);
            if (userId == 0) return Unauthorized();

            string userRole = User.FindFirst(ClaimTypes.Role).Value;

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != request.Id) return BadRequest(new EditStoryResponse { Message = "Id does not match", HasError = true });

            Story story = await dbContext.Stories.Include(x => x.Topic).FirstOrDefaultAsync(x => x.Id == id);

            if (story == null) return NotFound(new EditStoryResponse { Message = "Story does not exist", HasError = true });
            
            // if user is not the story author or the user is not an admin
            if (story.Topic.AuthorId != userId || userRole != UserRole.Admin) 
                return Unauthorized(new EditStoryResponse { Message = "You do no t have permissions to edit this story", HasError = true });
            
            story.Title = request.StoryTitle.Trim();
            story.Description = request.StoryDescription.Trim();
            story.Topic.Title = request.TopicTitle.Trim();
            story.Topic.Content = request.TopicContent;

            try
            {
                dbContext.Entry(story).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return Ok(new EditStoryResponse { Message = "Employee updated successfully" });
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
