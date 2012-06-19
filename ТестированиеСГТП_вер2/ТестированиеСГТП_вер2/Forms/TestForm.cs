using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ТестированиеСГТП_вер2
{
    
    public partial class TestForm : Form
    {

        private string ID_Worker = "";
        private string ID_Test = "";

        private string[] ABCD = { "A", "B", "C", "D", "E", "F", "G", "H" }; // на 8 вариантов

        private int CntFinishQ = 0;
        private bool iClose = false;

        private DateTime DateStart = DateTime.Now;
        private DateTime DateEnd;


        [Serializable]
        
        public struct QuestType
        {
            public string IndQ;
            public string TextQ;
            public object Answ;
        }


        public TestForm(string IDW, string IDT, string NameTest)
        {
            InitializeComponent();

            ID_Worker = IDW;
            ID_Test = IDT;
            this.Text = NameTest;

            this.KeyDown += new KeyEventHandler(TestForm_KeyDown);
            this.FormClosed +=new FormClosedEventHandler(TestForm_FormClosed);
            this.FormClosing += new FormClosingEventHandler(TestForm_FormClosing);
            this.Paint += new PaintEventHandler(TestForm_Paint);
            ListButtons.SelectionChanged += new EventHandler(ListButtons_SelChange);
            ListAnsw.CellValueChanged += new DataGridViewCellEventHandler(ListAnsw_CellValueChanged);
            ListAnsw.CellClick += new DataGridViewCellEventHandler(ListAnsw_CellClick);

        }

        private void TestForm_Paint(object sender, PaintEventArgs e)
        {

            Pen mypen = new Pen(Color.Gray, 1);
            Pen mypen_shadow = new Pen(Color.White, 1);

            int x; int y;

            x = ListButtons.Left - 2;
            y = ListButtons.Top - 2;
            e.Graphics.DrawRectangle(mypen, new Rectangle(new Point(x, y), new Size(ListButtons.Width + 2, ListButtons.Height + 2)));
            e.Graphics.DrawRectangle(mypen_shadow, new Rectangle(new Point(x + 1, y + 1), new Size(ListButtons.Width + 2, ListButtons.Height + 2)));


        }


        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iClose == false)
            {
                if (MessageBox.Show("Результаты тестирования не будут сохранены при закрытии теста. Продолжить?", "Закрытие теста", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) e.Cancel = true;
            }
        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            LoadQuetsions();

            if (ListButtons.Rows.Count == 0)
            {
                MessageBox.Show("К сожалению вопросов для тестирования не найдено.", "Остановка тестирования", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                iClose = true;
                this.Opacity = 0;
                Application.DoEvents();
                Application.Exit();
            }
        }

        private void ListAnsw_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && (int)ListAnsw.Tag == 1)
            {
                SetRightAnsw(1, ListButtons.SelectedRows[0].Index);
            }
        }

        private void ListAnsw_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex != -1 && (int)ListAnsw.Tag == 1)
            {
                //SetRightAnsw(0);
            }

        }

        private void TestForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) button1_Click(null,null);

            //for test:
            if (e.KeyCode == Keys.F5) CheckRightAnsw();

        }

        private void CheckRightAnsw()
        {
            QuestType SelQ = (QuestType)ListButtons.SelectedRows[0].Tag;
            string[,] Answ = (string[,])SelQ.Answ;

            bool FlRight = false;
            for (int i = 0; i <= Answ.GetUpperBound(0); i++)
            {
                if (Answ[i, 1] == Answ[i, 2])
                {
                    FlRight = true;
                    break;
                }
            }

            MessageBox.Show(FlRight.ToString());

        }

        private void SetRightAnsw(int SetValue,int SelIndQ)
        {

            if (ListButtons.SelectedRows.Count == 0) return;
            if (ListAnsw.SelectedRows.Count == 0) return;

            QuestType SelQ = (QuestType)ListButtons.Rows[SelIndQ].Tag;
            
            if (SetValue == 1)
            {
                if (Convert.ToInt32(ListAnsw.CurrentRow.Cells[2].Value) == 0) ListAnsw.CurrentRow.Cells[2].Value = "1";
                else ListAnsw.CurrentRow.Cells[2].Value = "0";
            }

            string[,] Answ = (string[,])SelQ.Answ;

            Answ[ListAnsw.CurrentRow.Index, 2] = ListAnsw.SelectedRows[0].Cells[2].Value.ToString();
            SelQ.Answ = Answ;
            ListButtons.Rows[SelIndQ].Tag = SelQ;

            if (Answ[ListAnsw.CurrentRow.Index, 2] == "0")
            {
                ListAnsw.CurrentRow.DefaultCellStyle.BackColor = Color.White;
                ListAnsw.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.White;
                ListAnsw.RefreshEdit();
            }
            else 
            {
                ListAnsw.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;
                ListAnsw.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
                
                ListAnsw.RefreshEdit();
            }

            ListButtons.Rows[SelIndQ].DefaultCellStyle.BackColor = Color.White;
            foreach (DataGridViewRow DGVR in ListAnsw.Rows)
            {
                if (DGVR.DefaultCellStyle.BackColor == Color.LightGreen)
                {
                    ListButtons.Rows[SelIndQ].DefaultCellStyle.BackColor = Color.LightYellow;
                    break;
                }
            }

            CheckEndTest();
   
        }

        private void CheckEndTest()
        {
            CntFinishQ = 0;
            foreach (DataGridViewRow DGVR in ListButtons.Rows)
            {
                if (DGVR.DefaultCellStyle.BackColor == Color.LightYellow) CntFinishQ++;
            }

            CntFinishQLabel.Text = "Получено ответов: " + CntFinishQ.ToString();
            CntLeftQLabel.Text = "Осталось вопросов: " + (ListButtons.Rows.Count - CntFinishQ).ToString(); 

            if (CntFinishQ == ListButtons.Rows.Count)
            {
                button1.Text = "Закончить тест";
            }

        }

        private void ListButtons_SelChange(object sender, EventArgs e)
        {

            if (ListButtons.Rows.Count > 0 && ListButtons.SelectedRows.Count != 0) LoadSelQ(ListButtons.SelectedRows[0]);
        }

        private void LoadSelQ( DataGridViewRow DGVRQ) {

            if (DGVRQ.Tag == null) return;

            QuestType SelQ = (QuestType)DGVRQ.Tag;
            TextQ.Text = SelQ.TextQ;

            if ((SelQ.Answ is Array) == false)
            {
                SelQ.Answ = LoadAnsw(SelQ.IndQ);
                DGVRQ.Tag = SelQ;
            }

            if (SelQ.Answ is Array)
            {
                string[,] Answ = (string[,])SelQ.Answ;

                DataGridViewRow DGVR;
                ListAnsw.Rows.Clear();
                ListAnsw.Tag = 0;

                for (int i = 0; i <= Answ.GetUpperBound(0); i++)
                {
                    ListAnsw.Rows.Add();
                    DGVR = ListAnsw.Rows[ListAnsw.Rows.Count - 1];
                    DGVR.Cells[0].Value = ABCD[DGVR.Index];
                    DGVR.Cells[1].Value = Answ[i, 0];
                    DGVR.Cells[2].Value = Answ[i, 2];

                    if (Answ[i, 2] == "1")
                    {
                        DGVR.DefaultCellStyle.BackColor = Color.LightGreen;
                        DGVR.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
                    }
                    else
                    {
                        DGVR.DefaultCellStyle.BackColor = Color.White;
                        DGVR.DefaultCellStyle.SelectionBackColor = Color.White;
                    }
                }
                ListAnsw.Tag = 1; //загружены все ответы
            }

            

        }

        private string[,] LoadAnsw(string IndQ)
        {
            string[,] Answ = new string[0, 0];

            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);

            try
            {

                string cmdsql = string.Format("SELECT Answer, RightAnswer FROM TestWorkersAnsw " + 
                                               " WHERE ID_Test = {0} AND ID_Quest = {1} ORDER BY Ord", ID_Test, IndQ);

                DataSet myDataSet = new DataSet();

                OleDbCommand myAccessCommand = new OleDbCommand(cmdsql, Conn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                int i = 0;

                Conn.Open();
                myDataAdapter.Fill(myDataSet, "ListDoc");

                DataRowCollection dra = myDataSet.Tables["ListDoc"].Rows;

                Answ = new string[dra.Count, 3];

                foreach (DataRow dr in dra)
                {
                    Answ[i, 0] = CheckAnsw(dr[0].ToString().Trim());
                    Answ[i, 1] = dr[1].ToString();
                    Answ[i, 2] = "0";
                    i++;
                }

                myDataAdapter.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка: Не удалось получить необходимые данные из базы данных.\n\n {0}", ex.Message), "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                string[,] err = new string[0, 0];
                return err;
            }
            finally
            {
                Conn.Close();
            }

            return Answ;
        }

        private void LoadQuetsions()
        {
            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);

            try
            {

                string cmdsql = string.Format("SELECT ID_Rec, Question FROM TestWorkersQuest WHERE ID_Test = {0} ORDER BY Ord", ID_Test);

                DataSet myDataSet = new DataSet();

                OleDbCommand myAccessCommand = new OleDbCommand(cmdsql, Conn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                DataGridViewRow DGVR;
                ListButtons.Rows.Clear();
                QuestType MyQ;
                int i = 0;

                Conn.Open();
                myDataAdapter.Fill(myDataSet, "ListDoc");

                DataRowCollection dra = myDataSet.Tables["ListDoc"].Rows;

                foreach (DataRow dr in dra)
                {
                    i++;
                    ListButtons.Rows.Add();
                    DGVR = ListButtons.Rows[ListButtons.Rows.Count - 1];
                    MyQ = new QuestType();
                    MyQ.IndQ = dr[0].ToString();
                    MyQ.TextQ = dr[1].ToString().Trim();
                    DGVR.Tag = MyQ;
                    DGVR.Cells[0].Value = string.Format("Вопрос №{0}", i);

                }

                CntAllQLabel.Text = "Всего вопросов: " + dra.Count;
                CntFinishQLabel.Text = "Получено ответов: 0";
                CntLeftQLabel.Text = "Осталось вопросов: " + dra.Count; 

                myDataAdapter.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка: Не удалось получить необходимые данные из базы данных.\n\n {0}", ex.Message), "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                Conn.Close();
                if (ListButtons.SelectedRows.Count != 0 ) LoadSelQ(ListButtons.SelectedRows[0]);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (ListButtons.SelectedRows.Count==0) return;

            

            if (button1.Text != "Закончить тест")
            {

                if (ListButtons.SelectedRows[0].Index < ListButtons.Rows.Count - 1)
                {
                    int SelInd = ListButtons.SelectedRows[0].Index;
                    ListButtons.ClearSelection();
                    ListButtons.Rows[SelInd + 1].Selected = true;
                }
            }
            else FinishTest();
            
        }

        private void FinishTest()
        {

            if (CntFinishQ != ListButtons.Rows.Count)
            {
                MessageBox.Show("Для завершения теста необходимо дать ответы на все вопросы!","Завершение теста",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            this.Enabled = false;
            this.UseWaitCursor = true;

            DateEnd = DateTime.Now;

            int CntRightAnsw = 0;
            int CntRight = 0; int CntUserRight=0;
            string report = "";
            string tmpRightAnsw = "";
            string tmpUserAnsw = "";
            string[,] Answ;

            foreach (DataGridViewRow DGVR in ListButtons.Rows)
            {
                Answ = (string[,])((QuestType)DGVR.Tag).Answ;
                tmpRightAnsw = "";
                tmpUserAnsw = "";
                CntRight = 0; CntUserRight = 0;
                
                for (int i = 0; i <= Answ.GetUpperBound(0); i++)
                {
                    if (Answ[i, 1] == "1")
                    {
                        if (tmpRightAnsw != "") tmpRightAnsw += ", ";
                        tmpRightAnsw += ABCD[i];
                        if (Answ[i, 2] == "1") CntUserRight++;
                    }


                    if (Answ[i, 2] == "1")
                    {
                        if (tmpUserAnsw != "") tmpUserAnsw += ", ";
                        tmpUserAnsw +=  ABCD[i];
                        CntRight++;
                    }
                }

                //если число правильно ответов совпадает
                if (CntUserRight == CntRight)
                {
                    CntRightAnsw++;
                    report += tmpUserAnsw + "|";
                }
                else report += tmpUserAnsw + " (верный ответ - " +  tmpRightAnsw + ")|";
            }

            SaveReport(report, ListButtons.Rows.Count, CntRightAnsw);

            this.UseWaitCursor = false;

            string reportBox = "Тест закончен.\n\n";
            reportBox += string.Format("Всего вопросов в тесте: {0}\n",ListButtons.Rows.Count);
            reportBox += string.Format("Дано правильных ответов: {0}\n\n",CntRightAnsw);
            reportBox += "Детальный отчет по результатам тестирования передан в Отдел кадров";

            MessageBox.Show(reportBox, "Окончание теста",MessageBoxButtons.OK,MessageBoxIcon.Information);
            iClose = true;
            this.Close();
        }

        private void закончитьТестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinishTest();
        }


        private void SaveReport(string report, int CntAllQ, int CntRightQ)
        {

            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();

            try
            {
                Conn.Open();

                cmd = Conn.CreateCommand();
                cmd.CommandText = "INSERT INTO TestWorkersReports (ID_Test, ID_Worker, RightAnswers, CountQuestions, Report, DateStart, DateEnd ) "+
                                  " VALUES (@ID_Test, @ID_Worker, @RightAnswers, @CountQuestions, @Report, @DateStart, @DateEnd)";
                cmd.Parameters.Add("@ID_Test", OleDbType.Integer).Value = ID_Test;
                cmd.Parameters.Add("@ID_Worker", OleDbType.Integer).Value = ID_Worker;
                cmd.Parameters.Add("@RightAnswers", OleDbType.Integer).Value = CntRightQ;
                cmd.Parameters.Add("@CountQuestions", OleDbType.Integer).Value = CntAllQ;
                cmd.Parameters.Add("@Report", OleDbType.Variant).Value = report;
                cmd.Parameters.Add("@DateStart", OleDbType.Date).Value = DateStart;
                cmd.Parameters.Add("@DateEnd", OleDbType.Date).Value = DateEnd;
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(string.Format("Ошибка: Не удалось получить необходимые данные из базы данных.\n\n {0}", ex.Message), "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                Conn.Close();
            }


        }

        private string CheckAnsw(string Answ)
        {

            if (Answ.IndexOf(")", 1) != -1 && Answ.IndexOf(")", 1) < 4 && Answ.Length > 4)
            {
                return Answ.Substring(Answ.IndexOf(")", 1) + 1, Answ.Length - Answ.IndexOf(")") - 1).Trim();
            }

            return Answ;
        }


    }
}
