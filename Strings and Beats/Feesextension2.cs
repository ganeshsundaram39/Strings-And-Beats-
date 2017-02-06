using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections;
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
    public partial class Feesextension2 : MetroForm
    {
        static string connectionString = Connection.getConnection();
        SQLiteConnection connection = new SQLiteConnection(connectionString);
        string feeid, studentid, fees, studentname,installmentid;

     
        
    

        public Feesextension2(String feeid, String name, Boolean status, DateTime paydate, String fees)
        {
            InitializeComponent();

            connection.Open();
            this.feeid = feeid;
            this.fees = fees;
            this.studentname = name;
            try
            {
                SQLiteDataAdapter sda2;
                this.name.Text = camelCase(name);
                this.feeslbl.Text = fees + "/-";
                if (status)
                {
                    installmentpaymentdate.Value = paydate;
                    installmentpaymentdate.Checked = true;
              
                   statuslbl.Text = "Paid";
                 
                   sendsms.Visible = true;
                }
                else
                {
                    installmentpaymentdate.Checked = false;
                    installmentpaymentdate.Value = paydate;
                    statuslbl.Text = "Unpaid";
                    sendsms.Visible = false;
                 }
                sda2 = new SQLiteDataAdapter("select Studentid from FeeshistoryTbl  where rowid='" + feeid + "'", connection);
                DataTable dg = new DataTable();
                sda2.Fill(dg);
                studentid = dg.Rows[0][0].ToString();
                sda2 = new SQLiteDataAdapter("select Gender from StudentTbl  where rowid='" + studentid + "'", connection);
                dg = new DataTable();
                sda2.Fill(dg);
                if (dg.Rows[0][0].ToString() == "male") { genderbox.Image = Properties.Resources.new_student; } else { genderbox.Image = Properties.Resources.female; }

          
              

                updater();
            }

            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            } 
        }
        ArrayList al = new ArrayList();
        private void updater() {


            gridview.ColumnCount = 6;
            gridview.Columns[0].Name = "Installmentid";
            gridview.Columns[1].Name = "Installment";
            gridview.Columns[2].Name = "Amount";
            gridview.Columns[3].Name = "Pay date";
            gridview.Columns[4].Name = "Status";
            gridview.Columns[5].Name = "Expectation";


            gridview.Columns[0].Visible = false;

           
        
            SQLiteCommand s = new SQLiteCommand("select rowid ,Installment,Paymentamt,Paymentdate,Status,Expecteddate  from InstallmentsTbl where Feeid='" + feeid + "'; ", connection);
            using (SQLiteDataReader reader = s.ExecuteReader())
            {
                while (reader.Read())
                {
                    gridview.Rows.Add(new object[] 
                        { 
                        reader.GetValue(reader.GetOrdinal("rowid")),  
                        reader.GetValue(reader.GetOrdinal("Installment")),
                   
                        reader.GetValue(reader.GetOrdinal("Paymentamt")),
                       reader.GetValue(reader.GetOrdinal("Paymentdate")).ToString()!=""? Convert.ToDateTime( reader.GetValue(reader.GetOrdinal("Paymentdate"))).ToString("dd/MM/yyyy hh:mm tt"):"",
              
                        reader.GetValue(reader.GetOrdinal("Status")) ,
                        reader.GetValue(reader.GetOrdinal("Expecteddate")).ToString()!=""? Convert.ToDateTime( reader.GetValue(reader.GetOrdinal("Expecteddate"))).ToString("dd/MM/yyyy"):"",
                    

                    });
                    al.Add(reader.GetValue(reader.GetOrdinal("Paymentdate")).ToString() != "" ? Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("Paymentdate"))).ToString("dd/MM/yyyy hh:mm tt") : "");
                }
            }
      
               
            
            this.ActiveControl = gridview;
        }

        private string camelCase(string text) { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text); }

        private void next1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Feesextension2_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
               string status;
               if (installmentpaymentdate.Checked)
               { status = "Paid"; }
               else { status = "Unpaid"; }

                    adapter.UpdateCommand = new SQLiteCommand("update InstallmentsTbl  SET Status='"+status+"',Paymentdate=@paydate  where rowid='" + installmentid + "'", connection);
                    if (installmentpaymentdate.Checked)
                    {
                        adapter.UpdateCommand.Parameters.Add("@paydate", DbType.DateTime).Value = installmentpaymentdate.Value;
                    }
                    else
                    {
                        adapter.UpdateCommand.Parameters.Add("@paydate", DbType.DateTime).Value = DBNull.Value;
                
                    }
                        adapter.UpdateCommand.ExecuteNonQuery();
                    sendsms.Visible = true;
                    adapter = new SQLiteDataAdapter("select count(rowid) from InstallmentsTbl  where Feeid='" + feeid + "' and Status='Unpaid';", connection);
                    DataTable dg = new DataTable();
                    adapter.Fill(dg);
                    string count=dg.Rows[0][0].ToString();
                    if ( count== "0")
                    {
                        statuslbl.Text = "Paid";
                      
                    }
                    else 
                    {
                        statuslbl.Text = "Unpaid"; 
                    }
                    sendsms.Visible = true;
                    gridview.Rows.Clear();
                    updater();
                    if (count == "0")
                    {
                        adapter.UpdateCommand = new SQLiteCommand("update FeeshistoryTbl  SET Status='Paid',Paymentdate=@paydate  where rowid='" + feeid + "'", connection);
                        adapter.UpdateCommand.Parameters.Add("@paydate", DbType.DateTime).Value = Convert.ToDateTime(al[al.Count - 1].ToString());
                        adapter.UpdateCommand.ExecuteNonQuery();

                    }
                    else
                    {
                        adapter.UpdateCommand = new SQLiteCommand("update FeeshistoryTbl  SET Status='Unpaid',Paymentdate=@paydate  where rowid='" + feeid + "'", connection);
                        adapter.UpdateCommand.Parameters.Add("@paydate", DbType.DateTime).Value = DBNull.Value;
                        adapter.UpdateCommand.ExecuteNonQuery();

                    }
                

            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            } 
        }

        private void gridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                try
            {
                if (e.RowIndex >= 0)
                {
                 
                    DataGridViewRow row = this.gridview.Rows[e.RowIndex];
                    installmentid = row.Cells["Installmentid"].Value.ToString();
                    SQLiteDataAdapter s = new SQLiteDataAdapter("select Paymentdate from InstallmentsTbl where rowid='" +installmentid + "'", connection);
                 DataTable dt=new DataTable();
                 s.Fill(dt);
                  

                        if (dt.Rows[0][0].ToString()!="")
                        {
                          
                            installmentpaymentdate.Checked = true;
                            installmentpaymentdate.Value = Convert.ToDateTime(dt.Rows[0][0].ToString());
                        }
                        else
                        {
                         

                            installmentpaymentdate.Value = DateTime.Now; installmentpaymentdate.Checked = false;
                        }


                    


                    

                    
                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void gridview_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                 
                    DataGridViewRow row = this.gridview.Rows[e.RowIndex];
                    installmentid = row.Cells["Installmentid"].Value.ToString();
                       SQLiteDataAdapter s = new SQLiteDataAdapter("select Paymentdate from InstallmentsTbl where rowid='" +installmentid + "'", connection);
                 DataTable dt=new DataTable();
                 s.Fill(dt);
                     


                        if (dt.Rows[0][0].ToString() != "")
                        {
                          
                            installmentpaymentdate.Checked = true;
                            installmentpaymentdate.Value = Convert.ToDateTime(dt.Rows[0][0].ToString());
                        }
                        else
                        {


                            installmentpaymentdate.Value = DateTime.Now; installmentpaymentdate.Checked = false;
                        }





                    


                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void sendsms_Click(object sender, EventArgs e)
        {

        }

  
    }
}
