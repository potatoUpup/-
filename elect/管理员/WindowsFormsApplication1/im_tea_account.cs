using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Threading;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class im_tea_account : UserControl
    {
        public im_tea_account()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();       
        SqlConnection conn;
        BLL.tea bll = new BLL.tea();
        //处理STA异常问题
        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(exceldata_tea));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        public void exceldata_tea()
        {
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string fileName = fd.FileName;
                bind_tea(fileName);
            }

        }
        //方法二，利用委托机制实现线程安全;
        private delegate void DelegateDataGridViewWRLUI();
        //打开Excel，把数据放进dataGridView中
        private void bind_tea(string fileName)
        {
            DelegateDataGridViewWRLUI delegateDataGridViewWRLUI = delegate
            {
                //取消跨线程检查，不使用委托的方法，解决“线程间操作无效，从不是创建控件的线程访问它”的问题
                //进行非安全线程访问时，运行环境就不去检验它是否是线程安全的
                //Control.CheckForIllegalCrossThreadCalls = false;//方法一，不建议，转 利用委托机制实现线程安全。
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + "Extended Properties='Excel 8.0; HDR=Yes; IMEX=1'";
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT *  FROM [sheet1$]", strConn);
                DataSet ds = new DataSet();
                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
                try
                {
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dataGridView1.Rows.Add(dt);
                            dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i]["账号"].ToString();
                            dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i]["密码"].ToString();
                            dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i]["姓名"].ToString();
                            dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i]["性别"].ToString();
                            dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i]["工号"].ToString();
                            dataGridView1.Rows[i].Cells[5].Value = dt.Rows[i]["职称"].ToString();
                            dataGridView1.Rows[i].Cells[6].Value = dt.Rows[i]["名额"].ToString();
                            dataGridView1.Rows[i].Cells[7].Value = dt.Rows[i]["名额"].ToString();
                            dataGridView1.Rows[i].Cells[8].Value = dt.Rows[i]["电话号码"].ToString();
                            dataGridView1.Rows[i].Cells[9].Value = dt.Rows[i]["邮箱地址"].ToString();
                            dataGridView1.Rows[i].Cells[10].Value = dt.Rows[i]["学院"].ToString();
                            dataGridView1.Rows[i].Cells[11].Value = dt.Rows[i]["研究方向"].ToString();
                            dataGridView1.Rows[i].Cells[12].Value = dt.Rows[i]["年级"].ToString();
                            dataGridView1.Rows[i].Cells[13].Value = dt.Rows[i]["专业"].ToString();
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("操作失败！" + err.ToString());
                }
                textBox1.Text = "" + fileName;
            };
            this.dataGridView1.Invoke(delegateDataGridViewWRLUI);
        }

        //主键判断还没写，发生重复后，系统直接报错
        //调用insertToSql方法，将数据导入数据库
        private void button4_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(SQLDBhelper.ConnectionString);
            conn.Open();
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        insertToSql_tea(dr);
                    }
                    conn.Close();
                    MessageBox.Show("导入成功！");
                }
                else
                {
                    MessageBox.Show("没有数据！");
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("已经添加过数据了");
            }
            
        }

        //Excel表中的列名和数据库中的列名一定要对应   
        private void insertToSql_tea(DataRow dr)
        {   
            string Account = dr["账号"].ToString();
            string Password = dr["密码"].ToString();
            string Name = dr["姓名"].ToString();
            string Sex = dr["性别"].ToString();
            string ID = dr["工号"].ToString();
            string Position = dr["职称"].ToString();
            string Number = dr["名额"].ToString();
            string groupnumber = dr["名额"].ToString();
            string Phone = dr["电话号码"].ToString();
            string Email = dr["邮箱地址"].ToString();
            string Academy = dr["学院"].ToString();
            string Research = dr["研究方向"].ToString();
            string grade = dr["年级"].ToString();
            string major = dr["专业"].ToString();
            SQLDBhelper.ExecuteNonQuery(string.Format("insert into teapasswordtable values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", Account, Password, Name, Sex, ID, Position, Number, groupnumber,Phone,Email,Academy,Research, grade, major));
        }

        private void im_tea_account_Load(object sender, EventArgs e)
        {
            select_tea();
        }

        //对应的年级专业的导师信息，使用函数以便后面使用
        public void select_tea()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            DataTable selectmessage = SQLDBhelper.ExecuteDataTable(string.Format("select * from teapasswordtable where grade='{0}' and major='{1}' ",comboBox1.Text.Trim(),comboBox2.Text.Trim()));
            if (selectmessage.Rows.Count != 0)
            {
                for (int i = 0; i < selectmessage.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(dt);
                    dataGridView1.Rows[i].Cells[0].Value = selectmessage.Rows[i]["account"].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = selectmessage.Rows[i]["password"].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = selectmessage.Rows[i]["name"].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = selectmessage.Rows[i]["sex"].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = selectmessage.Rows[i]["id"].ToString();
                    dataGridView1.Rows[i].Cells[5].Value = selectmessage.Rows[i]["position"].ToString();
                    dataGridView1.Rows[i].Cells[6].Value = selectmessage.Rows[i]["number"].ToString();
                    dataGridView1.Rows[i].Cells[7].Value = selectmessage.Rows[i]["groupnumber"].ToString();
                    dataGridView1.Rows[i].Cells[8].Value = selectmessage.Rows[i]["phone"].ToString();
                    dataGridView1.Rows[i].Cells[9].Value = selectmessage.Rows[i]["email"].ToString();
                    dataGridView1.Rows[i].Cells[10].Value = selectmessage.Rows[i]["academy"].ToString();
                    dataGridView1.Rows[i].Cells[11].Value = selectmessage.Rows[i]["research"].ToString();
                    dataGridView1.Rows[i].Cells[12].Value = selectmessage.Rows[i]["grade"].ToString();
                    dataGridView1.Rows[i].Cells[13].Value = selectmessage.Rows[i]["major"].ToString();
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            select_tea();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            base.Controls.Clear();
            base.Controls.Add(new tea_message());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                      
        }
        //删除
        private void button6_Click(object sender, EventArgs e)
        {
            bll.delect(UserHelper.xxid);
            //调用函数，对删除后的dataGridView进行刷新
            select_tea();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(UserHelper.xxid=="")
            {
                MessageBox.Show("请先选择需要修改信息的导师！");
            }
            else
            {
                base.Controls.Clear();
                base.Controls.Add(new edit_tea_message());
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                UserHelper.xxid = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            } 
        }
    }
}
