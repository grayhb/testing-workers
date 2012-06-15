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
    public partial class EditBlock : Form
    {
        public EditBlock()
        {
            InitializeComponent();
        }



        private void EditBlock_Load(object sender, EventArgs e)
        {
            LoadOtdels();
        }



        private void LoadOtdels()
        {

            OleDbConnection Conn = new OleDbConnection(ClassParams.ConnString);

            try
            {

                string cmdsql = "SELECT Otdels.ID_Otdel, Otdels.Name_Otdel, TestEditBlock.Block FROM Otdels LEFT OUTER JOIN TestEditBlock ON Otdels.ID_Otdel = TestEditBlock.ID_Otdel WHERE Otdels.Part is NULL AND Otdels.Actual = 1 ORDER BY Otdels.Name_Otdel";

                DataSet myDataSet = new DataSet();

                OleDbCommand myAccessCommand = new OleDbCommand(cmdsql, Conn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                Conn.Open();
                myDataAdapter.Fill(myDataSet, "Otdels");

                ListOtdels.Rows.Clear();
                DataGridViewRow DGVR;

                DataRowCollection dra = myDataSet.Tables["Otdels"].Rows;
                foreach (DataRow dr in dra)
                {
                    ListOtdels.Rows.Add();
                    DGVR = ListOtdels.Rows[ListOtdels.Rows.Count - 1];
                    DGVR.Cells[0].Value = dr[1];
                    DGVR.Tag = dr[0];
                    if (dr[2].ToString() == "1")
                    {
                        DGVR.Cells[1].Value = "тестирование";
                    }
                    else DGVR.Cells[1].Value = "редактирование";
                        
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            if (ListOtdels.SelectedRows.Count == 0) return;

            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();
            DataGridViewRow DGVR = ListOtdels.SelectedRows[0];
            try
            {
                Conn.Open();
                cmd = Conn.CreateCommand();
                cmd.CommandText = string.Format("DELETE FROM TestEditBlock WHERE ID_Otdel = {0}", DGVR.Tag);
                cmd.ExecuteNonQuery();
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
                DGVR.Cells[1].Value = "редактирование";
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (ListOtdels.SelectedRows.Count == 0) return;

            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();
            DataGridViewRow DGVR = ListOtdels.SelectedRows[0];

            try
            {
                Conn.Open();
                cmd = Conn.CreateCommand();
                cmd.CommandText = "INSERT INTO TestEditBlock (ID_Otdel, Block) VALUES (@ID_Otdel, @Block)";
                cmd.Parameters.Add("@ID_Otdel", OleDbType.Integer).Value = DGVR.Tag;
                cmd.Parameters.Add("@ID_OtBlockdel", OleDbType.Integer).Value = 1;
                cmd.ExecuteNonQuery();
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
                DGVR.Cells[1].Value = "тестирование";
            }

        }

    }
}
