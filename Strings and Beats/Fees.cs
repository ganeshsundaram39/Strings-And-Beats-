using iTalk;
using MetroFramework;
using MetroFramework.Controls;
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
    public partial class Fees : MetroForm
    {
        static string connectionString = Connection.getConnection();
        SQLiteConnection connection = new SQLiteConnection(connectionString);

        public Fees()
        {
            InitializeComponent();
            connection.Open();
            numericyear.Minimum = 2000;
            numericyear.Maximum = Convert.ToInt32(DateTime.Now.Year+1);
            numericyear.Value = Convert.ToInt32(DateTime.Now.Year);
            generateMonthly(Convert.ToInt32(DateTime.Now.Year));
            generaterOther(Convert.ToInt32(numericyear.Value));
            tabcontrol1.SelectedIndex = 0;
            tabcontrol2.SelectedIndex = DateTime.Now.Month - 1;
            MetroGrid[] monthgrid = { gridview1, gridview2, gridview3, gridview4, gridview5, gridview6, gridview7, gridview8, gridview9, gridview10, gridview11, gridview12 };
            this.ActiveControl =monthgrid[ DateTime.Now.Month - 1];
        }

        private void numericyear_Click(object sender, EventArgs e)
        {
            updater();
        }

        private void updater() { 
            MetroGrid[] monthgrid = { gridview1, gridview2, gridview3, gridview4, gridview5, gridview6, gridview7, gridview8, gridview9, gridview10, gridview11, gridview12 };
            iTalk_NotificationNumber[] notification = { jannot, febnot, marnot, aprnot, maynot, junnot, julnot, augnot, sepnot, octnot, novnot, decnot };
         
            for (int addcolumn = 0; addcolumn < 12; addcolumn++)
            {
                monthgrid[addcolumn].Rows.Clear();
                notification[addcolumn].Value = 0;
                notification[addcolumn].Visible = false;
            }          
            generateMonthly(Convert.ToInt16(numericyear.Value));
          
               othergrid.Rows.Clear();
               othernot.Value = 0;
               othernot.Visible = false;
               generaterOther(Convert.ToInt16(numericyear.Value));
        }

        private void generateMonthly(int year)
        {
            try
            {
                SQLiteDataAdapter sda2; DataTable dg;
                MetroGrid[] monthgrid = { gridview1, gridview2, gridview3, gridview4, gridview5, gridview6, gridview7, gridview8, gridview9, gridview10, gridview11, gridview12 };
                iTalk_NotificationNumber[] notification = { jannot, febnot, marnot, aprnot, maynot, junnot, julnot, augnot, sepnot, octnot, novnot, decnot };
         
                for (int addcolumn = 0; addcolumn < 12; addcolumn++)
                {

                    monthgrid[addcolumn].ColumnCount = 7;
                    monthgrid[addcolumn].Columns[0].Name = "Feeid";
                    monthgrid[addcolumn].Columns[1].Name = "Name";
                    monthgrid[addcolumn].Columns[2].Name = "First day";
                    monthgrid[addcolumn].Columns[3].Name = "Last day";
                    monthgrid[addcolumn].Columns[4].Name = "Status";
                    monthgrid[addcolumn].Columns[5].Name = "Pay date";
                    monthgrid[addcolumn].Columns[6].Name = "Fees";

                    monthgrid[addcolumn].Columns[0].Visible = false;
                 }
            
                      

                for (int month = 1; month <= 12; month++)
                {
                    string date1;
                if (month <10) { date1 = year + "-0" + month; } else { date1 = year + "-" + month; }

                      
                  
                    SQLiteCommand s = new SQLiteCommand("select rowid,Fullname , strftime('%d-%m-%Y',Firstday) as Firstday  ,strftime('%d-%m-%Y',Lastday) as Lastday,Status,Paymentdate ,Totalfees  from FeeshistoryTbl where  Firstday LIKE '"+date1+"%'  and Paymentplan='Monthly'", connection);
                    using (SQLiteDataReader reader = s.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                        monthgrid[month - 1].Rows.Add(new object[] { 
                        reader.GetValue(reader.GetOrdinal("rowid")),  
                        reader.GetValue(reader.GetOrdinal("Fullname")),
                   
                        reader.GetString(reader.GetOrdinal("Firstday")),
                        reader.GetString(reader.GetOrdinal("Lastday")) ,
                        reader.GetValue(reader.GetOrdinal("Status")),
                        reader.GetValue(reader.GetOrdinal("Paymentdate")).ToString()!=""? Convert.ToDateTime( reader.GetValue(reader.GetOrdinal("Paymentdate"))).ToString("dd-MM-yyyy hh:mm tt"):"",
                        reader.GetValue(reader.GetOrdinal("Totalfees")) 


            });
                        }
                    }


                    sda2 = new SQLiteDataAdapter("select count(rowid) from FeeshistoryTbl  where Firstday LIKE '" + date1 + "%' and Paymentplan='Monthly' ", connection);
                    dg = new DataTable();
                    sda2.Fill(dg);
                    
                    if (dg.Rows[0][0].ToString() != "0")
                    {

                    sda2 = new SQLiteDataAdapter("select count(rowid) from FeeshistoryTbl  where Status='Unpaid' and Firstday LIKE '" + date1 + "%' and Paymentplan='Monthly' ", connection);
                    dg= new DataTable();
                    sda2.Fill(dg);
                    
                   
                        notification[month - 1].Value = Convert.ToInt32(dg.Rows[0][0]);
                        notification[month - 1].Visible = true;
                    }
                    else
                    {
                        notification[month - 1].Value =0;
                        notification[month - 1].Visible = false;

                    }

                }

                sda2 = new SQLiteDataAdapter("select count(rowid) from FeeshistoryTbl  where Status='Unpaid' and Firstday LIKE '" + year + "%' and  Paymentplan='Monthly' ", connection);
                dg = new DataTable();
                sda2.Fill(dg);

                if (dg.Rows[0][0].ToString() != "0")
                {

                    monthlynot.Value = Convert.ToInt32(dg.Rows[0][0]);
                    monthlynot.Visible = true;
                }
                else
                {
                    monthlynot.Value = 0;
                    monthlynot.Visible = false;

                }
            

             }
            catch (Exception ex) {
                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void generaterOther(int year) 
        {
            try
            {
               
                 othergrid.ColumnCount = 9;
                 othergrid.Columns[0].Name = "Feeid";
                 othergrid.Columns[1].Name = "Name";
                 othergrid.Columns[2].Name = "Plan";
                 othergrid.Columns[3].Name = "First day";
                 othergrid.Columns[4].Name = "Last day";
                 othergrid.Columns[5].Name = "Mode";
                 othergrid.Columns[6].Name = "Status";
                 othergrid.Columns[7].Name = "Pay date";
                 othergrid.Columns[8].Name = "Fees";
                 othergrid.Columns[0].Visible = false;



                 SQLiteCommand s = new SQLiteCommand("select rowid,Fullname ,Paymentplan, strftime('%d-%m-%Y',Firstday) as Firstday  ,strftime('%d-%m-%Y',Lastday) as Lastday,Paymentmode,Status,Paymentdate,Totalfees  from FeeshistoryTbl where  Firstday LIKE '" + year + "%'  and not Paymentplan='Monthly'; ", connection);
                    using (SQLiteDataReader reader = s.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                       othergrid.Rows.Add(new object[] 
                        { 
                        reader.GetValue(reader.GetOrdinal("rowid")),  
                        reader.GetValue(reader.GetOrdinal("Fullname")),
                        reader.GetValue(reader.GetOrdinal("Paymentplan")),
                        reader.GetString(reader.GetOrdinal("Firstday")) ,
                        reader.GetString(reader.GetOrdinal("Lastday")),
                        reader.GetValue(reader.GetOrdinal("Paymentmode")),
                        reader.GetValue(reader.GetOrdinal("Status")),
                        reader.GetValue(reader.GetOrdinal("Paymentdate")).ToString()!=""? Convert.ToDateTime( reader.GetValue(reader.GetOrdinal("Paymentdate"))).ToString("dd-MM-yyyy hh:mm tt"):"",
                        reader.GetValue(reader.GetOrdinal("Totalfees")) 


                    });
                        }
                    }
                 SQLiteDataAdapter sda2 = new SQLiteDataAdapter("select count(rowid) from FeeshistoryTbl  where  Firstday LIKE '" + year + "%' and not Paymentplan='Monthly' ", connection);
                    DataTable dg = new DataTable();
                    sda2.Fill(dg);
                    
                    if (dg.Rows[0][0].ToString() != "0")
                    {

                     sda2 = new SQLiteDataAdapter("select count(rowid) from FeeshistoryTbl  where Status='Unpaid' and Firstday LIKE '" + year + "%' and not Paymentplan='Monthly' ", connection);
                     dg = new DataTable();
                    sda2.Fill(dg);
                    
                   

                     othernot.Value = Convert.ToInt32(dg.Rows[0][0]);
                     othernot.Visible = true;
                    }
                    else
                    {
                    othernot.Value =0;
                    othernot.Visible = false;

                    }

                
            

             }
            catch (Exception ex) {
                  
                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

    }
        }

        private void Fees_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        SQLiteDataAdapter m = new SQLiteDataAdapter();

        private void gridview1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0)
                {


                    DataGridViewRow row = this.gridview1.Rows[e.RowIndex];
                    DateTime paydate;
                    if (row.Cells["Pay date"].Value.ToString() != "")
                    {
                        paydate = Convert.ToDateTime(row.Cells["Pay date"].Value.ToString());
                    }
                    else
                    {
                        paydate = DateTime.Now;
                    }
                    Boolean ispaid = row.Cells["Status"].Value.ToString() == "Paid" ? true : false;
                    new Feesextension1(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString(),"Monthly").ShowDialog();
                    int a = gridview1.CurrentCell.RowIndex;
                    updater();
                    gridview1.Rows[a].Selected = true;
            
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
                    DateTime paydate;
                    if (row.Cells["Pay date"].Value.ToString() != "")
                    {
                        paydate = Convert.ToDateTime(row.Cells["Pay date"].Value.ToString());
                    }
                    else
                    {
                        paydate = DateTime.Now;
                    }
                    Boolean ispaid = row.Cells["Status"].Value.ToString() == "Paid" ? true : false;
                    new Feesextension1(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString(),"Monthly").ShowDialog();
                    int a = gridview2.CurrentCell.RowIndex;
                    updater();
                    gridview2.Rows[a].Selected = true;
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
                    DateTime paydate;
                    if (row.Cells["Pay date"].Value.ToString() != "")
                    {
                        paydate = Convert.ToDateTime(row.Cells["Pay date"].Value.ToString());
                    }
                    else
                    {
                        paydate = DateTime.Now;
                    }
                    Boolean ispaid = row.Cells["Status"].Value.ToString() == "Paid" ? true : false;
                    new Feesextension1(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString(),"Monthly").ShowDialog();
                    int a = gridview3.CurrentCell.RowIndex;
                    updater(); gridview3.Rows[a].Selected = true;
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
                    DateTime paydate;
                    if (row.Cells["Pay date"].Value.ToString() != "")
                    {
                        paydate = Convert.ToDateTime(row.Cells["Pay date"].Value.ToString());
                    }
                    else
                    {
                        paydate = DateTime.Now;
                    }
                    Boolean ispaid = row.Cells["Status"].Value.ToString() == "Paid" ? true : false;
                    new Feesextension1(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString(),"Monthly").ShowDialog();
                    int a = gridview4.CurrentCell.RowIndex;
                    updater(); gridview4.Rows[a].Selected = true;
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
                    DateTime paydate;
                    if (row.Cells["Pay date"].Value.ToString() != "")
                    {
                        paydate = Convert.ToDateTime(row.Cells["Pay date"].Value.ToString());
                    }
                    else
                    {
                        paydate = DateTime.Now;
                    } 
                    Boolean ispaid = row.Cells["Status"].Value.ToString() == "Paid" ? true : false;
                    new Feesextension1(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString(),"Monthly").ShowDialog();
                    int a = gridview5.CurrentCell.RowIndex;
                    updater(); gridview5.Rows[a].Selected = true;
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

                    DateTime paydate;
                    if (row.Cells["Pay date"].Value.ToString() != "")
                    {
                        paydate = Convert.ToDateTime(row.Cells["Pay date"].Value.ToString());
                    }
                    else
                    {
                        paydate = DateTime.Now;
                    } Boolean ispaid = row.Cells["Status"].Value.ToString() == "Paid" ? true : false;
                    new Feesextension1(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString(),"Monthly").ShowDialog();
                    int a = gridview6.CurrentCell.RowIndex;
                    updater(); gridview6.Rows[a].Selected = true;
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
                    DateTime paydate;
                    if (row.Cells["Pay date"].Value.ToString() != "")
                    {
                        paydate = Convert.ToDateTime(row.Cells["Pay date"].Value.ToString());
                    }
                    else
                    {
                        paydate = DateTime.Now;
                    } 
                    Boolean ispaid = row.Cells["Status"].Value.ToString() == "Paid" ? true : false;
                    new Feesextension1(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString(),"Monthly").ShowDialog();
                    int a = gridview7.CurrentCell.RowIndex;
                    updater(); gridview7.Rows[a].Selected = true;
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
                    DateTime paydate;
                    if (row.Cells["Pay date"].Value.ToString() != "")
                    {
                        paydate = Convert.ToDateTime(row.Cells["Pay date"].Value.ToString());
                    }
                    else
                    {
                        paydate = DateTime.Now;
                    } 
                    Boolean ispaid = row.Cells["Status"].Value.ToString() == "Paid" ? true : false;
                    new Feesextension1(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString(),"Monthly").ShowDialog();
                    int a = gridview8.CurrentCell.RowIndex;
                    updater(); gridview8.Rows[a].Selected = true;
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
                    DateTime paydate;

                    if (row.Cells["Pay date"].Value.ToString() != "")
                    {
                        paydate = Convert.ToDateTime(row.Cells["Pay date"].Value.ToString());
                    }
                    else
                    {
                        paydate = DateTime.Now;
                    } 
                    Boolean ispaid = row.Cells["Status"].Value.ToString() == "Paid" ? true : false; 
                    int a=gridview9.CurrentCell.RowIndex;
                    new Feesextension1(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString(),"Monthly").ShowDialog();

                    updater(); gridview9.Rows[a].Selected = true;
                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void gridview10_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {


                    DataGridViewRow row = this.gridview10.Rows[e.RowIndex];
                    DateTime paydate;
                    if (row.Cells["Pay date"].Value.ToString() != "")
                    {
                        paydate = Convert.ToDateTime(row.Cells["Pay date"].Value.ToString());
                    }
                    else
                    {
                        paydate = DateTime.Now;
                    } 
                    Boolean ispaid = row.Cells["Status"].Value.ToString() == "Paid" ? true : false;
                    new Feesextension1(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString(),"Monthly").ShowDialog();
                    int a = gridview10.CurrentCell.RowIndex;
                    updater(); gridview10.Rows[a].Selected = true;
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
                    DateTime paydate;
                    if (row.Cells["Pay date"].Value.ToString() != "")
                    {
                        paydate = Convert.ToDateTime(row.Cells["Pay date"].Value.ToString());
                    }
                    else
                    {
                        paydate = DateTime.Now;
                    } Boolean ispaid = row.Cells["Status"].Value.ToString() == "Paid" ? true : false;
                    new Feesextension1(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString(),"Monthly").ShowDialog();
                    int a = gridview11.CurrentCell.RowIndex;
                    updater(); gridview11.Rows[a].Selected = true;
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
                    DateTime paydate;
                    if (row.Cells["Pay date"].Value.ToString() != "")
                    {
                        paydate = Convert.ToDateTime(row.Cells["Pay date"].Value.ToString());
                    }
                    else
                    {
                        paydate = DateTime.Now;
                    } Boolean ispaid = row.Cells["Status"].Value.ToString() == "Paid" ? true : false;
                    new Feesextension1(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString(),"Monthly").ShowDialog();
                    int a = gridview12.CurrentCell.RowIndex;
                    updater(); gridview12.Rows[a].Selected = true;
                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

       

      

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


                                m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

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


                                m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

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


                                m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

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


                                m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery(); int a = gridview4.CurrentCell.RowIndex;

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


                                m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery(); int a = gridview5.CurrentCell.RowIndex;
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


                                m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery(); int a = gridview6.CurrentCell.RowIndex;
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


                                m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery(); int a = gridview7.CurrentCell.RowIndex;
                                updater(); if (a != 0)
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


                                m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery(); int a = gridview8.CurrentCell.RowIndex;

                                updater(); if (a != 0)
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


                                m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

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


                                m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery(); int a = gridview10.CurrentCell.RowIndex;
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


                                m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery(); int a = gridview11.CurrentCell.RowIndex;
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


                                m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery(); int a = gridview12.CurrentCell.RowIndex;
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

        private void tabcontrol2_SelectedIndexChanged(object sender, EventArgs e)
        {
                MetroGrid[] monthgrid = { gridview1, gridview2, gridview3, gridview4, gridview5, gridview6, gridview7, gridview8, gridview9, gridview10, gridview11, gridview12 };

                this.ActiveControl = monthgrid[tabcontrol2.SelectedIndex];
      
        }

        private void tabcontrol1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MetroGrid[] monthgrid = { gridview1, gridview2, gridview3, gridview4, gridview5, gridview6, gridview7, gridview8, gridview9, gridview10, gridview11, gridview12 };

            if (tabcontrol1.SelectedTab == Monthly)
            {
                this.ActiveControl = monthgrid[tabcontrol2.SelectedIndex];
            }
            else
            {
                this.ActiveControl = othergrid;
            }
        }

        private void othergrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0)
                {
                   

                    DataGridViewRow row = this.othergrid.Rows[e.RowIndex];
                    DateTime paydate;
                    if (row.Cells["Pay date"].Value.ToString() != "")
                    {
                        paydate = Convert.ToDateTime(row.Cells["Pay date"].Value.ToString());
                    }
                    else
                    {
                        paydate = DateTime.Now;
                    }
                    Boolean ispaid = row.Cells["Status"].Value.ToString() == "Paid" ? true : false;

                    SQLiteDataAdapter sda2 = new SQLiteDataAdapter("select Paymentplan,Paymentmode from FeeshistoryTbl  where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);
                    DataTable dg = new DataTable();
                    sda2.Fill(dg);
                    if (dg.Rows[0][1].ToString() == "Full Payment")
                    {
                        new Feesextension1(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString(), dg.Rows[0][0].ToString()).ShowDialog();
                    }
                    else if (dg.Rows[0][1].ToString() =="Installments") {
                        new Feesextension2(row.Cells["Feeid"].Value.ToString(), row.Cells["Name"].Value.ToString(), ispaid, paydate, row.Cells["Fees"].Value.ToString()).ShowDialog();
                

                    }
                    int a = othergrid.CurrentCell.RowIndex;
                    updater(); othergrid.Rows[a].Selected = true;
                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void othergrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (othergrid.Rows.Count != 0)
                    {

                        if (othergrid.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.othergrid.Rows[othergrid.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {
                                SQLiteDataAdapter sda2 = new SQLiteDataAdapter("select Paymentmode from FeeshistoryTbl  where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);
                                DataTable dg = new DataTable();
                                sda2.Fill(dg);
                                if (dg.Rows[0][0].ToString() == "Full Payment")
                                {


                                    m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

                                    m.DeleteCommand.ExecuteNonQuery();
                                }
                                else  if (dg.Rows[0][0].ToString() == "Installments")
                                {
                                    m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where rowid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

                                    m.DeleteCommand.ExecuteNonQuery();
                                    m.DeleteCommand = new SQLiteCommand("Delete from InstallmentsTbl where Feeid='" + row.Cells["Feeid"].Value.ToString() + "'", connection);

                                    m.DeleteCommand.ExecuteNonQuery();
                                }

                                int a = othergrid.CurrentCell.RowIndex;
                                updater();
                                if (a != 0)
                                {
                                    othergrid.Rows[a - 1].Selected = true;
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
