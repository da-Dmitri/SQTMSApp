using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public class City
    {
        public string Destination { get; set; }
        public int KMs { get; set; }
        public double Time { get; set; }
        public string West { get; set; }
        public string East { get; set; }

        public City(string destination, int kMs, double time, string west, string east)
        {
            Destination = destination;
            KMs = kMs;
            Time = time;
            West = west;
            East = east;
        }
    }
}
