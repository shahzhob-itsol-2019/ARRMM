using ARRMM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ARRMM.IServices
{
    public interface ICountryService
    {
        Task<List<CountryModel>> Get();
        Task<CountryModel> Get(string identifier);
    }
}
