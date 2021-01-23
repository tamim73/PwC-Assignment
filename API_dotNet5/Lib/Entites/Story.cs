using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lib.Entites
{
    public class Story: BaseEntity
    {

        public Post Topic { get; set; }

        public List<Post>  Posts { get; set; }

    }
}
