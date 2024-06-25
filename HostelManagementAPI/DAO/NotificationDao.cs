using BusinessObject.Models;

namespace DAO
{
    public class NotificationDao : BaseDAO<Notification>
    {
        private static NotificationDao instance = null;
        private static readonly object instacelock = new object();

        public NotificationDao()
        {
        }

        public static NotificationDao Instance
        {
            get
            {
                lock (instacelock)
                {
                    if (instance == null)
                    {
                        instance = new NotificationDao();
                    }
                    return instance;
                }

            }
        }

        //public static Task<Notification> GetNotificationsBaseOnReceiveId(int accountId)
        //{
        //    Notification response = new Notification()
        //}

        public async Task UpdateNotificationToRead(int id)
        {
            var dataContext = new DataContext();

            var notification = await dataContext.Notifications.FindAsync(id);

            if (notification != null)
            {

                notification.IsRead = true;

                await this.UpdateAsync(notification);
            }
        }
    }
}
