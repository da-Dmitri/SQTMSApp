using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TMS.ViewModel
{
    internal class Trips
    {

        public int TripID { get; set; }
        public int OrderID { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }


        public Trips(int tripNumber, int orderNumber, string theCarrier, string theOrigin, string theDestination)
        {
            TripID = tripNumber;
            OrderID = orderNumber;
            Origin = theOrigin;
            Destination = theDestination;
        }
    }
}
