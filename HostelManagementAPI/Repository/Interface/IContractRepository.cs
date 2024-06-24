using DTOs.Contract;


namespace Repository.Interface
{
    public interface IContractRepository
    {
        Task<IEnumerable<GetContractDto>> GetContractsAsync();
        Task UpdateContract(int id, UpdateContractDto contract);
        Task<int> CreateContract(CreateContractDto contract);
        Task<GetContractDto> GetContractById(int id);
        Task<IEnumerable<GetContractDto>> GetContractByOwnerId(int ownerId);
        Task<IEnumerable<GetContractDto>> GetContractByStudentId(int studentId);
        Task<GetContractDto> GetContractDetailsByContractId(int contractId);
        Task UpdateContract(GetContractDto getContractDto);
        Task AddContractMember(CreateListContractMemberDto contractMemberDto);
        Task<GetContractDto> GetCurrentContractByRoom(int roomId);
    }
}
