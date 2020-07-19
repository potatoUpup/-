using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SQLDAL
{
    public class message_and_choose
    {
        public message_and_choose() {}
        
        //在个人信息界面显示导师信息
        public Model.message_and_choose GetModel(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,name,sex,position,groupnumber,phone,email,academy,research,grade,major from teapasswordtable ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Char,9)
};
            parameters[0].Value = id;
            Model.message_and_choose model = new Model.message_and_choose();
            DataTable dt = SQLDBhelper.ExecuteDataTable(strSql.ToString(), CommandType.Text, parameters);
            if (dt.Rows.Count > 0)
            {
                model.id = dt.Rows[0]["id"].ToString();
                model.name = dt.Rows[0]["name"].ToString();
                model.sex = dt.Rows[0]["sex"].ToString();
                model.position = dt.Rows[0]["position"].ToString();
                model.groupnumber = dt.Rows[0]["groupnumber"].ToString();
                model.phone = dt.Rows[0]["phone"].ToString();
                model.email = dt.Rows[0]["email"].ToString();
                model.academy = dt.Rows[0]["academy"].ToString();
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

        //导师查看团队信息
        public Model.message_and_choose GetGroupModel(string xxid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 Teamnumber,topicname,captainname,memberonename,membertwoname,memberthreename,topicintroduce from team ");
            strSql.Append(" where Teamnumber=@xxid");
            SqlParameter[] parameters = {
					new SqlParameter("@xxid", SqlDbType.Char,11)
};
            parameters[0].Value = xxid;
            Model.message_and_choose model = new Model.message_and_choose();
            DataTable dt = SQLDBhelper.ExecuteDataTable(strSql.ToString(), CommandType.Text, parameters);
            if (dt.Rows.Count > 0)
            {
                model.Teamnumber = dt.Rows[0]["Teamnumber"].ToString();
                model.topicname = dt.Rows[0]["topicname"].ToString();
                model.captainname = dt.Rows[0]["captainname"].ToString();
                model.memberonename = dt.Rows[0]["memberonename"].ToString();
                model.membertwoname = dt.Rows[0]["membertwoname"].ToString();
                model.memberthreename = dt.Rows[0]["memberthreename"].ToString();
                model.topicintroduce = dt.Rows[0]["topicintroduce"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        //时间安排
        public Model.message_and_choose time()
        {
            DataTable time = SQLDBhelper.ExecuteDataTable(string.Format("select * from time"));
            Model.message_and_choose model = new Model.message_and_choose();
            model.d1 = Convert.ToDateTime(time.Rows[0]["time1"].ToString());
            model.d4 = Convert.ToDateTime(time.Rows[0]["time4"].ToString());
            return model;
        }

        //预选结果
        public DataTable SelectionList(string teaid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select team.Teamnumber,team.topicname,team.captainname,team.captainid,team.memberonename,team.memberoneid,team.membertwoname,team.membertwoid,team.memberthreename,team.memberthreeid,team.topicintroduce from team,ischoose where team.teamnumber =ischoose.stuid and ischoose.teaid='" + teaid + "'");
            return SQLDBhelper.ExecuteDataTable(strSql.ToString());
        }

        //分配结果
        public DataTable GetSureList(string teaid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select team.Teamnumber,team.topicname,team.captainname,team.captainid,stupasswordtable.phone,team.memberonename,team.memberoneid,team.membertwoname,team.membertwoid,team.memberthreename,team.memberthreeid,team.topicintroduce from team,stupasswordtable,finish where team.teamnumber =finish.stuid and finish.ischeck='1' and stupasswordtable.id=team.teamnumber");
            strSql.Append(" and " + teaid);
            SqlParameter[] parameters = {
                    new SqlParameter("@teaid", SqlDbType.Char,9)
};
            parameters[0].Value = teaid;
            return SQLDBhelper.ExecuteDataTable(strSql.ToString());
        }

        //未预选
        public DataTable GetNoChooseList(string teaid,string term1,string term2)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select stuname,stuid,Subjectname from will where ((isaccess1='0' and firstwill='" + teaid + "') or (isaccess2='0' and secondwill='" + teaid + "') or (isaccess3='0' and thirdwill='" + teaid + "')) and stuid in(select account from stupasswordtable where grade='" + term1 + "' and major='" + term2 + "')");
            return SQLDBhelper.ExecuteDataTable(strSql.ToString());            
        }

        //未预选界面的“预选”功能
        public void Chooseing(string teaid,string xxid)
        {
            DataTable member = SQLDBhelper.ExecuteDataTable(string.Format("select captainname,captainid,memberonename,memberoneid,membertwoname,membertwoid,memberthreename,memberthreeid from team where teamnumber='{0}'", xxid));
            SqlDataReader readteaname = SQLDBhelper.ExecuteReader(string.Format("select name from teapasswordtable where account='{0}'", teaid));
            readteaname.Read();
            SqlDataReader t = SQLDBhelper.ExecuteReader(string.Format(" select * from will where stuid='{0}'and (firstwill='{1}' or secondwill='{2}' or thirdwill='{3}')", xxid, teaid, teaid, teaid));
            if (t.Read())
            {
                DataTable will1 = SQLDBhelper.ExecuteDataTable(string.Format("select * from will where stuid='{0}'and firstwill='{1}'", xxid, teaid));
                DataTable will2 = SQLDBhelper.ExecuteDataTable(string.Format("select * from will where stuid='{0}'and secondwill='{1}'", xxid, teaid));
                DataTable will3 = SQLDBhelper.ExecuteDataTable(string.Format("select * from will where stuid='{0}'and thirdwill='{1}'", xxid, teaid));
                if (will1.Rows.Count != 0)
                {
                    SQLDBhelper.ExecuteNonQuery(string.Format("update will set isaccess1 = '1' where stuid='{0}'and firstwill='{1}'", xxid, teaid));
                    //插入到ischoose表
                    SQLDBhelper.ExecuteNonQuery(string.Format("insert into ischoose(stuid,stuname,teaid,teaname) values('{0}','{1}','{2}','{3}')", member.Rows[0]["captainid"].ToString(), member.Rows[0]["captainname"].ToString(), teaid, readteaname[0].ToString()));

                }
                else if (will2.Rows.Count != 0)
                {
                    SQLDBhelper.ExecuteNonQuery(string.Format("update will set isaccess2 = '1' where stuid='{0}'and secondwill='{1}'", xxid, teaid));
                    SQLDBhelper.ExecuteNonQuery(string.Format("insert into ischoose(stuid,stuname,teaid,teaname) values('{0}','{1}','{2}','{3}')", member.Rows[0]["captainid"].ToString(), member.Rows[0]["captainname"].ToString(), teaid, readteaname[0].ToString()));

                }
                else if (will3.Rows.Count != 0)
                {
                    SQLDBhelper.ExecuteNonQuery(string.Format("update will set isaccess3 = '1' where stuid='{0}'and thirdwill='{1}'", xxid, teaid));
                    SQLDBhelper.ExecuteNonQuery(string.Format("insert into ischoose(stuid,stuname,teaid,teaname) values('{0}','{1}','{2}','{3}')", member.Rows[0]["captainid"].ToString(), member.Rows[0]["captainname"].ToString(), teaid, readteaname[0].ToString()));
                }
            }
        }

        //预选界面的"退选功能"
        public void WithdrawalTeam(string id,string teaid)
        {
            //先删除ischoose表数据                  
            SQLDBhelper.ExecuteNonQuery(string.Format("delete from ischoose where stuid='{0}' and teaid='{1}'", id, teaid));
            //再更新will表中的isaccess
            DataTable will1 = SQLDBhelper.ExecuteDataTable(string.Format("select * from will where isaccess1='1' and stuid='{0}'and firstwill='{1}'", id, teaid));
            DataTable will2 = SQLDBhelper.ExecuteDataTable(string.Format("select * from will where isaccess2='1' and stuid='{0}'and secondwill='{1}'", id, teaid));
            DataTable will3 = SQLDBhelper.ExecuteDataTable(string.Format("select * from will where isaccess3='1' and stuid='{0}'and thirdwill='{1}'", id, teaid));
            if (will1.Rows.Count != 0)
            {
                SQLDBhelper.ExecuteNonQuery(string.Format("update will set isaccess1='0'  where stuid='{0}' and firstwill='{1}'", id, teaid));
            }
            else if (will2.Rows.Count != 0)
            {
                SQLDBhelper.ExecuteNonQuery(string.Format("update will set isaccess2='0'  where stuid='{0}' and secondwill='{1}'", id, teaid));
            }
            else if (will3.Rows.Count != 0)
            {
                SQLDBhelper.ExecuteNonQuery(string.Format("update will set isaccess3='0'  where stuid='{0}' and thirdwill='{1}'", id, teaid));
            }
        }
    }
}
