using DTOs;

namespace Repository.Interface
{
    public interface INotificationRepository
    {
        Task<List<NotificationDto>> GetAllNotifications();
        Task<NotificationDto> CreateNotification(NotificationDto notificationDto);
        Task UpdateNotificationStatus(int id);
    }
}
