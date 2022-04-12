using ARRMM.Database;
using ARRMM.Entities;
using ARRMM.IServices;
using ARRMM.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Services
{
    public class LocationService : ILocationService
    {
        private readonly IDbContext dbContext;
        private readonly IMapper mapper;

        public LocationService(IDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<LocationModel>> Get(string description = null)
        {
            IQueryable<Location> query = dbContext.Locations
                                                  .Include(x => x.Coordinates);

            if (!string.IsNullOrWhiteSpace(description))
                query = query.Where(x => x.Description.ToLower().Contains(description.ToLower()));

            var entities = await query.ToListAsync();
            //if (entities.Any())
            //    return null; // new BaseModel { Success = false, Message = "No record found." };

            var result = mapper.Map<List<LocationModel>>(entities);
            return result;
        }

        public async Task<LocationModel> Get(int id)
        {
            var entity = await dbContext.Locations
                                        .Include(x => x.Coordinates)
                                        .FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (entity == null)
                return null;

            var result = mapper.Map<LocationModel>(entity);
            return result;
        }

        public async Task<LocationModel> Add(LocationModel model)
        {
            var entity = new Location
            {
                Description = model.Description
            };
            if (model.Coordinates != null && model.Coordinates.Any())
                foreach (var coordinate in model.Coordinates)
                    await dbContext.Coordinates.AddAsync(new Coordinate
                    {
                        Id = entity.Id,
                        Latitude = coordinate.Latitude,
                        Longitude = coordinate.Longitude
                    });

            var response = await dbContext.Locations.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            var result = mapper.Map<LocationModel>(entity);
            return result;
        }

        public async Task<LocationModel> Update(LocationModel model)
        {
            if (model.Id <= 0)
                return null;

            var entity = await dbContext.Locations.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));
            if (entity == null)
                return null;

            entity.Description = model.Description;
            if (model.Coordinates != null && model.Coordinates.Any())
            {
                var foundCoordinates = model.Coordinates.Where(x => !dbContext.Coordinates.Select(y => y.Latitude).Equals(x.Latitude) && !dbContext.Coordinates.Select(y => y.Longitude).Equals(x.Longitude));
                if (foundCoordinates.Any())
                {
                    entity.Coordinates.ToList().RemoveAll(x => x.LocationId.Equals(model.Id));

                    foreach (var coordinate in model.Coordinates)
                        entity.Coordinates.Add(new Coordinate
                        {
                            Id = entity.Id,
                            Latitude = coordinate.Latitude,
                            Longitude = coordinate.Longitude
                        });
                }
            }
            var response = dbContext.Locations.Update(entity);
            await dbContext.SaveChangesAsync();

            var result = mapper.Map<LocationModel>(entity);
            return result;
        }

        public async Task<LocationModel> Delete(int id)
        {
            var entity = await dbContext.Locations.FindAsync(id);
            if (entity == null)
                return null;

            dbContext.Locations.Remove(entity);
            await dbContext.SaveChangesAsync();

            return new LocationModel();
        }
    }
}
