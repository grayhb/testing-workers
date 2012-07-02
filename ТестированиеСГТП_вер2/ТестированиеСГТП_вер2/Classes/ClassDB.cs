using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

namespace ТестированиеСГТП_вер2
{

    class ClassDB
    {



        /* альтернатива */
        public Dictionary<int, Hashtable> GET_Fields(string sqlq)
        {
            var result = new Dictionary<int, Hashtable>();
            int countrow = 0;
            Hashtable tmp;

            SqlConnection myConnection = new SqlConnection(ClassParams.ConString);

            try
            {
                myConnection.Open();

                try
                {
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand(sqlq, myConnection);
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        countrow++;
                        tmp = new Hashtable();
                        for (int i = 0; i <= myReader.FieldCount - 1; i++)
                        {
                            tmp[myReader.GetName(i)] = myReader[i].ToString();
                        }

                        result.Add(countrow, tmp);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }


                try
                {
                    myConnection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return result;
        }


        /* Функция GET_Field
         * Запрос на возврат одного поля
         * @table - имя таблицы
         * @where - условие запроса
         * @field - поле для выбора
         * Возвращает строку
         * */
        public string GET_Field(string table, string where, string field)
        {
            string result = "";

            SqlConnection myConnection = new SqlConnection(ClassParams.ConString);

            try
            {
                myConnection.Open();

                try
                {
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand("SELECT " + field + " as getfield FROM " + table + " WHERE " + where, myConnection);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        result = myReader["getfield"].ToString();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }


                try
                {
                    myConnection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            return result;
        }
        //--- end GET_Field

        /* Процедура ReadCFG
         * Считывание конфигурации из SSPD.cfg
         */
        public void ReadCFG()
        {
            string path = "w:\\sspd.cfg";
            string StrPar = "";

            int LenStrPar = 0;
            int BegPos = 0;

            byte[] fileData; //массив 

            using (System.IO.FileStream fs = System.IO.File.OpenRead(path))
            {
                fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
            }

            BegPos = 0;
            LenStrPar = 90 - fileData[fileData.Length - 1] - 1;
            StrPar = ret(fileData, BegPos, LenStrPar, ClassParams.Mask);
            ClassParams.ServerSQL.DataSource = StrPar;

            BegPos += LenStrPar + 1;

            LenStrPar = 90 - fileData[BegPos] - 1;
            BegPos += 1;
            StrPar = ret(fileData, BegPos, LenStrPar, ClassParams.Mask);
            ClassParams.ServerSQL.database = StrPar;

            BegPos += LenStrPar + 1;

            LenStrPar = 90 - fileData[BegPos] - 1;
            BegPos += 1;
            StrPar = ret(fileData, BegPos, LenStrPar, ClassParams.Mask);
            ClassParams.ServerSQL.SERVERProvider = StrPar;

            BegPos += LenStrPar + 1;

            LenStrPar = 90 - fileData[BegPos] - 1;
            BegPos += 1;
            StrPar = ret(fileData, BegPos, LenStrPar, ClassParams.Mask);
            ClassParams.ServerSQL.uid = StrPar;

            BegPos += LenStrPar + 1;

            LenStrPar = 90 - fileData[BegPos] - 1;
            BegPos += 1;
            StrPar = ret(fileData, BegPos, LenStrPar, ClassParams.Mask);
            ClassParams.ServerSQL.pwd = StrPar;

            BegPos += LenStrPar + 1;

            LenStrPar = 90 - fileData[BegPos] - 1;
            BegPos += 1;
            StrPar = ret(fileData, BegPos, LenStrPar, ClassParams.Mask);
            ClassParams.ServerFTP.Adress = StrPar;

            BegPos += LenStrPar + 1;

            LenStrPar = 90 - fileData[BegPos] - 1;
            BegPos += 1;
            StrPar = ret(fileData, BegPos, LenStrPar, ClassParams.Mask);
            ClassParams.ServerFTP.Port = StrPar;

            BegPos += LenStrPar + 1;

            LenStrPar = 90 - fileData[BegPos] - 1;
            BegPos += 1;
            StrPar = ret(fileData, BegPos, LenStrPar, ClassParams.Mask);
            ClassParams.ServerFTP.UserNameWrite = StrPar;

            BegPos += LenStrPar + 1;

            LenStrPar = 90 - fileData[BegPos] - 1;
            BegPos += 1;
            StrPar = ret(fileData, BegPos, LenStrPar, ClassParams.Mask);
            ClassParams.ServerFTP.PasswordWrite = StrPar;

            BegPos += LenStrPar + 1;

            LenStrPar = 90 - fileData[BegPos] - 1;
            BegPos += 1;
            StrPar = ret(fileData, BegPos, LenStrPar, ClassParams.Mask);
            ClassParams.ServerFTP.UserNameRead = StrPar;

            BegPos += LenStrPar + 1;

            LenStrPar = 90 - fileData[BegPos] - 1;
            BegPos += 1;
            StrPar = ret(fileData, BegPos, LenStrPar, ClassParams.Mask);
            ClassParams.ServerFTP.PasswordRead = StrPar;

            /*
            Console.WriteLine(ClassParams.ServerSQL.database);
            Console.WriteLine(ClassParams.ServerSQL.DataSource);
            Console.WriteLine(ClassParams.ServerSQL.SERVERProvider);
            Console.WriteLine(ClassParams.ServerSQL.uid);
            Console.WriteLine(ClassParams.ServerSQL.pwd);

            Console.WriteLine(ClassParams.ServerFTP.Adress);
            Console.WriteLine(ClassParams.ServerFTP.Port);
            Console.WriteLine(ClassParams.ServerFTP.UserNameRead);
            Console.WriteLine(ClassParams.ServerFTP.PasswordRead);
            Console.WriteLine(ClassParams.ServerFTP.UserNameWrite);
            Console.WriteLine(ClassParams.ServerFTP.PasswordWrite);
            */

            /* ClassParams.ConString = "Provider=" + ClassParams.ServerSQL.SERVERProvider + ";Data Source=" + ClassParams.ServerSQL.DataSource
                                    + ";database=" + ClassParams.ServerSQL.database + ";User Id=" + ClassParams.ServerSQL.uid + ";Password=" 
                                    + ClassParams.ServerSQL.pwd + ";"; */
            ClassParams.ConString = "Data Source=" + ClassParams.ServerSQL.DataSource
                                    + ";database=" + ClassParams.ServerSQL.database + ";User Id=" + ClassParams.ServerSQL.uid + ";Password="
                                    + ClassParams.ServerSQL.pwd + ";";


        }
        //--- end ReadCFG

        /* Функция ret
         * Расшифровка параметров конфигурации
         */
        public string ret(byte[] fileData, int BegPos, int LenParam, string Psw)
        {
            string Res = "";
            Int32 yy;

            // создаем цикличный ключ
            while (Psw.Length <= LenParam)
            {
                Psw += Psw;
            }

            ASCIIEncoding ascii = new ASCIIEncoding();

            byte[] PswByte = ascii.GetBytes(Psw);
            byte[] tmpbyte = new byte[LenParam + 1];

            for (int i = 0; i <= LenParam; i++)
            {
                yy = (fileData[i + BegPos] + 256) - PswByte[i];
                tmpbyte[i] = Convert.ToByte(yy % 256);
            }

            Res = ascii.GetString(tmpbyte);

            return (Res);
        }
        //--- end ret


        /* Функция Code
         * Шифрование параметров конфигурации
         */
        public string Code()
        {



            return (null);
        }
        //--- end Code

    }

    static class ClassParams
    {
        //настройки локальной бд
        public static string ConnString = @"provider=Microsoft.Jet.OLEDB.4.0;data source=//10.105.21.69/test/DB.mde;Jet OLEDB:Database Password=gfhjkzytn;";
        //public static string ConnString = @"provider=Microsoft.Jet.OLEDB.4.0;data source=D://test_DB.mde;Jet OLEDB:Database Password=gfhjkzytn;";

        public static string Mask = "dgk";      //маска

        public static string ConString;         //строка подключения к БД

        //public static int ID_Otdel;          //отдел 

        public static SQLS ServerSQL;

        public struct SQLS
        {
            public string DataSource;
            public string database;
            public string SERVERProvider;
            public string uid;
            public string pwd;
        }



        public static SFTP ServerFTP;

        public struct SFTP
        {
            public string Adress;
            public string Port;
            public string UserNameWrite;
            public string PasswordWrite;
            public string UserNameRead;
            public string PasswordRead;
        }


    }
}
