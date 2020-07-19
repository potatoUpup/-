using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student
{
    public partial class tea_show : UserControl
    {
        public bool ischoose = false;
        string id;//记录当前学生的id


        public bool radiocheck = false;
        public string teaname;
        public string teaid;
        DataRow t = SqlDbHelper.ExecuteDataTable("select * from teapasswordtable where 1=2").NewRow();
        public tea_show()
        {
            InitializeComponent();
        }

        public tea_show(DataRow get_tea)
        {
            InitializeComponent();
            t = get_tea;
        }
        public tea_show(string getid)
        {
            InitializeComponent();
            id = getid;
        }

        
        private void tea_show_Load(object sender, EventArgs e)
        {
            
            teaid = t["id"].ToString();
            teaname = t["name"].ToString();
            teaname = t["name"].ToString();
            teaid=t["id"].ToString();
            name.Text = t["name"].ToString();
            position.Text = t["position"].ToString();
            label4.Text = t["phone"].ToString();
            number.Text = t["groupnumber"].ToString();
            research.Text = t["research"].ToString();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radiocheck == false)
            {
                radioButton1.Checked = true;
                radiocheck = true;
            }
            else
            {
                radioButton1.Checked = false;
                radiocheck = false;
            }

            if (radioButton1.Checked == true)
                ischoose = true;
            else
                ischoose = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(radiocheck)
            {
                radioButton1.Checked = true;
                ischoose = true;
            }
            else
            {
                radioButton1.Checked = false;
                ischoose = false;
            }
        }

 
        
    }
}
