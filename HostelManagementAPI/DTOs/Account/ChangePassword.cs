using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Account
{
    public class ChangePassword
    {
        public int AccountID { get; set; }
        public string Password { get; set; }
    }
}
