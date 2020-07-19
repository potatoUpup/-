using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace SQLDAL
{
    public partial class my_will
    {
        public my_will() { }
        public DataTable sure(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct teapasswordtable.name,teapasswordtable.id,teapasswordtable.phone from teapasswordtable ,finish,stupasswordtable where teapasswordtable.id=finish.teaid and finish.stuid=stupasswordtable.groupID and stupasswordtable.groupID in(select groupID from stupasswordtable where id='" + id + "') and finish.ischeck='1'");
            return SqlDbHelper.ExecuteDataTable(strSql.ToString());
        }
    }
}
