namespace Service.Interface
{
    public interface IPaymentService
    {
        Task<int> GetPaymentTypeByTnxRef(string tnxRef);
    }
}
