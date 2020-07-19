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
        public tea_message()
        {
            InitializeComponent();
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }
        BLL.tea bll = new BLL.tea();
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || comboBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || comboBox2.Text.Trim() == "" || comboBox4.Text.Trim() == "" || comboBox3.Text.Trim() == "" || textBox9.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || textBox6.Text.Trim() == "" || comboBox5.Text.Trim() == "")
            {
                label9.Text = "以上信息均需要填写，请完善！";
            }
            else
            {
                string account = textBox2.Text.Trim();
                string password = textBox3.Text.Trim();
                string name = textBox1.Text.Trim();
                string sex = comboBox1.Text.Trim();
                string id = textBox2.Text.Trim();
                string position = comboBox3.Text.Trim();
                int number = Convert.ToInt32(textBox6.Text.Trim());
                int groupnumber = Convert.ToInt32(textBox6.Text.Trim());
                string phone = textBox4.Text.Trim();
                string email = textBox5.Text.Trim();
                string grade = comboBox2.Text.Trim();
                string academy = comboBox4.Text.Trim();
                string research = textBox9.Text.Trim();
                string major = comboBox5.Text.Trim();
                bll.add(account, password, name, sex, id, position, number, groupnumber, phone, email, academy, research, grade, major);
                label9.Text = "已添加工号为 " + textBox2.Text.Trim() + " 导师的信息";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Controls.Clear();
            base.Controls.Add(new im_tea_account());
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
