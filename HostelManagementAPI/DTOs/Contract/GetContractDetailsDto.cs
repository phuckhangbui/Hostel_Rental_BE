using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Contract
{
    public class GetContractDetailsDto 
    {
        public int ServiceID { get; set; }
        public string? ServiceName { get; set; }
        public double? ServicePrice { get; set; }
    }
}
