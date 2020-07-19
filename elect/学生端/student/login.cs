using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student
{
    public partial class login : Form
    {
        string id=null;
        public login()
        {
            InitializeComponent();
        }
        private void loginbutton_Click(object sender, EventArgs e)
        {
            string userName = username.Text.Trim();
            string passWord = password.Text.Trim();
            if (userName == "" || passWord == "")
            {
                label6.Text = "用户名或密码不能为空！";
                username.Focus();
                return;
            }
            else
            {
                BLL.User user = new BLL.User();
                if (user.Login(userName, passWord))
                {
                    UserHelper.username = username.Text.Trim();
                    UserHelper.userpwd = password.Text.Trim();
                    id = username.Text.Trim();
                    this.Close();
                    new System.Threading.Thread(() =>
                    {
                        Application.Run(new studentmain(id));
                    }).Start();
                }
                else
                {
                    label6.Text = "用户名或密码错误,请重新输入!";
                    username.Text = "";
                    password.Text = "";
                    username.Focus();
                }
            } 
        }

        private void login_Load(object sender, EventArgs e)
        {
            
        }


        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.loginbutton_Click(sender, e);
            }
        }

        private void logoutbutton_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void login_Resize(object sender, EventArgs e)
        {
            
        }
    }
}
