
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Models
{
    // Form-D and Form-E LargeScale
    public class MineralDepositRetentionModel
    {
        public MineralDepositRetentionModel()
        {
            ApplicionStatuses = new List<SelectListItem>();
            OperationStatuses = new List<SelectListItem>();
            Countries = new List<SelectListItem>();
            Minerals = new List<SelectListItem>();
        }

        public int ApplicationId { get; set; }
        public string ApplicantName { get; set; }
        public DateTime IncorporationDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int? CountryId { get; set; } // D
        public DateTime? DateOfBirth { get; set; } // D
        public string Occupation { get; set; } // D
        public string ResidentialAddress { get; set; } // D
        public string RegisteredAddress { get; set; }
        public string BusinessAddress { get; set; }
        public string OfficeAddress { get; set; }
        public string BusinessNature { get; set; }
        public double Amount { get; set; }
        public bool HasShareCapital { get; set; }
        public List<PersonModel> CompanyControllers { get; set; }
        public List<PersonModel> Directors { get; set; }
        public List<PersonModel> Officers { get; set; }
        public List<ApplicationMineralModel> PreviousAppliedMinerals { get; set; }
        public int LandAreaSize { get; set; }
        public LocationModel Location { get; set; }
        public List<ApplicationMineralModel> ApplicationMinerals { get; set; }
        public int NumberOfYears { get; set; }
        public string PreviousApplicationDescription { get; set; }
        public string PreviousApplicationStatusCode { get; set; }
        public string PreviousOperationDescription { get; set; }
        public string PreviousOperationStatusCode { get; set; }
        public string ClaimReason { get; set; }
        public DateTime ExpectedMiningDate { get; set; }
        public string RelatingPersonName { get; set; } // D
        public string RelatingCompanyName { get; set; }
        public string ChangesDetail { get; set; }
        public string AssignmentDetail { get; set; }
        public string Offences { get; set; }
        public string Remarks { get; set; }
        public bool IsCompany { get; set; }

        //public CountryModel Country { get; set; }
        //public StatusModel PreviousApplicionStatus { get; set; }
        //public StatusModel PreviousOperationStatus { get; set; }
        //public ApplicationModel Application { get; set; }

        //public List<CountryModel> Contries { get; set; }
        //public List<StatusModel> Statuses { get; set; }
        //public List<MineralModel> Minerals { get; set; }

        public List<SelectListItem> ApplicionStatuses { get; set; }
        public List<SelectListItem> OperationStatuses { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<int> SelectedMineralIds { get; set; }
        public List<SelectListItem> Minerals { get; set; }
    }
}
