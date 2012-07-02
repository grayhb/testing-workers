using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.OleDb;

namespace ТестированиеСГТП_вер2
{
    public partial class CopyTest : Form
    {

        private ClassDB ClsDB = new ClassDB();

        private string ID_Test;
        private string TestName = "";
        private string ID_Otdel = "";
        private Dictionary<int, Hashtable> Questions;

        public CopyTest()
        {
            InitializeComponent();
        }


        private void GetTestData()
        {
            if (IDT.Text == "") return;
            string SqlStr = "SELECT ID_Rec, TestName, ID_Otdel FROM TestWorkers WHERE ID_Rec = " + IDT.Text  ;
            //определяем отдел
            Dictionary<int, Hashtable> myData = ClsDB.GET_Fields(SqlStr);
            if (myData.Count == 0) return;
            TestName = myData[1]["TestName"].ToString().Trim();
            ID_Otdel = myData[1]["ID_Otdel"].ToString();
            GetTestQuestions();
        }

        private void GetTestQuestions()
        {
            string SqlStr = "SELECT ID_Rec, ID_Test, Question, Ord FROM dbo.TestWorkersQuest WHERE (ID_Test = " + IDT.Text + ") ORDER BY Ord";
            //грузим вопросы
            Questions = ClsDB.GET_Fields(SqlStr);
            if (Questions.Count == 0) return;

            progressBar1.Maximum = Questions.Count*2;
            progressBar1.Value = 0;

            for (int i = 1; i <= Questions.Count; i++ )
            {
                GetAnswer(Questions[i]["ID_Rec"].ToString(), i);
                progressBar1.Value++;
            }

            SaveTest();
        }

        private void GetAnswer(string IDQuest, int IndexQ)
        {
            Console.WriteLine(IDQuest);
            string SqlStr = "SELECT ID_Test, ID_Quest, Answer, RightAnswer, Ord FROM dbo.TestWorkersAnsw WHERE (ID_Test = " + IDT.Text + ") AND (ID_Quest = " + IDQuest + ")";
            //грузим вопросы
            Dictionary<int, Hashtable> myData = ClsDB.GET_Fields(SqlStr);
            if (myData.Count == 0) return;

            string [,] tmp = new  string [myData.Count,2];
            for (int i = 1; i <= myData.Count; i++)
            {
                tmp[i-1,0] = myData[i]["Answer"].ToString().Trim();
                tmp[i-1,1] = myData[i]["RightAnswer"].ToString().Trim();
            }

            Questions[IndexQ].Add("Answers", tmp);
            
        }

        private void SaveTest()
        {
            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();

            try
            {
                Conn.Open();
                cmd = Conn.CreateCommand();
                cmd.CommandText = "INSERT INTO TestWorkers (TestName, ID_Otdel ) VALUES (@TestName, @ID_Otdel)";
                cmd.Parameters.Add("@TestName", OleDbType.Variant).Value = TestName;
                cmd.Parameters.Add("@ID_Otdel", OleDbType.Integer).Value = ID_Otdel;
                cmd.ExecuteNonQuery();

                cmd = Conn.CreateCommand();
                cmd.CommandText = "SELECT ID_Rec FROM TestWorkers WHERE ID_Otdel = " + ID_Otdel + " ORDER BY ID_Rec DESC";
                OleDbDataReader result = cmd.ExecuteReader();
                result.Read();
                ID_Test = result["ID_Rec"].ToString();
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

                for (int i = 1; i <= Questions.Count; i++)
                {
                    SaveQuestions(Questions[i]["Question"].ToString(), i);
                    progressBar1.Value++;
                }

                progressBar1.Value = 0;
                MessageBox.Show("Готово!");
            }
        }

        private void SaveQuestions(string NameQ, int IndexQ)
        {
            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();
            string ID_Quest = "";
            try
            {
                Conn.Open();
                cmd.CommandText = "SELECT MAX(Ord) as Ord FROM TestWorkersQuest WHERE ID_Test = " + ID_Test;
                OleDbDataReader result = cmd.ExecuteReader();
                result.Read();
                int Ord = 0;
                if (result["Ord"].ToString() == "") Ord = 0;
                else Ord = (int)result["Ord"];
                result.Close();
                Ord++;

                cmd = Conn.CreateCommand();
                cmd.CommandText = "INSERT INTO TestWorkersQuest (ID_Test, Question, Ord ) VALUES (@ID_Test, '" + NameQ + "',@Ord)";
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(ex.Message, "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                Conn.Close();
                if (ID_Quest != "") SaveAnswers(IndexQ,ID_Quest);
            }
        }

        private void SaveAnswers(int IndexQ, string IDQ)
        {
            string[,] ArrAnswers = (string[,])Questions[IndexQ]["Answers"];

            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();

            try
            {
                Conn.Open();
                for (int i = 0; i < ArrAnswers.Length/2; i++)
                {
                    cmd = Conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO TestWorkersAnsw (ID_Quest, ID_Test, Answer, Ord, RightAnswer) " +
                        " VALUES (@ID_Quest, @ID_Test, '" + ArrAnswers[i, 0] + "', @Ord, @RightAnswer)  ";
                    cmd.Parameters.Add("@ID_Quest", OleDbType.Integer).Value = IDQ;
                    cmd.Parameters.Add("@ID_Test", OleDbType.Integer).Value = ID_Test;
                    cmd.Parameters.Add("@Ord", OleDbType.Integer).Value = i + 1;
                    if (ArrAnswers[i, 1] == "")
                    {
                        cmd.Parameters.Add("@RightAnswer", OleDbType.Integer).Value = 0;
                    }
                    else { cmd.Parameters.Add("@RightAnswer", OleDbType.Integer).Value = ArrAnswers[i, 1]; }
                    cmd.ExecuteNonQuery();
                    
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetTestData();
        }
    }
}
