using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using System.IO;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class noteam : UserControl
    {

        static string FileName = null;
        static DataTable dt = new DataTable();


        public noteam()
        {
            InitializeComponent();
        }

        private void noteam_Load(object sender, EventArgs e)
        {
            this.grade.SelectedIndex = 0;
            this.major.SelectedIndex = 0;
        }

        private void find_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();

            DataTable stu = SQLDBhelper.ExecuteDataTable(string.Format("select name ,id ,class,phone,email from stupasswordtable where grade='{0}' and major='{1}' and groupid='0'",grade.SelectedItem.ToString(),major.SelectedItem.ToString()));
            if(stu.Rows.Count!=0)
            {
                for(int i=0;i<stu.Rows.Count;i++)
                {
                    dataGridView1.Rows.Add(stu);
                    dataGridView1.Rows[i].Cells[0].Value = stu.Rows[i]["name"].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = stu.Rows[i]["id"].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = stu.Rows[i]["class"].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = stu.Rows[i]["phone"].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = stu.Rows[i]["email"].ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("暂无数据可以导出");
                return;
            }
            try
            {
                dt.Columns.Add("姓名");
                dt.Columns.Add("学号");
                dt.Columns.Add("班别");
                dt.Columns.Add("电话");
                dt.Columns.Add("邮箱地址");
                
            }
            catch (System.Data.DuplicateNameException)
            {

            }

            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataRow dc = dt.NewRow();
                for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
                {
                    dc[j] = this.dataGridView1.Rows[i].Cells[j].Value;
                }

                if (dt.Select(string.Format("学号='{0}'", dc[1].ToString())).Length == 0)
                    dt.Rows.Add(dc);
            }

            FileName = grade.SelectedItem.ToString() + major.SelectedItem.ToString() + "未完成组队学生名单";

            Thread thread = new Thread(new ThreadStart(savefile));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            



        }




        public void savefile()
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
                dt.Clear();
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



    }
}
