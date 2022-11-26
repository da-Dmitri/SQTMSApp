using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Edit(UserModel userModel);
        void Remove(UserModel userModel);
        UserModel GetByID(int id);
        UserModel GetByUserName(string userName);
        IEnumerable<UserModel> GetAll();

        
    }
}
