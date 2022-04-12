using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Models
{
    public class CoordinateModel
    {
        public int Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public LocationModel Location { get; set; }
    }
}
