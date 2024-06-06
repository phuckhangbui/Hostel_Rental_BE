//using BusinessObject.Models;
//using DAO;
//using Repository.Interface;

//namespace Repository.Implement
//{
//    public class ServiceRepository : IServiceRepository
//    {
//        public Task<bool> CreateService(Services service)
//        {
//            return ServiceDao.Instance.CreateAsync(service);
//        }

//        public Task<Services> GetServiceById(int id)
//        {
//            return ServiceDao.Instance.GetServiceById(id);
//        }

//        public Task<List<Services>> GetServices()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> UpdateService(Services service)
//        {
//            return ServiceDao.Instance.UpdateAsync(service);
//        }
//    }
//}
