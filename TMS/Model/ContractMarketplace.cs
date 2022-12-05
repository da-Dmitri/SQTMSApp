/*
 *  FILE            : ContractMarketplace.cs
 *  PROJECT         : TMS - Group 7
 *  PROGRAMMER      : Vincent Marshall
 *  FIRST VERSION   : 27/11/2022
 *  DESCRIPTION     :
 *      This file contains the class that is in charge of
 *      connecting to the contract marketplace and retrieving
 *      a list of customer contracts
 *      The current credentials for the marketplace is the following
 *      Username: DevOSHT
 *      Password: Snodgr4ss!
*/
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TMS.Model;

namespace TMS {
    internal class ContractMarketplace {

        //where to connect to
        public IPAddress MarketplaceIp { get; set; }
        public int MarketplacePort { get; set; }
        public string Username { get; private set;}
        public string Password { get; private set; }
        public string Name {get; private set;}

        private ContractMarketplace() {
            //there are no defaults, they must be set
            MarketplaceIp = IPAddress.Parse("159.89.117.198");
            MarketplacePort = 3306;
            Username = "DevOSHT";
            Password = "Snodgr4ss!";
            Name = "cmp";
        }

        //set the ip using the constructor
        public ContractMarketplace(IPAddress ip, int port, string user, string pass) {
            MarketplaceIp = ip;
            MarketplacePort = port;
            Username = user;
            Password = pass;
        }

        /*
         * FUNCTION     : GetContracts()
         * DESCRIPTION  :
         *  attempts to connect to the database at the ip set in the class
         * PARAMETERS   :
         *  none
         * RETURNS      :
         *  DataTable : a raw table of all the contracts
        */
        public DataTable GetContractsTable() {
            
            DataTable Output = new DataTable();

            //attempt to connect to the database
            string myConnectionString = "SERVER="+MarketplaceIp+";DATABASE="+Name+";UID="+Username+";PASSWORD="+Password+";";

            MySqlConnection connection = new MySqlConnection(myConnectionString);

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Contract", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            return dt;


            //do query
            //SELECT * FROM Contract

            //transfer the query output into "output"

            return output;
        }


    }
}
