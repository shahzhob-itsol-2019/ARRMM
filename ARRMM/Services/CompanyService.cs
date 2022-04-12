using ARRMM.Database;
using ARRMM.Entities;
using ARRMM.IServices;
using ARRMM.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IDbContext dbContext;
        private readonly IMapper mapper;

        public CompanyService(IDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<CompanyModel>> Get(string name = null)
        {
            IQueryable<Company> query = dbContext.Companies
                                                 .Include(x => x.Persons);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));

            var entities = await query.ToListAsync();
            //if (entities.Any())
            //    return null; // new BaseModel { Success = false, Message = "No record found." };

            var result = mapper.Map<List<CompanyModel>>(entities);
            return result;
        }

        public async Task<CompanyModel> Get(int id)
        {
            var entity = await dbContext.Companies
                                        .Include(x => x.Persons)
                                        .FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (entity == null)
                return null;

            var result = mapper.Map<CompanyModel>(entity);
            return result;
        }

        public async Task<CompanyModel> Add(CompanyModel model)
        {
            var record = await dbContext.Companies.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(model.Name.ToLower()));
            if (record != null)
                return mapper.Map<CompanyModel>(record);

            var entity = new Company
            {
                Name = model.Name,
                Description = model.Description
            };

            var response = await dbContext.Companies.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            var result = mapper.Map<CompanyModel>(entity);
            return result;
        }

        public async Task<CompanyModel> Update(CompanyModel model)
        {
            if (model.Id <= 0)
                return null;

            var entity = await dbContext.Companies.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));
            if (entity == null)
                return null;

            if (!entity.Name.Equals(model.Name))
            {
                var data = await dbContext.Companies.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(model.Name.ToLower()) && !x.Id.Equals(model.Id));
                if (data != null)
                    return null;
            }

            entity.Name = model.Name;
            entity.Description = model.Description;

            var response = dbContext.Companies.Update(entity);
            await dbContext.SaveChangesAsync();

            var result = mapper.Map<CompanyModel>(entity);
            return result;
        }

        public async Task<CompanyModel> Delete(int id)
        {
            var entity = await dbContext.Companies.FindAsync(id);
            if (entity == null)
                return null;

            dbContext.Companies.Remove(entity);
            await dbContext.SaveChangesAsync();

            return new CompanyModel();
        }
    }
}
