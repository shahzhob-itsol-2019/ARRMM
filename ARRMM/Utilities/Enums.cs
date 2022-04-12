using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Utilities.Enums
{
    public enum UserRoles
    {
        Guest
    }

    public enum LoginStatus
    {
        Locked = 0,
        AccountLocked,
        InvalidCredential,
        Succeded,
        TimeoutLocked,
        Failed,
        RequiresTwoFactor
    }

    public enum CustomClaimTypes
    {
        User,
        FirstName,
        LastName,
        ValidationCallTime,
        SecurityStamp
    }

    public enum ApplicantType
    {
        Indivisual,
        Partnership,
        Company,
        OutsidePakistan
    }

    public enum PersonType
    {
        Indivisual,
        Director,
        Officer,
        Controller,
        Partner,
        ShareHolder
    }

    public enum StatusType
    {
        ApplicionStatus = 1,
        OperationStatus = 2
    }

    public enum ApplicationType
    {
        Form_A_ReconnaissanceLicenceByCompany,
        Form_B_ExplorationLicenceByIndividual,
        Form_C_ExplorationLicenceByCompany,
        Form_D_MineralDepositRetentionLicenceByIndividual,
        Form_E_MineralDepositRetentionLicenceByCompany,
        Form_F_MiningLeaseByCompany,
        Form_G_ProspectingLicence,
        Form_H_MiningLease,
        Form_I,
        Form_J_MineralTitleOrConcessionApplicationForSurrenderOfLand,
        Form_K_MineralTitleOrMiningLeaseApplicationForApprovalOfAssignmentOrTransfer
    }

    public enum ScaleType
    {
        SmallScaleMining,
        LargeScaleMining,
        Other
    }
}
