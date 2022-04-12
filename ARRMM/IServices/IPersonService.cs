using ARRMM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.IServices
{
    public interface IPersonService
    {
        Task<List<PersonModel>> Get(string firstName = null, string lastName = null, string fatherName = null, string companyName = null, string nicNumber = null, string nationality = null);
        Task<PersonModel> Get(int id);
        Task<PersonModel> Add(PersonModel model);
        Task<PersonModel> Update(PersonModel model);
        Task<PersonModel> Delete(int id);
    }
}
