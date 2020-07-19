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
    public partial class edit_password : UserControl
    {
        public edit_password()
        {
            InitializeComponent();
        }

        private void UserControl3_Load(object sender, EventArgs e)
        {
            label4.Text = UserHelper.username;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() != UserHelper.userpwd)
            {               
                label5.Text = "原始密码错误！";
                textBox2.Focus();
                return;
            }
            if (textBox3.Text.Trim() == "")
            {
                label5.Text = "新密码不能为空，请输入！";
                textBox3.Focus();
                return;
            }
            if (textBox4.Text.Trim() != textBox3.Text.Trim())
            {
                label5.Text = "两次输入密码不一致，请重新输入！";
                textBox4.Focus();
                return;
            }
            Model.User model = new Model.User();
            model.UserName = UserHelper.username;
            model.Password = textBox3.Text.Trim();//新密码
            BLL.User user = new BLL.User();
            if (user.Update(model))
            {
                UserHelper.userpwd = model.Password;
                label5.Text = "密码更新成功！";//同步修改保存在UserHelpher类中的密码
                Application.Restart();//退出，重新登录
            }
            else
            {
                label5.Text = "密码修改失败！";
            }
        }
    }
}
