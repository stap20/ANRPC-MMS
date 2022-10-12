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
using System.Drawing.Printing;
namespace ANRPC_Inventory
{


    public partial class Tasnif : Form
    {
        public SqlConnection con;//sql conn for anrpc_sms db
        Image DefaulteImg;
        Image image1;
        Image image2;

        string Image1;
        string Image2;
        public int indeximg = 0;
        byte[] img1;
        byte[] img2;
        int picflag = 0;
        Boolean flag = false;
        Boolean Flag_other = false;
        public DataTable DT = new DataTable();
        public double VirtualQuan;
        private BindingSource bindingsource1 = new BindingSource();
        private string TableQuery;
        private int AddEditFlag;
        public Boolean executemsg;
        public double totalprice;
        public string stockallold;
        public double LockedQuan;
        DataTable table = new DataTable();
        public SqlDataAdapter dataadapter;
        public DataSet ds = new DataSet();

        AutoCompleteStringCollection TasnifColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection UnitColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection PartColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection DescriptionColl = new AutoCompleteStringCollection(); //empn

        public Tasnif()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }


        private void Tasnif_Load(object sender, EventArgs e)
        {
            //Form Validation 
            //--------------------
            //--> A for Search 
            //--> 1 for Edit
            //------------------
            if (Constants.User_Type == "A") 
            {
                ButtonsPanel.Visible = false;
            }
            if (Constants.User_Name == "User1_Inventory")
            {
                Num_Quan.ReadOnly = false;
            }
            else
            {
                Num_Quan.ReadOnly = true;
            }
            //---------------------------
            DisableControls();
            TXT_GR.MaxLength = 2;
            TXT_R1.MaxLength = 2;

            TXT_R2.MaxLength = 2;

            TXT_R3.MaxLength = 2;

            //DISABLE CONTROLS WILL BE OPENED IN CASE OF ADD OR EDIT
            DisableControls();
            con = new SqlConnection(Constants.constring);

            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            //*******************************************
            // ******    AUTO COMPLETE
            //*******************************************
            string cmdstring = "";
            SqlCommand cmd = new SqlCommand(cmdstring, con);
            if (Constants.User_Type == "B")
            {
                cmdstring = "select STOCK_NO_ALL from T_Tsnif ";
              cmd = new SqlCommand(cmdstring, con);

            }

            //  string cmdstring = "select STOCK_NO_ALL from T_Tsnif";
            if (Constants.User_Type == "A")
            {
                // cmdstring = "select STOCK_NO_ALL from T_Tsnif where CodeEdara='"+Constants.CodeEdara +"'";
                cmdstring = "select STOCK_NO_ALL,PartNO,BIAN_TSNIF from T_Tsnif where CodeEdara=" + Constants.CodeEdara;
                //   cmdstring = "select * from T_Tsnif where STOCK_NO_G in( select STOCK_NO_G from t_groupsedarat where edaracode1=@EC or edaracode2=@EC or edaracode3=@EC or edaracode4 =@EC or edaracode5 =@EC)";

                cmd = new SqlCommand(cmdstring, con);
                cmd.Parameters.AddWithValue("EC", Constants.CodeEdara);
                SqlDataReader dr = cmd.ExecuteReader();
                //---------------------------------
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        TasnifColl.Add(dr["STOCK_NO_ALL"].ToString());
                        PartColl.Add(dr["PartNO"].ToString());
                        DescriptionColl.Add(dr["BIAN_TSNIF"].ToString());
                    }
                }
                dr.Close();

            }
            else
            {
                SqlDataReader dr = cmd.ExecuteReader();
                //---------------------------------
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        TasnifColl.Add(dr["STOCK_NO_ALL"].ToString());

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
                        UnitColl.Add(dr2["eng_unit"].ToString());

                    }
                }
                dr2.Close();
                ///////////////////////////////////edara combo//////////////
                string cmdstring3 = "select PartNO from T_Tsnif";
                SqlCommand cmd3 = new SqlCommand(cmdstring3, con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                //---------------------------------
                if (dr3.HasRows == true)
                {
                    while (dr3.Read())
                    {
                        PartColl.Add(dr3["PartNO"].ToString());

                    }
                }
                dr3.Close();
                //////////////////////////////////////////////
                string cmdstring4 = "select BIAN_TSNIF from T_Tsnif";
                SqlCommand cmd4 = new SqlCommand(cmdstring4, con);
                SqlDataReader dr4 = cmd4.ExecuteReader();
                //---------------------------------
                if (dr4.HasRows == true)
                {
                    while (dr4.Read())
                    {
                        DescriptionColl.Add(dr4["BIAN_TSNIF"].ToString());

                    }
                }
                dr4.Close();
                //////////////////////////////////////////////
            }

            string query = "SELECT CodeEdara , NameEdara FROM Edarat";
            cmd = new SqlCommand(query, con);
            DataTable dts = new DataTable();
            dts.Load(cmd.ExecuteReader());
            Edara_cmb.DataSource = dts;
            Edara_cmb.ValueMember = "CodeEdara";
            Edara_cmb.DisplayMember = "NameEdara";
            Edara_cmb.SelectedIndex = -1;
            ///////////////////////////////////////////////////////////
            //
            ////////////////////////////////////////////////
            TXT_StockNoAll.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_StockNoAll.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_StockNoAll.AutoCompleteCustomSource = TasnifColl;
            TXT_Unit.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_Unit.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_Unit.AutoCompleteCustomSource = UnitColl;
            TXT_PartNo2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_PartNo2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_PartNo2.AutoCompleteCustomSource = PartColl;

            TXT_StockName_Search.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_StockName_Search.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_StockName_Search.AutoCompleteCustomSource = DescriptionColl;

            con.Close();

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
                pictureBox3.Image=Image.FromFile(@Image1);
                picflag = 1;
            }
        }
        // Search about the Tasnif with the name
        private void TXT_StockNoAll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }  //--> OPEN CONNECTION
                pictureBox3.Image = null;
                Image1 = "";
                Image2 = "";
                picflag = 0;
                string query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where STOCK_NO_ALL= @a";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@a", (TXT_StockNoAll.Text));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        TXT_GR.Text = dr["STOCK_NO_G"].ToString();
                        if (dr["VirtualQuan"].ToString() == "")
                        {
                            VirtualQuan = 0;
                        }
                        else
                        {
                            VirtualQuan = Convert.ToDouble(dr["VirtualQuan"].ToString());

                        }
                        if (dr["Quan"].ToString() == "")
                        {
                            LockedQuan = 0;
                        }
                        else
                        {


                            LockedQuan = Convert.ToDouble(dr["Quan"].ToString());
                           
                        }
                        LockedQuan = LockedQuan - VirtualQuan;
                        Num_LockedQuan.Text =LockedQuan.ToString();
                        TXT_PartNo.Text = dr["PartNO"].ToString();
                        TXT_R1.Text = dr["STOCK_NO_R1"].ToString();
                        TXT_R2.Text = dr["STOCK_NO_R2"].ToString();
                        TXT_R3.Text = dr["STOCK_NO_R3"].ToString();
                        TXT_StockName.Text = dr["STOCK_NO_NAM"].ToString();
                        TXT_Stockian.Text = dr["BIAN_TSNIF"].ToString();
                        TXT_Unit.Text = dr["Unit"].ToString();
                        Num_Quan.Text = dr["Quan"].ToString();
                     //   foreach (var f in Directory.EnumerateFiles(rootPath, "*12345*.jpg")){



                      //  pictureBox1.Image = Image.FromFile(@"Images\a.bmp");

                    SearchImage1(TXT_StockNoAll.Text);
                    SearchImage2(TXT_StockNoAll.Text);
                        if ((dr["SafeAmount"].ToString()) == "1")
                        {
                            checkBox1.Checked = true;
                        }
                        else
                        {
                            checkBox1.Checked = false;
                        }

                        if ((dr["StrategeAmount"].ToString()) == "1")
                        {
                            checkBox2.Checked = true;
                        }
                        else
                        {
                            checkBox2.Checked = false;
                        }
                        Num_Min.Text = dr["MinAmount"].ToString();
                        Num_Max.Text = dr["MaxAmount"].ToString();


                        if (dr["MinAmount"].ToString() == "")
                        {
                            Num_Min.Value = 0;
                        }

                        if (dr["MaxAmount"].ToString() == "")
                        {
                            Num_Max.Value = 0;
                        }
                        Edara_cmb.SelectedValue = dr["CodeEdara"].ToString();
                        LUser.Text = dr["LUser"].ToString();
                        LDate.Text = dr["LDate"].ToString();
                        AddEditFlag = 2;//ADDNEW

                    }
                }
                else
                {
                    MessageBox.Show("من فضلك تاكد من التصنيف");

                }
                dr.Close();
            }
        }
        //-----------------------------------------------------------------
        private void SaveBtn_Click(object sender, EventArgs e)
        {


            //Save Validations
            //------------------
            if (string.IsNullOrWhiteSpace(TXT_GR.Text) || string.IsNullOrWhiteSpace(TXT_R1.Text) || string.IsNullOrWhiteSpace(TXT_R2.Text) || string.IsNullOrWhiteSpace(TXT_R3.Text))
            {
                MessageBox.Show("من فضلك ادخل كود التصنيف كامل");
                return;
            }
            if (string.IsNullOrWhiteSpace(TXT_StockName.Text))
            {
               // MessageBox.Show("من فضلك ادخل اسم التصنيف ");
               // return;
            }
            if (string.IsNullOrWhiteSpace(TXT_Stockian.Text))
            {
                MessageBox.Show("من فضلك ادخل وصف الصنف ");
                return;
            }
            //----------------------------------------
            if (checkBox1.Checked || checkBox2.Checked)
            {
                if (Num_Min.Value == 0)
                {
                    MessageBox.Show("هذا التصنييف بند امان/ بند استراتيجى يجب ادخال حد ادنى له");
                    return;
                }
                if (Num_Max.Value == 0)
                {
                    MessageBox.Show("هذا التصنييف بند امان/ بند استراتيجى يجب ادخال حد اقصى له");
                    return;
                }
            }

            if (AddEditFlag == 1)//edit
            {


                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                //  string query1 = " date=@d, TimeIn=@TI ,TimeOut=@TO,LUser=@LU,LDate=@LD Where EMPN = @a and date =@olddate and TimeIn=@TII";
                string query1 = "exec SP_UpdateTasnif @St,@PPNN,@StO,@SN,@SG,@S1,@S2,@S3,@B,@U,@Q,@QQ,@PU,@TP,@Min,@Max,@StA,@Saf,@CE,@NE,@LU,@LD,@flag out";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                string stockall = TXT_GR.Text + TXT_R1.Text + TXT_R2.Text + TXT_R3.Text;
                cmd1.Parameters.AddWithValue("@St", stockall);
                cmd1.Parameters.AddWithValue("@PPNN", TXT_PartNo.Text);
                cmd1.Parameters.AddWithValue("@StO", stockallold);
                cmd1.Parameters.AddWithValue("@SN", TXT_StockName.Text);
                // string LDate = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                // DateTime dateTime2 = DateTime.Parse(LDate);
                // LDate = dateTime2.ToString("yyyy-MM-dd");
                cmd1.Parameters.AddWithValue("@SG", TXT_GR.Text);
                cmd1.Parameters.AddWithValue("@S1", TXT_R1.Text);
                cmd1.Parameters.AddWithValue("@S2", TXT_R2.Text);
                cmd1.Parameters.AddWithValue("@S3", TXT_R3.Text);

                cmd1.Parameters.AddWithValue("@B", TXT_Stockian.Text);
                cmd1.Parameters.AddWithValue("@U", TXT_Unit.Text);
                cmd1.Parameters.AddWithValue("@Q", Convert.ToInt32(Num_Quan.Text));

                cmd1.Parameters.AddWithValue("@QQ", Convert.ToDouble(Num_Quan.Value));//????????????????????????????????????????????????check

                if (!string.IsNullOrWhiteSpace(TXT_PricePerUnit.Text))
                {
                    cmd1.Parameters.AddWithValue("@PU", Convert.ToDouble(TXT_PricePerUnit.Text));
                }
                else
                {
                    cmd1.Parameters.AddWithValue("@PU", 0);
                }
                cmd1.Parameters.AddWithValue("@TP", totalprice);
                if (Num_Min.Text == "")
                {
                    cmd1.Parameters.AddWithValue("@Min", 0);
                }
                else
                {
                    cmd1.Parameters.AddWithValue("@Min", Convert.ToInt32(Num_Min.Text));
                }


                if (Num_Max.Text == "")
                {
                    cmd1.Parameters.AddWithValue("@Max", 0);
                }
                else
                {
                    cmd1.Parameters.AddWithValue("@Max", Convert.ToInt32(Num_Max.Text));
                }
              //  cmd1.Parameters.AddWithValue("@Max", Convert.ToInt32(Num_Max.Text));
                cmd1.Parameters.AddWithValue("@StA", Convert.ToInt32(checkBox2.Checked));
                cmd1.Parameters.AddWithValue("@Saf", Convert.ToInt32(checkBox1.Checked));
                cmd1.Parameters.AddWithValue("@NE", Edara_cmb.Text);
                cmd1.Parameters.AddWithValue("@CE", (Edara_cmb.SelectedValue));
                cmd1.Parameters.AddWithValue("@LU", Constants.User_Name.ToString());
                cmd1.Parameters.AddWithValue("@LD", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                cmd1.Parameters.Add("@flag", SqlDbType.Int, 32);  //-------> output parameter
                cmd1.Parameters["@flag"].Direction = ParameterDirection.Output;

                int flag;

                try
                {
                    cmd1.ExecuteNonQuery();
                    executemsg = true;
                    flag = (int)cmd1.Parameters["@flag"].Value;
                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    MessageBox.Show(sqlEx.ToString());
                    flag = (int)cmd1.Parameters["@flag"].Value;
                }
                if (executemsg == true && flag == 2)
                {
                    MessageBox.Show("تم التعديل بنجاح  ! ");
                   Addbtn.Enabled = true;
                   DeleteBtn.Enabled = false;
                   pic_upload.Enabled =false;
                    DisableControls();

                }
                else if (executemsg == true && flag == 3)
                {
                    MessageBox.Show("رقم التصنيف تم إدخاله من قبل ");
                }
                con.Close();


            }

            else if (AddEditFlag == 2)//add
            {
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string query1 = "exec SP_InsertTasnif @St,@PPNN,@SN,@SG,@S1,@S2,@S3,@B,@U,@Q,@QQ,@PU,@TP,@Min,@Max,@StA,@Saf,@CE,@NE,@LU,@LD,@flag out";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                string stockall = TXT_GR.Text + TXT_R1.Text + TXT_R2.Text + TXT_R3.Text;
                cmd1.Parameters.AddWithValue("@St", stockall);
                cmd1.Parameters.AddWithValue("@PPNN", TXT_PartNo.Text);

                cmd1.Parameters.AddWithValue("@SN", TXT_StockName.Text);
                // cmd1.Parameters.AddWithValue("@StO",stockallold);
                // string LDate = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                // DateTime dateTime2 = DateTime.Parse(LDate);
                // LDate = dateTime2.ToString("yyyy-MM-dd");
                cmd1.Parameters.AddWithValue("@SG", TXT_GR.Text);
                cmd1.Parameters.AddWithValue("@S1", TXT_R1.Text);
                cmd1.Parameters.AddWithValue("@S2", TXT_R2.Text);
                cmd1.Parameters.AddWithValue("@S3", TXT_R3.Text);

                cmd1.Parameters.AddWithValue("@B", TXT_Stockian.Text);
                cmd1.Parameters.AddWithValue("@U", TXT_Unit.Text);
                cmd1.Parameters.AddWithValue("@Q", Convert.ToInt32(Num_Quan.Value));

                cmd1.Parameters.AddWithValue("@QQ", Convert.ToDouble(Num_Quan.Value));

                if (!string.IsNullOrWhiteSpace(TXT_PricePerUnit.Text))
                {
                    cmd1.Parameters.AddWithValue("@PU", Convert.ToDouble(TXT_PricePerUnit.Text));
                }
                else
                {
                    cmd1.Parameters.AddWithValue("@PU", 0);
                }
                cmd1.Parameters.AddWithValue("@TP", totalprice);
                cmd1.Parameters.AddWithValue("@Min", Convert.ToInt32(Num_Min.Value));
                cmd1.Parameters.AddWithValue("@Max", Convert.ToInt32(Num_Max.Value));
                cmd1.Parameters.AddWithValue("@StA", Convert.ToInt32(checkBox2.Checked));
                cmd1.Parameters.AddWithValue("@Saf", Convert.ToInt32(checkBox1.Checked));
                cmd1.Parameters.AddWithValue("@NE", Edara_cmb.Text);
                cmd1.Parameters.AddWithValue("@CE", (Edara_cmb.SelectedValue.ToString()));
                cmd1.Parameters.AddWithValue("@LU", Constants.User_Name.ToString());
                cmd1.Parameters.AddWithValue("@LD", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                cmd1.Parameters.Add("@flag", SqlDbType.Int, 32);  //-------> output parameter
                cmd1.Parameters["@flag"].Direction = ParameterDirection.Output;

                int flag;

                try
                {
                    cmd1.ExecuteNonQuery();
                    executemsg = true;
                    flag = (int)cmd1.Parameters["@flag"].Value;
                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    MessageBox.Show(sqlEx.ToString());
                    flag = (int)cmd1.Parameters["@flag"].Value;
                }
                if (executemsg == true && flag == 1)
                {
                    MessageBox.Show("تم الاضافة بنجاح  ! ");
                    DisableControls();
                    EditBtn.Enabled = true;
                    
                  /*  string cmdstring = "select STOCK_NO_ALL from T_Tsnif";
                    SqlCommand cmd = new SqlCommand(cmdstring, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    //---------------------------------
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            TasnifColl.Add(dr["STOCK_NO_ALL"].ToString());

                        }
                    }
                    dr.Close();*/

                }
                else if (executemsg == true && flag == 2)
                {
                    MessageBox.Show("رقم التصنيف تم إدخاله من قبل ");
                }
                con.Close();

            }
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد اضافة تصنيف جديد؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                // BTN_PrintPerm.Visible = false;
                EnableControls();
                Input_Reset();
                table.Clear();
                AddEditFlag = 2;
                SaveBtn.Visible = true;
                EditBtn.Enabled = false;

            }
            else
            {
                //do nothing
            }
            // AddEditFlag = 2;
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد تعديل التصنيف؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                //  BTN_PrintPerm.Visible = false;
                EnableControls();
                AddEditFlag = 1;
                stockallold = TXT_GR.Text + TXT_R1.Text + TXT_R2.Text + TXT_R3.Text;
                DeleteBtn.Enabled = true;
                pic_upload.Enabled = true;
                Addbtn.Enabled = false;
                SaveBtn.Visible = true;




            }
            else
            {
                //do nothung
            }

        }


        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TXT_GR.Text) || string.IsNullOrWhiteSpace(TXT_R1.Text) || string.IsNullOrWhiteSpace(TXT_R2.Text) || string.IsNullOrWhiteSpace(TXT_R3.Text))
            {
                MessageBox.Show("من فضلك ادخل كود التصنيف كامل");
                return;
            }
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            //  string query1 = " date=@d, TimeIn=@TI ,TimeOut=@TO,LUser=@LU,LDate=@LD Where EMPN = @a and date =@olddate and TimeIn=@TII";
            string query1 = "exec SP_deleteTsnif @St,@flag out";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            string stockall = TXT_GR.Text + TXT_R1.Text + TXT_R2.Text + TXT_R3.Text;
            cmd1.Parameters.AddWithValue("@St", stockall);
            //cmd1.Parameters.AddWithV
            cmd1.Parameters.Add("@flag", SqlDbType.Int, 32);  //-------> output parameter
            cmd1.Parameters["@flag"].Direction = ParameterDirection.Output;

            int flag;

            try
            {
                cmd1.ExecuteNonQuery();
                executemsg = true;
                flag = (int)cmd1.Parameters["@flag"].Value;
            }
            catch (SqlException sqlEx)
            {
                executemsg = false;
                MessageBox.Show(sqlEx.ToString());
                flag = (int)cmd1.Parameters["@flag"].Value;
            }
            if (executemsg == true && flag == 1)
            {
                MessageBox.Show("تم الحذف بنجاح  ! ");
                Input_Reset();
                /*
                string cmdstring = "select STOCK_NO_ALL from T_Tsnif";
                SqlCommand cmd = new SqlCommand(cmdstring, con);
                SqlDataReader dr = cmd.ExecuteReader();
                //---------------------------------
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        TasnifColl.Add(dr["STOCK_NO_ALL"].ToString());

                    }
                }
                dr.Close();*/

            }
            else if (executemsg == true && flag == 2)
            {
                MessageBox.Show("رقم التصنيف المراد حذفه غير موجود");
            }

        }
        private void DisableControls()
        {
            TXT_StockName.Enabled = false;
            TXT_Stockian.Enabled = false;
            TXT_GR.Enabled = false;
            TXT_R1.Enabled = false;
            TXT_R2.Enabled = false;
            TXT_R3.Enabled = false;
            //  TXT_StockQuan.Enabled = true;
            TXT_Unit.Enabled = false;
            TXT_PricePerUnit.Enabled = false;
            checkBox1.Enabled = false;
            Num_Max.Enabled = false;
            Num_Min.Enabled = false;
            checkBox2.Enabled = false;
            Num_Quan.Enabled = false;
            Num_LockedQuan.Enabled =false;
        }
        private void EnableControls()
        {
            TXT_StockName.Enabled = true;
            TXT_Stockian.Enabled = true;
            TXT_GR.Enabled = true;
            TXT_R1.Enabled = true;
            TXT_R2.Enabled = true;
            TXT_R3.Enabled = true;
            //  TXT_StockQuan.Enabled = true;
            TXT_Unit.Enabled = true;
            TXT_PricePerUnit.Enabled = true;
           checkBox1.Enabled = true;
            Num_Max.Enabled = true;
            Num_Min.Enabled = true;
           checkBox2.Enabled = true;
           if (Constants.UserTypeB == "Inventory")
           {
               Num_Quan.Enabled = true;
               Num_LockedQuan.Enabled = true;
           }
           else
           {
                Num_Quan.Enabled = false;
               Num_LockedQuan.Enabled = false;
           }

           
        }
        private void Input_Reset()
        {
            Image1 = "";
            Image2 = "";
            picflag = 0;
            pictureBox3.Image = null;
            TXT_StockNoAll.Text = "";
            TXT_PartNo.Text = "";
            TXT_PartNo2.Text = "";
            LUser.Text = "";
            LDate.Text = "";
            TXT_GR.Text = "";
            TXT_R1.Text = "";
            TXT_R2.Text = "";
            TXT_R3.Text = "";
            TXT_StockName.Text = "";
            TXT_Stockian.Text = "";
            //TXT_StockQuan.Text ="0";
            TXT_Unit.Text = "";
            TXT_PricePerUnit.Text = "";
           checkBox1.Checked = false;
            Num_Max.Value = 0;
            Num_Min.Value = 0;
            checkBox2.Checked = false;
            Num_Quan.Value = 0;
            Num_LockedQuan.Value = 0;
            Edara_cmb.SelectedIndex= -1;


        }

        private void TXT_R1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
              && !char.IsDigit(e.KeyChar))
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

        private void TXT_GR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
              && !char.IsLetter(e.KeyChar)&&!char.IsDigit(e.KeyChar))
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TXT_PricePerUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void TXT_GR_Validating(object sender, CancelEventArgs e)
        {
            if ((sender as TextBox).Text.Length == 0) return;

            // Validate text, cancel when not valid and show error to user
            if ((sender as TextBox).Text.Length < 2 || (sender as TextBox).Text.Length > 2)
            {
                e.Cancel = true;
                MessageBox.Show("يجب ان يتكون من حرفين فقط");
                TXT_GR.Focus();
            }
        }

        private void TXT_R1_Validating(object sender, CancelEventArgs e)
        {
            if ((sender as TextBox).Text.Length == 0) return;

            // Validate text, cancel when not valid and show error to user
            if ((sender as TextBox).Text.Length < 2 || (sender as TextBox).Text.Length > 2)
            {
                e.Cancel = true;
                MessageBox.Show("يجب ان يتكون من رقمين فقط");
                TXT_R1.Focus();
            }
        }

        private void TXT_R2_Validating(object sender, CancelEventArgs e)
        {
            if ((sender as TextBox).Text.Length == 0) return;

            // Validate text, cancel when not valid and show error to user
            if ((sender as TextBox).Text.Length < 2 || (sender as TextBox).Text.Length > 2)
            {
                e.Cancel = true;
                MessageBox.Show("يجب ان يتكون من رقمينس فقط");
                TXT_R2.Focus();
            }
        }

        private void TXT_R3_Validating(object sender, CancelEventArgs e)
        {
            if ((sender as TextBox).Text.Length == 0) return;

            // Validate text, cancel when not valid and show error to user
            if ((sender as TextBox).Text.Length < 2 || (sender as TextBox).Text.Length > 2)
            {
                e.Cancel = true;
                MessageBox.Show("يجب ان يتكون من رقمين فقط");
                TXT_R3.Focus();
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TXT_StockNoAll_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXT_PartNo2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }  //--> OPEN CONNECTION
                pictureBox3.Image = null;
                Image1 = "";
                Image2 = "";
                picflag = 0;
                string query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where PartNO= @a";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@a", (TXT_PartNo2.Text));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        TXT_GR.Text = dr["STOCK_NO_G"].ToString();
                        TXT_StockNoAll.Text = dr["STOCK_NO_ALL"].ToString();
                        SearchImage1(TXT_StockNoAll.Text);
                        SearchImage2(TXT_StockNoAll.Text);



                        //   VirtualQuan = Convert.ToDouble(dr["VirtualQuan"].ToString());
                        if (dr["VirtualQuan"].ToString() == "")
                        {
                            VirtualQuan = 0;
                        }
                        else
                        {
                            VirtualQuan = Convert.ToDouble(dr["VirtualQuan"].ToString());

                        }
                ///
                     
                        if (dr["Quan"].ToString() == "")
                        {
                            LockedQuan = 0;
                        }
                        else
                        {


                            LockedQuan = Convert.ToDouble(dr["Quan"].ToString());
                           
                        }
                        LockedQuan = LockedQuan - VirtualQuan;
                        Num_LockedQuan.Text =LockedQuan.ToString();
                        ////
                        TXT_PartNo.Text = dr["PartNO"].ToString();
                        TXT_R1.Text = dr["STOCK_NO_R1"].ToString();
                        TXT_R2.Text = dr["STOCK_NO_R2"].ToString();
                        TXT_R3.Text = dr["STOCK_NO_R3"].ToString();
                        TXT_StockName.Text = dr["STOCK_NO_NAM"].ToString();
                        TXT_Stockian.Text = dr["BIAN_TSNIF"].ToString();
                        TXT_Unit.Text = dr["Unit"].ToString();
                        Num_Quan.Text = dr["Quan"].ToString();
                        if((dr["SafeAmount"].ToString())=="1"){
                             checkBox1.Checked=true;
                        }else{
                             checkBox1.Checked=false;
                        }

                        if((dr["StrategeAmount"].ToString())=="1"){
                             checkBox2.Checked=true;
                        }else{
                             checkBox2.Checked=false;
                        }
                      //  checkBox1.Checked =(int)(dr["SafeAmount"].ToString());
                     //   checkBox2.Checked= int(dr["StrategeAmount"].ToString());
                        Num_Min.Text = dr["MinAmount"].ToString();
                        Num_Max.Text = dr["MaxAmount"].ToString();
                        Edara_cmb.SelectedValue = dr["CodeEdara"].ToString();
                        LUser.Text = dr["LUser"].ToString();
                        LDate.Text = dr["LDate"].ToString();
                        AddEditFlag = 2;//ADDNEW

                    }
                }
                else
                {
                    MessageBox.Show("من فضلك تاكد من التصنيف");

                }
                dr.Close();
            }
        }

        private void TXT_PartNo2_TextChanged(object sender, EventArgs e)
        {

        }

        private void prev_btn_Click(object sender, EventArgs e)
        {
            if (Image1!= "")
            {
                picflag = 1;
                pictureBox3.Image = Image.FromFile(@Image1);

            }
        }

        private void next_btn_Click(object sender, EventArgs e)
        {

            if(Image2 != "")
            {
                picflag = 2;
                pictureBox3.Image = Image.FromFile(@Image2);

            }
        }

        private void pic_upload_Click(object sender, EventArgs e)
        {
            if (picflag == 0 && ( string.IsNullOrEmpty(Image1 ) || string.IsNullOrEmpty( Image2 )))//first picture
            {
                picflag = 1;
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files (*.jpg,*.png,*.jpeg,*.gif,*.bmp)|*.jpg;*.png; *.jpeg; *.gif; *.bmp";
               
                if (open.ShowDialog() == DialogResult.OK)
                {
                    Image1 = open.FileName;
                    pictureBox3.Image = Image.FromFile(@Image1);
                //    System.IO.File.Copy(@"Image1", @"\\warehouse-app\e\Photos\");
                   // pictureBox3.BackgroundImage = new Bitmap(open.FileName);
                 //   FileStream fs = new FileStream(@open.FileName, FileMode.Open, FileAccess.Read);
                //    img1 = new byte[fs.Length];
                //    fs.Read(img1, 0, Convert.ToInt32(fs.Length));

                }
            }
            else if (picflag == 1 && (string.IsNullOrEmpty(Image1) || string.IsNullOrEmpty(Image2)))//second picture
            {
                picflag = 2;
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files (*.jpg,*.png,*.jpeg,*.gif,*.bmp)|*.jpg;*.png; *.jpeg; *.gif; *.bmp";
               

                if (open.ShowDialog() == DialogResult.OK)
                {
                    Image2 = open.FileName;
                    pictureBox3.Image = Image.FromFile(@Image2);
                    //System.IO.File.Copy("Image2", @"\\warehouse-app\e\Photos\");
                   // pictureBox1.BackgroundImage = new Bitmap(open.FileName);
                  //  FileStream fs = new FileStream(@open.FileName, FileMode.Open, FileAccess.Read);
                  //  img2 = new byte[fs.Length];
                  //  fs.Read(img2, 0, Convert.ToInt32(fs.Length));
                }
            }
        }

        private void delete_img_Click(object sender, EventArgs e)
        {
            if (pictureBox1.BackgroundImage == DefaulteImg)
            {
                MessageBox.Show("لا يوجد صور للحذف");
                return;
            }


            if ((MessageBox.Show("هل تريد حذف الصورة ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                // if (Constants.formIndex == false)//إدخال بيانات
                //   {
                //
                if (picflag == 1)//delete img1////////////////////////////////////
                {
                    if (img2 == null)
                    {
                        pictureBox1.BackgroundImage = DefaulteImg;
                        img1 = null;
                        picflag = 0;
                    }
                    else
                    {
                        pictureBox1.BackgroundImage = ByteToImage(img2);
                        ///img1 = null;
                        img1 = img2;
                        img2 = null;

                        picflag = 1;//2

                    }
                }///////////////////////////////////////////////////////
                else if (picflag == 2)//delete img2
                {
                    if (img1 == null)
                    {
                        pictureBox1.BackgroundImage = DefaulteImg;
                        img2 = null;
                        picflag = 0;
                    }
                    else
                    {
                        pictureBox1.BackgroundImage = ByteToImage(img1);
                        img2 = null;
                        picflag = 1;
                    }
                }
            }
            else
            {

            }
        }

        private void PrintImg_btn_Click(object sender, EventArgs e)
        {
            if (pictureBox1.BackgroundImage == DefaulteImg)
            {
                MessageBox.Show("لا يوجد صورة لطباعة");
                return;
            }
            if ((MessageBox.Show("هل تريد طباعة المرفق ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                PrintDialog printDlg = new PrintDialog();
                PrintDocument printDoc = new PrintDocument();
                // printDoc.DocumentName = pictureBox1.BackgroundImage.ToString();
                printDoc.PrintPage += new PrintPageEventHandler(Tmpdoc_Print);
                printDlg.Document = printDoc;
                printDlg.AllowSelection = true;
                printDlg.AllowSomePages = true;
                //Call ShowDialog  
                if (printDlg.ShowDialog() == DialogResult.OK) printDoc.Print();
            }
        }
        private void Tmpdoc_Print(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(pictureBox1.BackgroundImage, e.PageBounds);
        }
        public static Bitmap ByteToImage(byte[] blob)//convert byte to image
        {
            if (blob != null)
            {
                MemoryStream ms = new MemoryStream();
                byte[] pdata = blob;
                ms.Write(pdata, 0, Convert.ToInt32(pdata.Length));
                Bitmap bm = new Bitmap(ms, false);
                ms.Dispose();
                return bm;
            }
            else
            {
                return null;
            }

        }

        private void TXT_StockName_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }  //--> OPEN CONNECTION
                pictureBox3.Image = null;
                Image1 = "";
                Image2 = "";
                picflag = 0;
                string query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where STOCK_NO_NAM = @a or BIAN_TSNIF = @a";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@a", (TXT_StockName_Search.Text));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        TXT_GR.Text = dr["STOCK_NO_G"].ToString();
                        TXT_StockNoAll.Text = dr["STOCK_NO_ALL"].ToString();
                        SearchImage1(TXT_StockNoAll.Text);
                        SearchImage2(TXT_StockNoAll.Text);



                        //   VirtualQuan = Convert.ToDouble(dr["VirtualQuan"].ToString());
                        if (dr["VirtualQuan"].ToString() == "")
                        {
                            VirtualQuan = 0;
                        }
                        else
                        {
                            VirtualQuan = Convert.ToDouble(dr["VirtualQuan"].ToString());

                        }
                        ///

                        if (dr["Quan"].ToString() == "")
                        {
                            LockedQuan = 0;
                        }
                        else
                        {


                            LockedQuan = Convert.ToDouble(dr["Quan"].ToString());

                        }
                        LockedQuan = LockedQuan - VirtualQuan;
                        Num_LockedQuan.Text = LockedQuan.ToString();
                        ////
                        TXT_PartNo.Text = dr["PartNO"].ToString();
                        TXT_R1.Text = dr["STOCK_NO_R1"].ToString();
                        TXT_R2.Text = dr["STOCK_NO_R2"].ToString();
                        TXT_R3.Text = dr["STOCK_NO_R3"].ToString();
                        TXT_StockName.Text = dr["STOCK_NO_NAM"].ToString();
                        TXT_Stockian.Text = dr["BIAN_TSNIF"].ToString();
                        TXT_Unit.Text = dr["Unit"].ToString();
                        Num_Quan.Text = dr["Quan"].ToString();
                        if ((dr["SafeAmount"].ToString()) == "1")
                        {
                            checkBox1.Checked = true;
                        }
                        else
                        {
                            checkBox1.Checked = false;
                        }

                        if ((dr["StrategeAmount"].ToString()) == "1")
                        {
                            checkBox2.Checked = true;
                        }
                        else
                        {
                            checkBox2.Checked = false;
                        }
                        //  checkBox1.Checked =(int)(dr["SafeAmount"].ToString());
                        //   checkBox2.Checked= int(dr["StrategeAmount"].ToString());
                        Num_Min.Text = dr["MinAmount"].ToString();
                        Num_Max.Text = dr["MaxAmount"].ToString();
                        Edara_cmb.SelectedValue = dr["CodeEdara"].ToString();
                        LUser.Text = dr["LUser"].ToString();
                        LDate.Text = dr["LDate"].ToString();
                        AddEditFlag = 2;//ADDNEW

                    }
                }
                else
                {
                    MessageBox.Show("من فضلك تاكد من التصنيف");

                }
                dr.Close();
            }
        }
    }

}
