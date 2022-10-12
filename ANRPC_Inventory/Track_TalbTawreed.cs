using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace ANRPC_Inventory
{
    public partial class Track_TalbTawreed : Form
    {
        public int StepFlag;
        public int talbstatus;

        public Track_TalbTawreed()
        {
            InitializeComponent();
            

        }
        public Track_TalbTawreed(string x,string y)
        {
            InitializeComponent();
            Cmb_FYear2.Text = x;
            Cmb_TalbNo2.Text = y;

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            /*  Graphics surface = e.Graphics;
            Pen pen1 = new Pen(Color.Black, 2);
            surface.DrawLine(pen1, panel1.Location.X + 4,  4, panel1.Location.X + 4, panel1.Location.Y + panel1.Size.Height); // Left Line
            surface.DrawLine(pen1, panel1.Size.Width - 4, 4, panel1.Size.Width - 4, panel1.Location.Y + panel1.Size.Height); // Right Line
            //---------------------------
            surface.DrawLine(pen1, 4,4, panel1.Location.X + panel1.Size.Width - 4,4); // Top Line
            surface.DrawLine(pen1, 4, panel1.Size.Height -1, panel1.Location.X + panel1.Size.Width - 4, panel1.Size.Height -1); // Bottom Line
       */
            //---------------------------
            // Middle_Line
            //-------------
           // surface.DrawLine(pen1, ((panel1.Size.Width) / 2) + 4, 4, ((panel1.Size.Width) / 2) + 4, panel1.Location.Y + panel1.Size.Height); // Left Line
            //surface.DrawLine(pen1, 4, 38, panel1.Location.X + panel1.Size.Width - 4, 40); // Top Line
          //  surface.Dispose();
        
        }

        private void Cmb_FYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            StepFlag = 0;
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            string cmdstring = "";
            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            if (Constants.User_Type == "A")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and CodeEdara=@CE  ";

            }
            else if (Constants.User_Type == "B" )
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY ";
            }
                /*
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Stock")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and( Stock_Sign is not null) and (Sign9 is not  null) and CH_Sign is not null and (Audit_Sign is not null) and Mohmat_Sign is null)";

            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == " Purchases")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and( Stock_Sign is not null) and (Sign9 is not  null) and CH_Sign is not null and (Audit_Sign is null )and Mohmat_Sign is null)";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "GMInventory")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and( Stock_Sign is not null) and (Sign9 is not  null) and CH_Sign is not null and (Audit_Sign is not null )and Mohmat_Sign is null";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "NewTasnif")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is null ) ";

            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Mwazna")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is null or Stock_Sign is null)";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "TechnicalFollowUp")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and( Stock_Sign is not  null) and Sign9 is null";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Chairman")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and( Stock_Sign is not null) and (Sign9 is  not null) and( CH_Sign is null)";

            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Purchases")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and( Stock_Sign is not null) and (Sign9 is  not null) and( CH_Sign is not  null) and Audit_Sign is null";

            }*/
            //string cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and CodeEdara=@CE  ";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
            cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);


            DataTable dts = new DataTable();

            dts.Load(cmd.ExecuteReader());
            Cmb_TalbNo2.DataSource = dts;
            Cmb_TalbNo2.ValueMember = "TalbTwareed_No";
            Cmb_TalbNo2.DisplayMember = "TalbTwareed_No";
            Cmb_TalbNo2.SelectedIndex = -1;
            Cmb_TalbNo2.SelectedIndexChanged += new EventHandler(Cmb_TalbNo2_SelectedIndexChanged);
            Constants.closecon();
           
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Cmb_TalbNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            StepFlag = 0;
            SearchTalb(2);
            CountDays(2);
        }

        public void CountDays(int x)
        {
            Constants.opencon();
            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring = "";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
            if (x == 1 && Constants.User_Type == "A")
            {
                cmdstring = "select date1,date2, ((DATEDIFF(dd,Date1, Date2)+1 )-(DATEDIFF(wk, date1, Date2) * 2) -(CASE WHEN DATENAME(dw, date1)  in( 'Friday' , 'Saturday')THEN 1 ELSE 0 END) -(CASE WHEN DATENAME(dw, date2)  in( 'Friday' , 'Saturday') THEN 1 ELSE 0 END)) as dayscount,SignatureNo FROM [T_SignaturesDates] where formno=1 and TalbTwareed_No=@TN and FYear=@FY";// and CodeEdara=@EC";
               
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", Cmb_TalbNo2.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
             //   cmd.Parameters.AddWithValue("@EC", Constants.CodeEdara);
            }
            else if (x == 2 && Constants.User_Type == "A")
            {
                cmdstring = "select date1,date2, ((DATEDIFF(dd,Date1, Date2)+1 )-(DATEDIFF(wk, date1, Date2) * 2) -(CASE WHEN DATENAME(dw, date1)  in( 'Friday' , 'Saturday')THEN 1 ELSE 0 END) -(CASE WHEN DATENAME(dw, date2)  in( 'Friday' , 'Saturday') THEN 1 ELSE 0 END)) as dayscount,SignatureNo FROM [T_SignaturesDates] where formno=1 and TalbTwareed_No=@TN and FYear=@FY";// and CodeEdara=@EC";
               
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", Cmb_TalbNo2.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
             //   cmd.Parameters.AddWithValue("@EC", Constants.CodeEdara);
            }
            else if (x == 2 && Constants.User_Type == "B")
            {
                cmdstring = "select date1,date2, ((DATEDIFF(dd,Date1, Date2)+1 )-(DATEDIFF(wk, date1, Date2) * 2) -(CASE WHEN DATENAME(dw, date1)  in( 'Friday' , 'Saturday')THEN 1 ELSE 0 END) -(CASE WHEN DATENAME(dw, date2)  in( 'Friday' , 'Saturday') THEN 1 ELSE 0 END)) as dayscount,SignatureNo FROM [T_SignaturesDates] where formno=1 and TalbTwareed_No=@TN and FYear=@FY";
               
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", Cmb_TalbNo2.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            }
            else if (x == 2 && Constants.User_Type == "B")
            {
                cmdstring = "select date1,date2, ((DATEDIFF(dd,Date1, Date2)+1 )-(DATEDIFF(wk, date1, Date2) * 2) -(CASE WHEN DATENAME(dw, date1)  in( 'Friday' , 'Saturday')THEN 1 ELSE 0 END) -(CASE WHEN DATENAME(dw, date2)  in( 'Friday' , 'Saturday') THEN 1 ELSE 0 END)) as dayscount,SignatureNo FROM [T_SignaturesDates] where formno=1 and TalbTwareed_No=@TN and FYear=@FY ";
               
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", Cmb_TalbNo2.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            }
            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);


            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {

                    int SignatureCode = Convert.ToInt32(dr["SignatureNo"].ToString());

                    if (SignatureCode == 1)
                    {
                        string C1 = dr["dayscount"].ToString();
                        Count1.Visible = true;
                        Count1.Text = C1;
                        if (C1 != "")
                        {
                            int s1 = Convert.ToInt32(C1);
                            if (s1 <= 2)
                            {
                                Count1.ForeColor = Color.Green;
                            }
                            else if (s1 > 2 && s1 < 5)
                            {
                                Count1.ForeColor = Color.Yellow;
                            }
                            else if (s1 >= 5)
                            {
                                Count1.ForeColor = Color.Red;
                            }
                        }
                        string CC1 = dr["Date1"].ToString();
                        if (CC1 != "")
                        {

                            StartDate1.Text = (Convert.ToDateTime(dr["Date1"].ToString())).ToShortDateString();
                        }
                       // if (C1== "" && StepFlag != 1)
                        if (C1 == "" && CC1 !="")
                        {
                            DateTime oDate = Convert.ToDateTime(dr["Date1"].ToString());
                            DateTime dt = DateTime.Now;
                            int diff = Convert.ToInt32((dt - oDate).TotalDays);
                            Count1.Text = "This step is late for " + diff.ToString() + " days";
                            Count1.ForeColor = Color.Red;
                      
                            StartDate1.Visible = true;
                            StepFlag = 1;
                        }

                    }
                    else
                    {
                        Count1.Visible = true;
                    }
                    if (SignatureCode == 2)
                    {
                        string C2 = dr["dayscount"].ToString();
                        Count2.Visible = true;
                        Count2.Text = C2 ;
                        if (C2 != "")
                        {
                            int s2 = Convert.ToInt32(C2);
                            if (s2 <= 2)
                            {
                                Count2.ForeColor = Color.Green;
                            }
                            else if (s2 > 2 && s2 < 5)
                            {
                                Count2.ForeColor = Color.Yellow;
                            }
                            else if (s2 >= 5)
                            {
                                Count2.ForeColor = Color.Red;
                            }
                        }
                        string CC2 = dr["Date1"].ToString();
                        if (CC2 != "")
                        {

                            StartDate2.Text = (Convert.ToDateTime(dr["Date1"].ToString())).ToShortDateString();
                        }
                        // if (C1== "" && StepFlag != 1)
                        if (C2== "" && CC2 != "")
                        {
                            DateTime oDate = Convert.ToDateTime(dr["Date1"].ToString());
                            DateTime dt = DateTime.Now;
                            int diff = Convert.ToInt32((dt - oDate).TotalDays);
                            Count2.Text = "This step is late for " + diff.ToString() + " days";
                            Count2.ForeColor = Color.Red;
                            StartDate2.Visible = true;
                            StepFlag = 1;
                        }

                    }
                    else
                    {
                        Count2.Visible = true;
                    }
                    if (SignatureCode == 3)
                    {
                        string C3 = dr["dayscount"].ToString();
                        Count3.Visible = true;
                        Count3.Text = C3;
                        if (C3 != "")
                        {
                            int s3 = Convert.ToInt32(C3);
                            if (s3 <= 2)
                            {
                                Count3.ForeColor = Color.Green;
                            }
                            else if (s3 > 2 && s3 < 5)
                            {
                                Count3.ForeColor = Color.Yellow;
                            }
                            else if (s3 >= 5)
                            {
                                Count3.ForeColor = Color.Red;
                            }
                        }
                        string CC3 = dr["Date1"].ToString();
                        if (CC3 != "")
                        {

                            StartDate3.Text = (Convert.ToDateTime(dr["Date1"].ToString())).ToShortDateString();
                        }
                        // if (C1== "" && StepFlag != 1)
                        if (C3 == "" && CC3 != "")
                        {
                            DateTime oDate = Convert.ToDateTime(dr["Date1"].ToString());
                            DateTime dt = DateTime.Now;
                            int diff = Convert.ToInt32((dt - oDate).TotalDays);
                            Count3.Text = "This step is late for " + diff.ToString() + " days";
                            Count3.ForeColor = Color.Red;
                            StartDate3.Visible = true;
                            StepFlag = 1;
                        }
                     
                            

                    }
                    else
                    {
                        Count3.Visible = true;
                    }
                    if (SignatureCode == 8)
                    {
                        string C4 = dr["dayscount"].ToString();
                      //  StartDate4.Text = dr["Date1"].ToString();
                        Count4.Visible = true;
                        Count4.Text = C4;
                        if (C4 != "")
                        {
                            int s4 = Convert.ToInt32(C4);
                            if (s4 <= 2)
                            {
                                Count4.ForeColor = Color.Green;
                            }
                            else if (s4 > 2 && s4 < 5)
                            {
                                Count4.ForeColor = Color.Yellow;
                            }
                            else if (s4 >= 5)
                            {
                                Count4.ForeColor = Color.Red;
                            }
                        }
                        string CC4= dr["Date1"].ToString();
                        if (CC4 != "")
                        {

                            StartDate4.Text = (Convert.ToDateTime(dr["Date1"].ToString())).ToShortDateString();
                        }
                        // if (C1== "" && StepFlag != 1)
                        if (C4 == "" && CC4 != "")
                        {
                            DateTime oDate = Convert.ToDateTime(dr["Date1"].ToString());
                            DateTime dt = DateTime.Now;
                            int diff = Convert.ToInt32((dt - oDate).TotalDays);
                            Count4.Text ="This step is late for "+ diff.ToString()+ " days";
                            Count4.ForeColor = Color.Red;
                            StartDate4.Visible = true;
                            StepFlag = 1;
                        }

                    }
                    else
                    {
                        Count4.Visible = true;
                    }
                    /////////////////////////////////////////////

                    if (SignatureCode == 12)
                    {
                        string C12 = dr["dayscount"].ToString();
                        //  StartDate4.Text = dr["Date1"].ToString();
                        Count44.Visible = true;
                        Count44.Text = C12;
                        if (C12 != "")
                        {
                            int s12 = Convert.ToInt32(C12);
                            if (s12 <= 2)
                            {
                                Count44.ForeColor = Color.Green;
                            }
                            else if (s12 > 2 && s12 < 5)
                            {
                                Count4.ForeColor = Color.Yellow;
                            }
                            else if (s12 >= 5)
                            {
                                Count44.ForeColor = Color.Red;
                            }
                        }
                        string CC12 = dr["Date1"].ToString();
                        if (CC12 != "")
                        {

                            StartDate12.Text = (Convert.ToDateTime(dr["Date1"].ToString())).ToShortDateString();
                        }
                        // if (C1== "" && StepFlag != 1)
                        if (C12 == "" && CC12!= "")
                        {
                            DateTime oDate = Convert.ToDateTime(dr["Date1"].ToString());
                            DateTime dt = DateTime.Now;
                            int diff = Convert.ToInt32((dt - oDate).TotalDays);
                            Count44.Text = "This step is late for " + diff.ToString() + " days";
                            Count44.ForeColor = Color.Red;
                            StartDate12.Visible = true;
                            StepFlag = 1;
                        }

                    }
                    else
                    {
                        Count44.Visible = true;
                    }



                    /////////////////////////////////////////////////
                    if (SignatureCode == 4)
                    {
                        string C5 = dr["dayscount"].ToString();
                        Count5.Visible = true;
                        Count5.Text = C5;
                        if (C5 != "")
                        {
                            int s5 = Convert.ToInt32(C5);
                            if (s5 <= 2)
                            {
                                Count5.ForeColor = Color.Green;
                            }
                            else if (s5 > 2 && s5 < 5)
                            {
                                Count5.ForeColor = Color.Yellow;
                            }
                            else if (s5 >= 5)
                            {
                                Count5.ForeColor = Color.Red;
                            }
                        }
                        string CC5 = dr["Date1"].ToString();
                        if (CC5 != "")
                        {

                            StartDate5.Text = (Convert.ToDateTime(dr["Date1"].ToString())).ToShortDateString();
                        }
                        // if (C1== "" && StepFlag != 1)
                        if (C5== "" && CC5 != "")
                        {
                            DateTime oDate = Convert.ToDateTime(dr["Date1"].ToString());
                            DateTime dt = DateTime.Now;
                            int diff = Convert.ToInt32((dt - oDate).TotalDays);
                            Count5.Text = "This step is late for " + diff.ToString() + " days";
                            Count5.ForeColor = Color.Red;
                            StartDate5.Visible = true;
                            StepFlag = 1;
                        }

                    }
                    else
                    {
                        Count5.Visible = true;
                    }
                    if (SignatureCode == 11)
                    {
                        string C6 = dr["dayscount"].ToString();
                        Count6.Visible = true;
                        Count6.Text = C6;
                        if (C6 != "")
                        {
                            int s6 = Convert.ToInt32(C6);
                            if (s6 <= 2)
                            {
                                Count6.ForeColor = Color.Green;
                            }
                            else if (s6 > 2 && s6 < 5)
                            {
                                Count6.ForeColor = Color.Yellow;
                            }
                            else if (s6 >= 5)
                            {
                                Count6.ForeColor = Color.Red;
                            }
                        }
                        string CC6 = dr["Date1"].ToString();
                        if (CC6 != "")
                        {

                            StartDate6.Text = (Convert.ToDateTime(dr["Date1"].ToString())).ToShortDateString();
                        }
                        // if (C1== "" && StepFlag != 1)
                        if (C6 == "" && CC6 != "")
                        {
                            DateTime oDate = Convert.ToDateTime(dr["Date1"].ToString());
                            DateTime dt = DateTime.Now;
                            int diff = Convert.ToInt32((dt - oDate).TotalDays);
                            Count6.Text = "This step is late for " + diff.ToString() + " days";
                            Count6.ForeColor = Color.Red;
                            StartDate6.Visible = true;
                            StepFlag = 1;
                        }

                    }
                    else
                    {
                        Count6.Visible = true;
                    }
                    if (SignatureCode == 9)
                    {
                        string C7 = dr["dayscount"].ToString();
                        Count7.Visible = true;
                        Count7.Text = C7;
                        if (C7 != "")
                        {
                            int s7 = Convert.ToInt32(C7);
                            if (s7 <= 2)
                            {
                                Count7.ForeColor = Color.Green;
                            }
                            else if (s7 > 2 && s7 < 5)
                            {
                                Count7.ForeColor = Color.Yellow;
                            }
                            else if (s7 >= 5)
                            {
                                Count7.ForeColor = Color.Red;
                            }
                        }
                        string CC7 = dr["Date1"].ToString();
                        if (CC7 != "")
                        {

                            StartDate7.Text = (Convert.ToDateTime(dr["Date1"].ToString())).ToShortDateString();
                        }
                        // if (C1== "" && StepFlag != 1)
                        if (C7 == "" && CC7 != "")
                        {
                            DateTime oDate = Convert.ToDateTime(dr["Date1"].ToString());
                            DateTime dt = DateTime.Now;
                            int diff = Convert.ToInt32((dt - oDate).TotalDays);
                            Count7.Text = "This step is late for " + diff.ToString() + " days";
                            Count7.ForeColor = Color.Red;
                            StartDate7.Visible = true;
                            StepFlag = 1;
                        }

                    }
                    else
                    {
                        Count7.Visible = true;
                    }
                    if (SignatureCode == 7)
                    {
                        string C8 = dr["dayscount"].ToString();
                        Count8.Visible = true;
                        Count8.Text = C8;
                        if (C8 != "")
                        {
                            int s8 = Convert.ToInt32(C8);
                            if (s8 <= 2)
                            {
                                Count8.ForeColor = Color.Green;
                            }
                            else if (s8 > 2 && s8 < 5)
                            {
                                Count8.ForeColor = Color.Yellow;
                            }
                            else if (s8 >= 5)
                            {
                                Count8.ForeColor = Color.Red;
                            }
                        }
                        string CC8 = dr["Date1"].ToString();
                        if (CC8 != "")
                        {

                            StartDate8.Text = (Convert.ToDateTime(dr["Date1"].ToString())).ToShortDateString();
                        }
                        // if (C1== "" && StepFlag != 1)
                        if (C8 == "" && CC8!= "")
                        {
                            DateTime oDate = Convert.ToDateTime(dr["Date1"].ToString());
                            DateTime dt = DateTime.Now;
                            int diff = Convert.ToInt32((dt - oDate).TotalDays);
                            Count8.Text = "This step is late for " + diff.ToString() + " days";
                            Count8.ForeColor = Color.Red;
                            StartDate8.Visible = true;
                            StepFlag = 1;
                        }

                    }
                    else
                    {
                        Count8.Visible = true;
                    }
                    if (SignatureCode == 5)
                    {
                        string C9 = dr["dayscount"].ToString();

                        Count9.Visible = true;
                        Count9.Text = C9;
                        if (C9 != "")
                        {
                            int s9 = Convert.ToInt32(C9);
                            if (s9 <= 2)
                            {
                                Count9.ForeColor = Color.Green;
                            }
                            else if (s9 > 2 && s9 < 5)
                            {
                                Count9.ForeColor = Color.Yellow;
                            }
                            else if (s9 >= 5)
                            {
                                Count9.ForeColor = Color.Red;
                            }
                        }
                        string CC9 = dr["Date1"].ToString();
                        if (CC9 != "")
                        {


                            StartDate9.Text = (Convert.ToDateTime(dr["Date1"].ToString())).ToShortDateString();
                        }
                        // if (C1== "" && StepFlag != 1)
                        if (C9== "" && CC9!= "")
                        {
                            DateTime oDate = Convert.ToDateTime(dr["Date1"].ToString());
                            DateTime dt = DateTime.Now;
                            int diff = Convert.ToInt32((dt - oDate).TotalDays);
                            Count9.Text = "This step is late for " + diff.ToString() + " days";
                            Count9.ForeColor = Color.Red;
                          //  StartDate9.Text = oDate.ToShortDateString();
                            StartDate9.Visible = true;
                            StepFlag = 1;
                        }

                    }
                    else
                    {
                        Count9.Visible = true;
                    }
                    if (SignatureCode == 6)
                    {
                        string C10 = dr["dayscount"].ToString();
                        Count10.Visible = true;
                        Count10.Text = C10;
                        if (C10 != "")
                        {
                            int s10 = Convert.ToInt32(C10);
                            if (s10 <= 2)
                            {
                                Count10.ForeColor = Color.Green;
                            }
                            else if (s10 > 2 && s10 < 5)
                            {
                                Count10.ForeColor = Color.Yellow;
                            }
                            else if (s10 >= 5)
                            {
                                Count10.ForeColor = Color.Red;
                            }
                        }
                        string CC10 = dr["Date1"].ToString();
                        if (CC10 != "")
                        {

                            StartDate10.Text = (Convert.ToDateTime(dr["Date1"].ToString())).ToShortDateString();
                        }
                        // if (C1== "" && StepFlag != 1)
                        if (C10 == "" && CC10 != "")
                        {

                            DateTime oDate = Convert.ToDateTime(dr["Date1"].ToString());
                            DateTime dt = DateTime.Now;
                            int diff = Convert.ToInt32((dt - oDate).TotalDays);
                            Count10.Text = "This step is late for " + diff.ToString() + " days";
                            Count10.ForeColor = Color.Red;
                            StartDate10.Visible = true;
                            StepFlag = 1;
                        }

                    }
                    else
                    {
                        Count10.Visible = true;
                    }
                    ////////////////////////////new//////////////////////////////
                    if (SignatureCode == 13)
                    {
                        string C13 = dr["dayscount"].ToString();
                        Count13.Visible = true;
                        Count13.Text = C13;
                        if (C13 != "")
                        {
                            int s13 = Convert.ToInt32(C13);
                            if (s13 <= 2)
                            {
                                Count13.ForeColor = Color.Green;
                            }
                            else if (s13 > 2 && s13 < 5)
                            {
                                Count13.ForeColor = Color.Yellow;
                            }
                            else if (s13 >= 5)
                            {
                                Count13.ForeColor = Color.Red;
                            }
                        }
                        string CC13 = dr["Date1"].ToString();
                        if (CC13 != "")
                        {

                            StartDate13.Text = (Convert.ToDateTime(dr["Date1"].ToString())).ToShortDateString();
                        }
                        // if (C1== "" && StepFlag != 1)
                        if (C13 == "" && CC13 != "")
                        {

                            DateTime oDate = Convert.ToDateTime(dr["Date1"].ToString());
                            DateTime dt = DateTime.Now;
                            int diff = Convert.ToInt32((dt - oDate).TotalDays);
                            Count13.Text = "This step is late for " + diff.ToString() + " days";
                            Count13.ForeColor = Color.Red;
                            StartDate13.Visible = true;
                            StepFlag = 1;
                        }

                    }
                    else
                    {
                        Count13.Visible = true;
                    }
                    ///////////////////////////////////////////////////////////////////////
                }


            }


            else
            {
              //  MessageBox.Show("من فضلك تاكد من رقم طلب التوريد");


             //   return;

            }
            dr.Close();


            //  string query1 = "SELECT  [TalbTwareed_No] ,[FYear] ,[Bnd_No],[RequestedQuan],[Unit],[BIAN_TSNIF] ,[STOCK_NO_ALL],[Quan] ,[ArrivalDate] FROM [T_TalbTawreed_Benod] where  [TalbTwareed_No]=@T and [FYear]=@F ";
            //  SqlCommand cmd1 = new SqlCommand(query1, Constants.con);
            //  cmd1.Parameters.AddWithValue("@T",Cmb_TalbNo2.Text);
            //  cmd1.Parameters.AddWithValue("@F", Cmb_FYear2.Text);


            // DT.Clear();
            // DT.Load(cmd1.ExecuteReader());
            // cleargridview();

            // searchbtn1 = false;
            //  DataGridViewReset();
            if (talbstatus == 1)
            {
                label9.Visible = false;
                Count7.Visible = false;
                pictureBox4.Visible = false;
                StartDate7.Visible = false;

                label8.Visible = false;
                Count8.Visible = false;
                pictureBox9.Visible = false;
                StartDate8.Visible = false;
                label133.Visible = false;
                Count13.Visible = false;
                StartDate13.Visible = false;
            }


            if (talbstatus == 2)
            {
                label9.Visible = false;
                Count7.Visible = false;
                pictureBox4.Visible = false;
                StartDate7.Visible = false;

                label8.Visible = false;
                Count8.Visible = false;
                pictureBox9.Visible = false;
                StartDate8.Visible = false;

                label133.Visible = true;
                Count13.Visible = true;
                StartDate13.Visible = true;
                pictureBox4.Visible = true;

            }

            if (talbstatus == 3 || talbstatus==4)
            {
                label133.Visible = false;
                Count13.Visible = false;
                StartDate13.Visible = false;
                pictureBox4.Visible = true;

                //  label8.Visible = false;
                //  Count8.Visible = false;
                //  pictureBox9.Visible = false;
            }
            Constants.closecon();
        }
        public void SearchTalb(int x)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring = "";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
            if (x == 1 && Constants.User_Type == "A")
            {
                cmdstring = "select * from  T_TalbTawreed where TalbTwareed_No=@TN and FYear=@FY and CodeEdara=@EC";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", Cmb_TalbNo2.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
                cmd.Parameters.AddWithValue("@EC", Constants.CodeEdara);
            }
            else if (x == 2 && Constants.User_Type == "A")
            {
                cmdstring = "select * from  T_TalbTawreed where TalbTwareed_No=@TN and FYear=@FY and CodeEdara=@EC";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", Cmb_TalbNo2.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
                cmd.Parameters.AddWithValue("@EC", Constants.CodeEdara);
            }
            else if (x == 2 && Constants.User_Type == "B")
            {
                cmdstring = "select * from  T_TalbTawreed where TalbTwareed_No=@TN and FYear=@FY";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", Cmb_TalbNo2.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            }
            else if (x == 2 && Constants.User_Type == "B")
            {
                cmdstring = "select * from  T_TalbTawreed where TalbTwareed_No=@TN and FYear=@FY";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", Cmb_TalbNo2.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            }
            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);


            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                
                    string s1 = dr["Req_Signature"].ToString();
                    string s2 = dr["Confirm_Sign1"].ToString();
                    string s3 = dr["Confirm_Sign2"].ToString();
                    string s4 = dr["Stock_Sign"].ToString();
                    string s5 = dr["Audit_Sign"].ToString();
                    string s6 = dr["Mohmat_Sign"].ToString();
                    string s7 = dr["CH_Sign"].ToString();

                    string s8 = dr["Sign8"].ToString();
                    string s9 = dr["Sign9"].ToString();
                    string s10 = dr["Sign10"].ToString();
                    string s11 = dr["Sign11"].ToString();
                    string s12 = dr["Sign12"].ToString();
                    string s13 = dr["Sign13"].ToString();

                    string BUM = dr["BuyMethod"].ToString();

                    if (s1 != "")
                    {
                        label1.ForeColor = Color.Green;
                    }
                    else
                    {
                        label1.ForeColor = Color.Red;
                    }
                    if (s2 != "")
                    {
                        label2.ForeColor = Color.Green;
                    }
                    else
                    {
                        label2.ForeColor = Color.Red;
                    }
                    if (s3 != "")
                    {
                        label3.ForeColor = Color.Green;
                    }
                    else
                    {
                        label3.ForeColor = Color.Red;
                    }

                    ////////////////////////////
                    if (s4 != "")
                    {
                        label6.ForeColor = Color.Green;
                    }
                    else
                    {
                        label6.ForeColor = Color.Red;
                    }
                    ///////////////////// ////////////////////////////
                    if (s5!= "")
                    {
                        label7.ForeColor = Color.Green;
                    }
                    else
                    {
                        label7.ForeColor = Color.Red;
                    }
                    /////////////////////
                    ///////////////////// ////////////////////////////
                    if (s6 != "")
                    {
                        label5.ForeColor = Color.Green;
                    }
                    else
                    {
                        label5.ForeColor = Color.Red;
                    }
                    /////////////////////

                    if (s7 != "")
                    {
                        label8.ForeColor = Color.Green;
                    }
                    else
                    {
                        label8.ForeColor = Color.Red;
                    }
                    /////////////////////
                    if (s8!= "")
                    {
                        label4.ForeColor = Color.Green;
                    }
                    else
                    {
                        label4.ForeColor = Color.Red;
                    }
                    /////////////////////
                    if (s9 != "")
                    {
                        label9.ForeColor = Color.Green;
                    }
                    else
                    {
                        label9.ForeColor = Color.Red;
                    }
                    /////////////////////
                    if (s11!= "")
                    {
                        label10.ForeColor = Color.Green;
                    }
                    else
                    {
                        label10.ForeColor = Color.Red;
                    }
                    if (s12 != "")
                    {
                        label13.ForeColor = Color.Green;
                    }
                    else
                    {
                        label13.ForeColor = Color.Red;
                    }
                    if (s13 != "")
                    {
                        label133.ForeColor = Color.Green;
                    }
                    else
                    {
                        label133.ForeColor = Color.Red;
                    }
                }
                talbstatus=Constants.GetTalbStatus(Cmb_TalbNo2.Text,Cmb_FYear2.Text);

                if (talbstatus == 1)
                {
                    label9.Visible = false;
                    Count7.Visible = false;
                    pictureBox4.Visible = false;
                    StartDate7.Visible = false;

                    label8.Visible = false;
                    Count8.Visible = false;
                    pictureBox9.Visible = false;
                    StartDate8.Visible = false;
                    label133.Visible = false;
                    Count13.Visible = false;
                    StartDate13.Visible = false;
                }


                if (talbstatus == 2)
                {
                    label9.Visible = false;
                    Count7.Visible = false;
                    pictureBox4.Visible = false;
                    StartDate7.Visible = false;

                    label8.Visible = false;
                    Count8.Visible = false;
                    pictureBox9.Visible = false;
                    StartDate8.Visible = false;

                    label133.Visible = true;
                    Count13.Visible = true;
                    StartDate13.Visible = true;
                    pictureBox4.Visible = true;

                }

                if (talbstatus == 3 || talbstatus == 4)
                {
                    label133.Visible = false;
                    Count13.Visible = false;
                    StartDate13.Visible = false;
                    pictureBox4.Visible = true;

                    //  label8.Visible = false;
                    //  Count8.Visible = false;
                    //  pictureBox9.Visible = false;
                }
            }
          

            else
            {
                MessageBox.Show("من فضلك تاكد من رقم طلب التوريد");


                return;

            }
            dr.Close();


            //  string query1 = "SELECT  [TalbTwareed_No] ,[FYear] ,[Bnd_No],[RequestedQuan],[Unit],[BIAN_TSNIF] ,[STOCK_NO_ALL],[Quan] ,[ArrivalDate] FROM [T_TalbTawreed_Benod] where  [TalbTwareed_No]=@T and [FYear]=@F ";
            //  SqlCommand cmd1 = new SqlCommand(query1, Constants.con);
            //  cmd1.Parameters.AddWithValue("@T",Cmb_TalbNo2.Text);
            //  cmd1.Parameters.AddWithValue("@F", Cmb_FYear2.Text);


            // DT.Clear();
            // DT.Load(cmd1.ExecuteReader());
            // cleargridview();
        
            // searchbtn1 = false;
            //  DataGridViewReset();

            Constants.closecon();
        }

        private void Cmb_TalbNo2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Count2_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void Count10_Click(object sender, EventArgs e)
        {

        }

        private void Count9_Click(object sender, EventArgs e)
        {

        }

        private void Count8_Click(object sender, EventArgs e)
        {

        }

        private void Count5_Click(object sender, EventArgs e)
        {

        }

        private void Count6_Click(object sender, EventArgs e)
        {

        }

        private void Count7_Click(object sender, EventArgs e)
        {

        }

        private void Count4_Click(object sender, EventArgs e)
        {

        }

        private void Count3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void StartDate1_Click(object sender, EventArgs e)
        {

        }

        private void StartDate2_Click(object sender, EventArgs e)
        {

        }

        private void StartDate3_Click(object sender, EventArgs e)
        {

        }

        private void StartDate4_Click(object sender, EventArgs e)
        {

        }

        private void StartDate5_Click(object sender, EventArgs e)
        {

        }

        private void StartDate6_Click(object sender, EventArgs e)
        {

        }

        private void StartDate7_Click(object sender, EventArgs e)
        {

        }

        private void StartDate8_Click(object sender, EventArgs e)
        {

        }

        private void StartDate9_Click(object sender, EventArgs e)
        {

        }

        private void StartDate10_Click(object sender, EventArgs e)
        {

        }

        private void Track_TalbTawreed_Load(object sender, EventArgs e)
        {
            HelperClass.comboBoxFiller(Cmb_FYear2, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
        }

        private void Track_TalbTawreed_InputLanguageChanging(object sender, InputLanguageChangingEventArgs e)
        {

        }
    }
}
