
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Entities
{
    // Form-D and Form-E LargeScale
    public class MineralDepositRetention
    {
        [Key, ForeignKey("Application")]
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
        //public ICollection<Person> Persons { get; set; }
        //public ICollection<Person> CompanyControllers { get; set; }
        //public ICollection<Person> Directors { get; set; }
        //public ICollection<Person> Officers { get; set; }
        //public ICollection<ApplicationMineral> PreviousAppliedMinerals { get; set; }
        public int LandAreaSize { get; set; }
        public int LocationId { get; set; }
        //public ICollection<ApplicationMineral> Minerals { get; set; }
        public int NumberOfYears { get; set; }
        public int PreviousApplicationStatusId { get; set; }
        public int PreviousOperationStatusId { get; set; }
        public string ClaimReason { get; set; }
        public DateTime ExpectedMiningDate { get; set; }
        public string RelatingPersonName { get; set; } // D
        public string RelatingCompanyName { get; set; }
        public string ChangesDetail { get; set; }
        public string AssignmentDetail { get; set; }
        public string Offences { get; set; }
        public string Remarks { get; set; }


        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        [ForeignKey("PreviousApplicationStatusId")]
        public Status PreviousApplicionStatus { get; set; }

        [ForeignKey("PreviousOperationStatusId")]
        public Status PreviousOperationStatus { get; set; }

        public Application Application { get; set; }
    }
}
