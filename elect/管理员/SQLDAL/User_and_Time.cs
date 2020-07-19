using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SQLDAL
{
    public partial class User_and_Time
    {
        public User_and_Time() { }
        public bool Login(string userName, string userPassword)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from manager");
            strSql.Append(" where account=@UserName and password=@UserPassword");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NChar,10),
                    new SqlParameter("@UserPassword", SqlDbType.NChar,10),};
            parameters[0].Value = userName;
            parameters[1].Value = userPassword;
            int n = Convert.ToInt32(SQLDBhelper.ExecuteScalar(strSql.ToString(), CommandType.Text, parameters));
            if (n == 1)
                return true;
            else
                return false;
        }
        public Model.User_and_time time1()
        {
            DataTable time = SQLDBhelper.ExecuteDataTable(string.Format("select * from time"));
            Model.User_and_time model = new Model.User_and_time();
            model.d1 = Convert.ToDateTime(time.Rows[0]["time1"].ToString());
            model.d2 = Convert.ToDateTime(time.Rows[0]["time2"].ToString());
            model.d3 = Convert.ToDateTime(time.Rows[0]["time3"].ToString());
            model.d4 = Convert.ToDateTime(time.Rows[0]["time4"].ToString());
            return model;
        }
        public void time2(string d1,string d2,string d3,string d4)
        {
            
            SQLDBhelper.ExecuteNonQuery(string.Format("update time set time1='{0}',time2='{1}',time3='{2}',time4='{3}'", d1, d2, d3, d4));
            
        }
    }
}
