using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using MySqlConnector;
using System.Windows;
using System.Configuration;

namespace TMS.Repositories
{
    public abstract class RepositoryBase
    {
        
        private string myConnectionString;
        //MySqlConnection conn;

        public RepositoryBase()
        {
            myConnectionString = ConfigurationManager.AppSettings.Get("userDatabase");
        }
        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(myConnectionString);
        }
    }
}
