using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Account
{
    public class AccountUpdate
    {
        public int AccountID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CitizenCard { get; set; }
        public string Address { get; set; }
    }
}
