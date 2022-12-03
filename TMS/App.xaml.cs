using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TMS.View;

namespace TMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            // create class for holding view state
            
            var loginView = new LoginView();
            loginView.Show();

                loginView.IsVisibleChanged += (s, ev) =>
                {
                        if (loginView.IsVisible == false && loginView.IsLoaded)
                        {
                            switch (loginView.txtUsername.Text)
                            {
                                case "buyer":
                                    var buyerView = new BuyerView();
                                    buyerView.Show();
                                    loginView.Close();
                                    break;

                                case "planner":
                                    break;

                                case "admin":
                                break;

                                default:
                                    break;
                            }
                        }

                };





        }
    }
}
