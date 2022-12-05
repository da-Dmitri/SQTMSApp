using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
    /// Interaction logic for OnRouteView.xaml
    /// </summary>
    public partial class OnRouteView : UserControl
    {
        private readonly OnRouteViewModel onRouteViewModel;

        private double _TimeRemaining = 0;
        private int orderNum = 0;

        public double TimeRemaining
        {
            get { return _TimeRemaining; }
            set { _TimeRemaining = value; }
        }
        public OnRouteView()
        {
            onRouteViewModel = new OnRouteViewModel();
            InitializeComponent();
            trips.DataContext = GetTrips();
            this.DataContext = onRouteViewModel;
        }

        private DataTable GetTrips()
        {
            string myConnectionString = ConfigurationManager.AppSettings.Get("localDatabase");

            MySqlConnection connection = new MySqlConnection(myConnectionString);

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM trips", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            return dt;
        }

        private void btnSimulateTime_Click(object sender, RoutedEventArgs e)
        {
            TimeRemaining -= 12;
            if (TimeRemaining <= 0)
            {
                string myConnectionString = ConfigurationManager.AppSettings.Get("localDatabase");

                MySqlConnection connection = new MySqlConnection(myConnectionString);

                // delete entry from trip table
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM trips WHERE OrderNumber = {orderNum}", connection);
                connection.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                connection.Close();

                // complete order
                PlannerViewModel plannerViewModel = new PlannerViewModel();
                plannerViewModel.CompleteOrder(orderNum);

                BuyerModel buyerModel = new BuyerModel();

                trips.DataContext = GetTrips();

            }
        }

        private void btnRefreshOrder_Click(object sender, RoutedEventArgs e)
        {
            trips.DataContext = GetTrips();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row_selected = trips.SelectedItem as DataRowView;
            TimeRemaining = (double)row_selected["Time"];
            orderNum = (int)row_selected["OrderNumber"];
        }
    }
}
