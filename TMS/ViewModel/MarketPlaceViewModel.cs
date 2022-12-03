using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TMS.Model;

namespace TMS.ViewModel
{
    public class MarketPlaceViewModel : ViewModelBase
    {
        private string myConnectionString = "SERVER=159.89.117.198:3306;DATABASE=cmp;UID=DevOHST;PASSWORD=Snodgr4ss!";
        
        public Contract SelectedContract;

        public string MyConnection
        {
            get { return myConnectionString; }
            set 
            { 
                myConnectionString = value;
                OnPropertyChanged(nameof(MyConnection));
            }
        }


        // Constructor
        public MarketPlaceViewModel()
        {

        }


    }
}
