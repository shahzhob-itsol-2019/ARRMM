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
    public class MineralService : IMineralService
    {
        private readonly IDbContext dbContext;
        private readonly IMapper mapper;

        public MineralService(IDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<MineralModel>> Get()
        {
            var entities = await dbContext.Minerals
                                          .ToListAsync();
            //if (entities.Any())
            //    return null; // new BaseModel { Success = false, Message = "No record found." };

            var result = mapper.Map<List<MineralModel>>(entities);
            return result;
        }

        public async Task<MineralModel> Get(string identifier)
        {
            var entity = await dbContext.Minerals
                                        .FirstOrDefaultAsync(x => x.Name.Equals(identifier) || x.Code.Equals(identifier));
            if (entity == null)
                return null; // new BaseModel { Success = false, Message = "No record found." };

            var result = mapper.Map<MineralModel>(entity);
            return result;
        }

        public async Task<MineralModel> Get(int id)
        {
            var entity = await dbContext.Minerals
                                        .FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (entity == null)
                return null;

            var result = mapper.Map<MineralModel>(entity);
            return result;
        }

        public async Task<MineralModel> Add(MineralModel model)
        {
            var entity = new Mineral
            {
                Name = model.Name,
                Code = model.Code,
                Cost = model.Cost
            };

            var response = await dbContext.Minerals.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            var result = mapper.Map<MineralModel>(entity);
            return result;
        }

        public async Task<MineralModel> Update(MineralModel model)
        {
            if (model.Id <= 0)
                return null;

            var entity = await dbContext.Minerals.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));
            if (entity == null)
                return null;

            entity.Name = model.Name;
            entity.Code = model.Code;
            entity.Cost = model.Cost;

            var response = dbContext.Minerals.Update(entity);
            await dbContext.SaveChangesAsync();

            var result = mapper.Map<MineralModel>(entity);
            return result;
        }

        public async Task<MineralModel> Delete(int id)
        {
            var entity = await dbContext.Minerals.FindAsync(id);
            if (entity == null)
                return null;

            dbContext.Minerals.Remove(entity);
            await dbContext.SaveChangesAsync();

            return new MineralModel();
        }
    }
}

