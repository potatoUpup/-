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
    public partial class edit_stu_message : UserControl
    {
        public edit_stu_message()
        {
            InitializeComponent();
        }
        BLL.stu bll = new BLL.stu();
        private void edit_stu_message_Load(object sender, EventArgs e)
        {
            try
            {
                Model.stu model = bll.updatetoselect(UserHelper.xxid);
                textBox1.Text = model.name;
                comboBox1.Text = model.sex;
                textBox2.Text = model.id;
                textBox3.Text = model.password;
                comboBox2.Text = model.grade;
                comboBox3.Text = model.Class;
                comboBox4.Text = model.academy;
                comboBox5.Text = model.major;
                textBox4.Text = model.phone;
                textBox5.Text = model.email ;
                textBox6.Text = model.groupID;
                textBox9.Text = model.introduce;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Controls.Clear();
            base.Controls.Add(new im_stu_account());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || comboBox1.Text.Trim() == "" || comboBox2.Text.Trim() == "" || comboBox3.Text.Trim() == "" || comboBox4.Text.Trim() == "" || comboBox5.Text.Trim() == "")
            {
                label15.Text = "以上信息均不能为空，请完善！";
            }
            else
            {
                bll.update(textBox1.Text.Trim(), comboBox1.Text.Trim(), comboBox4.Text.Trim(), comboBox5.Text.Trim(), comboBox2.Text.Trim(), comboBox3.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim(), textBox9.Text.Trim(), UserHelper.xxid);
                label15.Text = "已对 " + textBox2.Text.Trim() + " 学生的信息进行修改！";
            }
        }
    }
}
