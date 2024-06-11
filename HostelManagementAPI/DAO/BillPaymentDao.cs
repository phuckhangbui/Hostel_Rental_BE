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
    }
}
