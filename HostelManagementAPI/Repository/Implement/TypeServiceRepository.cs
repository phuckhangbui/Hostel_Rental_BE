using AutoMapper;
using BusinessObject.Models;
using DAO;
using DTOs.Membership;
using DTOs.TypeService;
using Repository.Interface;

namespace Repository.Implement
{
    public class TypeServiceRepository : ITypeServiceRepository
    {
        private readonly IMapper _mapper;

        public TypeServiceRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<bool> CheckTypeServiceNameExist(string typeServiceName)
        {
            return await TypeServiceDao.Instance.CheckTypeServiceNameExist(typeServiceName);
        }

        public async Task<bool> CreateTypeService(CreateTypeServiceDto createTypeServiceDto)
        {
            var typeService = _mapper.Map<TypeService>(createTypeServiceDto);
            return await TypeServiceDao.Instance.CreateAsync(typeService);
        }

        public Task DeleteTypeService(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TypeService> GetTypeServiceById(int id)
        {
            return TypeServiceDao.Instance.GetTypeServiceById(id);
        }

        public async Task<IEnumerable<ViewAllTypeServiceDto>> GetTypeServices()
        {
            var typeService = await TypeServiceDao.Instance.GetAllAsync();
            return _mapper.Map<IEnumerable<ViewAllTypeServiceDto>>(typeService);
        }

        public async Task<bool> UpdateTypeService(UpdateTypeServiceDto updateTypeServiceDto)
        {
            var typeService = _mapper.Map<TypeService>(updateTypeServiceDto);
            return await TypeServiceDao.Instance.UpdateAsync(typeService);
        }
    }
}
