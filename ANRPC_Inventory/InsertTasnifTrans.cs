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
    public partial class InsertTasnifTrans : Form
    { 
        public SqlConnection con;//sql conn for anrpc_sms db
         AutoCompleteStringCollection TasnifColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection UnitColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection PartColl = new AutoCompleteStringCollection(); //empn
         public DataTable table = new DataTable();
        public SqlDataAdapter dataadapter;
        public DataSet ds = new DataSet();
        public double VirtualQuan;
        public double LockedQuan;
        public InsertTasnifTrans()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void TalbTawred_Load(object sender, EventArgs e)
        {
           // dataGridView1.Parent = panel1;
            //dataGridView1.Dock = DockStyle.Bottom;
             con = new SqlConnection(Constants.constring);

            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            //*******************************************
            // ******    AUTO COMPLETE
            //*******************************************
            string cmdstring = "";
            if (Constants.User_Type == "A")
            {
                cmdstring = "select STOCK_NO_ALL,PartNO from T_Tsnif  where (StatusFlag in (0,1,2)) and  CodeEdara=" + Constants.CodeEdara;
                 dataGridView1.Visible = false;
                 BTN_Print.Visible = false;
                 Cmb_StockNo.Visible = true;
                 TXT_StockNoAll.Visible = false;
                 BTN_Search.Visible = false;
                 Cmb_PartNO.Visible = true;
                 TXT_PartNo.Visible = false;
                 BTN_Search2.Visible = false;

            }
            else if(Constants.User_Type=="B")
            {
                cmdstring = "select STOCK_NO_ALL,PartNO from T_Tsnif where  (StatusFlag in (0,1,2)) ";
                 Cmb_StockNo.Visible = false;
                 Cmb_PartNO.Visible = false;
            }
            else
            {
                cmdstring = "select STOCK_NO_ALL,PartNO from T_Tsnif where  (StatusFlag in (0,1,2)) ";
            }
          //  string cmdstring = "select STOCK_NO_ALL from T_Tsnif where CodeEdara="+Constants.CodeEdara;
            SqlCommand cmd = new SqlCommand(cmdstring, con);
            SqlDataReader dr = cmd.ExecuteReader();
            //---------------------------------
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    TasnifColl.Add(dr["STOCK_NO_ALL"].ToString());
                    PartColl.Add(dr["PartNO"].ToString());

                }
            }
            dr.Close();
            ///////////////////////////
            //string query = "SELECT CodeEdara , NameEdara FROM Edarat";
            cmd = new SqlCommand(cmdstring, con);
         //   DataTable dtts = new DataTable();
        //    dtts.Load(cmd.ExecuteReader());

      //      Cmb_StockNo.DataSource = dtts;
        //  Cmb_StockNo.ValueMember = "STOCK_NO_ALL";
         

        //    Cmb_PartNO.DataSource = dtts;
         //   Cmb_PartNO.ValueMember = "PartNO";
         //   Cmb_StockNo.SelectedIndexChanged += new EventHandler(Cmb_StockNo_SelectedIndexChanged);

        //    Cmb_PartNO.SelectedIndexChanged += new EventHandler(Cmb_PartNO_SelectedIndexChanged);
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
            ///////////////////////////////////edara combo//////////////

            string query = "SELECT CodeEdara , NameEdara FROM Edarat";
            cmd = new SqlCommand(query, con);
            DataTable dts = new DataTable();
            dts.Load(cmd.ExecuteReader());

           // Edara_cmb.DataSource = dts;
           // Edara_cmb.ValueMember = "CodeEdara";
          //  Edara_cmb.DisplayMember = "NameEdara";
            ///////////////////////////////////////////////////////////
            //
            ////////////////////////////////////////////////
            TXT_StockNoAll.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_StockNoAll.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_StockNoAll.AutoCompleteCustomSource = TasnifColl;

            TXT_PartNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_PartNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_PartNo.AutoCompleteCustomSource = PartColl;

            TXT_Unit.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_Unit.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_Unit.AutoCompleteCustomSource = UnitColl;

            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics surface = CreateGraphics();
            Pen pen1 = new Pen(Color.Black, 2);
            surface.DrawLine(pen1, 0, 185, 1000, 185);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics surface = e.Graphics;
            Pen pen1 = new Pen(Color.Black, 2);
            surface.DrawLine(pen1, panel1.Location.X + 4,  4, panel1.Location.X + 4, panel1.Location.Y + panel1.Size.Height); // Left Line
            surface.DrawLine(pen1, panel1.Size.Width - 4, 4, panel1.Size.Width - 4, panel1.Location.Y + panel1.Size.Height); // Right Line
            //---------------------------
            surface.DrawLine(pen1, 4,4, panel1.Location.X + panel1.Size.Width - 4,4); // Top Line
            surface.DrawLine(pen1, 4, panel1.Size.Height -1, panel1.Location.X + panel1.Size.Width - 4, panel1.Size.Height -1); // Bottom Line
       
            //---------------------------
            // Middle_Line
            //-------------
            surface.DrawLine(pen1, ((panel1.Size.Width) / 2) + 4, 4, ((panel1.Size.Width) / 2) + 4, panel1.Location.Y + panel1.Size.Height); // Left Line
            surface.DrawLine(pen1, 4, 38, panel1.Location.X + panel1.Size.Width - 4, 40); // Top Line
            surface.Dispose();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Getdata(SqlCommand cmd)
        {
            dataadapter = new SqlDataAdapter(cmd);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataadapter.Fill(table);
            dataGridView1.DataSource = table;
            //SELECT [EznSarf_No],[FYear],[CodeEdara],[NameEdara],[Date],[Momayz],[RequestedFor],[Responsiblecenter],[TR_NO] ,[Sign1],[Sign2],[Sign3],[Sign4] ,[Sign5],[LUser] ,[LDate] FROM [dbo].[T_EznSarf]

            dataGridView1.Columns["Date"].HeaderText = "التاريخ";
            dataGridView1.Columns["Date"].Width = 80;
            dataGridView1.Columns["Date"].ContextMenuStrip = contextMenuStrip1;
            // dataGridView1.Columns["TalbTwareed_No"].Width = 60;
            dataGridView1.Columns["ser_doc"].HeaderText = "المستند";
            dataGridView1.Columns["ser_doc"].Width = 60;
            dataGridView1.Columns["ser_doc"].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns["ward"].HeaderText = "وارد";
            dataGridView1.Columns["ward"].Width =70;
            dataGridView1.Columns["ward"].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns["sadr"].HeaderText = "منصرف";
            dataGridView1.Columns["sadr"].Width = 70;
            dataGridView1.Columns["sadr"].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns["availablequan"].HeaderText = "رصيد";
            dataGridView1.Columns["availablequan"].Width = 70;
            dataGridView1.Columns["availablequan"].ContextMenuStrip = contextMenuStrip1;
        }
        private void cleargridview()
        {
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            table.Clear();

        }
        private void BTN_Search_Click(object sender, EventArgs e)
        {
               if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }  //--> OPEN CONNECTION
               if (string.IsNullOrEmpty(TXT_StockNoAll.Text))
               {
                   MessageBox.Show("من فضلك اختار رقم التصريح");
                   return;
               }
               cleargridview();
                string query = "select [STOCK_NO_ALL] ,PartNO,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where STOCK_NO_ALL= @a";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@a", (TXT_StockNoAll.Text));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                     //   TXT_GR.Text = dr["STOCK_NO_G"].ToString();
                    //    TXT_R1.Text = dr["STOCK_NO_R1"].ToString();
                     //   TXT_R2.Text = dr["STOCK_NO_R2"].ToString();
                     //   TXT_R3.Text = dr["STOCK_NO_R3"].ToString();
                        TXT_StockName.Text = dr["BIAN_TSNIF"].ToString();//STOCK_NO_NAM
                    TXT_PartNo.Text = dr["PartNO"].ToString();

                    VirtualQuan = 0;
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
                      Num_LockedQuan.Text = LockedQuan.ToString();
                     //   Num_LockedQuan.Text = VirtualQuan.ToString();
                        
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
                        Num_Min.Text = dr["MinAmount"].ToString();
                        Num_Max.Text = dr["MaxAmount"].ToString();

                        string group = TXT_StockNoAll.Text;
                        group = group.Substring(0, 2);
                        TXT_STOCKNO.Text = Constants.GetStock(group);
                    }
                       // AddEditFlag = 2;//ADDNEW
                        ///////get trans//////////////////
                        string st = "exec SP_SearchTasnifTrans  @st";
                        cmd = new SqlCommand(st, con);
                       // cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@st",TXT_StockNoAll.Text);
                        cleargridview();
                        Getdata(cmd);
                      //  SqlDataAdapter adp = new SqlDataAdapter(cmd);
                     //   DataTable dt = new DataTable();
                      //  adp.Fill(dt);
                       //dataGridView2.DataSource = dt;
                      //dataGridView1.DataBind();
                    





                        /////////////////////////
        }
                else
                {
                    MessageBox.Show("من فضلك تاكد من التصنيف");

                }
                dr.Close();
            }
    
        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
        
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void contextMenuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "printTool")
            {
                if ((MessageBox.Show("هل تريد طباعة بطاقة الصنف ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
                {
                    Constants.Unit = TXT_Unit.Text;
                    Constants.TasnifNo = TXT_StockNoAll.Text;
                    Constants.TasnifName = TXT_StockName.Text;
                    Constants.Desc = TXT_Stockian.Text;
                    if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "")
                    {
                        Constants.Quan = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    }
                    else
                    {
                        Constants.Quan = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    }
                   
                    Constants.RakmEdafa = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    Constants.DateEdafa = dataGridView1.CurrentRow.Cells[0].Value.ToString();

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

        }

        private void BTN_EstlamReport_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد طباعة تقرير حركة صنف؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_StockNoAll.Text))
                {
                    MessageBox.Show("يجب اختيار رقم التصنيف المراد طباعتها اولا");
                    return;
                }
                else
                {

                    Constants.FormNo = 9;
                    Constants.STockNoALL = (TXT_StockNoAll.Text);

                    Constants.STockname = TXT_StockName.Text;
                    Constants.STockBian = TXT_Stockian.Text;
                    Constants.STockno = TXT_STOCKNO.Text;
                    Constants.stockmax = Num_Max.Value.ToString();
                    Constants.STockmin = Num_Min.Value.ToString();
                    Constants.Stockunit = TXT_Unit.Text;
                    Constants.Stocklocation = TXT_StockPlace.Text;


                    FReports F = new FReports();
                    F.Show();
                }
            }
            else
            {

            }
        }

        private void Cmb_StockNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }  //--> OPEN CONNECTION
            if (string.IsNullOrEmpty(Cmb_StockNo.SelectedValue.ToString()))
            {
                //  MessageBox.Show("من فضلك اختار ");
                //   return;
            }
            else
            {


                cleargridview();
                string query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where STOCK_NO_ALL= @a";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@a", (Cmb_StockNo.SelectedValue));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        //   TXT_GR.Text = dr["STOCK_NO_G"].ToString();
                        //    TXT_R1.Text = dr["STOCK_NO_R1"].ToString();
                        //   TXT_R2.Text = dr["STOCK_NO_R2"].ToString();
                        //   TXT_R3.Text = dr["STOCK_NO_R3"].ToString();
                       // Cmb_PartNO.Text = dr["PartNO"].ToString();
                        TXT_StockName.Text = dr["STOCK_NO_NAM"].ToString();
                        TXT_Stockian.Text = dr["BIAN_TSNIF"].ToString();
                        TXT_Unit.Text = dr["Unit"].ToString();
                        Num_Quan.Text = dr["Quan"].ToString();
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
                        Num_LockedQuan.Text = LockedQuan.ToString();
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

                        string group = Cmb_StockNo.Text;
                        group = group.Substring(0, 2);
                        TXT_STOCKNO.Text = Constants.GetStock(group);
                    }
                    // AddEditFlag = 2;//ADDNEW
                    ///////get trans//////////////////
                    string st = "exec SP_SearchTasnifTrans  @st";
                    cmd = new SqlCommand(st, con);
                    // cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@st", Cmb_StockNo.Text);
                    cleargridview();
                    Getdata(cmd);
                    //  SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    //   DataTable dt = new DataTable();
                    //  adp.Fill(dt);
                    //dataGridView2.DataSource = dt;
                    //dataGridView1.DataBind();






                    /////////////////////////
                }
                else
                {
                    MessageBox.Show("من فضلك تاكد من التصنيف");

                }
                dr.Close();
            }
        }
        ///

        private void Cmb_PartNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }  //--> OPEN CONNECTION
            if (string.IsNullOrEmpty(Cmb_PartNO.SelectedValue.ToString()))
            {
                //  MessageBox.Show("من فضلك اختار ");
                //  return;
            }
            else
            {
                cleargridview();
                string query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQUan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where PartNO= @a";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@a", (Cmb_PartNO.SelectedValue));
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        //   TXT_GR.Text = dr["STOCK_NO_G"].ToString();
                        //    TXT_R1.Text = dr["STOCK_NO_R1"].ToString();
                        //   TXT_R2.Text = dr["STOCK_NO_R2"].ToString();
                        //   TXT_R3.Text = dr["STOCK_NO_R3"].ToString();
                        Cmb_StockNo.Text = dr["STOCK_NO_ALL"].ToString();
                        TXT_StockName.Text = dr["STOCK_NO_NAM"].ToString();
                        TXT_Stockian.Text = dr["BIAN_TSNIF"].ToString();
                        TXT_Unit.Text = dr["Unit"].ToString();
                        Num_Quan.Text = dr["Quan"].ToString();
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
                        Num_LockedQuan.Text = LockedQuan.ToString();
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

                        string group = Cmb_StockNo.Text;
                        group = group.Substring(0, 2);
                        TXT_STOCKNO.Text = Constants.GetStock(group);
                    }
                    // AddEditFlag = 2;//ADDNEW
                    ///////get trans//////////////////
                    string st = "exec SP_SearchTasnifTrans  @st";
                    cmd = new SqlCommand(st, con);
                    // cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@st", Cmb_PartNO.Text);
                    cleargridview();
                    Getdata(cmd);
                    //  SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    //   DataTable dt = new DataTable();
                    //  adp.Fill(dt);
                    //dataGridView2.DataSource = dt;
                    //dataGridView1.DataBind();






                    /////////////////////////
                }
                else
                {
                    MessageBox.Show("من فضلك تاكد من التصنيف");

                }

                dr.Close();
            }
        }
        ////

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }  //--> OPEN CONNECTION
            if (string.IsNullOrEmpty(TXT_PartNo.Text))
            {
                MessageBox.Show("من فضلك اختار رقم التصريح");
                return;
            }
            cleargridview();
            string query = "select [STOCK_NO_ALL] ,PartNO,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where PartNO= @a";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@a", (TXT_PartNo.Text));
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    //   TXT_GR.Text = dr["STOCK_NO_G"].ToString();
                    //    TXT_R1.Text = dr["STOCK_NO_R1"].ToString();
                    //   TXT_R2.Text = dr["STOCK_NO_R2"].ToString();
                    //   TXT_R3.Text = dr["STOCK_NO_R3"].ToString();
                    TXT_StockName.Text = dr["STOCK_NO_NAM"].ToString();
                    TXT_PartNo.Text = dr["PartNO"].ToString();
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
                    Num_LockedQuan.Text = LockedQuan.ToString();
                    TXT_Stockian.Text = dr["BIAN_TSNIF"].ToString();
                    TXT_StockNoAll.Text = dr["STOCK_NO_ALL"].ToString();
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
                    Num_Min.Text = dr["MinAmount"].ToString();
                    Num_Max.Text = dr["MaxAmount"].ToString();

                    string group = TXT_StockNoAll.Text;
                    group = group.Substring(0, 2);
                    TXT_STOCKNO.Text = Constants.GetStock(group);
                }
                // AddEditFlag = 2;//ADDNEW
                ///////get trans//////////////////
                string st = "exec SP_SearchTasnifTrans  @st";
                cmd = new SqlCommand(st, con);
                // cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@st", TXT_PartNo.Text);
                cleargridview();
                Getdata(cmd);
                //  SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //   DataTable dt = new DataTable();
                //  adp.Fill(dt);
                //dataGridView2.DataSource = dt;
                //dataGridView1.DataBind();






                /////////////////////////
            }
            else
            {
                MessageBox.Show("من فضلك تاكد من التصنيف");

            }
            dr.Close();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(TXT_StockNoAll.Text))
            {
                MessageBox.Show("من فضلك اختار رقم التصنيف");
                return;
            }
                else if (Quan_Trans.Value == 0)
            {
                MessageBox.Show("من فضلك ادخل الكمية");
                return;
            }
            else if (Cmb_FYear.Text=="")
            {
                MessageBox.Show("من فضلك اختار السنة المالية");
                return;
            }
            else if (TXT_TalbNo.Text == "")
            {
                MessageBox.Show("من فضلك اختار رقم الطلب");
                return;
            }
            else if (radioButton1.Checked && Num_Quan.Value < Quan_Trans.Value)
            {
                MessageBox.Show("الكمية غير متاحة للصرف");
                return;
            }
       
            else   if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("من فضلك اختر نوع الحركة");
                return;
            }
            else if(radioButton1.Checked && Num_Quan.Value<Quan_Trans.Value){
                MessageBox.Show("الكمية غير متاحة للصرف");
                return;
            }
       
            else
            {

                InsertTrans();
                UpdateQuan();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                Quan_Trans.Value = 0;
                TXT_TalbNo.Text = "";
                CMB_Type.Text = "";
                Cmb_FYear.Text = "";
            }
        }


        public void InsertTrans()
        {
            Constants.opencon();

            string cmdstring = "Exec SP_InsertAllTrans @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            //  cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[4].Value));
            //will send rased badl else monsrf
            cmd.Parameters.AddWithValue("@p1", TXT_TalbNo.Text);
            cmd.Parameters.AddWithValue("@p2", Cmb_FYear.Text);
            cmd.Parameters.AddWithValue("@p3", CMB_Type.Text);
            cmd.Parameters.AddWithValue("@p4", CMB_Type.Text);
            cmd.Parameters.AddWithValue("@p5", TXT_StockNoAll.Text);
            cmd.Parameters.AddWithValue("@p6",Quan_Trans.Value);
    

            if (radioButton1.Checked == true)
            {
                cmd.Parameters.AddWithValue("@p7", 1);
            }
            else if (radioButton2.Checked == true)
            {
                cmd.Parameters.AddWithValue("@p7", 2);
            }
            //cmd.Parameters.AddWithValue("@p7", TXT_StockNoAll.Text);
            cmd.Parameters.AddWithValue("@p8",  Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
            cmd.Parameters.AddWithValue("@p9", Constants.User_Name.ToString());
            cmd.Parameters.AddWithValue("@p10", Convert.ToDateTime(DateTime.Now.ToShortDateString()));

            cmd.ExecuteNonQuery();
            MessageBox.Show("تم ادخال الحركة بنجاح");
            Constants.closecon();
        }
        public void UpdateQuan()
        {

           
           
                     Constants.opencon();
          
                    string cmdstring = "Exec SP_UpdateQuanTsnif2 @Quan,@ST,@F";

                    SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                    //  cmd.Parameters.AddWithValue("@Quan", Convert.ToDouble(row.Cells[4].Value));
                    //will send rased badl else monsrf
                    cmd.Parameters.AddWithValue("@Quan",Quan_Trans.Value);
                    cmd.Parameters.AddWithValue("@ST", TXT_StockNoAll.Text);
                    if (radioButton1.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@F", 1);
                    }
                    else if (radioButton2.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@F", 2);
                    }
                   

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم ادخال الحركة بنجاح");
                    Constants.closecon();
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
