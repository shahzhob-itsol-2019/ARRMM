using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Models
{
    public class ApplicationMineralModel
    {
        public int Id { get; set; }
        public ApplicationModel Application { get; set; }
        public MineralModel Mineral { get; set; }
    }
}
