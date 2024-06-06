using System.ComponentModel.DataAnnotations;

namespace DTOs.AccountAuthentication
{
    public class ConfirmPasswordDtos
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string OtpToken { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
