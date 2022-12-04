using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace TMS.ViewModel
{
    internal class PlannerViewModel
    {
        /* Declaring the necesary objects */
        internal MySqlConnection connection = null;
        internal MySqlCommand cmd = null;
        internal MySqlDataReader rdr = null;


        public PlannerViewModel(string thePassword, string theTable)
        {
            string connectString = "SERVER=server=127.0.0.1;uid=root;pwd=" + thePassword +
                                   ";database=contracts;";
            connection = new MySqlConnection(connectString);
            cmd = new MySqlCommand();
            cmd.Connection = connection;
        }

        public PlannerViewModel(string theServer, string theDatabase,
                                string theUID, string thePassword,
                                string theTable)
        {
            string connectString = "SERVER=" + theServer + ";" +
                                   "DATABASE=" + theDatabase + ";" +
                                   "UID=" + theUID + ";" +
                                   "PASSWORD" + thePassword + ";";
            connection = new MySqlConnection(connectString);
            cmd = new MySqlCommand();
            cmd.Connection = connection;
        }

        public MySqlConnection GetOrderSummary()
        {
            cmd.CommandText = "SELECT * FROM acceptedcontracts" + ";";

            MySqlConnection ret = connection;

            /* Back to the place where the return is accepted, we would do
             * connection.Open();
             * 
             * make use of the info
             * when done using it
             * 
             * connection.Close() */
            return ret;
        }

        public MySqlConnection GetActiveOrderSummary()
        {
            cmd.CommandText = "SELECT * FROM acceptedcontracts" +
                              "WHERE Completed = False;";

            MySqlConnection ret = connection;

            /* Back to the place where the return is accepted, we would do
             * connection.Open();
             * 
             * make use of the info
             * when done using it
             * 
             * connection.Close() */
            return ret;
        }

        public MySqlConnection AddTrip(int theOrderNum,
                                       int theCarrier,
                                       string theOrigin,
                                       string theDestination)
        {
            cmd.CommandText = "INSERT INTO trips (OrderNumber, CarrierNumber, Origin, Destination) " +
                              "Values(" + theOrderNum.ToString() + ", " + theCarrier.ToString() +
                              ", '" + theOrigin + "', '" + theDestination + "');";

            MySqlConnection ret = connection;

            /* Back to the place where the return is accepted, we would do
             * connection.Open();
             * 
             * make use of the info
             * when done using it
             * 
             * connection.Close() */
            return ret;
        }
    }
}
