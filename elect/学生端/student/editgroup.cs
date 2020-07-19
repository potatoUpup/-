using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student
{
    public partial class editgroup : UserControl
    {
        DataTable dt;
        TextBox[] txts = new TextBox[3];
        TextBox[] txts1 = new TextBox[3];
        member[] mbs = new member[3];
        int t_idx = 0;
        bool[] added = new bool[3];
        SqlDataReader thisperson;
        DataTable studentlist;



        string id = null;
        public editgroup()
        {
            InitializeComponent();
        }
        void draw_controls()
        {
            int top = 210, left = 90, h = 30, y;
            int top1 = 210, left1 = 282, h1 = 30, y1;
            for (int i = 0; i < 3; i++)
            {
                y = top + h * i;
                txts[i] = new TextBox();
                txts[i].Width = 100;
                txts[i].Top = y;
                txts[i].Left = left;
                txts[i].Enabled = true;
                txts[i].Font = new System.Drawing.Font("微软雅黑", 9F);
                txts[i].Click += new System.EventHandler(this.check_click);
                this.Controls.Add(txts[i]);

                mbs[i] = new member();
            }
            for (int i = 0; i < 3; i++)
            {
                y1 = top1 + h1 * i;
                txts1[i] = new TextBox();
                txts1[i].Width = 100;
                txts1[i].Top = y1;
                txts1[i].Left = left1;
                txts1[i].Enabled = true;
                txts1[i].Font = new System.Drawing.Font("微软雅黑", 9F);
                txts1[i].Click += new System.EventHandler(this.check_click);
                this.Controls.Add(txts1[i]);
                mbs[i] = new member();
            }

        }
        public editgroup(string getid)
        {
            InitializeComponent();
            id = getid;
            teamid.Text = id;
            //captainid.Text = id;
            SqlDataReader thisperson = SqlDbHelper.ExecuteReader(string.Format("select name,major,grade from stupasswordtable where account='{0}'", id));
            thisperson.Read();
            studentlist = new DataTable();
            studentlist = SqlDbHelper.ExecuteDataTable(string.Format("select id,name from stupasswordtable where groupID='0' and major='{0}'", thisperson[1].ToString()));


        }
        
        private void editgroup_Load(object sender, EventArgs e)
        {
            //读取当前用户的姓名  学号 作为队长的信息
            SqlDataReader myname = SqlDbHelper.ExecuteReader(string.Format("select name,major,grade from stupasswordtable where account='{0}'", id));
            myname.Read();
            captainname.Text = myname[0].ToString();
            captainid.Text = id;
            draw_controls();
            DataTable team=SqlDbHelper.ExecuteDataTable(string.Format("select * from team where captainid='{0}'",id));
            if(team.Rows.Count!=0)
            {
                topicname.Text = team.Rows[0]["topicname"].ToString();
                topicintroduce.Text = team.Rows[0]["topicintroduce"].ToString();
                txts1[0].Text = team.Rows[0]["memberonename"].ToString();
                txts[0].Text = team.Rows[0]["memberoneid"].ToString();
                txts1[1].Text = team.Rows[0]["membertwoname"].ToString();
                txts[1].Text = team.Rows[0]["membertwoid"].ToString();
                txts1[2].Text = team.Rows[0]["memberthreename"].ToString();
                txts[2].Text = team.Rows[0]["memberthreeid"].ToString();
            }
            string a = myname[1].ToString();
            dt = SqlDbHelper.ExecuteDataTable(string.Format("select id,name from stupasswordtable where groupID='0' and major='{0}'",myname[1].ToString()));
            dataGridView1.DataSource = dt;
        }
        
        void check_click(object sender, EventArgs e)
        {
            for (int i = 0; i < txts.Length; i++)
            {
                if (txts[i] == sender)
                {
                    t_idx = i;
                    txts[i].BackColor = Color.Pink;
                }
                else
                    txts[i].BackColor = Color.White;
            }
        }
        
        class member
        {
            public string id1;
            public string name;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d_idx = dataGridView1.CurrentRow.Index;
            txts[t_idx].Text = dt.Rows[d_idx][0].ToString();
            txts1[t_idx].Text = dt.Rows[d_idx][1].ToString();
            mbs[t_idx].id1 = (string)dt.Rows[d_idx][0];
            mbs[t_idx].name = dt.Rows[d_idx][1].ToString();
            added[t_idx] = true;
        }

        //保存团队
        private void createteam_Click_1(object sender, EventArgs e)
        {
            string[,] teamlist = new string[3, 2];//记录保存编辑的成员信息
            DataTable team = SqlDbHelper.ExecuteDataTable(string.Format("select * from team where teamnumber='{0}'", teamid.Text));
            //在重新编辑时，把旧队员的groupID重新设置为0
            if (team.Rows.Count != 0)
            {
                //在重新编辑时，把旧队员的groupID重新设置为0
                //删除原有团队信息
                DataTable member1 = SqlDbHelper.ExecuteDataTable(string.Format("select captainid,memberoneid,membertwoid,memberthreeid from team where teamnumber='{0}' ",teamid.Text));
                SqlDbHelper.ExecuteNonQuery(string.Format("update stupasswordtable set groupID='0' where account='{0}' or account='{1}' or account='{2}' or account='{3}'", member1.Rows[0]["captainid"].ToString(), member1.Rows[0]["memberoneid"].ToString(), member1.Rows[0]["membertwoid"].ToString(), member1.Rows[0]["memberthreeid"].ToString()));
                DataTable teamdt = SqlDbHelper.ExecuteDataTable(string.Format("select * from team where teamnumber='{0}'", teamid.Text));
                teamdt.Rows[0].Delete();
                SqlDbHelper.updatedatatable(teamdt, "team");

                //最后把新团队的信息填充到team表，和更新学生账户表groupID
                DataTable dt = new DataTable();
                dt = SqlDbHelper.ExecuteDataTable("select * from team where 1=2");
                DataRow dr = dt.NewRow();


                for (int i = 0; i < 3;i++ )
                {
                    if(!ismember(txts1[i].Text, txts[i].Text))
                    {
                        MessageBox.Show(string.Format("成员{0}不是本系统用户", i + 1));
                        return;
                    }
                }

                for (int i = 0; i < 3;i++)
                {
                    teamlist[i, 0] = txts[i].Text;//学号
                    teamlist[i, 1] = txts1[i].Text;//姓名
                }
                if ((teamlist[1, 0] == teamlist[0, 0]&&teamlist[1,0]!=null) || (teamlist[2, 0] == teamlist[1, 0]&&teamlist[2,0]!=null) || (teamlist[2, 0] == teamlist[0, 0]&&teamlist[2,0]!=null))
                {
                    MessageBox.Show("组员之间存在重复信息");
                    return;
                }

                dr["teamnumber"] = teamid.Text;
                dr["topicname"] = topicname.Text.Trim();
                dr["topicintroduce"] = topicintroduce.Text.Trim();
                dr["captainid"] = captainid.Text;
                dr["captainname"] = captainname.Text;

                for (int i = 0; i < 3;i++ )
                {
                    dr[i*2 + 5] = teamlist[i, 0];
                    dr[i*2 + 4] = teamlist[i, 1];
                }

                dt.Rows.Add(dr);
                SqlDbHelper.UpdateDataTable(dt, "team");
                SqlDbHelper.ExecuteNonQuery(string.Format("update stupasswordtable set groupID='{0}' where account='{0}' or account='{1}' or account='{2}' or account='{3}'", teamid.Text, txts[0].Text, txts[1].Text, txts[2].Text));
                label1.Text = "已重新编辑团队！";
            }

            //在没有团队的情况下，创建团队
            else
            {
                DataTable dt = new DataTable();
                dt = SqlDbHelper.ExecuteDataTable("select * from team where 1=2");
                DataRow dr = dt.NewRow();

                for (int i = 0; i < 3; i++)
                {
                    if (!ismember(txts1[i].Text, txts[i].Text))
                    {
                        MessageBox.Show(string.Format("成员{0}不是本系统用户", i + 1));
                        return;
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    teamlist[i, 0] = txts[i].Text;//学号
                    teamlist[i, 1] = txts1[i].Text;//姓名
                }


                if ((teamlist[1, 0] == teamlist[0, 0] && teamlist[1, 0] != "") || (teamlist[2, 0] == teamlist[1, 0] && teamlist[2, 0] != "") || (teamlist[2, 0] == teamlist[0, 0] && teamlist[2, 0] != ""))
                {
                    MessageBox.Show("组员之间存在重复信息");
                    return;
                }

                dr["teamnumber"] = teamid.Text;
                dr["topicname"] = topicname.Text.Trim();
                dr["topicintroduce"] = topicintroduce.Text.Trim();
                dr["captainid"] = captainid.Text;
                dr["captainname"] = captainname.Text;

                for (int i = 0; i < 3; i++)
                {
                    dr[i * 2 + 5] = teamlist[i, 0];
                    dr[i * 2 + 4] = teamlist[i, 1];
                }

                dt.Rows.Add(dr);
                SqlDbHelper.UpdateDataTable(dt, "team");                
                label1.Text = "已创建团队！";
                SqlDbHelper.ExecuteNonQuery(string.Format("update stupasswordtable set groupID='{0}' where account='{0}' or account='{1}' or account='{2}' or account='{3}'", teamid.Text, mbs[0].id1, mbs[1].id1, mbs[2].id1));
            }
        }

        /// <summary>
        /// 判断手动添加的成员是否是系统用户
        /// </summary>
        /// <param name="name">成员的姓名</param>
        /// <param name="id">成员的id</param>
        /// <returns></returns>
        bool ismember(string name,string id)
        {
            if(name=="" && id=="")
            {
                return true;
            }
            else
            {
                using (SqlConnection con = new SqlConnection(SqlDbHelper.ConnectionString))
                {
                    con.Open();
                    SqlCommand sc = new SqlCommand();
                    sc.Connection = con;
                    sc.CommandText = "select count(*) from stupasswordtable where name=@NAME and id=@ID";
                    SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@NAME", name), new SqlParameter("@ID", id) };
                    sc.Parameters.AddRange(sp);

                    int num = (int)sc.ExecuteScalar();
                    if (num == 1)
                        return true;
                    else
                        return false;
                }
            }
            
        }



        /**************************************************************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            SqlDataReader myname = SqlDbHelper.ExecuteReader(string.Format("select name,major,grade from stupasswordtable where account='{0}'", id));
            myname.Read();
            DataTable dtt = new DataTable();
            dtt = SqlDbHelper.ExecuteDataTable(string.Format("select id,name from stupasswordtable where groupID='0' and major='{0}'", myname[1].ToString()));
            if (!CompareDataTable(studentlist,dtt))
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
                this.dataGridView1.DataSource = dtt;
                studentlist.Rows.Clear();
                studentlist = dtt;
            }
            //DataTable flash = new DataTable();
            //flash = SqlDbHelper.ExecuteDataTable(string.Format("select id,name from stupasswordtable where groupID='0' and major='{0}'", myname[1].ToString()));
            //DataTable  t= 
            //dataGridView1.DataSource = dtt;
        }


        private static bool CompareColumn(System.Data.DataColumnCollection dcA, System.Data.DataColumnCollection dcB)
        {
            if (dcA.Count == dcB.Count)
            {
                foreach (DataColumn dc in dcA)
                {
                    //找相同字段名称 
                    if (dcB.IndexOf(dc.ColumnName) > -1)
                    {
                        //测试数据类型 
                        if (dc.DataType != dcB[dcB.IndexOf(dc.ColumnName)].DataType)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        ///   <summary> 
        ///   比较两个DataTable内容是否相等，先是比数量，数量相等就比内容 
        ///   </summary> 
        ///   <param   name= "dtA "> </param> 
        ///   <param   name= "dtB "> </param> 
        public static bool CompareDataTable(DataTable dtA, DataTable dtB)
        {
            if (dtA.Rows.Count == dtB.Rows.Count)
            {
                if (CompareColumn(dtA.Columns, dtB.Columns))
                {
                    //比内容 
                    for (int i = 0; i < dtA.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtA.Columns.Count; j++)
                        {
                            if (!dtA.Rows[i][j].Equals(dtB.Rows[i][j]))
                            {
                                return false;
                            }
                        }
                    }
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
}
