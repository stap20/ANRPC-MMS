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
    public partial class EzonTahwel_PopUp : Form
    {
          public SqlConnection con;//sql conn for anrpc_sms db

        public DataTable DT = new DataTable();
        private BindingSource bindingsource1 = new BindingSource();
        private string TableQuery;
        private int AddEditFlag;
        public string BM;
        public string BM2;
        public Boolean executemsg;
        public double totalprice;
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
        public int r;
        public int rowflag = 0;
     //  public string TableQuery;
        
        AutoCompleteStringCollection TasnifColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TasnifNameColl = new AutoCompleteStringCollection(); //empn

        AutoCompleteStringCollection UnitColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TalbColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection MsgColl = new AutoCompleteStringCollection(); //empn
        public EzonTahwel_PopUp()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
        public void Input_Reset()
        {
            TXT_AmrNo.Text = "";
            TXT_AmrSana.Text = "";
            Cmb_Msg.Text = "";
            CMB_FYear.Text = "";
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();


        }
        private void Getdata(string cmd)
        {
            dataadapter = new SqlDataAdapter(cmd,Constants.con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
      
            dataadapter.Fill(table);
            dataGridView1.DataSource = table;
           // Edafa_No,Edafa_FY,Amrshraa_No,AmrSheraa_sanamalia,TalbTwareed_No,FYear,Bnd_No,QuanArrived
            dataGridView1.Columns["Edafa_No"].HeaderText = "رقم الرسالة";//col0
            dataGridView1.Columns["Edafa_No"].ReadOnly = true;
            // dataGridView1.Columns["TalbTwareed_No"].Width = 60;
            dataGridView1.Columns["Edafa_FY"].HeaderText = "السنة المالية";//col1
            dataGridView1.Columns["Edafa_FY"].ReadOnly = true;

            dataGridView1.Columns["Amrshraa_No"].HeaderText = "رقم امرالشراء";//col2
            dataGridView1.Columns["Amrshraa_No"].ReadOnly = true;

            dataGridView1.Columns["AmrSheraa_sanamalia"].HeaderText = "السنة المالية";//col3
            dataGridView1.Columns["AmrSheraa_sanamalia"].ReadOnly = true;


            dataGridView1.Columns["TalbTwareed_No"].HeaderText = "رقم الطلب التوريد";//col4
            dataGridView1.Columns["TalbTwareed_No"].ReadOnly = true;


            dataGridView1.Columns["FYear"].HeaderText = "السنة المالية";//col5
            dataGridView1.Columns["FYear"].ReadOnly = true;

            dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col6
            dataGridView1.Columns["Bnd_No"].ReadOnly = true;
           
            dataGridView1.Columns["QuanArrived"].HeaderText = "الكمية";//col7
            dataGridView1.Columns["QuanArrived"].ReadOnly = true;


           // b.Rakm_Tasnif,b.Unit,b.Bayan,b.Rased_After,b.UnitPrice

            dataGridView1.Columns["Rakm_Tasnif"].HeaderText = "رقم التصنيف";//col8
            dataGridView1.Columns["Rakm_Tasnif"].ReadOnly = true;

            dataGridView1.Columns["Unit"].HeaderText = "الوحدة";//col9
            dataGridView1.Columns["Unit"].ReadOnly = true;

            dataGridView1.Columns["Bayan"].HeaderText = "البيان";//col10
            dataGridView1.Columns["Bayan"].ReadOnly = true;


            dataGridView1.Columns["Rased_After"].HeaderText = "الرصيد";//col11
            dataGridView1.Columns["Rased_After"].ReadOnly = true;

            dataGridView1.Columns["UnitPrice"].HeaderText = "القيمة";//col12
            dataGridView1.Columns["UnitPrice"].ReadOnly = true;


            dataGridView1.AllowUserToAddRows = true;
          //  dataGridView1.Enabled = false;

        }
        private void GetData(string x, string y)
        {
            if (string.IsNullOrWhiteSpace(Cmb_Msg.Text))
            {
                // MessageBox.Show("ادخل رقم التصريح");
                //  PermNo_text.Focus();
                return;
            }
            else
            {
                table.Clear();
                //TableQuery = "SELECT  Edafa_No,Edafa_FY,e.Amrshraa_No,e.AmrSheraa_sanamalia,e.TalbTwareed_No,e.FYear,e.Bnd_No,e.QuanArrived,b.Rakm_Tasnif,b.Unit,b.Bayan,b.Rased_After,b.UnitPrice FROM View_BnodEzonTahwel Where Edafa_No= '" + x + "' and Edafa_FY='" + y +"'" ;
               TableQuery = "SELECT * FROM View_BnodEzonTahwel Where Edafa_No= '" + x + "' and Edafa_FY='" + y + "'";
                Getdata(TableQuery);
            }

        }
        private void TalbTawred_Load(object sender, EventArgs e)
        {
            // dataGridView1.Parent = panel1;
            //dataGridView1.Dock = DockStyle.Bottom;
            CMB_FYear.Text = BM2;
            Cmb_Msg.Text = BM;
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
           


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics surface = CreateGraphics();
            Pen pen1 = new Pen(Color.Black, 2);
            surface.DrawLine(pen1, 0, 185, 1000, 185);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {/*
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
            surface.Dispose();*/
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CMB_FYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            Constants.opencon();
            Cmb_Msg.SelectedIndexChanged -= new EventHandler(Cmb_Msg_SelectedIndexChanged);


            Cmb_Msg.AutoCompleteMode = AutoCompleteMode.None;
            // TXT_msg.AutoCompleteSource = AutoCompleteSource.None; ;
            //tlbat el tawred bs el 5alst hia el a2dr a3ml mnha amr sheraa
            string cmdstring3 = "select Edafa_No FROM [ANRPC_Inventory_foriegn_v2].[dbo].[T_Edafa]  where Edafa_FY='" + CMB_FYear.Text + "'";
            SqlCommand cmd3 = new SqlCommand(cmdstring3, Constants.con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            //---------------------------------
            if (dr3.HasRows == true)
            {
                while (dr3.Read())
                {
                    MsgColl.Add(dr3["Edafa_No"].ToString());
                    //   CMB_TalbNo2.d

                }
            }
            dr3.Close();
            Cmb_Msg.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Cmb_Msg.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Cmb_Msg.AutoCompleteCustomSource = MsgColl;
            ////////////////////////////////////////////////////////

            DataTable dts = new DataTable();
            dts.Load(cmd3.ExecuteReader());

            Cmb_Msg.DataSource = dts;
            Cmb_Msg.ValueMember = "Edafa_No";
            Cmb_Msg.DisplayMember = "Edafa_No";
            Cmb_Msg.SelectedIndex = -1;
            Cmb_Msg.SelectedIndexChanged += new EventHandler(Cmb_Msg_SelectedIndexChanged);


            Constants.closecon();
        }
    
        private void Addbtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (!row.IsNewRow && row.Selected)
                {
                   // MessageBox.Show(row.Index.ToString());
                ///////////////  AmrSheraa.dataGridView1.rows(count).cells(i).value = dataGridView1.Rows(selectedIndex).Cells(i).Value;
                }
            }




            //////////////////
            /*
            Form2 obj = new Form2();
            obj.ShowDialog();
            if (obj.dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in obj.dataGridView1.SelectedRows)
                {
                    dtData.ImportRow(((DataTable)obj.dataGridView1.DataSource).Rows[row.Index]);
                }
                dtData.AcceptChanges();
            }
            dataGridView1.DataSource = dtData;  */
        }
        private void cleargridview()
        {
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            
            
            dataGridView1.Refresh();

        }
        public void SearchTalb(int x)
          {
               //call sp that get last num that eentered for this MM and this YYYY
              Constants.opencon();
              // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
              string cmdstring = "select * FROM [ANRPC_Inventory_foriegn_v2].[dbo].[T_Edafa]  where Edafa_FY=@FY  and Edafa_No=@FNO";
              SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
             
                  cmd.Parameters.AddWithValue("@FNO", Cmb_Msg.Text);
                  cmd.Parameters.AddWithValue("@FY", CMB_FYear.Text);
              
            
              // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
            

              SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    CMB_FYear.Text = dr["Edafa_FY"].ToString();
                    TXT_AmrNo.Text = dr["Amrshraa_No"].ToString();
                    TXT_AmrSana.Text = dr["AmrSheraa_sanamalia"].ToString();
                    TXT_Mward.Text= dr["MwardName"].ToString();
                    //dr.Close();


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

                    GetData((Cmb_Msg.Text), CMB_FYear.Text);


                }
            }

            else
            {
                MessageBox.Show("من فضلك تاكد من رقم الرسالة");

            }
              dr.Close();

  
            //  string query1 = "SELECT  [TalbTwareed_No] ,[FYear] ,[Bnd_No],[RequestedQuan],[Unit],[BIAN_TSNIF] ,[STOCK_NO_ALL],[Quan] ,[ArrivalDate] FROM [T_TalbTawreed_Benod] where  [TalbTwareed_No]=@T and [FYear]=@F ";
            //  SqlCommand cmd1 = new SqlCommand(query1, Constants.con);
           //  cmd1.Parameters.AddWithValue("@T",Cmb_TalbNo2.Text);
           //  cmd1.Parameters.AddWithValue("@F", Cmb_FYear2.Text);


            // DT.Clear();
            // DT.Load(cmd1.ExecuteReader());
             // cleargridview();
          
            
              if (DT.Rows.Count == 0)
              {
                  //  MessageBox.Show("لا يوجد حركات لهذا الموظف");
                  // Input_Reset();
                  //  
              }
              else
              {


              }
              // searchbtn1 = false;
              //  DataGridViewReset();

              Constants.closecon();
          }

        private void TXT_TalbNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cleargridview();
                SearchTalb(1);
            }
        }

        private void TXT_TalbNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void TXT_TalbNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Constants.validatenumberkeypress(sender,e);
        }

       
       

       

        private void CMB_TalbNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cleargridview();
            SearchTalb(2);
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {

        }

        private void Cmb_Msg_SelectedIndexChanged(object sender, EventArgs e)
        {
            Constants.opencon();
            string query = "select Amrshraa_No,AmrSheraa_sanamalia,TalbTwareed_No,FYear,MwardName FROM [ANRPC_Inventory_foriegn_v2].[dbo].[T_Edafa]  where Edafa_FY=@FY  and Edafa_No=@ENO";

            SqlCommand cmd = new SqlCommand(query,Constants.con);

            // query = "select [STOCK_NO_ALL],PartNO ,[STOCK_NO_NAM],[STOCK_NO_G],[STOCK_NO_R1],[STOCK_NO_R2],[STOCK_NO_R3],[BIAN_TSNIF],[Unit],[Quan],VirtualQuan   ,[MinAmount],[MaxAmount] ,[StrategeAmount] ,[SafeAmount],[CodeEdara],[NameEdara],[LUser],[LDate] from T_Tsnif where PartNO= @a";
            //cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FY", (CMB_FYear.Text));
            cmd.Parameters.AddWithValue("@ENO", (Cmb_Msg.Text));

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    TXT_AmrNo.Text = dr["Amrshraa_No"].ToString();
                    TXT_AmrSana.Text = dr["AmrSheraa_sanamalia"].ToString();
                   // TXT_TalbNo.Text = dr["TalbTwareed_No"].ToString();
                 //   TXT_TalbSana.Text = dr["FYear"].ToString();
                    TXT_Mward.Text = dr["MwardName"].ToString();
                    Constants.MwardName = TXT_Mward.Text;
                }

            }
            else
            {
                MessageBox.Show("من فضلك تاكد من رقم الرسالة");

            }
            dr.Close();
            SearchTalb(1);
        }
    }
    
}
