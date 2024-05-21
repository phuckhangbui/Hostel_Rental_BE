using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class EmailRegisterDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
