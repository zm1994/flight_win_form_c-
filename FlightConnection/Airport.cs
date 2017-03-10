using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightConnection
{
    public class Airport
    {
        public string code_airport;
        public int id_airport;
        public Location location;

        public Airport() 
        {
            location = new Location();
        }
    }
}
