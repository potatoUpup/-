using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class message
    {
        SQLDAL.message dal = new SQLDAL.message();
        public Model.message GetModel(string id)
        {
            return dal.GetModel(id);
        }
    }
}
