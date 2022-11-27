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

    

    internal class Contract {

        //Contract Properties
        public string ClientName { get; set; }

        public Contract() {
        
        }

        //fill data into the constructor
        public Contract(string client_name) {
        
        }
    }
}
