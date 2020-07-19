using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BLL
{
    public class stu
    {
        SQLDAL.stu dal= new SQLDAL.stu();
        public DataTable select(string com1, string com2)
        {
            return dal.select(com1, com2);
        }
        public void delect(string xxid)
        {
            dal.delect(xxid);
        }
        public void add(string Account, string Password, string Name, string Sex, string ID, string Grade, string Major, string Class, string Phone, string Email, string introduce, string academy, string groupID) 
        {
            dal.add(Account, Password, Name, Sex, ID, Grade, Major, Class, Phone,Email, introduce, academy, groupID) ;
        }
        public Model.stu updatetoselect(string xxid)
        {
            return dal.updatetoselect(xxid);
        }
        public void update(string Name, string Sex, string academy, string Major, string Grade, string Class, string Phone, string Email, string introduce, string ID)
        {
            dal.update(Name, Sex, academy, Major, Grade, Class, Phone, Email, introduce, ID);
        }

    }
}
