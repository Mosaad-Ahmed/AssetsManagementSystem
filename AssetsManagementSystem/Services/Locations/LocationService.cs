
using AssetsManagementSystem.DTOs.LocationDTOs;

namespace AssetsManagementSystem.Services.Locations
{
    public class LocationService : BaseClassForServices
    {
        public LocationService(IUnitOfWork unitOfWork, 
            Others.Interfaces.IAutoMapper.IMapper mapper,
            IHttpContextAccessor httpContextAccessor) 
            : base(unitOfWork, mapper, httpContextAccessor)
        {

        }

        #region Adding a new location
        public async Task AddLocationAsync(AddLocationRequestDTO addLocationRequest)
        {
            if (addLocationRequest == null)
            {
                throw new ArgumentNullException(nameof(addLocationRequest), "Location details cannot be null.");
            }

            var existingLocation = await UnitOfWork.readRepository<Location>()
                                       .GetAsync(l => l.Name == addLocationRequest.Name);

            if (existingLocation != null)
            {
                throw new InvalidOperationException("A location with the same name already exists.");
            }

            var location = Mapper.Map<Location,AddLocationRequestDTO>(addLocationRequest);

            location.AddedOnDate = DateTime.Now;

            await UnitOfWork.writeRepository<Location>().AddAsync(location);

            await UnitOfWork.SaveChangeAsync();
        }
        #endregion

        #region Retrieve a location by ID
        public async Task<GetLocationRequestDTO> GetLocationByIdAsync(int locationId)
        {
            if (locationId <= 0)
            {
                throw new ArgumentException("Invalid location ID.");
            }

            var location = await UnitOfWork.readRepository<Location>().GetAsync(l => l.Id == locationId);
            var getLocationRequestDTO = Mapper.Map<GetLocationRequestDTO,Location>(location);

            if (location == null)
            {
                throw new KeyNotFoundException("Location not found.");
            }

            return getLocationRequestDTO;
        }
        #endregion

        #region Retrieve all locations
        public async Task<IEnumerable<GetLocationRequestDTO>> GetAllLocationsAsync()
        {
             var Locations= await UnitOfWork.readRepository<Location>().GetAllAsync();

            var GetLocationRequestDTOs = Mapper.Map<GetLocationRequestDTO,Location>(Locations);

            return GetLocationRequestDTOs;
        }
        #endregion

        #region Update a location
        public async Task UpdateLocationAsync(int locationId, UpdateLocationRequestDTO updateLocationRequest)
        {
            if (updateLocationRequest == null)
            {
                throw new ArgumentNullException(nameof(updateLocationRequest), "Location details cannot be null.");
            }
            var updateLocation = await GetLocationByIdAsync(locationId);

            var location = Mapper.Map<Location,GetLocationRequestDTO>(updateLocation);

            var existingLocation = await UnitOfWork.readRepository<Location>()
                                      .GetAsync(l => l.Name == updateLocationRequest.Name && l.Id != locationId);

            if (existingLocation != null)
            {
                throw new InvalidOperationException("Another location with the same name already exists.");
            }

            location.Name = updateLocationRequest.Name;
            location.Address = updateLocationRequest.Address;
            location.UpdatedDate= DateTime.UtcNow;
            location.AddedOnDate = location.AddedOnDate;
            await UnitOfWork.writeRepository<Location>().UpdateAsync(location.Id, location);

            await UnitOfWork.SaveChangeAsync();
        }
        #endregion

        #region Delete a location
        public async Task DeleteLocationAsync(int locationId)
        {
            var updateLocation = await GetLocationByIdAsync(locationId);

            var location = Mapper.Map<Location, GetLocationRequestDTO>(updateLocation);

            location.DeletedDate = DateTime.UtcNow;
            location.IsDeleted = true;
            
            await UnitOfWork.writeRepository<Location>().UpdateAsync(location.Id, location);

            await UnitOfWork.SaveChangeAsync();
        }

        #endregion
         
    }
}
