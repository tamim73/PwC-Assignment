using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Stories
{
    public class AddStoryRequest
    {

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
