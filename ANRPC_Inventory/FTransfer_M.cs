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
    public partial class FTransfer_M : Form
    {
        //------------------------------------------ Define Variables ---------------------------------
        #region Def Variables
        public SqlConnection con;//sql conn for anrpc_sms db
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
        public DataTable DT = new DataTable();
        private BindingSource bindingsource1 = new BindingSource();
        private string TableQuery;
        private int AddEditFlag;
        public Boolean executemsg;
        public double totalprice;
        public double oldvalue;
        //  private string TableQuery;
        public string stockallold;
        public DataTable table = new DataTable();
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
        public string Sign111;
        public string Empn1;
        public string Empn2;
        public string Empn3;
        public string Empn4;
        public string Empn5;
        public string Empn6;
        public string Empn7;
        public string Empn111;
        public string FlagEmpn1;
        public string FlagEmpn2;
        public string FlagEmpn3;
        public string FlagEmpn4;
        public string FlagEmpn5;
        public string FlagEmpn6;
        public string FlagEmpn7;
        public string FlagEmpn111;


        public int FlagSign1;
        public int FlagSign2;
        public int FlagSign3;
        public int FlagSign4;
        public int FlagSign5;
        public int FlagSign6;
        public int FlagSign7;
        public int FlagSign111;



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
        public string wazifa111;
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
        public string Ename111;

        public string pp;
        public string TNO;
        public string FY;
        public int r;
        public int rowflag = 0;
        public int MaxFlag;
        //  public string TableQuery;

        AutoCompleteStringCollection TasnifColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TasnifNameColl = new AutoCompleteStringCollection(); //empn

        AutoCompleteStringCollection UnitColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection EznColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection PartColl = new AutoCompleteStringCollection(); //empn
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
        private void cleargridview()
        {
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

        }

        public void SP_UpdateSignatures(int x, DateTime D1, DateTime? D2 = null)
        {
            string cmdstring = "Exec  SP_UpdateSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_TRansferNo.Text));
            cmd.Parameters.AddWithValue("@TNO2", Convert.ToInt32(TXT_TRNO.Text));

            cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
            cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
            cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);

            cmd.Parameters.AddWithValue("@FN", 2);

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

        public void InsertTransSarf()
        {
            Constants.opencon();
            string cmdstring = "Exec SP_deleteTR2 @TNO,@FY,@TR";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", (TXT_TRansferNo.Text));
            cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            cmd.Parameters.AddWithValue("@TR", TXT_TRNO.Text.ToString());

            cmd.ExecuteNonQuery();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (!row.IsNewRow)
                {

                    cmdstring = "exec SP_InsertTR2 @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29";
                    cmd = new SqlCommand(cmdstring, Constants.con);

                    cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_TRansferNo.Text));
                    cmd.Parameters.AddWithValue("@p2", Cmb_FYear.Text.ToString());
                    cmd.Parameters.AddWithValue("@p3", row.Cells[3].Value);
                    cmd.Parameters.AddWithValue("@p4", row.Cells[6].Value);
                    cmd.Parameters.AddWithValue("@p5", TXT_Date.Text.ToString());
                    cmd.Parameters.AddWithValue("@p6", TXT_TRNO.Text.ToString());
                    cmd.Parameters.AddWithValue("@p7", TXT_AccNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@p8", TXT_PaccNo.Text.ToString());
                    string st = row.Cells[6].Value.ToString();
                    cmd.Parameters.AddWithValue("@p9", (st).Substring(0, 2));
                    cmd.Parameters.AddWithValue("@p10", (st).Substring(2, 2));

                    cmd.Parameters.AddWithValue("@p11", (st).Substring(4, 2));
                    cmd.Parameters.AddWithValue("@p12", (st).Substring(6, 2));
                    cmd.Parameters.AddWithValue("@p13", row.Cells[4].Value);
                    cmd.Parameters.AddWithValue("@p14", row.Cells[9].Value);
                    cmd.Parameters.AddWithValue("@p15", row.Cells[7].Value);
                    cmd.Parameters.AddWithValue("@p16", Constants.CodeEdara);
                    cmd.Parameters.AddWithValue("@p17", Constants.NameEdara);
                    cmd.Parameters.AddWithValue("@p18", TXT_Date.Value.Day.ToString());
                    cmd.Parameters.AddWithValue("@p19", TXT_Date.Value.Month.ToString());
                    cmd.Parameters.AddWithValue("@p20", TXT_Date.Value.Year.ToString());

                    cmd.Parameters.AddWithValue("@p21", (row.Cells[5].Value));
                    cmd.Parameters.AddWithValue("@p22", row.Cells[8].Value);
                    cmd.Parameters.AddWithValue("@p23", TXT_MTaklif.Text.ToString());
                    cmd.Parameters.AddWithValue("@p24", TXT_MResp.Text.ToString());
                    cmd.Parameters.AddWithValue("@p25", TXT_Masrof.Text.ToString());
                    cmd.Parameters.AddWithValue("@p26", TXT_Enfak.Text.ToString());
                    cmd.Parameters.AddWithValue("@p27", TXT_Enfak.Text.ToString());
                    cmd.Parameters.AddWithValue("@p28", TXT_Morakba.Text.ToString());
                    cmd.Parameters.AddWithValue("@p29", row.Cells[10].Value);
                    // cmd.Parameters.AddWithValue("@p30", Cmb_FYear.Text.ToString());
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("تم ادخال الحركة بنجاح");
        }

        public void InsertTransEdafa()
        {
            Constants.opencon();
            string cmdstring = "Exec SP_deleteTR1 @TNO,@FY,@TR";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", (TXT_TRansferNo.Text));
            cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            cmd.Parameters.AddWithValue("@TR", TXT_TRNO.Text.ToString());

            cmd.ExecuteNonQuery();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (!row.IsNewRow)
                {

                    cmdstring = "exec SP_InsertTR1 @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29";
                    cmd = new SqlCommand(cmdstring, Constants.con);

                    cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_TRansferNo.Text));
                    cmd.Parameters.AddWithValue("@p2", Cmb_FYear.Text.ToString());
                    cmd.Parameters.AddWithValue("@p3", row.Cells[3].Value);
                    cmd.Parameters.AddWithValue("@p4", row.Cells[13].Value);
                    cmd.Parameters.AddWithValue("@p5", TXT_Date.Text.ToString());
                    cmd.Parameters.AddWithValue("@p6", TXT_TRNO.Text.ToString());
                    cmd.Parameters.AddWithValue("@p7", TXT_AccNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@p8", TXT_PaccNo.Text.ToString());
                    string st = row.Cells[13].Value.ToString();
                    cmd.Parameters.AddWithValue("@p9", (st).Substring(0, 2));
                    cmd.Parameters.AddWithValue("@p10", (st).Substring(2, 2));

                    cmd.Parameters.AddWithValue("@p11", (st).Substring(4, 2));
                    cmd.Parameters.AddWithValue("@p12", (st).Substring(6, 2));
                    cmd.Parameters.AddWithValue("@p13", row.Cells[4].Value);
                    cmd.Parameters.AddWithValue("@p14", row.Cells[16].Value);
                    cmd.Parameters.AddWithValue("@p15", row.Cells[14].Value);
                    cmd.Parameters.AddWithValue("@p16", Constants.CodeEdara);
                    cmd.Parameters.AddWithValue("@p17", Constants.NameEdara);
                    cmd.Parameters.AddWithValue("@p18", TXT_Date.Value.Day.ToString());
                    cmd.Parameters.AddWithValue("@p19", TXT_Date.Value.Month.ToString());
                    cmd.Parameters.AddWithValue("@p20", TXT_Date.Value.Year.ToString());

                    cmd.Parameters.AddWithValue("@p21", (row.Cells[16].Value));///////???plz check
                    cmd.Parameters.AddWithValue("@p22", row.Cells[16].Value);///////////////????plz check
                    cmd.Parameters.AddWithValue("@p23", row.Cells[12].Value);
                    cmd.Parameters.AddWithValue("@p24", row.Cells[15].Value);
                    cmd.Parameters.AddWithValue("@p25", TXT_Masrof.Text.ToString());//??????????????????plz chec
                    cmd.Parameters.AddWithValue("@p26", TXT_MTaklif.Text.ToString());///??????????????????plz chec
                    cmd.Parameters.AddWithValue("@p27", TXT_Enfak.Text.ToString());
                    cmd.Parameters.AddWithValue("@p28", TXT_Morakba.Text.ToString());
                    cmd.Parameters.AddWithValue("@p29", TXT_Enfak.Text.ToString());
                    // cmd.Parameters.AddWithValue("@p30", Cmb_FYear.Text.ToString());
                    cmd.ExecuteNonQuery();
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
                    string cmdstring = "Exec SP_UpdateQuanTsnif @Quan,@ST,@F,@EZ,@FY,@B,@TRNO";

                    SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                    cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[4].Value));
                    //will send rased badl else monsrf
                    // cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[10].Value));
                    cmd.Parameters.AddWithValue("@ST", (row.Cells[13].Value));
                    cmd.Parameters.AddWithValue("@F", 3);
                    cmd.Parameters.AddWithValue("@EZ", TXT_TRansferNo.Text);
                    cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text);

                    cmd.Parameters.AddWithValue("@B", row.Cells[3].Value);

                    cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text);
                    cmd.ExecuteNonQuery();
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
                    string cmdstring = "Exec SP_UpdateQuanTsnif @Quan,@ST,@F,@EZ,@FY,@B,@TRNO";

                    SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                    cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[4].Value));
                    //will send rased badl else monsrf
                    // cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[10].Value));
                    cmd.Parameters.AddWithValue("@ST", (row.Cells[6].Value));
                    cmd.Parameters.AddWithValue("@F", 4);
                    cmd.Parameters.AddWithValue("@EZ", TXT_TRansferNo.Text);
                    cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text);

                    cmd.Parameters.AddWithValue("@B", row.Cells[3].Value);
                    cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertEznTahweelBnood()
        {
            SqlCommand cmd;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string q = "exec SP_InsertEzonTahwel_Benod @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19 ";
                    cmd = new SqlCommand(q, Constants.con);
                    cmd.Parameters.AddWithValue("@p1", row.Cells[0].Value);
                    cmd.Parameters.AddWithValue("@p2", row.Cells[1].Value);
                    cmd.Parameters.AddWithValue("@p3", row.Cells[2].Value);
                    cmd.Parameters.AddWithValue("@p4", row.Cells[3].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p5", row.Cells[4].Value);
                    cmd.Parameters.AddWithValue("@p6", row.Cells[5].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p7", row.Cells[6].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p8", row.Cells[7].Value);
                    cmd.Parameters.AddWithValue("@p9", row.Cells[8].Value);
                    cmd.Parameters.AddWithValue("@p10", row.Cells[9].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p11", row.Cells[10].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p12", row.Cells[11].Value);
                    cmd.Parameters.AddWithValue("@p13", row.Cells[12].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p14", row.Cells[13].Value);
                    cmd.Parameters.AddWithValue("@p15", row.Cells[14].Value);
                    cmd.Parameters.AddWithValue("@p16", row.Cells[15].Value);
                    cmd.Parameters.AddWithValue("@p17", row.Cells[16].Value);

                    cmd.Parameters.AddWithValue("@p18", Constants.User_Name.ToString());
                    cmd.Parameters.AddWithValue("@p19", Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                    cmd.ExecuteNonQuery();
                }
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string q = "exec SP_UpdateVirtualQuan @p1,@p2,@p3";
                    cmd = new SqlCommand(q, Constants.con);

                    if (row.Cells[4].Value == DBNull.Value || row.Cells[4].Value.ToString() == "")
                    {
                        cmd.Parameters.AddWithValue("@p1", row.Cells[3].Value);
                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@p1", row.Cells[4].Value);
                    }
                    cmd.Parameters.AddWithValue("@p2", row.Cells[9].Value);
                    cmd.Parameters.AddWithValue("@p3", 1);
                    cmd.ExecuteNonQuery();
                }
            }


        }

        private void AddNewTasnifInDataGridView()
        {
            #region Add row to dataGridView
            r = dataGridView1.Rows.Count - 1;

            rowflag = 1;
            DataRow newRow = table.NewRow();
            table.Rows.InsertAt(newRow, r);
            dataGridView1.DataSource = table;

            dataGridView1.Rows[r].Cells[4].Value = Txt_ReqQuan.Text.ToString();
            dataGridView1.Rows[r].Cells[5].Value = Cmb_From.Text;
            dataGridView1.Rows[r].Cells[6].Value = TXT_StockNoAll.Text;
            dataGridView1.Rows[r].Cells[7].Value = TXT_Unit.Text;
            dataGridView1.Rows[r].Cells[8].Value = TXT_StockBian.Text;

            if (string.IsNullOrEmpty(Txt_Quan.Text))
            {
                dataGridView1.Rows[r].Cells[9].Value = DBNull.Value;
            }
            else
            {
                dataGridView1.Rows[r].Cells[9].Value = Txt_Quan.Text;
            }
            dataGridView1.Rows[r].Cells[12].Value = Cmb_To.Text;

            dataGridView1.Rows[r].Cells[0].Value = TXT_TRansferNo.Text;
            dataGridView1.Rows[r].Cells[1].Value = TXT_TRNO.Text;
            dataGridView1.Rows[r].Cells[2].Value = Cmb_FYear.Text;
            dataGridView1.Rows[r].Cells[3].Value = r + 1;
            dataGridView1.DataSource = table;
            #endregion

            //dataGridView1.Rows[r + 1].Cells[4].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[5].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[6].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[7].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[8].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[9].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[10].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[11].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[12].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[13].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[14].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[15].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[16].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[0].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[1].Value = DBNull.Value;

            //dataGridView1.Rows[r + 1].Cells[2].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[3].Value = DBNull.Value;
        }

        private void GetEznTahweelBnod(string transNo, string fyear, string momayz)
        {
            table.Clear();
            string TableQuery = @"SELECT [TransNo] ,[TR_NO],[FYear],[BndNo],[Quan],[FStockName],
                                [FStockNoALL],[FUnit],[FBayan],[FRased],[Fprice],[FNsbetSalhia] ,
                                [TStockName],[TStockNoALL],[TUnit] ,[TBayan],[TRased] FROM [T_EzonTahwel_Benod] 
                                Where TransNo = " + transNo + " and Fyear='" + fyear + "' and TR_NO='" + momayz + "'";

            dataadapter = new SqlDataAdapter(TableQuery, Constants.con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataadapter.Fill(table);
            dataGridView1.DataSource = table;


            dataGridView1.Columns["BndNo"].HeaderText = "19/18";//col3
            dataGridView1.Columns["Quan"].HeaderText = "27/20";//col4
            dataGridView1.Columns["FStockName"].HeaderText = "21/20";//col5
            dataGridView1.Columns["FStockNoALL"].HeaderText = "41/33";//col6
            dataGridView1.Columns["FUnit"].HeaderText = "29/28";//col7
            dataGridView1.Columns["FBayan"].HeaderText = "البيان";//col8
            dataGridView1.Columns["FRased"].HeaderText = "49/3";//col9
            dataGridView1.Columns["Fprice"].HeaderText = "59/50";//col10
            dataGridView1.Columns["FNsbetSalhia"].HeaderText = "29/28";//col11
            dataGridView1.Columns["TStockName"].HeaderText = "61/60";//col12


            dataGridView1.Columns["TStockNoALL"].HeaderText = "71/62";//col13
            dataGridView1.Columns["TStockNoALL"].Width = 83;
            dataGridView1.Columns["TStockNoALL"].ContextMenuStrip = contextMenuStrip1;

            dataGridView1.Columns["TUnit"].HeaderText = "الوحدة";//col14
            dataGridView1.Columns["TUnit"].Width = 59;
            dataGridView1.Columns["TUnit"].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns["TUnit"].ReadOnly = true;

            dataGridView1.Columns["TBayan"].HeaderText = "البيان";//col15
            dataGridView1.Columns["TBayan"].Width = 120;
            dataGridView1.Columns["TBayan"].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns["TBayan"].ReadOnly = true;

            dataGridView1.Columns["TRased"].HeaderText = "79/72";//col16
            dataGridView1.Columns["TRased"].Width = 58;
            dataGridView1.Columns["TRased"].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns["TRased"].ReadOnly = true;


            dataGridView1.Columns["TransNo"].HeaderText = "رقم اذن التحويل";//col0
            dataGridView1.Columns["TransNo"].Visible = false;

            dataGridView1.Columns["TR_NO"].HeaderText = "مميز مستند";//col1
            dataGridView1.Columns["TR_NO"].Visible = false;

            dataGridView1.Columns["FYear"].HeaderText = "السنة المالية";//col2
            dataGridView1.Columns["FYear"].Visible = false;



            if (Constants.User_Type == "B" && Constants.UserTypeB == "NewTasnif")
            {
                dataGridView1.Columns["TStockNoALL"].DefaultCellStyle.BackColor = Color.Salmon;
                dataGridView1.Columns["TStockNoALL"].ReadOnly = false;
            }

            if (Constants.User_Type == "B")
            {
                //dataGridView1.Columns["TStockNoALL"].DefaultCellStyle.BackColor = Color.Salmon;
                dataGridView1.Columns["FNsbetSalhia"].ReadOnly = false;
            }
            if (Constants.User_Type == "A")
            {
                //dataGridView1.Columns["TStockNoALL"].DefaultCellStyle.BackColor = Color.Salmon;
                dataGridView1.Columns["TStockName"].ReadOnly = true;
                dataGridView1.Columns["TStockNoALL"].ReadOnly = true;
                dataGridView1.Columns["TBayan"].ReadOnly = true;
                dataGridView1.Columns["TRased"].ReadOnly = true;

            }
            dataGridView1.AllowUserToAddRows = true;




        }

        public bool SearchEznTahweel(string transNo, string fyear, string momayz)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            string cmdstring;
            SqlCommand cmd;

            cmdstring = "select * from T_EzonTahwel where TransNo=@TN and FYear=@FY and TR_NO=@TRNO";

            cmd = new SqlCommand(cmdstring, Constants.con);
            cmd.Parameters.AddWithValue("@TN", transNo);
            cmd.Parameters.AddWithValue("@FY", fyear);
            cmd.Parameters.AddWithValue("@TRNO", momayz);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                try
                {
                    while (dr.Read())
                    {
                        TXT_TRansferNo.Text = dr["TransNo"].ToString();
                        TXT_MomayzHarka.Text = dr["TypeTransName"].ToString();
                        TXT_CodeEdara.Text = dr["FromEdaraName"].ToString();
                        TXT_Date.Text = dr["TransDate"].ToString();
                        TXT_TRNO.Text = dr["TR_NO"].ToString();
                        Cmb_From.Text = dr["FromEdaraName"].ToString();
                        Cmb_To.Text = dr["TToStock"].ToString();

                        if (TXT_TRNO.Text.ToString() != "")
                        {
                            Cmb_CType.SelectedValue = TXT_TRNO.Text.ToString();
                        }

                        TXT_AccNo.Text = dr["Acc_No"].ToString();
                        TXT_PaccNo.Text = dr["Pacc_No"].ToString();
                        TXT_MTaklif.Text = dr["MTakalif"].ToString();
                        TXT_MResp.Text = dr["MResponsible"].ToString();
                        TXT_Masrof.Text = dr["Masrof"].ToString();
                        TXT_Enfak.Text = dr["Enfak"].ToString();
                        TXT_Morakba.Text = dr["Morakba"].ToString();

                        string s111 = dr["Sign11"].ToString();
                        string s1 = dr["Sign1"].ToString();
                        string s2 = dr["Sign2"].ToString();
                        string s3 = dr["Sign3"].ToString();
                        string s4 = dr["Sign4"].ToString();
                        string s5 = dr["Sign5"].ToString();
                        string s6 = dr["Sign6"].ToString();

                        Cmb_FYear.Text = dr["FYear"].ToString();

                        if (s111 != "")
                        {
                            string p = Constants.RetrieveSignature("111", "7", s1);
                            if (p != "")
                            {
                                Ename111 = p.Split(':')[1];
                                wazifa111 = p.Split(':')[2];
                                pp = p.Split(':')[0];

                                FlagSign111 = 1;
                                FlagEmpn111 = s111;
                            }

                        }

                        if (s1 != "")
                        {
                            string p = Constants.RetrieveSignature("1", "7", s1);
                            if (p != "")
                            {
                                Ename1 = p.Split(':')[1];
                                wazifa1 = p.Split(':')[2];
                                pp = p.Split(':')[0];

                                ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel3"].Controls["Pic_Sign" + "1"]).Image = Image.FromFile(@pp);

                                FlagSign1 = 1;
                                FlagEmpn1 = s1;
                                ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel3"].Controls["Pic_Sign" + "1"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign1, Ename1 + Environment.NewLine + wazifa1);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel3"].Controls["Pic_Sign" + "1"]).BackColor = Color.Red;
                        }
                        if (s2 != "")
                        {
                            string p = Constants.RetrieveSignature("2", "7", s2);
                            if (p != "")
                            {
                                Ename2 = p.Split(':')[1];
                                wazifa2 = p.Split(':')[2];
                                pp = p.Split(':')[0];

                                ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel14"].Controls["Pic_Sign" + "2"]).Image = Image.FromFile(@pp);
                                FlagSign2 = 1;
                                FlagEmpn2 = s2;
                                ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel14"].Controls["Pic_Sign" + "2"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign2, Ename2 + Environment.NewLine + wazifa2);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel14"].Controls["Pic_Sign" + "2"]).BackColor = Color.Red;
                        }
                        if (s3 != "")
                        {
                            string p = Constants.RetrieveSignature("3", "7", s3);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename3 = p.Split(':')[1];
                                wazifa3 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel17"].Controls["Pic_Sign" + "3"]).Image = Image.FromFile(@pp);
                                FlagSign3 = 1;
                                FlagEmpn3 = s3;
                                ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel17"].Controls["Pic_Sign" + "3"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign3, Ename3 + Environment.NewLine + wazifa3);


                            }

                        }
                        else
                        {
                            ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel17"].Controls["Pic_Sign" + "3"]).BackColor = Color.Red;
                        }
                        if (s4 != "")
                        {
                            string p = Constants.RetrieveSignature("4", "7", s4);
                            if (p != "")
                            {
                                Ename3 = p.Split(':')[1];
                                wazifa3 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel16"].Controls["Pic_Sign" + "4"]).Image = Image.FromFile(@pp);
                                FlagSign4 = 1;
                                FlagEmpn4 = s4;
                                ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel16"].Controls["Pic_Sign" + "4"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign4, Ename4 + Environment.NewLine + wazifa4);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel16"].Controls["Pic_Sign" + "4"]).BackColor = Color.Red;
                        }
                        if (s5 != "")
                        {
                            string p = Constants.RetrieveSignature("5", "7", s5);
                            if (p != "")
                            {
                                Ename5 = p.Split(':')[1];
                                wazifa5 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel15"].Controls["Pic_Sign" + "5"]).Image = Image.FromFile(@pp);
                                FlagSign5 = 1;
                                FlagEmpn5 = s5;
                                ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel15"].Controls["Pic_Sign" + "5"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign5, Ename5 + Environment.NewLine + wazifa5);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel15"].Controls["Pic_Sign" + "5"]).BackColor = Color.Red;
                        }
                        if (s6 != "")
                        {
                            string p = Constants.RetrieveSignature("6", "7", s5);
                            if (p != "")
                            {
                                Ename6 = p.Split(':')[1];
                                wazifa6 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel13"].Controls["Pic_Sign" + "6"]).Image = Image.FromFile(@pp);
                                FlagSign6 = 1;
                                FlagEmpn6 = s6;
                                ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel13"].Controls["Pic_Sign" + "6"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign5, Ename5 + Environment.NewLine + wazifa5);

                            }

                        }
                        else
                        {
                            ((PictureBox)this.panel1.Controls["signatureTable"].Controls["panel13"].Controls["Pic_Sign" + "6"]).BackColor = Color.Red;
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
                MessageBox.Show("من فضلك تاكد من رقم اذن التحويل");
                reset();
                return false;
            }

            dr.Close();

            GetData((TXT_TRansferNo.Text), Cmb_FYear.Text, TXT_TRNO.Text);

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
            //Search sec
            changePanelState(panel4, true);

            //dataViewre sec
            changePanelState(panel5, false);
            Txt_ReqQuan.Enabled = true;

            //fyear sec
            changePanelState(panel6, false);
            Cmb_FYear.Enabled = true;
            Cmb_CType.Enabled = true;

            //tahweel sec
            changePanelState(panel8, true);
            TXT_MomayzHarka.Enabled = false;


            //btn Section
            //generalBtn
            SaveBtn.Enabled = true;
            BTN_Cancel.Enabled = true;
            Addbtn2.Enabled = true;
            browseBTN.Enabled = true;
            BTN_PDF.Enabled = true;

            Addbtn.Enabled = false;
            Editbtn2.Enabled = false;
            BTN_Search.Enabled = false;
            BTN_Print.Enabled = false;

            //signature btn
            changePanelState(signatureTable, false);
            BTN_Sign1.Enabled = true;

            //takalid types
            DisableTakalef();

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
            changePanelState(panel8, false);
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
            //DisableControls();
            //BTN_Save2.Enabled = true;

            //if (Constants.User_Type == "A")
            //{
            //    if (FlagSign2 != 1 && FlagSign1 == 1)
            //    {
            //        BTN_Sign2.Enabled = true;
            //        DeleteBtn.Enabled = true;
            //        currentSignNumber = 2;
            //    }
            //    else if (FlagSign4 != 1 && FlagSign3 == 1)
            //    {
            //        BTN_Sign4.Enabled = true;
            //        currentSignNumber = 4;
            //    }
            //}
            //else if (Constants.User_Type == "B")
            //{
            //    if (Constants.UserTypeB == "Sarf")
            //    {
            //        BTN_Sign3.Enabled = true;
            //        //dataGridView1.ReadOnly = false;
            //        dataGridView1.Columns["Quan2"].ReadOnly = false;
            //        currentSignNumber = 3;
            //    }
            //    else if (Constants.UserTypeB == "Tkalif" || Constants.UserTypeB == "Finance")
            //    {
            //        EnableTakalef();
            //    }
            //}

            //AddEditFlag = 1;
            //TNO = TXT_EznNo.Text;
            //FY = Cmb_FYear.Text;
        }

        public void prepareSearchState()
        {
            DisableControls();
            Input_Reset();

            if (Constants.EzonTahwel_FF)
            {
                Cmb_FYear.Enabled = true;
                Cmb_CType.Enabled = true;
                TXT_TRansferNo.Enabled = true;
                BTN_Print.Enabled = true;
            }
        }

        public void EnableTakalef()
        {
            changePanelState(takalefTable, true);
        }

        public void DisableTakalef()
        {
            changePanelState(takalefTable, false);
        }

        public void reset()
        {
            prepareSearchState();
        }

        public void DisableControls()
        {
            //Search sec
            changePanelState(panel4, false);

            //dataViewre sec
            changePanelState(panel5, false);

            //fyear sec
            changePanelState(panel6, false);

            //tahweel sec
            changePanelState(panel8, false);

            //btn Section
            //generalBtn
            Addbtn.Enabled = true;
            BTN_Search.Enabled = true;

            SaveBtn.Enabled = false;
            BTN_Save2.Enabled = false;
            Editbtn.Enabled = false;
            BTN_Cancel.Enabled = false;
            Addbtn2.Enabled = false;
            Editbtn2.Enabled = false;
            BTN_Print.Enabled = false;
            browseBTN.Enabled = false;
            BTN_PDF.Enabled = false;

            //signature btn
            changePanelState(signatureTable, false);

            changeDataGridViewColumnState(dataGridView1, true);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            //takalif
            DisableTakalef();
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

            TXT_TRansferNo.Text = "";
            TXT_TRNO.Text = "";


            //tahweel sec
            TXT_Date.Value = DateTime.Today;
            TXT_MomayzHarka.Text = "";

            Cmb_From.Text = "";
            Cmb_From.SelectedIndex = -1;

            Cmb_To.Text = "";
            Cmb_To.SelectedIndex = -1;



            //search sec
            Cmb_CType2.Text = "";
            Cmb_CType2.SelectedIndex = -1;

            Cmb_FYear2.Text = "";
            Cmb_FYear2.SelectedIndex = -1;

            Cmb_TRansferNo2.Text = "";
            Cmb_TRansferNo2.SelectedIndex = -1;

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

        //------------------------------------------ Logic Handler ---------------------------------
        #region Logic Handler
        private void AddLogic()
        {
            Constants.opencon();

            string cmdstring = "Exec SP_InsertEzonTahwel @TNO,@FY,@TD,@TRNO,@TName,@MH,@FS,@FCE,@FNE,@TS,@MNO,@OR,@RTT,@ACC,@PACC,@MT,@MR,@MA,@EN,@MK,@S11,@S1,@S2,@S3,@S4,@S5,@S6,@LU,@LD,@aot output";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", (TXT_TRansferNo.Text));
            cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            cmd.Parameters.AddWithValue("@TD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));

            cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text.ToString());
            cmd.Parameters.AddWithValue("@TName", Cmb_CType.Text.ToString());
            cmd.Parameters.AddWithValue("@MH", TXT_MomayzHarka.Text.ToString());
            cmd.Parameters.AddWithValue("@FS", Cmb_From.Text.ToString());

            cmd.Parameters.AddWithValue("@FCE", Cmb_From.Text.ToString());
            cmd.Parameters.AddWithValue("@FNE", Constants.CodeEdara);
            cmd.Parameters.AddWithValue("@TS", Cmb_To.Text);
            cmd.Parameters.AddWithValue("@MNO", DBNull.Value);
            cmd.Parameters.AddWithValue("@OR", DBNull.Value);
            cmd.Parameters.AddWithValue("@RTT", "اذن تحويل مهمات");
            cmd.Parameters.AddWithValue("@ACC", TXT_AccNo.Text.ToString());
            cmd.Parameters.AddWithValue("@PACC", TXT_PaccNo.Text.ToString());
            cmd.Parameters.AddWithValue("@MT", TXT_MTaklif.Text.ToString());
            cmd.Parameters.AddWithValue("@MR", TXT_MResp.Text.ToString());
            cmd.Parameters.AddWithValue("@MA", TXT_Masrof.Text.ToString());
            cmd.Parameters.AddWithValue("@EN", TXT_Enfak.Text.ToString());
            cmd.Parameters.AddWithValue("@MK", TXT_Morakba.Text.ToString());
            cmd.Parameters.AddWithValue("@S11", DBNull.Value);
            cmd.Parameters.AddWithValue("@S1", FlagEmpn1);

            cmd.Parameters.AddWithValue("@S2", DBNull.Value);

            cmd.Parameters.AddWithValue("@S3", DBNull.Value);

            cmd.Parameters.AddWithValue("@S4", DBNull.Value);

            cmd.Parameters.AddWithValue("@S5", DBNull.Value);
            cmd.Parameters.AddWithValue("@S6", DBNull.Value);



            cmd.Parameters.AddWithValue("@LU", Constants.User_Name.ToString());
            cmd.Parameters.AddWithValue("@LD", Convert.ToDateTime(DateTime.Now.ToShortDateString()));

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
                InsertEznTahweelBnood();
                ////////////////

                for (int i = 1; i <= 5; i++)
                {
                    cmdstring = "Exec  SP_InsertSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
                    cmd = new SqlCommand(cmdstring, Constants.con);

                    cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_TRansferNo.Text));
                    cmd.Parameters.AddWithValue("@TNO2", Convert.ToInt32(TXT_TRNO.Text));

                    cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
                    cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
                    cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
                    cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);

                    cmd.Parameters.AddWithValue("@FN", 7);

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
                        string query = "exec SP_InsertTMinQuan @p1,@p2,@p3,@p4,@p5,@p6,@p7";
                        SqlCommand cmd1 = new SqlCommand(query, Constants.con);
                        cmd1.Parameters.AddWithValue("@p1", array1[i, 0]);
                        cmd1.Parameters.AddWithValue("@p2", array1[i, 1]);
                        cmd1.Parameters.AddWithValue("@p3", array1[i, 2]);
                        cmd1.Parameters.AddWithValue("@p4", array1[i, 3]);
                        cmd1.Parameters.AddWithValue("@p5", array1[i, 4]);
                        cmd1.Parameters.AddWithValue("@p6", array1[i, 5]);
                        cmd1.Parameters.AddWithValue("@p7", DBNull.Value);



                        cmd1.ExecuteNonQuery();

                    }
                }

                MessageBox.Show("تم الإضافة بنجاح  ! ");
                reset();
            }
            else if (executemsg == true && flag == 2)
            {
                MessageBox.Show("تم إدخال رقم اذن التحويل  من قبل  ! ");
            }
            else if (executemsg == false)
            {
                MessageBox.Show("لم يتم إدخال اذن التحويل بنجاج!!");
            }

            Constants.closecon();
        }

        private void UpdateEznTahweelTSignatureCycle()
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
            }
        }

        public void UpdateEznTahweel()
        {
            Constants.opencon();

            string cmdstring1 = "select STOCK_NO_ALL,quan1,quan2 from T_EznSarf_Benod where FYear=@FY and EznSarf_No=@TNO";
            SqlCommand cmd1 = new SqlCommand(cmdstring1, Constants.con);


            cmd1.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_TRansferNo.Text));
            cmd1.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            SqlDataReader dr = cmd1.ExecuteReader();

            //---------------------------------
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    if (dr["quan1"].ToString() != "")
                    {
                        string cmdstring2 = "Exec SP_UndoVirtualQuan2 @TNO,@FY";

                        SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);

                        cmd2.Parameters.AddWithValue("@TNO", (dr["STOCK_NO_ALL"].ToString()));
                        if (dr["quan2"].ToString() == "")
                        {

                            cmd2.Parameters.AddWithValue("@FY", Convert.ToDouble(dr["quan1"].ToString()));
                        }
                        else
                        {
                            cmd2.Parameters.AddWithValue("@FY", Convert.ToDouble(dr["quan2"].ToString()));
                        }

                        cmd2.ExecuteNonQuery();
                    }
                }
            }
            dr.Close();

            /////////////////////////////////////////
            string cmdstring = "Exec SP_UpdateEzonTahwel @TO,@FYO,@TNO,@FY,@TD,@TRNO,@TName,@MH,@FS,@FCE,@FNE,@TS,@MNO,@OR,@RTT,@ACC,@PACC,@MT,@MR,@MA,@EN,@MK,@S11,@S1,@S2,@S3,@S4,@S5,@S6,@LU,@LD,@aot output";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
            cmd.Parameters.AddWithValue("@TO", TNO);
            cmd.Parameters.AddWithValue("@FYO", FY);
            cmd.Parameters.AddWithValue("@TNO", (TXT_TRansferNo.Text));
            cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            cmd.Parameters.AddWithValue("@TD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));

            cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text.ToString());
            cmd.Parameters.AddWithValue("@TName", Cmb_CType.Text.ToString());
            cmd.Parameters.AddWithValue("@MH", TXT_MomayzHarka.Text.ToString());
            cmd.Parameters.AddWithValue("@FS", Cmb_From.Text.ToString());
            cmd.Parameters.AddWithValue("@FCE", Cmb_From.Text.ToString());
            cmd.Parameters.AddWithValue("@FNE", Constants.CodeEdara);
            cmd.Parameters.AddWithValue("@TS", Cmb_To.Text);
            cmd.Parameters.AddWithValue("@MNO", DBNull.Value);
            cmd.Parameters.AddWithValue("@OR", DBNull.Value);
            cmd.Parameters.AddWithValue("@RTT", "اذن تحويل مهمات");
            cmd.Parameters.AddWithValue("@ACC", TXT_AccNo.Text.ToString());
            cmd.Parameters.AddWithValue("@PACC", TXT_PaccNo.Text.ToString());
            cmd.Parameters.AddWithValue("@MT", TXT_MTaklif.Text.ToString());
            cmd.Parameters.AddWithValue("@MR", TXT_MResp.Text.ToString());
            cmd.Parameters.AddWithValue("@MA", TXT_Masrof.Text.ToString());
            cmd.Parameters.AddWithValue("@EN", TXT_Enfak.Text.ToString());
            cmd.Parameters.AddWithValue("@MK", TXT_Morakba.Text.ToString());


            if (FlagSign111 == 1)
            {
                cmd.Parameters.AddWithValue("@S11", FlagEmpn111);

            }
            else
            {
                cmd.Parameters.AddWithValue("@S11", DBNull.Value);

            }

            if (FlagSign1 == 1)
            {
                cmd.Parameters.AddWithValue("@S1", FlagEmpn1);

            }
            else
            {
                cmd.Parameters.AddWithValue("@S1", DBNull.Value);

            }

            if (FlagSign2 == 1)
            {
                cmd.Parameters.AddWithValue("@S2", FlagEmpn2);

            }
            else
            {
                cmd.Parameters.AddWithValue("@S2", DBNull.Value);

            }

            if (FlagSign3 == 1)
            {
                cmd.Parameters.AddWithValue("@S3", FlagEmpn3);

            }
            else
            {
                cmd.Parameters.AddWithValue("@S3", DBNull.Value);

            }

            if (FlagSign4 == 1)
            {
                cmd.Parameters.AddWithValue("@S4", FlagEmpn4);

            }
            else
            {
                cmd.Parameters.AddWithValue("@S4", DBNull.Value);

            }

            if (FlagSign5 == 1)
            {
                cmd.Parameters.AddWithValue("@S5", FlagEmpn5);

            }
            else
            {
                cmd.Parameters.AddWithValue("@S5", DBNull.Value);

            }

            if (FlagSign6 == 1)
            {
                cmd.Parameters.AddWithValue("@S6", FlagEmpn6);

            }
            else
            {
                cmd.Parameters.AddWithValue("@S6", DBNull.Value);

            }

            cmd.Parameters.AddWithValue("@LU", Constants.User_Name.ToString());
            cmd.Parameters.AddWithValue("@LD", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
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

            if (executemsg == true && flag == 2)
            {
                InsertEznTahweelBnood();
                UpdateEznTahweelTSignatureCycle();



                if (FlagSign4 == 1 && FlagSign5 != 1 && FlagSign6 != 1 && TXT_TRNO.Text.ToString() == "15")
                {
                    UpdateQuan();
                    InsertTransSarf();
                    //then
                    UpdateQuan2();
                    InsertTransEdafa();
                }
                else if (FlagSign4 == 1 && FlagSign5 != 1 && FlagSign6 != 1 && TXT_TRNO.Text.ToString() == "62")
                {
                    UpdateQuan2();
                    InsertTransEdafa();
                }


                MessageBox.Show("تم التعديل بنجاح  ! ");
                reset();
            }
            else if (executemsg == true && flag == 3)
            {
                MessageBox.Show("تم إدخال رقم طلب التوريد  من قبل  ! ");
            }
            Constants.closecon();
        }

        private void EditLogic()
        {
            UpdateEznTahweel();
        }

        private void DeleteLogic()
        {
            if ((MessageBox.Show("هل تريد حذف اذن التحويل ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(TXT_TRansferNo.Text) || string.IsNullOrWhiteSpace(TXT_TRNO.Text))
                {
                    MessageBox.Show("يجب اختياراذن التحويل  اولا");
                    return;
                }
                Constants.opencon();

                string cmdstring = "Exec SP_DeleteEznTahwel @TNO,@FY,@TR,@aot output";

                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_TRansferNo.Text));
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
                cmd.Parameters.AddWithValue("@TR", TXT_TRNO.Text.ToString());
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
                    //reset();
                }
                Constants.closecon();
            }
        }
      
        #endregion


        public FTransfer_M()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        //======================================
        private void FTransfer_M_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aNRPC_InventoryDataSet.T_BnodAwamershraa' table. You can move, or remove it, as needed.
            // this.t_BnodAwamershraaTableAdapter.Fill(this.aNRPC_InventoryDataSet.T_BnodAwamershraa);

            HelperClass.comboBoxFiller(Cmb_FYear, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
            HelperClass.comboBoxFiller(Cmb_FYear2, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);

            if (Constants.EzonTahwel_FF == false)
            {
                panel7.Visible = true;
                panel2.Visible = false;
            }
            else if (Constants.EzonTahwel_FF == true)
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

            //*******************************************s
            // ******    AUTO COMPLETE
            //*******************************************
            string cmdstring = "select STOCK_NO_ALL,Stock_NO_Nam,PartNO,BIAN_TSNIF from T_Tsnif  where (StatusFlag in (0,1,2)) and   CodeEdara=" + Constants.CodeEdara;
            SqlCommand cmd = new SqlCommand(cmdstring, con);
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
            Cmb_FYear.SelectedIndex = 0;
            string cmdstring3 = "SELECT [TransNo] from T_EzonTahwel where FromEdaraCode=" + Constants.CodeEdara + " and  FYear='" + Cmb_FYear.Text + "'and TR_NO='" + TXT_TRNO.Text + "'";
            SqlCommand cmd3 = new SqlCommand(cmdstring3, con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            //---------------------------------
            if (dr3.HasRows == true)
            {
                while (dr3.Read())
                {
                    EznColl.Add(dr3["TransNo"].ToString());

                }
            }
            dr3.Close();
            ///////////////////////////////////////////////////////
            Constants.opencon();
            Cmb_CType.SelectedIndexChanged -= new EventHandler(Cmb_CType_SelectedIndexChanged);
            cmdstring = "SELECT  [CCode],[CName] FROM [T_TransferTypes] where CType=3 and CFlag=1";//will use cmdstring3


            cmd = new SqlCommand(cmdstring, Constants.con);

            //cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
            DataTable dts = new DataTable();

            dts.Load(cmd.ExecuteReader());
            Cmb_CType.DataSource = dts;
            Cmb_CType.ValueMember = "CCode";
            Cmb_CType.DisplayMember = "CName";
            Cmb_CType.SelectedIndex = -1;
            Cmb_CType.SelectedIndexChanged += new EventHandler(Cmb_CType_SelectedIndexChanged);

            //////////////////
            Cmb_CType2.DataSource = dts;
            Cmb_CType2.ValueMember = "CCode";
            Cmb_CType2.DisplayMember = "CName";
            Cmb_CType2.SelectedIndex = -1;

            ////////////////////////////////////////////////
            TXT_StockNoAll.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_StockNoAll.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_StockNoAll.AutoCompleteCustomSource = TasnifColl;

            TXT_PartNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_PartNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_PartNo.AutoCompleteCustomSource = PartColl;


            TXT_StockName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_StockName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_StockName.AutoCompleteCustomSource = TasnifNameColl;

            TXT_TRansferNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_TRansferNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_TRansferNo.AutoCompleteCustomSource = EznColl;

            con.Close();

            reset();
        }
        //===========================================================================

        public void SearchTasnif(int searchflag)
        {

            string query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where STOCK_NO_ALL= @a";

            SqlCommand cmd = new SqlCommand(query, con);
            if (searchflag == 1)
            {
                cmd.Parameters.AddWithValue("@a", (TXT_StockNoAll.Text));
            }
            else if (searchflag == 2)
            {
                query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where STOCK_NO_NAM = @a or BIAN_TSNIF = @a";
                cmd = new SqlCommand(query, con);
                // cmd.Parameters.AddWithValue("@a", (TXT_PartNo.Text));
                cmd.Parameters.AddWithValue("@a", (TXT_StockName.Text));
            }

            else if (searchflag == 3)
            {

                query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where PartNO= @a";
                cmd = new SqlCommand(query, con);
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
                    //  Num_Quan.Text = dr["Quan"].ToString();


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

                //   string query = "SELECT stock_no_all,[PRICE_UNIT] , in_mm ,in_yy FROM [tr_out_1_2015_2020] where stock_no_all=@a order by in_yy desc ,in_mm desc";
                SqlCommand cmd4 = new SqlCommand(query, con);
                cmd4.Parameters.AddWithValue("@a", TXT_StockNoAll.Text);
                //      }




                DataTable dts = new DataTable();
                dts.Load(cmd4.ExecuteReader());

                CMB_ApproxValue.DataSource = dts;
                CMB_ApproxValue.ValueMember = "PRICE_UNIT";
                CMB_ApproxValue.DisplayMember = "x";
                CMB_ApproxValue.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("من فضلك تاكد من التصنيف");

            }
            dr.Close();
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics surface = e.Graphics;
            Pen pen1 = new Pen(Color.Black, 2);
            surface.DrawLine(pen1, panel1.Location.X + 4, 4, panel1.Location.X + 4, panel1.Location.Y + panel1.Size.Height); // Left Line
            surface.DrawLine(pen1, panel1.Size.Width - 4, 4, panel1.Size.Width - 4, panel1.Location.Y + panel1.Size.Height); // Right Line
            //---------------------------
            surface.DrawLine(pen1, 4, 4, panel1.Location.X + panel1.Size.Width - 4, 4); // Top Line
            surface.DrawLine(pen1, 4, panel1.Size.Height - 1, panel1.Location.X + panel1.Size.Width - 4, panel1.Size.Height - 1); // Bottom Line

            //---------------------------
            // Middle_Line
            //-------------
            // surface.DrawLine(pen1, ((panel1.Size.Width) / 2) + 4, 4, ((panel1.Size.Width) / 2) + 4, panel1.Location.Y + panel1.Size.Height); // Left Line
            //surface.DrawLine(pen1, 4, 38, panel1.Location.X + panel1.Size.Width - 4, 40); // Top Line
            surface.Dispose();
        }



        private void TXT_StockName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }  //--> OPEN CONNECTION
                CMB_ApproxValue.Text = "";
                SearchTasnif(2);
            }
        }

        private void TXT_StockNoAll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }  //--> OPEN CONNECTION

                CMB_ApproxValue.Text = "";
                SearchTasnif(1);
            }
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد اضافة اذن تحويل جديد؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                reset();
                PrepareAddState();

                AddEditFlag = 2;
                TXT_MomayzHarka.Text = Constants.NameEdara;
            }
        }





        private void EditBtn_Click(object sender, EventArgs e)
        {
            AddEditFlag = 1;
        }

        private void Addbtn2_Click(object sender, EventArgs e)
        {

            //if (!IsValidCase(VALIDATION_TYPES.ADD_TASNIF))
            //{
            //    return;
            //}


            if (checkBox1.Checked == true || checkBox2.Checked == true)
                {
                    if (Convert.ToDouble(Txt_Quan.Text) - Convert.ToDouble(Txt_ReqQuan.Text) < Convert.ToDouble(Quan_Min.Value))
                    {
                        MessageBox.Show("بعد صرف الكمية المطلوبة الكمية المتاحة ستكون اقل من الحد الادنى ");
                        MaxFlag = MaxFlag + 1;

                        //  return;
                        array1[MaxFlag - 1, 3] = TXT_StockNoAll.Text;
                        array1[MaxFlag - 1, 0] = TXT_TRansferNo.Text;
                        array1[MaxFlag - 1, 1] = TXT_TRansferNo.Text;

                        array1[MaxFlag - 1, 2] = Cmb_FYear.Text;
                        array1[MaxFlag - 1, 4] = Txt_ReqQuan.Text;
                        array1[MaxFlag - 1, 5] = Quan_Min.Text;

                    }

                }

            AddNewTasnifInDataGridView();
        }

        private void Cmb_FYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AddEditFlag == 0)
            {
                Constants.opencon();

                TXT_TRansferNo.AutoCompleteMode = AutoCompleteMode.None;
                TXT_TRansferNo.AutoCompleteSource = AutoCompleteSource.None; ;

                string cmdstring3 = "";
                if (Constants.User_Type == "A")
                {
                    cmdstring3 = "SELECT [TransNo] from T_EzonTahwel where FromEdaraCode=" + Constants.CodeEdara + " and  FYear='" + Cmb_FYear.Text + "' and TR_NO='" + TXT_TRNO.Text + "'";
                }
                else
                {
                    cmdstring3 = "SELECT [TransNo] from T_EzonTahwel where  FYear='" + Cmb_FYear.Text + "' and TR_NO='" + TXT_TRNO.Text + "'";
                }

                SqlCommand cmd3 = new SqlCommand(cmdstring3, Constants.con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                //---------------------------------
                if (dr3.HasRows == true)
                {
                    while (dr3.Read())
                    {
                        EznColl.Add(dr3["TransNo"].ToString());
                    }
                }

                TXT_TRansferNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                TXT_TRansferNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                TXT_TRansferNo.AutoCompleteCustomSource = EznColl;
            }

            if (AddEditFlag == 2)//add
            {

                if (TXT_TRansferNo.Text != "")
                {
                    return;
                }

                Constants.opencon();

                string cmdstring = "select ( COALESCE(MAX(cast(TransNo as int)), 0)) from  T_EzonTahwel where FYear=@FY and TR_NO=@TR ";
                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text);
                cmd.Parameters.AddWithValue("@TR", TXT_TRNO.Text);

                int flag;

                try
                {
                    Constants.opencon();

                    var count = cmd.ExecuteScalar();
                    executemsg = true;

                    if (count != null && count != DBNull.Value)
                    {

                        flag = (int)count;
                        flag = flag + 1;
                        /////////////////////////done by nouran//////////////////////

                        string cmdstring2 = "select ( COALESCE(MAX(cast(Tahwel_No as int)), 0)) from  T_TempTahwelNo where FYear=@FY and TRNO=@TR ";

                        SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);

                        cmd2.Parameters.AddWithValue("@FY", Cmb_FYear.Text);
                        cmd2.Parameters.AddWithValue("@TR", TXT_TRNO.Text);

                        //-----------------------------------
                        var count2 = cmd2.ExecuteScalar();
                        executemsg = true;

                        if (count2 != null && count2 != DBNull.Value)
                        {
                            if (flag <= (int)count2)
                            {
                                flag = (int)count2 + 1;
                            }
                        }

                        /////// insert temp table//////////////
                        string query = "exec SP_InsertTempTahwelNo @p1,@p2,@p3";
                        SqlCommand cmd1 = new SqlCommand(query, Constants.con);
                        cmd1.Parameters.AddWithValue("@p1", flag);
                        cmd1.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd1.Parameters.AddWithValue("@p3", TXT_TRNO.Text);

                        cmd1.ExecuteNonQuery();

                        ///////////////////////////end by nouran///////////////////////


                        TXT_TRansferNo.Text = flag.ToString();//el rakm el new
                        //    TXT_EznNo.Focus();
                        if (AddEditFlag == 2)
                        {
                            // GetData(Convert.ToInt32(TXT_TalbNo.Text), Cmb_FYear.Text);
                            if (string.IsNullOrEmpty(TXT_TRansferNo.Text) == false)
                            {
                                GetData((TXT_TRansferNo.Text), Cmb_FYear.Text, TXT_TRNO.Text);

                            }

                        }

                    }

                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    Console.WriteLine(sqlEx);
                }
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {

            if (AddEditFlag == 2)
            {
                if (FlagSign1 != 1)
                {
                    MessageBox.Show("من فضلك تاكد من توقيع اذن الصرف");
                    return;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells[11].Value.ToString() == "")
                        {
                            MessageBox.Show("من فضلك تاكد من ادخال نسبة الصلاحية لجميع البنود");
                            return;
                        }
                    }
                    //  dataGridView1.ReadOnly = true;
                    row.Cells[6].ReadOnly = false;//in perm
                }

                AddLogic();
            }
            else if (AddEditFlag == 1)
            {
                EditLogic();
            }

        }

        private void Cmb_FYear2_SelectedIndexChanged(object sender, EventArgs e)
        {

            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            string cmdstring = "";
            if (Constants.User_Type == "A")
            {
                cmdstring = "select TransNo from T_EzonTahwel where FYear=@FY and FromEdaraCode=@CE ";//and TR_NO=@TR";

            }

            else if (Constants.User_Type == "B" && Constants.UserTypeB == "NewTasnif")
            {

                cmdstring = "select TransNo from T_EzonTahwel where FYear=@FY and TR_NO=@TR and( Sign1 is not null and Sign2 is not null)  and(Sign3 is null) ";


            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Estlam")
            {

                cmdstring = "select TransNo from T_EzonTahwel where FYear=@FY and TR_NO=@TR and( Sign3 is not null)  and(Sign4 is null) ";

            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Transfer1")
            {

                cmdstring = "select TransNo from T_EzonTahwel where FYear=@FY and TR_NO=@TR and( Sign4 is not null)  and(Sign5 is null) ";


            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Transfer2")
            {
                cmdstring = "select TransNo from T_EzonTahwel where FYear=@FY and TR_NO=@TR and( Sign5 is not null)  and(Sign6 is null) ";

            }
            else if (Constants.User_Type == "B" && (Constants.UserTypeB == "Tkalif" || Constants.UserTypeB == "Finance"))
            {

                cmdstring = "select TransNo from T_EzonTahwel where FYear=@FY and TR_NO=@TR and( Sign6 is not null)  ";

            }

            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            // cmdstring = "select (EznSarf_No) from  T_EznSarf where FYear=@FY and CodeEdara=@CE ";


            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
            cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
            cmd.Parameters.AddWithValue("@TR", TXT_TRNO.Text);

            DataTable dts = new DataTable();

            dts.Load(cmd.ExecuteReader());
            Cmb_TRansferNo2.DataSource = dts;
            Cmb_TRansferNo2.ValueMember = "TransNo";
            Cmb_TRansferNo2.DisplayMember = "TransNo";
            Cmb_TRansferNo2.SelectedIndex = -1;
            Cmb_TRansferNo2.SelectedIndexChanged += new EventHandler(Cmb_TalbNo2_SelectedIndexChanged);
            Constants.closecon();

        }

        private void Cmb_TalbNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_TRansferNo2.SelectedIndex != -1)
            {
                SearchTalb(2);
            }
        }


        private void BTN_Save2_Click(object sender, EventArgs e)
        {
            //if (!IsValidCase(VALIDATION_TYPES.SAVE))
            //{
            //    return;
            //}

            EditLogic();

            reset();

            Cmb_CType2.SelectedIndex = -1;
            Cmb_TRansferNo2.SelectedIndex = -1;
            Cmb_FYear2.SelectedIndex = -1;

            TXT_TRansferNo.Enabled = false;
            Cmb_FYear.Enabled = false;
            Cmb_CType.Enabled = false;
        }


        private void Cmb_TRansferNo2_TextChanged(object sender, EventArgs e)
        {
            Pic_Sign1.Image = null;
            Pic_Sign2.Image = null;
            Pic_Sign3.Image = null;
            Pic_Sign4.Image = null;
            Pic_Sign5.Image = null;

            FlagSign1 = 0;
            FlagSign2 = 0;
            FlagSign3 = 0;
            FlagSign4 = 0;
            FlagSign5 = 0;

            Pic_Sign1.BackColor = Color.White;
            Pic_Sign2.BackColor = Color.White;
            Pic_Sign3.BackColor = Color.White;
            Pic_Sign4.BackColor = Color.White;
            Pic_Sign5.BackColor = Color.White;
        }

        private void dataGridView1_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == dataGridView1.NewRowIndex)
            {
                // user is in the new row, disable controls.


                dataGridView1.Rows[e.RowIndex].Cells[0].Value = TXT_TRansferNo.Text;
                dataGridView1.Rows[e.RowIndex].Cells[1].Value = TXT_TRNO.Text;
                dataGridView1.Rows[e.RowIndex].Cells[2].Value = Cmb_FYear.Text;//in perm
                dataGridView1.Rows[e.RowIndex].Cells[3].Value = e.RowIndex + 1;
                //   dataGridView1.Rows[e.RowIndex].Cells[5].Value = 1;//in perm
                //  dataGridView1.Rows[e.RowIndex].Cells[10].Value = PermNo_text.Text;
                dataGridView1.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                dataGridView1.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                dataGridView1.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                dataGridView1.Rows[e.RowIndex].Cells[3].ReadOnly = true;
                /*
                 dataGridView1.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                 dataGridView1.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                 dataGridView1.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                 dataGridView1.Rows[e.RowIndex].Cells[8].ReadOnly = true;
                 dataGridView1.Rows[e.RowIndex].Cells[9].ReadOnly = true;
                 dataGridView1.Rows[e.RowIndex].Cells[10].ReadOnly = true;
                 dataGridView1.Rows[e.RowIndex].Cells[11].ReadOnly = true*/


                //  dataGridView1.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;

            }
        }

        private void TXT_TalbNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && AddEditFlag == 2)
            {
                GetData((TXT_TRansferNo.Text), Cmb_FYear.Text, TXT_TRNO.Text);

            }
            else if (e.KeyCode == Keys.Enter && AddEditFlag == 0)
            {
                cleargridview();
                SearchTalb(1);

            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {

                if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[4].Value != null)
                {
                    //oldvalue = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                }
            }

        }


        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "printTool")
            {
                if ((MessageBox.Show("هل تريد طباعة بطاقة الصنف ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
                {
                    Constants.Unit = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    Constants.TasnifNo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    Constants.TasnifName = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    Constants.Desc = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    Constants.Quan = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    Constants.RakmEdafa = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    Constants.DateEdafa = dataGridView1.CurrentRow.Cells[8].Value.ToString();

                    Constants.FormNo = 1;
                    FReports F = new FReports();
                    F.Show();

                }

                else
                { //No
                    //----
                }
                //----------------------------------------
            }
        }


        private void TXT_StockNoAll_TextChanged(object sender, EventArgs e)
        {
            Txt_ReqQuan.Text = "";
        }

        private void TXT_EznNo_TextChanged(object sender, EventArgs e)
        {
            Constants.validateTextboxNumbersonly(sender);
        }

        private void TXT_Momayz_TextChanged(object sender, EventArgs e)
        {
            Constants.validateTextboxNumbersonly(sender);

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 4)// || dataGridView1.CurrentCell.ColumnIndex == 4 || dataGridView1.CurrentCell.ColumnIndex == 10 || dataGridView1.CurrentCell.ColumnIndex == 11)//reqQuan
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


        private void BTN_Print_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد طباعة تقرير اذن التحويل؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_TRansferNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text))
                {
                    MessageBox.Show("يجب اختيار اذن التحويل المراد طباعتها اولا");
                    return;
                }
                else
                {

                    Constants.FormNo = 77;
                    Constants.TransferNO = (TXT_TRansferNo.Text);
                    Constants.TRFY = Cmb_FYear.Text;
                    Constants.TRNO = TXT_TRNO.Text;
                    FReports F = new FReports();
                    F.Show();
                }
            }
            else
            {

            }
        }

        private void TXT_PartNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }  //--> OPEN CONNECTION
                CMB_ApproxValue.Text = "";
                SearchTasnif(3);
            }
        }

        private void TXT_EznNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TXT_TRansferNo.Text) == false)
            {
                GetData((TXT_TRansferNo.Text), Cmb_FYear.Text, TXT_TRNO.Text);

            }
        }

        private void Cmb_CType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!(string.IsNullOrEmpty(Cmb_CType.Text) || string.IsNullOrWhiteSpace(Cmb_CType.Text) || Cmb_CType.SelectedIndex == -1))
            {
                TXT_TRNO.Text = Cmb_CType.SelectedValue.ToString();
            }
            else
            {
                return;
            }

            Constants.opencon();

            string cmdstring;
            SqlCommand cmd;
            DataTable dts2;

            if (TXT_TRNO.Text == "62")
            {
                Cmb_From.Enabled = false;
                cmdstring = "SELECT  CodeEdara,NameEdara FROM Edarat ";//will use cmdstring3
                cmd = new SqlCommand(cmdstring, Constants.con);

                dts2 = new DataTable();
                dts2.Load(cmd.ExecuteReader());
                Cmb_From.DataSource = dts2;
                Cmb_From.ValueMember = "CodeEdara";
                Cmb_From.DisplayMember = "NameEdara";
                Cmb_From.SelectedIndex = -1;

                Cmb_From.SelectedValue = Constants.CodeEdara;
            }
            else if (TXT_TRNO.Text == "15")
            {
                Cmb_From.Enabled = true;
                cmdstring = "SELECT  [CCode],[CName] FROM T_InventoryNames";//will use cmdstring3
                cmd = new SqlCommand(cmdstring, Constants.con);

                dts2 = new DataTable();
                dts2.Load(cmd.ExecuteReader());
                Cmb_From.DataSource = dts2;
                Cmb_From.ValueMember = "CCode";
                Cmb_From.DisplayMember = "CName";
                Cmb_From.SelectedIndex = -1;
            }

            cmdstring = "SELECT  [CCode],[CName] FROM T_InventoryNames";
            cmd = new SqlCommand(cmdstring, Constants.con);
            dts2 = new DataTable();
            dts2.Load(cmd.ExecuteReader());
            Cmb_To.DataSource = dts2;
            Cmb_To.ValueMember = "CCode";
            Cmb_To.DisplayMember = "CName";
            Cmb_To.SelectedIndex = -1;
    
        }


        private void Cmb_To_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_To.SelectedIndex >= 0 && Cmb_From.SelectedIndex == Cmb_To.SelectedIndex)
            {
                MessageBox.Show("لايمكن عمل إذن تحويل من مخزن إلي نفس المخزن");
                Cmb_To.SelectedIndex = -1;
                Cmb_To.Text = "";

                return;
            }
        }


        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 13) // 1 should be your column index
            {

                if (Convert.ToString(e.FormattedValue).Length != 8 && dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString() != "True")
                {

                    e.Cancel = true;
                    MessageBox.Show("رقم التصنيف يجب ان يتكون من 8 ");
                    //  dataGridView1.Rows[e.RowIndex].ErrorText = "please enter numeric";

                }
                //check that it exist in master 
                //    else if (dataGridView1.Rows[e.RowIndex].Cells[6].Value != DBNull.Value)// && dataGridView1.Rows[e.RowIndex].Cells[11].Value != "true")
                else if (e.FormattedValue != DBNull.Value && e.FormattedValue != "")// && dataGridView1.Rows[e.RowIndex].Cells[11].Value != "true")
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

                    int flag1;


                    // cmd3.ExecuteNonQuery();
                    //  int flag1;
                    Constants.opencon();
                    try
                    {

                        cmd.ExecuteNonQuery();
                        executemsg = true;

                        flag1 = (int)cmd.Parameters["@flag"].Value;

                        dataGridView1.Rows[e.RowIndex].Cells[15].Value = cmd.Parameters["@p1"].Value;
                        dataGridView1.Rows[e.RowIndex].Cells[14].Value = cmd.Parameters["@p2"].Value;
                        dataGridView1.Rows[e.RowIndex].Cells[16].Value = cmd.Parameters["@p3"].Value;
                        dataGridView1.Rows[e.RowIndex].Cells[13].Value = e.FormattedValue;
                        //   dataGridView1.Rows[e.RowIndex].Cells[9].ReadOnly = false;//Approx value can be changed 

                        if (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString().ToUpper() == dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString().ToUpper())
                        {
                            MessageBox.Show("لايمكن تحويل تصنيف إلي نفسة");
                            dataGridView1.Rows[e.RowIndex].Cells[13].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells[15].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells[14].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells[16].Value = "";
                       
                        }
                        if (flag1 != 2)
                        {
                            /*
                            if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value) >= Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value))
                            {
                                MessageBox.Show("كمية المطلوبة اقل من كمية المخزن لا نحناج الى طلب توريد");
                                return;
                            }

                            else if ((Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value) < Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value)) && Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value) != 0)
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[3].Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value) - Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                                dataGridView1.Rows[e.RowIndex].Cells[10].Value = dataGridView1.Rows[e.RowIndex].Cells[7].Value;

                            }
                            else if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value) == 0)
                            {
                                // dataGridView1.Rows[e.RowIndex].Cells[3].Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value) - Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                                dataGridView1.Rows[e.RowIndex].Cells[10].Value = 0;

                            }
                            */
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


        //------------------------------------------ Signature Handler ---------------------------------
        #region Signature Handler
        private void BTN_Sign1_Click(object sender, EventArgs e)
        {
            Empn1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على انشاء اذن تحويل", "");

            Sign1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على انشاء اذن تحويل", "");
            if (Sign1 != "" && Empn1 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("1", "7", Sign1, Empn1);
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
            Empn2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على اعتماد اذن تحويل", "");

            Sign2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على اعتماد اذن تحويل", "");

            if (Sign2 != "" && Empn2 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("2", "7", Sign2, Empn2);
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

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteLogic();
        }

        private void BTN_Sign3_Click(object sender, EventArgs e)
        {
            Empn3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على استلام طلب توريد", "");

            Sign3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على استلام طلب توريد", "");

            if (Sign3 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("3", "7", Sign3, Empn3);
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
            Empn4 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على استلام اذن الصرف", "");

            Sign4 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على استلام اذن الصرف", "");

            if (Sign4 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("4", "7", Sign4, Empn4);

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

        private void BTN_Sign5_Click(object sender, EventArgs e)
        {
            Empn5 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على رقم القيد", "");

            Sign5 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على رقم القيد", "");

            if (Sign5 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("5", "7", Sign5, Empn5);
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
            }
        }

        private void BTN_Sign111_Click(object sender, EventArgs e)
        {
            Empn6 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على انشاء اذن تحويل", "");

            Sign6 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على انشاء اذن تحويل", "");
            
            if (Sign6 != "" && Empn6 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("6", "7", Sign6, Empn6);
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

        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            AddEditFlag = 0;
            reset();
        }

        private void BTN_Search_Click(object sender, EventArgs e)
        {
            //if (!IsValidCase(VALIDATION_TYPES.SEARCH))
            //{
            //    return;
            //}

            //string ezn_no = TXT_EznNo.Text;
            //string fyear = Cmb_FYear.Text;
            //string momayz = TXT_TRNO.Text;

            //reset();

            //if (SearchEznSarf(ezn_no, fyear, momayz))
            //{
            //    if (FlagSign2 != 1 && FlagSign1 != 1)
            //    {
            //        Editbtn2.Enabled = true;
            //    }
            //    else
            //    {
            //        Editbtn2.Enabled = false;
            //    }
            //}
        }

        private void BTN_Search_Motab3a_Click(object sender, EventArgs e)
        {
            //if (!IsValidCase(VALIDATION_TYPES.CONFIRM_SEARCH))
            //{
            //    return;
            //}

            //string ezn_no = Cmb_EznNo2.Text;
            //string fyear = Cmb_FYear2.Text;
            //string momayz = TXT_TRNO2.Text;

            //reset();

            //if (SearchEznSarf(ezn_no, fyear, momayz))
            //{
            //    Editbtn.Enabled = true;
            //    BTN_Print2.Enabled = true;
            //}

            //TXT_EznNo.Enabled = false;
            //Cmb_FYear.Enabled = false;
            //Cmb_CType.Enabled = false;
        }

        private void Editbtn2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد تعديل اذن التحويل ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_TRansferNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text) || string.IsNullOrEmpty(TXT_TRNO.Text))
                {
                    MessageBox.Show("يجب اختيار اذن التحويل المراد تعديله");
                    return;
                }

                PrepareEditState();
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد تعديل اذن التحويل ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_TRansferNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text) || string.IsNullOrEmpty(TXT_TRNO.Text))
                {
                    MessageBox.Show("يجب اختيار اذن التحويل المراد تعديله");
                    return;
                }

                PrepareConfirmState();
            }
        }
    }
}
