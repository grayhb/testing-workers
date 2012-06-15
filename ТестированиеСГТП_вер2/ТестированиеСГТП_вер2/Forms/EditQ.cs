using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace ТестированиеСГТП_вер2
{
    public partial class EditQ : Form
    {

        public bool FlSave = false;
        string[] ABCD = { "", "A", "B", "C", "D" };

        private string ID_Test = "";
        private string ID_Quest = "";

        public EditQ(int means, string IDT, string IDQ)
        {
            InitializeComponent();
            
            ID_Test = IDT;
            ID_Quest = IDQ;

            this.KeyDown += new KeyEventHandler(EditQ_KeyDown);
            this.Shown += new EventHandler(EditQ_Shown);
            this.ResizeEnd += new EventHandler(EditQ_ResizeEnd);
            this.SizeChanged += new EventHandler(EditQ_ResizeEnd);

            for (int i = 1; i <= 4; i++)
            {
                DGVList.Rows.Add();
                DGVList.Rows[i - 1].Cells[0].Value = ABCD[i];
            }


            

            //загружаем ответы
            if (means != 0) LoadAnswers();
            
            
        }


        private void EditQ_Load(object sender, EventArgs e)
        {
           if (NameQ.Text !="") NameQ.Text = CheckQuest(NameQ.Text);
        }

        private void ResizeAnswRow()
        {
            int ConstH = (DGVList.Height - 20) / 4;
            if (ConstH < 20) ConstH = 20;
            foreach (DataGridViewRow DGVR in DGVList.Rows)
            {
                DGVR.Height = ConstH;
            }
        }


        private void EditQ_ResizeEnd(object sender, EventArgs e)
        {
            ResizeAnswRow();
        }

        private void EditQ_Shown(object sender, EventArgs e)
        {
            NameQ.SelectionStart = NameQ.SelectionLength;
            NameQ.SelectionLength = 0;

            
        }

        private void EditQ_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2) SaveQuest();
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5)  UpdateFields();
            
        }

        private string CheckAnsw(string Answ){

            if (Answ.IndexOf(")", 1) != -1 && Answ.IndexOf(")", 1) < 4 && Answ.Length > 4)
            {
                return Answ.Substring(Answ.IndexOf(")", 1)+1,Answ.Length-Answ.IndexOf(")")-1).Trim();
            }

            return Answ;
        }

        private string CheckQuest(string Question)
        {

            Console.WriteLine(Question.IndexOf(".", 1).ToString());
            if (Question.IndexOf(".", 1) > 1 && Question.IndexOf(".", 1) < 3)
            {
                Question = Question.Substring(Question.IndexOf(".", 1) + 1, Question.Length - (Question.IndexOf(".", 1) + 1));
            }

            /*
            if (Question.IndexOf(".", 1) != -1) 
                Question.Substring(Question.IndexOf(".", 1) + 1, Question.Length - Question.IndexOf(".") - 1).Trim();

            if (Question.IndexOf(".", 2) != -1)
                Question.Substring(Question.IndexOf(".", 2) + 1, Question.Length - Question.IndexOf(".") - 1).Trim();

            if (Question.IndexOf(".", 3) != -1)
                Question.Substring(Question.IndexOf(".", 3) + 1, Question.Length - Question.IndexOf(".") - 1).Trim();
            */

            return Question;
        }

        private bool CheckFieldAnsw() {

            foreach (DataGridViewRow DGVR in DGVList.Rows) {
                if (DGVR.Cells[1].Value == null) return false;
                if (DGVR.Cells[1].Value.ToString().Trim() == "") return false;
            }

            return true;
        }

        private void LoadAnswers()
        {
            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);

            try
            {

                string cmdsql = string.Format("SELECT TOP 4 Answer, RightAnswer FROM TestWorkersAnsw WHERE ID_Test = {0} AND ID_Quest = {1} ORDER BY Ord", ID_Test, ID_Quest);

                DataSet myDataSet = new DataSet();

                OleDbCommand myAccessCommand = new OleDbCommand(cmdsql, Conn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                DataGridViewRow DGVR;
                int i = -1 ;

                Conn.Open();
                myDataAdapter.Fill(myDataSet, "ListDoc");

                DataRowCollection dra = myDataSet.Tables["ListDoc"].Rows;

                foreach (DataRow dr in dra)
                {
                    i++;
                    DGVR = DGVList.Rows[i];
                    if (dr[0] != null)
                    {
                        DGVR.Cells[1].Value = CheckAnsw(dr[0].ToString().Trim());
                    }
                    if (dr[1].ToString() == "1")
                    {
                        DGVR.Cells[2].Value = 1;
                    }
                    
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
                ResizeAnswRow();
            }


        }


        private void SaveQuest() {

            if (DGVList.SelectedRows.Count > 0)
            {
                DGVList.ClearSelection();
                DGVList.CurrentCell= DGVList.Rows[0].Cells[0];
                DGVList.Rows[0].Selected = true;
            }

            if (CheckFieldAnsw() == false || NameQ.Text.Trim() == "")
            {
                MessageBox.Show("Для сохранения заполните все поля (вопроса и ответов)", "Остановка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();

            try
            {
                Conn.Open();

                if (ID_Quest == "")
                {
                    //сохранение нового вопроса:
                    int Ord = 0;
                    cmd.CommandText = "SELECT MAX(Ord) as Ord FROM TestWorkersQuest WHERE ID_Test = " + ID_Test;
                    OleDbDataReader result = cmd.ExecuteReader();
                    result.Read();
                    if (result["Ord"].ToString() == "") Ord = 0;
                    else Ord = (int)result["Ord"];
                    result.Close();
                    Ord++;

                    cmd = Conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO TestWorkersQuest (ID_Test, Question, Ord ) VALUES (@ID_Test, '" + NameQ.Text + "',@Ord)";
                    cmd.Parameters.Add("@ID_Test", OleDbType.Integer).Value = ID_Test;
                    cmd.Parameters.Add("@Ord", OleDbType.Integer).Value = Ord;
                    cmd.ExecuteNonQuery();

                    cmd = Conn.CreateCommand();
                    cmd.CommandText = "SELECT ID_Rec FROM TestWorkersQuest WHERE ID_Test = " + ID_Test + " AND Ord = " + Ord.ToString();
                    result = cmd.ExecuteReader();
                    result.Read();
                    ID_Quest = result["ID_Rec"].ToString();
                    result.Close();
                }
                else
                {
                    //редактирование :
                    cmd = Conn.CreateCommand();
                    cmd.CommandText = "UPDATE TestWorkersQuest SET Question='" + NameQ.Text.Trim() + "' WHERE ID_Rec=" + ID_Quest;
                    cmd.ExecuteNonQuery();
                }

                //удаление ответов
                cmd = Conn.CreateCommand();
                cmd.CommandText = string.Format("DELETE FROM TestWorkersAnsw WHERE ID_Test = {0} AND ID_Quest = {1}",ID_Test, ID_Quest);
                cmd.ExecuteNonQuery();

                //сохраняем ответы
                foreach (DataGridViewRow DGVR in DGVList.Rows) {

                    cmd = Conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO TestWorkersAnsw (ID_Quest, ID_Test, Answer, Ord, RightAnswer) " +
                        " VALUES (@ID_Quest, @ID_Test, '" + DGVR.Cells[1].Value.ToString() + "', @Ord, @RightAnswer)  ";
                    cmd.Parameters.Add("@ID_Quest", OleDbType.Integer).Value = ID_Quest;
                    cmd.Parameters.Add("@ID_Test", OleDbType.Integer).Value = ID_Test;
                    cmd.Parameters.Add("@Ord", OleDbType.Integer).Value = DGVR.Index+1;
                    if (DGVR.Cells[2].Value == null)
                    {
                        cmd.Parameters.Add("@RightAnswer", OleDbType.Integer).Value = 0;
                    }
                    else { cmd.Parameters.Add("@RightAnswer", OleDbType.Integer).Value = DGVR.Cells[2].Value; }
                    cmd.ExecuteNonQuery();
                }

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
                FlSave = true;
                this.Close();
            }

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveQuest();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            SaveQuest();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            UpdateFields();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            PasteInBuffer();
        }

        private void PasteInBuffer()
        {
            Buffer.Focus();
            Buffer.Text = Clipboard.GetText();
        }

        private void UpdateFields()
        {
            if (Buffer.Text == "") return;
            string[] ArrQ = Regex.Split(Buffer.Text, "\r\n");

            if (ArrQ.Length < 2) return;

            NameQ.Text = ArrQ[0];

            for (int i = 1; i <= ArrQ.Length - 1; i++)
            {
                if (i > 4) break;
                DGVList.Rows[i - 1].Cells[1].Value = CheckAnsw(ArrQ[i]);
            }
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void копироватьВопросВБуферToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string myStr = NameQ.Text;

            foreach (DataGridViewRow DGVR in DGVList.Rows)
            {
                myStr += "\n";
                myStr += DGVR.Cells[0].Value.ToString() + ") " + DGVR.Cells[1].Value.ToString();
            }

            Clipboard.SetText(myStr);

        }


    }
}
