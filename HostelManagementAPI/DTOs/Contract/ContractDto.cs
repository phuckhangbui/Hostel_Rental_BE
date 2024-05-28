using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Contract
{
    public class ContractDTO
    {
        public int ContractID { get; set; }
        public int? AccountID { get; set; }
        public int? RoomID { get; set; }
        public string? ContractTerm { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? DateSign { get; set; }
        public int Status { get; set; }
    }
}
