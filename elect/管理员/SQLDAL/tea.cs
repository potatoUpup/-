using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SQLDAL
{
    public class tea
    {
        public tea() { }
        public void delect(string xxid)
        {
            SQLDBhelper.ExecuteNonQuery(string.Format("delete from teapasswordtable where id='{0}'", xxid));
        }
        public void add(string Account, string Password, string Name, string Sex, string ID, string position, int number, int groupnumber, string Phone, string Email, string academy, string research, string Grade, string Major)
        {
            SQLDBhelper.ExecuteNonQuery(string.Format("insert into teapasswordtable values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", Account, Password, Name, Sex, ID, position, number, groupnumber, Phone, Email, academy, research, Grade, Major));
        }
        public Model.tea updatetoselect(string xxid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from teapasswordtable where id='" + xxid + "' ");
            Model.tea model = new Model.tea();
            DataTable dt = SQLDBhelper.ExecuteDataTable(strSql.ToString());
            if (dt.Rows.Count > 0)
            {
                model.name = dt.Rows[0]["Name"].ToString();
                model.sex = dt.Rows[0]["Sex"].ToString();
                model.id = dt.Rows[0]["ID"].ToString();
                model.password = dt.Rows[0]["password"].ToString();
                model.position = dt.Rows[0]["position"].ToString();
                model.number = dt.Rows[0]["number"].ToString();
                model.groupnumber = dt.Rows[0]["groupnumber"].ToString();
                model.phone = dt.Rows[0]["phone"].ToString();
                model.email = dt.Rows[0]["Email"].ToString();
                model.academy = dt.Rows[0]["Academy"].ToString();
                model.research = dt.Rows[0]["research"].ToString();
                model.grade = dt.Rows[0]["grade"].ToString();
                model.major = dt.Rows[0]["major"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        public void update(string Name, string Sex, string Grade, string academy, string position, string research, string Phone, string Email, int number,int groupnumber, string Major,string ID)
        {
            SQLDBhelper.ExecuteNonQuery(string.Format("update teapasswordtable set name='{0}', sex='{1}', grade='{2}',academy='{3}',  position='{4}', research='{5}', phone='{6}' , email='{7}' , number='{8}' , groupnumber='{9}', major='{10}' where id='{11}'", Name, Sex, Grade, academy, position, research, Phone, Email,number,groupnumber, Major, ID));
        }
    }
}
