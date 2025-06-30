using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Core.Entities;
using HotelReservation.Core.Enum;

namespace HotelReservation.Core.Dtos.Reservation;
public class ReservationDto
{
    [Required]
    public int Id { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfGuests { get; set; } 
    public decimal TotalPrice { get; set; } 
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  
    public string CustomerId { get; set; }

    [StringLength(100, ErrorMessage = "Customer name can't be more than 100 characters")]
    public string CustomerName { get; set; }
    public int RoomId { get; set; }
    public int? OfferId { get; set; }
    public int? PaymentId { get; set; }
}
 

