using Ecom.core.Interfaces;
using HotelReservation.Core.DTOs.FacilityDTOs;
using HotelReservation.Core.Entities;
using HotelReservation.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infrastructure.Services
{
    public class FacilityService : IFacilityService
    {
        private readonly IGenericRepository<Facility> _facilityRepo;
        public FacilityService(IGenericRepository<Facility> facilityRepo)
        {
            _facilityRepo = facilityRepo;
        }
        public async Task<bool> CreateFacility(CreateFacilityDTO createFacilityDTO)
        {
            var existingFacility = await _facilityRepo.GetAllAsync(f => f.Name == createFacilityDTO.Name);
            if (existingFacility.Any())
            {
                return false;
            }

            var facility = new Facility
            {
                Name = createFacilityDTO.Name
            };
            await _facilityRepo.AddAsync(facility);
            await _facilityRepo.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateFacility(UpdateFacilityDTO updateFacilityDTO)
        {
            var facility = await _facilityRepo.GetByIdAsync(updateFacilityDTO.Id);
            if (facility == null)
            {
                return false;
            }
            facility.Name = updateFacilityDTO.Name;
            await _facilityRepo.UpdateAsync(updateFacilityDTO.Id, facility);
            await _facilityRepo.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteFacility(DeleteFacilityDTO deleteFacilityDTO)
        {
            var facility = await _facilityRepo.GetByIdAsync(deleteFacilityDTO.Id);
            if (facility == null)
            {
                return false;
            }
            await _facilityRepo.DeleteAsync(deleteFacilityDTO.Id);
            await _facilityRepo.SaveChangesAsync();
            return true;
        }
    }
}
