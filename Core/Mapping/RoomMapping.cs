
using AutoMapper;
using HotelReservation.Core.Dtos.Reservation;
using HotelReservation.Core.Entities;
namespace HotelReservation.Core.Mapping
{
    public class RoomMapping : Profile
    {
        public RoomMapping()
        {            
          CreateMap<CreateDto, Reservation>();
          CreateMap<Reservation, ReservationDto>();

            CreateMap<UpdateDto, Reservation>();


        }
    }
}

