using ARRMM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.IServices
{
    public interface IApplicationService
    {
        Task<List<ApplicationModel>> GetApplications(string userId = null);
        Task<List<ApplicationMineralModel>> GetAppliedMinerals(string userId = null);

        Task<ReconnaissanceModel> LoadReconnaissanceApplication(string userId, ReconnaissanceModel model = null);
        Task<ResponseModel> SubmitReconnaissanceApplication(ReconnaissanceModel model, string userId);

        Task<ExplorationModel> LoadExplorationApplication(string userId, ExplorationModel model = null);
        Task<ResponseModel> SubmitExplorationApplication(ExplorationModel model, string userId);

        Task<MineralDepositRetentionModel> LoadMineralDepositRetentionApplication(string userId, MineralDepositRetentionModel model = null);
        Task<ResponseModel> SubmitMineralDepositRetentionApplication(MineralDepositRetentionModel model, string userId);

        Task<LargeMiningLeaseModel> LoadLargeMiningLeaseApplication(string userId, LargeMiningLeaseModel model = null);
        Task<ResponseModel> SubmitLargeMiningLeaseApplication(LargeMiningLeaseModel model, string userId);

        Task<ProspectingModel> LoadProspectingApplication(string userId, ProspectingModel model = null);
        Task<ResponseModel> SubmitProspectingApplication(ProspectingModel model, string userId);

        Task<SmallMiningLeaseModel> LoadSmallMiningLeaseApplication(string userId, SmallMiningLeaseModel model = null);
        Task<ResponseModel> SubmitSmallMiningLeaseApplication(SmallMiningLeaseModel model, string userId);

        Task<LandSurrenderModel> LoadLandSurrenderApplication(string userId, LandSurrenderModel model = null);
        Task<ResponseModel> SubmitLandSurrenderApplication(LandSurrenderModel model, string userId);

        Task<LandTransferModel> LoadLandTransferApplication(string userId, LandTransferModel model = null);
        Task<ResponseModel> SubmitLandTransferApplication(LandTransferModel model, string userId);
    }
}
