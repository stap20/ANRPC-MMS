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
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;
using static System.Windows.Forms.LinkLabel;

namespace ANRPC_Inventory
{
    public partial class TalbTawred : Form
    {
        //------------------------------------------ Define Variables ---------------------------------
        #region Def Variables
        public int talbstatus = 0;
        public int FlagExchange = 0;
        string PDF = "";
        Image DefaulteImg;
        Image image1;
        Image image2;
        string[,] array1 = new string[4, 6];
        string Image1;
        string Image2;
        public int indeximg = 0;
        byte[] img1;
        byte[] img2;
        int picflag = 0;
        List<CurrencyInfo> currencies = new List<CurrencyInfo>();
        public SqlConnection con;//sql conn for anrpc_sms db
        public string currentcodeedara = "";
        public DataTable DT = new DataTable();
        private BindingSource bindingsource1 = new BindingSource();
        private string TableQuery;
        private int AddEditFlag;
        public int flag1;
        public Boolean executemsg;
        public double totalprice;
        public int newtasnifcount;
        public int AdditionFlag;
        public int NewTasnifFlag;
        public double AdditionQuan;

        //  private string TableQuery;
        public string stockallold;
        DataTable table = new DataTable();
        public SqlDataAdapter dataadapter;
        public DataSet ds = new DataSet();
        public int MaxFlag;

        public string RediectionName = "";
        public string redirectionDate = "";
        ///////////////////////
        List<Dictionary<string,object>> signaturesList = new List<Dictionary<string,object>>();


        public string FlagEmpn1;
        public string FlagEmpn2;
        public string FlagEmpn3;
        public string FlagEmpn4;
        public string FlagEmpn5;
        public string FlagEmpn6;
        public string FlagEmpn7;
        public string FlagEmpn8;
        public string FlagEmpn9;
        public string FlagEmpn10;
        public string FlagEmpn11;
        public string FlagEmpn12;
        public string FlagEmpn13;

        public int FlagSign1;
        public int FlagSign2;
        public int FlagSign3;
        public int FlagSign4;
        public int FlagSign5;
        public int FlagSign6;
        public int FlagSign7;
        public int FlagSign8;
        public int FlagSign9;
        public int FlagSign10;
        public int FlagSign11;
        public int FlagSign12;
        public int FlagSign13;

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
        public string wazifa12;
        public string wazifa13;

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
        public string Ename12;
        public string Ename13;

        public string pp;
        public string TNO;
        public string FY;
        public int r;
        public int rowflag = 0;
        public decimal AppValueEgp;
        public decimal AppValueOriginal;
        public double ExchangeRate;
        public string Currency = "";
        private int lastCurrencySelectedIdx = 0;
        //  public string TableQuery;

        AutoCompleteStringCollection TasnifColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TasnifNameColl = new AutoCompleteStringCollection(); //empn

        AutoCompleteStringCollection UnitColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TalbColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TalbColl2 = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection PartColl = new AutoCompleteStringCollection(); //empn

        #endregion

        //------------------------------------------ Helper ---------------------------------
        #region Helpers
        private int GetCurrentActivatedBuyMethod(Panel panel)
        {
            int current_active = -1;
            try
            {
                foreach (RadioButton radio in panel.Controls)
                {
                    if(radio.Checked == true)
                    {
                        string s = radio.Name;

                        current_active = s[s.Length - 1]-48;

                        return current_active;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return current_active;
        }
        
        private string GetActiveRegions()
        {
            string regions = "";

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    if(i == checkedListBox1.Items.Count-1)
                    {
                        regions = regions + checkedListBox1.Items[i].ToString() + ",";
                    }
                    else
                    {
                        regions = regions + checkedListBox1.Items[i].ToString();
                    }
                    
                }
            }

            return regions;
        }

        public void SP_UpdateSignatures(int x, DateTime D1, DateTime? D2 = null)
        {
            string cmdstring = "Exec  SP_UpdateSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_TalbNo.Text));
            cmd.Parameters.AddWithValue("@TNO2", DBNull.Value);

            cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
            cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
            cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);

            cmd.Parameters.AddWithValue("@FN", 1);

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

        public void SP_InsertSignatures(int signNumber,int formNumber,int talbNo,string fyear,DateTime creationDate,string codeEdara,string nameEdara)
        {
            string cmdstring = @"Exec  SP_InsertSignDates @TalbTwareed_No,@TalbTwareed_No2,@FYear,@CreationDate,@CodeEdara,
                                 @NameEdara,@FormNo,@SignatureNo,@Date1,@Date2";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TalbTwareed_No", talbNo);
            cmd.Parameters.AddWithValue("@TalbTwareed_No2", DBNull.Value);

            cmd.Parameters.AddWithValue("@FYear", fyear);
            cmd.Parameters.AddWithValue("@CreationDate", creationDate);
            cmd.Parameters.AddWithValue("@CodeEdara", codeEdara);
            cmd.Parameters.AddWithValue("@NameEdara", nameEdara);

            cmd.Parameters.AddWithValue("@FormNo", formNumber);

            cmd.Parameters.AddWithValue("@SignatureNo", signNumber);

            cmd.Parameters.AddWithValue("@Date1", DBNull.Value);

            cmd.Parameters.AddWithValue("@Date2", DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void LoopGridview()
        {
            newtasnifcount = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (row.Cells[11].Value.ToString() == "True")
                    {
                        newtasnifcount = newtasnifcount + 1;
                        NewTasnifFlag = 1;
                    }
                }
            }
        }

        private void InsertTalbTawreedBnood()
        {
            SqlCommand cmd;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string q = @"exec SP_InsertBnodTalbTawreed @TalbTwareed_No,@FYear,@Bnd_No,@RequestedQuan,
                                    @Unit,@BIAN_TSNIF,@STOCK_NO_ALL,@Quan,@ApproxValue,@AdditionStockFlag,@NewTasnifFlag";
                    cmd = new SqlCommand(q, Constants.con);
                    cmd.Parameters.AddWithValue("@TalbTwareed_No", row.Cells[0].Value);
                    cmd.Parameters.AddWithValue("@FYear", row.Cells[1].Value);
                    cmd.Parameters.AddWithValue("@Bnd_No", row.Cells[2].Value);
                    cmd.Parameters.AddWithValue("@RequestedQuan", row.Cells[3].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Unit", row.Cells[4].Value);
                    cmd.Parameters.AddWithValue("@BIAN_TSNIF", row.Cells[5].Value);
                    cmd.Parameters.AddWithValue("@STOCK_NO_ALL", row.Cells[6].Value);
                    cmd.Parameters.AddWithValue("@Quan", row.Cells[7].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ApproxValue", row.Cells[9].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AdditionStockFlag", row.Cells[10].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NewTasnifFlag", row.Cells[11].Value ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string q = "exec SP_UpdateVirtualQuan @stockall,@additionstock";
                    cmd = new SqlCommand(q, Constants.con);
                    cmd.Parameters.AddWithValue("@stockall", row.Cells[10].Value);
                    cmd.Parameters.AddWithValue("@additionstock", row.Cells[6].Value);
                }
            }

        }

        private void AddNewTasnifInDataGridView(int isNewTasnif = 0)
        {
            #region Add row to dataGridView
                r = dataGridView1.Rows.Count - 1;

                rowflag = 1;
                DataRow newRow = table.NewRow();

                // Add the row to the rows collection.
                //   table.Rows.Add(newRow);
                table.Rows.InsertAt(newRow, r);

                dataGridView1.DataSource = table;
                dataGridView1.Rows[r].Cells[4].Value = TXT_Unit.Text.ToString();
                dataGridView1.Rows[r].Cells[5].Value = TXT_StockBian.Text;
                //  dataGridView1.Rows[r].Cells[3].Value = TXT_StockBian.Text;
                dataGridView1.Rows[r].Cells[6].Value = TXT_StockNoAll.Text;
                if (string.IsNullOrWhiteSpace(Txt_Quan.Text))
                {
                    dataGridView1.Rows[r].Cells[7].Value = DBNull.Value;

                }
                else
                {
                    dataGridView1.Rows[r].Cells[7].Value = Convert.ToDouble(Txt_Quan.Text);
                }
                //////////////////////newpart///////////////////
                if (AdditionFlag == 1)
                {
                    dataGridView1.Rows[r].Cells[10].Value = Convert.ToDouble(Txt_Quan.Text);
                    dataGridView1.Rows[r].Cells[3].Value = AdditionQuan;
                }
                else
                {
                    dataGridView1.Rows[r].Cells[3].Value = Convert.ToDouble(Txt_ReqQuan.Text);
                    dataGridView1.Rows[r].Cells[10].Value = 0;
                }
                // dataGridView1.Rows[r].Cells[9].Value = CMB_ApproxValue.SelectedValue;
                if (CMB_ApproxValue.SelectedIndex == -1)
                {
                    //   dataGridView1.Rows[r].Cells[9].Value = Convert.ToString(Convert.ToDouble(CMB_ApproxValue.Text) * Convert.ToDouble(Txt_ReqQuan.Text));
                    dataGridView1.Rows[r].Cells[9].Value = Convert.ToDouble(Convert.ToDouble(CMB_ApproxValue.Text) * Convert.ToDouble(dataGridView1.Rows[r].Cells[3].Value));
                    dataGridView1.Rows[r].Cells[9].Value = Convert.ToDouble(dataGridView1.Rows[r].Cells[9].Value) * ExchangeRate;

                }
                else if (CMB_ApproxValue.SelectedIndex >= 0)
                {
                    // dataGridView1.Rows[r].Cells[9].Value = Convert.ToString(Convert.ToDouble(CMB_ApproxValue.SelectedValue) * Convert.ToDouble(Txt_ReqQuan.Text));

                    dataGridView1.Rows[r].Cells[9].Value = Convert.ToDouble(Convert.ToDouble(CMB_ApproxValue.SelectedValue) * Convert.ToDouble(dataGridView1.Rows[r].Cells[3].Value));

                }


                dataGridView1.Rows[r].Cells[11].Value = isNewTasnif;//not new tasnif

                ///////////////////////////////////////////////

                dataGridView1.Rows[r].Cells[0].Value = TXT_TalbNo.Text;
                dataGridView1.Rows[r].Cells[1].Value = Cmb_FYear.Text;

                dataGridView1.Rows[r].Cells[2].Value = r + 1;
                //  dataGridView1.Rows[r].Cells[3].Value = Txt_ReqQuan.Value;


                dataGridView1.DataSource = table;
            #endregion
        }
        #endregion

        //------------------------------------------ State Handler ---------------------------------
        #region State Handler

        private void changePanelState(Panel panel, bool state) {
            try
            {
                foreach (Control control in panel.Controls)
                {
                    control.Enabled = state;
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
            changePanelState(panel12, true);

            //dataViewre sec
            changePanelState(panel11, false);
            Txt_ReqQuan.Enabled = true;
            CMB_ApproxValue.Enabled = true;

            //fyear sec
            changePanelState(panel3, false);
            Cmb_FYear.Enabled = true;
            Cmb_Currency.SelectedIndex = 0;

            //bian edara sec
            changePanelState(panel4, true);
            TXT_Edara.Enabled = false;

            //aprrox value
            changePanelState(panel5, false);

            //ta2men 5%
            changePanelState(panel10, true);
            changePanelState(panel14, true);

            //mowazna
            changePanelState(panel6, false);

            //redirect sec
            changePanelState(panel9, false);

            //btn Section
            //generalBtn
            SaveBtn.Enabled = true;
            BTN_Cancel.Enabled = true;
            Addbtn2.Enabled = true;
            AddNewbtn.Enabled = true;
            browseBTN.Enabled = true;
            BTN_PDF.Enabled = true;
            Addbtn.Enabled = false;
            Editbtn2.Enabled = false;
            BTN_Print.Enabled = false;

            //signature btn
            changePanelState(panel13, false);
            BTN_Sign1.Enabled = true;

            //moshtrayat types
            EnableMoshtryat();

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }

        public void PrepareEditState()
        {
            PrepareAddState();
            panel3.Enabled = false;
            BTN_Print.Enabled = true;

            Pic_Sign1.Image = null;
            Pic_Sign2.Image = null;
            FlagSign1 = 0;
            FlagSign2 = 0;
            Pic_Sign1.BackColor = Color.White;
            Pic_Sign2.BackColor = Color.White;
        }

        public void PrepareConfirmState(int currentUserNumber)
        {
            DisableControls();
            
            if(currentUserNumber == 8)
            {
                //mo3taz
            }

            else if(currentUserNumber == 4 || currentUserNumber == 11)
            {
                TXT_BndMwazna.Enabled = true;
            }
            else if(currentUserNumber == 3)
            {
                DeleteBtn2.Enabled = true;
            }

            ((Button)panel13.Controls["BTN_Sign" + Convert.ToString(currentUserNumber)]).Enabled = true;
        }

        public void prepareSearchState()
        {
            DisableControls();
            Input_Reset();
            Cmb_FYear.Enabled = true;
            TXT_TalbNo.Enabled = true;
            TXT_TalbNo2.Enabled = true;
            BTN_Print.Enabled = false;
            Cmb_Currency.Enabled = false;
        }

        public void reset()
        {
            prepareSearchState();
        }

        public void DisableControls()
        {
            //Search sec
            changePanelState(panel12, false);

            //dataViewre sec
            changePanelState(panel11, false);

            //fyear sec
            changePanelState(panel3, false);
            Cmb_Currency.SelectedIndex = 0;

            //bian edara sec
            changePanelState(panel4, false);

            //aprrox value
            changePanelState(panel5, false);

            //ta2men 5%
            changePanelState(panel10, false);
            changePanelState(panel14, false);

            //mowazna
            changePanelState(panel6, false);

            //redirect sec
            changePanelState(panel9, false);

            //btn Section
            //generalBtn
            Addbtn.Enabled = true;
            SaveBtn.Enabled = false;
            BTN_Cancel.Enabled = false;
            Addbtn2.Enabled = false;
            AddNewbtn.Enabled = false;
            browseBTN.Enabled = false;
            BTN_PDF.Enabled = false;
            Editbtn2.Enabled = false;
            BTN_Print.Enabled = false;

            //signature btn
            changePanelState(panel13, false);

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            //moshtrayat types
            DisableMoshtryat();
        }

        public void EnableMoshtryat()
        {
            panel8.Enabled = true;
            changePanelState(panel8, true);
            BTN_Sign5.Enabled = true;
        }

        public void DisableMoshtryat()
        {
            panel8.Enabled = false;
            changePanelState(panel8, false);
            BTN_Sign5.Enabled = false;
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

            Pic_Sign5.Image = null;
            FlagSign5 = 0;
            Pic_Sign5.BackColor = Color.White;

            Pic_Sign6.Image = null;
            FlagSign6 = 0;
            Pic_Sign6.BackColor = Color.White;

            Pic_Sign7.Image = null;
            FlagSign7 = 0;
            Pic_Sign7.BackColor = Color.White;

            Pic_Sign8.Image = null;
            FlagSign8 = 0;
            Pic_Sign8.BackColor = Color.White;

            Pic_Sign9.Image = null;
            FlagSign9 = 0;
            Pic_Sign9.BackColor = Color.White;

            Pic_Sign11.Image = null;
            FlagSign11 = 0;
            Pic_Sign11.BackColor = Color.White;

            Pic_Sign12.Image = null;
            FlagSign12 = 0;
            Pic_Sign12.BackColor = Color.White;

            Pic_Sign13.Image = null;
            FlagSign13 = 0;
            Pic_Sign13.BackColor = Color.White;
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
            TXT_PartNo.Text = "";
            TXT_Unit.Text = "";
            CMB_ApproxValue.Text = "";
            Quan_Min.Value = 0;
            Quan_Max.Value = 0;
            checkBox1.Checked = false;
            checkBox2.Checked = false;


            //fyear sec
            Cmb_FYear.Text = "";
            TXT_TalbNo.Text = "";
            TXT_TalbNo2.Text = "";
            Cmb_Currency.SelectedIndex = 0;

            //bian edara sec
            TXT_Edara.Text = "";
            TXT_RedirectedFor.Text = "";
            TXT_Date.Value = DateTime.Today;

            //aprrox value
            TXT_AppValue.Text = "";
            TXT_ArabicValue.Text = "";
            TXT_PriceSarf.Text = "";

            //ta2men 5%      
            RadioBTN_Tammen1.Checked = false;
            RadioBTN_Taamen2.Checked = false;
            ChBTN_Tests.Checked = false;
            ChBTN_Origin.Checked = false;
            ChBTN_Analysis.Checked = false;
            checkedListBox1.ClearSelected();
            TXT_DateTaamen.Value = DateTime.Today;

            //mowazna
            TXT_BndMwazna.Text = "";

            //redirect sec
            TXT_RedirectedFor.Text = "";
            TXT_RedirectedDate.Text = "";

            resetSignature();

            //moshtrayat types
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;

            cleargridview();

            Image1 = "";
            Image2 = "";
            pictureBox2.Image = null;

            FlagExchange = 0;
            AppValueOriginal = 0;
            AppValueEgp = 0;
            ExchangeRate = 1;
            picflag = 0;
            MaxFlag = 0;
            lastCurrencySelectedIdx = 0;
            NewTasnifFlag = 0;
            newtasnifcount = 0;
            AdditionQuan = 0;
            AdditionFlag = 0;
        }
        #endregion


        //------------------------------------------ Logic Handler ---------------------------------
        #region Logic Handler
        private void AddLogic()
        {
            Constants.opencon();

            string cmdstring = @"Exec SP_InsertTalbTawreed @TalbTwareed_No,@FYear,@CreationDate,@CodeEdara,@NameEdara,
                                @RequiredFor,@ApproxAmount,@ArabicAmount,@Taamen,@BndMwazna,@Req_Signature,
                                @BuyMethod,@ExchangeRate,@CurrencyBefore,@CurrencyAfter,
                                @PDF,@NeedTestsFlag,@NeedAnalysisFlag,@OriginFlag,@Country,
                                @TaamenFlag,@TaamenDate,@LUser,@flag output";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TalbTwareed_No", Convert.ToInt32(TXT_TalbNo.Text));
            cmd.Parameters.AddWithValue("@FYear", Cmb_FYear.Text.ToString());
            cmd.Parameters.AddWithValue("@CreationDate", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
            cmd.Parameters.AddWithValue("@CodeEdara", Constants.CodeEdara);
            cmd.Parameters.AddWithValue("@NameEdara", Constants.NameEdara);
            cmd.Parameters.AddWithValue("@RequiredFor", TXT_ReqFor.Text.ToString());
            cmd.Parameters.AddWithValue("@ApproxAmount", TXT_AppValue.Text.ToString());
            cmd.Parameters.AddWithValue("@ArabicAmount", TXT_ArabicValue.Text.ToString());
            cmd.Parameters.AddWithValue("@Taamen", TXT_Tamen.Text.ToString());
            cmd.Parameters.AddWithValue("@BndMwazna", TXT_BndMwazna.Text.ToString());
            cmd.Parameters.AddWithValue("@Req_Signature", FlagEmpn1);

            int currentActiveBuyMethod = GetCurrentActivatedBuyMethod(panel8);
            
            if (currentActiveBuyMethod != -1)
            {
                cmd.Parameters.AddWithValue("@BuyMethod", currentActiveBuyMethod);
            }

            cmd.Parameters.AddWithValue("@ExchangeRate", TXT_PriceSarf.Text);
            cmd.Parameters.AddWithValue("@CurrencyBefore", Currency);
            cmd.Parameters.AddWithValue("@CurrencyAfter", Currency);

            cmd.Parameters.AddWithValue("@PDF", PDF);

            cmd.Parameters.AddWithValue("@NeedTestsFlag", ChBTN_Tests.Checked);
            cmd.Parameters.AddWithValue("@NeedAnalysisFlag", ChBTN_Analysis.Checked);
            cmd.Parameters.AddWithValue("@OriginFlag", ChBTN_Origin.Checked);

            string regions = GetActiveRegions();
            cmd.Parameters.AddWithValue("@Country", regions);
                 
            if (RadioBTN_Tammen1.Checked == true)
            {
                cmd.Parameters.AddWithValue("@TaamenFlag", RadioBTN_Tammen1.Checked);
                cmd.Parameters.AddWithValue("@TaamenDate", DBNull.Value);
            }
            else if (RadioBTN_Taamen2.Checked == true)
            {
                cmd.Parameters.AddWithValue("@TaamenFlag", RadioBTN_Taamen2.Checked);
                cmd.Parameters.AddWithValue("@TaamenDate", Convert.ToDateTime(TXT_DateTaamen.Text.ToString()));
            }

            cmd.Parameters.AddWithValue("@LUser", Constants.User_Name.ToString());
            cmd.Parameters.Add("@flag", SqlDbType.Int, 32);  //-------> output parameter
            cmd.Parameters["@flag"].Direction = ParameterDirection.Output;

            int flag;

            try
            {
                cmd.ExecuteNonQuery();
                executemsg = true;
            }
            catch (SqlException sqlEx)
            {
                executemsg = false;
                Console.WriteLine(sqlEx);
            }

            Console.WriteLine(cmd.Parameters["@flag"].Value);

            flag = (int)cmd.Parameters["@flag"].Value;

            if (executemsg == true && flag == 1)
            {
                InsertTalbTawreedBnood();

                // -------------------------------------- SIGNATURE WITH NEW LOGIC BUT NOT COMPLETED --------------------------------
                /*for (int i = 1; i <= 2; i++)
                {
                    SP_InsertSignatures(i, 1, Convert.ToInt32(TXT_TalbNo.Text), Cmb_FYear.Text.ToString(), 
                                        Convert.ToDateTime(TXT_Date.Value.ToShortDateString()), Constants.CodeEdara, 
                                       Constants.NameEdara);
                }

                SP_UpdateSignatures(1, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()));*/
                //----------------------------------------------------------------------------------------------------------------

                for (int i = 1; i <= 13; i++)
                {


                    cmdstring = "Exec  SP_InsertSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
                    cmd = new SqlCommand(cmdstring, Constants.con);

                    cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_TalbNo.Text));
                    cmd.Parameters.AddWithValue("@TNO2", DBNull.Value);

                    cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
                    cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
                    cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
                    cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);

                    cmd.Parameters.AddWithValue("@FN", 1);

                    cmd.Parameters.AddWithValue("@SN", i);

                    cmd.Parameters.AddWithValue("@D1", DBNull.Value);

                    cmd.Parameters.AddWithValue("@D2", DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
                SP_UpdateSignatures(1, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                //////////////////////////////////////////////////////////////////

                if (MaxFlag > 0)
                {
                    for (int i = 0; i < MaxFlag; i++)
                    {
                        string query = @"exec SP_InsertTMaxQuan @TalbTwareed_No,@TalbTwareed_No2,@FYear,
                                        @STOCK_NO_ALL,@Quan,@MaxQuan";
                        SqlCommand cmd1 = new SqlCommand(query, Constants.con);
                        cmd1.Parameters.AddWithValue("@TalbTwareed_No", array1[i, 0]);
                        cmd1.Parameters.AddWithValue("@TalbTwareed_No2", array1[i, 1]);
                        cmd1.Parameters.AddWithValue("@FYear", array1[i, 2]);
                        cmd1.Parameters.AddWithValue("@STOCK_NO_ALL", array1[i, 3]);
                        cmd1.Parameters.AddWithValue("@Quan", array1[i, 4]);
                        cmd1.Parameters.AddWithValue("@MaxQuan", array1[i, 5]);

                        cmd1.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("تم الإضافة بنجاح  ! ");

                reset();
            }
            else if (executemsg == true && flag == 2)
            {
                MessageBox.Show("تم إدخال رقم طلب التوريد  من قبل  ! ");
            }
            else if (executemsg == false)
            {
                MessageBox.Show("لم يتم إدخال طلب التوريد بنجاج!!");
            }
            Constants.closecon();
        }

        private void ConfirmLogic(int currentUserNumber)
        {
            if (currentUserNumber == 8)
            {
                //mo3taz
            }

            else if (currentUserNumber == 4 || currentUserNumber == 11)
            {
               
            }
            else if (currentUserNumber == 3)
            {
                
            }

            ((Button)panel13.Controls["BTN_Sign" + Convert.ToString(currentUserNumber)]).Enabled = true;
        }

        private void UpdateTalbTawreedTSignatureCycle()
        {
            if (FlagSign3 != 1 && FlagSign2 == 1)
            {
                SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                SP_UpdateSignatures(3, Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            }

            if (Constants.AuthFlag == 3 || Constants.AuthFlag == 4)//normal case
            {
                if (FlagSign2 == 1)
                {

                    SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(3, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign3 == 1)
                {

                    SP_UpdateSignatures(3, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(8, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign8 == 1)
                {

                    SP_UpdateSignatures(8, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(12, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign12 == 1)
                {

                    SP_UpdateSignatures(12, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign4 == 1)
                {

                    SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(11, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign11 == 1)
                {

                    SP_UpdateSignatures(11, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(9, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign9 == 1)
                {

                    SP_UpdateSignatures(9, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(7, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign7 == 1)
                {

                    SP_UpdateSignatures(7, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(5, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign5 == 1)
                {

                    SP_UpdateSignatures(5, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign6 == 1)
                {

                    SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    // SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
            }
            else if (Constants.AuthFlag == 1)
            {
                if (FlagSign2 == 1)
                {

                    SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(3, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign3 == 1)
                {

                    SP_UpdateSignatures(3, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(8, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign8 == 1)
                {

                    SP_UpdateSignatures(8, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(12, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign12 == 1)
                {

                    SP_UpdateSignatures(12, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign4 == 1)
                {

                    SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(11, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign11 == 1)
                {

                    SP_UpdateSignatures(11, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    //   SP_UpdateSignatures(9, Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    SP_UpdateSignatures(5, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                /*
                if (FlagSign9 == 1)
                {

                    SP_UpdateSignatures(9, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(7, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign7 == 1)
                {

                    SP_UpdateSignatures(7, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(5, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }*/
                if (FlagSign5 == 1)
                {

                    SP_UpdateSignatures(5, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign6 == 1)
                {

                    SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    // SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
            }
            else if (Constants.AuthFlag == 2)
            {
                if (FlagSign2 == 1)
                {

                    SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(3, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign3 == 1)
                {

                    SP_UpdateSignatures(3, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(8, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign8 == 1)
                {

                    SP_UpdateSignatures(8, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(12, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign12 == 1)
                {

                    SP_UpdateSignatures(12, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign4 == 1)
                {

                    SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(11, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign11 == 1)
                {

                    SP_UpdateSignatures(11, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    //   SP_UpdateSignatures(9, Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    SP_UpdateSignatures(13, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                /*
                if (FlagSign9 == 1)
                {

                    SP_UpdateSignatures(9, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(7, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }*/
                if (FlagSign13 == 1)
                {

                    SP_UpdateSignatures(13, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(5, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign5 == 1)
                {

                    SP_UpdateSignatures(5, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
                if (FlagSign6 == 1)
                {

                    SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    // SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
            }

        }
        
        private void UpdateTalbTawreedStepsAndNotification()
        {
            SqlCommand cmd,cmd1;
            int flag;

            if (FlagSign3 == 1 && FlagSign8 == 0)
            {
                string q = "exec  SP_SendNewTasnifAlarm @p1,@p2,@p3,@p4,@p5,@p6,@LU,@LD";
                Constants.opencon();

                cmd = new SqlCommand(q, Constants.con);
                cmd.Parameters.AddWithValue("@p1", TXT_TalbNo.Text);
                cmd.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                cmd.Parameters.AddWithValue("@p3", newtasnifcount);
                cmd.Parameters.AddWithValue("@p4", Constants.CodeEdara);
                cmd.Parameters.AddWithValue("@p5", TXT_Edara.Text);
                cmd.Parameters.AddWithValue("@p6", 0);
                cmd.Parameters.AddWithValue("@LU", Constants.User_Name.ToString());
                cmd.Parameters.AddWithValue("@LD", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                cmd.ExecuteNonQuery();
                Constants.closecon();
                MessageBox.Show("تم ارسال تنبيه لادارة التصنيف بنجاح");
            }
            if (FlagSign8 == 1 && Constants.UserTypeB == "NewTasnif")
            {


                LoopGridview();
                if (NewTasnifFlag == 0)
                {
                    string q = "exec  SP_deleteTasnifAlarm @p1,@p2";
                    Constants.opencon();
                    cmd = new SqlCommand(q, Constants.con);
                    cmd = new SqlCommand(q, Constants.con);
                    cmd.Parameters.AddWithValue("@p1", TXT_TalbNo.Text);
                    cmd.Parameters.AddWithValue("@p2", Cmb_FYear.Text);

                    cmd.ExecuteNonQuery();
                    Constants.closecon();

                }
            }
            if (FlagSign6 == 1 && Constants.UserTypeB == "GMInventory")
            {
                MessageBox.Show("تم الانتهاء من طلب التوريد بنجاح ");
            }
            if (FlagSign5 == 1 && Constants.UserTypeB == "Purchases")
            {



                string q = "exec  SP_UpdateTalbNo2  @p1,@p2,@p22,@p3 out";
                Constants.opencon();
                cmd = new SqlCommand(q, Constants.con);
                cmd = new SqlCommand(q, Constants.con);
                cmd.Parameters.AddWithValue("@p1", TXT_TalbNo.Text);
                cmd.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                if (radioButton1.Checked == true)
                {


                    cmd.Parameters.AddWithValue("@p22", "1");
                }
                else if (radioButton2.Checked == true)
                {


                    cmd.Parameters.AddWithValue("@p22", "2");
                }
                else if (radioButton3.Checked == true)
                {


                    cmd.Parameters.AddWithValue("@p22", "3");
                }
                else if (radioButton4.Checked == true)
                {


                    cmd.Parameters.AddWithValue("@p22", "4");
                }
                else if (radioButton5.Checked == true)
                {


                    cmd.Parameters.AddWithValue("@p22", "5");
                }
                else if (radioButton6.Checked == true)
                {


                    cmd.Parameters.AddWithValue("@p22", "6");
                }
                cmd.Parameters.Add("@p3", SqlDbType.Int, 32);  //-------> output parameter
                cmd.Parameters["@p3"].Direction = ParameterDirection.Output;

                int Talbno2;

                try
                {
                    cmd.ExecuteNonQuery();
                    executemsg = true;
                    Talbno2 = (int)cmd.Parameters["@p3"].Value;
                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    MessageBox.Show(sqlEx.ToString());
                    Talbno2 = (int)cmd.Parameters["@p3"].Value;
                }
                if (executemsg == true)
                {
                    MessageBox.Show("تم اصدار رقم نهائى لطلب التوريد بنجاح و هو " + Talbno2.ToString());
                    //Input_Reset();
                }
                Constants.closecon();




            }


            ///////////////////////////////////////////////Case t3del rakm tsnif////////////////////////////////
            if (Constants.UserTypeB == "ChangeTasnif" && FlagSign6 == 1)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (!row.IsNewRow)
                    {



                        string q = "exec SP_UpdateSTOCKNOALL @p1,@p2,@p3,@p4 ";
                        cmd = new SqlCommand(q, Constants.con);
                        cmd.Parameters.AddWithValue("@p1", row.Cells[5].Value);
                        cmd.Parameters.AddWithValue("@p2", row.Cells[0].Value);
                        cmd.Parameters.AddWithValue("@p3", row.Cells[1].Value);
                        cmd.Parameters.AddWithValue("@p4", row.Cells[2].Value ?? DBNull.Value);



                        cmd.ExecuteNonQuery();
                    }
                }
            }
            MessageBox.Show("تم التعديل بنجاح  ! ");

            if (FlagSign3 == 1)
            {
                Constants.opencon();
                string query = "exec  SP_CheckFinancialTalb @p1,@p2,@p3,@p4 out";
                cmd1 = new SqlCommand(query, Constants.con);
                cmd1.Parameters.AddWithValue("@p1", Convert.ToDecimal(TXT_AppValue.Text));
                if (radioButton1.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@p2", 1);
                }
                else if (radioButton2.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@p2", 2);
                }
                else if (radioButton3.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@p2", 3);
                }
                else if (radioButton4.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@p2", 4);
                }
                else if (radioButton5.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@p2", 5);
                }
                else if (radioButton6.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@p2", 6);
                }
                cmd1.Parameters.AddWithValue("@p3", 1);//mhaly
                cmd1.Parameters.Add("@p4", SqlDbType.Int, 32);  //-------> output parameter
                cmd1.Parameters["@p4"].Direction = ParameterDirection.Output;

                // int flag;

                try
                {
                    cmd1.ExecuteNonQuery();
                    executemsg = true;
                    flag = (int)cmd1.Parameters["@p4"].Value;
                    MessageBox.Show("flag number is" + flag);

                    //call the other procedure ///////////////////////////////\   string query = "exec  SP_CheckFinancialTalb @p1,@p2,@p3,@p4 out";

                    string query2 = "exec  SP_CheckFinancialTalb2 @p1,@p2,@p3 out";
                    SqlCommand cmd2 = new SqlCommand(query2, Constants.con);
                    //  cmd2.Parameters.AddWithValue("@p1", Constants.CodeEdara);
                    cmd2.Parameters.AddWithValue("@p1", currentcodeedara);
                    cmd2.Parameters.AddWithValue("@p2", flag);
                    cmd2.Parameters.Add("@p3", SqlDbType.Int, 32);  //-------> output parameter
                    cmd2.Parameters["@p3"].Direction = ParameterDirection.Output;

                    int flag2;
                    cmd2.ExecuteNonQuery();
                    executemsg = true;
                    flag2 = (int)cmd2.Parameters["@p3"].Value;
                    Constants.AuthFlag = flag2;
                    MessageBox.Show("flag number2 is" + flag2);
                    if (flag2 == 1)
                    {
                        //go and update flag9 and flag7 and set =1
                        MessageBox.Show("next step is mohmat");

                        string q = "exec SP_UpdateTalbTawreedAuthority  @p1,@p2,@p3";
                        SqlCommand cmd3 = new SqlCommand(q, Constants.con);
                        cmd3.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_TalbNo.Text));
                        cmd3.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd3.Parameters.AddWithValue("@p3", flag2);
                        cmd3.ExecuteNonQuery();

                    }
                    else if (flag2 == 2)
                    {
                        //change in notfication go and set flag9=1 and make flag7 for vice not for manger
                        MessageBox.Show("next step is vice");
                        string q = "exec SP_UpdateTalbTawreedAuthority  @p1,@p2,@p3";
                        SqlCommand cmd3 = new SqlCommand(q, Constants.con);
                        cmd3.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_TalbNo.Text));
                        cmd3.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd3.Parameters.AddWithValue("@p3", flag2);
                        cmd3.ExecuteNonQuery();
                    }
                    else if (flag2 == 3)
                    {
                        //notification will go normal
                        MessageBox.Show("nextstep is r2es sherka");
                        string q = "exec SP_UpdateTalbTawreedAuthority  @p1,@p2,@p3";
                        SqlCommand cmd3 = new SqlCommand(q, Constants.con);
                        cmd3.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_TalbNo.Text));
                        cmd3.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd3.Parameters.AddWithValue("@p3", flag2);
                        cmd3.ExecuteNonQuery();
                    }
                    else if (flag2 == 4)
                    {
                        //notfication will go normal
                        MessageBox.Show("next step is mgls edara");
                        string q = "exec SP_UpdateTalbTawreedAuthority  @p1,@p2,@p3";
                        SqlCommand cmd3 = new SqlCommand(q, Constants.con);
                        cmd3.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_TalbNo.Text));
                        cmd3.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd3.Parameters.AddWithValue("@p3", flag2);
                        cmd3.ExecuteNonQuery();
                    }






                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    MessageBox.Show(sqlEx.ToString());
                    flag = (int)cmd1.Parameters["@p4"].Value;
                }
                cmd1.ExecuteNonQuery();


            }

        }

        public void UpdateTalbTawreed()
        {
            Constants.opencon();
            LoopGridview();

            string cmdstring1 = @"select STOCK_NO_ALL,AdditionStockFlag,Bnd_No from T_TalbTawreed_Benod 
                                where FYear=@FY and TalbTwareed_No=@TNO";

            SqlCommand cmd1 = new SqlCommand(cmdstring1, Constants.con);
            cmd1.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_TalbNo.Text));
            cmd1.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            SqlDataReader dr = cmd1.ExecuteReader();

            //---------------------------------
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    if (dr["AdditionStockFlag"].ToString() == "")
                    {

                    }
                    else
                    {
                        string cmdstring2 = "Exec SP_UndoVirtualQuan @TNO,@FY,@BN";

                        SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);

                        cmd2.Parameters.AddWithValue("@TNO", (dr["STOCK_NO_ALL"].ToString()));
                        cmd2.Parameters.AddWithValue("@FY", Convert.ToDouble(dr["AdditionStockFlag"].ToString()));
                        cmd2.Parameters.AddWithValue("@BN", (dr["Bnd_No"].ToString()));
                        ////    cmd2.ExecuteNonQuery();
                    }

                }
            }
            dr.Close();


            /////////////////////////////////////////////
            string cmdstring = @"Exec SP_UpdateTalbTawreed @TT,@FY,@TalbTwareed_No,@TalbTwareed_No2,@FYear,@CreationDate,@CodeEdara,
                                @NameEdara,@RequiredFor,@ApproxAmount,@ArabicAmount,@Taamen,@BndMwazna,@Req_Signature,@Confirm_Sign1,
                                @Confirm_Sign2,@Stock_Sign,@Audit_Sign,@Mohmat_Sign,@CH_Sign,@Sign8,@Sign9,@Sign10,@Sign11,@Sign12,
                                @Sign13,@BuyMethod,@ExchangeRate,@CurrencyBefore,@CurrencyAfter,@PDF,@RedirectedFor,
                                @RedirectedForDate,@NeedTestsFlag,@NeedAnalysisFlag,@OriginFlag,@Country,@TaamenFlag,@TaamenDate,
                                @LUser,@LDate,@flag output";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
            cmd.Parameters.AddWithValue("@TT", TNO);
            cmd.Parameters.AddWithValue("@FY", FY);
            cmd.Parameters.AddWithValue("@TalbTwareed_No", Convert.ToInt32(TXT_TalbNo.Text));
            if (TXT_TalbNo2.Text == "")
            {
                cmd.Parameters.AddWithValue("@TalbTwareed_No2", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TalbTwareed_No2", Convert.ToInt32(TXT_TalbNo2.Text) );
            }

            cmd.Parameters.AddWithValue("@FYear", Cmb_FYear.Text.ToString());
            cmd.Parameters.AddWithValue("@CreationDate", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
            cmd.Parameters.AddWithValue("@CodeEdara", currentcodeedara);
            cmd.Parameters.AddWithValue("@NameEdara", TXT_Edara.Text);
            cmd.Parameters.AddWithValue("@RequiredFor", TXT_ReqFor.Text.ToString());
            cmd.Parameters.AddWithValue("@ApproxAmount", TXT_AppValue.Text.ToString());
            cmd.Parameters.AddWithValue("@ArabicAmount", TXT_ArabicValue.Text.ToString());
            cmd.Parameters.AddWithValue("@Taamen", TXT_Tamen.Text.ToString());
            cmd.Parameters.AddWithValue("@BndMwazna", TXT_BndMwazna.Text.ToString());

            #region signature
            if (FlagSign1 == 1)
            {
                cmd.Parameters.AddWithValue("@Req_Signature", FlagEmpn1);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Req_Signature", DBNull.Value);

            }

            if (FlagSign2 == 1)
            {
                cmd.Parameters.AddWithValue("@Confirm_Sign1", FlagEmpn2);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Confirm_Sign1", DBNull.Value);

            }

            if (FlagSign3 == 1)
            {
                cmd.Parameters.AddWithValue("@Confirm_Sign2", FlagEmpn3);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Confirm_Sign2", DBNull.Value);

            }

            if (FlagSign4 == 1)
            {
                cmd.Parameters.AddWithValue("@Stock_Sign", FlagEmpn4);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Stock_Sign", DBNull.Value);

            }

            if (FlagSign5 == 1)
            {
                cmd.Parameters.AddWithValue("@Audit_Sign", FlagEmpn5);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Audit_Sign", DBNull.Value);

            }

            if (FlagSign6 == 1)
            {
                cmd.Parameters.AddWithValue("@Mohmat_Sign", FlagEmpn6);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Mohmat_Sign", DBNull.Value);

            }

            if (FlagSign7 == 1)
            {
                cmd.Parameters.AddWithValue("@CH_Sign", FlagEmpn7);

            }
            else
            {
                cmd.Parameters.AddWithValue("@CH_Sign", DBNull.Value);

            }

            if (FlagSign8 == 1)
            {
                cmd.Parameters.AddWithValue("@Sign8", FlagEmpn8);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Sign8", DBNull.Value);

            }

            if (FlagSign9 == 1)
            {
                cmd.Parameters.AddWithValue("@Sign9", FlagEmpn9);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Sign9", DBNull.Value);

            }

            if (FlagSign10 == 1)
            {
                cmd.Parameters.AddWithValue("@Sign10", FlagEmpn10);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Sign10", DBNull.Value);

            }

            if (FlagSign11 == 1)
            {
                cmd.Parameters.AddWithValue("@Sign11", FlagEmpn11);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Sign11", DBNull.Value);

            }

            if (FlagSign12 == 1)
            {
                cmd.Parameters.AddWithValue("@Sign12", FlagEmpn12);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Sign12", DBNull.Value);

            }

            if (FlagSign13 == 1)
            {
                cmd.Parameters.AddWithValue("@Sign13", FlagEmpn13);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Sign13", DBNull.Value);

            }

            #endregion

            int currentActiveBuyMethod = GetCurrentActivatedBuyMethod(panel8);
            if (currentActiveBuyMethod != -1)
            {
                cmd.Parameters.AddWithValue("@BuyMethod", currentActiveBuyMethod);
            }

            cmd.Parameters.AddWithValue("@ExchangeRate", TXT_PriceSarf.Text);
            cmd.Parameters.AddWithValue("@CurrencyBefore", Currency);
            cmd.Parameters.AddWithValue("@CurrencyAfter", Currency);
            cmd.Parameters.AddWithValue("@PDF", PDF);
            cmd.Parameters.AddWithValue("@RedirectedFor", RediectionName);

            if (string.IsNullOrEmpty(redirectionDate))
            {
                cmd.Parameters.AddWithValue("@RedirectedForDate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RedirectedForDate", Convert.ToDateTime(redirectionDate));
            }

            cmd.Parameters.AddWithValue("@NeedTestsFlag", ChBTN_Tests.Checked);
            cmd.Parameters.AddWithValue("@NeedAnalysisFlag", ChBTN_Analysis.Checked);
            cmd.Parameters.AddWithValue("@OriginFlag", ChBTN_Origin.Checked);

            string regions = GetActiveRegions();
            cmd.Parameters.AddWithValue("@Country", regions);

            if (RadioBTN_Tammen1.Checked == true)
            {
                cmd.Parameters.AddWithValue("@TaamenFlag", RadioBTN_Tammen1.Checked);
                cmd.Parameters.AddWithValue("@TaamenDate", DBNull.Value);
            }
            else if (RadioBTN_Taamen2.Checked == true)
            {
                cmd.Parameters.AddWithValue("@TaamenFlag", RadioBTN_Tammen1.Checked);
                cmd.Parameters.AddWithValue("@TaamenDate", Convert.ToDateTime(TXT_DateTaamen.Text.ToString()));
            }
            
            cmd.Parameters.AddWithValue("@LUser", Constants.User_Name.ToString());
            cmd.Parameters.AddWithValue("@LDate", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            cmd.Parameters.Add("@flag", SqlDbType.Int, 32);  //-------> output parameter
            cmd.Parameters["@flag"].Direction = ParameterDirection.Output;

            int flag;
            try
            {
                cmd.ExecuteNonQuery();
                executemsg = true;
            }
            catch (SqlException sqlEx)
            {
                executemsg = false;
                Console.WriteLine(sqlEx);
            }

            flag = (int)cmd.Parameters["@flag"].Value;

            if (executemsg == true && flag == 1)
            {
                InsertTalbTawreedBnood();

                //////////////////////send notification
               
                UpdateTalbTawreedStepsAndNotification();
                UpdateTalbTawreedTSignatureCycle();

                DisableControls();
                Input_Reset();

                SaveBtn.Visible = false;
                Addbtn.Enabled = true;
                AddEditFlag = 0;
            }
            else if (executemsg == true && flag == 2)
            {
                MessageBox.Show("طلب التوريد المراد تعديله غير موجود !!");
            }
            Constants.closecon();
        }

        private void EditLogic()
        {
            ////////////////call sp to know status of talb/////////////////////
            //    SP_CheckFinancialTalb
            UpdateTalbTawreed();
            //   if (FlagSign11 == 1 || FlagSign11 !=1)//check anyway with every update


            if (FlagSign3 == 1)
            {
                string query = "exec  SP_CheckFinancialTalb @p1,@p2,@p3,@p4 out";
                SqlCommand cmd1 = new SqlCommand(query, Constants.con);
                cmd1.Parameters.AddWithValue("@p1", Convert.ToDecimal(TXT_AppValue.Text));
                if (radioButton1.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@p2", 1);
                }
                else if (radioButton2.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@p2", 2);
                }
                else if (radioButton3.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@p2", 3);
                }
                else if (radioButton4.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@p2", 4);
                }
                else if (radioButton5.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@p2", 5);
                }
                else if (radioButton6.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@p2", 6);
                }
                cmd1.Parameters.AddWithValue("@p3", 1);//mhaly
                cmd1.Parameters.Add("@p4", SqlDbType.Int, 32);  //-------> output parameter
                cmd1.Parameters["@p4"].Direction = ParameterDirection.Output;

                int flag;

                try
                {
                    cmd1.ExecuteNonQuery();
                    executemsg = true;
                    flag = (int)cmd1.Parameters["@p4"].Value;
                    MessageBox.Show("flag number is" + flag);

                    //call the other procedure ///////////////////////////////\   string query = "exec  SP_CheckFinancialTalb @p1,@p2,@p3,@p4 out";

                    string query2 = "exec  SP_CheckFinancialTalb2 @p1,@p2,@p3 out";
                    SqlCommand cmd2 = new SqlCommand(query2, Constants.con);
                    // cmd2.Parameters.AddWithValue("@p1", Constants.CodeEdara);
                    cmd2.Parameters.AddWithValue("@p1", currentcodeedara);
                    cmd2.Parameters.AddWithValue("@p2", flag);
                    cmd2.Parameters["@p3"].Direction = ParameterDirection.Output;

                    int flag2;
                    cmd2.ExecuteNonQuery();
                    executemsg = true;
                    flag2 = (int)cmd2.Parameters["@p3"].Value;
                    Constants.AuthFlag = flag2;
                    MessageBox.Show("flag number2 is" + flag2);
                    if (flag2 == 1)
                    {
                        //go and update flag9 and flag7 and set =1
                        MessageBox.Show("next step is mohmat");

                        string q = "exec SP_UpdateTalbTawreedAuthority  @p1,@p2,@p3";
                        SqlCommand cmd3 = new SqlCommand(q, Constants.con);
                        cmd3.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_TalbNo.Text));
                        cmd3.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd3.Parameters.AddWithValue("@p3", flag2);
                        cmd3.ExecuteNonQuery();

                    }
                    else if (flag2 == 2)
                    {
                        //change in notfication go and set flag9=1 and make flag7 for vice not for manger
                        MessageBox.Show("next step is vice");
                        string q = "exec SP_UpdateTalbTawreedAuthority  @p1,@p2,@p3";
                        SqlCommand cmd3 = new SqlCommand(q, Constants.con);
                        cmd3.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_TalbNo.Text));
                        cmd3.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd3.Parameters.AddWithValue("@p3", flag2);
                        cmd3.ExecuteNonQuery();
                    }
                    else if (flag2 == 3)
                    {
                        //notification will go normal
                        MessageBox.Show("nextstep is r2es sherka");
                        string q = "exec SP_UpdateTalbTawreedAuthority  @p1,@p2,@p3";
                        SqlCommand cmd3 = new SqlCommand(q, Constants.con);
                        cmd3.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_TalbNo.Text));
                        cmd3.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd3.Parameters.AddWithValue("@p3", flag2);
                        cmd3.ExecuteNonQuery();
                    }
                    else if (flag2 == 4)
                    {
                        //notfication will go normal
                        MessageBox.Show("next step is mgls edara");
                        string q = "exec SP_UpdateTalbTawreedAuthority  @p1,@p2,@p3";
                        SqlCommand cmd3 = new SqlCommand(q, Constants.con);
                        cmd3.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_TalbNo.Text));
                        cmd3.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd3.Parameters.AddWithValue("@p3", flag2);
                        cmd3.ExecuteNonQuery();
                    }

                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    MessageBox.Show(sqlEx.ToString());
                    flag = (int)cmd1.Parameters["@p4"].Value;
                }
                cmd1.ExecuteNonQuery();


            }

        }

        #endregion

        //------------------------------------------ Validation Handler ---------------------------------
        #region Validation Handler

        #endregion

        public TalbTawred()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        public TalbTawred(string x, string y)
        {
            InitializeComponent();
            Cmb_FYear.Text = x;
            TXT_TalbNo.Text = y;
            TXT_TalbNo2.Focus();

            ActiveControl = TXT_TalbNo2;
        }
        //======================================
        private void TalbTawred_Load(object sender, EventArgs e)
        {
            //////////////////////////load financial year into any combobox///////////////////
            alertProvider.Icon = SystemIcons.Warning;
            HelperClass.comboBoxFiller(Cmb_FYear, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
            HelperClass.comboBoxFiller(Cmb_FYear2, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);

            //Cmb_FYear2.Items.Clear();
            //Cmb_FYear2.DataSource = FinancialYearHandler.getFinancialYear();
            //Cmb_FYear2.DisplayMember = "FinancialYear";
            //Cmb_FYear2.ValueMember = "FinancialYear";
            //-----------------------------------------------------------------------------------
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Syria));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Tunisia));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Gold));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.USA));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.EUR));
            MaxFlag = 0;
            PDF = "";
            Currency = "";
            DisableMoshtryat();
            Cmb_Currency.SelectedIndex = 0;
            //   Cmb_Currency.Text = "";

            // TODO: This line of code loads data into the 'aNRPC_InventoryDataSet.T_BnodAwamershraa' table. You can move, or remove it, as needed.
            // this.t_BnodAwamershraaTableAdapter.Fill(this.aNRPC_InventoryDataSet.T_BnodAwamershraa);
            AddEditFlag = 0;
            if (Constants.talbtawred_F == false)
            {
                panel7.Visible = true;
                panel2.Visible = false;
                panel7.Dock = DockStyle.Top;
            }
            else if (Constants.talbtawred_F == true)
            {
                panel2.Visible = true;
                panel7.Visible = false;
                panel2.Dock = DockStyle.Top;
            }
            else { }
            //------------------------------------------

            Constants.opencon();

            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            //*******************************************
            // ******    AUTO COMPLETE
            //*******************************************
            if (Constants.User_Type == "A")
            {
                string cmdstring = "select STOCK_NO_ALL,Stock_NO_Nam ,PartNO,BIAN_TSNIF from T_Tsnif  where (StatusFlag in (0,1,2)) and CodeEdara=" + Constants.CodeEdara;

                // string cmdstring = "select * from T_Tsnif where STOCK_NO_G in( select STOCK_NO_G from t_groupsedarat where edaracode1=@EC or edaracode2=@EC or edaracode3=@EC or edaracode4 =@EC or edaracode5 =@EC)";

                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("EC", Constants.CodeEdara);
                SqlDataReader dr = cmd.ExecuteReader();
                //---------------------------------
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        TasnifColl.Add(dr["STOCK_NO_ALL"].ToString());
                        TasnifNameColl.Add(dr["BIAN_TSNIF"].ToString());
                        PartColl.Add(dr["PartNO"].ToString());


                    }
                }
                dr.Close();
            }


            ///////////////////////////////////////
            string cmdstring2 = "SELECT [arab_unit] ,[eng_unit] ,[cod_unit] from Tunit";
            SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);
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
            Cmb_FYear.SelectedIndex = 0;
            string cmdstring3 = "SELECT [TalbTwareed_No] from T_TalbTawreed where CodeEdara=" + Constants.CodeEdara + " and  FYear='" + Cmb_FYear.Text + "'";
            SqlCommand cmd3 = new SqlCommand(cmdstring3, Constants.con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            //---------------------------------
            if (dr3.HasRows == true)
            {
                while (dr3.Read())
                {
                    TalbColl.Add(dr3["TalbTwareed_No"].ToString());

                }
            }
            dr3.Close();
            ///////////////////
            string cmdstring4 = "SELECT [TalbTwareed_No2] from T_TalbTawreed where CodeEdara=" + Constants.CodeEdara + " and  FYear='" + Cmb_FYear.Text + "'";
            SqlCommand cmd4 = new SqlCommand(cmdstring4, Constants.con);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            //---------------------------------
            if (dr4.HasRows == true)
            {
                while (dr4.Read())
                {
                    TalbColl2.Add(dr4["TalbTwareed_No2"].ToString());

                }
            }
            dr4.Close();


            //////////////////////////
            TXT_StockNoAll.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_StockNoAll.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_StockNoAll.AutoCompleteCustomSource = TasnifColl;

            //dr3.Close();
            TXT_PartNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_PartNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_PartNo.AutoCompleteCustomSource = PartColl;

            TXT_StockName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_StockName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_StockName.AutoCompleteCustomSource = TasnifNameColl;

            TXT_TalbNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_TalbNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_TalbNo.AutoCompleteCustomSource = TalbColl;


            TXT_TalbNo2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_TalbNo2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_TalbNo2.AutoCompleteCustomSource = TalbColl2;

            if (string.IsNullOrEmpty(TXT_TalbNo.Text) == false)//for constructor case
            {
                //GetData(Convert.ToInt32(TXT_TalbNo.Text), Cmb_FYear.Text);
                cleargridview();
                SearchTalb(1);
                BTN_Print.Visible = true;

            }
            Constants.closecon();

            reset();
        }
        //===========================================================================

        public void SearchTasnif(int searchflag)
        {

            string query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where STOCK_NO_ALL = @a";

            SqlCommand cmd = new SqlCommand(query, Constants.con);
            if (searchflag == 1)
            {
                cmd.Parameters.AddWithValue("@a", (TXT_StockNoAll.Text));
            }
            else if (searchflag == 2)
            {
                query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where STOCK_NO_NAM = @a or BIAN_TSNIF = @a";
                cmd = new SqlCommand(query, Constants.con);
                // cmd.Parameters.AddWithValue("@a", (TXT_PartNo.Text));
                cmd.Parameters.AddWithValue("@a", (TXT_StockName.Text));
            }

            else if (searchflag == 3)
            {

                query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where PartNO = @a";
                cmd = new SqlCommand(query, Constants.con);
                cmd.Parameters.AddWithValue("@a", (TXT_PartNo.Text));
            }
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    TXT_StockNoAll.Text = dr["STOCK_NO_ALL"].ToString();
                    TXT_PartNo.Text = dr["PartNo"].ToString();
                    TXT_StockName.Text = dr["STOCK_NO_NAM"].ToString();
                    TXT_StockBian.Text = dr["BIAN_TSNIF"].ToString();
                    TXT_Unit.Text = dr["Unit"].ToString();
                    //  Txt_Quan.Text = dr["Quan"].ToString();
                    if (dr["SafeAmount"] == DBNull.Value || dr["SafeAmount"].ToString() == "0")
                    {
                        checkBox1.Checked = false;
                    }
                    else if (dr["SafeAmount"].ToString() == "1")
                    {


                        checkBox1.Checked = true;
                    }


                    if (dr["StrategeAmount"] == DBNull.Value || dr["StrategeAmount"].ToString() == "0")
                    {
                        checkBox2.Checked = false;
                    }
                    else if (dr["StrategeAmount"].ToString() == "1")
                    {


                        checkBox2.Checked = true;
                    }

                    if (dr["MinAmount"] == DBNull.Value)
                    {
                        Quan_Min.Value = 0;
                    }
                    else
                    {
                        Quan_Min.Text = dr["MinAmount"].ToString();
                    }

                    if (dr["MaxAmount"] == DBNull.Value)
                    {
                        Quan_Max.Value = 0;
                    }
                    else
                    {

                        Quan_Max.Text = dr["MaxAmount"].ToString();


                    }

                    Txt_Quan.Text = dr["VirtualQuan"].ToString();

                }

                pictureBox2.Image = null;
                Image1 = "";
                Image2 = "";
                picflag = 0;

                SearchImage1(TXT_StockNoAll.Text);
                SearchImage2(TXT_StockNoAll.Text);
                //    if (searchflag == 1)
                //    {

                CMB_ApproxValue.Text = "";
                query = "SELECT stock_no_all,[PRICE_UNIT] ,(PRICE_UNIT + ' '+ in_mm + '/' +in_yy) as x FROM [tr_out_1_2015_2020] where stock_no_all=@a order by in_yy desc ,in_mm desc";



                query = "SELECT stock_no_all,[PRICE_UNIT] ,(cast(price_unit as nvarchar(50)) + '     '+ in_mm + '/' +in_yy) as x FROM [tr_out_1_2015_2020] where stock_no_all=@a order by in_yy desc ,in_mm desc";

                //   query = "SELECT stock_no_all,[PRICE_UNIT]/" + Convert.ToString(ExchangeRate) + " as EX,(cast(price_unit/" + Convert.ToString(ExchangeRate) + " as nvarchar(50)) + '     '+ in_mm + '/' +in_yy) as x FROM [tr_out_1_2015_2020] where stock_no_all=@a order by in_yy desc ,in_mm desc";
                //

                //   string query = "SELECT stock_no_all,[PRICE_UNIT] , in_mm ,in_yy FROM [tr_out_1_2015_2020] where stock_no_all=@a order by in_yy desc ,in_mm desc";
                SqlCommand cmd4 = new SqlCommand(query, Constants.con);
                cmd4.Parameters.AddWithValue("@a", TXT_StockNoAll.Text);
                //      }




                DataTable dts = new DataTable();
                dts.Load(cmd4.ExecuteReader());

                CMB_ApproxValue.DataSource = dts;


                CMB_ApproxValue.ValueMember = "PRICE_UNIT";
                // CMB_ApproxValue.ValueMember = "EX";


                CMB_ApproxValue.DisplayMember = "x";
                CMB_ApproxValue.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("من فضلك تاكد من التصنيف");

            }
            dr.Close();

        }

        private void cleargridview()
        {
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

        }

        private void TXT_StockName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {
                Constants.opencon();
                CMB_ApproxValue.Text = "";
                SearchTasnif(2);
                //  string query = "select  A.Amrshraa_No,A.AmrSheraa_sanamalia,Rakm_Tasnif,UnitPrice ,A.Date_amrshraa from T_BnodAwamershraa BA inner join T_Awamershraa  A on A.Amrshraa_No=Ba.Amrshraa_No and A.AmrSheraa_sanamalia=BA.AmrSheraa_sanamalia where Rakm_Tasnif=@a order by Date_amrshraa ";
                /*    string query = "SELECT stock_no_all,[PRICE_UNIT] ,in_mm ,in_dd,in_yy FROM [tr_out_1_2015_2020] where stock_no_all=@a order by in_yy desc ,in_mm desc";
                    SqlCommand cmd4 = new SqlCommand(query, Constants.con);
                    cmd4.Parameters.AddWithValue("@a", TXT_StockNoAll.Text);
                    DataTable dts = new DataTable();
                    dts.Load(cmd4.ExecuteReader());

                    CMB_ApproxValue.DataSource = dts;
                    CMB_ApproxValue.ValueMember = "PRICE_UNIT";
                   // CMB_ApproxValue.DisplayMember = "PRICE_UNIT" + "   " + "in_mm"+"/"+"in_yy";
                 * 
                    CMB_ApproxValue.DisplayMember = "x";*/
            }
        }
        public void SearchImage2(string stockall)
        {
            // string partialName = "webapi";

            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(Constants.warehouse_app_machine_directory);
            FileSystemInfo[] filesAndDirs = hdDirectoryInWhichToSearch.GetFileSystemInfos("*" + stockall + "*");

            foreach (FileSystemInfo foundFile in filesAndDirs)
            {


                string I2 = foundFile.FullName;
                Console.WriteLine(I2);

                if (I2 != Image1)
                {
                    Image2 = foundFile.FullName;
                    //  pictureBox3.Image = Image.FromFile(@Image2);
                    picflag = 2;
                }
            }
        }
        public void SearchImage1(string stockall)
        {
            // string partialName = "webapi";

            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(Constants.warehouse_app_machine_directory);
            FileSystemInfo[] filesAndDirs = hdDirectoryInWhichToSearch.GetFileSystemInfos("*" + stockall + "*");

            foreach (FileSystemInfo foundFile in filesAndDirs)
            {
                Image1 = foundFile.FullName;
                Console.WriteLine(Image1);
                pictureBox2.Image = Image.FromFile(@Image1);
                picflag = 1;
            }
        }
        private void TXT_StockNoAll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {
                Constants.opencon();
                CMB_ApproxValue.Text = "";
                SearchTasnif(1);
                /*
                 string query = "select  A.Amrshraa_No,A.AmrSheraa_sanamalia,Rakm_Tasnif,UnitPrice ,A.Date_amrshraa from T_BnodAwamershraa BA inner join T_Awamershraa  A on A.Amrshraa_No=Ba.Amrshraa_No and A.AmrSheraa_sanamalia=BA.AmrSheraa_sanamalia where Rakm_Tasnif=@a order by Date_amrshraa ";
                 SqlCommand cmd4 = new SqlCommand(query, Constants.con);
                 cmd4.Parameters.AddWithValue("@a", TXT_StockNoAll.Text);
                 DataTable dts = new DataTable();
                 dts.Load(cmd4.ExecuteReader());

                 CMB_ApproxValue.DataSource = dts;
                 CMB_ApproxValue.ValueMember = "UnitPrice";
                 CMB_ApproxValue.DisplayMember = "UnitPrice" + "   " + "Date_amrshraa";/*/

                // CMB_ApproxValue.Text = "";
                //SearchTasnif(2);
                //   string query = "select  A.Amrshraa_No,A.AmrSheraa_sanamalia,Rakm_Tasnif,UnitPrice ,A.Date_amrshraa from T_BnodAwamershraa BA inner join T_Awamershraa  A on A.Amrshraa_No=Ba.Amrshraa_No and A.AmrSheraa_sanamalia=BA.AmrSheraa_sanamalia where Rakm_Tasnif=@a order by Date_amrshraa ";


            }
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {

            if ((MessageBox.Show("هل تريد اضافة طلب توريد جديد؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                reset();
                PrepareAddState();
                AddEditFlag = 2;
                TXT_Edara.Text = Constants.NameEdara;
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            AddEditFlag = 1;
        }




        private void errorProviderHandler(List<(ErrorProvider, Control, string)> errosList)
        {
            alertProvider.Clear();
            errorProvider.Clear();
            foreach (var error in errosList)
            {
                ////Txt_ReqQuan.Location = new Point(Txt_ReqQuan.Location.X + errorProvider.Icon.Width, Txt_ReqQuan.Location.Y);
                //error.Item2.Width = error.Item2.Width - error.Item1.Icon.Width;
                error.Item1.SetError(error.Item2, error.Item3);
            }
        }
        private bool isNumber(string s)
        {
            int t;
            decimal f;

            if (!(int.TryParse(s, out t) || decimal.TryParse(s, out f)))
            {
                return false;
            }

            return true;
        }

        #region AddTasnif
            private List<(ErrorProvider, Control, string)> ValidateAddTasnif(bool isNewTasnif = false)
            {
                List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

                if (AddEditFlag != 2 && AddEditFlag != 1)//not in add mode
                {
                    // MessageBox.Show("يجب اضافة/تعديل طلب التوريد اولا");
                    // return;
                   // isValid = false;
                }

                if (!isNewTasnif)
                {
                    #region TXT_StockNoAll
                        if (string.IsNullOrWhiteSpace(TXT_StockNoAll.Text))
                        {
                            errorsList.Add((errorProvider, TXT_StockNoAll, "يجب اختيار التصنيف المراد اضافته"));
                        }
                        else if (TXT_StockNoAll.Text.Length != 8)
                        {
                            errorsList.Add((alertProvider, TXT_StockNoAll, "رقم التصنيف يجب ان يتكون من 8"));
                        }
                        else if (Txt_Quan.Text == "")
                        {
                            errorsList.Add((alertProvider, TXT_StockNoAll, "هذا التصنيف غير موجود"));
                        }
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                if (row.Cells[6].Value.ToString().ToLower() == TXT_StockNoAll.Text.ToLower() && TXT_StockNoAll.Text != "")
                                {
                                    errorsList.Add((alertProvider, TXT_StockNoAll, "تم ادخال رقم هذا التصنيف من قبل"));

                                    break;
                                }
                            }
                        }
                    #endregion
                }

                #region Txt_ReqQuan
            if (string.IsNullOrWhiteSpace(Txt_ReqQuan.Text))
                    {
                        errorsList.Add((errorProvider, Txt_ReqQuan, "يجب ادخال الكمية المطلوبة"));
                    }
                    else if (!isNumber(Txt_ReqQuan.Text))
                    {
                        errorsList.Add((alertProvider, Txt_ReqQuan, "يجب ان تكون الكمية مكونة من ارقام فقط"));
                    }
                    else if (!string.IsNullOrWhiteSpace(Txt_Quan.Text) && Txt_Quan.Text != "" && Convert.ToDouble(Txt_Quan.Text) > 0 && Convert.ToDouble(Txt_Quan.Text) >= Convert.ToDouble(Txt_ReqQuan.Text))
                    {
                        errorsList.Add((alertProvider, Txt_ReqQuan, "الكمية المطلوبة متاحة فى المخزن يمكنك انشاء اذن صرف بها"));
                    }
                    else if (!string.IsNullOrWhiteSpace(Txt_Quan.Text) && Convert.ToDecimal(Txt_Quan.Text) <= 0)
                    {
                        errorsList.Add((alertProvider, Txt_Quan, "يجب ان تكون الكمية اكبر من صفر"));
                    }
                #endregion

                #region CMB_ApproxValue
                if (string.IsNullOrWhiteSpace(CMB_ApproxValue.Text))
                    {
                        errorsList.Add((errorProvider, CMB_ApproxValue, "يجب اختيار القيمة التقديرية "));
                    }
                    else if (!isNumber(CMB_ApproxValue.Text))
                    {
                        errorsList.Add((alertProvider, CMB_ApproxValue, "يجب ان تكون القيمة التقديرية مكونة من ارقام فقط"));
                    }
                    else if(Convert.ToDecimal(CMB_ApproxValue.Text) <= 0)
                    {
                        errorsList.Add((alertProvider, CMB_ApproxValue, "يجب ان تكون القيمة التقديرية اكبر من صفر"));
                    }
                #endregion

                #region Cmb_FYear
                    if (string.IsNullOrWhiteSpace(Cmb_FYear.Text) || Cmb_FYear.SelectedIndex == -1)
                    {
                        errorsList.Add((errorProvider, Cmb_FYear, "تاكد من  اختيار السنة المالية"));
                    }
                #endregion

                if (isNewTasnif)
                {
                    #region Description
                    if (string.IsNullOrWhiteSpace(TXT_StockBian.Text))
                    {
                        errorsList.Add((errorProvider, TXT_StockBian, "يجب ادخال مواصفة للتصنيف الجديد"));
                    }
                    #endregion
                }

                return errorsList;
            }
            private void Addbtn2_Click(object sender, EventArgs e)
            {
            
                List<(ErrorProvider,Control, string)> errorsList= ValidateAddTasnif();
                if (errorsList.Count > 0)
                {
                    errorProviderHandler(errorsList);
                    return;
                }

                Currency = Cmb_Currency.Text;
            
                string stocknoall = TXT_StockNoAll.Text;

                if (checkBox1.Checked == true || checkBox2.Checked == true)
                {
                    if ((Convert.ToDouble(Txt_ReqQuan.Text)) > Convert.ToDouble(Quan_Max.Value))
                    {
                        MessageBox.Show("بعد توريد الكمية المطلوبة الكمية المتاحة ستكون اكثر من الحد الاقصى ");
                        MaxFlag = MaxFlag + 1;

                        //  return;
                        array1[MaxFlag - 1, 3] = TXT_StockNoAll.Text;
                        array1[MaxFlag - 1, 0] = TXT_TalbNo.Text;
                        array1[MaxFlag - 1, 1] = TXT_TalbNo.Text;

                        array1[MaxFlag - 1, 2] = Cmb_FYear.Text;
                        array1[MaxFlag - 1, 4] = Txt_ReqQuan.Text;
                        array1[MaxFlag - 1, 5] = Quan_Max.Text;

                    }

                    if (Convert.ToDouble(Txt_Quan.Text) < Convert.ToDouble(Quan_Min.Value)) //case lw el availble less than min
                    {
                        if (Convert.ToDouble(Txt_ReqQuan.Text) < Convert.ToDouble(Quan_Min.Value))
                        {
                            MessageBox.Show("بعد توريد الكمية المطلوبة الكمية المتاحة ستكون اقل  من الحد الادنى ");
                        }

                        MaxFlag = MaxFlag + 1;

                        //  return;
                        array1[MaxFlag - 1, 3] = TXT_StockNoAll.Text;
                        array1[MaxFlag - 1, 0] = TXT_TalbNo.Text;
                        array1[MaxFlag - 1, 1] = TXT_TalbNo.Text;

                        array1[MaxFlag - 1, 2] = Cmb_FYear.Text;
                        array1[MaxFlag - 1, 4] = Txt_ReqQuan.Text;
                        array1[MaxFlag - 1, 5] = Quan_Max.Text;

                    }
                }

                if (Convert.ToDouble(Txt_Quan.Text) > 0 && Convert.ToDouble(Txt_Quan.Text) < Convert.ToDouble(Txt_ReqQuan.Text))
                {
                    MessageBox.Show("طلب توريد بالاضافة الى رصيد");
                    AdditionFlag = 1;
                    AdditionQuan = Convert.ToDouble(Txt_ReqQuan.Text) - Convert.ToDouble(Txt_Quan.Text);
                }
                else
                {
                    AdditionFlag = 0;
                }

                AddNewTasnifInDataGridView();
            }

        private void AddNewbtn_Click(object sender, EventArgs e)
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();
            if (CHK_NewTasnif.Checked == false)
            {
                //MessageBox.Show("برجاء اختيار إضافة تصنيف جديد أولاً");
                errorsList.Add((errorProvider, CHK_NewTasnif, "برجاء اختيار إضافة تصنيف جديد أولاً"));
                errorProviderHandler(errorsList);
                return;
            }

            errorsList = ValidateAddTasnif(true);
            if (errorsList.Count > 0)
            {
                errorProviderHandler(errorsList);
                return;
            }
            
            Currency = Cmb_Currency.Text;
            NewTasnifFlag = 1;

            AddNewTasnifInDataGridView(NewTasnifFlag);

            CHK_NewTasnif.Checked = false;
        }

        #endregion



        private void Cmb_FYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AddEditFlag == 0)
            {
                Constants.opencon();

                TXT_TalbNo.AutoCompleteMode = AutoCompleteMode.None;
                TXT_TalbNo.AutoCompleteSource = AutoCompleteSource.None; ;
                string cmdstring3 = "";
                if (Constants.User_Type == "A")
                {
                    cmdstring3 = "SELECT [TalbTwareed_No] from T_TalbTawreed where CodeEdara=" + Constants.CodeEdara + " and  FYear='" + Cmb_FYear.Text + "'";

                }
                else
                {
                    cmdstring3 = "SELECT [TalbTwareed_No] from T_TalbTawreed where  FYear='" + Cmb_FYear.Text + "'";

                }
                SqlCommand cmd3 = new SqlCommand(cmdstring3, Constants.con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                //---------------------------------
                if (dr3.HasRows == true)
                {
                    while (dr3.Read())
                    {
                        TalbColl.Add(dr3["TalbTwareed_No"].ToString());

                    }
                }

                TXT_TalbNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                TXT_TalbNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                TXT_TalbNo.AutoCompleteCustomSource = TalbColl;
                //   Constants.closecon();

            }
            //go and get talbTawreed_no for this FYear
            if (AddEditFlag == 2)//add
            {
                //call sp that get last num that eentered for this MM and this YYYY
                Constants.opencon();

                // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
                //  string cmdstring = "select max(TalbTwareed_No) from  T_TalbTawreed where FYear=@FY ";
                string cmdstring = "select ( COALESCE(MAX(Talbtwareed_No), 0)) from  T_TalbTawreed where FYear=@FY ";

                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text);

                int flag;

                try
                {
Constants.opencon();
                    // cmd.ExecuteNonQuery();
                    var count = cmd.ExecuteScalar();
                    executemsg = true;
                    //  if (cmd.Parameters["@Num"].Value != null && cmd.Parameters["@Num"].Value != DBNull.Value)
                    if (count != null && count != DBNull.Value)
                    {
                        //  flag = (int)cmd.Parameters["@Num"].Value;

                        flag = (int)count;
                        flag = flag + 1;

                        ///////////////////////////////
                        string cmdstring2 = "select ( COALESCE(MAX(Talbtwareed_No), 0)) from  T_TempTalbNo where FYear=@FY ";

                        SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);

                        // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
                        cmd2.Parameters.AddWithValue("@FY", Cmb_FYear.Text);

                        //cmd2.Parameters.AddWithValue("@T",flag);
                        //-----------------------------------
                        var count2 = cmd2.ExecuteScalar();
                        executemsg = true;
                        //  if (cmd.Parameters["@Num"].Value != null && cmd.Parameters["@Num"].Value != DBNull.Value)
                        if (count2 != null && count2 != DBNull.Value)
                        {
                            //  flag = (int)cmd.Parameters["@Num"].Value;
                            //if((int)count2>0)
                            //{
                            //    flag = (int)count2 + 1;
                            //}
                            //flag = (int)count2 + 1;
                            if (flag <= (int)count2)
                            {
                                flag = (int)count2 + 1;
                            }
                        }

                        /////// insert temp table//////////////
                        string query = "exec SP_InsertTempTalbNo @p1,@p2";
                        SqlCommand cmd1 = new SqlCommand(query, Constants.con);
                        cmd1.Parameters.AddWithValue("@p1", flag);
                        cmd1.Parameters.AddWithValue("@p2", Cmb_FYear.Text);



                        Constants.opencon();
                        cmd1.ExecuteNonQuery();

                        ///////////////////////////
                        TXT_TalbNo.Text = flag.ToString();//el rakm el new
                        if (AddEditFlag == 2)
                        {
                            GetData(Convert.ToInt32(TXT_TalbNo.Text), Cmb_FYear.Text);

                        }


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

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            browseBTN.Enabled = false;

            foreach (DataGridViewRow rw in this.dataGridView1.Rows)
            {
                if (!rw.IsNewRow && rw.Cells[11].Value.ToString() == "True")
                {
                    if (rw.Cells[3].Value == null || rw.Cells[3].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[3].Value.ToString()))
                    {
                        MessageBox.Show("من فضلك تاكد من ادخال الكمية لكل التصنييفات الجديدة");
                        return;

                        // here is your message box...
                    }
                    if (rw.Cells[9].Value == null || rw.Cells[9].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[9].Value.ToString()))
                    {
                        MessageBox.Show("من فضلك تاكد من ادخال القيمة التقديرية لكل التصنييفات الجديدة");
                        return;

                        // here is your message box...
                    }
                }
            }


            if (AddEditFlag == 2)
            {
                if (FlagSign1 != 1)
                {
                    MessageBox.Show("من فضلك تاكد من توقيع الطلب");
                    return;
                }

                AddLogic();
            }
            else if (AddEditFlag == 1)
            {

                EditLogic();
                ////////////////call sp to know status of talb/////////////////////
                //    SP_CheckFinancialTalb

                //   if (FlagSign11 == 1 || FlagSign11 !=1)//check anyway with every update

                //if (FlagSign3 == 1)

            }

            reset();
        }

        private void Getdata(string cmd)
        {
            dataadapter = new SqlDataAdapter(cmd, Constants.con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataadapter.Fill(table);
            dataGridView1.DataSource = table;

            dataGridView1.Columns["TalbTwareed_No"].HeaderText = "رقم طلب التوريد";//col0
            dataGridView1.Columns["TalbTwareed_No"].ReadOnly = true;
            // dataGridView1.Columns["TalbTwareed_No"].Width = 60;
            dataGridView1.Columns["FYear"].HeaderText = "السنة المالية";//col1
            dataGridView1.Columns["FYear"].ReadOnly = true;
            dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col2
            dataGridView1.Columns["Bnd_No"].ReadOnly = true;
            //dataGridView1.Columns["Bnd_No"].Width = 40;
            dataGridView1.Columns["RequestedQuan"].HeaderText = "الكمية";//col3

            //dataGridView1.Columns["RequestedQuan"].Width = 50;
            dataGridView1.Columns["Unit"].HeaderText = "الوحدة";//col4
            dataGridView1.Columns["BIAN_TSNIF"].HeaderText = "بيان الموصفات";//col5
            //dataGridView1.Columns["BIAN_TSNIF"].Width = 150;
            dataGridView1.Columns["STOCK_NO_ALL"].HeaderText = "الدليل الرقمى";//col6
            dataGridView1.Columns["STOCK_NO_ALL"].ReadOnly = true;

            dataGridView1.Columns["Quan"].HeaderText = "رصيد المخزن";//col7
            dataGridView1.Columns["Quan"].ReadOnly = true;

            if (Constants.User_Type == "NewTasnif")
            {
                dataGridView1.Columns["STOCK_NO_ALL"].ReadOnly = false;
                dataGridView1.Columns["Quan"].ReadOnly = false;
            }
            dataGridView1.Columns["ArrivalDate"].HeaderText = "تاريخ وروده";//col8
            dataGridView1.Columns["ArrivalDate"].Visible = false;
            dataGridView1.Columns["ApproxValue"].HeaderText = "القيمة التقديرية";//col9
            dataGridView1.Columns["AdditionStockFlag"].HeaderText = "بالاضافة الى رصيد";//col10
            dataGridView1.Columns["AdditionStockFlag"].ReadOnly = true;
            dataGridView1.Columns["NewTasnifFlag"].HeaderText = "تصنيف جديد";//col11

            dataGridView1.Columns["NewTasnifFlag"].ReadOnly = true;
            dataGridView1.Columns["TalbTwareed_No2"].HeaderText = "رقم طلب التوريد";//col12
            dataGridView1.Columns["TalbTwareed_No2"].Visible = false;




            if (Constants.User_Type == "A")
            {
                dataGridView1.Columns["ArrivalDate"].ReadOnly = true;
            }


            dataGridView1.AllowUserToAddRows = true;

        }

        private void GetData(int x, string y)
        {
            if (string.IsNullOrWhiteSpace(TXT_TalbNo.Text))
            {
                // MessageBox.Show("ادخل رقم التصريح");
                //  PermNo_text.Focus();
                return;
            }
            else
            {
                table.Clear();
                TableQuery = "SELECT  [TalbTwareed_No] ,[FYear],[Bnd_No],[RequestedQuan],Unit,[BIAN_TSNIF] ,STOCK_NO_ALL,Quan,[ArrivalDate] ,ApproxValue,AdditionStockFlag,NewTasnifFlag ,TalbTwareed_No2 FROM [T_TalbTawreed_Benod] Where TalbTwareed_No = " + x + " and Fyear='" + y + "'";
                Getdata(TableQuery);
            }

        }


        private void Editbtn_Click_1(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد تعديل طلب توريد ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_TalbNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text))
                {
                    MessageBox.Show("يجب اختيار طلب التوريد المراد تعديله");
                    return;
                }
                else
                {
                    AddEditFlag = 1;
                    TNO = TXT_TalbNo.Text;
                    FY = Cmb_FYear.Text;
                    var button = (Button)sender;


                    if (button.Name == "Editbtn2") //da al edit button bata3 create talb al tawred me4 aly fi al motab3a
                    {
                        PrepareEditState();
                    }
                    else
                    {
                        DisableControls();
                    }
                    /////////////////////////////////////////////////////بعد توقيع مدير العام لا يمكن التعديل///////////////////////////////
                    if (FlagSign3 == 1)
                    {
                        DisableControls();
                    }

                    /////////////////////////////////////////////////////////////////////
                    if (button.Name == "Editbtn" && Constants.UserTypeB == "ChangeTasnif")
                    {
                        dataGridView1.ReadOnly = false;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            for (int i = 0; i <= 12; i++)
                            {
                                row.Cells[i].ReadOnly = true;
                            }
                            //  dataGridView1.ReadOnly = true;
                            row.Cells[6].ReadOnly = false;//in perm
                        }
                    }

                    if (button.Name == "Editbtn" && Constants.User_Type == "A")
                    {
                        //BTN_Sign1.Enabled = true;

                        BTN_Sign2.Enabled = true;
                        BTN_Sign3.Enabled = true;
                        DeleteBtn2.Enabled = true;
                        BTN_Sign4.Enabled = false;
                        TXT_BndMwazna.Enabled = false;
                        BTN_Sign5.Enabled = false;
                        BTN_Sign6.Enabled = false;
                        BTN_Sign7.Enabled = false;
                        BTN_Sign8.Enabled = false;
                        BTN_Sign9.Enabled = false;
                        BTN_Sign10.Enabled = false;
                        BTN_Sign11.Enabled = false;
                        BTN_Sign12.Enabled = false;
                        BTN_Sign13.Enabled = false;
                        DisableMoshtryat();
                        TXT_AppValue.Enabled = false;
                        TXT_ArabicValue.Enabled = false;


                    }
                    else if (button.Name == "Editbtn" && Constants.User_Type == "B" && Constants.UserTypeB == "Stock")
                    {
                        //BTN_Sign1.Enabled = true;
                        BTN_Sign1.Enabled = false;
                        BTN_Sign2.Enabled = false;
                        BTN_Sign3.Enabled = false;

                        BTN_Sign4.Enabled = false;
                        TXT_BndMwazna.Enabled = false;
                        BTN_Sign5.Enabled = false;
                        BTN_Sign6.Enabled = true;
                        // EnableMoshtryat();
                        BTN_Sign8.Enabled = false;
                        BTN_Sign9.Enabled = false;
                        BTN_Sign10.Enabled = false;
                        BTN_Sign11.Enabled = false;
                        BTN_Sign12.Enabled = false;
                        BTN_Sign13.Enabled = false;
                        BTN_Sign7.Enabled = false;
                        TXT_AppValue.Enabled = true;
                        TXT_ArabicValue.Enabled = true;
                    }

                    else if (button.Name == "Editbtn" && Constants.User_Type == "B" && Constants.UserTypeB == "Purchases")
                    {
                        //BTN_Sign1.Enabled = true;
                        BTN_Sign1.Enabled = false;
                        BTN_Sign2.Enabled = false;
                        BTN_Sign3.Enabled = false;

                        BTN_Sign4.Enabled = false;
                        TXT_BndMwazna.Enabled = false;
                        BTN_Sign5.Enabled = true;
                        BTN_Sign6.Enabled = false;
                        //  EnableMoshtryat();
                        BTN_Sign8.Enabled = false;
                        BTN_Sign9.Enabled = false;
                        BTN_Sign10.Enabled = false;
                        BTN_Sign11.Enabled = false;
                        BTN_Sign12.Enabled = false;
                        BTN_Sign13.Enabled = false;
                        BTN_Sign7.Enabled = false;
                        TXT_AppValue.Enabled = true;
                        TXT_ArabicValue.Enabled = true;
                    }
                    else if (button.Name == "Editbtn" && Constants.User_Type == "B" && Constants.UserTypeB == "GMInventory")
                    {
                        //BTN_Sign1.Enabled = true;
                        BTN_Sign1.Enabled = false;
                        BTN_Sign2.Enabled = false;
                        BTN_Sign3.Enabled = false;

                        BTN_Sign4.Enabled = false;
                        TXT_BndMwazna.Enabled = false;
                        BTN_Sign5.Enabled = false;
                        BTN_Sign6.Enabled = true;
                        // EnableMoshtryat();
                        BTN_Sign8.Enabled = false;
                        BTN_Sign9.Enabled = false;
                        BTN_Sign10.Enabled = false;
                        BTN_Sign11.Enabled = false;
                        BTN_Sign12.Enabled = false;
                        BTN_Sign13.Enabled = false;
                        BTN_Sign7.Enabled = false;
                        TXT_AppValue.Enabled = true;
                        TXT_ArabicValue.Enabled = true;
                    }


                    else if ((button.Name == "Editbtn" && Constants.User_Type == "B" && Constants.UserTypeB == "Chairman") && (talbstatus == 3 || talbstatus == 4))
                    {
                        //BTN_Sign1.Enabled = true;
                        BTN_Sign1.Enabled = false;
                        BTN_Sign2.Enabled = false;
                        BTN_Sign3.Enabled = false;
                        DisableMoshtryat();
                        BTN_Sign4.Enabled = false;
                        TXT_BndMwazna.Enabled = false;
                        BTN_Sign5.Enabled = false;
                        BTN_Sign6.Enabled = false;
                        BTN_Sign8.Enabled = false;
                        BTN_Sign9.Enabled = false;
                        BTN_Sign11.Enabled = false;
                        BTN_Sign12.Enabled = false;
                        BTN_Sign13.Enabled = false;
                        BTN_Sign7.Enabled = true;
                        BTN_Sign10.Enabled = true;
                    }
                    else if ((button.Name == "Editbtn" && Constants.User_Type == "B" && Constants.UserTypeB == "ViceChairman") && (talbstatus == 2))
                    {
                        //BTN_Sign1.Enabled = true;
                        BTN_Sign1.Enabled = false;
                        BTN_Sign2.Enabled = false;
                        BTN_Sign3.Enabled = false;
                        DisableMoshtryat();
                        BTN_Sign4.Enabled = false;
                        TXT_BndMwazna.Enabled = false;
                        BTN_Sign5.Enabled = false;
                        BTN_Sign6.Enabled = false;
                        BTN_Sign8.Enabled = false;
                        BTN_Sign9.Enabled = false;
                        BTN_Sign11.Enabled = false;
                        BTN_Sign12.Enabled = false;
                        BTN_Sign13.Enabled = true;
                        BTN_Sign7.Enabled = false;
                        BTN_Sign10.Enabled = false;
                    }
                    else if (button.Name == "Editbtn" && Constants.User_Type == "B" && Constants.UserTypeB == "Mwazna")
                    {
                        //BTN_Sign1.Enabled = true;
                        BTN_Sign1.Enabled = false;
                        BTN_Sign2.Enabled = false;
                        BTN_Sign3.Enabled = false;
                        DisableMoshtryat();
                        BTN_Sign4.Enabled = true;
                        TXT_BndMwazna.Enabled = true;
                        BTN_Sign5.Enabled = false;
                        BTN_Sign6.Enabled = false;
                        BTN_Sign8.Enabled = false;
                        BTN_Sign9.Enabled = false;
                        BTN_Sign7.Enabled = false;
                        BTN_Sign10.Enabled = false;
                        BTN_Sign12.Enabled = false;
                        BTN_Sign13.Enabled = false;
                        BTN_Sign11.Enabled = true;

                    }
                    else if (button.Name == "Editbtn" && Constants.User_Type == "B" && Constants.UserTypeB == "TechnicalFollowUp")
                    {
                        //BTN_Sign1.Enabled = true;
                        BTN_Sign1.Enabled = false;
                        BTN_Sign2.Enabled = false;
                        BTN_Sign3.Enabled = false;
                        DisableMoshtryat();
                        BTN_Sign4.Enabled = false;
                        TXT_BndMwazna.Enabled = false;
                        BTN_Sign5.Enabled = false;
                        BTN_Sign6.Enabled = false;
                        BTN_Sign8.Enabled = false;
                        BTN_Sign9.Enabled = true;
                        BTN_Sign7.Enabled = false;
                        BTN_Sign10.Enabled = false;
                        BTN_Sign11.Enabled = false;
                        BTN_Sign12.Enabled = false;
                        BTN_Sign13.Enabled = false;
                    }
                    else if (button.Name == "Editbtn" && Constants.User_Type == "B" && Constants.UserTypeB == "InventoryControl")
                    {
                        //BTN_Sign1.Enabled = true;
                        BTN_Sign1.Enabled = false;
                        BTN_Sign2.Enabled = false;
                        BTN_Sign3.Enabled = false;
                        DisableMoshtryat();
                        BTN_Sign4.Enabled = false;
                        TXT_BndMwazna.Enabled = false;
                        BTN_Sign5.Enabled = false;
                        BTN_Sign6.Enabled = false;
                        BTN_Sign8.Enabled = false;
                        BTN_Sign9.Enabled = false;
                        BTN_Sign7.Enabled = false;
                        BTN_Sign10.Enabled = false;
                        BTN_Sign11.Enabled = false;
                        BTN_Sign12.Enabled = true;
                        BTN_Sign13.Enabled = false;
                    }
                    else if (button.Name == "Editbtn" && Constants.User_Type == "B" && Constants.UserTypeB == "NewTasnif")
                    {
                        //BTN_Sign1.Enabled = true;
                        BTN_Sign1.Enabled = false;
                        BTN_Sign2.Enabled = false;
                        BTN_Sign3.Enabled = false;
                        DisableMoshtryat();
                        BTN_Sign4.Enabled = false;
                        TXT_BndMwazna.Enabled = false;
                        BTN_Sign5.Enabled = false;
                        BTN_Sign6.Enabled = false;
                        BTN_Sign8.Enabled = true;
                        BTN_Sign9.Enabled = false;
                        BTN_Sign7.Enabled = false;
                        BTN_Sign10.Enabled = false;
                        BTN_Sign11.Enabled = false;
                        BTN_Sign12.Enabled = false;
                        BTN_Sign13.Enabled = false;
                        dataGridView1.Enabled = true;
                        dataGridView1.ReadOnly = false;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            for (int i = 0; i <= 12; i++)
                            {
                                row.Cells[i].ReadOnly = true;
                            }
                            //  dataGridView1.ReadOnly = true;
                            row.Cells[6].ReadOnly = false;//in perm
                        }
                    }
                }

            }

            browseBTN.Enabled = false;
        }

        private void Cmb_FYear2_SelectedIndexChanged(object sender, EventArgs e)
        {

            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            string cmdstring = "";
            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            if (Constants.RedirectedFlag == 1)
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and( Stock_Sign is not null) and (Sign9 is  not null) and( CH_Sign is not  null) and Audit_Sign is not null and RedirectedFor='" + Constants.FlagRedirectEmpn + "'";

            }
            else if (Constants.User_Type == "A")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and CodeEdara=@CE  and(( Confirm_Sign1 is  null) or( Confirm_Sign2 is  null)) ";

            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Stock")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and( Stock_Sign is not null) and (Sign9 is not  null) and CH_Sign is not null and (Audit_Sign is not null) and (Mohmat_Sign is null)";

            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == " Purchases")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )  and (Sign12 is not null )   and( Stock_Sign is not null) and (Sign9 is not  null) and CH_Sign is not null and (Audit_Sign is null )and Mohmat_Sign is null)";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "GMInventory")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and (Sign12 is not null )  and( Stock_Sign is not null) and (Sign9 is not  null) and CH_Sign is not null and (Audit_Sign is not null )and Mohmat_Sign is null";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "NewTasnif")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is null ) ";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Mwazna")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null ) and Sign12 is not null and (Sign11 is null or Stock_Sign is null)";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "TechnicalFollowUp")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and (Sign12 is not null )  and( Stock_Sign is not  null) and Sign9 is null";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Chairman")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and (Sign12 is not null )  and( Stock_Sign is not null) and (Sign9 is  not null) and( CH_Sign is null)";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "ViceChairman")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and (Sign12 is not null )  and( Stock_Sign is not null)  and( Sign13 is null)";

            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Purchases")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and (Sign12 is not null )  and( Stock_Sign is not null) and (Sign9 is  not null) and( CH_Sign is not  null) and Audit_Sign is null";

            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "InventoryControl")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null ) and Sign12 is null ";
            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "ChangeTasnif")
            {
                cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and( Mohmat_Sign is not null)  ";

            }
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
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.Pic_Sign1, "My button1");
            toolTip1.SetToolTip(this.Pic_Sign2, Ename2 + Environment.NewLine + wazifa2);
            Constants.closecon();

        }

        private void Cmb_TalbNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_TalbNo2.SelectedIndex != -1)
            {
                SearchTalb(2);
            }
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
                cmd.Parameters.AddWithValue("@TN", TXT_TalbNo.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text);
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
            else if (x == 1 && Constants.User_Type == "B")
            {
                cmdstring = "select * from  T_TalbTawreed where TalbTwareed_No=@TN and FYear=@FY";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", TXT_TalbNo.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text);
            }
            else if (x == 3 && Constants.User_Type == "B")
            {
                cmdstring = "select * from  T_TalbTawreed where TalbTwareed_No2=@TN and FYear=@FY";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", TXT_TalbNo2.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text);
            }
            else if (x == 3 && Constants.User_Type == "A")
            {
                cmdstring = "select * from  T_TalbTawreed where TalbTwareed_No2=@TN and FYear=@FY and CodeEdara=@EC";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", TXT_TalbNo2.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text);
                cmd.Parameters.AddWithValue("@EC", Constants.CodeEdara);
            }
            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);


            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                try
                {
                    while (dr.Read())
                    {

                        TXT_TalbNo.Text = dr["TalbTwareed_No"].ToString();
                        TXT_TalbNo2.Text = dr["TalbTwareed_No2"].ToString();
                        /////////////////////////////////////////////////////
                        TXT_DateTaamen.Text = dr["TaamenDate"].ToString();
                        if (Convert.ToBoolean(dr["TaamenFlag"].ToString()) == true)
                        {
                            RadioBTN_Tammen1.Checked = true;
                            RadioBTN_Taamen2.Checked = false;
                        }
                        else if (Convert.ToBoolean(dr["TaamenFlag"].ToString()) == false)
                        {
                            RadioBTN_Tammen1.Checked = false;
                            RadioBTN_Taamen2.Checked = true;
                            TXT_DateTaamen.Text = (dr["TaamenDate"].ToString());

                        }
                        // RadioBTN_Taamen2.Checked=Convert.ToBoolean( dr["TammenFlag"].ToString());

                        ChBTN_Analysis.Checked = Convert.ToBoolean(dr["NeedAnalysisFlag"].ToString());
                        ChBTN_Origin.Checked = Convert.ToBoolean(dr["OriginFlag"].ToString());
                        ChBTN_Tests.Checked = Convert.ToBoolean(dr["NeedTestsFlag"].ToString());
                        string country = dr["Country"].ToString();
                        //   string author = "Name: Mahesh Chand, Book: C# Programming, Publisher: C# Corner, Year: 2020";
                        string[] countryinfo = country.Split(',');
                        //  foreach (string info in countryinfo)
                        // {
                        //     checkedListBox1.Items(info).Checked = true;
                        //Console.WriteLine("   {0}", info.Substring(info.IndexOf(": ") + 1));
                        // }

                        for (int count = 0; count < checkedListBox1.Items.Count; count++)
                        {
                            if (countryinfo.Contains(checkedListBox1.Items[count].ToString()))
                            {
                                checkedListBox1.SetItemChecked(count, true);
                            }
                        }
                        //   checkedListBox1.Checked= dr["Country1"].ToString();


                        ///////////////////////////////////////////
                        TXT_Edara.Text = dr["NameEdara"].ToString();
                        currentcodeedara = dr["CodeEdara"].ToString();
                        TXT_Date.Text = dr["CreationDate"].ToString();
                        TXT_ReqFor.Text = dr["RequiredFor"].ToString();
                        TXT_AppValue.Text = dr["ApproxAmount"].ToString();
                        TXT_ArabicValue.Text = dr["ArabicAmount"].ToString();
                        TXT_Tamen.Text = dr["Taamen"].ToString();
                        TXT_BndMwazna.Text = dr["BndMwazna"].ToString();
                        Cmb_Currency.Text = dr["CurrencyBefore"].ToString();
                        TXT_PriceSarf.Text = dr["ExchangeRate"].ToString();

                        TXT_RedirectedFor.Text = dr["RedirectedFor"].ToString();
                        TXT_RedirectedDate.Text = dr["RedirectedForDate"].ToString();

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
                        string BUM = dr["BuyMethod"].ToString();
                        if (BUM == "1")
                        {
                            radioButton1.Checked = true;
                        }
                        else if (BUM == "2")
                        {
                            radioButton2.Checked = true;
                        }
                        else if (BUM == "3")
                        {
                            radioButton3.Checked = true;
                        }
                        else if (BUM == "4")
                        {
                            radioButton4.Checked = true;
                        }
                        else if (BUM == "5")
                        {
                            radioButton5.Checked = true;
                        }
                        else if (BUM == "6")
                        {
                            radioButton6.Checked = true;
                        }
                        //  FlagCmbYear = 1;
                        Cmb_FYear.Text = dr["FYear"].ToString();

                        ////////////////////////////////
                        talbstatus = Constants.GetTalbStatus(TXT_TalbNo.Text, Cmb_FYear.Text);
                        MessageBox.Show("talb status is" + talbstatus.ToString());

                        ///////////////////////////////////////

                        if (s1 != "")
                        {
                            string p = Constants.RetrieveSignature("1", "1", s1);


                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename1 = p.Split(':')[1];
                                wazifa1 = p.Split(':')[2];
                                pp = p.Split(':')[0];

                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "1"]).Image = Image.FromFile(@pp);

                                FlagSign1 = 1;
                                FlagEmpn1 = s1;
                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "1"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign1, Ename1 + Environment.NewLine + wazifa1);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.panel13.Controls["Pic_Sign" + "1"]).BackColor = Color.Red;
                        }
                        if (s2 != "")
                        {
                            string p = Constants.RetrieveSignature("2", "1", s2);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename2 = p.Split(':')[1];
                                wazifa2 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "2"]).Image = Image.FromFile(@pp);
                                FlagSign2 = 1;
                                FlagEmpn2 = s2;
                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "2"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign2, Ename2 + Environment.NewLine + wazifa2);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.panel13.Controls["Pic_Sign" + "2"]).BackColor = Color.Red;
                        }
                        if (s3 != "")
                        {
                            string p = Constants.RetrieveSignature("3", "1", s3);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename3 = p.Split(':')[1];
                                wazifa3 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "3"]).Image = Image.FromFile(@pp);
                                FlagSign3 = 1;
                                FlagEmpn3 = s3;
                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "3"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign3, Ename3 + Environment.NewLine + wazifa3);

                            }


                        }
                        else
                        {
                            ((PictureBox)this.panel13.Controls["Pic_Sign" + "3"]).BackColor = Color.Red;
                        }
                        if (s4 != "")
                        {
                            string p = Constants.RetrieveSignature("4", "1", s4);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename4 = p.Split(':')[1];
                                wazifa4 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "4"]).Image = Image.FromFile(@pp);
                                FlagSign4 = 1;
                                FlagEmpn4 = s4;
                                toolTip1.SetToolTip(Pic_Sign4, Ename4 + Environment.NewLine + wazifa4);

                            }
                        }
                        else
                        {
                            ((PictureBox)this.panel13.Controls["Pic_Sign" + "4"]).BackColor = Color.Red;
                        }
                        if (s5 != "")
                        {
                            string p = Constants.RetrieveSignature("5", "1", s5);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename5 = p.Split(':')[1];
                                wazifa5 = p.Split(':')[2];
                                pp = p.Split(':')[0];

                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "5"]).Image = Image.FromFile(@pp);
                                FlagSign5 = 1;
                                FlagEmpn5 = s5;
                                toolTip1.SetToolTip(Pic_Sign5, Ename5 + Environment.NewLine + wazifa5);

                            }


                        }
                        else
                        {
                            ((PictureBox)this.panel13.Controls["Pic_Sign" + "5"]).BackColor = Color.Red;
                        }
                        if (s6 != "")
                        {
                            string p = Constants.RetrieveSignature("6", "1", s6);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename6 = p.Split(':')[1];
                                wazifa6 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "6"]).Image = Image.FromFile(@pp);
                                FlagSign6 = 1;
                                FlagEmpn6 = s6;
                                toolTip1.SetToolTip(Pic_Sign6, Ename6 + Environment.NewLine + wazifa6);

                            }


                        }
                        else
                        {
                            ((PictureBox)this.panel13.Controls["Pic_Sign" + "6"]).BackColor = Color.Red;
                        }
                        if (s7 != "")
                        {
                            string p = Constants.RetrieveSignature("7", "1", s7);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename7 = p.Split(':')[1];
                                wazifa7 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "7"]).Image = Image.FromFile(@pp);
                                FlagSign7 = 1;
                                FlagEmpn7 = s7;
                                toolTip1.SetToolTip(Pic_Sign7, Ename7 + Environment.NewLine + wazifa7);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.panel13.Controls["Pic_Sign" + "7"]).BackColor = Color.Red;
                        }
                        if (s8 != "")
                        {
                            string p = Constants.RetrieveSignature("8", "1", s8);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename8 = p.Split(':')[1];
                                wazifa8 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "8"]).Image = Image.FromFile(@pp);
                                FlagSign8 = 1;
                                FlagEmpn8 = s8;
                                toolTip1.SetToolTip(Pic_Sign8, Ename8 + Environment.NewLine + wazifa8);

                            }


                        }
                        else
                        {
                            ((PictureBox)this.panel13.Controls["Pic_Sign" + "8"]).BackColor = Color.Red;
                        }
                        if (s9 != "")
                        {
                            string p = Constants.RetrieveSignature("9", "1", s9);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename9 = p.Split(':')[1];
                                wazifa9 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "9"]).Image = Image.FromFile(@pp);
                                FlagSign9 = 1;
                                FlagEmpn9 = s9;
                                toolTip1.SetToolTip(Pic_Sign9, Ename9 + Environment.NewLine + wazifa9);
                            }


                        }
                        else
                        {
                            ((PictureBox)this.panel13.Controls["Pic_Sign" + "9"]).BackColor = Color.Red;
                        }

                        if (s11 != "")
                        {
                            string p = Constants.RetrieveSignature("11", "1", s11);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename11 = p.Split(':')[1];
                                wazifa11 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "11"]).Image = Image.FromFile(@pp);
                                FlagSign11 = 1;
                                FlagEmpn11 = s11;
                                toolTip1.SetToolTip(Pic_Sign11, Ename11 + Environment.NewLine + wazifa11);

                            }


                        }
                        else
                        {
                            ((PictureBox)this.panel13.Controls["Pic_Sign" + "11"]).BackColor = Color.Red;
                        }

                        if (s12 != "")
                        {
                            string p = Constants.RetrieveSignature("12", "1", s12);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename12 = p.Split(':')[1];
                                wazifa12 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.panel13.Controls["Pic_Sign" + "12"]).Image = Image.FromFile(@pp);
                                FlagSign12 = 1;
                                FlagEmpn12 = s12;
                                toolTip1.SetToolTip(Pic_Sign12, Ename12 + Environment.NewLine + wazifa12);

                            }


                        }
                        else
                        {
                            ((PictureBox)this.panel13.Controls["Pic_Sign" + "12"]).BackColor = Color.Red;
                        }







                    }
                }
                finally
                {
                    if (dr != null)
                    {
                        dr.Dispose();
                    }
                }
                /*
                for (int i = 1; i <= 7; i++)
                {
                    string p = Constants.RetrieveSignature(i.ToString(), "1");
                    if (p != "")
                    {
                        //   Pic_Sign1
                        //	"Pic_Sign1"	string

                        ((PictureBox)this.panel1.Controls["Pic_Sign" + i.ToString()]).Image = Image.FromFile(@p);

                    }

                }*/
                if (x == 1) {
                    BTN_Print.Visible = true;
                    BTN_Print.Enabled = true;
                }
                else {
                    BTN_Print2.Visible = true;
                    BTN_Print2.Enabled = true;
                }

            }

            else
            {
                MessageBox.Show("من فضلك تاكد من رقم طلب التوريد");
                if (x == 1) {
                    //   BTN_Print.Visible=true;
                    BTN_Print.Enabled = false;
                }
                else {
                    //  BTN_Print2.Visible=true;
                    BTN_Print2.Enabled = false;
                }

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
            GetData(Convert.ToInt32(TXT_TalbNo.Text), Cmb_FYear.Text);
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

        private void BTN_Save2_Click(object sender, EventArgs e)
        {
            if (AddEditFlag == 1)
            {
                UpdateTalbTawreed();
                Input_Reset();
                Cmb_FYear2.SelectedIndex = -1;
                Cmb_TalbNo2.SelectedIndex = -1;
                Cmb_TalbNo2.Text = "";
            }
        }

        public void DeleteTalb()
        {

            if ((MessageBox.Show("هل تريد حذف طلب توريد ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(TXT_TalbNo.Text))
                {
                    MessageBox.Show("يجب اختيار طلب التوريد اولا");
                    return;
                }
                Constants.opencon();
                string cmdstring1 = "select STOCK_NO_ALL,AdditionStockFlag,Bnd_No from T_TalbTawreed_Benod where FYear=@FY and TalbTwareed_No=@TNO";
                SqlCommand cmd1 = new SqlCommand(cmdstring1, Constants.con);


                cmd1.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_TalbNo.Text));
                cmd1.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
                SqlDataReader dr = cmd1.ExecuteReader();

                //---------------------------------
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {

                        string cmdstring2 = "Exec SP_UndoVirtualQuan @TNO,@FY,@BN";

                        SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);

                        cmd2.Parameters.AddWithValue("@TNO", (dr["STOCK_NO_ALL"].ToString()));
                        if (dr["AdditionStockFlag"].ToString() == "" || dr["AdditionStockFlag"] == DBNull.Value)
                        {
                            cmd2.Parameters.AddWithValue("@FY", 0);
                        }
                        else
                        {

                            cmd2.Parameters.AddWithValue("@FY", Convert.ToDouble(dr["AdditionStockFlag"].ToString()));
                        }
                        cmd2.Parameters.AddWithValue("@BN", (dr["Bnd_No"].ToString()));
                        ///   cmd2.ExecuteNonQuery();

                    }
                }
                dr.Close();


                string cmdstring = "Exec SP_deleteTalbTawreed @TNO,@FY,@aot output";

                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_TalbNo.Text));
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
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

        private void TXT_TalbNo_TextChanged(object sender, EventArgs e)
        {
            Constants.validateTextboxNumbersonly(sender);
            //  MessageBox.Show("من فضلك تاكد من العملة ");
        }

        private void Cmb_TalbNo2_TextChanged(object sender, EventArgs e)
        {
            // Input_Reset();
            Pic_Sign1.Image = null;
            Pic_Sign2.Image = null;
            Pic_Sign3.Image = null;
            Pic_Sign4.Image = null;


            Pic_Sign5.Image = null;
            Pic_Sign6.Image = null;
            Pic_Sign7.Image = null;
            Pic_Sign8.Image = null;
            Pic_Sign9.Image = null;
            Pic_Sign11.Image = null;


            FlagSign1 = 0;
            FlagSign2 = 0;
            FlagSign3 = 0;
            FlagSign4 = 0;
            FlagSign5 = 0;
            FlagSign6 = 0;
            FlagSign7 = 0;
            FlagSign8 = 0;
            FlagSign9 = 0;
            FlagSign10 = 0;
            FlagSign11 = 0;
            Pic_Sign1.BackColor = Color.White;
            Pic_Sign2.BackColor = Color.White;
            Pic_Sign3.BackColor = Color.White;
            Pic_Sign4.BackColor = Color.White;
            Pic_Sign5.BackColor = Color.White;
            Pic_Sign6.BackColor = Color.White;
            Pic_Sign7.BackColor = Color.White;
            Pic_Sign8.BackColor = Color.White;
            Pic_Sign9.BackColor = Color.White;
            Pic_Sign11.BackColor = Color.White;
        }

        private void Cmb_TalbNo2_DropDownClosed(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(Cmb_TalbNo2.Text) || string.IsNullOrEmpty(Cmb_TalbNo2.Text)))       
            {
                Input_Reset();
                SearchTalb(2);
            }
        }

        private void TXT_TalbNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TXT_TalbNo.Text) == false)
            {
                GetData(Convert.ToInt32(TXT_TalbNo.Text), Cmb_FYear.Text);
             
            }
       
        }

        private void TXT_TalbNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode ==Keys.Enter && AddEditFlag==2)
            {
                GetData(Convert.ToInt32(TXT_TalbNo.Text), Cmb_FYear.Text);
             
            }
            else if (e.KeyCode == Keys.Enter && AddEditFlag == 0)
            {
                cleargridview();
                SearchTalb(1);
                BTN_Print.Visible=true;
            }
        }

        private void TXT_AppValue_TextChanged(object sender, EventArgs e)
        {
           // Constants.validateTextboxNumbersonly(sender);
            try
            {
                ToWord toWord = new ToWord(Convert.ToDecimal(TXT_AppValue.Text), currencies[0]);
                TXT_ArabicValue.Text = toWord.ConvertToArabic();
            }
            catch (Exception ex)
            {
                TXT_ArabicValue.Text = String.Empty;
                Console.WriteLine(ex);
            }
        }

        private void TXT_StockNoAll_TextChanged(object sender, EventArgs e)
        {
            Txt_ReqQuan.Text="";
        }

        private void TXT_AppValue_KeyPress(object sender, KeyPressEventArgs e)
        {
           Constants.validatenumbersanddecimal(TXT_AppValue.Text, e);
        }
       
        private void Column2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled =false;
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
            if (dataGridView1.CurrentCell.ColumnIndex == 3)//reqQuan
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

        private void BTN_Print_Click(object sender, EventArgs e)
        {
              if (string.IsNullOrEmpty(TXT_TalbNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text))
                {
                    MessageBox.Show("يجب اختيار طلب التوريد المراد طباعتها اولا");
                    return;
                }
                else
                {

                    Constants.TalbFY = Cmb_FYear.Text;
                    Constants.TalbNo = Convert.ToInt32(TXT_TalbNo.Text);
                    Constants.FormNo = 8;
                    FReports f = new FReports();
                    f.Show();
                }
            }
    
        private void BTN_Print2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Cmb_TalbNo2.Text) || string.IsNullOrEmpty(Cmb_FYear2.Text))
                {
                    MessageBox.Show("يجب اختيار طلب التوريد المراد طباعتها اولا");
                    return;
                }
                else
                {

                    Constants.TalbFY = Cmb_FYear2.Text;
                    Constants.TalbNo = Convert.ToInt32(Cmb_TalbNo2.Text);
                    Constants.FormNo = 8;
                    FReports f = new FReports();
                    f.Show();
                }
        }

        private void TXT_PartNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {
                Constants.opencon();

                CMB_ApproxValue.Text = "";
                SearchTasnif(3);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal sum;
            if (e.ColumnIndex == 9)
            {
                if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[9].Value != null && dataGridView1.Rows[e.RowIndex].Cells[9].Value != DBNull.Value)
                {
                    sum = 0;
                    for (int i = 0; i <= e.RowIndex;i++ )
                    {

                        sum = sum + Convert.ToDecimal(dataGridView1.Rows[i].Cells[9].Value);
              
                    }
                    AppValueOriginal = sum;
                    TXT_AppValue.Text = sum.ToString();

                }
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 6) // 1 should be your column index
            {

               if (e.FormattedValue != DBNull.Value  && e.FormattedValue !="")// && dataGridView1.Rows[e.RowIndex].Cells[11].Value != "true")
                
                {




                    string query = "exec Sp_CheckTasnif @a,@p1 out,@p2 out,@p3 out,@flag out ";
                    SqlCommand cmd = new SqlCommand(query, Constants.con);
                    cmd.Parameters.AddWithValue("@a", (e.FormattedValue));
                    cmd.Parameters.Add("@flag", SqlDbType.Int, 32);  //-------> output parameter
                    cmd.Parameters["@flag"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@p1", SqlDbType.NVarChar, 500);  //-------> output parameter
                    cmd.Parameters["@p1"].Direction = ParameterDirection.Output;


                    cmd.Parameters.Add("@p2", SqlDbType.NVarChar, 50);  //-------> output parameter
                    cmd.Parameters["@p2"].Direction = ParameterDirection.Output;


                    cmd.Parameters.Add("@p3", SqlDbType.Int, 32);  //-------> output parameter
                    cmd.Parameters["@p3"].Direction = ParameterDirection.Output;




                    // cmd3.ExecuteNonQuery();
                    //  int flag1;
                    Constants.opencon();
                    try
                    {

                        cmd.ExecuteNonQuery();
                        executemsg = true;

                        flag1 = (int)cmd.Parameters["@flag"].Value;

                        dataGridView1.Rows[e.RowIndex].Cells[5].Value = cmd.Parameters["@p1"].Value;
                        dataGridView1.Rows[e.RowIndex].Cells[4].Value = cmd.Parameters["@p2"].Value;
                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = cmd.Parameters["@p3"].Value;
                        dataGridView1.Rows[e.RowIndex].Cells[11].Value = false;
                        dataGridView1.Rows[e.RowIndex].Cells[9].ReadOnly = false;//Approx value can be changed 

                        if (flag1 != 2)
                        {

                            if (Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[7].Value) >= Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value))
                            {
                                MessageBox.Show("كمية المطلوبة اقل من كمية المخزن لا نحناج الى طلب توريد");
                                return;
                            }

                            else if ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[7].Value) < Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value)) && Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[7].Value) != 0)
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[3].Value = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value) - Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                                dataGridView1.Rows[e.RowIndex].Cells[10].Value = dataGridView1.Rows[e.RowIndex].Cells[7].Value;

                            }
                            else if (Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[7].Value) == 0)
                            {
                                // dataGridView1.Rows[e.RowIndex].Cells[3].Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value) - Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                                dataGridView1.Rows[e.RowIndex].Cells[10].Value = 0;

                            }

                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        executemsg = false;
                        MessageBox.Show(sqlEx.ToString());
                        flag1 = (int)cmd.Parameters["@flag"].Value;
                    }
                    if (flag1 == 2)
                    {
                        MessageBox.Show("لا يوجد رقم تصنييف بهذا الرقم");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void TXT_BndMwazna_TextChanged(object sender, EventArgs e)
        {
            Constants.validateTextboxNumbersonly(sender);
        }

        private void BTN_Tracking_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
             //   Constants.currentOpened.Close();
            }
            //----------------------
            Track_TalbTawreed F = new Track_TalbTawreed(); 
            // main Ff = new Main();
            Constants.currentOpened = F;
            F.MdiParent = this.MdiParent;
            F.Show();
         // this.IsMdiContainer = true;
         // 
         F.Dock = DockStyle.Fill;
          //  tableLayoutPanel1.Visible = false;
        }

        private void TXT_TalbNo2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && AddEditFlag == 2)
            {
              //  GetData(Convert.ToInt32(TXT_TalbNo2.Text), Cmb_FYear.Text);

            }
            else if (e.KeyCode == Keys.Enter && AddEditFlag == 0)
            {
                cleargridview();
                SearchTalb(3);
                BTN_Print.Visible = true;
            }
        }

        private void BTN_Search_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                //   Constants.currentOpened.Close();
            }
            //----------------------
            Search_TalbTawreed F = new Search_TalbTawreed();
            // main Ff = new Main();
            Constants.currentOpened = F;
            F.MdiParent = this.MdiParent;
            F.Show();
            // this.IsMdiContainer = true;
            // 
            F.Dock = DockStyle.Fill;
            //  tableLayoutPanel1.Visible = false;
        }

        private void next_btn_Click(object sender, EventArgs e)
        {
            if (Image2 != "")
            {
                picflag = 2;
                pictureBox2.Image = Image.FromFile(@Image2);
            }
        }

        private void prev_btn_Click(object sender, EventArgs e)
        {
            if (Image1 != "")
            {
                picflag = 1;
                pictureBox2.Image = Image.FromFile(@Image1);

            }
        }

        private void reInitDataGridView(DataTable table)
        {
            cleargridview();
            table.Rows.Clear();
            dataGridView1.DataSource = table;

            dataGridView1.Columns["TalbTwareed_No"].HeaderText = "رقم طلب التوريد";//col0
            dataGridView1.Columns["TalbTwareed_No"].ReadOnly = true;
            // dataGridView1.Columns["TalbTwareed_No"].Width = 60;
            dataGridView1.Columns["FYear"].HeaderText = "السنة المالية";//col1
            dataGridView1.Columns["FYear"].ReadOnly = true;
            dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col2
            dataGridView1.Columns["Bnd_No"].ReadOnly = true;
            //dataGridView1.Columns["Bnd_No"].Width = 40;
            dataGridView1.Columns["RequestedQuan"].HeaderText = "الكمية";//col3
            //dataGridView1.Columns["RequestedQuan"].Width = 50;
            dataGridView1.Columns["Unit"].HeaderText = "الوحدة";//col4
            dataGridView1.Columns["BIAN_TSNIF"].HeaderText = "بيان الموصفات";//col5
            //dataGridView1.Columns["BIAN_TSNIF"].Width = 150;
            dataGridView1.Columns["STOCK_NO_ALL"].HeaderText = "الدليل الرقمى";//col6
            dataGridView1.Columns["STOCK_NO_ALL"].ReadOnly = true;

            dataGridView1.Columns["Quan"].HeaderText = "رصيد المخزن";//col7
            dataGridView1.Columns["Quan"].ReadOnly = true;

            if (Constants.User_Type == "NewTasnif")
            {
                dataGridView1.Columns["STOCK_NO_ALL"].ReadOnly = false;
                dataGridView1.Columns["Quan"].ReadOnly = false;
            }
            dataGridView1.Columns["ArrivalDate"].HeaderText = "تاريخ وروده";//col8
            dataGridView1.Columns["ArrivalDate"].Visible = false;
            dataGridView1.Columns["ApproxValue"].HeaderText = "القيمة التقديرية";//col9
            dataGridView1.Columns["AdditionStockFlag"].HeaderText = "بالاضافة الى رصيد";//col10
            dataGridView1.Columns["AdditionStockFlag"].ReadOnly = true;
            dataGridView1.Columns["NewTasnifFlag"].HeaderText = "تصنيف جديد";//col11

            dataGridView1.Columns["NewTasnifFlag"].ReadOnly = true;
            dataGridView1.Columns["TalbTwareed_No2"].HeaderText = "رقم طلب التوريد";//col12
            dataGridView1.Columns["TalbTwareed_No2"].Visible = false;

            if (Constants.User_Type == "A")
            {
                dataGridView1.Columns["ArrivalDate"].ReadOnly = true;
            }


            dataGridView1.AllowUserToAddRows = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 1 && lastCurrencySelectedIdx != Cmb_Currency.SelectedIndex) //because deafault is one
            {
                DialogResult dialogResult = MessageBox.Show("إذا قمت بالضغط علي نعم سوف يتم محو البيانات", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                if (dialogResult == DialogResult.Yes)
                {
                    reInitDataGridView(table);
                }
                else if (dialogResult == DialogResult.No)
                {
                    Cmb_Currency.SelectedIndex = lastCurrencySelectedIdx;
                    //do something else
                }
            }

            TXT_Currency.Text = Cmb_Currency.Text;

            if (Cmb_Currency.Text != "EGP")
            {


                TXT_PriceSarf.Text = ((CurrencyData3)CurrencyConverter3.getCurrencyData(Cmb_Currency.SelectedItem.ToString())).getExchangeRate().ToString();

                ExchangeRate = ((CurrencyData3)CurrencyConverter3.getCurrencyData(Cmb_Currency.SelectedItem.ToString())).getExchangeRate();

            }
            else
            {
                ExchangeRate = 1;
                TXT_PriceSarf.Text="";
            }

            lastCurrencySelectedIdx = Cmb_Currency.SelectedIndex;


        }

        private void BTN_ConvertToEG_Click(object sender, EventArgs e)
        {
            TXT_PriceSarf.Text = ((CurrencyData3)CurrencyConverter3.getCurrencyData(Cmb_Currency.Text)).getExchangeRate().ToString();      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FlagExchange = 1;
            TXT_AppValue.Text = CurrencyConverter3.convertFromToCurrency(Currency, "EGP", Convert.ToDouble(AppValueOriginal)).ToString();
        }

        private void CMB_ApproxValue_TextChanged(object sender, EventArgs e)
        {
            if (CMB_ApproxValue.SelectedIndex > -1)//value choosen==>EGP
            {
                TXT_Currency.Text = "EGP";
            }
            else//value written ===>currency
            {
                TXT_Currency.Text = Cmb_Currency.SelectedItem.ToString();
            }
        }

        private void BTN_PDF_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TXT_TalbNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text))
            {
                MessageBox.Show("يجب اختيار السنة المالية ورقم طلب التوريد اولا");
                return;
            }
            else
            {

                PDF_PopUp popup = new PDF_PopUp();

                if (AddEditFlag == 0)//search
                {
                    popup.TalbNo= TXT_TalbNo.Text;
                    popup.Fyear = Cmb_FYear.Text;
                    popup.CodeEdara = currentcodeedara;
                }
                else//add or edit
                {
                    popup.TalbNo = TXT_TalbNo.Text;
                    popup.Fyear = Cmb_FYear.Text;
                    popup.CodeEdara = Constants.CodeEdara;
                }
                try
                {
                    popup.ShowDialog(this);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }

                popup.Dispose();
            }
        }

        private void browseBTN_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TXT_TalbNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text))
            {
                MessageBox.Show("يجب اختيار السنة المالية ورقم طلب التوريد اولا");
                return;
            }
            else
            {
                openFileDialog1.Filter = "PDF(*.pdf)|*.pdf";
                openFileDialog1.ShowDialog();
                string ConstantPath = @"\\172.18.8.83\MaterialAPP\PDF\";//////////////////change it to server path

                foreach (String file in openFileDialog1.FileNames)
                {
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string VariablePath = string.Concat(Constants.CodeEdara, @"\");
                        string path = ConstantPath + VariablePath;

                        if (!Directory.Exists(path))
                        {
                            MessageBox.Show("عفوا لايمكنك ارفاق مرفقات برجاء الرجوع إلي إدارة نظم المعلومات");
                            return;
                        }

                        path += Cmb_FYear.Text + @"\";

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        path += TXT_TalbNo.Text + @"\";

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        string filename = Path.GetFileName(file);
                        path += filename;

                        if (!File.Exists(path))
                        {
                            File.Copy(file, path);
                        }

                        //File.Move(file, path);

                        //MessageBox.Show(file);
                    }
                }

                MessageBox.Show("تم إرفاق المرفقات");
            }
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioBTN_Taamen2.Checked == true)
            {
                RadioBTN_Tammen1.Checked = false;
                TXT_DateTaamen.Enabled = true;
            }
        }

        private void RadioBTN_Tammen1_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioBTN_Tammen1.Checked == true)
            {
                RadioBTN_Taamen2.Checked = false;
                TXT_DateTaamen.Enabled = false;
            }
        }

        private void Txt_ReqQuan_KeyPress(object sender, KeyPressEventArgs e)
        {
            Constants.validatenumbersanddecimal(Txt_ReqQuan.Text, e);
        }

        private void CMB_ApproxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            Constants.validatenumbersanddecimal(CMB_ApproxValue.Text, e);
        }

        private void Txt_Quan_KeyPress(object sender, KeyPressEventArgs e)
        {
            Constants.validatenumbersanddecimal(Txt_Quan.Text, e);
        }

        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            AddEditFlag = 0;
            reset();
        }

        private void CHK_NewTasnif_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_NewTasnif.Checked == true)
            {
                //do new tasnif
                changePanelState(panel12, false);

                //Search sec
                TXT_StockNoAll.Text = "";
                TXT_StockName.Text = "";
                TXT_PartNo.Text = "";

                //dataViewre sec
                TXT_StockBian.Text = "";
                Txt_Quan.Text = "";
                Txt_ReqQuan.Text = "";
                TXT_Unit.Text = "";
                CMB_ApproxValue.Text = "";
                Quan_Min.Value = 0;
                Quan_Max.Value = 0;
                checkBox1.Checked = false;
                checkBox2.Checked = false;

                TXT_StockBian.Enabled = true;
                TXT_Unit.Enabled = true;

            }
            else
            {
                //reset to default
                changePanelState(panel12, true);

                //dataViewre sec
                TXT_StockBian.Text = "";
                Txt_ReqQuan.Text = "";
                TXT_Unit.Text = "";
                CMB_ApproxValue.Text = "";

                TXT_StockBian.Enabled = false;
                TXT_Unit.Enabled = false;
            }
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells[2].Value = row.Index + 1;
                }
            }
            dataGridView1.Refresh();
        }
        //------------------------------------------ Signature Handler ---------------------------------
        #region Signature Handler
        private void BTN_Sign1_Click(object sender, EventArgs e)
        {
            LoopGridview();

            //if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false && radioButton4.Checked == false && radioButton5.Checked == false && radioButton6.Checked == false)
            //{
            //    //MessageBox.Show("من فضلك تاكد من اختيار طريقة الشراء");
            //    return;
            //}

            string Empn1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على انشاء طلب توريد", "");

            string Sign1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على انشاء طلب توريد", "");

            if (Sign1 != "" && Empn1 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);


                Tuple<string, int, int, string, string> result = Constants.CheckSign("1", "1", Sign1, Empn1);
                if (result.Item3 == 1)
                {
                    Pic_Sign1.Image = Image.FromFile(@result.Item1);

                    FlagSign1 = result.Item2;
                    FlagEmpn1 = Empn1;
                    Ename1 = result.Item4;

                    wazifa1 = result.Item5;
                    toolTip1.SetToolTip(Pic_Sign1, Ename1 + Environment.NewLine + wazifa1);
                }
                else
                {
                    FlagSign1 = 0;
                    FlagEmpn1 = "";
                    Ename1 = "";
                    wazifa1 = "";
                }
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }
        private void BTN_Sign2_Click(object sender, EventArgs e)
        {
            LoopGridview();
            if (FlagSign1 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            string Empn2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "تصديق على  طلب توريد", "");

            string Sign2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "تصديق على  طلب توريد", "");

            if (Sign2 != "" && Empn2 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("2", "1", Sign2, Empn2);
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
        private void BTN_Sign3_Click(object sender, EventArgs e)
        {
            LoopGridview();
            if (FlagSign1 != 1 || FlagSign2 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            string Empn3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "اعتماد المدير العام", "");

            string Sign3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "اعتماد المدير العام", "");

            if (Sign3 != "" && Empn3 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.سSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("3", "1", Sign3, Empn3);
                if (result.Item3 == 1)
                {
                    Pic_Sign3.Image = Image.FromFile(@result.Item1);

                    FlagSign3 = result.Item2;
                    FlagEmpn3 = Empn3;
                    /////////////////////////send newtasnifAlarm////////////////////

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
        private void DeleteBtn2_Click(object sender, EventArgs e)
        {
            string Empn3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "اعتماد المدير العام", "");

            string Sign3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "اعتماد المدير العام", "");

            if (Sign3 != "" && Empn3 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.سSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("3", "1", Sign3, Empn3);
                if (result.Item3 == 1)
                {
                    Pic_Sign3.Image = Image.FromFile(@result.Item1);

                    FlagSign3 = result.Item2;
                    FlagEmpn3 = Empn3;
                    DeleteTalb();
                    //  MessageBox.Show(" تم الحذف بنجاح");

                    ///////////////////////////////
                }
                else
                {
                    FlagSign3 = 0;
                    FlagEmpn3 = "";
                }
                // result.Item1;
                // result.Item2;


            }
        }


        private void BTN_Sign8_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            LoopGridview();
            if (NewTasnifFlag == 1)
            {

                MessageBox.Show(" يجب ادخال التصنييفات الجديدة");
                return;
            }

            string Empn8 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع مدير ادارة التصنيف", "");

            string Sign8 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مدير ادارة التصنيف", "");

            if (Sign8 != "" && Empn8 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("8", "1", Sign8, Empn8);
                if (result.Item3 == 1)
                {
                    Pic_Sign8.Image = Image.FromFile(@result.Item1);

                    FlagSign8 = result.Item2;
                    FlagEmpn8 = Empn8;
                }
                else
                {
                    FlagSign8 = 0;
                    FlagEmpn8 = "";
                }
                // result.Item1;
                // result.Item2;


            }

        }
        private void BTN_Sign12_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign8 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }

            string Empn12 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع مدير ادارة المخزون", "");

            string Sign12 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مدير ادارة المخزون", "");

            if (Sign12 != "" && Empn12 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("12", "1", Sign12, Empn12);
                if (result.Item3 == 1)
                {
                    Pic_Sign12.Image = Image.FromFile(@result.Item1);

                    FlagSign12 = result.Item2;
                    FlagEmpn12 = Empn12;
                }
                else
                {
                    FlagSign12 = 0;
                    FlagEmpn12 = "";
                }
            }
        }
        private void BTN_Sign4_Click(object sender, EventArgs e)
        {
            LoopGridview();
            if (NewTasnifFlag == 1)
            {
                MessageBox.Show("لا يمكن التوقيع هناك تصنييفات جديدة");
                return;
            }
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign8 != 1 || FlagSign12 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            if (TXT_BndMwazna.Text.ToString() == "")
            {
                MessageBox.Show("يجب التأكد من ادخال بند الموازنة");
                return;
            }
            string Empn4 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع ادارة الموازنة", "");

            string Sign4 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع ادارة الموازنة", "");

            if (Sign4 != "" && Empn4 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("4", "1", Sign4, Empn4);
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
        private void BTN_Sign11_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign8 != 1 || FlagSign12 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            if (TXT_BndMwazna.Text.ToString() == "")
            {
                MessageBox.Show("يجب التأكد من ادخال بند الموازنة");
                return;
            }
            string Empn11 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع الادارة الموازنة", "");

            string Sign11 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع الادارة الموازنة", "");


            if (Sign11 != "" && Empn11 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("11", "1", Sign11, Empn11);
                if (result.Item3 == 1)
                {
                    Pic_Sign11.Image = Image.FromFile(@result.Item1);

                    FlagSign11 = result.Item2;
                    FlagEmpn11 = Empn11;
                }
                else
                {
                    FlagSign11 = 0;
                    FlagEmpn11 = "";
                }
                // result.Item1;
                // result.Item2;


            }
        }
        private void BTN_Sign5_Click(object sender, EventArgs e)
        {
            if ((talbstatus == 3 || talbstatus == 4) && (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 || FlagSign8 != 1 || FlagSign9 != 1 || FlagSign7 != 1 || FlagSign12 != 1))
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }

            if ((talbstatus == 1) && (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 || FlagSign8 != 1 || FlagSign12 != 1))
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            if ((talbstatus == 2) && (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 || FlagSign8 != 1 || FlagSign12 != 1 || FlagSign13 != 1))
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false && radioButton4.Checked == false && radioButton5.Checked == false && radioButton6.Checked == false)
            {
                MessageBox.Show("من فضلك تاكد من اختيار طريقة الشراء");
                return;
            }


            Redirect_PopUp popup = new Redirect_PopUp();
            // popup.Show();


            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (popup.ShowDialog(this) == DialogResult.OK)
            {
                redirectionDate = popup.BM;
                RediectionName = popup.BM3;
                TXT_RedirectedFor.Text = RediectionName;
                TXT_RedirectedDate.Text = redirectionDate;
            }

            else
            {
                //  this.txtResult.Text = "Cancelled";
            }
            if (string.IsNullOrEmpty(redirectionDate) || string.IsNullOrEmpty(RediectionName))
            {


                MessageBox.Show("من فضلك قم باختيار التوجيه");
                return;
            }
            popup.Dispose();
            string Empn5 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع مدير قطاع المشتريات", "");

            string Sign5 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مدير قطاع المشتريات", "");

            if (Sign5 != "" && Empn5 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("5", "1", Sign5, Empn5);
                if (result.Item3 == 1)
                {
                    Pic_Sign5.Image = Image.FromFile(@result.Item1);

                    FlagSign5 = result.Item2;
                    FlagEmpn5 = Empn5;
                }
                else
                {
                    FlagSign5 = 0;
                    FlagEmpn5 = "";
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
            if ((talbstatus == 3 || talbstatus == 4) && (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 || FlagSign8 != 1 || FlagSign9 != 1 || FlagSign7 != 1 || FlagSign5 != 1 || FlagSign12 != 1))
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            if ((talbstatus == 1) && (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 || FlagSign8 != 1 || FlagSign5 != 1 || FlagSign12 != 1))
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            if ((talbstatus == 2) && (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 || FlagSign8 != 1 || FlagSign5 != 1 || FlagSign12 != 1 || FlagSign13 != 1))
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }

            string Empn6 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع مدير عام المهمات", "");

            string Sign6 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مدير عام المهمات", "");


            if (Sign6 != "" && Empn6 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("6", "1", Sign6, Empn6);
                if (result.Item3 == 1)
                {
                    Pic_Sign6.Image = Image.FromFile(@result.Item1);

                    FlagSign6 = result.Item2;
                    FlagEmpn6 = Empn6;
                    ////////////////////////////////////put report on path directly///////////////
                    if (string.IsNullOrEmpty(TXT_RedirectedFor.Text) || string.IsNullOrEmpty(TXT_RedirectedDate.Text))
                    {
                        //TXT_RedirectedFor.Text = RediectionName;
                        //  TXT_RedirectedDate.Text = redirectionDate;


                    }
                    else
                    {
                        if ((MessageBox.Show("هل تريد تاكيد التوجيه؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
                        {

                            Constants.TalbFY = Cmb_FYear.Text;
                            Constants.TalbNo = Convert.ToInt32(TXT_TalbNo.Text);
                            Constants.FormNo = 88;
                            FReports f = new FReports();
                            f.Show();

                            /*
                            Stream rdlStream = this.GetType().Assembly.GetManifestResourceStream("LightSwitchApplication.ReportTemplate.GroupingAggReport.rdlc");
                            ReportWriter writer = new ReportWriter();
                            writer.ReportProcessingMode = ProcessingMode.Local;
                            writer.LoadReport(rdlStream);
                            writer.DataSources.Add(new ReportDataSource { Name = "Sales", Value = items });
                            writer.ExportCompleted += writer_ExportCompleted;
                            writer.Save(System.IO.Path.GetTempPath() + "Exported_Report.pdf", WriterFormat.PDF);*/
                        }
                        //  Sign6 = Microsoft.VisualBasic.Interaction.InputBox("هل تريد تاكيد التوجيه", "توجيه الى ", "");

                    }
                    //////////////////////////////////////////////////
                }
                else
                {
                    FlagSign6 = 0;
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
        private void BTN_Sign9_Click(object sender, EventArgs e)
        {
            if (talbstatus == 1 || talbstatus == 2)
            {
                MessageBox.Show("لا يتطلب  التوقيع");
                return;
            }

            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 || FlagSign8 != 1 || FlagSign12 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }

            string Empn9 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع الادارة الفنية", "");

            string Sign9 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع الادارة الفنية", "");


            if (Sign9 != "" && Empn9 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("9", "1", Sign9, Empn9);
                if (result.Item3 == 1)
                {
                    Pic_Sign9.Image = Image.FromFile(@result.Item1);

                    FlagSign9 = result.Item2;
                    FlagEmpn9 = Empn9;
                }
                else
                {
                    FlagSign9 = 0;
                    FlagEmpn9 = "";
                }
                // result.Item1;
                // result.Item2;


            }
        }


        private void BTN_Sign13_Click(object sender, EventArgs e)
        {
            if (talbstatus == 3 || talbstatus == 4)
            {
                MessageBox.Show("لا يتطلب  التوقيع");
                return;
            }
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 || FlagSign8 != 1 || FlagSign12 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            string Empn13 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع مساعد رئيس الشركة ", "");

            string Sign13 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مساعد رئيس الشركة ", "");

            if (Sign13 != "" && Empn13 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("13", "1", Sign13, Empn13);
                if (result.Item3 == 1)
                {
                    Pic_Sign13.Image = Image.FromFile(@result.Item1);

                    FlagSign13 = result.Item2;
                    FlagEmpn13 = Empn13;
                }
                else
                {
                    FlagSign13 = 0;
                    FlagEmpn13 = "";
                }
                // result.Item1;
                // result.Item2;


            }
        }      
        private void BTN_Sign7_Click(object sender, EventArgs e)
        {
            if (talbstatus == 1 || talbstatus == 2)
            {
                MessageBox.Show("لا يتطلب  التوقيع");
                return;
            }
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 || FlagSign8 != 1 || FlagSign9 != 1 || FlagSign12 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            string Empn7 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع رئيس مجلس الادارة و العضو المنتدب ", "");

            string Sign7 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع رئيس مجلس الادارة و العضو المنتدب ", "");

            if (Sign7 != "" && Empn7 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("7", "1", Sign7, Empn7);
                if (result.Item3 == 1)
                {
                    Pic_Sign7.Image = Image.FromFile(@result.Item1);

                    FlagSign7 = result.Item2;
                    FlagEmpn7 = Empn7;
                }
                else
                {
                    FlagSign7 = 0;
                    FlagEmpn7 = "";
                }
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }
        private void BTN_Sign10_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 || FlagSign8 != 1 || FlagSign9 != 1 || FlagSign12 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            string Empn10 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع رئيس مجلس الادارة و العضو المنتدب ", "");

            string Sign10 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع رئيس مجلس الادارة و العضو المنتدب ", "");

            if (Sign10 != "" && Empn10 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("7", "1", Sign10, Empn10);
                if (result.Item3 == 1)
                {
                    Pic_Sign7.Image = Image.FromFile(@result.Item1);

                    FlagSign10 = result.Item2;
                    FlagEmpn10 = Empn10;
                    DeleteTalb();

                }
                else
                {
                    FlagSign10 = 0;
                    FlagEmpn10 = "";
                }
                // result.Item1;
                // result.Item2;


            }
        }
        #endregion


    }
}
