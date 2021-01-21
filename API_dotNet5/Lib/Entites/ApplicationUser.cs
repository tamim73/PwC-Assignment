using System.ComponentModel.DataAnnotations;

namespace Lib.Entites
{
    public class ApplicationUser : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Role { get; set; }
        
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
