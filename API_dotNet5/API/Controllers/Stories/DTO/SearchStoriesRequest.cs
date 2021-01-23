using Lib.Models;

namespace API.Controllers.Stories
{
    public class SearchStoriesRequest : PageBatch
    {
        public string General { get; set; }
    }
}
