using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Core.Entities.Enums;

namespace HotelReservation.Core.Entities;
public class Payment
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    [Required]
    public string PaymentMethod { get; set; }
    public PaymentStatus Status { get; set; }
}
