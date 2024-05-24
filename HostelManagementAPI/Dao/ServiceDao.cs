using BusinessObject.Models;

namespace DAO
{
    public class ServiceDao : BaseDAO<Service>
    {
        private static ServiceDao instance = null;
        private static readonly object instacelock = new object();

        private ServiceDao()
        {
        }

        public static ServiceDao Instance
        {
            get
            {
                lock (instacelock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceDao();
                    }
                    return instance;
                }

            }
        }
    }
}
