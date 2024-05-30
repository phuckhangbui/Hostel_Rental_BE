using BusinessObject.Models;
using DTOs.TypeService;
using Repository.Interface;
using Service.Interface;

namespace Service.Implement
{
    public class TypeServiceService : ITypeServiceService
    {
        public ITypeServiceRepository typeServiceRepository;

        public TypeServiceService(ITypeServiceRepository typeServiceRepository)
        {
            this.typeServiceRepository = typeServiceRepository;
        }

        public async Task CreateTypeService(CreateTypeServiceDto createTypeServiceDto)
        {
            TypeService typeService = new TypeService
            {
                TypeName = createTypeServiceDto.TypeName,
            };
            await typeServiceRepository.CreateTypeService(typeService);
        }

        public async Task<IEnumerable<TypeService>> GetAllTypeService()
        {
            return await typeServiceRepository.GetTypeServices();
        }

        public async Task<bool> UpdateTypeServiceName(UpdateTypeServiceDto updateTypeServiceDto)
        {
            var typeService = await typeServiceRepository.GetTypeServiceById(updateTypeServiceDto.TypeServiceID);
            if (typeService == null)
            {
                return false;
            }

            typeService.TypeName = updateTypeServiceDto.TypeServiceName;
            return await typeServiceRepository.UpdateTypeService(typeService);
        }

        public async Task<bool> CheckExistTypeService(int TypeServiceId)
        {
            var typeService = await typeServiceRepository.GetTypeServiceById(TypeServiceId);
            if (typeService == null)
            {
                return false;
            }

            return true;
        }
    }
}