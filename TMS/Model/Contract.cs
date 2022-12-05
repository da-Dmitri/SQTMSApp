using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public class Contract
    {
        private string client_name;
        private int job_type;
        private int quantity;
        private string origin;
        private string destination;
        private int van_type;


        public string ClientName
        {
            get { return client_name; }
            set { client_name = value; }
        }

        public int JobType
        {
            get { return job_type; }
            set { job_type = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public string Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public string Destination
        {
            get { return destination; }
            set
            {
                destination = value;
            }
        }

        public int Van_Type
        {
            get { return van_type; }
            set
            {
                van_type = value;
            }
        }

    }
}
