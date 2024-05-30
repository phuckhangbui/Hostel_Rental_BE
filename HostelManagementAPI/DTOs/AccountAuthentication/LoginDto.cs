using System.ComponentModel.DataAnnotations;

namespace DTOs.AccountAuthentication
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
