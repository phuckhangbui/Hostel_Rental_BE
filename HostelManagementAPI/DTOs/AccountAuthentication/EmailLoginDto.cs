using System.ComponentModel.DataAnnotations;

namespace DTOs.AccountAuthentication
{
    public class EmailLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? FirebaseRegisterToken { get; set; }
    }
}
