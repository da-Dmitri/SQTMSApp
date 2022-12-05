using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ActiveOrderView.xaml
    /// </summary>
    public partial class ActiveOrderView : UserControl
    {
        private readonly ActiveOrderViewModel activeOrderViewModel;

        private PlannerViewModel plannerViewModel = new PlannerViewModel();

        private Order newOrder = new Order();

        public ActiveOrderView()
        {
            this.activeOrderViewModel = new ActiveOrderViewModel();
            InitializeComponent();
            AcceptedContracts.DataContext = GetAcceptedContracts();
            this.DataContext = this.activeOrderViewModel;
        }

        private DataTable GetAcceptedContracts()
        {
            string myConnectionString = ConfigurationManager.AppSettings.Get("localDatabase");

            MySqlConnection connection = new MySqlConnection(myConnectionString);

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM acceptedcontracts WHERE Completed = 'False'", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            return dt;
        }

        private void btnRefreshOrder_Click(object sender, RoutedEventArgs e)
        {
            AcceptedContracts.DataContext = GetAcceptedContracts();
        }

        private void btnCompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row_selected = AcceptedContracts.SelectedItem as DataRowView;
            int orderNum = (int)row_selected["OrderNumber"];
            

            plannerViewModel = new PlannerViewModel();
            plannerViewModel.AddTrip(orderNum);
            AcceptedContracts.DataContext = GetAcceptedContracts();
            // Order object
            // Contract
            // Client name


        }
    }
}
