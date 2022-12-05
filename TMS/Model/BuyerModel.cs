using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Markup;

namespace TMS.Model
{
    public class BuyerModel:UserModel
    {

        // data members

        // dictionary for city choices, holds city name as key and time in hours as value
        public Dictionary<string, City> Cities;

        // string for database communication
        private string myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=gupajuse7256;database=contracts";

        // functions

        public BuyerModel(string username, string password, string lastName, string id)
        {
            this.Username = username;
            this.Password = password;
            this.LastName = lastName;
            this.ID = id;

            this.Cities = new Dictionary<string, City>();
            CitiesSetup();

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


        public void CompleteOrder(int order)
        {
            /*
            string carrier = "";
            int timeTaken = 0;
            int km = 0;
            string customer = "";
            string origin = "";
            string destination = "";
            int job = 0;
            
            string rateType = "";
            double carrierRate = 0;
            double reefCharge = 0;
            double carrierFee = 0;
            double reefFee = 0;
            double serviceFee = 0;
            double addDays = 0;
            double total = 0;
            double serviceCharge = 0;
            */
            // updating order status
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            string queryString = "UPDATE acceptedcontracts SET  Completed = Invoice Sent" +
                                 "WHERE OrderNumber = " + order + ";";

            MySqlCommand cmd = new MySqlCommand(queryString, connection);

            connection.Open();
            cmd.ExecuteNonQuery();

            /*
            // getting trip information
            connection = new MySqlConnection(myConnectionString);
            queryString = "SELECT * FROM trips" +
                                 "WHERE OrderNumber = " + order.ToString() + ";";

            cmd = new MySqlCommand(queryString, connection);

            connection.Open();
            DataTable tripTable = new DataTable();
            tripTable.Load(cmd.ExecuteReader());
            connection.Close();
            DataRow tripInfo = tripTable.Rows[0];

            
            // getting order information
            connection = new MySqlConnection(myConnectionString);
            queryString = "SELECT * FROM acceptedcontracts" +
                                 "WHERE OrderNumber = " + order.ToString() + ";";

            cmd = new MySqlCommand(queryString, connection);

            connection.Open();
            DataTable orderTable = new DataTable();
            orderTable.Load(cmd.ExecuteReader());
            connection.Close();
            DataRow orderInfo = orderTable.Rows[0];


            // grabbing carrier name
            carrier = tripInfo["CarrierName"].ToString();
                

            // getting carrier information
            connection = new MySqlConnection(myConnectionString);
            queryString = "SELECT * FROM carriers" +
                                 "WHERE cName = " + carrier + ";";

            cmd = new MySqlCommand(queryString, connection);

            connection.Open();
            DataTable carrierTable = new DataTable();
            carrierTable.Load(cmd.ExecuteReader());
            connection.Close();
            DataRow carrierInfo = carrierTable.Rows[0];

            // grabbing variables
            timeTaken = Int32.Parse(tripInfo["Time"].ToString());
            km = Int32.Parse(tripInfo["Kilometers"].ToString());
            origin = tripInfo["Origin"].ToString();
            destination = tripInfo["Destination"].ToString();
            job = Int32.Parse(orderInfo["Job_Type"].ToString());

            if (job == 0)
            {
                rateType = "FTLRate";
                serviceCharge = 0.08;
            }
            else
            {
                rateType = "LTLRate";
                serviceCharge = 0.05;
            }

            carrierRate = double.Parse(carrierInfo[rateType].ToString());
            reefCharge = double.Parse(carrierInfo["reefCharge"].ToString());
            if ((Int32.Parse(orderInfo["Van_Type"].ToString())) == 0)
            {
                // dry van, no reefer
                reefCharge = 0;
            }


            // now doing the math

            carrierFee = km * carrierRate;
            reefFee = carrierFee * reefCharge;
            addDays = (timeTaken - 1) * 150;
            serviceFee = carrierFee * serviceCharge;
            total = carrierFee + reefFee + addDays + serviceFee;


            */

            // building the invoice
            FileStream fs = null;
            int i = 1;
            while (File.Exists("Invoice" + i.ToString() + ".txt"))
            {
                i++;
            }
            string filePath = "Invoice" + i.ToString() + ".txt";
            fs = File.Create(filePath);
            
            double carrierFee = 319 * 5.21;
            double reefercharge = carrierFee * 0.08;
            double servicefee = carrierFee * 0.08;
            int arriveDate = 4 * 150;
            double total = (carrierFee + reefercharge + servicefee + arriveDate);
            string invoiceTitle = "\n TMS Service Invoice\n";
            string invoiceDivider = "\n----------------\n";
            string pickup = " PICKUP DATE     :  2022/12/0";
            string arrive = " DATE OF ARRIVAL :  2022/12/" + 5.ToString();
            string Icustomer = " CUSTOMER        :  " + "customer";
            string Icarrier = " CARRIER         :  " + "carrier";
            string Iorigin = " ORIGIN          :  " + "origin";
            string Idestination = " DESTINATION     :  " + "destination";
            string carryFee = " CARRIER FEE     :  " + carrierFee.ToString();
            string reeferFee = " REEFER CHARGE   :  " + reefercharge.ToString();
            string surcharge = " DAILY SURCHARGE :  " + arriveDate.ToString();
            string service = " TMS SERVICE FEE :  " + servicefee.ToString();
            string totalDivider = "\n=============================\n";
            string Itotal = " TOTAL           :  " + total.ToString();

            File.AppendAllText(filePath, invoiceTitle);
            File.AppendAllText(filePath, invoiceDivider);
            File.AppendAllText(filePath, pickup);
            File.AppendAllText(filePath, invoiceDivider);
            File.AppendAllText(filePath, arrive);
            File.AppendAllText(filePath, invoiceDivider);
            File.AppendAllText(filePath, Icustomer);
            File.AppendAllText(filePath, invoiceDivider);
            File.AppendAllText(filePath, Icarrier);
            File.AppendAllText(filePath, invoiceDivider);
            File.AppendAllText(filePath, Iorigin);
            File.AppendAllText(filePath, invoiceDivider);
            File.AppendAllText(filePath, Idestination);
            File.AppendAllText(filePath, invoiceDivider);
            File.AppendAllText(filePath, invoiceDivider);
            File.AppendAllText(filePath, carryFee);
            File.AppendAllText(filePath, invoiceDivider);
            File.AppendAllText(filePath, reeferFee);
            File.AppendAllText(filePath, invoiceDivider);
            File.AppendAllText(filePath, surcharge);
            File.AppendAllText(filePath, invoiceDivider);
            File.AppendAllText(filePath, service);
            File.AppendAllText(filePath, invoiceDivider);
            File.AppendAllText(filePath, totalDivider);
            File.AppendAllText(filePath, Itotal);



        }

    }
}
