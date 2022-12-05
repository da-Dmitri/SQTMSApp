using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using TMS.Model;
using TMS.Repositories;
using TMS.View;
using FontAwesome.Sharp;
using System.Windows;
using System.Windows.Input;
using System.Threading;

namespace TMS.ViewModel
{
    internal class PlannerViewModel : ViewModelBase
    {

        /* Declaring the necesary objects */
        internal MySqlConnection connection = null;
        internal MySqlCommand cmd = null;
        internal MySqlDataReader rdr = null;
        public Dictionary<string, City> Cities;



        public PlannerViewModel(string thePassword)
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        private IUserRepository userRepository;


        // Properties
        public UserAccountModel CurrentUserAccount
        {
            get { return _currentUserAccount; }
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public ViewModelBase CurrentChildView
        {
            get { return _currentChildView; }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption
        {
            get { return _caption; }
            set { _caption = value; OnPropertyChanged(nameof(Caption)); }
        }
        public IconChar Icon
        {
            get { return _icon; }
            set { _icon = value; OnPropertyChanged(nameof(Icon)); }
        }

        // commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowActiveOrderViewCommand { get; }

        public PlannerViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowActiveOrderViewCommand = new ViewModelCommand(ExecuteShowActiveOrderCommand);

            ExecuteShowActiveOrderCommand(null);


            LoadCurrentUserData();

        }



        private void ExecuteShowActiveOrderCommand(object obj)
        {
            CurrentChildView = new OrderViewModel();
            Caption = "Orders";
            Icon = IconChar.Book;

        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            throw new NotImplementedException();
        }

        public PlannerViewModel(string thePassword, string theTable)
        {
            string connectString = "SERVER=server=127.0.0.1;uid=root;pwd=" + thePassword +
                                   ";database=contracts;";
            CitiesSetup();
            connection = new MySqlConnection(connectString);
            cmd = new MySqlCommand();
            cmd.Connection = connection;
        }

        public PlannerViewModel(string theServer, string theDatabase,
                                string theUID, string thePassword)
        {
            string connectString = "SERVER=" + theServer + ";" +
                                   "DATABASE=" + theDatabase + ";" +
                                   "UID=" + theUID + ";" +
                                   "PASSWORD" + thePassword + ";";
            CitiesSetup();
            connection = new MySqlConnection(connectString);
            cmd = new MySqlCommand();
            cmd.Connection = connection;
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


        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUserName(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                {
                    CurrentUserAccount.UserName = user.Username;
                    CurrentUserAccount.DisplayName = $"{user.FirstName} {user.LastName}";
                    CurrentUserAccount.ProfilePicture = null;
                };
            }
            else
            {
                CurrentUserAccount.DisplayName = "User not logged in";
                // Hide child views.
            }
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
            string currentCity;

            //cmd.CommandText = "INSERT INTO trips (OrderNumber, CarrierNumber, Origin, Destination) " +
            //                  "Values(" + theOrderNum.ToString() + ", " + theCarrier.ToString() +
            //                  ", '" + theOrigin + "', '" + theDestination + "');";

            // FTL will have no stop till destination
            // LTL will stop at every city in route till destination
            string destination;
            while(currentcity == destination) 
            {
                cmd.CommandText = "SELECT Origin FROM acceptedContracts "
            }

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
