using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student
{
    public partial class studentmain : Form
    {
        string id=null;
        stu_message mess;


        public editgroup eg; 


        public studentmain()
        {
            InitializeComponent();
        }
        public studentmain(string getid)
        {
            InitializeComponent();
            id = getid;
            mess = new stu_message(id);
            eg = new editgroup(id);
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(mess);
        }

        private void main_Load(object sender, EventArgs e)
        {
            mainpanel.Controls.Add(mess);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
            new System.Threading.Thread(() => { Application.Run(new login()); }).Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new edit_password(id));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable team = SqlDbHelper.ExecuteDataTable(string.Format("select memberoneid,membertwoid,memberthreeid from team where memberoneid='"+id+"'or membertwoid='"+id+"'or memberthreeid='"+id+"'"));
            if (team.Rows.Count != 0)
            {
                MessageBox.Show("你已加入团队，请前往“团队信息”查看！");
            }
            else
            {
                this.mainpanel.Controls.Clear();
                this.mainpanel.Controls.Add(new editgroup(id));

            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new mygroup(id));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new tea_issue(id));
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new my_will(id));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }
    }
}
