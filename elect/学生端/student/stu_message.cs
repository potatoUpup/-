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
    public partial class stu_message : UserControl
    {
         string id ;
        public stu_message()
        {
            InitializeComponent();
        }
        public stu_message(string getid)
        {
            InitializeComponent();
            id = getid;
        }

        private void stu_message_Load(object sender, EventArgs e)
        {
            BLL.message bll = new BLL.message();
            Model.message model = bll.GetModel(id);//绑定账号
            if (model == null)
            {
                MessageBox.Show("数据查询出错");
                return;
            }
            else
            {
                name_stu.Text = model.name;
                sex_stu.Text = model.sex;
                number_stu.Text = model.id;
                major_stu.Text = model.major;
                grade.Text = model.grade;
                ban.Text = model.Class;
                phone.Text = model.phone;
                email.Text = model.email;
                textBox1.Text = model.introduce;
            }
        }

        private void evaluate_Click(object sender, EventArgs e)
        {

        }
    }
}
