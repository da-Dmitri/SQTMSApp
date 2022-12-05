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
using TMS.ViewModel;

namespace TMS.Model
{

    public enum OrderStatus
    {
        Default,
        Accepted,
        Schedueled,
        Delivered,
        Completed
    }

    public class Order
    {

        public OrderStatus Status { get; set; }

        //the contract information
        public Contract Contract { get; set; }

        //trips assigned to this order with trip id as the key
        //public Dictionary<int, Trips> AssignedTrips { get; set; }

        //relevant cities for the order, with city name as the key
        public Dictionary<string, City> RelevantCities { get; set; }

        public Order() { }

        public Order(Contract input)
        {
            Contract = input;
            Status = OrderStatus.Default;
        }

        public Order(Contract input, OrderStatus status)
        {
            Contract = input;
            Status = status;
        }
    }
}