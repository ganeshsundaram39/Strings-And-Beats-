using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strings_and_Beats
{
    public partial class Forgottenpassword : MetroForm
    {
        [DllImport("wininet.dll")]

        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        string username, usertype,email,pass;
        public Forgottenpassword(String username,String usertype)
        {
            InitializeComponent();
            
            this.DoubleBuffered = true;
            this.username = username;
            this.usertype = usertype;
            string connectionString = Connection.getConnection();
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            try
            {

                SQLiteDataAdapter sda = new SQLiteDataAdapter("select EmailId ,Password from LoginTbl WHERE Role='" + usertype + "'", connection);

                DataTable dt = new DataTable();

                sda.Fill(dt);
                this.email = dt.Rows[0][0].ToString();
            
                this.pass = dt.Rows[0][1].ToString();
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

            finally
            {
               
                connection.Close();

            }

            emailid.Text = email;
        }

        private void send_Click(object sender, EventArgs e)
        {
            int Desc;
            if (InternetGetConnectedState(out Desc, 0))
            {
                Cursor.Current = Cursors.WaitCursor;
                if (emailid.Checked)
                {
                    this.sendMail();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MetroMessageBox.Show(this, "Select a medium..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }


            }
            else
            {
                MetroMessageBox.Show(this, "Internet not Connected..!!\nConnect with Internet..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private  void sendMail()
        {
            
       

                if (email != "")
                {
                    string s ="Hello "+usertype.ToLower()+ " "+ username.ToLower() + ",\nYour password is " + pass+".\nThank you,\nStrings And Beats.";
                    MailMessage mail = new MailMessage("stringsandbeatss@gmail.com ", email, "Password Recovery Email", s);
                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    client.Port = 587;

                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential("stringsandbeatss@gmail.com", "imran@@123$$%%");
                    client.Send(mail);
              
                    MetroMessageBox.Show(this, "Mail Sent..!!\nCheck Mail..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                {

                    MetroMessageBox.Show(this, "Mail Not Sent..!! \n Mail Id not found..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                Cursor.Current = Cursors.Default;
        }
    

      

    }
}
