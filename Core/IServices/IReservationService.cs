using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Core.Dtos.Reservation;

namespace HotelReservation.Core.IServices;
public interface IReservationService
{
    Task<IEnumerable<ReservationDto>> GetAllAsync();
    Task<ReservationDto> GetByIdAsync(int id);
    Task<ReservationDto> CreateAsync(CreateDto createDto);
    Task<ReservationDto> UpdateAsync(int id,UpdateDto updateDto);
    Task<bool> DeleteAsync(int id);

    //Task<IEnumerable<ReservationDto>> GetByCustomerIdAsync(string customerId);

    //Task<IEnumerable<ReservationDto>> GetByStatusAsync(ReservationStatus status);
}
