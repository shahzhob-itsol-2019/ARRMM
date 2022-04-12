using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Models
{
    public class LocationModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<CoordinateModel> Coordinates { get; set; }
    }
}
