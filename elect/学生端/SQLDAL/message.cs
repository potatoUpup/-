using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SQLDAL
{
    public class message
    {
        public message() {}
        //在个人信息界面显示导师信息
        public Model.message GetModel(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,name,sex,grade,major,class,phone,email,introduce,academy from stupasswordtable ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Char,11)
};
            parameters[0].Value = id;
            Model.message model = new Model.message();
            DataTable dt = SqlDbHelper.ExecuteDataTable(strSql.ToString(), CommandType.Text, parameters);
            if (dt.Rows.Count > 0)
            {
                model.id = dt.Rows[0]["id"].ToString();
                model.name = dt.Rows[0]["name"].ToString();
                model.sex = dt.Rows[0]["sex"].ToString();
                model.grade = dt.Rows[0]["grade"].ToString();
                model.major = dt.Rows[0]["major"].ToString();
                model.Class = dt.Rows[0]["class"].ToString();
                model.phone = dt.Rows[0]["phone"].ToString();
                model.email = dt.Rows[0]["email"].ToString();
                model.introduce = dt.Rows[0]["introduce"].ToString();
                model.academy = dt.Rows[0]["academy"].ToString();              
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
