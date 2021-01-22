using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Auth
{
    public class LoginRequestDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
