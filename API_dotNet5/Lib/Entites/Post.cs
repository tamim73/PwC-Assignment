using System.ComponentModel.DataAnnotations;

namespace Lib.Entites
{
    public class Post: BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public string Description { get; set; }

        public int AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

    }
}
