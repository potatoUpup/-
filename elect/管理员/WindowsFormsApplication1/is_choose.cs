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
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;

namespace WindowsFormsApplication1
{
    public partial class is_choose : UserControl
    {
        static string FileName=null;
        static DataTable dt = new DataTable();
        public is_choose()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void is_choose_Load(object sender, EventArgs e)
        {
            this.comboBox3.SelectedIndex = 0;
            this.comboBox4.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count==0)
            {
                MessageBox.Show("暂无数据可以导出");
                return;
                
            }
            try
            {
                dt.Columns.Add("队长姓名");
                dt.Columns.Add("队长学号");
                dt.Columns.Add("队员1姓名");
                dt.Columns.Add("队员1学号");
                dt.Columns.Add("队员2姓名");
                dt.Columns.Add("队员2学号");
                dt.Columns.Add("队员3姓名");
                dt.Columns.Add("队员3学号");
                dt.Columns.Add("导师姓名");
                dt.Columns.Add("工号");
            }
            catch(System.Data.DuplicateNameException)
            {

            }

            for(int i=0;i<this.dataGridView1.Rows.Count;i++)
            {
                DataRow dc = dt.NewRow();
                for(int j=0;j<this.dataGridView1.Columns.Count;j++)
                {
                    dc[j] = this.dataGridView1.Rows[i].Cells[j].Value;
                }

                if (dt.Select(string.Format("队长学号='{0}'", dc[1].ToString())).Length==0)
                    dt.Rows.Add(dc);
            }

            FileName = comboBox4.SelectedItem.ToString() + comboBox3.SelectedItem.ToString()+"匹配结果";


            Thread thread = new Thread(new ThreadStart(savefile));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            


        }



        public  void  savefile()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "导出Excel文件";
            //设置文件类型
            save.Filter = "Excel 97-2003 文件(*.xls)|*.xls|Excel文件(*.xls)|*.xls|Csv文件(*.csv)|*.csv|所有文件(*.*)|*.*";
            //设置默认文件类型显示顺序  
            save.FilterIndex = 1;
            //是否自动在文件名中添加扩展名
            save.AddExtension = true;
            //是否记忆上次打开的目录
            save.RestoreDirectory = true;
            //设置默认文件名
            save.FileName = FileName;
            //按下确定选择的按钮
            if (save.ShowDialog() == DialogResult.OK)
            {
                //获得文件路径 
                string localFilePath = save.FileName.ToString();
                toExcel(dt, localFilePath);
                
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            DataTable stuidandteaid = SQLDBhelper.ExecuteDataTable(string.Format("select * from team where Teamnumber in (select account from stupasswordtable where grade='{0}' and major='{1}') and Teamnumber in (select stuid from finish)", comboBox4.SelectedItem.ToString(), comboBox3.SelectedItem.ToString()));
            stuidandteaid.Columns.Add("teaname");
            stuidandteaid.Columns.Add("teaid");
            for (int i = 0; i < stuidandteaid.Rows.Count; i++)
            {
                SqlDataReader readteaname = SQLDBhelper.ExecuteReader(string.Format("select t.name ,a.teaid from system2.dbo.teapasswordtable  t left join system2.dbo.finish a on a.teaid=t.account where a.stuid='{0}'", stuidandteaid.Rows[i]["captainid"].ToString()));
                readteaname.Read();
                stuidandteaid.Rows[i]["teaname"] = readteaname[0].ToString();
                stuidandteaid.Rows[i]["teaid"] = readteaname[1].ToString();


                dataGridView1.Rows.Add(stuidandteaid);
                dataGridView1.Rows[i].Cells[0].Value = stuidandteaid.Rows[i]["captainname"].ToString();
                dataGridView1.Rows[i].Cells[1].Value = stuidandteaid.Rows[i]["captainid"].ToString();
                dataGridView1.Rows[i].Cells[2].Value = stuidandteaid.Rows[i]["memberonename"].ToString();
                dataGridView1.Rows[i].Cells[3].Value = stuidandteaid.Rows[i]["memberoneid"].ToString();
                dataGridView1.Rows[i].Cells[4].Value = stuidandteaid.Rows[i]["membertwoname"].ToString();
                dataGridView1.Rows[i].Cells[5].Value = stuidandteaid.Rows[i]["membertwoid"].ToString();
                dataGridView1.Rows[i].Cells[6].Value = stuidandteaid.Rows[i]["memberthreename"].ToString();
                dataGridView1.Rows[i].Cells[7].Value = stuidandteaid.Rows[i]["memberthreeid"].ToString();
                dataGridView1.Rows[i].Cells[8].Value = stuidandteaid.Rows[i]["teaname"].ToString();
                dataGridView1.Rows[i].Cells[9].Value = stuidandteaid.Rows[i]["teaid"].ToString();
            }
        }






        public void toExcel(System.Data.DataTable data, string output)
        {
            try
            {
                //创建工作薄
                HSSFWorkbook workbook = new HSSFWorkbook();
                //创建一个表sheet
                ISheet sheet = workbook.CreateSheet("sheet");
                //创建第一行,新创建的表是没有单元格的,每一个需要写入数据的单元格都要手动创建
                IRow row = sheet.CreateRow(0);
                //将列名写入表的第一行
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    row.CreateCell(i);
                    sheet.GetRow(0).GetCell(i).SetCellValue(data.Columns[i].ColumnName);
                }
                //写入数据
                for (int i = 1; i <= data.Rows.Count; i++)
                {
                    row = sheet.CreateRow(i);
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        row.CreateCell(j);
                        sheet.GetRow(i).GetCell(j).SetCellValue(data.Rows[i - 1][j].ToString());
                    }
                }
                FileStream file = new FileStream(output, FileMode.Create);
                workbook.Write(file);
                file.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(SQLDBhelper.ConnectionString))
            {
                con.Open();
                string command = "update finish set ischeck=1";
                SqlCommand updateischeck = new SqlCommand(command,con);
                updateischeck.ExecuteNonQuery();
                MessageBox.Show("审核通过，已经发布到教师和学生端");
            }
        }
    }
}
