using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Stories
{
    public class EditStoryRequest
    {
        public int Id { get; set; }

        [Required]
        public string StoryTitle { get; set; }

        [Required]
        public string StoryDescription { get; set; }

        [Required]
        public string TopicTitle { get; set; }

        [Required]
        public string TopicContent { get; set; }
    }
}
