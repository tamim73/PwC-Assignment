using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lib.Entites
{
    public class Story: BaseEntity
    {

        public int TopicId { get; set; }

        public Post Topic { get; set; }

        public List<Post>  Posts { get; set; }

    }
}
