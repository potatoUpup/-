using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace SQLDAL
{
    public partial class User
    {
        public User() {}
        //登录
        public bool Login(string userName, string userPassword)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from stupasswordtable");
            strSql.Append(" where account=@UserName and password=@UserPassword");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.Char,11),
                    new SqlParameter("@UserPassword", SqlDbType.VarChar,30),};
            parameters[0].Value = userName;
            parameters[1].Value = userPassword;
            int n = Convert.ToInt32(SqlDbHelper.ExecuteScalar(strSql.ToString(), CommandType.Text, parameters));
            if (n == 1)
                return true;
            else
                return false;
        }
        //修改密码
        public bool Update(Model.User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update stupasswordtable set ");
            strSql.Append("password=@Password");
            strSql.Append(" where account=@UserName ");
            SqlParameter[] parameters = {
					new SqlParameter("@Password", SqlDbType.VarChar,30),
					new SqlParameter("@UserName", SqlDbType.VarChar,11)};
            parameters[0].Value = model.Password;
            parameters[1].Value = model.UserName;
            int rows = SqlDbHelper.ExecuteNonQuery(strSql.ToString(), CommandType.Text, parameters);
            if (rows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
