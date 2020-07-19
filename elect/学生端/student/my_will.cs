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
    public partial class my_will : UserControl
    {
        string id = null;
        BLL.my_will bll = new BLL.my_will();

        public my_will()
        {
            InitializeComponent();
        }
        public my_will(string getid)
        {
            InitializeComponent();
            id = getid;
        }
       
        private void my_will_Load(object sender, EventArgs e)
        {
            
            DataTable teamwill = SqlDbHelper.ExecuteDataTable(string.Format("select * from will where stuid=(select teamnumber from team where captainid='{0}' or memberoneid='{0}' or membertwoid='{0}' or memberthreeid='{0}')", id));
            if (teamwill.Rows.Count != 0)
            {
                //firstwill.Rows[0].DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("微软雅黑, 9pt", 11F, FontStyle.Bold) };
                firstwill.Rows[0].Cells[0].Value = teamwill.Rows[0]["firstwillname"].ToString(); ;
                firstwill.Rows[0].Cells[1].Value = teamwill.Rows[0]["firstwill"].ToString();
                if (teamwill.Rows[0]["isaccess1"].ToString() == "False")
                {
                    firstwill.Rows[0].Cells[2].Value = "未预选";
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.Name = "退选";
                    btn.UseColumnTextForButtonValue = true;
                    btn.DefaultCellStyle.NullValue = "退选";
                    //btn.Text = "退选";
                    btn.HeaderText = "退选";
                    firstwill.Columns.Insert(firstwill.Columns.Count, btn);
                }
                else if (teamwill.Rows[0]["isaccess1"].ToString() == "True")
                {
                    firstwill.Rows[0].Cells[2].Value = "已预选";
                }

                secondwill.Rows[0].Cells[0].Value = teamwill.Rows[0]["secondwillname"].ToString();
                secondwill.Rows[0].Cells[1].Value = teamwill.Rows[0]["secondwill"].ToString();
                if (teamwill.Rows[0]["isaccess2"].ToString() == "False")
                {
                    secondwill.Rows[0].Cells[2].Value = "未预选";
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.Name = "退选";
                    btn.UseColumnTextForButtonValue = true;
                    btn.DefaultCellStyle.NullValue = "退选";
                    btn.Text = "退选";
                    btn.HeaderText = "退选";
                    secondwill.Columns.Insert(secondwill.Columns.Count, btn);

                }
                else if (teamwill.Rows[0]["isaccess2"].ToString() == "True")
                {
                    secondwill.Rows[0].Cells[2].Value = "已预选";
                    
                }

                thirdwill.Rows[0].Cells[0].Value = teamwill.Rows[0]["thirdwillname"].ToString();
                thirdwill.Rows[0].Cells[1].Value = teamwill.Rows[0]["thirdwill"].ToString();
                if (teamwill.Rows[0]["isaccess3"].ToString() == "False")
                {
                    thirdwill.Rows[0].Cells[2].Value = "未预选";
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.Name = "退选";
                    btn.UseColumnTextForButtonValue = true;
                    btn.DefaultCellStyle.NullValue = "退选";
                    btn.Text = "退选";
                    btn.HeaderText = "退选";
                    thirdwill.Columns.Insert(thirdwill.Columns.Count, btn);
                }
                else if (teamwill.Rows[0]["isaccess3"].ToString() == "True")
                {
                    thirdwill.Rows[0].Cells[2].Value = "已预选";
                }
            }
            else
            {
                firstwill.Rows[0].Cells[0].Value = null;
                firstwill.Rows[0].Cells[1].Value = null;
                secondwill.Rows[0].Cells[0].Value = null;
                secondwill.Rows[0].Cells[1].Value = null;
                thirdwill.Rows[0].Cells[0].Value = null;
                thirdwill.Rows[0].Cells[1].Value = null;
            }

            //分配结果
            finish.DataSource = bll.sure(id);           
        }

        private void firstwill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (firstwill.Columns[e.ColumnIndex].Name == "退选")
            {
                SqlDataReader readcaptainid = SqlDbHelper.ExecuteReader(string.Format("select captainid from team where teamnumber=(select teamnumber from team where captainid='{0}' or memberoneid='{0}' or membertwoid='{0}' or memberthreeid='{0}')", id));
                if (readcaptainid.HasRows)
                {
                    readcaptainid.Read();
                    SqlDataReader readnumber = SqlDbHelper.ExecuteReader(string.Format("select mnumber from team where captainid='{0}'", readcaptainid[0].ToString()));
                    readnumber.Read();
                    if (id == readcaptainid[0].ToString())
                    {
                        SqlDbHelper.ExecuteNonQuery(string.Format("update will set firstwill = NULL,firstwillname = NULL,isaccess1=NULL where stuid='{0}'", id));
                        label4.Text = "已成功退选第一志愿！";
                        firstwill.Rows.Clear();
                        firstwill.Columns.Remove("退选");
                    }
                    else
                    {
                        MessageBox.Show("仅能由队长退选志愿");
                    }
                }
             }
        }

        private void secondwill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (secondwill.Columns[e.ColumnIndex].Name == "退选")
            {
                SqlDataReader readcaptainid = SqlDbHelper.ExecuteReader(string.Format("select captainid from team where teamnumber=(select teamnumber from team where captainid='{0}' or memberoneid='{0}' or membertwoid='{0}' or memberthreeid='{0}')", id));
                if (readcaptainid.HasRows)
                {
                    readcaptainid.Read();
                    SqlDataReader readnumber = SqlDbHelper.ExecuteReader(string.Format("select mnumber from team where captainid='{0}'", readcaptainid[0].ToString()));
                    readnumber.Read();
                    if (id == readcaptainid[0].ToString())
                    {
                        SqlDbHelper.ExecuteNonQuery(string.Format("update will set secondwill = NULL,secondwillname = NULL,isaccess2=NULL where stuid='{0}'", id));
                        label4.Text = "已成功退选第二志愿！";
                        secondwill.Rows.Clear();
                        secondwill.Columns.Remove("退选");
                    }
                    else
                    {
                        MessageBox.Show("仅能由队长退选志愿");
                    }
                }
            }
        }

        private void thirdwill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (thirdwill.Columns[e.ColumnIndex].Name == "退选")
            {
                SqlDataReader readcaptainid = SqlDbHelper.ExecuteReader(string.Format("select captainid from team where teamnumber=(select teamnumber from team where captainid='{0}' or memberoneid='{0}' or membertwoid='{0}' or memberthreeid='{0}')", id));
                if (readcaptainid.HasRows)
                {
                    readcaptainid.Read();
                    SqlDataReader readnumber = SqlDbHelper.ExecuteReader(string.Format("select mnumber from team where captainid='{0}'", readcaptainid[0].ToString()));
                    readnumber.Read();
                    if (id == readcaptainid[0].ToString())
                    {
                        SqlDbHelper.ExecuteNonQuery(string.Format("update will set thirdwill = NULL,thirdwillname = NULL,isaccess3=NULL where stuid='{0}'", id));
                        label4.Text = "已成功退选第三志愿！";
                        thirdwill.Rows.Clear();
                        thirdwill.Columns.Remove("退选");
                    }
                    else
                    {
                        MessageBox.Show("仅能由队长退选志愿");
                    }
                }
            }
        }
    }
}
