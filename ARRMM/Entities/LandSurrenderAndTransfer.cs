using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Entities
{
    // Form-J and Form-K Other
    public class LandSurrenderAndTransfer
    {
        [Key, ForeignKey("Application")]
        public int ApplicationId { get; set; }
        public string ApplicantName { get; set; }
        //public ICollection<ApplicationMineral> Minerals { get; set; }
        public DateTime GrantedDate { get; set; }
        public string RegisteredMineralTitle { get; set; }
        public string AssigneeName { get; set; }
        public DateTime? SurrenderDate { get; set; } // J
        public int? LandAreaSize { get; set; } // J
        public string ParticularDetail { get; set; } // J
        //public ICollection<ApplicationMineral> PreviousAppliedMinerals { get; set; }
        public int? PreviousApplicationStatusId { get; set; }
        public int? PreviousOperationStatusId { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }


        [ForeignKey("PreviousApplicationStatusId")]
        public Status PreviousApplicionStatus { get; set; }

        [ForeignKey("PreviousOperationStatusId")]
        public Status PreviousOperationStatus { get; set; }

        public Application Application { get; set; }
    }
}
