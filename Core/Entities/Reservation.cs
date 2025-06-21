using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Core.Entities.Enums;

namespace HotelReservation.Core.Entities;
public class Reservation : BaseModel
{
    public int ID { get; set; }
    public int RoomID { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public string GuestEmail { get; set; } = string.Empty;
    public string GuestPhone { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; } = 0.0m;
    public ReservationStatus ReservationStatus { get; set; }
    public bool IsConfirmed { get; set; } = false;
    //check it
  
}
