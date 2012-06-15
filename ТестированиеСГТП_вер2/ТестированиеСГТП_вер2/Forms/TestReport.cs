using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;

namespace ТестированиеСГТП_вер2
{
    public partial class TestReport : Form
    {

        private string sqlFltr = "";

        //объявляем класс для работы с БД
        private ClassDB ClsDB = new ClassDB();

        public TestReport()
        {
            InitializeComponent();
            this.Shown +=new EventHandler(TestReport_Shown);
            this.ResizeBegin +=new EventHandler(TestReport_ResizeBegin);
            this.SizeChanged += new EventHandler(TestReport_SizeChanged);
            this.MaximumSizeChanged += new EventHandler(TestReport_SizeChanged);
            LoadReports();
        }

        private void TestReport_SizeChanged(object sender, EventArgs e)
        {
            ListReport.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Application.DoEvents();
            ListReport.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }


        private void TestReport_ResizeBegin(object sender, EventArgs e)
        {
            //ListReport.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void TestReport_Shown(object sender, EventArgs e)
        {
            ListReport.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }


        private void LoadReports()
        {

            OleDbConnection Conn = new OleDbConnection(ClassParams.ConnString);

            try
            {

                string cmdsql = "SELECT ID_Rec, ID_Test, ID_Worker, RightAnswers, CountQuestions, Report, DateStart, DateEnd "
                              + " FROM TestWorkersReports " + sqlFltr + " ORDER BY ID_Rec DESC";

                DataSet myDataSet = new DataSet();

                OleDbCommand myAccessCommand = new OleDbCommand(cmdsql, Conn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                Conn.Open();
                myDataAdapter.Fill(myDataSet, "Otdels");

                Hashtable ReportDetail = new Hashtable();
                ListReport.Rows.Clear();
                DataGridViewRow DGVR;

                DataRowCollection dra = myDataSet.Tables["Otdels"].Rows;
                
                CountLabel.Text = "Найдено отчетов: " + dra.Count.ToString();

                foreach (DataRow dr in dra)
                {
                    ListReport.Rows.Add();
                    DGVR = ListReport.Rows[ListReport.Rows.Count - 1];



                    Hashtable UD = UserDetail(dr[2].ToString());

                    DGVR.Cells[0].Value = UD["Name_Otdel"].ToString();
                    DGVR.Cells[1].Value = UD["FIO"].ToString();
                    DGVR.Cells[2].Value = UD["N_Post"].ToString();

                    DGVR.Cells[3].Value = Convert.ToDateTime(dr[6]).ToShortDateString();
                    DGVR.Cells[4].Value = dr[4];
                    DGVR.Cells[5].Value = dr[3];
                    if ((int)dr[4] > 0) DGVR.Cells[6].Value = Convert.ToInt32( 100 / Convert.ToDouble(dr[4]) * Convert.ToDouble(dr[3]));

                    ReportDetail = new Hashtable();
                    ReportDetail.Add("ID_Rec", dr[0]);
                    ReportDetail.Add("ID_Test", dr[1]);
                    ReportDetail.Add("Otdel", UD["Name_Otdel"].ToString());
                    ReportDetail.Add("FIO", UD["FIO"].ToString());
                    ReportDetail.Add("Post", UD["N_Post"].ToString());
                    ReportDetail.Add("RightAnswers", dr[3]);
                    ReportDetail.Add("CountQuestions", dr[4]);
                    ReportDetail.Add("Report", dr[5]);
                    ReportDetail.Add("DateStart", dr[6]);
                    ReportDetail.Add("DateEnd", dr[7]);
                    ReportDetail.Add("Percent", DGVR.Cells[6].Value);

                    DGVR.Tag = ReportDetail;
                    
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка: Не удалось получить необходимые данные из базы данных.\n\n {0}", ex.Message), "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.StackTrace);
                return;
            }
            finally
            {
                Conn.Close();
            }

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private Hashtable UserDetail (string ID_Worker)
        {

            string SqlStr = "SELECT      dbo.Posts.N_Post, dbo.Workers.F_Worker + ' ' + dbo.Workers.N_Worker + ' ' + dbo.Workers.P_Worker AS FIO, dbo.Otdels.Name_Otdel"
                           + " FROM          dbo.Posts INNER JOIN "
                           + " dbo.Workers ON dbo.Posts.ID_Post = dbo.Workers.ID_Post INNER JOIN "
                           + " dbo.Otdels ON dbo.MainOtdel(dbo.Workers.ID_Otdel) = dbo.Otdels.ID_Otdel "
                           + " WHERE      (dbo.Workers.ID_Worker = " + ID_Worker + ") ";

            //определяем отдел
            Dictionary<int, Hashtable> myData = ClsDB.GET_Fields(SqlStr);

            return myData[1];
        }

        private void отчетПоВыбранномуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (ListReport.SelectedRows.Count == 0) return;

            ClassExport CE = new ClassExport();

            CE.ExportSelectUserReport((Hashtable)ListReport.CurrentRow.Tag);

        }

        private void удалитьВыбранныйОтчетToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ListReport.SelectedRows.Count == 0) return;

            Hashtable UD = (Hashtable)ListReport.CurrentRow.Tag;

            if (MessageBox.Show("Удалить отчет по тесту сотрудника: " + UD["FIO"].ToString() + "?", "Удаление отчета", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;

            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();

            try
            {
                Conn.Open();
                cmd.CommandText = "DELETE FROM TestWorkersReports WHERE ID_Rec = " + UD["ID_Rec"].ToString();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка: Не удалось получить необходимые данные из базы данных.\n\n {0}", ex.Message), "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Conn.Close();
                LoadReports();
            }


        }



    }
}
