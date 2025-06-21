using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.Entities;
public class Feedback:BaseModel
{
    public int ID { get; set; }
    public int ReservationID { get; set; }
    public Reservation Reservation { get; set; } = null!;
    public string GuestName { get; set; } = string.Empty;
    public string GuestEmail { get; set; } = string.Empty;
    public string Comments { get; set; } = string.Empty;
    public int Rating { get; set; } 
  
}
