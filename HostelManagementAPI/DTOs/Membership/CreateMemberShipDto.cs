using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Membership
{
    public class CreateMemberShipDto
    {
        public string MemberShipName { get; set; }
        public int CapacityHostel { get; set; }
        public int Month { get; set; }
        public double MemberShipFee { get; set; }
    }
}
