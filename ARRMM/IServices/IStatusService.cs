using ARRMM.Models;
using ARRMM.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ARRMM.IServices
{
    public interface IStatusService
    {
        Task<List<StatusModel>> Get(StatusType? type = null);
        Task<StatusModel> Get(string code, StatusType type);
    }
}
