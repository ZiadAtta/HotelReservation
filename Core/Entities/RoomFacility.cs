using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.Entities;
public class RoomFacility
{
    public int ID { get; set; }
    public string Name { get; set; }=string.Empty;

    public ICollection<Room> Rooms { get; set; } = [];
}
