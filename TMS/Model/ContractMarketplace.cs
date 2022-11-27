/*
 *  FILE            : ContractMarketplace.cs
 *  PROJECT         : TMS - Group 7
 *  PROGRAMMER      : Vincent Marshall
 *  FIRST VERSION   : 27/11/2022
 *  DESCRIPTION     :
 *      This file contains the class that is in charge of
 *      connecting to the contract marketplace and retrieving
 *      a list of customer contracts
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TMS.Model;

namespace TMS {
    internal class ContractMarketplace {

        //where to connect to
        public IPAddress MarketplaceAddress { get; set; }

        public ContractMarketplace() {
            //default to localhost
            MarketplaceAddress = IPAddress.Parse("127.0.0.1");
        }

        //set the ip using the constructor
        public ContractMarketplace(IPAddress ip) {
            MarketplaceAddress = ip;
        }

        /*
         * FUNCTION     : GetContracts()
         * DESCRIPTION  :
         *  attempts to connect to the database at the ip set in the class
         * PARAMETERS   :
         *  datatype name : purpose
         * RETURNS      :
         *  dataype : value/condition
        */
        public List<Contract> GetContracts() {
            
            List<Contract> output = new List<Contract>();

            //attempt to connect to the database

            //select the contracts table

            //transfer the query output into the list

            return output;
        }


    }
}
