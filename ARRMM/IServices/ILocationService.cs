using ARRMM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.IServices
{
    public interface ILocationService
    {
        Task<List<LocationModel>> Get(string description = null);
        Task<LocationModel> Get(int id);
        Task<LocationModel> Add(LocationModel model);
        Task<LocationModel> Update(LocationModel model);
        Task<LocationModel> Delete(int id);
    }
}
