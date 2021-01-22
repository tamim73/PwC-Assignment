using Lib.Models;

namespace API.Controllers.Auth
{
    public class RegisterResponseDTO: BaseResponse
    {
        public int UserId { get; set; }
    }
}
