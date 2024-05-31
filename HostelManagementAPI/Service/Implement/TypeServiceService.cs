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
            await typeServiceRepository.CreateTypeService(createTypeServiceDto);
        }

        public async Task<IEnumerable<ViewAllTypeServiceDto>> GetAllTypeService()
        {
            return await typeServiceRepository.GetTypeServices();
        }

        public async Task<bool> UpdateTypeServiceName(UpdateTypeServiceDto updateTypeServiceDto)
        {
            return await typeServiceRepository.UpdateTypeService(updateTypeServiceDto);
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

        public async Task<bool> CheckTypeServiceNameExist(string typeServiceName)
        {
            return await typeServiceRepository.CheckTypeServiceNameExist(typeServiceName);
        }
    }
}