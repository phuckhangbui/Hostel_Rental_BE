using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class LoginWithGoogleDto
    {
        [Required]
        public string IdTokenString { get; set; }
    }
}
