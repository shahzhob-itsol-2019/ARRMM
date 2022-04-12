using ARRMM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ARRMM.IServices
{
    public interface ICompanyService
    {
        Task<List<CompanyModel>> Get(string name = null);
        Task<CompanyModel> Get(int id);
        Task<CompanyModel> Add(CompanyModel model);
        Task<CompanyModel> Update(CompanyModel model);
        Task<CompanyModel> Delete(int id);
    }
}
