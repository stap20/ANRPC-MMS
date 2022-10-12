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
        List<CurrencyInfo> currencies = new List<CurrencyInfo>();
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
        public decimal sum = 0;
        public int MaxFlag;
        //  public string TableQuery;

        AutoCompleteStringCollection TasnifColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TasnifNameColl = new AutoCompleteStringCollection(); //empn

        AutoCompleteStringCollection UnitColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection EznColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection PartColl = new AutoCompleteStringCollection(); //empn

        public FTransfer_M()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        //======================================
        private void TalbTawred_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aNRPC_InventoryDataSet.T_BnodAwamershraa' table. You can move, or remove it, as needed.
            // this.t_BnodAwamershraaTableAdapter.Fill(this.aNRPC_InventoryDataSet.T_BnodAwamershraa);

            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Syria));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Tunisia));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Gold));
            MaxFlag = 0;

            AddEditFlag = 0;
            if (Constants.EzonTahwel_FF == false)
            {
                panel7.Visible = true;
                panel2.Visible = false;
                panel7.Dock = DockStyle.Top;
            }
            else if (Constants.EzonTahwel_FF == true)
            {
                panel2.Visible = true;
                panel7.Visible = false;
                panel2.Dock = DockStyle.Top;
            }
            else { }
            //    if (Constants.User_Type != "A")
            //  {
            DisableControls();
            // }
            //------------------------------------------

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
            Cmb_CType2.SelectedIndexChanged += new EventHandler(Cmb_CType2_SelectedIndexChanged);
            //   TXT_Momayz.Text = Cmb_CType.SelectedValue.ToString();
            ////////////////////////////////////////////////
            ///

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

                    Num_Quan.Text = dr["VirtualQuan"].ToString();

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
        private void cleargridview()
        {
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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
                //btn_print.Enabled = false;
                EnableControls();

                Input_Reset();
                cleargridview();
                AddEditFlag = 2;
                TXT_MomayzHarka.Text = Constants.NameEdara;
                /////////////////// TXT_Momayz.Text = "68"; //default valud
                SaveBtn.Visible = true;
                BTN_Print.Enabled = false;
                Editbtn2.Enabled = false;

            }
            else
            {
                //do nothing
            }

        }



        public void EnableControls()
        {
            //AddNewbtn.Enabled = true;
            Addbtn2.Enabled = true;
            //dataGridView1.Enabled = true;
            TXT_TRansferNo.Enabled = true;
            Cmb_FYear.Enabled = true;
            TXT_Date.Enabled = true;
            // TXT_Momayz.Enabled = true;
            Cmb_CType.Enabled = true;
            Cmb_From.Enabled = true;
            Cmb_To.Enabled = true;
            TXT_MomayzHarka.Enabled = false;
            BTN_Sign1.Enabled = true;
            BTN_Sign2.Enabled = true;
            //  BTN_Sign3.Enabled = true;
            dataGridView1.Enabled = true;
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;


        }
        public void EnableControls_Malya()
        {
            TXT_AccNo.Enabled = true;
            TXT_PaccNo.Enabled = true;
            TXT_MTaklif.Enabled = true;
            TXT_MResp.Enabled = true;
            TXT_Masrof.Enabled = true;
            TXT_Enfak.Enabled = true;
            TXT_Morakba.Enabled = true;
            Cmb_CType.Enabled = true;

        }
        public void DisableControls_Malya()
        {
            TXT_AccNo.Enabled = false;
            TXT_PaccNo.Enabled = false;
            TXT_MTaklif.Enabled = false;
            TXT_MResp.Enabled = false;
            TXT_Masrof.Enabled = false;
            TXT_Enfak.Enabled = false;
            TXT_Morakba.Enabled = false;


        }


        public void Input_Reset()
        {
            Image1 = "";
            Image2 = "";
            picflag = 0;
            MaxFlag = 0;
            pictureBox2.Image = null;
            cleargridview();
            TXT_TRansferNo.Text = "";
            Cmb_FYear.Text = "";
            Cmb_CType.Text = "";
            Cmb_To.Text = "";
            Cmb_From.Text = "";
            TXT_TRNO.Text = "";
            TXT_MomayzHarka.Text = "";

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            MaxFlag = 0;
            //    TXT_Date.Enabled = true;
            TXT_TRNO.Text = "";

            TXT_AccNo.Text = "";
            TXT_PaccNo.Text = "";
            TXT_MTaklif.Text = "";
            TXT_MResp.Text = "";
            TXT_Masrof.Text = "";
            TXT_Enfak.Text = "";
            TXT_Morakba.Text = "";
            TXT_StockBian.Text = "";
            TXT_StockNoAll.Text = "";
            TXT_StockName.Text = "";
            Num_Quan.Value = 0;
            Num_ReqQuan.Value = 0;
            sum = 0;
            oldvalue = 0;
            TXT_Unit.Text = "";
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
        public void DisableControls()
        {
            // TXT_TalbNo.Enabled = false;
            //Cmb_FYear.Enabled =false;
            // AddNewbtn.Enabled = false;
            Addbtn2.Enabled = false;
            //dataGridView1.Enabled = false;
            TXT_Date.Enabled = false;
            TXT_TRNO.Enabled = false;
            Cmb_CType.Enabled = false;

            BTN_Sign1.Enabled = false;
            BTN_Sign2.Enabled = false;

            //dataGridView1.Enabled = false;
            // dataGridView1.ReadOnly = true;
            foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
            {
                dgvc.ReadOnly = true;
            }
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;


            TXT_MomayzHarka.Enabled = false;
            // BTN_Sign3.Enabled =false;
        }




        private void EditBtn_Click(object sender, EventArgs e)
        {
            AddEditFlag = 1;
        }

        private void Addbtn2_Click(object sender, EventArgs e)
        {

            if (AddEditFlag != 2 && AddEditFlag != 1)//not in add mode
            {
                MessageBox.Show("يجب اضافة/تعديل اذن التحويل اولا");
                return;

            }
            else
            {
                if (string.IsNullOrEmpty(TXT_TRansferNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text) || string.IsNullOrEmpty(TXT_TRNO.Text))
                {
                    MessageBox.Show("تاكد من  اختيار السنة المالية ورقم اذن التحويل و نوع التحويل");
                    return;
                }
                if (string.IsNullOrWhiteSpace(TXT_StockNoAll.Text))
                {
                    MessageBox.Show("يجب اختيار التصنيف المراد اضافته");
                    return;
                }
                if (string.IsNullOrWhiteSpace(Cmb_From.Text) || string.IsNullOrWhiteSpace(Cmb_To.Text))
                {
                    MessageBox.Show("يجب اختيار من و الى");
                    return;
                }
                if (TXT_TRNO.Text != "62" && (Num_ReqQuan.Value > Num_Quan.Value))
                {
                    MessageBox.Show("الكمية المطلوبة اكتر من المتاحة");
                    return;

                }
                if (TXT_TRNO.Text != "62" &&  Num_Quan.Value == 0)
                {
                    MessageBox.Show("لا يوجد رصيد من هذا الصنف");
                    return;
                }




                if ((Num_ReqQuan.Value == 0))
                {
                    MessageBox.Show("يجب ادخال الكمية المطلوبة");
                    return;

                }

                string stocknoall = TXT_StockNoAll.Text;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells[9].Value.ToString().ToLower() == stocknoall.ToLower())
                        {
                            MessageBox.Show("تم ادخال رقم هذا التصنيف من قبل");
                            return;
                        }
                    }
                }

                if (checkBox1.Checked == true || checkBox2.Checked == true)
                {
                    if ((Num_Quan.Value) - (Num_ReqQuan.Value) < Quan_Min.Value)
                    {
                        MessageBox.Show("بعد صرف الكمية المطلوبة الكمية المتاحة ستكون اقل من الحد الادنى ");
                        MaxFlag = MaxFlag + 1;

                        //  return;
                        array1[MaxFlag - 1, 3] = TXT_StockNoAll.Text;
                        array1[MaxFlag - 1, 0] = TXT_TRansferNo.Text;
                        array1[MaxFlag - 1, 1] = TXT_TRansferNo.Text;

                        array1[MaxFlag - 1, 2] = Cmb_FYear.Text;
                        array1[MaxFlag - 1, 4] = Num_ReqQuan.Text;
                        array1[MaxFlag - 1, 5] = Quan_Min.Text;

                    }

                }
                if (CMB_ApproxValue.Text.ToString() == "")
                {
                    //  MessageBox.Show("يجب اختيار القيمة التقديرية ");
                    //  return;
                }

                //    for (int row = 0; row < dataGridView1.Rows.Count - 1; row++)
                //  {

                //  dataGridView1.Rows.AddCopy(dataGridView1.Rows.Count - 1);
                //  r = dataGridView1.CurrentCell.RowIndex ;

                // if ( rowflag==0)
                //   {
                r = dataGridView1.Rows.Count - 1;

                rowflag = 1;
                DataRow newRow = table.NewRow();
                // Add the row to the rows collection.
                //   table.Rows.Add(newRow);
                table.Rows.InsertAt(newRow, r);
                dataGridView1.DataSource = table;

                dataGridView1.Rows[r].Cells[4].Value = Num_ReqQuan.Text.ToString();
                // dataGridView1.Rows[r].Cells[4].Value = TXT_Unit.Text.ToString();
                dataGridView1.Rows[r].Cells[5].Value = Cmb_From.Text;
                //  dataGridView1.Rows[r].Cells[3].Value = TXT_StockBian.Text;
                dataGridView1.Rows[r].Cells[6].Value = TXT_StockNoAll.Text;
                dataGridView1.Rows[r].Cells[7].Value = TXT_Unit.Text;

                dataGridView1.Rows[r].Cells[8].Value = TXT_StockBian.Text;




                if (string.IsNullOrEmpty(Num_Quan.Text))
                {
                    dataGridView1.Rows[r].Cells[9].Value = DBNull.Value;

                }
                else
                {
                    dataGridView1.Rows[r].Cells[9].Value = Num_Quan.Text;

                }
                dataGridView1.Rows[r].Cells[12].Value = Cmb_To.Text;
                dataGridView1.Rows[r].Cells[0].Value = TXT_TRansferNo.Text;
                dataGridView1.Rows[r].Cells[1].Value = TXT_TRNO.Text;
                dataGridView1.Rows[r].Cells[2].Value = Cmb_FYear.Text;

                dataGridView1.Rows[r].Cells[3].Value = r + 1;


                //   dataGridView1.Rows[r].Cells[3].Value = Num_ReqQuan.Value;
                dataGridView1.DataSource = table;
                dataGridView1.Rows[r + 1].Cells[4].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[5].Value = DBNull.Value;
                //  dataGridView1.Rows[r].Cells[3].Value = TXT_StockBian.Text;
                dataGridView1.Rows[r + 1].Cells[6].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[7].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[8].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[9].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[10].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[11].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[12].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[13].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[14].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[15].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[16].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[0].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[1].Value = DBNull.Value;

                dataGridView1.Rows[r + 1].Cells[2].Value = DBNull.Value;
                dataGridView1.Rows[r + 1].Cells[3].Value = DBNull.Value;
                // AddARow(table);
                //  dataGridView1.Rows[r+1].Cells[4].Value = DBNull.Value;
                // AddARow(table);
                // dataGridView1.Rows[r+1].Cells[0].Value = TXT_TRansferNo.Text;
                // dataGridView1.Rows[r+1].Cells[1].Value = TXT_TRNO.Text;
                //  dataGridView1.Rows[r+1].Cells[2].Value = Cmb_FYear.Text;

                // dataGridView1.Rows[r+1].Cells[3].Value = r + 1;

                /*   dataGridView1.Rows[r + 1].Cells[0].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[1].Value = DBNull.Value;

                  dataGridView1.Rows[r + 1].Cells[2].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[3].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[4].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[5].Value = DBNull.Value;
                   //  dataGridView1.Rows[r].Cells[3].Value = TXT_StockBian.Text;
                   dataGridView1.Rows[r + 1].Cells[6].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[7].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[8].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[9].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[10].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[11].Value = DBNull.Value;

                   dataGridView1.Rows[r + 1].Cells[12].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[13].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[14].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[15].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[16].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[0].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[1].Value = DBNull.Value;

                   dataGridView1.Rows[r + 1].Cells[2].Value = DBNull.Value;
                   dataGridView1.Rows[r + 1].Cells[3].Value = DBNull.Value;*/
                //   }
                /*  else if(rowflag==1)
                   {
                       r = dataGridView1.Rows.Count - 1;
                       //dataGridView1.Rows.AddCopy(dataGridView1.Rows.Count - 1);
                       //    AddARow(table);
                       // the table's schema.
                       /*
                       DataRow newRow = table.NewRow();

                       // Add the row to the rows collection.
                       //   table.Rows.Add(newRow);
                       table.Rows.InsertAt(newRow, r );

                       dataGridView1.DataSource = table;
                    

                       dataGridView1.Rows[r].Cells[4].Value = dataGridView1.Rows[r + 1].Cells[4].Value;
                       dataGridView1.Rows[r].Cells[5].Value = dataGridView1.Rows[r + 1].Cells[5].Value;
                       //  dataGridView1.Rows[r].Cells[3].Value = TXT_StockBian.Text;
                       dataGridView1.Rows[r].Cells[6].Value = dataGridView1.Rows[r + 1].Cells[6].Value;

                       if (string.IsNullOrEmpty(dataGridView1.Rows[r + 1].Cells[7].Value.ToString()))
                       {

                           dataGridView1.Rows[r].Cells[7].Value = DBNull.Value;
                       }
                       else
                       {

                           dataGridView1.Rows[r].Cells[7].Value = dataGridView1.Rows[r + 1].Cells[7].Value;
                       }
                       if (dataGridView1.Rows[r + 1].Cells[8].Value == null)
                       {
                           dataGridView1.Rows[r + 1].Cells[8].Value = "";
                       }
                       if (string.IsNullOrEmpty(dataGridView1.Rows[r + 1].Cells[8].Value.ToString()))
                       {

                           dataGridView1.Rows[r].Cells[8].Value = DBNull.Value;
                       }
                       else
                       {

                           dataGridView1.Rows[r].Cells[8].Value = dataGridView1.Rows[r + 1].Cells[8].Value;
                       }
                       //   dataGridView1.Rows[r].Cells[8].Value = dataGridView1.Rows[r + 1].Cells[8].Value;

                       dataGridView1.Rows[r].Cells[0].Value = dataGridView1.Rows[r + 1].Cells[0].Value;
                       dataGridView1.Rows[r].Cells[1].Value = dataGridView1.Rows[r + 1].Cells[1].Value;

                       dataGridView1.Rows[r].Cells[2].Value =r+1;
                       dataGridView1.Rows[r].Cells[3].Value = dataGridView1.Rows[r + 1].Cells[3].Value;

                       dataGridView1.DataSource = table;
                       dataGridView1.Rows[r].Cells[4].Value = TXT_Unit.Text.ToString();
                       dataGridView1.Rows[r].Cells[5].Value = TXT_StockBian.Text;
                       //  dataGridView1.Rows[r].Cells[3].Value = TXT_StockBian.Text;
                       dataGridView1.Rows[r].Cells[6].Value = TXT_StockNoAll.Text;
                     //  dataGridView1.Rows[r].Cells[7].Value = Num_Quan.Text;

                       dataGridView1.Rows[r].Cells[0].Value = TXT_TalbNo.Text;
                       dataGridView1.Rows[r].Cells[1].Value = Cmb_FYear.Text;

                       dataGridView1.Rows[r].Cells[2].Value = r + 2;
                       dataGridView1.Rows[r].Cells[3].Value = Num_ReqQuan.Value;
                      // dataGridView1.CurrentCell = dataGridView1.Rows[r + 1].Cells[0];


                      // dataGridView1.DataSource = table;
                    //   dataGridView1.DataSource = table;
                       //  table.DefaultView.Sort = "Bnd_No asc";
                       // table = table.DefaultView.ToTable();
                       //    GetData(Convert.ToInt32(TXT_TalbNo.Text), Cmb_FYear.Text);
                       //  dataGridView1.DataSource = table;
                       //  dataGridView1.DataBind();
                 

                  // }
                   //r = table.Rows.Count ;
                   /*    dataGridView1.Rows[r].Cells[4].Value = TXT_Unit.Text.ToString();
                       dataGridView1.Rows[r].Cells[5].Value = TXT_StockBian.Text;
                     //  dataGridView1.Rows[r].Cells[3].Value = TXT_StockBian.Text;
                       dataGridView1.Rows[r].Cells[6].Value = TXT_StockNoAll.Text;
                       dataGridView1.Rows[r].Cells[7].Value = Num_Quan.Text;

                       dataGridView1.Rows[r].Cells[0].Value = TXT_TalbNo.Text;
                       dataGridView1.Rows[r].Cells[1].Value = Cmb_FYear.Text;

                       dataGridView1.Rows[r].Cells[2].Value = r + 1;
                       dataGridView1.Rows[r].Cells[3].Value = Num_ReqQuan.Value;
                       dataGridView1.DataSource=table;*/
                //   dataGridView1.Rows[r].Cells[0].ReadOnly = true;
                //   dataGridView1.Rows[r].Cells[1].ReadOnly = true;
                //   dataGridView1.Rows[r].Cells[2].ReadOnly = true;
                //   dataGridView1.Rows[r].Cells[4].ReadOnly = true;
                //    dataGridView1.Rows[r].Cells[5].ReadOnly = true;
                //     dataGridView1.Rows[r].Cells[6].ReadOnly = true;
                //     dataGridView1.Rows[r].Cells[7].ReadOnly = true;
                //  dataGridView1.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                //     dataGridView1.AllowUserToAddRows = true;
                //         dataGridView1.AllowUserToDeleteRows = true;
                // dataGridView1.EndEdit();


                // }


            }
        }
        private void AddARow(DataTable t)
        {

            // Use the NewRow method to create a DataRow with 
            // the table's schema.
            DataRow newRow = t.NewRow();

            // Add the row to the rows collection.
            // t.Rows.Add(newRow);
            t.Rows.InsertAt(newRow, table.Rows.Count + 1);
        }

        private void Cmb_FYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AddEditFlag == 0)
            {
                Constants.opencon();

                TXT_TRansferNo.AutoCompleteMode = AutoCompleteMode.None;
                TXT_TRansferNo.AutoCompleteSource = AutoCompleteSource.None; ;
                // string cmdstring3 = "SELECT [EznSarf_No] from T_EznSarf where FYear='" + Cmb_FYear.Text + "'";
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
            //go and get talbTawreed_no for this FYear
            if (AddEditFlag == 2)//add
            {
                if (Cmb_CType.SelectedIndex == -1 && String.IsNullOrEmpty(TXT_TRNO.Text) == true)
                {
                    MessageBox.Show("يجب اختيار نوع اذن التحويل");
                    return;
                }

                if (Cmb_FYear.SelectedIndex == -1)
                {
                    return;
                }
                //call sp that get last num that eentered for this MM and this YYYY
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
                string cmdstring = "select ( COALESCE(MAX(cast(TransNo as int)), 0)) from  T_EzonTahwel where FYear=@FY and TR_NO=@TR ";
                SqlCommand cmd = new SqlCommand(cmdstring, con);

                // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text);
                cmd.Parameters.AddWithValue("@TR", TXT_TRNO.Text);

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
                        /////////////////////////done by nouran//////////////////////

                        string cmdstring2 = "select ( COALESCE(MAX(cast(Tahwel_No as int)), 0)) from  T_TempTahwelNo where FYear=@FY and TRNO=@TR ";

                        SqlCommand cmd2 = new SqlCommand(cmdstring2, con);

                        // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
                        cmd2.Parameters.AddWithValue("@FY", Cmb_FYear.Text);
                        cmd2.Parameters.AddWithValue("@TR", TXT_TRNO.Text);

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
                            if (flag <= (int)count2)
                            {
                                flag = (int)count2 + 1;
                            }
                        }

                        /////// insert temp table//////////////
                        string query = "exec SP_InsertTempTahwelNo @p1,@p2,@p3";
                        SqlCommand cmd1 = new SqlCommand(query, con);
                        cmd1.Parameters.AddWithValue("@p1", flag);
                        cmd1.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
                        cmd1.Parameters.AddWithValue("@p3", TXT_TRNO.Text);




                        cmd1.ExecuteNonQuery();

                        ///////////////////////////end by nouran///////////////////////



                        //////////////////////////////////////////////////






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
                    MessageBox.Show(sqlEx.ToString());
                    // flag = (int)cmd.Parameters["@Num"].Value;
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
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                MessageBox.Show(dataGridView1.Rows[0].Cells[16].Value.ToString());
                string cmdstring = "Exec SP_InsertEzonTahwel @TNO,@FY,@TD,@TRNO,@TName,@MH,@FS,@FCE,@FNE,@TS,@MNO,@OR,@RTT,@ACC,@PACC,@MT,@MR,@MA,@EN,@MK,@S11,@S1,@S2,@S3,@S4,@S5,@S6,@LU,@LD,@aot output";

                SqlCommand cmd = new SqlCommand(cmdstring, con);

                cmd.Parameters.AddWithValue("@TNO", (TXT_TRansferNo.Text));
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
                cmd.Parameters.AddWithValue("@TD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));

                cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text.ToString());
                cmd.Parameters.AddWithValue("@TName", Cmb_CType.Text.ToString());
                cmd.Parameters.AddWithValue("@MH", TXT_MomayzHarka.Text.ToString());
                cmd.Parameters.AddWithValue("@FS", Cmb_From.Text.ToString());

                cmd.Parameters.AddWithValue("@FCE", Cmb_From.Text.ToString());
                //cmd.Parameters.AddWithValue("@FNE", Cmb_From.SelectedValue.ToString());
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
                /*
                                if (TXT_Total.Text.ToString() == "")
                                {
                                    cmd.Parameters.AddWithValue("@TT", DBNull.Value);

                                }
                                else
                                {
                                 //   cmd.Parameters.AddWithValue("@TT", Convert.ToDecimal(TXT_Total.Text));

                                }
                                */
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


                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (!row.IsNewRow)
                        {



                            string q = "exec SP_InsertEzonTahwel_Benod @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19 ";
                            cmd = new SqlCommand(q, con);
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
                            cmd = new SqlCommand(q, con);

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


                    ////////////////

                    for (int i = 1; i <= 5; i++)
                    {


                        cmdstring = "Exec  SP_InsertSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
                        cmd = new SqlCommand(cmdstring, con);

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



                    ///////////////
                    MessageBox.Show("تم الإضافة بنجاح  ! ");

                    //  dataGridView1.EndEdit();
                    //   dataGridView1.DataSource = table;

                    //Getdata("SELECT  [TalbTwareed_No] ,[FYear],[Bnd_No],[RequestedQuan],Unit,[BIAN_TSNIF] ,STOCK_NO_ALL,Quan,[ArrivalDate] FROM [ANRPC_Inventory].[dbo].[T_TalbTawreed_Benod] ");
                    //  // getdata2();

                    //    dataadapter.InsertCommand = new SqlCommandBuilder(dataadapter).GetInsertCommand();

                    //   dataadapter.Update(table);
                    //  MessageBox.Show("تم  الإضافة بنجاح");
                    DisableControls();
                    BTN_Print.Visible = true;
                    SaveBtn.Visible = false;
                    Editbtn2.Enabled = true;
                    AddEditFlag = 0;
                }
                else if (executemsg == true && flag == 2)
                {
                    MessageBox.Show("تم إدخال رقم اذن التحويل  من قبل  ! ");
                }
                con.Close();
            }
            else if (AddEditFlag == 1)
            {
                UpdateEznSarf();
            }

        }

        public void SP_UpdateSignatures(int x, DateTime D1, DateTime? D2 = null)
        {
            string cmdstring = "Exec  SP_UpdateSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
            SqlCommand cmd = new SqlCommand(cmdstring, con);

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
        private void Getdata(string cmd)
        {
            dataadapter = new SqlDataAdapter(cmd, con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataadapter.Fill(table);
            dataGridView1.DataSource = table;
            //SELECT [EznSarf_No],[FYear],[CodeEdara],[NameEdara],[Date],[Momayz],[RequestedFor],[Responsiblecenter],[TR_NO] ,[Sign1],[Sign2],[Sign3],[Sign4] ,[Sign5],[LUser] ,[LDate] FROM [dbo].[T_EznSarf]

            dataGridView1.Columns["TransNo"].HeaderText = "رقم اذن التحويل";//col0
            dataGridView1.Columns["TransNo"].Visible = false;
            dataGridView1.Columns["TransNo"].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns["TransNo"].ReadOnly = true;

            dataGridView1.Columns["TR_NO"].HeaderText = "مميز مستند";//col1
            dataGridView1.Columns["TR_NO"].Visible = false;
            dataGridView1.Columns["TR_NO"].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns["TR_NO"].ReadOnly = true;

            // dataGridView1.Columns["TalbTwareed_No"].Width = 60;
            dataGridView1.Columns["FYear"].HeaderText = "السنة المالية";//col2
            dataGridView1.Columns["FYear"].Visible = false;

            dataGridView1.Columns["BndNo"].HeaderText = "19/18";//col3
            dataGridView1.Columns["BndNo"].Width = 40;
            dataGridView1.Columns["BndNo"].ReadOnly = true;
            dataGridView1.Columns["BndNo"].ContextMenuStrip = contextMenuStrip1;

            dataGridView1.Columns["Quan"].HeaderText = "27/20";//col4
            dataGridView1.Columns["Quan"].Width = 45;
            dataGridView1.Columns["Quan"].ReadOnly = true;
            dataGridView1.Columns["Quan"].ContextMenuStrip = contextMenuStrip1;
            //  dataGridView1.Columns["Quan1"].ReadOnly = true;
            /*  dataGridView1.Columns["Quan2"].HeaderText = "المنصرف";//col5
            dataGridView1.Columns["Quan2"].Width = 80;
            
                dataGridView1.Columns["Quan2"].ContextMenuStrip = contextMenuStrip1;
                if (Constants.User_Type == "B" && Constants.UserTypeB=="Sarf")
                {
                    dataGridView1.Columns["Quan2"].DefaultCellStyle.BackColor = Color.Salmon;
                }
              if(Constants.User_Type=="A")
              {
                  dataGridView1.Columns["Quan2"].ReadOnly = true;
           
              }
              if (Constants.User_Type == "B" && Constants.UserTypeB != "Sarf")
              {
                  dataGridView1.Columns["Quan2"].ReadOnly = false;

              }*/

            dataGridView1.Columns["FStockName"].HeaderText = "21/20";//col5
            dataGridView1.Columns["FStockName"].Width = 93;
            dataGridView1.Columns["FStockName"].ReadOnly = true;
            dataGridView1.Columns["FStockName"].ContextMenuStrip = contextMenuStrip1;

            dataGridView1.Columns["FStockNoALL"].HeaderText = "41/33";//col6
            dataGridView1.Columns["FStockNoALL"].Width = 88;
            dataGridView1.Columns["FStockNoALL"].ReadOnly = true;
            dataGridView1.Columns["FStockNoALL"].ContextMenuStrip = contextMenuStrip1;


            dataGridView1.Columns["FUnit"].HeaderText = "29/28";//col7
            dataGridView1.Columns["FUnit"].Width = 59;
            dataGridView1.Columns["FUnit"].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns["FUnit"].ReadOnly = true;

            dataGridView1.Columns["FBayan"].HeaderText = "البيان";//col8
            dataGridView1.Columns["FBayan"].Width = 120;
            dataGridView1.Columns["FBayan"].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns["FBayan"].ReadOnly = true;

            dataGridView1.Columns["FRased"].HeaderText = "49/3";//col9
            dataGridView1.Columns["FRased"].Width = 56;
            dataGridView1.Columns["FRased"].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns["FRased"].ReadOnly = true;

            dataGridView1.Columns["Fprice"].HeaderText = "59/50";//col10
            //dataGridView1.Columns["Fprice"].ReadOnly = true;
            dataGridView1.Columns["Fprice"].Width = 52;
            dataGridView1.Columns["Fprice"].ContextMenuStrip = contextMenuStrip1;

            dataGridView1.Columns["FNsbetSalhia"].HeaderText = "29/28";//col11
            dataGridView1.Columns["FNsbetSalhia"].Width = 99;
            dataGridView1.Columns["FNsbetSalhia"].ContextMenuStrip = contextMenuStrip1;
            //dataGridView1.Columns["FNsbetSalhia"].ReadOnly = true;

            //dataGridView1.Columns["PricePerUnit"].HeaderText = "سعر الوحدة";
            dataGridView1.Columns["TStockName"].HeaderText = "61/60";//col12
            dataGridView1.Columns["TStockName"].Width = 70;
            dataGridView1.Columns["TStockName"].ReadOnly = true;
            dataGridView1.Columns["TStockName"].ContextMenuStrip = contextMenuStrip1;
            /*if (Constants.User_Type == "B" && Constants.UserTypeB=="Finance")
            {
                dataGridView1.Columns["TotalPrice"].DefaultCellStyle.BackColor = Color.Salmon;
            }
            if (Constants.User_Type == "A")
            {
                dataGridView1.Columns["TotalPrice"].ReadOnly = true;
            }
            if (Constants.User_Type == "B" && Constants.UserTypeB !="Finance")
            {
                dataGridView1.Columns["TotalPrice"].ReadOnly = true;
            }
            // dataGridView1.Columns["TotalPrice"].ReadOnly = true;
            */

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
        private void GetData(string x, string y, string z)
        {
            if (string.IsNullOrWhiteSpace(TXT_TRansferNo.Text))
            {
                // MessageBox.Show("ادخل رقم التصريح");
                //  PermNo_text.Focus();'EznSarf_No
                return;
            }
            else
            {
                table.Clear();
                TableQuery = "SELECT [TransNo] ,[TR_NO],[FYear],[BndNo],[Quan],[FStockName],[FStockNoALL],[FUnit],[FBayan],[FRased],[Fprice],[FNsbetSalhia] ,[TStockName],[TStockNoALL],[TUnit] ,[TBayan],[TRased] FROM [T_EzonTahwel_Benod] Where TransNo = " + x + " and Fyear='" + y + "' and TR_NO='" + z + "'";
                Getdata(TableQuery);
            }

        }


        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void BTN_Sign1_Click(object sender, EventArgs e)
        {


            Empn1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على انشاء اذن تحويل", "");

            Sign1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على انشاء اذن تحويل", "");
            if (Sign1 != "" && Empn1 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
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
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 || FlagSign2 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {/*
                  if (!row.IsNewRow)
                  {
                      if (row.Cells[4].Value == DBNull.Value || row.Cells[4].Value.ToString()=="")
                      {
                          MessageBox.Show("يجب ادخال الكمية المنصرفة لجميع البنود");
                          return;
                      }
                  }*/
            }
            Empn3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على استلام طلب توريد", "");

            Sign3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على استلام طلب توريد", "");

            if (Sign3 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
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
            Empn2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على اعتماد اذن تحويل", "");

            Sign2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على اعتماد اذن تحويل", "");

            if (Sign2 != "" && Empn2 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
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
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }

        private void Num_ReqQuan_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Editbtn_Click_1(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد تعديل اذن التحويل ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_TRansferNo.Text) || string.IsNullOrEmpty(Cmb_FYear.Text) || string.IsNullOrEmpty(TXT_TRNO.Text))
                {
                    MessageBox.Show("يجب اختيار اذن التحويل المراد تعديله");
                    return;
                }
                else
                {

                    AddEditFlag = 1;
                    BTN_Print.Enabled = false;
                    TNO = TXT_TRansferNo.Text;
                    FY = Cmb_FYear.Text;
                    SaveBtn.Visible = true;
                    Addbtn.Enabled = false;
                    var button = (Button)sender;
                    if (button.Name == "Editbtn")
                    {
                        dataGridView1.Enabled = true;
                        DisableControls();

                        if (Constants.User_Type == "A")
                        {
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign1.Enabled = true;
                            BTN_Sign2.Enabled = true;
                            BTN_Sign4.Enabled = false;
                            BTN_Sign5.Enabled = false;
                            BTN_Sign6.Enabled = false;
                            BTN_Sign3.Enabled = false;


                        }
                        else if (Constants.User_Type == "B" && Constants.UserTypeB == "Transfer1")
                        {
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign1.Enabled = false;
                            BTN_Sign2.Enabled = false;
                            BTN_Sign4.Enabled = false;
                            BTN_Sign3.Enabled = false;

                            BTN_Sign5.Enabled = true;
                            BTN_Sign6.Enabled = false;
                            dataGridView1.Enabled = true;
                            // dataGridView1.Columns["Quan2"].ReadOnly = false;

                            //EnableControls_Malya();


                        }
                        else if (Constants.User_Type == "B" && Constants.UserTypeB == "Transfer2")
                        {
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign1.Enabled = false;
                            BTN_Sign2.Enabled = false;
                            BTN_Sign4.Enabled = false;
                            BTN_Sign3.Enabled = false;

                            BTN_Sign5.Enabled = false;
                            BTN_Sign6.Enabled = true;
                            dataGridView1.Enabled = true;
                            // dataGridView1.Columns["Quan2"].ReadOnly = false;

                            //EnableControls_Malya();


                        }
                        else if (Constants.User_Type == "B" && (Constants.UserTypeB == "Tkalif" || Constants.UserTypeB == "Finance"))
                        {
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign1.Enabled = false;
                            BTN_Sign2.Enabled = false;
                            BTN_Sign4.Enabled = false;
                            BTN_Sign5.Enabled = false;
                            BTN_Sign6.Enabled = false;
                            BTN_Sign3.Enabled = false;
                            EnableControls_Malya();


                        }

                        else if (Constants.User_Type == "B" && Constants.UserTypeB == "NewTasnif")
                        {
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign1.Enabled = false;
                            BTN_Sign2.Enabled = false;
                            BTN_Sign4.Enabled = false;
                            BTN_Sign3.Enabled = true;

                            BTN_Sign5.Enabled = false;
                            BTN_Sign6.Enabled = false;
                            dataGridView1.Enabled = true;
                            dataGridView1.Columns["TStockNoALL"].ReadOnly = false;

                            //EnableControls_Malya();


                        }
                        else if (Constants.User_Type == "B" && Constants.UserTypeB == "Estlam")
                        {
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign1.Enabled = false;
                            BTN_Sign2.Enabled = false;
                            BTN_Sign4.Enabled = true;
                            BTN_Sign3.Enabled = false;

                            BTN_Sign5.Enabled = false;
                            BTN_Sign6.Enabled = false;
                            dataGridView1.Enabled = true;
                            dataGridView1.Columns["TStockNoALL"].ReadOnly = false;

                            //EnableControls_Malya();


                        }
                    }
                    else if (button.Name == "Editbtn2")
                    {

                        if (Constants.User_Type == "A")
                        {
                            EnableControls();
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign1.Enabled = true;
                            BTN_Sign2.Enabled = true;
                            BTN_Sign4.Enabled = false;
                            BTN_Sign5.Enabled = false;
                            BTN_Sign3.Enabled = false;
                            BTN_Sign6.Enabled = false;


                        }

                        else if (Constants.User_Type == "B" && Constants.UserTypeB == "Transfer1")
                        {
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign1.Enabled = false;
                            BTN_Sign2.Enabled = false;
                            BTN_Sign4.Enabled = false;
                            BTN_Sign3.Enabled = false;

                            BTN_Sign5.Enabled = true;
                            BTN_Sign6.Enabled = false;
                            dataGridView1.Enabled = true;
                            // dataGridView1.Columns["Quan2"].ReadOnly = false;

                            //EnableControls_Malya();


                        }
                        else if (Constants.User_Type == "B" && Constants.UserTypeB == "Transfer2")
                        {
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign1.Enabled = false;
                            BTN_Sign2.Enabled = false;
                            BTN_Sign4.Enabled = false;
                            BTN_Sign3.Enabled = false;

                            BTN_Sign5.Enabled = false;
                            BTN_Sign6.Enabled = true;
                            dataGridView1.Enabled = true;
                            // dataGridView1.Columns["Quan2"].ReadOnly = false;

                            //EnableControls_Malya();


                        }
                        else if (Constants.User_Type == "B" && Constants.UserTypeB == "Estlam")
                        {
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign1.Enabled = false;
                            BTN_Sign2.Enabled = false;
                            BTN_Sign4.Enabled = true;
                            BTN_Sign3.Enabled = false;

                            BTN_Sign5.Enabled = false;
                            BTN_Sign6.Enabled = false;
                            dataGridView1.Enabled = true;
                            dataGridView1.Columns["TStockNoALL"].ReadOnly = false;

                            //EnableControls_Malya();


                        }
                        else if (Constants.User_Type == "B" && (Constants.UserTypeB == "Tkalif" || Constants.UserTypeB == "Finance"))
                        {
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign1.Enabled = false;
                            BTN_Sign2.Enabled = false;
                            BTN_Sign4.Enabled = false;
                            BTN_Sign5.Enabled = false;
                            BTN_Sign6.Enabled = false;
                            BTN_Sign3.Enabled = false;
                            EnableControls_Malya();



                            //  BTN_Sign3.Enabled = true;
                        }
                        else if (Constants.User_Type == "B" && Constants.UserTypeB == "NewTasnif")
                        {
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign1.Enabled = false;
                            BTN_Sign2.Enabled = false;
                            BTN_Sign4.Enabled = false;
                            BTN_Sign3.Enabled = true;

                            BTN_Sign5.Enabled = false;
                            BTN_Sign6.Enabled = false;
                            dataGridView1.Enabled = true;
                            dataGridView1.Columns["TStockNoALL"].ReadOnly = false;

                            //EnableControls_Malya();


                        }
                    }
                }

            }
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

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
            Cmb_TalbNo2.DataSource = dts;
            Cmb_TalbNo2.ValueMember = "TransNo";
            Cmb_TalbNo2.DisplayMember = "TransNo";
            Cmb_TalbNo2.SelectedIndex = -1;
            Cmb_TalbNo2.SelectedIndexChanged += new EventHandler(Cmb_TalbNo2_SelectedIndexChanged);
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

            //  SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
            if (x == 1 && Constants.User_Type == "A")
            {

                cmdstring = "select * from  T_EzonTahwel where TransNo=@TN and FYear=@FY and FromEdaraCode=@EC and TR_NO=@TR";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", TXT_TRansferNo.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text);
                cmd.Parameters.AddWithValue("@EC", Constants.CodeEdara);

            }
            else if (x == 2 && Constants.User_Type == "A")
            {
                //  cmd = new SqlCommand(cmdstring, Constants.con);
                cmdstring = "select * from  T_EzonTahwel where TransNo=@TN and FYear=@FY and FromEdaraCode=@EC and TR_NO=@TR";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", Cmb_TalbNo2.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
                cmd.Parameters.AddWithValue("@EC", Constants.CodeEdara);
                cmd.Parameters.AddWithValue("@TR", TXT_TRNO.Text);

            }
            else if (x == 2 && Constants.User_Type == "B")
            {
                //   cmd = new SqlCommand(cmdstring, Constants.con);
                cmdstring = "select * from  T_EzonTahwel where TransNo=@TN and FYear=@FY and TR_NO=@TR";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", Cmb_TalbNo2.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
                cmd.Parameters.AddWithValue("@TR", TXT_TRNO.Text);
            }
            else if (x == 1 && Constants.User_Type == "B")
            {
                //  cmd = new SqlCommand(cmdstring, Constants.con);
                cmdstring = "select * from  T_EzonTahwel where TransNo=@TN and FYear=@FY and TR_NO=@TR";
                cmd = new SqlCommand(cmdstring, Constants.con);
                cmd.Parameters.AddWithValue("@TN", TXT_TRansferNo.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text);
                cmd.Parameters.AddWithValue("@TR", TXT_TRNO.Text);
            }
            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);


            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    Cmb_FYear.Text = dr["FYear"].ToString();
                    TXT_TRansferNo.Text = dr["TransNo"].ToString();
                    TXT_MomayzHarka.Text = dr["TypeTransName"].ToString();
                    TXT_CodeEdara.Text = dr["FromEdaraName"].ToString();
                    TXT_Date.Text = dr["TransDate"].ToString();
                    TXT_TRNO.Text = dr["TR_NO"].ToString();
                    Cmb_From.Text = dr["FromEdaraName"].ToString();
                    Cmb_To.Text = dr["TToStock"].ToString();

                    
                    if (TXT_TRNO.Text.ToString() == "")
                    {

                    }
                    else
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




                    // TXT_BndMwazna.Text=dr["BndMwazna"].ToString();
                    string s111 = dr["Sign11"].ToString();
                    string s1 = dr["Sign1"].ToString();
                    string s2 = dr["Sign2"].ToString();
                    string s3 = dr["Sign3"].ToString();
                    string s4 = dr["Sign4"].ToString();
                    string s5 = dr["Sign5"].ToString();
                    string s6 = dr["Sign6"].ToString();

                    if (s111 != "")
                    {
                        string p = Constants.RetrieveSignature("111", "7", s1);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename111 = p.Split(':')[1];
                            wazifa111 = p.Split(':')[2];
                            pp = p.Split(':')[0];

                            //   ((PictureBox)this.panel1.Controls["Pic_Sign" + "111"]).Image = Image.FromFile(@pp);

                            FlagSign111 = 1;
                            FlagEmpn111 = s111;
                            //   ((PictureBox)this.panel1.Controls["Pic_Sign" + "111"]).BackColor = Color.Green;
                            //   toolTip1.SetToolTip(Pic_Sign1, Ename1 + Environment.NewLine + wazifa11);
                        }

                    }
                    else
                    {
                        //  ((PictureBox)this.panel1.Controls["Pic_Sign" + "111"]).BackColor = Color.Red;
                    }

                    //if (s1 == "1")
                    if (s1 != "")
                    {
                        string p = Constants.RetrieveSignature("1", "7", s1);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
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
                        string p = Constants.RetrieveSignature("2", "7", s2);
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
                        string p = Constants.RetrieveSignature("3", "7", s3);
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
                    if (s4 != "")
                    {
                        string p = Constants.RetrieveSignature("4", "7", s4);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename3 = p.Split(':')[1];
                            wazifa3 = p.Split(':')[2];
                            pp = p.Split(':')[0];
                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "4"]).Image = Image.FromFile(@pp);
                            FlagSign4 = 1;
                            FlagEmpn4 = s4;
                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "4"]).BackColor = Color.Green;
                            toolTip1.SetToolTip(Pic_Sign4, Ename4 + Environment.NewLine + wazifa4);



                            ////
                        }

                    }
                    else
                    {
                        ((PictureBox)this.panel1.Controls["Pic_Sign" + "4"]).BackColor = Color.Red;
                    }
                    if (s5 != "")
                    {
                        string p = Constants.RetrieveSignature("5", "7", s5);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename5 = p.Split(':')[1];
                            wazifa5 = p.Split(':')[2];
                            pp = p.Split(':')[0];
                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "5"]).Image = Image.FromFile(@pp);
                            FlagSign5 = 1;
                            FlagEmpn5 = s5;
                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "5"]).BackColor = Color.Green;
                            toolTip1.SetToolTip(Pic_Sign5, Ename5 + Environment.NewLine + wazifa5);

                        }

                    }
                    else
                    {
                        ((PictureBox)this.panel1.Controls["Pic_Sign" + "5"]).BackColor = Color.Red;
                    }

                    //  string s6=dr["Mohmat_Sign"].ToString();
                    // string s7=dr["CH_Sign"].ToString();
                    //dr.Close();

                    ///////
                    /////////////////
                    if (s6 != "")
                    {
                        string p = Constants.RetrieveSignature("6", "7", s5);
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string
                            Ename6 = p.Split(':')[1];
                            wazifa6 = p.Split(':')[2];
                            pp = p.Split(':')[0];
                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "6"]).Image = Image.FromFile(@pp);
                            FlagSign6 = 1;
                            FlagEmpn6 = s6;
                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "6"]).BackColor = Color.Green;
                            toolTip1.SetToolTip(Pic_Sign5, Ename5 + Environment.NewLine + wazifa5);

                        }

                    }
                    else
                    {
                        ((PictureBox)this.panel1.Controls["Pic_Sign" + "6"]).BackColor = Color.Red;
                    }
                }
                BTN_Print.Enabled = true;

            }

            else
            {

                MessageBox.Show("من فضلك تاكد من رقم اذن الصرف");

                BTN_Print.Enabled = false;

            }
            dr.Close();

            /* for (int i = 1; i <= 5; i++)
             {
                 
                 string p = Constants.RetrieveSignature( i.ToString(),"2");
                 if (p != "")
                 {
                     //   Pic_Sign1
                     //	"Pic_Sign1"	string
                     
                     ((PictureBox)this.panel1.Controls["Pic_Sign" + i.ToString()]).Image = Image.FromFile(@p);

                 }

             }*/
            //  string query1 = "SELECT  [TalbTwareed_No] ,[FYear] ,[Bnd_No],[RequestedQuan],[Unit],[BIAN_TSNIF] ,[STOCK_NO_ALL],[Quan] ,[ArrivalDate] FROM [T_TalbTawreed_Benod] where  [TalbTwareed_No]=@T and [FYear]=@F ";
            //  SqlCommand cmd1 = new SqlCommand(query1, Constants.con);
            //  cmd1.Parameters.AddWithValue("@T",Cmb_TalbNo2.Text);
            //  cmd1.Parameters.AddWithValue("@F", Cmb_FYear2.Text);


            // DT.Clear();
            // DT.Load(cmd1.ExecuteReader());
            // cleargridview();
            GetData((TXT_TRansferNo.Text), Cmb_FYear.Text, TXT_TRNO.Text);
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

        private void Cmb_TalbNo2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //SearchTalb();
        }

        private void BTN_Save2_Click(object sender, EventArgs e)
        {
            if (Constants.User_Type == "A")
            {
                if (FlagSign2 != 1)
                {
                    MessageBox.Show("برجاء تأكد من توقيع الاعتماد");
                    return;
                }
            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "NewTasnif")
            {


                if (FlagSign3 != 1)
                {
                    MessageBox.Show("برجاء توقيع مدير ادارة التصنيف");
                    return;
                }
            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Estlam")
            {


                if (FlagSign4 != 1)
                {
                    MessageBox.Show("برجاء توقيع امين المخزن");
                    return;
                }
            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Transfer1")
            {


                if (FlagSign5 != 1)
                {
                    MessageBox.Show("برجاء توقيع الاعتماد مدير ادارة المخازن");
                    return;
                }
            }
            else if (Constants.User_Type == "B" && Constants.UserTypeB == "Transfer2")
            {


                if (FlagSign6 != 1)
                {
                    MessageBox.Show("برجاء توقيع الاعتماد مدير قطاع المخازن");
                    return;
                }
            }
            if (AddEditFlag == 1)
            {
                UpdateEznSarf();
                Input_Reset();
                Cmb_FYear2.SelectedIndex = -1;
                Cmb_TalbNo2.SelectedIndex = -1;
                Cmb_TalbNo2.Text = "";
            }
        }
        public void UpdateEznSarf()
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            /////////////////////////////////////////////////


            string cmdstring1 = "select STOCK_NO_ALL,quan1,quan2 from T_EznSarf_Benod where FYear=@FY and EznSarf_No=@TNO";
            SqlCommand cmd1 = new SqlCommand(cmdstring1, con);


            cmd1.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_TRansferNo.Text));
            cmd1.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            SqlDataReader dr = cmd1.ExecuteReader();

            //---------------------------------
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    if (dr["quan1"].ToString() == "")
                    {

                    }
                    else
                    {
                        string cmdstring2 = "Exec SP_UndoVirtualQuan2 @TNO,@FY";

                        SqlCommand cmd2 = new SqlCommand(cmdstring2, con);

                        cmd2.Parameters.AddWithValue("@TNO", (dr["STOCK_NO_ALL"].ToString()));
                        if (dr["quan2"].ToString() == "")
                        {

                            cmd2.Parameters.AddWithValue("@FY", Convert.ToDouble(dr["quan1"].ToString()));
                        }
                        else
                        {
                            cmd2.Parameters.AddWithValue("@FY", Convert.ToDouble(dr["quan2"].ToString()));
                        }
                        //  cmd2.Parameters.AddWithValue("@BN", (dr["Bnd_No"].ToString()));
                        cmd2.ExecuteNonQuery();
                    }

                }
            }
            dr.Close();



            /////////////////////////////////////////
            string cmdstring = "Exec SP_UpdateEzonTahwel @TO,@FYO,@TNO,@FY,@TD,@TRNO,@TName,@MH,@FS,@FCE,@FNE,@TS,@MNO,@OR,@RTT,@ACC,@PACC,@MT,@MR,@MA,@EN,@MK,@S11,@S1,@S2,@S3,@S4,@S5,@S6,@LU,@LD,@aot output";

            SqlCommand cmd = new SqlCommand(cmdstring, con);
            cmd.Parameters.AddWithValue("@TO", TNO);
            cmd.Parameters.AddWithValue("@FYO", FY);
            cmd.Parameters.AddWithValue("@TNO", (TXT_TRansferNo.Text));
            cmd.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
            cmd.Parameters.AddWithValue("@TD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));

            cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text.ToString());
            cmd.Parameters.AddWithValue("@TName", Cmb_CType.Text.ToString());
            cmd.Parameters.AddWithValue("@MH", TXT_MomayzHarka.Text.ToString());
            cmd.Parameters.AddWithValue("@FS", Cmb_From.Text.ToString());

            //   cmd.Parameters.AddWithValue("@FCE", Cmb_From.SelectedValue.ToString());
            //   cmd.Parameters.AddWithValue("@FNE", Cmb_From.Text.ToString());
            cmd.Parameters.AddWithValue("@FCE", Cmb_From.Text.ToString());
            //cmd.Parameters.AddWithValue("@FNE", Cmb_From.SelectedValue.ToString());
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
            /*
            cmd.Parameters.AddWithValue("@S11", DBNull.Value);
            cmd.Parameters.AddWithValue("@S1", DBNull.Value);

            cmd.Parameters.AddWithValue("@S2", DBNull.Value);

            cmd.Parameters.AddWithValue("@S3", DBNull.Value);

            cmd.Parameters.AddWithValue("@S4", DBNull.Value);

            cmd.Parameters.AddWithValue("@S5", DBNull.Value);

            cmd.Parameters.AddWithValue("@S6", DBNull.Value);

            */
            cmd.Parameters.AddWithValue("@LU", Constants.User_Name.ToString());
            cmd.Parameters.AddWithValue("@LD", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            /*   if (TXT_Total.Text.ToString() == "")
               {
                       cmd.Parameters.AddWithValue("@TT",DBNull.Value);

               }
               else
               {
                   cmd.Parameters.AddWithValue("@TT", Convert.ToDecimal(TXT_Total.Text));

               }
            */
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
            if (executemsg == true && flag == 2)
            {


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (!row.IsNewRow)
                    {


                        string q = "exec SP_InsertEzonTahwel_Benod @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19 ";
                        cmd = new SqlCommand(q, con);
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



                    //dataGridView1.EndEdit();
                    //    dataGridView1.DataSource = table;

                    //   Getdata("SELECT  [TalbTwareed_No] ,[FYear],[Bnd_No],[RequestedQuan],Unit,[BIAN_TSNIF] ,STOCK_NO_ALL,Quan,[ArrivalDate] FROM [ANRPC_Inventory].[dbo].[T_TalbTawreed_Benod] ");
                    //  // getdata2();

                    //  dataadapter.InsertCommand = new SqlCommandBuilder(dataadapter).GetInsertCommand();
                    //   MessageBox.Show(dataadapter.InsertCommand.CommandText);
                    //      MessageBox.Show(dataadapter.InsertCommand.Parameter);
                    //   dataadapter.InsertCommand.Parameters.AddWithValue("p1", )

                    //  dataadapter.Update(table);
                    // MessageBox.Show("تم  التعديلس بنجاح");
                    //////////////////////////////////////here must insert trans in tr_in_2////////////



                    //////////////////////////

                }
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (!row.IsNewRow)
                    {



                        string q = "exec SP_UpdateVirtualQuan @p1,@p2,@p3";
                        cmd = new SqlCommand(q, con);

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

                    //  SP_UpdateSignatures(11, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                }

                // InsertTrans();
                //   UpdateQuan();

                if (FlagSign4 == 1 && FlagSign5 != 1 && FlagSign6 != 1 && TXT_TRNO.Text.ToString() == "15")
                {

                    // InsertTrans();
                    // UpdateQuan();


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

                DisableControls();
                DisableControls_Malya();
                BTN_Print.Visible = true;
                SaveBtn.Visible = false;
                Addbtn.Enabled = true;
                AddEditFlag = 0;
            }
            else if (executemsg == true && flag == 3)
            {
                MessageBox.Show("تم إدخال رقم طلب التوريد  من قبل  ! ");
            }
            con.Close();
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

                Tuple<string, int, int, string, string> result = Constants.CheckSign("4", "7", Sign4, Empn4);
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
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);

                //will make them same pople as e3tmad9not inserted in table)
                //

                // Tuple<string, int, int, string, string> result = Constants.CheckSign("5", "2", Sign5, Empn5);


                Tuple<string, int, int, string, string> result = Constants.CheckSign("4", "7", Sign5, Empn5);
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



        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد حذف اذن التحويل ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(TXT_TRansferNo.Text) || string.IsNullOrWhiteSpace(TXT_TRNO.Text))
                {
                    MessageBox.Show("يجب اختياراذن التحويل  اولا");
                    return;
                }
                Constants.opencon();

                /*
                string cmdstring1 = "select STOCK_NO_ALL,quan1,quan2 from T_EznSarf_Benod where FYear=@FY and EznSarf_No=@TNO";
                SqlCommand cmd1 = new SqlCommand(cmdstring1, con);


                cmd1.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_TRansferNo.Text));
                cmd1.Parameters.AddWithValue("@FY", Cmb_FYear.Text.ToString());
                SqlDataReader dr = cmd1.ExecuteReader();

                //---------------------------------
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        if (dr["quan1"].ToString() == "")
                        {

                        }
                        else
                        {
                            string cmdstring2 = "Exec SP_UndoVirtualQuan2 @TNO,@FY";

                            SqlCommand cmd2 = new SqlCommand(cmdstring2, con);

                            cmd2.Parameters.AddWithValue("@TNO", (dr["STOCK_NO_ALL"].ToString()));
                            if (dr["quan2"].ToString() == "")
                            {

                                cmd2.Parameters.AddWithValue("@FY", Convert.ToDouble(dr["quan1"].ToString()));
                            }
                            else
                            {
                                cmd2.Parameters.AddWithValue("@FY", Convert.ToDouble(dr["quan2"].ToString()));
                            }
                            //  cmd2.Parameters.AddWithValue("@BN", (dr["Bnd_No"].ToString()));
                            cmd2.ExecuteNonQuery();
                        }

                    }
                }
                dr.Close();

              */
                /////////////////////////////

                /////////////////////////////////////////////////////////////////////////
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
            // GetData(Convert.ToInt32(TXT_TalbNo.Text), Cmb_FYear.Text);

        }

        private void Cmb_TalbNo2_TextChanged(object sender, EventArgs e)
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

        private void Cmb_TalbNo2_DropDownClosed(object sender, EventArgs e)
        {
            //
            //SearchTalb(2);
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

        private void TXT_TalbNo_Leave(object sender, EventArgs e)
        {

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

        private void Cmb_TalbNo2_SelectedValueChanged(object sender, EventArgs e)
        {
            //    SearchTalb(2);
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

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
        /// <summary>
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
        /// </summary>
        /// 
        public void UpdateQuan2()
        {
            Constants.opencon();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (!row.IsNewRow)
                {
                    // string st = "select AvailableQuan from TR_IN_2 where SER_DOC=@S and FYear=@FY and SER_LIN=@L ";
                    //   SqlCommand cmd2 = new SqlCommand(st, Constants.con);

                    //   cmd2.Parameters.AddWithValue("@S", TXT_EznNo.Text);
                    ////  cmd2.Parameters.AddWithValue("@FY", Cmb_FYear.Text);
                    //////  cmd2.Parameters.AddWithValue("@L", (row.Cells[2].Value));

                    //  var oldvalue = cmd2.ExecuteScalar();


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
                    // string st = "select AvailableQuan from TR_IN_2 where SER_DOC=@S and FYear=@FY and SER_LIN=@L ";
                    //   SqlCommand cmd2 = new SqlCommand(st, Constants.con);

                    //   cmd2.Parameters.AddWithValue("@S", TXT_EznNo.Text);
                    ////  cmd2.Parameters.AddWithValue("@FY", Cmb_FYear.Text);
                    //////  cmd2.Parameters.AddWithValue("@L", (row.Cells[2].Value));

                    //  var oldvalue = cmd2.ExecuteScalar();


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

        private void Pic_Sign1_Click(object sender, EventArgs e)
        {

        }

        private void TXT_StockBian_TextChanged(object sender, EventArgs e)
        {

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

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void TXT_StockNoAll_TextChanged(object sender, EventArgs e)
        {
            Num_ReqQuan.Value = 0;
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
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

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

        private void TXT_Total_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // ToWord toWord = new ToWord(Convert.ToDecimal(TXT_Total.Text), currencies[0]);
                //   txt_englishword.Text = toWord.ConvertToEnglish();
                //  TXT_ArabicValue.Text = toWord.ConvertToArabic();
            }
            catch (Exception ex)
            {
                //   txt_englishword.Text = String.Empty;
                //  TXT_ArabicValue.Text = String.Empty;
            }
        }

        private void TXT_EznNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TXT_TRansferNo.Text) == false)
            {
                GetData((TXT_TRansferNo.Text), Cmb_FYear.Text, TXT_TRNO.Text);

            }
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void Cmb_TalbNo2_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void Cmb_CType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_CType.SelectedValue.ToString() == "")
            {
 
            }
            else
            {

                Cmb_FYear.SelectedIndex = -1;
                Cmb_FYear.Text = "";
                TXT_TRansferNo.Text = "";
                TXT_TRNO.Text = Cmb_CType.SelectedValue.ToString();
                if (TXT_TRNO.Text == "62")
                {

                    Cmb_From.Enabled = false;
                    Cmb_From.SelectedIndexChanged -= new EventHandler(Cmb_From_SelectedIndexChanged);
                    string cmdstring = "SELECT  CodeEdara,NameEdara FROM Edarat ";//will use cmdstring3


                    SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                    //cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
                    DataTable dts2 = new DataTable();
                    Constants.opencon();
                    dts2.Load(cmd.ExecuteReader());
                    Cmb_From.DataSource = dts2;
                    Cmb_From.ValueMember = "CodeEdara";
                    Cmb_From.DisplayMember = "NameEdara";
                    Cmb_From.SelectedIndex = -1;
                    Cmb_From.SelectedIndexChanged += new EventHandler(Cmb_From_SelectedIndexChanged);
                    ///////
                    ///        Cmb_To.SelectedIndexChanged -= new EventHandler(Cmb_To_SelectedIndexChanged);
                    cmdstring = "SELECT  [CCode],[CName] FROM T_InventoryNames";//will use cmdstring3


                    cmd = new SqlCommand(cmdstring, Constants.con);

                    //cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
                    dts2 = new DataTable();

                    dts2.Load(cmd.ExecuteReader());
                    Cmb_To.DataSource = dts2;
                    Cmb_To.ValueMember = "CCode";
                    Cmb_To.DisplayMember = "CName";
                    Cmb_To.SelectedIndex = -1;
                    Cmb_To.SelectedIndexChanged += new EventHandler(Cmb_To_SelectedIndexChanged);
                    Cmb_From.SelectedValue = Constants.CodeEdara;
                }
                else if (TXT_TRNO.Text == "15")
                {

                    Cmb_From.Enabled = true;
                    Cmb_From.SelectedIndexChanged -= new EventHandler(Cmb_From_SelectedIndexChanged);
                    string cmdstring = "SELECT  [CCode],[CName] FROM T_InventoryNames";//will use cmdstring3


                    SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                    //cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
                    DataTable dts2 = new DataTable();

                    dts2.Load(cmd.ExecuteReader());
                    Cmb_From.DataSource = dts2;
                    Cmb_From.ValueMember = "CCode";
                    Cmb_From.DisplayMember = "CName";
                    Cmb_From.SelectedIndex = -1;
                    Cmb_From.SelectedIndexChanged += new EventHandler(Cmb_From_SelectedIndexChanged);

                    ///////////////////////
                    ///

                    Cmb_To.SelectedIndexChanged -= new EventHandler(Cmb_To_SelectedIndexChanged);
                    cmdstring = "SELECT  [CCode],[CName] FROM T_InventoryNames";//will use cmdstring3


                    cmd = new SqlCommand(cmdstring, Constants.con);

                    //cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
                    dts2 = new DataTable();

                    dts2.Load(cmd.ExecuteReader());
                    Cmb_To.DataSource = dts2;
                    Cmb_To.ValueMember = "CCode";
                    Cmb_To.DisplayMember = "CName";
                    Cmb_To.SelectedIndex = -1;
                    Cmb_To.SelectedIndexChanged += new EventHandler(Cmb_To_SelectedIndexChanged);
                }
            }
        }

        private void BTN_Sign111_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 || FlagSign2 != 1 || FlagSign3 != 1 || FlagSign4 != 1 || FlagSign5 != 1)
            {
                MessageBox.Show("من فضلك تاكد من التوقيعات السابقة");
                return;
            }
            Empn6 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع على انشاء اذن تحويل", "");

            Sign6 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع على انشاء اذن تحويل", "");
            if (Sign6 != "" && Empn6 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
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
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }

        private void Cmb_From_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Pic_Sign3_Click(object sender, EventArgs e)
        {

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

        private void Cmb_CType2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Pic_Sign2_Click(object sender, EventArgs e)
        {

        }
    }
}
