using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strings_and_Beats
{
    public partial class Mainform : MetroForm
    {
        public Form RefTologinform { get; set; }
        string usertype;
        public Mainform(string usertype)
        {
            InitializeComponent();
            this.usertype = usertype;
            this.Icon = Properties.Resources.s;
            this.DoubleBuffered = true;

        }

        private void mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MetroMessageBox.Show(this, "Do you really want to exit ?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (result == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            new NewStudent().ShowDialog();
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            if (usertype != "Admin")
            {
                new EditStudent().ShowDialog();
            }
            else {
                MetroMessageBox.Show(this, "Admin can't edit..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void metroTile4_Click(object sender, EventArgs e)
        {
            new Search("icamefrommain").ShowDialog();
        }

        private void metroTile5_Click(object sender, EventArgs e)
        {
            new Fees().ShowDialog();
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            new Attendance().ShowDialog();
        }

        private void metroTile6_Click(object sender, EventArgs e)
        {
            if (usertype != "Admin")
            {
                new Owner().ShowDialog();
            }
            else
            {
                MetroMessageBox.Show(this, "This is owner zone..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.RefTologinform.Visible = true;

            this.Visible = false;
          
        
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Cursor = Cursors.Default;
        }

        private void metroTile7_Click(object sender, EventArgs e)
        {
            new Report().ShowDialog();
        }
    }
}
