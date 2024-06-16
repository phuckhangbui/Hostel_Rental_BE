namespace DTOs.Account
{
    public class AccountLoginDto
    {
        public int AccountId { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public int? RoleId { get; set; }
        public string? Token { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsLoginWithGmail { get; set; }
        public int? PackageStatus { get; set; }
        public string? RefreshToken { get; set; }
    }
}
