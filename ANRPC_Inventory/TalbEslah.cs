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
    public partial class TalbEslah : Form
    {
        //------------------------------------------ Define Variables ---------------------------------
        #region Def Variables
        public int talbstatus = 0;
        public int FlagExchange = 0;
        string PDF = "";
        Image DefaulteImg;
        Image image1;
        Image image2;
        string[,] array1 = new string[100, 6];
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

        decimal sum = 0;
        //  public string TableQuery;

        AutoCompleteStringCollection TasnifColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TasnifNameColl = new AutoCompleteStringCollection(); //empn

        AutoCompleteStringCollection UnitColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TalbColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TalbColl2 = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection PartColl = new AutoCompleteStringCollection(); //empn

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

        //------------------------------------------ Helper ---------------------------------
        #region Helpers
            private decimal getApproxValue()
            {
                decimal result;

                int index = CMB_ApproxValue.Text.IndexOf(' ');

                if (index == -1)
                {
                    result = Convert.ToDecimal(CMB_ApproxValue.Text);
                }
                else
                {
                    index = CMB_ApproxValue.Text.IndexOf(' ', index);
                    result = Convert.ToDecimal(CMB_ApproxValue.Text.Substring(0, index));
                }

                return result;
            }
            
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

            private void SetCurrentActivatedBuyMethod(Panel panel,string bum)
            {
                try
                {
                    foreach (RadioButton radio in panel.Controls)
                    {
                        string s = radio.Name;
                        if (Convert.ToString(s[s.Length - 1]) == bum)
                        {
                            radio.Checked = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
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

     
            public void SP_UpdateSignatures(int x, DateTime D1, DateTime? D2 = null)
            {
                Constants.opencon();
                string cmdstring = "Exec  SP_UpdateSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_TalbNo.Text));
                cmd.Parameters.AddWithValue("@TNO2", DBNull.Value);

                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
                cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
                cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
                cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);

                cmd.Parameters.AddWithValue("@FN",8);

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
                Constants.closecon();
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

     

            private void InsertTalbTawreedBnood()
            {
                SqlCommand cmd;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string q = @"exec SP_InsertBnodTalbEslah @TalbTwareed_No,@FYear,@Bnd_No,@RequestedQuan,
                                        @Unit,@BIAN_TSNIF,@STOCK_NO_ALL,@Quan,@ApproxValue,@AdditionStockFlag,@NewTasnifFlag";
                        cmd = new SqlCommand(q, Constants.con);
                        cmd.Parameters.AddWithValue("@TalbTwareed_No", row.Cells[0].Value);
                        cmd.Parameters.AddWithValue("@FYear", row.Cells[1].Value);
                        cmd.Parameters.AddWithValue("@Bnd_No", row.Cells[2].Value);
                        cmd.Parameters.AddWithValue("@RequestedQuan", row.Cells[3].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Unit", row.Cells[4].Value);
                        cmd.Parameters.AddWithValue("@BIAN_TSNIF", row.Cells[5].Value);
                        cmd.Parameters.AddWithValue("@STOCK_NO_ALL", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Quan",  DBNull.Value);
                        cmd.Parameters.AddWithValue("@ApproxValue", row.Cells[7].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AdditionStockFlag", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NewTasnifFlag",  DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
/*
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string q = "exec SP_UpdateVirtualQuan @stockall,@additionstock,@p3";
                        cmd = new SqlCommand(q, Constants.con);
                        cmd.Parameters.AddWithValue("@stockall", row.Cells[10].Value);
                        cmd.Parameters.AddWithValue("@additionstock", row.Cells[6].Value);
                        cmd.Parameters.AddWithValue("@p3", 2);
                        cmd.ExecuteNonQuery();
                    }
                }*/

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
                    dataGridView1.Rows[r].Cells[4].Value = CMB_Unit.Text.ToString();
                    dataGridView1.Rows[r].Cells[5].Value = TXT_StockBian.Text;
                    //  dataGridView1.Rows[r].Cells[3].Value = TXT_StockBian.Text;
                   //dataGridView1.Rows[r].Cells[6].Value = TXT_StockNoAll.Text;
                 //   if (string.IsNullOrWhiteSpace(Txt_Quan.Text))
                  //  {
                  //      dataGridView1.Rows[r].Cells[7].Value = DBNull.Value;

                 //   }
                  //  else
                  //  {
                    //    dataGridView1.Rows[r].Cells[7].Value = Convert.ToDouble(Txt_Quan.Text);
                   // }
                    //////////////////////newpart///////////////////
                  
                        dataGridView1.Rows[r].Cells[3].Value = Convert.ToDouble(Txt_ReqQuan.Text);
                       
                    


                    dataGridView1.Rows[r].Cells[7].Value = Convert.ToDouble(Convert.ToDouble(getApproxValue()) * Convert.ToDouble(dataGridView1.Rows[r].Cells[3].Value));
                    dataGridView1.Rows[r].Cells[7].Value = Convert.ToDouble(dataGridView1.Rows[r].Cells[7].Value) * ExchangeRate;

                    //dataGridView1.Rows[r].Cells[11].Value = isNewTasnif;//not new tasnif

                    ///////////////////////////////////////////////

                    dataGridView1.Rows[r].Cells[0].Value = TXT_TalbNo.Text;
                    dataGridView1.Rows[r].Cells[1].Value = Cmb_FYear.Text;


                    sum = sum + (decimal)dataGridView1.Rows[r].Cells[7].Value;
                    AppValueOriginal = sum;
                    TXT_AppValue.Text = sum.ToString();



                    dataGridView1.Rows[r].Cells[2].Value = r + 1;
                    //  dataGridView1.Rows[r].Cells[3].Value = Txt_ReqQuan.Value;


                    dataGridView1.DataSource = table;
                #endregion
            }
            
            private void GetTalbTawreedBnod(string talbNo, string fyear)
        {
            table.Clear();

            string TableQuery = @"SELECT  [Eslah_No] ,[FYear],[Bnd_No],[RequestedQuan],Unit,[BIAN_TSNIF] ,[ArrivalDate] ,
                                ApproxValue,Eslah_No2 FROM [T_TalbEslah_Benod] 
                                Where Eslah_No = " + talbNo + " and Fyear='" + fyear + "'";

            dataadapter = new SqlDataAdapter(TableQuery, Constants.con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataadapter.Fill(table);
            dataGridView1.DataSource = table;

            dataGridView1.Columns["Eslah_No"].HeaderText = "رقم طلب الإصلاح";//col0
            dataGridView1.Columns["FYear"].HeaderText = "السنة المالية";//col1
            dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col2
            dataGridView1.Columns["RequestedQuan"].HeaderText = "الكمية";//col3
            dataGridView1.Columns["Unit"].HeaderText = "الوحدة";//col4
            dataGridView1.Columns["BIAN_TSNIF"].HeaderText = "بيان المواصفات";//col5
            //dataGridView1.Columns["STOCK_NO_ALL"].HeaderText = "الدليل الرقمى";//col6
          //  dataGridView1.Columns["Quan"].HeaderText = "رصيد المخزن";//col7

            dataGridView1.Columns["ArrivalDate"].HeaderText = "تاريخ وروده";//col6
           dataGridView1.Columns["ArrivalDate"].Visible = false;

            dataGridView1.Columns["ApproxValue"].HeaderText = "القيمة التقديرية";//col7
           // dataGridView1.Columns["AdditionStockFlag"].HeaderText = "بالاضافة الى رصيد";//col10
           // dataGridView1.Columns["NewTasnifFlag"].HeaderText = "تصنيف جديد";//col11

            dataGridView1.Columns["Eslah_No2"].HeaderText = "رقم طلب إصلاح";//col8
            dataGridView1.Columns["Eslah_No2"].Visible = false;

            //if (Constants.User_Type == "A")
            //{
            //    dataGridView1.Columns["ArrivalDate"].ReadOnly = true;
            //}
        }

            public bool SearchTalb(string talbNo, string fyear, bool isCompleted = false)
            {
                //call sp that get last num that eentered for this MM and this YYYY
                Constants.opencon();

                // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
                string cmdstring;
                SqlCommand cmd;

                if (isCompleted)
                {
                    cmdstring = "select * from  T_TalbEslah where Eslah_No2=@TN and FYear=@FY";
                }
                else
                {
                    cmdstring = "select * from  T_TalbEslah where Eslah_No=@TN and FYear=@FY";
                }

                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", talbNo);
                cmd.Parameters.AddWithValue("@FY", fyear);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    try
                    {
                        while (dr.Read())
                        {

                            TXT_TalbNo.Text = dr["Eslah_No"].ToString();
                            TXT_TalbNo2.Text = dr["Eslah_No2"].ToString();
                            TXT_DateMohmat.Text = dr["TaamenDate"].ToString();

                            if (Convert.ToBoolean(dr["TaamenFlag"].ToString()) == true)
                            {
                                RadioBTN_Tammen1.Checked = true;
                                RadioBTN_Taamen2.Checked = false;
                            }
                            else if (Convert.ToBoolean(dr["TaamenFlag"].ToString()) == false)
                            {
                                RadioBTN_Tammen1.Checked = false;
                                RadioBTN_Taamen2.Checked = true;
                               

                            }
                        TXT_DateMohmat.Text = (dr["TaamenDate"].ToString());
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
                            TXT_RecommendedCompanies.Text = dr["Country"].ToString();

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

                            SetCurrentActivatedBuyMethod(panel8, BUM);
                            Cmb_FYear.Text = dr["FYear"].ToString();

                            //talbstatus = Constants.GetTalbStatus(TXT_TalbNo.Text, Cmb_FYear.Text);
                            ////MessageBox.Show("talb status is" + talbstatus.ToString());
                            ///////////////////////////////////////

                            if (s1 != "")
                            {
                                string p = Constants.RetrieveSignature("1", "8", s1);

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
                                string p = Constants.RetrieveSignature("2", "8", s2);
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
                                string p = Constants.RetrieveSignature("3", "8", s3);
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
                                string p = Constants.RetrieveSignature("4", "8", s4);
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
                                string p = Constants.RetrieveSignature("5", "8", s5);
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
                                string p = Constants.RetrieveSignature("6", "8", s6);
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
                                string p = Constants.RetrieveSignature("7", "8", s7);
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
                                string p = Constants.RetrieveSignature("8", "8", s8);
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
                                string p = Constants.RetrieveSignature("9", "8", s9);
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
                                string p = Constants.RetrieveSignature("11", "8", s11);
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
                                string p = Constants.RetrieveSignature("12", "8", s12);
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
                }
                else
                {
                    MessageBox.Show("من فضلك تاكد من رقم طلب الاصلاح");
                    reset();
                    return false;
                }

                dr.Close();

                GetTalbTawreedBnod(talbNo, fyear);
                Constants.closecon();

                return true;
            }

        #endregion

        //------------------------------------------ State Handler ---------------------------------
        #region State Handler

        private void changePanelState(Panel panel, bool state) {
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
        
        private void HandleNewTasnifState()
        {
         

            //dataViewre sec
            TXT_StockBian.Text = "";
        
            Txt_ReqQuan.Text = "";
            CMB_Unit.Text = "";
            CMB_ApproxValue.Text = "";
           

            TXT_StockBian.Enabled = true;
            CMB_Unit.Enabled = true;
        }

        private void HandleExistTasnifState()
        {
            //reset to default
           // changePanelState(panel12, true);

            //dataViewre sec
            TXT_StockBian.Text = "";
            Txt_ReqQuan.Text = "";
            CMB_Unit.Text = "";
            CMB_ApproxValue.Text = "";

            TXT_StockBian.Enabled = false;
            CMB_Unit.Enabled = false;
        }

        public void PrepareAddState()
        {
            //Search sec
           // changePanelState(panel12, true);

            //dataViewre sec
            changePanelState(panel11, true);
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
           // checkedListBox1.SelectionMode = SelectionMode.One;
            RadioBTN_Taamen2.Checked = true;

            //mowazna
            changePanelState(panel6, false);

            //redirect sec
            changePanelState(panel9, false);

            //btn Section
            //generalBtn
            SaveBtn.Enabled = true;
            BTN_Cancel.Enabled = true;
            Addbtn2.Enabled = true;
            browseBTN.Enabled = true;
            BTN_PDF.Enabled = true;
            Addbtn.Enabled = false;
            Editbtn2.Enabled = false;
            BTN_SearchTalb.Enabled = false;
            BTN_Print.Enabled = false;

            //new tasnif
            CHK_NewTasnif.Enabled = true;

            //signature btn
            changePanelState(panel13, false);
            BTN_Sign1.Enabled = true;

            //moshtrayat types
            EnableMoshtryat();
            radioButton1.Checked = true;

            Pic_Sign1.Image = null;
            FlagSign1 = 0;
            Pic_Sign1.BackColor = Color.Green;
            currentSignNumber = 1;

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
        }

        public void PrepareEditState()
        {
            PrepareAddState();

            Cmb_FYear.Enabled = false;
            TXT_TalbNo.Enabled = false;

            Pic_Sign1.Image = null;
            FlagSign1 = 0;
            Pic_Sign1.BackColor = Color.Green;
            currentSignNumber = 1;

            AddEditFlag = 1;
            TNO = TXT_TalbNo.Text;
            FY = Cmb_FYear.Text;
        }

        public void PrepareConfirmState()
        {
            DisableControls();
            BTN_Save2.Enabled = true;
            browseBTN.Enabled=true;
            BTN_PDF.Enabled=true;

            if (Constants.User_Type == "A")
            {
                if (FlagSign2 != 1 && FlagSign1 == 1)
                {
                    BTN_Sign2.Enabled = true;
                    Pic_Sign2.BackColor = Color.Green;
                    currentSignNumber = 2;
                }
                else if(FlagSign3!=1 && FlagSign2 == 1)
                {
                    BTN_Sign3.Enabled = true;
                    DeleteBtn2.Enabled = true;

                    Pic_Sign3.BackColor = Color.Green;
                    currentSignNumber = 3;
                }
            }
            else if(Constants.User_Type == "B")
            {
                if (Constants.UserTypeB == "ChangeTasnif" || Constants.UserTypeB == "NewTasnif")
                {
                    dataGridView1.ReadOnly = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            row.Cells[i].ReadOnly = true;
                        }
                    }

                    BTN_Sign8.Enabled = true;

                    Pic_Sign8.BackColor = Color.Green;
                    currentSignNumber = 8;
                }
                else if (Constants.UserTypeB == "InventoryControl")
                {
                    BTN_Sign12.Enabled = true;

                    Pic_Sign12.BackColor = Color.Green;
                    currentSignNumber = 12;
                }
                else if (Constants.UserTypeB == "Mwazna")
                { 
                    if (FlagSign4 != 1 && FlagSign3 == 1)
                    {
                        BTN_Sign4.Enabled = true;

                        Pic_Sign4.BackColor = Color.Green;
                        currentSignNumber = 4;
                    }

                    else if (FlagSign11 != 1 && FlagSign4 == 1)
                    {
                        BTN_Sign11.Enabled = true;

                        Pic_Sign11.BackColor = Color.Green;
                        currentSignNumber = 11;
                    }

                    TXT_BndMwazna.Enabled = true;
                }
                else if (Constants.UserTypeB == "TechnicalFollowUp")
                {
                    BTN_Sign9.Enabled = true;

                    Pic_Sign9.BackColor = Color.Green;
                    currentSignNumber = 9;
                }
                else if (Constants.UserTypeB == "Chairman")
                {
                    BTN_Sign7.Enabled = true;
                    BTN_Sign10.Enabled = true;

                    Pic_Sign7.BackColor = Color.Green;
                    currentSignNumber = 7;
                }
                else if (Constants.UserTypeB == "ViceChairman" && talbstatus == 2)
                {
                    BTN_Sign13.Enabled = true;

                    Pic_Sign13.BackColor = Color.Green;
                    currentSignNumber = 13;
                }
                else if (Constants.UserTypeB == "Purchases")
                {
                    EnableMoshtryat();
                    BTN_Sign5.Enabled = true;
                    TXT_AppValue.Enabled = true;
                    TXT_ArabicValue.Enabled = true;

                    Pic_Sign5.BackColor = Color.Green;
                    currentSignNumber = 5;
                    TXT_DateMohmat.Enabled = true;
                }
                else if (Constants.UserTypeB == "GMInventory")
                {
                    BTN_Sign6.Enabled = true;
                    TXT_AppValue.Enabled = true;
                    TXT_ArabicValue.Enabled = true;

                    Pic_Sign6.BackColor = Color.Green;
                    currentSignNumber = 6;
                }
            }

            AddEditFlag = 1;
            TNO = TXT_TalbNo.Text;
            FY = Cmb_FYear.Text;        
        }

        public void prepareSearchState()
        {
            DisableControls();
            Input_Reset();
            Cmb_FYear.Enabled = true;
            TXT_TalbNo.Enabled = true;
            BTN_Print.Enabled=true;
            TXT_TalbNo2.Enabled = false;
            Cmb_Currency.Enabled = false;
        }

        public void reset()
        {
            prepareSearchState();
        }

        public void DisableControls()
        {
            //Search sec
            //changePanelState(panel12, false);

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

            //datemohmat sec
            changePanelState(panel1, false);
            //btn Section
            //generalBtn
            Addbtn.Enabled = true;
            BTN_SearchTalb.Enabled = true;
            SaveBtn.Enabled = false;
            BTN_Save2.Enabled = false;
            Editbtn.Enabled = false;
            BTN_Cancel.Enabled = false;
            Addbtn2.Enabled = false;
            browseBTN.Enabled = false;
            BTN_PDF.Enabled = false;
            Editbtn2.Enabled = false;
            BTN_Print.Enabled = false;
            BTN_Print2.Enabled = false;

            //new tasnif
            CHK_NewTasnif.Enabled = false;

            //signature btn
            changePanelState(panel13, false);

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //moshtrayat types
            DisableMoshtryat();
        }

        public void EnableMoshtryat()
        {
            changePanelState(panel8, true);
        }

        public void DisableMoshtryat()
        {
            changePanelState(panel8, false);
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

            currentSignNumber = 0;
        }

        public void Input_Reset()
        {
     
            //dataViewre sec
            TXT_StockBian.Text = "";
       
            Txt_ReqQuan.Text = "";
            CMB_Unit.Text = "";
            CMB_Unit.SelectedIndex = -1;

            CMB_ApproxValue.Text = "";
            CMB_ApproxValue.SelectedIndex = -1;
        


            //fyear sec
            Cmb_FYear.Text = "";
            Cmb_FYear.SelectedIndex = -1;

            TXT_TalbNo.Text = "";
            TXT_TalbNo2.Text = "";
            Cmb_Currency.SelectedIndex = 0;

            //bian edara sec
            TXT_Edara.Text = "";
            TXT_ReqFor.Text = "";
            TXT_Date.Value = DateTime.Today;

            //aprrox value
            TXT_AppValue.Text = "";
            TXT_ArabicValue.Text = "";
            TXT_PriceSarf.Text = "";

            //ta2men 5%      
            RadioBTN_Tammen1.Checked = false;
            RadioBTN_Taamen2.Checked = false;
            TXT_RecommendedCompanies.Text = "";
            TXT_Tamen.Text = "";

            TXT_DateMohmat.Value = DateTime.Today;

            //mowazna
            TXT_BndMwazna.Text = "";

            //redirect sec
            TXT_RedirectedFor.Text = "";
            TXT_RedirectedDate.Text = "";


            //search sec
            Cmb_FYear2.Text = "";
            Cmb_FYear2.SelectedIndex = -1;

            Cmb_TalbNo2.Text = "";
            Cmb_TalbNo2.SelectedIndex = -1;

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
            AddEditFlag = 0;

            sum = 0;

        }
        #endregion

        //------------------------------------------ Logic Handler ---------------------------------
        #region Logic Handler
        private void AddLogic()
        {
            Constants.opencon();

            string cmdstring = @"Exec SP_InsertTalbEslah @TalbTwareed_No,@FYear,@CreationDate,@CodeEdara,@NameEdara,
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

            cmd.Parameters.AddWithValue("@NeedTestsFlag", DBNull.Value);
            cmd.Parameters.AddWithValue("@NeedAnalysisFlag", DBNull.Value);
            cmd.Parameters.AddWithValue("@OriginFlag", DBNull.Value);

          
            cmd.Parameters.AddWithValue("@Country", TXT_RecommendedCompanies.Text);
                 
            if (RadioBTN_Tammen1.Checked == true)
            {
                cmd.Parameters.AddWithValue("@TaamenFlag", RadioBTN_Tammen1.Checked);
                cmd.Parameters.AddWithValue("@TaamenDate", DBNull.Value);
            }
            else if (RadioBTN_Taamen2.Checked == true)
            {
                cmd.Parameters.AddWithValue("@TaamenFlag", RadioBTN_Tammen1.Checked);
                cmd.Parameters.AddWithValue("@TaamenDate", Convert.ToDateTime(TXT_DateMohmat.Text.ToString()));
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

                /////////////////////// int[] sequence = { 1, 2, 3, 8,12,4,11,5,6 };
                ///
                ///i removed 8 and 12
                int[] sequence = { 1, 2, 3, 4, 11, 5, 6 };
                for (int i = 0; i < sequence.Length; i++)
                {

                    if (i != 10)
                    {
                        cmdstring = "Exec  SP_InsertSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
                        cmd = new SqlCommand(cmdstring, Constants.con);

                        cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_TalbNo.Text));
                        cmd.Parameters.AddWithValue("@TNO2", DBNull.Value);

                        cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
                        cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
                        cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
                        cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);

                        cmd.Parameters.AddWithValue("@FN", 8);

                        cmd.Parameters.AddWithValue("@SN", sequence[i]);

                        cmd.Parameters.AddWithValue("@D1", DBNull.Value);

                        cmd.Parameters.AddWithValue("@D2", DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }


                SP_UpdateSignatures(1, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                //////////////////////////////////////////////////////////////////
/*
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
                }*/

                MessageBox.Show("تم الإضافة بنجاح  ! ");

                reset();
            }
            else if (executemsg == true && flag == 2)
            {
                MessageBox.Show("تم إدخال رقم طلب الاصلاح  من قبل  ! ");
            }
            else if (executemsg == false)
            {
                MessageBox.Show("لم يتم إدخال طلب الاصلاح بنجاج!!");
            }
            Constants.closecon();
        }

        private void UpdateTalbTawreedTSignatureCycle()
        {
            if (currentSignNumber == 2 && FlagSign2 == 1)
            {
                SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                SP_UpdateSignatures(3, Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            }

            if (currentSignNumber == 3 && FlagSign3 == 1)
            {

                SP_UpdateSignatures(3, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                //  SP_UpdateSignatures(8, Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            }
          /*  if (currentSignNumber == 8 && FlagSign8 == 1)
            {

                SP_UpdateSignatures(8, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                SP_UpdateSignatures(12, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

            }
            if (currentSignNumber == 12 && FlagSign12 == 1)
            {

                SP_UpdateSignatures(12, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

            }*/
            if (currentSignNumber == 4 && FlagSign4 == 1)
            {

                SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                SP_UpdateSignatures(11, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

            }
            if (currentSignNumber == 11 && FlagSign11 == 1)
            {

                SP_UpdateSignatures(11, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                SP_UpdateSignatures(9, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

            }
            if (currentSignNumber == 5 && FlagSign5 == 1)
            {

                SP_UpdateSignatures(5, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

            }
            if (currentSignNumber == 6 && FlagSign6 == 1)
            {

                SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                // SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

            }
            
            if (Constants.AuthFlag == 3 || Constants.AuthFlag == 4)//ra2es 4erka
            {
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
            }
            else if (Constants.AuthFlag == 2) //mosa3ed ra2es 4erka
            {
                if (FlagSign13 == 1)
                {

                    SP_UpdateSignatures(13, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    SP_UpdateSignatures(5, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }
            }

        }
        
        private void UpdateTalbTawreedStepsAndNotification()
        {
            SqlCommand cmd,cmd1;
            int flag;
            /*
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
            }*/
            if (FlagSign6 == 1 && Constants.UserTypeB == "GMInventory")
            {

                string q = "exec  SP_UpdateTalbEslahNo2  @p1,@p2,@p22,@p3 out";
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
                    MessageBox.Show("تم اصدار رقم نهائى لطلب الاصلاح بنجاح و هو " + Talbno2.ToString());
                    //Input_Reset();
                }
                Constants.closecon();



            }
            if (FlagSign5 == 1 && Constants.UserTypeB == "Purchases")
            {

                //MessageBox.Show("تم الانتهاء من طلب التوريد بنجاح ");


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

            if (FlagSign5 == 1)
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
                    //MessageBox.Show("flag number is" + flag);

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
                    //MessageBox.Show("flag number2 is" + flag2);
                    if (flag2 == 1)
                    {
                        //go and update flag9 and flag7 and set =1
                        //MessageBox.Show("next step is mohmat");

                        string q = "exec SP_UpdateTalbEslahAuthority  @p1,@p2,@p3";
                        SqlCommand cmd3 = new SqlCommand(q, Constants.con);
                        cmd3.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_TalbNo.Text));
                        cmd3.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd3.Parameters.AddWithValue("@p3", flag2);
                        cmd3.ExecuteNonQuery();

                    }
                    else if (flag2 == 2)
                    {
                        //change in notfication go and set flag9=1 and make flag7 for vice not for manger
                        //MessageBox.Show("next step is vice");
                        string q = "exec SP_UpdateTalbEslahAuthority  @p1,@p2,@p3";
                        SqlCommand cmd3 = new SqlCommand(q, Constants.con);
                        cmd3.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_TalbNo.Text));
                        cmd3.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd3.Parameters.AddWithValue("@p3", flag2);
                        cmd3.ExecuteNonQuery();
                    }
                    else if (flag2 == 3)
                    {
                        //notification will go normal
                        //MessageBox.Show("nextstep is r2es sherka");
                        string q = "exec SP_UpdateTalbEslahAuthority  @p1,@p2,@p3";
                        SqlCommand cmd3 = new SqlCommand(q, Constants.con);
                        cmd3.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_TalbNo.Text));
                        cmd3.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd3.Parameters.AddWithValue("@p3", flag2);
                        cmd3.ExecuteNonQuery();
                    }
                    else if (flag2 == 4)
                    {
                        //notfication will go normal
                        //MessageBox.Show("next step is mgls edara");
                        string q = "exec SP_UpdateTalbEslahAuthority  @p1,@p2,@p3";
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
         //////////////   LoopGridview();
         /*
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
         */

            /////////////////////////////////////////////
            string cmdstring = @"Exec SP_UpdateTalbEslah @TT,@FY,@TalbTwareed_No,@TalbTwareed_No2,@FYear,@CreationDate,@CodeEdara,
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

            cmd.Parameters.AddWithValue("@NeedTestsFlag", DBNull.Value);
            cmd.Parameters.AddWithValue("@NeedAnalysisFlag", DBNull.Value);
            cmd.Parameters.AddWithValue("@OriginFlag", DBNull.Value);

          
            cmd.Parameters.AddWithValue("@Country", TXT_RecommendedCompanies.Text);

            if (RadioBTN_Tammen1.Checked == true)
            {
                cmd.Parameters.AddWithValue("@TaamenFlag", RadioBTN_Tammen1.Checked);
                //cmd.Parameters.AddWithValue("@TaamenDate", DBNull.Value);
            }
            else if (RadioBTN_Taamen2.Checked == true)
            {
                cmd.Parameters.AddWithValue("@TaamenFlag", RadioBTN_Tammen1.Checked);
                //cmd.Parameters.AddWithValue("@TaamenDate", Convert.ToDateTime(TXT_DateMohmat.Text.ToString()));
            }
            cmd.Parameters.AddWithValue("@TaamenDate", Convert.ToDateTime(TXT_DateMohmat.Text.ToString()) );
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
            }
            else if (executemsg == true && flag == 2)
            {
                MessageBox.Show("طلب إصلاح المراد تعديله غير موجود !!");
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

                        string q = "exec SP_UpdateTalbEslahAuthority  @p1,@p2,@p3";
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
                        string q = "exec SP_UpdateTalbEslahAuthority  @p1,@p2,@p3";
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
                        string q = "exec SP_UpdateTalbEslahAuthority  @p1,@p2,@p3";
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
                        string q = "exec SP_UpdateTalbEslahAuthority  @p1,@p2,@p3";
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
                    Console.WriteLine(sqlEx);
                }
                cmd1.ExecuteNonQuery();


            }

        }

        public bool DeleteTalb()
        {

            if ((MessageBox.Show("هل تريد حذف طلب الاصلاح؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(TXT_TalbNo.Text))
                {
                    MessageBox.Show("يجب اختيار طلب الاصلاح اولا");
                    return false;
                }
                Constants.opencon();
          /*    string cmdstring1 = "select STOCK_NO_ALL,AdditionStockFlag,Bnd_No from T_TalbTawreed_Benod where FYear=@FY and TalbTwareed_No=@TNO";
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

                */
                string cmdstring = "Exec SP_DeleteTalbEslah @TNO,@FY,@aot output";

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
                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    Console.WriteLine(sqlEx);                    
                }

                flag = (int)cmd.Parameters["@aot"].Value;
                Constants.closecon();

                if (executemsg == true && flag == 1)
                {
                    MessageBox.Show("تم الحذف بنجاح");
                    return true;
                }
                else
                {
                    MessageBox.Show("لم يتم الحذف");
                    return false;
                }
            }
            return false;
        }

        #endregion

        //------------------------------------------ Validation Handler ---------------------------------
        #region Validation Handler
        private List<(ErrorProvider, Control, string)> ValidateAddTasnif(bool isNewTasnif = false)
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

            if (!isNewTasnif)
            {
               
            }

            #region Txt_ReqQuan
            if (string.IsNullOrWhiteSpace(Txt_ReqQuan.Text))
            {
                errorsList.Add((errorProvider, Txt_ReqQuan, "يجب ادخال الكمية المطلوبة"));
            }
            else if (!string.IsNullOrWhiteSpace(Txt_ReqQuan.Text) && Convert.ToDecimal(Txt_ReqQuan.Text) <= 0)
            {
                errorsList.Add((alertProvider, Txt_ReqQuan, "يجب ان تكون الكمية المطلوبة اكبر من صفر"));
            }
           

            #endregion

            #region CMB_ApproxValue
            if (string.IsNullOrWhiteSpace(CMB_ApproxValue.Text))
            {
                errorsList.Add((errorProvider, CMB_ApproxValue, "يجب اختيار القيمة التقديرية "));
            }
            else if (Convert.ToDecimal(getApproxValue()) <= 0)
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
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells[5].Value.ToString().ToLower() == TXT_StockBian.Text.ToLower() && TXT_StockBian.Text != "")
                        {
                            errorsList.Add((alertProvider, TXT_StockBian, "تم ادخال مواصفة هذا التصنيف من قبل"));

                            break;
                        }
                    }
                }
                #endregion

                #region Unit
                if (string.IsNullOrWhiteSpace(CMB_Unit.Text))
                {
                    errorsList.Add((errorProvider, CMB_Unit, "يجب ادخال وحدة للتصنيف الجديد"));
                }
                #endregion                     
            }

            return errorsList;
        }

        private List<(ErrorProvider, Control, string)> ValidateAttachFile()
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

            #region Cmb_FYear
                if (string.IsNullOrWhiteSpace(Cmb_FYear.Text) || Cmb_FYear.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_FYear, "تاكد من  اختيار السنة المالية"));
                }
            #endregion
            #region TXT_TalbNo
                if (string.IsNullOrWhiteSpace(TXT_TalbNo.Text))
                {
                    errorsList.Add((errorProvider, TXT_TalbNo, "يجب اختيار رقم طلب إصلاح"));
                }
            #endregion

            return errorsList;
        }

        private List<(ErrorProvider, Control, string)> ValidateSearch(bool isConfirm = false)
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

        if (isConfirm)
        {
            #region Cmb_FYear2
            if (string.IsNullOrWhiteSpace(Cmb_FYear2.Text) || Cmb_FYear2.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_FYear2, "تاكد من  اختيار السنة المالية"));
            }
            #endregion

            #region Cmb_TalbNo2
            if (string.IsNullOrWhiteSpace(Cmb_TalbNo2.Text) || Cmb_TalbNo2.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_TalbNo2, "يجب اختيار رقم طلب الاصلاح"));
            }
            #endregion
        }
        else
        {
            #region Cmb_FYear
            if (string.IsNullOrWhiteSpace(Cmb_FYear.Text) || Cmb_FYear.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_FYear, "تاكد من  اختيار السنة المالية"));
            }
            #endregion

            #region TXT_TalbNo
            if (string.IsNullOrWhiteSpace(TXT_TalbNo.Text))
            {
                errorsList.Add((errorProvider, TXT_TalbNo, "يجب اختيار رقم طلب الاصلاح"));
            }
            #endregion
        }

            return errorsList;
        }

        private List<(ErrorProvider, Control, string)> ValidateSave()
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

            #region Cmb_FYear
            if (string.IsNullOrWhiteSpace(Cmb_FYear.Text) || Cmb_FYear.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_FYear, "تاكد من  اختيار السنة المالية"));
            }
            #endregion

            #region RadioBTN_Tammen1 || RadioBTN_Taamen2
            if (RadioBTN_Tammen1.Checked == false && RadioBTN_Taamen2.Checked == false)
            {
                errorsList.Add((errorProvider, RadioBTN_Tammen1, "تاكد من  اختيار نوع تأمين"));
            }
            #endregion

            #region Buy Method
            if (GetCurrentActivatedBuyMethod(panel8) == -1)
            {
                errorsList.Add((errorProvider, panel8, "تاكد من  اختيار طريقة شراء"));
            }
            #endregion
            if (RadioBTN_Tammen1.Checked == true)
            {
                if (string.IsNullOrWhiteSpace(TXT_RecommendedCompanies.Text))
                {
                    errorsList.Add((errorProvider, TXT_RecommendedCompanies, "تاكد من  اختيار اسماء الشركات"));
                }
            }
            #region dataGridView1
            if (dataGridView1.Rows.Count <=0)
            {
                //errorsList.Add((errorProvider, dataGridView1, "لايمكن ان يتكون طلب توريد بدون بنود"));
                MessageBox.Show("لايمكن ان يتكون طلب اصلاح بدون بنود");
            }
            else if (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow == true)
            {
                //errorsList.Add((errorProvider, dataGridView1, "لايمكن ان يتكون طلب توريد بدون بنود"));
                MessageBox.Show("لايمكن ان يتكون طلب اصلاح بدون بنود");
            }
            #endregion

            if (((PictureBox)this.panel13.Controls["Pic_Sign" + currentSignNumber]).Image == null)
            {
                errorsList.Add((errorProvider, ((PictureBox)this.panel13.Controls["Pic_Sign" + currentSignNumber]), "تاكد من التوقيع"));
            }

            return errorsList;
        }

        private bool IsValidCase(VALIDATION_TYPES type)
            {
                List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();
            
                if (type == VALIDATION_TYPES.ADD_TASNIF)
                {
                    errorsList = ValidateAddTasnif(false);
                }
                else if(type == VALIDATION_TYPES.ADD_NEW_TASNIF)
                {
                    errorsList = ValidateAddTasnif(true);
                }
                else if(type == VALIDATION_TYPES.ATTACH_FILE)
                {
                    errorsList = ValidateAttachFile();
                }
                else if(type == VALIDATION_TYPES.SEARCH)
                {
                    errorsList = ValidateSearch(false);
                }
                else if (type == VALIDATION_TYPES.CONFIRM_SEARCH)
                {
                    errorsList = ValidateSearch(true);
                }
                else if (type == VALIDATION_TYPES.SAVE)
                {
                    errorsList = ValidateSave();
                }

                errorProviderHandler(errorsList);

                if (errorsList.Count > 0)
                {                  
                    return false;
                }

                return true;
            }
        #endregion

        public TalbEslah()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        public TalbEslah(string x, string y)
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

            HelperClass.comboBoxFiller(CMB_Unit, UnitsHandler.getUnits(), "eng_unit", "eng_unit", this);



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
            if (Constants.talbEslah_F == false)
            {
                panel7.Visible = true;
                panel2.Visible = false;
                panel7.Dock = DockStyle.Top;
            }
            else if (Constants.talbEslah_F == true)
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
            string cmdstring3 = "SELECT [Eslah_No] from T_TalbEslah where CodeEdara=" + Constants.CodeEdara + " and  FYear='" + Cmb_FYear.Text + "'";
            SqlCommand cmd3 = new SqlCommand(cmdstring3, Constants.con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            //---------------------------------
            if (dr3.HasRows == true)
            {
                while (dr3.Read())
                {
                    TalbColl.Add(dr3["Eslah_No"].ToString());

                }
            }
            dr3.Close();
            ///////////////////
            string cmdstring4 = "SELECT [Eslah_No2] from T_TalbEslah where CodeEdara=" + Constants.CodeEdara + " and  FYear='" + Cmb_FYear.Text + "'";
            SqlCommand cmd4 = new SqlCommand(cmdstring4, Constants.con);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            //---------------------------------
            if (dr4.HasRows == true)
            {
                while (dr4.Read())
                {
                    TalbColl2.Add(dr4["Eslah_No2"].ToString());

                }
            }
            dr4.Close();


            //////////////////////////
          

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
                //SearchTalb(1);
                BTN_Print.Visible = true;

            }
            Constants.closecon();
            Cmb_FYear2.SelectedIndex = -1;
            Cmb_FYear.SelectedIndex = -1;
            reset();

            if (Constants.talbtawred_F == false)
            {
                TXT_TalbNo.Enabled = false;
                Cmb_FYear.Enabled=false;
            }
        }
        //===========================================================================

      

        private void cleargridview()
        {
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

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


       

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد اضافة طلب إصلاح جديد؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                reset();
                PrepareAddState();
                AddEditFlag = 2;
                TXT_Edara.Text = Constants.NameEdara;
            }
        }

        #region AddTasnif

        private void HandleTasnifALreadyExistInMaster()
        {
            Currency = Cmb_Currency.Text;

           

        

            AddNewTasnifInDataGridView();
        }

        private void HandleNewTasnid()
        {
            Currency = Cmb_Currency.Text;
            NewTasnifFlag = 1;

            AddNewTasnifInDataGridView(NewTasnifFlag);
        }

        private void AddTasnifToDataGridView(bool isNew)
        {
            if (isNew)
            {
                HandleNewTasnid();
            }
            else
            {               
                HandleTasnifALreadyExistInMaster();
            }
        }
        
        private void Addbtn2_Click(object sender, EventArgs e)
        {
            VALIDATION_TYPES type;

            type = CHK_NewTasnif.Checked ? VALIDATION_TYPES.ADD_NEW_TASNIF:VALIDATION_TYPES.ADD_TASNIF;
               
            if (!IsValidCase(type))
            {
                return;
            }

            AddTasnifToDataGridView(CHK_NewTasnif.Checked);
        }

        private void CHK_NewTasnif_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_NewTasnif.Checked == true)
            {
                HandleNewTasnifState();
                Addbtn2.Text = "إضافة التصنيف الى طلب إصلاح مهمات";
            }
            else
            {
                HandleExistTasnifState();
                Addbtn2.Text = "إضافة التصنيف الى طلب إصلاح";
            }
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
                    cmdstring3 = "SELECT [Eslah_No] from T_TalbEslah where CodeEdara=" + Constants.CodeEdara + " and  FYear='" + Cmb_FYear.Text + "'";

                }
                else
                {
                    cmdstring3 = "SELECT [Eslah_No] from T_TalbEslah where  FYear='" + Cmb_FYear.Text + "'";

                }
                SqlCommand cmd3 = new SqlCommand(cmdstring3, Constants.con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                //---------------------------------
                if (dr3.HasRows == true)
                {
                    while (dr3.Read())
                    {
                        TalbColl.Add(dr3["Eslah_No"].ToString());

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

                if (TXT_TalbNo.Text != "")
                {
                    return;
                }
                //call sp that get last num that eentered for this MM and this YYYY
                Constants.opencon();

                // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
                //  string cmdstring = "select max(TalbTwareed_No) from  T_TalbTawreed where FYear=@FY ";
                string cmdstring = "select ( COALESCE(MAX(Eslah_No), 0)) from  T_TalbEslah where FYear=@FY ";

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
                        string cmdstring2 = "select ( COALESCE(MAX(Eslah_No), 0)) from  T_TempEslahNo where FYear=@FY ";

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
                        string query = "exec SP_InsertTempEslahNo @p1,@p2";
                        SqlCommand cmd1 = new SqlCommand(query, Constants.con);
                        cmd1.Parameters.AddWithValue("@p1", flag);
                        cmd1.Parameters.AddWithValue("@p2", Cmb_FYear.Text);



                        Constants.opencon();
                        cmd1.ExecuteNonQuery();

                        ///////////////////////////
                        TXT_TalbNo.Text = flag.ToString();//el rakm el new
                        if (AddEditFlag == 2)
                        {
                            GetTalbTawreedBnod(TXT_TalbNo.Text, Cmb_FYear.Text);

                        }


                    }

                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    Console.WriteLine(sqlEx);
                    // flag = (int)cmd.Parameters["@Num"].Value;
                }
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.SAVE))
            {
                return;
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
            }

            reset();
        }



        private void Editbtn_Click_1(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد تعديل طلب إصلاح ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_TalbNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text))
                {
                    MessageBox.Show("يجب اختيار طلب إصلاح المراد تعديله");
                    return;
                }

                PrepareConfirmState();
            }
        }

        private void Cmb_FYear2_SelectedIndexChanged(object sender, EventArgs e)
        {

            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            string cmdstring = "";
            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            if (Constants.RedirectedFlag == 1)
            {
                cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and( Stock_Sign is not null) and (Sign9 is  not null) and( CH_Sign is not  null) and Audit_Sign is not null and RedirectedFor='" + Constants.FlagRedirectEmpn + "'";

            }
            else if (Constants.User_Type == "A")
            {
                cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and CodeEdara=@CE  and(( Confirm_Sign1 is  null) or( Confirm_Sign2 is  null)) ";

            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Stock")
            {
                cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and( Stock_Sign is not null) and (Sign9 is not  null) and CH_Sign is not null and (Audit_Sign is not null) and (Mohmat_Sign is null)";

            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Purchases")
            {
                cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and (Sign11 is not null )  and( Stock_Sign is not  null) and (Audit_Sign is null)";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "GMInventory")
            {
                cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)   and (Sign11 is not null ) and( Stock_Sign is not null) and (Sign9 is not  null) and CH_Sign is not null and (Audit_Sign is not null )and Mohmat_Sign is null";

            }
            /*
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "NewTasnif")
            {
                cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is null ) ";

            }
            */
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Mwazna")
            {
                cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and (Sign11 is null or Stock_Sign is null)";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "TechnicalFollowUp")
            {
                cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)    and (Sign11 is not null )    and( Stock_Sign is not null) and (Audit_Sign is not null ) and (Sign9 is null)";
            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Chairman")
            {
               // cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null )  and (Sign11 is not null )and (Sign12 is not null )  and( Stock_Sign is not null) and (Sign9 is  not null) and( CH_Sign is null)";
                cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)    and (Sign11 is not null ) and( Stock_Sign is not null) and (Sign9 is  not null) and( CH_Sign is null)";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "ViceChairman")
            {
                cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)   and (Sign11 is not null ) and( Stock_Sign is not null)  and( Sign13 is null)";

            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Purchases")
            {
                cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and (Sign11 is not null )  and( Stock_Sign is not null) and (Sign9 is  not null) and( CH_Sign is not  null) and Audit_Sign is null";

            }
            /*
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "InventoryControl")
            {
                cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and( Confirm_Sign1 is not null) and( Confirm_Sign2 is not null)  and(Sign8 is not null ) and Sign12 is null ";
            }
            */
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "ChangeTasnif")
            {
                cmdstring = "select (Eslah_No) from  T_TalbEslah where FYear=@FY and( Mohmat_Sign is not null)  ";

            }
            //string cmdstring = "select (TalbTwareed_No) from  T_TalbTawreed where FYear=@FY and CodeEdara=@CE  ";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
            cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);


            DataTable dts = new DataTable();

            dts.Load(cmd.ExecuteReader());
            Cmb_TalbNo2.DataSource = dts;
            Cmb_TalbNo2.ValueMember = "Eslah_No";
            Cmb_TalbNo2.DisplayMember = "Eslah_No";
            Cmb_TalbNo2.SelectedIndex = -1;
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.Pic_Sign1, "My button1");
            toolTip1.SetToolTip(this.Pic_Sign2, Ename2 + Environment.NewLine + wazifa2);
            Constants.closecon();

        }



        private void BTN_Save2_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.SAVE))
            {
                return;
            }

            UpdateTalbTawreed();

            reset();

            Cmb_TalbNo2.SelectedIndex = -1;
            Cmb_FYear2.SelectedIndex = -1;

            TXT_TalbNo.Enabled = false;
            Cmb_FYear.Enabled = false;
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
       

        private void BTN_Print_Click(object sender, EventArgs e)
        {
              if (string.IsNullOrEmpty(TXT_TalbNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text))
                {
                    MessageBox.Show("يجب اختيار طلب الاصلاح المراد طباعتها اولا");
                    return;
                }
                else
                {

                    Constants.TalbFY = Cmb_FYear.Text;
                    Constants.TalbNo = Convert.ToInt32(TXT_TalbNo.Text);
                    Constants.FormNo = 13;
                    FReports f = new FReports();
                    f.Show();
                }
            }
    
        private void BTN_Print2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TXT_TalbNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text))
                {
                    MessageBox.Show("يجب اختيار طلب الاصلاح المراد طباعتها اولا");
                    return;
                }
                else
                {

                    Constants.TalbFY = Cmb_FYear.Text;
                    Constants.TalbNo = Convert.ToInt32(TXT_TalbNo.Text);
                    Constants.FormNo = 13;
                    FReports f = new FReports();
                    f.Show();
                }
        }

     

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex >= 0 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                if (e.ColumnIndex == 6 && (bool)dataGridView1.Rows[e.RowIndex].Cells[11].Value == true) // 1 should be your column index
                {

                    if (e.FormattedValue != DBNull.Value && e.FormattedValue != "")// && dataGridView1.Rows[e.RowIndex].Cells[11].Value != "true")

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

                            if (flag1 != 2)
                            {

                                //if (Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[7].Value) >= Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value))
                                //{
                                //    MessageBox.Show("كمية المطلوبة اقل من كمية المخزن لا نحناج الى طلب توريد");
                                //    return;
                                //}

                                if ((Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[7].Value) < Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value)) && Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[7].Value) != 0)
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

        private void BTN_Search_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                //   Constants.currentOpened.Close();
            }
            //----------------------
            SearchForm F = new SearchForm();
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

            dataGridView1.Columns["Eslah_No"].HeaderText = "رقم طلب االاصلاح";//col0
            dataGridView1.Columns["Eslah_No"].ReadOnly = true;
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
        /////    dataGridView1.Columns["STOCK_NO_ALL"].HeaderText = "الدليل الرقمى";//col6
          /////////  dataGridView1.Columns["STOCK_NO_ALL"].ReadOnly = true;

        /////////    dataGridView1.Columns["Quan"].HeaderText = "رصيد المخزن";//col7
        ///////    dataGridView1.Columns["Quan"].ReadOnly = true;
///
            
            dataGridView1.Columns["ArrivalDate"].HeaderText = "تاريخ وروده";//col6
            dataGridView1.Columns["ArrivalDate"].Visible = false;
            dataGridView1.Columns["ApproxValue"].HeaderText = "القيمة التقديرية";//col7
          //  dataGridView1.Columns["AdditionStockFlag"].HeaderText = "بالاضافة الى رصيد";//col10
        //    dataGridView1.Columns["AdditionStockFlag"].ReadOnly = true;
         //   dataGridView1.Columns["NewTasnifFlag"].HeaderText = "تصنيف جديد";//col11

         //   dataGridView1.Columns["NewTasnifFlag"].ReadOnly = true;
            dataGridView1.Columns["Eslah_No2"].HeaderText = "رقم طلب الاصلاح";//col8
            dataGridView1.Columns["Eslah_No2"].Visible = false;

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
            if (!IsValidCase(VALIDATION_TYPES.ATTACH_FILE))
            {
                return;
            }

            PDF_PopUp popup = new PDF_PopUp();

            if (AddEditFlag == 0)//search
            {
                //popup.TalbNo= TXT_TalbNo.Text;
                //popup.Fyear = Cmb_FYear.Text;
                //popup.CodeEdara = currentcodeedara;.

                popup.WholePath = @"\\172.18.8.83\MaterialAPP\PDF\" + currentcodeedara + @"\"+ Cmb_FYear.Text + @"\TALB_Eslah\"+ TXT_TalbNo.Text + @"\";
            }
            else//add or edit
            {
                //popup.TalbNo = TXT_TalbNo.Text;
                //popup.Fyear = Cmb_FYear.Text;
                //popup.CodeEdara = Constants.CodeEdara;

                popup.WholePath = @"\\172.18.8.83\MaterialAPP\PDF\" + Constants.CodeEdara + @"\" + Cmb_FYear.Text + @"\TALB_Eslah\" + TXT_TalbNo.Text + @"\";
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

        private void browseBTN_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.ATTACH_FILE))
            {
                return;
            }

            openFileDialog1.Filter = "PDF(*.pdf)|*.pdf";
            DialogResult dialogRes = openFileDialog1.ShowDialog();
            string ConstantPath = @"\\172.18.8.83\MaterialAPP\PDF\";//////////////////change it to server path

            foreach (String file in openFileDialog1.FileNames)
            {
                if (dialogRes == DialogResult.OK)
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


                    path += "TALB_TAWREED" + @"\";

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
                }
            }

            if (dialogRes == DialogResult.OK)
            {
                MessageBox.Show("تم إرفاق المرفقات");
            }
            else
            {
                MessageBox.Show("لم يتم إرفاق المرفقات");
            }
            
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioBTN_Taamen2.Checked == true)
            {
                RadioBTN_Tammen1.Checked = false;
                TXT_DateMohmat.Enabled = true;
            }
        }

        private void RadioBTN_Tammen1_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioBTN_Tammen1.Checked == true)
            {
                RadioBTN_Taamen2.Checked = false;
                TXT_DateMohmat.Enabled = false;
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

       

        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            AddEditFlag = 0;
            reset();
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
           // LoopGridview();

            string Empn1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على انشاء طلب إصلاح", "");

            string Sign1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على انشاء طلب إصلاح", "");

            if (Sign1 != "" && Empn1 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);


                Tuple<string, int, int, string, string> result = Constants.CheckSign("1", "8", Sign1, Empn1);
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
           // LoopGridview();
            if (FlagSign1 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            string Empn2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "تصديق على  طلب إصلاح", "");

            string Sign2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "تصديق على  طلب إصلاح", "");

            if (Sign2 != "" && Empn2 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("2", "8", Sign2, Empn2);
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
          //  LoopGridview();
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
                Tuple<string, int, int, string, string> result = Constants.CheckSign("3", "8", Sign3, Empn3);
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
                Tuple<string, int, int, string, string> result = Constants.CheckSign("3", "8", Sign3, Empn3);
                if (result.Item3 == 1)
                {
                    Pic_Sign3.Image = Image.FromFile(@result.Item1);

                    FlagSign3 = result.Item2;
                    FlagEmpn3 = Empn3;
                    if (DeleteTalb())
                    {
                        reset();
                    }
                }
                else
                {
                    FlagSign3 = 0;
                    FlagEmpn3 = "";
                }
            }
        }

        private void BTN_Sign8_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
           // LoopGridview();
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
                Tuple<string, int, int, string, string> result = Constants.CheckSign("8", "8", Sign8, Empn8);
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
                Tuple<string, int, int, string, string> result = Constants.CheckSign("12", "8", Sign12, Empn12);
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
           // LoopGridview();
            if (NewTasnifFlag == 1)
            {
                MessageBox.Show("لا يمكن التوقيع هناك تصنييفات جديدة");
                return;
            }
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1)
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
                Tuple<string, int, int, string, string> result = Constants.CheckSign("4", "8", Sign4, Empn4);
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
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 )
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
                Tuple<string, int, int, string, string> result = Constants.CheckSign("11", "8", Sign11, Empn11);
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
            if ((talbstatus == 3 || talbstatus == 4) && (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 ||  FlagSign9 != 1 || FlagSign7 != 1 ))
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }

            if ((talbstatus == 1) && (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 ))
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            if ((talbstatus == 2) && (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 ||  FlagSign13 != 1))
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
                Tuple<string, int, int, string, string> result = Constants.CheckSign("5", "8", Sign5, Empn5);
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
            if ((talbstatus == 3 || talbstatus == 4) && (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 ||  FlagSign9 != 1 || FlagSign7 != 1 || FlagSign5 != 1 ))
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            if ((talbstatus == 1) && (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 || FlagSign5 != 1 ))
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            if ((talbstatus == 2) && (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 || FlagSign5 != 1 ||  FlagSign13 != 1))
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
                Tuple<string, int, int, string, string> result = Constants.CheckSign("6", "8", Sign6, Empn6);
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
                            
                            //FReports f = new FReports();
                            //f.Show();

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

            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 )
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
                Tuple<string, int, int, string, string> result = Constants.CheckSign("9", "8", Sign9, Empn9);
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
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 )
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
                Tuple<string, int, int, string, string> result = Constants.CheckSign("13", "8", Sign13, Empn13);
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
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 ||  FlagSign9 != 1 )
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
                Tuple<string, int, int, string, string> result = Constants.CheckSign("7", "8", Sign7, Empn7);
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
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign11 != 1 ||  FlagSign9 != 1 )
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
                Tuple<string, int, int, string, string> result = Constants.CheckSign("7", "8", Sign10, Empn10);
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

        private void BTN_SearchTalb_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.SEARCH))
            {
                return;
            }

            string talb_no = TXT_TalbNo.Text;
            string fyear = Cmb_FYear.Text;

            reset();

            if (SearchTalb(talb_no, fyear, false))
            {
                if (FlagSign3 != 1 && FlagSign2 != 1)
                {
                    Editbtn2.Enabled = true;
                }
                else
                {
                    Editbtn2.Enabled = false;
                }
            }

        }

        private void BTN_Search_Motab3a_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.CONFIRM_SEARCH))
            {
                return;
            }

            string talb_no = Cmb_TalbNo2.Text;
            string fyear = Cmb_FYear2.Text;

            reset();

            if (SearchTalb(talb_no, fyear, false))
            {
                Editbtn.Enabled = true;
                BTN_Print2.Enabled = true;
            }

            TXT_TalbNo.Enabled = false;
            Cmb_FYear.Enabled = false;
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                if (e.ColumnIndex == 6 && (bool)dataGridView1.Rows[e.RowIndex].Cells[11].Value == true) // 1 should be your column index
                {
                    dataGridView1.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                }
            }
        }

        private void Editbtn2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد تعديل طلب إصلاح ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_TalbNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text))
                {
                    MessageBox.Show("يجب اختيار طلب إصلاح المراد تعديله");
                    return;
                }

                PrepareEditState();
            }
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            sum = sum - (decimal)dataGridView1.Rows[e.Row.Index].Cells[9].Value;
            AppValueOriginal = sum;
            TXT_AppValue.Text = sum.ToString();
        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TXT_RedirectedDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXT_RedirectedFor_TextChanged(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void PDF_box_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TXT_CurrencyTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void TXT_PriceSarf_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXT_ArabicValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void TXT_Tamen_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TXT_Date_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void TXT_ReqFor_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void TXT_Edara_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void TXT_TalbNo2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void TXT_TalbNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void tBnodAwamershraaBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TXT_StockBian_TextChanged(object sender, EventArgs e)
        {

        }

        private void Txt_ReqQuan_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void TXT_Currency_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXT_Unit_TextChanged(object sender, EventArgs e)
        {

        }

        private void CMB_ApproxValue_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign9_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign11_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign4_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign12_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign7_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign13_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign5_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign6_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign8_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign3_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign2_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign1_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Cmb_TalbNo2_SelectedIndexChanged(object sender, EventArgs e)
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

        private void TXT_RecommendedCompanies_TextChanged(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void TXT_DateMohmat_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
