using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }
        public Account? AccountNotice { get; set; }
        public int? AccountID { get; set; }
        public Account? ReceiveAccount { get; set; }
        public string? NotificationText { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? NotificationType { get; set; }
        public string? Title { get; set; }

    }
}
