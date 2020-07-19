using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class manager_main : Form
    {
        public manager_main()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
            new System.Threading.Thread(() =>
            {
                Application.Run(new login());
            }).Start();
        }

        private void manager_main_Load(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Add(new time());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new im_stu_account());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new im_tea_account());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new no_choose());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new is_choose());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new time());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new noteam());
        }
    }
}
