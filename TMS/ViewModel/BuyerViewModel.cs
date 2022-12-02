using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TMS.Model;
using TMS.Repositories;

namespace TMS.ViewModel
{
    public class BuyerViewModel : ViewModelBase
    {
        // Fields
        private UserAccountModel _currentUserAccount;
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

        public BuyerViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
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
