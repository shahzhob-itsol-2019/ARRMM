using ARRMM.Database;
using ARRMM.IServices;
using ARRMM.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Services
{
    public class CountryService : ICountryService
    {
        private readonly IDbContext dbContext;
        private readonly IMapper mapper;

        public CountryService(IDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<CountryModel>> Get()
        {
            var entities = await dbContext.Countries
                                          .ToListAsync();
            //if (entities.Any())
            //    return null; // new BaseModel { Success = false, Message = "No record found." };

            var result = mapper.Map<List<CountryModel>>(entities);
            return result;
        }

        public async Task<CountryModel> Get(string identifier)
        {
            var entity = await dbContext.Countries
                                        .FirstOrDefaultAsync(x => x.Name.Equals(identifier) || x.Code.Equals(identifier) || x.Nationality.Equals(identifier));
            if (entity == null)
                return null; // new BaseModel { Success = false, Message = "No record found." };

            var result = mapper.Map<CountryModel>(entity);
            return result;
        }
    }
}
