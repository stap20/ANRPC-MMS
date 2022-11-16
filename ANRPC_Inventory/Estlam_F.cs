﻿using System;
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
    public partial class Estlam_F : Form
    {
        //------------------------------------------ Define Variables ---------------------------------
        #region Def Variables
        public SqlConnection con;//sql conn for anrpc_sms db
        public DataTable DT = new DataTable();
        private BindingSource bindingsource1 = new BindingSource();

        public string pp;
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
        public string ST;
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
        private string TableQuery;
        private int AddEditFlag;
        public string  SignPath1="";
        public string SignPath2="";
        public string SignPath3="";
        public string SignPath4="";

        public Boolean executemsg;
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
        public DateTime Dateold;
        public int r;
        public int rowflag = 0;


        AutoCompleteStringCollection UnitColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TalbColl = new AutoCompleteStringCollection(); //empn
        #endregion

        #region myDefVariable
        enum VALIDATION_TYPES
        {
            ADD_AMRSHERAA_BNOD,
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

            //fyear sec
            changePanelState(panel5, false);
            Cmb_FY.Enabled = true;
            CMB_Sadr.Enabled = true;

            //moward sec
            changePanelState(panel6, true);

            //bian edara sec
            changePanelState(panel10, true);
            TXT_Edara.Enabled = false;
            TXT_CodeEdara.Enabled = false;
            TXT_Egmali.Enabled = false;

            //mowazna value
            changePanelState(panel11, false);
            TXT_Payment.Enabled = true;


            //btn Section
            //generalBtn
            SaveBtn.Enabled = true;
            BTN_Cancel.Enabled = true;
            BTN_ChooseTalb.Enabled = true;
            browseBTN.Enabled = true;
            BTN_PDF.Enabled = true;

            Addbtn.Enabled = false;
            EditBtn.Enabled = false;
            BTN_Search.Enabled = false;
            BTN_Print.Enabled = false;


            //signature btn
            changePanelState(signatureTable, false);
            BTN_Sigm1.Enabled = true;

            changeDataGridViewColumnState(dataGridView1, true);

            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;

            currentSignNumber = 1;
        }

        public void PrepareEditState()
        {
            PrepareAddState();
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


            if (Constants.User_Type == "B")
            {
                if (Constants.UserTypeB == "Stock")
                {
                    if (FlagSign2 != 1 && FlagSign1 == 1)
                    {
                        BTN_Sigm12.Enabled = true;

                        Pic_Sign2.BackColor = Color.Green;
                        currentSignNumber = 2;
                    }
                    else if (FlagSign3 != 1 && FlagSign2 == 1)
                    {
                        BTN_Sigm13.Enabled = true;

                        Pic_Sign3.BackColor = Color.Green;
                        currentSignNumber = 3;
                    }
                    else if (FlagSign4 != 1 && FlagSign3 == 1)
                    {
                        BTN_Sigm14.Enabled = true;

                        Pic_Sign4.BackColor = Color.Green;
                        currentSignNumber = 4;
                    }
                }
            }

            AddEditFlag = 1;
            TNO = TXT_AmrNo.Text;
            FY = Cmb_FY.Text;
        }

        public void prepareSearchState()
        {
            DisableControls();
            Input_Reset();
            Cmb_FY.Enabled = true;
            TXT_AmrNo.Enabled = true;
            BTN_Print.Enabled = true;
        }

        public void reset()
        {
            prepareSearchState();
        }

        public void DisableControls()
        {
            //fyear
            changePanelState(panel5, false);

            //moward sec
            changePanelState(panel3, false);


            //btn Section
            //generalBtn
            Addbtn.Enabled = true;
            BTN_Search.Enabled = true;
            SaveBtn.Enabled = false;
            EditBtn.Enabled = false;
            BTN_Cancel.Enabled = false;
            DeleteBtn.Enabled = false;
            BTN_Print.Enabled = false;
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
            //amr sheraa types
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;


            //fyear sec
            TXT_AmrNo.Text = "";
            TXT_TalbNo.Text = "";
            Cmb_FY.Text = "";
            Cmb_FY.SelectedIndex = -1;

            CMB_Sadr.Text = "";
            CMB_Sadr.SelectedIndex = -1;


            //moward sec
            TXT_Name.Text = "";
            TXT_HesabMward1.Text = "";
            TXT_HesabMward2.Text = "";
            TXT_TaslemDate.Text = "";


            //bian edara sec
            TXT_Edara.Text = "";
            TXT_CodeEdara.Text = "";
            TXT_Egmali.Text = "";
            TXT_TaslemPlace.Text = "";
            TXT_Date.Value = DateTime.Today;

            //mowazna value
            TXT_Momayz.Text = "";
            TXT_BndMwazna.Text = "";
            TXT_Payment.Text = "";

            //egamle dareba
            TXT_EgmaliBefore.Text = "";
            TXT_EgmaliAfter.Text = "";
            TXT_EgmaliDareba.Text = "";
            txt_arabicword.Text = "";

            //search sec
            Cmb_FY2.Text = "";
            Cmb_FY2.SelectedIndex = -1;

            Cmb_AmrNo2.Text = "";
            Cmb_AmrNo2.SelectedIndex = -1;

            resetSignature();

            //shek sec
            TXT_ShickNo.Text = "";

            cleargridview();

            AddEditFlag = 0;
        }
        #endregion


        public Estlam_F()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void Cmb_AmrNo_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(Cmb_AmrNo);
        }

        private void TalbTawred_Load(object sender, EventArgs e)
        {
            HelperClass.comboBoxFiller(Cmb_FY, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);

            DisableControls();
            con = new SqlConnection(Constants.constring);
            Cmb_AmrNo.DrawMode = DrawMode.OwnerDrawFixed;
            Cmb_AmrNo.DrawItem += Cmb_AmrNo_DrawItem;
            Cmb_AmrNo.DropDownClosed += Cmb_AmrNo_DropDownClosed;

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
            string cmdstring = "select Amrshraa_No from   T_Awamershraa where  AmrSheraa_sanamalia='" +Cmb_FY+"'";
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
          
        
               //cmdstring = "select (Amrshraa_No) from  T_Awamershraa where Sign3 =1 and AmrSheraa_sanamalia=@FY and Sign2=1   order by  Amrshraa_No";

               /*  cmdstring = "select (Amrshraa_No) from  T_Estlam  order by  Amrshraa_No";


                  cmd = new SqlCommand(cmdstring, con);

                 //   cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
               
                    DataTable dts = new DataTable();

                    dts.Load(cmd.ExecuteReader());
                    Cmb_AmrNo.DataSource = dts;
                    Cmb_AmrNo.ValueMember = "Amrshraa_No";
                    Cmb_AmrNo.DisplayMember = "Amrshraa_No";
                    Cmb_AmrNo.SelectedIndex = -1;*/
                   // Cmb_AmrNo.SelectedIndexChanged += new EventHandler(Cmb_AmrNo_SelectedIndexChanged);


            con.Close();
        }
       
        private void Getdata(string cmd)
        {

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
  
            table.Clear();
         
            this.dataGridView1.Columns.Clear();

            dataGridView1.Refresh();
            cleargridview();
            dataGridView1.DataSource = null;
            
          //  DT.Load(cmd1.ExecuteReader());
           

           // dataGridView1.AutoGenerateColumns = false;
           
          //  dataGridView1.DataSource = DT;
            //dataGridView1.Columns.Clear();
           
            dataadapter = new SqlDataAdapter(cmd, con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataadapter.Fill(DT);
            dataGridView1.DataSource = DT;
            dataGridView1.Refresh();
            dataGridView1.Columns["Amrshraa_No"].HeaderText = "رقم أمر الشراء";//col0
            dataGridView1.Columns["Amrshraa_No"].ReadOnly = true;
            // dataGridView1.Columns["TalbTwareed_No"].Width = 60;
            dataGridView1.Columns["Monaksa_No"].HeaderText = " رقم المناقصة";//col1
            dataGridView1.Columns["Monaksa_No"].Visible =false;
            dataGridView1.Columns["monaksa_sanamalia"].HeaderText = "مناقصةسنةمالية";//col2
            dataGridView1.Columns["monaksa_sanamalia"].Visible = false;
            dataGridView1.Columns["AmrSheraa_sanamalia"].HeaderText = "امر الشراء سنةمالية";//col3
            dataGridView1.Columns["AmrSheraa_sanamalia"].ReadOnly = true;

            
            dataGridView1.Columns["TalbTwareed_No"].HeaderText = "رقم طلب التوريد";//col4
            dataGridView1.Columns["TalbTwareed_No"].ReadOnly = true;
            dataGridView1.Columns["FYear"].HeaderText = "سنة مالية طلب التوريد";//col5
            dataGridView1.Columns["FYear"].ReadOnly = true;
            dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col6
            dataGridView1.Columns["Bnd_No"].ReadOnly = true;
            dataGridView1.Columns["CodeEdara"].HeaderText = "كود ادارة";//col7
            dataGridView1.Columns["CodeEdara"].Visible = false;

            dataGridView1.Columns["NameEdara"].HeaderText = "الادارة الطالبة";//col8
            dataGridView1.Columns["NameEdara"].Visible = false
                ;

            dataGridView1.Columns["BndMwazna"].HeaderText = "بند موازنة";//col9
            dataGridView1.Columns["BndMwazna"].Visible = false;

            dataGridView1.Columns["Quan"].HeaderText = "الكمية المطلوبة";//col10

            dataGridView1.Columns["Quan"].Visible = true; ;
            dataGridView1.Columns["Quan"].ReadOnly = true;
             dataGridView1.Columns["Quan2"].HeaderText = "الكمية الكلية  الواردة ";//col11

            dataGridView1.Columns["Quan2"].Visible = true;
            dataGridView1.Columns["Quan2"].ReadOnly = false;

            dataGridView1.Columns["Unit"].HeaderText = "الوحدة";//col12

            dataGridView1.Columns["Unit"].Visible = false;


            dataGridView1.Columns["Bayan"].HeaderText = "بيان المهمات";//col13
            dataGridView1.Columns["Bayan"].Visible =true;
            dataGridView1.Columns["Bayan"].ReadOnly = true;
            dataGridView1.Columns["Makhzn"].HeaderText = "مخزن";//col14
            dataGridView1.Columns["Makhzn"].Visible = false;

            dataGridView1.Columns["Rakm_Tasnif"].HeaderText = "رقم التصنيف";//col15
            dataGridView1.Columns["Rakm_Tasnif"].Visible= false;
            dataGridView1.Columns["Rased_After"].HeaderText = "رصيد بعد";//col16
            dataGridView1.Columns["Rased_After"].Visible = false;

            dataGridView1.Columns["UnitPrice"].HeaderText = "سعر الوحدة";//col17
            dataGridView1.Columns["UnitPrice"].Visible = false;
           
           dataGridView1.Columns["TotalPrice"].HeaderText = "الثمن الاجمالى";//col18
           dataGridView1.Columns["TotalPrice"].Visible= false;

           dataGridView1.Columns["ApplyDareba"].HeaderText = "تطبق الضريبة";//col19
           dataGridView1.Columns["ApplyDareba"].Visible = false;

         //    DataColumn  dc = new DataColumn("ApplyDareba", typeof(bool));

            // dataGridView1.Columns[dc].HeaderText = "";

           dataGridView1.Columns["Darebapercent"].HeaderText = "نسبة الضريبة";//col20
           dataGridView1.Columns["Darebapercent"].Visible = false;
       //    dataGridView1.Columns["Darebapercent"].Type = DataGridViewCheckBoxCell;


           dataGridView1.Columns["TotalPriceAfter"].HeaderText = "السعر الاجمالى ";//col21
           dataGridView1.Columns["TotalPriceAfter"].Visible = false;
            

           dataGridView1.Columns["EstlamFlag"].HeaderText ="تم الاستلام ";//col22
           dataGridView1.Columns["EstlamFlag"].Visible = true;
           dataGridView1.Columns["EstlamFlag"].ReadOnly = true; ///////bzbtha auto #la hsb Quan el warda 

           dataGridView1.Columns["EstlamDate"].HeaderText = "تاريخ الاستلام ";//col23
           dataGridView1.Columns["EstlamDate"].Visible= true;

           dataGridView1.Columns["LessQuanFlag"].HeaderText = "يوجد عجز ";//col24
           dataGridView1.Columns["LessQuanFlag"].Visible = false;

           dataGridView1.Columns["NotIdenticalFlag"].HeaderText = "مطابق/غير مطابق ";//col25
           dataGridView1.Columns["NotIdenticalFlag"].Visible = false;
            /////////////////////////////////////////////////////////////////////////////////////

           dataGridView1.Columns["TalbEsdarShickNo"].HeaderText = "طلب اصدار الشيك ";//col25
           dataGridView1.Columns["TalbEsdarShickNo"].Visible = false;

           dataGridView1.Columns["ShickNo"].HeaderText = "رقم الشيك ";//col25
           dataGridView1.Columns["ShickNo"].Visible = false;

           dataGridView1.Columns["ShickDate"].HeaderText = "تاريخ الشيك ";//col25
           dataGridView1.Columns["ShickDate"].Visible = false;
            ///////////////////////////////////////////////////////////////////////////////////
            dataGridView1.AllowUserToAddRows = true;
         //   dataGridView1.DataSourceChanged       //  decimal total = table.AsEnumerable().Sum(row => row.Field<decimal>("TotalPriceAfter"));
                //    dataGridView1.FooterRow.Cells[1].Text = "Total";
                 //   dataGridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                   // TXT_Egmali.Text = total.ToString("N2");
            //cleargridview();
        }
          
        private void GetData(int x,string y)
          {

             // if (string.IsNullOrWhiteSpace(Cmb_AmrNo.Text))
              if (Cmb_AmrNo.SelectedIndex<0)
              {
                  // MessageBox.Show("ادخل رقم التصريح");
                  //  PermNo_text.Focus();
                  return;
              }
              else
              {
                  table.Clear();
                  if (AddEditFlag ==  0 ||AddEditFlag==1)
                  {

                      dataGridView1.Columns.Clear();
                      dataGridView1.DataSource = null;

                      table.Clear();

                      this.dataGridView1.Columns.Clear();

                      dataGridView1.Refresh();
                      cleargridview();
                      dataGridView1.DataSource = null;
            
                      TableQuery = "select  Amrshraa_No,AmrSheraa_sanamalia,TalbTwareed_No,FYear,Bnd_No,Quan,QuanArrived,BayanBnd,EstlamFlag,EstlamDate from T_Estlam Where  Amrshraa_No = " + x + " and AmrSheraa_sanamalia='" + y + "' and date='" + Convert.ToDateTime(TXT_Date.Value.ToShortDateString()) + "'";
                      Getdata2(TableQuery);
                  }
                //  TableQuery = "SELECT *  FROM [T_BnodAwamershraa] Where Amrshraa_No = " + x + " and AmrSheraa_sanamalia='" + y + "'";

                  if (AddEditFlag == 2)
                  {
                      dataGridView1.Columns.Clear();
                      dataGridView1.DataSource = null;

                      table.Clear();

                      this.dataGridView1.Columns.Clear();

                      dataGridView1.Refresh();
                      cleargridview();
                      dataGridView1.DataSource = null;
            
                      TableQuery = "SELECT *  FROM [T_BnodAwamershraa] Where (quan2 is null or quan2<quan) and Amrshraa_No = " + x + " and AmrSheraa_sanamalia='" + y + "'";
                      Getdata(TableQuery);
                  }
                  
                //  Getdata(TableQuery);
              }

          }

         
        private void Getdata2(string cmd)
          {
              dataGridView1.Columns.Clear();
              dataGridView1.DataSource = null;

              table.Clear();

              this.dataGridView1.Columns.Clear();

              dataGridView1.Refresh();
              cleargridview();
              dataGridView1.DataSource = null;
             
              dataadapter = new SqlDataAdapter(cmd, con);
              table.Locale = System.Globalization.CultureInfo.InvariantCulture;
              dataadapter.Fill(table);
              dataGridView1.DataSource = table;
              dataGridView1.Refresh();

              dataGridView1.Columns["Amrshraa_No"].HeaderText = "رقم أمر الشراء";//col0
              dataGridView1.Columns["Amrshraa_No"].ReadOnly = true;
              // dataGridView1.Columns["TalbTwareed_No"].Width = 60;

              dataGridView1.Columns["AmrSheraa_sanamalia"].HeaderText = "امر الشراء سنةمالية";//col1
              dataGridView1.Columns["AmrSheraa_sanamalia"].ReadOnly = true;


              dataGridView1.Columns["TalbTwareed_No"].HeaderText = "رقم طلب التوريد";//col2
              dataGridView1.Columns["TalbTwareed_No"].ReadOnly = true;
              dataGridView1.Columns["FYear"].HeaderText = "سنة مالية طلب التوريد";//col3
              dataGridView1.Columns["FYear"].ReadOnly = true;
              dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col4
              dataGridView1.Columns["Bnd_No"].ReadOnly = true;
            

             dataGridView1.Columns["Quan"].HeaderText = "الكمية المطلوبة";//col5

             dataGridView1.Columns["Quan"].Visible = true; ;
            dataGridView1.Columns["Quan"].ReadOnly = true;
              dataGridView1.Columns["QuanArrived"].HeaderText = "الكمية  الواردة ";//col6

              dataGridView1.Columns["QuanArrived"].Visible = true;
              dataGridView1.Columns["QuanArrived"].ReadOnly = false;



              dataGridView1.Columns["BayanBnd"].HeaderText = "بيان المهمات";//col7
              dataGridView1.Columns["BayanBnd"].Visible = true;
              dataGridView1.Columns["BayanBnd"].ReadOnly = true;
              


              //    DataColumn  dc = new DataColumn("ApplyDareba", typeof(bool));


              dataGridView1.Columns["EstlamFlag"].HeaderText = "تم الاستلام ";//col8
              dataGridView1.Columns["EstlamFlag"].Visible = true;
              dataGridView1.Columns["EstlamFlag"].ReadOnly = true; ///////bzbtha auto #la hsb Quan el warda 


              dataGridView1.Columns["EstlamDate"].HeaderText = "تاريخ الاستلام ";//col9
              dataGridView1.Columns["EstlamDate"].Visible = true;

              ///////////////////////////////////////////////////////////////////////////////////
              dataGridView1.AllowUserToAddRows = true;
              //  decimal total = table.AsEnumerable().Sum(row => row.Field<decimal>("TotalPriceAfter"));
              //    dataGridView1.FooterRow.Cells[1].Text = "Total";
              //   dataGridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
              // TXT_Egmali.Text = total.ToString("N2");
             // cleargridview();
          }   

        public void EnableControls()
        {
         //  BTN_ChooseTalb.Enabled = true;
          
            Cmb_AmrNo.Enabled = true;
            Cmb_FY.Enabled = true;
            TXT_Date.Enabled = true;
         //   TXT_MonksaNo.Enabled = true;
        //    Cmb_FY2.Enabled = true;
       //    CMB_Sadr.Enabled = true;
            TXT_QuanBnod.Enabled = true;
            TXT_QuanTard.Enabled = true;
           TXT_Sanf.Enabled = true;
            //TXT_TaslemPlace.Enabled = true;
            TXT_QuanBnod.Enabled = true;
            TXT_NameMward.Enabled = true;
        //    TXT_Edara.Enabled = true;
        //    TXT_TalbNo.Enabled = true;
         //   TXT_HesabMward1.Enabled = true;
          //  TXT_HesabMward2.Enabled = true;
         //   TXT_Egmali.Enabled = true;
          //  TXT_BndMwazna.Enabled = true;




            BTN_Sigm1.Enabled = true;
            BTN_Sign2.Enabled = true;
            BTN_Sign3.Enabled = true;
  
        
        }

        public void Input_Reset()
        {
            Cmb_AmrNo.SelectedIndex = -1;
            Cmb_FY.Text = "";
            TXT_Date.Text = "";
          //  TXT_MonksaNo.Text = "";
         ///   Cmb_FY2.Text= "";
          //  CMB_Sadr.Text = "";
            TXT_QuanBnod.Text = "";
            TXT_QuanTard.Text = "";
            TXT_Sanf.Text = "";
            //TXT_TaslemPlace.Text = "";
            TXT_QuanBnod.Text = "";
            TXT_NameMward.Text= "";
          //  TXT_Edara.Text = "";
           // TXT_TalbNo.Text = "";
          //  TXT_HesabMward1.Text = "";
          //  TXT_HesabMward2.Text= "";
         //   TXT_Egmali.Text="";
         //   TXT_BndMwazna.Text = "";
            Pic_Sign1.Image = null;
            Pic_Sign2.Image = null;
            Pic_Sign3.Image = null;


        }

        public void DisableControls()
        {
           // BTN_ChooseTalb.Enabled = false;

       //     TXT_AmrNo.Enabled = false;
        //    Cmb_FY.Enabled = false;
       //     TXT_Date.Enabled = false;
          //  TXT_MonksaNo.Enabled = false;
        ///    Cmb_FY2.Enabled = false;
          //  CMB_Sadr.Enabled = false;
            TXT_QuanBnod.Enabled = false;
            TXT_QuanTard.Enabled = false;
            TXT_Sanf.Enabled = false;
         ///   TXT_TaslemPlace.Enabled = false;
            TXT_QuanBnod.Enabled = false;
            TXT_NameMward.Enabled = false;
          //  TXT_Edara.Enabled = false;
          //  TXT_TalbNo.Enabled = false;
          //  TXT_HesabMward1.Enabled = false;
          //  TXT_HesabMward2.Enabled = false;
          //  TXT_Egmali.Enabled = false;
         //   TXT_BndMwazna.Enabled = false;
            BTN_Sigm1.Enabled = false;
            BTN_Sign2.Enabled = false;
            BTN_Sign3.Enabled = false;
  
        
        }

        private void cleargridview()
        {
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();


        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد طلب استلام جديد؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                //btn_print.Enabled = false;
                EnableControls();
                Input_Reset();
                cleargridview();
                AddEditFlag = 2;
                BTN_Print.Enabled =false;
                EditBtn.Enabled = false;
              //  TXT_Edara.Text = Constants.NameEdara;
              //  BTN_ChooseTalb.Enabled = true;
                SaveBtn.Visible = true;

            }
            else
            {
                //do nothing
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

            if ((MessageBox.Show("هل تريد تعديل طلب استلام ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(Cmb_AmrNo.Text) || string.IsNullOrEmpty(Cmb_FY.Text))
                {
                    MessageBox.Show("يجب اختيار امر الشراء المراد تعديله");
                    return;
                }
                else
                {
                    Addbtn.Enabled = false;
                    BTN_Print.Enabled =false;
                    AddEditFlag = 1;
                    TNO = Cmb_AmrNo.SelectedValue.ToString();
                    FY = Cmb_FY.Text;
                    Dateold = Convert.ToDateTime(TXT_Date.Value.ToShortDateString());
              //      FY2 = Cmb_FY2.Text;
             //       MNO = TXT_MonksaNo.Text;
                    SaveBtn.Visible = true;
                    var button = (Button)sender;
                    if (button.Name == "EditBtn")
                    {
                        EnableControls();
                    }
                    else if (button.Name == "EditBtn2")
                    {
                        //BTN_Sign1.Enabled = true;
                        BTN_Sign2.Enabled = true;
                        BTN_Sign3.Enabled = true;
                    }
                }

            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد حذف طلب استلام ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(Cmb_AmrNo.Text))
                {
                    MessageBox.Show("يجب اختيار امر الشراء  اولا");
                    return;
                }
                Constants.opencon();
                int flag=0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string cmdstring = "SP_DeleteEstlam @TNO,@FY,@D,@B,@TTNo,@FY2,@aot output";

                        SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                        cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(Cmb_AmrNo.SelectedValue));
                        cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text.ToString());

                        cmd.Parameters.AddWithValue("@D", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));

                        if (AddEditFlag == 2)
                        {


                            cmd.Parameters.AddWithValue("@B", Convert.ToInt32(row.Cells[6].Value));

                            cmd.Parameters.AddWithValue("@TTNo", Convert.ToInt32(row.Cells[4].Value));

                            cmd.Parameters.AddWithValue("@FY2", (row.Cells[5].Value));

                        }
                        if (AddEditFlag == 0 || AddEditFlag==1)
                        {


                            cmd.Parameters.AddWithValue("@B", Convert.ToInt32(row.Cells[4].Value));

                            cmd.Parameters.AddWithValue("@TTNo", Convert.ToInt32(row.Cells[2].Value));

                            cmd.Parameters.AddWithValue("@FY2", (row.Cells[3].Value));

                        }

                        cmd.Parameters.Add("@aot", SqlDbType.Int, 32);  //-------> output parameter
                        cmd.Parameters["@aot"].Direction = ParameterDirection.Output;

                        

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
                    }
                }
             
                if (executemsg == true )//)&& flag == 1)
                {
                    MessageBox.Show("تم الحذف بنجاح");
                    Input_Reset();
                    cleargridview();
                }
                Constants.closecon();
            }
        }

        private void Cmb_FY_SelectedIndexChanged(object sender, EventArgs e)
        {
          
                Constants.opencon();
                Cmb_AmrNo.DataSource = null;
                Cmb_AmrNo.Items.Clear();
                string cmdstring3 = "SELECT  Amrshraa_No from T_Awamershraa  where  Sign14 is not null and AmrSheraa_sanamalia='" + Cmb_FY.Text + "' order by  Amrshraa_No";
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
              

            ////////////////////////////////////////////////////////


                //call sp that get last num that eentered for this MM and this YYYY
                Constants.opencon();
                // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
               string cmdstring = "";
               SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
               if (AddEditFlag == 0)
               {


                   //       cmdstring = "select (Amrshraa_No) from  T_Awamershraa where Sign3 =1 and AmrSheraa_sanamalia=@FY and Sign2=1   order by  Amrshraa_No";

                 //  cmdstring = "select distinct(Amrshraa_No) from  T_Estlam where AmrSheraa_sanamalia=@FY order by  Amrshraa_No";
                   cmdstring = "select  (Amrshraa_No),date,AmrShraa_No +' ==> '+  Convert(nvarchar(50),Date ) as x from T_Estlam group by date,Amrshraa_No,AmrSheraa_sanamalia having AmrSheraa_sanamalia=@FY   order by Amrshraa_No ";

                  cmd = new SqlCommand(cmdstring, Constants.con);
                  cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
                  DataTable dts = new DataTable();

                  dts.Load(cmd.ExecuteReader());
                  Cmb_AmrNo.DataSource = dts;
                  Cmb_AmrNo.ValueMember = "Amrshraa_No";
                  Cmb_AmrNo.DisplayMember = "x";
                  Cmb_AmrNo.SelectedIndex = -1;

               }
               else if (AddEditFlag == 2)
               {
                    cmdstring = "select (Amrshraa_No) from  T_Awamershraa where (Sign14 is not null) and AmrSheraa_sanamalia=@FY   order by  Amrshraa_No";

                //   cmdstring = "select (Amrshraa_No) from  T_Estlam  order by  Amrshraa_No";


                  cmd = new SqlCommand(cmdstring, Constants.con);

                    cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
                    DataTable dts = new DataTable();

                    dts.Load(cmd.ExecuteReader());
                    Cmb_AmrNo.DataSource = dts;
                    Cmb_AmrNo.ValueMember = "Amrshraa_No";
                    Cmb_AmrNo.DisplayMember = "Amrshraa_No";
                    Cmb_AmrNo.SelectedIndex = -1;
               }
             

             
            ////////////////////////////////////////////////
                Constants.closecon();

            
        
        }
  
        public void SearchTalb(int x)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
          string cmdstring = "select * from T_Estlam where Amrshraa_No=@TN and AmrSheraa_sanamalia=@FY and date=@D";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
            if (x == 1)
            {

                if (Cmb_AmrNo.SelectedValue == null)
                {
                    return;
                }
                if(Cmb_AmrNo.SelectedValue ==null)
                {
                    cmd.Parameters.AddWithValue("@TN", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TN", Cmb_AmrNo.SelectedValue);
                }
              
                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
                cmd.Parameters.AddWithValue("@D", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
            }
            else
            {
              //  cmd.Parameters.AddWithValue("@TN", Cmb_AmrNo2.Text);
             //   cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            }
            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);


            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {

                    Cmb_FY.Text = dr["AmrSheraa_sanamalia"].ToString();
                //    Cmb_FY2.Text = dr["monaksa_sanamalia"].ToString();
                    Cmb_AmrNo.SelectedValue = dr["Amrshraa_No"].ToString();
                //    TXT_MonksaNo.Text = dr["Monaksa_No"].ToString();
                    TXT_NameMward.Text = dr["NameMward"].ToString();

                  //  TXT_Edara.Text = dr["NameEdara"].ToString();
                    TXT_Date.Text = dr["Date"].ToString();
                 ///   CMB_Sadr.Text = dr["Sadr_To"].ToString();
                  //  TXT_BndMwazna.Text = dr["Bnd_Mwazna"].ToString();
                    TXT_QuanBnod.Text = dr["Quan_Bnd"].ToString();
                    TXT_Sanf.Text = dr["BayanSanf"].ToString();
                    //TXT_TaslemPlace.Text = dr["Mkan_Tslem"].ToString();
                    TXT_QuanTard.Text = dr["Quan_Tard"].ToString();
               //     TXT_HesabMward1.Text = dr["Hesab_Mward"].ToString();
                 //   TXT_HesabMward2.Text = dr["Hesab_Mward"].ToString();
              ///      TXT_Egmali.Text = dr["Egmali"].ToString();

                    string s1 = dr["Sign1"].ToString();
                    string s2 = dr["Sign2"].ToString();
                    string s3 = dr["Sign3"].ToString();

                    //dr.Close();


                    if (s1 != "")
                    {
                        string p = Constants.RetrieveSignature("1", "4",s1);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename1 = p.Split(':')[1];
                            wazifa1 = p.Split(':')[2];
                            pp = p.Split(':')[0];

                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "1"]).Image = Image.FromFile(@pp);

                            FlagSign1 = 1;
                            FlagEmpn1 = s1;
                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "1"]).BackColor = Color.Green;
                            toolTip1.SetToolTip(Pic_Sign1, Ename1 + Environment.NewLine + wazifa1);
                        }

                    }
                    else
                    {
                        ((PictureBox)this.panel1.Controls["Pic_Sign" + "1"]).BackColor = Color.Red;
                    }
                    if (s2 != "")
                    {
                        string p = Constants.RetrieveSignature("2", "4",s2);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename2 = p.Split(':')[1];
                            wazifa2 = p.Split(':')[2];
                            pp = p.Split(':')[0];

                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "2"]).Image = Image.FromFile(@pp);

                            FlagSign2 = 1;
                            FlagEmpn2 = s2;
                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "2"]).BackColor = Color.Green;
                            toolTip1.SetToolTip(Pic_Sign2, Ename2 + Environment.NewLine + wazifa2);
                        }

                    }
                    else
                    {
                        ((PictureBox)this.panel1.Controls["Pic_Sign" + "2"]).BackColor = Color.Red;
                    }
                    if (s3 != "")
                    {
                        string p = Constants.RetrieveSignature("3", "4",s3);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename3 = p.Split(':')[1];
                            wazifa3 = p.Split(':')[2];
                            pp = p.Split(':')[0];

                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "3"]).Image = Image.FromFile(@pp);

                            FlagSign3 = 1;
                            FlagEmpn3 = s3;
                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "3"]).BackColor = Color.Green;
                            toolTip1.SetToolTip(Pic_Sign3, Ename3 + Environment.NewLine + wazifa3);
                        }

                    }
                    else
                    {
                        ((PictureBox)this.panel1.Controls["Pic_Sign" + "3"]).BackColor = Color.Red;
                    }
                }
                GetData(Convert.ToInt32(Cmb_AmrNo.SelectedValue), Cmb_FY.Text);
                BTN_Print.Enabled = true;


            }
            else
            {
                MessageBox.Show("من فضلك تاكد من تاريخ الاستلام و رقم امر الشراء");
                BTN_Print.Enabled = false;

            }
            dr.Close();


            //  string query1 = "SELECT  [TalbTwareed_No] ,[FYear] ,[Bnd_No],[RequestedQuan],[Unit],[BIAN_TSNIF] ,[STOCK_NO_ALL],[Quan] ,[ArrivalDate] FROM [T_TalbTawreed_Benod] where  [TalbTwareed_No]=@T and [FYear]=@F ";
            //  SqlCommand cmd1 = new SqlCommand(query1, Constants.con);
            //  cmd1.Parameters.AddWithValue("@T",Cmb_TalbNo2.Text);
            //  cmd1.Parameters.AddWithValue("@F", Cmb_FYear2.Text);


            // DT.Clear();
            // DT.Load(cmd1.ExecuteReader());
            // cleargridview();
           // GetData(Convert.ToInt32(TXT_AmrNo.Text), Cmb_FY.Text);
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

      

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (AddEditFlag == 2)
            {
                if (FlagSign1 != 1)
                {
                    MessageBox.Show("من فضلك تاكد من توقيع الاستلام");
                    return;
                }
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {

                        //  if (row.Cells[22].Value != DBNull.Value)
                        if (row.Cells[11].Value != DBNull.Value && row.Cells[11].Value != null && row.Cells[11].Value.ToString() != "")
                        {
                            //  if (Convert.ToBoolean(row.Cells[22].Value) == true)
                            //   {
                            if (row.Cells[23].Value == DBNull.Value || row.Cells[23].Value == null || row.Cells[23].Value.ToString() == "")
                            { // as long as eni estlmt ay kmya lazm a7ot tare5 el estlam bs lw goz2 msh 7a7ot mark eni estlmt el band kolo
                                MessageBox.Show("يجب ادخال تاريخ الاستلام لاى بند تم استلام كل/جزء منه");
                                return;
                            }
                        }
                    }
                }
            
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                         if (!row.IsNewRow)
                    {

                      //  if (row.Cells[22].Value != DBNull.Value)
                        if (row.Cells[11].Value != DBNull.Value && row.Cells[11].Value != null && row.Cells[11].Value.ToString() !="")
                        {
                        
                                string cmdstring = "exec SP_InsertEstlam @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p77,@p777,@p8,@p9,@p10,@p17,@p188,@p18,@p1888,@p11,@p12,@p13,@p14,@p15,@p16";

                                SqlCommand cmd = new SqlCommand(cmdstring, con);


                                cmd.Parameters.AddWithValue("@p1", (Convert.ToDateTime(TXT_Date.Value.ToShortDateString())));

                                cmd.Parameters.AddWithValue("@p2", TXT_NameMward.Text);
                                if (TXT_QuanTard.Text.ToString() == "")
                                {
                                    cmd.Parameters.AddWithValue("@p3", DBNull.Value  );
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@p3", Convert.ToDouble(TXT_QuanTard.Text));
                                }
                             //   cmd.Parameters.AddWithValue("@p3", Convert.ToDouble(TXT_QuanTard.Text) );
                                if (TXT_QuanBnod.Text.ToString() == "")
                                {
                                    cmd.Parameters.AddWithValue("@p4", DBNull.Value );
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@p4", Convert.ToDouble(TXT_QuanBnod.Text));
                                }
                               // cmd.Parameters.AddWithValue("@p4", Convert.ToDouble(TXT_QuanTard.Text) );

                                cmd.Parameters.AddWithValue("@p5", TXT_Sanf.Text);

                                // cmd.Parameters.AddWithValue("@p6", (Convert.ToDateTime(TXT_Date.Value.ToShortDateString())));

                                cmd.Parameters.AddWithValue("@p6", Convert.ToInt32(Cmb_AmrNo.SelectedValue));
                                cmd.Parameters.AddWithValue("@p7", (Cmb_FY.Text));

                                cmd.Parameters.AddWithValue("@p77", row.Cells[4].Value);
                                cmd.Parameters.AddWithValue("@p777", row.Cells[5].Value);



                                cmd.Parameters.AddWithValue("@p8", row.Cells[6].Value);


                                cmd.Parameters.AddWithValue("@p9", row.Cells[22].Value);
                                cmd.Parameters.AddWithValue("@p10", row.Cells[23].Value);

                                cmd.Parameters.AddWithValue("@p1888", row.Cells[13].Value);

                         

                                  if (row.Cells[11].Value == null || row.Cells[11].Value == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[11].Value.ToString()))
                                  {
                                              cmd.Parameters.AddWithValue("@p18",0);
                                              cmd.Parameters.AddWithValue("@p188", row.Cells[10].Value);//
                                             cmd.Parameters.AddWithValue("@p17", 0);//type goz2i koly no estlam// zero==>no estlam
                                  }
                                  else
                                  {
                                      cmd.Parameters.AddWithValue("@p188", row.Cells[10].Value);//
                                      ////////////////////////////////////////////////
                                      string st = "exec SP_GetAllQuanArrived @p1,@p2,@p3,@p4,@p5,@p6 out";
                                      SqlCommand cmd2 = new SqlCommand(st, con);

                                      cmd2.Parameters.AddWithValue("@p1", Convert.ToInt32(Cmb_AmrNo.SelectedValue));
                                      cmd2.Parameters.AddWithValue("@p2", (Cmb_FY.Text));

                                      cmd2.Parameters.AddWithValue("@p3", row.Cells[4].Value);
                                      cmd2.Parameters.AddWithValue("@p4", row.Cells[5].Value);


                                      cmd2.Parameters.AddWithValue("@p5", row.Cells[6].Value);
                                      cmd2.Parameters.Add("@p6", SqlDbType.Float, 32);  //-------> output parameter
                                      cmd2.Parameters["@p6"].Direction = ParameterDirection.Output;


                                     double sumquan = 0;
                                     double currentTotal = 0;
                                      try
                                      {
                                          cmd2.ExecuteNonQuery();
                                          executemsg = true;
                                         sumquan = (double)cmd2.Parameters["@p6"].Value;
                                      }
                                      catch (SqlException sqlEx)
                                      {
                                          executemsg = false;
                                          MessageBox.Show(sqlEx.ToString());

                                      }
                                      currentTotal = Convert.ToDouble(row.Cells[11].Value);
                                      if (sumquan == 0)
                                      {
                                          cmd.Parameters.AddWithValue("@p18", row.Cells[11].Value);//
                                      }
                                      else if(sumquan>0)
                                      {

                                          currentTotal = currentTotal - sumquan;
                                          cmd.Parameters.AddWithValue("@p18",currentTotal);//


                                      }

                                      ///////////////////////////////////////////////////////////////////////////
                                            
                                      if(String.Compare( row.Cells[11].Value.ToString(), row.Cells[10].Value.ToString())==0)
                                      {
                                           cmd.Parameters.AddWithValue("@p17", 1);//type goz2i koly no estlam// two  ====> all kmya
                                      }
                                      else   if (String.Compare(row.Cells[11].Value.ToString(), row.Cells[10].Value.ToString()) < 0)
                                      {
                                           cmd.Parameters.AddWithValue("@p17", 2);//type goz2i koly no estlam//one ==> goz2 mn kmya a2al mn el mtloba
                                      }
                                      else if (String.Compare(row.Cells[11].Value.ToString(), row.Cells[10].Value.ToString()) > 0)
                                      {
                                          cmd.Parameters.AddWithValue("@p17", 3);//type goz2i koly no estlam//one ==> aaknr  mn kmya el mtloba f talb el tawreed 
                                      }   
                                        
                                  }
                        

                                // cmd.Parameters.AddWithValue("@p11", (TXT_TaslemPlace.Text));
                                //  cmd.Parameters.AddWithValue("@p12",(TXT_Edara.Text));
                                //  cmd.Parameters.AddWithValue("@p13",(TXT_Edara.Text));
                                //  cmd.Parameters.AddWithValue("@p14", (TXT_BndMwazna.Text));
                                //  cmd.Parameters.AddWithValue("@p15",(TXT_TalbNo.Text));
                                //  cmd.Parameters.AddWithValue("@p16",(TXT_HesabMward1.Text));
                                //  cmd.Parameters.AddWithValue("@p17",Convert.ToDecimal(TXT_Egmali.Text)??DBNull.Value);
                                cmd.Parameters.AddWithValue("@p11", FlagEmpn1);
                                cmd.Parameters.AddWithValue("@p12", DBNull.Value);//taamen
                                cmd.Parameters.AddWithValue("@p13", DBNull.Value);//dman
                                cmd.Parameters.AddWithValue("@p14", DBNull.Value);//dareba





                                cmd.Parameters.AddWithValue("@p15", Constants.User_Name.ToString());
                                cmd.Parameters.AddWithValue("@p16", Convert.ToDateTime(DateTime.Now.ToShortDateString()));


                                try
                                {
                                    cmd.ExecuteNonQuery();
                                    executemsg = true;
                                    //  flag = (int)cmd.Parameters["@p34"].Value;
                                }
                                catch (SqlException sqlEx)
                                {
                                    executemsg = false;
                                    MessageBox.Show(sqlEx.ToString());
                                    //   flag = (int)cmd.Parameters["@p34"].Value;
                                }
                           // }
                        }
                    }

                    ////////////////////


                    ///////////
                }

                        if (executemsg == true)
                        {

                            for (int i = 1; i <= 3; i++)
                            {


                                string cmdstring = "Exec  SP_InsertSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
                                SqlCommand cmd = new SqlCommand(cmdstring, con);

                                cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(Cmb_AmrNo.Text));
                                cmd.Parameters.AddWithValue("@TNO2", DBNull.Value);

                                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text.ToString());
                                cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
                                cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
                                cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);

                                cmd.Parameters.AddWithValue("@FN", 4);

                                cmd.Parameters.AddWithValue("@SN", i);

                                cmd.Parameters.AddWithValue("@D1", DBNull.Value);

                                cmd.Parameters.AddWithValue("@D2", DBNull.Value);
                                cmd.ExecuteNonQuery();
                            }
                            SP_UpdateSignatures(1, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                            SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                            ///////////////////////////////////////////////////
                         //   MessageBox.Show("تم الإضافة بنجاح  ! ");

                            MessageBox.Show("تم الإضافة بنجاح  ! ");
                            EditBtn.Enabled = true;
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
                            Input_Reset();
                            cleargridview();

                        }

                        con.Close();
                    
                }
            
            else if (AddEditFlag == 1)
            {
                UpdateEstlam();
            }
        }
        
        public void UpdateEstlam()
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {

                   // if (row.Cells[22].Value != DBNull.Value)
                   // {
                      //  if (Convert.ToBoolean(row.Cells[22].Value) == true)
                    //    {
                    if (row.Cells[6].Value != DBNull.Value && row.Cells[6].Value != null && row.Cells[6].Value.ToString() != "")
                    {
                        string cmdstring = "exec SP_UpdateEstlam @ff1,@o1,@o2,@o3,@o4,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p77,@p777,@p8,@p9,@p10,@p17,@p18,@p11,@p12,@p13,@p14,@p15,@p16";

                        SqlCommand cmd = new SqlCommand(cmdstring, con);

                        cmd.Parameters.AddWithValue("@ff1", FlagSign3);
                        cmd.Parameters.AddWithValue("@o1", TXT_Date.Value.ToShortDateString() );
                        cmd.Parameters.AddWithValue("@o2", TNO);
                        cmd.Parameters.AddWithValue("@o3", FY);
                        cmd.Parameters.AddWithValue("@o4", row.Cells[4].Value);


                        cmd.Parameters.AddWithValue("@p1", (Convert.ToDateTime(TXT_Date.Value.ToShortDateString())));

                        cmd.Parameters.AddWithValue("@p2", TXT_NameMward.Text);
                        if (TXT_QuanTard.Text.ToString() == "")
                        {
                            cmd.Parameters.AddWithValue("@p3", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p3", Convert.ToDouble(TXT_QuanTard.Text));
                        }
                        //   cmd.Parameters.AddWithValue("@p3", Convert.ToDouble(TXT_QuanTard.Text) );
                        if (TXT_QuanBnod.Text.ToString() == "")
                        {
                            cmd.Parameters.AddWithValue("@p4", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p4", Convert.ToDouble(TXT_QuanBnod.Text));
                        }
                        cmd.Parameters.AddWithValue("@p5", TXT_Sanf.Text);

                        // cmd.Parameters.AddWithValue("@p6", (Convert.ToDateTime(TXT_Date.Value.ToShortDateString())));

                        cmd.Parameters.AddWithValue("@p6", Convert.ToInt32(Cmb_AmrNo.SelectedValue));
                        cmd.Parameters.AddWithValue("@p7", (Cmb_FY.Text));
                      cmd.Parameters.AddWithValue("@p77", row.Cells[2].Value);

                        cmd.Parameters.AddWithValue("@p777", row.Cells[3].Value);

                        cmd.Parameters.AddWithValue("@p8", row.Cells[4].Value);

                    /*    cmd.Parameters.AddWithValue("@p77", row.Cells[4].Value);

                        cmd.Parameters.AddWithValue("@p777", row.Cells[5].Value);

                        cmd.Parameters.AddWithValue("@p8", row.Cells[6].Value);
                        */

                       // cmd.Parameters.AddWithValue("@p9", row.Cells[22].Value);
                        cmd.Parameters.AddWithValue("@p9", row.Cells[8].Value);

                        if (row.Cells[8].Value.ToString() == "True")
                        {


                            cmd.Parameters.AddWithValue("@p10", ((row.Cells[9].Value)));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p10", DBNull.Value);
                        }


                        if (row.Cells[6].Value == null || row.Cells[6].Value == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[6].Value.ToString()))
                        {
                            cmd.Parameters.AddWithValue("@p18", 0);
                            cmd.Parameters.AddWithValue("@p17", 0);//type goz2i koly no estlam// zero==>no estlam
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p18", row.Cells[6].Value);//
                            if (String.Compare(row.Cells[6].Value.ToString(), row.Cells[5].Value.ToString()) == 0)
                            {
                                cmd.Parameters.AddWithValue("@p17", 1);//type goz2i koly no estlam// two  ====> all kmya
                            }
                            else if (String.Compare(row.Cells[6].Value.ToString(), row.Cells[5].Value.ToString()) < 0)
                            {
                                cmd.Parameters.AddWithValue("@p17", 2);//type goz2i koly no estlam//one ==> goz2 mn kmya a2al mn el mtloba
                            }
                            else if (String.Compare(row.Cells[6].Value.ToString(), row.Cells[5].Value.ToString()) > 0)
                            {
                                cmd.Parameters.AddWithValue("@p17", 3);//type goz2i koly no estlam//one ==> aaknr  mn kmya el mtloba f talb el tawreed 
                            }


                        }
                        // cmd.Parameters.AddWithValue("@p11", (TXT_TaslemPlace.Text));
                        //  cmd.Parameters.AddWithValue("@p12",(TXT_Edara.Text));
                        //  cmd.Parameters.AddWithValue("@p13",(TXT_Edara.Text));
                        //  cmd.Parameters.AddWithValue("@p14", (TXT_BndMwazna.Text));
                        //  cmd.Parameters.AddWithValue("@p15",(TXT_TalbNo.Text));
                        //  cmd.Parameters.AddWithValue("@p16",(TXT_HesabMward1.Text));
                        //  cmd.Parameters.AddWithValue("@p17",Convert.ToDecimal(TXT_Egmali.Text)??DBNull.Value);

                        if (FlagSign1 == 1)
                        {
                            cmd.Parameters.AddWithValue("@p11", FlagEmpn1);

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p11", DBNull.Value);

                        }
                        if (FlagSign2 == 1)
                        {
                            cmd.Parameters.AddWithValue("@p12", FlagEmpn2);

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p12", DBNull.Value);

                        }
                        if (FlagSign3 == 1)
                        {
                            cmd.Parameters.AddWithValue("@p13", FlagEmpn3);

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p13", DBNull.Value);

                        }


                        cmd.Parameters.AddWithValue("@p14", DBNull.Value);
                        cmd.Parameters.AddWithValue("@p15", Constants.User_Name.ToString());
                        cmd.Parameters.AddWithValue("@p16", Convert.ToDateTime(DateTime.Now.ToShortDateString()));


                        try
                        {
                            cmd.ExecuteNonQuery();
                            executemsg = true;
                            //  flag = (int)cmd.Parameters["@p34"].Value;
                        }
                        catch (SqlException sqlEx)
                        {
                            executemsg = false;
                            MessageBox.Show(sqlEx.ToString());
                            //   flag = (int)cmd.Parameters["@p34"].Value;
                        }
                    }
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

                //  SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

            }
            MessageBox.Show("تم التعديل بنجاح  ! ");
            DisableControls();
            Addbtn.Enabled = true;
            // BTN_PrintPerm.Visible = true;
            SaveBtn.Visible = false;
            AddEditFlag = 0;
            con.Close();
        }
       
        private void BTN_Sign2_Click(object sender, EventArgs e)
        {
            if ( FlagSign1 != 1)
            {
                MessageBox.Show("يرجى التاكد من التوقعات السابقة اولا");
                return;
            }
            Empn2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع مدير مخزن الاستلام", "");
           
            Sign2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مدير مخزن الاستلام", "");
           
            if (Sign2 != ""&& Empn2 !="")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("2", "4", Sign2, Empn2);
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

        private void BTN_Sigm1_Click(object sender, EventArgs e)
        {
            Empn1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع مخزن الاستلام", "");
            
            Sign1= Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مخزن الاستلام", "");
            
            if (Sign1 != "" && Empn1 !="")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("1", "4", Sign1, Empn1);
                if (result.Item3 == 1)
                {
                    Pic_Sign1.Image = Image.FromFile(@result.Item1);

                    FlagSign1 = result.Item2;
                    FlagEmpn1 = Empn1;
                }
                else
                {
                    FlagSign1= 0;
                    FlagEmpn1 = "";
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
            if (FlagSign2 != 1 || FlagSign1 != 1)
            {
                MessageBox.Show("يرجى التاكد من التوقعات السابقة اولا");
                return;
            }
           Empn3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "مدير عام مساعد مخازن", "");
          
            Sign3= Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "مدير عام مساعد مخازن", "");
          
            if (Sign3 != "" && Empn3 !="")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("3", "4", Sign3, Empn3);
                if (result.Item3 == 1)
                {
                    Pic_Sign3.Image = Image.FromFile(@result.Item1);

                    FlagSign3 = result.Item2;
                    FlagEmpn3 = Empn3;
                }
                else
                {
                    FlagSign3 = 0;
                    FlagEmpn3 = "";
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
            string cmdstring = "Exec  SP_UpdateSignDatesEstlam  @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
            SqlCommand cmd = new SqlCommand(cmdstring, con);

            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(Cmb_AmrNo.SelectedValue.ToString()));
            cmd.Parameters.AddWithValue("@TNO2", DBNull.Value);
            if (Cmb_FY.Text.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text.ToString());
            }
            else
            {



                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text.ToString());
            }
            cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
            cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
            cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);

            cmd.Parameters.AddWithValue("@FN", 4);

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


        private void BTN_Save2_Click(object sender, EventArgs e)
        {
            if (AddEditFlag == 1)
            {
                UpdateEstlam();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (AddEditFlag == 2)
            {


                if (e.RowIndex >= 0)
                {

                    if (e.ColumnIndex == 11)
                    {
                        if (String.Compare(dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString()) == 0)
                        {
                            MessageBox.Show("استلام كلى للبند");
                            dataGridView1.Rows[e.RowIndex].Cells[22].Value = "true";//تم الاستلام
                        }
                        else if (String.Compare(dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString()) < 0)
                        {
                            MessageBox.Show("استلام جزئى للبند");
                            dataGridView1.Rows[e.RowIndex].Cells[22].Value = "false";//تم الاستلام
                        }
                        else if (String.Compare(dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString()) > 0)
                        {
                            MessageBox.Show("الكمية الواردة اكبر من المطلوبة");
                            dataGridView1.Rows[e.RowIndex].Cells[22].Value = "true";//تم الاستلام
                        }
                    }
                }
                }
            if (AddEditFlag == 1)
            {


                if (e.RowIndex >= 0)
                {

                    if (e.ColumnIndex == 6)
                    {
                        if (String.Compare(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()) == 0)
                        {
                            MessageBox.Show("استلام كلى للبند");
                            dataGridView1.Rows[e.RowIndex].Cells[8].Value = "true";//تم الاستلام
                        }
                        else if (String.Compare(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()) < 0)
                        {
                            MessageBox.Show("استلام جزئى للبند");
                            dataGridView1.Rows[e.RowIndex].Cells[8].Value = "false";//تم الاستلام
                        }
                        else if (String.Compare(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()) > 0)
                        {
                            MessageBox.Show("الكمية الواردة اكبر من المطلوبة");
                            dataGridView1.Rows[e.RowIndex].Cells[8].Value = "true";//تم الاستلام
                        }
                    }
                }
            }
                /*
                if (e.ColumnIndex == 17)
                {
                    if (e.RowIndex >= 0)
                    {

                          quan = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString());

                         price = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString());
                         totalprice = ((decimal)quan * price);
                    
                        dataGridView1.Rows[e.RowIndex].Cells[18].Value =totalprice;
                          dataGridView1.Rows[e.RowIndex].Cells[21].Value =totalprice;

                    
                    }
                }

                if ( e.ColumnIndex == 19)
                {
                    if (e.RowIndex >= 0)
                    {
                        if ((dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString() == "True") && dataGridView1.Rows[e.RowIndex].Cells[19].Value!=null)
                        {
                          dareba=(Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[19].Value))/100;
                            dataGridView1.Rows[e.RowIndex].Cells[20].Value = totalprice+((decimal)dareba * totalprice);
                        }
                    }
                }
                if (e.ColumnIndex == 20)
                {
                    changedflag = 1;
                }*/

            
        }


        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
                  
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            //printaya
            if ((MessageBox.Show("هل تريد طباعة تقرير الاستلام ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                Constants.Date_E = TXT_Date.Text;
                Constants.AmrNo = Cmb_AmrNo.SelectedValue.ToString();
                Constants.AmrSanaMalya = Cmb_FY.Text;
                Constants.MwardName = TXT_NameMward.Text;
            
                Constants.No_Tard = TXT_QuanTard.Text;
                Constants.No_Bnod = TXT_QuanBnod.Text;
                Constants.Sanf = TXT_Sanf.Text;
                Constants.Date_Amr = TXT_DateEstlam.Text;
               // Constants.Sign1 =SignPath1;
              //  Constants.Sign2 = SignPath2;

              //  Constants.Sign3 = SignPath3;
              //  Constants.Sign4 = SignPath4;
                Constants.Sign1 = FlagEmpn1.ToString();
                Constants.Sign2 = FlagEmpn2.ToString();

                Constants.Sign3 = FlagEmpn3.ToString();
               // Constants.Sign4 = FlagEmpn4.ToString();


                Constants.FormNo = 2;
                FReports F = new FReports();
                F.Show();

            }

            else
            { //No
                //----
            }
        }


        private void TXT_QuanTard_KeyPress(object sender, KeyPressEventArgs e)
        {
            Constants.validatenumberkeypress(sender, e);
        }

        private void Cmb_AmrNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Constants.validatenumberkeypress(sender, e);
        }


        private void Cmb_AmrNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (AddEditFlag == 2 && Cmb_AmrNo.SelectedValue.ToString() != "")
            {

                GetData(Convert.ToInt32(Cmb_AmrNo.SelectedValue), Cmb_FY.Text);

            }
            if (AddEditFlag == 0 && Cmb_AmrNo.SelectedIndex >= 0)
            {
           
                cleargridview();
             //  string x = Cmb_AmrNo.GetItemText(Cmb_AmrNo.Items[0]);
               string x = Cmb_AmrNo.GetItemText(Cmb_AmrNo.SelectedItem);



               // string name = ((DataRowView)Cmb_AmrNo.Items[0])["x"];
               

               // string x = Cmb_AmrNo.GetItemText(Cmb_AmrNo.SelectedValue);
                string xx = x.Substring(x.Length - 10, 10);
                TXT_Date.Text = xx;
                SearchTalb(1);

                //    GetData(Convert.ToInt32(TXT_AmrNo.Text), Cmb_FY.Text);

            }
        }




        private void Cmb_AmrNo_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) { return; } // added this line thanks to Andrew's comment
            string text = Cmb_AmrNo.GetItemText(Cmb_AmrNo.Items[e.Index]);
            // string text ="xxxxx";

            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            { e.Graphics.DrawString(text, e.Font, br, e.Bounds); }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
               // GetTalbData(text);


            //    toolTip2.Show(ST, Cmb_AmrNo, e.Bounds.Right, e.Bounds.Bottom);
            }
            e.DrawFocusRectangle();
        }
    }
}
