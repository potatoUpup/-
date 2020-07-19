using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace student
{
    class SqlDbHelper
    {
        //private static string connString = "server=.;database=system2;Trusted_Connection=SSPI";
        private static string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static string ConnectionString
        {
            get { return connString; }
            set { connString = value; }
        }

        /// <summary>
        /// 执行一个查询,并返回查询结果
        /// </summary>
        /// <param name="commandText">要执行的SQL语句</param>
        /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
        /// <param name="parameters">Transact-SQL 语句或存储过程的参数数组</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            DataTable data = new DataTable();//实例化DataTable，用于装载查询结果集
            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;//设置command的CommandType为指定的CommandType
                    //如果同时传入了参数，则添加这些参数
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    //通过包含查询SQL的SqlCommand实例来实例化SqlDataAdapter
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    adapter.Fill(data);//填充DataTable
                }
            }
            return data;
        }
        public static DataTable ExecuteDataTable(string commandText)
        {
            return ExecuteDataTable(commandText, CommandType.Text, null);
        }
        /// <summary>
        /// 执行一个查询,并返回查询结果
        /// </summary>
        /// <param name="commandText">要执行的SQL语句</param>
        /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
        /// <returns>返回查询结果集</returns>
        public static DataTable ExecuteDataTable(string commandText, CommandType commandType)
        {
            return ExecuteDataTable(commandText, commandType, null);
        }




        /// <summary>
        /// 将 CommandText 发送到 Connection 并生成一个 SqlDataReader。
        /// </summary>
        /// <param name="commandText">要执行的SQL语句</param>
        /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
        /// <param name="parameters">Transact-SQL 语句或存储过程的参数数组</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connString);
            SqlCommand command = new SqlCommand(commandText, connection);
            command.CommandType = commandType;
            //如果同时传入了参数，则添加这些参数
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            connection.Open();
            //CommandBehavior.CloseConnection参数指示关闭Reader对象时关闭与其关联的Connection对象
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 将 CommandText 发送到 Connection 并生成一个 SqlDataReader。
        /// </summary>
        /// <param name="commandText">要执行的查询SQL文本命令</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(commandText, CommandType.Text, null);
        }
        /// <summary>
        /// 将 CommandText 发送到 Connection 并生成一个 SqlDataReader。
        /// </summary>
        /// <param name="commandText">要执行的SQL语句</param>
        /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string commandText, CommandType commandType)
        {
            return ExecuteReader(commandText, commandType, null);
        }




        /// <summary>
        /// 从数据库中检索单个值（例如一个聚合值）。
        /// </summary>
        /// <param name="commandText">要执行的SQL语句</param>
        /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
        /// <param name="parameters">Transact-SQL 语句或存储过程的参数数组</param>
        /// <returns></returns>
        public static Object ExecuteScalar(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            object result = null;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;//设置command的CommandType为指定的CommandType
                    //如果同时传入了参数，则添加这些参数
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    connection.Open();//打开数据库连接
                    result = command.ExecuteScalar();
                }
            }
            return result;//返回查询结果的第一行第一列，忽略其它行和列
        }
        /// <summary>
        /// 从数据库中检索单个值（例如一个聚合值）。
        /// </summary>
        /// <param name="commandText">要执行的查询SQL文本命令</param>
        /// <returns></returns>
        public static Object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(commandText, CommandType.Text, null);
        }
        /// <summary>
        /// 从数据库中检索单个值（例如一个聚合值）。
        /// </summary>
        /// <param name="commandText">要执行的SQL语句</param>
        /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
        /// <returns></returns>
        public static Object ExecuteScalar(string commandText, CommandType commandType)
        {
            return ExecuteScalar(commandText, commandType, null);
        }




        /// <summary>
        /// 对数据库执行增删改操作
        /// </summary>
        /// <param name="commandText">要执行的SQL语句</param>
        /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
        /// <param name="parameters">Transact-SQL 语句或存储过程的参数数组</param>
        /// <returns>返回执行操作受影响的行数</returns>
        public static int ExecuteNonQuery(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;//设置command的CommandType为指定的CommandType
                    //如果同时传入了参数，则添加这些参数
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    connection.Open();//打开数据库连接
                    count = command.ExecuteNonQuery();
                }
            }
            return count;//返回执行增删改操作之后，数据库中受影响的行数
        }
        /// <summary>
        /// 对数据库执行增删改操作
        /// </summary>
        /// <param name="commandText">要执行的查询SQL文本命令</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(commandText, CommandType.Text, null);
        }
        /// <summary>
        /// 对数据库执行增删改操作
        /// </summary>
        /// <param name="commandText">要执行的SQL语句</param>
        /// <param name="commandType">要执行的查询语句的类型，如存储过程或者SQL文本命令</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(commandText, commandType, null);
        }



        /// <summary>
        /// 对相应数据表进行更新操作
        /// </summary>
        /// <param name="dtsource"></param>
        /// <param name="tablename"></param>
        public static void updatedatatable(DataTable dtsource,string tablename)
        {
            using(SqlConnection con=new SqlConnection(connString))
            {
                string s = string.Format("select * from {0}", tablename);
                SqlCommand command = new SqlCommand(s, con);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                SqlCommandBuilder commandbuilder = new SqlCommandBuilder(adapter);
                adapter.Update(dtsource);
            }
        }
        public static DataSet GetDataSet(string sql)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sql,con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                try
                {
                    con.Open();
                    da.Fill(ds);
                    return ds;

                }
                catch(Exception)
                {
                    throw;
                }
            }
        }
        public DataSet GetAllClasses()
        {

            StringBuilder sql = new StringBuilder().Append("select id from stupasswordtable");
            return SqlDbHelper.GetDataSet(sql.ToString());

        }

        public static bool isuser(string username,string password)//输入用户名 密码验证是否符合学生  返回true false
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand check = new SqlCommand("select password from stupasswordtable where account='" + username + "'",con);
                SqlDataReader pa = check.ExecuteReader();
                if(pa.HasRows)
                {
                    pa.Read();
                    if (pa[0].ToString() == password)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }  
                }
                else
                {
                    return false;
                }  
            }
        }



        public static DataRow getstumessage(string id)//输入学生id 返回学生对象信息
        {
            DataTable stu = new DataTable();
            string commandstr = "select * from stupasswordtable where account='" + id + "'";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlDataAdapter s = new SqlDataAdapter(commandstr, con);
                s.Fill(stu);
                if (stu.Rows.Count != 0)
                    return stu.Rows[0];
                else
                    return null;
            }
        }


        public static void editpassword(string username ,string newpassword)//传入用户账号 新密码进行更改
        {
            string sql="update stupasswordtable set password='"+newpassword+"'where account='"+username+"'";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand edit = new SqlCommand(sql, con);
                edit.ExecuteNonQuery();
            }
        }


        
        
        public static DataTable tea()//返回对应对象（表数据）
        {
            DataTable t = new DataTable();
            string command = "select * from teapasswordtable";
            using (SqlConnection con =new SqlConnection(connString))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command,con);
                adapter.Fill(t);
            }
            return t;
        }


        public static void createteam(string teamid,string topicname,string captainname,string captainid,string topicintroduce)//输入团队id 课题名称 队长名称 队长id  创建团队
        {
            string command = string.Format("insert into team(teamnumber,topicname,captainname,captainid,topicintroduce) values('{0}','{1}','{2}','{3}','{4}')", teamid, topicname, captainname, captainid, topicintroduce);
            using(SqlConnection con=new SqlConnection(connString))
            {
                con.Open();
                SqlCommand create = new SqlCommand(command, con);
                create.ExecuteNonQuery();
            }
        }


        public static bool addteam(string teamid,string membername,string memberid)//输入团队编号 个人姓名 个人学号加入团队 成功返回true 失败返回false
        {
            string command = string.Format("if (select memberonename from system.dbo.team where Teamnumber='{0}')is null begin update system.dbo.team set memberonename='{1}',memberoneid='{2}' end else if (select membertwoname from system.dbo.team where Teamnumber='{0}')is null begin update system.dbo.team set membertwoname='{1}',membertwoid='{2}' end else if(select memberthreename from system.dbo.team where Teamnumber='{0}')is null begin update system.dbo.team set memberthreename='{1}',memberthreeid='{2}' end", teamid, membername, memberid);
            using(SqlConnection con=new SqlConnection(connString))
            {
                con.Open();
                SqlCommand update = new SqlCommand(command, con);
                int n=update.ExecuteNonQuery();
                if(n==-1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static DataTable myteam(string id)
        {
            DataTable my_team=new DataTable();
            string command = string.Format("select * from system.dbo.team where captainid='{0}' or memberoneid='{0}' or membertwoid='{0}' or memberthreeid='{0}'", id);
            using(SqlConnection con=new SqlConnection(connString))
            {
                con.Open();
                SqlDataAdapter my = new SqlDataAdapter(command, con);
                my.Fill(my_team);
            }
            return my_team;
        }

        public static void UpdateDataTable(DataTable dtSource, string TableName)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                string sqlCmd = string.Format("SELECT * FROM [{0}]", TableName);
                SqlCommand dbCommand = new SqlCommand(sqlCmd, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCommand);
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.Update(dtSource);
            }
        }
        

    }
}

