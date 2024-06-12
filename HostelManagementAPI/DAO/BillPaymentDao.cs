using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class BillPaymentDao : BaseDAO<BillPayment>
    {
        private static BillPaymentDao instance = null;
        private static readonly object instacelock = new object();

        public BillPaymentDao()
        {
        }

        public static BillPaymentDao Instance
        {
            get
            {
                lock (instacelock)
                {
                    if (instance == null)
                    {
                        instance = new BillPaymentDao();
                    }
                    return instance;
                }

            }
        }

        public async Task<BillPayment> GetCurrentBillPayment(int contractId, int month, int year)
        {
            using (var context = new DataContext())
            {
                return await context.BillPayment
                    .FirstOrDefaultAsync(b => b.ContractId == contractId && b.Month == month && b.Year == year);
            }
        }

        public async Task<BillPaymentDetail> GetLastBillPaymentDetail(int roomServiceId)
        {
            using (var context = new DataContext())
            {
                return await context.BillPaymentDetail
                    .Where(d => d.RoomServiceID == roomServiceId)
                    .Include(d => d.RoomService)
                    .Include(d => d.RoomService.TypeService)
                    .OrderByDescending(d => d.BillPayment.CreatedDate)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<BillPayment> GetLastBillPayment(int contractId)
        {
            using (var context = new DataContext())
            {
                return await context.BillPayment
                    .Where(b => b.ContractId == contractId)
                    .OrderByDescending(d => d.CreatedDate)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<BillPayment> GetBillPayment(int id)
        {
            DataContext context = new DataContext();

            return await context.BillPayment.FindAsync(id);
        }

        public async Task<BillPayment> GetBillPaymentByTnxRef(string tnxRef)
        {
            DataContext context = new DataContext();

            return await context.BillPayment.FirstOrDefaultAsync(b => b.TnxRef == tnxRef);
        }

        public async Task<IEnumerable<BillPayment>> GetBillPaymentByContractId(int contractId)
        {
            using (var context = new DataContext())
            {
                return await context.BillPayment
                    .Where(b => b.ContractId == contractId)
                    .ToListAsync();
            }
        }
    }
}
