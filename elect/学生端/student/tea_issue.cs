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
    public partial class tea_issue : UserControl
    {
        int page = 0;//记录当前页数
        string id;//记录当前用户的id
        DataTable mywill = new DataTable();//记录当前用户个人志愿信息
        
        DataTable tealist = new DataTable();
        SqlDataReader readacademy;


        /// <summary>
        /// 提取数据库导师列表 生成多个teashow数组用于显示
        /// </summary>
        
        //DataTable will = SqlDbHelper.ExecuteDataTable("");//志愿表
        tea_show[] teashow =new tea_show[50];

        public tea_issue()
        {
            InitializeComponent();
            readacademy = SqlDbHelper.ExecuteReader(string.Format("select academy from stupasswordtable where account='{0}'", id));
            readacademy.Read();

            tealist = SqlDbHelper.ExecuteDataTable(string.Format("select * from teapasswordtable where academy='{0}'",readacademy[0].ToString()));
            for (int i = 0; i < teashow.Length; i++)
            {
                teashow[i] = new tea_show(tealist.Rows[i]);
            }
        }
        public tea_issue(string getid)
        {
            InitializeComponent();
            id = getid;
            readacademy = SqlDbHelper.ExecuteReader(string.Format("select academy from stupasswordtable where account='{0}'", id));
            //获取当前用户所在学院

            readacademy.Read();

            tealist = SqlDbHelper.ExecuteDataTable(string.Format("select * from teapasswordtable where academy='{0}'", readacademy[0].ToString()));
            //获取当前学院导师  形成列表
            for (int i = 0; i < tealist.Rows.Count; i++)
            {
                if (tealist.Rows.Count != 0)
                {
                    teashow[i] = new tea_show(tealist.Rows[i]);
                }
            }
            
            mywill = SqlDbHelper.ExecuteDataTable(string.Format("select * from will where stuid='{0}'", id));
            //读取当前用户的志愿信息
        }

        private void tea_issue_Load(object sender, EventArgs e)
        {
            this.whichwill.SelectedIndex = 0;
            label1.Text = string.Format("当前为 {0} 导师",readacademy[0].ToString());
            if (tealist.Rows.Count >= 3)
            {
                panel1.Controls.Add(teashow[0]);
                panel2.Controls.Add(teashow[1]);
                panel3.Controls.Add(teashow[2]);
            }
            else if (tealist.Rows.Count == 2)
            {
                panel1.Controls.Add(teashow[0]);
                panel2.Controls.Add(teashow[1]);
            }
            else if (tealist.Rows.Count == 1)
            {
                panel1.Controls.Add(teashow[0]);
            }
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if ((page + 1) * 3 <= tealist.Rows.Count - 1)
            {
                page += 1;
                panel1.Controls.Clear();
                panel2.Controls.Clear();
                panel3.Controls.Clear();
                if (page * 3 > tealist.Rows.Count - 1)
                {

                }
                else if ((page * 3 + 1) > tealist.Rows.Count - 1)
                {
                    panel1.Controls.Add(teashow[page * 3]);
                }
                else if ((page * 3 + 2) > tealist.Rows.Count - 1)
                {
                    panel1.Controls.Add(teashow[page * 3]);
                    panel2.Controls.Add(teashow[page * 3 + 1]);
                }
                else
                {
                    panel1.Controls.Add(teashow[page * 3]);
                    panel2.Controls.Add(teashow[page * 3 + 1]);
                    panel3.Controls.Add(teashow[page * 3 + 2]);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (page > 0)
            {
                page -= 1;
                panel1.Controls.Clear();
                panel2.Controls.Clear();
                panel3.Controls.Clear();
                panel1.Controls.Add(teashow[page * 3]);
                panel2.Controls.Add(teashow[page * 3 + 1]);
                panel3.Controls.Add(teashow[page * 3 + 2]);
            }
        }


        private void summit_Click(object sender, EventArgs e)
        {
            SqlDataReader reader = SqlDbHelper.ExecuteReader(string.Format("select name from stupasswordtable where account='{0}'", id));
            reader.Read();

            int count = 0;
            string willteaid = null;
            string willteaname = null;
            DataTable table = SqlDbHelper.ExecuteDataTable(string.Format("select * from team where captainid='{0}' or memberoneid='{0}' or membertwoid='{0}' or memberthreeid='{0}'", id));//获取团队信息  以表形式呈现
            DataTable a = SqlDbHelper.ExecuteDataTable(string.Format("select * from will where stuid='{0}'", table.Rows[0]["teamnumber"].ToString()));//获取当前团队的志愿表 有行则已经填报过
            string[] mywill = new string[3]{null,null,null};

            if(a.Rows.Count!=0)
            {
                mywill[0] = a.Rows[0]["firstwill"].ToString();
                mywill[1] = a.Rows[0]["secondwill"].ToString();
                mywill[2] = a.Rows[0]["thirdwill"].ToString();
            }


            foreach (tea_show b in teashow)
            {
                if(b !=null)
                {
                    if (b.ischoose)
                    {
                        for (int i = 0; i < 3;i++ )
                        {
                            if(b.teaid==mywill[i])
                            {
                                MessageBox.Show("已经选择过该导师啦");
                                return;
                            }
                        }
                        willteaid = b.teaid;
                        willteaname = b.teaname;
                        count++;
                    }
                }  
            }

            if (count == 0)
            {
                MessageBox.Show("还没有选择导师哟");
                return;
            }
            else if (count == 1)
            {
                try
                {
                    if (table.Rows.Count != 0)//当前有这个团队
                    {
                        if (id == table.Rows[0]["captainid"].ToString())//判断当前角色是否是队长
                        {
                            if (a.Rows.Count != 0)//判断当前团队是否有填报过志愿
                            {
                                switch (whichwill.SelectedItem.ToString())
                                {
                                    case "第一志愿":
                                        if (a.Rows[0].IsNull("firstwill"))
                                        {
                                            a.Rows[0]["firstwill"] = willteaid;
                                            a.Rows[0]["firstwillname"] = willteaname;
                                            a.Rows[0]["isaccess1"] = 0;
                                        }
                                        else
                                        {
                                            MessageBox.Show("已经填报第一志愿，如需修改请先退选");
                                            return;
                                        }   
                                        break;

                                    case "第二志愿":
                                        if (a.Rows[0].IsNull("secondwill"))
                                        {
                                            a.Rows[0]["secondwill"] = willteaid;
                                            a.Rows[0]["secondwillname"] = willteaname;
                                            a.Rows[0]["isaccess2"] = 0;
                                        }
                                        else
                                        {
                                            MessageBox.Show("已经填报第二志愿，如需修改请先退选");
                                            return;
                                        }  
                                        break;

                                    case "第三志愿":
                                        if (a.Rows[0].IsNull("thirdwill"))
                                        {
                                            a.Rows[0]["thirdwill"] = willteaid;
                                            a.Rows[0]["thirdwillname"] = willteaname;
                                            a.Rows[0]["isaccess3"] = 0;
                                        }
                                        else
                                        {
                                            MessageBox.Show("已经填报第三志愿，如需修改请先退选");
                                            return;
                                        }
                                        break;
                                    default:
                                        break;
                                }

                            }
                            else
                            {
                                DataRow newteawill = a.NewRow();
                                newteawill["stuname"] = table.Rows[0]["captainname"];
                                newteawill["stuid"] = table.Rows[0]["teamnumber"];
                                newteawill["subjectname"] = table.Rows[0]["topicname"];
                                newteawill["isteam"] = 1;

                                switch (whichwill.SelectedItem.ToString())
                                {
                                    case "第一志愿":
                                        newteawill["firstwill"] = willteaid;
                                        newteawill["firstwillname"] = willteaname;
                                        newteawill["isaccess1"] = 0;
                                        break;
                                    case "第二志愿":
                                        newteawill["secondwill"] = willteaid;
                                        newteawill["secondwillname"] = willteaname;
                                        newteawill["isaccess2"] = 0;
                                        break;
                                    case "第三志愿":
                                        newteawill["thirdwill"] = willteaid;
                                        newteawill["thirdwillname"] = willteaname;
                                        newteawill["isaccess3"] = 0;
                                        break;
                                    default:
                                        break;
                                }
                                a.Rows.Add(newteawill);
                            }
                            
                            try
                            {
                                SqlDbHelper.updatedatatable(a, "will");
                                
                                label2.Text = whichwill.Text+"已选择"+willteaname;
                                
                            }
                            catch (System.Data.SqlClient.SqlException)
                            {
                                MessageBox.Show("请完善课题信息或先填报其他志愿");
                            }
                        }
                        else
                            MessageBox.Show("请由队长来填报志愿");
                    }
                    else
                        MessageBox.Show("暂无团队信息");
                    
                }
                catch (System.NullReferenceException)
                {
                    MessageBox.Show("未选中志愿");
                }
            }

            else if (count > 1)
            {
                MessageBox.Show("一次志愿只能填一位导师哟");
                return;
            }
        }

        private void whichwill_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(tea_show a in teashow)
            {
                if (a != null && true==a.radiocheck)
                    a.radiocheck = false;
            }
            
        }
    }
}
