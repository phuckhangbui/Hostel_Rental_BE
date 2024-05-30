using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Account
{
    public class CustomerViewAccount
    {
        public string? Username { get; set; }
        public int? RoleId { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? CitizenCard { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Status { get; set; }
    }
}
