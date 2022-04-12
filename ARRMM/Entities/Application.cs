using ARRMM.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Entities
{
    public class Application
    {
        public Application()
        {
            Persons = new HashSet<Person>();
            Minerals = new HashSet<ApplicationMineral>();
        }

        [Key]
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


        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [ForeignKey("SendBy")]
        public ApplicationUser SendByUser { get; set; }

        [ForeignKey("SendTo")]
        public ApplicationUser SendToUser { get; set; }

        [ForeignKey("GrantedOrRefusedBy")]
        public ApplicationUser GrantedOrRefusedByUser { get; set; }

        public Reconnaissance Reconnaissance { get; set; }

        public Exploration Exploration { get; set; }

        public MineralDepositRetention MineralDepositRetention { get; set; }

        public LargeMiningLease LargeMiningLease { get; set; }

        public Prospecting Prospecting { get; set; }

        public SmallMiningLease SmallMiningLease { get; set; }

        public LandSurrenderAndTransfer LandSurrenderAndTransfer { get; set; }

        public ICollection<Person> Persons { get; set; }

        public ICollection<ApplicationMineral> Minerals { get; set; }
    }
}
