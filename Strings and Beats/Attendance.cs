using iTalk;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections;
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
    public partial class Attendance : MetroForm
    {
        static string connectionString = Connection.getConnection();
        SQLiteConnection connection = new SQLiteConnection(connectionString);
        int count = 0;

        public Attendance()
        {
            InitializeComponent();
            connection.Open();
            tabcontrol1.SelectedTab = Todayattendance;
            numericyear.Minimum = 2000;
            numericyear.Maximum = Convert.ToInt32(DateTime.Now.Year);
            numericyear.Value = Convert.ToInt32(DateTime.Now.Year);

            tabcontrol2.SelectedIndex = DateTime.Now.Month - 1;
            todaydate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            SQLiteDataAdapter sda2 = new SQLiteDataAdapter("select count(rowid) from StudentTbl  ", connection);
            DataTable dg = new DataTable();
            sda2.Fill(dg);
            count = Convert.ToInt32(dg.Rows[0][0]);
            if (count > 0)
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter("select DISTINCT Instructorname from StudentTbl", connection);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    instructorcombo.Items.Add(row["Instructorname"].ToString());

                }
                sda = new SQLiteDataAdapter("select DISTINCT Batch from StudentTbl", connection);
                dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {

                    batchcombo.Items.Add(row["Batch"].ToString());
                }
                sda = new SQLiteDataAdapter("select DISTINCT Timing from StudentTbl", connection);
                dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    timingcombo.Items.Add(row["Timing"].ToString());
                }


                instructorcombo.SelectedIndex = 0;
                batchcombo.SelectedIndex = 0;
                timingcombo.SelectedIndex = 0;
            }

              

            
        }

        private void generateMonthly(int year)
        {
            try
            {
                SQLiteDataAdapter sda2;
                DataTable dg;
                MetroGrid[] monthgrid = { gridview1, gridview2, gridview3, gridview4, gridview5, gridview6, gridview7, gridview8, gridview9, gridview10, gridview11, gridview12 };
                iTalk_NotificationNumber[] notification = { jannot, febnot, marnot, aprnot, maynot, junnot, julnot, augnot, sepnot, octnot, novnot, decnot };

                for (int addcolumn = 0; addcolumn < 12; addcolumn++)
                {
                    monthgrid[addcolumn].ColumnCount = 8;
                    monthgrid[addcolumn].Columns[0].Name = "rowid";
                    monthgrid[addcolumn].Columns[1].Name = "Date";
                    monthgrid[addcolumn].Columns[2].Name = "Batch";
                    monthgrid[addcolumn].Columns[3].Name = "Timing";
                    monthgrid[addcolumn].Columns[4].Name = "Instructor";
                    monthgrid[addcolumn].Columns[5].Name = "Students";
                    monthgrid[addcolumn].Columns[6].Name = "Present";
                    monthgrid[addcolumn].Columns[7].Name = "Absent";
                    monthgrid[addcolumn].Columns[0].Visible = false;
                }



                for (int month = 1; month <= 12; month++)
                {
                    string date1;
                    if (month < 10) { date1 = year + "-0" + month; } else { date1 = year + "-" + month; }

                    SQLiteCommand s = new SQLiteCommand("select rowid,strftime('%d-%m-%Y',Date) as Date,Batch,Timing,Instructorname as Instructor,Numberofstudent as \"Students\" ,Present,Absent  from AttendanceTbl where  Date LIKE '" + date1 + "%'", connection);
                    using (SQLiteDataReader reader = s.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            monthgrid[month - 1].Rows.Add(new object[] { 
                        reader.GetValue(reader.GetOrdinal("rowid")),  
                        Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Date"))).ToString("dd-MM-yyyy"),
                        reader.GetString(reader.GetOrdinal("Batch")),
                        reader.GetString(reader.GetOrdinal("Timing")) ,
                        reader.GetValue(reader.GetOrdinal("Instructor")),
                        reader.GetValue(reader.GetOrdinal("Students")),
                        reader.GetValue(reader.GetOrdinal("Present")),
                        reader.GetValue(reader.GetOrdinal("Absent")) 
                        });
                        }
                    }

                    sda2 = new SQLiteDataAdapter("select count(rowid) from AttendanceTbl  where   Date LIKE '" + date1 + "%' and Absent!=''", connection);
                    dg = new DataTable();
                    sda2.Fill(dg);

                    if (dg.Rows[0][0].ToString() != "0")
                    {

                        s = new SQLiteCommand("select Absent from AttendanceTbl  where  Date LIKE '" + date1 + "%' and Absent!=''", connection);
                        int totalabsents = 0;
                        using (SQLiteDataReader reader = s.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                totalabsents = totalabsents + Convert.ToInt32(reader.GetString(reader.GetOrdinal("Absent")));
                            }
                        }


                        notification[month - 1].Value = totalabsents;
                        notification[month - 1].Visible = true;
                    }
                    else
                    {
                        notification[month - 1].Value = 0;
                        notification[month - 1].Visible = false;

                    }

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void tabcontrol1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabcontrol1.SelectedTab == Record)
            {
                Cursor.Current = Cursors.WaitCursor;
           
                SQLiteDataAdapter sda = new SQLiteDataAdapter("select count(rowid) from AttendanceTbl ", connection);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows[0][0].ToString() != "0")
                {
                    sda = new SQLiteDataAdapter("select rowid  from AttendanceTbl ", connection);
                    dt = new DataTable();
                    sda.Fill(dt);
                    ArrayList attendancelistg = new ArrayList(); ;
                    foreach (DataRow row in dt.Rows)
                    {
                        attendancelistg.Add(row["rowid"].ToString());
                               
                    }
                    for (int i = 0; i < attendancelistg.Count; i++) {

                        string presentcount, absentcount,totalstuds;
                        string attendanceid = attendancelistg[i].ToString();
                        sda = new SQLiteDataAdapter("select count(*) from AttendancerecordTbl where Attendanceid='" + attendanceid + "' and Status='Present'", connection);
                        dt = new DataTable();
                        sda.Fill(dt);
                        presentcount = dt.Rows[0][0].ToString();
                        sda = new SQLiteDataAdapter("select count(*) from AttendancerecordTbl where Attendanceid='" + attendanceid + "' and Status='Absent'", connection);
                        dt = new DataTable();
                        sda.Fill(dt);
                        absentcount = dt.Rows[0][0].ToString();
                        sda = new SQLiteDataAdapter("select count(*) from AttendancerecordTbl where Attendanceid='" + attendanceid + "'", connection);
                        dt = new DataTable();
                        sda.Fill(dt);
                        totalstuds = dt.Rows[0][0].ToString();
                        sda.UpdateCommand = new SQLiteCommand("update AttendanceTbl  SET Present='" + presentcount + "',Absent='" + absentcount + "',Numberofstudent='"+totalstuds+"' where rowid='" + attendanceid + "'", connection);
                        sda.UpdateCommand.ExecuteNonQuery();
               
                    }
                }
                Cursor.Current = Cursors.Default;
           
                yearlbl.Visible = true;
                numericyear.Visible = true;
                yearlbl.BringToFront();
                numericyear.BringToFront();
                numericyear.Value = Convert.ToInt32(DateTime.Now.Year);
                updater();
                MetroGrid[] monthgrid = { gridview1, gridview2, gridview3, gridview4, gridview5, gridview6, gridview7, gridview8, gridview9, gridview10, gridview11, gridview12 };
        
                this.ActiveControl = monthgrid[DateTime.Now.Month - 1];
            }
            else if (tabcontrol1.SelectedTab == Todayattendance)
            {
                yearlbl.Visible = false;
                numericyear.Visible = false;
            }
        }

        ArrayList rowidlist;
        ArrayList studentnamelist;

        private void next_Click(object sender, EventArgs e)
        {
            try
            {

                if (count > 0)
                {

                    rowidlist = new ArrayList();
                    studentnamelist = new ArrayList();
                    todaygrid.Rows.Clear();
                    SQLiteDataAdapter sda = new SQLiteDataAdapter("select rowid,Fullname  from StudentTbl where Instructorname='" + instructorcombo.Text + "' and Batch='" + batchcombo.Text + "' and Timing='" + timingcombo.Text + "'", connection);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {

                        sda = new SQLiteDataAdapter("select LastDay from FeeshistoryTbl where rowid=(select max(rowid)  from FeeshistoryTbl where Studentid='" + row["rowid"].ToString() + "')", connection);
                        DataTable dt1 = new DataTable();
                        sda.Fill(dt1);

                        if (dt1.Rows[0][0].ToString() != "")
                        {


                            if (DateTime.Now.Date <= Convert.ToDateTime((dt1.Rows[0][0])).Date)
                            {

                                rowidlist.Add(row["rowid"].ToString());
                                studentnamelist.Add(row["Fullname"].ToString());
                            }
                        }
                    }

                    if (rowidlist.Count == 0)
                    {
                        MetroMessageBox.Show(this, "No student under this category..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    else
                    {
                        string msg = "Students found under this category :\n";
                        for (int i = 0; i < studentnamelist.Count; i++)
                        {
                            if (i != 0)
                            {
                                if (i % 3 == 0)
                                {
                                    msg += "\n";
                                }
                            }
                            if (i == studentnamelist.Count - 1)
                            {
                                msg += studentnamelist[i].ToString() + ".";
                            }
                            else
                            {
                                msg += studentnamelist[i] + ",";
                            }
                        }
                        msg += "\n\nDo you want to continue..??";
                        DialogResult result = MetroMessageBox.Show(this, msg, "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                        if (result == DialogResult.Yes)
                        {

                            SQLiteDataAdapter compensatetbl = new SQLiteDataAdapter();

                            SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  count(rowid) from AttendanceTbl where Instructorname='" + instructorcombo.Text + "' and Batch='" + batchcombo.Text + "' and Timing='" + timingcombo.Text + "' and strftime('%Y-%m-%d',Date)='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'", connection);
                            dt = new DataTable();
                            attendancetbl.Fill(dt);

                            if (dt.Rows[0][0].ToString() == "0")
                            {   
                                attendancetbl = new SQLiteDataAdapter("select  count(rowid) from StudentTbl where Instructorname='" + instructorcombo.Text + "' and Batch='" + batchcombo.Text + "' and Timing='" + timingcombo.Text + "'", connection);
                                dt = new DataTable();
                                attendancetbl.Fill(dt);
                                string numberofstudents = dt.Rows[0][0].ToString();
                                attendancetbl.InsertCommand = new SQLiteCommand("insert into AttendanceTbl(Batch,Timing,Date,Present,Absent,Instructorname,Numberofstudent) values('" + batchcombo.Text + "','" + timingcombo.Text + "',@date,'','','" + instructorcombo.Text + "','" + numberofstudents + "');", connection);
                                attendancetbl.InsertCommand.Parameters.Add("@date", DbType.DateTime).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                                attendancetbl.InsertCommand.ExecuteNonQuery();

                                attendancetbl = new SQLiteDataAdapter("select max(rowid) from AttendanceTbl", connection);
                                DataTable attendancedt = new DataTable();
                                attendancetbl.Fill(attendancedt);
                                string attendanceid = attendancedt.Rows[0][0].ToString();

                                for (int i = 0; i < rowidlist.Count; i++)
                                {
                                    compensatetbl = new SQLiteDataAdapter("select count(rowid)  from  CompensateTbl  where Studentid='" + rowidlist[i].ToString() + "'", connection);
                                    dt = new DataTable();
                                    compensatetbl.Fill(dt);
                                    if (dt.Rows[0][0].ToString() == "1")
                                    {
                                        compensatetbl = new SQLiteDataAdapter("select  Compensatedate,Reason from  CompensateTbl  where Studentid='" + rowidlist[i].ToString() + "'", connection);
                                        dt = new DataTable();
                                        compensatetbl.Fill(dt);

                                        DateTime compensatedate = Convert.ToDateTime(dt.Rows[0][0]);
                                        
                                        if (DateTime.Now.Date < compensatedate.Date)
                                        {
                                            attendancetbl.InsertCommand = new SQLiteCommand("insert into AttendancerecordTbl(Attendanceid,Batch,Timing,Date,Studentid,Fullname,Status,Reason,Compensatedate,Informed,Instructorname) values('" + attendanceid + "','" + batchcombo.Text + "','" + timingcombo.Text + "',@date,'" + rowidlist[i].ToString() + "','" + studentnamelist[i].ToString() + "','Absent','" + dt.Rows[0][1].ToString() + "',@compensatedate,'Yes','" + instructorcombo.Text + "');", connection);
                                            attendancetbl.InsertCommand.Parameters.Add("@date", DbType.DateTime).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                                            attendancetbl.InsertCommand.Parameters.Add("@compensatedate", DbType.DateTime).Value = new DateTime(compensatedate.Year, compensatedate.Month, compensatedate.Day);
                                            attendancetbl.InsertCommand.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            attendancetbl.DeleteCommand = new SQLiteCommand("delete from CompensateTbl where Studentid='" + rowidlist[i].ToString() + "'", connection);
                                            attendancetbl.DeleteCommand.ExecuteNonQuery();
                                            attendancetbl.InsertCommand = new SQLiteCommand("insert into AttendancerecordTbl(Attendanceid,Batch,Timing,Date,Studentid,Fullname,Status,Reason,Compensatedate,Informed,Instructorname) values('" + attendanceid + "','" + batchcombo.Text + "','" + timingcombo.Text + "',@date,'" + rowidlist[i].ToString() + "','" + studentnamelist[i].ToString() + "','','','','','" + instructorcombo.Text + "');", connection);
                                            attendancetbl.InsertCommand.Parameters.Add("@date", DbType.DateTime).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                                            attendancetbl.InsertCommand.ExecuteNonQuery();
                                        }
                                    }
                                    else if (dt.Rows[0][0].ToString() == "0")
                                    {
                                        attendancetbl.InsertCommand = new SQLiteCommand("insert into AttendancerecordTbl(Attendanceid,Batch,Timing,Date,Studentid,Fullname,Status,Reason,Compensatedate,Informed,Instructorname) values('" + attendanceid + "','" + batchcombo.Text + "','" + timingcombo.Text + "',@date,'" + rowidlist[i].ToString() + "','" + studentnamelist[i].ToString() + "','','','','','" + instructorcombo.Text + "');", connection);
                                        attendancetbl.InsertCommand.Parameters.Add("@date", DbType.DateTime).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                                        attendancetbl.InsertCommand.ExecuteNonQuery();
                                    }
                                }
                               todaygridgenerator();
                            }
                            else if (dt.Rows[0][0].ToString() == "1")
                            {
                                SQLiteDataAdapter attendancetbl1 = new SQLiteDataAdapter("select rowid from AttendanceTbl where Instructorname='" + instructorcombo.Text + "' and Batch='" + batchcombo.Text + "' and Timing='" + timingcombo.Text + "' and strftime('%Y-%m-%d',Date)='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'", connection);
                                DataTable dt1 = new DataTable();
                                attendancetbl1.Fill(dt1);


                             attendancetbl1 = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Attendanceid='" + dt.Rows[0][0].ToString() + "'", connection);
                             dt1 = new DataTable();
                             attendancetbl1.Fill(dt1);

                            if (Convert.ToInt32(dt1.Rows[0][0]) > 0)
                            {
                                todaygridgenerator();
                            }
                            else
                            {
                                
                            }


                            }
                        }
                    }

                }
                else
                {
                    MetroMessageBox.Show(this, "No student for attendance..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void todaygridgenerator()
        {

            SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  rowid  from AttendanceTbl where Instructorname='" + instructorcombo.Text + "' and Batch='" + batchcombo.Text + "' and Timing='" + timingcombo.Text + "' and strftime('%Y-%m-%d',Date)='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'", connection);
            DataTable dt = new DataTable();
            attendancetbl.Fill(dt);
            string attendanceid = dt.Rows[0][0].ToString();

          
            todaygrid.Visible = true;
            pictureBox2.Visible = false;

            back.Visible = true;
          
            attendancetbl = new SQLiteDataAdapter("select  count(rowid) from StudentTbl where Instructorname='" + instructorcombo.Text + "' and Batch='" + batchcombo.Text + "' and Timing='" + timingcombo.Text + "'", connection);
            dt = new DataTable();
            attendancetbl.Fill(dt);
            string numberofstudents = dt.Rows[0][0].ToString();

            attendancetbl.UpdateCommand = new SQLiteCommand("update AttendanceTbl Set Numberofstudent='" + numberofstudents + "' where rowid='" + attendanceid + "';", connection);
            attendancetbl.UpdateCommand.ExecuteNonQuery();


            todaygrid.ColumnCount = 9;
            todaygrid.Columns[0].Name = "rowid";
            todaygrid.Columns[1].Name = "Date";
            todaygrid.Columns[2].Name = "Batch";
            todaygrid.Columns[3].Name = "Timing";
            todaygrid.Columns[4].Name = "Instructor";
            todaygrid.Columns[5].Name = "Full name";
            todaygrid.Columns[6].Name = "Status";
            todaygrid.Columns[7].Name = "Informed";
            todaygrid.Columns[8].Name = "Studentid";
            todaygrid.Columns[0].Visible = false;
            todaygrid.Columns[8].Visible = false;


            SQLiteCommand s = new SQLiteCommand("select rowid,strftime('%d-%m-%Y',Date) as Date,Batch,Timing,Instructorname as Instructor,Fullname as \"Full name\",Status,Informed,Studentid   from AttendancerecordTbl where Attendanceid='" + attendanceid + "'", connection);
            using (SQLiteDataReader reader = s.ExecuteReader())
            {
                while (reader.Read())
                {
                    todaygrid.Rows.Add(new object[] 
                        { 
                        reader.GetValue(reader.GetOrdinal("rowid")),  
                        reader.GetString(reader.GetOrdinal("Date")),
                        reader.GetValue(reader.GetOrdinal("Batch")),
                        reader.GetValue(reader.GetOrdinal("Timing")) ,
                        reader.GetValue(reader.GetOrdinal("Instructor")),
                        reader.GetValue(reader.GetOrdinal("Full name")),
                        reader.GetValue(reader.GetOrdinal("Status")),
                        reader.GetValue(reader.GetOrdinal("Informed")), 
                        reader.GetValue(reader.GetOrdinal("Studentid")) 
                        });
                }
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            todaygrid.Visible = false;
            back.Visible = false;
            pictureBox2.Visible = true;
        }

        private void todaygrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.todaygrid.Rows[e.RowIndex];
                    new Attendanceextension1(row.Cells["rowid"].Value.ToString(), row.Cells["Full name"].Value.ToString(), row.Cells["Studentid"].Value.ToString()).ShowDialog();
                    int a = todaygrid.CurrentCell.RowIndex;
                    todaygrid.Rows.Clear();
                    todaygridgenerator();
                    todaygrid.Rows[a].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void numericyear_Click(object sender, EventArgs e)
        {
            updater();
        }

        private void updater()
        {
            MetroGrid[] monthgrid = { gridview1, gridview2, gridview3, gridview4, gridview5, gridview6, gridview7, gridview8, gridview9, gridview10, gridview11, gridview12 };
            iTalk_NotificationNumber[] notification = { jannot, febnot, marnot, aprnot, maynot, junnot, julnot, augnot, sepnot, octnot, novnot, decnot };

            for (int addcolumn = 0; addcolumn < 12; addcolumn++)
            {
                monthgrid[addcolumn].Rows.Clear();
                notification[addcolumn].Value = 0;
                notification[addcolumn].Visible = false;
            }
            generateMonthly(Convert.ToInt32(numericyear.Value));
        }

        private void gridview10_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

         try{
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview10.Rows[e.RowIndex];
                    SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);
                    DataTable dt = new DataTable();
                    attendancetbl.Fill(dt);

            if (Convert.ToInt32(dt.Rows[0][0]) > 0)
            {
            
                new Attendanceextension2(row.Cells["rowid"].Value.ToString(),row.Cells["Date"].Value.ToString(),row.Cells["Batch"].Value.ToString(),row.Cells["Timing"].Value.ToString(),row.Cells["Instructor"].Value.ToString()).ShowDialog();

            }
            else
            {
                MetroMessageBox.Show(this, "No students found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }


                }
            }
         catch (Exception ex)
         {

             MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

         }
        }

        private void gridview1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview1.Rows[e.RowIndex];

                    SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);
                    DataTable dt = new DataTable();
                    attendancetbl.Fill(dt);

                    if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        new Attendanceextension2(row.Cells["rowid"].Value.ToString(), row.Cells["Date"].Value.ToString(), row.Cells["Batch"].Value.ToString(), row.Cells["Timing"].Value.ToString(), row.Cells["Instructor"].Value.ToString()).ShowDialog();
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "No students found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }


                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void gridview2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview2.Rows[e.RowIndex];

                    SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);
                    DataTable dt = new DataTable();
                    attendancetbl.Fill(dt);

                    if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        new Attendanceextension2(row.Cells["rowid"].Value.ToString(), row.Cells["Date"].Value.ToString(), row.Cells["Batch"].Value.ToString(), row.Cells["Timing"].Value.ToString(), row.Cells["Instructor"].Value.ToString()).ShowDialog();

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "No students found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }


                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void gridview3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview3.Rows[e.RowIndex];

                    SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);
                    DataTable dt = new DataTable();
                    attendancetbl.Fill(dt);

                    if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        new Attendanceextension2(row.Cells["rowid"].Value.ToString(), row.Cells["Date"].Value.ToString(), row.Cells["Batch"].Value.ToString(), row.Cells["Timing"].Value.ToString(), row.Cells["Instructor"].Value.ToString()).ShowDialog();

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "No students found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }


                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void gridview4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview4.Rows[e.RowIndex];

                    SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);
                    DataTable dt = new DataTable();
                    attendancetbl.Fill(dt);

                    if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        new Attendanceextension2(row.Cells["rowid"].Value.ToString(), row.Cells["Date"].Value.ToString(), row.Cells["Batch"].Value.ToString(), row.Cells["Timing"].Value.ToString(), row.Cells["Instructor"].Value.ToString()).ShowDialog();

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "No students found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }


                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void gridview5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview5.Rows[e.RowIndex];

                    SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);
                    DataTable dt = new DataTable();
                    attendancetbl.Fill(dt);

                    if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        new Attendanceextension2(row.Cells["rowid"].Value.ToString(), row.Cells["Date"].Value.ToString(), row.Cells["Batch"].Value.ToString(), row.Cells["Timing"].Value.ToString(), row.Cells["Instructor"].Value.ToString()).ShowDialog();

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "No students found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }


                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void gridview6_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview6.Rows[e.RowIndex];

                    SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);
                    DataTable dt = new DataTable();
                    attendancetbl.Fill(dt);

                    if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        new Attendanceextension2(row.Cells["rowid"].Value.ToString(), row.Cells["Date"].Value.ToString(), row.Cells["Batch"].Value.ToString(), row.Cells["Timing"].Value.ToString(), row.Cells["Instructor"].Value.ToString()).ShowDialog();

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "No students found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }


                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void gridview7_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview7.Rows[e.RowIndex];

                    SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);
                    DataTable dt = new DataTable();
                    attendancetbl.Fill(dt);

                    if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        new Attendanceextension2(row.Cells["rowid"].Value.ToString(), row.Cells["Date"].Value.ToString(), row.Cells["Batch"].Value.ToString(), row.Cells["Timing"].Value.ToString(), row.Cells["Instructor"].Value.ToString()).ShowDialog();

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "No students found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }


                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void gridview8_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview8.Rows[e.RowIndex];

                    SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);
                    DataTable dt = new DataTable();
                    attendancetbl.Fill(dt);

                    if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        new Attendanceextension2(row.Cells["rowid"].Value.ToString(), row.Cells["Date"].Value.ToString(), row.Cells["Batch"].Value.ToString(), row.Cells["Timing"].Value.ToString(), row.Cells["Instructor"].Value.ToString()).ShowDialog();

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "No students found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }


                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void gridview9_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview9.Rows[e.RowIndex];

                    SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);
                    DataTable dt = new DataTable();
                    attendancetbl.Fill(dt);

                    if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        new Attendanceextension2(row.Cells["rowid"].Value.ToString(), row.Cells["Date"].Value.ToString(), row.Cells["Batch"].Value.ToString(), row.Cells["Timing"].Value.ToString(), row.Cells["Instructor"].Value.ToString()).ShowDialog();

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "No students found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }


                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void gridview11_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview11.Rows[e.RowIndex];

                    SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);
                    DataTable dt = new DataTable();
                    attendancetbl.Fill(dt);

                    if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        new Attendanceextension2(row.Cells["rowid"].Value.ToString(), row.Cells["Date"].Value.ToString(), row.Cells["Batch"].Value.ToString(), row.Cells["Timing"].Value.ToString(), row.Cells["Instructor"].Value.ToString()).ShowDialog();

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "No students found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }


                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void gridview12_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview12.Rows[e.RowIndex];

                    SQLiteDataAdapter attendancetbl = new SQLiteDataAdapter("select  count(rowid) from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);
                    DataTable dt = new DataTable();
                    attendancetbl.Fill(dt);

                    if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    {
                        new Attendanceextension2(row.Cells["rowid"].Value.ToString(), row.Cells["Date"].Value.ToString(), row.Cells["Batch"].Value.ToString(), row.Cells["Timing"].Value.ToString(), row.Cells["Instructor"].Value.ToString()).ShowDialog();

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "No students found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }


                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        SQLiteDataAdapter m = new SQLiteDataAdapter();
        private void gridview1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (gridview1.Rows.Count != 0)
                    {

                        if (gridview1.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.gridview1.Rows[gridview1.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {


                                m.DeleteCommand = new SQLiteCommand("Delete from AttendanceTbl where rowid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();
                            
                                int a = gridview1.CurrentCell.RowIndex;
                                updater();
                                if (a != 0)
                                {
                                    gridview1.Rows[a - 1].Selected = true;
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void gridview2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (gridview2.Rows.Count != 0)
                    {

                        if (gridview2.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.gridview2.Rows[gridview2.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {


                                m.DeleteCommand = new SQLiteCommand("Delete from AttendanceTbl where rowid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                int a = gridview2.CurrentCell.RowIndex;
                                updater();
                                if (a != 0)
                                {
                                    gridview2.Rows[a - 1].Selected = true;
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void gridview3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (gridview3.Rows.Count != 0)
                    {

                        if (gridview3.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.gridview3.Rows[gridview3.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {


                                m.DeleteCommand = new SQLiteCommand("Delete from AttendanceTbl where rowid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                int a = gridview3.CurrentCell.RowIndex;
                                updater();
                                if (a != 0)
                                {
                                    gridview3.Rows[a - 1].Selected = true;
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void gridview4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (gridview4.Rows.Count != 0)
                    {

                        if (gridview4.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.gridview4.Rows[gridview4.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {


                                m.DeleteCommand = new SQLiteCommand("Delete from AttendanceTbl where rowid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                int a = gridview4.CurrentCell.RowIndex;
                                updater();
                                if (a != 0)
                                {
                                    gridview4.Rows[a - 1].Selected = true;
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void gridview5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (gridview5.Rows.Count != 0)
                    {

                        if (gridview5.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.gridview5.Rows[gridview5.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {


                                m.DeleteCommand = new SQLiteCommand("Delete from AttendanceTbl where rowid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                int a = gridview5.CurrentCell.RowIndex;
                                updater();
                                if (a != 0)
                                {
                                    gridview5.Rows[a - 1].Selected = true;
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void gridview6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (gridview6.Rows.Count != 0)
                    {

                        if (gridview6.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.gridview6.Rows[gridview6.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {


                                m.DeleteCommand = new SQLiteCommand("Delete from AttendanceTbl where rowid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                int a = gridview6.CurrentCell.RowIndex;
                                updater();
                                if (a != 0)
                                {
                                    gridview6.Rows[a - 1].Selected = true;
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void gridview7_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (gridview7.Rows.Count != 0)
                    {

                        if (gridview7.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.gridview7.Rows[gridview7.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {


                                m.DeleteCommand = new SQLiteCommand("Delete from AttendanceTbl where rowid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                int a = gridview7.CurrentCell.RowIndex;
                                updater();
                                if (a != 0)
                                {
                                    gridview7.Rows[a - 1].Selected = true;
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void gridview8_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (gridview8.Rows.Count != 0)
                    {

                        if (gridview8.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.gridview8.Rows[gridview8.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {


                                m.DeleteCommand = new SQLiteCommand("Delete from AttendanceTbl where rowid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                int a = gridview8.CurrentCell.RowIndex;
                                updater();
                                if (a != 0)
                                {
                                    gridview8.Rows[a - 1].Selected = true;
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void gridview9_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (gridview9.Rows.Count != 0)
                    {

                        if (gridview9.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.gridview9.Rows[gridview9.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {


                                m.DeleteCommand = new SQLiteCommand("Delete from AttendanceTbl where rowid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                int a = gridview9.CurrentCell.RowIndex;
                                updater();
                                if (a != 0)
                                {
                                    gridview9.Rows[a - 1].Selected = true;
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void gridview10_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (gridview10.Rows.Count != 0)
                    {

                        if (gridview10.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.gridview10.Rows[gridview10.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {


                                m.DeleteCommand = new SQLiteCommand("Delete from AttendanceTbl where rowid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                int a = gridview10.CurrentCell.RowIndex;
                                updater();
                                if (a != 0)
                                {
                                    gridview10.Rows[a - 1].Selected = true;
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void gridview11_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (gridview11.Rows.Count != 0)
                    {

                        if (gridview11.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.gridview11.Rows[gridview11.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {


                                m.DeleteCommand = new SQLiteCommand("Delete from AttendanceTbl where rowid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                int a = gridview11.CurrentCell.RowIndex;
                                updater();
                                if (a != 0)
                                {
                                    gridview11.Rows[a - 1].Selected = true;
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void gridview12_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (gridview12.Rows.Count != 0)
                    {

                        if (gridview12.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.gridview12.Rows[gridview12.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {


                                m.DeleteCommand = new SQLiteCommand("Delete from AttendanceTbl where rowid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl where Attendanceid='" + row.Cells["rowid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();


                            
                                int a = gridview12.CurrentCell.RowIndex;
                                updater();
                                if (a != 0)
                                {
                                    gridview12.Rows[a - 1].Selected = true;
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

    }
}