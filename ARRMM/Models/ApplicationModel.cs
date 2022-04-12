using ARRMM.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Models
{
    public class ApplicationModel
    {
        public int Id { get; set; }
        public string ApplicationNumber { get; set; }
        public ApplicationType Type { get; set; }
        public ScaleType ScaleType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int StatusId { get; set; }
        public string UserId { get; set; }
        public string SendBy { get; set; }
        public string SendTo { get; set; }
        public string GrantedOrRefusedBy { get; set; }

        public StatusModel Status { get; set; }
        public UserModel User { get; set; }
        public UserModel SendByUser { get; set; }
        public UserModel SendToUser { get; set; }
        public UserModel GrantedOrRefusedByUser { get; set; }
        public ReconnaissanceModel Reconnaissance { get; set; }
        public ExplorationModel Exploration { get; set; }
        public MineralDepositRetentionModel MineralDepositRetention { get; set; }
        public LargeMiningLeaseModel LargeMiningLease { get; set; }
        public ProspectingModel Prospecting { get; set; }
        public SmallMiningLeaseModel SmallMiningLease { get; set; }
        public LandSurrenderAndTransferModel LandSurrenderAndTransfer { get; set; }
    }
}
