using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student
{
    public partial class mygroup : UserControl
    {
        string id;
        DataTable mygroupshow;
        public mygroup()
        {
            InitializeComponent();
        }

        public mygroup(string getid)
        {
            InitializeComponent();
            id = getid;
            mygroupshow = SqlDbHelper.ExecuteDataTable(string.Format("select * from team where captainid='{0}' or memberoneid='{0}' or membertwoid='{0}' or memberthreeid='{0}'", id));
        }
        
        private void mygroup_Load(object sender, EventArgs e)
        {
            button3.Enabled = true;
            if (mygroupshow.Rows.Count != 0)//有当前团队
            {
                teamnumberbox.Text = mygroupshow.Rows[0]["teamnumber"].ToString();
                captainnamebox.Text = mygroupshow.Rows[0]["captainname"].ToString();
                captainidbox.Text = mygroupshow.Rows[0]["captainid"].ToString();
                memberonename.Text = mygroupshow.Rows[0]["memberonename"].ToString();
                memberoneid.Text = mygroupshow.Rows[0]["memberoneid"].ToString();
                membertwoname.Text = mygroupshow.Rows[0]["membertwoname"].ToString();
                membertwoid.Text = mygroupshow.Rows[0]["membertwoid"].ToString();
                memberthreename.Text = mygroupshow.Rows[0]["memberthreename"].ToString();
                memberthreeid.Text = mygroupshow.Rows[0]["memberthreeid"].ToString();
                topicname.Text = mygroupshow.Rows[0]["topicname"].ToString();
                topicintroduce.Text = mygroupshow.Rows[0]["topicintroduce"].ToString();
                if (id == mygroupshow.Rows[0]["captainid"].ToString())
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = false;
                }
            }
            else
            {
                tips.Visible = true;
            }
        }

        private void membertwoname_TextChanged(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定解散团队？", "删除团队", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            DataTable will = SqlDbHelper.ExecuteDataTable(string.Format("select * from will where stuid='{0}'",id));
            if(dr==DialogResult.Yes)
            {
                mygroupshow.Rows[0].Delete();
                SqlDbHelper.updatedatatable(mygroupshow,"team");
                if (will.Rows.Count != 0)
                {
                    will.Rows[0].Delete();
                    SqlDbHelper.updatedatatable(will, "will");
                }
                //will.Rows[0].Delete();
                //SqlDbHelper.updatedatatable(will, "will");
                SqlDbHelper.ExecuteNonQuery(string.Format("update stupasswordtable set groupID='0' where account='{0}' or account='{1}' or account='{2}' or account='{3}'", captainidbox.Text, memberoneid.Text, membertwoid.Text, memberthreeid.Text));
                clear();
            }
            else
            {
            }
        }
        //退出团队
        private void button3_Click(object sender, EventArgs e)
        {
            if(teamnumberbox.Text.Trim()=="")
            {
                MessageBox.Show("已退出团队！");
            }
            else if (id == mygroupshow.Rows[0]["memberoneid"].ToString())
            {
                DialogResult dr = MessageBox.Show("确定退出团队？", "退出团队", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    mygroupshow.Rows[0]["memberoneid"] = null;
                    mygroupshow.Rows[0]["memberonename"] = null;
                    mygroupshow.Rows[0]["mnumber"] = (int)mygroupshow.Rows[0]["mnumber"] - 1;
                    SqlDbHelper.updatedatatable(mygroupshow, "team");
                    SqlDbHelper.ExecuteNonQuery(string.Format("update stupasswordtable set groupID='0' where account='{0}' ", memberoneid.Text));
                    clear();
                }
            }
            else if (id == mygroupshow.Rows[0]["membertwoid"].ToString())
            {
                DialogResult dr = MessageBox.Show("确定退出团队？", "退出团队", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    mygroupshow.Rows[0]["membertwoid"] = null;
                    mygroupshow.Rows[0]["membertwoname"] = null;
                    mygroupshow.Rows[0]["mnumber"] = (int)mygroupshow.Rows[0]["mnumber"] - 1;
                    SqlDbHelper.updatedatatable(mygroupshow, "team");
                    SqlDbHelper.ExecuteNonQuery(string.Format("update stupasswordtable set groupID='0' where account='{0}' ", membertwoid.Text));
                    clear();
                }
            }
            else if (id == mygroupshow.Rows[0]["memberthreeid"].ToString())
            {
                DialogResult dr = MessageBox.Show("确定退出团队？", "退出团队", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    mygroupshow.Rows[0]["memberthreeid"]=null;
                    mygroupshow.Rows[0]["memberthreename"] = null;
                    mygroupshow.Rows[0]["mnumber"] = (int)mygroupshow.Rows[0]["mnumber"] - 1;
                    SqlDbHelper.updatedatatable(mygroupshow, "team");
                    SqlDbHelper.ExecuteNonQuery(string.Format("update stupasswordtable set groupID='0' where account='{0}' ", memberthreeid.Text));
                    clear();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Controls.Clear();
            base.Controls.Add(new editgroup(id));
        }
        public void clear()
        {
            foreach(Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
        }
    }
}
