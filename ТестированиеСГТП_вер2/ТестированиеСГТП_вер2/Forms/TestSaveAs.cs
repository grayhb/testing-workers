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
    public partial class TestSaveAs : Form
    {

        private string OLD_ID_Test;
        private string NEW_ID_Test;
        private string ID_Otdel;


        private DataRowCollection Q_DRC;
        private DataRowCollection[] Answ_DRC;

        public bool FlSave =false;


        public TestSaveAs(string IDO, string IDT)
        {
            InitializeComponent();

            OLD_ID_Test = IDT;
            ID_Otdel = IDO;
            this.KeyDown +=new KeyEventHandler(TestSaveAs_KeyDown);
        }

        private void TestSaveAs_KeyDown(object sender, KeyEventArgs e)
        {
            if (progressBar1.Visible == true) return;

            if (e.KeyCode == Keys.F2) SaveAs();
            if(e.KeyCode == Keys.Escape) this.Close();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            SaveAs();
        }


        private void SaveAs()
        {
            progressBar1.Visible = true;
            NameTest.Visible = false;

            SaveNewTest();
            LoadQuestions();

            progressBar1.Maximum = Q_DRC.Count;
            Application.DoEvents();

            SaveQuestions();

            MessageBox.Show("Тест успешно сохранен","Сохранить как...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FlSave = true;
            this.Close();

        }


        private void SaveQuestions()
        {
            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();

            string NEW_ID_Quest;
            int i =0;
            try
            {
                Conn.Open();

                foreach (DataRow drq in Q_DRC)
                {

                    progressBar1.Value++;

                    //сохраняем вопрос
                    cmd = Conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO TestWorkersQuest (ID_Test, Question, Ord) VALUES (@ID_Test, @Question, @Ord)";
                    cmd.Parameters.Add("@ID_Test", OleDbType.Integer).Value = NEW_ID_Test;
                    cmd.Parameters.Add("@Question", OleDbType.Variant).Value = drq["Question"];
                    cmd.Parameters.Add("@Ord", OleDbType.Integer).Value = drq["Ord"];
                    cmd.ExecuteNonQuery();


                    cmd = Conn.CreateCommand();
                    cmd.CommandText = "SELECT ID_Rec FROM TestWorkersQuest WHERE ID_Test = " + NEW_ID_Test + " ORDER BY ID_Rec DESC";
                    OleDbDataReader result = cmd.ExecuteReader();
                    result.Read();
                    NEW_ID_Quest = result["ID_Rec"].ToString();
                    result.Close();

                    foreach (DataRow drAnsw in Answ_DRC[i])
                    {
                        //сохраняем ответ
                        cmd = Conn.CreateCommand();
                        cmd.CommandText = "INSERT INTO TestWorkersAnsw (ID_Quest, ID_Test, Answer, Ord, RightAnswer) "+
                                          " VALUES (@ID_Quest, @ID_Test, @Answer, @Ord, @RightAnswer)";
                        cmd.Parameters.Add("@ID_Quest", OleDbType.Integer).Value = NEW_ID_Quest;
                        cmd.Parameters.Add("@ID_Test", OleDbType.Integer).Value = NEW_ID_Test;
                        cmd.Parameters.Add("@Answer", OleDbType.Variant).Value = drAnsw["Answer"];
                        cmd.Parameters.Add("@Ord", OleDbType.Integer).Value = drAnsw["Ord"];
                        cmd.Parameters.Add("@RightAnswer", OleDbType.Integer).Value = drAnsw["RightAnswer"];
                        cmd.ExecuteNonQuery();

                    }

                    i++;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(ex.Message, "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                Conn.Close();
            }
        }
        
        private void LoadQuestions()
        {

            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();
            string cmdsql = string.Format("SELECT ID_Rec, Question, Ord FROM TestWorkersQuest WHERE ID_Test = {0} ORDER BY Ord", OLD_ID_Test);

            DataSet myDataSet = new DataSet();

            OleDbCommand myAccessCommand = new OleDbCommand(cmdsql, Conn);
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

            int i= 0;
            try
            {
                Conn.Open();
                myDataAdapter.Fill(myDataSet, "List");

                Q_DRC = myDataSet.Tables["List"].Rows;
                Answ_DRC = new DataRowCollection[Q_DRC.Count];

                foreach (DataRow dr in Q_DRC)
                {
                    cmdsql = string.Format("SELECT Answer, RightAnswer, Ord FROM TestWorkersAnsw WHERE ID_Quest = {0} ORDER BY Ord", dr["ID_Rec"].ToString());
                    myAccessCommand = new OleDbCommand(cmdsql, Conn);
                    myDataAdapter = new OleDbDataAdapter(myAccessCommand);
                    myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet, "List");
                    Answ_DRC[i] = myDataSet.Tables["List"].Rows;
                    i++;
                }

                myDataAdapter.Dispose();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(ex.Message, "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                Conn.Close();
            }

        }

        private void SaveNewTest()
        {
            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();
            try
            {
                Conn.Open();
                cmd = Conn.CreateCommand();
                cmd.CommandText = "INSERT INTO TestWorkers (TestName, ID_Otdel ) VALUES (@TestName, @ID_Otdel)";
                cmd.Parameters.Add("@TestName", OleDbType.Variant).Value = NameTest.Text;
                cmd.Parameters.Add("@ID_Otdel", OleDbType.Integer).Value = ID_Otdel;
                cmd.ExecuteNonQuery();

                cmd = Conn.CreateCommand();
                cmd.CommandText = "SELECT ID_Rec FROM TestWorkers WHERE ID_Otdel = " + ID_Otdel + " ORDER BY ID_Rec DESC";
                OleDbDataReader result = cmd.ExecuteReader();
                result.Read();
                NEW_ID_Test = result["ID_Rec"].ToString();
                result.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(ex.Message, "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                Conn.Close();
            }
        }


    }
}
