using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class login : Form
    {
        
        public login()
        {
            InitializeComponent();
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            string userName = username.Text.Trim();
            string password = PassWord.Text.Trim();
            if (userName == "" || password == "")
            {
                message.Text = "用户名或密码不能为空！";
                username.Focus();
                return;
            }
            else
            {
                BLL.User_and_Time user = new BLL.User_and_Time();
                if (user.Login(userName, password))
                {
                    this.Close();
                    new System.Threading.Thread(() =>
                    {
                        Application.Run(new manager_main());
                    }).Start();
                }
                else
                {
                    message.Text = "用户名或密码错误,请重新输入!";
                    username.Text = "";
                    PassWord.Text = "";
                    username.Focus();
                }
            }            
        }

        private void logoutbutton_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void PassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.loginbutton_Click(sender, e);
            }
        }
    }
}
