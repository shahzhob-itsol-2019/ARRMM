using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Models
{
    public class LandSurrenderModel
    {
        public LandSurrenderModel()
        {
            Minerals = new List<SelectListItem>();
        }

        public int ApplicationId { get; set; }
        public string ApplicantName { get; set; }
        public List<ApplicationMineralModel> ApplicationMinerals { get; set; }
        public DateTime GrantedDate { get; set; }
        public string RegisteredMineralTitle { get; set; }
        public DateTime SurrenderDate { get; set; }
        public int LandAreaSize { get; set; }
        public string ParticularDetail { get; set; }

        //public ApplicationModel Application { get; set; }

        //public List<StatusModel> Statuses { get; set; }
        //public List<MineralModel> Minerals { get; set; }

        public List<int> SelectedMineralIds { get; set; }
        public List<SelectListItem> Minerals { get; set; }
    }
}
