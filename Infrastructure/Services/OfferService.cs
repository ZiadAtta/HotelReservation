using Ecom.core.Interfaces;
using HotelReservation.Core;
using HotelReservation.Core.DTOs.FacilityDTOs;
using HotelReservation.Core.DTOs.OfferDTOs;
using HotelReservation.Core.DTOs.RoomDTOs;
using HotelReservation.Core.Entities;
using HotelReservation.Core.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infrastructure.Services
{
    public class OfferService :IOfferService
    {
        private readonly IGenericRepository<Room> _roomRepo;
        private readonly IGenericRepository<Offer> _offerRepo;

        public OfferService(IGenericRepository<Room> roomRepo,
            IGenericRepository<Offer> offerRepo) 
        {
            _roomRepo = roomRepo;
            _offerRepo = offerRepo;
        }
        public async Task<bool> CreateOffer(CreateOfferDTO createFacilityDTO)
        {
            var room =  await _roomRepo.GetByIdAsync(createFacilityDTO.RoomId);
            if (room == null)
            {
                return false;
            }

            if(room.PricePerNight < createFacilityDTO.Discount)
            {
                return false; 
            }
            var offer = new Offer
            {
                RoomId = createFacilityDTO.RoomId,
                Discount = createFacilityDTO.Discount,
                IsActive = createFacilityDTO.IsActive
            };
            await _offerRepo.AddAsync(offer);
            await _offerRepo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteFacility(DeleteOfferDTO model)
        {
            var offer = await _offerRepo.GetByIdAsync(model.OfferId);
            if (offer == null)
            {
                return false;
            }
            await _offerRepo.DeleteAsync(model.OfferId);
            await _offerRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateFacility(UpdateOfferDTO updateOfferDTO)
        {
            var room = await _roomRepo.GetByIdAsync(updateOfferDTO.RoomId);
            if (room == null)
            {
                return false;
            }

            if (room.PricePerNight < updateOfferDTO.Discount)
            {
                return false;
            }

            var offer = await _offerRepo.GetByIdAsync(updateOfferDTO.OfferId);
            if (offer == null)
            {
                return false;
            }

            offer.RoomId = updateOfferDTO.RoomId;
            offer.Discount = updateOfferDTO.Discount;
            offer.IsActive = updateOfferDTO.IsActive;

            await _offerRepo.UpdateAsync(updateOfferDTO.OfferId, offer);
            await _offerRepo.SaveChangesAsync();

            return true;
        }
        public async Task<List<GetOfferDTO>> GetOffersByRoomId(RoomPaginationDTO model)
        {
            var offerrooms = _offerRepo.GetAll()
                .Include(x=>x.Room)
                .Select(o => new GetOfferDTO
                {
                    RoomId = o.RoomId,
                    Price = o.Room.PricePerNight,
                    capacity = o.Room.Capacity,
                    Name = o.Room.RoomNumber,
                    OfferId = o.Id,
                    Discount = o.Discount,
                    IsActive = o.IsActive,
                });

            return await Pagination<GetOfferDTO>.ToPagedList(offerrooms, model.PageNumber, model.PageSize);

        }

        public async Task<List<GetOfferDTO>> SearchByName(SearchRoomDTO model)
        {
            var offerrooms = _offerRepo.GetAll()
                .Include(x => x.Room)
                .Where(x=>x.Room.RoomNumber == model.Name)
                .Select(o => new GetOfferDTO
                {
                    RoomId = o.RoomId,
                    Price = o.Room.PricePerNight,
                    capacity = o.Room.Capacity,
                    Name = o.Room.RoomNumber,
                    OfferId = o.Id,
                    Discount = o.Discount,
                    IsActive = o.IsActive,
                });

            return await Pagination<GetOfferDTO>.ToPagedList(offerrooms, model.PageNumber, model.PageSize);

        }
    }
}
