using DTOs.Contract;

namespace Service.Interface
{
    public interface IContractService
    {
        Task<IEnumerable<GetContractDto>> GetContracts();
        Task UpdateContract(int id, UpdateContractDto contractDto);
        Task CreateContract(CreateContractDto contractDto);
        //Task ChangeContractStatus(int contractId, int status);
        Task<IEnumerable<GetContractDto>> GetContractsByOwnerId(int ownerId);
        Task<IEnumerable<GetContractDto>> GetContractsByStudentId(int studentId);
        Task<GetContractDto> GetContractDetailByContractId(int contractId);

    }
}
