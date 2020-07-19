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
    public partial class im_stu_account : UserControl
    {
        public im_stu_account()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(SQLDBhelper.ConnectionString);
        BLL.stu bll = new BLL.stu();


        //处理STA异常问题
        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(exceldata_stu));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        public void exceldata_stu()
        {           
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog();    
            if (fd.ShowDialog() == DialogResult.OK)    
            {          
                string fileName = fd.FileName;      
                bind_stu(fileName);
            }

        }

        //方法二，利用委托机制实现线程安全;
        private delegate void DelegateDataGridViewWRLUI();
        //打开Excel，把数据放进dataGridView中
        private void bind_stu(string fileName)
        {
            DelegateDataGridViewWRLUI delegateDataGridViewWRLUI = delegate
            {
                //取消跨线程检查，不使用委托的方法，解决“线程间操作无效，从不是创建控件的线程访问它”的问题
                //进行非安全线程访问时，运行环境就不去检验它是否是线程安全的
                //Control.CheckForIllegalCrossThreadCalls = false;//方法一，不建议，转 利用委托机制实现线程安全。
                
                //office2007版本以上
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + "Extended Properties='Excel 8.0; HDR=Yes; IMEX=1'";
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT *  FROM [sheet1$]", strConn);
                DataSet ds = new DataSet();
                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
                //DataTable a = SQLDBhelper.ExecuteDataTable("select * from stupasswordtable where 1=2");
                //dataGridView1.DataSource = a;
                //DataTable dt = (DataTable)dataGridView1.DataSource;
                //dt.Rows.Clear();
                //dataGridView1.DataSource = dt; 
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
                            dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i]["学号"].ToString();
                            dataGridView1.Rows[i].Cells[5].Value = dt.Rows[i]["年级"].ToString();
                            dataGridView1.Rows[i].Cells[6].Value = dt.Rows[i]["专业"].ToString();
                            dataGridView1.Rows[i].Cells[7].Value = dt.Rows[i]["班别"].ToString();
                            dataGridView1.Rows[i].Cells[8].Value = dt.Rows[i]["电话号码"].ToString();
                            dataGridView1.Rows[i].Cells[9].Value = dt.Rows[i]["邮箱地址"].ToString();
                            dataGridView1.Rows[i].Cells[10].Value = dt.Rows[i]["自我介绍"].ToString();
                            dataGridView1.Rows[i].Cells[11].Value = dt.Rows[i]["学院"].ToString();
                            dataGridView1.Rows[i].Cells[12].Value = dt.Rows[i]["团队编号"].ToString();
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
        
        private void button5_Click(object sender, EventArgs e)
        {
        }

        //Excel表中的列名和数据库中的列名一定要对应   
        private void insertToSql_stu(DataRow dr)      
        {            
            string account = dr["账号"].ToString();
            string password = dr["密码"].ToString();
            string name = dr["姓名"].ToString();
            string sex = dr["性别"].ToString();
            string id = dr["学号"].ToString();
            string grade = dr["年级"].ToString();
            string major = dr["专业"].ToString();
            string Class = dr["班别"].ToString();
            string phone = dr["电话号码"].ToString();
            string email = dr["邮箱地址"].ToString();
            string introduce = dr["自我介绍"].ToString();
            string academy = dr["学院"].ToString();
            string groupID = dr["团队编号"].ToString();
            SQLDBhelper.ExecuteNonQuery(string.Format("insert into stupasswordtable values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", account, password, name, sex, id, grade, major, Class, phone, email, introduce, academy, groupID));        
        }

        //调用insertToSql方法，将数据导入数据库
        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    DataRow dr = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        insertToSql_stu(dr);
                    }
                    
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
            conn.Close();
        }

        private void im_stu_account_Load(object sender, EventArgs e)
        {
            select_stu();
        }

        
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {                       
        }

        //对应的年级专业的学生信息，使用函数以便后面使用
        public void select_stu()
        {          
                 
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            //dataGridView1.DataSource = bll.select(comboBox1.Text.Trim(), comboBox2.Text.Trim());
            DataTable selectmessage = SQLDBhelper.ExecuteDataTable(string.Format("select * from stupasswordtable where grade='{0}' and major='{1}' ", comboBox1.Text.Trim(), comboBox2.Text.Trim()));
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
                    dataGridView1.Rows[i].Cells[5].Value = selectmessage.Rows[i]["grade"].ToString();
                    dataGridView1.Rows[i].Cells[6].Value = selectmessage.Rows[i]["major"].ToString();
                    dataGridView1.Rows[i].Cells[7].Value = selectmessage.Rows[i]["class"].ToString();
                    dataGridView1.Rows[i].Cells[8].Value = selectmessage.Rows[i]["phone"].ToString();
                    dataGridView1.Rows[i].Cells[9].Value = selectmessage.Rows[i]["email"].ToString();
                    dataGridView1.Rows[i].Cells[10].Value = selectmessage.Rows[i]["introduce"].ToString();
                    dataGridView1.Rows[i].Cells[11].Value = selectmessage.Rows[i]["academy"].ToString();
                    dataGridView1.Rows[i].Cells[12].Value = selectmessage.Rows[i]["groupid"].ToString();
                }
            }
        }

        //查询功能
        private void button3_Click(object sender, EventArgs e)
        {
            select_stu();        
        }

        //添加
        private void button5_Click_1(object sender, EventArgs e)
        {
            base.Controls.Clear();
            base.Controls.Add(new stu_message());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        //修改
        private void button2_Click(object sender, EventArgs e)
        {
            if (UserHelper.xxid=="")
            {
                MessageBox.Show("请先选择需要修改信息的学生！");
            }
            else
            {
                base.Controls.Clear();
                base.Controls.Add(new edit_stu_message());
            }
            
        }
        //删除
        private void button6_Click(object sender, EventArgs e)
        {
            bll.delect(UserHelper.xxid);
            //调用函数刷新dataGridView1
            select_stu();
            
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
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
