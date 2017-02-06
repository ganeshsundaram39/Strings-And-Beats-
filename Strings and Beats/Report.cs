using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strings_and_Beats
{
    public partial class Report : MetroForm
    {
        static string connectionString = Connection.getConnection();
        SQLiteConnection connection = new SQLiteConnection(connectionString);
        private static Dictionary<string, string> studentlist = new Dictionary<string, string> { };

        public Report()
        {
        InitializeComponent();
        try
        {
        connection.Open();
        askpanel.Visible = true;

        numericyear.Minimum = 2000;
        numericyear.Maximum = Convert.ToInt32(DateTime.Now.Year);
        numericyear.Value = Convert.ToInt32(DateTime.Now.Year);

        studentnametxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        studentnametxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
     

        SQLiteCommand s = new SQLiteCommand("select rowid,Fullname from StudentTbl", connection);
        SQLiteDataReader reader = s.ExecuteReader();
            studentlist.Clear();
        
        while (reader.Read())
        {
            coll.Add(reader.GetString(reader.GetOrdinal("Fullname"))); 
          
            studentlist.Add(reader.GetString(reader.GetOrdinal("Fullname")), reader.GetInt32(reader.GetOrdinal("rowid")).ToString());
        }

        studentnametxt.AutoCompleteCustomSource = coll;

        }
        catch (Exception ex)
        {

        MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
         
        }

        private void next_Click(object sender, EventArgs e)
        {
            try
            {
                if (studentnametxt.Text == "") 
                {
                    MetroMessageBox.Show(this, "Select student name..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    studentnametxt.Focus();
                }
                else
                {
                    SQLiteDataAdapter sda2 = new SQLiteDataAdapter("select count(rowid)  from StudentTbl where Fullname='" + camelCase(studentnametxt.Text) + "'", connection);
                    DataTable dg = new DataTable();
                    sda2.Fill(dg);

                    if (Convert.ToInt32(dg.Rows[0][0]) > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        askpanel.Visible = true;
                        reportpanel.Visible = true;
                        reportpanel.BringToFront();
                        sda2 = new SQLiteDataAdapter("select Grade  from StudentTbl where rowid='" + studentlist[camelCase( studentnametxt.Text)] + "'", connection);
                        dg = new DataTable();
                        sda2.Fill(dg);
                        gradelbl.Text=camelCase(dg.Rows[0][0].ToString());
                      
                        ChartValues<double> present = new ChartValues<double>();
                        ChartValues<double> absent = new ChartValues<double>();

                        for (int month = 1; month <= 12; month++)
                        {
                            string date1;
                            if (month < 10) { date1 = numericyear.Value + "-0" + month; } else { date1 = numericyear.Value + "-" + month; }

                            sda2 = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Status='Present' and Date LIKE '" + date1 + "%' and Studentid='" + studentlist[camelCase(studentnametxt.Text)] + "';", connection);
                            dg = new DataTable();
                            sda2.Fill(dg);

                            present.Add(Convert.ToDouble(dg.Rows[0][0]));
                            sda2 = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Status='Absent' and Date LIKE '" + date1 + "%'  and Studentid='" + studentlist[camelCase(studentnametxt.Text)] + "';", connection);
                            dg = new DataTable();
                            sda2.Fill(dg);
                            absent.Add(Convert.ToDouble(dg.Rows[0][0]));

                        }
                        cartesianChart1.Series.Clear();
                        cartesianChart1.AxisX.Clear();
                        cartesianChart1.AxisY.Clear();
                       cartesianChart1.Series = new SeriesCollection
                        {
                            new ColumnSeries
                            {
                                Title ="Present",
                                Values =present
                            }
                        };

                        //adding series will update and animate the chart automatically
                        cartesianChart1.Series.Add(new ColumnSeries
                        {
                            Title = "Absent",
                            Values = absent
                        });

                      

                        cartesianChart1.AxisX.Add(new Axis
                        {
                            Title = "Year " + numericyear.Value,
                            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" }
                        });

                        cartesianChart1.AxisY.Add(new Axis
                        {
                            Title = "Number of days",
                            LabelFormatter = value => value.ToString("N")
                        });

                       
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "Student not found..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private string camelCase(string text) { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text); }
        
        private void Report_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
                 }

        private void studentnametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsLetter(ch) && ch != 8 && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;

            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            askpanel.Visible = true;
            reportpanel.Visible = false; 
        }

    }
}
