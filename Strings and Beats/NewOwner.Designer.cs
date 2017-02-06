namespace Strings_and_Beats
{
    partial class NewOwner
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.close = new MetroFramework.Controls.MetroButton();
            this.save = new MetroFramework.Controls.MetroButton();
            this.emailid = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.mobileno = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.password = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.ownername = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.reenterpassword = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Strings_and_Beats.Properties.Resources.owner;
            this.pictureBox1.Location = new System.Drawing.Point(28, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(290, 316);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // close
            // 
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.Location = new System.Drawing.Point(700, 347);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(73, 24);
            this.close.Style = MetroFramework.MetroColorStyle.Yellow;
            this.close.TabIndex = 7;
            this.close.Text = "Close";
            this.close.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.close.UseSelectable = true;
            this.close.UseStyleColors = true;
            this.close.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // save
            // 
            this.save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.save.Location = new System.Drawing.Point(610, 347);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(73, 24);
            this.save.Style = MetroFramework.MetroColorStyle.Yellow;
            this.save.TabIndex = 6;
            this.save.Text = "Save";
            this.save.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.save.UseSelectable = true;
            this.save.UseStyleColors = true;
            this.save.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // emailid
            // 
            this.emailid.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            // 
            // 
            // 
            this.emailid.CustomButton.Image = null;
            this.emailid.CustomButton.Location = new System.Drawing.Point(233, 1);
            this.emailid.CustomButton.Name = "";
            this.emailid.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.emailid.CustomButton.Style = MetroFramework.MetroColorStyle.Yellow;
            this.emailid.CustomButton.TabIndex = 1;
            this.emailid.CustomButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.emailid.CustomButton.UseSelectable = true;
            this.emailid.CustomButton.Visible = false;
            this.emailid.DisplayIcon = true;
            this.emailid.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.emailid.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.emailid.ForeColor = System.Drawing.Color.Snow;
            this.emailid.Icon = global::Strings_and_Beats.Properties.Resources.email;
            this.emailid.IconRight = true;
            this.emailid.Lines = new string[0];
            this.emailid.Location = new System.Drawing.Point(515, 293);
            this.emailid.MaxLength = 32767;
            this.emailid.Name = "emailid";
            this.emailid.PasswordChar = '\0';
            this.emailid.PromptText = "enter valid email-id !!";
            this.emailid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.emailid.SelectedText = "";
            this.emailid.SelectionLength = 0;
            this.emailid.SelectionStart = 0;
            this.emailid.ShortcutsEnabled = false;
            this.emailid.Size = new System.Drawing.Size(259, 27);
            this.emailid.Style = MetroFramework.MetroColorStyle.Yellow;
            this.emailid.TabIndex = 5;
            this.emailid.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.emailid.UseCustomForeColor = true;
            this.emailid.UseSelectable = true;
            this.emailid.WaterMark = "enter valid email-id !!";
            this.emailid.WaterMarkColor = System.Drawing.Color.Silver;
            this.emailid.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailid.Validating += new System.ComponentModel.CancelEventHandler(this.emailid_Validating);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(420, 292);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(86, 25);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroLabel4.TabIndex = 16;
            this.metroLabel4.Text = "Email-Id :";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel4.UseStyleColors = true;
            // 
            // mobileno
            // 
            this.mobileno.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            // 
            // 
            // 
            this.mobileno.CustomButton.Image = null;
            this.mobileno.CustomButton.Location = new System.Drawing.Point(233, 1);
            this.mobileno.CustomButton.Name = "";
            this.mobileno.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.mobileno.CustomButton.Style = MetroFramework.MetroColorStyle.Yellow;
            this.mobileno.CustomButton.TabIndex = 1;
            this.mobileno.CustomButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.mobileno.CustomButton.UseSelectable = true;
            this.mobileno.CustomButton.Visible = false;
            this.mobileno.DisplayIcon = true;
            this.mobileno.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mobileno.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.mobileno.ForeColor = System.Drawing.Color.Snow;
            this.mobileno.Icon = global::Strings_and_Beats.Properties.Resources.phone;
            this.mobileno.IconRight = true;
            this.mobileno.Lines = new string[0];
            this.mobileno.Location = new System.Drawing.Point(515, 243);
            this.mobileno.MaxLength = 10;
            this.mobileno.Name = "mobileno";
            this.mobileno.PasswordChar = '\0';
            this.mobileno.PromptText = "enter your mobile number !!";
            this.mobileno.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mobileno.SelectedText = "";
            this.mobileno.SelectionLength = 0;
            this.mobileno.SelectionStart = 0;
            this.mobileno.ShortcutsEnabled = false;
            this.mobileno.Size = new System.Drawing.Size(259, 27);
            this.mobileno.Style = MetroFramework.MetroColorStyle.Yellow;
            this.mobileno.TabIndex = 4;
            this.mobileno.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.mobileno.UseCustomForeColor = true;
            this.mobileno.UseSelectable = true;
            this.mobileno.WaterMark = "enter your mobile number !!";
            this.mobileno.WaterMarkColor = System.Drawing.Color.Silver;
            this.mobileno.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mobileno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mobileno_KeyPress);
            this.mobileno.Validating += new System.ComponentModel.CancelEventHandler(this.mobileno_Validating);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(399, 242);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(107, 25);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroLabel3.TabIndex = 14;
            this.metroLabel3.Text = "Mobile-No :";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel3.UseStyleColors = true;
            // 
            // password
            // 
            this.password.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            // 
            // 
            // 
            this.password.CustomButton.Image = null;
            this.password.CustomButton.Location = new System.Drawing.Point(233, 1);
            this.password.CustomButton.Name = "";
            this.password.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.password.CustomButton.Style = MetroFramework.MetroColorStyle.Yellow;
            this.password.CustomButton.TabIndex = 1;
            this.password.CustomButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.password.CustomButton.UseSelectable = true;
            this.password.CustomButton.Visible = false;
            this.password.DisplayIcon = true;
            this.password.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.password.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.password.ForeColor = System.Drawing.Color.Snow;
            this.password.Icon = global::Strings_and_Beats.Properties.Resources.password;
            this.password.IconRight = true;
            this.password.Lines = new string[0];
            this.password.Location = new System.Drawing.Point(515, 143);
            this.password.MaxLength = 32767;
            this.password.Name = "password";
            this.password.PasswordChar = '•';
            this.password.PromptText = "enter strong password !!";
            this.password.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.password.SelectedText = "";
            this.password.SelectionLength = 0;
            this.password.SelectionStart = 0;
            this.password.ShortcutsEnabled = false;
            this.password.Size = new System.Drawing.Size(259, 27);
            this.password.Style = MetroFramework.MetroColorStyle.Yellow;
            this.password.TabIndex = 2;
            this.password.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.password.UseCustomForeColor = true;
            this.password.UseSelectable = true;
            this.password.WaterMark = "enter strong password !!";
            this.password.WaterMarkColor = System.Drawing.Color.Silver;
            this.password.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.Validating += new System.ComponentModel.CancelEventHandler(this.password_Validating);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(409, 142);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(97, 25);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroLabel2.TabIndex = 12;
            this.metroLabel2.Text = "Password :";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel2.UseStyleColors = true;
            // 
            // ownername
            // 
            this.ownername.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            // 
            // 
            // 
            this.ownername.CustomButton.Image = null;
            this.ownername.CustomButton.Location = new System.Drawing.Point(233, 1);
            this.ownername.CustomButton.Name = "";
            this.ownername.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.ownername.CustomButton.Style = MetroFramework.MetroColorStyle.Yellow;
            this.ownername.CustomButton.TabIndex = 1;
            this.ownername.CustomButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ownername.CustomButton.UseSelectable = true;
            this.ownername.CustomButton.Visible = false;
            this.ownername.DisplayIcon = true;
            this.ownername.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.ownername.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.ownername.ForeColor = System.Drawing.Color.Snow;
            this.ownername.Icon = global::Strings_and_Beats.Properties.Resources.username;
            this.ownername.IconRight = true;
            this.ownername.Lines = new string[0];
            this.ownername.Location = new System.Drawing.Point(515, 93);
            this.ownername.MaxLength = 32767;
            this.ownername.Name = "ownername";
            this.ownername.PasswordChar = '\0';
            this.ownername.PromptText = "what\'s your name ??";
            this.ownername.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ownername.SelectedText = "";
            this.ownername.SelectionLength = 0;
            this.ownername.SelectionStart = 0;
            this.ownername.ShortcutsEnabled = false;
            this.ownername.Size = new System.Drawing.Size(259, 27);
            this.ownername.Style = MetroFramework.MetroColorStyle.Yellow;
            this.ownername.TabIndex = 1;
            this.ownername.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ownername.UseCustomForeColor = true;
            this.ownername.UseSelectable = true;
            this.ownername.WaterMark = "what\'s your name ??";
            this.ownername.WaterMarkColor = System.Drawing.Color.Silver;
            this.ownername.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ownername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.productname_KeyPress);
            this.ownername.Validating += new System.ComponentModel.CancelEventHandler(this.ownername_Validating);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(381, 92);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(125, 25);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroLabel1.TabIndex = 10;
            this.metroLabel1.Text = "Owner Name :";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel1.UseStyleColors = true;
            // 
            // reenterpassword
            // 
            this.reenterpassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            // 
            // 
            // 
            this.reenterpassword.CustomButton.Image = null;
            this.reenterpassword.CustomButton.Location = new System.Drawing.Point(233, 1);
            this.reenterpassword.CustomButton.Name = "";
            this.reenterpassword.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.reenterpassword.CustomButton.Style = MetroFramework.MetroColorStyle.Yellow;
            this.reenterpassword.CustomButton.TabIndex = 1;
            this.reenterpassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.reenterpassword.CustomButton.UseSelectable = true;
            this.reenterpassword.CustomButton.Visible = false;
            this.reenterpassword.DisplayIcon = true;
            this.reenterpassword.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.reenterpassword.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.reenterpassword.ForeColor = System.Drawing.Color.Snow;
            this.reenterpassword.Icon = global::Strings_and_Beats.Properties.Resources.password;
            this.reenterpassword.IconRight = true;
            this.reenterpassword.Lines = new string[0];
            this.reenterpassword.Location = new System.Drawing.Point(515, 193);
            this.reenterpassword.MaxLength = 32767;
            this.reenterpassword.Name = "reenterpassword";
            this.reenterpassword.PasswordChar = '•';
            this.reenterpassword.PromptText = "re-enter password !!";
            this.reenterpassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.reenterpassword.SelectedText = "";
            this.reenterpassword.SelectionLength = 0;
            this.reenterpassword.SelectionStart = 0;
            this.reenterpassword.ShortcutsEnabled = false;
            this.reenterpassword.Size = new System.Drawing.Size(259, 27);
            this.reenterpassword.Style = MetroFramework.MetroColorStyle.Yellow;
            this.reenterpassword.TabIndex = 3;
            this.reenterpassword.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.reenterpassword.UseCustomForeColor = true;
            this.reenterpassword.UseSelectable = true;
            this.reenterpassword.WaterMark = "re-enter password !!";
            this.reenterpassword.WaterMarkColor = System.Drawing.Color.Silver;
            this.reenterpassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reenterpassword.Validating += new System.ComponentModel.CancelEventHandler(this.reenterpassword_Validating);
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.Location = new System.Drawing.Point(340, 192);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(166, 25);
            this.metroLabel5.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroLabel5.TabIndex = 22;
            this.metroLabel5.Text = "Confirm Password :";
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel5.UseStyleColors = true;
            // 
            // NewOwner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(801, 395);
            this.Controls.Add(this.close);
            this.Controls.Add(this.save);
            this.Controls.Add(this.emailid);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.mobileno);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.password);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.ownername);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.reenterpassword);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.MaximizeBox = false;
            this.Movable = false;
            this.Name = "NewOwner";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Add New Owner .. !!";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroButton close;
        private MetroFramework.Controls.MetroButton save;
        private MetroFramework.Controls.MetroTextBox emailid;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroTextBox mobileno;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroTextBox password;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox ownername;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox reenterpassword;
        private MetroFramework.Controls.MetroLabel metroLabel5;
    }
}