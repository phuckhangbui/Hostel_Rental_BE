using DTOs.Contract;

namespace Service.Interface
{
    public interface IContractService
    {
        Task<IEnumerable<ContractDto>> GetContracts();
        Task UpdateContract(ContractDto ContractDto);

    }
}
