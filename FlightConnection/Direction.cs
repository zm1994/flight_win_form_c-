using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightConnection
{
    
    public class Direction
    {
        public List<Airport> Airports { get; set; }

        public Direction() 
        {
            Airports = new List<Airport>();
        }

        public Direction(List<Airport> airports)
        {
            Airports = airports;
        }

        public override string ToString()
        {
            string str = Airports.First().code_airport + " ";
            if (Airports.Count > 2) 
            {
                str += "via ";
                for (int i = 1; i < Airports.Count - 1; i++)
                    str += Airports[i].code_airport + "/";
            }
            str += " " + Airports.Last().code_airport + "\n";
            return str;
        }
    }
}
