using EventVault.Data.Repositories;
using EventVault.Data.Repositories.IRepositories;
using EventVault.Models;
using EventVault.Models.DTOs;
using EventVault.Services.IServices;

namespace EventVault.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IEventServices _eventServices;

        public UserService(IUserRepository userRepository , IEventRepository eventRepository, IEventServices eventServices)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _eventServices = eventServices;

        }
        public async Task<IEnumerable<UserGetAllDTO>> GetAllUsersAsync()
        {
           var allusers =  await _userRepository.GetAllUsersAsync();

            return allusers.Select(u=> new UserGetAllDTO 
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                NickName = u.NickName,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                ProfilePictureUrl = u.ProfilePictureUrl,
            });
        }
        public async Task<IEnumerable<EventGetDTO>> GetAllSavedEventsAsync(string userId)
        {
            var eventsFromDb = await _userRepository.GetAllSavedEventsAsync(userId);

            var eventDTOs = eventsFromDb.Select(eventFromDb => new EventGetDTO
            {
                Id = eventFromDb.Id,
                EventId = eventFromDb.EventId,
                Category = eventFromDb.Category,
                Title = eventFromDb.Title,
                Description = eventFromDb.Description,
                ImageUrl = eventFromDb.ImageUrl,
                APIEventUrlPage = eventFromDb.APIEventUrlPage,
                EventUrlPage = eventFromDb.EventUrlPage,
                Date = eventFromDb.Date,
                TicketsRelease = eventFromDb.TicketsRelease,
                HighestPrice = eventFromDb.HighestPrice,
                LowestPrice = eventFromDb.LowestPrice,
                Venue = new VenueGetDTO
                {
                    Id = eventFromDb.Venue.Id,
                    Name = eventFromDb.Venue.Name,
                    Address = eventFromDb.Venue.Address,
                    City = eventFromDb.Venue.City,
                    LocationLat = eventFromDb.Venue.LocationLat,
                    LocationLong = eventFromDb.Venue.LocationLong
                } ?? new VenueGetDTO()
            }).ToList();

            return eventDTOs;

        }

        public async Task<EventGetDTO> GetSavedEventAsync(string userId, int eventId)
        {
            var eventFromDb = await _userRepository.GetSavedEventAsync(userId, eventId);

            if (eventFromDb == null)
            {
                return new EventGetDTO { };
            }

            var eventDTO = new EventGetDTO
            {
                Id = eventFromDb.Id,
                EventId = eventFromDb.EventId,
                Category = eventFromDb.Category,
                Title = eventFromDb.Title,
                Description = eventFromDb.Description,
                ImageUrl = eventFromDb.ImageUrl,
                APIEventUrlPage = eventFromDb.APIEventUrlPage,
                EventUrlPage = eventFromDb.EventUrlPage,
                Date = eventFromDb.Date,
                TicketsRelease = eventFromDb.TicketsRelease,
                HighestPrice = eventFromDb.HighestPrice,
                LowestPrice = eventFromDb.LowestPrice,
                Venue = new VenueGetDTO
                {
                    Id = eventFromDb.Venue.Id,
                    Name = eventFromDb.Venue.Name,
                    Address = eventFromDb.Venue.Address,
                    City = eventFromDb.Venue.City,
                    LocationLat = eventFromDb.Venue.LocationLat,
                    LocationLong = eventFromDb.Venue.LocationLong
                } ?? new VenueGetDTO()
            } ?? new EventGetDTO { };

            return eventDTO;
        }

        public async Task AddEventToUserAsync(string userId, EventCreateDTO eventCreateDTO)
        {
            // Check if the event already exists in the database
            var existingEvent = await _eventRepository.GetEventFromDbAsync(eventCreateDTO);

            if (existingEvent == null)
            {
                // If event doesn't exist, create a new event in the database
                existingEvent = new Event
                {
                    EventId = eventCreateDTO.EventId,
                    Category = eventCreateDTO.Category,
                    Title = eventCreateDTO.Title,
                    Description = eventCreateDTO.Description,
                    ImageUrl = eventCreateDTO.ImageUrl,
                    APIEventUrlPage = eventCreateDTO.APIEventUrlPage,
                    EventUrlPage = eventCreateDTO.EventUrlPage,
                    Date = eventCreateDTO.Date,
                    TicketsRelease = eventCreateDTO.TicketsRelease,
                    HighestPrice = eventCreateDTO.HighestPrice,
                    LowestPrice = eventCreateDTO.LowestPrice,
                    Venue = new Venue
                    {
                        Name = eventCreateDTO.Venue.Name,
                        Address = eventCreateDTO.Venue.Address,
                        City = eventCreateDTO.Venue.City,
                        LocationLat = eventCreateDTO.Venue.LocationLat,
                        LocationLong = eventCreateDTO.Venue.LocationLong
                    }
                };
                await _eventRepository.AddEventAsync(existingEvent);
            }

            // Retrieve the user and add the event to their collection if not already added
            var user = await _userRepository.GetUserAsync(userId);
            if (user != null && !user.Events.Any(e => e.Title == eventCreateDTO.Title && e.Date == eventCreateDTO.Date && e.Venue.Name == eventCreateDTO.Venue.Name))
            {

                 await _userRepository.AddEventToUserAsync(userId, eventCreateDTO);

            }

            Console.WriteLine("added event to user");
        }

        public async Task RemoveEventFromUserAsync(string userId, int eventId)
        {
            await _userRepository.RemoveEventFromUserAsync(userId, eventId);
        }

        public async Task<UserShowOneDTO> GetOneUserByIdAsync(string userId)
        {
            var userWithId = await _userRepository.GetOneUserByIdAsync(userId);

            var userDTO = new UserShowOneDTO
            {
                UserId = userWithId.Id,
                FirstName = userWithId.FirstName,
                LastName = userWithId.LastName,
                Email = userWithId.Email,
                PhoneNumber = userWithId.PhoneNumber,
                NickName = userWithId.NickName,
                UserName = userWithId.UserName,
                ProfilePictureUrl = userWithId.ProfilePictureUrl,
            };
            return userDTO;
        }

        public async Task<UserShowOneDTO> GetOneUserByUserNameAsync(string userName)
        {
            var userWithUserName = await _userRepository.GetOneUserByUserNameAsync(userName);

            var userDTO = new UserShowOneDTO
            {
                FirstName = userWithUserName.FirstName,
                LastName = userWithUserName.LastName,
                Email = userWithUserName.Email,
                PhoneNumber = userWithUserName.PhoneNumber,
                NickName = userWithUserName.NickName,
                UserName = userWithUserName.UserName,
                ProfilePictureUrl = userWithUserName.ProfilePictureUrl,
            };
            return userDTO;
        }

        public async Task UpdateUserAsync(string userId, UserUpdateDTO user)
        {
           var getUser = await _userRepository.GetOneUserByIdAsync(userId);

            getUser.FirstName = user.FirstName;
            getUser.LastName = user.LastName;
            getUser.PhoneNumber = user.PhoneNumber;
            getUser.NickName = user.NickName;
            //getUser.ProfilePictureUrl = user.ProfilePictureUrl;

            await _userRepository.UpdateUserAsync(getUser);
        }
    }
}
