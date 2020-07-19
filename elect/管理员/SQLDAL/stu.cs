using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SQLDAL
{
    public class stu
    {
        public stu() { }

        public DataTable select(string com1, string com2)
        {
            //DataTable dt = new DataTable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from stupasswordtable where grade='" + com1 + "' and major='" + com2 + "' ");
            return SQLDBhelper.ExecuteDataTable(strSql.ToString());
        }
        //public DataTable select(string com1, string com2)
        //{
        //    DataTable table = SQLDBhelper.ExecuteQuery("select * from stupasswordtable where grade='" + com1 + "' and major='" + com2 + "' ");
        //    return table;
        //}
        public void delect(string xxid)
        {
            SQLDBhelper.ExecuteNonQuery(string.Format("delete from stupasswordtable where id='{0}'", xxid));
        }       
        public void add(string Account, string Password, string Name, string Sex, string ID, string Grade, string Major, string Class, string Phone, string Email, string introduce, string academy, string groupID)
        {
            SQLDBhelper.ExecuteNonQuery(string.Format("insert into stupasswordtable values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", Account, Password, Name, Sex, ID, Grade, Major, Class, Phone, Email, introduce, academy, groupID));
        }
        public Model.stu updatetoselect(string xxid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from stupasswordtable where id='"+xxid+"' ");
            Model.stu model = new Model.stu();
            DataTable dt = SQLDBhelper.ExecuteDataTable(strSql.ToString());
            if (dt.Rows.Count > 0)
            {
                model.name = dt.Rows[0]["Name"].ToString();
                model.sex = dt.Rows[0]["Sex"].ToString();
                model.id = dt.Rows[0]["ID"].ToString();
                model.password = dt.Rows[0]["password"].ToString();
                model.grade = dt.Rows[0]["grade"].ToString();
                model.Class = dt.Rows[0]["class"].ToString();
                model.academy = dt.Rows[0]["Academy"].ToString();
                model.major = dt.Rows[0]["major"].ToString();
                model.phone = dt.Rows[0]["phone"].ToString();
                model.email = dt.Rows[0]["Email"].ToString();
                model.groupID = dt.Rows[0]["groupID"].ToString();
                model.introduce = dt.Rows[0]["introduce"].ToString();
                return model;
            }
            else
            {
                return null;
            }         
        }
        public void update(string Name, string Sex, string academy, string Major, string Grade, string Class, string Phone, string Email, string introduce, string ID)
        {
            SQLDBhelper.ExecuteNonQuery(string.Format("update stupasswordtable set name='{0}', sex='{1}', academy='{2}', major='{3}', grade='{4}', class='{5}', phone='{6}' , email='{7}' , introduce='{8}' where id='{9}'", Name,Sex,academy,Major,Grade,Class,Phone,Email,introduce,ID));
        }
    }
}
