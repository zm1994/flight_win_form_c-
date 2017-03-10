using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightConnection
{
    [Serializable]
    public class Location
    {
        public double lat{ get; set; }
        public double lon { get; set; }
    }
}
