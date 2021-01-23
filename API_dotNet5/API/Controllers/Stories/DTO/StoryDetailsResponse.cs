using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Stories
{
    public class StoryDetailsResponse: StoryPostItem
    {
        public int StoryId { get; set; }
        public List<StoryPostItem> Posts { get; set; }
    }

    public class StoryPostItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
    }
}
