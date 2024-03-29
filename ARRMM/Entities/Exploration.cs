﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Entities
{
    // Form-B and Form-C LargeScale
    public class Exploration
    {
        [Key, ForeignKey("Application")]
        public int ApplicationId { get; set; }
        public string ApplicantName { get; set; }
        public DateTime IncorporationDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int? CountryId { get; set; } // B
        public DateTime? DateOfBirth { get; set; } // B
        public string Occupation { get; set; } // B
        public string ResidentialAddress { get; set; } // B
        public string RegisteredAddress { get; set; }
        public string BusinessAddress { get; set; }
        public string OfficeAddress { get; set; }
        public string BusinessNature { get; set; }
        public double Amount { get; set; }
        public bool HasShareCapital { get; set; }
        //public virtual ICollection<Person> Persons { get; set; }
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
