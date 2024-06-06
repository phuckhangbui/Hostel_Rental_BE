using BusinessObject.Models;
using DTOs.Service;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class ServiceDao : BaseDAO<Services>
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

        public async Task<Services> GetServiceById(int id)
        {
            Services service = null;
            using (var context = new DataContext())
            {
                service = context.Service.FirstOrDefault(x => x.ServiceID == id);
            }
            return service;
        }

        public async Task<List<ServiceResponseDto>> GetAllServicesAsync()
        {
            using (var context = new DataContext())
            {
                return await context.Service
                    .Select(service => new ServiceResponseDto
                    {
                        ServiceID = service.ServiceID,
                        TypeServiceID = service.TypeServiceID,
                        ServiceName = service.ServiceName,
                        ServicePrice = service.ServicePrice
                    })
                    .ToListAsync();
            }
        }

        public async Task RemoveService(int serviceId)
        {
            var context = new DataContext();
            var service = await context.Service.FirstOrDefaultAsync(s => s.ServiceID == serviceId);
            if (service != null)
            {
                context.Service.Remove(service);
                await context.SaveChangesAsync();
            }
        }
    }
}
