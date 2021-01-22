using System.ComponentModel.DataAnnotations;

namespace Lib.DTO
{
    public  class RegisterRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
