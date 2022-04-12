using ARRMM.Utilities.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Models
{
    // Form-H SmallScale
    public class SmallMiningLeaseModel
    {
        public SmallMiningLeaseModel()
        {
            Countries = new List<SelectListItem>();
            Minerals = new List<SelectListItem>();
            ApplicantTypes = new List<SelectListItem>();
            PersonTypes = new List<SelectListItem>();
        }

        public int ApplicationId { get; set; }
        public string ApplicantName { get; set; }
        public List<ApplicationMineralModel> ApplicationMinerals { get; set; }
        public int LandAreaSize { get; set; }
        public LocationModel Location { get; set; }

        [Required]
        public ApplicantType? ApplicantType { get; set; }
        public List<PersonModel> ApplicantDetails { get; set; }
        public string Authorized { get; set; }
        public string IssuedAndSubscribed { get; set; }
        public string BusinessName { get; set; }
        public string AmountOfCapital { get; set; }
        public string PreviousMiningExperienceDetail { get; set; }
        public string PastSubmittedApplicationDetail { get; set; }
        public string NameOfTechnicalExpert { get; set; }
        public string QualificationOfTechnicalExpert { get; set; }
        public string MiningConcessionsDetail { get; set; }
        public string GovernmentDuesDetail { get; set; }
        public string Remarks { get; set; }
        public string ChallanNumber { get; set; }
        public DateTime Dated { get; set; }
        public double Amount { get; set; }
        public string AmountInWords { get; set; }

        //public ApplicationModel Application { get; set; }
        //public List<CountryModel> Contries { get; set; }
        //public List<MineralModel> Minerals { get; set; }

        public List<SelectListItem> Countries { get; set; }
        public List<int> SelectedMineralIds { get; set; }
        public List<SelectListItem> Minerals { get; set; }
        public List<SelectListItem> ApplicantTypes { get; set; }
        public List<SelectListItem> PersonTypes { get; set; }
    }
}
