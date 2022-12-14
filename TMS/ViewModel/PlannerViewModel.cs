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
using System.Configuration;

namespace TMS.ViewModel
{
    internal class PlannerViewModel : ViewModelBase
    {

        /* Declaring the necesary objects */
        internal MySqlConnection connection = null;
        internal MySqlCommand cmd = null;
        internal MySqlDataReader rdr = null;
        public Dictionary<string, City> Cities = null;

        private double timePassed = 0;

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
        public ICommand ShowCompletedOrdersCommand { get; }
        public ICommand ShowActiveOrderViewCommand { get; }
        public ICommand ShowOnRouteViewCommand { get; }

        public double TimePassed
        {
            get { return timePassed; }
            set { timePassed = value; OnPropertyChanged(nameof(TimePassed)); }
        }

        public PlannerViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            ShowCompletedOrdersCommand = new ViewModelCommand(ExecuteShowCompletedOrdersCommand);
            ShowActiveOrderViewCommand = new ViewModelCommand(ExecuteShowActiveOrderCommand);
            ShowOnRouteViewCommand = new ViewModelCommand(ExecuteShowOnRouteViewCommand);

            ExecuteShowActiveOrderCommand(null);


            LoadCurrentUserData();
            string connectString = "SERVER=server=127.0.0.1;uid=root;pwd=" + "gupajuse7256" +
                       ";database=contracts;";
            this.Cities = new Dictionary<string, City>();
            CitiesSetup();
            connection = new MySqlConnection(connectString);
            cmd = new MySqlCommand();
            cmd.Connection = connection;

        }

        private void ExecuteShowOnRouteViewCommand(object obj)
        {
            CurrentChildView = new OnRouteViewModel();
            Caption = "OnRoute";
            Icon = IconChar.Truck;
        }

        private void ExecuteShowActiveOrderCommand(object obj)
        {
            CurrentChildView = new ActiveOrderViewModel();
            Caption = "Orders";
            Icon = IconChar.Book;

        }

        private void ExecuteShowCompletedOrdersCommand(object obj)
        {
            CurrentChildView = new CompletedOrdersViewModel();
            Caption = "Completed Orders";
            Icon = IconChar.Check;
        }

        //public PlannerViewModel(string thePassword)
        //{
        //    string connectString = "SERVER=server=127.0.0.1;uid=root;pwd=" + thePassword +
        //                           ";database=contracts;";
            
        //    connection = new MySqlConnection(connectString);
        //    CitiesSetup();
        //    cmd = new MySqlCommand();
        //    cmd.Connection = connection;
        //}

        //public PlannerViewModel(string theServer, string theDatabase,
        //                        string theUID, string thePassword)
        //{
        //    string connectString = "SERVER=" + theServer + ";" +
        //                           "DATABASE=" + theDatabase + ";" +
        //                           "UID=" + theUID + ";" +
        //                           "PASSWORD" + thePassword + ";";
        //    CitiesSetup();
        //    connection = new MySqlConnection(connectString);
        //    cmd = new MySqlCommand();
        //    cmd.Connection = connection;
        //}


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

        public void AddTrip(int theOrderNum)
        {
            Dictionary<string, string> carriers = new Dictionary<string, string>()
            {
                { "Windsor", "Planet Express" },
                { "Hamilton", "Tillman Transport" },
                { "Oshawa", "Planet Express" },
                { "Belleville", "Planet Express" },
                { "Ottawa", "We Haul" },
                { "Kingston", "Schooner s" },
                { "London", "Tillman Transport" },
                { "Toronto", "We Haul" }
            };
            string connectString = ConfigurationManager.AppSettings.Get("localDatabase");

            connection = new MySqlConnection(connectString);

            cmd.Connection = connection;
            /* Getting the job type */
            cmd.CommandText = "SELECT Job_Type FROM acceptedcontracts " +
                                  "Where OrderNumber = " + theOrderNum.ToString();
            
            connection.Open();
            string theJob = cmd.ExecuteScalar().ToString();
            /* Converting to online */
            if (!Int32.TryParse(theJob, out int jobType))
            {
                return;
            }
            connection.Close();

            /* Getting the origin city */
            cmd.CommandText = "SELECT Origin FROM acceptedcontracts " +
                              "Where OrderNumber = " + theOrderNum.ToString();
            connection.Open();
            string origin = cmd.ExecuteScalar().ToString();         // Origin from city
            connection.Close();

            /* Getting the destination city */
            cmd.CommandText = "SELECT Destination FROM acceptedcontracts " +
                              "Where OrderNumber = " + theOrderNum.ToString();
            connection.Open();
            string destination = cmd.ExecuteScalar().ToString();    // Destination of contract
            connection.Close();

            string currentLocationWest = origin;
            string currentLocationEast = origin;
            
            
            int kmEast = 0;
            int kmWest = 0;
            double hoursEast = 0;
            double hoursWest = 0;
            /* We are processing FTL trips */
            if (jobType >= 0)
            {
                hoursWest = hoursWest + 4;
                hoursEast = hoursEast + 4;
                while (currentLocationEast != destination || currentLocationWest != destination)
                {
                    /* Going East */
                    City nextEast = null;
                    if (!Cities.TryGetValue(currentLocationEast, out nextEast))
                    {
                        nextEast = new City("", 0, 0, "", "");
                    }

                    /* Going West */
                    City nextWest = null;
                    if (!Cities.TryGetValue(currentLocationWest, out nextWest))
                    {
                        nextWest = new City("", 0, 0, "", "");
                    }

                    if (nextEast.East != destination)
                    {
                        currentLocationEast = nextEast.East;
                        hoursEast += nextEast.Time;
                        kmEast += nextEast.KMs;
                    }
                    if (nextWest.West != destination)
                    {
                        currentLocationWest = nextWest.West;
                        hoursWest += nextWest.Time;
                        kmWest += nextWest.KMs;
                    }
                    if (nextEast.East == destination || nextWest.West == destination)
                    {
                        if (nextEast.East == destination)
                        {
                            hoursEast += nextEast.Time;
                            kmEast += nextEast.KMs;
                            string theCarrier;
                            Cities.TryGetValue(destination, out City tempKms);
                            hoursEast += tempKms.Time;
                            kmEast += tempKms.KMs;
                            carriers.TryGetValue(origin, out theCarrier);
                            cmd.CommandText = "INSERT trips (OrderNumber, CarrierName, Origin, Destination, Kilometers, Time) " +
                              "VALUES (" + theOrderNum.ToString() + ", '" + theCarrier + "', '" +
                              origin + "', '" + destination + "', " + kmEast + ", " + hoursEast + ");";
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                            break;
                        }
                        if (nextWest.West == destination)
                        {
                            hoursWest += nextWest.Time;
                            kmWest += nextWest.KMs;
                            string theCarrier;
                            Cities.TryGetValue(destination, out City tempKms);
                            hoursWest += tempKms.Time;
                            kmWest += tempKms.KMs;
                            carriers.TryGetValue(origin, out theCarrier);
                            cmd.CommandText = "INSERT trips (OrderNumber, CarrierName, Origin, Destination, Kilometers, Time) " +
                              "VALUES (" + theOrderNum.ToString() + ", '" + theCarrier + "', '" +
                              origin + "', '" + destination + "', " + kmWest + ", " + hoursWest + ");";
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                            break;
                        }
                    }
                }

            }
            
        }

        public MySqlConnection CompleteOrder(int orderNumber)
        {
            string myConnectionString = ConfigurationManager.AppSettings.Get("localDatabase");
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            cmd.Connection = connection;
            cmd.CommandText = "UPDATE acceptedcontracts SET Completed = 'True' WHERE OrderNumber =" +
                              orderNumber + ";";

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
           

           



            /* Back to the place where the return is accepted, we would do
             * connection.Open();
             * 
             * make use of the info
             * when done using it
             * 
             * connection.Close() */
            return connection;
        }


    }
}
