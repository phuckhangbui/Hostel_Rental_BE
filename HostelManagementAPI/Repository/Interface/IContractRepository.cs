using BusinessObject.Models;
using DTOs.Contract;


namespace Repository.Interface
{
    public interface IContractRepository
    {
        Task<IEnumerable<GetContractDto>> GetContractsAsync();
        Task UpdateContract(int id, UpdateContractDto contract);
        Task<bool> CreateContract(CreateContractDto contract);
        Task<GetContractDto> GetContractById(int id);
        Task<IEnumerable<GetContractDto>> GetContractByOwnerId(int ownerId);
        Task<IEnumerable<GetContractDto>> GetContractByStudentId(int studentId);
        Task<GetContractDto> GetContractDetailsByContractId(int contractId);


    }
}
