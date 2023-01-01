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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ANRPC_Inventory
{
    public partial class EznSarf_F : Form
    {
        #region Def Variables
            List<CurrencyInfo> currencies = new List<CurrencyInfo>();
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

        public void SP_UpdateSignatures(int x, DateTime D1, DateTime? D2 = null)
        {
            string cmdstring = "Exec  SP_UpdateSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_EznNo.Text));
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

        private void InsertEznSarfBnood()
        {
            SqlCommand cmd;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string q = "exec SP_InsertBnodEznSarf @p1,@p111,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12 ";
                    cmd = new SqlCommand(q, Constants.con);
                    cmd.Parameters.AddWithValue("@p1", row.Cells[0].Value);
                    cmd.Parameters.AddWithValue("@p111", TXT_TRNO.Text);///new
                    cmd.Parameters.AddWithValue("@p2", row.Cells[1].Value);

                    cmd.Parameters.AddWithValue("@p3", row.Cells[2].Value);
                    cmd.Parameters.AddWithValue("@p4", row.Cells[3].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p5", row.Cells[5].Value);
                    cmd.Parameters.AddWithValue("@p6", row.Cells[4].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p7", row.Cells[6].Value);
                    cmd.Parameters.AddWithValue("@p8", row.Cells[7].Value);
                    cmd.Parameters.AddWithValue("@p9", row.Cells[8].Value);
                    cmd.Parameters.AddWithValue("@p10", row.Cells[9].Value);
                    cmd.Parameters.AddWithValue("@p11", row.Cells[10].Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p12", row.Cells[11].Value);

                    cmd.ExecuteNonQuery();
                }
            }
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string q = "exec SP_UpdateVirtualQuan @stockall,@additionstock,@p3";
                    cmd = new SqlCommand(q, Constants.con);

                    if (row.Cells[4].Value == DBNull.Value || row.Cells[4].Value.ToString() == "")
                    {
                        cmd.Parameters.AddWithValue("@stockall", row.Cells[3].Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@stockall", row.Cells[4].Value);
                    }
                    cmd.Parameters.AddWithValue("@additionstock", row.Cells[9].Value);
                    cmd.Parameters.AddWithValue("@p3", 1);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateQuan()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string cmdstring = "Exec SP_UpdateQuanTsnif @Quan,@ST,@F,@EZ,@FY,@B,@TRNO";

                    SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                    cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[4].Value));
                    //will send rased badl else monsrf
                    // cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[10].Value));
                    cmd.Parameters.AddWithValue("@ST", (row.Cells[9].Value));
                    cmd.Parameters.AddWithValue("@F", 2);
                    cmd.Parameters.AddWithValue("@EZ", TXT_EznNo.Text);
                    cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text);

                    cmd.Parameters.AddWithValue("@B", row.Cells[2].Value);
                    cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertTrans()
        {
            string cmdstring = "Exec SP_deleteTR2 @TNO,@FY,@TRNO";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_EznNo.Text));
            cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text.ToString());

            cmd.ExecuteNonQuery();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (!row.IsNewRow)
                {

                    cmdstring = "exec SP_InsertTR2 @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29";
                    cmd = new SqlCommand(cmdstring, Constants.con);

                    cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_EznNo.Text));
                    cmd.Parameters.AddWithValue("@p2", Cmb_FYear.Text.ToString());
                    cmd.Parameters.AddWithValue("@p3", row.Cells[2].Value);
                    cmd.Parameters.AddWithValue("@p4", row.Cells[9].Value);
                    cmd.Parameters.AddWithValue("@p5", TXT_Date.Value.Date);
                    cmd.Parameters.AddWithValue("@p6", TXT_TRNO.Text.ToString());
                    cmd.Parameters.AddWithValue("@p7", TXT_AccNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@p8", TXT_PaccNo.Text.ToString());
                    string st = row.Cells[9].Value.ToString();
                    cmd.Parameters.AddWithValue("@p9", (st).Substring(0, 2));
                    cmd.Parameters.AddWithValue("@p10", (st).Substring(2, 2));

                    cmd.Parameters.AddWithValue("@p11", (st).Substring(4, 2));
                    cmd.Parameters.AddWithValue("@p12", (st).Substring(6, 2));
                    cmd.Parameters.AddWithValue("@p13", row.Cells[4].Value);
                    cmd.Parameters.AddWithValue("@p14", row.Cells[10].Value);
                    cmd.Parameters.AddWithValue("@p15", row.Cells[5].Value);
                    cmd.Parameters.AddWithValue("@p16", TXT_Edara.SelectedText);
                    cmd.Parameters.AddWithValue("@p17", TXT_Edara.SelectedText);
                    cmd.Parameters.AddWithValue("@p18", TXT_Date.Value.Day.ToString());
                    cmd.Parameters.AddWithValue("@p19", TXT_Date.Value.Month.ToString());
                    cmd.Parameters.AddWithValue("@p20", TXT_Date.Value.Year.ToString());

                    cmd.Parameters.AddWithValue("@p21", (row.Cells[8].Value));
                    cmd.Parameters.AddWithValue("@p22", row.Cells[7].Value);
                    cmd.Parameters.AddWithValue("@p23", TXT_MTaklif.Text.ToString());
                    cmd.Parameters.AddWithValue("@p24", TXT_MResp.Text.ToString());
                    cmd.Parameters.AddWithValue("@p25", TXT_Masrof.Text.ToString());
                    cmd.Parameters.AddWithValue("@p26", TXT_Enfak.Text.ToString());
                    cmd.Parameters.AddWithValue("@p27", TXT_Enfak.Text.ToString());
                    cmd.Parameters.AddWithValue("@p28", TXT_Morakba.Text.ToString());
                    cmd.Parameters.AddWithValue("@p29", row.Cells[11].Value);
                    // cmd.Parameters.AddWithValue("@p30", Cmb_FYear.Text.ToString());
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("تم ادخال الحركة بنجاح");
        }

        private void AddNewTasnifInDataGridView()
        {
            #region Add row to dataGridView
            r = dataGridView1.Rows.Count - 1;

            rowflag = 1;
            DataRow newRow = table.NewRow();

            // Add the row to the rows collection.
            //   table.Rows.Add(newRow);
            table.Rows.InsertAt(newRow, r);

            dataGridView1.DataSource = table;
            dataGridView1.Rows[r].Cells[3].Value = Convert.ToDouble(Txt_ReqQuan.Text);
            dataGridView1.Rows[r].Cells[5].Value = TXT_Unit.Text;
            dataGridView1.Rows[r].Cells[6].Value = TXT_Unit.Text;
            dataGridView1.Rows[r].Cells[7].Value = TXT_StockBian.Text;
            dataGridView1.Rows[r].Cells[8].Value = TXT_StockName.Text;
            dataGridView1.Rows[r].Cells[9].Value = TXT_StockNoAll.Text;

            if (string.IsNullOrEmpty(Txt_Quan.Text))
            {
                dataGridView1.Rows[r].Cells[10].Value = DBNull.Value;
            }
            else
            {
                dataGridView1.Rows[r].Cells[10].Value = Convert.ToDouble(Txt_Quan.Text);
            }

            dataGridView1.Rows[r].Cells[11].Value = DBNull.Value;

            dataGridView1.Rows[r].Cells[0].Value = TXT_EznNo.Text;
            dataGridView1.Rows[r].Cells[1].Value = Cmb_FYear.Text;
            dataGridView1.Rows[r].Cells[2].Value = r + 1;
            dataGridView1.DataSource = table;
            #endregion

            //dataGridView1.Rows[r + 1].Cells[4].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[5].Value = DBNull.Value;
            ////  dataGridView1.Rows[r].Cells[3].Value = TXT_StockBian.Text;
            //dataGridView1.Rows[r + 1].Cells[6].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[7].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[8].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[9].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[10].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[11].Value = DBNull.Value;

            //dataGridView1.Rows[r + 1].Cells[0].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[1].Value = DBNull.Value;

            //dataGridView1.Rows[r + 1].Cells[2].Value = DBNull.Value;
            //dataGridView1.Rows[r + 1].Cells[3].Value = DBNull.Value;

        }

        private void HandleDataGridViewStyle()
        {

            if (Constants.User_Type == "A")
            {
                //dataGridView1.Columns["Quan2"].ReadOnly = true;
                //dataGridView1.Columns["TotalPrice"].ReadOnly = true;
            }
            else if (Constants.User_Type == "B")
            {
                if (Constants.UserTypeB == "Sarf")
                {
                    dataGridView1.Columns["Quan2"].DefaultCellStyle.BackColor = Color.Salmon;
                }
                else
                {
                    //dataGridView1.Columns["Quan2"].ReadOnly = false;
                }

                if (Constants.UserTypeB == "Finance")
                {
                    dataGridView1.Columns["TotalPrice"].DefaultCellStyle.BackColor = Color.Salmon;
                }
                else
                {
                    //dataGridView1.Columns["TotalPrice"].ReadOnly = true;
                }
            }

        }


        private void GetEznBnod(string eznNo, string fyear, string momayz)
        {
            table.Clear();

            string TableQuery = @"SELECT  [EznSarf_No],[FYear] ,[Bnd_No] ,[Quan1],[Quan2],[Unit1],[Unit2],
                                [BIAN_TSNIF],[Stock_No],[STOCK_NO_ALL],[AvailableQuan],[TotalPrice]FROM [T_EznSarf_Benod] 
                                Where EznSarf_No = " + eznNo + " and Fyear='" + fyear + "'and TR_NO='" + momayz + "'";

            dataadapter = new SqlDataAdapter(TableQuery, Constants.con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataadapter.Fill(table);
            dataGridView1.DataSource = table;

            dataGridView1.Columns["EznSarf_No"].HeaderText = "رقم اذن الصرف";//col0;
            dataGridView1.Columns["FYear"].HeaderText = "السنة المالية";//col1
            dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col2
            dataGridView1.Columns["Quan1"].HeaderText = "المطلوب";//col3
            dataGridView1.Columns["Quan2"].HeaderText = "المنصرف";//col4           
            dataGridView1.Columns["Unit1"].HeaderText = "الوحدة";//col5
            dataGridView1.Columns["Unit2"].HeaderText = "الوحدة";//col6

            dataGridView1.Columns["BIAN_TSNIF"].HeaderText = "البيان";//col7
            dataGridView1.Columns["Stock_No"].HeaderText = "رقم المخزن";//col8
            dataGridView1.Columns["STOCK_NO_ALL"].HeaderText = "رقم التصنيف";//col9

            dataGridView1.Columns["AvailableQuan"].HeaderText = "رصيد المخزن";//col10
            //dataGridView1.Columns["PricePerUnit"].HeaderText = "سعر الوحدة";
            dataGridView1.Columns["TotalPrice"].HeaderText = "القيمة";//col11

            HandleDataGridViewStyle();
        }

        public bool SearchEznSarf(string eznNo, string fyear, string momayz)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();


            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring;
            SqlCommand cmd;

            cmdstring = "select * from T_EznSarf where EznSarf_No=@TN and FYear=@FY and TR_NO=@TRNO";

            cmd = new SqlCommand(cmdstring, Constants.con);
            cmd.Parameters.AddWithValue("@TN", eznNo);
            cmd.Parameters.AddWithValue("@FY", fyear);
            cmd.Parameters.AddWithValue("@TRNO", momayz);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                try
                {
                    while (dr.Read())
                    {
                        TXT_Edara.Text = dr["NameEdara"].ToString();
                        TXT_CodeEdara.Text = dr["CodeEdara"].ToString();
                        TXT_Date.Text = dr["Date"].ToString();
                        TXT_EznNo.Text = dr["EznSarf_No"].ToString();
                        TXT_TRNO.Text = dr["TR_NO"].ToString();

                        if (!(string.IsNullOrEmpty(TXT_TRNO.Text) || string.IsNullOrWhiteSpace(TXT_TRNO.Text)))
                        {
                            Cmb_CType.SelectedValue = TXT_TRNO.Text.ToString();
                        }

                        TXT_RequestedFor.Text = dr["RequestedFor"].ToString();
                        TXT_ProcessNo.Text = dr["TR_NO"].ToString();
                        TXT_RespCentre.Text = dr["Responsiblecenter"].ToString();
                        TXT_AccNo.Text = dr["Acc_No"].ToString();
                        TXT_PaccNo.Text = dr["Pacc_No"].ToString();
                        TXT_MTaklif.Text = dr["MTakalif"].ToString();
                        TXT_MResp.Text = dr["MResponsible"].ToString();
                        TXT_Masrof.Text = dr["Masrof"].ToString();
                        TXT_Enfak.Text = dr["Enfak"].ToString();
                        TXT_Morakba.Text = dr["Morakba"].ToString();
                        TXT_Total.Text = dr["Total"].ToString();

                        string s1 = dr["Sign1"].ToString();
                        string s2 = dr["Sign2"].ToString();
                        string s3 = dr["Sign3"].ToString();
                        string s4 = dr["Sign4"].ToString();
                        string s5 = dr["Sign5"].ToString();
                        Cmb_FYear.Text = dr["FYear"].ToString();

                        if (s1 != "")
                        {
                            string p = Constants.RetrieveSignature("1", "2", s1);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename1 = p.Split(':')[1];
                                wazifa1 = p.Split(':')[2];
                                pp = p.Split(':')[0];

                                ((PictureBox)this.bottomPanel.Controls["signatureTable"].Controls["panel13"].Controls["Pic_Sign" + "1"]).Image = Image.FromFile(@pp);

                                FlagSign1 = 1;
                                FlagEmpn1 = s1;
                                ((PictureBox)this.bottomPanel.Controls["signatureTable"].Controls["panel13"].Controls["Pic_Sign" + "1"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign1, Ename1 + Environment.NewLine + wazifa1);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.bottomPanel.Controls["signatureTable"].Controls["panel13"].Controls["Pic_Sign" + "1"]).BackColor = Color.Red;
                        }
                        if (s2 != "")
                        {
                            string p = Constants.RetrieveSignature("2", "2", s2);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename2 = p.Split(':')[1];
                                wazifa2 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.bottomPanel.Controls["signatureTable"].Controls["panel14"].Controls["Pic_Sign" + "2"]).Image = Image.FromFile(@pp);
                                FlagSign2 = 1;
                                FlagEmpn2 = s2;
                                ((PictureBox)this.bottomPanel.Controls["signatureTable"].Controls["panel14"].Controls["Pic_Sign" + "2"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign2, Ename2 + Environment.NewLine + wazifa2);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.bottomPanel.Controls["signatureTable"].Controls["panel14"].Controls["Pic_Sign" + "2"]).BackColor = Color.Red;
                        }
                        if (s3 != "")
                        {
                            string p = Constants.RetrieveSignature("3", "2", s3);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename3 = p.Split(':')[1];
                                wazifa3 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.bottomPanel.Controls["signatureTable"].Controls["panel16"].Controls["Pic_Sign" + "3"]).Image = Image.FromFile(@pp);
                                FlagSign3 = 1;
                                FlagEmpn3 = s3;
                                ((PictureBox)this.bottomPanel.Controls["signatureTable"].Controls["panel16"].Controls["Pic_Sign" + "3"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign3, Ename3 + Environment.NewLine + wazifa3);


                            }

                        }
                        else
                        {
                            ((PictureBox)this.bottomPanel.Controls["signatureTable"].Controls["panel16"].Controls["Pic_Sign" + "3"]).BackColor = Color.Red;
                        }
                        if (s4 != "")
                        {
                            string p = Constants.RetrieveSignature("4", "2", s4);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename3 = p.Split(':')[1];
                                wazifa3 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                                ((PictureBox)this.bottomPanel.Controls["signatureTable"].Controls["panel15"].Controls["Pic_Sign" + "4"]).Image = Image.FromFile(@pp);
                                FlagSign4 = 1;
                                FlagEmpn4 = s4;
                                ((PictureBox)this.bottomPanel.Controls["signatureTable"].Controls["panel15"].Controls["Pic_Sign" + "4"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign4, Ename4 + Environment.NewLine + wazifa4);



                                ////
                            }

                        }
                        else
                        {
                            ((PictureBox)this.bottomPanel.Controls["signatureTable"].Controls["panel15"].Controls["Pic_Sign" + "4"]).BackColor = Color.Red;
                        }
                        if (s5 != "")
                        {
                            string p = Constants.RetrieveSignature("4", "2", s5);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename5 = p.Split(':')[1];
                                wazifa5 = p.Split(':')[2];
                                pp = p.Split(':')[0];
                               // ((PictureBox)this.bottomPanel.Controls["Pic_Sign" + "5"]).Image = Image.FromFile(@pp);
                                FlagSign5 = 1;
                                FlagEmpn5 = s5;
                                //((PictureBox)this.bottomPanel.Controls["Pic_Sign" + "5"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign5, Ename5 + Environment.NewLine + wazifa5);

                            }

                        }
                        else
                        {
                           // ((PictureBox)this.bottomPanel.Controls["Pic_Sign" + "5"]).BackColor = Color.Red;
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
                MessageBox.Show("من فضلك تاكد من رقم اذن الصرف");
                reset();
                return false;

            }
            dr.Close();

            GetEznBnod(eznNo, fyear, momayz);

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
            browseBTN.Enabled = true;
            BTN_PDF.Enabled = true;

            Addbtn.Enabled = false;
            Editbtn2.Enabled = false;
            BTN_SearchEzn.Enabled = false;
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
            DisableControls();
            BTN_Save2.Enabled = true;

            if (Constants.User_Type == "A")
            {
                if (FlagSign2 != 1 && FlagSign1 == 1)
                {
                    BTN_Sign2.Enabled = true;
                    DeleteBtn.Enabled = true;
                    currentSignNumber = 2;
                }
                else if(FlagSign4 != 1 && FlagSign3 == 1)
                {
                    BTN_Sign4.Enabled = true;
                    currentSignNumber = 4;
                }
            }
            else if (Constants.User_Type == "B")
            {
                if (Constants.UserTypeB == "Sarf")
                {
                    BTN_Sign3.Enabled = true;
                    //dataGridView1.ReadOnly = false;
                    dataGridView1.Columns["Quan2"].ReadOnly = false;
                    currentSignNumber = 3;
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

            if (Constants.isConfirmForm)
            {
                Cmb_FYear.Enabled = true;
                Cmb_CType.Enabled = true;
                TXT_EznNo.Enabled = true;
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
            changePanelState(panel5, false);

            //dataViewre sec
            changePanelState(panel6, false);

            //fyear sec
            changePanelState(panel8, false);

            //bian edara sec
            changePanelState(panel9, false);

            //arabic value
            changePanelState(panel11, false);

            //btn Section
            //generalBtn
            Addbtn.Enabled = true;
            BTN_SearchEzn.Enabled = true;
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

            changeDataGridViewColumnState(dataGridView1,true);
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

        //------------------------------------------ Logic Handler ---------------------------------
        #region Logic Handler
        private void AddLogic()
        {
            Constants.opencon();

            string cmdstring = "Exec SP_InsertEznSarf @TNO,@FY,@CE,@NE,@CD,@MO,@RF,@RC,@TR,@ACC,@PACC,@MT,@MR,@MA,@EN,@MK,@S1,@S2,@S3,@S4,@S5,@LU,@LD,@TT,@aot output";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_EznNo.Text));
            cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
            cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);
            cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
            cmd.Parameters.AddWithValue("@MO", TXT_TRNO.Text.ToString());
            cmd.Parameters.AddWithValue("@RF", TXT_RequestedFor.Text.ToString());
            cmd.Parameters.AddWithValue("@RC", TXT_RespCentre.Text.ToString());
            cmd.Parameters.AddWithValue("@TR", TXT_TRNO.Text.ToString());
            cmd.Parameters.AddWithValue("@ACC", TXT_AccNo.Text.ToString());
            cmd.Parameters.AddWithValue("@PACC", TXT_PaccNo.Text.ToString());
            cmd.Parameters.AddWithValue("@MT", TXT_MTaklif.Text.ToString());
            cmd.Parameters.AddWithValue("@MR", TXT_MResp.Text.ToString());
            cmd.Parameters.AddWithValue("@MA", TXT_Masrof.Text.ToString());
            cmd.Parameters.AddWithValue("@EN", TXT_Enfak.Text.ToString());
            cmd.Parameters.AddWithValue("@MK", TXT_Morakba.Text.ToString());
            cmd.Parameters.AddWithValue("@S1", FlagEmpn1);
            cmd.Parameters.AddWithValue("@S2", DBNull.Value);
            cmd.Parameters.AddWithValue("@S3", DBNull.Value);
            cmd.Parameters.AddWithValue("@S4", DBNull.Value);
            cmd.Parameters.AddWithValue("@S5", DBNull.Value);
            cmd.Parameters.AddWithValue("@LU", Constants.User_Name.ToString());
            cmd.Parameters.AddWithValue("@LD", Convert.ToDateTime(DateTime.Now.ToShortDateString()));

            if (TXT_Total.Text.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@TT", DBNull.Value);

            }
            else
            {
                cmd.Parameters.AddWithValue("@TT", Convert.ToDecimal(TXT_Total.Text));

            }

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
                Console.WriteLine(sqlEx);                
            }

            flag = (int)cmd.Parameters["@aot"].Value;

            if (executemsg == true && flag == 1)
            {
                InsertEznSarfBnood();

                for (int i = 1; i <= 4; i++)
                {
                    cmdstring = "Exec  SP_InsertSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
                    cmd = new SqlCommand(cmdstring, Constants.con);
                    cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_EznNo.Text));
                    cmd.Parameters.AddWithValue("@TNO2", Convert.ToInt32(TXT_TRNO.Text));
                    cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
                    cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
                    cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
                    cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);
                    cmd.Parameters.AddWithValue("@FN", 2);
                    cmd.Parameters.AddWithValue("@SN", i);
                    cmd.Parameters.AddWithValue("@D1", DBNull.Value);
                    cmd.Parameters.AddWithValue("@D2", DBNull.Value);
                    cmd.ExecuteNonQuery();
                }

                SP_UpdateSignatures(1, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                //////////////////////////////////////////////////////////////////
                //if (MaxFlag > 0)
                //{
                //    for (int i = 0; i < MaxFlag; i++)
                //    {
                //        string query = "exec SP_InsertTMinQuan @p1,@p2,@p3,@p4,@p5,@p6,@p7";
                //        SqlCommand cmd1 = new SqlCommand(query, Constants.con);
                //        cmd1.Parameters.AddWithValue("@p1", array1[i, 0]);
                //        cmd1.Parameters.AddWithValue("@p2", array1[i, 1]);
                //        cmd1.Parameters.AddWithValue("@p3", array1[i, 2]);
                //        cmd1.Parameters.AddWithValue("@p4", array1[i, 3]);
                //        cmd1.Parameters.AddWithValue("@p5", array1[i, 4]);
                //        cmd1.Parameters.AddWithValue("@p6", array1[i, 5]);
                //        cmd1.Parameters.AddWithValue("@p7", DBNull.Value);
                //        cmd1.ExecuteNonQuery();
                //    }
                //}

                MessageBox.Show("تم الإضافة بنجاح  ! ");
                reset();
            }
            else if (executemsg == true && flag == 2)
            {
                MessageBox.Show("تم إدخال رقم اذن الصرف  من قبل  ! ");
            }
            else if (executemsg == false)
            {
                MessageBox.Show("لم يتم إدخال إذن الصرف بنجاج!!");
            }

            Constants.closecon();
        }

        private void UpdateEznSarfTSignatureCycle()
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

        public void UpdateEznSarf()
        {
            Constants.opencon();

            string cmdstring1 = "select STOCK_NO_ALL,quan1,quan2 from T_EznSarf_Benod where FYear=@FY and EznSarf_No=@TNO";
            SqlCommand cmd1 = new SqlCommand(cmdstring1, Constants.con);
            cmd1.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_EznNo.Text));
            cmd1.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            SqlDataReader dr = cmd1.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    if (!(string.IsNullOrWhiteSpace(dr["quan1"].ToString()) || string.IsNullOrEmpty(dr["quan1"].ToString()))) 
                    { 
                        string cmdstring2 = "Exec SP_UndoVirtualQuan2 @TNO,@QUAN";
                        SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);

                        cmd2.Parameters.AddWithValue("@TNO", (dr["STOCK_NO_ALL"].ToString()));
                        if (dr["quan2"].ToString() == "")
                        {

                            cmd2.Parameters.AddWithValue("@QUAN", Convert.ToDouble(dr["quan1"].ToString()));
                        }
                        else
                        {
                            cmd2.Parameters.AddWithValue("@QUAN", Convert.ToDouble(dr["quan2"].ToString()));
                        }

                        cmd2.ExecuteNonQuery();
                    }

                }
            }
            dr.Close();

            /////////////////////////////////////////
            string cmdstring = "Exec SP_UpdateEznSarf @TNOold,@FYold, @TNO,@FY,@CE,@NE,@CD,@MO,@RF,@RC,@TR,@ACC,@PACC,@MT,@MR,@MA,@EN,@MK,@S1,@S2,@S3,@S4,@S5,@LU,@LD,@TT,@aot output";


            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
            cmd.Parameters.AddWithValue("@TNOold", TNO);
            cmd.Parameters.AddWithValue("@FYold", FY);
            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_EznNo.Text));
            cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            cmd.Parameters.AddWithValue("@CE", TXT_CodeEdara.Text);
            cmd.Parameters.AddWithValue("@NE", TXT_Edara.Text);
            cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
            cmd.Parameters.AddWithValue("@MO", TXT_TRNO.Text.ToString());
            cmd.Parameters.AddWithValue("@RF", TXT_RequestedFor.Text.ToString());
            cmd.Parameters.AddWithValue("@RC", TXT_RespCentre.Text.ToString());
            cmd.Parameters.AddWithValue("@TR", TXT_TRNO.Text.ToString());
            cmd.Parameters.AddWithValue("@ACC", TXT_AccNo.Text.ToString());
            cmd.Parameters.AddWithValue("@PACC", TXT_PaccNo.Text.ToString());
            cmd.Parameters.AddWithValue("@MT", TXT_MTaklif.Text.ToString());
            cmd.Parameters.AddWithValue("@MR", TXT_MResp.Text.ToString());
            cmd.Parameters.AddWithValue("@MA", TXT_Masrof.Text.ToString());
            cmd.Parameters.AddWithValue("@EN", TXT_Enfak.Text.ToString());
            cmd.Parameters.AddWithValue("@MK", TXT_Morakba.Text.ToString());

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

            cmd.Parameters.AddWithValue("@LU", Constants.User_Name.ToString());
            cmd.Parameters.AddWithValue("@LD", Convert.ToDateTime(DateTime.Now.ToShortDateString()));

            if (TXT_Total.Text.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@TT", DBNull.Value);

            }
            else
            {
                cmd.Parameters.AddWithValue("@TT", Convert.ToDecimal(TXT_Total.Text));

            }

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
                InsertEznSarfBnood();
                UpdateEznSarfTSignatureCycle();

                if (FlagSign3 == 1)
                {
                    UpdateQuan();
                    InsertTrans();
                }

                MessageBox.Show("تم التعديل بنجاح  ! ");

                reset();
            }
            else if (executemsg == true && flag == 2)
            {
                MessageBox.Show("إذن الصرف المراد تعديله غير موجود !!");
            }

            Constants.closecon();
        }

        private void EditLogic()
        {
            UpdateEznSarf();
        }

        public bool DeleteEznSarf()
        {
            if ((MessageBox.Show("هل تريد حذف اذن الصرف ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(TXT_EznNo.Text))

                {
                    MessageBox.Show("يجب اختياراذن الصرف   اولا");
                    return false;
                }

                Constants.opencon();

                string cmdstring1 = "select STOCK_NO_ALL,quan1,quan2 from T_EznSarf_Benod where FYear=@FY and EznSarf_No=@TNO";
                SqlCommand cmd1 = new SqlCommand(cmdstring1, Constants.con);


                cmd1.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_EznNo.Text));
                cmd1.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
                SqlDataReader dr = cmd1.ExecuteReader();

                //---------------------------------
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        if (!(string.IsNullOrWhiteSpace(dr["quan1"].ToString()) || string.IsNullOrEmpty(dr["quan1"].ToString())))
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

                string cmdstring = "Exec SP_deleteEznSarf @TNO,@FY,@TRNO,@aot output";

                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_EznNo.Text));
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
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
        private List<(ErrorProvider, Control, string)> ValidateAddTasnif()
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

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
                        if (row.Cells[9].Value.ToString().ToLower() == TXT_StockNoAll.Text.ToLower() && TXT_StockNoAll.Text != "")
                        {
                            errorsList.Add((alertProvider, TXT_StockNoAll, "تم ادخال رقم هذا التصنيف من قبل"));

                            break;
                        }
                    }
                }
            #endregion

            #region Txt_ReqQuan
            if (string.IsNullOrWhiteSpace(Txt_ReqQuan.Text))
            {
                errorsList.Add((errorProvider, Txt_ReqQuan, "يجب ادخال الكمية المطلوبة"));
            }
            else if (!string.IsNullOrWhiteSpace(Txt_ReqQuan.Text) && Convert.ToDecimal(Txt_ReqQuan.Text) <= 0)
            {
                errorsList.Add((alertProvider, Txt_ReqQuan, "يجب ان تكون الكمية المطلوبة اكبر من صفر"));
            }
            else if (!string.IsNullOrWhiteSpace(Txt_Quan.Text) && Txt_Quan.Text != "" && Convert.ToDouble(Txt_Quan.Text) >= 0 && Convert.ToDouble(Txt_ReqQuan.Text) > Convert.ToDouble(Txt_Quan.Text))
            {
                errorsList.Add((alertProvider, Txt_ReqQuan, "الكمية المطلوبة اكبر من المتاحة فى المخزن"));
            }
            #endregion

            #region Cmb_FYear
            if (string.IsNullOrWhiteSpace(Cmb_FYear.Text) || Cmb_FYear.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_FYear, "تاكد من  اختيار السنة المالية"));
            }
            #endregion

            #region Cmb_CType
            if (string.IsNullOrWhiteSpace(Cmb_CType.Text) || Cmb_CType.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_CType, "تاكد من  اختيار نوع إذن الصرف"));
            }
            #endregion

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

            #region Cmb_CType
                if (string.IsNullOrWhiteSpace(Cmb_CType.Text) || Cmb_CType.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_CType, "تاكد من  اختيار نوع إذن الصرف"));
                }
            #endregion

            #region TXT_EznNo
                if (string.IsNullOrWhiteSpace(TXT_EznNo.Text))
                {
                    errorsList.Add((errorProvider, TXT_EznNo, "يجب اختيار رقم إذن الصرف"));
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

                #region Cmb_EznNo2
                if (string.IsNullOrWhiteSpace(Cmb_EznNo2.Text) || Cmb_EznNo2.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_EznNo2, "يجب اختيار رقم إذن الصرف"));
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

                #region Cmb_FYear
                if (string.IsNullOrWhiteSpace(Cmb_FYear.Text) || Cmb_FYear.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_FYear, "تاكد من  اختيار السنة المالية"));
                }
                #endregion

                #region TXT_EznNo
                if (string.IsNullOrWhiteSpace(TXT_EznNo.Text))
                {
                    errorsList.Add((errorProvider, TXT_EznNo, "يجب اختيار رقم إذن الصرف"));
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

            #region Cmb_CType
            if (string.IsNullOrWhiteSpace(Cmb_CType.Text) || Cmb_CType.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_CType, "تاكد من  اختيار نوع إذن الصرف"));
            }
            #endregion

            #region TXT_EznNo
            if (string.IsNullOrWhiteSpace(TXT_EznNo.Text))
            {
                errorsList.Add((errorProvider, TXT_EznNo, "يجب اختيار رقم إذن الصرف"));
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

            if (type == VALIDATION_TYPES.ADD_TASNIF)
            {
                errorsList = ValidateAddTasnif();
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
            // TODO: This line of code loads data into the 'aNRPC_InventoryDataSet.T_BnodAwamershraa' table. You can move, or remove it, as needed.
            // this.t_BnodAwamershraaTableAdapter.Fill(this.aNRPC_InventoryDataSet.T_BnodAwamershraa);

            alertProvider.Icon = SystemIcons.Warning;
            HelperClass.comboBoxFiller(Cmb_FYear, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
            HelperClass.comboBoxFiller(Cmb_FYear2, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);


            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Syria));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Tunisia));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Gold));
            MaxFlag = 0;

            AddEditFlag = 0;

            if (Constants.isConfirmForm)
            {
                panel7.Visible = true;
                eznSarfDataPanel.Visible = false;
                panel7.Dock = DockStyle.Top;
            }
            else
            {
                eznSarfDataPanel.Visible = true;
                panel7.Visible = false;
                eznSarfDataPanel.Dock = DockStyle.Top;
            }
            //    if (Constants.User_Type != "A")
            //  {
            // DisableControls();
            // }
            //------------------------------------------

            Constants.opencon();
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            //*******************************************s
            // ******    AUTO COMPLETE
            //*******************************************
            string cmdstring = "select STOCK_NO_ALL,Stock_NO_Nam,PartNO,BIAN_TSNIF from T_Tsnif  where (StatusFlag in (0,1,2)) and   CodeEdara=" + Constants.CodeEdara;
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
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
            string cmdstring3 = "SELECT [EznSarf_No] from T_EznSarf where CodeEdara=" + Constants.CodeEdara + " and  FYear='" + Cmb_FYear.Text + "'";
            SqlCommand cmd3 = new SqlCommand(cmdstring3, Constants.con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            //---------------------------------
            if (dr3.HasRows == true)
            {
                while (dr3.Read())
                {
                    EznColl.Add(dr3["EznSarf_No"].ToString());

                }
            }
            dr3.Close();
            ///////////////////////////////////////////////////////
            Constants.opencon();
            Cmb_CType.SelectedIndexChanged -= new EventHandler(Cmb_CType_SelectedIndexChanged);
            Cmb_CType2.SelectedIndexChanged -= new EventHandler(Cmb_CType2_SelectedIndexChanged);
            cmdstring = "SELECT  [CCode],[CName] FROM [T_TransferTypes] where CType=2 and CFlag=1";//will use cmdstring3


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
            //   TXT_Momayz.Text = Cmb_CType.SelectedValue.ToString();



            ///////////////
            TXT_StockNoAll.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_StockNoAll.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_StockNoAll.AutoCompleteCustomSource = TasnifColl;

            TXT_PartNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_PartNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_PartNo.AutoCompleteCustomSource = PartColl;


            TXT_StockName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_StockName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_StockName.AutoCompleteCustomSource = TasnifNameColl;

            TXT_EznNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_EznNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_EznNo.AutoCompleteCustomSource = EznColl;

            Constants.closecon();

            reset();
        }
        public EznSarf_F()
        {
            InitializeComponent();
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            init();
        }
        //===========================================================================

        public void SearchTasnif(int searchflag)
        {

            string query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where STOCK_NO_ALL= @a";

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

                query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where PartNO= @a";
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
                    if (dr["SafeAmount"] == DBNull.Value  || dr["SafeAmount"].ToString() =="0")
                    {
                        checkBox1.Checked = false;
                    }
                    else if(dr["SafeAmount"].ToString() =="1")
                    {


                        checkBox1.Checked = true;
                    }


                    if (dr["StrategeAmount"] == DBNull.Value ||  dr["StrategeAmount"] .ToString()=="0" )
                    {
                        checkBox2.Checked= false;
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

                    Txt_Quan.Text = dr["Quan"].ToString();

                }

                pictureBox2.Image = null;
                Image1 = "";
                Image2 = "";
                picflag = 0;

               //SearchImage1(TXT_StockNoAll.Text);
               //SearchImage2(TXT_StockNoAll.Text);
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
                SearchTasnif(2);
            }
        }

        private void TXT_StockNoAll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {
                Constants.opencon();

                SearchTasnif(1);
            }
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد اضافة اذن صرف جديد؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                reset();
                PrepareAddState();

                AddEditFlag = 2;
                TXT_Edara.Text = Constants.NameEdara;
            }       
        }


        private void Addbtn2_Click(object sender, EventArgs e)
        {

            if (!IsValidCase(VALIDATION_TYPES.ADD_TASNIF))
            {
                return;
            }

            string stocknoall = TXT_StockNoAll.Text;

            if (checkBox1.Checked == true || checkBox2.Checked == true)
            {
                if ((Convert.ToDouble(Txt_Quan.Text)) - (Convert.ToDouble(Txt_ReqQuan.Text)) < Convert.ToDouble(Quan_Min.Value))
                {
                    MessageBox.Show("بعد صرف الكمية المطلوبة الكمية المتاحة ستكون اقل من الحد الادنى ");
                    MaxFlag = MaxFlag + 1;

                    //  return;
                    array1[MaxFlag - 1, 3] = TXT_StockNoAll.Text;
                    array1[MaxFlag - 1, 0] = TXT_EznNo.Text;
                    array1[MaxFlag - 1, 1] = TXT_EznNo.Text;

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
               
                TXT_EznNo.AutoCompleteMode = AutoCompleteMode.None;
                TXT_EznNo.AutoCompleteSource = AutoCompleteSource.None; ;

                string cmdstring3 = "";
                if (Constants.User_Type == "A")
                {
                    cmdstring3 = "SELECT [EznSarf_No] from T_EznSarf where CodeEdara=" + Constants.CodeEdara + " and  FYear='" + Cmb_FYear.Text + "'";
                }
                else
                {
                    cmdstring3 = "SELECT [EznSarf_No] from T_EznSarf where  FYear='" + Cmb_FYear.Text + "'";
                }

                SqlCommand cmd3 = new SqlCommand(cmdstring3, Constants.con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                //---------------------------------
                if (dr3.HasRows == true)
                {
                    while (dr3.Read())
                    {
                        EznColl.Add(dr3["EznSarf_No"].ToString());

                    }
                }
              
                TXT_EznNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                TXT_EznNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                TXT_EznNo.AutoCompleteCustomSource = EznColl;
                Constants.closecon();

            }
            //go and get talbTawreed_no for this FYear
            if (AddEditFlag == 2)//add
            {

                if (TXT_EznNo.Text != "")
                {
                    return;
                }

                Constants.opencon();
                string cmdstring = "select ( COALESCE(MAX(EznSarf_No), 0)) from  T_EznSarf where FYear=@FY and TR_NO=@TRNO ";
                
                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
                
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
                cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text.ToString());
                int flag;

                try
                {
                    Constants.opencon();

                    var count = cmd.ExecuteScalar();
                    executemsg = true;

                    if (count != null && count != DBNull.Value)
                    {
                        //  flag = (int)cmd.Parameters["@Num"].Value;

                        flag = (int)count;
                        flag = flag + 1;
                        /////////////////////////done by nouran//////////////////////

                        string cmdstring2 = "select ( COALESCE(MAX(EznSarf_No), 0)) from  T_TempSarfNo where FYear=@FY and TRNO=@TRNO ";

                        SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);

                        // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
                        cmd2.Parameters.AddWithValue("@FY", Cmb_FYear.Text);
                        cmd2.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text);
                        //cmd2.Parameters.AddWithValue("@T",flag);
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
                        string query = "exec SP_InsertTempSarfNo @p1,@p2,@p3";
                        SqlCommand cmd1 = new SqlCommand(query, Constants.con);
                        cmd1.Parameters.AddWithValue("@p1", flag);
                        cmd1.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd1.Parameters.AddWithValue("@p3", TXT_TRNO.Text);

                        cmd1.ExecuteNonQuery();

                        TXT_EznNo.Text = flag.ToString();//el rakm el new
                        if (AddEditFlag == 2)
                        {
                            if (!(string.IsNullOrWhiteSpace(TXT_TRNO.Text) || string.IsNullOrEmpty(TXT_TRNO.Text)))
                            {
                                GetEznBnod(TXT_EznNo.Text, Cmb_FYear.Text, TXT_TRNO.Text);
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
            if (!IsValidCase(VALIDATION_TYPES.SAVE))
            {
                return;
            }

            if (AddEditFlag == 2)
            {
                if (FlagSign1 != 1)
                {
                    MessageBox.Show("من فضلك تاكد من توقيع اذن الصرف");
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
              if ((MessageBox.Show("هل تريد تعديل اذن الصرف ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
              {
                    if (string.IsNullOrEmpty(TXT_EznNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text)|| string.IsNullOrEmpty(TXT_TRNO.Text))
                    {
                        MessageBox.Show("يجب اختيار نوع اذن الصرف و رقم اذن الصرف المراد تعديله و السنة المالية");
                        return;
                    }

                    PrepareConfirmState();
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
                cmdstring = "select (EznSarf_No) from  T_EznSarf where FYear=@FY and CodeEdara=@CE and TR_NO=@TRNO and ( Sign1 is not null ) and (Sign2 is null or (Sign3 is not null and Sign4 is null)) ";

            }
            else if (Constants.User_Type == "B")
            {
                if(Constants.UserTypeB == "Sarf")
                {
                    cmdstring = "select (EznSarf_No) from  T_EznSarf where FYear=@FY and ( Sign1 is not null and Sign2 is not null)  and(Sign3 is null) and TR_NO=@TRNO  ";
                }
                else if (Constants.UserTypeB == "Tkalif" || Constants.UserTypeB == "Finance")
                {
                    cmdstring = "select (EznSarf_No) from  T_EznSarf where FYear=@FY and (Sign4 is not null) and TR_NO=@TRNO  ";
                }
            }


            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            // cmdstring = "select (EznSarf_No) from  T_EznSarf where FYear=@FY and CodeEdara=@CE ";


            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

              // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
              cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
              cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
              cmd.Parameters.AddWithValue("@TRNO",TXT_TRNO2.Text);

            DataTable dts = new DataTable();
              
              dts.Load(cmd.ExecuteReader());
              Cmb_EznNo2.DataSource = dts;
              Cmb_EznNo2.ValueMember = "EznSarf_No";
              Cmb_EznNo2.DisplayMember = "EznSarf_No";
              Cmb_EznNo2.SelectedIndex = -1;
              Cmb_EznNo2.SelectedIndexChanged += new EventHandler(Cmb_TalbNo2_SelectedIndexChanged);
              Constants.closecon();
            
          }

        private void Cmb_TalbNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_EznNo2.SelectedIndex != -1)
            {
                //SearchTalb(2);
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
            Cmb_EznNo2.SelectedIndex = -1;
            Cmb_FYear2.SelectedIndex = -1;

            TXT_EznNo.Enabled = false;
            Cmb_FYear.Enabled = false;
            Cmb_CType.Enabled = false;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            decimal sum = 0;
        
            if (e.ColumnIndex == 11)
            {
                if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[11].Value != null  && dataGridView1.Rows[e.RowIndex].Cells[11].Value != DBNull.Value)
                {

                    sum = sum + Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
                    TXT_Total.Text = sum.ToString();
                }
            }
            if (e.ColumnIndex == 4) //if second cell
            {
                        if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[9].Value!=null)
                {

                    Constants.opencon();
                    string x = "select quan from T_Tsnif where STOCK_NO_ALL=@st";
                    SqlCommand cmd = new SqlCommand(x, Constants.con);
                    cmd.Parameters.AddWithValue("@st", dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString());//stock_no_all
                    var scalar = cmd.ExecuteScalar();
                    if (scalar != DBNull.Value && scalar != null && dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString()!="") // Case where the DB value is null
                    {
                        string g = scalar.ToString();
                        double availablerased = Convert.ToDouble(g);
                        double newrased;
                        double quan = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                        string xx = "select Quan2 from T_EznSarf_Benod where EznSarf_No=@x and FYear=@Y and Bnd_No=@Z";
                        SqlCommand cmd2 = new SqlCommand(xx, Constants.con);
                      
                        
                        cmd2.Parameters.AddWithValue("@X", dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());//stock_no_all
                        cmd2.Parameters.AddWithValue("@Y", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());//stock_no_all
                        cmd2.Parameters.AddWithValue("@Z", dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());//stock_no_all
                       
                        var scalar2 = cmd2.ExecuteScalar();
                        if (scalar2 != DBNull.Value && scalar2 != null)
                        {


                            oldvalue = Convert.ToDouble(scalar2.ToString());
                            newrased = availablerased + oldvalue - quan;
                            dataGridView1.Rows[e.RowIndex].Cells[10].Value = newrased;
                            executemsg = true;
                        }
                        else
                        {
                            oldvalue = 0;
                            newrased = availablerased + oldvalue - quan;
                            dataGridView1.Rows[e.RowIndex].Cells[10].Value = newrased;
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
                    Constants.Quan= dataGridView1.CurrentRow.Cells[2].Value.ToString();
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

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 3 || dataGridView1.CurrentCell.ColumnIndex == 4 || dataGridView1.CurrentCell.ColumnIndex == 10 || dataGridView1.CurrentCell.ColumnIndex == 11)//reqQuan
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
            if (string.IsNullOrEmpty(TXT_EznNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text))
            {
                MessageBox.Show("يجب اختيار اذن الصرف المراد طباعتها اولا");
                return;
            }
            else
            {

                Constants.FormNo = 7;
                Constants.EznNo = Convert.ToInt32(TXT_EznNo.Text);
                Constants.EznFY = Cmb_FYear.Text;
                FReports F = new FReports();
                F.Show();
            }
        }

        private void TXT_PartNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {
                Constants.opencon();
                SearchTasnif(3);
            }
        }

        private void TXT_Total_TextChanged(object sender, EventArgs e)
        {
            if(TXT_Total.Text != "")
            {
                ToWord toWord = new ToWord(Convert.ToDecimal(TXT_Total.Text), currencies[0]);
                TXT_ArabicValue.Text = toWord.ConvertToArabic();
            }

        }

        private void Cmb_CType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(Cmb_CType.Text)||string.IsNullOrWhiteSpace(Cmb_CType.Text)||Cmb_CType.SelectedIndex == -1))
            {
                TXT_TRNO.Text = Cmb_CType.SelectedValue.ToString();
            }
        }




        //------------------------------------------ Signature Handler ---------------------------------
        #region Signature Handler
        private void BTN_Sign1_Click(object sender, EventArgs e)
        {


            Empn1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على انشاء اذن صرف", "");

            Sign1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على انشاء اذن صرف", "");
            if (Sign1 != "" && Empn1 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("1", "2", Sign1, Empn1);
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
            if (FlagSign1 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            Empn2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على اعتماد اذن صرف", "");

            Sign2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على اعتماد اذن صرف", "");

            if (Sign2 != "" && Empn2 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("2", "2", Sign2, Empn2);
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
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            string Empn2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "اعتماد المدير العام", "");
            string Sign2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "اعتماد المدير العام", "");

            if (Sign2 != "" && Empn2 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("2", "2", Sign2, Empn2);
                if (result.Item3 == 1)
                {
                    Pic_Sign2.Image = Image.FromFile(@result.Item1);

                    FlagSign2 = result.Item2;
                    FlagEmpn2 = Empn2;

                    if (DeleteEznSarf())
                    {
                        reset();
                    }
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
            if (FlagSign1 != 1 || FlagSign2 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (row.Cells[4].Value == DBNull.Value || row.Cells[4].Value.ToString() == "")
                    {
                        MessageBox.Show("يجب ادخال الكمية المنصرفة لجميع البنود");
                        return;
                    }
                }
            }
            Empn3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على استلام طلب توريد", "");

            Sign3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على استلام طلب توريد", "");

            if (Sign3 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("3", "2", Sign3, Empn3);
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
        private void BTN_Sign4_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }

            Empn4 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على استلام اذن الصرف", "");


            Sign4 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على استلام اذن الصرف", "");

            if (Sign4 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);

                ///////// i will make them same people that make e3tmad so change 4 to 2(not inserted in table)

                Tuple<string, int, int, string, string> result = Constants.CheckSign("4", "2", Sign4, Empn4);
                // Tuple<string, int, int, string, string> result = Constants.CheckSign("1", "2", Sign4, Empn4);


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
        private void BTN_Sign5_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }

            Empn5 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على رقم القيد", "");

            Sign5 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على رقم القيد", "");

            if (Sign5 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("4", "2", Sign5, Empn5);
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
        }
        #endregion




        private void BTN_SearchEzn_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.SEARCH))
            {
                return;
            }

            string ezn_no = TXT_EznNo.Text;
            string fyear = Cmb_FYear.Text;
            string momayz = TXT_TRNO.Text;

            reset();

            if (SearchEznSarf(ezn_no, fyear, momayz))
            {
                if (FlagSign2 != 1 && FlagSign1 != 1)
                {
                    Editbtn2.Enabled = true;
                }
                else
                {
                    Editbtn2.Enabled = false;
                }
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

        private void Txt_ReqQuan_KeyPress(object sender, KeyPressEventArgs e)
        {
            Constants.validatenumbersanddecimal(Txt_ReqQuan.Text, e);
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

                    path += "EZN_SARF" + @"\";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }


                    path += TXT_TRNO.Text + @"\";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    path += TXT_EznNo.Text + @"\";

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

            popup.WholePath = @"\\172.18.8.83\MaterialAPP\PDF\" + Constants.CodeEdara + @"\" + Cmb_FYear.Text + @"\EZN_SARF\" + TXT_TRNO.Text + @"\" + TXT_EznNo.Text + @"\";
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

        private void Editbtn2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد تعديل اذن الصرف ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_EznNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text) || string.IsNullOrEmpty(TXT_TRNO.Text))
                {
                    MessageBox.Show("يجب اختيار نوع اذن الصرف و رقم اذن الصرف المراد تعديله و السنة المالية");
                    return;
                }

                PrepareEditState();
            }
        }

        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            AddEditFlag = 0;
            reset();
        }

        private void TXT_Total_KeyPress(object sender, KeyPressEventArgs e)
        {
            Constants.validatenumbersanddecimal(TXT_Total.Text, e);
        }

        private void BTN_Search_Motab3a_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.CONFIRM_SEARCH))
            {
                return;
            }

            string ezn_no = Cmb_EznNo2.Text;
            string fyear = Cmb_FYear2.Text;
            string momayz = TXT_TRNO2.Text;

            reset();

            if (SearchEznSarf(ezn_no, fyear, momayz))
            {
                Editbtn.Enabled = true;
                BTN_Print2.Enabled = true;
            }

            TXT_EznNo.Enabled = false;
            Cmb_FYear.Enabled = false;
            Cmb_CType.Enabled = false;
        }

        private void BTN_Print2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(TXT_EznNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text))
            {
                MessageBox.Show("يجب اختيار اذن الصرف المراد طباعتها اولا");
                return;
            }
            else
            {

                Constants.FormNo = 7;
                Constants.EznNo = Convert.ToInt32(TXT_EznNo.Text);
                Constants.EznFY = Cmb_FYear.Text;
                FReports F = new FReports();
                F.Show();
            }
        }

        private void Cmb_CType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(Cmb_CType2.Text) || string.IsNullOrWhiteSpace(Cmb_CType2.Text) || Cmb_CType2.SelectedIndex == -1))
            {
                TXT_TRNO2.Text = Cmb_CType2.SelectedValue.ToString();
            }
        }

    }
}
