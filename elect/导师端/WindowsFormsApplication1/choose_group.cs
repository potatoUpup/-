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
    public partial class choose_group : UserControl
    {
        public choose_group()
        {
            InitializeComponent();
        }
        BLL.message_and_choose bll = new BLL.message_and_choose();
        DateTime d1;//志愿预选开始时间
        DateTime d4;//志愿预选截止时间
        DateTime d5 = DateTime.Now;//当前系统时间
        private void choose_group_Load(object sender, EventArgs e)
        {
            Model.message_and_choose model = bll.time();
            d1 = model.d1;
            d4 = model.d4;
            label5.Text = "志愿预选开始时间为" + d1 + ",志愿预选截止时间为" + d4 + "";
        }

        //"已预选"界面：在第一、二、三志愿为当前老师的情况下，对已预选的团队进行查询，并把团队信息显示在"已预选"界面中
        public void issueteam()
        {
            this.dataGridView2.DataSource = null;
            this.dataGridView2.Rows.Clear();
            dataGridView2.AutoGenerateColumns = false;//datagridview禁止自动生成额外列
            dataGridView2.DataSource = bll.SelectionList(UserHelper.username);
            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("暂无学生团队！");
            }           
        }

        //查询选择当前导师账号的学生团队信息，仅显示队长信息和课题信息
        public void selectteam()
        {           
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            if (d5 <= d1)
            {
                MessageBox.Show("未到预选开放时间，请等候！");
            }
            else if (d1 < d5 && d5 <= d4)
            {
                dataGridView1.AutoGenerateColumns = false;//datagridview禁止自动生成额外列
                dataGridView1.DataSource = bll.GetNoChooseList(UserHelper.username, comboBox1.Text.Trim(), comboBox2.Text.Trim());
                if (dataGridView1.Rows.Count==0)
                {
                    MessageBox.Show("该年级或专业的条件下暂无学生团队！");
                }                
            }
            else MessageBox.Show("预选时间已过，请留意分配结果！");
        }
        //查询功能
        private void btnselect_Click(object sender, EventArgs e)
        {
            selectteam();//调用函数selectteam()
        }

        //绑定当前学生团队编号
        public static string xxid = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //绑定当前行的团队编号
            if (e.RowIndex >= 0)
            {
                xxid = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        //预选团队
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "预选")
            {
                bll.Chooseing(UserHelper.username,xxid);
                label4.Text = "已预选团队" + xxid;
                selectteam();               
            }
        }
        
        //双击查看团队信息
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {          
            groupmessage f = new groupmessage(xxid);
            f.ShowDialog();//ShowDialog：必须关闭小窗体，才能打开第二次            
        }

        //"已预选"界面的退选功能
        public static string id = "";
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                 id = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "退选")
            {
                if (d1 < d5 && d5 <= d4)
                {
                    bll.WithdrawalTeam(id, UserHelper.username);
                    issueteam();
                }
                else MessageBox.Show("目前不在退选时间内！");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)//也可以判断tabControl1.SelectedTab.Text的值
            {
                label4.Text = "";
                selectteam();//调用函数selectteam()
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                issueteam();//调用函数issueteam()把已预选的团队显示在dataGridView2               
            }
        }
    }
}
