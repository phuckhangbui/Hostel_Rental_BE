using System.ComponentModel.DataAnnotations;

namespace DTOs.Membership
{
    public class MembershipUpdatePackageDto
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        public int MembershipId { get; set; }
        [Required]
        public string ReturnUrl { get; set; }
        [Required]
        public double Fee { get; set; }

    }
}
