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

    public partial class AmrSheraa : Form
    {

        //------------------------------------------ Define Variables ---------------------------------
        #region Def Variables
        List<CurrencyInfo> currencies = new List<CurrencyInfo>();
        public SqlConnection con;//sql conn for anrpc_sms db
        public string UserB = "";
        public DataTable DT = new DataTable();
        private BindingSource bindingsource1 = new BindingSource();
        private string TableQuery;
        private int AddEditFlag;
        public Boolean executemsg;
       // public double totalprice;
        //  private string TableQuery;
        public string stockallold;
        DataTable table = new DataTable();
        public SqlDataAdapter dataadapter;
        public DataSet ds = new DataSet();
        ///////////////////////
        public string Sign1;
        public string Sign2;
        public string Sign3;
        public string Sign4;
        public string Sign5;
        public string Sign6;
        public string Sign7;

        public string Empn1;
        public string Empn2;
        public string Empn3;
        public string Empn4;
        public string Empn5;
        public string Empn6;
        public string Empn7;

        public string FlagEmpn1;
        public string FlagEmpn2;
        public string FlagEmpn3;
        public string FlagEmpn4;
        public string FlagEmpn5;
        public string FlagEmpn6;
        public string FlagEmpn7;

        public int FlagSign1;
        public int FlagSign2;
        public int FlagSign3;
        public int FlagSign4;
        public int FlagSign5;
        public int FlagSign6;
        public int FlagSign7;
        public string TNO;
        public string FY;
        public string MNO;
        public string FY2;
        public int r;
        public int rowflag = 0;
        double quan;
        double dareba;
        decimal price;
        decimal totalprice;
        int changedflag = 0;
        string edara = "";
        string codeedara = "";
        string talbtawreed = "";
        string bndmwazna = "";
        public string wazifa1;
        public string wazifa2;
        public string wazifa3;
        public string wazifa4;
        public string wazifa5;
        public string wazifa6;
        public string wazifa7;
        public string wazifa8;
        public string wazifa9;
        public string wazifa10;
        public string wazifa11;

        public string Ename1;
        public string Ename2;
        public string Ename3;
        public string Ename4;
        public string Ename5;
        public string Ename6;
        public string Ename7;
        public string Ename8;
        public string Ename9;
        public string Ename10;
        public string Ename11;

        public string pp;
        public  string FinancialTypeText;
        public int FinancialType;
        public string BuyMethod;
        public   int AmrsheraaType = 1;//محلى
        //  public string TableQuery;

        AutoCompleteStringCollection TasnifColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TasnifNameColl = new AutoCompleteStringCollection(); //empn

        AutoCompleteStringCollection UnitColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TalbColl = new AutoCompleteStringCollection(); //empn

        #endregion

        #region myDefVariable
        enum VALIDATION_TYPES
        {
            ADD_TASNIF,
            ADD_NEW_TASNIF,
            ATTACH_FILE,
            SEARCH,
            CONFIRM_SEARCH,
            SAVE,

        }
        int currentSignNumber = 0;
        #endregion


        //------------------------------------------ State Handler ---------------------------------
        #region State Handler
        private void changePanelState(Panel panel, bool state)
        {
            try
            {
                foreach (Control control in panel.Controls)
                {
                    if (control.GetType() == typeof(Panel))
                    {
                        changePanelState((Panel)control, state);
                    }
                    else
                    {
                        control.Enabled = state;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void changeDataGridViewColumnState(DataGridView dataGridView, bool state)
        {
            try
            {
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    dataGridView.Columns[column.Index].ReadOnly = state;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void PrepareAddState()
        {
            //Search sec
            changePanelState(panel5, true);

            //dataViewre sec
            changePanelState(panel6, false);
            Txt_ReqQuan.Enabled = true;

            //fyear sec
            changePanelState(panel8, false);
            Cmb_FYear.Enabled = true;
            Cmb_CType.Enabled = true;

            //bian edara sec
            changePanelState(panel9, true);
            TXT_Edara.Enabled = false;

            //arabic value
            changePanelState(panel11, true);
            TXT_ArabicValue.Enabled = false;


            //btn Section
            //generalBtn
            SaveBtn.Enabled = true;
            BTN_Cancel.Enabled = true;
            Addbtn2.Enabled = true;
            Addbtn.Enabled = false;
            Editbtn2.Enabled = false;
            BTN_SearchEzn.Enabled = false;
            BTN_Print.Enabled = false;
            browseBTN.Enabled = true;
            BTN_PDF.Enabled = true;

            //signature btn
            changePanelState(signatureTable, false);
            BTN_Sign1.Enabled = true;

            //takalid types
            DisableTakalef();

            changeDataGridViewColumnState(dataGridView1, true);

            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
        }

        public void PrepareEditState()
        {
            PrepareAddState();
            panel8.Enabled = false;
            BTN_Print.Enabled = true;

            Pic_Sign1.Image = null;
            Pic_Sign2.Image = null;
            FlagSign1 = 0;
            FlagSign2 = 0;
            Pic_Sign1.BackColor = Color.White;
            Pic_Sign2.BackColor = Color.White;
        }

        public void PrepareConfirmState()
        {
            DisableControls();
            BTN_Save2.Enabled = true;

            if (Constants.User_Type == "A")
            {
                if (FlagSign2 != 1 && FlagSign1 == 1)
                {
                    BTN_Sign2.Enabled = true;
                    DeleteBtn.Enabled = true;
                }
                else if (FlagSign4 != 1 && FlagSign3 == 1)
                {
                    BTN_Sign4.Enabled = true;
                }
            }
            else if (Constants.User_Type == "B")
            {
                if (Constants.UserTypeB == "Sarf")
                {
                    BTN_Sign3.Enabled = true;
                    //dataGridView1.ReadOnly = false;
                    dataGridView1.Columns["Quan2"].ReadOnly = false;
                }
                else if (Constants.UserTypeB == "Tkalif" || Constants.UserTypeB == "Finance")
                {
                    EnableTakalef();
                }
            }

            AddEditFlag = 1;
            TNO = TXT_EznNo.Text;
            FY = Cmb_FYear.Text;
        }

        public void prepareSearchState()
        {
            DisableControls();
            Input_Reset();
            Cmb_FYear.Enabled = true;
            Cmb_CType.Enabled = true;
            TXT_EznNo.Enabled = true;
            BTN_Print.Enabled = true;
        }


        public void reset()
        {
            prepareSearchState();
        }

        public void DisableControls()
        {

            //amr sheraa type sec
            changePanelState(panel3, false);

            //fyear sec
            changePanelState(panel5, false);

            //moward sec
            changePanelState(panel6, false);

            //bian edara sec
            changePanelState(panel10, false);

            //mowazna value
            changePanelState(panel11, false);

            //dareba sec
            changePanelState(panel14, false);

            //sheek sec
            changePanelState(panel20, false);

            //btn Section
            //generalBtn
            Addbtn.Enabled = true;
            BTN_Search.Enabled = true;
            BTN_Search_Motab3a.Enabled = false;
            SaveBtn.Enabled = false;
            BTN_Save2.Enabled = false;

            EditBtn.Enabled = false;
            BTN_Cancel.Enabled = false;
            BTN_ChooseTalb.Enabled = false;
            EditBtn2.Enabled = false;
            BTN_Print.Enabled = false;
            BTN_Print2.Enabled = false;
            browseBTN.Enabled = false;
            BTN_PDF.Enabled = false;

            //signature btn
            changePanelState(signatureTable, false);

            changeDataGridViewColumnState(dataGridView1, true);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

        }

        public void resetSignature()
        {
            //btn Section
            //signature btn
            Pic_Sign1.Image = null;
            FlagSign1 = 0;
            Pic_Sign1.BackColor = Color.White;

            Pic_Sign2.Image = null;
            FlagSign2 = 0;
            Pic_Sign2.BackColor = Color.White;

            Pic_Sign3.Image = null;
            FlagSign3 = 0;
            Pic_Sign3.BackColor = Color.White;

            Pic_Sign4.Image = null;
            FlagSign4 = 0;
            Pic_Sign4.BackColor = Color.White;
        }

        public void Input_Reset()
        {
            //Search sec
            TXT_StockNoAll.Text = "";
            TXT_StockName.Text = "";
            TXT_PartNo.Text = "";

            //dataViewre sec
            TXT_StockBian.Text = "";
            Txt_Quan.Text = "";
            Txt_ReqQuan.Text = "";
            TXT_Unit.Text = "";
            Quan_Min.Value = 0;
            Quan_Max.Value = 0;
            checkBox1.Checked = false;
            checkBox2.Checked = false;


            //fyear sec
            Cmb_CType.Text = "";
            Cmb_CType.SelectedIndex = -1;

            Cmb_FYear.Text = "";
            Cmb_FYear.SelectedIndex = -1;

            TXT_EznNo.Text = "";
            TXT_TRNO.Text = "";

            //bian edara sec
            TXT_Edara.Text = "";
            TXT_RequestedFor.Text = "";
            TXT_Date.Value = DateTime.Today;

            //arabic value
            TXT_ProcessNo.Text = "";
            TXT_RespCentre.Text = "";
            TXT_Total.Text = "";
            TXT_ArabicValue.Text = "";

            //search sec
            Cmb_CType2.Text = "";
            Cmb_CType2.SelectedIndex = -1;

            Cmb_FYear2.Text = "";
            Cmb_FYear2.SelectedIndex = -1;

            Cmb_EznNo2.Text = "";
            Cmb_EznNo2.SelectedIndex = -1;

            resetSignature();

            //tkalifData types
            TXT_AccNo.Text = "";
            TXT_PaccNo.Text = "";
            TXT_MTaklif.Text = "";
            TXT_MResp.Text = "";
            TXT_Masrof.Text = "";
            TXT_Morakba.Text = "";
            TXT_Enfak.Text = "";

            cleargridview();

            Image1 = "";
            Image2 = "";
            pictureBox2.Image = null;

            oldvalue = 0;
            picflag = 0;
            MaxFlag = 0;
            AddEditFlag = 0;
        }
        #endregion





        public AmrSheraa()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void AmrSheraa_Load(object sender, EventArgs e)
        {
            HelperClass.comboBoxFiller(Cmb_FY2, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
            HelperClass.comboBoxFiller(Cmb_FY, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
            HelperClass.comboBoxFiller(Cmb_FYear2, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);


            // dataGridView1.Parent = panel1;
            //dataGridView1.Dock = DockStyle.Bottom;
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Syria));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Tunisia));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Gold));

            cboCurrency.DataSource = currencies;

            cboCurrency_DropDownClosed(null, null);
            AddEditFlag = 0;
            if (Constants.Amrshera_F == false)
            {
                panel7.Visible = true;
                panel2.Visible = false;
                panel7.Dock = DockStyle.Top;
            }
            else if (Constants.Amrshera_F == true)
            {
                panel2.Visible = true;
               panel7.Visible = false;
                panel2.Dock = DockStyle.Top;
            }
            else { }

            UserB = Constants.User_Name.Substring(Constants.User_Name.LastIndexOf('_') + 1);

            if (UserB == "Stock")
            {
                EnableControls();
              //  dataGridView1.ReadOnly =true;
                BTN_Sigm1.Enabled = true;
                BTN_Sigm12.Enabled = true;
                BTN_Sigm13.Enabled = true;
                BTN_Sigm14.Enabled = true;

                BTN_Sign5.Enabled = false;
                BTN_Sign6.Enabled = false;
                BTN_Sign7.Enabled = false;
            }
            else if (UserB == "Finance")
            {
                DisableControls();
                dataGridView1.ReadOnly = true;
                BTN_Sigm1.Enabled = false;
                BTN_Sigm12.Enabled = false;
                BTN_Sigm13.Enabled = false;
                BTN_Sigm14.Enabled = false;

                BTN_Sign5.Enabled = false;
                BTN_Sign6.Enabled = false;
                BTN_Sign7.Enabled = true;
                
            }
            else if (UserB == "Chairman")
            {
            DisableControls();
            dataGridView1.ReadOnly = true;
            BTN_Sigm1.Enabled = false;
            BTN_Sigm12.Enabled = false;
            BTN_Sigm13.Enabled = false;
            BTN_Sigm14.Enabled = false;

            BTN_Sign5.Enabled = false;
            BTN_Sign6.Enabled = true;
            BTN_Sign7.Enabled = false;
           // {

            }
            else if (UserB == "ViceChairman")
            {
                DisableControls();
                dataGridView1.ReadOnly = true;
                BTN_Sigm1.Enabled = false;
                BTN_Sigm12.Enabled = false;
                BTN_Sigm13.Enabled = false;
                BTN_Sigm14.Enabled = false;

                BTN_Sign5.Enabled = false;
                BTN_Sign6.Enabled = true;
                BTN_Sign7.Enabled = false;
                // {

            }
            if (Constants.User_Type == "A")
            {
                dataGridView1.ReadOnly = true;
                BTN_Sigm1.Enabled = false;
                BTN_Sigm12.Enabled = false;
                BTN_Sigm13.Enabled = false;
                BTN_Sigm14.Enabled = false;

                BTN_Sign5.Enabled = true;
                BTN_Sign6.Enabled = false;
                BTN_Sign7.Enabled = false;
            }
            //------------------------------------------

            con = new SqlConnection(Constants.constring);

            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            //*******************************************
            // ******    AUTO COMPLETE
            //*******************************************
            string cmdstring = "";
            if (Constants.User_Type == "B")
            {

            
            cmdstring = "select Amrshraa_No from   T_Awamershraa where  AmrSheraa_sanamalia='" + Cmb_FY.Text + "'";
           }
            else
            {
               cmdstring = "select Amrshraa_No from   T_Awamershraa where  AmrSheraa_sanamalia='" + Cmb_FY.Text + "'" +" and CodeEdara='"+codeedara+"'";

            }
         
            SqlCommand cmd = new SqlCommand(cmdstring, con);
            SqlDataReader dr = cmd.ExecuteReader();
            //---------------------------------
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    TalbColl.Add(dr["Amrshraa_No"].ToString());
                    //TasnifNameColl.Add(dr["Stock_No_Nam"].ToString());

                }
            }
            dr.Close();

            ///////////////////////////////////////
            string cmdstring2 = "SELECT [arab_unit] ,[eng_unit] ,[cod_unit] from Tunit";
            SqlCommand cmd2 = new SqlCommand(cmdstring2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            //---------------------------------
            if (dr2.HasRows == true)
            {
                while (dr2.Read())
                {
                    UnitColl.Add(dr2["arab_unit"].ToString());

                }
            }
            dr2.Close();
            //////////////////////////////////////////////
            Cmb_FY.SelectedIndex = 0;
            Cmb_FY2.SelectedIndex = 0;
            string cmdstring3 = "SELECT DISTINCT [Sadr_To] FROM T_Awamershraa ORDER BY [Sadr_To]";
            SqlCommand cmd3 = new SqlCommand(cmdstring3, con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            //---------------------------------
            if (dr3.HasRows == true)
            {
                while (dr3.Read())
                {
                    TasnifColl.Add(dr3["Sadr_To"].ToString());

                }
            }
            dr3.Close();
          CMB_Sadr.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
          CMB_Sadr.AutoCompleteSource = AutoCompleteSource.CustomSource;
          CMB_Sadr.AutoCompleteCustomSource = TasnifColl;
      
            TXT_AmrNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_AmrNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_AmrNo.AutoCompleteCustomSource = TalbColl;

            con.Close();
        }
        
        
        
        private void Getdata(string cmd)
        {
            dataadapter = new SqlDataAdapter(cmd, con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataadapter.Fill(table);
            dataGridView1.DataSource = table;

            dataGridView1.Columns["Amrshraa_No"].HeaderText = "رقم أمر الشراء";//col0
            // dataGridView1.Columns["TalbTwareed_No"].Width = 60;
            dataGridView1.Columns["Amrshraa_No"].Visible = false;

            dataGridView1.Columns["Monaksa_No"].HeaderText = " رقم المناقصة";//col1
            dataGridView1.Columns["Monaksa_No"].Visible = false;

            dataGridView1.Columns["monaksa_sanamalia"].HeaderText = "مناقصةسنةمالية";//col2
            dataGridView1.Columns["monaksa_sanamalia"].Visible = false;

            dataGridView1.Columns["AmrSheraa_sanamalia"].HeaderText = "امر الشراء سنةمالية";//col3
            dataGridView1.Columns["AmrSheraa_sanamalia"].Visible = false;

            dataGridView1.Columns["TalbTwareed_No"].HeaderText = "رقم طلب التوريد";//col4
            dataGridView1.Columns["TalbTwareed_No"].Width = 50;
            dataGridView1.Columns["TalbTwareed_No"].ReadOnly = true;

            dataGridView1.Columns["FYear"].HeaderText = "سنة مالية طلب التوريد";//col5
            dataGridView1.Columns["FYear"].ReadOnly = true;
            dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col6
            dataGridView1.Columns["Bnd_No"].Width = 30;
            dataGridView1.Columns["Bnd_No"].ReadOnly = true;
            dataGridView1.Columns["CodeEdara"].HeaderText = "كود ادارة";//col7
            dataGridView1.Columns["CodeEdara"].Visible = false;
            dataGridView1.Columns["NameEdara"].HeaderText = "الادارة الطالبة";//col8
            dataGridView1.Columns["NameEdara"].ReadOnly = true;

            dataGridView1.Columns["BndMwazna"].HeaderText = "بند موازنة";
            dataGridView1.Columns["BndMwazna"].ReadOnly = true;
            dataGridView1.Columns["BndMwazna"].Width = 40;
            dataGridView1.Columns["Quan"].HeaderText = " الكمية المطلوبة";//COL10
            dataGridView1.Columns["Quan"].ReadOnly = true;
            dataGridView1.Columns["Quan"].Width = 40;
            dataGridView1.Columns["Quan2"].HeaderText = " الكمية الموردة";////COL11
            dataGridView1.Columns["Quan2"].Visible = false;
            dataGridView1.Columns["Quan2"].Visible = false;//////////////extracolumn 
            dataGridView1.Columns["Unit"].HeaderText = "الوحدة";//col12
            dataGridView1.Columns["Unit"].ReadOnly = true;
            dataGridView1.Columns["Unit"].Width = 40;
            dataGridView1.Columns["Bayan"].HeaderText = "بيان المهمات";//col13
            dataGridView1.Columns["Bayan"].ReadOnly = true;
            dataGridView1.Columns["Bayan"].Width =250;
            dataGridView1.Columns["Makhzn"].HeaderText = "مخزن";//col14
            dataGridView1.Columns["Makhzn"].Visible = false;
            dataGridView1.Columns["Rakm_Tasnif"].HeaderText = "رقم التصنيف";//col15
            dataGridView1.Columns["Rakm_Tasnif"].Visible = false;

            dataGridView1.Columns["Rased_After"].HeaderText = "رصيد بعد";//col16
            dataGridView1.Columns["Rased_After"].Visible = false;

            dataGridView1.Columns["UnitPrice"].HeaderText = "سعر الوحدة غير شامل الضريبة";//col17
           
           dataGridView1.Columns["TotalPrice"].HeaderText = "الاجمالى غير شامل الضريبة";//col18
           dataGridView1.Columns["TotalPrice"].ReadOnly = true;

           dataGridView1.Columns["ApplyDareba"].HeaderText = "تطبق الضريبة";//col19
           dataGridView1.Columns["ApplyDareba"].Width = 40;

           dataGridView1.Columns["Darebapercent"].HeaderText = "نسبة الضريبة";//col20
           dataGridView1.Columns["Darebapercent"].Width = 40;
/*
 * 
           dataGridView1.Columns["TotalPriceAfter"].HeaderText = "الثمن الاجمالى  شامل الضريبة";


           if (UserB == "Finance" || UserB == "Chairman")
           {

               dataGridView1.Columns["UnitPrice"].ReadOnly = true;

               dataGridView1.Columns["TotalPrice"].ReadOnly = true;

               dataGridView1.Columns["ApplyDareba"].ReadOnly = true;


               dataGridView1.Columns["Darebapercent"].ReadOnly = true;
               dataGridView1.ReadOnly = true;

           }
           else if (UserB == "Stock")
           {
               dataGridView1.Columns["UnitPrice"].ReadOnly = false;

               dataGridView1.Columns["TotalPrice"].ReadOnly = false;

               dataGridView1.Columns["ApplyDareba"].ReadOnly = false;


               dataGridView1.Columns["Darebapercent"].ReadOnly = false;

           }
            */
           dataGridView1.Columns["TotalPriceAfter"].HeaderText = "الاجمالى شامل الضريبة ";//col21
           dataGridView1.Columns["TotalPriceAfter"].ReadOnly = true;


           dataGridView1.Columns["EstlamFlag"].HeaderText ="تم الاستلام ";//col22
           dataGridView1.Columns["EstlamFlag"].Visible = false;

           dataGridView1.Columns["EstlamDate"].HeaderText = "تاريخ الاستلام ";//col23
           dataGridView1.Columns["EstlamDate"].Visible= false;

           dataGridView1.Columns["LessQuanFlag"].HeaderText = "يوجد عجز ";//col24
           dataGridView1.Columns["LessQuanFlag"].Visible = false;

           dataGridView1.Columns["NotIdenticalFlag"].HeaderText = "مطابق/غير مطابق ";//col25
           dataGridView1.Columns["NotIdenticalFlag"].Visible = false;
  
           dataGridView1.Columns["TalbEsdarShickNo"].HeaderText = "رقم طلب الاصدار ";//col26
          
          // dataGridView1.Columns["TalbEsdarShickNo"].ReadOnly = true ;

           dataGridView1.Columns["ShickNo"].HeaderText = "رقم الشيك ";//col27
          // dataGridView1.Columns["ShickNo"].ReadOnly = true;

           dataGridView1.Columns["ShickDate"].HeaderText = "تاريخ الشيك ";//col28
          // dataGridView1.Columns["ShickDate"].ReadOnly = true;
           dataGridView1.Columns["TalbEsdarShickNo"].Visible = false;
           dataGridView1.Columns["ShickNo"].Visible = false;
           dataGridView1.Columns["ShickDate"].Visible = false;
/*
           if (Constants.User_Type == "B" && Constants.UserTypeB == "Finance")
           {
               dataGridView1.Columns["TalbEsdarShickNo"].Visible = true;
               dataGridView1.Columns["ShickNo"].Visible = true;
               dataGridView1.Columns["ShickDate"].Visible = true;

               dataGridView1.Columns["TalbEsdarShickNo"].ReadOnly = false;
               dataGridView1.Columns["ShickNo"].ReadOnly = false;
               dataGridView1.Columns["ShickDate"].ReadOnly = false;
           }
 */



           dataGridView1.AllowUserToAddRows = true;
          //  decimal total = table.AsEnumerable().Sum(row => row.Field<decimal>("TotalPriceAfter"));
                //    dataGridView1.FooterRow.Cells[1].Text = "Total";
                 //   dataGridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                   // TXT_Egmali.Text = total.ToString("N2");
        }
                private void GetData(int x,string y)
          {
              if (string.IsNullOrWhiteSpace(TXT_AmrNo.Text))
              {
                  // MessageBox.Show("ادخل رقم التصريح");
                  //  PermNo_text.Focus();
                  return;
              }
              else
              {
                  table.Clear();
                  TableQuery = "SELECT *  FROM [T_BnodAwamershraa] Where Amrshraa_No = " + x + " and AmrSheraa_sanamalia='" + y + "'";
                  Getdata(TableQuery);
              }

          }
        


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
          public void EnableControls()
        {
           BTN_ChooseTalb.Enabled = true;
    /*       radioButton1.Enabled = true;
           radioButton2.Enabled = true;
           radioButton3.Enabled = true;
           radioButton4.Enabled = true;
           radioButton5.Enabled = true;
           radioButton6.Enabled = true;*/
            TXT_AmrNo.Enabled = true;
            Cmb_FY.Enabled = true;
            TXT_Date.Enabled = true;
            TXT_MonksaNo.Enabled = true;
            Cmb_FY2.Enabled = true;
           CMB_Sadr.Enabled = true;
            TXT_Payment.Enabled = true;
            TXT_Name.Enabled = true;
           TXT_TaslemDate.Enabled = true;
            TXT_TaslemPlace.Enabled = true;
            TXT_Payment.Enabled = true;
            TXT_Momayz.Enabled = true;
        //    TXT_Edara.Enabled = true;
        //    TXT_TalbNo.Enabled = true;
            TXT_HesabMward1.Enabled = true;
            TXT_HesabMward2.Enabled = true;
         //   TXT_Egmali.Enabled = true;
          //  TXT_BndMwazna.Enabled = true;

/*
            BTN_Sigm1.Enabled =true;
            BTN_Sigm12.Enabled =true;
            BTN_Sigm13.Enabled = true;
            BTN_Sigm14.Enabled = true;
            BTN_Sign5.Enabled =true;
            BTN_Sign6.Enabled =true;
            BTN_Sign7.Enabled = true;
            dataGridView1.Enabled = true;

  */
        
        }
        public void Input_Reset()
        {
            BTN_Print.Enabled = false;
            TXT_AmrNo.Text = "";
            Cmb_FY.Text = "";
            TXT_Date.Text = "";
            TXT_MonksaNo.Text = "";
            Cmb_FY2.Text= "";
            CMB_Sadr.Text = "";
            TXT_Payment.Text = "";
            TXT_Name.Text = "";
            TXT_TaslemDate.Text = "";
            TXT_TaslemPlace.Text = "";
            TXT_Payment.Text = "";
            TXT_Momayz.Text= "";
            TXT_Edara.Text = "";
            TXT_TalbNo.Text = "";
            TXT_HesabMward1.Text = "";
            TXT_HesabMward2.Text= "";
            TXT_Egmali.Text="";
            TXT_BndMwazna.Text = "";
            TXT_ShickNo.Text = "";
            TXT_EgmaliBefore.Text = "";
            TXT_EgmaliAfter.Text = "";
            TXT_EgmaliDareba.Text = "";

            FlagSign1 = 0;
            FlagSign2 = 0;
            FlagSign3 = 0;
            FlagSign4 = 0;
            FlagSign5 = 0;
            FlagSign6 = 0;
            Pic_Sign1.Image = null;
            Pic_Sign2.Image = null;
            Pic_Sign3.Image = null;
            Pic_Sign4.Image = null;
            Pic_Sign5.Image = null;
            Pic_Sign6.Image = null;
            Pic_Sign7.Image = null;

            Pic_Sign6.Visible = false;
            label23.Visible = false;
            BTN_Sign6.Visible = false;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;

        }
        public void DisableControls()
        {
            BTN_ChooseTalb.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
            radioButton5.Enabled = false;
            radioButton6.Enabled = false;
            //TXT_AmrNo.Enabled = false;
            //Cmb_FY.Enabled = false;
            TXT_Date.Enabled = false;
            TXT_MonksaNo.Enabled = false;
            Cmb_FY2.Enabled = false;
            CMB_Sadr.Enabled = false;
            TXT_Payment.Enabled = false;
            TXT_Name.Enabled = false;
            TXT_TaslemDate.Enabled = false;
            TXT_TaslemPlace.Enabled = false;
            TXT_Payment.Enabled = false;
            TXT_Momayz.Enabled = false;
            TXT_Edara.Enabled = false;
            TXT_TalbNo.Enabled = false;
            TXT_HesabMward1.Enabled = false;
            TXT_HesabMward2.Enabled = false;
            TXT_Egmali.Enabled = false;
            TXT_BndMwazna.Enabled = false;
            /*
            BTN_Sigm1.Enabled = false;
            BTN_Sigm12.Enabled = false;
            BTN_Sigm13.Enabled = false;
            BTN_Sigm14.Enabled = false;
            BTN_Sign5.Enabled = false;
            BTN_Sign6.Enabled = false;
            BTN_Sign7.Enabled = false;
            dataGridView1.Enabled = false;*/
  
        
        }
        private void cleargridview()
        {
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد اضافة امر شراء جديد؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                //btn_print.Enabled = false;
                EnableControls();
                Input_Reset();
                cleargridview();
                AddEditFlag = 2;
                EditBtn.Enabled = false;
              //  TXT_Edara.Text = Constants.NameEdara;
                BTN_ChooseTalb.Enabled = true;
                SaveBtn.Visible = true;

            }
            else
            {
                //do nothing
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

            if ((MessageBox.Show("هل تريد تعديل امر الشراء ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_AmrNo.Text) || string.IsNullOrEmpty(Cmb_FY.Text))
                {
                    MessageBox.Show("يجب اختيار امر الشراء المراد تعديله");
                    return;
                }
                else
                {
                    BTN_Print.Enabled = false;
                    Addbtn.Enabled = false;
                    AddEditFlag = 1;
                    TNO = TXT_AmrNo.Text;
                    FY = Cmb_FY.Text;
                    FY2 = Cmb_FY2.Text;
                    MNO = TXT_MonksaNo.Text;
                    SaveBtn.Visible = true;
                    var button = (Button)sender;
                    if (button.Name == "EditBtn")
                    {
                        EnableControls();
                       if (Constants.User_Type == "B" && Constants.UserTypeB == "Stock")
                        {
                            //EnabControls();
                            BTN_Sigm1.Enabled = true;
                            BTN_Sigm12.Enabled = true; 
                            BTN_Sigm13.Enabled = true;
                            BTN_Sigm14.Enabled = true;
                            BTN_Sign7.Enabled = false;
                            BTN_Sign6.Enabled = false;
                            BTN_Sign5.Enabled = false;
                            dataGridView1.ReadOnly =false;
                       
                        }
                       
                    
                    }
                    else if (button.Name == "EditBtn2")
                    {
                        //BTN_Sign1.Enabled = true;
                        if (Constants.User_Type == "B" && Constants.UserTypeB == "Finance")
                        {
                            DisableControls();
                            BTN_Sigm1.Enabled = false;
                            BTN_Sigm12.Enabled = false;
                            BTN_Sigm13.Enabled = false;
                            BTN_Sigm14.Enabled = false;
                            BTN_Sign5.Enabled = false;
                            BTN_Sign6.Enabled = false;
                            BTN_Sign7.Enabled = true;
                            if(FlagSign5 ==1)
                            {
                                TXT_ShickNo.Enabled = true;
                            }
                            else
                            {
                                TXT_ShickNo.Enabled = false;
                            }
                            
                            dataGridView1.ReadOnly = true;
                       

                        }
                        else if (Constants.User_Type == "B" && Constants.UserTypeB == "Chairman")
                        {
                            DisableControls();
                            Pic_Sign6.Visible = true;
                            label23.Visible = true;
                            BTN_Sign6.Visible = true;

                            BTN_Sigm1.Enabled = false;
                            BTN_Sigm12.Enabled = false;
                            BTN_Sigm13.Enabled =false;
                            BTN_Sigm14.Enabled = false;
                            BTN_Sign5.Enabled = false;
                            BTN_Sign6.Enabled = true;
                            BTN_Sign7.Enabled = false;
                            dataGridView1.ReadOnly = true;

                       
                        }
                        else if (Constants.User_Type == "B" && Constants.UserTypeB == "ViceChairman")
                        {
                            DisableControls();
                            Pic_Sign6.Visible = true;
                            label23.Visible = true;
                            BTN_Sign6.Visible = true;

                            BTN_Sigm1.Enabled = false;
                            BTN_Sigm12.Enabled = false;
                            BTN_Sigm13.Enabled = false;
                            BTN_Sigm14.Enabled = false;
                            BTN_Sign5.Enabled = false;
                            BTN_Sign6.Enabled = true;
                            BTN_Sign7.Enabled = false;
                            dataGridView1.ReadOnly = true;
                       
                        }
                        else if (Constants.User_Type == "B" && Constants.UserTypeB == "Stock")
                        {
                            //EnabControls();
                            BTN_Sigm1.Enabled = true;
                            BTN_Sigm12.Enabled = true; 
                            BTN_Sigm13.Enabled = true;
                            BTN_Sigm14.Enabled = true;
                            BTN_Sign7.Enabled = false;
                            BTN_Sign6.Enabled = false;
                            BTN_Sign5.Enabled = false;
                            dataGridView1.ReadOnly =false;
                       
                        }
                        else if (Constants.User_Type == "A" )
                        {
                            DisableControls();
                            BTN_Sigm1.Enabled = false;
                            BTN_Sigm12.Enabled = false;
                            BTN_Sigm13.Enabled = false;
                            BTN_Sigm14.Enabled = false;
                            BTN_Sign7.Enabled = false;
                            BTN_Sign6.Enabled = false;
                            BTN_Sign5.Enabled =true;
                            dataGridView1.ReadOnly = true;
                        }
                        BTN_ChooseTalb.Enabled = false;
                       // dataGridView1.ReadOnly = false;
                       
                    }
                }

            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد حذف امر الشراء ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(TXT_AmrNo.Text))
                {
                    MessageBox.Show("يجب اختيار امر الشراء  اولا");
                    return;
                }
                Constants.opencon();
                string cmdstring = "Exec SP_DeleteAmrshera @TNO,@FY,@aot output";

                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_AmrNo.Text));
                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text.ToString());
                cmd.Parameters.Add("@aot", SqlDbType.Int, 32);  //-------> output parameter
                cmd.Parameters["@aot"].Direction = ParameterDirection.Output;

                int flag;

                try
                {
                    cmd.ExecuteNonQuery();
                    executemsg = true;
                    flag = (int)cmd.Parameters["@aot"].Value;
                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    MessageBox.Show(sqlEx.ToString());
                    flag = (int)cmd.Parameters["@aot"].Value;
                }
                if (executemsg == true && flag == 1)
                {
                    MessageBox.Show("تم الحذف بنجاح");
                    Input_Reset();
                }
                Constants.closecon();
            }
        }

        private void Cmb_FY_SelectedIndexChanged(object sender, EventArgs e)
        {
             if (AddEditFlag == 0)
            {
                Constants.opencon();
               
               TXT_AmrNo.AutoCompleteMode = AutoCompleteMode.None;
                TXT_AmrNo.AutoCompleteSource = AutoCompleteSource.None; ;
                string cmdstring3 = "SELECT  Amrshraa_No from T_Awamershraa  where AmrSheraa_sanamalia='" + Cmb_FY.Text + "' order by  Amrshraa_No";
                SqlCommand cmd3 = new SqlCommand(cmdstring3, Constants.con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                //---------------------------------
                if (dr3.HasRows == true)
                {
                    while (dr3.Read())
                    {
                        TalbColl.Add(dr3["Amrshraa_No"].ToString());

                    }
                }
              
                TXT_AmrNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                TXT_AmrNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                TXT_AmrNo.AutoCompleteCustomSource = TalbColl;
                Constants.closecon();

            }
            //go and get talbTawreed_no for this FYear
            if (AddEditFlag == 2)//add
            {
                //call sp that get last num that eentered for this MM and this YYYY
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
                string cmdstring = "select ( COALESCE(MAX( Amrshraa_No), 0)) from  T_Awamershraa where AmrSheraa_sanamalia=@FY ";
                SqlCommand cmd = new SqlCommand(cmdstring, con);

                // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
                
                int flag;

                try
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    // cmd.ExecuteNonQuery();
                    var count = cmd.ExecuteScalar();
                    executemsg = true;
                    //  if (cmd.Parameters["@Num"].Value != null && cmd.Parameters["@Num"].Value != DBNull.Value)
                    if (count != null && count != DBNull.Value)
                    {
                        //  flag = (int)cmd.Parameters["@Num"].Value;

                        flag = (int)count;
                        flag = flag + 1;
                        TXT_AmrNo.Text = flag.ToString();//el rakm el new

                    }

                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    MessageBox.Show(sqlEx.ToString());
                    // flag = (int)cmd.Parameters["@Num"].Value;
                }
            }
        
        }
        public void SearchTalb(int x)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring = "select * from T_Awamershraa where Amrshraa_No=@TN and AmrSheraa_sanamalia=@FY";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
            if (x == 1)
            {
                cmd.Parameters.AddWithValue("@TN", TXT_AmrNo.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TN", Cmb_AmrNo2.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            }
            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);


            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {

                    Cmb_FY.Text = dr["AmrSheraa_sanamalia"].ToString();
                   // Cmb_FY2.Text = dr["monaksa_sanamalia"].ToString();
                    TXT_AmrNo.Text = dr["Amrshraa_No"].ToString();
                   // TXT_MonksaNo.Text = dr["Monaksa_No"].ToString();
                    TXT_Momayz.Text = dr["Momayz"].ToString();
                    txt_arabicword.Text = dr["ArabicAmount"].ToString();
                    TXT_TalbNo.Text = dr["Talb_Twred"].ToString();
                    TXT_Edara.Text = dr["NameEdara"].ToString();
                    TXT_CodeEdara.Text = dr["CodeEdara"].ToString();
                    TXT_ShickNo.Text = dr["ShickNo"].ToString();
                    TXT_Date.Text = dr["Date_amrshraa"].ToString();
                    CMB_Sadr.Text = dr["Sadr_To"].ToString();
                    TXT_BndMwazna.Text = dr["Bnd_Mwazna"].ToString();
                    TXT_Payment.Text = dr["Payment_Method"].ToString();
                    TXT_TaslemDate.Text = dr["Date_Tslem"].ToString();
                    TXT_TaslemPlace.Text = dr["Mkan_Tslem"].ToString();
                    TXT_Name.Text = dr["Shick_Name"].ToString();
                    TXT_HesabMward1.Text = dr["Hesab_Mward"].ToString();
                    TXT_HesabMward2.Text = dr["Hesab_Mward"].ToString();
                    TXT_Egmali.Text = dr["Egmali"].ToString();
                    TXT_EgmaliAfter.Text = dr["Egmali"].ToString();
                    TXT_EgmaliBefore.Text = dr["EgmaliBefore"].ToString();
                    TXT_EgmaliDareba.Text = dr["EgmaliDareba"].ToString();
                    BuyMethod = dr["BuyMethod"].ToString();
                    if (BuyMethod == "1")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (BuyMethod == "2")
                    {
                        radioButton2.Checked = true;
                    }
                    else if (BuyMethod == "3")
                    {
                        radioButton3.Checked = true;
                    }
                    else if (BuyMethod == "4")
                    {
                        radioButton4.Checked = true;
                    }
                    else if (BuyMethod == "5")
                    {
                        radioButton5.Checked = true;
                    }
                    else  if (BuyMethod == "6")
                    {
                        radioButton6.Checked = true;
                    }

                    AmrsheraaType = Convert.ToInt32(dr["AmrsheraaType"].ToString());
                    FinancialType = Convert.ToInt32(dr["FinancialType"].ToString());

                    string s1 = dr["Sign1"].ToString();
                    string s2 = dr["Sign12"].ToString();
                    string s3 = dr["Sign13"].ToString();
                    string s4 = dr["Sign14"].ToString();
                    string s5 = dr["Sign3"].ToString();
                    string s6 = dr["Sign33"].ToString();
                    string s7= dr["Sign2"].ToString();
                    //dr.Close();


                    if (s1 != "")
                    {
                        string p = Constants.RetrieveSignature("1", "3",s1);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename1 = p.Split(':')[1];
                            wazifa1 = p.Split(':')[2];
                            pp = p.Split(':')[0];

                            ((PictureBox)this.signatureTable.Controls["panel15"].Controls["Pic_Sign" + "1"]).Image = Image.FromFile(@pp);

                            FlagSign1 = 1;
                            FlagEmpn1 = s1;
                            ((PictureBox)this.signatureTable.Controls["panel15"].Controls["Pic_Sign" + "1"]).BackColor = Color.Green;
                            toolTip1.SetToolTip(Pic_Sign1, Ename1 + Environment.NewLine + wazifa1);
                        }

                    }
                    else
                    {
                        ((PictureBox)this.signatureTable.Controls["panel15"].Controls["Pic_Sign" + "1"]).BackColor = Color.Red;
                    }
                    if (s2 != "")
                    {
                        string p = Constants.RetrieveSignature("2", "3",s2);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename2 = p.Split(':')[1];
                            wazifa2 = p.Split(':')[2];
                            pp = p.Split(':')[0];

                            ((PictureBox)this.signatureTable.Controls["panel16"].Controls["Pic_Sign" + "2"]).Image = Image.FromFile(@pp);

                            FlagSign2 = 1;
                            FlagEmpn2 = s2;
                            ((PictureBox)this.signatureTable.Controls["panel16"].Controls["Pic_Sign" + "2"]).BackColor = Color.Green;
                            toolTip1.SetToolTip(Pic_Sign2, Ename2 + Environment.NewLine + wazifa2);
                        }

                    }
                    else
                    {
                        ((PictureBox)this.signatureTable.Controls["panel16"].Controls["Pic_Sign" + "2"]).BackColor = Color.Red;
                    }
                    if (s3 != "")
                    {
                        string p = Constants.RetrieveSignature("3", "3",s3);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename3 = p.Split(':')[1];
                            wazifa3 = p.Split(':')[2];
                            pp = p.Split(':')[0];

                            ((PictureBox)this.signatureTable.Controls["panel17"].Controls["Pic_Sign" + "3"]).Image = Image.FromFile(@pp);

                            FlagSign3 = 1;
                            FlagEmpn3 = s3;
                            ((PictureBox)this.signatureTable.Controls["panel17"].Controls["Pic_Sign" + "3"]).BackColor = Color.Green;
                            toolTip1.SetToolTip(Pic_Sign3, Ename3 + Environment.NewLine + wazifa3);
                        }

                    }
                    else
                    {
                        ((PictureBox)this.signatureTable.Controls["panel17"].Controls["Pic_Sign" + "3"]).BackColor = Color.Red;
                    }
                    if (s4 != "")
                    {
                        string p = Constants.RetrieveSignature("4", "3", s4);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename4 = p.Split(':')[1];
                            wazifa4 = p.Split(':')[2];
                            pp = p.Split(':')[0];

                            ((PictureBox)this.signatureTable.Controls["panel18"].Controls["Pic_Sign" + "4"]).Image = Image.FromFile(@pp);

                            FlagSign4 = 1;
                            FlagEmpn4 = s4;
                            ((PictureBox)this.signatureTable.Controls["panel18"].Controls["Pic_Sign" + "4"]).BackColor = Color.Green;
                            toolTip1.SetToolTip(Pic_Sign4, Ename4 + Environment.NewLine + wazifa4);
                        }

                    }
                    else
                    {
                        ((PictureBox)this.signatureTable.Controls["panel18"].Controls["Pic_Sign" + "4"]).BackColor = Color.Red;
                    }
                    ///////////////////
                    if (s5 != "")
                    {
                    //    string p = Constants.RetrieveSignature("5", "3", s5);
                        string p = Constants.RetrieveSignature("3", "1", s5);

                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename5 = p.Split(':')[1];
                            wazifa5 = p.Split(':')[2];
                            pp = p.Split(':')[0];

                            ((PictureBox)this.signatureTable.Controls["panel19"].Controls["Pic_Sign" + "5"]).Image = Image.FromFile(@pp);

                            FlagSign5 = 1;
                            FlagEmpn5 = s5;
                            ((PictureBox)this.signatureTable.Controls["panel19"].Controls["Pic_Sign" + "5"]).BackColor = Color.Green;
                            toolTip1.SetToolTip(Pic_Sign5, Ename5 + Environment.NewLine + wazifa5);
                        }

                    }
                    else
                    {
                        ((PictureBox)this.signatureTable.Controls["panel19"].Controls["Pic_Sign" + "5"]).BackColor = Color.Red;
                    }
                    ////////////////////
                    if (s6 != "")
                    {
                        string p = Constants.RetrieveSignature("6", "3", s6);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename6 = p.Split(':')[1];
                            wazifa6 = p.Split(':')[2];
                            pp = p.Split(':')[0];

                            //((PictureBox)this.panel1.Controls["Pic_Sign" + "6"]).Image = Image.FromFile(@pp);

                            FlagSign6= 1;
                            FlagEmpn6 = s6;
                            //((PictureBox)this.panel1.Controls["Pic_Sign" + "6"]).BackColor = Color.Green;
                            toolTip1.SetToolTip(Pic_Sign6, Ename6 + Environment.NewLine + wazifa6);
                        }

                    }
                    else
                    {
                        //((PictureBox)this.panel1.Controls["Pic_Sign" + "6"]).BackColor = Color.Red;
                    }
                    ///////////////////////////
                    if (s7 != "")
                    {
                        string p = Constants.RetrieveSignature("7", "3", s7);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename7 = p.Split(':')[1];
                            wazifa7= p.Split(':')[2];
                            pp = p.Split(':')[0];

                            //((PictureBox)this.panel1.Controls["Pic_Sign" + "7"]).Image = Image.FromFile(@pp);

                            FlagSign7 = 1;
                            FlagEmpn7 = s7;
                            //((PictureBox)this.panel1.Controls["Pic_Sign" + "7"]).BackColor = Color.Green;
                            toolTip1.SetToolTip(Pic_Sign7, Ename7 + Environment.NewLine + wazifa7);
                        }

                    }
                    else
                    {
                        //((PictureBox)this.panel1.Controls["Pic_Sign" + "7"]).BackColor = Color.Red;
                    }


                }
                if (x == 1)
                {
                    BTN_Print.Enabled = true;
                }
                else
                {
                    BTN_Print2.Enabled = true;
                }
               
            }
            else
            {
                MessageBox.Show("من فضلك تاكد من رقم امر الشراء");
                if (x == 1)
                {
                    BTN_Print.Enabled = false;
                }
                else
                {
                    BTN_Print2.Enabled = false;
                }

            }
            dr.Close();


            //  string query1 = "SELECT  [TalbTwareed_No] ,[FYear] ,[Bnd_No],[RequestedQuan],[Unit],[BIAN_TSNIF] ,[STOCK_NO_ALL],[Quan] ,[ArrivalDate] FROM [T_TalbTawreed_Benod] where  [TalbTwareed_No]=@T and [FYear]=@F ";
            //  SqlCommand cmd1 = new SqlCommand(query1, Constants.con);
            //  cmd1.Parameters.AddWithValue("@T",Cmb_TalbNo2.Text);
            //  cmd1.Parameters.AddWithValue("@F", Cmb_FYear2.Text);


            // DT.Clear();
            // DT.Load(cmd1.ExecuteReader());
            // cleargridview();
            GetData(Convert.ToInt32(TXT_AmrNo.Text), Cmb_FY.Text);
            if (DT.Rows.Count == 0)
            {
                //  MessageBox.Show("لا يوجد حركات لهذا الموظف");
                // Input_Reset();
                //   label11.Visible = false;
                // label12.Visible = false;
                // BTN_Save.Visible = false;
                // panel2.Visible = false;

            }
            else
            {

                
            }
            // searchbtn1 = false;
            //  DataGridViewReset();

            Constants.closecon();
        }
        private void TXT_AmrNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && AddEditFlag == 2)
            {
                
                GetData(Convert.ToInt32(TXT_AmrNo.Text), Cmb_FY.Text);

            }
            else if (e.KeyCode == Keys.Enter && AddEditFlag == 0)
            {
                cleargridview();
                SearchTalb(1);
            }
        }

        private void BTN_ChooseTalb_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Cmb_FY.Text))
            {
                MessageBox.Show("من فضلك اختار السنة المالية لامر الشراء");
                return;
            }
            if (string.IsNullOrEmpty(Cmb_FY2.Text))
            {
               // MessageBox.Show("من فضلك اختار السنة المالية للمناقصة");
             //   return;
            }
            if (string.IsNullOrEmpty(TXT_AmrNo.Text))
            {
                MessageBox.Show("من فضلك اختار رقم لامر الشراء");
                return;
            }
            if (string.IsNullOrEmpty(TXT_MonksaNo.Text))
            {
                //MessageBox.Show("من فضلك اختار رقم المناقصة");
               // return;
            }



            GetData(Convert.ToInt32(TXT_AmrNo.Text), Cmb_FY.Text);

            Amrsheraa_PopUp popup = new Amrsheraa_PopUp();
          // popup.Show();
       

           // Show testDialog as a modal dialog and determine if DialogResult = OK.
           if (popup.ShowDialog(this) == DialogResult.OK)
           {
               TXT_Type.Text = popup.BM2;
               BuyMethod = popup.BM;
               if (popup.BM == "1")
               {

                   radioButton1.Checked = true;
                   radioButton2.Checked = false;
                   radioButton3.Checked = false;
                   radioButton4.Checked = false;
                   radioButton5.Checked = false;
                   radioButton6.Checked = false;
               }
               else if (popup.BM == "2")
               {
                   radioButton1.Checked = false;
                   radioButton2.Checked = true;
                   radioButton3.Checked = false;
                   radioButton4.Checked = false;
                   radioButton5.Checked = false;
                   radioButton6.Checked = false;
               }
               else if (popup.BM == "3")
               {
                   radioButton1.Checked = false;
                   radioButton2.Checked = false;
                   radioButton3.Checked =true;
                   radioButton4.Checked = false;
                   radioButton5.Checked = false;
                   radioButton6.Checked = false;
               }
               else if (popup.BM == "4")
               {
                   radioButton1.Checked = false;
                   radioButton2.Checked =false;
                   radioButton3.Checked = false;
                   radioButton4.Checked = true;
                   radioButton5.Checked = false;
                   radioButton6.Checked = false;
               }
               else if (popup.BM == "5")
               {
                   radioButton1.Checked = false;
                   radioButton2.Checked = false;
                   radioButton3.Checked = false;
                   radioButton4.Checked = false;
                   radioButton5.Checked = true;
                   radioButton6.Checked = false;
               }
               else if (popup.BM == "6")
               {
                   radioButton1.Checked = false;
                   radioButton2.Checked = false;
                   radioButton3.Checked = false;
                   radioButton4.Checked = false;
                   radioButton5.Checked = false;
                   radioButton6.Checked = true;
               }
               if (popup.dataGridView1.SelectedRows.Count > 0)
               {
                 //  foreach (DataGridViewRow row in popup.dataGridView1.SelectedRows)
                 //  {
                   foreach (DataGridViewRow row in popup.dataGridView1.Rows)
                   {

                       if (!row.IsNewRow && row.Selected)
                       {
                           // MessageBox.Show(row.Index.ToString());
                /////////////////////   //   table.ImportRow(((DataTable)popup.dataGridView1.DataSource).Rows[row.Index]);
                      // /////////////////////////////  {
                       r = dataGridView1.Rows.Count - 1;

                       rowflag = 1;
                       DataRow newRow = table.NewRow();

                       // Add the row to the rows collection.
                       //   table.Rows.Add(newRow);
                       table.Rows.InsertAt(newRow, r);

                       dataGridView1.DataSource = table;
                      dataGridView1.Rows[r].Cells[0].Value = TXT_AmrNo.Text.ToString();
                     // dataGridView1.Rows[r].Cells[1].Value = TXT_MonksaNo.Text.ToString();
                      dataGridView1.Rows[r].Cells[2].Value = Cmb_FY2.Text.ToString();
                      dataGridView1.Rows[r].Cells[3].Value = Cmb_FY.Text.ToString();

                      dataGridView1.Rows[r].Cells[4].Value = row.Cells[0].Value;
                      dataGridView1.Rows[r].Cells[5].Value = row.Cells[1].Value;
                      dataGridView1.Rows[r].Cells[6].Value = row.Cells[2].Value;
                      dataGridView1.Rows[r].Cells[7].Value = popup.TXT_CodeEdara.Text.ToString();

                      dataGridView1.Rows[r].Cells[8].Value = popup.TXT_Edara.Text.ToString();
                      dataGridView1.Rows[r].Cells[9].Value = popup.TXT_BndMwazna.Text.ToString();
                      dataGridView1.Rows[r].Cells[10].Value = row.Cells[3].Value;
                      dataGridView1.Rows[r].Cells[12].Value = row.Cells[4].Value;
                      dataGridView1.Rows[r].Cells[13].Value =row.Cells[5].Value;
                      dataGridView1.Rows[r].Cells[15].Value =row.Cells[6].Value;
                    //  table.Rows.InsertAt(newRow, r+1);
                       /*
                      dataGridView1.Rows[r+1].Cells[0].Value = DBNull.Value;
                      dataGridView1.Rows[r + 1].Cells[1].Value = DBNull.Value;
                      dataGridView1.Rows[r + 1].Cells[2].Value = DBNull.Value;
                      dataGridView1.Rows[r + 1].Cells[3].Value = DBNull.Value;

                      dataGridView1.Rows[r + 1].Cells[4].Value = DBNull.Value;
                      dataGridView1.Rows[r + 1].Cells[5].Value = DBNull.Value;
                      dataGridView1.Rows[r + 1].Cells[6].Value = DBNull.Value;
                      dataGridView1.Rows[r + 1].Cells[7].Value = DBNull.Value;

                      dataGridView1.Rows[r + 1].Cells[8].Value = DBNull.Value;
                      dataGridView1.Rows[r + 1].Cells[9].Value = DBNull.Value;
                      dataGridView1.Rows[r + 1].Cells[10].Value = DBNull.Value;
                      dataGridView1.Rows[r + 1].Cells[11].Value = DBNull.Value;
                      dataGridView1.Rows[r + 1].Cells[12].Value = DBNull.Value;
                      dataGridView1.Rows[r + 1].Cells[14].Value = DBNull.Value;*/
                      if (rowflag == 1)
                      {

                      }
                      //  dataGridView1.Rows[r].Cells[3].Value = TXT_StockBian.Text;
                    //  dataGridView1.Rows[r].Cells[6].Value = TXT_StockNoAll.Text;

                       
                   }  }
                  table.AcceptChanges();
               }
               dataGridView1.DataSource = table;
               // Read the contents of testDialog's TextBox.سس
              // this.txtResult.Text = popup.TextBox1.Text;
           }
           else
           {
             //  this.txtResult.Text = "Cancelled";
           }
          popup.Dispose();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(TXT_Edara.Text ))
            {
                MessageBox.Show("من فضلك تاكد من توقيع ادخال جميع البيانات");
                return;
            }
            if (AddEditFlag == 2)
            {
                if (FlagSign1 != 1)
                {
                    MessageBox.Show("من فضلك تاكد من توقيع امر الشراء");
                    return;
                }
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string cmdstring = "exec SP_InsertAwamershraa @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p311,@p3111,@p31111,@p311111,@p32,@p33,@p333,@p3333,@p33333,@p38,@p39,@p40,@p41,@p34 out";
                SqlCommand cmd = new SqlCommand(cmdstring, con);

                cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_AmrNo.Text));
              
              //  cmd.Parameters.AddWithValue("@p2", Convert.ToInt32(TXT_MonksaNo.Text));
                cmd.Parameters.AddWithValue("@p2",DBNull.Value);
              
                cmd.Parameters.AddWithValue("@p3",(Cmb_FY2.Text));
                cmd.Parameters.AddWithValue("@p4", (Cmb_FY.Text));
                cmd.Parameters.AddWithValue("@p5", (CMB_Sadr.Text));

                cmd.Parameters.AddWithValue("@p6", (Convert.ToDateTime(TXT_Date.Value.ToShortDateString())));
        
                cmd.Parameters.AddWithValue("@p7",(TXT_Momayz.Text));
                cmd.Parameters.AddWithValue("@p8",(TXT_Name.Text));
                cmd.Parameters.AddWithValue("@p9", (TXT_Payment.Text));
                cmd.Parameters.AddWithValue("@p10", (TXT_TaslemDate.Text));



                cmd.Parameters.AddWithValue("@p11", (TXT_TaslemPlace.Text));
                cmd.Parameters.AddWithValue("@p12",(TXT_CodeEdara.Text));
                cmd.Parameters.AddWithValue("@p13",(TXT_Edara.Text));
                cmd.Parameters.AddWithValue("@p14", (TXT_BndMwazna.Text));
                cmd.Parameters.AddWithValue("@p15",(TXT_TalbNo.Text));
                cmd.Parameters.AddWithValue("@p16",(TXT_HesabMward1.Text));
                if (TXT_Egmali.Text.ToString() == "")
                {
                      cmd.Parameters.AddWithValue("@p17", DBNull.Value);
                }
    
                else
                {
                      cmd.Parameters.AddWithValue("@p17",Convert.ToDecimal(TXT_Egmali.Text));
                }

            //    cmd.Parameters.AddWithValue("@p17", DBNull.Value);
                cmd.Parameters.AddWithValue("@p18", DBNull.Value);//taamen
                cmd.Parameters.AddWithValue("@p19", DBNull.Value);//dman
                cmd.Parameters.AddWithValue("@p20",  DBNull.Value);//dareba


               cmd.Parameters.AddWithValue("@p21", DBNull.Value);//shroot
                cmd.Parameters.AddWithValue("@p22", DBNull.Value);//confirm date
                cmd.Parameters.AddWithValue("@p23", DBNull.Value);//date of arrival
                cmd.Parameters.AddWithValue("@p24", DBNull.Value);//finished
                cmd.Parameters.AddWithValue("@p25",TXT_Date.Value.Day.ToString() );//dd
                cmd.Parameters.AddWithValue("@p26", DBNull.Value);//ww
                cmd.Parameters.AddWithValue("@p27", TXT_Date.Value.Month.ToString());//mm
                cmd.Parameters.AddWithValue("@p28",TXT_Date.Value.Year.ToString() );//yy
              
              
                cmd.Parameters.AddWithValue("@p29", FlagEmpn1);

                cmd.Parameters.AddWithValue("@p30", DBNull.Value);

                cmd.Parameters.AddWithValue("@p31", DBNull.Value);

                cmd.Parameters.AddWithValue("@p311", DBNull.Value);
                cmd.Parameters.AddWithValue("@p3111", DBNull.Value);
                cmd.Parameters.AddWithValue("@p31111", DBNull.Value);
                cmd.Parameters.AddWithValue("@p311111", DBNull.Value);



                cmd.Parameters.AddWithValue("@p32", Constants.User_Name.ToString());
                cmd.Parameters.AddWithValue("@p33", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                cmd.Parameters.AddWithValue("@p333", txt_arabicword.Text);
                if (TXT_EgmaliDareba.Text.ToString() == "")
                {
                    cmd.Parameters.AddWithValue("@p3333", DBNull.Value);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@p3333", Convert.ToDecimal(TXT_EgmaliDareba.Text));
                }
                if (TXT_EgmaliBefore.Text.ToString() == "")
                {
                    cmd.Parameters.AddWithValue("@p33333", DBNull.Value);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@p33333", Convert.ToDecimal(TXT_EgmaliBefore.Text));
                }

                cmd.Parameters.AddWithValue("@p38", BuyMethod);
                
                cmd.Parameters.AddWithValue("@p39",FinancialType.ToString());
                
                cmd.Parameters.AddWithValue("@p40",1);
                cmd.Parameters.AddWithValue("@p41", TXT_ShickNo.Text);
                
                cmd.Parameters.Add("@p34", SqlDbType.Int, 32);  //-------> output parameter
                cmd.Parameters["@p34"].Direction = ParameterDirection.Output;

                int flag;

                try
                {
                    cmd.ExecuteNonQuery();
                    executemsg = true;
                    flag = (int)cmd.Parameters["@p34"].Value;
                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    MessageBox.Show(sqlEx.ToString());
                    flag = (int)cmd.Parameters["@p34"].Value;
                }
                
                if (executemsg == true && flag == 1)
                {
                    

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (!row.IsNewRow)
                        {



                            string q = "exec SP_InsertBnodAwamershraa @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@P111,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24 ";
                            cmd = new SqlCommand(q, con);
                            cmd.Parameters.AddWithValue("@p1",Convert.ToInt32( row.Cells[0].Value));
                       //     cmd.Parameters.AddWithValue("@p2",Convert.ToInt32(  row.Cells[1].Value));
                            cmd.Parameters.AddWithValue("@p2", DBNull.Value);
                            cmd.Parameters.AddWithValue("@p3", row.Cells[2].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p4", row.Cells[3].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p5",Convert.ToInt32(  row.Cells[4].Value));
                            cmd.Parameters.AddWithValue("@p6", row.Cells[5].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p7", row.Cells[6].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p8", row.Cells[7].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p9", row.Cells[8].Value ?? DBNull.Value);
                            //cmd.Parameters.AddWithValue("@p9", row.Cells[9].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p10", row.Cells[9].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p11", row.Cells[10].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p111",  DBNull.Value);
                            cmd.Parameters.AddWithValue("@p12", ( row.Cells[12].Value) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p13", row.Cells[13].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p14", row.Cells[14].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p15", row.Cells[15].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p16", row.Cells[16].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p17", row.Cells[17].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p18", row.Cells[18].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p19", row.Cells[19].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p20", row.Cells[20].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p21", row.Cells[21].Value ?? DBNull.Value);

                            cmd.Parameters.AddWithValue("@p22", row.Cells[22].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p23", row.Cells[23].Value ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@p24", row.Cells[24].Value ?? DBNull.Value);





                            cmd.ExecuteNonQuery();
                        }
                    }
                    for (int i = 1; i <= 7; i++)
                    {


                        cmdstring = "Exec  SP_InsertSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
                        cmd = new SqlCommand(cmdstring, con);

                        cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_AmrNo.Text));
                        cmd.Parameters.AddWithValue("@TNO2", DBNull.Value);

                        cmd.Parameters.AddWithValue("@FY", Cmb_FY2.Text.ToString());
                        cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
                        cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
                        cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);

                        cmd.Parameters.AddWithValue("@FN", 3);

                        cmd.Parameters.AddWithValue("@SN", i);

                        cmd.Parameters.AddWithValue("@D1", DBNull.Value);

                        cmd.Parameters.AddWithValue("@D2", DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                    SP_UpdateSignatures(1, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    ///////////////////////////////////////////////////
                    MessageBox.Show("تم الإضافة بنجاح  ! ");
                     /*
                      
                         dataGridView1.EndEdit();
                           dataGridView1.DataSource = table;

                        // Getdata("SELECT  [TalbTwareed_No] ,[FYear],[Bnd_No],[RequestedQuan],Unit,[BIAN_TSNIF] ,STOCK_NO_ALL,Quan,[ArrivalDate] FROM [ANRPC_Inventory].[dbo].[T_TalbTawreed_Benod] ");
                        //  // getdata2();

                          dataadapter.InsertCommand = new SqlCommandBuilder(dataadapter).GetInsertCommand();
                          MessageBox.Show(dataadapter.InsertCommand.CommandText);
                        //      MessageBox.Show(dataadapter.InsertCommand.Parameter);
                        //   dataadapter.InsertCommand.Parameters.AddWithValue("p1", )
                        //  dataadapter.ContinueUpdateOnError = true;
                          dataadapter.AcceptChangesDuringUpdate = true;
                         dataadapter.Update(table);
                       MessageBox.Show("تم  الإضافة بنجاح");*/
                    DisableControls();
                    // BTN_PrintPerm.Visible = true;
                    SaveBtn.Visible = false;
                    AddEditFlag = 0;
                    EditBtn.Enabled = true;
                }
                else if (executemsg == true && flag == 2)
                {
                    MessageBox.Show("تم إدخال رقم امر الشراء  من قبل  ! ");
                }
                con.Close();
            }
            else if (AddEditFlag == 1)
            {
              UpdateAmrsheraa();
            }
        }
        public void UpdateAmrsheraa()
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string cmdstring = "Exec SP_UpdateAwamershraa @TNOold,@FYold,@Mold,@FY2old,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p311,@p3111,@p31111,@p311111,@p32,@p33,@p333,@p3333,@p33333,@p38,@p39,@p40,@p41,@p34 out";
          //  SqlCommand cmd = new SqlCommand(cmdstring, con);

            SqlCommand cmd = new SqlCommand(cmdstring, con);
            cmd.Parameters.AddWithValue("@TNOold",Convert.ToInt32( TNO));
            cmd.Parameters.AddWithValue("@FYold", FY);
        //       cmd.Parameters.AddWithValue("@Mold",Convert.ToInt32( MNO));
        //    cmd.Parameters.AddWithValue("@FY2old", FY2);

            cmd.Parameters.AddWithValue("@Mold", DBNull.Value);
            cmd.Parameters.AddWithValue("@FY2old",DBNull.Value);
            

            cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_AmrNo.Text));
           // cmd.Parameters.AddWithValue("@p2", Convert.ToInt32(TXT_MonksaNo.Text));
            cmd.Parameters.AddWithValue("@p2", DBNull.Value);
         
            cmd.Parameters.AddWithValue("@p3", (Cmb_FY2.Text));
            cmd.Parameters.AddWithValue("@p4", (Cmb_FY.Text));
            cmd.Parameters.AddWithValue("@p5", (CMB_Sadr.Text));

            cmd.Parameters.AddWithValue("@p6", (Convert.ToDateTime(TXT_Date.Value.ToShortDateString())));

            cmd.Parameters.AddWithValue("@p7", (TXT_Momayz.Text));
            cmd.Parameters.AddWithValue("@p8", (TXT_Name.Text));
            cmd.Parameters.AddWithValue("@p9", (TXT_Payment.Text));
            cmd.Parameters.AddWithValue("@p10", (TXT_TaslemDate.Text));



            cmd.Parameters.AddWithValue("@p11", (TXT_TaslemPlace.Text));
            cmd.Parameters.AddWithValue("@p12", (TXT_CodeEdara.Text));
            cmd.Parameters.AddWithValue("@p13", (TXT_Edara.Text));
            cmd.Parameters.AddWithValue("@p14", (TXT_BndMwazna.Text));
            cmd.Parameters.AddWithValue("@p15", (TXT_TalbNo.Text));
            cmd.Parameters.AddWithValue("@p16", (TXT_HesabMward1.Text));
            //  cmd.Parameters.AddWithValue("@p17",Convert.ToDecimal(TXT_Egmali.Text)??DBNull.Value);
            if (TXT_Egmali.Text.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@p17", DBNull.Value);
            }

            else
            {
                cmd.Parameters.AddWithValue("@p17", Convert.ToDecimal(TXT_Egmali.Text));
            }
            cmd.Parameters.AddWithValue("@p18", DBNull.Value);//taamen
            cmd.Parameters.AddWithValue("@p19", DBNull.Value);//dman
            cmd.Parameters.AddWithValue("@p20", DBNull.Value);//dareba


            cmd.Parameters.AddWithValue("@p21", DBNull.Value);//shroot
            cmd.Parameters.AddWithValue("@p22", DBNull.Value);//confirm date
            cmd.Parameters.AddWithValue("@p23", DBNull.Value);//date of arrival
            cmd.Parameters.AddWithValue("@p24", DBNull.Value);//finished
            cmd.Parameters.AddWithValue("@p25", TXT_Date.Value.Day.ToString());//dd
            cmd.Parameters.AddWithValue("@p26", DBNull.Value);//ww
            cmd.Parameters.AddWithValue("@p27", TXT_Date.Value.Month.ToString());//mm
            cmd.Parameters.AddWithValue("@p28", TXT_Date.Value.Year.ToString());//yy
              
              
            if (FlagSign1 == 1)
            {
                cmd.Parameters.AddWithValue("@p29", FlagEmpn1);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p29", DBNull.Value);

            }
            if (FlagSign2 == 1)
            {
                cmd.Parameters.AddWithValue("@p30", FlagEmpn2);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p30", DBNull.Value);

            }
            if (FlagSign3 == 1)
            {
                cmd.Parameters.AddWithValue("@p31", FlagEmpn3);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p31", DBNull.Value);

            }

            if (FlagSign4== 1)
            {
                cmd.Parameters.AddWithValue("@p311", FlagEmpn4);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p311", DBNull.Value);

            }
            if (FlagSign7 == 1)
            {
                cmd.Parameters.AddWithValue("@p3111", FlagEmpn7);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p3111", DBNull.Value);

            }
            if (FlagSign5 == 1)
            {
                cmd.Parameters.AddWithValue("@p31111", FlagEmpn5);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p31111", DBNull.Value);

            }
            if (FlagSign6 == 1)
            {
                cmd.Parameters.AddWithValue("@p311111", FlagEmpn6);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p311111", DBNull.Value);

            }

            cmd.Parameters.AddWithValue("@p32", Constants.User_Name.ToString());
            cmd.Parameters.AddWithValue("@p33", Convert.ToDateTime(DateTime.Now.ToShortDateString()));

            cmd.Parameters.AddWithValue("@p333", txt_arabicword.Text);
            if (TXT_EgmaliDareba.Text.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@p3333", DBNull.Value);
            }

            else
            {
                cmd.Parameters.AddWithValue("@p3333", Convert.ToDecimal(TXT_EgmaliDareba.Text));
            }
            if (TXT_EgmaliBefore.Text.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@p33333", DBNull.Value);
            }

            else
            {
                cmd.Parameters.AddWithValue("@p33333", Convert.ToDecimal(TXT_EgmaliBefore.Text));
            }
            cmd.Parameters.AddWithValue("@p38", BuyMethod);

            cmd.Parameters.AddWithValue("@p39", FinancialType.ToString());

            cmd.Parameters.AddWithValue("@p40", 1);

            cmd.Parameters.AddWithValue("@p41", TXT_ShickNo.Text);
                
            cmd.Parameters.Add("@p34", SqlDbType.Int, 32);  //-------> output parameter
            cmd.Parameters["@p34"].Direction = ParameterDirection.Output;

            int flag;

            try
            {
                cmd.ExecuteNonQuery();
                executemsg = true;
                flag = (int)cmd.Parameters["@p34"].Value;
            }
            catch (SqlException sqlEx)
            {
                executemsg = false;
                //MessageBox.Show(sqlEx.ToString());
                flag = (int)cmd.Parameters["@p34"].Value;
            }
            if (executemsg == true && flag == 2)
            {


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (!row.IsNewRow)
                    {

                        string q = "exec SP_InsertBnodAwamershraa @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p111,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24 ";
                        cmd = new SqlCommand(q, con);
                        cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(row.Cells[0].Value));
                       
                     //   cmd.Parameters.AddWithValue("@p2", Convert.ToInt32(row.Cells[1].Value));
                        cmd.Parameters.AddWithValue("@p2", DBNull.Value);
                       
                        cmd.Parameters.AddWithValue("@p3", row.Cells[2].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p4", row.Cells[3].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p5", Convert.ToInt32(row.Cells[4].Value));
                        cmd.Parameters.AddWithValue("@p6", row.Cells[5].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p7", row.Cells[6].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p8", row.Cells[7].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p9", row.Cells[8].Value ?? DBNull.Value);
                        //cmd.Parameters.AddWithValue("@p9", row.Cells[9].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p10", row.Cells[9].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p11", row.Cells[10].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p111",  DBNull.Value);
                        cmd.Parameters.AddWithValue("@p12", (row.Cells[12].Value) ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p13", row.Cells[13].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p14", row.Cells[14].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p15", row.Cells[15].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p16", row.Cells[16].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p17", row.Cells[17].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p18", row.Cells[18].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p19", row.Cells[19].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p20", row.Cells[20].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p21", row.Cells[21].Value ?? DBNull.Value);


                        cmd.Parameters.AddWithValue("@p22", row.Cells[22].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p23", row.Cells[23].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@p24", row.Cells[24].Value ?? DBNull.Value);



                        cmd.ExecuteNonQuery();
                    }
                }
                if (FlagSign2 == 1)
                {

                    SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(3, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign3 == 1)
                {

                    SP_UpdateSignatures(3, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign4 == 1)
                {

                    SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(5, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign5 == 1)
                {

                    SP_UpdateSignatures(5, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(7, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign7 == 1)
                {

                    SP_UpdateSignatures(7, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
           
                if (FlagSign6 == 1)
                {

                    SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    // SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                    /*
                    
                     dataGridView1.EndEdit();
                       dataGridView1.DataSource = table;

                     Getdata("SELECT  [TalbTwareed_No] ,[FYear],[Bnd_No],[RequestedQuan],Unit,[BIAN_TSNIF] ,STOCK_NO_ALL,Quan,[ArrivalDate] FROM [ANRPC_Inventory].[dbo].[T_TalbTawreed_Benod] ");
                    //  // getdata2();

                      dataadapter.InsertCommand = new SqlCommandBuilder(dataadapter).GetInsertCommand();
                      MessageBox.Show(dataadapter.InsertCommand.CommandText);
                    //      MessageBox.Show(dataadapter.InsertCommand.Parameter);
                    //   dataadapter.InsertCommand.Parameters.AddWithValue("p1", )

                    dataadapter.Update(table);
                    MessageBox.Show("تم  الإضافة بنجاح");

                }*/
            //    dataadapter.InsertCommand = new SqlCommandBuilder(dataadapter).GetInsertCommand();
             //   MessageBox.Show(dataadapter.InsertCommand.CommandText);
                //      MessageBox.Show(dataadapter.InsertCommand.Parameter);
                //   dataadapter.InsertCommand.Parameters.AddWithValue("p1", )

           //     dataadapter.Update(table);

                MessageBox.Show("تم التعديل بنجاح  ! ");
                DisableControls();
                // BTN_PrintPerm.Visible = true;
                SaveBtn.Visible = false;
                AddEditFlag = 0;
                Addbtn.Enabled = true;
            }
            else if (executemsg == true && flag == 3)
            {
                MessageBox.Show("تم إدخال رقم امر الشراء  من قبل  ! ");
            }
            con.Close();
        }
        private void BTN_Sign2_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 || FlagSign1 != 2 || FlagSign1 != 3 || FlagSign1 != 4 || FlagSign1 != 5 || FlagSign1 != 6)
            {
                MessageBox.Show("يرجى التاكد من التوقعات السابقة اولا");
                return;
            }
            Empn7 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع الحسابات", "");
         
            Sign7= Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع الحسابات", "");
         
            if (Sign7 != "" && Empn7 !="")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("7", "3", Sign7, Empn7);
                if (result.Item3 == 1)
                {
                    Pic_Sign7.Image = Image.FromFile(@result.Item1);

                    FlagSign7 = result.Item2;
                    FlagEmpn7 = Empn7;
                }
                else
                {
                    FlagSign7= 0;
                    FlagEmpn7= "";
                }
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }

        private void BTN_Sigm1_Click(object sender, EventArgs e)
        {

           Empn1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل  رقم القيد الخاص بك", "توقيع الاعدداد", "");
           
            Sign1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع الاعدادس", "");
           
            if (Sign1 != "" && Empn1 !="")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("1", "3", Sign1, Empn1);
                if (result.Item3 == 1)
                {
                    Pic_Sign1.Image = Image.FromFile(@result.Item1);

                    FlagSign1 = result.Item2;
                    FlagEmpn1 = Empn1;
                }
                else
                {
                    FlagSign1= 0;
                    FlagEmpn1="";
                    
                }
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }

        private void BTN_Sign3_Click(object sender, EventArgs e)
        {
            if (FlagSign2 != 1 || FlagSign1 != 1  || FlagSign3 !=1 || FlagSign4 !=1)
            {
                MessageBox.Show("يرجى التاكد من التوقعات السابقة اولا");
                return;
            }
            Empn5 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "اعتماد مدير عام الادارة الطالبة", "");
          
            Sign5= Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "  اعتماد مدير عام الادارة الطالبة", "");

            Constants.opencon();
            string cmdst = "exec   SP_GetSignatureDetailsAmrSheraaEdaraTalba   @EC,@Empn out,@Password out,@Path out,@Flag out";
            SqlCommand cmd = new SqlCommand(cmdst,Constants.con);
            cmd.Parameters.AddWithValue("@EC", Constants.CodeEdara);

            cmd.Parameters.Add("@Empn", SqlDbType.VarChar, 50);  //-------> output parameter
            cmd.Parameters["@Empn"].Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50);  //-------> output parameter
            cmd.Parameters["@Password"].Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@Path", SqlDbType.VarChar, 250);  //-------> output parameter
            cmd.Parameters["@Path"].Direction = ParameterDirection.Output;


            cmd.Parameters.Add("@Flag", SqlDbType.Int, 32);  //-------> output parameter
            cmd.Parameters["@Flag"].Direction = ParameterDirection.Output;
            int flag = 0;
            string empn="";
            string pass="";
            string path = "";
            try
            {
                cmd.ExecuteNonQuery();
                executemsg = true;
                if(Convert.ToInt32(cmd.Parameters["@Flag"].Value.ToString())==1)
                {
                  empn  = cmd.Parameters["@Empn"].Value.ToString();
                  pass = cmd.Parameters["@Password"].Value.ToString();
                  path=cmd.Parameters["@Path"].Value.ToString();
                  flag = Convert.ToInt32(cmd.Parameters["@Flag"].Value.ToString());
                }
         
               
            }
            catch (SqlException sqlEx)
            {
                executemsg = false;
                MessageBox.Show(sqlEx.ToString());
                //  FinancialType = (int)cmd.Parameters["@F"].Value;
            }
         

            Constants.closecon();

            if (Sign5 != ""&& Empn5 !="")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
            //    Tuple<string, int, int, string, string> result = Constants.CheckSign("5", "3", Sign5,Empn5);
                if (flag == 1 && ((String.Compare(Sign5, pass)) == 0)&& ((String.Compare(Empn5,empn)) == 0))
                {
                    Pic_Sign5.Image = Image.FromFile(@path);

                    FlagSign5 = 1;
                    FlagEmpn5 = Empn5;
                }
                else
                {
                    FlagSign5 = 0;
                    FlagEmpn5 = "";
                }
             /*   if (result.Item3 == 1)
                {
                    Pic_Sign5.Image = Image.FromFile(@result.Item1);

                    FlagSign5 = result.Item2;
                    FlagEmpn5 = Empn5;
                }
                else
                {
                    FlagSign5 = 0;
                    FlagEmpn5 = "";
                }*/
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }

        private void TXT_HesabMward1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXT_HesabMward2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXT_TaslemPlace_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXT_Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXT_Payment_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXT_Date_ValueChanged(object sender, EventArgs e)
        {

        }

        private void TXT_Egmali_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ToWord toWord = new ToWord(Convert.ToDecimal(TXT_Egmali.Text), currencies[0]);
             //   txt_englishword.Text = toWord.ConvertToEnglish();
                txt_arabicword.Text = toWord.ConvertToArabic();
            }
            catch (Exception ex)
            {
             //   txt_englishword.Text = String.Empty;
                txt_arabicword.Text = String.Empty;
            }
        }

        private void TXT_TalbNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXT_BndMwazna_TextChanged(object sender, EventArgs e)
        {

        }


        private void TXT_Edara_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXT_TaslemDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXT_Momayz_TextChanged(object sender, EventArgs e)
        {

        }


        private void Cmb_FY2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TXT_AmrNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXT_MonksaNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void CMB_Sadr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BTN_Save2_Click(object sender, EventArgs e)
        {
            if (AddEditFlag == 1)
            {
                UpdateAmrsheraa();
            }
        }

        private void Cmb_ِAmrNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Input_Reset();
            cleargridview();
            SearchTalb(2);
        }

        private void Cmb_FYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            Input_Reset();
            cleargridview();
            Cmb_AmrNo2.SelectedIndexChanged -= new EventHandler(Cmb_ِAmrNo2_SelectedIndexChanged);
            
            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring = "";
            if (UserB == "Stock")
            {
                cmdstring = "select (Amrshraa_No) from  T_Awamershraa where AmrSheraa_sanamalia=@FY  and (Sign12 is null or Sign13  is null or Sign14 is null) order by  Amrshraa_No";
            }
            else if (UserB=="Finance")
            {
            cmdstring = "select (Amrshraa_No) from  T_Awamershraa where AmrSheraa_sanamalia=@FY  and (Sign3 is not null) order by  Amrshraa_No";
             }
            else if (UserB == "Chairman" || UserB == "ViceChairman")
            {
                cmdstring = "select (Amrshraa_No) from  T_Awamershraa where AmrSheraa_sanamalia=@FY and (Sign3 is not null)  order by  Amrshraa_No";
          
            }
            else if (Constants.User_Type == "A")
            {
                cmdstring = "select (Amrshraa_No) from  T_Awamershraa where AmrSheraa_sanamalia=@FY and (Sign14 is not null) and( Sign3 is null) and CodeEdara=@C  order by  Amrshraa_No";
          
            }

            if (cmdstring != "")
            {

                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
                cmd.Parameters.AddWithValue("@C", Constants.CodeEdara);
                ///   cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);

                DataTable dts = new DataTable();

                dts.Load(cmd.ExecuteReader());
                Cmb_AmrNo2.DataSource = dts;
                Cmb_AmrNo2.ValueMember = "Amrshraa_No";
                Cmb_AmrNo2.DisplayMember = "Amrshraa_No";
                Cmb_AmrNo2.SelectedIndex = -1;
                Cmb_AmrNo2.SelectedIndexChanged += new EventHandler(Cmb_ِAmrNo2_SelectedIndexChanged);
                Constants.closecon();
            }
        }

        private void Column2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
            return;
        }
        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
               && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {

                e.Handled = true;
                return;
            }
            else
            {
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                    return;

                }
                else
                {
                    e.Handled = false;
                    return;
                }


            }

        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 21 ||dataGridView1.CurrentCell.ColumnIndex == 17 ||dataGridView1.CurrentCell.ColumnIndex == 18|dataGridView1.CurrentCell.ColumnIndex == 20 )//reqQuan
            {
                e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);

                //because 2 or 4 or 5 can accept digits also
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
                return;

            }

            else
            {
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);
                // if (dataGridView1.CurrentCell.ColumnIndex != 2 && dataGridView1.CurrentCell.ColumnIndex != 4 && dataGridView1.CurrentCell.ColumnIndex != 5 && dataGridView1.CurrentCell.ColumnIndex != 9) //Desired Column
                // {
                //     //because 2 or 4 or 5 can accept digits also
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() == "" || AddEditFlag == 0)//new row or search mode
                {

                }
                else
                {


                    if (e.ColumnIndex == 17 || e.ColumnIndex == 19 || e.ColumnIndex == 20)
                    {
                        if (e.RowIndex >= 0)
                        {

                            quan = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString());

                            price = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString());
                            totalprice = ((decimal)quan * price);

                            dataGridView1.Rows[e.RowIndex].Cells[18].Value = totalprice;
                            dataGridView1.Rows[e.RowIndex].Cells[21].Value = totalprice;


                        }
                    }



                    if (e.ColumnIndex == 19)
                    {
                        if (e.RowIndex >= 0)
                        {
                            if ((dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString() == "False") && dataGridView1.Rows[e.RowIndex].Cells[20].Value != null)
                            {
                                dareba = 0;
                                //  dareba = (Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[20].Value)) / 100;
                                //dataGridView1.Rows[e.RowIndex].Cells[21].Value = totalprice + ((decimal)dareba * totalprice);
                                dataGridView1.Rows[e.RowIndex].Cells[20].Value = 0;
                                dataGridView1.Rows[e.RowIndex].Cells[21].Value = totalprice;
                            }

                        }
                    }
                    if (e.ColumnIndex == 20)
                    {
                        if (e.RowIndex >= 0)
                        {
                            if ((dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString() == "True") && dataGridView1.Rows[e.RowIndex].Cells[20].Value != null)
                            {
                                dareba = (Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[20].Value)) / 100;
                                dataGridView1.Rows[e.RowIndex].Cells[21].Value = totalprice + ((decimal)dareba * totalprice);
                            }
                        }
                    }
                    // if (e.ColumnIndex ==21 || e.ColumnIndex ==20 ||e.ColumnIndex ==19 ||e.ColumnIndex ==18)
                    if (e.ColumnIndex == 21)
                    {
                        changedflag = 1;

                        decimal sum = 0;

                        decimal sumDareba = 0;
                        decimal sumBefore = 0;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!(row.Cells[e.ColumnIndex].Value == null || row.Cells[e.ColumnIndex].Value == DBNull.Value))
                            {

                                sum = sum + Convert.ToDecimal(row.Cells[21].Value.ToString());
                                if (row.Cells[20].Value.ToString() == "")
                                {
                                    sumDareba = sumDareba + 0;
                                }
                                else
                                {
                                    sumDareba = sumDareba + Convert.ToDecimal(row.Cells[20].Value.ToString());

                                }

                                sumBefore = sumBefore + Convert.ToDecimal(row.Cells[18].Value.ToString());

                                sumDareba = sum - sumBefore;
                                if (e.RowIndex == 0)
                                {


                                    edara = row.Cells[8].Value.ToString();
                                    codeedara = row.Cells[7].Value.ToString();
                                    talbtawreed = row.Cells[4].Value.ToString();
                                    bndmwazna = row.Cells[9].Value.ToString();
                                    TXT_Egmali.Text = sum.ToString("N2");
                                    TXT_EgmaliDareba.Text = sumDareba.ToString("N2");
                                    TXT_EgmaliBefore.Text = sumBefore.ToString("N2");
                                    TXT_EgmaliAfter.Text = sum.ToString("N2");
                                    TXT_Edara.Text = edara;
                                    TXT_BndMwazna.Text = bndmwazna;
                                    TXT_TalbNo.Text = talbtawreed;
                                    TXT_CodeEdara.Text = codeedara;
                                    FinancialType = CheckFinancialStatus(sum, BuyMethod, 1);
                                }
                                else if (e.RowIndex > 0)
                                {
                                    if (string.Compare(TXT_TalbNo.Text, row.Cells[4].Value.ToString()) == 0)
                                    {

                                    }
                                    else
                                    {
                                        edara = edara + "-" + row.Cells[8].Value.ToString();
                                        talbtawreed = talbtawreed + "-" + row.Cells[4].Value.ToString();
                                        bndmwazna = bndmwazna + "-" + row.Cells[9].Value.ToString();
                                        codeedara = codeedara + "-" + row.Cells[7].Value.ToString();
                                    }
                                    //    edara = edara + row.Cells[8].Value.ToString() + "-";
                                    //  talbtawreed = talbtawreed + row.Cells[5].Value.ToString() + "-";
                                    ////   bndmwazna = bndmwazna + row.Cells[9].Value.ToString() + "-";
                                    TXT_Egmali.Text = sum.ToString("N2");
                                    TXT_EgmaliDareba.Text = sumDareba.ToString("N2");
                                    TXT_EgmaliBefore.Text = sumBefore.ToString("N2");
                                    TXT_EgmaliAfter.Text = sum.ToString("N2");
                                    //    dataGridView1.Columns[21].FooterText = 3;
                                    TXT_Edara.Text = edara;
                                    TXT_BndMwazna.Text = bndmwazna;
                                    TXT_TalbNo.Text = talbtawreed;
                                    TXT_CodeEdara.Text = codeedara;
                                    FinancialType = CheckFinancialStatus(sum, BuyMethod, 1);
                                    try
                                    {
                                        ToWord toWord = new ToWord(Convert.ToDecimal(TXT_Egmali.Text), currencies[0]);
                                        //   txt_englishword.Text = toWord.ConvertToEnglish();
                                        txt_arabicword.Text = toWord.ConvertToArabic();
                                    }
                                    catch (Exception ex)
                                    {
                                        //   txt_englishword.Text = String.Empty;
                                        txt_arabicword.Text = String.Empty;
                                    }
                                }

                            }
                        }

                    }
                }
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
                                                    {/*
                if (e.ColumnIndex == 21 )
            {
                if (!string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[21].ToString()))
          {
               // your code goes here
         
            decimal total = table.AsEnumerable().Sum(row => row.Field<decimal>("TotalPriceAfter"));
                            //  TXT_Egmali.Text = total.ToString("N2");
                             
            //    dataGridView1.FooterRow.Cells[1].Text = "Total";
            //   dataGridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
               string edara = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string codeedara="";
              if(  string.Compare(edara,TXT_Edara.Text)==0)
              {

              }
              else
              {
                  TXT_Edara.Text += edara;
                  TXT_CodeEdara.Text +=codeedara;
              }
             
            }
  
            }}*/

        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {/*
            if ((e.ColumnIndex == 21 ||e.ColumnIndex==1 || e.ColumnIndex==20) && changedflag == 1)
            {


                    // your code goes here

                    //decimal total = table.AsEnumerable().Sum(row => row.Field<decimal>("TotalPriceAfter"));
                 //   decimal total = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());

                                                        decimal sum = 0;

                                                        decimal sumDareba = 0;
                                                        decimal sumBefore = 0;
                                                                                                                                        foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!(row.Cells[e.ColumnIndex].Value == null || row.Cells[e.ColumnIndex].Value ==DBNull.Value))
                        {

                            sum = sum + Convert.ToDecimal(row.Cells[21].Value.ToString());
                            if (row.Cells[20].Value.ToString() == "")
                            {
                                sumDareba = sumDareba + 0;
                            }
                            else
                            {
                                sumDareba = sumDareba + Convert.ToDecimal(row.Cells[20].Value.ToString());

                            }
                         
                            sumBefore = sumBefore + Convert.ToDecimal(row.Cells[18].Value.ToString());

                            sumDareba = sum - sumBefore;
                            if (e.RowIndex == 0)
                            {

                             
                                    edara = edara + row.Cells[8].Value.ToString();
                                    codeedara = codeedara + row.Cells[7].Value.ToString();
                                talbtawreed = talbtawreed + row.Cells[4].Value.ToString() ;
                                bndmwazna = bndmwazna + row.Cells[9].Value.ToString() ;
                                TXT_Egmali.Text = sum.ToString("N2");
                                TXT_EgmaliDareba.Text = sumDareba.ToString("N2");
                                TXT_EgmaliBefore.Text = sumBefore.ToString("N2");
                                TXT_EgmaliAfter.Text = sum.ToString("N2");
                                TXT_Edara.Text = edara;
                                TXT_BndMwazna.Text = bndmwazna;
                                TXT_TalbNo.Text = talbtawreed;
                                TXT_CodeEdara.Text = codeedara;
                                FinancialType= CheckFinancialStatus(sum, BuyMethod, 1);
                            }
                            else if (e.RowIndex > 0)
                            {
                                if (string.Compare(TXT_TalbNo.Text, row.Cells[4].Value.ToString()) == 0)
                                {

                                }
                                else
                                {
                                    edara = edara + "-" + row.Cells[8].Value.ToString();
                                    talbtawreed = talbtawreed +"-"+ row.Cells[4].Value.ToString() ;
                                    bndmwazna = bndmwazna +"-"+ row.Cells[9].Value.ToString() ;
                                    codeedara = codeedara + "-" + row.Cells[7].Value.ToString();
                                }
                            //    edara = edara + row.Cells[8].Value.ToString() + "-";
                              //  talbtawreed = talbtawreed + row.Cells[5].Value.ToString() + "-";
                             ////   bndmwazna = bndmwazna + row.Cells[9].Value.ToString() + "-";
                                TXT_Egmali.Text = sum.ToString("N2");
                                TXT_EgmaliDareba.Text = sumDareba.ToString("N2");
                                TXT_EgmaliBefore.Text = sumBefore.ToString("N2");
                                TXT_EgmaliAfter.Text = sum.ToString("N2");
                            //    dataGridView1.Columns[21].FooterText = 3;
                                TXT_Edara.Text = edara;
                                TXT_BndMwazna.Text = bndmwazna;
                                TXT_TalbNo.Text = talbtawreed;
                                TXT_CodeEdara.Text = codeedara;
                                FinancialType = CheckFinancialStatus(sum, BuyMethod, 1);
                                try
                                {
                                    ToWord toWord = new ToWord(Convert.ToDecimal(TXT_Egmali.Text), currencies[0]);
                                    //   txt_englishword.Text = toWord.ConvertToEnglish();
                                    txt_arabicword.Text = toWord.ConvertToArabic();
                                }
                                catch (Exception ex)
                                {
                                    //   txt_englishword.Text = String.Empty;
                                    txt_arabicword.Text = String.Empty;
                                }
                            }

                        }
                    }
            }*/
        }

        private void cboCurrency_DropDownClosed(object sender, EventArgs e)
        {
            TXT_Egmali_TextChanged(null, null);
        }

    

        private void cboCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TXT_AmrNo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
           // Constants.validatenukeypress(sender, e);
        }

        private void TXT_MonksaNo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Constants.validatenumberkeypress(sender, e);
        }

        private void BTN_Print_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد طباعة تقرير امر الشراء؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_AmrNo.Text) || string.IsNullOrEmpty(Cmb_FY.Text))
                {
                    MessageBox.Show("يجب اختيار امر شراء المراد طباعتها اولا");
                    return;
                }
                else
                {

                    Constants.AmrSanaMalya = Cmb_FY.Text;
                    Constants.AmrNo = TXT_AmrNo.Text;
                    Constants.FormNo = 6;
                    FReports f = new FReports();
                    f.Show();
                }
            }

        }

        private void Cmb_AmrNo2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BTN_Print2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد طباعة تقرير امر الشراء؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(Cmb_AmrNo2.Text) || string.IsNullOrEmpty(Cmb_FY2.Text))
                {
                    MessageBox.Show("يجب اختيار امر شراء المراد طباعتها اولا");
                    return;
                }
                else
                {

                    Constants.AmrSanaMalya = Cmb_FY2.Text;
                    Constants.AmrNo =Cmb_AmrNo2.Text;
                    Constants.FormNo = 6;
                    FReports f = new FReports();
                    f.Show();
                }
            }
        }

        private void TXT_AmrNo_Leave(object sender, EventArgs e)
        {
            if (AddEditFlag == 2 && !string.IsNullOrEmpty(TXT_AmrNo.Text))
            {
                GetData(Convert.ToInt32(TXT_AmrNo.Text), Cmb_FY.Text);
            }
        }
        public int CheckFinancialStatus(decimal T, string BM, int AT)
        {
            Constants.opencon();
            string query = "exec SP_CheckFinancial @T,@BM,@AT,@F out";
            SqlCommand cmd = new SqlCommand(query,Constants.con);
            cmd.Parameters.AddWithValue("@T", T);
            cmd.Parameters.AddWithValue("@BM", BM);
            cmd.Parameters.AddWithValue("@AT", AT);
            cmd.Parameters.Add("@F", SqlDbType.Int, 32);  //-------> output parameter
            cmd.Parameters["@F"].Direction = ParameterDirection.Output;

       

            try
            {
                cmd.ExecuteNonQuery();
                executemsg = true;
                FinancialType = (int)cmd.Parameters["@F"].Value;
            }
            catch (SqlException sqlEx)
            {
                executemsg = false;
                MessageBox.Show(sqlEx.ToString());
            //  FinancialType = (int)cmd.Parameters["@F"].Value;
            }
            if (executemsg == true && FinancialType == 1)
            {
                FinancialTypeText = "مدير عام";

            }
            else  if (executemsg == true && FinancialType == 2)
            {
                FinancialTypeText = "مساعد رئيس الشركة";

            }
            else if (executemsg == true && FinancialType == 3)
            {
                FinancialTypeText = "رئيس مجلس الادارة و العضو المنتدب";

            }

            else if (executemsg == true && FinancialType == 4)
            {
                FinancialTypeText = "مجلس الادارة";

            }
        

            Constants.closecon();

            return FinancialType;

        }

        private void BTN_Sigm12_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1)
            {
                MessageBox.Show("يرجى التاكد من التوقعات السابقة اولا");
                return;
            }
            Empn2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد سالخاص بك", "توقيع التصديق", "");

            Sign2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع التصديق", "");

            if (Sign2 != "" && Empn2 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("2", "3", Sign2, Empn2);
                if (result.Item3 == 1)
                {
                    Pic_Sign2.Image = Image.FromFile(@result.Item1);

                    FlagSign2 = result.Item2;
                    FlagEmpn2 = Empn2;
                }
                else
                {
                    FlagSign2 = 0;
                    FlagEmpn2 = "";
                }
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }

        private void BTN_Sigm13_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 || FlagSign2 !=1)
            {
                MessageBox.Show("يرجى التاكد من التوقعات السابقة اولا");
                return;
            }
            Empn3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد سالخاص بك", "توقيع مدير عام مساعد", "");

            Sign3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مدير عام مساعد", "");

            if (Sign3!= "" && Empn3 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("3", "3", Sign3, Empn3);
                if (result.Item3 == 1)
                {
                    Pic_Sign3.Image = Image.FromFile(@result.Item1);

                    FlagSign3 = result.Item2;
                    FlagEmpn3 = Empn3;
                }
                else
                {
                    FlagSign3= 0;
                    FlagEmpn3= "";
                }
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }

        private void BTN_Sigm14_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 !=1)
            {
                MessageBox.Show("يرجى التاكد من التوقعات السابقة اولا");
                return;
            }
            Empn4 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد سالخاص بك", "توقيع مدير عام ", "");

            Sign4 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مدير عام ", "");

            if (Sign4 != "" && Empn4 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("4", "3", Sign4, Empn4);
                if (result.Item3 == 1)
                {
                    Pic_Sign4.Image = Image.FromFile(@result.Item1);

                    FlagSign4 = result.Item2;
                    FlagEmpn4 = Empn4;
                }
                else
                {
                    FlagSign4 = 0;
                    FlagEmpn4 = "";
                }
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }

        private void BTN_Sign6_Click(object sender, EventArgs e)
        {
            if (FlagSign2 != 1 || FlagSign1 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign5 !=1)
            {
                MessageBox.Show("يرجى التاكد من التوقعات السابقة اولا");
                return;
            }
            Empn6 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "اعتماد مدير عام الادارة الطالبة", "");

            Sign6 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "  اعتماد مدير عام الادارة الطالبة", "");

            if (Sign6 != "" && Empn6 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("6", "3", Sign6, Empn6);
                if (result.Item3 == 1)
                {
                    Pic_Sign6.Image = Image.FromFile(@result.Item1);
                    FlagSign6 = result.Item2;
                    FlagEmpn6 = Empn6;
                }
                else
                {
                    FlagSign6= 0;
                    FlagEmpn6 = "";
                }
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }
        
        public void SP_UpdateSignatures(int x, DateTime D1, DateTime? D2 = null)
        {
            string cmdstring = "Exec  SP_UpdateSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
            SqlCommand cmd = new SqlCommand(cmdstring, con);

            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_AmrNo.Text ));
            cmd.Parameters.AddWithValue("@TNO2", DBNull.Value);
            if (Cmb_FY2.Text.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text.ToString());
            }
            else
            {



                cmd.Parameters.AddWithValue("@FY", Cmb_FY2.Text.ToString());
            }
            cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
            cmd.Parameters.AddWithValue("@CE", TXT_CodeEdara.Text);
            cmd.Parameters.AddWithValue("@NE", TXT_Edara.Text);

            cmd.Parameters.AddWithValue("@FN", 3);

            cmd.Parameters.AddWithValue("@SN", x);

            cmd.Parameters.AddWithValue("@D1", D1);
            if (D2 == null)
            {
                cmd.Parameters.AddWithValue("@D2", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@D2", D2);
            }

            cmd.ExecuteNonQuery();
        }

    }
}
