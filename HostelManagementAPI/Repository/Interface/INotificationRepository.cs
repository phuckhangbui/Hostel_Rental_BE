using DTOs;

namespace Repository.Interface
{
    public interface INotificationRepository
    {
        Task<List<NotificationDto>> GetAllNotifications();
        Task CreateNotification(NotificationDto notificationDto);
    }
}
