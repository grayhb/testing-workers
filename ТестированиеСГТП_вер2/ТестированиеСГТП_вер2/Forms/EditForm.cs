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
    public partial class EditForm : Form
    {
        private string ID_Test;
        private string ID_Otdel;

        private bool NeedSaveOrd = false;

        public bool FlSave = false;

        public EditForm(int means, string IDT, string IDO)
        {
            InitializeComponent();

            DGVList.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(DGVList_CellMouseDoubleClick);

            this.KeyDown += new KeyEventHandler(EditForm_KeyDown);
            this.FormClosing +=new FormClosingEventHandler(EditForm_FormClosing);

            if (IDT != null) ID_Test = IDT;
            if (IDO != null) ID_Otdel = IDO;

            if (means == 0)  //добавить новый тест
            {
                groupBox2.Visible = false;
                toolStrip1.Visible = false;
                this.Size = new Size(this.Size.Width, 154);
                toolStripStatusLabel4.Visible = false;
                toolStripStatusLabel5.Visible = false;
            }
            else  //редактировать
            {
                if (ID_Test == null) this.Close();
                LoadQuetsions();
            }
        }

        private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (NeedSaveQuest == true) SaveQuest();
            if (NeedSaveOrd == true) SaveOrd();
        }

        private void EditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) SaveQuest();
            if (e.KeyCode == Keys.Escape) this.Close();

            if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.Up) OrdUp(0);
            if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.Down) OrdDown(0);

        }

        
        private void OrdUp(int Par)
        {
            if (DGVList.SelectedRows.Count == 0) return;
            if (DGVList.SelectedRows[0].Index == 0) return;

            DataGridViewRow DGVRSelect = DGVList.SelectedRows[0];
            DataGridViewRow DGVRUp = DGVList.Rows[DGVList.SelectedRows[0].Index-1];

            string SelQuest = DGVRSelect.Cells[1].Value.ToString();
            string SelID = DGVRSelect.Tag.ToString();

            DGVRSelect.Cells[1].Value = DGVRUp.Cells[1].Value;
            DGVRSelect.Tag = DGVRUp.Tag;

            DGVRUp.Tag = SelID;
            DGVRUp.Cells[1].Value = SelQuest;

            if (Par == 1)
            {
                DGVList.ClearSelection();
                DGVRUp.Selected = true;
                DGVRUp.Cells[0].Selected = true;
            }

            NeedSaveOrd = true;

        }

        private void OrdDown(int Par)
        {
            if (DGVList.SelectedRows.Count == 0) return;
            if (DGVList.SelectedRows[0].Index == DGVList.Rows.Count-1) return;

            DataGridViewRow DGVRSelect = DGVList.SelectedRows[0];
            DataGridViewRow DGVRDown = DGVList.Rows[DGVList.SelectedRows[0].Index + 1];

            string SelQuest = DGVRSelect.Cells[1].Value.ToString();
            string SelID = DGVRSelect.Tag.ToString();

            DGVRSelect.Cells[1].Value = DGVRDown.Cells[1].Value;
            DGVRSelect.Tag = DGVRDown.Tag;

            DGVRDown.Tag = SelID;
            DGVRDown.Cells[1].Value = SelQuest;

            if (Par == 1)
            {
                DGVList.ClearSelection();
                DGVRDown.Selected = true;
                DGVRDown.Cells[0].Selected = true;
            }

            NeedSaveOrd = true;

        }

        private void SaveOrd()
        {
            if (DGVList.Rows.Count == 0) return;

            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();

            try
            {
                Conn.Open();

                foreach (DataGridViewRow DGVR in DGVList.Rows)
                {
                    cmd = Conn.CreateCommand();
                    cmd.CommandText = "UPDATE TestWorkersQuest SET Ord=@Ord WHERE ID_Rec=" + DGVR.Tag.ToString();
                    cmd.Parameters.Add("@Ord", OleDbType.Integer).Value = DGVR.Index;
                    cmd.ExecuteNonQuery();
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

        private void SaveQuest()
        {
            bool iNew = false;
            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();

            try
            {
                Conn.Open();

                if (ID_Test  == "")
                {
                    //сохранение нового теста:
                    iNew = true;

                    cmd = Conn.CreateCommand();
                    cmd.CommandText = "INSERT INTO TestWorkers (TestName, ID_Otdel ) VALUES (@TestName, @ID_Otdel)";
                    cmd.Parameters.Add("@TestName", OleDbType.Variant).Value = NameTest.Text;
                    cmd.Parameters.Add("@ID_Otdel", OleDbType.Integer).Value = ID_Otdel;
                    cmd.ExecuteNonQuery();

                    cmd = Conn.CreateCommand();
                    cmd.CommandText = "SELECT ID_Rec FROM TestWorkers WHERE ID_Otdel = " + ID_Otdel + " ORDER BY ID_Rec DESC";
                    OleDbDataReader result = cmd.ExecuteReader();
                    result.Read();
                    ID_Test = result["ID_Rec"].ToString();
                    result.Close();
                }
                else
                {
                    //редактирование :
                    cmd = Conn.CreateCommand();
                    cmd.CommandText = "UPDATE TestWorkers SET TestName=@TestName WHERE ID_Rec=" + ID_Test;
                    cmd.Parameters.Add("@TestName", OleDbType.Variant).Value = NameTest.Text;
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
            }

            if (iNew == false) this.Close();
            else
            {
                this.Size = new Size(this.Width,639);

                this.Location = new Point(
                    Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2,
                    Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2);

                groupBox2.Visible = true;
                toolStrip1.Visible = true;

                toolStripStatusLabel4.Visible = true;
                toolStripStatusLabel5.Visible = true;

            }


        }
        
        private void DGVList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            
            if (e.RowIndex == -1) return;

            EditQuest();

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

                DGVList.Rows.Clear();
                DataGridViewRow DGVR;
                int i = 0;

                Conn.Open();
                myDataAdapter.Fill(myDataSet, "ListDoc");

                DataRowCollection dra = myDataSet.Tables["ListDoc"].Rows;
                
                foreach (DataRow dr in dra)
                {
                    i++;
                    DGVList.Rows.Add();
                    DGVR = DGVList.Rows[DGVList.Rows.Count - 1];

                    DGVR.Tag = dr[0];
                    DGVR.Cells[0].Value = string.Format("{0:00}",i);
                    DGVR.Cells[1].Value = dr[1].ToString().Trim();
                }

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
            }


        }

        private void toolStripButton2_Click(object sender, EventArgs e) {
            EditQuest();
        }

        private void EditQuest() {

            if (DGVList.SelectedRows.Count == 0) return;


            EditQ EQ = new EditQ(1,ID_Test,DGVList.SelectedRows[0].Tag.ToString());
            EQ.NameQ.Text = DGVList.SelectedRows[0].Cells[1].Value.ToString().Trim();
            EQ.ShowDialog();

            if (EQ.FlSave == false) return;

            DGVList.SelectedRows[0].Cells[1].Value = EQ.NameQ.Text;

        }
        
        private void AddNewQuest()
        {

            EditQ EQ = new EditQ(0, ID_Test, "");
            EQ.ShowDialog();

            if (EQ.FlSave == false) return;

            LoadQuetsions();

        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            SaveQuest();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (DGVList.SelectedRows.Count == 0) return;

            if (MessageBox.Show(string.Format("Удалить выбранный вопрос?\r\n{0}", DGVList.SelectedRows[0].Cells[1].Value.ToString()),"Удаление вопроса",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;

            int ID_Quest = (int)DGVList.SelectedRows[0].Tag;
            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();

            try
            {
                Conn.Open();

                //удаление ответов
                cmd = Conn.CreateCommand();
                cmd.CommandText = string.Format("DELETE FROM TestWorkersAnsw WHERE ID_Test = {0} AND ID_Quest = {1}", ID_Test, ID_Quest);
                cmd.ExecuteNonQuery();

                //удаление вопроса
                cmd = Conn.CreateCommand();
                cmd.CommandText = string.Format("DELETE FROM TestWorkersQuest WHERE ID_Rec = {0}", ID_Quest);
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
                DGVList.Rows.RemoveAt(DGVList.SelectedRows[0].Index);
            }


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddNewQuest();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveQuest();
        }

        private void toolStripStatusLabel4_Click(object sender, EventArgs e)
        {
            OrdUp(1);
        }

        private void toolStripStatusLabel5_Click(object sender, EventArgs e)
        {
            OrdDown(1);
        }

    }
}
