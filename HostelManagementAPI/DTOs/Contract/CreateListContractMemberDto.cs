using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Contract
{
    public class CreateListContractMemberDto
    {
        public int ContractID { get; set; }
        public List<CreateContractMemberDto> Members { get; set; }
    }
}
