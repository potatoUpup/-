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
    public partial class groupmessage : Form
    {
        private string xxid;

        public groupmessage()
        {
            InitializeComponent();
        }

        public groupmessage(string xxid)
        {
            this.xxid = xxid;
            InitializeComponent();
        }

        private void groupmessage_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        //根据在choosegroup窗体中输入的团队编号，查找该账号的团队信息
        private void groupmessage_Load(object sender, EventArgs e)
        {
            BLL.message_and_choose bll = new BLL.message_and_choose();
            Model.message_and_choose model = bll.GetgroupModel(xxid);//绑定账号
            if (model == null)
            {
                MessageBox.Show("数据查询出错");
                return;
            }
            else
            {
                teamnumber.Text = model.Teamnumber;
                topicname.Text = model.topicname;
                captainname.Text = model.captainname;
                memberonename.Text = model.memberonename;
                membertwoname.Text = model.membertwoname;
                memberthreename.Text = model.memberthreename;
                topicintroduce.Text = model.topicintroduce;
            }
        }
    }
}
