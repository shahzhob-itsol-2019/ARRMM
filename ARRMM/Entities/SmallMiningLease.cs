using ARRMM.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Entities
{
    // Form-H SmallScale
    public class SmallMiningLease
    {
        [Key, ForeignKey("Application")]
        public int ApplicationId { get; set; }
        public string ApplicantName { get; set; }
        //public ICollection<ApplicationMineral> Minerals { get; set; }
        public int LandAreaSize { get; set; }
        public int LocationId { get; set; }
        public ApplicantType ApplicantType { get; set; }
        //public ICollection<Person> ApplicantDetails { get; set; }
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


        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public Application Application { get; set; }
    }
}
