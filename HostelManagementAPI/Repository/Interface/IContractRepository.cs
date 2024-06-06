using BusinessObject.Models;
using DTOs.Contract;


namespace Repository.Interface
{
    public interface IContractRepository
    {
        //Task<bool> CreateContract(CreateContractDto contract);
        Task<IEnumerable<Contract>> GetContractsAsync();
        //Task<Contract> GetContractById(int id);
        //Task UpdateContract(Contract contract);
        //Task<IEnumerable<Contract>> GetContractByOwnerId(int ownerId);
        //Task<IEnumerable<Contract>> GetContractByStudentId(int studentId);
        //Task<Contract> GetContractDetailsByContractId(int contractId);


    }
}
