using System.ComponentModel.DataAnnotations;

namespace DTOs.AccountAuthentication
{
    public class AccountConfirmDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string OtpToken { get; set; }
    }
}
