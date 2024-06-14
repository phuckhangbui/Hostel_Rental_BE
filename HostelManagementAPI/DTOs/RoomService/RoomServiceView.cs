using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RoomService
{
    public class RoomServiceView
    {
        public int? RoomServiceId { get; set; }
        public int? TypeServiceID { get; set; }
        public string TypeName { get; set; }
        public string Unit { get; set; }
        public double? Price { get; set; }
        public int Status { get; set; }
    }
}
