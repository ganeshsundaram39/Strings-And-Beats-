namespace Strings_and_Beats
{
    partial class Forgottenpassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.emailid = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.close = new MetroFramework.Controls.MetroButton();
            this.send = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(242, 60);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(196, 25);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "No Problem, it happens.";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // emailid
            // 
            this.emailid.Appearance = System.Windows.Forms.Appearance.Button;
            this.emailid.BackColor = System.Drawing.Color.Transparent;
            this.emailid.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.emailid.Location = new System.Drawing.Point(406, 199);
            this.emailid.Name = "emailid";
            this.emailid.Size = new System.Drawing.Size(239, 45);
            this.emailid.Style = MetroFramework.MetroColorStyle.Black;
            this.emailid.TabIndex = 7;
            this.emailid.TabStop = false;
            this.emailid.Text = "Email Id ";
            this.emailid.Theme = MetroFramework.MetroThemeStyle.Light;
            this.emailid.UseSelectable = true;
            this.emailid.UseStyleColors = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(397, 157);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(251, 25);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Black;
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "Receive your password using :";
            this.metroLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel2.UseStyleColors = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Strings_and_Beats.Properties.Resources.forgotpassword;
            this.pictureBox1.Location = new System.Drawing.Point(77, 110);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(263, 202);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // close
            // 
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.Location = new System.Drawing.Point(575, 264);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(78, 27);
            this.close.Style = MetroFramework.MetroColorStyle.Yellow;
            this.close.TabIndex = 11;
            this.close.TabStop = false;
            this.close.Text = "Close";
            this.close.Theme = MetroFramework.MetroThemeStyle.Light;
            this.close.UseSelectable = true;
            this.close.UseStyleColors = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // send
            // 
            this.send.Cursor = System.Windows.Forms.Cursors.Hand;
            this.send.Location = new System.Drawing.Point(485, 264);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(78, 27);
            this.send.Style = MetroFramework.MetroColorStyle.Yellow;
            this.send.TabIndex = 10;
            this.send.TabStop = false;
            this.send.Text = "Send";
            this.send.Theme = MetroFramework.MetroThemeStyle.Light;
            this.send.UseSelectable = true;
            this.send.UseStyleColors = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // Forgottenpassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(691, 333);
            this.Controls.Add(this.close);
            this.Controls.Add(this.send);
            this.Controls.Add(this.emailid);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.Movable = false;
            this.Name = "Forgottenpassword";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Forgot your password ??";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroCheckBox emailid;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroButton close;
        private MetroFramework.Controls.MetroButton send;
    }
}