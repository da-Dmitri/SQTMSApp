using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using MySqlConnector;
using System.Windows;

namespace TMS.Repositories
{
    public abstract class RepositoryBase
    {
        
        private string myConnectionString;
        //MySqlConnection conn;

        public RepositoryBase()
        {
            myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=gupajuse7256;database=loginDb";
        }
        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(myConnectionString);
        }
    }
}
