using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Account
{
    public class ProfileDto
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CitizenCard { get; set; }
        public int Gender { get; set; }
        public int Status { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime DateExpire { get; set; }
        public string PackName { get; set; }
        public int CapacityHostel { get; set; }
        public double FeePackage { get; set; }
    }
}
