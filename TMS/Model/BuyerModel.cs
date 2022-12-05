using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace TMS.Model
{
    public class BuyerModel:UserModel
    {

        // data members

        // string for database communication
        private string myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=gupajuse7256;database=contracts";

        // functions

        public BuyerModel(string username, string password, string lastName, string id)
        {
            this.Username = username;
            this.Password = password;
            this.LastName = lastName;
            this.ID = id;

        }

        public void CreateOrder(Contract contract)
        {

            // create a new Order for the given contract, add it to orders table

            MySqlConnection connection = new MySqlConnection(myConnectionString);
            string queryString = "INSERT INTO acceptedcontracts (Client_Name, Job_Type, Quantity, Origin, Destination, Van_Type, Completed) " +
                                 "VALUES ('" + contract.ClientName + "', " + contract.JobType.ToString() + ", " +
                                 contract.Quantity.ToString() + ", '" + contract.Origin + "', '" + contract.Destination + "', " +
                                 contract.Van_Type.ToString() + "Accepted" + ");";

            MySqlCommand cmd = new MySqlCommand(queryString, connection);

            connection.Open();
            cmd.ExecuteNonQuery();

        }



        public void CompleteOrder(int order)
        {

            MySqlConnection connection = new MySqlConnection(myConnectionString);
            string queryString = "UPDATE acceptedcontracts SET  Completed = Invoice Sent" +
                                 "WHERE OrderNumber = " + order + ";";

            MySqlCommand cmd = new MySqlCommand(queryString, connection);

            connection.Open();
            cmd.ExecuteNonQuery();


            // get carrier rate
            // multiply carrier rate by km

        }

    }
}
