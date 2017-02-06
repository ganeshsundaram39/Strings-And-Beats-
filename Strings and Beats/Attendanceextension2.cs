using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strings_and_Beats
{
    public partial class Attendanceextension2 : MetroForm
    {
        static string connectionString = Connection.getConnection();
        SQLiteConnection connection = new SQLiteConnection(connectionString);

        public Attendanceextension2(string rowid, string date, string batch, string timing, string instructor)
        {
            InitializeComponent();
            connection.Open();
            this.Text = date;
            batchlbl.Text = batch;
            timinglbl.Text = timing;
            instructorlbl.Text = instructor;
          


            gridview.ColumnCount = 5;
            gridview.Columns[0].Name = "Name";
            gridview.Columns[1].Name = "Status";
            gridview.Columns[2].Name = "Reason";
            gridview.Columns[3].Name = "Informed";
            gridview.Columns[4].Name = "Compensate";

            SQLiteCommand s = new SQLiteCommand("select Fullname as Name,Status,Reason,Informed,Compensatedate as Compensate from AttendancerecordTbl where Attendanceid='" + rowid + "'", connection);

            using (SQLiteDataReader reader = s.ExecuteReader())
            {
                while (reader.Read())
                {
                    gridview.Rows.Add(new object[] 
                        { 
                          reader.GetValue(reader.GetOrdinal("Name")),  
                          reader.GetValue(reader.GetOrdinal("Status")),  
                          reader.GetValue(reader.GetOrdinal("Reason")),  
                          reader.GetValue(reader.GetOrdinal("Informed")),  
                       !reader.IsDBNull(reader.GetOrdinal("Compensate"))?dateReturner(reader.GetString(reader.GetOrdinal("Compensate"))):""
                       });
                }
            }
        }
        private string dateReturner(string date){
            if (date == "")
            {
                return "";
            }
            else {
                return Convert.ToDateTime(date).ToString("dd-MM-yyyy");
            }
        }
        private void Attendanceextension2_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }


    }
}
