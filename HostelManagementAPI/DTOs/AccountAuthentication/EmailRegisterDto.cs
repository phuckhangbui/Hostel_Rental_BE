using System.ComponentModel.DataAnnotations;

namespace DTOs.AccountAuthentication
{
    public class EmailRegisterDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
