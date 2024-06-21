using DTOs.Complain;

namespace Service.Interface
{
    public interface IComplainService
    {
        Task CreateComplain(CreateComplainDto complainDto);
        Task<ComplainDto> GetComplainById(int id);
        Task<IEnumerable<ComplainDto>> GetComplains();
        Task<IEnumerable<ComplainDto>> GetComplainsByRoom(int id);
        Task<IEnumerable<ComplainDto>> GetComplainsByAccountCreator(int id);
        Task UpdateComplainStatus(UpdateComplainStatusDto updateComplainRequest);
    }
}
