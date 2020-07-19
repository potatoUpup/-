using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class no_choose : UserControl
    {


        static int teacount = 0;//导师数
        static int stucount = 0;//学生数

        static bool[] used;//记录导师是否已经被访问
        static int[,] line;//记录匹配关系   图

        static int[] teacarrynumber;//导师带的学生数
        static int[] cnt;//导师当前已经匹配的学生数


        static int[,] match;//学生与导师的第n次匹配

        static string htea;//手动匹配的导师账号
        static string hstu;//手动匹配的学生账号

        static int maxnum;

        public no_choose()
        {
            InitializeComponent();
        }
        //button1的查询功能所对应的函数
        public void selectmess()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            DataTable selectmessage = SQLDBhelper.ExecuteDataTable(string.Format("select * from ischoose where stuid in (select account from stupasswordtable where grade='{0}' and major='{1}') and stuid not in (select stuid from finish)", comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString()));
            if (selectmessage.Rows.Count != 0)
            //dataGridView1.DataSource = selectmessage.DefaultView;
            {
                for (int i = 0; i < selectmessage.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(selectmessage);
                    dataGridView1.Rows[i].Cells[0].Value = selectmessage.Rows[i]["stuid"].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = selectmessage.Rows[i]["stuname"].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = selectmessage.Rows[i]["teaid"].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = selectmessage.Rows[i]["teaname"].ToString();
                }
            }            
        }
        //自动分配的查询功能
        private void button1_Click(object sender, EventArgs e)
        {
            selectmess();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.dataGridView2.DataSource = null;
            this.dataGridView3.DataSource = null;
            this.dataGridView2.Rows.Clear();
            this.dataGridView3.Rows.Clear();
            //DataTable nochoosestudent = SQLDBhelper.ExecuteDataTable(string.Format("select captainid,captainname from team where grade='{0}' and major='{1}' and account not in (select stuid from ischoose)", comboBox4.SelectedItem.ToString(), comboBox3.SelectedItem.ToString()));
            DataTable nochoosestudent = SQLDBhelper.ExecuteDataTable(string.Format("select captainname,captainid from team where captainid in (select account from stupasswordtable where grade='{0}' and major='{1}') and captainid not in (select stuid from finish)", comboBox4.SelectedItem.ToString(), comboBox3.SelectedItem.ToString()));

            if(nochoosestudent.Rows.Count!=0)
            {
                for(int i=0;i<nochoosestudent.Rows.Count;i++)
                {
                    dataGridView2.Rows.Add(nochoosestudent);
                    dataGridView2.Rows[i].Cells[0].Value = nochoosestudent.Rows[i]["captainid"].ToString();
                    dataGridView2.Rows[i].Cells[1].Value = nochoosestudent.Rows[i]["captainname"].ToString();
                }
            }
            this.dataGridView3.DataSource = null;

            DataTable tea = SQLDBhelper.ExecuteDataTable(string.Format("select name,id,number from teapasswordtable where grade='{0}' and major='{1}' and number>0", comboBox4.SelectedItem.ToString(), comboBox3.SelectedItem.ToString()));
            if(tea.Rows.Count!=0)
            {
                for(int i=0;i<tea.Rows.Count;i++)
                {
                    dataGridView3.Rows.Add(tea);
                    dataGridView3.Rows[i].Cells[0].Value = tea.Rows[i]["id"].ToString();
                    dataGridView3.Rows[i].Cells[1].Value = tea.Rows[i]["name"].ToString();
                    dataGridView3.Rows[i].Cells[2].Value = tea.Rows[i]["number"].ToString();
                }
            }
        }

        private void no_choose_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
            this.comboBox3.SelectedIndex = 0;
            this.comboBox4.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("列表中暂无学生互选信息");
                return;
            }
            DialogResult ischoose = MessageBox.Show("是否对列表的师生进行匹配？", "自动匹配", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ischoose == DialogResult.No)
                return;


            string[] stuid = new string[this.dataGridView1.Rows.Count];
            string[] teaid = new string[this.dataGridView1.Rows.Count];

            //int stucount = 0;
            bool issame = false;

            #region
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                //if (i == 0)
                //    stuid[stucount] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                for (int j = i; j >= 0; j--)
                {
                    if (j == i)
                        continue;
                    if (dataGridView1.Rows[j].Cells[0].Value.ToString() == dataGridView1.Rows[i].Cells[0].Value.ToString())
                    {
                        issame = true;
                    }
                }
                if (issame)
                { issame = false; }
                else
                {
                    stuid[stucount] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    stucount++;

                }
            }//读取已经被老师预选的学生学号  进入到stuid字符串数组中
            #endregion//读取团队编号进入数组stuid[]     stucount


            #region
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {

                for (int j = i; j >= 0; j--)
                {
                    if (j == i)
                        continue;
                    if (dataGridView1.Rows[j].Cells[2].Value.ToString() == dataGridView1.Rows[i].Cells[2].Value.ToString())
                    {
                        issame = true;
                    }
                }
                if (issame)
                { issame = false; }
                else
                {
                    teaid[teacount] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    teacount++;

                }
            }//读取已完成互选的导师工号
            #endregion//读取导师工号进入数组 teaid[]   teacount



            teacarrynumber = new int[teacount];//记录导师可带多少团队
            for (int i = 0; i < teacount; i++)
            {
                SqlDataReader read = SQLDBhelper.ExecuteReader(string.Format("select number from teapasswordtable where account='{0}'", teaid[i]));
                read.Read();
                teacarrynumber[i] = (int)read[0];
            }


            used = new bool[teacount];//标记是否已经进行访问
            cnt = new int[teacount];//记录已有多少匹配了
            for (int i = 0; i < teacount; i++)
            {
                cnt[i] = 0;
                used[i] = false;
            }



            line = new int[stucount, teacount];//声明一个二维数组 用于存储匹配的关系
            //int k = 0;


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int x = stuid.ToList().IndexOf(dataGridView1.Rows[i].Cells[0].Value.ToString());
                int y = teaid.ToList().IndexOf(dataGridView1.Rows[i].Cells[2].Value.ToString());

                line[x, y] = 1;
            }//读取标记已经完成互选的关系图  进入line数组
            
            //读取导师表中导师能带最大团队数
            SqlDataReader readmaxnumber = SQLDBhelper.ExecuteReader("select max(groupnumber) groupnumber from teapasswordtable");
            readmaxnumber.Read();
            maxnum=Convert.ToInt32(readmaxnumber[0]);


            match = new int[teacount, maxnum];



            //button1.Text = max_match().ToString();
            int number = max_match();
            for (int i = 0; i < teacount; i++)
            {
                for (int j = 0; j < maxnum; j++)
                {
                    if (match[i, j] != -1)
                    {
                        SqlConnection conn = new SqlConnection(SQLDBhelper.ConnectionString);
                        conn.Open();
                        SqlCommand insert = new SqlCommand(string.Format("insert into finish values('{0}','{1}','0')", stuid[match[i, j]], teaid[i]), conn);//导师id
                        insert.ExecuteNonQuery();
                        conn.Close();
                    }


                }
            }
            MessageBox.Show(string.Format("匹配完成,当前学生团队数{0}，已完成匹配数{1}", stucount, number));
            using(SqlConnection con=new SqlConnection(SQLDBhelper.ConnectionString))
            {
                con.Open();
                for (int i = 0; i < teacount; i++)
                {
                    SqlCommand command = new SqlCommand(string.Format("update teapasswordtable set number-={0} where account='{1}'", cnt[i], teaid[i]),con);
                    command.ExecuteNonQuery();
                }
            }
            stucount = 0;
            teacount = 0;

            //调用函数，重新刷新dataGridView1，显示的是未分配到老师的学生团队
            selectmess();
            //this.dataGridView1.DataSource = null;
            //DataTable selectmessage = SQLDBhelper.ExecuteDataTable(string.Format("select * from ischoose where stuid in (select account from stupasswordtable where grade='{0}' and major='{1}') and stuid not in (select stuid from finish)", comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString()));
            //if (selectmessage.Rows.Count != 0)
            //    dataGridView1.DataSource = selectmessage.DefaultView;
        }





        //多重匹配算法   
        bool find(int u)
        {
            for (int i = 0; i < teacount; i++)
            {
                if (line[u, i] == 1 && used[i] == false)
                {
                    used[i] = true;
                    if (cnt[i] < teacarrynumber[i])
                    {
                        match[i, cnt[i]++] = u;
                        return true;
                    }
                    for (int j = 0; j < cnt[i]; j++)
                    {
                        if (find(match[i, j]))
                        {
                            match[i, j] = u;
                            return true;
                        }
                    }
                }
            }
            return false;

        }

        int max_match()
        {
            int res = 0;
            //Array.Clear(match, 0, match.Length);
            for (int i = 0; i < teacount; i++)
            {
                for (int j = 0; j < maxnum; j++)
                {
                    match[i, j] = -1;
                }
            }
            Array.Clear(cnt, 0, cnt.Length);
            for (int i = 0; i < stucount; i++)
            {
                Array.Clear(used, 0, used.Length);
                if (find(i)) res++;
            }
            return res;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                hstu = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                stuidshow.Visible = true;
                stuidshow.Text = string.Format("当前选中学生：{0}", hstu);
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                htea = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
                teaidshow.Visible = true;
                teaidshow.Text = string.Format("当前选中导师：{0}", htea);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(htea!=null && hstu!=null)
            {
                using (SqlConnection con = new SqlConnection(SQLDBhelper.ConnectionString))
                {
                    con.Open();
                    SqlCommand insert = new SqlCommand(string.Format("insert into finish values('{0}','{1}','0')", hstu, htea), con);
                    insert.ExecuteNonQuery();
                    SqlCommand update = new SqlCommand(string.Format("update teapasswordtable set number-=1 where account='{0}'", htea),con);
                    update.ExecuteNonQuery();
                    MessageBox.Show("匹配成功");
                }
            }
            else
            {
                MessageBox.Show("未选中学生或导师");
            }
            this.button4_Click(sender, e);
            

        }


        /*int max_match()//计算多重匹配的最大匹配数
        {
            int res=0;
            memset(match,-1,sizeof(match));
            memset(cnt,0,sizeof(cnt));
            for(int i=0; i<nx; i++)//注意，理由同上！！
            {
                memset(vis,0,sizeof(vis));
                if(find_path(i)) res++;
            }
            return res;
        }*/



        /*bool all_match()//判断左边的点是否都与右边的点匹配了
        {
            memset(match,-1,sizeof(match));
            memset(cnt,0,sizeof(cnt));
            for(int i=0; i<nx; i++)
            {
                memset(vis,0,sizeof(vis));
                if(!find_path(i)) return false;
            }
            return true;
        }*/
    }
}
