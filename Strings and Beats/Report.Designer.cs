namespace Strings_and_Beats
{
    partial class Report
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
            this.askpanel = new System.Windows.Forms.Panel();
            this.reportpanel = new System.Windows.Forms.Panel();
            this.back = new MetroFramework.Controls.MetroButton();
            this.gradelbl = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.studentnamelbl = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.metroLabel17 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.numericyear = new iTalk.iTalk_NumericUpDown();
            this.next = new MetroFramework.Controls.MetroButton();
            this.studentnametxt = new MetroFramework.Controls.MetroTextBox();
            this.askpanel.SuspendLayout();
            this.reportpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // askpanel
            // 
            this.askpanel.Controls.Add(this.reportpanel);
            this.askpanel.Controls.Add(this.pictureBox1);
            this.askpanel.Controls.Add(this.metroLabel17);
            this.askpanel.Controls.Add(this.metroLabel4);
            this.askpanel.Controls.Add(this.numericyear);
            this.askpanel.Controls.Add(this.next);
            this.askpanel.Controls.Add(this.studentnametxt);
            this.askpanel.Location = new System.Drawing.Point(20, 60);
            this.askpanel.Name = "askpanel";
            this.askpanel.Size = new System.Drawing.Size(890, 491);
            this.askpanel.TabIndex = 0;
            // 
            // reportpanel
            // 
            this.reportpanel.Controls.Add(this.back);
            this.reportpanel.Controls.Add(this.gradelbl);
            this.reportpanel.Controls.Add(this.metroLabel3);
            this.reportpanel.Controls.Add(this.studentnamelbl);
            this.reportpanel.Controls.Add(this.metroLabel2);
            this.reportpanel.Controls.Add(this.cartesianChart1);
            this.reportpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportpanel.Location = new System.Drawing.Point(0, 0);
            this.reportpanel.Name = "reportpanel";
            this.reportpanel.Size = new System.Drawing.Size(890, 491);
            this.reportpanel.TabIndex = 111;
            this.reportpanel.Visible = false;
            // 
            // back
            // 
            this.back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.back.Location = new System.Drawing.Point(10, 453);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(73, 24);
            this.back.Style = MetroFramework.MetroColorStyle.Yellow;
            this.back.TabIndex = 107;
            this.back.TabStop = false;
            this.back.Text = "Back";
            this.back.Theme = MetroFramework.MetroThemeStyle.Light;
            this.back.UseMnemonic = false;
            this.back.UseSelectable = true;
            this.back.UseStyleColors = true;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // gradelbl
            // 
            this.gradelbl.AutoSize = true;
            this.gradelbl.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.gradelbl.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.gradelbl.Location = new System.Drawing.Point(627, 3);
            this.gradelbl.Name = "gradelbl";
            this.gradelbl.Size = new System.Drawing.Size(38, 25);
            this.gradelbl.Style = MetroFramework.MetroColorStyle.Black;
            this.gradelbl.TabIndex = 112;
            this.gradelbl.Text = "A+";
            this.gradelbl.Theme = MetroFramework.MetroThemeStyle.Light;
            this.gradelbl.UseStyleColors = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(561, 3);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(68, 25);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroLabel3.TabIndex = 111;
            this.metroLabel3.Text = "Grade :";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel3.UseStyleColors = true;
            // 
            // studentnamelbl
            // 
            this.studentnamelbl.AutoSize = true;
            this.studentnamelbl.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.studentnamelbl.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.studentnamelbl.Location = new System.Drawing.Point(218, 3);
            this.studentnamelbl.Name = "studentnamelbl";
            this.studentnamelbl.Size = new System.Drawing.Size(166, 25);
            this.studentnamelbl.Style = MetroFramework.MetroColorStyle.Black;
            this.studentnamelbl.TabIndex = 110;
            this.studentnamelbl.Text = "Ganesh Sundaram";
            this.studentnamelbl.Theme = MetroFramework.MetroThemeStyle.Light;
            this.studentnamelbl.UseStyleColors = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(152, 3);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(68, 25);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroLabel2.TabIndex = 109;
            this.metroLabel2.Text = "Name :";
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel2.UseStyleColors = true;
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(3, 30);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(884, 422);
            this.cartesianChart1.TabIndex = 108;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Strings_and_Beats.Properties.Resources.Report1;
            this.pictureBox1.Location = new System.Drawing.Point(67, 99);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(296, 252);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 105;
            this.pictureBox1.TabStop = false;
            // 
            // metroLabel17
            // 
            this.metroLabel17.AutoSize = true;
            this.metroLabel17.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel17.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel17.Location = new System.Drawing.Point(455, 227);
            this.metroLabel17.Name = "metroLabel17";
            this.metroLabel17.Size = new System.Drawing.Size(134, 25);
            this.metroLabel17.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroLabel17.TabIndex = 103;
            this.metroLabel17.Text = "Select Student :";
            this.metroLabel17.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel17.UseStyleColors = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(483, 180);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(106, 25);
            this.metroLabel4.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroLabel4.TabIndex = 104;
            this.metroLabel4.Text = "Select Year :";
            this.metroLabel4.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel4.UseStyleColors = true;
            // 
            // numericyear
            // 
            this.numericyear.BackColor = System.Drawing.Color.Transparent;
            this.numericyear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numericyear.Font = new System.Drawing.Font("Tahoma", 11F);
            this.numericyear.ForeColor = System.Drawing.Color.Black;
            this.numericyear.Location = new System.Drawing.Point(605, 180);
            this.numericyear.Maximum = ((long)(2016));
            this.numericyear.Minimum = ((long)(0));
            this.numericyear.MinimumSize = new System.Drawing.Size(62, 28);
            this.numericyear.Name = "numericyear";
            this.numericyear.Size = new System.Drawing.Size(237, 28);
            this.numericyear.TabIndex = 110;
            this.numericyear.TabStop = false;
            this.numericyear.Text = "iTalk_NumericUpDown1";
            this.numericyear.TextAlignment = iTalk.iTalk_NumericUpDown._TextAlignment.Near;
            this.numericyear.Value = ((long)(2016));
            // 
            // next
            // 
            this.next.Cursor = System.Windows.Forms.Cursors.Hand;
            this.next.Location = new System.Drawing.Point(769, 273);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(73, 24);
            this.next.Style = MetroFramework.MetroColorStyle.Yellow;
            this.next.TabIndex = 102;
            this.next.Text = "Next";
            this.next.Theme = MetroFramework.MetroThemeStyle.Light;
            this.next.UseSelectable = true;
            this.next.UseStyleColors = true;
            this.next.Click += new System.EventHandler(this.next_Click);
            // 
            // studentnametxt
            // 
            // 
            // 
            // 
            this.studentnametxt.CustomButton.Image = null;
            this.studentnametxt.CustomButton.Location = new System.Drawing.Point(209, 1);
            this.studentnametxt.CustomButton.Name = "";
            this.studentnametxt.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.studentnametxt.CustomButton.Style = MetroFramework.MetroColorStyle.Yellow;
            this.studentnametxt.CustomButton.TabIndex = 1;
            this.studentnametxt.CustomButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.studentnametxt.CustomButton.UseSelectable = true;
            this.studentnametxt.CustomButton.Visible = false;
            this.studentnametxt.DisplayIcon = true;
            this.studentnametxt.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.studentnametxt.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.studentnametxt.ForeColor = System.Drawing.Color.Black;
            this.studentnametxt.IconRight = true;
            this.studentnametxt.Lines = new string[0];
            this.studentnametxt.Location = new System.Drawing.Point(605, 226);
            this.studentnametxt.MaxLength = 32767;
            this.studentnametxt.Name = "studentnametxt";
            this.studentnametxt.PasswordChar = '\0';
            this.studentnametxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.studentnametxt.SelectedText = "";
            this.studentnametxt.SelectionLength = 0;
            this.studentnametxt.SelectionStart = 0;
            this.studentnametxt.ShortcutsEnabled = false;
            this.studentnametxt.Size = new System.Drawing.Size(237, 29);
            this.studentnametxt.Style = MetroFramework.MetroColorStyle.Yellow;
            this.studentnametxt.TabIndex = 112;
            this.studentnametxt.Theme = MetroFramework.MetroThemeStyle.Light;
            this.studentnametxt.UseCustomForeColor = true;
            this.studentnametxt.UseSelectable = true;
            this.studentnametxt.WaterMarkColor = System.Drawing.Color.Silver;
            this.studentnametxt.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.studentnametxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.studentnametxt_KeyPress);
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(930, 571);
            this.Controls.Add(this.askpanel);
            this.MaximizeBox = false;
            this.Movable = false;
            this.Name = "Report";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Report";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Report_FormClosing);
            this.askpanel.ResumeLayout(false);
            this.askpanel.PerformLayout();
            this.reportpanel.ResumeLayout(false);
            this.reportpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel askpanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroButton next;
        private MetroFramework.Controls.MetroLabel metroLabel17;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private iTalk.iTalk_NumericUpDown numericyear;
        private System.Windows.Forms.Panel reportpanel;
        private MetroFramework.Controls.MetroLabel studentnamelbl;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel gradelbl;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroTextBox studentnametxt;
        private MetroFramework.Controls.MetroButton back;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;




    }
}