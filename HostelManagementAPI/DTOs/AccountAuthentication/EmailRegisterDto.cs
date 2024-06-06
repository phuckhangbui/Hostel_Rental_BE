using System.ComponentModel.DataAnnotations;

namespace DTOs.AccountAuthentication
{
    public class EmailRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int RoleId { get; set; }

        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
