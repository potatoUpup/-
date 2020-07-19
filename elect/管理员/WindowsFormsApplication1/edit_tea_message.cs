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
    public partial class edit_tea_message : UserControl
    {
        public edit_tea_message()
        {
            InitializeComponent();
        }
        BLL.tea bll = new BLL.tea();
        //提交
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || comboBox1.Text.Trim() == "" || comboBox2.Text.Trim() == "" || comboBox4.Text.Trim() == "" || comboBox3.Text.Trim() == "" || comboBox5.Text.Trim() == "" || textBox9.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || textBox6.Text.Trim() == "")
            {
                label9.Text = "以上信息均不能为空，请完善！";
            }
            else
            {
                bll.update(textBox1.Text.Trim(),comboBox1.Text.Trim(), comboBox2.Text.Trim(), comboBox4.Text.Trim(), comboBox3.Text.Trim(),textBox9.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim(), Convert.ToInt32(textBox6.Text.Trim()), Convert.ToInt32(textBox6.Text.Trim()), comboBox5.Text.Trim(), UserHelper.xxid);
                label9.Text = "已对 " + textBox2.Text.Trim() + " 导师的信息进行修改！";
            }
        }

        private void edit_tea_message_Load(object sender, EventArgs e)
        {
            try
            {
                Model.tea model = bll.updatetoselect(UserHelper.xxid);
                textBox1.Text = model.name;
                comboBox1.Text = model.sex;
                textBox2.Text = model.id;
                textBox3.Text = model.password;
                comboBox2.Text = model.grade;
                comboBox5.Text = model.major;
                comboBox4.Text = model.academy;
                comboBox3.Text = model.position;
                textBox9.Text = model.research;
                textBox4.Text = model.phone;
                textBox5.Text = model.email;
                textBox6.Text = model.groupnumber;               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //返回
        private void button2_Click(object sender, EventArgs e)
        {
            base.Controls.Clear();
            base.Controls.Add(new im_tea_account());
        }
    }
}
