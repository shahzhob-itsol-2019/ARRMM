using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Models
{
    public class LandTransferModel
    {
        public LandTransferModel()
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
        public List<ApplicationMineralModel> PreviousAppliedMinerals { get; set; }
        public string PreviousApplicationDescription { get; set; }
        public string PreviousApplicationStatusCode { get; set; }
        public string PreviousOperationDescription { get; set; }
        public string PreviousOperationStatusCode { get; set; }
        public string Reason { get; set; }
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
