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
    public partial class edit_password : UserControl
    {

        string id = null;
        public edit_password()
        {
            InitializeComponent();
        }

        public edit_password(string getid)
        {
            InitializeComponent();
            id = getid;
            label4.Text = id;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (oldpassword.Text.Trim() != UserHelper.userpwd)
            {
                label7.Text = "原始密码错误！";
                oldpassword.Focus();
                return;
            }
            if (newpassword1.Text.Trim() == "")
            {
                label7.Text = "新密码不能为空，请输入！";
                newpassword1.Focus();
                return;
            }
            if (newpassword2.Text.Trim() != newpassword1.Text.Trim())
            {
                label7.Text = "两次输入密码不一致，请重新输入！";
                newpassword2.Focus();
                return;
            }
            Model.User model = new Model.User();
            model.UserName = UserHelper.username;
            model.Password = newpassword1.Text.Trim();//新密码
            BLL.User user = new BLL.User();
            if (user.Update(model))
            {
                UserHelper.userpwd = model.Password;
                label7.Text = "密码更新成功！";//同步修改保存在UserHelpher类中的密码
                Application.Restart();//退出，重新登录
            }
            else
            {
                label7.Text = "密码修改失败！";
            }
        }

        private void edit_password_Load(object sender, EventArgs e)
        {

        }
    }
}
