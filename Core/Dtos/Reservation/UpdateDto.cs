using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Core.Enum;

namespace HotelReservation.Core.Dtos.Reservation;
public class UpdateDto
{

    [Required(ErrorMessage = "Check-in date is required")]
    public DateTime CheckInDate { get; set; }

    [Required(ErrorMessage = "Check-out date is required")]
    public DateTime CheckOutDate { get; set; }

    [Range(1, 20, ErrorMessage = "Number of guests must be between 1 and 20")]
    public int NumberOfGuests { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Total price must be non-negative")]
    public decimal TotalPrice { get; set; }

    [Required]
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Customer ID is required")]
    public string CustomerId { get; set; }


    [Required(ErrorMessage = "Room ID is required")]
    public int RoomId { get; set; }
    public int OfferId { get; set; }
    public int PaymentId { get; set; }

}
