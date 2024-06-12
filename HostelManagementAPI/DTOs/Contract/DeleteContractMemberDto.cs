using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Contract
{
    public class DeleteContractMemberDto
    {
        public int ContractID { get; set; }
        public List<int> ContractMemberIDs { get; set; }
    }
}
