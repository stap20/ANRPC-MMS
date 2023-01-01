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
using System.Runtime.Serialization;

namespace ANRPC_Inventory
{
    public partial class SearchForm : Form
    {
        public int StepFlag;

        string BN ="";
        string SA = "";
        string BT ="";
        string ST = "";
        public SearchForm()
        {
            InitializeComponent();
            

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

        public Tuple<string, string,int> HandleReports(string TransType)
        {
            string FirstPart = "";
            string ColumnName = "";
            int FormNo = 0;
            if (TransType== "طلب التوريد")
            {
                FirstPart = "select(TalbTwareed_No) from T_TalbTawreed";
                ColumnName = "TalbTwareed_No";
                FormNo = 8;
            }
            else if (TransType == "طلب الاصلاح")
            {
                FirstPart = "SELECT [Eslah_No] FROM [T_TalbEslah]";
                ColumnName = "Eslah_No";
                FormNo = 13;
            }
            else if (TransType == "طلب معايرة")
            {
                FirstPart = "SELECT  [Moaera_No] FROM [T_TalbMoaera]";
                ColumnName = "Moaera_No";
                FormNo = 14;
            }
            else if (TransType == "طلب تنفيذ اعمال")
            {
                FirstPart = "SELECT  [Tanfiz_No]  FROM [T_TalbTanfiz]";
                ColumnName = "Tanfiz_No";
                FormNo = 15;
            }

            return new Tuple<string, string,int>(FirstPart, ColumnName,FormNo);
        }

        private void Cmb_FYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMostandType.Text != "")
            {
                var tuple = HandleReports(cmbMostandType.Text);
                string FP = tuple.Item1;
                string CN = tuple.Item2;
                int FN= tuple.Item3;
                //call sp that get last num that eentered for this MM and this YYYY
                Constants.opencon();
                string cmdstring = "";
                // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
                if (Constants.User_Type == "A" && radioButton2.Checked == true)
                {
                    //  cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and CodeEdara=@CE and CreationDate between @D1 and @D2 ";

                    cmdstring = FP + " where FYear=@FY and CodeEdara=@CE and CreationDate between @D1 and @D2 ";


                }
                if (Constants.User_Type == "A" && radioButton2.Checked == false)
                {
                    //cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and CodeEdara=@CE ";
                    cmdstring = FP + " where FYear=@FY and CodeEdara=@CE ";
                }
                else if (Constants.User_Type == "B" && radioButton1.Checked == true)
                {
                    //cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and CodeEdara=@CE ";
                    cmdstring = FP + " where FYear=@FY and CodeEdara=@CE ";
                }
                else if (Constants.User_Type == "B" && radioButton2.Checked == true)
                {
                    cmdstring = FP + " where FYear=@FY and CreationDate between @D1 and @D2 ";
                }

                else if (Constants.User_Type == "B" && radioButton2.Checked == false && radioButton1.Checked == false)
                {
                    cmdstring = FP + " select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY ";
                }

                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
                cmd.Parameters.AddWithValue("@FY", cmbYear.Text);
                if (radioButton1.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@CE", Cmb_Edara.SelectedValue);


                }
                if (Constants.User_Type == "A")
                {
                    cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);


                }
                cmd.Parameters.AddWithValue("@D1", Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString()));

                cmd.Parameters.AddWithValue("@D2", Convert.ToDateTime(dateTimePicker2.Value.ToShortDateString()));

                DataTable dts = new DataTable();

                //dts.Load(cmd.ExecuteReader());
                //cmbReqNo.DataSource = dts;
                //cmbReqNo.ValueMember = CN;
                //cmbReqNo.DisplayMember = CN;
                //cmbReqNo.SelectedIndex = -1;
                //cmbReqNo.SelectedIndexChanged += new EventHandler(Cmb_TalbNo2_SelectedIndexChanged);
                Constants.closecon();
            }

        }
        private void Cmb_TalbNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            StepFlag = 0;
          //  SearchTalb(2);
            GetTalbData(cmbReqNo.Text);
         
            //CountDays(2);
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
                cmd.Parameters.AddWithValue("@TN", cmbReqNo.Text);
                cmd.Parameters.AddWithValue("@FY", cmbYear.Text);
                cmd.Parameters.AddWithValue("@EC", Constants.CodeEdara);
            }
            else if (x == 2 && Constants.User_Type == "A")
            {
                cmdstring = "select * from  T_TalbTawreed where TalbTwareed_No=@TN and FYear=@FY and CodeEdara=@EC";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", cmbReqNo.Text);
                cmd.Parameters.AddWithValue("@FY", cmbYear.Text);
                cmd.Parameters.AddWithValue("@EC", Constants.CodeEdara);
            }
            else if (x == 2 && Constants.User_Type == "B")
            {
                cmdstring = "select * from  T_TalbTawreed where TalbTwareed_No=@TN and FYear=@FY";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", cmbReqNo.Text);
                cmd.Parameters.AddWithValue("@FY", cmbYear.Text);
            }
            else if (x == 2 && Constants.User_Type == "B")
            {
                cmdstring = "select * from  T_TalbTawreed where TalbTwareed_No=@TN and FYear=@FY";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", cmbReqNo.Text);
                cmd.Parameters.AddWithValue("@FY", cmbYear.Text);
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
                    string BUM = dr["BuyMethod"].ToString();
                


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

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Search_TalbTawreed_Load(object sender, EventArgs e)
        {
            HelperClass.comboBoxFiller(cmbYear, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
            HelperClass.comboBoxFiller(cmbMostandType, TransHandler.getTrans(), "TransName", "TransName", this);
            dateTimePicker1.Text = DateTime.Now.ToShortDateString();
            dateTimePicker2.Text = DateTime.Now.ToShortDateString();
            cmbReqNo.DrawMode = DrawMode.OwnerDrawFixed;
            cmbReqNo.DrawItem += Cmb_TalbNo2_DrawItem;
            cmbReqNo.DropDownClosed += Cmb_Edara_DropDownClosed;
            Constants.opencon();
            string cmdstring = "select * from Edarat  ";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

       
            DataTable dts = new DataTable();
            Cmb_Edara.SelectedIndexChanged -= new EventHandler(Cmb_Edara_SelectedIndexChanged);
          
            dts.Load(cmd.ExecuteReader());
            Cmb_Edara.DataSource = dts;
            Cmb_Edara.ValueMember = "CodeEdara";
            Cmb_Edara.DisplayMember = "NameEdara";
           Cmb_Edara.SelectedIndex = -1;
           Cmb_Edara.SelectedIndexChanged += new EventHandler(Cmb_Edara_SelectedIndexChanged);
          
            toolTip1.ShowAlways = true;
/*
            foreach (DataRow dr in dts.Rows)
            {
                int CurrentRow = Convert.ToInt32(dr["CodeEdara"].ToString());
                Cmb_Edara.Items[CurrentRow - 1].toolTip1 = dr["NameEdara"].ToString();
            }    
            */
            // Set up the ToolTip text for the Button and Checkbox.
         //   toolTip1.SetToolTip(this.Pic_Sign1, "My button1");
         //   toolTip1.SetToolTip(this.Pic_Sign2, Ename2 + Environment.NewLine + wazifa2);
            Constants.closecon();
            if (Constants.User_Type == "A" )
            {
                Cmb_Edara.Visible = false;
                radioButton1.Visible = false;
            }
        }

        private void Cmb_Edara_SelectedIndexChanged(object sender, EventArgs e)
        {
                       
   // ToolTip toolTip1 = new ToolTip();
   /* toolTip2.AutoPopDelay = 0;
    toolTip2.InitialDelay = 0;
    toolTip2.ReshowDelay = 0;
    toolTip2.ShowAlways = true;
    toolTip2.SetToolTip(this.Cmb_Edara, Cmb_Edara.SelectedText.ToString());
      */
            cmbYear.SelectedIndex = -1;
            cmbReqNo.SelectedIndex = -1; ;
            cmbReqNo.Text = "";
        }

        private void Cmb_Edara_MouseHover(object sender, EventArgs e)
        {
              /* string caption = "Selected value is: " + Cmb_Edara.SelectedText;
               toolTip1.SetToolTip(Cmb_Edara, caption);
                 toolTip1.AutoPopDelay = 5000;
                toolTip1.InitialDelay = 200;
                 toolTip1.ReshowDelay = 100;
                 toolTip1.ShowAlways = true;*/

 
        
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Cmb_Edara.Enabled = true;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                cmbYear.Enabled =true;
                cmbReqNo.Enabled = true;
                cmbYear.SelectedIndex = -1;
                cmbReqNo.SelectedIndex = -1; ;
                cmbReqNo.Text = "";
            }
            else
            {

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                Cmb_Edara.Enabled = false;
                dateTimePicker1.Enabled =true;
                dateTimePicker2.Enabled = true;
                cmbYear.Enabled =true;
                cmbReqNo.Enabled = true;
                cmbYear.SelectedIndex = -1;
                cmbReqNo.SelectedIndex = -1; ;
                cmbReqNo.Text = "";
            }
            else
            {

            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Cmb_Edara.Enabled =false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                cmbYear.Enabled = true;
                cmbReqNo.Enabled = true;
            }
            else
            {

            }
        }

        private void Cmb_TalbNo2_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void Cmb_TalbNo2_CursorChanged(object sender, EventArgs e)
        {

        }
        public void GetTalbData(string t)
        {
            Constants.opencon();
            string cmdstring = "";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
       
           if (Constants.User_Type == "A")
            {
                cmdstring = "select * from  T_TalbTawreed_Benod where TalbTwareed_No=@TN and FYear=@FY";// and CodeEdara=@EC";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN",t);
                cmd.Parameters.AddWithValue("@FY", cmbYear.Text);
              //  cmd.Parameters.AddWithValue("@EC", Constants.CodeEdara);
            }
            else if ( Constants.User_Type == "B")
            {
                cmdstring = "select * from  T_TalbTawreed_Benod where TalbTwareed_No=@TN and FYear=@FY";
                cmd = new SqlCommand(cmdstring, Constants.con);

                // cmd.Parameters.AddWithValue("@TN", Cmb_TalbNo2.Text);
                cmd.Parameters.AddWithValue("@TN", t);
                cmd.Parameters.AddWithValue("@FY", cmbYear.Text);
            }
          
            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);


            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                ST = "";
                while (dr.Read())
                {
                  
                    BN = dr["Bnd_No"].ToString();
                    SA = dr["STOCK_NO_ALL"].ToString();
                    BT = dr["BIAN_TSNIF"].ToString();
                    ST = ST+ BT+Environment.NewLine;



                }
                label1.Text = ST;
              //  ShowToolTip(ST);

            }


            else
            {
               // MessageBox.Show("من فضلك تاكد من رقم طلب التوريد");


                return;

            }
            dr.Close();
            Constants.closecon();
        }

        public void ShowToolTip(string TalbData)
        {

            

            string caption = "Selected value is: " + TalbData;
            toolTip1.SetToolTip(cmbReqNo, caption);
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 200;
            toolTip1.ReshowDelay = 100;
            toolTip1.ShowAlways = true;
        }
        private void Cmb_TalbNo2_MouseHover(object sender, EventArgs e)
        {
            ShowToolTip(ST);
        }

        private void toolTip2_Popup(object sender, PopupEventArgs e)
        {

        }
        private void Cmb_Edara_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(cmbReqNo);
        }

        private void Cmb_TalbNo2_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) { return; } // added this line thanks to Andrew's comment
          string text = cmbReqNo.GetItemText(cmbReqNo.Items[e.Index]);
         // string text ="xxxxx";
            
            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            { e.Graphics.DrawString(text, e.Font, br, e.Bounds); }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                GetTalbData(text);


                toolTip2.Show(ST, cmbReqNo, e.Bounds.Right, e.Bounds.Bottom);
            }
            e.DrawFocusRectangle();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            cmbYear.SelectedIndex = -1;
            cmbReqNo.SelectedIndex = -1; ;
            cmbReqNo.Text = "";
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            cmbYear.SelectedIndex = -1;
            cmbReqNo.SelectedIndex = -1; ;
            cmbReqNo.Text = "";
        }

        private void BTN_Form_Click(object sender, EventArgs e)
        {
            if (cmbYear.SelectedIndex == -1 || cmbReqNo.SelectedIndex == -1)
            {
                MessageBox.Show("يجب اختيار السنة المالية و رقم الطلب اولا");
                return;

            }
            if (Constants.currentOpened != null)
            {
                //   Constants.currentOpened.Close();
            }
            //----------------------

            TalbTawred F = new TalbTawred(cmbYear.Text, cmbReqNo.Text);
            // main Ff = new Main();
            Constants.currentOpened = F;
            F.MdiParent = this.MdiParent;
            F.Show();
            // this.IsMdiContainer = true;
            // 
            F.Dock = DockStyle.Fill;
            //TalbTawred popup = new TalbTawred(Cmb_FYear2.Text,Cmb_TalbNo2.Text);
          //  popup.Show();
       


        }

        private void BTN_Cycle_Click(object sender, EventArgs e)
        {
          //  if (Cmb_FYear2.SelectedIndex == -1 || Cmb_TalbNo2.SelectedIndex == -1)
          //  {
          //      MessageBox.Show("يجب اختيار السنة المالية و رقم الطلب التوريد اولا");
          //      return;

          //  }
          //  Track_TalbTawreed F = new Track_TalbTawreed(Cmb_FYear2.Text, Cmb_TalbNo2.Text);
          //  // main Ff = new Main();
          //  Constants.currentOpened = F;
          //  F.MdiParent = this.MdiParent;
          //  F.Show();
          //  // this.IsMdiContainer = true;
          //  // 
          //  F.Dock = DockStyle.Fill;
          ////  Track_TalbTawreed AF = new Track_TalbTawreed(Cmb_FYear2.Text,Cmb_TalbNo2.Text);
          // // AF.Show();
          // // this.Hide();
        }

        private void BTN_Report_Click(object sender, EventArgs e)
        {
            if (cmbYear.SelectedIndex == -1 || cmbReqNo.SelectedIndex == -1)
            {
                MessageBox.Show("يجب اختيار السنة المالية و رقم الطلب  اولا");
                return;

            }

            Constants.TalbFY = cmbYear.Text;
            Constants.TalbNo = Convert.ToInt32(cmbReqNo.Text);
            var tuple = HandleReports(cmbMostandType.Text);
            string FP = tuple.Item1;
            string CN = tuple.Item2;
            int FN = tuple.Item3;
            Constants.FormNo =FN;
            FReports f = new FReports();
            f.Show();
        }



        //public  Dictionary<MostndType, List<string>> MostndObj = new Dictionary<MostndType, List<string>>();
      
        
        
        public  List<string> mostndTypeInfo = new List<string>();


        public enum MostndType
        {
            TalbTawreed,
            EznSarf,
            Estlam,
            AmrSheraa,
            EdafaMakhaznya,
            EznTahwel,
            TalbEslah,
            TalbMoaayra,
            talbTanfizAamal,       
        }


        public List<string> GetmostnadTypeInfo(MostndType type)
        {
            if (type == MostndType.TalbTawreed)
            {
                SearchData.formName = "TalbTawred";
            }
            else if (type == MostndType.EznSarf)
            {
                mostndTypeInfo.Add("type1");
                mostndTypeInfo.Add("type2");
                mostndTypeInfo.Add("type3");
                SearchData.formName = "EznSarf_F";
                return mostndTypeInfo;
            }
            else if(type == MostndType.Estlam)
            {
                SearchData.formName = "Estlam_F";
            }
            else if(type == MostndType.AmrSheraa)
            {
                SearchData.formName = "AmrSheraa";
            }
            else if(type == MostndType.EdafaMakhaznya)
            {
                mostndTypeInfo.Add("type3");
                mostndTypeInfo.Add("type4");
                mostndTypeInfo.Add("type5");
                SearchData.formName = "FEdafaMakhzania_F";
                return mostndTypeInfo;
            }
            else if(type == MostndType.EznTahwel)
            {
                mostndTypeInfo.Add("type6");
                mostndTypeInfo.Add("type7");
                mostndTypeInfo.Add("type8");
                SearchData.formName = "FTransfer_M";
            }
            else if (type == MostndType.TalbEslah)
            {
                SearchData.formName = "TalbEslah";
            }
            else if (type == MostndType.TalbMoaayra)
            {
                SearchData.formName = "TalbMoaera";
            }

            else if (type == MostndType.talbTanfizAamal)
            {
                SearchData.formName = "TalbTnfiz";                
            }
       
            return new List<string>();

        }

        public void getRequsetNUmbers()
        {
            DataTable dtRequestedNumbers = new DataTable();
            SqlDataAdapter daRequestedNumbers = new SqlDataAdapter("select * from users", Constants.foreignCon);
            Constants.openForeignCon();
            daRequestedNumbers.Fill(dtRequestedNumbers);

            cmbReqNo.DataSource= dtRequestedNumbers;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData.reqNo = cmbReqNo.SelectedValue.ToString();
            SearchData.year = cmbYear.SelectedValue.ToString();
            SearchData.mostndType = cmbMostandType.SelectedValue.ToString();
            SearchData.mostndTypeInfo = cmbMostandTypeInfo.SelectedValue.ToString();
        }

        private void cmbMostandType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostndType type = (MostndType)cmbMostandType.SelectedIndex;
            mostndTypeInfo.Clear();
            mostndTypeInfo = GetmostnadTypeInfo(type);

            if(mostndTypeInfo.Count > 0 )
            {
                cmbMostandTypeInfo.Visible = true;
                string x = cmbMostandType.SelectedIndex.ToString();
                MessageBox.Show(x);
                HelperClass.comboBoxFiller(cmbMostandTypeInfo, mostndTypeInfo, "type", "type", this);
            }
            else
            {
                cmbMostandTypeInfo.Visible = false; ;
            }

        }
    }
}
