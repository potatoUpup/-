using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class tea
    {
        SQLDAL.tea dal = new SQLDAL.tea();
        public void delect(string xxid)
        {
            dal.delect(xxid);
        }
        public void add(string Account, string Password, string Name, string Sex, string ID, string position, int number, int groupnumber, string Phone, string Email, string academy, string research, string Grade, string Major)
        {
            dal.add(Account, Password, Name, Sex, ID, position, number, groupnumber, Phone, Email, academy, research, Grade, Major);
        }
        public Model.tea updatetoselect(string xxid)
        {
            return dal.updatetoselect(xxid);
        }
        public void update(string Name, string Sex, string Grade, string academy, string position, string research, string Phone, string Email, int number, int groupnumber, string Major, string ID)
        {
            dal.update(Name, Sex, Grade, academy, position, research, Phone, Email, number, groupnumber, Major, ID);
        }
    }
}
