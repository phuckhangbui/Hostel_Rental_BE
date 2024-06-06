//using BusinessObject.Models;

//namespace DAO
//{
//    public class ServiceDao : BaseDAO<Services>
//    {
//        private static ServiceDao instance = null;
//        private static readonly object instacelock = new object();

//        private ServiceDao()
//        {
//        }

//        public static ServiceDao Instance
//        {
//            get
//            {
//                lock (instacelock)
//                {
//                    if (instance == null)
//                    {
//                        instance = new ServiceDao();
//                    }
//                    return instance;
//                }

//            }
//        }

//        public async Task<Services> GetServiceById(int id)
//        {
//            Services service = null;
//            using (var context = new DataContext())
//            {
//                service = context.Service.FirstOrDefault(x => x.ServiceID == id);
//            }
//            return service;
//        }
//    }
//}
