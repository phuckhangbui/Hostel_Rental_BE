using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }
        public Account? AccountNotice { get; set; }
        public int? AccountNoticeId { get; set; }
        public Account? ReceiveAccount { get; set; }
        public int? ReceiveAccountId { get; set; }
        public string? NotificationText { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? NotificationType { get; set; }
        public string? Title { get; set; }
        public string? ForwardToPath { get; set; }
        public bool? IsRead { get; set; }
    }
}
