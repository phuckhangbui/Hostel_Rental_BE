using System.ComponentModel.DataAnnotations;

namespace DTOs.AccountAuthentication
{
    public class LoginWithGoogleDto
    {
        [Required]
        public string IdTokenString { get; set; }
    }
}
