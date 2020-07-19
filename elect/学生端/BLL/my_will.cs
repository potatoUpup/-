using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BLL
{
    public class my_will
    {
        SQLDAL.my_will dal = new SQLDAL.my_will();
        public DataTable sure(string id)
        {
            return dal.sure(id);
        }
        
    }
}
