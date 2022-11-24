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
using Microsoft.Win32;

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
            ADD_AMRSHERAA_BNOD,
            ATTACH_FILE,
            SEARCH,
            CONFIRM_SEARCH,
            SAVE,

        }
        int currentSignNumber = 0;
        #endregion

        //------------------------------------------ Helper ---------------------------------
        #region Helpers
        private PictureBox CheckSignatures(Panel panel, int signNumber)
        {
            try
            {
                foreach (Control control in panel.Controls)
                {
                    if (control.GetType() == typeof(Panel))
                    {
                        PictureBox signControl = CheckSignatures((Panel)control, signNumber);

                        if (signControl != null)
                        {
                            return signControl;
                        }
                    }
                    else
                    {
                        if (control.Name == "Pic_Sign" + signNumber && ((PictureBox)control).Image == null)
                        {
                            return (PictureBox)control;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
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

        private void cleargridview()
        {
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

        }
        
        private void InsertAmrSheraaBnood()
        {
            SqlCommand cmd;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string q = "exec SP_InsertBnodAwamershraa @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@P111,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24 ";
                    cmd = new SqlCommand(q, Constants.con);

                    cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(row.Cells[0].Value));
                    cmd.Parameters.AddWithValue("@p2", DBNull.Value);
                    cmd.Parameters.AddWithValue("@p3", row.Cells[2].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p4", row.Cells[3].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p5", Convert.ToInt32(row.Cells[4].Value));
                    cmd.Parameters.AddWithValue("@p6", row.Cells[5].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p7", row.Cells[6].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p8", row.Cells[7].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p9", row.Cells[8].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p10", row.Cells[9].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p11", row.Cells[10].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p111", DBNull.Value);
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
        }

        public void SP_UpdateSignatures(int x, DateTime D1, DateTime? D2 = null)
        {
            string cmdstring = "Exec  SP_UpdateSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_AmrNo.Text));
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

        private void GetAmrBnod(string amrNo, string fyear)
        {
            table.Clear();

            string TableQuery = "SELECT *  FROM [T_BnodAwamershraa] Where Amrshraa_No = " + amrNo + " and AmrSheraa_sanamalia='" + fyear + "'";

            dataadapter = new SqlDataAdapter(TableQuery, Constants.con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataadapter.Fill(table);
            dataGridView1.DataSource = table;

            dataGridView1.Columns["TalbTwareed_No"].HeaderText = "رقم طلب التوريد";//col4

            dataGridView1.Columns["FYear"].HeaderText = "سنة مالية طلب التوريد";//col5

            dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col6

            dataGridView1.Columns["NameEdara"].HeaderText = "الادارة الطالبة";//col8

            dataGridView1.Columns["BndMwazna"].HeaderText = "بند موازنة";

            dataGridView1.Columns["Quan"].HeaderText = " الكمية المطلوبة";//COL10

            dataGridView1.Columns["Unit"].HeaderText = "الوحدة";//col12

            dataGridView1.Columns["Bayan"].HeaderText = "بيان المهمات";//col13

            dataGridView1.Columns["UnitPrice"].HeaderText = "سعر الوحدة غير شامل الضريبة";//col17

            dataGridView1.Columns["TotalPrice"].HeaderText = "الاجمالى غير شامل الضريبة";//col18

            dataGridView1.Columns["ApplyDareba"].HeaderText = "تطبق الضريبة";//col19

            dataGridView1.Columns["Darebapercent"].HeaderText = "نسبة الضريبة";//col20

            dataGridView1.Columns["TotalPriceAfter"].HeaderText = "الاجمالى شامل الضريبة ";//col21

            dataGridView1.Columns["TalbEsdarShickNo"].HeaderText = "رقم طلب الاصدار ";//col26

            dataGridView1.Columns["ShickNo"].HeaderText = "رقم الشيك ";//col27

            dataGridView1.Columns["ShickDate"].HeaderText = "تاريخ الشيك ";//col28



            dataGridView1.Columns["Amrshraa_No"].HeaderText = "رقم أمر الشراء";//col0
            dataGridView1.Columns["Amrshraa_No"].Visible = false;

            dataGridView1.Columns["Monaksa_No"].HeaderText = " رقم المناقصة";//col1
            dataGridView1.Columns["Monaksa_No"].Visible = false;

            dataGridView1.Columns["monaksa_sanamalia"].HeaderText = "مناقصةسنةمالية";//col2
            dataGridView1.Columns["monaksa_sanamalia"].Visible = false;

            dataGridView1.Columns["AmrSheraa_sanamalia"].HeaderText = "امر الشراء سنةمالية";//col3
            dataGridView1.Columns["AmrSheraa_sanamalia"].Visible = false;

            dataGridView1.Columns["CodeEdara"].HeaderText = "كود ادارة";//col7
            dataGridView1.Columns["CodeEdara"].Visible = false;

            dataGridView1.Columns["Quan2"].HeaderText = " الكمية الموردة";////COL11
            dataGridView1.Columns["Quan2"].Visible = false;

            dataGridView1.Columns["Makhzn"].HeaderText = "مخزن";//col14
            dataGridView1.Columns["Makhzn"].Visible = false;

            dataGridView1.Columns["Rakm_Tasnif"].HeaderText = "رقم التصنيف";//col15
            dataGridView1.Columns["Rakm_Tasnif"].Visible = false;

            dataGridView1.Columns["Rased_After"].HeaderText = "رصيد بعد";//col16
            dataGridView1.Columns["Rased_After"].Visible = false;

            dataGridView1.Columns["EstlamFlag"].HeaderText = "تم الاستلام ";//col22
            dataGridView1.Columns["EstlamFlag"].Visible = false;

            dataGridView1.Columns["EstlamDate"].HeaderText = "تاريخ الاستلام ";//col23
            dataGridView1.Columns["EstlamDate"].Visible = false;

            dataGridView1.Columns["LessQuanFlag"].HeaderText = "يوجد عجز ";//col24
            dataGridView1.Columns["LessQuanFlag"].Visible = false;

            dataGridView1.Columns["NotIdenticalFlag"].HeaderText = "مطابق/غير مطابق ";//col25
            dataGridView1.Columns["NotIdenticalFlag"].Visible = false;

            dataGridView1.Columns["TalbEsdarShickNo"].Visible = false;
            dataGridView1.Columns["ShickNo"].Visible = false;
            dataGridView1.Columns["ShickDate"].Visible = false;

        }

        public bool SearchAmrSheraa(string amrNo, string fyear)
        {
            Constants.opencon();

            string cmdstring;
            SqlCommand cmd;

            cmdstring = "select * from T_Awamershraa where Amrshraa_No=@TN and AmrSheraa_sanamalia=@FY";
            cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TN", amrNo);
            cmd.Parameters.AddWithValue("@FY", fyear);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                try
                {
                    while (dr.Read())
                    {

                        TXT_AmrNo.Text = dr["Amrshraa_No"].ToString();
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
                        else if (BuyMethod == "6")
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
                        string s7 = dr["Sign2"].ToString();

                        Cmb_FY.Text = dr["AmrSheraa_sanamalia"].ToString();

                        //dr.Close();


                        if (s1 != "")
                        {
                            string p = Constants.RetrieveSignature("1", "3", s1);
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
                            string p = Constants.RetrieveSignature("2", "3", s2);
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
                            string p = Constants.RetrieveSignature("3", "3", s3);
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

                                FlagSign6 = 1;
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
                                wazifa7 = p.Split(':')[2];
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

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
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
                MessageBox.Show("من فضلك تاكد من رقم امر الشراء");
                reset();
                return false;
            }

            dr.Close();

            GetAmrBnod(amrNo,fyear);

            Constants.closecon();
            return true;
        }

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

            Pic_Sign1.Image = null;
            FlagSign1 = 0;
            Pic_Sign1.BackColor = Color.Green;
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

            if (Constants.Amrshera_F)
            {
                Cmb_FY.Enabled = true;
                TXT_AmrNo.Enabled = true;
                BTN_Print.Enabled = true;
            }
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
            BTN_Search_Motab3a.Enabled = true;
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

        //------------------------------------------ Logic Handler ---------------------------------
        #region Logic Handler
        private void AddLogic()
        {
            Constants.opencon();

            string cmdstring = "exec SP_InsertAwamershraa @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p311,@p3111,@p31111,@p311111,@p32,@p33,@p333,@p3333,@p33333,@p38,@p39,@p40,@p41,@p34 out";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_AmrNo.Text));
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
            }
            catch (SqlException sqlEx)
            {
                executemsg = false;
                Console.WriteLine(sqlEx);
            }

            flag = (int)cmd.Parameters["@p34"].Value;

            if (executemsg == true && flag == 1)
            {

                InsertAmrSheraaBnood();
                
                for (int i = 1; i <= 7; i++)
                {
                    cmdstring = "Exec  SP_InsertSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
                    cmd = new SqlCommand(cmdstring, Constants.con);
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
                reset();
            }
            else if (executemsg == true && flag == 2)
            {
                MessageBox.Show("تم إدخال رقم امر الشراء  من قبل  ! ");
            }
            else if (executemsg == false)
            {
                MessageBox.Show("لم يتم إدخال إذن الصرف بنجاج!!");
            }

            Constants.closecon();
        }

        private void UpdateAmrSheraaSignatureCycle()
        {
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
        }

        public void UpdateAmrsheraa()
        {
            Constants.opencon();

            string cmdstring = "Exec SP_UpdateAwamershraa @TNOold,@FYold,@Mold,@FY2old,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p311,@p3111,@p31111,@p311111,@p32,@p33,@p333,@p3333,@p33333,@p38,@p39,@p40,@p41,@p34 out";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
            cmd.Parameters.AddWithValue("@TNOold", Convert.ToInt32(TNO));
            cmd.Parameters.AddWithValue("@FYold", FY);
            cmd.Parameters.AddWithValue("@Mold", DBNull.Value);
            cmd.Parameters.AddWithValue("@FY2old", DBNull.Value);
            cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_AmrNo.Text));
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

            if (FlagSign4 == 1)
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
            }
            catch (SqlException sqlEx)
            {
                executemsg = false;
                Console.WriteLine(sqlEx);
            }

            flag = (int)cmd.Parameters["@p34"].Value;

            if (executemsg == true && flag == 2)
            {
                InsertAmrSheraaBnood();
                UpdateAmrSheraaSignatureCycle();

                MessageBox.Show("تم التعديل بنجاح  ! ");

                reset();
            }
            else if (executemsg == true && flag == 3)
            {
                MessageBox.Show("تم إدخال رقم امر الشراء  من قبل  ! ");
            }
            else if (executemsg == false)
            {
                MessageBox.Show("لم يتم تعديل امر الشراء بنجاج!!");
            }

            Constants.closecon();
        }

        private void EditLogic()
        {
            UpdateAmrsheraa();
        }
        
        private void DeleteLogic()
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
                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    Console.WriteLine(sqlEx);                   
                }

                flag = (int)cmd.Parameters["@aot"].Value;

                if (executemsg == true && flag == 1)
                {
                    MessageBox.Show("تم الحذف بنجاح");
                    reset();
                }
                Constants.closecon();
            }
        }
        #endregion

        //------------------------------------------ Validation Handler ---------------------------------
        #region Validation Handler
            private List<(ErrorProvider, Control, string)> ValidateAddBnodAmrSheraa()
            {
                List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

                #region Cmb_FYear
                if (string.IsNullOrWhiteSpace(Cmb_FY.Text) || Cmb_FY.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_FY, "تاكد من  اختيار السنة المالية"));
                }
                #endregion

                return errorsList;
            }

            private List<(ErrorProvider, Control, string)> ValidateAttachFile()
            {
                List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

                #region Cmb_FYear
                if (string.IsNullOrWhiteSpace(Cmb_FY.Text) || Cmb_FY.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_FY, "تاكد من  اختيار السنة المالية"));
                }
                #endregion

                #region TXT_EznNo
                if (string.IsNullOrWhiteSpace(TXT_AmrNo.Text))
                {
                    errorsList.Add((errorProvider, TXT_AmrNo, "يجب اختيار رقم أمر الشراء"));
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

                    #region Cmb_AmrNo2
                    if (string.IsNullOrWhiteSpace(Cmb_AmrNo2.Text) || Cmb_AmrNo2.SelectedIndex == -1)
                    {
                        errorsList.Add((errorProvider, Cmb_AmrNo2, "يجب اختيار رقم أمر الشراء"));
                    }
                    #endregion
                }
                else
                {
                    #region Cmb_FYear
                    if (string.IsNullOrWhiteSpace(Cmb_FY.Text) || Cmb_FY.SelectedIndex == -1)
                    {
                        errorsList.Add((errorProvider, Cmb_FY, "تاكد من  اختيار السنة المالية"));
                    }
                    #endregion

                    #region TXT_AmrNo
                    if (string.IsNullOrWhiteSpace(TXT_AmrNo.Text))
                    {
                        errorsList.Add((errorProvider, TXT_AmrNo, "يجب اختيار رقم أمر الشراء"));
                    }
                    #endregion
                }

                return errorsList;
            }

            private List<(ErrorProvider, Control, string)> ValidateSave()
            {
                List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

                #region Cmb_FYear
                if (string.IsNullOrWhiteSpace(Cmb_FY.Text) || Cmb_FY.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_FY, "تاكد من  اختيار السنة المالية"));
                }
                #endregion

                #region dataGridView1
                if (dataGridView1.Rows.Count <= 0)
                {
                    //errorsList.Add((errorProvider, dataGridView1, "لايمكن ان يتكون طلب توريد بدون بنود"));
                    MessageBox.Show("لايمكن ان يتكون طلب توريد بدون بنود");
                }
                else if (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow == true)
                {
                    //errorsList.Add((errorProvider, dataGridView1, "لايمكن ان يتكون طلب توريد بدون بنود"));
                    MessageBox.Show("لايمكن ان يتكون طلب توريد بدون بنود");
                }
                #endregion

                PictureBox signControl = CheckSignatures(signatureTable, currentSignNumber);
                if (signControl != null)
                {
                    errorsList.Add((errorProvider, signControl, "تاكد من التوقيع"));
                }


                return errorsList;
            }

            private bool IsValidCase(VALIDATION_TYPES type)
            {
                List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

                if (type == VALIDATION_TYPES.ADD_AMRSHERAA_BNOD)
                {
                    errorsList = ValidateAddBnodAmrSheraa();
                }

                else if (type == VALIDATION_TYPES.ATTACH_FILE)
                {
                    errorsList = ValidateAttachFile();
                }
                else if (type == VALIDATION_TYPES.SEARCH)
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

        private void init()
        {
            alertProvider.Icon = SystemIcons.Warning;

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
                cmdstring = "select Amrshraa_No from   T_Awamershraa where  AmrSheraa_sanamalia='" + Cmb_FY.Text + "'" + " and CodeEdara='" + codeedara + "'";

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

            Cmb_FY.SelectedIndex = -1;
            Cmb_FYear2.SelectedIndex = -1;

            reset();
        }

        public AmrSheraa()
        {
            InitializeComponent();
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            init();
        }

        private void AmrSheraa_Load(object sender, EventArgs e)
        {

        }
                 
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد اضافة امر شراء جديد؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                reset();
                PrepareAddState();

                AddEditFlag = 2;

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

                PrepareEditState();
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
                Constants.opencon();

                // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
                string cmdstring = "select ( COALESCE(MAX( Amrshraa_No), 0)) from  T_Awamershraa where AmrSheraa_sanamalia=@FY ";
                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
                
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
                        TXT_AmrNo.Text = flag.ToString();//el rakm el new

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

        private void BTN_ChooseTalb_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.ADD_AMRSHERAA_BNOD))
            {
                return;
            }

            GetAmrBnod(TXT_AmrNo.Text, Cmb_FY.Text);

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

            if (!IsValidCase(VALIDATION_TYPES.SAVE))
            {
                return;
            }

            if (AddEditFlag == 2)
            {
                if (FlagSign1 != 1)
                {
                    MessageBox.Show("من فضلك تاكد من توقيع امر الشراء");
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
                EditLogic();
            }
        }



        private void Cmb_FYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            
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

        private void BTN_Print2_Click(object sender, EventArgs e)
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

        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            AddEditFlag = 0;
            reset();
        }

        private void BTN_Search_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.SEARCH))
            {
                return;
            }

            string amr_no = TXT_AmrNo.Text;
            string fyear = Cmb_FY.Text;

            reset();

            if (SearchAmrSheraa(amr_no, fyear))
            {
                if (FlagSign2 != 1 && FlagSign1 != 1)
                {
                    EditBtn.Enabled = true;
                }
                else
                {
                    EditBtn.Enabled = false;
                }
            }
        }

        private void BTN_Search_Motab3a_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.CONFIRM_SEARCH))
            {
                return;
            }

            string amr_no = Cmb_AmrNo2.Text;
            string fyear = Cmb_FYear2.Text;

            reset();

            if (SearchAmrSheraa(amr_no, fyear))
            {
                EditBtn2.Enabled = true;
                BTN_Print2.Enabled = true;
            }

            TXT_AmrNo.Enabled = false;
            Cmb_FY.Enabled = false;
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

                    path += Cmb_FY.Text + @"\";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    path += "AMR_SHERAA" + @"\";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    path += TXT_AmrNo.Text + @"\";

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

        private void BTN_PDF_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.ATTACH_FILE))
            {
                return;
            }

            PDF_PopUp popup = new PDF_PopUp();

            popup.WholePath = @"\\172.18.8.83\MaterialAPP\PDF\" + Constants.CodeEdara + @"\" + Cmb_FY.Text + @"\AMR_SHERAA\" + TXT_AmrNo.Text + @"\";
            try
            {
                popup.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            popup.Dispose();
        }

        private void EditBtn2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد تعديل امر الشراء ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_AmrNo.Text) || string.IsNullOrEmpty(Cmb_FY.Text))
                {
                    MessageBox.Show("يجب اختيار امر الشراء المراد تعديله");
                    return;
                }

                PrepareConfirmState();
            }
        }

        private void DeleteBtn2_Click(object sender, EventArgs e)
        {
            DeleteLogic();
        }





        //------------------------------------------ Signature Handler ---------------------------------
        #region Signature Handler
        private void BTN_Sigm1_Click(object sender, EventArgs e)
        {

            Empn1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل  رقم القيد الخاص بك", "توقيع الاعدداد", "");

            Sign1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع الاعدادس", "");

            if (Sign1 != "" && Empn1 != "")
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
                    FlagSign1 = 0;
                    FlagEmpn1 = "";

                }


            }
        }

        private void BTN_Sign2_Click(object sender, EventArgs e)
        {
            Empn7 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع الحسابات", "");

            Sign7 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع الحسابات", "");

            if (Sign7 != "" && Empn7 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("7", "3", Sign7, Empn7);
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
            }
        }

        private void BTN_Sigm12_Click(object sender, EventArgs e)
        {
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
            Empn3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد سالخاص بك", "توقيع مدير عام مساعد", "");

            Sign3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مدير عام مساعد", "");

            if (Sign3 != "" && Empn3 != "")
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
                    FlagSign3 = 0;
                    FlagEmpn3 = "";
                }
            }
        }

        private void BTN_Sigm14_Click(object sender, EventArgs e)
        {
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
            }
        }

        private void BTN_Sign6_Click(object sender, EventArgs e)
        {
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
                    FlagSign6 = 0;
                    FlagEmpn6 = "";
                }
            }
        }
        #endregion

    }
}
