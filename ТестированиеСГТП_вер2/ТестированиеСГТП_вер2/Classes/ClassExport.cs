using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections;
using Word = Microsoft.Office.Interop.Word;

namespace ТестированиеСГТП_вер2
{
    class ClassExport
    {
        private string[] ABCD = { "A", "B", "C", "D", "E", "F", "G", "H" }; // на 8 вариантов

        private string CheckAnsw(string Answ)
        {

            if (Answ.IndexOf(")", 1) != -1 && Answ.IndexOf(")", 1) < 4 && Answ.Length > 4)
            {
                return Answ.Substring(Answ.IndexOf(")", 1) + 1, Answ.Length - Answ.IndexOf(")") - 1).Trim();
            }

            return Answ;
        }


        public void ExportToWord(string ID_Test, string NameTest)
        {
            Application.DoEvents();
            Dictionary<int, Hashtable> QData = new Dictionary<int, Hashtable>();
            Hashtable QHash = new Hashtable();
            int i = 0;
            int MaxAnsw=0;

            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);

            try
            {

                string cmdsql = string.Format("SELECT ID_Rec, Question FROM TestWorkersQuest WHERE ID_Test = {0} ORDER BY Ord", ID_Test);

                DataSet myDataSet = new DataSet();

                OleDbCommand myAccessCommand = new OleDbCommand(cmdsql, Conn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                Conn.Open();
                myDataAdapter.Fill(myDataSet, "ListDoc");

                DataRowCollection dra = myDataSet.Tables["ListDoc"].Rows;

                foreach (DataRow dr in dra)
                {
                    QHash = new Hashtable();
                    QHash.Add("Question",dr[1].ToString().Trim());
                    QHash.Add("Answers", LoadAnswers(ID_Test,dr[0].ToString()));

                    QData.Add(i, QHash);
                    i++;
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


            if (QHash.Count == 0)
            {
                MessageBox.Show("Экспорт не возможен. Вопросы не найдены.","Остановка экспорта",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            //руководитель подразделения:
            Hashtable HeadO = HeadOtdel(ID_Test);



            int k = 0;
            string[] tmp;
            string[] Answ;
            string[] RightAnsw = new string[QData.Count];

            //OBJECT OF MISSING "NULL VALUE"
            Object oMissing = System.Reflection.Missing.Value;
            //OBJECTS OF FALSE AND TRUE
            Object oTrue = true;
            Object oFalse = false;
            //CREATING OBJECTS OF WORD AND DOCUMENT
            Word.Application oWord = new Word.Application();
            Word.Document oWordDoc = new Word.Document();

            //MAKING THE APPLICATION VISIBLE
            oWord.Visible = false;
            oWord.ScreenUpdating = false;
            oWord.System.Cursor = Word.WdCursorType.wdCursorWait;

            try
            {

                //ADDING A NEW DOCUMENT TO THE APPLICATION
                oWordDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                Microsoft.Office.Interop.Word.Document doc = oWord.ActiveDocument;

                doc.Paragraphs[1].Range.ParagraphFormat.LineSpacing = 12;
                doc.Paragraphs[1].Range.ParagraphFormat.SpaceAfter = 12;

                doc.Paragraphs[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc.Paragraphs[1].Range.Font.Bold = 1;
                doc.Paragraphs[1].Range.Text = NameTest;

                doc.Paragraphs.Add();
                doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.SpaceAfter = 6;
                doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                i = 0;
                foreach (Hashtable myHash in QData.Values)
                {

                    doc.Paragraphs[doc.Paragraphs.Count].Range.Text = (i + 1).ToString() + ". " + myHash["Question"].ToString();
                    doc.Paragraphs[doc.Paragraphs.Count].Range.Font.Bold = 1;


                    tmp = myHash["Answers"].ToString().Split("|".ToCharArray());
                    Answ = tmp[0].Split("&".ToCharArray());
                    RightAnsw[i] = tmp[1];

                    k = 0;
                    foreach (string AnswStr in Answ)
                    {
                        doc.Paragraphs.Add();
                        doc.Paragraphs[doc.Paragraphs.Count].Range.Font.Bold = 0;
                        doc.Paragraphs[doc.Paragraphs.Count].Range.Text = ABCD[k].ToString() + ") " + AnswStr;
                        
                        //максимальное кол-во ответов:
                        if (k > MaxAnsw) MaxAnsw = k;

                        k++;
                    }


                    i++;

                    doc.Paragraphs.Add();
                    doc.Paragraphs.Add();

                }


                //вывод таблицы ответов

                int CountRow = QData.Count + 1;
                int CountCol = MaxAnsw + 2;

                if (QData.Count > 25)
                {
                    CountRow = (QData.Count / 2) + 1;
                    if ((QData.Count % 2) != 0) CountRow++;
                    CountCol = (MaxAnsw + 1) * 2 + 3;
                }

                doc.Paragraphs[doc.Paragraphs.Count].Range.Delete();
                doc.Paragraphs[doc.Paragraphs.Count].Range.InsertBreak();
                doc.Paragraphs[doc.Paragraphs.Count].Range.Text = "Ключи к тесту";
                doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc.Paragraphs[doc.Paragraphs.Count].Range.Font.Bold = 1;
                doc.Paragraphs.Add();
                doc.Paragraphs.Add();
                doc.Paragraphs[doc.Paragraphs.Count].Range.Font.Bold = 0;
                doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;


                doc.Tables.Add(doc.Paragraphs[doc.Paragraphs.Count].Range, CountRow, CountCol);

                doc.Tables[1].Rows[1].Cells[1].Range.Text = "№";
                if (QData.Count > 25) doc.Tables[1].Rows[1].Cells[MaxAnsw + 4].Range.Text = "№";

                doc.Tables[1].Rows[1].Range.Font.Bold = 1;


                for (i = 0; i <= MaxAnsw; i++)
                {
                    doc.Tables[1].Rows[1].Cells[2 + i].Range.Text = ABCD[i];
                    if (QData.Count > 25) doc.Tables[1].Rows[1].Cells[MaxAnsw + 5 + i].Range.Text = ABCD[i];
                }

                int j = 1;
                i = 2; k = 2;
                foreach (string str in RightAnsw)
                {

                    if (i > CountRow) { i = 2; k = MaxAnsw + 5; }

                    doc.Tables[1].Rows[i].Cells[k - 1].Range.Text = j.ToString();
                    doc.Tables[1].Rows[i].Cells[k + Convert.ToInt32(str)].Range.Text = "+";


                    i++; j++;
                }

                doc.Paragraphs.Add();
                doc.Paragraphs.Add();
                if (HeadO != null)
                {
                    doc.Paragraphs[doc.Paragraphs.Count].Range.Text = GetPostHead(HeadO["N_Post"].ToString(), HeadO["ID_Post"].ToString(), HeadO["NB_Otdel"].ToString()) + ((char)9).ToString() + HeadO["FIO"].ToString();
                    doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.TabStops.Add(oWord.CentimetersToPoints(12));

                    doc.Paragraphs.Add();
                    doc.Paragraphs.Add();
                    doc.Paragraphs[doc.Paragraphs.Count].Range.Text = GetPostHead(HeadO["UpHeadPost"].ToString(), "1", ((char)9).ToString() + HeadO["UpHeadFIO"].ToString());
                    doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.TabStops.Add(oWord.CentimetersToPoints(12));
                }

                //оформление:

                doc.Tables[1].PreferredWidthType = Word.WdPreferredWidthType.wdPreferredWidthPercent;
                if (QData.Count > 25) doc.Tables[1].PreferredWidth = 100;
                else doc.Tables[1].PreferredWidth = 50;


                doc.Tables[1].Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderHorizontal].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;


                doc.Tables[1].Select();
                oWord.Selection.SelectCell();

                oWord.Selection.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                oWord.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                oWord.Selection.Range.Paragraphs.SpaceAfter = 3;
                oWord.Selection.Range.Paragraphs.SpaceBefore = 3;

                if (QData.Count > 25)
                {
                    doc.Tables[1].Columns[MaxAnsw + 3].Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                    doc.Tables[1].Columns[MaxAnsw + 3].Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                    doc.Tables[1].Columns[MaxAnsw + 3].Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderHorizontal].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                }

                //колонтитул верхний
                oWord.ActiveWindow.View.SplitSpecial = Word.WdSpecialPane.wdPaneCurrentPageHeader;
                oWord.Selection.Range.Text = NameTest;
                oWord.Selection.Range.Font.Size = 10;
                oWord.Selection.Range.Font.Color = Word.WdColor.wdColorGray50;
                oWord.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                if (oWord.ActiveWindow.Panes.Count == 2) oWord.ActiveWindow.Panes[2].Close();

                //колонтитул нижний
                oWord.ActiveWindow.View.SplitSpecial = Word.WdSpecialPane.wdPaneCurrentPageFooter;
                oWord.Selection.Range.InsertBefore(HeadO["NB_Otdel"].ToString() + ((char)9).ToString() + ((char)9).ToString());
                oWord.Selection.EndKey();
                oWord.Selection.Fields.Add (oWord.Selection.Range, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                
                if (oWord.ActiveWindow.Panes.Count == 2 ) oWord.ActiveWindow.Panes[2].Close();

                doc.PageSetup.TopMargin = oWord.CentimetersToPoints(2);
                doc.PageSetup.BottomMargin = oWord.CentimetersToPoints(2);
                doc.PageSetup.LeftMargin = oWord.CentimetersToPoints(2);
                doc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

                doc.Paragraphs[1].Range.Select();
                oWord.Selection.MoveLeft(Microsoft.Office.Interop.Word.WdUnits.wdCharacter, 1);
                oWord.System.Cursor = Word.WdCursorType.wdCursorNormal;
                oWord.ScreenUpdating = true;
                oWord.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка: Не удалось получить необходимые данные из базы данных.\n\n {0}", ex.Message), "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                oWord.Visible = true;
                return;
            }

        }

        public void ExportSelectUserReport(Hashtable UserDetail)
        {
            Application.DoEvents();

            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();
            try
            {
                Conn.Open();
                cmd.CommandText = "SELECT TestName FROM TestWorkers WHERE ID_Rec = " + UserDetail["ID_Test"].ToString();
                OleDbDataReader result = cmd.ExecuteReader();
                result.Read();
                UserDetail.Add("TestName", result["TestName"].ToString());
                result.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка: Не удалось получить необходимые данные из базы данных.\n\n {0}", ex.Message), "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Conn.Close();
            }

            //OBJECT OF MISSING "NULL VALUE"
            Object oMissing = System.Reflection.Missing.Value;
            //OBJECTS OF FALSE AND TRUE
            Object oTrue = true;
            Object oFalse = false;
            //CREATING OBJECTS OF WORD AND DOCUMENT
            Word.Application oWord = new Word.Application();
            Word.Document oWordDoc = new Word.Document();

            //MAKING THE APPLICATION VISIBLE
            oWord.Visible = false;
            oWord.ScreenUpdating = false;
            oWord.System.Cursor = Word.WdCursorType.wdCursorWait;

            try
            {
                //ADDING A NEW DOCUMENT TO THE APPLICATION
                oWordDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                Microsoft.Office.Interop.Word.Document doc = oWord.ActiveDocument;

                doc.Paragraphs[1].Range.ParagraphFormat.LineSpacing = 12;
                doc.Paragraphs[1].Range.ParagraphFormat.SpaceAfter = 6;
                doc.Paragraphs[1].Range.Font.Bold = 1;
                doc.Paragraphs[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc.Paragraphs[1].Range.Text = UserDetail["TestName"].ToString();

                doc.Paragraphs.Add();
                doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.SpaceAfter = 6;
                doc.Paragraphs[doc.Paragraphs.Count].Range.Font.Bold = 0;
                doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                doc.Paragraphs[doc.Paragraphs.Count].Range.Text = "Фамилия, Имя, Отчество тестируемого: " + 
                                                                  UserDetail["FIO"].ToString();
                for (int i = 8; i <= 10; i++)
                {
                    doc.Paragraphs[doc.Paragraphs.Count].Range.Words[i].Font.Bold = 1;
                }

                doc.Paragraphs.Add();
                doc.Paragraphs[doc.Paragraphs.Count].Range.Text = "Должность, отдел: " +
                                                                  UserDetail["Post"].ToString() + ", " + 
                                                                  UserDetail["Otdel"].ToString();
                for (int i = 5; i <= doc.Paragraphs[doc.Paragraphs.Count].Range.Words.Count-1; i++)
                {
                    doc.Paragraphs[doc.Paragraphs.Count].Range.Words[i].Font.Bold = 1;
                }

                doc.Paragraphs.Add();
                doc.Paragraphs[doc.Paragraphs.Count].Range.Text = "Дата тестирования (чч.мм.гггг): " +
                                                                  Convert.ToDateTime(UserDetail["DateStart"]).ToShortDateString().ToString();
                for (int i = 10; i <= doc.Paragraphs[doc.Paragraphs.Count].Range.Words.Count - 1; i++)
                {
                    doc.Paragraphs[doc.Paragraphs.Count].Range.Words[i].Font.Bold = 1;
                }

                doc.Paragraphs.Add();
                doc.Paragraphs[doc.Paragraphs.Count].Range.Text = "Время начала тестирования (чч:мм): " +
                                                                  Convert.ToDateTime(UserDetail["DateStart"]).ToShortTimeString().ToString();
                for (int i = 9; i <= doc.Paragraphs[doc.Paragraphs.Count].Range.Words.Count - 1; i++)
                {
                    doc.Paragraphs[doc.Paragraphs.Count].Range.Words[i].Font.Bold = 1;
                }

                doc.Paragraphs.Add();
                doc.Paragraphs[doc.Paragraphs.Count].Range.Text = "Время окончания тестирования (чч:мм): " +
                                                                  Convert.ToDateTime(UserDetail["DateEnd"]).ToShortTimeString().ToString();
                for (int i = 9; i <= doc.Paragraphs[doc.Paragraphs.Count].Range.Words.Count - 1; i++)
                {
                    doc.Paragraphs[doc.Paragraphs.Count].Range.Words[i].Font.Bold = 1;
                }

                doc.Paragraphs.Add();
                doc.Paragraphs[doc.Paragraphs.Count].Range.Text = "Результаты тестирования:";
                doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.SpaceAfter = 12;

                //массив ответов
                string[] ArrAnsw = UserDetail["Report"].ToString().Split("|".ToCharArray());

                int CountCol = 1;
                int CountRow = ArrAnsw.Length;

                if (ArrAnsw.Length - 1 > 25) { 
                    CountCol = 2;
                    CountRow = (ArrAnsw.Length-1) / 2;
                }

                doc.Paragraphs.Add();
                doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.SpaceAfter = 3;
                doc.Tables.Add(doc.Paragraphs[doc.Paragraphs.Count].Range, 1, CountCol);
                doc.Tables[1].PreferredWidthType = Word.WdPreferredWidthType.wdPreferredWidthPercent;
                doc.Tables[1].PreferredWidth = 100;
                doc.Tables[1].Rows[1].Cells[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                doc.Tables[1].Rows[1].Cells[1].Range.ParagraphFormat.LineSpacing = 12;

                string outstr = "";
                int k = 1; int selCol = 1;
                foreach (string str in ArrAnsw){
                    if (str != "")
                    {
                        if (CountRow >= 25 && k > CountRow && selCol != 2)
                        {
                            doc.Tables[1].Rows[1].Cells[selCol].Range.Text = outstr;
                            selCol = 2;
                            outstr = "";
                            doc.Tables[1].Rows[1].Cells[selCol].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        }

                        if (outstr != "") outstr += "\n";
                        outstr += "№" + k.ToString() + ". " + str;

                        k++;
                    }
                }

                doc.Tables[1].Rows[1].Cells[selCol].Range.Text = outstr;

                doc.Paragraphs.Add();
                doc.Paragraphs[doc.Paragraphs.Count].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                doc.Paragraphs[doc.Paragraphs.Count].Range.Text = "Всего вопросов в тесте: " + UserDetail["CountQuestions"].ToString();
                doc.Paragraphs.Add();
                doc.Paragraphs[doc.Paragraphs.Count].Range.Text = "Всего верных ответов: " + UserDetail["RightAnswers"].ToString()
                                                                + "(" + UserDetail["Percent"].ToString() + "%)";

                doc.Paragraphs.Add();
                doc.Paragraphs.Add();
                doc.Paragraphs.Add();
                doc.Tables.Add (doc.Paragraphs[doc.Paragraphs.Count].Range, 2, 5);
                doc.Tables[2].Columns[1].Width = 150;
                doc.Tables[2].Columns[2].Width = 35;
                doc.Tables[2].Columns[3].Width = 120;
                doc.Tables[2].Columns[4].Width = 35;
                doc.Tables[2].Columns[5].Width = 138;

                doc.Tables[2].Rows[1].Range.ParagraphFormat.SpaceBefore = 0;
                doc.Tables[2].Rows[1].Range.ParagraphFormat.SpaceAfter = 0;

                doc.Tables[2].Cell(1, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[2].Cell(1, 3).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[2].Cell(1, 5).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                doc.Tables[2].Cell(2, 1).Range.Text = "наименование должности";
                doc.Tables[2].Cell(2, 3).Range.Text = "подпись";
                doc.Tables[2].Cell(2, 5).Range.Text = "расшифровка подписи";
 
                doc.Tables[2].Rows[2].Range.Font.Size = 8;
                doc.Tables[2].Rows[2].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                doc.Paragraphs[1].Range.Select();
                oWord.Selection.MoveLeft(Microsoft.Office.Interop.Word.WdUnits.wdCharacter, 1);
                oWord.System.Cursor = Word.WdCursorType.wdCursorNormal;
                oWord.ScreenUpdating = true;
                oWord.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка: Не удалось получить необходимые данные из базы данных.\n\n {0}", ex.Message), "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                oWord.ScreenUpdating = true;
                oWord.Visible = true;
                return;
            }

        }

        private string LoadAnswers(string ID_Test, string ID_Quest)
        {
            string Answers = "";
            string RightAnswers = "";
            int i = 0;

            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);

            try
            {

                string cmdsql = string.Format("SELECT TOP 8 Answer, RightAnswer FROM TestWorkersAnsw WHERE ID_Test = {0} AND ID_Quest ={1} ORDER BY Ord", ID_Test, ID_Quest);

                DataSet myDataSet = new DataSet();

                OleDbCommand myAccessCommand = new OleDbCommand(cmdsql, Conn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                Conn.Open();
                myDataAdapter.Fill(myDataSet, "ListDoc");

                DataRowCollection dra = myDataSet.Tables["ListDoc"].Rows;

                foreach (DataRow dr in dra)
                {
                    if (Answers != "") Answers += "&";
                    Answers += CheckAnsw(dr[0].ToString().Trim());

                    if (dr[1].ToString() == "1")
                    {
                        if (RightAnswers != "") RightAnswers += ";";
                        RightAnswers += i.ToString();
                    }
                    
                    i++;
                }

                Answers += "|" + RightAnswers;


            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка: Не удалось получить необходимые данные из базы данных.\n\n {0}", ex.Message), "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.StackTrace);
                return "";
            }
            finally
            {
                Conn.Close();
            }

            return Answers;

        }

        private Hashtable HeadOtdel(string ID_Test)
        {
            string IDO;
            string ConnString = ClassParams.ConnString;
            OleDbConnection Conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = Conn.CreateCommand();

            try
            {

                Conn.Open();

                cmd.CommandText = "SELECT ID_Otdel FROM TestWorkers WHERE ID_Rec = " + ID_Test;
                OleDbDataReader result = cmd.ExecuteReader();
                result.Read();
                IDO = result["ID_Otdel"].ToString();
                result.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка: Не удалось получить необходимые данные из базы данных.\n\n {0}", ex.Message), "Остановка операции", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
            finally
            {
                Conn.Close();
            }

            if (IDO == "") return null;

            ClassDB ClsDB = new ClassDB();

            string SqlStr = "SELECT dbo.Posts.N_Post, dbo.Otdels.NB_Otdel, dbo.Workers.I_Worker + ' ' + dbo.Workers.F_Worker AS FIO, dbo.Posts.ID_Post, dbo.Otdels.IndOtdel " + 
                           " FROM dbo.Otdels INNER JOIN " +
                           " dbo.Posts ON dbo.Otdels.PostHeadUnit = dbo.Posts.ID_Post INNER JOIN " +
                           "dbo.Workers ON dbo.Posts.ID_Post = dbo.Workers.ID_Post AND dbo.Otdels.ID_Otdel = dbo.Workers.ID_Otdel " +
                           " WHERE (dbo.Otdels.ID_Otdel = " + IDO + ")";

            Dictionary<int, Hashtable> myData = ClsDB.GET_Fields(SqlStr);

            if (myData.Count == 0) return null;

            //вышестоящий руководитель:
            string IndOtdel = myData[1]["IndOtdel"].ToString();
            if (IndOtdel != "")
            {
                IndOtdel = IndOtdel.Substring(0, 1);
                SqlStr = "SELECT dbo.Posts.N_Post, dbo.Workers.I_Worker + ' ' + dbo.Workers.F_Worker AS FIO " +
                               " FROM dbo.Posts INNER JOIN " +
                               "dbo.Workers ON dbo.Posts.ID_Post = dbo.Workers.ID_Post " +
                               " WHERE (dbo.Workers.PersonIndexDP = '" + IndOtdel + "')";

                Dictionary<int, Hashtable> tmp = ClsDB.GET_Fields(SqlStr);

                if (tmp.Count > 0)
                {
                    myData[1].Add("UpHeadPost", tmp[1]["N_Post"]);
                    myData[1].Add("UpHeadFIO", tmp[1]["FIO"]);
                }
            }

            //myData[1].Add("IDO", IDO);

            return myData[1];

        }

        private string GetPostHead(string N_Post, string ID_Post, string NB_Otdel)
        {
            string str = N_Post + " " + NB_Otdel;
            if (ID_Post == "2") str = "Начальник " + NB_Otdel;
            if (ID_Post == "54") str = N_Post;

            return str;
        }

    }
}
