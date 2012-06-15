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
    public partial class SelOtdel : Form
    {

        public string SelIDO = "";
        public string SelNameO = "";

        public SelOtdel()
        {
            InitializeComponent();

            this.Paint += new PaintEventHandler(SelOtdel_Paint);
            this.KeyDown +=new KeyEventHandler(SelOtdel_KeyDown);
            ListOtdels.CellDoubleClick +=new DataGridViewCellEventHandler(ListOtdels_CellDoubleClick);

            LoadOtdels();
        }

        private void SelOtdel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SelectOtdel();
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void SelOtdel_Paint(object sender, PaintEventArgs e)
        {

            Pen mypen = new Pen(Color.Gray, 1);
            Pen mypen_shadow = new Pen(Color.White, 1);

            int x; int y;

            x = ListOtdels.Left - 2;
            y = ListOtdels.Top - 2;
            e.Graphics.DrawRectangle(mypen, new Rectangle(new Point(x, y), new Size(ListOtdels.Width + 2, ListOtdels.Height + 2)));
            e.Graphics.DrawRectangle(mypen_shadow, new Rectangle(new Point(x + 1, y + 1), new Size(ListOtdels.Width + 2, ListOtdels.Height + 2)));

        }

        private void ListOtdels_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            SelectOtdel();
        }


        private void LoadOtdels()
        {

            OleDbConnection Conn = new OleDbConnection(ClassParams.ConnString);

            try
            {

                string cmdsql = "SELECT ID_Otdel, Name_Otdel FROM Otdels WHERE Part is NULL AND Actual = 1 ORDER BY Name_Otdel";

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

        private void SelectOtdel()
        {
            if (ListOtdels.SelectedRows.Count == 0) return;

            SelIDO = ListOtdels.CurrentRow.Tag.ToString();
            SelNameO = ListOtdels.CurrentCell.Value.ToString();

            this.Close();
        }


    }
}
