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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        string id;
        //登录
        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            if (userName == "" || password == "")
            {
                label5.Text = "用户名或密码不能为空！";
                textBox1.Focus();
                return;
            }
            else
            {
                BLL.User user = new BLL.User();
                if (user.Login(userName, password))
                {
                    UserHelper.username = textBox1.Text.Trim();
                    UserHelper.userpwd = textBox2.Text.Trim();
                    id = UserHelper.username;
                    this.Close();
                    new System.Threading.Thread(() =>
                    {
                        Application.Run(new main(id));
                    }).Start();
                }
                else
                {
                    label5.Text = "用户名或密码错误,请重新输入!";
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox1.Focus();
                }
            }           
        }
        private void label3_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();         
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }
    }
}
