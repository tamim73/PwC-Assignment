using Lib.Models;

namespace API.Controllers.Posts
{
    public class PostDetailsResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
