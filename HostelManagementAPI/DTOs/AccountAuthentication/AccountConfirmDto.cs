using System.ComponentModel.DataAnnotations;

namespace DTOs.AccountAuthentication
{
    public class AccountConfirmDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string OtpToken { get; set; }
    }
}
