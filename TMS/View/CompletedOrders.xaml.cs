using MySqlConnector;
using System;
using System.Collections.Generic;
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
using TMS.ViewModel;

namespace TMS.View
{
    /// <summary>
    /// Interaction logic for CompletedOrders.xaml
    /// </summary>
    public partial class CompletedOrders : UserControl
    {
        private readonly CompletedOrdersViewModel completedOrdersViewModel;
        

        public CompletedOrders()
        {
            this.completedOrdersViewModel = new CompletedOrdersViewModel();
            InitializeComponent();
            CompletedContracts.DataContext = GetAcceptedContracts();

        }

        private DataTable GetAcceptedContracts()
        {
            string myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=gupajuse7256;database=contracts";

            MySqlConnection connection = new MySqlConnection(myConnectionString);

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM acceptedcontracts WHERE Completed = 'True'", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            return dt;
        }

        private void btnRefreshOrder_Click(object sender, RoutedEventArgs e)
        {
            CompletedContracts.DataContext = GetAcceptedContracts();
        }
    }
}
