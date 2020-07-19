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
    public partial class main : Form
    {
        private string id;

        public main()
        {
            InitializeComponent();
        }

        public main(string id)
        {            
            this.id = id;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new tea_message(id));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new edit_password());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new choose_group());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.mainpanel.Controls.Clear();
            this.mainpanel.Controls.Add(new my_stu());
        }       

        private void main_Load_1(object sender, EventArgs e)
        {
            mainpanel.Controls.Add(new tea_message(id));
        }

        private void mainpanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
