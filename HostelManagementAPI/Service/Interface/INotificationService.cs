using DTOs;

namespace Service.Interface
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> GetAllNotifications();
        Task CreateNotification(NotificationDto notificationDto);
    }
}
