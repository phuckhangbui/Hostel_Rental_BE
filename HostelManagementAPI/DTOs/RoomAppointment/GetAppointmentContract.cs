using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RoomAppointment
{
    public class GetAppointmentContract
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public double? RoomFee { get; set; }
        public int Capacity { get; set; }
        public IEnumerable<AccountAppointment> AccountAppointments { get; set; }
    }
}
