using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Core.Entities.Enums;

namespace HotelReservation.Core.Entities;
public class Room : BaseModel
{
    public int ID { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0.0m;
    public int Capacity { get; set; }
    public RoomType Type { get; set; }
    public RoomStatus Status { get; set; } = RoomStatus.Available;
    public bool IsAvailable { get; set; } = true;
    //check it ana mzwd roomstatus  
    public int RoomFacilityID { get; set; }
    public int ReservationsID { get; set; }


    public ICollection<RoomFacility> Facilities { get; set; } = [];
    public ICollection<Reservation> Reservations { get; set; } = [];
}
