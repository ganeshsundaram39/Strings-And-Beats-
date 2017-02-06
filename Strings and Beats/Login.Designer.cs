namespace Strings_and_Beats
{
    partial class Login
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
            this.username = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.password = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.usertype = new MetroFramework.Controls.MetroComboBox();
            this.close = new MetroFramework.Controls.MetroButton();
            this.save = new MetroFramework.Controls.MetroButton();
            this.forgotpassword = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Strings_and_Beats.Properties.Resources.login;
            this.pictureBox1.Location = new System.Drawing.Point(61, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(272, 281);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // username
            // 
            this.username.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            // 
            // 
            // 
            this.username.CustomButton.Image = null;
            this.username.CustomButton.Location = new System.Drawing.Point(233, 1);
            this.username.CustomButton.Name = "";
            this.username.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.username.CustomButton.Style = MetroFramework.MetroColorStyle.Yellow;
            this.username.CustomButton.TabIndex = 1;
            this.username.CustomButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.username.CustomButton.UseSelectable = true;
            this.username.CustomButton.Visible = false;
            this.username.DisplayIcon = true;
            this.username.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.username.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.username.ForeColor = System.Drawing.Color.Snow;
            this.username.Icon = global::Strings_and_Beats.Properties.Resources.username;
            this.username.IconRight = true;
            this.username.Lines = new string[0];
            this.username.Location = new System.Drawing.Point(468, 140);
            this.username.MaxLength = 32767;
            this.username.Name = "username";
            this.username.PasswordChar = '\0';
            this.username.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.username.SelectedText = "";
            this.username.SelectionLength = 0;
            this.username.SelectionStart = 0;
            this.username.ShortcutsEnabled = false;
            this.username.Size = new System.Drawing.Size(259, 27);
            this.username.Style = MetroFramework.MetroColorStyle.Yellow;
            this.username.TabIndex = 2;
            this.username.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.username.UseCustomForeColor = true;
            this.username.UseSelectable = true;
            this.username.WaterMarkColor = System.Drawing.Color.Silver;
            this.username.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.Click += new System.EventHandler(this.username_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(349, 139);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(100, 25);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroLabel2.TabIndex = 27;
            this.metroLabel2.Text = "Username :";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel2.UseStyleColors = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(350, 89);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(99, 25);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroLabel1.TabIndex = 26;
            this.metroLabel1.Text = "User Type :";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel1.UseStyleColors = true;
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
            this.password.Location = new System.Drawing.Point(468, 190);
            this.password.MaxLength = 32767;
            this.password.Name = "password";
            this.password.PasswordChar = '•';
            this.password.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.password.SelectedText = "";
            this.password.SelectionLength = 0;
            this.password.SelectionStart = 0;
            this.password.ShortcutsEnabled = false;
            this.password.Size = new System.Drawing.Size(259, 27);
            this.password.Style = MetroFramework.MetroColorStyle.Yellow;
            this.password.TabIndex = 3;
            this.password.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.password.UseCustomForeColor = true;
            this.password.UseSelectable = true;
            this.password.WaterMarkColor = System.Drawing.Color.Silver;
            this.password.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.Click += new System.EventHandler(this.password_Click);
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.Location = new System.Drawing.Point(352, 189);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(97, 25);
            this.metroLabel5.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroLabel5.TabIndex = 28;
            this.metroLabel5.Text = "Password :";
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroLabel5.UseStyleColors = true;
            // 
            // usertype
            // 
            this.usertype.FormattingEnabled = true;
            this.usertype.ItemHeight = 23;
            this.usertype.Items.AddRange(new object[] {
            "Owner",
            "Admin",
            "Instructor"});
            this.usertype.Location = new System.Drawing.Point(468, 88);
            this.usertype.Name = "usertype";
            this.usertype.Size = new System.Drawing.Size(259, 29);
            this.usertype.Style = MetroFramework.MetroColorStyle.Yellow;
            this.usertype.TabIndex = 1;
            this.usertype.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.usertype.UseSelectable = true;
            this.usertype.UseStyleColors = true;
            this.usertype.SelectedIndexChanged += new System.EventHandler(this.usertype_SelectedIndexChanged);
            this.usertype.Click += new System.EventHandler(this.usertype_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(650, 248);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(73, 24);
            this.close.Style = MetroFramework.MetroColorStyle.Yellow;
            this.close.TabIndex = 5;
            this.close.Text = "Close";
            this.close.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.close.UseSelectable = true;
            this.close.UseStyleColors = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(560, 248);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(73, 24);
            this.save.Style = MetroFramework.MetroColorStyle.Yellow;
            this.save.TabIndex = 4;
            this.save.Text = "&Login";
            this.save.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.save.UseSelectable = true;
            this.save.UseStyleColors = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            this.save.Validating += new System.ComponentModel.CancelEventHandler(this.save_Validating);
            // 
            // forgotpassword
            // 
            this.forgotpassword.AutoSize = true;
            this.forgotpassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.forgotpassword.Font = new System.Drawing.Font("Century", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.forgotpassword.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.forgotpassword.LinkColor = System.Drawing.Color.Red;
            this.forgotpassword.Location = new System.Drawing.Point(511, 296);
            this.forgotpassword.Name = "forgotpassword";
            this.forgotpassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.forgotpassword.Size = new System.Drawing.Size(218, 22);
            this.forgotpassword.TabIndex = 3333;
            this.forgotpassword.TabStop = true;
            this.forgotpassword.Text = "Forgotten Password ??";
            this.forgotpassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.forgotpassword.Visible = false;
            this.forgotpassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forgotpassword_LinkClicked);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(755, 348);
            this.Controls.Add(this.forgotpassword);
            this.Controls.Add(this.close);
            this.Controls.Add(this.save);
            this.Controls.Add(this.username);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.usertype);
            this.MaximizeBox = false;
            this.Movable = false;
            this.Name = "Login";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Login";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroTextBox username;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox password;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroComboBox usertype;
        private MetroFramework.Controls.MetroButton close;
        private MetroFramework.Controls.MetroButton save;
        private System.Windows.Forms.LinkLabel forgotpassword;

    }
}