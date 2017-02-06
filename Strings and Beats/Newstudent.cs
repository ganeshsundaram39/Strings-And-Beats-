using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;


namespace Strings_and_Beats
{
    public partial class NewStudent : MetroForm
    {
        static string connectionString = Connection.getConnection();
        SQLiteConnection connection = new SQLiteConnection(connectionString);
        bool dateofbirthvalid;
        String gender="female";
        String plan = "monthly";
        String mode;
        public NewStudent()
        {
            InitializeComponent();
            connection.Open();
            this.DoubleBuffered = true; 
            metroTabControl1.SelectedTab = tab1;
            dateofbirth.Value = DateTime.Today.Date;
            dateofjoining.Value = DateTime.Today.Date;
            monthlypaymentdate.Value = DateTime.Today.Date;
            quartelypaymentdate.Value = DateTime.Today.Date;
            fullpaymentdate.Value = DateTime.Today.Date;
            firstinstallmentdate.Value = DateTime.Today.Date;
            monthlypaymentdate.Checked = false; ;
            quartelypaymentdate.Checked = false; ;
            fullpaymentdate.Checked = false; ;
            firstinstallmentdate.Checked = false; ;
            autoComplete();
            this.ActiveControl = fullname;
            
        }

        private void autoComplete()
        {
            complete(studentinstrument, "Musicalinstrument");
            complete(instructorname, "Instructorname");
            complete(batch, "Batch");
            complete(timing, "Timing");
            complete(branch, "Branch");
        }

        public void complete(MetroTextBox t,String column)
        {
        t.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        t.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            try
            {
               
                SQLiteCommand s = new SQLiteCommand("select "+column+" from StudentTbl", connection);
                SQLiteDataReader reader = s.ExecuteReader();
                while (reader.Read())
                {
                    coll.Add(reader.GetString(reader.GetOrdinal(column)));
                }

                t.AutoCompleteCustomSource = coll;
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            finally
            {
               
            }
        }

        private void next1_Click(object sender, EventArgs e)
        {
            if (isDateAfterOrToday(dateofbirth.Value))
            {

                dateofbirthvalid = false;
            }
            else
            {
                dateofbirthvalid = true;
            }


            if (fullname.Text == "") {
                MetroMessageBox.Show(this, "Enter full name..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                fullname.Focus();
            
            }
            else if (!dateofbirthvalid) {
                MetroMessageBox.Show(this, "Enter correct Date of Birth..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               dateofbirth.Focus();
            
            }
            else if(mobileno.Text==""){
                MetroMessageBox.Show(this, "Enter mobile number..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mobileno.Focus();
            
            }
            else if (parentname.Text == "") {
                MetroMessageBox.Show(this, "Enter parent or guardian name..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                parentname.Focus();
            
            }
            else if (parentcontactno.Text == "") {
                MetroMessageBox.Show(this, "Enter parent contact number..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                parentcontactno.Focus();
            
            }
         
            else if (address.Text == "")
            {
                MetroMessageBox.Show(this, "Enter address..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                address.Focus();

            }
            else {
                this.metroTabControl1.SelectedTab = tab2;
                studentinstrument.Focus();
                
            }
        }

        private void fullname_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsLetter(ch) && ch != 8 && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;

            }
        }

        private void mobileno_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;



            }
        }

        private void mobileno_Validating(object sender, CancelEventArgs e)
        {
            if (mobileno.Text != "")
            {

                if (mobileno.Text.Length < 10 || !IsPhoneNumber(mobileno.Text))
                {

                    MetroMessageBox.Show(this, "Mobile number not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    mobileno.Clear();
                    
                }
            }
            else
            {

            }
        }
        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^[789]\d{9}$").Success;
        }

        private void parentname_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsLetter(ch) && ch != 8 && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;

            }
        }

        private void parentcontactno_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void parentcontactno_Validating(object sender, CancelEventArgs e)
        {
            if (parentcontactno.Text != "")
            {

                if (parentcontactno.Text.Length < 10 || !IsPhoneNumber(parentcontactno.Text))
                {
                    MetroMessageBox.Show(this, "Mobile number not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    parentcontactno.Clear();
                }
            }
            else
            {

            }
        }

        private void emailid_Validating(object sender, CancelEventArgs e)
        {
             if (emailid.Text != "")
            {
                if (!IsValidEmail(emailid.Text))
                {
                    MetroMessageBox.Show(this, "Email id not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    emailid.Clear();
                    emailid.Focus();
                }
            }
            else
            {

            }
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
      
        private void dateofbirth_Validating(object sender, CancelEventArgs e)
        {
            if (isDateAfterOrToday(dateofbirth.Value))
            {

                dateofbirthvalid = false;
            }
            else {
                dateofbirthvalid = true;
            }
        }

        public static bool isDateAfterOrToday(DateTime pDate)
        {
           return  pDate>= DateTime.Today.AddYears(-1);
        }

        private void previous1_Click(object sender, EventArgs e)
        {
            this.metroTabControl1.SelectedTab = tab1;
            fullname.Focus();
        }

        private void studentinstrument_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsLetter(ch) && ch != 8)
            {
                e.Handled = true;

            }
        }


        private void instructorname_KeyPress(object sender, KeyPressEventArgs e)
        {

            char ch = e.KeyChar;
            if (!char.IsLetter(ch) && ch != 8)
            {
                e.Handled = true;

            }
        }
        private void next2_Click(object sender, EventArgs e)
        {
            if (studentinstrument.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's instrument..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                studentinstrument.Focus();
            }
            else if (instructorname.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's instructor..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                studentinstrument.Focus();
            }
            else if (batch.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's batch..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                studentinstrument.Focus();
            }
            else if (timing.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's timing..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                studentinstrument.Focus();
            }
            else if (branch.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's branch..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                studentinstrument.Focus();
            }
            else if (dateofjoining.Value.Date < DateTime.Now.Date)
                {
                    MetroMessageBox.Show(this, "Date of joining not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    dateofjoining.Value = DateTime.Now;

                }
            else
            {
                this.metroTabControl1.SelectedTab = tab3;
                monthly.Focus();
            }


        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.metroTabControl1.SelectedTab = tab2;
            studentinstrument.Focus();
        }

        private void monthly_CheckedChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            plan = "monthly";
            fees.Clear();
            discount.Visible = false;
            discountlbl.Visible = false;
            calculate.Visible = false;
            totalfees.Visible = false;
            discount.Clear();
            totalfees.Clear();
            totalfeeslabel.Visible = false;
            paymentmode.Visible = false;
            paymentmodelbl.Visible = false;
            fullpaymentdate.Visible = false;
            fullpaymentdatelbl.Visible = false;
            totalfeeslabel.Visible = false;
            noofinst.Visible = false;
            numberofinstallmentlbl.Visible = false;
            firstinstalldatelbl.Visible = false;
            firstinstallmentdate.Visible = false;
            monthlypaymentdate.Visible = true;
            monthlypaymentlabel.Visible = true;
            quarterlypaymentdatelbl.Visible = false;
            quartelypaymentdate.Visible = false;
            fullpayment.Checked = true;
        }

        private void addrefresh()
        {
            refresh();
            monthly.Checked = true;
            metroTabControl1.SelectedTab = tab1;
            this.ActiveControl = fullname;
            dateofbirth.Value = DateTime.Today.Date;
            dateofjoining.Value = DateTime.Today.Date;
            monthlypaymentdate.Value = DateTime.Today.Date;
            quartelypaymentdate.Value = DateTime.Today.Date;
            fullpaymentdate.Value = DateTime.Today.Date;
            firstinstallmentdate.Value = DateTime.Today.Date;
            fullname.Clear();
            parentname.Clear();
            parentcontactno.Clear();
            emailid.Clear();
            mobileno.Clear();
            address.Clear();
            studentinstrument.Clear();
            branch.Clear();
            timing.Clear();
            female.Checked = true;
            instructorname.Clear();
            batch.Clear();
        }

        private void quarterly_CheckedChanged(object sender, EventArgs e)
        {
            plan = "quartely";
            discount.Visible = true;
            discount.Clear();
            fees.Clear();
            totalfees.Clear();
            discountlbl.Visible = true;
            calculate.Visible = true;
            totalfees.Visible = true;
            totalfeeslabel.Visible = true;
            paymentmode.Visible = false;
            paymentmodelbl.Visible = false;
            fullpaymentdate.Visible = false;
            fullpaymentdatelbl.Visible = false;
            totalfeeslabel.Visible = true;
            noofinst.Visible = false;
            numberofinstallmentlbl.Visible = false;
            firstinstalldatelbl.Visible = false;
            firstinstallmentdate.Visible = false;
            quartelypaymentdate.Visible = true;
            quarterlypaymentdatelbl.Visible = true;
            monthlypaymentdate.Visible = false;
            monthlypaymentlabel.Visible = false;
            fullpayment.Checked = true;
        }

        private void halfyearly_CheckedChanged(object sender, EventArgs e)
        {
            plan = "half yearly";
            discount.Clear();
            fees.Clear();
            totalfees.Clear();
            monthlypaymentlabel.Visible = false;
            monthlypaymentdate.Visible = false;
            quartelypaymentdate.Visible = false;
            quarterlypaymentdatelbl.Visible = false;
            discount.Visible = true;
            discountlbl.Visible = true;
            calculate.Visible = true;
            totalfees.Visible = true;
            totalfeeslabel.Visible = true;
            paymentmode.Visible = true;
            fullpayment.Checked = true;
            paymentmodelbl.Visible = true;
            fullpaymentdate.Visible = true;
            fullpaymentdatelbl.Visible = true;
            totalfeeslabel.Visible = true;
            noofinst.Visible = false;
            numberofinstallmentlbl.Visible = false;
            firstinstalldatelbl.Visible = false;
            firstinstallmentdate.Visible = false;
        }

        private void fullpayment_CheckedChanged(object sender, EventArgs e)
        {
            fullpaymentdatelbl.Visible = true;
            fullpaymentdate.Visible = true;
            numberofinstallmentlbl.Visible = false;
            noofinst.Visible = false;
            firstinstallmentdate.Visible = false;
            firstinstalldatelbl.Visible = false;
        }

        private void annually_CheckedChanged(object sender, EventArgs e)
        {
            plan = "annually";
            discount.Visible = true;
            discountlbl.Visible = true;
            calculate.Visible = true;
            totalfees.Visible = true;
            totalfeeslabel.Visible = true;
            paymentmode.Visible = true;
            fullpayment.Checked = true;
            paymentmodelbl.Visible = true;
            fullpaymentdate.Visible = true;
            fullpaymentdatelbl.Visible = true;
            totalfeeslabel.Visible = true;
            noofinst.Visible = false;
            numberofinstallmentlbl.Visible = false;
            firstinstalldatelbl.Visible = false;
            firstinstallmentdate.Visible = false;
            monthlypaymentlabel.Visible = false;
            monthlypaymentdate.Visible = false;
            quartelypaymentdate.Visible = false;
            quarterlypaymentdatelbl.Visible = false;
        }

        private void installments_CheckedChanged(object sender, EventArgs e)
        {
            fullpaymentdatelbl.Visible = false;
            fullpaymentdate.Visible = false;
            numberofinstallmentlbl.Visible = true;
            noofinst.Visible = true;
            firstinstallmentdate.Visible = true;
            firstinstalldatelbl.Visible = true;
        }

        private void fees_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
        private void discount_Validating(object sender, CancelEventArgs e)
        {
            if (discount.Text != "")
            {
                if (Convert.ToInt16(discount.Text) != 5 && Convert.ToInt16(discount.Text)!=10)
                {
                    discount.Clear();
                    totalfees.Clear();
                    MetroMessageBox.Show(this, "Discount that can be given is 5% or 10%..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
                }
            }
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            if (fees.Text != "" && discount.Text != "") {
                totalfees.Text = (Convert.ToInt32(fees.Text)-(Convert.ToInt32(fees.Text) * Convert.ToInt16(discount.Text)/100)).ToString();
            }
            else if (fees.Text == "" || discount.Text == "") {
                totalfees.Clear();
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            add.Enabled = false;
            if (fullpayment.Checked)
            { mode = "Full Payment"; } 
            else if (installments.Checked) 
            { mode = "Installments"; }
            if (isDateAfterOrToday(dateofbirth.Value))
            {
                dateofbirthvalid = false;
            }
            else
            {
                dateofbirthvalid = true;
            }

             if (fullname.Text == "") {
                MetroMessageBox.Show(this, "Enter full name..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.metroTabControl1.SelectedTab = tab1;
                 fullname.Focus();
            }
            else if (!dateofbirthvalid) {
                MetroMessageBox.Show(this, "Enter correct Date of Birth..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.metroTabControl1.SelectedTab = tab1;
                dateofbirth.Focus();
            
            }
            else if(mobileno.Text==""){
                MetroMessageBox.Show(this, "Enter mobile number..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.metroTabControl1.SelectedTab = tab1;
                mobileno.Focus();
            
            }
            else if (parentname.Text == "") {
                MetroMessageBox.Show(this, "Enter parent or guardian name..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.metroTabControl1.SelectedTab = tab1;
                parentname.Focus();
            }
            else if (parentcontactno.Text == "") {
                MetroMessageBox.Show(this, "Enter parent contact number..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.metroTabControl1.SelectedTab = tab1;
                parentcontactno.Focus();
              }
            else if (address.Text == "")
            {
                MetroMessageBox.Show(this, "Enter address..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.metroTabControl1.SelectedTab = tab1;
                address.Focus();
            }
            else if (studentinstrument.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's instrument..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.metroTabControl1.SelectedTab = tab2;
                studentinstrument.Focus();
            }
            else if (instructorname.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's instructor..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.metroTabControl1.SelectedTab = tab2;
                studentinstrument.Focus();
            }
            else if (batch.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's batch..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.metroTabControl1.SelectedTab = tab2;
                studentinstrument.Focus();
            }
            else if (timing.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's timing..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.metroTabControl1.SelectedTab = tab2;
                studentinstrument.Focus();
            }
             else if (branch.Text == "")
             {
                 MetroMessageBox.Show(this, "Enter student's branch..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                 this.metroTabControl1.SelectedTab = tab2;
                 studentinstrument.Focus();
             }
             else if(fees.Text=="")
             {
                 MetroMessageBox.Show(this, "Enter student's fees..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                 fees.Focus();
             }
             else if (discount.Text != "" && totalfees.Text=="")
             {
                 MetroMessageBox.Show(this, "Calculate total fees..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                 totalfees.Focus();
             }
             else
             {
                 addStudent();
             }
             Cursor.Current = Cursors.Default;
             add.Enabled = true;
        }

        private string camelCase(string text) { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text); }

        private void addStudent() {

            if (fees.Text != "" && discount.Text != ""&&discount.Text!="No discount")
            {
                totalfees.Text = (Convert.ToInt32(fees.Text) - (Convert.ToInt32(fees.Text) * Convert.ToInt16(discount.Text) / 100)).ToString();
            }
            else if (fees.Text == "" || discount.Text == "")
            {
               
                discount.Text = "No discount";
                totalfees.Text = fees.Text;
               
            }


            if (monthly.Checked || quarterly.Checked) {
                mode = "full payment";
            }

            SQLiteCommand command;
      
            try
            {
                    command = connection.CreateCommand(); 
                    command.CommandText = "SELECT count(*) FROM StudentTbl where Fullname='"+camelCase(fullname.Text)+"' and Musicalinstrument='"+camelCase( studentinstrument.Text)+"'";
               
                  
                    var i = command.ExecuteScalar();
                    
                    if (Convert.ToInt16(i) != 1)
                    {
                        SQLiteDataAdapter mg = new SQLiteDataAdapter(); 
                        mg.InsertCommand=new SQLiteCommand("INSERT INTO StudentTbl VALUES ('" +camelCase(fullname.Text) + "',@dob,'" + mobileno.Text + "','" +camelCase(parentname.Text) + "','" + parentcontactno.Text + "','" + emailid.Text + "','" + gender + "','" +camelCase(address.Text) + "','" + camelCase(studentinstrument.Text) + "',@doj,'" +camelCase( instructorname.Text) + "','" +camelCase( batch.Text) + "','" + timing.Text + "','" +camelCase(branch.Text) + "','" +camelCase(plan) + "','" + fees.Text + "','" + discount.Text + "','" + totalfees.Text + "','" + camelCase(mode) + "','','');",connection);
                        mg.InsertCommand.Parameters.Add("@doj", DbType.DateTime).Value = new DateTime(dateofjoining.Value.Year, dateofjoining.Value.Month, dateofjoining.Value.Day);
                        mg.InsertCommand.Parameters.Add("@dob", DbType.DateTime).Value = new DateTime(dateofbirth.Value.Year, dateofbirth.Value.Month, dateofbirth.Value.Day);
                        mg.InsertCommand.ExecuteNonQuery();
                        String status = "";
                        Boolean isPaid = false;
                        DateTime? datetime = new DateTime();
                        String installmentstatus = "";
                        int noofmonths=0;
                        if (monthly.Checked)
                        {
                            if (monthlypaymentdate.Checked)
                            { isPaid = true; status = "Paid"; datetime = monthlypaymentdate.Value; }
                            else { isPaid = false; status = "Unpaid"; datetime = null; }
                            noofmonths = 1;
                        }
                        else if (quarterly.Checked)
                        {
                            noofmonths = 3;
                            if (quartelypaymentdate.Checked)
                            { isPaid = true; status = "Paid"; datetime =quartelypaymentdate.Value; }
                            else { isPaid = false; status = "Unpaid"; datetime = null; }
                        }
                        else if (halfyearly.Checked)
                        {
                            noofmonths = 6;
                            datetime = null;
                            if (fullpayment.Checked)
                            {
                                if (fullpaymentdate.Checked)
                                {
                                    isPaid = true; status = "Paid"; datetime = fullpaymentdate.Value;

                                }
                                else
                                {
                                    isPaid = false; status = "Unpaid";
                                }
                            }
                            else if (installments.Checked)
                            {
                                if (firstinstallmentdate.Checked)
                                {
                                    isPaid = true; status = "Unpaid"; installmentstatus = "Paid"; datetime = firstinstallmentdate.Value;

                                }
                                else
                                {
                                    isPaid = false; status = "Unpaid"; installmentstatus = "Unpaid";
                                }

                            }
                        }
                        else if (annually.Checked)
                        {
                            noofmonths = 12;
                            datetime = null;
                            if (fullpayment.Checked)
                            {
                                if (fullpaymentdate.Checked)
                                {
                                    isPaid = true; status = "Paid"; datetime =  fullpaymentdate.Value;

                                }
                                else
                                {
                                    isPaid = false; status = "Unpaid";
                                }
                            }
                            else if (installments.Checked)
                            {
                                if (firstinstallmentdate.Checked)
                                {
                                    isPaid = true; status = "Unpaid"; datetime =firstinstallmentdate.Value;
                                    installmentstatus = "Paid";
                                }
                                else
                                {
                                    isPaid = false; status = "Unpaid"; installmentstatus = "Unpaid";
                                }
                            }
                        }

                        command.CommandText = "select max(rowid) from StudentTbl";
                        i = command.ExecuteScalar();

                        SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                        adapter.InsertCommand = new SQLiteCommand("insert into FeeshistoryTbl(Studentid,Status,Paymentdate,Paymentplan,Paymentmode,Lastday,Firstday,Fees,Discount,Totalfees,Fullname) values(@studentid,@status,@paymentdate,'" + camelCase(plan)+ "','" +camelCase( mode) + "',@lastday,@firstday,'" + fees.Text + "','" + discount.Text + "','" + totalfees.Text + "','"+camelCase( fullname.Text)+"');", connection);
                        adapter.InsertCommand.Parameters.Add("@studentid", DbType.String).Value = i;
                        adapter.InsertCommand.Parameters.Add("@status", DbType.String).Value=camelCase(status) ;
                    
                        if (isPaid==true &&  !installments.Checked)
                        {

                            adapter.InsertCommand.Parameters.Add("@paymentdate", DbType.DateTime).Value = datetime;
                          
                        }
                        else
                        {
                            adapter.InsertCommand.Parameters.Add("@paymentdate", DbType.DateTime).Value = DBNull.Value;
                        }

                        adapter.InsertCommand.Parameters.Add("@lastday", DbType.DateTime).Value = new DateTime(dateofjoining.Value.Year, dateofjoining.Value.Month, dateofjoining.Value.Day).AddMonths(noofmonths);

                        adapter.InsertCommand.Parameters.Add("@firstday", DbType.DateTime).Value = new DateTime(dateofjoining.Value.Year, dateofjoining.Value.Month, dateofjoining.Value.Day);
                  
                        adapter.InsertCommand.ExecuteNonQuery();
                        if (installments.Checked)
                            {
                            
                            int noofinstallment=Convert.ToInt16( noofinst.Value);
                            string[] installmentarray=new string[noofinstallment];
                            
                            for (int j = 1; j < noofinstallment; j++) {
                                installmentarray[j] = "Unpaid";
                            }
                            Int32 installmentamount = Convert.ToInt32(totalfees.Text) / Convert.ToInt32(noofinst.Value);
                            Int32 sum = 0;
                            for (int k = 0; k < noofinstallment; k++) {
                                sum = sum + installmentamount;
                            }
                            Int32 lastinstallmentfees=installmentamount;
                            if (sum < Convert.ToInt32(totalfees.Text)) {
                             Int32 amt=   Convert.ToInt32(totalfees.Text) - sum;
                             lastinstallmentfees = installmentamount + amt;
                            }
                            DateTime [] datelist = new DateTime[noofinstallment];
                            if (halfyearly.Checked)
                            {
                                DateTime hydt = new DateTime(dateofjoining.Value.Year, dateofjoining.Value.Month, dateofjoining.Value.Day);
                                if (noofinstallment == 2) { datelist[1] = hydt.AddMonths(3); }
                                else if (noofinstallment == 3) { datelist[1] = hydt.AddMonths(2); datelist[2] = hydt.AddMonths(4); }
                                else if (noofinstallment == 4) { datelist[1] = hydt.AddMonths(1).AddDays(15); datelist[2] = hydt.AddMonths(3); datelist[3] = hydt.AddMonths(4).AddDays(15); }
                                else if (noofinstallment == 5) { datelist[1] = hydt.AddMonths(1).AddDays(6); datelist[2] = datelist[1].AddMonths(1).AddDays(6); datelist[3] = datelist[2].AddMonths(1).AddDays(6); datelist[4] = datelist[3].AddMonths(1).AddDays(6); }
                            }
                            else if (annually.Checked)
                            {
                                DateTime adt = new DateTime(dateofjoining.Value.Year, dateofjoining.Value.Month, dateofjoining.Value.Day);

                                if (noofinstallment == 2) { datelist[1] = adt.AddMonths(6); }
                                else if (noofinstallment == 3) { datelist[1] = adt.AddMonths(4); datelist[2] = adt.AddMonths(8); }
                                else if (noofinstallment == 4) { datelist[1] = adt.AddMonths(3); datelist[2] = adt.AddMonths(6); datelist[3] = adt.AddMonths(9); }
                                else if (noofinstallment == 5) { datelist[1] = adt.AddMonths(2).AddDays(12); datelist[2] = datelist[1].AddMonths(2).AddDays(12); datelist[3] = datelist[2].AddMonths(2).AddDays(12); datelist[4] = datelist[3].AddMonths(2).AddDays(12); }
                            }
          
                            SQLiteDataAdapter sda2 = new SQLiteDataAdapter();
                      
                            string rowid="";


                            sda2 = new SQLiteDataAdapter("select max(rowid)  from StudentTbl  ", connection);
                            DataTable dg = new DataTable();
                            sda2.Fill(dg);
                            rowid = dg.Rows[0][0].ToString();

                            sda2 = new SQLiteDataAdapter();
                         
                            sda2 = new SQLiteDataAdapter("select max(rowid)  from FeeshistoryTbl  where Studentid='" + rowid + "'", connection);
                            dg = new DataTable();
                            sda2.Fill(dg);
                            string feeid=dg.Rows[0][0].ToString();
                            adapter.InsertCommand = new SQLiteCommand("insert into InstallmentsTbl(Studentid,Installment,Paymentamt,Paymentdate,Status,Feeid) values(@studentid,@installment,@paymentamt,@paymentdate,@status,'"+feeid+"');", connection);
                            adapter.InsertCommand.Parameters.Add("@studentid", DbType.String).Value = i;
                            adapter.InsertCommand.Parameters.Add("@installment", DbType.String).Value = "1st installment";
                            adapter.InsertCommand.Parameters.Add("@paymentamt", DbType.String).Value = installmentamount;
                            
                            if (firstinstallmentdate.Checked)
                            {

                                adapter.InsertCommand.Parameters.Add("@paymentdate", DbType.DateTime).Value = datetime;

                            }
                            else
                            {
                                adapter.InsertCommand.Parameters.Add("@paymentdate", DbType.DateTime).Value = DBNull.Value;

                            }

                            adapter.InsertCommand.Parameters.Add("@status", DbType.String).Value =camelCase( installmentstatus);
                            adapter.InsertCommand.ExecuteNonQuery();
                            string[] expected = {"1st installment","2nd installment","3rd installment","4th installment","5th installment"};
                            for (int g = 1; g < noofinstallment;g++ )
                            {
                                adapter.InsertCommand = new SQLiteCommand("insert into InstallmentsTbl(Studentid,Installment,Paymentamt,Expecteddate,Paymentdate,Status,Feeid) values(@studentid,@installment,@paymentamt,@expecteddate,@paymentdate,@status,'" + feeid + "');", connection);
                                adapter.InsertCommand.Parameters.Add("@studentid", DbType.String).Value = i;
                                adapter.InsertCommand.Parameters.Add("@installment", DbType.String).Value = expected[g];
                               
                                if (g == noofinstallment - 1)
                                {
                                    adapter.InsertCommand.Parameters.Add("@paymentamt", DbType.String).Value = lastinstallmentfees;
                                }
                                else
                                {
                                    adapter.InsertCommand.Parameters.Add("@paymentamt", DbType.String).Value = installmentamount;
                                }

                                adapter.InsertCommand.Parameters.Add("@expecteddate", DbType.DateTime).Value = datelist[g];
                                adapter.InsertCommand.Parameters.Add("@paymentdate", DbType.DateTime).Value = DBNull.Value;
                                adapter.InsertCommand.Parameters.Add("@status", DbType.String).Value =camelCase( installmentarray[g]);


                               
                                adapter.InsertCommand.ExecuteNonQuery();
                            }
               
                          
                        }
                        SQLiteDataAdapter search = new SQLiteDataAdapter("select count(*) from LoginTbl where Username='" + instructorname.Text.ToLower() + "' and Role='Instructor'", connection);
                        DataTable searchdt = new DataTable();
                        search.Fill(searchdt);
                        if (Convert.ToInt16(searchdt.Rows[0][0].ToString()) == 0)
                        {
                            search = new SQLiteDataAdapter();
                            search.InsertCommand = new SQLiteCommand("insert into LoginTbl(Username,Role) values('" + instructorname.Text.ToLower() + "','Instructor');", connection);
                            search.InsertCommand.ExecuteNonQuery();
                        }
                        MetroMessageBox.Show(this, "One Student added..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                       
                       addrefresh();
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "Student already added with same name and instrument..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.metroTabControl1.SelectedTab = tab1;
                        fullname.Focus();
                    }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
              
              
                 autoComplete();
            }

        }


        private void totalfees_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
               
                
                e.Handled = true;



            }
        }

        private void numberofinstallment_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;



            }
        }

 

        private void female_CheckedChanged(object sender, EventArgs e)
        {
            gender = "female";
        }

        private void male_CheckedChanged(object sender, EventArgs e)
        {
            gender = "male";
        }

       

        private void newstudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

      
      

        private void dateofjoining_Validating(object sender, CancelEventArgs e)
        {
           
            if (dateofjoining.Value.Date < DateTime.Now.Date) {
                MetroMessageBox.Show(this, "Date of joining not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
                dateofjoining.Value = DateTime.Now;

            }
        }

      
    

    

    


    }
}
