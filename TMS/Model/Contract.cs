/*
 *  FILE            : Contract.cs
 *  PROJECT         : TMS - Group 7
 *  PROGRAMMER      : Vincent Marshall
 *  FIRST VERSION   : 27/11/2022
 *  DESCRIPTION     :
 *      This file contains a class that
 *      represents a customer contract
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model {

    public enum ShipmentType {
        FullTruckLoad,
        LessThanLoad
    }    

    internal class Contract {

        //Contract Properties
        public string ClientName { get; set; }

        public ShipmentType Type { get; set; }

        //number of full trucks, or LTL pallettes
        public int Amount { get; set; }

        //whether it's a reefer request or no
        public bool IsFrozen { get; set; }

        //replace these with like, location classes maybe?
        public string Origin { get; set; }
        public string Destination {get; set; }

        private Contract() {
        
        }

        //fill data into the contract on construction
        public Contract(
            string client_name,
            ShipmentType type,
            int amount,
            bool isReefer,
            string origin,
            string destination
        ) {
            ClientName = client_name;
            Type = type;
            Amount = amount;
            IsFrozen = isReefer;
            Origin = origin;
            Destination = destination;
        }
    }
}
