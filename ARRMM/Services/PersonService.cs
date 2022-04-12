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
    public class PersonService : IPersonService
    {
        private readonly IDbContext dbContext;
        private readonly IMapper mapper;

        public PersonService(IDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<PersonModel>> Get(string firstName = null, string lastName = null, string fatherName = null, string companyName = null, string nicNumber = null, string nationality = null)
        {
            IQueryable<Person> query = dbContext.Persons
                                                .Include(x => x.Company)
                                                .Include(x => x.Country);

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(x => x.FirstName.ToLower().Contains(firstName.ToLower()));
            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(x => x.LastName.ToLower().Contains(lastName.ToLower()));
            if (!string.IsNullOrWhiteSpace(fatherName))
                query = query.Where(x => x.FatherName.ToLower().Contains(fatherName.ToLower()));
            if (!string.IsNullOrWhiteSpace(companyName))
                query = query.Where(x => x.Company.Name.ToLower().Contains(companyName.ToLower()));
            if (!string.IsNullOrWhiteSpace(nicNumber))
                query = query.Where(x => x.NicNumber.ToLower().Equals(nicNumber.ToLower()));
            if (!string.IsNullOrWhiteSpace(nationality))
                query = query.Where(x => x.Country.Nationality.ToLower().Contains(nationality.ToLower()));

            var entities = await query.ToListAsync();
            //if (entities.Any())
            //    return null; // new BaseModel { Success = false, Message = "No record found." };

            var result = mapper.Map<List<PersonModel>>(entities);
            return result;
        }

        public async Task<PersonModel> Get(int id)
        {
            var entity = await dbContext.Persons
                                        .Include(x => x.Company)
                                        .Include(x => x.Country)
                                        .FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (entity == null)
                return null;

            var result = mapper.Map<PersonModel>(entity);
            return result;
        }

        public async Task<PersonModel> Add(PersonModel model)
        {
            var entity = new Person
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                FatherName = model.FatherName,
                Address = model.Address,
                NicNumber = model.NicNumber,
                NicIssueDate = model.NicIssueDate,
                NicIssuePlace = model.NicIssuePlace,
                IsLocalDomicile = model.IsLocalDomicile,
                NameOfDistrict = model.NameOfDistrict,
                NameOfTribe = model.NameOfTribe,
                Occupation = model.Occupation,
                PlaceOfBusiness = model.PlaceOfBusiness,
                SharesPercentage = model.SharesPercentage,
                Type = model.Type,
                ApplicationId = model.Application.Id,
                CompanyId = model.Company.Id,
                CountryId = model.Country.Id
            };

            var response = await dbContext.Persons.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            var result = mapper.Map<PersonModel>(entity);
            return result;
        }

        public async Task<PersonModel> Update(PersonModel model)
        {
            if (model.Id <= 0)
                return null;

            var entity = await dbContext.Persons.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));
            if (entity == null)
                return null;

            var company = await dbContext.Companies.FirstOrDefaultAsync(x => x.Id.Equals(model.Company.Id));
            if (company == null)
                return null;

            var country = await dbContext.Countries.FirstOrDefaultAsync(x => x.Id.Equals(model.Country.Id));
            if (country == null)
                return null;

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.FatherName = model.FatherName;
            entity.Address = model.Address;
            entity.NicNumber = model.NicNumber;
            entity.NicIssueDate = model.NicIssueDate;
            entity.NicIssuePlace = model.NicIssuePlace;
            entity.IsLocalDomicile = model.IsLocalDomicile;
            entity.NameOfDistrict = model.NameOfDistrict;
            entity.NameOfTribe = model.NameOfTribe;
            entity.Occupation = model.Occupation;
            entity.PlaceOfBusiness = model.PlaceOfBusiness;
            entity.SharesPercentage = model.SharesPercentage;
            entity.Type = model.Type;
            entity.ApplicationId = model.Application.Id;
            entity.CompanyId = model.Company.Id;
            entity.CountryId = model.Country.Id;

            var response = dbContext.Persons.Update(entity);
            await dbContext.SaveChangesAsync();

            var result = mapper.Map<PersonModel>(entity);
            return result;
        }

        public async Task<PersonModel> Delete(int id)
        {
            var entity = await dbContext.Persons.FindAsync(id);
            if (entity == null)
                return null;

            dbContext.Persons.Remove(entity);
            await dbContext.SaveChangesAsync();

            return new PersonModel();
        }
    }
}
