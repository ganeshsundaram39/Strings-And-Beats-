namespace Strings_and_Beats
{
    partial class Loading
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.intro2 = new iTalk.iTalk_ProgressIndicator();
            this.intro3 = new iTalk.iTalk_HeaderLabel();
            this.intro = new iTalk.iTalk_HeaderLabel();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // intro2
            // 
            this.intro2.Location = new System.Drawing.Point(265, 155);
            this.intro2.MinimumSize = new System.Drawing.Size(80, 80);
            this.intro2.Name = "intro2";
            this.intro2.P_AnimationColor = System.Drawing.Color.Black;
            this.intro2.P_AnimationSpeed = 100;
            this.intro2.P_BaseColor = System.Drawing.Color.DarkGray;
            this.intro2.Size = new System.Drawing.Size(80, 80);
            this.intro2.TabIndex = 4;
            this.intro2.Text = "iTalk_ProgressIndicator1";
            this.intro2.Visible = false;
            // 
            // intro3
            // 
            this.intro3.AutoSize = true;
            this.intro3.BackColor = System.Drawing.Color.Transparent;
            this.intro3.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.intro3.ForeColor = System.Drawing.Color.Black;
            this.intro3.Location = new System.Drawing.Point(351, 172);
            this.intro3.Name = "intro3";
            this.intro3.Size = new System.Drawing.Size(169, 39);
            this.intro3.TabIndex = 5;
            this.intro3.Text = "Loading...";
            this.intro3.Visible = false;
            // 
            // intro
            // 
            this.intro.AutoSize = true;
            this.intro.BackColor = System.Drawing.Color.Transparent;
            this.intro.Font = new System.Drawing.Font("Microsoft Sans Serif", 53F);
            this.intro.ForeColor = System.Drawing.Color.Black;
            this.intro.Location = new System.Drawing.Point(2, 148);
            this.intro.Name = "intro";
            this.intro.Size = new System.Drawing.Size(601, 81);
            this.intro.TabIndex = 6;
            this.intro.Text = "Strings And Beats";
            // 
            // Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Strings_and_Beats.Properties.Resources.intro;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(957, 429);
            this.Controls.Add(this.intro2);
            this.Controls.Add(this.intro3);
            this.Controls.Add(this.intro);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Loading";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private iTalk.iTalk_ProgressIndicator intro2;
        private iTalk.iTalk_HeaderLabel intro3;
        private System.Windows.Forms.Timer timer2;
        private iTalk.iTalk_HeaderLabel intro;


    }
}

