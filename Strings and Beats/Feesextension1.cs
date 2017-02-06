using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strings_and_Beats
{
    public partial class Feesextension1 : MetroForm
    {
        static string connectionString = Connection.getConnection();
        SQLiteConnection connection = new SQLiteConnection(connectionString);
        string feeid,studentid,fees,studentname;

        int noofmonths;
        string plan;
        public Feesextension1(String feeid,String name,Boolean status,DateTime paydate,String fees,string plan)
        {
            InitializeComponent();
            connection.Open();
            this.feeid = feeid;
            this.fees = fees;
            this.studentname = name;
            try
            {
                SQLiteDataAdapter sda2;
                this.name.Text = camelCase(name);
                this.feeslbl.Text = fees+"/-";
                if (status)
                {
                    monthlypaymentdate.Value = paydate;
                    monthlypaymentdate.Checked = true;
                    statuslbl.Text = "Paid";
                    sendsms.Visible = true;
                }
                  else
                {
                    monthlypaymentdate.Checked = false;
                    monthlypaymentdate.Value = paydate;
                    statuslbl.Text = "Unpaid";
                    sendsms.Visible = false;
                }
                sda2 = new SQLiteDataAdapter("select Studentid from FeeshistoryTbl  where rowid='" + feeid + "'", connection);
                DataTable dg = new DataTable();
                sda2.Fill(dg);
                studentid= dg.Rows[0][0].ToString();
                sda2 = new SQLiteDataAdapter("select Gender from StudentTbl  where rowid='" +studentid+ "'", connection);
                dg = new DataTable();
                sda2.Fill(dg);
                if (dg.Rows[0][0].ToString() == "male") { genderbox.Image = Properties.Resources.new_student; } else { genderbox.Image = Properties.Resources.female; }

                if (plan == "Monthly") { noofmonths = 1; }
                else if (plan == "Quartely") { noofmonths = 3; }
                else if (plan == "Half Yearly") { noofmonths = 6; }
                else if (plan == "Annually") { noofmonths = 12; }
                this.plan = plan;
            }
               
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private string camelCase(string text) { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text); }

     
        private void metroButton3_Click(object sender, EventArgs e)
        {
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                if (monthlypaymentdate.Checked)
                {

                    adapter.UpdateCommand = new SQLiteCommand("update FeeshistoryTbl  SET Status='Paid',Paymentdate=@paydate  where rowid= '" + feeid + "'", connection);
                    adapter.UpdateCommand.Parameters.Add("@paydate", DbType.DateTime).Value = monthlypaymentdate.Value;
                    adapter.UpdateCommand.ExecuteNonQuery();
                    statuslbl.Text = "Paid";
                    sendsms.Visible = true;
                    adapter= new SQLiteDataAdapter("select max(rowid) from FeeshistoryTbl where Studentid='"+studentid+"'",connection);
                    DataTable dg = new DataTable();
                   adapter.Fill(dg);
                   Int32 max=Convert.ToInt32(dg.Rows[0][0]);
                   if (max == Convert.ToInt32(feeid))
                   {
                   adapter= new SQLiteDataAdapter("select Lastday,Discount,Totalfees from FeeshistoryTbl where rowid='"+feeid+"'",connection);
                   dg = new DataTable();
                   adapter.Fill(dg);
                   string firstday=Convert.ToDateTime(dg.Rows[0][0]).AddDays(1).ToString("yyyy-MM-dd");
                   string discount = dg.Rows[0][1].ToString();
                   string totalfees = dg.Rows[0][2].ToString();

                       DialogResult result = MetroMessageBox.Show(this, "Do you want to extend same plan..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                       if (result == DialogResult.Yes)
                       {
                           adapter.InsertCommand = new SQLiteCommand("insert into FeeshistoryTbl(Studentid,Status,Paymentdate,Paymentplan,Paymentmode,Lastday,Firstday,Fees,Discount,Totalfees,Fullname) values(@studentid,@status,@paymentdate,'"+plan+"','Full Payment',@lastday,@firstday,'" + fees + "','" + discount + "','" + totalfees+ "','" +camelCase( studentname) + "');", connection);
                           adapter.InsertCommand.Parameters.Add("@studentid", DbType.String).Value = studentid;


                           adapter.InsertCommand.Parameters.Add("@status", DbType.String).Value = "Unpaid";

                       
                                adapter.InsertCommand.Parameters.Add("@paymentdate", DbType.DateTime).Value = DBNull.Value;



                                adapter.InsertCommand.Parameters.Add("@lastday", DbType.DateTime).Value = Convert.ToDateTime(firstday).AddMonths(noofmonths).ToString("yyyy-MM-dd");

                           adapter.InsertCommand.Parameters.Add("@firstday", DbType.DateTime).Value = firstday ;

                           adapter.InsertCommand.ExecuteNonQuery();
                     

                       }
                       else if (result == DialogResult.No)
                       {

                       }

                   }
                }
                else {
                    adapter.UpdateCommand = new SQLiteCommand("update FeeshistoryTbl  SET Status='Unpaid',Paymentdate=@paydate  where rowid= '" + feeid + "'", connection);
                    adapter.UpdateCommand.Parameters.Add("@paydate", DbType.DateTime).Value = DBNull.Value;
                    adapter.UpdateCommand.ExecuteNonQuery();
                    statuslbl.Text = "Unpaid";
                    sendsms.Visible = false;
                }

            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void next1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Feesextension_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        private void Feesextension1_Load(object sender, EventArgs e)
        {

        }

        private void sendsms_Click(object sender, EventArgs e)
        {

        }
        
    }
}
