namespace BusinessObject.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public int? RoleId { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? CitizenCard { get; set; }
        public int? Gender { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Status { get; set; }
        public int? PackageStatus { get; set; }
        public bool? IsLoginWithGmail { get; set; }
        public string? OtpToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime?
            RefreshTokenExpiryTime
        { get; set; }
        public string? FirebaseToken { get; set; }


        public IList<Hostel> Hostels { get; set; }
        public IList<MemberShipRegisterTransaction> Memberships { get; set; }
        public IList<Complain> AccountComplain { get; set; }
        public IList<Notification> AccountNotice { get; set; }
        public IList<Notification> AccountNoticeReceive { get; set; }
        public IList<Contract> OwnerContract { get; set; }
        public IList<Contract> StudentContract { get; set; }
        public IList<RoomAppointment> Appointments { get; set; }

        public IList<BillPayment> BillPaymentReceives { get; set; }
        public IList<BillPayment> BillPaymentPays { get; set; }
    }
}
