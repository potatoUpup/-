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
    public partial class time : UserControl
    {
        public time()
        {
            InitializeComponent();
        }
        BLL.User_and_Time bll = new BLL.User_and_Time();
        private void time_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker3.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker4.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            Model.User_and_time model = bll.time1();
            dateTimePicker1.Value = model.d1 ;
            dateTimePicker2.Value = model.d2;
            dateTimePicker3.Value = model.d3;
            dateTimePicker4.Value = model.d4;            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bll.time2(dateTimePicker1.Text, dateTimePicker2.Text, dateTimePicker3.Text, dateTimePicker4.Text);
            label6.Text = "时间设置成功！";        
        }
    }
}
