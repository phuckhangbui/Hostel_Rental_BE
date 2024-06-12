using AutoMapper;
using BusinessObject.Models;
using DTOs.Complain;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;

namespace Service.Implement
{
    public class ComplainService : IComplainService
    {
        private readonly IComplainRepository _complainRepository;
        private readonly IMapper _mapper;

        public ComplainService(IMapper mapper, IComplainRepository complainRepository)
        {
            _mapper = mapper;
            _complainRepository = complainRepository;
        }

        public Task CreateComplain(CreateComplainDto complainDto)
        {
            var complain = _mapper.Map<Complain>(complainDto);
            complain.AccountID = complainDto.AccountID;
            complain.DateComplain = DateTime.Now;
            complain.DateUpdate = DateTime.Now;
            complain.Status = 1;

            return _complainRepository.CreateComplain(complain);
        }

        public async Task<ComplainDto> GetComplainById(int id)
        {
            var complain = await _complainRepository.GetComplainById(id);
            var displayComplain = _mapper.Map<ComplainDto>(complain);
            return displayComplain;
        }

        public async Task<IEnumerable<ComplainDto>> GetComplains()
        {
            var complains = await _complainRepository.GetComplains();

            var displayComplains = _mapper.Map<IEnumerable<ComplainDto>>(complains);

            return displayComplains;
        }

        public async Task<IEnumerable<ComplainDto>> GetComplainsByAccountCreator(int id)
        {
            var complains = await _complainRepository.GetComplains();
            var selectedComplains = complains.Where(c => c.AccountID == id);

            var displayComplains = _mapper.Map<IEnumerable<ComplainDto>>(selectedComplains);

            return displayComplains;
        }

        public async Task<IEnumerable<ComplainDto>> GetComplainsByRoom(int id)
        {
            var complains = await _complainRepository.GetComplains();
            var selectedComplains = complains.Where(c => c.RoomID == id);

            var displayComplains = _mapper.Map<IEnumerable<ComplainDto>>(selectedComplains);

            return displayComplains;
        }

        public async Task UpdateComplainStatus(UpdateComplainStatusDto updateComplainRequest)
        {
            var complain = await _complainRepository.GetComplainById(updateComplainRequest.ComplainId);
            if (complain == null)
            {
                throw new ServiceException("Complain not found");
            }

            complain.Status = updateComplainRequest.Status;
            complain.DateUpdate = DateTime.Now;
            await _complainRepository.UpdateComplain(complain);
        }
    }
}
