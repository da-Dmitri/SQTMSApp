﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMS.Model;
using TMS.ViewModel;


namespace TMS.View
{
    /// <summary>
    /// Interaction logic for MarketplaceView.xaml
    /// </summary>
    public partial class MarketplaceView : UserControl
    {
        private string myConnectionString;
        private readonly MarketPlaceViewModel marketPlaceViewModel;


        public MarketplaceView()
        {
            this.marketPlaceViewModel = new MarketPlaceViewModel();
            InitializeComponent();

            MarketPlace.DataContext = GetMarketPlaceContracts();
   

            this.DataContext = marketPlaceViewModel;
            
    }


        private void btnAcceptContract_Click(object sender, RoutedEventArgs e)
        {
            if (MarketPlace.SelectedItem == null)
            {
                return;
            }
            else
            {
                AcceptContract();
            }

        }

        private DataTable GetMarketPlaceContracts()
        {
            string myConnectionString = "SERVER=159.89.117.198;DATABASE=cmp;UID=DevOSHT;PASSWORD=Snodgr4ss!";

            MySqlConnection connection = new MySqlConnection(myConnectionString);

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Contract", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            return dt;
        }

        private void AcceptContract()
        {
            DataRowView row_selected = MarketPlace.SelectedItem as DataRowView;
            Contract contract = new Contract();
            contract.ClientName = row_selected["Client_Name"].ToString();
            contract.JobType = (int)row_selected["Job_Type"];
            contract.Quantity = (int)row_selected["Quantity"];
            contract.Origin = row_selected["Origin"].ToString();
            contract.Destination = row_selected["Destination"].ToString();
            contract.Van_Type = (int)row_selected["Van_Type"];

            myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=gupajuse7256;database=contracts";

            MySqlConnection connection = new MySqlConnection(myConnectionString);

            MySqlCommand cmd = new MySqlCommand($"INSERT INTO acceptedcontracts VALUES (\"{contract.ClientName}\",{contract.JobType},{contract.Quantity},\"{contract.Origin}\",\"{contract.Destination}\",{contract.Van_Type});", connection);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            MarketPlace.DataContext = GetMarketPlaceContracts();
        }
    }
}