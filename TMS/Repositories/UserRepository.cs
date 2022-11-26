using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TMS.Model;

namespace TMS.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select * from Users where UserName=@Username and Password=@Password";
                    command.Parameters.Add("@Username", MySqlDbType.VarChar, 50).Value = credential.UserName;
                    command.Parameters.Add("@Password", MySqlDbType.VarChar, 50).Value = credential.Password;
                    validUser = command.ExecuteScalar() == null ? false : true;
                }
                catch(MySqlException e)
                {
                    Console.WriteLine("Error connecting to database: " + e);
                    validUser = false;
                }

            }
            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public void Remove(UserModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}
