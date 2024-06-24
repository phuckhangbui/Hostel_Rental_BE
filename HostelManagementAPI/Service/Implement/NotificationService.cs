using DTOs;
using Repository.Interface;
using Service.Interface;

namespace Service.Implement
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<List<NotificationDto>> GetAllNotifications()
        {
            return await _notificationRepository.GetAllNotifications();
        }
        public async Task CreateNotification(NotificationDto notificationDto)
        {
            await _notificationRepository.CreateNotification(notificationDto);
        }

        public async Task<List<NotificationDto>> GetNotificationsBaseOnReceiveId(int accountId)
        {
            var list = await _notificationRepository.GetAllNotifications();

            return list.Where(n => n.ReceiveAccountId == accountId).ToList();
        }
    }
}
