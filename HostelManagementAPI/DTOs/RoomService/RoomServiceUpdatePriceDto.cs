using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RoomService
{
    public class RoomServiceUpdatePriceDto
    {
        public int TypeServiceId { get; set; }
        public double? Price { get; set; }
    }
}
