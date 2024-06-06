//using BusinessObject.Models;
//using DTOs.Service;
//using Repository.Interface;
//using Service.Interface;

//namespace Service.Implement
//{
//    public class ServiceService : IServiceService
//    {   private readonly IServiceRepository _serviceRepository;
//        public ServiceService(IServiceRepository serviceRepository)
//        {
//            _serviceRepository = serviceRepository;
//        }        

//        public Task<bool> CreateService(CreateServiceDto createService)
//        {
//            Services service = new Services
//            {
//                ServiceName = createService.ServiceName,
//                ServicePrice = createService.ServicePrice,
//                TypeServiceID = createService.TypeServiceID
//            };
//            return _serviceRepository.CreateService(service);
//        }        

//        public async Task<bool> UpdateService(UpdateServiceDto updateService)
//        {
//            Services service = await _serviceRepository.GetServiceById(updateService.ServiceID);
//            if (service == null)
//            {
//                return false;
//            }
//            else
//            {
//                service.ServiceName = updateService.ServiceName;
//                service.ServicePrice = updateService.ServicePrice;
//                service.TypeServiceID = updateService.TypeServiceID;
//                return await _serviceRepository.UpdateService(service);
//            }            
//        }

//        public Task<Services> GetServiceById(int id)
//        {
//            return _serviceRepository.GetServiceById(id);
//        }

//        public async Task<bool> CheckServiceExist(int id)
//        {
//            var service = await _serviceRepository.GetServiceById(id);
//            if(service == null)
//            {
//                return false;
//            }
//            return true;
//        }

//        public async Task<List<ServiceResponseDto>> GetServices()
//        {
//            List<ServiceResponseDto> services = await _serviceRepository.GetServices();
            
//            return services;
//        }

//        public async Task RemoveService(int serviceId)
//        {
//           await _serviceRepository.RemoveServiceAsync(serviceId);
//        }
//    }
//}

//        public async Task<bool> CheckServiceExist(int id)
//        {
//            var service = await _serviceRepository.GetServiceById(id);
//            if(service == null)
//            {
//                return false;
//            }
//            return true;
//        }
//    }
//}
