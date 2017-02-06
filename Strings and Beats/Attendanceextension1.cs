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
    public partial class Attendanceextension1 : MetroForm
    {
        string rowid;
        static string connectionString = Connection.getConnection();
        SQLiteConnection connection = new SQLiteConnection(connectionString);
        Boolean alreadyexisting = false;
        string studentid;
        public Attendanceextension1(string rowid,string fullname,string studentid)
        {
            InitializeComponent();
            connection.Open();
            this.studentid=studentid;
            this.rowid = rowid;
            this.fullnamelbl.Text = fullname;
            compensatedatedt.Value = DateTime.Now.AddDays(1);
            
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("select Status,Reason,strftime('%d-%m-%Y',Compensatedate),Informed from AttendancerecordTbl where rowid='" + rowid + "'", connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows[0][0].ToString() == "Absent") 
            {
                informedgrp.Visible = true;
                informedlbl.Visible = true; 
                absent.Checked = true;
                if (dt.Rows[0][3].ToString() == "Yes")
                {
                    yes.Checked = true;
                    adapter = new SQLiteDataAdapter("select count(rowid)  from  CompensateTbl  where Studentid='" + studentid + "'", connection);
                    dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {

                        adapter = new SQLiteDataAdapter("select  Compensatedate,Reason from  CompensateTbl  where Studentid='" + studentid + "'", connection);
                        dt = new DataTable();
                        adapter.Fill(dt);
                        DateTime compensatedate = Convert.ToDateTime(dt.Rows[0][0].ToString());
                        if (DateTime.Now.Date < compensatedate.Date)
                        {
                            compensatelbl.Visible = true;
                            compensatedatedt.Visible = true;
                            reasonlbl.Visible = true;
                            reasontxtbox.Visible = true;
                           
                            compensatedatedt.Value = new DateTime(compensatedate.Year, compensatedate.Month, compensatedate.Day);
                          
                           
                            reasontxtbox.Text = dt.Rows[0][1].ToString();
                            alreadyexisting = true;
                        }
                    }
                }
                else if (dt.Rows[0][3].ToString() == "No")
                {
                    no.Checked = true;
                    compensatelbl.Visible = false;
                    compensatedatedt.Visible = false;
                    reasonlbl.Visible = false;
                    reasontxtbox.Visible = false;
                }

            }
          
           
        }

        private void female_CheckedChanged(object sender, EventArgs e)
        {
            informedgrp.Visible = false;
            informedlbl.Visible = false;
            reasonlbl.Visible = false;
            reasontxtbox.Visible = false;
            compensatedatedt.Visible = false;
            compensatelbl.Visible = false;
        }

        private void absent_CheckedChanged(object sender, EventArgs e)
        {
            informedgrp.Visible = true;
            informedlbl.Visible = true;
            no.Checked = true;

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string camelCase(string text) { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text); }

        private void save_Click(object sender, EventArgs e)
        {
           
            if (yes.Checked)
            {
                if (isValid())
                {
                    if (reasontxtbox.Text == "") {
                        MetroMessageBox.Show(this, "Enter Reason for taking leave..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        reasontxtbox.Focus() ;
                    }
                    else 
                    { 
                        saver();
                    }
                }
                else
                {
                    MetroMessageBox.Show(this, "Date of joining not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);



                    compensatedatedt.Value = DateTime.Now.AddDays(1);

                }
            }
            else
            {
                saver();
            }
          
        }

        private Boolean isValid(){
            if (compensatedatedt.Value.Date <= DateTime.Now.Date)
                {
             
                    return false;
                }
            
            else
            {

              return true;



            }
          
        }

        private void saver() {
            try
            {
                SQLiteDataAdapter sda2 = new SQLiteDataAdapter();
                SQLiteDataAdapter g = new SQLiteDataAdapter();
                sda2.UpdateCommand = new SQLiteCommand("update AttendancerecordTbl SET Status=@status,Reason=@reason,Compensatedate=@compensatedate,Informed=@informed where rowid='" + rowid + "'", connection);
                if (present.Checked)
                {
                    sda2.UpdateCommand.Parameters.Add("@status", DbType.String).Value = "Present";
                    sda2.UpdateCommand.Parameters.Add("@reason", DbType.String).Value = "";
                    sda2.UpdateCommand.Parameters.Add("@Compensatedate", DbType.DateTime).Value = DBNull.Value;
                    sda2.UpdateCommand.Parameters.Add("@informed", DbType.String).Value = "";
                    if (alreadyexisting) {
                        
                        g.DeleteCommand = new SQLiteCommand("delete from CompensateTbl where Studentid='" + studentid + "'", connection);
                        g.DeleteCommand.ExecuteNonQuery();
                                          
                    }
                }
                else
                {
                    if (yes.Checked)
                    {
                        sda2.UpdateCommand.Parameters.Add("@status", DbType.String).Value = "Absent";
                        sda2.UpdateCommand.Parameters.Add("@reason", DbType.String).Value = camelCase(reasontxtbox.Text);
                        sda2.UpdateCommand.Parameters.Add("@Compensatedate", DbType.DateTime).Value = new DateTime(compensatedatedt.Value.Year, compensatedatedt.Value.Month, compensatedatedt.Value.Day);
                        sda2.UpdateCommand.Parameters.Add("@informed", DbType.String).Value = "Yes";
                        SQLiteDataAdapter sda3 = new SQLiteDataAdapter();
                           
                        if (alreadyexisting) {
                            sda3.UpdateCommand = new SQLiteCommand("update CompensateTbl SET  Compensatedate=@compensatedate,Reason=@reason where Studentid='" + studentid + "';", connection);
                            sda3.UpdateCommand.Parameters.Add("@reason", DbType.String).Value = camelCase(reasontxtbox.Text);
                            sda3.UpdateCommand.Parameters.Add("@Compensatedate", DbType.DateTime).Value = new DateTime(compensatedatedt.Value.Year, compensatedatedt.Value.Month, compensatedatedt.Value.Day);
                            sda3.UpdateCommand.ExecuteNonQuery();
                        } 
                        else {
                          sda3.InsertCommand = new SQLiteCommand("insert into CompensateTbl values('"+studentid+"',@compensatedate,@reason);", connection);
                          sda3.InsertCommand.Parameters.Add("@reason", DbType.String).Value = camelCase(reasontxtbox.Text);
                          sda3.InsertCommand.Parameters.Add("@Compensatedate", DbType.DateTime).Value = new DateTime(compensatedatedt.Value.Year, compensatedatedt.Value.Month, compensatedatedt.Value.Day);
                          sda3.InsertCommand.ExecuteNonQuery();
                        }
                       
               

                    }

                    else if (no.Checked)
                    {
                        sda2.UpdateCommand.Parameters.Add("@status", DbType.String).Value = "Absent";
                        sda2.UpdateCommand.Parameters.Add("@reason", DbType.String).Value = "";
                        sda2.UpdateCommand.Parameters.Add("@Compensatedate", DbType.DateTime).Value = DBNull.Value;
                        sda2.UpdateCommand.Parameters.Add("@informed", DbType.String).Value = "No";
                        if (alreadyexisting)
                        {

                            g.DeleteCommand = new SQLiteCommand("delete from CompensateTbl where Studentid='" + studentid + "'", connection);
                            g.DeleteCommand.ExecuteNonQuery();

                        }
                    }
                }
                sda2.UpdateCommand.ExecuteNonQuery();
                string presentcount, absentcount;
                sda2 = new SQLiteDataAdapter("select Attendanceid from AttendancerecordTbl where rowid='" + rowid + "'", connection);
                DataTable dt = new DataTable();
                sda2.Fill(dt);
                string attendanceid = dt.Rows[0][0].ToString();
                sda2 = new SQLiteDataAdapter("select count(*) from AttendancerecordTbl where Attendanceid='" + attendanceid + "' and Status='Present'", connection);
                dt = new DataTable();
                sda2.Fill(dt);
                presentcount = dt.Rows[0][0].ToString();
                sda2 = new SQLiteDataAdapter("select count(*) from AttendancerecordTbl where Attendanceid='" + attendanceid + "' and Status='Absent'", connection);
                dt = new DataTable();
                sda2.Fill(dt);
                absentcount = dt.Rows[0][0].ToString();
                sda2.UpdateCommand = new SQLiteCommand("update AttendanceTbl  SET Present='" + presentcount + "',Absent='" + absentcount + "'  where rowid='" + attendanceid + "'", connection);
                sda2.UpdateCommand.ExecuteNonQuery();
                MetroMessageBox.Show(this,"Attendance saved..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            }

        private void yes_CheckedChanged(object sender, EventArgs e)
        {
            compensatelbl.Visible = true;
            compensatedatedt.Visible = true;
            reasontxtbox.Visible = true;
            reasonlbl.Visible = true;
        }

        private void no_CheckedChanged(object sender, EventArgs e)
        {
            compensatelbl.Visible = false;
            compensatedatedt.Visible = false ;
            reasontxtbox.Visible = false;
            reasonlbl.Visible = false;
        }

        private void Attendanceextension_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }
    }
}
