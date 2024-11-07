using EventVault.Data.Repositories.IRepositories;
using EventVault.Models;
using EventVault.Models.DTOs;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;
using TicketmasterTesting.Models.TicketMasterModels;

namespace EventVault.Services
{
    public class VenueServices : IVenueServices
    {
        private readonly IVenueRepository _venueRepository;

        public VenueServices(IVenueRepository venueRepository) 
        {
            _venueRepository = venueRepository;
        }
        public async Task AddVenueAsync (VenueCreateDTO venueCreateDTO)
        {

            var venueToAdd = new EventVault.Models.Venue
            {
                Name = venueCreateDTO.Name,
                Address = venueCreateDTO.Address,
                City = venueCreateDTO.City,
                ZipCode = venueCreateDTO.ZipCode,
                LocationLat = venueCreateDTO.LocationLat,
                LocationLong = venueCreateDTO.LocationLong,
            };

            await _venueRepository.AddVenueAsync(venueToAdd);
        }
        
        public async Task<VenueGetDTO> GetVenueAsync(int id)
        {
            var venueById = await _venueRepository.GetVenueAsync(id);

            if (venueById != null)
            {
                var venue = new VenueGetDTO
                {
                    Id = venueById.Id,
                    Name = venueById.Name,
                    Address = venueById.Address,
                    City = venueById.City,
                    ZipCode = venueById.ZipCode,
                    LocationLat = venueById.LocationLat,
                    LocationLong = venueById.LocationLong,
                    Events = venueById.Events?.Select(e => new EventGetDTO
                    {
                        Id = e.Id,
                        EventId = e.EventId,
                        Category = e.Category,
                        Title = e.Title,
                        Description = e.Description,
                        ImageUrl = e.ImageUrl,
                        APIEventUrlPage = e.APIEventUrlPage,
                        EventUrlPage = e.EventUrlPage,
                        Date = e.Date,
                        TicketsRelease = e.TicketsRelease,
                        HighestPrice = e.HighestPrice,
                        LowestPrice = e.LowestPrice
                    }).ToList() ?? new List<EventGetDTO>()
                };

                return venue;
            }

            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<VenueGetDTO>> GetAllVenuesAsync()
        {
            var venues = await _venueRepository.GetAllVenuesAsync();

            if (venues != null)
            {
                var venueList = venues.Select(venue => new VenueGetDTO
                {
                    Id = venue.Id,
                    Name = venue.Name,
                    Address = venue.Address,
                    City = venue.City,
                    ZipCode = venue.ZipCode,
                    LocationLat = venue.LocationLat,
                    LocationLong = venue.LocationLong,
                    Events = venue.Events?.Select(e => new EventGetDTO
                    {
                        Id = e.Id,
                        EventId = e.EventId,
                        Category = e.Category,
                        Title = e.Title,
                        Description = e.Description,
                        ImageUrl = e.ImageUrl,
                        APIEventUrlPage = e.APIEventUrlPage,
                        EventUrlPage = e.EventUrlPage,
                        Date = e.Date,
                        TicketsRelease = e.TicketsRelease,
                        HighestPrice = e.HighestPrice,
                        LowestPrice = e.LowestPrice
                    }).ToList() ?? new List<EventGetDTO>()
                });

                return venueList;
            }

            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateVenueAsync(VenueUpdateDTO venueUpdateDTO)
        {
            if (venueUpdateDTO.Name == null || venueUpdateDTO.Id == null)
            {
                return false;
            }

            var venueToUpdate = await _venueRepository.GetVenueAsync(venueUpdateDTO.Id);

            if (venueToUpdate != null) 
            {
                venueToUpdate.Name = venueUpdateDTO.Name;
                venueToUpdate.Address = venueUpdateDTO.Address;
                venueToUpdate.City = venueUpdateDTO.City;
                venueToUpdate.ZipCode = venueUpdateDTO.ZipCode;
                venueToUpdate.LocationLat = venueUpdateDTO.LocationLat;
                venueToUpdate.LocationLong = venueUpdateDTO.LocationLong;
            
                await _venueRepository.UpdateVenueAsync (venueToUpdate);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteVenueAsync(int id)
        {
            var venueToDelete = await _venueRepository.GetVenueAsync(id);

            if (venueToDelete != null)
            {
                await _venueRepository.DeleteVenueAsync(venueToDelete);

                return true;
            }

            else 
            {
                return false; 
            }
        }
    }
}
