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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strings_and_Beats
{
    public partial class EditStudent : MetroForm
    {
        
      static string connectionString = Connection.getConnection();
       SQLiteConnection connection = new SQLiteConnection(connectionString);
            DateTime olddoj;
        string rowid;
        bool dateofbirthvalid;
        String gender = "female";
        String plan = "monthly";
        String mode;
         
        public EditStudent()
        {
            InitializeComponent();

            connection.Open();
            this.DoubleBuffered = true;
            tabcontrol.SelectedTab = tab1;


           
            monthlypaymentdate.Value = DateTime.Today.Date;
            quartelypaymentdate.Value = DateTime.Today.Date;
            fullpaymentdate.Value = DateTime.Today.Date;
            firstinstallmentdate.Value = DateTime.Today.Date;
           
            gridViewgenerate();
           
            autoComplete();
            this.ActiveControl = update;
         
        }

        private void autoComplete()
        {
            complete(studentinstrument, "Musicalinstrument");
            complete(instructorname, "Instructorname");
            complete(batch, "Batch");
            complete(timing, "Timing");
            complete(branch, "Branch");
        }

        public void complete(MetroTextBox t, String column)
        {
            t.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            t.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            try
            {
           
                SQLiteCommand s = new SQLiteCommand("select " + column + " from StudentTbl", connection);
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
         
        }

       
        private  void gridViewgenerate()
        {
            try
            {
              
                SQLiteDataAdapter d = new SQLiteDataAdapter("select rowid as Id,Fullname as \"Student name\",Musicalinstrument as \"Instrument\",Instructorname as \"Instructor\" from StudentTbl;", connection);
                DataTable dt = new DataTable();
                d.Fill(dt);
                gridviewmain.DataSource = dt;
         
            

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }

        private void gridviewmain_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
            
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridviewmain.Rows[e.RowIndex];
                
                    rowid = row.Cells["Id"].Value.ToString();
                    addrefresh();
                 
                    gridToText(rowid);

                    dateofjoining.Checked = false;
                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        
        private void gridviewmain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                   
                    DataGridViewRow row = this.gridviewmain.Rows[e.RowIndex];
                    rowid = row.Cells["Id"].Value.ToString();
                    addrefresh();
                 
                    gridToText(rowid);
                    dateofjoining.Checked = false;
                }
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
       
        }

        private string camelCase(string text) { return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text); }

        private void gridToText(string rowid)
        {


            toggleOff();

            SQLiteDataAdapter d = new SQLiteDataAdapter("select Fullname,Mobilenumber,Parentname,Parentcontactno,Emailid,Gender,Address,Musicalinstrument,Instructorname,Batch,Timing,Branch,Fees,Discount,Totalfees from StudentTbl where rowid='" + rowid + "'", connection);

            DataTable dt = new DataTable();
            d.Fill(dt);

            fullname.Text = dt.Rows[0][0].ToString();


            mobileno.Text = dt.Rows[0][1].ToString();
            parentname.Text = dt.Rows[0][2].ToString();
            parentcontactno.Text = dt.Rows[0][3].ToString();
            emailid.Text = dt.Rows[0][4].ToString();
            if (dt.Rows[0][5].ToString() == "female")
            {
                female.Checked = true;
            }
            else if (dt.Rows[0][5].ToString() == "male")
            {
                male.Checked = true;
            }
            address.Text = dt.Rows[0][6].ToString();
            studentinstrument.Text = dt.Rows[0][7].ToString();

            instructorname.Text = dt.Rows[0][8].ToString();
            batch.Text = dt.Rows[0][9].ToString();
            timing.Text = dt.Rows[0][10].ToString();
            branch.Text = dt.Rows[0][11].ToString();


            fees.Text = dt.Rows[0][12].ToString();

            if (dt.Rows[0][13].ToString() == "No discount")
            {
                discount.Clear();
            }
            else
            {
                discount.Text = dt.Rows[0][13].ToString();

            }

            totalfees.Text = dt.Rows[0][14].ToString();


            SQLiteCommand s = new SQLiteCommand("select Dateofbirth,Dateofjoining from StudentTbl where rowid='" + rowid + "'", connection);
            SQLiteDataReader reader = s.ExecuteReader();
            while (reader.Read())
            {

                DateTime dob = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("Dateofbirth")));

                dateofbirth.Value = new DateTime(dob.Year, dob.Month, dob.Day);



                dateofjoining.Value = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("Dateofjoining")));
                olddoj = dateofjoining.Value;
            }
        }

     

        private void metroButton3_Click(object sender, EventArgs e)
        {
       
            int a = gridviewmain.CurrentCell.RowIndex;
            gridViewgenerate();
            gridviewmain.CurrentCell = gridviewmain.Rows[a].Cells[0];
            panel1.Visible = true;
            tabcontrol.Visible = false;
            this.ActiveControl = update;
        }


        private void listnext_Click(object sender, EventArgs e)
        {
            SQLiteDataAdapter d = new SQLiteDataAdapter("select count(*) from StudentTbl", connection);

            DataTable dt = new DataTable();

            d.Fill(dt);
            if (dt.Rows[0][0].ToString() != "0")
            {
                if (update.Checked)
                {
                    panel1.Visible = false;
                    tabcontrol.Visible = true;
                    changeplan.Checked = false;
                    tabcontrol.SelectedTab = tab1;
                    next1.Focus(); panel2.Visible = false; 
                }
                else if (delete.Checked)
                {
                    panel2.Visible = true;
                    tabcontrol.Visible = false;
                    panel1.Visible = false;
                
                 
                    SQLiteDataAdapter sda2 = new SQLiteDataAdapter();
                    sda2 = new SQLiteDataAdapter("select Gender  from StudentTbl  where rowid='" + rowid + "'", connection);
                    DataTable dg = new DataTable();
                    sda2.Fill(dg);

                    if (dg.Rows[0][0].ToString() == "male")
                    {

                        deletepicturebox.Image = Properties.Resources.male;
                    }
                    else
                    {

                        deletepicturebox.Image = Properties.Resources.woman;
                    }
                }
            }
            else
            {

                MetroMessageBox.Show(this, "No students for update or delete..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

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

        private void dateofbirth_Validating(object sender, CancelEventArgs e)
        {

            if (isDateAfterOrToday(dateofbirth.Value))
            {

                dateofbirthvalid = false;
            }
            else
            {
                dateofbirthvalid = true;
            }
        }

        public static bool isDateAfterOrToday(DateTime pDate)
        {


            return pDate >= DateTime.Today.AddYears(-1);
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

        private void parentcontactno_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;



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

                }
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

        private void female_CheckedChanged(object sender, EventArgs e)
        {
            gender = "female";
        }

        private void male_CheckedChanged(object sender, EventArgs e)
        {
            gender = "male";
        }
        
        private void next1_Click_1(object sender, EventArgs e)
        {
           
           
            if (isDateAfterOrToday(dateofbirth.Value))
            {

                dateofbirthvalid = false;
            }
            else
            {
                dateofbirthvalid = true;
            }


            if (fullname.Text == "")
            {
                MetroMessageBox.Show(this, "Enter full name..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                fullname.Focus();

            }
            else if (!dateofbirthvalid)
            {
                MetroMessageBox.Show(this, "Enter correct Date of Birth..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dateofbirth.Focus();

            }
            else if (mobileno.Text == "")
            {
                MetroMessageBox.Show(this, "Enter mobile number..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mobileno.Focus();

            }
            else if (parentname.Text == "")
            {
                MetroMessageBox.Show(this, "Enter parent or guardian name..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                parentname.Focus();

            }
            else if (parentcontactno.Text == "")
            {
                MetroMessageBox.Show(this, "Enter parent contact number..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                parentcontactno.Focus();

            }

            else if (address.Text == "")
            {
                MetroMessageBox.Show(this, "Enter address..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                address.Focus();

            }
            else
            {
                this.tabcontrol.SelectedTab = tab2;
               
            
                next2.Focus();
            }
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
            if (dateofjoining.Checked)
            {

                if (dateofjoining.Value.Date < DateTime.Now.Date)
                {
                    MetroMessageBox.Show(this, "Date of joining not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    dateofjoining.Value = DateTime.Now;

                }
            }
            else
            {

                dateofjoining.Value = olddoj; dateofjoining.Checked = false;




            }

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
            else
            {
                this.tabcontrol.SelectedTab = tab3;
                next2.Focus();
                this.ActiveControl = metroButton2;
            }
        }

        private void previous1_Click(object sender, EventArgs e)
        {
            this.tabcontrol.SelectedTab = tab1;
            fullname.Focus();
            this.ActiveControl = metroButton3;
        }

        private void monthly_CheckedChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            plan = "Monthly";
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
            noofinst. Visible = false;
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
            tabcontrol.SelectedTab = tab1;
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
            noofinst. Visible = false;
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
            noofinst. Visible = false;
            numberofinstallmentlbl.Visible = false;
            firstinstalldatelbl.Visible = false;
            firstinstallmentdate.Visible = false;
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
            noofinst. Visible = false;
            numberofinstallmentlbl.Visible = false;
            firstinstalldatelbl.Visible = false;
            firstinstallmentdate.Visible = false;
            monthlypaymentlabel.Visible = false;
            monthlypaymentdate.Visible = false;
            quartelypaymentdate.Visible = false;
            quarterlypaymentdatelbl.Visible = false;
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
                if (Convert.ToInt16(discount.Text) != 5 && Convert.ToInt16(discount.Text) != 10)
                {
                    discount.Clear();
                    totalfees.Clear();
                    MetroMessageBox.Show(this, "Discount that can be given is 5% or 10%..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            if (fees.Text != "" && discount.Text != "")
            {
                totalfees.Text = (Convert.ToInt32(fees.Text) - (Convert.ToInt32(fees.Text) * Convert.ToInt16(discount.Text) / 100)).ToString();
            }
            else if (fees.Text == "" || discount.Text == "")
            {
                totalfees.Clear();
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

        private void fullpayment_CheckedChanged(object sender, EventArgs e)
        {
            fullpaymentdatelbl.Visible = true;
            fullpaymentdate.Visible = true;
            numberofinstallmentlbl.Visible = false;
            noofinst. Visible = false;
            firstinstallmentdate.Visible = false;
            firstinstalldatelbl.Visible = false;
        }

        private void numberofinstallment_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;



            }
        }



        private void add_Click(object sender, EventArgs e)
        {
             
            Cursor.Current = Cursors.WaitCursor;
            add.Enabled = false;
            if (fullpayment.Checked)
            { mode = "full payment"; }
            else if (installments.Checked)
            { mode = "installments"; }
            if (isDateAfterOrToday(dateofbirth.Value))
            {

                dateofbirthvalid = false;
            }
            else
            {
                dateofbirthvalid = true;
            }

            if (fullname.Text == "")
            {
                MetroMessageBox.Show(this, "Enter full name..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tabcontrol.SelectedTab = tab1;
                fullname.Focus();

            }
            else if (!dateofbirthvalid)
            {
                MetroMessageBox.Show(this, "Enter correct Date of Birth..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tabcontrol.SelectedTab = tab1;
                dateofbirth.Focus();

            }
            else if (mobileno.Text == "")
            {
                MetroMessageBox.Show(this, "Enter mobile number..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tabcontrol.SelectedTab = tab1;
                mobileno.Focus();

            }
            else if (parentname.Text == "")
            {
                MetroMessageBox.Show(this, "Enter parent or guardian name..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tabcontrol.SelectedTab = tab1;
                parentname.Focus();

            }
            else if (parentcontactno.Text == "")
            {
                MetroMessageBox.Show(this, "Enter parent contact number..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tabcontrol.SelectedTab = tab1;
                parentcontactno.Focus();

            }

            else if (address.Text == "")
            {
                MetroMessageBox.Show(this, "Enter address..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tabcontrol.SelectedTab = tab1;
                address.Focus();

            }
            else if (studentinstrument.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's instrument..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tabcontrol.SelectedTab = tab2;
                studentinstrument.Focus();
            }
            else if (instructorname.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's instructor..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tabcontrol.SelectedTab = tab2;
                studentinstrument.Focus();
            }
            else if (batch.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's batch..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tabcontrol.SelectedTab = tab2;
                studentinstrument.Focus();
            }
            else if (timing.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's timing..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tabcontrol.SelectedTab = tab2;
                studentinstrument.Focus();
            }
            else if (branch.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's branch..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tabcontrol.SelectedTab = tab2;
                studentinstrument.Focus();
            }
            else if (fees.Text == "")
            {
                MetroMessageBox.Show(this, "Enter student's fees..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                fees.Focus();

            }
            else if (discount.Text != "" && totalfees.Text == "")
            {
                MetroMessageBox.Show(this, "Calculate total fees..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                totalfees.Focus();

            }
       
            else
            {
             
                updateStudent();
            }
            Cursor.Current = Cursors.Default;
            add.Enabled = true;
            toggleOff();
           
        }

        private void updateStudent() {
            try
            {
               
                if (fees.Text != "" && discount.Text != "" && discount.Text != "No discount")
                {
                    totalfees.Text = (Convert.ToInt32(fees.Text) - (Convert.ToInt32(fees.Text) * Convert.ToInt16(discount.Text) / 100)).ToString();
                }
                else if (fees.Text == "" || discount.Text == "")
                {

                    discount.Text = "No discount";
                    totalfees.Text = fees.Text;

                }
                

                if (monthly.Checked || quarterly.Checked)
                {
                    mode = "full payment";
                }
                
                SQLiteDataAdapter sda2 = new SQLiteDataAdapter();

                sda2.UpdateCommand = new SQLiteCommand("update StudentTbl  SET Fullname='" + camelCase(fullname.Text) + "',Dateofbirth=@dob ,Mobilenumber='" + mobileno.Text + "',Parentname='" + camelCase(parentname.Text) + "',Parentcontactno='" + parentcontactno.Text + "' , Emailid='" + emailid.Text + "' ,Gender='" + gender + "',Musicalinstrument='" + camelCase(studentinstrument.Text) + "',Dateofjoining=@doj,Instructorname='" + camelCase(instructorname.Text) + "',Batch='" + camelCase(batch.Text) + "',Timing='" + timing.Text + "',Branch='" + camelCase(branch.Text) + "',Fees='" + fees.Text + "',Discount='" + discount.Text + "',Totalfees='" + totalfees.Text + "' where rowid= '" + Convert.ToInt32(rowid) + "'", connection);
                sda2.UpdateCommand.Parameters.Add("@doj", DbType.DateTime).Value = new DateTime(dateofjoining.Value.Year, dateofjoining.Value.Month, dateofjoining.Value.Day);
                sda2.UpdateCommand.Parameters.Add("@dob", DbType.DateTime).Value = new DateTime(dateofbirth.Value.Year, dateofbirth.Value.Month, dateofbirth.Value.Day);

                sda2.UpdateCommand.ExecuteNonQuery();
                sda2 = new SQLiteDataAdapter("select max(rowid)  from FeeshistoryTbl  where Studentid='" + rowid + "'", connection);
                DataTable dg = new DataTable();
                sda2.Fill(dg);

                if (dg.Rows[0][0].ToString() != "")
                {
                    int studentfeeid = Convert.ToInt32(dg.Rows[0][0]);
                    sda2.UpdateCommand = new SQLiteCommand("update FeeshistoryTbl  SET Fees='" + fees.Text + "',Discount='" + discount.Text + "',Totalfees='" + totalfees.Text + "' where rowid= " + studentfeeid + " and Studentid='" + rowid + "'", connection);
                    sda2.UpdateCommand.ExecuteNonQuery();
                }

                sda2.UpdateCommand = new SQLiteCommand("update FeeshistoryTbl  SET Fullname='" + camelCase(fullname.Text) + "' where  Studentid='" + rowid + "'", connection);
                sda2.UpdateCommand.ExecuteNonQuery();


                if (changeplan.Checked)
                {
                    changePlan();
                }

                SQLiteDataAdapter search = new SQLiteDataAdapter("select count(rowid) from LoginTbl where Username='"+instructorname.Text.ToLower()+"' and Role='Instructor'",connection);
                DataTable searchdt = new DataTable();
                search.Fill(searchdt);
                if (Convert.ToInt16(searchdt.Rows[0][0].ToString())==0)
                {
                    search = new SQLiteDataAdapter();
                    search.InsertCommand = new SQLiteCommand("insert into LoginTbl(Username,Role) values('"+instructorname.Text.ToLower()+"','Instructor');", connection);
                    search.InsertCommand.ExecuteNonQuery();  
                }



                MetroMessageBox.Show(this, "Student information updated..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.ActiveControl = update;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                
                 Cursor.Current = Cursors.Default;
                 autoComplete();
                 int a = gridviewmain.CurrentCell.RowIndex;
                 gridViewgenerate();
                 gridviewmain.CurrentCell = gridviewmain.Rows[a].Cells[0];
                 panel1.Visible = true;
                 tabcontrol.Visible = false;
            }
        }

        private void changePlan()
        {
             SQLiteDataAdapter sda2 = new SQLiteDataAdapter();
           
             if (deletethecurrent)
             {
                 sda2 = new SQLiteDataAdapter("select max(rowid) from FeeshistoryTbl  where Studentid='" + rowid + "'", connection);
                 DataTable dg = new DataTable();
                 sda2.Fill(dg);

                 int studentfeeid = Convert.ToInt32(dg.Rows[0][0]);
                 sda2.DeleteCommand = new SQLiteCommand("delete  from InstallmentsTbl where Studentid='" + rowid + "' and Feeid='" + studentfeeid + "'", connection);
                 sda2.DeleteCommand.ExecuteNonQuery();

                 sda2.DeleteCommand = new SQLiteCommand("delete  from FeeshistoryTbl where Studentid='" + rowid + "' and rowid=" + studentfeeid + "", connection);
                 sda2.DeleteCommand.ExecuteNonQuery();
             }
             String status = "";
                       Boolean isPaid = false;
                       DateTime? datetime = new DateTime();
                       String installmentstatus = "";
                       int noofmonths = 0;
                       if (monthly.Checked)
                       {
                           if (monthlypaymentdate.Checked)
                           { isPaid = true; status = "paid"; datetime =monthlypaymentdate.Value; }
                           else { isPaid = false; status = "unpaid"; datetime = null; }
                           noofmonths = 1;
                       }
                       else if (quarterly.Checked)
                       {
                           noofmonths = 3;
                           if (quartelypaymentdate.Checked)
                           { isPaid = true; status = "paid"; datetime = quartelypaymentdate.Value; }
                           else { isPaid = false; status = "unpaid"; datetime = null; }
                       }
                       else if (halfyearly.Checked)
                       {
                           noofmonths = 6;
                           datetime = null;
                           if (fullpayment.Checked)
                           {
                               if (fullpaymentdate.Checked)
                               {
                                   isPaid = true; status = "paid"; datetime =fullpaymentdate.Value;

                               }
                               else
                               {
                                   isPaid = false; status = "unpaid";
                               }
                           }
                           else if (installments.Checked)
                           {
                               if (firstinstallmentdate.Checked)
                               {
                                   isPaid = true; status = "unpaid"; installmentstatus = "paid"; datetime = firstinstallmentdate.Value;

                               }
                               else
                               {
                                   isPaid = false; status = "unpaid"; installmentstatus = "unpaid";
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
                                   isPaid = true; status = "paid"; datetime = fullpaymentdate.Value;

                               }
                               else
                               {
                                   isPaid = false; status = "unpaid";
                               }
                           }
                           else if (installments.Checked)
                           {
                               if (firstinstallmentdate.Checked)
                               {
                                   isPaid = true; status = "unpaid"; datetime =firstinstallmentdate.Value;
                                   installmentstatus = "paid";
                               }
                               else
                               {
                                   isPaid = false; status = "unpaid"; installmentstatus = "unpaid";
                               }
                           }
                       }
                        
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                        adapter.InsertCommand = new SQLiteCommand("insert into FeeshistoryTbl(Studentid,Status,Paymentdate,Paymentplan,Paymentmode,Lastday,Firstday,Fees,Discount,Totalfees,Fullname) values(@studentid,@status,@paymentdate,'" +camelCase( plan) + "','" +camelCase(mode) + "',@lastday,@firstday,'" + fees.Text + "','" + discount.Text + "','" + totalfees.Text + "','" +camelCase( fullname.Text) + "');", connection);
                        adapter.InsertCommand.Parameters.Add("@studentid", DbType.String).Value = rowid;
                     

                        adapter.InsertCommand.Parameters.Add("@status", DbType.String).Value=camelCase( status) ;
                        if (isPaid==true &&  !installments.Checked)
                        {

                            adapter.InsertCommand.Parameters.Add("@paymentdate", DbType.DateTime).Value = datetime;

                        }
                        else
                        {
                            adapter.InsertCommand.Parameters.Add("@paymentdate", DbType.DateTime).Value = DBNull.Value;

                        }
                        adapter.InsertCommand.Parameters.Add("@lastday", DbType.DateTime).Value = new DateTime(dateofjoining.Value.Year,dateofjoining.Value.Month,dateofjoining.Value.Day).AddMonths(noofmonths);
                        adapter.InsertCommand.Parameters.Add("@firstday", DbType.DateTime).Value = new DateTime(dateofjoining.Value.Year, dateofjoining.Value.Month, dateofjoining.Value.Day);
                  
                        adapter.InsertCommand.ExecuteNonQuery();
                        adapter.UpdateCommand = new SQLiteCommand("update StudentTbl  SET Paymentplan='"+camelCase(plan)+"',Paymentmode='"+camelCase( mode)+"' where rowid= '" + rowid + "'", connection);
                        adapter.UpdateCommand.ExecuteNonQuery();
                          

                        if (installments.Checked)
                        {

                            int noofinstallment = Convert.ToInt16(noofinst.Value);
                            string[] installmentarray = new string[noofinstallment];

                            for (int j = 1; j < noofinstallment; j++)
                            {
                                installmentarray[j] = "Unpaid";
                            }
                            Int32 installmentamount = Convert.ToInt32(totalfees.Text) / Convert.ToInt32(noofinst.Value);
                            Int32 sum = 0;
                            for (int k = 0; k < noofinstallment; k++)
                            {
                                sum = sum + installmentamount;
                            }
                            Int32 lastinstallmentfees = installmentamount;
                            if (sum < Convert.ToInt32(totalfees.Text))
                            {
                                Int32 amt = Convert.ToInt32(totalfees.Text) - sum;
                                lastinstallmentfees = installmentamount + amt;
                            }
                            DateTime[] datelist = new DateTime[noofinstallment];
                            if (halfyearly.Checked)
                            {
                               DateTime hydt= new DateTime(dateofjoining.Value.Year, dateofjoining.Value.Month, dateofjoining.Value.Day);
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
                           
                           
                            sda2 = new SQLiteDataAdapter("select max(rowid)  from FeeshistoryTbl  where Studentid='" + rowid + "'", connection);
                            DataTable  dg = new DataTable();
                            sda2.Fill(dg);
                            string feeid = dg.Rows[0][0].ToString();
                            adapter.InsertCommand = new SQLiteCommand("insert into InstallmentsTbl(Studentid,Installment,Paymentamt,Paymentdate,Status,Feeid) values(@studentid,@installment,@paymentamt,@paymentdate,@status,'" + feeid + "');", connection);
                            adapter.InsertCommand.Parameters.Add("@studentid", DbType.String).Value = rowid;
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

                            string[] expected = { "1st installment", "2nd installment", "3rd installment", "4th installment", "5th installment" };
                            for (int g = 1; g < noofinstallment; g++)
                            {
                                adapter.InsertCommand = new SQLiteCommand("insert into InstallmentsTbl(Studentid,Installment,Paymentamt,Expecteddate,Paymentdate,Status,Feeid) values(@studentid,@installment,@paymentamt,@expecteddate,@paymentdate,@status,'" + feeid + "');", connection);
                                adapter.InsertCommand.Parameters.Add("@studentid", DbType.String).Value = rowid;
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
                                adapter.InsertCommand.Parameters.Add("@status", DbType.String).Value =camelCase(installmentarray[g]);



                                adapter.InsertCommand.ExecuteNonQuery();
                            }
                        }
              

       
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.tabcontrol.SelectedTab = tab2;
            studentinstrument.Focus();
            this.ActiveControl = previous1;
        }

        Boolean deletethecurrent;

        private void metroToggle2_CheckedChanged(object sender, EventArgs e)
        {
           
            if (changeplan.Checked)
            {
                SQLiteDataAdapter sda2 = new SQLiteDataAdapter();
              
                sda2 = new SQLiteDataAdapter("select max(rowid)  from FeeshistoryTbl  where Studentid='" + rowid + "'", connection);
                DataTable dg = new DataTable();
                sda2.Fill(dg);

               
                String studentfeeid = dg.Rows[0][0].ToString();
     
                if (studentfeeid != "")
                {
                    sda2 = new SQLiteDataAdapter("select Status,Lastday  from FeeshistoryTbl  where rowid='" + studentfeeid + "'", connection);
                    dg = new DataTable();
                    sda2.Fill(dg);

                    if (dg.Rows[0][0].ToString() == "Unpaid")
                    {

                        DialogResult result = MetroMessageBox.Show(this, "This student's current plan is still unpaid and its last date is " + Convert.ToDateTime(dg.Rows[0][1]).ToString("dd/MM/yyyy") + ".\n\nPress 'Yes' if you want to delete the current plan.\nPress 'No' if you want to keep the current plan in history.", "Notification", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                        if (result == DialogResult.Yes)
                        {

                            deletethecurrent = true;

                            toggleOn();
                        }
                        else if (result == DialogResult.No)
                        {
                            deletethecurrent = false;
                            toggleOn();
                        }
                        else if (result == DialogResult.Cancel)
                        {

                            toggleOff();
                            deletethecurrent = false;
                        }


                    }
                    else
                    {
                        toggleOn(); deletethecurrent = false;
                    }
                }
                else
                {
                    toggleOn(); deletethecurrent = false;
                }

        }
            else 
            {

                toggleOff(); deletethecurrent = false;
            }
           
        }

        private void toggleOff()
        {
            try
            {
                changeplan.Checked = false;
                paymentplan.Enabled = false;
                monthlypaymentdate.Enabled = false;
                quartelypaymentdate.Enabled = false;
                firstinstallmentdate.Enabled = false;
                paymentmode.Enabled = false;
                noofinst.  Enabled = false;
                fullpaymentdate.Enabled = false;
                deletethecurrent = false;
                monthlypaymentdate.Checked = false;
                quartelypaymentdate.Checked = false;
                fullpaymentdate.Checked = false;
                firstinstallmentdate.Checked = false;

                SQLiteDataAdapter sda2 = new SQLiteDataAdapter();


                sda2 = new SQLiteDataAdapter("select max(rowid)  from FeeshistoryTbl  where Studentid='" + rowid + "'", connection);
                DataTable dg = new DataTable();
                sda2.Fill(dg);
                string feeid = dg.Rows[0][0].ToString();

                SQLiteDataAdapter d = new SQLiteDataAdapter("select Paymentplan,Paymentmode from StudentTbl where rowid='" + rowid + "'", connection);

                DataTable dt = new DataTable();
                d.Fill(dt);

                if (dt.Rows[0][0].ToString() == "Monthly")
                {
                    monthly.Checked = true;
                    monthlypaymentdate.Visible = true;
                    monthlypaymentlabel.Visible = true;
                }
                else if (dt.Rows[0][0].ToString() == "Quartely")
                {
                    quarterly.Checked = true;
                }
                else if (dt.Rows[0][0].ToString() == "Half Yearly")
                {
                    halfyearly.Checked = true;
                }
                else if (dt.Rows[0][0].ToString() == "Annually")
                {
                    annually.Checked = true;
                }



                if (dt.Rows[0][1].ToString() == "Installments")
                {   
                    installments.Checked = true;
                    firstinstalldatelbl.Visible = true;
                    firstinstallmentdate.Visible = true;
                    fullpaymentdate.Visible = false;
                    fullpaymentdatelbl.Visible = false;
                    noofinst.Visible = true;
                    numberofinstallmentlbl.Visible = true;
                    d = new SQLiteDataAdapter("select count(*)  from InstallmentsTbl where Studentid='" + rowid + "' and Feeid='" + feeid + "'", connection);

                    dt = new DataTable();
                    d.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "0") 
                    { noofinst.Value = 2; }
                    else
                    {
                        noofinst.Value = Convert.ToInt16(dt.Rows[0][0]);
                    }
                }

                SQLiteCommand s;
                SQLiteDataReader reader;
                if (installments.Checked)
                {

                    s = new SQLiteCommand("select Status,Paymentdate from InstallmentsTbl  where Studentid='" + rowid + "' and Installment='1st installment' and Feeid='" + feeid + "'", connection);
                    reader = s.ExecuteReader();

                    while (reader.Read())
                    {
                        if (reader.GetString(reader.GetOrdinal("Status")) == "Paid")
                        {
                           
                            firstinstallmentdate.Checked = installments.Checked ? true : false;
                            firstinstallmentdate.Value = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("Paymentdate")));

                        }
                        else
                        {
                            firstinstallmentdate.Checked = false;
                        }


                    }
                }
                else if (fullpayment.Checked)
                {
                    s = new SQLiteCommand("select Status,Paymentdate from FeeshistoryTbl  where Studentid='" + rowid + "'", connection);
                    reader = s.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetString(reader.GetOrdinal("Status")) == "Paid")
                        {
                          


                            monthlypaymentdate.Checked = monthly.Checked ? true : false;
                            quartelypaymentdate.Checked = quarterly.Checked ? true : false;
                            fullpaymentdate.Checked = fullpayment.Checked ? true : false;

                            if (monthlypaymentdate.Checked)
                            {
                                monthlypaymentdate.Value = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("Paymentdate")));
                            }
                            else if (quartelypaymentdate.Checked)
                            {
                                quartelypaymentdate.Value = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("Paymentdate")));
                            }
                            else if (fullpaymentdate.Checked)
                            {
                                fullpaymentdate.Value = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("Paymentdate")));
                            }

                        }
                        else
                        {
                            monthlypaymentdate.Checked = false;
                            quartelypaymentdate.Checked = false;
                            fullpaymentdate.Checked = false;
                        }
                    }
                }

                 d = new SQLiteDataAdapter("select Fees,Discount,Totalfees from StudentTbl where rowid='" + rowid + "'", connection);

                 dt = new DataTable();
                d.Fill(dt);

          

                fees.Text = dt.Rows[0][0].ToString();

                if (dt.Rows[0][1].ToString() == "No discount")
                {
                    discount.Clear();
                }
                else
                {
                    discount.Text = dt.Rows[0][1].ToString();

                }

                totalfees.Text = dt.Rows[0][2].ToString();
       
           
            }
            catch (Exception exe)
            {
                MetroMessageBox.Show(this, exe.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            


        }

        private void toggleOn()
        {
            paymentplan.Enabled = true;
            monthlypaymentdate.Enabled = true;
            quartelypaymentdate.Enabled = true;
            firstinstallmentdate.Enabled = true;
            paymentmode.Enabled = true;
            noofinst.  Enabled = true;
            fullpaymentdate.Enabled = true;
          
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

        private void editstudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {

                SQLiteDataAdapter m = new SQLiteDataAdapter();

                SQLiteDataAdapter search = new SQLiteDataAdapter("select Instructorname from StudentTbl where rowid='" + rowid + "'", connection);
                DataTable searchdt = new DataTable();
                search.Fill(searchdt);
                string instructorname=searchdt.Rows[0][0].ToString();
                search = new SQLiteDataAdapter("select count(rowid) from StudentTbl where Instructorname='"+instructorname+"'", connection);
                searchdt = new DataTable();
                search.Fill(searchdt);

                if (Convert.ToInt16(searchdt.Rows[0][0].ToString()) == 1)
                {
                    m.DeleteCommand = new SQLiteCommand("Delete from LoginTbl where Username='" + instructorname.ToLower()+ "' and Role='Instructor'", connection);

                    m.DeleteCommand.ExecuteNonQuery();
                }



               
                        m.DeleteCommand = new SQLiteCommand("Delete from StudentTbl where rowid='" + rowid + "'", connection);

                        m.DeleteCommand.ExecuteNonQuery();

                        m.DeleteCommand = new SQLiteCommand("Delete from FeeshistoryTbl where Studentid='" + rowid + "'", connection);

                        m.DeleteCommand.ExecuteNonQuery();

                        m.DeleteCommand = new SQLiteCommand("Delete from InstallmentsTbl where Studentid='" + rowid + "'", connection);

                        m.DeleteCommand.ExecuteNonQuery();

                        m.DeleteCommand = new SQLiteCommand("Delete from AttendancerecordTbl where Studentid='" + rowid + "'", connection);

                        m.DeleteCommand.ExecuteNonQuery();

                        m.DeleteCommand = new SQLiteCommand("Delete from CompensateTbl where Studentid='" + rowid + "'", connection);

                        m.DeleteCommand.ExecuteNonQuery();

                        
                        rowid = "";
                        int a = gridviewmain.CurrentCell.RowIndex;
                        gridViewgenerate();
                        if (a != 0)
                        {
                            gridviewmain.CurrentCell = gridviewmain.Rows[a - 1].Cells[0];
                            DataGridViewRow row = this.gridviewmain.Rows[a - 1];
                            rowid = row.Cells["Id"].Value.ToString();
                        }
                        MetroMessageBox.Show(this, "Student deleted..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        panel2.Visible = false;
                        panel1.Visible = true;
                        tabcontrol.Visible = false;
                        this.ActiveControl = update;
                    

               
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
            tabcontrol.Visible = false;
            this.ActiveControl = update;
        }
    
        private void dateofjoining_Validating(object sender, CancelEventArgs e)
        {
              
            if (dateofjoining.Checked)
            {

                if (dateofjoining.Value.Date < DateTime.Now.Date)
                {
                    MetroMessageBox.Show(this, "Date of joining not valid..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    dateofjoining.Value = DateTime.Now;

                }
            }
            else
            {

                dateofjoining.Value = olddoj; dateofjoining.Checked = false;



               
            }
        }

      

       

    }
}
