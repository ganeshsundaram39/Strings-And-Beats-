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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strings_and_Beats
{
    public partial class Login : MetroForm
    {
        String name, type;
        static string connectionString = Connection.getConnection();
        SQLiteConnection connection = new SQLiteConnection(connectionString);
        public Login()
        {
            InitializeComponent();
            connection.Open();
            this.Icon = Properties.Resources.s;
            this.DoubleBuffered = true;
            usertype.SelectedIndex = 0;
        }

        private void save_Click(object sender, EventArgs e)
        {
            forgotpassword.Hide();
            name = "";
            type = "";
        


                if (usertype.SelectedIndex == -1)
                {
                    MetroMessageBox.Show(this, "Select User type..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    usertype.Focus();
                }
                else if (username.Text == "")
                {
                    MetroMessageBox.Show(this, "Enter Username..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    username.Focus();
                }
                else if (password.Text == "")
                {
                    MetroMessageBox.Show(this, "Enter password..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    username.Focus();
                }
                else
                {
                    SQLiteCommand command;
                
                    command = connection.CreateCommand();
                    command.CommandText = "SELECT count(*) FROM LoginTbl where Role='" + usertype.Text + "' and Username='" + username.Text + "'  and Password='" + password.Text + "';";
                    var i = command.ExecuteScalar();
                    if (Convert.ToInt16(i) == 1)
                    {
                        if (usertype.SelectedIndex != 2)
                        {
                            username.Clear();
                            password.Clear();
                           
                            this.Hide();


                            Mainform m = new Mainform(usertype.Text); usertype.SelectedIndex = 0;
                           m.RefTologinform = this;
                           m.Show();
                        }
                        else
                        {
                           
                           
                            this.Hide();
                            Search m = new Search(username.Text); usertype.SelectedIndex = 0; username.Clear();
                            password.Clear();
                            m.RefTologinform = this;
                            m.Show();
                    
                        }
                    }
                    else
                    {
                        command.CommandText = "SELECT count(*) FROM LoginTbl where Role='" + usertype.Text + "' and Username='" + username.Text + "' and EmailId!='';";
                        var j = command.ExecuteScalar();
                        if (Convert.ToInt16(j) == 1)
                        {
                            MetroMessageBox.Show(this, "Username or Password not correct..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            if (usertype.SelectedIndex != 2)
                            {
                                forgotpassword.Show();
                                name = username.Text;
                                type = usertype.Text;
                            }
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "Username or Password not correct..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }

                    }
                }
            }
        

        private void close_Click(object sender, EventArgs e)
        {
            connection.Close();
            Environment.Exit(0); 
        }

        private void usertype_Click(object sender, EventArgs e)
        {
            forgotpassword.Hide();
        }

        private void username_Click(object sender, EventArgs e)
        {
            forgotpassword.Hide();
        }

        private void password_Click(object sender, EventArgs e)
        {

            forgotpassword.Hide();
        }

        private void save_Validating(object sender, CancelEventArgs e)
        {
            forgotpassword.Hide();
        }

        private void forgotpassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (name != "" && type != "")
                new Forgottenpassword(name,type).ShowDialog();
        }

        private void usertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            username.Clear();
            password.Clear();
            forgotpassword.Visible = false;
            if (usertype.SelectedIndex == 2)
            {
                autoComplete();
            }
            else
            {
                username.AutoCompleteMode = AutoCompleteMode.None;

            }
        }

        public void autoComplete()
        {
            username.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            username.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            try
            {

                SQLiteCommand s = new SQLiteCommand("select * from LoginTbl where Role='Instructor'", connection);
                SQLiteDataReader reader = s.ExecuteReader();
                while (reader.Read())
                {
                    coll.Add(reader.GetString(reader.GetOrdinal("Username")));
                }

                username.AutoCompleteCustomSource = coll;
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
         

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
            Environment.Exit(0);
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
