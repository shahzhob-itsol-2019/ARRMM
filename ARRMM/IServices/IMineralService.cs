using ARRMM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.IServices
{
    public interface IMineralService
    {
        Task<List<MineralModel>> Get();
        Task<MineralModel> Get(string identifier);
        Task<MineralModel> Get(int id);
        Task<MineralModel> Add(MineralModel model);
        Task<MineralModel> Update(MineralModel model);
        Task<MineralModel> Delete(int id);
    }
}
