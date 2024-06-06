using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Contract
{
    public class GetContractDetailsDto 
    {
        public int ContractMemberID { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? CitizenCard { get; set; }
    }
}
