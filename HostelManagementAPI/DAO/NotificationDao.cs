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
    }
}
