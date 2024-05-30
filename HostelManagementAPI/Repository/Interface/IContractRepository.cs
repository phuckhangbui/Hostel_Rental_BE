using BusinessObject.Models;


namespace Repository.Interface
{
    public interface IContractRepository
    {
        Task<bool> CreateContract(Contract contract);
        Task<IEnumerable<Contract>> GetContractsAsync();
        Task<Contract> GetContractById(int id);
        Task UpdateContract(Contract contract);

    }
}
