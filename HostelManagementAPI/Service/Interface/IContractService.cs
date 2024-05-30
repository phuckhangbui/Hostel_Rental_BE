using DTOs.Contract;

namespace Service.Interface
{
    public interface IContractService
    {
        Task<IEnumerable<GetContractDto>> GetContracts();
        Task UpdateContract(UpdateContractDto contractDto);
        Task CreateContract(CreateContractDto contractDto);
        Task ChangeContractStatus(int contractId, int status);
       
    }
}
