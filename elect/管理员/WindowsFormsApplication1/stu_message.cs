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
    public partial class stu_message : UserControl
    {
        public stu_message()
        {
            InitializeComponent();
        }
        BLL.stu bll = new BLL.stu();
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || comboBox1.Text.Trim() == "" || comboBox2.Text.Trim() == "" || comboBox3.Text.Trim() == "" || comboBox4.Text.Trim() == "" || comboBox5.Text.Trim() == "")
            {
                label9.Text = "以上信息均需要填写，请完善！";
            }
            else
            {
                string Account = textBox2.Text.Trim();
                string Password = textBox3.Text.Trim();
                string Name = textBox1.Text.Trim();
                string Sex = comboBox1.Text.Trim();
                string ID = textBox2.Text.Trim();
                string Grade = comboBox2.Text.Trim();
                string Major = comboBox5.Text.Trim();
                string Class = comboBox3.Text.Trim();
                string Phone = textBox4.Text.Trim();
                string Email = textBox5.Text.Trim();
                string introduce = textBox9.Text.Trim();
                string academy = comboBox4.Text.Trim();
                string groupID = "0";
                bll.add(Account, Password, Name, Sex, ID, Grade, Major, Class, Phone, Email, introduce, academy, groupID);
                label9.Text = "已提交" + textBox2.Text.Trim();
            }            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        //返回
        private void button2_Click(object sender, EventArgs e)
        {
            base.Controls.Clear();
            base.Controls.Add(new im_stu_account());
        }
    }
}
