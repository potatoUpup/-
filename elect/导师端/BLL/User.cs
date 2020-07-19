using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class User
    {
        SQLDAL.User user = new SQLDAL.User();
        public bool Login(string userName, string userPassword)
        {
            return user.Login(userName, userPassword);
        }

        public bool Update(Model.User model)
        {
            return user.Update(model);
        }
    }
}
