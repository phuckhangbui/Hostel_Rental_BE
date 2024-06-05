using System.ComponentModel.DataAnnotations;

namespace DTOs.AccountAuthentication
{
    public class EmailRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
