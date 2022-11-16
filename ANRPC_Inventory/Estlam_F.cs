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
            ATTACH_FILE,
            SEARCH,
            CONFIRM_SEARCH,
            SAVE,
        }
        int currentSignNumber = 0;
        #endregion

        //------------------------------------------ Helper ---------------------------------
        #region Helpers
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
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
        }

        public void SP_UpdateSignatures(int x, DateTime D1, DateTime? D2 = null)
        {
            string cmdstring = "Exec  SP_UpdateSignDatesEstlam  @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

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

        private void GetEstlamBnod(string amrNo, string fyear,bool isConfirm = false)
        {

            string TableQuery;

            if (isConfirm)
            {
                TableQuery = "select  Amrshraa_No,AmrSheraa_sanamalia,TalbTwareed_No,FYear,Bnd_No,Quan,QuanArrived,BayanBnd,EstlamFlag,EstlamDate from T_Estlam Where  Amrshraa_No = " + amrNo + " and AmrSheraa_sanamalia='" + fyear + "' and date='" + Convert.ToDateTime(TXT_Date.Value.ToShortDateString()) + "'";
            }
            else
            {
                TableQuery = "SELECT *  FROM [T_BnodAwamershraa] Where (quan2 is null or quan2<quan) and Amrshraa_No = " + amrNo + " and AmrSheraa_sanamalia='" + fyear + "'";
            }


            table.Clear();

            dataadapter = new SqlDataAdapter(TableQuery, Constants.con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataadapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.Refresh();

            if (isConfirm == true)
            {
                dataGridView1.Columns["Amrshraa_No"].HeaderText = "رقم أمر الشراء";//col0

                dataGridView1.Columns["AmrSheraa_sanamalia"].HeaderText = "امر الشراء سنةمالية";//col1

                dataGridView1.Columns["TalbTwareed_No"].HeaderText = "رقم طلب التوريد";//col2

                dataGridView1.Columns["FYear"].HeaderText = "سنة مالية طلب التوريد";//col3

                dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col4

                dataGridView1.Columns["Quan"].HeaderText = "الكمية المطلوبة";//col5

                dataGridView1.Columns["QuanArrived"].HeaderText = "الكمية  الواردة ";//col6

                dataGridView1.Columns["BayanBnd"].HeaderText = "بيان المهمات";//col7

                dataGridView1.Columns["EstlamFlag"].HeaderText = "تم الاستلام ";//col8

                dataGridView1.Columns["EstlamDate"].HeaderText = "تاريخ الاستلام ";//col9

            }

            else
            {
                dataGridView1.Columns["Amrshraa_No"].HeaderText = "رقم أمر الشراء";//col0

                dataGridView1.Columns["AmrSheraa_sanamalia"].HeaderText = "امر الشراء سنةمالية";//col3

                dataGridView1.Columns["TalbTwareed_No"].HeaderText = "رقم طلب التوريد";//col4

                dataGridView1.Columns["FYear"].HeaderText = "سنة مالية طلب التوريد";//col5

                dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col6

                dataGridView1.Columns["Quan"].HeaderText = "الكمية المطلوبة";//col10

                dataGridView1.Columns["Quan2"].HeaderText = "الكمية الكلية  الواردة ";//col11

                dataGridView1.Columns["Bayan"].HeaderText = "بيان المهمات";//col13

                dataGridView1.Columns["EstlamFlag"].HeaderText = "تم الاستلام ";//col22

                dataGridView1.Columns["EstlamDate"].HeaderText = "تاريخ الاستلام ";//col23


                dataGridView1.Columns["Monaksa_No"].HeaderText = " رقم المناقصة";//col1
                dataGridView1.Columns["Monaksa_No"].Visible = false;

                dataGridView1.Columns["monaksa_sanamalia"].HeaderText = "مناقصةسنةمالية";//col2
                dataGridView1.Columns["monaksa_sanamalia"].Visible = false;

                dataGridView1.Columns["CodeEdara"].HeaderText = "كود ادارة";//col7
                dataGridView1.Columns["CodeEdara"].Visible = false;

                dataGridView1.Columns["NameEdara"].HeaderText = "الادارة الطالبة";//col8
                dataGridView1.Columns["NameEdara"].Visible = false;

                dataGridView1.Columns["BndMwazna"].HeaderText = "بند موازنة";//col9
                dataGridView1.Columns["BndMwazna"].Visible = false;

                dataGridView1.Columns["Unit"].HeaderText = "الوحدة";//col12
                dataGridView1.Columns["Unit"].Visible = false;

                dataGridView1.Columns["Makhzn"].HeaderText = "مخزن";//col14
                dataGridView1.Columns["Makhzn"].Visible = false;

                dataGridView1.Columns["Rakm_Tasnif"].HeaderText = "رقم التصنيف";//col15
                dataGridView1.Columns["Rakm_Tasnif"].Visible = false;

                dataGridView1.Columns["Rased_After"].HeaderText = "رصيد بعد";//col16
                dataGridView1.Columns["Rased_After"].Visible = false;

                dataGridView1.Columns["UnitPrice"].HeaderText = "سعر الوحدة";//col17
                dataGridView1.Columns["UnitPrice"].Visible = false;

                dataGridView1.Columns["TotalPrice"].HeaderText = "الثمن الاجمالى";//col18
                dataGridView1.Columns["TotalPrice"].Visible = false;

                dataGridView1.Columns["ApplyDareba"].HeaderText = "تطبق الضريبة";//col19
                dataGridView1.Columns["ApplyDareba"].Visible = false;

                dataGridView1.Columns["Darebapercent"].HeaderText = "نسبة الضريبة";//col20
                dataGridView1.Columns["Darebapercent"].Visible = false;

                dataGridView1.Columns["TotalPriceAfter"].HeaderText = "السعر الاجمالى ";//col21
                dataGridView1.Columns["TotalPriceAfter"].Visible = false;

                dataGridView1.Columns["LessQuanFlag"].HeaderText = "يوجد عجز ";//col24
                dataGridView1.Columns["LessQuanFlag"].Visible = false;

                dataGridView1.Columns["NotIdenticalFlag"].HeaderText = "مطابق/غير مطابق ";//col25
                dataGridView1.Columns["NotIdenticalFlag"].Visible = false;

                dataGridView1.Columns["TalbEsdarShickNo"].HeaderText = "طلب اصدار الشيك ";//col25
                dataGridView1.Columns["TalbEsdarShickNo"].Visible = false;

                dataGridView1.Columns["ShickNo"].HeaderText = "رقم الشيك ";//col25
                dataGridView1.Columns["ShickNo"].Visible = false;

                dataGridView1.Columns["ShickDate"].HeaderText = "تاريخ الشيك ";//col25
                dataGridView1.Columns["ShickDate"].Visible = false;

            }
        }

        public bool SearchEstlam(string amrNo, string fyear,bool isConfirm = true)
        {
            Constants.opencon();

            string cmdstring;
            SqlCommand cmd;

            cmdstring = "select * from T_Estlam where Amrshraa_No=@TN and AmrSheraa_sanamalia=@FY and date=@D";
            cmd = new SqlCommand(cmdstring, Constants.con);


            cmd.Parameters.AddWithValue("@TN", amrNo);
            cmd.Parameters.AddWithValue("@FY", fyear);
            cmd.Parameters.AddWithValue("@D", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));           

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                try
                {
                    while (dr.Read())
                    {                   
                        TXT_NameMward.Text = dr["NameMward"].ToString();
                        TXT_Date.Text = dr["Date"].ToString();
                        TXT_QuanBnod.Text = dr["Quan_Bnd"].ToString();
                        TXT_Sanf.Text = dr["BayanSanf"].ToString();
                        TXT_QuanTard.Text = dr["Quan_Tard"].ToString();

                        string s1 = dr["Sign1"].ToString();
                        string s2 = dr["Sign2"].ToString();
                        string s3 = dr["Sign3"].ToString();

                        if (s1 != "")
                        {
                            string p = Constants.RetrieveSignature("1", "4", s1);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename1 = p.Split(':')[1];
                                wazifa1 = p.Split(':')[2];
                                pp = p.Split(':')[0];

                                ((PictureBox)this.signatureTable.Controls["panel6"].Controls["Pic_Sign" + "1"]).Image = Image.FromFile(@pp);

                                FlagSign1 = 1;
                                FlagEmpn1 = s1;
                                ((PictureBox)this.signatureTable.Controls["panel6"].Controls["Pic_Sign" + "1"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign1, Ename1 + Environment.NewLine + wazifa1);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.signatureTable.Controls["panel6"].Controls["Pic_Sign" + "1"]).BackColor = Color.Red;
                        }
                        if (s2 != "")
                        {
                            string p = Constants.RetrieveSignature("2", "4", s2);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename2 = p.Split(':')[1];
                                wazifa2 = p.Split(':')[2];
                                pp = p.Split(':')[0];

                                ((PictureBox)this.signatureTable.Controls["panel8"].Controls["Pic_Sign" + "2"]).Image = Image.FromFile(@pp);

                                FlagSign2 = 1;
                                FlagEmpn2 = s2;
                                ((PictureBox)this.signatureTable.Controls["panel8"].Controls["Pic_Sign" + "2"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign2, Ename2 + Environment.NewLine + wazifa2);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.signatureTable.Controls["panel8"].Controls["Pic_Sign" + "2"]).BackColor = Color.Red;
                        }
                        if (s3 != "")
                        {
                            string p = Constants.RetrieveSignature("3", "4", s3);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename3 = p.Split(':')[1];
                                wazifa3 = p.Split(':')[2];
                                pp = p.Split(':')[0];

                                ((PictureBox)this.signatureTable.Controls["panel9"].Controls["Pic_Sign" + "3"]).Image = Image.FromFile(@pp);

                                FlagSign3 = 1;
                                FlagEmpn3 = s3;
                                ((PictureBox)this.signatureTable.Controls["panel9"].Controls["Pic_Sign" + "3"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign3, Ename3 + Environment.NewLine + wazifa3);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.signatureTable.Controls["panel9"].Controls["Pic_Sign" + "3"]).BackColor = Color.Red;
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
                MessageBox.Show("من فضلك تاكد من تاريخ الاستلام و رقم امر الشراء");
                reset();
                return false;
            }
            dr.Close();

            Cmb_FY.Text = fyear;
            Cmb_AmrNo.Text = amrNo;

            GetEstlamBnod(amrNo, fyear, isConfirm);

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
            changePanelState(panel5, true);

            //moward sec
            changePanelState(panel3, true);


            //btn Section
            //generalBtn
            SaveBtn.Enabled = true;
            BTN_Cancel.Enabled = true;
            browseBTN.Enabled = true;
            BTN_PDF.Enabled = true;

            Addbtn.Enabled = false;
            EditBtn.Enabled = false;
            BTN_Search.Enabled = false;
            BTN_Print.Enabled = false;
            DeleteBtn.Enabled = false;

            //signature btn
            changePanelState(signatureTable, false);
            BTN_Sigm1.Enabled = true;

            changeDataGridViewColumnState(dataGridView1, true);

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
                if (Constants.UserTypeB == "Estlam")
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
                }
            }

            AddEditFlag = 1;
            TNO = Cmb_AmrNo.Text;
            FY = Cmb_FY.Text;
            Dateold = Convert.ToDateTime(TXT_Date.Value.ToShortDateString());
        }

        public void prepareSearchState()
        {
            DisableControls();
            Input_Reset();

            if (Constants.Estlam_F)
            {
                Cmb_FY.Enabled = true;
                Cmb_AmrNo.Enabled = true;
                BTN_Print.Enabled = true;
            }
            
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
            BTN_Search_Motab3a.Enabled = true;

            BTN_Save2.Enabled = false;
            SaveBtn.Enabled = false;
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
        }

        public void Input_Reset()
        {
            //fyear sec
            TXT_Date.Value = DateTime.Today;
            TXT_DateEstlam.Value = DateTime.Today;
            Cmb_FY.Text = "";
            Cmb_FY.SelectedIndex = -1;

            Cmb_AmrNo.Text = "";
            Cmb_AmrNo.SelectedIndex = -1;


            //moward sec
            TXT_NameMward.Text = "";
            TXT_QuanBnod.Text = "";
            TXT_QuanTard.Text = "";
            TXT_Sanf.Text = "";


            //search sec
            Cmb_FYear2.Text = "";
            Cmb_FYear2.SelectedIndex = -1;

            Cmb_AmrNo2.Text = "";
            Cmb_AmrNo2.SelectedIndex = -1;

            resetSignature();

            cleargridview();

            AddEditFlag = 0;
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

                    //  if (row.Cells[22].Value != DBNull.Value)
                    if (row.Cells[11].Value != DBNull.Value && row.Cells[11].Value != null && row.Cells[11].Value.ToString() != "")
                    {

                        string cmdstring = "exec SP_InsertEstlam @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p77,@p777,@p8,@p9,@p10,@p17,@p188,@p18,@p1888,@p11,@p12,@p13,@p14,@p15,@p16";

                        SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);


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
                            cmd.Parameters.AddWithValue("@p18", 0);
                            cmd.Parameters.AddWithValue("@p188", row.Cells[10].Value);//
                            cmd.Parameters.AddWithValue("@p17", 0);//type goz2i koly no estlam// zero==>no estlam
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@p188", row.Cells[10].Value);//
                                                                                      ////////////////////////////////////////////////
                            string st = "exec SP_GetAllQuanArrived @p1,@p2,@p3,@p4,@p5,@p6 out";
                            SqlCommand cmd2 = new SqlCommand(st, Constants.con);

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
                            else if (sumquan > 0)
                            {

                                currentTotal = currentTotal - sumquan;
                                cmd.Parameters.AddWithValue("@p18", currentTotal);//


                            }

                            ///////////////////////////////////////////////////////////////////////////

                            if (String.Compare(row.Cells[11].Value.ToString(), row.Cells[10].Value.ToString()) == 0)
                            {
                                cmd.Parameters.AddWithValue("@p17", 1);//type goz2i koly no estlam// two  ====> all kmya
                            }
                            else if (String.Compare(row.Cells[11].Value.ToString(), row.Cells[10].Value.ToString()) < 0)
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
                        }
                        catch (SqlException sqlEx)
                        {
                            executemsg = false;
                            Console.WriteLine(sqlEx);
                        }
                    }
                }
            }

            if (executemsg == true)
            {
                for (int i = 1; i <= 3; i++)
                {


                    string cmdstring = "Exec  SP_InsertSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
                    SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

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
                MessageBox.Show("تم الإضافة بنجاح  ! ");
                reset();

            }
            else if (executemsg == false)
            {
                MessageBox.Show("لم يتم إدخال طلب الاستلام بنجاج!!");
            }

            Constants.closecon();

        }

        private void UpdateEstlamSignatureCycle()
        {
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
        public void UpdateEstlam()
        {
            Constants.opencon();

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

                        SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                        cmd.Parameters.AddWithValue("@ff1", FlagSign3);
                        cmd.Parameters.AddWithValue("@o1", TXT_Date.Value.ToShortDateString());
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
                            Console.WriteLine(sqlEx);
                            //   flag = (int)cmd.Parameters["@p34"].Value;
                        }
                    }
                }
            }

            if(executemsg == true)
            {
                UpdateEstlamSignatureCycle();

                MessageBox.Show("تم التعديل بنجاح  ! ");

                reset();
            }
            
            else if (executemsg == false)
            {
                MessageBox.Show("لم يتم تعديل طلب الاستلام بنجاج!!");
            }

            Constants.closecon();
        }
        private void EditLogic()
        {
            UpdateEstlam();
        }
        #endregion


        //------------------------------------------ Validation Handler ---------------------------------
        #region Validation Handler
        private List<(ErrorProvider, Control, string)> ValidateAttachFile()
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

            #region Cmb_FY
            if (string.IsNullOrWhiteSpace(Cmb_FY.Text) || Cmb_FY.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_FY, "تاكد من  اختيار السنة المالية"));
            }
            #endregion
            #region Cmb_AmrNo
            if (string.IsNullOrWhiteSpace(Cmb_AmrNo.Text) || Cmb_AmrNo.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_AmrNo, "تاكد من اختيار رقم أمر الشراء"));
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
                    errorsList.Add((errorProvider, Cmb_AmrNo, "تاكد من اختيار رقم أمر الشراء"));
                }
                #endregion
            }
            else
            {
                #region Cmb_FY
                if (string.IsNullOrWhiteSpace(Cmb_FY.Text) || Cmb_FY.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_FY, "تاكد من  اختيار السنة المالية"));
                }
                #endregion

                #region Cmb_AmrNo
                if (string.IsNullOrWhiteSpace(Cmb_AmrNo.Text) || Cmb_AmrNo.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_AmrNo, "تاكد من اختيار رقم أمر الشراء"));
                }
                #endregion
            }

            return errorsList;
        }

        private List<(ErrorProvider, Control, string)> ValidateSave()
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

            #region Cmb_FY
            if (string.IsNullOrWhiteSpace(Cmb_FY.Text) || Cmb_FY.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_FY, "تاكد من  اختيار السنة المالية"));
            }
            #endregion

            #region Cmb_AmrNo
            if (string.IsNullOrWhiteSpace(Cmb_AmrNo.Text) || Cmb_AmrNo.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_AmrNo, "تاكد من اختيار رقم أمر الشراء"));
            }
            #endregion

            #region dataGridView1
            if (dataGridView1.Rows.Count <= 0)
            {
                //errorsList.Add((errorProvider, dataGridView1, "لايمكن ان يتكون طلب توريد بدون بنود"));
                MessageBox.Show("لايمكن ان يتكون طلب الاستلام بدون بنود");
            }
            else if (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow == true)
            {
                //errorsList.Add((errorProvider, dataGridView1, "لايمكن ان يتكون طلب توريد بدون بنود"));
                MessageBox.Show("لايمكن ان يتكون طلب الاستلام بدون بنود");
            }
            #endregion

            //if (((PictureBox)this.signatureTable.Controls["Pic_Sign" + currentSignNumber]).Image == null)
            //{
            //    errorsList.Add((errorProvider, ((PictureBox)this.signatureTable.Controls["Pic_Sign" + currentSignNumber]), "تاكد من التوقيع"));
            //}

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

        private void Estlam_Load(object sender, EventArgs e)
        {
            alertProvider.Icon = SystemIcons.Warning;
            HelperClass.comboBoxFiller(Cmb_FY, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
            HelperClass.comboBoxFiller(Cmb_FYear2, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);

            if (Constants.Estlam_F == false)
            {
                panel10.Visible = true;
                panel2.Visible = false;
            }
            else if (Constants.Estlam_F == true)
            {
                panel2.Visible = true;
                panel10.Visible = false;
            }


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
            string cmdstring = "select Amrshraa_No from   T_Awamershraa where  AmrSheraa_sanamalia='" + Cmb_FY + "'";
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

            con.Close();
            reset();
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد طلب استلام جديد؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                reset();
                PrepareAddState();

                AddEditFlag = 2;
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

            if ((MessageBox.Show("هل تريد تعديل طلب استلام ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(Cmb_AmrNo.Text) || string.IsNullOrEmpty(Cmb_FY.Text))
                {
                    MessageBox.Show("يجب اختيار طلب الاستلام المراد تعديله");
                    return;
                }


                AddEditFlag = 1;
                TNO = Cmb_AmrNo.SelectedValue.ToString();
                FY = Cmb_FY.Text;
                Dateold = Convert.ToDateTime(TXT_Date.Value.ToShortDateString());

                PrepareEditState();
            }
        }

        private void EditBtn2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد تعديل طلب استلام ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(Cmb_AmrNo.Text) || string.IsNullOrEmpty(Cmb_FY.Text))
                {
                    MessageBox.Show("يجب اختيار طلب الاستلام المراد تعديله");
                    return;
                }

                PrepareConfirmState();
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
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();

            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring = @"select T_Awamershraa.Amrshraa_No from  T_Awamershraa left join T_Estlam on T_Awamershraa.Amrshraa_No = T_Estlam.Amrshraa_No
                                where (T_Awamershraa.Sign14 is not null) and T_Awamershraa.AmrSheraa_sanamalia=@FY and (T_Estlam.Amrshraa_No is null) 
                                group by T_Awamershraa.Amrshraa_No  order by  T_Awamershraa.Amrshraa_No";

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


        private void Cmb_FYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();

            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring = "select  (Amrshraa_No),date,AmrShraa_No +' ==> '+  Convert(nvarchar(50),Date ) as x from T_Estlam where AmrSheraa_sanamalia=@FY and (Sign3 is null)  group by date,Amrshraa_No,AmrSheraa_sanamalia   order by Amrshraa_No ";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
            cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            ///   cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);

            DataTable dts = new DataTable();

            dts.Load(cmd.ExecuteReader());
            Cmb_AmrNo2.DataSource = dts;
            Cmb_AmrNo2.ValueMember = "Amrshraa_No";
            Cmb_AmrNo2.DisplayMember = "x";
            Cmb_AmrNo2.SelectedIndex = -1;
            Constants.closecon();
            
        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {

            if (!IsValidCase(VALIDATION_TYPES.SAVE))
            {
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


            if (AddEditFlag == 2)
            {
                if (FlagSign1 != 1)
                {
                    MessageBox.Show("من فضلك تاكد من توقيع الاستلام");
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

        private void BTN_Save2_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.SAVE))
            {
                return;
            }

            EditLogic();

            reset();

            Cmb_AmrNo2.SelectedIndex = -1;
            Cmb_FYear2.SelectedIndex = -1;

            Cmb_AmrNo.Enabled = false;
            Cmb_FY.Enabled = false;
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


        private void TXT_QuanTard_KeyPress(object sender, KeyPressEventArgs e)
        {
            Constants.validatenumberkeypress(sender, e);
        }

        private void Cmb_AmrNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (AddEditFlag == 2 && Cmb_AmrNo.SelectedIndex != -1)
            {
                GetEstlamBnod(Cmb_AmrNo.SelectedValue.ToString(), Cmb_FY.Text,false);
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

        private void BTN_Search_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.SEARCH))
            {
                return;
            }

            string amr_no = Cmb_AmrNo.Text;
            string fyear = Cmb_FY.Text;

            reset();

            if (SearchEstlam(amr_no, fyear, false))
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

            string amr_no = Cmb_AmrNo2.SelectedValue.ToString();
            string fyear = Cmb_FYear2.Text;


            string x = Cmb_AmrNo2.GetItemText(Cmb_AmrNo2.SelectedItem);
            string xx = x.Substring(x.Length - 10, 10);

            reset();

            TXT_Date.Text = xx;

            if (SearchEstlam(amr_no, fyear, true))
            {
                EditBtn2.Enabled = true;
                BTN_Print2.Enabled = true;
            }

            Cmb_AmrNo.Enabled = false;
            Cmb_FY.Enabled = false;
        }

        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            AddEditFlag = 0;
            reset();
        }


        private void browseBTN_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.ATTACH_FILE))
            {
                return;
            }

            //openFileDialog1.Filter = "PDF(*.pdf)|*.pdf";
            //DialogResult dialogRes = openFileDialog1.ShowDialog();
            //string ConstantPath = @"\\172.18.8.83\MaterialAPP\PDF\";//////////////////change it to server path

            //foreach (String file in openFileDialog1.FileNames)
            //{
            //    if (dialogRes == DialogResult.OK)
            //    {
            //        string VariablePath = string.Concat(Constants.CodeEdara, @"\");
            //        string path = ConstantPath + VariablePath;

            //        if (!Directory.Exists(path))
            //        {
            //            MessageBox.Show("عفوا لايمكنك ارفاق مرفقات برجاء الرجوع إلي إدارة نظم المعلومات");
            //            return;
            //        }

            //        path += Cmb_FY.Text + @"\";

            //        if (!Directory.Exists(path))
            //        {
            //            Directory.CreateDirectory(path);
            //        }

            //        path += "ESTLAM" + @"\";

            //        if (!Directory.Exists(path))
            //        {
            //            Directory.CreateDirectory(path);
            //        }

            //        path += Cmb_AmrNo.Text + @"\";

            //        if (!Directory.Exists(path))
            //        {
            //            Directory.CreateDirectory(path);
            //        }

            //        string filename = Path.GetFileName(file);
            //        path += filename;

            //        if (!File.Exists(path))
            //        {
            //            File.Copy(file, path);
            //        }
            //    }
            //}

            //if (dialogRes == DialogResult.OK)
            //{
            //    MessageBox.Show("تم إرفاق المرفقات");
            //}
            //else
            //{
            //    MessageBox.Show("لم يتم إرفاق المرفقات");
            //}
        }

        private void BTN_PDF_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.ATTACH_FILE))
            {
                return;
            }

            //PDF_PopUp popup = new PDF_PopUp();

            //popup.WholePath = @"\\172.18.8.83\MaterialAPP\PDF\" + Constants.CodeEdara + @"\" + Cmb_FY.Text + @"\ESTLAM\" + Cmb_AmrNo.Text + @"\";
            //try
            //{
            //    popup.ShowDialog(this);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            //popup.Dispose();
        }


        private void BTN_Print_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Cmb_AmrNo.Text) || string.IsNullOrEmpty(Cmb_FY.Text))
            {
                MessageBox.Show("يجب اختيار طلب الاستلام المراد طباعتها اولا");
                return;
            }
            else
            {
                Constants.Date_E = TXT_Date.Text;
                Constants.AmrNo = Cmb_AmrNo.SelectedValue.ToString();
                Constants.AmrSanaMalya = Cmb_FY.Text;
                Constants.MwardName = TXT_NameMward.Text;

                Constants.No_Tard = TXT_QuanTard.Text;
                Constants.No_Bnod = TXT_QuanBnod.Text;
                Constants.Sanf = TXT_Sanf.Text;
                Constants.Date_Amr = TXT_DateEstlam.Text;
                Constants.Sign1 = FlagEmpn1.ToString();
                Constants.Sign2 = FlagEmpn2.ToString();

                Constants.Sign3 = FlagEmpn3.ToString();
                // Constants.Sign4 = FlagEmpn4.ToString();


                Constants.FormNo = 2;
                FReports F = new FReports();
                F.Show();
            }
        }

        private void BTN_Print2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Cmb_AmrNo2.Text) || string.IsNullOrEmpty(Cmb_FYear2.Text))
            {
                MessageBox.Show("يجب اختيار طلب الاستلام المراد طباعتها اولا");
                return;
            }
            else
            {
                Constants.Date_E = TXT_Date.Text;
                Constants.AmrNo = Cmb_AmrNo2.SelectedValue.ToString();
                Constants.AmrSanaMalya = Cmb_FYear2.Text;
                Constants.MwardName = TXT_NameMward.Text;

                Constants.No_Tard = TXT_QuanTard.Text;
                Constants.No_Bnod = TXT_QuanBnod.Text;
                Constants.Sanf = TXT_Sanf.Text;
                Constants.Date_Amr = TXT_DateEstlam.Text;
                Constants.Sign1 = FlagEmpn1.ToString();
                Constants.Sign2 = FlagEmpn2.ToString();

                Constants.Sign3 = FlagEmpn3.ToString();
                // Constants.Sign4 = FlagEmpn4.ToString();


                Constants.FormNo = 2;
                FReports F = new FReports();
                F.Show();
            }
        }

        //------------------------------------------ Signature Handler ---------------------------------
        #region Signature Handler
        private void BTN_Sigm1_Click(object sender, EventArgs e)
        {
            Empn1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع مخزن الاستلام", "");

            Sign1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مخزن الاستلام", "");

            if (Sign1 != "" && Empn1 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("1", "4", Sign1, Empn1);
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
            Empn2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع مدير مخزن الاستلام", "");

            Sign2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مدير مخزن الاستلام", "");

            if (Sign2 != "" && Empn2 != "")
            {
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
            }

        }

        private void BTN_Sign3_Click(object sender, EventArgs e)
        {
            Empn3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "مدير عام مساعد مخازن", "");

            Sign3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "مدير عام مساعد مخازن", "");

            if (Sign3 != "" && Empn3 != "")
            {
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

            }
        }


        #endregion


    }
}
