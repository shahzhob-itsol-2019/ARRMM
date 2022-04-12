using ARRMM.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Models
{
    public class StatusModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public StatusType Type { get; set; }
    }
}
