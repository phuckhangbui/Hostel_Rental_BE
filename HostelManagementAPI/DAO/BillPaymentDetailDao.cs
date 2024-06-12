using BusinessObject.Models;

namespace DAO
{
    public class BillPaymentDetailDao : BaseDAO<BillPaymentDetail>
    {
        private static BillPaymentDetailDao instance = null;
        private static readonly object instacelock = new object();

        public BillPaymentDetailDao()
        {
        }

        public static BillPaymentDetailDao Instance
        {
            get
            {
                lock (instacelock)
                {
                    if (instance == null)
                    {
                        instance = new BillPaymentDetailDao();
                    }
                    return instance;
                }

            }
        }


    }
}
