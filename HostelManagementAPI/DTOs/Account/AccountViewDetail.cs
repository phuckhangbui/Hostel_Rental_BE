namespace DTOs.Account
{
    public class AccountViewDetail
    {
        public int AccountID { get; set; }
        public int? RoleId { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int? Gender { get; set; }
        public string? CitizenCard { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Status { get; set; }
    }
}
