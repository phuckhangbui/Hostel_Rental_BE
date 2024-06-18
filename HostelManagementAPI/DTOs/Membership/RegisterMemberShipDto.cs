using System.ComponentModel.DataAnnotations;

namespace DTOs.Membership
{
    public class RegisterMemberShipDto
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        public int MembershipId { get; set; }
        [Required]
        public string ReturnUrl { get; set; }
    }
}
