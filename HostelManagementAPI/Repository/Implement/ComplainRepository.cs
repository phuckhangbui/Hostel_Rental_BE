using AutoMapper;
using BusinessObject.Models;
using DAO;
using DTOs.Complain;
using Repository.Interface;

namespace Repository.Implement
{
    public class ComplainRepository : IComplainRepository
    {
        private readonly IMapper _mapper;

        public ComplainRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task CreateComplain(Complain complain)
        {
            await ComplainDao.Instance.CreateAsync(complain);
        }

        public async Task<Complain> GetComplainById(int id)
        {
            return await ComplainDao.Instance.GetComplainById(id);
        }

        public async Task<IEnumerable<ComplainDto>> GetComplains()
        {

            var complains = await ComplainDao.Instance.GetComplainWithOnwerId();
            var displayComplains = _mapper.Map<IEnumerable<ComplainDto>>(complains);

            return displayComplains;
        }

        public Task UpdateComplain(Complain complain)
        {
            return ComplainDao.Instance.UpdateAsync(complain);
        }
    }
}
