using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strings_and_Beats
{
    public partial class NewOwner : MetroForm
    {
        public NewOwner()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.s;
            this.DoubleBuffered = true;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
          

            if (ownername.Text == "") {

                MetroMessageBox.Show(this, "Owner name cannot be empty..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ownername.Focus();
            }
            else if (password.Text == "")
            {
                MetroMessageBox.Show(this, "Password cannot be empty..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                password.Focus();
            }
            else if (reenterpassword.Text == "")
            {
                MetroMessageBox.Show(this, "Enter password again..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                reenterpassword.Focus();  
            }
            else if (emailid.  Text == "")
            {
                MetroMessageBox.Show(this, "Enter your Email-id ..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                emailid.Focus();
            }
            else if (mobileno  .Text == "")
            {
                MetroMessageBox.Show(this, "Enter your mobile number..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mobileno.Focus();
            }
            else
            {

                if (password.Text.Equals(reenterpassword.Text))
                {

                    string connectionString = Connection.getConnection();


                    SQLiteConnection connection = new SQLiteConnection(connectionString);
                    SQLiteCommand command;
                    connection.Open();

                    command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO LoginTbl VALUES ('Owner','" + emailid.Text + "','" + mobileno.Text + "','" + ownername.Text + "','" + password.Text + "');";


                    command.ExecuteNonQuery();
                    command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO LoginTbl VALUES ('Admin','','','','');";


                    command.ExecuteNonQuery();

                    MetroMessageBox.Show(this, "Owner added..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Visible = false;
                    new Login().Show();
                  
                }
                else
                {
                    password.Clear();
                    reenterpassword.Clear();
                    password.Focus();
                    MetroMessageBox.Show(this, "Password didn't matched enter again..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                }
            }
        }

        private void productname_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsLetter(ch) && ch != 8)
            {
                e.Handled = true;

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

                    MetroMessageBox.Show(this, "Mobile-No not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    mobileno.Clear();
                   
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
        private void emailid_Validating(object sender, CancelEventArgs e)
        {
            if (emailid.Text != "")
            {
             
                if (!IsValidEmail(emailid.Text))
                {


                    MetroMessageBox.Show(this, "Email id not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                    emailid.Clear();
            
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

        private void ownername_Validating(object sender, CancelEventArgs e)
        {
            if (ownername.Text != "")
            {

                if (!(ownername.Text.Length >= 3))
                {
                    MetroMessageBox.Show(this, "Enter a valid Owner name..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    ownername.Clear();

                }
            }
        }

        private void password_Validating(object sender, CancelEventArgs e)
        {
            if(password.Text!=""){
            if (!(password.Text.Length >= 6))
            {
                MetroMessageBox.Show(this, "Enter a valid Password of atleast 6 digits ..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                password.Clear();

            }}
        }

        private void reenterpassword_Validating(object sender, CancelEventArgs e)
        {
           
        }

     
 

       
    }
}
