using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class MemberShipDao : BaseDAO<MemberShip>
    {
        private static MemberShipDao instance = null;
        private readonly DataContext dataContext;

        public MemberShipDao()
        {
            dataContext = new DataContext();
        }

        public static MemberShipDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MemberShipDao();
                }
                return instance;
            }
        }
    }
}
