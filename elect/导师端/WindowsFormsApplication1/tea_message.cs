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
    public partial class tea_message : UserControl
    {
        private string id;
        public tea_message()
        {
            InitializeComponent();
        }

        public tea_message(string id)
        {
            this.id = id;
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void tea_message_Load(object sender, EventArgs e)
        {
            BLL.message_and_choose bll = new BLL.message_and_choose();
            Model.message_and_choose model = bll.GetModel(id);//绑定账号
            if (model == null)
            {
                MessageBox.Show("数据查询出错");
                return;
            }
            else
            {
                name_tea.Text = model.name;
                sex_tea.Text = model.sex;
                number_tea.Text = model.id;
                title.Text = model.position;
                people.Text = model.groupnumber;
                college.Text = model.academy;
                phone.Text = model.phone;
                Email.Text = model.email;
                research_tea.Text = model.research;
                grade.Text = model.grade;
                major.Text = model.major;
            }
        }
    }
}
