using System.ComponentModel.DataAnnotations;

namespace DTOs.AccountAuthentication
{
    public class RegisterWithGoogleDto
    {
        [Required]
        public string IdTokenString { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
