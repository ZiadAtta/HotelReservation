using Core.Entities;
using Ecom.core.Interfaces;
using HotelReservation.Core;
using HotelReservation.Core.DTOs;
using HotelReservation.Core.DTOs.RoomDTOs;
using HotelReservation.Core.Entities;
using HotelReservation.Core.IServices;
using HotelReservation.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infrastructure.Services
{
    public class RoomService : IRoomService
    {
        private readonly IGenericRepository<Room> _roomRepo; 
        private readonly IGenericRepository<RoomFacility> _roomFacilityRepo;
        private readonly IGenericRepository<Facility> _facilityRepo;
        private readonly IGenericRepository<Offer> _offerRepo;
        private readonly IGenericRepository<Photo> _photoRepo;

        public RoomService(IGenericRepository<Room> roomRepo
            , IGenericRepository<RoomFacility> roomFacilityRepo
            , IGenericRepository<Facility> facilityRepo
            , IGenericRepository<Offer> offerRepo
            , IGenericRepository<Photo> photoRepo)
        {
            _roomRepo = roomRepo; 
            _roomFacilityRepo = roomFacilityRepo;
            _facilityRepo = facilityRepo;
            _offerRepo = offerRepo;
            _photoRepo = photoRepo;
        }
        public async Task<bool> CreateRoomAsync(CreateRoomDTO model)
        {
            if(model == null)
            {
                return false;
            }
            // adding the room 
            var room = new Room
            {
                RoomNumber = model.RoomNumber,
                PricePerNight = model.Price,
                Capacity = model.Capacity,
            };
            await _roomRepo.AddAsync(room);

            var photo = new Photo
            {
                ImageName = model.Image,
                RoomId = room.Id
            };
            await _photoRepo.AddAsync(photo);   

            // adding the ids of facilities 
            if (model.Facilities.Count > 0)
            {
                foreach (var i in model.Facilities)
                {
                    var facility = _facilityRepo.GetAll().Where(x => x.Id == i)
                        .Select(x => x.Id)
                        .FirstOrDefault();

                    if(facility != null)
                    {
                        var roomFacility = new RoomFacility
                        {
                            RoomId = room.Id,
                            FacilityId= i
                        };
                        await _roomFacilityRepo.AddAsync(roomFacility);
                    }
                    else
                    {
                        return false;
                    }

                }
            }

            var offer = new Offer
            {
                Discount = model.Discount,
                RoomId = room.Id
            };

            await _offerRepo.AddAsync(offer);

            await _roomRepo.SaveChangesAsync();


            return true;
        }
        public async Task<List<GetRoomDTO>> GetAllRoomsAsync(RoomPaginationDTO model)
        {
            var roomsQuery = _roomRepo.GetAll()
         .Include(r => r.Photos)
         .Include(r => r.Offer)
         .Include(r => r.RoomFacilities)
             .ThenInclude(rf => rf.Facility)
         .Select(r => new GetRoomDTO
         {
             Id = r.Id,
             RoomNumber = r.RoomNumber,
             Image = r.Photos.FirstOrDefault().ImageName,
             Price = r.PricePerNight,
             Discount = r.Offer != null ? r.Offer.Discount : 0,
             Tag = r.Capacity,
             Facilities = r.RoomFacilities.Select(rf => rf.Facility.Name).ToList()
         });

            return await Pagination<GetRoomDTO>.ToPagedList(roomsQuery, model.PageNumber, model.PageSize);
        }

        public async Task<List<GetRoomDTO>> SearchByNameAsync(SearchRoomDTO model)
        {

            var roomsQuery = _roomRepo.GetAll()
         .Include(r => r.Photos)
         .Include(r => r.Offer)
         .Include(r => r.RoomFacilities)
             .ThenInclude(rf => rf.Facility)
             .Where(r => r.RoomNumber.Contains(model.Name))
         .Select(r => new GetRoomDTO
         {
             Id = r.Id,
             RoomNumber = r.RoomNumber,
             Image = r.Photos.FirstOrDefault().ImageName,
             Price = r.PricePerNight,
             Discount = r.Offer != null ? r.Offer.Discount : 0,
             Tag = r.Capacity,
             Facilities = r.RoomFacilities.Select(rf => rf.Facility.Name).ToList()
         });

            return await Pagination<GetRoomDTO>.ToPagedList(roomsQuery, model.PageNumber, model.PageSize);
        }

        public async Task<List<GetRoomDTO>> FilterByTageAsync(FilterByTageDTO model)
        {

            var roomsQuery = _roomRepo.GetAll()
         .Include(r => r.Photos)
         .Include(r => r.Offer)
         .Include(r => r.RoomFacilities)
             .ThenInclude(rf => rf.Facility)
             .Where(r => r.Capacity == model.Tage)
         .Select(r => new GetRoomDTO
         {
             Id = r.Id,
             RoomNumber = r.RoomNumber,
             Image = r.Photos.FirstOrDefault().ImageName,
             Price = r.PricePerNight,
             Discount = r.Offer != null ? r.Offer.Discount : 0,
             Tag = r.Capacity,
             Facilities = r.RoomFacilities.Select(rf => rf.Facility.Name).ToList()
         });

            return await Pagination<GetRoomDTO>.ToPagedList(roomsQuery, model.PageNumber, model.PageSize);
        }

        public async Task<List<GetRoomDTO>> FilterByFacilityAsync(FilterByFacilityDTO model)
        {
            var roomsQuery = _roomRepo.GetAll()
                .Include(r => r.Photos)
                .Include(r => r.Offer)
                .Include(r => r.RoomFacilities)
                    .ThenInclude(rf => rf.Facility)
                .Where(r => r.RoomFacilities.Any(rf => rf.Facility.Name == model.FacilityName)) 
                .Select(r => new GetRoomDTO
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber,
                    Image = r.Photos.FirstOrDefault().ImageName,
                    Price = r.PricePerNight,
                    Discount = r.Offer != null ? r.Offer.Discount : 0,
                    Tag = r.Capacity,
                    Facilities = r.RoomFacilities.Select(rf => rf.Facility.Name).ToList()
                });

            return await Pagination<GetRoomDTO>.ToPagedList(roomsQuery, model.PageNumber, model.PageSize);
        }

    }
}
