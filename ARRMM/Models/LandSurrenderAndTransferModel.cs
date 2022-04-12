using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Models
{
    // Form-J and Form-K Other
    public class LandSurrenderAndTransferModel
    {
        public LandSurrenderAndTransferModel()
        {
            ApplicionStatuses = new List<SelectListItem>();
            OperationStatuses = new List<SelectListItem>();
            Minerals = new List<SelectListItem>();
        }

        public int ApplicationId { get; set; }
        public string ApplicantName { get; set; }
        public List<ApplicationMineralModel> ApplicationMinerals { get; set; }
        public DateTime GrantedDate { get; set; }
        public string RegisteredMineralTitle { get; set; }
        public string AssigneeName { get; set; }
        public DateTime? SurrenderDate { get; set; } // J
        public int? LandAreaSize { get; set; } // J
        public string ParticularDetail { get; set; } // J
        public List<ApplicationMineralModel> PreviousAppliedMinerals { get; set; }
        public string PreviousApplicationDescription { get; set; }
        public int? PreviousApplicationStatusId { get; set; }
        public string PreviousOperationDescription { get; set; }
        public int? PreviousOperationStatusId { get; set; }
        public string Offences { get; set; }
        public string Remarks { get; set; }

        //public StatusModel PreviousApplicionStatus { get; set; }
        //public StatusModel PreviousOperationStatus { get; set; }
        //public ApplicationModel Application { get; set; }

        //public List<StatusModel> Statuses { get; set; }
        //public List<MineralModel> Minerals { get; set; }

        public List<SelectListItem> ApplicionStatuses { get; set; }
        public List<SelectListItem> OperationStatuses { get; set; }
        public List<int> SelectedMineralIds { get; set; }
        public List<SelectListItem> Minerals { get; set; }
    }
}
