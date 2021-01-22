using Lib.Models;

namespace API.Controllers.Auth
{
    public class LoginResponseDTO : BaseResponse
    {
        public string AccessToken { get; set; }
    }
}
