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
    public partial class FEdafaMakhzania_F : Form
    {
        //------------------------------------------ Define Variables ---------------------------------
        #region Def Variables
        public SqlConnection con;//sql conn for anrpc_sms db
        public int directflag = 0;
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
        public double oldvalue;
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
        public int flag = 0;
        //  public string TableQuery;

        AutoCompleteStringCollection TasnifColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection UnitColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TypeColl = new AutoCompleteStringCollection(); //empn

        #endregion

        #region myDefVariable
        enum VALIDATION_TYPES
        {
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

        public void SP_UpdateSignatures(int x, DateTime D1, DateTime? D2 = null)
        {
            Constants.opencon();
            string cmdstring = "Exec  SP_UpdateSignDates  @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_EdafaNo.Text.ToString()));
            cmd.Parameters.AddWithValue("@TNO2", Convert.ToInt32(TXT_TRNO.Text));


            cmd.Parameters.AddWithValue("@FY", Cmb_FY2.Text.ToString());

            cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
            cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
            cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);

            cmd.Parameters.AddWithValue("@FN", 5);

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

        private void GetEdafaBnod(string amrNo, string fyear)
        {
            table.Clear();

            string TableQuery = @"SELECT *  FROM [T_BnodAwamershraa] Where Estlamflag=1 and 
                                Amrshraa_No = " + amrNo + " and AmrSheraa_sanamalia='" + fyear + "'";

            dataadapter = new SqlDataAdapter(TableQuery, Constants.con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataadapter.Fill(table);
            dataGridView1.DataSource = table;

            dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col6
            dataGridView1.Columns["BndMwazna"].HeaderText = "بند موازنة";//col9
            dataGridView1.Columns["Quan"].HeaderText = "الكمية";//col10

            dataGridView1.Columns["Quan2"].HeaderText = "الكمية الواردة";//col11
            dataGridView1.Columns["Quan2"].DefaultCellStyle.BackColor = Color.SandyBrown;

            dataGridView1.Columns["Unit"].HeaderText = "الوحدة";//col12
            dataGridView1.Columns["Bayan"].HeaderText = "بيان المهمات";//col13
            dataGridView1.Columns["Rakm_Tasnif"].HeaderText = "رقم التصنيف";//col15
            dataGridView1.Columns["Rased_After"].HeaderText = "رصيد بعد";//col16
            dataGridView1.Columns["UnitPrice"].HeaderText = "سعر الوحدة";//col17
            dataGridView1.Columns["TotalPrice"].HeaderText = "الثمن الاجمالى";//col18
            dataGridView1.Columns["ApplyDareba"].HeaderText = "تطبق الضريبة";//col19
            dataGridView1.Columns["Darebapercent"].HeaderText = "نسبة الضريبة";//col20
            dataGridView1.Columns["TotalPriceAfter"].HeaderText = "السعر الاجمالى ";//col21
            dataGridView1.Columns["NotIdenticalFlag"].HeaderText = "مطابق/غير مطابق ";
            dataGridView1.Columns["ExpirationDate"].HeaderText = "تاريخ انتهاء الصلاحية ";//col28

            dataGridView1.Columns["Amrshraa_No"].HeaderText = "رقم أمر الشراء";//col0
            dataGridView1.Columns["Amrshraa_No"].Visible = false;

            dataGridView1.Columns["Monaksa_No"].HeaderText = " رقم المناقصة";//col1
            dataGridView1.Columns["Monaksa_No"].Visible = false;

            dataGridView1.Columns["monaksa_sanamalia"].HeaderText = "مناقصةسنةمالية";//col2
            dataGridView1.Columns["monaksa_sanamalia"].Visible = false;

            dataGridView1.Columns["AmrSheraa_sanamalia"].HeaderText = "امر الشراء سنةمالية";//col3
            dataGridView1.Columns["AmrSheraa_sanamalia"].Visible = false;

            dataGridView1.Columns["TalbTwareed_No"].HeaderText = "رقم طلب التوريد";//col4
            dataGridView1.Columns["TalbTwareed_No"].Visible = false;

            dataGridView1.Columns["FYear"].HeaderText = "سنة مالية طلب التوريد";//col5
            dataGridView1.Columns["FYear"].Visible = false;

            dataGridView1.Columns["CodeEdara"].HeaderText = "كود ادارة";//col7
            dataGridView1.Columns["CodeEdara"].Visible = false;

            dataGridView1.Columns["NameEdara"].HeaderText = "الادارة الطالبة";//col8
            dataGridView1.Columns["NameEdara"].Visible = false;

            dataGridView1.Columns["Makhzn"].HeaderText = "مخزن";//col14
            dataGridView1.Columns["Makhzn"].Visible = false;

            dataGridView1.Columns["EstlamFlag"].HeaderText = "تم الاستلام ";//col22
            dataGridView1.Columns["EstlamFlag"].Visible = false;

            dataGridView1.Columns["EstlamDate"].HeaderText = "تاريخ الاستلام ";//col23
            dataGridView1.Columns["EstlamDate"].Visible = false;

            dataGridView1.Columns["LessQuanFlag"].HeaderText = "يوجد عجز ";//col24
            dataGridView1.Columns["LessQuanFlag"].Visible = false;
            dataGridView1.Columns["LessQuanFlag"].DefaultCellStyle.BackColor = Color.Aqua;

            dataGridView1.Columns["TalbEsdarShickNo"].HeaderText = "رقم طلب الاصدار ";//col26
            dataGridView1.Columns["TalbEsdarShickNo"].Visible = false;

            dataGridView1.Columns["ShickNo"].HeaderText = "رقم الشيك ";//col27
            dataGridView1.Columns["ShickNo"].Visible = false;

            dataGridView1.Columns["ShickDate"].HeaderText = "تاريخ الشيك ";//col28
            dataGridView1.Columns["ShickDate"].Visible = false;//col28
        }

        public bool GetAmrSheraaData(string amrNo, string fyear)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring = "select * from T_Awamershraa where Amrshraa_No=@TN and AmrSheraa_sanamalia=@FY";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TN", amrNo);
            cmd.Parameters.AddWithValue("@FY", fyear);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    TXT_AmrNo.Text = dr["Amrshraa_No"].ToString();
                   
                    TXT_Momayz.Text = dr["Momayz"].ToString();

                    TXT_Edara.Text = dr["NameEdara"].ToString();
                    TXT_Date.Text = dr["Date_amrshraa"].ToString();
                    TXT_BndMwazna.Text = dr["Bnd_Mwazna"].ToString();
                    TXT_Payment.Text = dr["Payment_Method"].ToString();
                    TXT_TaslemDate.Text = dr["Date_Tslem"].ToString();
                    TXT_TaslemPlace.Text = dr["Mkan_Tslem"].ToString();
                    TXT_Name.Text = dr["Shick_Name"].ToString();
                    TXT_HesabMward1.Text = dr["Hesab_Mward"].ToString();
                    TXT_HesabMward2.Text = dr["Hesab_Mward"].ToString();
                    TXT_Egmali.Text = dr["Egmali"].ToString();
                }
            }
            else
            {
                MessageBox.Show("من فضلك تاكد من رقم امر الشراء المراد اضافته");
                reset();

                return false;
            }
            dr.Close();

            GetEdafaBnod(amrNo, fyear);

            Cmb_FY.Text = fyear;
            Cmb_AmrNo.Text = amrNo;

            Constants.closecon();

            return true;
        }


        public bool SearchEdafa(string edafaNo, string fyear, string momayz)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();

            string cmdstring;
            SqlCommand cmd;

            cmdstring = "select * from   T_Edafa where Edafa_No=@TN and Edafa_FY=@FY  and TR_NO=@TRNO";

            cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TN", edafaNo);
            cmd.Parameters.AddWithValue("@FY", fyear);
            cmd.Parameters.AddWithValue("@TRNO", momayz);

            SqlDataReader dr = cmd.ExecuteReader();
            string amrno = "";
            string amrsana = "";
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {

                    amrsana = dr["AmrSheraa_sanamalia"].ToString();

                    amrno = dr["Amrshraa_No"].ToString();
                    TXT_TRNO.Text = dr["TR_NO"].ToString();

                    if (TXT_TRNO.Text.ToString() != "")
                    {
                        Cmb_CType.SelectedValue = TXT_TRNO.Text.ToString();
                    }

                    string s1 = dr["Sign1"].ToString();
                    string s2 = dr["Sign2"].ToString();
                    string s3 = dr["Sign3"].ToString();
                    string s4 = dr["Sign4"].ToString();

                    if (s1 != "")
                    {
                        string p = Constants.RetrieveSignature("1", "5", s1);
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
                        string p = Constants.RetrieveSignature("2", "5", s2);
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
                        string p = Constants.RetrieveSignature("3", "5", s3);
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
                        string p = Constants.RetrieveSignature("3", "1", s4);
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
                }
            }
            else
            {
                MessageBox.Show("من فضلك تاكد من رقم الاضافة المخزنية");
                reset();
                return false ;
            }

            dr.Close();

            if (!GetAmrSheraaData(amrno, amrsana))
            {
                return false ;
            }

            Cmb_FY2.Text = fyear;
            TXT_EdafaNo.Text = edafaNo;

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
            changePanelState(panel8, true);
            TXT_EdafaNo.Enabled = false;

            //amr sheraa sec
            changePanelState(panel6, true);

            //btn Section
            //generalBtn
            SaveBtn.Enabled = true;
            BTN_Cancel.Enabled = true;
            browseBTN.Enabled = true;
            BTN_PDF.Enabled = true;

            Addbtn.Enabled = false;
            BTN_Search.Enabled = false;
            BTN_Search_Motab3a.Enabled = false;
            BTN_Save2.Enabled = false;
            EditBtn.Enabled = false;
            EditBtn2.Enabled = false;
            DeleteBtn.Enabled = false;
            BTN_Print.Enabled = false;
            BTN_Print2.Enabled = false;


            //signature btn
            changePanelState(signatureTable, false);
            BTN_Sigm1.Enabled = true;

            changeDataGridViewColumnState(dataGridView1, true);

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

            if (Constants.User_Type == "A")
            {
                BTN_Sign4.Enabled = true;
                DeleteBtn.Enabled=true;

                Pic_Sign4.BackColor = Color.Green;
                currentSignNumber = 4;

                dataGridView1.Columns["LessQuanFlag"].ReadOnly = false;
                dataGridView1.Columns["NotIdenticalFlag"].ReadOnly = false;//col25
            }
            else if (Constants.User_Type == "B")
            {
                if (Constants.UserTypeB == "Edafa")
                {
                    if (FlagSign2 != 1 && FlagSign1 == 1)
                    {
                        BTN_Sign2.Enabled = true;

                        Pic_Sign2.BackColor = Color.Green;
                        currentSignNumber = 2;
                    }
                    else if (FlagSign3 != 1 && FlagSign2 == 1)
                    {
                        BTN_Sign3.Enabled = true;

                        Pic_Sign3.BackColor = Color.Green;
                        currentSignNumber = 3;
                    }

                    dataGridView1.Columns["LessQuanFlag"].ReadOnly = true;
                    dataGridView1.Columns["NotIdenticalFlag"].ReadOnly = true;//col25
                }
            }

            AddEditFlag = 1;
            TNO = Cmb_AmrNo.Text;
            FY = Cmb_FY.Text;
            FY2 = Cmb_FY2.Text;
            MNO = TXT_EdafaNo.Text;
        }

        public void prepareSearchState()
        {
            DisableControls();
            Input_Reset();

            if (Constants.Edafa_F)
            {
                Cmb_CType.Enabled = true;
                Cmb_FY2.Enabled = true;
                TXT_EdafaNo.Enabled = true;
                BTN_Print.Enabled = true;
            }

        }

        public void reset()
        {
            prepareSearchState();
        }

        public void DisableControls()
        {
            //fyear sec
            changePanelState(panel8, false);

            //amr sheraa sec
            changePanelState(panel6, false);

            //bian edara sec
            changePanelState(panel9, false);

            //moward sec
            changePanelState(panel10, false);

            //sheraa methods
            changePanelState(panel5, false);

            //btn Section
            //generalBtn
            Addbtn.Enabled = true;
            BTN_Search.Enabled = true;
            BTN_Search_Motab3a.Enabled = true;

            SaveBtn.Enabled = false;
            BTN_Save2.Enabled = false;
            EditBtn.Enabled = false;
            EditBtn2.Enabled = false;
            BTN_Cancel.Enabled = false;
            DeleteBtn.Enabled = false;
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
            //fyear sec
            TXT_EdafaNo.Text = "";
            TXT_TRNO.Text = "";

            Cmb_FY2.Text = "";
            Cmb_FY2.SelectedIndex = -1;

            Cmb_CType.Text = "";
            Cmb_CType.SelectedIndex = -1;

            //amr sheraa sec
            Cmb_FY.Text = "";
            Cmb_FY.SelectedIndex = -1;

            Cmb_AmrNo.Text = "";
            Cmb_AmrNo.SelectedIndex = -1;

            //bian edara sec
            TXT_Edara.Text = "";
            TXT_TaslemDate.Text = "";
            TXT_TaslemPlace.Text = "";
            TXT_Date.Value = DateTime.Today;

            //moward sec
            TXT_Payment.Text = "";
            TXT_Egmali.Text = "";
            TXT_BndMwazna.Text = "";
            TXT_HesabMward1.Text = "";
            TXT_HesabMward2.Text = "";

            //sheraa methods
            TXT_Name.Text = "";
            TXT_NameMward.Text = "";
            TXT_TalbNo.Text = "";
            TXT_Momayz.Text = "";


            //search sec
            TXT_TRNO2.Text = "";

            Cmb_CType2.Text = "";
            Cmb_CType2.SelectedIndex = -1;

            Cmb_FYear2.Text = "";
            Cmb_FYear2.SelectedIndex = -1;

            Cmb_EdafaNo2.Text = "";
            Cmb_EdafaNo2.SelectedIndex = -1;

            resetSignature();

            //tkalifData types
            TXT_AccNo.Text = "";
            TXT_PaccNo.Text = "";
            TXT_MTaklif.Text = "";
            TXT_MResp.Text = "";
            TXT_Masrof.Text = "";
            TXT_Morakba.Text = "";
            TXT_Enfak.Text = "";
            TXT_Tasnif.Text = "";
            TXT_Mobashr.Text = "";

            cleargridview();

            pictureBox2.Image = null;

            CH_Direct.Checked = false;
            oldvalue = 0;
            AddEditFlag = 0;
            directflag = 0;
        }
        #endregion

        //------------------------------------------ Logic Handler ---------------------------------
        #region Logic Handler
        private void AddLogic()
        {

            Constants.opencon();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string cmdstring = "exec SP_InsertEdafa @p1,@p2,@p3,@p4,@p44,@p444,@p5,@p55,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p34 out";
                    SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                    cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_EdafaNo.Text));
                    cmd.Parameters.AddWithValue("@p3", Convert.ToInt32(Cmb_AmrNo.Text));
                    cmd.Parameters.AddWithValue("@p2", (Cmb_FY2.Text));
                    cmd.Parameters.AddWithValue("@p4", (Cmb_FY.Text));
                    cmd.Parameters.AddWithValue("@p44", (row.Cells[4].Value));
                    cmd.Parameters.AddWithValue("@p444", (row.Cells[5].Value));
                    cmd.Parameters.AddWithValue("@p5", Convert.ToInt32(row.Cells[6].Value));
                    cmd.Parameters.AddWithValue("@p55", (TXT_TRNO.Text));
                    cmd.Parameters.AddWithValue("@p6", (Convert.ToDateTime(TXT_Date.Value.ToShortDateString())));

                    cmd.Parameters.AddWithValue("@p7", Convert.ToDouble(row.Cells[11].Value));
                    cmd.Parameters.AddWithValue("@p8", (row.Cells[24].Value));
                    cmd.Parameters.AddWithValue("@p9", (row.Cells[25].Value));
                    cmd.Parameters.AddWithValue("@p10", (TXT_NameMward.Text));



                    cmd.Parameters.AddWithValue("@p11", (TXT_AccNo.Text));
                    cmd.Parameters.AddWithValue("@p12", DBNull.Value);

                    if (string.IsNullOrEmpty(TXT_MTaklif.Text))
                    {
                        cmd.Parameters.AddWithValue("@p13", DBNull.Value);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p13", Convert.ToDecimal(TXT_MTaklif.Text));

                    }
                    if (string.IsNullOrEmpty(TXT_MResp.Text))
                    {
                        cmd.Parameters.AddWithValue("@p14", DBNull.Value);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p14", Convert.ToDecimal(TXT_MResp.Text));
                    }
                    if (string.IsNullOrEmpty(TXT_Masrof.Text))
                    {
                        cmd.Parameters.AddWithValue("@p15", DBNull.Value);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p15", Convert.ToDecimal(TXT_Masrof.Text));
                    }
                    if (string.IsNullOrEmpty(TXT_Enfak.Text))
                    {
                        cmd.Parameters.AddWithValue("@p16", DBNull.Value);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p16", Convert.ToDecimal(TXT_Enfak.Text));
                    }
                    //  cmd.Parameters.AddWithValue("@p17",Convert.ToDecimal(TXT_Egmali.Text)??DBNull.Value);

                    if (string.IsNullOrEmpty(TXT_Morakba.Text))
                    {
                        cmd.Parameters.AddWithValue("@p17", DBNull.Value);

                    }
                    else
                    {


                        cmd.Parameters.AddWithValue("@p17", Convert.ToDecimal(TXT_Morakba.Text));
                    }
                    cmd.Parameters.AddWithValue("@p18", FlagEmpn1);//taamen
                    cmd.Parameters.AddWithValue("@p19", DBNull.Value);//dman
                    cmd.Parameters.AddWithValue("@p20", DBNull.Value);//dareba


                    cmd.Parameters.AddWithValue("@p21", DBNull.Value);//shroot


                    cmd.Parameters.AddWithValue("@p22", Constants.User_Name.ToString());
                    cmd.Parameters.AddWithValue("@p23", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    //cmd.Parameters.AddWithValue("@p233",TXT_TRNO.Text.ToString() );
                    if (row.Cells[16].Value.ToString() == "" || row.Cells[16].Value == DBNull.Value)
                    {
                        cmd.Parameters.AddWithValue("@p24", (row.Cells[16].Value));

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p24", Convert.ToInt32(row.Cells[16].Value));

                    }
                    //   cmd.Parameters.AddWithValue("@p24", Convert.ToInt32(row.Cells[16].Value));

                    cmd.Parameters.Add("@p34", SqlDbType.Int, 32);  //-------> output parameter
                    cmd.Parameters["@p34"].Direction = ParameterDirection.Output;

                    //int flag=0;

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

                }
            }
            if (executemsg == true && flag == 1)
            {
                string st = "exec SP_DeleteEdaraAlarm @p2,@p3,@p4";
                SqlCommand cmd1 = new SqlCommand(st, Constants.con);

                // cmd1.Parameters.AddWithValue("@p1", row.Cells[7].Value);


                cmd1.Parameters.AddWithValue("@p2", Convert.ToInt32(TXT_EdafaNo.Text));

                cmd1.Parameters.AddWithValue("@p3", (Cmb_FY2.Text));
                cmd1.Parameters.AddWithValue("@p4", (TXT_TRNO.Text));
                cmd1.ExecuteNonQuery();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        st = "exec SP_SendEdaraAlarm @p1,@p11,@p111,@p2,@p3,@p33,@p4,@p5,@p6,@p7";
                        cmd1 = new SqlCommand(st, Constants.con);

                        cmd1.Parameters.AddWithValue("@p1", row.Cells[7].Value);
                        cmd1.Parameters.AddWithValue("@p11", row.Cells[8].Value);
                        cmd1.Parameters.AddWithValue("@p111", row.Cells[6].Value);

                        cmd1.Parameters.AddWithValue("@p2", Convert.ToInt32(TXT_EdafaNo.Text));
                        cmd1.Parameters.AddWithValue("@p4", Convert.ToInt32(Cmb_AmrNo.Text));
                        cmd1.Parameters.AddWithValue("@p3", (Cmb_FY2.Text));
                        cmd1.Parameters.AddWithValue("@p33", (TXT_TRNO.Text));
                        cmd1.Parameters.AddWithValue("@p5", (Cmb_FY.Text));
                        // cmd.Parameters.AddWithValue("@p5", Convert.ToInt32(row.Cells[6].Value));

                        //  cmd.Parameters.AddWithValue("@p6", (Convert.ToDateTime(TXT_Date.Value.ToShortDateString())));
                        cmd1.Parameters.AddWithValue("@p6", Constants.User_Name.ToString());
                        cmd1.Parameters.AddWithValue("@p7", Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                        cmd1.ExecuteNonQuery();
                    }
                }

                //////////////////////////////////////////////////////////////////
                for (int i = 1; i <= 4; i++)
                {


                    string cmdstring = "Exec  SP_InsertSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
                    SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                    cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_EdafaNo.Text));
                    //cmd.Parameters.AddWithValue("@TNO2", DBNull.Value);
                    cmd.Parameters.AddWithValue("@TNO2", Convert.ToInt32(TXT_TRNO.Text));
                    cmd.Parameters.AddWithValue("@FY", Cmb_FY2.Text.ToString());
                    cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
                    cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
                    cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);

                    cmd.Parameters.AddWithValue("@FN", 5);

                    cmd.Parameters.AddWithValue("@SN", i);

                    cmd.Parameters.AddWithValue("@D1", DBNull.Value);

                    cmd.Parameters.AddWithValue("@D2", DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
                SP_UpdateSignatures(1, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()));



                //////////////////////////////////////////////////////////////

                MessageBox.Show("تم الإضافة بنجاح  ! ");

                reset();
            }
            else if (executemsg == true && flag == 2)
            {
                MessageBox.Show("تم إدخال رقم الاضافة المخزنية  من قبل  ! ");
            }
            else if (executemsg == false)
            {
                MessageBox.Show("لم يتم إدخال الاضافة المخزنية بنجاج!!");
            }

            Constants.closecon();

        }

        private void UpdateEdafaSignatureCycle()
        {
            if (FlagSign4 == 1)
            {

                SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

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

        }
        public void InsertTrans2()
        {
            Constants.opencon();
            string cmdstring = "Exec SP_deleteTR2 @TNO,@FY,@TRNO";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_EdafaNo.Text));
            cmd.Parameters.AddWithValue("@FY", Cmb_FY2.Text.ToString());

            cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text.ToString());
            cmd.ExecuteNonQuery();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (!row.IsNewRow)
                {
                    if ((row.Cells[25].Value.ToString() == "True"))//lw motabk
                    {

                        cmdstring = "exec SP_InsertTR2 @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29";
                        cmd = new SqlCommand(cmdstring, Constants.con);

                        cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_EdafaNo.Text));
                        cmd.Parameters.AddWithValue("@p2", Cmb_FY2.Text.ToString());
                        cmd.Parameters.AddWithValue("@p3", row.Cells[6].Value);
                        cmd.Parameters.AddWithValue("@p4", row.Cells[15].Value);
                        cmd.Parameters.AddWithValue("@p5", TXT_Date.Text.ToString());
                        cmd.Parameters.AddWithValue("@p6", TXT_TRNO.Text.ToString());
                        cmd.Parameters.AddWithValue("@p7", TXT_AccNo.Text.ToString());
                        cmd.Parameters.AddWithValue("@p8", DBNull.Value);
                        string st = row.Cells[15].Value.ToString();
                        if (st != "")
                        {
                            cmd.Parameters.AddWithValue("@p9", (st).Substring(0, 2));
                            cmd.Parameters.AddWithValue("@p10", (st).Substring(2, 2));

                            cmd.Parameters.AddWithValue("@p11", (st).Substring(4, 2));
                            cmd.Parameters.AddWithValue("@p12", (st).Substring(6, 2));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p9", DBNull.Value);
                            cmd.Parameters.AddWithValue("@p10", DBNull.Value);

                            cmd.Parameters.AddWithValue("@p11", DBNull.Value);
                            cmd.Parameters.AddWithValue("@p12", DBNull.Value);
                        }
                        //  cmd.Parameters.AddWithValue("@p13", row.Cells[10].Value);
                        //  cmd.Parameters.AddWithValue("@p14", row.Cells[11].Value);
                        cmd.Parameters.AddWithValue("@p13", row.Cells[11].Value);
                        cmd.Parameters.AddWithValue("@p14", row.Cells[16].Value);

                        /*string stt = "select Quan from T_Tsnif where STOCK_NO_ALL=@ST";
                        SqlCommand cmd2 = new SqlCommand(stt, Constants.con);
                        cmd2.Parameters.AddWithValue("@ST", (row.Cells[15].Value));
                        var AvQUan = cmd2.ExecuteScalar();
                        if (AvQUan != null)
                        {


                            cmd.Parameters.AddWithValue("@p14", AvQUan);

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p14",DBNull.Value);
                        }*/

                        cmd.Parameters.AddWithValue("@p15", row.Cells[12].Value);
                        cmd.Parameters.AddWithValue("@p16", row.Cells[7].Value);
                        cmd.Parameters.AddWithValue("@p17", row.Cells[8].Value);
                        cmd.Parameters.AddWithValue("@p18", TXT_Date.Value.Day.ToString());
                        cmd.Parameters.AddWithValue("@p19", TXT_Date.Value.Month.ToString());
                        cmd.Parameters.AddWithValue("@p20", TXT_Date.Value.Year.ToString());

                        cmd.Parameters.AddWithValue("@p21", (row.Cells[17].Value));
                        cmd.Parameters.AddWithValue("@p22", row.Cells[18].Value);
                        cmd.Parameters.AddWithValue("@p23", TXT_MTaklif.Text.ToString());
                        cmd.Parameters.AddWithValue("@p24", TXT_MResp.Text.ToString());
                        cmd.Parameters.AddWithValue("@p25", TXT_MTaklif.Text.ToString());
                        cmd.Parameters.AddWithValue("@p26", DBNull.Value);
                        cmd.Parameters.AddWithValue("@p27", DBNull.Value);
                        cmd.Parameters.AddWithValue("@p28", TXT_Morakba.Text.ToString());
                        cmd.Parameters.AddWithValue("@p29", TXT_Enfak.Text.ToString());
                        // cmd.Parameters.AddWithValue("@p30", Cmb_FYear.Text.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            MessageBox.Show("تم ادخال الحركة بنجاح");


        }
        public void InsertTrans()
        {
            Constants.opencon();
            string cmdstring = "Exec SP_deleteTR1 @TNO,@FY,@TRNO";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_EdafaNo.Text));
            cmd.Parameters.AddWithValue("@FY", Cmb_FY2.Text.ToString());
            cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text.ToString());


            cmd.ExecuteNonQuery();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (!row.IsNewRow)
                {
                    if ((row.Cells[25].Value.ToString() == "True"))//lw motabk
                    {

                        cmdstring = "exec SP_InsertTR1 @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29";
                        cmd = new SqlCommand(cmdstring, Constants.con);

                        cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_EdafaNo.Text));
                        cmd.Parameters.AddWithValue("@p2", Cmb_FY2.Text.ToString());
                        cmd.Parameters.AddWithValue("@p3", row.Cells[6].Value);
                        cmd.Parameters.AddWithValue("@p4", row.Cells[15].Value);
                        cmd.Parameters.AddWithValue("@p5", TXT_Date.Text.ToString());
                        cmd.Parameters.AddWithValue("@p6", TXT_TRNO.Text.ToString());
                        cmd.Parameters.AddWithValue("@p7", TXT_AccNo.Text.ToString());
                        cmd.Parameters.AddWithValue("@p8", DBNull.Value);
                        string st = row.Cells[15].Value.ToString();
                        if (st != "")
                        {
                            cmd.Parameters.AddWithValue("@p9", (st).Substring(0, 2));
                            cmd.Parameters.AddWithValue("@p10", (st).Substring(2, 2));

                            cmd.Parameters.AddWithValue("@p11", (st).Substring(4, 2));
                            cmd.Parameters.AddWithValue("@p12", (st).Substring(6, 2));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p9", DBNull.Value);
                            cmd.Parameters.AddWithValue("@p10", DBNull.Value);

                            cmd.Parameters.AddWithValue("@p11", DBNull.Value);
                            cmd.Parameters.AddWithValue("@p12", DBNull.Value);
                        }
                        //  cmd.Parameters.AddWithValue("@p13", row.Cells[10].Value);
                        //  cmd.Parameters.AddWithValue("@p14", row.Cells[11].Value);
                        cmd.Parameters.AddWithValue("@p13", row.Cells[11].Value);
                        cmd.Parameters.AddWithValue("@p14", row.Cells[16].Value);

                        /*string stt = "select Quan from T_Tsnif where STOCK_NO_ALL=@ST";
                        SqlCommand cmd2 = new SqlCommand(stt, Constants.con);
                        cmd2.Parameters.AddWithValue("@ST", (row.Cells[15].Value));
                        var AvQUan = cmd2.ExecuteScalar();
                        if (AvQUan != null)
                        {


                            cmd.Parameters.AddWithValue("@p14", AvQUan);

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p14",DBNull.Value);
                        }*/

                        cmd.Parameters.AddWithValue("@p15", row.Cells[12].Value);
                        cmd.Parameters.AddWithValue("@p16", row.Cells[7].Value);
                        cmd.Parameters.AddWithValue("@p17", row.Cells[8].Value);
                        cmd.Parameters.AddWithValue("@p18", TXT_Date.Value.Day.ToString());
                        cmd.Parameters.AddWithValue("@p19", TXT_Date.Value.Month.ToString());
                        cmd.Parameters.AddWithValue("@p20", TXT_Date.Value.Year.ToString());

                        cmd.Parameters.AddWithValue("@p21", (row.Cells[17].Value));
                        cmd.Parameters.AddWithValue("@p22", row.Cells[18].Value);
                        cmd.Parameters.AddWithValue("@p23", TXT_MTaklif.Text.ToString());
                        cmd.Parameters.AddWithValue("@p24", TXT_MResp.Text.ToString());
                        cmd.Parameters.AddWithValue("@p25", TXT_MTaklif.Text.ToString());
                        cmd.Parameters.AddWithValue("@p26", DBNull.Value);
                        cmd.Parameters.AddWithValue("@p27", DBNull.Value);
                        cmd.Parameters.AddWithValue("@p28", TXT_Morakba.Text.ToString());
                        cmd.Parameters.AddWithValue("@p29", TXT_Enfak.Text.ToString());
                        // cmd.Parameters.AddWithValue("@p30", Cmb_FYear.Text.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            MessageBox.Show("تم ادخال الحركة بنجاح");


        }
        public void UpdateQuan2()
        {
            Constants.opencon();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (!row.IsNewRow)
                {

                    if ((row.Cells[25].Value.ToString() == "True"))//lw motabk
                    {
                        //   string st = "select AvailableQuan from TR_IN_1 where SER_DOC=@S and FYear=@FY and SER_LIN=@L ";
                        //   SqlCommand cmd2 = new SqlCommand(st, Constants.con);

                        //    cmd2.Parameters.AddWithValue("@S",TXT_EdafaNo.Text);
                        //    cmd2.Parameters.AddWithValue("@FY",Cmb_FY2.Text);
                        //    cmd2.Parameters.AddWithValue("@L",(row.Cells[6].Value));

                        /*
                        string st = "select Quan from T_Tsnif where STOCK_NO_ALL=@ST";
                        SqlCommand cmd2 = new SqlCommand(st, Constants.con);
                        cmd2.Parameters.AddWithValue("@ST", (row.Cells[15].Value));
                        var scalar= cmd2.ExecuteScalar();*/

                        string cmdstring = "Exec SP_UpdateQuanTsnif @Quan,@ST,@F,@EN,@EFY,@BN,@TRNO";

                        SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                        //  cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[4].Value));
                        //will send rased badl else monsrf
                        //   cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[16].Value));
                        cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[11].Value));
                        cmd.Parameters.AddWithValue("@ST", (row.Cells[15].Value));
                        cmd.Parameters.AddWithValue("@F", 2);///sarf
                        cmd.Parameters.AddWithValue("@EN", TXT_EdafaNo.Text.ToString());
                        cmd.Parameters.AddWithValue("@EFY", Cmb_FY2.Text);
                        cmd.Parameters.AddWithValue("@BN", (row.Cells[6].Value));
                        cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text);
                        cmd.ExecuteNonQuery();
                        /*
                        if (oldvalue != null)
                        {


                            string cmdstring = "Exec SP_UpdateQuanTsnif @Quan,@ST,@F";

                            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                            cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[11].Value) );

                            cmd.Parameters.AddWithValue("@ST", (row.Cells[15].Value));
                            cmd.Parameters.AddWithValue("@F", 1);

                            cmd.ExecuteNonQuery();
                        }*/
                        /*
                        if (scalar != DBNull.Value && scalar != null && row.Cells[11].Value.ToString() != "") // Case where the DB value is null
                        {
                            string g = scalar.ToString();
                            double availablerased = Convert.ToDouble(g);
                            double newrased;
                            double quan = Convert.ToDouble(row.Cells[11].Value);
                            string xx = "select QuanArrived from T_Edafa where Edafa_No=@x and Edafa_FY=@Y and Bnd_No=@Z";
                            cmd2 = new SqlCommand(xx, Constants.con);


                            cmd2.Parameters.AddWithValue("@X", TXT_EdafaNo.Text);//stock_no_all
                            cmd2.Parameters.AddWithValue("@Y", Cmb_FY2.Text);//stock_no_all
                            cmd2.Parameters.AddWithValue("@Z",row.Cells[6].Value.ToString());//stock_no_all

                            var scalar2 = cmd2.ExecuteScalar();
                            double oldvalue = Convert.ToDouble(scalar2.ToString());
                            newrased = availablerased - oldvalue + quan;
                            //dataGridView1.Rows[e.RowIndex].Cells[10].Value = newrased;
                            executemsg = true;


                        }
                        else
                        {

                        }*/
                    }

                }
            }
        }
        public void UpdateQuan()
        {
            Constants.opencon();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (!row.IsNewRow)
                {

                    if ((row.Cells[25].Value.ToString() == "True"))//lw motabk
                    {
                        //   string st = "select AvailableQuan from TR_IN_1 where SER_DOC=@S and FYear=@FY and SER_LIN=@L ";
                        //   SqlCommand cmd2 = new SqlCommand(st, Constants.con);

                        //    cmd2.Parameters.AddWithValue("@S",TXT_EdafaNo.Text);
                        //    cmd2.Parameters.AddWithValue("@FY",Cmb_FY2.Text);
                        //    cmd2.Parameters.AddWithValue("@L",(row.Cells[6].Value));

                        /*
                        string st = "select Quan from T_Tsnif where STOCK_NO_ALL=@ST";
                        SqlCommand cmd2 = new SqlCommand(st, Constants.con);
                        cmd2.Parameters.AddWithValue("@ST", (row.Cells[15].Value));
                        var scalar= cmd2.ExecuteScalar();*/

                        string cmdstring = "Exec SP_UpdateQuanTsnif @Quan,@ST,@F,@EN,@EFY,@BN,@TRNO";

                        SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                        //  cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[4].Value));
                        //will send rased badl else monsrf
                        //   cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[16].Value));
                        cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[11].Value));
                        cmd.Parameters.AddWithValue("@ST", (row.Cells[15].Value));
                        cmd.Parameters.AddWithValue("@F", 1);
                        cmd.Parameters.AddWithValue("@EN", TXT_EdafaNo.Text.ToString());
                        cmd.Parameters.AddWithValue("@EFY", Cmb_FY2.Text);
                        cmd.Parameters.AddWithValue("@BN", (row.Cells[6].Value));
                        cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text);
                        cmd.ExecuteNonQuery();
                        /*
                        if (oldvalue != null)
                        {


                            string cmdstring = "Exec SP_UpdateQuanTsnif @Quan,@ST,@F";

                            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                            cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[11].Value) );

                            cmd.Parameters.AddWithValue("@ST", (row.Cells[15].Value));
                            cmd.Parameters.AddWithValue("@F", 1);

                            cmd.ExecuteNonQuery();
                        }*/
                        /*
                        if (scalar != DBNull.Value && scalar != null && row.Cells[11].Value.ToString() != "") // Case where the DB value is null
                        {
                            string g = scalar.ToString();
                            double availablerased = Convert.ToDouble(g);
                            double newrased;
                            double quan = Convert.ToDouble(row.Cells[11].Value);
                            string xx = "select QuanArrived from T_Edafa where Edafa_No=@x and Edafa_FY=@Y and Bnd_No=@Z";
                            cmd2 = new SqlCommand(xx, Constants.con);


                            cmd2.Parameters.AddWithValue("@X", TXT_EdafaNo.Text);//stock_no_all
                            cmd2.Parameters.AddWithValue("@Y", Cmb_FY2.Text);//stock_no_all
                            cmd2.Parameters.AddWithValue("@Z",row.Cells[6].Value.ToString());//stock_no_all

                            var scalar2 = cmd2.ExecuteScalar();
                            double oldvalue = Convert.ToDouble(scalar2.ToString());
                            newrased = availablerased - oldvalue + quan;
                            //dataGridView1.Rows[e.RowIndex].Cells[10].Value = newrased;
                            executemsg = true;


                        }
                        else
                        {

                        }*/
                    }

                }
            }
        }


        public void UpdateEdafa()
        {
            Constants.opencon();

            string cmdstring = "Exec SP_DeleteEdafa @TNO,@FY,@TRNO,@aot output";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", MNO);
            cmd.Parameters.AddWithValue("@FY", FY2);
            cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text);
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

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    cmdstring = "exec SP_UpdateEdafa @fff,@p1old,@p2old,@p1,@p2,@p3,@p4,@p44,@p444,@p5,@p55,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p34 out,@p35";
                    cmd = new SqlCommand(cmdstring, Constants.con);
                    cmd.Parameters.AddWithValue("@fff", FlagSign3);
                    cmd.Parameters.AddWithValue("@p1old", MNO);
                    cmd.Parameters.AddWithValue("@p2old", FY2);
                    cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_EdafaNo.Text));
                    cmd.Parameters.AddWithValue("@p3", Convert.ToInt32(Cmb_AmrNo.Text));
                    cmd.Parameters.AddWithValue("@p2", (Cmb_FY2.Text));
                    cmd.Parameters.AddWithValue("@p4", (Cmb_FY.Text));
                    cmd.Parameters.AddWithValue("@p44", (row.Cells[4].Value));
                    cmd.Parameters.AddWithValue("@p444", (row.Cells[5].Value));
                    cmd.Parameters.AddWithValue("@p5", Convert.ToInt32(row.Cells[6].Value));
                    cmd.Parameters.AddWithValue("@p55", (TXT_TRNO.Text));

                    cmd.Parameters.AddWithValue("@p6", (Convert.ToDateTime(TXT_Date.Value.ToShortDateString())));
                    cmd.Parameters.AddWithValue("@p7", Convert.ToDouble(row.Cells[11].Value));
                    cmd.Parameters.AddWithValue("@p8", (row.Cells[24].Value));
                    cmd.Parameters.AddWithValue("@p9", (row.Cells[25].Value));
                    cmd.Parameters.AddWithValue("@p10", (TXT_NameMward.Text));



                    cmd.Parameters.AddWithValue("@p11", (TXT_AccNo.Text));
                    cmd.Parameters.AddWithValue("@p12", DBNull.Value);

                    if (string.IsNullOrEmpty(TXT_MTaklif.Text))
                    {
                        cmd.Parameters.AddWithValue("@p13", DBNull.Value);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p13", Convert.ToDecimal(TXT_MTaklif.Text));

                    }
                    if (string.IsNullOrEmpty(TXT_MResp.Text))
                    {
                        cmd.Parameters.AddWithValue("@p14", DBNull.Value);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p14", Convert.ToDecimal(TXT_MResp.Text));
                    }
                    if (string.IsNullOrEmpty(TXT_Masrof.Text))
                    {
                        cmd.Parameters.AddWithValue("@p15", DBNull.Value);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p15", Convert.ToDecimal(TXT_Masrof.Text));
                    }
                    if (string.IsNullOrEmpty(TXT_Enfak.Text))
                    {
                        cmd.Parameters.AddWithValue("@p16", DBNull.Value);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p16", Convert.ToDecimal(TXT_Enfak.Text));
                    }
                    //  cmd.Parameters.AddWithValue("@p17",Convert.ToDecimal(TXT_Egmali.Text)??DBNull.Value);

                    if (string.IsNullOrEmpty(TXT_Morakba.Text))
                    {
                        cmd.Parameters.AddWithValue("@p17", DBNull.Value);

                    }
                    else
                    {


                        cmd.Parameters.AddWithValue("@p17", Convert.ToDecimal(TXT_Morakba.Text));
                    }
                    if (FlagSign1 == 1)
                    {
                        cmd.Parameters.AddWithValue("@p18", FlagEmpn1);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p18", DBNull.Value);

                    }
                    if (FlagSign2 == 1)
                    {
                        cmd.Parameters.AddWithValue("@p19", FlagEmpn2);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p19", DBNull.Value);

                    }
                    if (FlagSign3 == 1)
                    {
                        cmd.Parameters.AddWithValue("@p20", FlagEmpn3);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p20", DBNull.Value);

                    }
                    if (FlagSign4 == 1)
                    {
                        cmd.Parameters.AddWithValue("@p21", FlagEmpn4);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p21", DBNull.Value);

                    }

                    cmd.Parameters.AddWithValue("@p22", Constants.User_Name.ToString());
                    cmd.Parameters.AddWithValue("@p23", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    // cmd.Parameters.AddWithValue("@p233", TXT_TRNO.Text.ToString());
                    cmd.Parameters.Add("@p34", SqlDbType.Int, 32);  //-------> output parameter
                    cmd.Parameters["@p34"].Direction = ParameterDirection.Output;

                    if (string.IsNullOrEmpty(row.Cells[29].Value.ToString()))
                    {
                        cmd.Parameters.AddWithValue("@p35", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p35", Convert.ToDateTime(row.Cells[29].Value));
                    }
                    //int flag=0;

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
                }
            }
            if (FlagSign3 == 1)
            {

                if (TXT_TRNO.Text.ToString() == "70")/////mobashr
                {
                    UpdateQuan();
                    InsertTrans();

                    UpdateQuan2();
                    InsertTrans2();
                }
                else
                { 
                    UpdateQuan();
                    InsertTrans();
                }



            }
            if (executemsg == true && flag == 1)
            {
                if(Constants.User_Type == "A")
                {
                    string st = "exec  SP_UpdateEdaraNotfication @p1,@p2,@p3,@p4";
                    SqlCommand cmd1 = new SqlCommand(st, Constants.con);

                    cmd1.Parameters.AddWithValue("@p1", TXT_EdafaNo.Text);
                    cmd1.Parameters.AddWithValue("@p2", Cmb_FY2.Text);
                    cmd1.Parameters.AddWithValue("@p3", Constants.CodeEdara);
                    cmd1.Parameters.AddWithValue("@p4", TXT_TRNO.Text);

                    cmd1.ExecuteNonQuery();
                }
                else if (Constants.User_Type == "B")
                {
                    if (FlagSign4 != 1 && Constants.UserTypeB != "Finance")
                    {
                        string st = "exec SP_DeleteEdaraAlarm @p2,@p3,@p4";
                        SqlCommand cmd1 = new SqlCommand(st, Constants.con);

                        // cmd1.Parameters.AddWithValue("@p1", row.Cells[7].Value);


                        cmd1.Parameters.AddWithValue("@p2", Convert.ToInt32(TXT_EdafaNo.Text));

                        cmd1.Parameters.AddWithValue("@p3", (Cmb_FY2.Text));
                        cmd1.Parameters.AddWithValue("@p4", (TXT_TRNO.Text));
                        //  cmd1.ExecuteNonQuery();

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                st = "exec SP_SendEdaraAlarm @p1,@p11,@p111,@p2,@p3,@p33,@p4,@p5,@p6,@p7";
                                cmd1 = new SqlCommand(st, Constants.con);

                                cmd1.Parameters.AddWithValue("@p1", row.Cells[7].Value);
                                cmd1.Parameters.AddWithValue("@p11", row.Cells[8].Value);
                                cmd1.Parameters.AddWithValue("@p111", row.Cells[6].Value);

                                cmd1.Parameters.AddWithValue("@p2", Convert.ToInt32(TXT_EdafaNo.Text));
                                cmd1.Parameters.AddWithValue("@p4", Convert.ToInt32(Cmb_AmrNo.Text));
                                cmd1.Parameters.AddWithValue("@p3", (Cmb_FY2.Text));
                                cmd1.Parameters.AddWithValue("@p33", (TXT_TRNO.Text));// cmd1.Parameters.AddWithValue("@p3", (Cmb_FY2.Text));
                                cmd1.Parameters.AddWithValue("@p5", (Cmb_FY.Text));
                                // cmd.Parameters.AddWithValue("@p5", Convert.ToInt32(row.Cells[6].Value));

                                //  cmd.Parameters.AddWithValue("@p6", (Convert.ToDateTime(TXT_Date.Value.ToShortDateString())));
                                cmd1.Parameters.AddWithValue("@p6", Constants.User_Name.ToString());
                                cmd1.Parameters.AddWithValue("@p7", Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                                cmd1.ExecuteNonQuery();
                            }
                        }
                    }

                }


                UpdateEdafaSignatureCycle();

                MessageBox.Show("تم التعديل بنجاح  ! ");

                reset();
            }
            else if (executemsg == true && flag == 2)
            {
                MessageBox.Show("تم إدخال رقم الاضافة المخزنية  من قبل  ! ");
            }
            else if (executemsg == false)
            {
                MessageBox.Show("لم يتم تعديل الاضافة المخزنية بنجاج!!");
            }

            Constants.closecon();
        }


        private void EditLogic()
        {
            UpdateEdafa();
        }

        private void DeleteLogic()
        {
            if ((MessageBox.Show("هل تريد حذف الاضافة المخزنية؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(TXT_EdafaNo.Text))
                {
                    MessageBox.Show("يجب اختيار الاضافة المخزنية  اولا");
                    return;
                }
                Constants.opencon();
                string cmdstring = "Exec SP_DeleteEdafa @TNO,@FY,@TRNO,@aot output";

                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_EdafaNo.Text));
                cmd.Parameters.AddWithValue("@FY", Cmb_FY2.Text.ToString());
                cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text.ToString());
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
        private List<(ErrorProvider, Control, string)> ValidateAttachFile()
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

            #region Cmb_FY2
            if (string.IsNullOrWhiteSpace(Cmb_FY2.Text) || Cmb_FY2.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_FY2, "تاكد من  اختيار السنة المالية"));
            }
            #endregion

            #region Cmb_CType
            if (string.IsNullOrWhiteSpace(Cmb_CType.Text) || Cmb_CType.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_CType, "تاكد من  اختيار نوع إذن الصرف"));
            }
            #endregion

            #region TXT_EdafaNo
            if (string.IsNullOrWhiteSpace(TXT_EdafaNo.Text))
            {
                errorsList.Add((errorProvider, TXT_EdafaNo, "يجب اختيار رقم إذن الصرف"));
            }
            #endregion

            return errorsList;
        }

        private List<(ErrorProvider, Control, string)> ValidateSearch(bool isConfirm = false)
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

            if (isConfirm)
            {
                #region Cmb_CType2
                if (string.IsNullOrWhiteSpace(Cmb_CType2.Text) || Cmb_CType2.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_CType2, "تاكد من  اختيار نوع إذن الصرف"));
                }
                #endregion

                #region Cmb_FYear2
                if (string.IsNullOrWhiteSpace(Cmb_FYear2.Text) || Cmb_FYear2.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_FYear2, "تاكد من  اختيار السنة المالية"));
                }
                #endregion

                #region Cmb_EdafaNo2
                if (string.IsNullOrWhiteSpace(Cmb_EdafaNo2.Text) || Cmb_EdafaNo2.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_EdafaNo2, "يجب اختيار رقم إذن الصرف"));
                }
                #endregion
            }
            else
            {
                #region Cmb_CType
                if (string.IsNullOrWhiteSpace(Cmb_CType.Text) || Cmb_CType.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_CType, "تاكد من  اختيار نوع إذن الصرف"));
                }
                #endregion

                #region Cmb_FY2
                if (string.IsNullOrWhiteSpace(Cmb_FY2.Text) || Cmb_FY2.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_FY2, "تاكد من  اختيار السنة المالية"));
                }
                #endregion

                #region TXT_EdafaNo
                if (string.IsNullOrWhiteSpace(TXT_EdafaNo.Text))
                {
                    errorsList.Add((errorProvider, TXT_EdafaNo, "يجب اختيار رقم إذن الصرف"));
                }
                #endregion
            }

            return errorsList;
        }

        private List<(ErrorProvider, Control, string)> ValidateSave()
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

            #region Cmb_FY2
            if (string.IsNullOrWhiteSpace(Cmb_FY2.Text) || Cmb_FY2.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_FY2, "تاكد من  اختيار السنة المالية"));
            }
            #endregion

            #region Cmb_CType
            if (string.IsNullOrWhiteSpace(Cmb_CType.Text) || Cmb_CType.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_CType, "تاكد من  اختيار نوع إذن الصرف"));
            }
            #endregion

            #region TXT_EdafaNo
            if (string.IsNullOrWhiteSpace(TXT_EdafaNo.Text))
            {
                errorsList.Add((errorProvider, TXT_EdafaNo, "يجب اختيار رقم إذن الصرف"));
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

            if (type == VALIDATION_TYPES.ATTACH_FILE)
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
            HelperClass.comboBoxFiller(Cmb_FY, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
            HelperClass.comboBoxFiller(Cmb_FY2, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
            HelperClass.comboBoxFiller(Cmb_FYear2, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);


            if (Constants.Edafa_F == false)
            {
                panel7.Visible = true;
                panel2.Visible = false;
            }
            else if (Constants.Edafa_F == true)
            {
                panel2.Visible = true;
                panel7.Visible = false;
            }


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
            string cmdstring;
            SqlCommand cmd;

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

            //////////////////////////CType/////////////////////
            string cmdstring4 = "SELECT  [CCode],[CName] FROM [T_TransferTypes] where CType=1 and CFlag=1";
            SqlCommand cmd4 = new SqlCommand(cmdstring4, con);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            //---------------------------------
            if (dr4.HasRows == true)
            {
                while (dr4.Read())
                {
                    TypeColl.Add(dr4["CName"].ToString());

                }
            }
            dr3.Close();



            Constants.opencon();

            Cmb_CType.SelectedIndexChanged -= new EventHandler(Cmb_CType_SelectedIndexChanged);
            Cmb_CType2.SelectedIndexChanged -= new EventHandler(Cmb_CType2_SelectedIndexChanged);

            cmdstring = "SELECT  [CCode],[CName] FROM [T_TransferTypes] where CType=1 and CFlag=1";//will use cmdstring3


            cmd = new SqlCommand(cmdstring, Constants.con);

            //cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
            DataTable dts = new DataTable();

            dts.Load(cmd.ExecuteReader());
            Cmb_CType.DataSource = dts;
            Cmb_CType.ValueMember = "CCode";
            Cmb_CType.DisplayMember = "CName";
            Cmb_CType.SelectedIndex = -1;
            Cmb_CType.SelectedIndexChanged += new EventHandler(Cmb_CType_SelectedIndexChanged);

            Cmb_CType2.DataSource = dts;
            Cmb_CType2.ValueMember = "CCode";
            Cmb_CType2.DisplayMember = "CName";
            Cmb_CType2.SelectedIndex = -1;
            Cmb_CType2.SelectedIndexChanged += new EventHandler(Cmb_CType2_SelectedIndexChanged);

            Constants.closecon();


            con.Close();

            reset();
        }


        public FEdafaMakhzania_F()
        {
            InitializeComponent();
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            init();
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
            if (dataGridView1.CurrentCell.ColumnIndex == 11 ){
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

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد اضافة اضافة مخزنية جديدة؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                reset();
                PrepareAddState();

                AddEditFlag = 2;
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

            if ((MessageBox.Show("هل تريد تعديل الاضافة المخزنية؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(Cmb_AmrNo.Text) || string.IsNullOrEmpty(Cmb_FY.Text) || string.IsNullOrEmpty(TXT_TRNO.Text))
                {
                    MessageBox.Show("يجب اختيار نوع الاضافة و رقم الاضافة المخزنية المراد تعديله و السنة المالية");
                    return;
                }
                else
                {
                    AddEditFlag = 1;
                    TNO = Cmb_AmrNo.Text;
                    FY = Cmb_FY.Text;
                    FY2 = Cmb_FY2.Text;
                    MNO = TXT_EdafaNo.Text;

                    PrepareEditState();
                }

            }
        }


        private void Cmb_FY_SelectedIndexChanged(object sender, EventArgs e)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();

            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring = @"select T_Estlam.Amrshraa_No from T_Estlam left join T_Edafa on T_Estlam.Amrshraa_No = T_Edafa.Amrshraa_No
                                where(T_Estlam.Sign3 is not null) and T_Estlam.AmrSheraa_sanamalia =@FY and(T_Edafa.Amrshraa_No is null)
                                group by T_Estlam.Amrshraa_No order by T_Estlam.Amrshraa_No";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
            cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
            ///   cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);

            DataTable dts = new DataTable();

            dts.Load(cmd.ExecuteReader());
            Cmb_AmrNo.DataSource = dts;
            Cmb_AmrNo.ValueMember = "Amrshraa_No";
            Cmb_AmrNo.DisplayMember = "Amrshraa_No";
            Cmb_AmrNo.SelectedIndex = -1;
            Constants.closecon();
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
                    MessageBox.Show("من فضلك تاكد من توقيع الاضافة المخزنية");
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
  

        private void Cmb_FY2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //go and get talbTawreed_no for this FYear
            if (AddEditFlag == 2 && Cmb_FY2.SelectedIndex != -1)//add
            {
                //call sp that get last num that eentered for this MM and this YYYY
                Constants.opencon();
                if (String.IsNullOrEmpty(TXT_TRNO.Text) == true)
                {
                    MessageBox.Show("يجب اختيار نوع الاضافة المخزنية");
                    return;
                }
                // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
                string cmdstring = "select  ( COALESCE(MAX( Edafa_No), 0))  from  T_Edafa where Edafa_FY=@FY  and TR_NO=@TRNO ";
                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
                cmd.Parameters.AddWithValue("@FY", Cmb_FY2.Text);
                cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text);
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
                        TXT_EdafaNo.Text = flag.ToString();//el rakm el new

                    }

                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    Console.WriteLine(sqlEx);
                }
            }
       
        }


        private void Cmb_FYear2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(TXT_TRNO2.Text))
            {
                return;
            }

            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();

            string cmdstring = "";

            if (Constants.User_Type == "A")
            {
                cmdstring = "SELECT [Edafa_No] from T_EdaraNotfication where Edafa_FY=@FY and TR_NO=@TRNO and EdaraCode = '" + Constants.CodeEdara + "' and (Sign4 is null) group by Edafa_No order by  Edafa_No";
            }
            else if (Constants.User_Type == "B")
            {
                if (Constants.UserTypeB == "Edafa")
                { 
                    cmdstring = "SELECT [Edafa_No] from T_Edafa where Edafa_FY=@FY and TR_NO=@TRNO and ( Sign1 is not null ) and (Sign4 is not null) and (Sign3 is null) group by Edafa_No order by  Edafa_No";
                }
            }

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
            cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO2.Text);

            DataTable dts = new DataTable();

            dts.Load(cmd.ExecuteReader());
            Cmb_EdafaNo2.DataSource = dts;
            Cmb_EdafaNo2.ValueMember = "Edafa_No";
            Cmb_EdafaNo2.DisplayMember = "Edafa_No";
            Cmb_EdafaNo2.SelectedIndex = -1;
            Constants.closecon();
        }

        
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11 ||e.ColumnIndex==15) //if second cell
            {
                if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[15].Value != null)
                {

                    Constants.opencon();
                    string x = "select quan from T_Tsnif where STOCK_NO_ALL=@st";
                    SqlCommand cmd = new SqlCommand(x, Constants.con);
                    cmd.Parameters.AddWithValue("@st", dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString());//stock_no_all
                    var scalar = cmd.ExecuteScalar();
                    if (scalar != DBNull.Value && scalar != null && dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString() != "") // Case where the DB value is null
                    {
                        string g = scalar.ToString();
                        double availablerased = Convert.ToDouble(g);
                        double newrased;
                        double quan = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
                        string xx = "select QuanArrived from T_Edafa where Edafa_No=@x and Edafa_FY=@Y and Bnd_No=@Z and TR_NO=@TRNO";
                      SqlCommand  cmd2 = new SqlCommand(xx, Constants.con);


                        cmd2.Parameters.AddWithValue("@X", TXT_EdafaNo.Text);//stock_no_all
                        cmd2.Parameters.AddWithValue("@Y", Cmb_FY2.Text);//stock_no_all
                        cmd2.Parameters.AddWithValue("@Z",dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());//stock_no_all
                        cmd2.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text);
                       var scalar2 = cmd2.ExecuteScalar();
                        if (scalar2 != DBNull.Value && scalar2 != null)
                        {


                            oldvalue = Convert.ToDouble(scalar2.ToString());
                          //  newrased = availablerased - oldvalue + quan; //equation di used lw ana 3deld el quanavailable fel t_tsnif w get a#dl b3d a5er sign
                            newrased = availablerased + quan;
                            dataGridView1.Rows[e.RowIndex].Cells[16].Value = newrased;
                            executemsg = true;
                        }
                        else
                        {
                            oldvalue = 0;
                           // newrased = availablerased - oldvalue + quan;
                            newrased = availablerased + quan;
                            dataGridView1.Rows[e.RowIndex].Cells[16].Value = newrased;
                            executemsg = true;
                        }

                    }
                    else
                    {

                    }
                    Constants.closecon();
                }
            }
            if (e.ColumnIndex == 16)
            {
                if (e.RowIndex >= 0)
                {

                      quan = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString());

                     price = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString());
                     totalprice = ((decimal)quan * price);
                    
                    dataGridView1.Rows[e.RowIndex].Cells[17].Value =totalprice;
                      dataGridView1.Rows[e.RowIndex].Cells[20].Value =totalprice;

                    
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
            }
          
        }


        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
                                    {
                if (e.ColumnIndex == 20)
            {
                if (!string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToString()))
          {
               // your code goes here
         
            decimal total = table.AsEnumerable().Sum(row => row.Field<decimal>("TotalPriceAfter"));
                            //  TXT_Egmali.Text = total.ToString("N2");
                             
            //    dataGridView1.FooterRow.Cells[1].Text = "Total";
            //   dataGridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
               string edara = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
               TXT_Edara.Text += edara;
            }
  
            }}

        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==15 ||e.ColumnIndex==11)
            {
                if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[15].Value != null)
                {

                    Constants.opencon();
                    string x = "select quan from T_Tsnif where STOCK_NO_ALL=@st";
                    SqlCommand cmd = new SqlCommand(x, Constants.con);
                    cmd.Parameters.AddWithValue("@st", dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString());//stock_no_all
                    var scalar = cmd.ExecuteScalar();
                    if (scalar != DBNull.Value && scalar != null && dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString() != "") // Case where the DB value is null
                    {
                        string g = scalar.ToString();
                        double availablerased = Convert.ToDouble(g);
                        double newrased;
                        double quan = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
                        string xx = "select QuanArrived from T_Edafa where Edafa_No=@x and Edafa_FY=@Y and Bnd_No=@Z  and TR_NO=@TRNO";
                        SqlCommand cmd2 = new SqlCommand(xx, Constants.con);


                        cmd2.Parameters.AddWithValue("@X", TXT_EdafaNo.Text);//stock_no_all
                        cmd2.Parameters.AddWithValue("@Y", Cmb_FY2.Text);//stock_no_all
                        cmd2.Parameters.AddWithValue("@Z", dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());//stock_no_all
                        cmd2.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text);//stock_no_all
                        var scalar2 = cmd2.ExecuteScalar();
                        if (scalar2 != DBNull.Value && scalar2 != null)
                        {


                            oldvalue = Convert.ToDouble(scalar2.ToString());
                            //  newrased = availablerased - oldvalue + quan; //equation di used lw ana 3deld el quanavailable fel t_tsnif w get a#dl b3d a5er sign
                            newrased = availablerased + quan;
                            dataGridView1.Rows[e.RowIndex].Cells[16].Value = newrased;
                            executemsg = true;
                        }
                        else
                        {
                            oldvalue = 0;
                            // newrased = availablerased - oldvalue + quan;
                            newrased = availablerased + quan;
                            dataGridView1.Rows[e.RowIndex].Cells[16].Value = newrased;
                            executemsg = true;
                        }

                    }
                    else
                    {

                    }
                    Constants.closecon();
                }
            }
            
            
            /*
            if (e.ColumnIndex == 20 && changedflag == 1)
            {


                    // your code goes here

                    //decimal total = table.AsEnumerable().Sum(row => row.Field<decimal>("TotalPriceAfter"));
                 //   decimal total = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());

                    decimal sum = 0;
                    string edara="";
                    string talbtawreed = "";
                    string bndmwazna = "-"; 
                                                foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!(row.Cells[e.ColumnIndex].Value == null || row.Cells[e.ColumnIndex].Value ==DBNull.Value))
                        {

                            sum = sum + Convert.ToDecimal(row.Cells[e.ColumnIndex].Value.ToString());
                            if (e.RowIndex == 0)
                            {


                                edara = edara + row.Cells[8].Value.ToString() ;
                                talbtawreed = talbtawreed + row.Cells[5].Value.ToString() ;
                                bndmwazna = bndmwazna + row.Cells[9].Value.ToString() ;
                                TXT_Egmali.Text = sum.ToString("N2");
                                TXT_Edara.Text = edara;
                                TXT_BndMwazna.Text = bndmwazna;
                                TXT_TalbNo.Text = talbtawreed;
                            }
                            else if (e.RowIndex > 0)
                            {
                                edara = edara + row.Cells[8].Value.ToString() + "-";
                                talbtawreed = talbtawreed + row.Cells[5].Value.ToString() + "-";
                                bndmwazna = bndmwazna + row.Cells[9].Value.ToString() + "-";
                                TXT_Egmali.Text = sum.ToString("N2");
                                TXT_Edara.Text = edara;
                                TXT_BndMwazna.Text = bndmwazna;
                                TXT_TalbNo.Text = talbtawreed;
                            }

                        }
                    }
            }*/
        }


        ////////////////////

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                var oldValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                var newValue = e.FormattedValue;
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد طباعة اخطار مهمات غير مطابقة /عجز ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
          
                Constants.FormNo = 3;
                Constants.EdafaNo = Convert.ToInt32(TXT_EdafaNo.Text);
                Constants.EdafaFY =(Cmb_FY2.Text);

                FReports F = new FReports();
                F.Show();

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            if ((MessageBox.Show("هل تريد طباعة نموذج استعجال مطابقة فنية ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {

                Constants.FormNo =4 ;
                Constants.EdafaNo = Convert.ToInt32(TXT_EdafaNo.Text);
                Constants.EdafaFY = (Cmb_FY2.Text);
              //  Constants.MangerName = Ename4;
                Constants.opencon();
                string st="exec SP_GeTNameModerEdara @Ec,@aot out";
                SqlCommand cmd=new SqlCommand(st,Constants.con);

                cmd.Parameters.AddWithValue("Ec", dataGridView1.Rows[0].Cells[7].Value.ToString());
                cmd.Parameters.Add("@aot", SqlDbType.NVarChar, 500);  //-------> output parameter
                cmd.Parameters["@aot"].Direction = ParameterDirection.Output;

               

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

                if (executemsg)
                {
                    Constants.MangerName = (string)cmd.Parameters["@aot"].Value;
                }

                Constants.closecon();
                //GET NAME MODER 3AM


                FReports F = new FReports();
                F.Show();

            }
        }


        private void BTN_Print_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد طباعة تقرير الاضافة المخزنية؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_EdafaNo.Text) || string.IsNullOrEmpty(Cmb_FY2.Text))
                {
                    MessageBox.Show("يجب اختيار الاضافة المخزنية المراد طباعتها اولا");
                    return;
                }
                else
                {

                    Constants.FormNo = 5;
                    Constants.EdafaNo = Convert.ToInt32(TXT_EdafaNo.Text);
                    Constants.EdafaFY = Cmb_FY2.Text;
                    FReports F = new FReports();
                    F.Show();
                }
            }
        }

        
        public int CheckDirect70()
        {
            Constants.opencon();
            string cmdstring = "exec sp_CheckDirect70  @A,@F,@aot out";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@A", Convert.ToInt32(Cmb_AmrNo.SelectedValue.ToString()));
            cmd.Parameters.AddWithValue("@F", Cmb_FY2.Text.ToString());
           
            cmd.Parameters.Add("@aot", SqlDbType.Int, 32);  //-------> output parameter
            cmd.Parameters["@aot"].Direction = ParameterDirection.Output;

            int flag=0;

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
                // MessageBox.Show("تم الحذف بنجاح");
                //   Input_Reset();
            }
            return flag;
         
        }

        private void Cmb_AmrNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (AddEditFlag == 2 && Cmb_AmrNo.SelectedIndex != -1)
            {
                if (directflag == 1)
                {
                    int x = CheckDirect70();
                    if (x != 1)
                    {
                        MessageBox.Show(" يحتوى على تصنييفات غير مباشرة برجاء اعادة الاختيار ");
                        return;
                    }
                }
                cleargridview();

                GetAmrSheraaData(Cmb_AmrNo.SelectedValue.ToString(), Cmb_FY.Text);
            }
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 11 || e.ColumnIndex == 15) //if second cell
            {
                if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[15].Value != null)
                {

                    Constants.opencon();
                    string x = "select quan from T_Tsnif where STOCK_NO_ALL=@st";
                    SqlCommand cmd = new SqlCommand(x, Constants.con);
                    cmd.Parameters.AddWithValue("@st", dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString());//stock_no_all
                    var scalar = cmd.ExecuteScalar();
                    if (scalar != DBNull.Value && scalar != null && dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString() != "") // Case where the DB value is null
                    {
                        string g = scalar.ToString();
                        double availablerased = Convert.ToDouble(g);
                        double newrased;
                        double quan = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
                        string xx = "select QuanArrived from T_Edafa where Edafa_No=@x and Edafa_FY=@Y and Bnd_No=@Z and TR_NO=@TRNO";
                        SqlCommand cmd2 = new SqlCommand(xx, Constants.con);


                        cmd2.Parameters.AddWithValue("@X", TXT_EdafaNo.Text);//stock_no_all
                        cmd2.Parameters.AddWithValue("@Y", Cmb_FY2.Text);//stock_no_all
                        cmd2.Parameters.AddWithValue("@Z", dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());//stock_no_all
                        cmd2.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text);
                        var scalar2 = cmd2.ExecuteScalar();
                        if (scalar2 != DBNull.Value && scalar2 != null)
                        {


                            oldvalue = Convert.ToDouble(scalar2.ToString());
                            //  newrased = availablerased - oldvalue + quan; //equation di used lw ana 3deld el quanavailable fel t_tsnif w get a#dl b3d a5er sign
                            newrased = availablerased + quan;
                            dataGridView1.Rows[e.RowIndex].Cells[16].Value = newrased;
                            executemsg = true;
                        }
                        else
                        {
                            oldvalue = 0;
                            // newrased = availablerased - oldvalue + quan;
                            newrased = availablerased + quan;
                            dataGridView1.Rows[e.RowIndex].Cells[16].Value = newrased;
                            executemsg = true;
                        }

                    }
                    else
                    {

                    }
                    Constants.closecon();
                }
            }
        }

        private void Cmb_CType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmb_FY.Text = "";
            Cmb_FY.SelectedIndex = -1;
            Cmb_FY.ResetText();
            TXT_EdafaNo.Text = "";
            directflag = 0;
           // Cmb_CType.SelectedIndexChanged += new EventHandler(Cmb_CType_SelectedIndexChanged);
            if (Cmb_CType.SelectedValue == null)
            {

            }
            else
            {


                TXT_TRNO.Text = Cmb_CType.SelectedValue.ToString();
                if (TXT_TRNO.Text.ToString() == "70")
                {
                    directflag = 1;

                    CH_Direct.Checked = true;
                    
                }
                else
                {
                    directflag = 0;

                    CH_Direct.Checked = false;
                }
            }
            



            ////////////////////////////////////////////////
          //  Constants.closecon();

        }

        private void TXT_TRNO_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("مميز مستند ", TXT_TRNO);
        }


































        //------------------------------------------ Signature Handler ---------------------------------
        #region Signature Handler
        private void BTN_Sigm1_Click(object sender, EventArgs e)
        {
            Empn1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع مخزن الاستلام", "");

            Sign1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مخزن الاستلام", "");

            if (Sign1 != "" && Empn1 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("1", "5", Sign1, Empn1);
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
            Empn2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع المخازن", "");

            Sign2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع المخازن", "");

            if (Sign2 != "" && Empn2 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("2", "5", Sign2, Empn2);
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
            }
        }
        private void BTN_Sign3_Click(object sender, EventArgs e)
        {
            Empn3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "اعتماد مدير عام م المخازن", "");

            Sign3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "اعتماد مدير عام م المخازن", "");

            if (Sign3 != "" && Empn3 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("3", "5", Sign3, Empn3);
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
        private void BTN_Sign4_Click(object sender, EventArgs e)
        {
            Empn4 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع الادارة الطالبة", "");

            Sign4 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع الادارة الطالبة", "");

            if (Sign4 != "" && Empn4 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("4", "5", Sign4, Empn4);
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
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteLogic();
        }
        #endregion

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

            string amr_no = TXT_EdafaNo.Text;
            string fyear = Cmb_FY2.Text;
            string momayz = TXT_TRNO.Text;

            reset();

            if (SearchEdafa(amr_no, fyear, momayz))
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

            string edafa_no = Cmb_EdafaNo2.Text;
            string fyear = Cmb_FYear2.Text;
            string momayz = TXT_TRNO2.Text;

            reset();

            if (SearchEdafa(edafa_no, fyear, momayz))
            {
                EditBtn2.Enabled = true;
                BTN_Print2.Enabled = true;
            }

            TXT_EdafaNo.Enabled = false;
            Cmb_FY2.Enabled = false;
            Cmb_CType.Enabled = false;
        }

        private void EditBtn2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد تعديل الاضافة المخزنية؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(Cmb_AmrNo.Text) || string.IsNullOrEmpty(Cmb_FY.Text) || string.IsNullOrEmpty(TXT_TRNO.Text))
                {
                    MessageBox.Show("يجب اختيار نوع الاضافة و رقم الاضافة المخزنية المراد تعديله و السنة المالية");
                    return;
                }

                PrepareConfirmState();
            }
        }

        private void BTN_Save2_Click(object sender, EventArgs e)
        {

            if (!IsValidCase(VALIDATION_TYPES.SAVE))
            {
                return;
            }

            EditLogic();

            reset();

            Cmb_CType2.SelectedIndex = -1;
            Cmb_EdafaNo2.SelectedIndex = -1;
            Cmb_FYear2.SelectedIndex = -1;

            TXT_EdafaNo.Enabled = false;
            Cmb_FY2.Enabled = false;
            Cmb_CType.Enabled = false;
        }

        private void BTN_Print2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد طباعة تقرير الاضافة المخزنية؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_EdafaNo.Text) || string.IsNullOrEmpty(Cmb_FY2.Text))
                {
                    MessageBox.Show("يجب اختيار الاضافة المخزنية المراد طباعتها اولا");
                    return;
                }
                else
                {

                    Constants.FormNo = 5;
                    Constants.EdafaNo = Convert.ToInt32(TXT_EdafaNo.Text);
                    Constants.EdafaFY = Cmb_FY2.Text;
                    FReports F = new FReports();
                    F.Show();
                }
            }
        }

        private void Cmb_CType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(Cmb_CType2.Text) || string.IsNullOrWhiteSpace(Cmb_CType2.Text) || Cmb_CType2.SelectedIndex == -1))
            {
                TXT_TRNO2.Text = Cmb_CType2.SelectedValue.ToString();
            }
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

                    path += Cmb_FY2.Text + @"\";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    path += "EDAFA_MAKHZANIA" + @"\";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }


                    path += TXT_TRNO.Text + @"\";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    path += TXT_EdafaNo.Text + @"\";

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

            popup.WholePath = @"\\172.18.8.83\MaterialAPP\PDF\" + Constants.CodeEdara + @"\" + Cmb_FY2.Text + @"\EDAFA_MAKHZANIA\" + TXT_TRNO.Text + @"\" + TXT_EdafaNo.Text + @"\";
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
    }
}
