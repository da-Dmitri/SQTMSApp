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
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {

        private string myConnectionString;
        private readonly CustomerViewModel customerViewModel;

        public CustomerView()
        {
            this.customerViewModel = new CustomerViewModel();
            InitializeComponent();

            Customers.DataContext = GetAcceptedContracts();

            this.DataContext = customerViewModel;
        }

        private DataTable GetAcceptedContracts()
        {
            string myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=gupajuse7256;database=contracts";

            MySqlConnection connection = new MySqlConnection(myConnectionString);

            MySqlCommand cmd = new MySqlCommand("SELECT Client_Name FROM acceptedcontracts group by Client_Name", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            return dt;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Customers.DataContext = GetAcceptedContracts();
        }
    }
}
