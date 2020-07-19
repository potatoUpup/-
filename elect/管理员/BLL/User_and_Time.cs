using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class User_and_Time
    {
        SQLDAL.User_and_Time user = new SQLDAL.User_and_Time();
        public bool Login(string userName, string userPassword)
        {
            return user.Login(userName, userPassword);
        }
        public Model.User_and_time time1()
        {
            return user.time1();
        }
        public void time2(string d1,string d2,string d3,string d4)
        {
            user.time2(d1,d2,d3,d4);
        }
    }
}
