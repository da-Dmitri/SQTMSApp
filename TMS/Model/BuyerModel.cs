using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace TMS.Model
{
    public class BuyerModel:UserModel
    {

        // data members
        // dictionary for storing contracts?
        public Dictionary<string, string> Contracts;

        // dictionary for active orders? (accessible by buyer and planner?)
        public Dictionary<string, string> ActiveOrders;

        // dictionary for completed orders? (accessible by buyer and planner?)
        public Dictionary<string, string> CompletedOrders;

        // dictionary for city choices, holds city name as key and time in hours as value
        public Dictionary<string, City> Cities;

        // placeholder Communications class for database interaction
        private string Communications;



        // functions

        public BuyerModel(string username, string password, string lastName, string id)
        {
            this.Username = username;
            this.Password = password;
            this.LastName = lastName;
            this.ID = id;

            this.Contracts = new Dictionary<string, string>();
            this.ActiveOrders = new Dictionary<string, string>();
            this.CompletedOrders = new Dictionary<string, string>();
            this.Communications = "call Communications Class constructor";
            this.Cities = new Dictionary<string, City>();
            CitiesSetup();

        }

        public void CitiesSetup()
        {
            this.Cities.Clear();
            this.Cities.Add("Windsor", new City("Windsor", 191, 2.5, "END", "London"));
            this.Cities.Add("London", new City("London", 128, 1.75, "Windsor", "Hamilton"));
            this.Cities.Add("Hamilton", new City("Hamilton", 68, 1.25, "London", "Toronto"));
            this.Cities.Add("Toronto", new City("Toronto", 60, 1.3, "Hamilton", "Oshawa"));
            this.Cities.Add("Oshawa", new City("Oshawa", 134, 1.65, "Toronto", "Belleville"));
            this.Cities.Add("Belleville", new City("Belleville", 82, 1.2, "Oshawa", "Kingston"));
            this.Cities.Add("Kingston", new City("Kingston", 196, 2.5, "Belleville", "Ottawa"));
            this.Cities.Add("Ottawa", new City("Ottawa", 0, 0, "Kingston", "END"));
        }


        public void ViewCustomers()
        {

            // fetch customers from local database, to be displayed
            // need to know how customers list is to be sent
            //string customers = Communications.ViewCustomers();

        }

        public void AddCustomer(Contract contract)
        {

            // get customer info from contract(?), send it to the local database to be added
            // need to recieve contracts
            //Communications.AddCustomer(Contracts[contractKey]);

        }

        public void CreateOrder(Contract contract)
        {

            // create a new Order class for the given contract, then send it to SelectCities()
            // need to know what Order needs to be created, need to recieve contracts
            /*
            Order order = new Order(Contracts[contractKey]);
            SelectCities(order);
            */

        }

        internal void SelectCities(Order order)
        {

            // add relevant cities to the Order, then put the order in the active orders dictionary?
            // need to create Order
            Dictionary<string, City> orderCities = new Dictionary<string, City>();
            orderCities.Clear();
            
            // get contract's origin point and destination
            string orig = order.Contract.Origin;
            string dest = order.Contract.Destination;
            int start = -1;
            int end = -1;

            // find start and end indexes
            for (int i = 0; i < 8; i++)
            {
                if (this.Cities.ElementAt(i).Key == orig)
                {
                    // save start index
                    start = i;
                }
                else if (this.Cities.ElementAt(i).Key == dest)
                {
                    // save end index
                    end = i;
                }

            }

            // now grab the cities between origin and destination in order
            if (start < end)
            {
                for (int i = start; i <= end; i++)
                {
                    if (this.Cities.ElementAt(i).Key == orig)
                    {
                        // add that to order's cities
                        orderCities.Add(this.Cities.ElementAt(i).Key, this.Cities.ElementAt(i).Value);

                    }
                    else if (this.Cities.ElementAt(i).Key == dest)
                    {
                        // add to order's cities and stop
                        orderCities.Add(this.Cities.ElementAt(i).Key, this.Cities.ElementAt(i).Value);
                        break;
                    }
                    else
                    {
                        // add to order's cities
                        orderCities.Add(this.Cities.ElementAt(i).Key, this.Cities.ElementAt(i).Value);
                    }

                }

            }
            else
            {
                for (int i = start; i >= end; i--)
                {
                    if (this.Cities.ElementAt(i).Key == orig)
                    {
                        // add that to order's cities
                        orderCities.Add(this.Cities.ElementAt(i).Key, this.Cities.ElementAt(i).Value);

                    }
                    else if (this.Cities.ElementAt(i).Key == dest)
                    {
                        // add to order's cities and stop
                        orderCities.Add(this.Cities.ElementAt(i).Key, this.Cities.ElementAt(i).Value);
                        break;
                    }
                    else
                    {
                        // add to order's cities
                        orderCities.Add(this.Cities.ElementAt(i).Key, this.Cities.ElementAt(i).Value);
                    }

                }

            }

            foreach (KeyValuePair<string, City> pair in orderCities)
            {
                Console.WriteLine(pair.Key);
            }

        }

        public void ReceiveCompleted()
        {

            // grab the completed orders dictionary, to be displayed
            

        }

        internal void CompleteOrder(Order order)
        {

            // generate an invoice for a completed order
            // need to know what invoice needs OR just do it here?

        }




    }
}
