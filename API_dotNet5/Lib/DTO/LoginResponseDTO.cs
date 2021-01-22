using Lib.Models;

namespace Lib.DTO
{
    public class LoginResponseDTO : BaseResponse
    {
        public string AccessToken { get; set; }
    }
}
