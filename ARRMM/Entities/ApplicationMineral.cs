using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Entities
{
    public class ApplicationMineral
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int MineralId { get; set; }


        [ForeignKey("ApplicationId")]
        public Application Application { get; set; }

        [ForeignKey("MineralId")]
        public Mineral Mineral { get; set; }
    }
}
