namespace DTOs.Account
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public int? RoleId { get; set; }
        public string? Token { get; set; }
        public int? Status { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsLoginWithGmail { get; set; }
        public bool? IsNewAccount { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
