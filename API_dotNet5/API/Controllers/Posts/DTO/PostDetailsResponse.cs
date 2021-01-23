using Lib.Models;

namespace API.Controllers.Posts
{
    public class PostDetailsResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Contetnt { get; set; }
    }
}
