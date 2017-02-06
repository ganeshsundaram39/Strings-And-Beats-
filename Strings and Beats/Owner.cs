using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strings_and_Beats
{
    public partial class Owner : MetroForm
    {
        static string connectionString = Connection.getConnection();
        SQLiteConnection connection = new SQLiteConnection(connectionString);
        string ownerid, adminid;
        private static Dictionary<string, string> studentlist = new Dictionary<string, string> { };
        [DllImport("wininet.dll")]

        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public Owner()
        {
            InitializeComponent();
            try
            {
              
            connection.Open();
            
            tabcontrol.SelectedTab = tab1;
            this.DoubleBuffered = true;
            this.ActiveControl = updatebtn;
            SQLiteDataAdapter sda = new SQLiteDataAdapter("select rowid,Username,Password,EmailId,MobileNo from LoginTbl where Role='Owner'",connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ownerid = dt.Rows[0][0].ToString();
            ownernametxt.Text = dt.Rows[0][1].ToString();
            passwordtxt.Text = dt.Rows[0][2].ToString();
            emailidtxt.Text = dt.Rows[0][3].ToString();
            mobilenumbertxt.Text = dt.Rows[0][4].ToString();
            instructorGridGenerator();
            sda = new SQLiteDataAdapter("select rowid,Username,Password,EmailId,MobileNo from LoginTbl where Role='Admin'", connection);
            dt = new DataTable();
            sda.Fill(dt);
            adminid = dt.Rows[0][0].ToString();
            adminnametxt.Text = dt.Rows[0][1].ToString();
            adminpasswordtxt.Text = dt.Rows[0][2].ToString();
            adminemailidtxt.Text = dt.Rows[0][3].ToString();
            adminmobilenotxt.Text = dt.Rows[0][4].ToString();

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

        private void instructorGridGenerator()
        {
            instructorgridview.ColumnCount = 3;
            instructorgridview.Columns[0].Name = "rowid";
            instructorgridview.Columns[1].Name = "Name";
            instructorgridview.Columns[2].Name = "Mobile Number";
            instructorgridview.Columns[0].Visible = false;



            SQLiteCommand s = new SQLiteCommand("select rowid,Username as Name,MobileNo as \"Mobile Number\" from LoginTbl where Role='Instructor'; ", connection);
            using (SQLiteDataReader reader = s.ExecuteReader())
            {
                while (reader.Read())
                {
                    instructorgridview.Rows.Add(new object[] 
                        { 
                        reader.GetValue(reader.GetOrdinal("rowid")),  
                        reader.GetValue(reader.GetOrdinal("Name")),
                        reader.GetValue(reader.GetOrdinal("Mobile Number")),
                        });
                }
            }
        }

        private void ownername_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsLetter(ch) && ch != 8)
            {
                e.Handled = true;

            }
        }
        void mobilenumbertxt_Validating(object sender, CancelEventArgs e)
        {
            if (mobilenumbertxt.Text != "")
            {

                if (mobilenumbertxt.Text.Length < 10 || !IsPhoneNumber(mobilenumbertxt.Text))
                {

                    MetroMessageBox.Show(this, "Mobile-No not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    mobilenumbertxt.Clear();
                   
                }
            }
            else
            {

            }
        }
        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^[789]\d{9}$").Success;
        }

        private void emailidtxt_Validating(object sender, CancelEventArgs e)
        {
            if (emailidtxt.Text != "")
            {

                if (!IsValidEmail(emailidtxt.Text))
                {


                    MetroMessageBox.Show(this, "Email id not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                    emailidtxt.Clear();
                   
                }
            }
            else
            {

            }
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void mobilenumbertxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;



            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
         
            if (ownernametxt.Text == "")
            {

                MetroMessageBox.Show(this, "Owner name cannot be empty..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ownernametxt.Focus();
            }
            else if (passwordtxt.Text == "")
            {
                MetroMessageBox.Show(this, "Password cannot be empty..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                passwordtxt.Focus();
            }
          
            else if (emailidtxt.  Text == "")
            {
                MetroMessageBox.Show(this, "Enter your Email-id ..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                emailidtxt.Focus();
            }
            else if (mobilenumbertxt.Text == "")
            {
                MetroMessageBox.Show(this, "Enter your mobile number..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mobilenumbertxt.Focus();
            }
            else
            {
                SQLiteDataAdapter sda2 = new SQLiteDataAdapter();

                sda2.UpdateCommand = new SQLiteCommand("update LoginTbl  SET Username='" + ownernametxt.Text + "',Password='" + passwordtxt.Text + "' ,EmailId='" + emailidtxt.Text + "',MobileNo='" + mobilenumbertxt.Text + "' where Role= 'Owner'", connection);

                sda2.UpdateCommand.ExecuteNonQuery();
                MetroMessageBox.Show(this, "Owner information saved..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               

            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
              DialogResult result = MetroMessageBox.Show(this, "Deleting Owner will delete all the data such as Student's information, Fees history, etc.\n\nAre you sure you want to delete the owner..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

              if (result == DialogResult.Yes)
              {

                  SQLiteDataAdapter m = new SQLiteDataAdapter();

                  m.DeleteCommand = new SQLiteCommand("Delete from StudentTbl", connection);

                  m.DeleteCommand.ExecuteNonQuery();

                  m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl", connection);

                  m.DeleteCommand.ExecuteNonQuery();

                  m.DeleteCommand = new SQLiteCommand("Delete from InstallmentsTbl ", connection);

                  m.DeleteCommand.ExecuteNonQuery();

                  m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl ", connection);

                  m.DeleteCommand.ExecuteNonQuery();

                  m.DeleteCommand = new SQLiteCommand("Delete from CompensateTbl", connection);

                  m.DeleteCommand.ExecuteNonQuery();

                  m.DeleteCommand = new SQLiteCommand("Delete from LoginTbl", connection);

                  m.DeleteCommand.ExecuteNonQuery();

                  m.DeleteCommand = new SQLiteCommand("Delete from AttendanceTbl", connection);

                  m.DeleteCommand.ExecuteNonQuery();

                  MetroMessageBox.Show(this, "Owner and all data deleted successfully..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  Environment.Exit(0);

              }
        }

        private void save_Click(object sender, EventArgs e)
        {

            if (adminnametxt. Text == "")
            {

                MetroMessageBox.Show(this, "Admin name cannot be empty..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                adminnametxt.Focus();
            }
            else if (adminpasswordtxt.Text == "")
            {
                MetroMessageBox.Show(this, "Password cannot be empty..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               adminpasswordtxt.Focus();
            }

            else if (adminemailidtxt.Text == "")
            {
                MetroMessageBox.Show(this, "Enter your Email-id ..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                adminemailidtxt.Focus();
            }
            else if (adminmobilenotxt.Text == "")
            {
                MetroMessageBox.Show(this, "Enter your mobile number..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                adminmobilenotxt.Focus();
            }
            else
            {
                SQLiteDataAdapter sda2 = new SQLiteDataAdapter();

                sda2.UpdateCommand = new SQLiteCommand("update LoginTbl  SET Username='" + adminnametxt.Text + "',Password='" + adminpasswordtxt.Text + "' ,EmailId='" + adminemailidtxt.Text + "',MobileNo='" + adminmobilenotxt.Text + "' where Role= 'Admin'", connection);

                sda2.UpdateCommand.ExecuteNonQuery();
               
                MetroMessageBox.Show(this, "Admin information saved..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               
            }
        }

        private void ownernametxt_Validating(object sender, CancelEventArgs e)
        {
            if (ownernametxt.Text != "") {
                if (!(ownernametxt.Text.Length >= 3))
                {
                    MetroMessageBox.Show(this, "Enter a valid Owner name..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    ownernametxt.Clear();
                }
            }
        }

        private void passwordtxt_Validating(object sender, CancelEventArgs e)
        {
            if (passwordtxt.Text != "")
            {
                if (!(passwordtxt.Text.Length >= 6))
                {
                    MetroMessageBox.Show(this, "Enter a valid Password of atleast 6 digits..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    passwordtxt.Clear();

                }
            }
        }

        private void adminnametxt_Validating(object sender, CancelEventArgs e)
        {
            if (adminnametxt.Text != "")
            {
                if (!(adminnametxt.Text.Length >= 3))
                {
                    MetroMessageBox.Show(this, "Enter a valid Admin name..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    adminnametxt.Clear();

                }
            }
        }

        private void adminpasswordtxt_Validating(object sender, CancelEventArgs e)
        {
            if (adminpasswordtxt.Text != "")
            {
                if (!(adminpasswordtxt.Text.Length >= 6))
                {
                    MetroMessageBox.Show(this, "Enter a valid Password of atleast 6 digits..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    adminpasswordtxt.Clear();

                }
            }
        }

        private void adminmobilenotxt_Validating(object sender, CancelEventArgs e)
        {

            if (adminmobilenotxt.Text != "")
            {

                if (adminmobilenotxt.Text.Length < 10 || !IsPhoneNumber(adminmobilenotxt.Text))
                {

                    MetroMessageBox.Show(this, "Mobile-No not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    adminmobilenotxt.Clear();
                  
                }
            }
            else
            {

            }
        }
       

        private void adminnametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            char ch = e.KeyChar;
            if (!char.IsLetter(ch) && ch != 8)
            {
                e.Handled = true;

            }
        
        }

        private void adminmobilenotxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;



            }
        }

        private void adminemailidtxt_Validating(object sender, CancelEventArgs e)
        {
            if (adminemailidtxt.Text != "")
            {

                if (!IsValidEmail(adminemailidtxt.Text))
                {


                    MetroMessageBox.Show(this, "Email id not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                    adminemailidtxt.Clear();
                  
                }
            }
            else
            {

            }
        }

        private void Owner_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        private void tabcontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabcontrol.SelectedTab == tab1)
            {
                updatebtn.Focus();
            }
            else if(tabcontrol.SelectedTab==tab2){
                save.Focus();
            }
            else if (tabcontrol.SelectedTab == tab3) {
                instructorsave.Focus();
            }
            else if (tabcontrol.SelectedTab == tab4) {
                studentnametxt.Focus();
            }
        }

        private void mobileno_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;



            }
        }

        private void mobileno_Validating(object sender, CancelEventArgs e)
        {

          if (mobileno.Text != "")
            {

                if (mobileno.Text.Length < 10 || !IsPhoneNumber(mobileno.Text))
                {

                    MetroMessageBox.Show(this, "Mobile number not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    mobileno.Clear();

                }
            }
            else
            {

            }
        }

        private void instructorsave_Click(object sender, EventArgs e)
        {
            SQLiteDataAdapter sda = new SQLiteDataAdapter("select count(*) from LoginTbl where Role='Instructor'",connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() != "0")
            {
               
                    sda.UpdateCommand = new SQLiteCommand("update LoginTbl  SET Password='" + mobileno.Text + "' ,MobileNo='" + mobileno.Text + "' where Role= 'Instructor' and rowid='" + instructorrowid + "'", connection);

                    sda.UpdateCommand.ExecuteNonQuery(); 
                  
                    MetroMessageBox.Show(this, "Number saved..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    int a = instructorgridview.CurrentCell.RowIndex; 
                    instructorgridview.Rows.Clear();
                    instructorGridGenerator(); 
                    instructorgridview.Rows[a].Selected = true;
              

                }
            else
            {


                MetroMessageBox.Show(this, "No instructor found..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        string instructorrowid;

        private void instructorgridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {

                    DataGridViewRow row = this.instructorgridview.Rows[e.RowIndex];
                    instructorrowid = row.Cells["rowid"].Value.ToString();
                    mobileno.Text = row.Cells["Mobile Number"].Value.ToString();

                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void instructorgridview_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {

                    DataGridViewRow row = this.instructorgridview.Rows[e.RowIndex];
                    instructorrowid = row.Cells["rowid"].Value.ToString();
                    mobileno.Text = row.Cells["Mobile Number"].Value.ToString();

                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void send_Click(object sender, EventArgs e)
        {
            int Desc;
            if (InternetGetConnectedState(out Desc, 0))
            {
                if (studentnametxt.Text != "")
                {

                    SQLiteDataAdapter sda2 = new SQLiteDataAdapter("select count(rowid)  from StudentTbl where Fullname='" + camelCase(studentnametxt.Text) + "'", connection);
                    DataTable dg = new DataTable();
                    sda2.Fill(dg);
                    if (phonebook.SelectedIndex != -1)
                    {
                        if (Convert.ToInt32(dg.Rows[0][0]) > 0)
                        {
                            sda2 = new SQLiteDataAdapter("select Totalfees,Parentcontactno,Mobilenumber  from StudentTbl where rowid='" + studentlist[camelCase(studentnametxt.Text)] + "'", connection);
                            dg = new DataTable();
                            sda2.Fill(dg);
                            string totalfees = dg.Rows[0][0].ToString();


                            if (phonebook.Text == "Student")
                            {
                                contact = dg.Rows[0][2].ToString();
                                messagetxt.Text = "Dear student, This message is from Strings and Beats. You have paid amount Rs. " + totalfees + ". Thank you.";
                            }
                            else if (phonebook.Text == "Parent")
                            {
                                messagetxt.Text = "Dear parent, This message is from Strings and Beats. " + camelCase(studentnametxt.Text) + " has paid amount Rs. " + totalfees + ". Thank you.";
                                contact = dg.Rows[0][1].ToString();
                            }

                            WebClient client = new WebClient();
                            string baseurl = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=mantdemo&password=1038101106&sendername=SBEATS&mobileno=" + contact + "&message=" + messagetxt.Text;
                            Stream data = client.OpenRead(baseurl);
                            StreamReader reader = new StreamReader(data);
                            string s = reader.ReadToEnd();
                            data.Close();
                            reader.Close();


                            MetroMessageBox.Show(this, "Message sent successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }
                        else
                        {
                            MetroMessageBox.Show(this, "This student was not found..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            studentnametxt.Focus();

                        }
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "Select a receiver..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        phonebook.Focus();

                    }

                }
                else
                {
                    MetroMessageBox.Show(this, "Select student..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    studentnametxt.Focus();
                }

             }
            else
            {
                MetroMessageBox.Show(this, "Internet not Connected..!!\nConnect with Internet..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private string camelCase(string text) { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text); }
        string contact;
        private void phonebook_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (studentnametxt.Text != "")
            {
                
                    SQLiteDataAdapter sda2 = new SQLiteDataAdapter("select count(rowid)  from StudentTbl where Fullname='" + camelCase(studentnametxt.Text) + "'", connection);
                    DataTable dg = new DataTable();
                    sda2.Fill(dg);

                    if (Convert.ToInt32(dg.Rows[0][0]) > 0)
                    {
                        sda2 = new SQLiteDataAdapter("select Totalfees,Parentcontactno,Mobilenumber  from StudentTbl where rowid='" + studentlist[camelCase(studentnametxt.Text)] + "'", connection);
                        dg = new DataTable();
                        sda2.Fill(dg);
                        string totalfees = dg.Rows[0][0].ToString();
                      
                        
                        if (phonebook.Text == "Student") {
                            contact= dg.Rows[0][2].ToString();
                            messagetxt.Text = "Dear student, This message is from Strings and Beats. You have paid amount Rs. "+totalfees+". Thank you.";
                        }
                        else if (phonebook.Text == "Parent") {
                            messagetxt.Text = "Dear parent, This message is from Strings and Beats. " + camelCase(studentnametxt.Text) + " has paid amount Rs. "  + totalfees + ". Thank you.";
                            contact= dg.Rows[0][1].ToString();
                        }
                    }
            }
            else
            {
                MetroMessageBox.Show(this, "Select student..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                studentnametxt.Focus();
            }
        }

        private void instructorgridview_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (instructorgridview.Rows.Count != 0)
                    {

                        if (instructorgridview.CurrentRow.Index >= 0)
                        {


                            DataGridViewRow row = this.instructorgridview.Rows[instructorgridview.CurrentRow.Index];
                            DialogResult result = MetroMessageBox.Show(this, "Delete this record..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                            if (result == DialogResult.Yes)
                            {

                                SQLiteDataAdapter m = new SQLiteDataAdapter();
                                m.DeleteCommand = new SQLiteCommand("Delete from LoginTbl where Username='" + row.Cells["Name"].Value.ToString() + "' and Password='" + row.Cells["Mobile Number"].Value.ToString() + "'", connection);

                                m.DeleteCommand.ExecuteNonQuery();

                                mobileno.Clear();
                                int a = instructorgridview.CurrentCell.RowIndex; instructorgridview.Rows.Clear();
                                instructorGridGenerator();
                                if (a != 0)
                                {
                                    instructorgridview.Rows[a - 1].Selected = true;
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
