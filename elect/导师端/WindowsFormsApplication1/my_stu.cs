using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class my_stu : UserControl
    {
        public my_stu()
        {
            InitializeComponent();
        }
        BLL.message_and_choose bll = new BLL.message_and_choose();
        private void my_stu_Load(object sender, EventArgs e)
        {
            string teaid = "";
            teaid = "finish.teaid='"+UserHelper.username+"'";
            dataGridView1.DataSource = bll.GetSureList(teaid);
        }
    }
}
