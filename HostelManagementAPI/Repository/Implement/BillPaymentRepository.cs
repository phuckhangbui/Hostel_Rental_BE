using BusinessObject.Models;
using DAO;
using Repository.Interface;

namespace Repository.Implement
{
    public class BillPaymentRepository : IBillPaymentRepository
    {
        public async Task CreateBillPaymentMonthly(BillPayment billPayment)
        {
            await BillPaymentDao.Instance.CreateAsync(billPayment);
        }

        public async Task<BillPayment> GetCurrentMonthBillPayment(int contractId, int month, int year)
        {
            
            var currentBillPayment = await BillPaymentDao.Instance.GetCurrentBillPayment(contractId, month, year);
            if (currentBillPayment != null)
            {
                return currentBillPayment;
            }

            return null;
        }

        public async Task<BillPaymentDetail> GetLastBillPaymentDetail(int roomServiceId)
        {
            return await BillPaymentDao.Instance.GetLastBillPaymentDetail(roomServiceId);
        }
    }
}
