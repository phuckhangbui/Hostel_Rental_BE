using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RoomService
{
    public class UpdateRoomServicesPriceRequest
    {
        public int RoomId { get; set; }
        public List<RoomServiceUpdatePriceDto> Services { get; set; }
    }
}
