using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Posts
{
    public class EditPostRequest
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
