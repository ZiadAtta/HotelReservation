using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.core.Interfaces;
using HotelReservation.Core.Dtos.Reservation;
using HotelReservation.Core.Entities;
using HotelReservation.Core.IServices;

namespace HotelReservation.Core.Services;
public class ReservationService(
    IGenericRepository<Reservation> genericRepository, IMapper mapper) : IReservationService
{
    private readonly IGenericRepository<Reservation> _genericRepository = genericRepository;
    private readonly IMapper _mapper = mapper;


    public async Task<ReservationDto> CreateAsync(CreateDto createDto)
    {
        var newReservation = _mapper.Map<Reservation>(createDto);
        await _genericRepository.AddAsync(newReservation);
        var response = _mapper.Map<ReservationDto>(newReservation);
        return response;

    }

    public async Task<bool> DeleteAsync(int id)
    {
        var isExists =await  _genericRepository.GetByIdAsync(id);
        if (isExists == null)
        {
            return false;
        }
       await  _genericRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<ReservationDto>> GetAllAsync()
    {

        var reservations = await _genericRepository
            .GetAllAsync(x => x.Customer);

        var response = _mapper.Map<IEnumerable<ReservationDto>>(reservations);

        return response;

    }

    public async Task<ReservationDto> GetByIdAsync(int id)
    {
        var isexists = await _genericRepository.GetByIdAsync(id, x => x.Customer);
        if (isexists == null)
        {
            return null;
        }
        var response = _mapper.Map<ReservationDto>(isexists);
        return response;

    }

    public async Task<ReservationDto> UpdateAsync(int id, UpdateDto updateDto)
    {

        var reservation = await _genericRepository.GetByIdAsync(id);
        if (reservation == null)
        {
            return null;
        }
        _mapper.Map(updateDto, reservation);
       //update async will compare between old and new values
        await _genericRepository.UpdateAsync(id, reservation);
        var response = _mapper.Map<ReservationDto>(reservation);
        return response;
    }



}



