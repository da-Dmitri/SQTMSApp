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

    public enum OrderStatus {
        Default,
        Accepted,
        Schedueled,
        Delivered,
        Completed
    }

    internal class Order {

        public OrderStatus Status {get; set;}

        //the contract information
        public Contract Contract {get; set;}

        private Order() {}

        public Order(Contract input) {
            Contract = input;
            Status = OrderStatus.Default;
        }

        public Order(Contract input, OrderStatus status) {
            Contract = input;
            Status = status;
        }
    }
}
