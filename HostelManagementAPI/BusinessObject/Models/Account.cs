namespace BusinessObject.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? CitizenCard { get; set; }
        public string? Username { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }
        public int RoleID { get; set; }
        public int Status { get; set; }
        public string? FirebaseToken { get; set; }

        public IEnumerable<Hostel> Hostels { get; set; }
        public IEnumerable<MemberShipRegisterTransaction> Memberships { get; set; }
        public IEnumerable<Complain> AccountComplain { get; set; }
        public IEnumerable<Notice> AccountNotice { get; set; }
        public IEnumerable<Notice> AccountNoticeReceive { get; set; }
        public IEnumerable<Contract> OwnerContract { get; set; }
        public IEnumerable<Contract> StudentContract { get; set; }
        public IEnumerable<ContractMember> contractMembers { get; set; }
    }
}
