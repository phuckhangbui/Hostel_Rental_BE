using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DTOs.MemberShipRegisterTransaction
{
    public class ViewMemberShipDetailDto
    {
        public int AccountID {  get; set; }
        public string Name {  get; set; }
        public string Phone {  get; set; }
        public string Address {  get; set; }
        public string Email {  get; set; }
        public string MembershipName {  get; set; }
        public int CapacityHostel { get; set; }
        public int Month {  get; set; }
        public double PackageFee {  get; set; }
        public DateTime DateRegister{  get; set; }
        public DateTime DateExpire{  get; set; }
        public int Status{  get; set; }
    }
}
