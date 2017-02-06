using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Strings_and_Beats
{
    public partial class Loading : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public Loading()
        {
            InitializeComponent();
          
           
            

           timer1.Start();

            string executableLocation = Path.GetDirectoryName(
    Assembly.GetExecutingAssembly().Location);
            string xslLocation = Path.Combine(executableLocation, "samarkan.TTF");
            PrivateFontCollection pfc = new PrivateFontCollection();
            
            pfc.AddFontFile(xslLocation);
            intro.Font = new Font(pfc.Families[0], 53, FontStyle.Regular);
            this.DoubleBuffered = true;
        }

      
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;  // Turn on WS_EX_TOOLWINDOW
                return cp;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.1;
            if (this.Opacity == 1) {
                timer1.Stop();
                intro.Visible = false;
                Thread.Sleep(1500);
                intro2.Visible = true;
                intro3.Visible = true;
                timer2.Start();
            }
        }
        int i = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            ++i;
           
            if (i == 4) {
                timer2.Stop();
                this.Close();
            }
        }

       
    
  

  
    }
}
