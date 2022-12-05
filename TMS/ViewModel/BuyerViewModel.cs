using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TMS.Model;
using TMS.Repositories;
using TMS.View;

namespace TMS.ViewModel
{
    public class BuyerViewModel : ViewModelBase
    {
        // Fields
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        private bool _isViewVisible = true;
        private IUserRepository userRepository;


        // buyer object


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
            { _currentChildView = value;
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

        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        // commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowMarketPlaceViewCommand { get; }
        public ICommand ShowOrdersViewCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }
        public ICommand ShowCompletedOrdersCommand { get; }


        public BuyerViewModel()
        {

            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            // Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowMarketPlaceViewCommand = new ViewModelCommand(ExecuteShowMarketPlaceCommand);
            ShowOrdersViewCommand = new ViewModelCommand(ExecuteShowOrdersViewCommand);
            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);
            ShowCompletedOrdersCommand = new ViewModelCommand(ExecuteShowCompletedOrdersCommand);

            // defualt view
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowCompletedOrdersCommand(object obj)
        {
            CurrentChildView = new CompletedOrdersViewModel();
            Caption = "Completed Orders";
            Icon = IconChar.Check;
        }

        private void ExecuteShowCustomerViewCommand(object obj)
        {
            CurrentChildView = new CustomerViewModel();
            Caption = "Customers";
            Icon = IconChar.User;
        }

        private void ExecuteLogoutCommand(object obj)
        {
            IsViewVisible = false;
        }

        private void ExecuteShowOrdersViewCommand(object obj)
        {
            CurrentChildView = new OrderViewModel();
            Caption = "Orders";
            Icon = IconChar.Book;
        }

        private void ExecuteShowMarketPlaceCommand(object obj)
        {
            CurrentChildView = new MarketPlaceViewModel();
            Caption = "Marketplace";
            Icon = IconChar.Store; 
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
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
                CurrentUserAccount.DisplayName="User not logged in";
                // Hide child views.
            }
        }
    }
}
