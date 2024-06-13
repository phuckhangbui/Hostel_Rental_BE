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

        public string Address { get; set; }
        public string Phone { get; set; }
        public string CitizenCard { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
