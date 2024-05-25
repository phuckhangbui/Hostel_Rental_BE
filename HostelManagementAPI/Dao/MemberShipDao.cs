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
        private static readonly object instacelock = new object();

        public MemberShipDao()
        {
        }

        public static MemberShipDao Instance
        {
            get
            {
                lock (instacelock)
                {
                    if (instance == null)
                    {
                        instance = new MemberShipDao();
                    }
                    return instance;
                }
                
            }
        }

        public MemberShip GetMemberShipById(int id)
        {
            MemberShip memberShip = null;
            using (var context = new DataContext())
            {
                memberShip = context.Membership.FirstOrDefault(x => x.MemberShipID == id);
            }
            return memberShip;
        }
    }
}
