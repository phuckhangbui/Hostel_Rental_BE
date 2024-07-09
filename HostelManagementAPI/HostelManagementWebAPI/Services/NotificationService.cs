using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Repository.Interface;

namespace HostelManagementWebAPI.Services
{
    public class NotificationService : NotificationGRPCService.NotificationGRPCServiceBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }


        public override async Task<Notifications> GetNotifications(NotificationFilter requestData, ServerCallContext context)
        {
            try
            {
                var list = await _notificationRepository.GetAllNotifications();

                list = list.Where(n => n.ReceiveAccountId == requestData.AccountId).ToList();

                var response = new Notifications();
                response.Items.AddRange(list.Select(notification => new Notification
                {
                    NotificationId = notification.NotificationId.HasValue == true ? (int)notification.NotificationId.Value : 0,
                    AccountNoticeId = notification.AccountNoticeId.HasValue == true ? (int)notification.AccountNoticeId.Value : 0,
                    ReceiveAccountId = notification.ReceiveAccountId.HasValue == true ? (int)notification.ReceiveAccountId.Value : 0,
                    NotificationText = notification.NotificationText, // No null check needed (string can handle null)
                    CreateDate = notification.CreateDate.HasValue ? Timestamp.FromDateTime(DateTime.SpecifyKind(notification.CreateDate.Value, DateTimeKind.Utc)) : null,
                    NotificationType = notification.NotificationType.HasValue == true ? (int)notification.NotificationType.Value : 0,
                    Title = notification.Title, // No null check needed (string can handle null)
                    ForwardToPath = notification.ForwardToPath, // No null check needed (string can handle null)
                    IsRead = notification.IsRead ?? false,
                }));

                // Return the response directly (no need for Task.FromResult)
                return response;
            }
            catch
            {
                return new Notifications();
            }

        }

        public override async Task<Empty> UpdateNotificationStatus(UpdateNotification requestData, ServerCallContext context)
        {
            try
            {
                await _notificationRepository.UpdateNotificationStatus(requestData.NotificationId);
                return new Empty();
            }
            catch
            {
                return new Empty();
            }
        }
    }
}
