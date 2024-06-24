namespace DTOs
{
    public class NotificationDto
    {
        public int? NotificationId { get; set; }
        public int? AccountNoticeId { get; set; }
        public int? ReceiveAccountId { get; set; }
        public string? NotificationText { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? NotificationType { get; set; }
        public string? Title { get; set; }
        public string? ForwardToPath { get; set; }
        public bool? IsRead { get; set; }
    }
}
