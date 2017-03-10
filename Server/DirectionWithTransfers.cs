using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class DirectionWithTransfers
    {
        public int departure_id { get; set; }
        public int[] transfers_id { get; set; }
        public int arrival_id { get; set; }

        public DirectionWithTransfers()
        {
            departure_id = 0;
            transfers_id = new int[0];
            arrival_id = 0;
        }
    }
}
