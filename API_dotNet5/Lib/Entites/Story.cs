using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lib.Entites
{
    public class Story: BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string OrginalAuthorName { get; set; }

        public int TopicId { get; set; }

        public Post Topic { get; set; }

        public List<Post>  Posts { get; set; }

    }
}
