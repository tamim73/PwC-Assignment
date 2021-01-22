using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Stories
{
    public class EditStoryRequest
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
