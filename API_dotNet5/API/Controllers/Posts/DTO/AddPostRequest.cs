using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Posts
{
    public class AddPostRequest
    {

        public int StoryId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

    }
}
