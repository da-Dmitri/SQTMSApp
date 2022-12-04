/*
 *  FILE            : Order.cs
 *  PROJECT         : TMS - Group 7
 *  PROGRAMMER      : Vincent Marshall
 *  FIRST VERSION   : 27/11/2022
 *  DESCRIPTION     :
 *      This file contains an order structure
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model {

    public enum Status {
        AwaitingApproval,
        Schedueled,
        Delivered,
        Completed
    }

    internal class Order {

        public Status OrderStatus {get; set;}


    }
}
