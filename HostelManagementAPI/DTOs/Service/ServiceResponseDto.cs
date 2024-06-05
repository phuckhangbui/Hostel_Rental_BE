using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Service
{
    public class ServiceResponseDto
    {
        public int ServiceID { get; set; }
        public int? TypeServiceID { get; set; }
        public string? ServiceName { get; set; }
        public double? ServicePrice { get; set; }
    }
}
