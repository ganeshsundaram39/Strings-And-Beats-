using MetroFramework;
using MetroFramework.Controls;
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
    public partial class Search : MetroForm
    {
        SQLiteConnection connection;
        string connectionString; 

        public Form RefTologinform { get; set; }
        string instructorname;

        public Search(string instructorname)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.instructorname = instructorname;
            connectionString = Connection.getConnection();
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            try
            {

                gridview.ColumnCount = 13;
                gridview.Columns[0].Name = "Id";
                gridview.Columns[1].Name = "Name";
                gridview.Columns[2].Name = "Instrument";
                gridview.Columns[3].Name = "Instructor name";
                gridview.Columns[4].Name = "Date of joining";
                gridview.Columns[5].Name = "Batch";
                gridview.Columns[6].Name = "Timing";
                gridview.Columns[7].Name = "Branch";
                gridview.Columns[8].Name = "Plan";
                gridview.Columns[9].Name = "Fees";
                gridview.Columns[10].Name = "Discount";
                gridview.Columns[11].Name = "Total fees";
                gridview.Columns[12].Name = "Mode";
                SQLiteCommand s;
                if (instructorname == "icamefrommain") {
                    s = new SQLiteCommand("select rowid,Fullname,Musicalinstrument,Instructorname,Dateofjoining,Batch,Timing,Branch,Paymentplan,Fees,Discount,Totalfees,Paymentmode   from StudentTbl ", connection);
                    pictureBox2.Visible = false;
                }
                else
                {
                    s = new SQLiteCommand("select rowid,Fullname,Musicalinstrument,Instructorname,Dateofjoining,Batch,Timing,Branch,Paymentplan,Fees,Discount,Totalfees,Paymentmode   from StudentTbl where Instructorname='"+camelCase(instructorname)+"'", connection);
                    pictureBox2.Visible = true;
              
                }
                using (SQLiteDataReader reader = s.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        gridview.Rows.Add(new object[] { 
                        reader.GetValue(reader.GetOrdinal("rowid")),  
                        reader.GetValue(reader.GetOrdinal("Fullname")),
                        reader.GetValue(reader.GetOrdinal("Musicalinstrument")) ,
                        reader.GetValue(reader.GetOrdinal("Instructorname")),  
                        Convert.ToDateTime(reader.GetString(reader.GetOrdinal("Dateofjoining"))).ToString("dd-MM-yyyy"),
                        reader.GetValue(reader.GetOrdinal("Batch")) ,
                        reader.GetValue(reader.GetOrdinal("Timing")),
                        reader.GetValue(reader.GetOrdinal("Branch")),
                        reader.GetValue(reader.GetOrdinal("Paymentplan")) ,
                        reader.GetValue(reader.GetOrdinal("Fees")) ,
                        reader.GetValue(reader.GetOrdinal("Discount")),
                        reader.GetValue(reader.GetOrdinal("Totalfees")),
                        reader.GetValue(reader.GetOrdinal("Paymentmode")) 
                      });
                    }

                   
                }
 gradeAutoComplete();

            }
            catch (Exception ex) {
                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            
            }
            gridview.Visible = true;
            panel.Visible = false;
       
            


        
        }

        private void gradeAutoComplete()
        {
            SQLiteCommand s;
            gradetxt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            gradetxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();


            s = new SQLiteCommand("select DISTINCT Grade from StudentTbl", connection);
            SQLiteDataReader reader1 = s.ExecuteReader();


            while (reader1.Read())
            {
                coll.Add(reader1.GetString(reader1.GetOrdinal("Grade")));
            }

            gradetxt.AutoCompleteCustomSource = coll;


          
        }

        string rowid;

        private string camelCase(string text){return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);}

        private void gridview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {   try
            {
                if (e.RowIndex >= 0)
                {

                    gridview.Visible = false;
                    panel.Visible = true;
                    if (instructorname != "icamefrommain")
                    {
                        pictureBox2.Visible = false;
                    }

                    DataGridViewRow row = this.gridview.Rows[e.RowIndex];

                    
                      rowid = row.Cells["Id"].Value.ToString();
                      SQLiteDataAdapter d = new SQLiteDataAdapter("select Fullname as \"Full name\" ,Mobilenumber as\"Mobile number\",Parentname as \"Parent name\",Parentcontactno as \"Parent contact no\",Emailid as \"Email id\",Gender as \"Gender\",Address,Grade,Remark  from StudentTbl where rowid='" + row.Cells["Id"].Value.ToString() + "'", connection);

                      DataTable dt = new DataTable();
                      d.Fill(dt);

                      fullname.Text =dt.Rows[0][0].ToString();
                      mobileno.Text = dt.Rows[0][1].ToString(); parentname.Text = dt.Rows[0][2].ToString(); parentcontactno.Text = dt.Rows[0][3].ToString(); emailid.Text = dt.Rows[0][4].ToString();
                      if (dt.Rows[0][5].ToString() == "male") { pictureBox1.Image = Properties.Resources.new_student; } else { pictureBox1.Image = Properties.Resources.female; }
                      if (dt.Rows[0][6].ToString().Length > 87)
                      { 
                          address.Text =dt.Rows[0][6].ToString().Substring(0, 87) + "\n" + dt.Rows[0][6].ToString().Substring(87); }
                      else
                      {

                          address.Text =dt.Rows[0][6].ToString();   
                      
                      }
                    gradetxt.Text= camelCase( dt.Rows[0][7].ToString());
                    remarktxt.Text = camelCase(dt.Rows[0][8].ToString());


                      SQLiteCommand s = new SQLiteCommand("select Dateofbirth from StudentTbl where rowid='" + row.Cells["Id"].Value.ToString() + "'", connection);
                      SQLiteDataReader reader = s.ExecuteReader();
                      while (reader.Read())
                      {
                          
                              dateofbirth.Text = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("Dateofbirth"))).ToString("dd-MM-yyyy");
                      
                 
                      }

   

                }
            }
        catch (Exception ex)
        {

            MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
       
        }

        private void next1_Click(object sender, EventArgs e)
        {
            gridview.Visible = true;
            panel.Visible = false;
            this.Text = "Search";
            if (instructorname != "icamefrommain")
            {
                pictureBox2.Visible = true;
                 
            }
        }

        private void Search_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

       

        private void metroButton3_Click(object sender, EventArgs e)
        {
            SQLiteDataAdapter sda2 = new SQLiteDataAdapter();
          
                sda2.UpdateCommand = new SQLiteCommand("update StudentTbl  SET Grade='" +camelCase( gradetxt.Text)+"' , Remark='"+camelCase( remarktxt.Text)+"'  where rowid= '" +rowid+ "'", connection);
                sda2.UpdateCommand.ExecuteNonQuery();
                MetroMessageBox.Show(this, "Student's feedback saved..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                gradeAutoComplete();
           
          }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            this.RefTologinform.Visible = true;

            this.Visible = false;

        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Cursor = Cursors.Default;
        }

        private void metroTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void remarktxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsLetter(ch) && ch != 8 && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;

            }
        }

      
    
      
    }
}
