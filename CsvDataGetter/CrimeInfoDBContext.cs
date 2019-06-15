using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace CsvDataGetter
{
    public class CrimeInfoDBContext
    {
        public void OpenConnection()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string filepath = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\ksr.db";
            SQLiteConnection myConnection = new SQLiteConnection($"Data Source={filepath};Version=3;");
            myConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = myConnection;
            cmd.CommandText = "Select * from ksr";
            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                DataTable dt = new DataTable();
                dt.Load(sdr);
                sdr.Close();
                myConnection.Close();
            }
        }
    }
}
