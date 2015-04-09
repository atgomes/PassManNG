using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;
using System.Data.SQLite;

namespace PassManNG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void CreateDB()
        {
            if(!File.Exists("databaseFile.db3"))
            {
                string createTableQuery1 = @"CREATE TABLE IF NOT EXISTS [MyTable] ([ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [dKey] NVARCHAR(2048)  NOT NULL, [ePassword] NVARCHAR(2048)  NOT NULL, [username] NVARCHAR(2048)  NULL, [notes] NVARCHAR(2048)  NULL)";

                string createTableQuery2 = @"CREATE TABLE IF NOT EXISTS [Profiles] ([ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [dKey] NVARCHAR(2048) NOT NULL, [ePassword] NVARCHAR (2048) NOT NULL, [username] NVARCHAR(2048) NOT NULL, [notes] NVARCHAR(2048) NOT NULL)";


                SQLiteConnection.CreateFile(Properties.Settings.Default.PassDBFile);

                //SQLiteConnection.

                SQLiteConnection conn = new SQLiteConnection(Properties.Settings.Default.PassDatabase);
                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand(conn);
                cmd.CommandText = createTableQuery1;
                cmd.ExecuteNonQuery();

                cmd.CommandText = createTableQuery2;
                cmd.ExecuteNonQuery();

                //cmd.CommandText = "INSERT INTO MyTable (Key,Value) Values ('key one','value one')";
                //cmd.ExecuteNonQuery();
                //cmd.CommandText = "INSERT INTO MyTable (Key,Value) Values ('testea','testeb')";
                //cmd.ExecuteNonQuery();

                //cmd.CommandText = "Select * FROM MyTable";

                //SQLiteDataReader reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //   keytextbox.Text = reader["Key"] + " : " + reader["Value"];
                //}
                //reader.Close();

                conn.Close();
            }
        }
    }
}