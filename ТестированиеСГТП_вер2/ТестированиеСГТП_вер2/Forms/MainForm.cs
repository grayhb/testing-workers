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
    public partial class MainForm : Form
    {

        //объявляем класс для работы с БД
        private ClassDB ClsDB = new ClassDB();

        private string ID_Otdel = "";
        private string ID_Worker = "";


        public struct ComboType
        {

            public int ID_Item;

            public string Name_Item;
            public ComboType(int _ID_Item, string _Name_Item)
            {
                ID_Item = _ID_Item;
                Name_Item = _Name_Item;
            }

            public override string ToString()
            {
                return this.Name_Item;
            }

        }


        public MainForm()
        {
            InitializeComponent();

            выбратьОтделToolStripMenuItem.Visible = false;
            результатыТестовToolStripMenuItem.Visible = false;
            праваДоступаToolStripMenuItem.Visible = false;

            //параметры для подключения к БД
            ClsDB.ReadCFG();

            // загрузка данных о сотруднике
            LoadPersonData(System.Environment.UserName);

            //LoadPersonData("DonskovYF");


            //загрузка перечня тестов
            LoadListTests();

        }

        
        private void LoadListTests()
        {

            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);

            try
            {

                string cmdsql = string.Format("SELECT ID_Rec, TestName FROM TestWorkers WHERE ID_Otdel = {0}", ID_Otdel);

                DataSet myDataSet = new DataSet();

                OleDbCommand myAccessCommand = new OleDbCommand(cmdsql, Conn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                ComboTests.Items.Clear();

                Conn.Open();
                myDataAdapter.Fill(myDataSet, "ListDoc");

                DataRowCollection dra = myDataSet.Tables["ListDoc"].Rows;

                foreach (DataRow dr in dra)
                {
                    ComboTests.Items.Add(new ComboType((int)dr[0],(string)dr[1].ToString().Trim()) );
                }

                if (dra.Count > 8) ComboTests.MaxDropDownItems = dra.Count;


            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка: Не удалось получить необходимые данные из базы данных.\n\n {0}", ex.Message), "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                Conn.Close();
            }

        }

        //определение данных пользователя
        private void LoadPersonData(string login)
        {

            ID_Worker = ClsDB.GET_Field("Workers", "Workers.Login = '" + login + "'", "ID_Worker");
            FIOWorker.Text = ClsDB.GET_Field("Workers", "Workers.Login = '" + login + "'", "dbo.Workers.F_Worker + ' ' + dbo.Workers.I_Worker");
            string IDO = ClsDB.GET_Field("Workers", "Workers.Login = '" + login + "'", "ID_Otdel");
            string Part = ClsDB.GET_Field("Otdels", "Otdels.ID_Otdel = " + IDO, "Part");
            if (Part != "") IDO = Part;
            string SqlStr = "SELECT Otdels.ID_Otdel, Otdels.Name_Otdel FROM Workers INNER JOIN " +
                            " Otdels ON Otdels.ID_Otdel = " + IDO + " WHERE Workers.Login = '" + login + "'";

            //определяем отдел
            Dictionary<int, Hashtable> myData = ClsDB.GET_Fields(SqlStr);

            if (myData.Count > 0)
            {
                Hashtable tmp;
                tmp = myData[1];
                ID_Otdel = tmp["ID_Otdel"].ToString();
                NameOtdelLabel.Text = tmp["Name_Otdel"].ToString();

                if (ID_Otdel == "11" || ID_Worker == "6013" || ID_Worker == "6858")
                {
                    выбратьОтделToolStripMenuItem.Visible = true;
                    результатыТестовToolStripMenuItem.Visible = true;
                    праваДоступаToolStripMenuItem.Visible = true;
                }

                //блокировка редактирования
                if (GetBlock() == true)
                {
                    добавитьТестToolStripMenuItem.Visible = false;
                    редактироватьТестToolStripMenuItem.Visible = false;
                    удалитьВыбранныйТестToolStripMenuItem.Visible = false;
                    toolStripSeparator1.Visible = false;
                    сохранитьВыбранныйТестКакToolStripMenuItem.Visible = false;
                    экспортToolStripMenuItem.Visible = false;
                    toolStripSeparator2.Visible = false;
                }
            }
        }

        private bool GetBlock()
        {
            bool block = false;
            OleDbConnection Conn = new OleDbConnection(ClassParams.ConnString);
            OleDbCommand cmd = Conn.CreateCommand();
            try
            {
                Conn.Open();
                cmd = Conn.CreateCommand();
                cmd.CommandText = "SELECT Block FROM TestEditBlock WHERE ID_Otdel = " + ID_Otdel;
                OleDbDataReader result = cmd.ExecuteReader();
                result.Read();
                if (result.HasRows)
                {
                    if (result["Block"].ToString() == "1") block = true;
                }
                result.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(ex.Message, "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            finally
            {
                Conn.Close();
            }

            return block;
        }


        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ComboTests.SelectedIndex == -1) return;
            button1.Enabled = false;
            TestForm TF = new TestForm(ID_Worker, 
                                       ((ComboType)ComboTests.SelectedItem).ID_Item.ToString(), 
                                       ((ComboType)ComboTests.SelectedItem).Name_Item);
            TF.FIO.Text = FIOWorker.Text;
            this.Hide();
            TF.ShowDialog();
        }

        private void добавитьТестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditForm EF = new EditForm(0,"", ID_Otdel );
            EF.ShowDialog();
            if (EF.FlSave != true) return;
            LoadListTests();
        }

        private void редактироватьТестToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ComboTests.SelectedIndex == -1) return;

            EditForm EF = new EditForm(1, ((ComboType)ComboTests.SelectedItem).ID_Item.ToString(),ID_Otdel);

            EF.NameTest.Text = ((ComboType)ComboTests.SelectedItem).Name_Item.Trim();
            EF.ShowDialog();

            if (EF.FlSave != true) return;
            LoadListTests();

        }

        private void удалитьВыбранныйТестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Удалить выбранный тест?\r\n\n \"{0}\"\r\n\n Результаты теста в том числе буду удалены.", ((ComboType)ComboTests.SelectedItem).Name_Item), "Запрос на удаление теста",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;

            int ID_Test = ((ComboType)ComboTests.SelectedItem).ID_Item;
            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();

            try
            {
                Conn.Open();

                //удаление ответов
                cmd = Conn.CreateCommand();
                cmd.CommandText = string.Format("DELETE FROM TestWorkersAnsw WHERE ID_Test = {0}", ID_Test);
                cmd.ExecuteNonQuery();

                //удаление вопросов
                cmd = Conn.CreateCommand();
                cmd.CommandText = string.Format("DELETE FROM TestWorkersQuest WHERE ID_Test = {0}", ID_Test);
                cmd.ExecuteNonQuery();

                //удаление отчетов-результатов
                cmd = Conn.CreateCommand();
                cmd.CommandText = string.Format("DELETE FROM TestWorkersReports WHERE ID_Test = {0}", ID_Test);
                cmd.ExecuteNonQuery();

                //удаление теста
                cmd = Conn.CreateCommand();
                cmd.CommandText = string.Format("DELETE FROM TestWorkers WHERE ID_Rec = {0}", ID_Test);
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
                LoadListTests();
            }

        }

        private void экспортироватьВыбранныйТестВWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ComboTests.SelectedIndex == -1) return;

            ClassExport CE = new ClassExport();
            this.UseWaitCursor = true;
            CE.ExportToWord(((ComboType)ComboTests.SelectedItem).ID_Item.ToString(), ((ComboType)ComboTests.SelectedItem).Name_Item);
            this.UseWaitCursor = false;
        }

        private void выбратьОтделToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SelOtdel SO = new SelOtdel();
            SO.ShowDialog();

            if (SO.SelIDO == "") return;

            ID_Otdel = SO.SelIDO;
            NameOtdelLabel.Text = SO.SelNameO;

            LoadListTests();

        }

        private void результатыТестовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestReport TR = new TestReport();
            TR.ShowDialog();

        }

        private void сохранитьВыбранныйТестКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ComboTests.SelectedIndex == -1) return;

            TestSaveAs TSA = new TestSaveAs(ID_Otdel, ((ComboType)ComboTests.SelectedItem).ID_Item.ToString());
            TSA.NameTest.Text = ((ComboType)ComboTests.SelectedItem).Name_Item;
            TSA.ShowDialog();

            if (TSA.FlSave == false) return;

            LoadListTests();

        }

        private void праваДоступаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            EditBlock EB = new EditBlock();
            EB.ShowDialog();

        }

        
    }
}
