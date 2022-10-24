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
    public partial class FEdafaMakhzania_F : Form
    {
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
        AutoCompleteStringCollection TasnifNameColl = new AutoCompleteStringCollection(); //empn

        AutoCompleteStringCollection UnitColl = new AutoCompleteStringCollection(); //empn
        AutoCompleteStringCollection TalbColl = new AutoCompleteStringCollection(); //empn
              AutoCompleteStringCollection EdafaColl = new AutoCompleteStringCollection(); //empn
              AutoCompleteStringCollection TypeColl = new AutoCompleteStringCollection(); //empn
        public FEdafaMakhzania_F()
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


            HelperClass.comboBoxFiller(Cmb_FY, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
            HelperClass.comboBoxFiller(Cmb_FY2, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);


            // dataGridView1.Parent = panel1;
            //dataGridView1.Dock = DockStyle.Bottom;
            AddEditFlag = 0;
            DisableControls();
            /*
            if (Constants.Amrshera_F == false)
            {
                //panel7.Visible = true;
                panel2.Visible = false;
               // panel7.Dock = DockStyle.Top;
            }
            else if (Constants.Amrshera_F == true)
            {
                panel2.Visible = true;
              // panel7.Visible = false;
                panel2.Dock = DockStyle.Top;
            }
            else { }*/
            //------------------------------------------

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
          //
          //  string cmdstring = "select Amrshraa_No from   T_Awamershraa where  AmrSheraa_sanamalia='" +Cmb_FY+"'";
            string cmdstring = "select distinct(Amrshraa_No) from   T_Estlam where  (Sign3 is not null )and AmrSheraa_sanamalia='" + Cmb_FY + "'";
            
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
            //string cmdstring = "";
            //SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            //  cmdstring = "select (Amrshraa_No) from  T_Awamershraa where (Sign3 is not null) and AmrSheraa_sanamalia=@FY   order by  Amrshraa_No";

         //   Cmb_CType.SelectedIndexChanged += new EventHandler(Cmb_CType_SelectedIndexChanged);
            Cmb_CType.SelectedIndexChanged -= new EventHandler(Cmb_CType_SelectedIndexChanged);
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
         //   TXT_Momayz.Text = Cmb_CType.SelectedValue.ToString();



            ////////////////////////////////////////////////
            Constants.closecon();





            /////////////////////////////////////////////////////////////
       //   CMB_Sadr.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //  CMB_Sadr.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //  CMB_Sadr.AutoCompleteCustomSource = TasnifColl;
/*
            string cmdstring5 = "select distinct(Edafa_No) from   T_Edafa where Edafa_FY='" + Cmb_FY2 + "'";

            SqlCommand cmd5 = new SqlCommand(cmdstring5, con);
            SqlDataReader dr5 = cmd5.ExecuteReader();
            //---------------------------------
            if (dr5.HasRows == true)
            {
                while (dr5.Read())
                {
                    edafacoll.Add(dr5["Edafa_No"].ToString());
                    //TasnifNameColl.Add(dr["Stock_No_Nam"].ToString());

                }
            }
            dr5.Close();



            TXT_EdafaNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TXT_EdafaNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TXT_EdafaNo.AutoCompleteCustomSource = edafacoll;*/

            con.Close();
        }
        private void Getdata(string cmd)
        {
            dataadapter = new SqlDataAdapter(cmd, con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataadapter.Fill(table);
            dataGridView1.DataSource = table;

            dataGridView1.Columns["Amrshraa_No"].HeaderText = "رقم أمر الشراء";//col0
            dataGridView1.Columns["Amrshraa_No"].Visible = false;
            dataGridView1.Columns["Amrshraa_No"].Width = 60;
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

            dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col6
            dataGridView1.Columns["Bnd_No"].ReadOnly = true;

            dataGridView1.Columns["Bnd_No"].Width = 50;
            dataGridView1.Columns["CodeEdara"].HeaderText = "كود ادارة";//col7
            dataGridView1.Columns["CodeEdara"].Visible = false;
            dataGridView1.Columns["NameEdara"].HeaderText = "الادارة الطالبة";//col8
            dataGridView1.Columns["NameEdara"].Visible = false;
            dataGridView1.Columns["BndMwazna"].HeaderText = "بند موازنة";//col9
            dataGridView1.Columns["BndMwazna"].ReadOnly = true;
            dataGridView1.Columns["BndMwazna"].Width = 50;
            dataGridView1.Columns["Quan"].HeaderText = "الكمية";//col10
            dataGridView1.Columns["Quan"].ReadOnly = true;
            dataGridView1.Columns["Quan"].Width = 50;
          dataGridView1.Columns["Quan2"].HeaderText = "الكمية الواردة";//col11
          dataGridView1.Columns["Quan2"].DefaultCellStyle.BackColor =Color.SandyBrown;
        //  dataGridView1.Columns["Quan2"].DefaultHeaderCellType.b

            dataGridView1.Columns["Quan2"].ReadOnly =false;
       
            dataGridView1.Columns["Unit"].HeaderText = "الوحدة";//col12
            dataGridView1.Columns["Unit"].ReadOnly = true;
            dataGridView1.Columns["Unit"].Width = 50;

            dataGridView1.Columns["Bayan"].HeaderText = "بيان المهمات";//col13
            dataGridView1.Columns["Bayan"].ReadOnly = true;
            dataGridView1.Columns["Makhzn"].HeaderText = "مخزن";//col14
            dataGridView1.Columns["Makhzn"].ReadOnly = true;
            dataGridView1.Columns["Makhzn"].Visible = false;
            dataGridView1.Columns["Rakm_Tasnif"].HeaderText = "رقم التصنيف";//col15
            dataGridView1.Columns["Rakm_Tasnif"].ReadOnly = true;
            dataGridView1.Columns["Rased_After"].HeaderText = "رصيد بعد";//col16
            dataGridView1.Columns["Rased_After"].ReadOnly = true;
            dataGridView1.Columns["Rased_After"].Width = 50;
            dataGridView1.Columns["UnitPrice"].HeaderText = "سعر الوحدة";//col17
            dataGridView1.Columns["UnitPrice"].ReadOnly = true;
           dataGridView1.Columns["TotalPrice"].HeaderText = "الثمن الاجمالى";//col18
           dataGridView1.Columns["TotalPrice"].ReadOnly = true;
           dataGridView1.Columns["ApplyDareba"].HeaderText = "تطبق الضريبة";//col19
           dataGridView1.Columns["ApplyDareba"].ReadOnly = true;
         //    DataColumn  dc = new DataColumn("ApplyDareba", typeof(bool));

            // dataGridView1.Columns[dc].HeaderText = "";

           dataGridView1.Columns["Darebapercent"].HeaderText = "نسبة الضريبة";//col20
           dataGridView1.Columns["Darebapercent"].ReadOnly = true;

           //    dataGridView1.Columns["Darebapercent"].Type = DataGridViewCheckBoxCell;


           dataGridView1.Columns["TotalPriceAfter"].HeaderText = "السعر الاجمالى ";//col21
           dataGridView1.Columns["TotalPriceAfter"].ReadOnly = true;
           dataGridView1.Columns["EstlamFlag"].HeaderText ="تم الاستلام ";//col22
           dataGridView1.Columns["EstlamFlag"].Visible = false;

           dataGridView1.Columns["EstlamDate"].HeaderText = "تاريخ الاستلام ";//col23
           dataGridView1.Columns["EstlamDate"].Visible= false;


           dataGridView1.Columns["LessQuanFlag"].HeaderText = "يوجد عجز ";//col24
           dataGridView1.Columns["LessQuanFlag"].Visible = false;
           dataGridView1.Columns["LessQuanFlag"].DefaultCellStyle.BackColor = Color.Aqua;
           dataGridView1.Columns["NotIdenticalFlag"].HeaderText = "مطابق/غير مطابق ";
     
           dataGridView1.Columns["LessQuanFlag"].DefaultCellStyle.BackColor = Color.BlueViolet;
           if (Constants.User_Type == "B")
           {
               dataGridView1.Columns["LessQuanFlag"].ReadOnly = true;
               dataGridView1.Columns["NotIdenticalFlag"].ReadOnly = true;//col25
           }

           dataGridView1.Columns["TalbEsdarShickNo"].HeaderText = "رقم طلب الاصدار ";//col26

           // dataGridView1.Columns["TalbEsdarShickNo"].ReadOnly = true ;

           dataGridView1.Columns["ShickNo"].HeaderText = "رقم الشيك ";//col27
           // dataGridView1.Columns["ShickNo"].ReadOnly = true;

           dataGridView1.Columns["ShickDate"].HeaderText = "تاريخ الشيك ";//col28
           // dataGridView1.Columns["ShickDate"].ReadOnly = true;
           dataGridView1.Columns["TalbEsdarShickNo"].Visible = false;
           dataGridView1.Columns["ShickNo"].Visible = false;
           dataGridView1.Columns["ShickDate"].Visible = false;//col28

           dataGridView1.Columns["ExpirationDate"].HeaderText = "تاريخ انتهاء الصلاحية ";//col28
           dataGridView1.Columns["ExpirationDate"].Visible = true;//col2
            dataGridView1.AllowUserToAddRows = true;
          //  decimal total = table.AsEnumerable().Sum(row => row.Field<decimal>("TotalPriceAfter"));
                //    dataGridView1.FooterRow.Cells[1].Text = "Total";
                 //   dataGridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                   // TXT_Egmali.Text = total.ToString("N2");
        }
        ///
        private void Getdata2(string cmd)
        {
            dataadapter = new SqlDataAdapter(cmd, con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataadapter.Fill(table);
            dataGridView1.DataSource = table;

            dataGridView1.Columns["Amrshraa_No"].HeaderText = "رقم أمر الشراء";//col0
            dataGridView1.Columns["Amrshraa_No"].Visible = false;
            // dataGridView1.Columns["TalbTwareed_No"].Width = 60;
          
           
            dataGridView1.Columns["AmrSheraa_sanamalia"].HeaderText = "امر الشراء سنةمالية";//col3

            dataGridView1.Columns["AmrSheraa_sanamalia"].Visible = false;

            dataGridView1.Columns["TalbTwareed_No"].HeaderText = "رقم طلب التوريد";//col4
            dataGridView1.Columns["TalbTwareed_No"].Visible = false;
            dataGridView1.Columns["FYear"].HeaderText = "سنة مالية طلب التوريد";//col5
            dataGridView1.Columns["FYear"].Visible = false;

            dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";//col6
            dataGridView1.Columns["Bnd_No"].ReadOnly = true;

            dataGridView1.Columns["Quan"].HeaderText = "الكمية";//col10
            dataGridView1.Columns["Quan"].ReadOnly = true;
            dataGridView1.Columns["Quan2"].HeaderText = "الكمية الواردة";//col11
            dataGridView1.Columns["Quan2"].DefaultCellStyle.BackColor = Color.SandyBrown;
            //  dataGridView1.Columns["Quan2"].DefaultHeaderCellType.b

            dataGridView1.Columns["Quan2"].ReadOnly = false;

            dataGridView1.Columns["Unit"].HeaderText = "الوحدة";//col12
            dataGridView1.Columns["Unit"].ReadOnly = true;


            dataGridView1.Columns["Bayan"].HeaderText = "بيان المهمات";//col13
            dataGridView1.Columns["Bayan"].ReadOnly = true;
            dataGridView1.Columns["Makhzn"].HeaderText = "مخزن";//col14
            dataGridView1.Columns["Makhzn"].ReadOnly = true;
            dataGridView1.Columns["Rakm_Tasnif"].HeaderText = "رقم التصنيف";//col15
            dataGridView1.Columns["Rakm_Tasnif"].ReadOnly = true;
            dataGridView1.Columns["Rased_After"].HeaderText = "رصيد بعد";//col16
            dataGridView1.Columns["Rased_After"].ReadOnly = true;
            dataGridView1.Columns["UnitPrice"].HeaderText = "سعر الوحدة";//col17
            dataGridView1.Columns["UnitPrice"].ReadOnly = true;
            dataGridView1.Columns["TotalPrice"].HeaderText = "الثمن الاجمالى";//col18
            dataGridView1.Columns["TotalPrice"].ReadOnly = true;
            dataGridView1.Columns["ApplyDareba"].HeaderText = "تطبق الضريبة";//col19
            dataGridView1.Columns["ApplyDareba"].ReadOnly = true;
            //    DataColumn  dc = new DataColumn("ApplyDareba", typeof(bool));

            // dataGridView1.Columns[dc].HeaderText = "";

            dataGridView1.Columns["Darebapercent"].HeaderText = "نسبة الضريبة";//col20
            dataGridView1.Columns["Darebapercent"].ReadOnly = true;

            //    dataGridView1.Columns["Darebapercent"].Type = DataGridViewCheckBoxCell;


            dataGridView1.Columns["TotalPriceAfter"].HeaderText = "السعر الاجمالى ";//col21
            dataGridView1.Columns["TotalPriceAfter"].ReadOnly = true;
            dataGridView1.Columns["EstlamFlag"].HeaderText = "تم الاستلام ";//col22
            dataGridView1.Columns["EstlamFlag"].Visible = false;

            dataGridView1.Columns["EstlamDate"].HeaderText = "تاريخ الاستلام ";//col23
            dataGridView1.Columns["EstlamDate"].Visible = false;


            dataGridView1.Columns["LessQuanFlag"].HeaderText = "يوجد عجز ";//col24

            dataGridView1.Columns["LessQuanFlag"].DefaultCellStyle.BackColor = Color.Aqua;
            dataGridView1.Columns["NotIdenticalFlag"].HeaderText = "مطابق/غير مطابق ";

            dataGridView1.Columns["LessQuanFlag"].DefaultCellStyle.BackColor = Color.BlueViolet;
            if (Constants.User_Type == "B")
            {
                dataGridView1.Columns["LessQuanFlag"].ReadOnly = true;
                dataGridView1.Columns["NotIdenticalFlag"].ReadOnly = true;//col25
            }

            dataGridView1.Columns["TalbEsdarShickNo"].HeaderText = "رقم طلب الاصدار ";//col26

            // dataGridView1.Columns["TalbEsdarShickNo"].ReadOnly = true ;

            dataGridView1.Columns["ShickNo"].HeaderText = "رقم الشيك ";//col27
            // dataGridView1.Columns["ShickNo"].ReadOnly = true;

            dataGridView1.Columns["ShickDate"].HeaderText = "تاريخ الشيك ";//col28
            // dataGridView1.Columns["ShickDate"].ReadOnly = true;
            dataGridView1.Columns["TalbEsdarShickNo"].Visible = false;
            dataGridView1.Columns["ShickNo"].Visible = false;
            dataGridView1.Columns["ShickDate"].Visible = false;//col28

            dataGridView1.AllowUserToAddRows = true;
            //  decimal total = table.AsEnumerable().Sum(row => row.Field<decimal>("TotalPriceAfter"));
            //    dataGridView1.FooterRow.Cells[1].Text = "Total";
            //   dataGridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            // TXT_Egmali.Text = total.ToString("N2");
        }
        ///////////
                private void GetData(int x,string y)
          {
              if (string.IsNullOrWhiteSpace(Cmb_AmrNo.Text))
              {
                  // MessageBox.Show("ادخل رقم التصريح");
                  //  PermNo_text.Focus();
                  return;
              }
              else
              {
                  table.Clear();
                  TableQuery = "SELECT *  FROM [T_BnodAwamershraa] Where Estlamflag=1 and Amrshraa_No = " + x + " and AmrSheraa_sanamalia='" + y + "'";
                  Getdata(TableQuery);
              }

          }
        
               
        private void button1_Click(object sender, EventArgs e)
        {
            Graphics surface = CreateGraphics();
            Pen pen1 = new Pen(Color.Black, 2);
            surface.DrawLine(pen1, 0, 185, 1000, 185);
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


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            /*
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
          public void EnableControls()
        {
          // BTN_ChooseTalb.Enabled = true;
            //BTN_ChooseTalb.Enabled = false;
            TXT_NameMward.Enabled =true;
            Cmb_CType.Enabled = true;
            TXT_Date.Enabled = true;
           

            BTN_Sigm1.Enabled = true;
            BTN_Sign2.Enabled = true;
            BTN_Sign3.Enabled = true;
            Cmb_FY.Enabled = true;
            Cmb_AmrNo.Enabled = true;
            dataGridView1.Enabled = true;
  
        
        }
        public void Input_Reset()
        {
            directflag = 0;
            CH_Direct.Checked = false;
            BTN_Print.Enabled = false;
            Cmb_AmrNo.Text = "";
            Cmb_AmrNo.SelectedIndex = -1;
            Cmb_CType.Text = "";
            Cmb_CType.SelectedIndex = -1;
            Cmb_FY.Text = "";
            TXT_Date.Text = "";
            TXT_EdafaNo.Text = "";
            Cmb_FY2.Text= "";
           // CMB_Sadr.Text = "";
            TXT_Payment.Text = "";
            TXT_Name.Text = "";
            TXT_TaslemDate.Text = "";
            TXT_TaslemPlace.Text = "";
            TXT_Payment.Text = "";
            TXT_Momayz.Text= "";
            TXT_TRNO.Text = "";
            TXT_Edara.Text = "";
            TXT_TalbNo.Text = "";
            TXT_HesabMward1.Text = "";
            TXT_HesabMward2.Text= "";
            TXT_Egmali.Text="";
            TXT_BndMwazna.Text = "";
            Pic_Sign1.Image = null;
            Pic_Sign2.Image = null;
            Pic_Sign3.Image = null;
            Pic_Sign4.Image = null;
            FlagSign1 = 0;
            FlagSign2 = 0;
            FlagSign3 = 0;
            FlagSign4 = 0;


        }
        public void DisableControls()
        {      //BTN_ChooseTalb.Enabled = false;
            dataGridView1.Enabled = false;
            TXT_NameMward.Enabled = false;
            Cmb_AmrNo.Enabled = false;
            Cmb_FY.Enabled = false;

           //  Cmb_CType.Enabled = false;
            TXT_Date.Enabled = false;
            ////  TXT_EdafaNo.Enabled = false;
            //   Cmb_FY2.Enabled = false;
            //   CMB_Sadr.Enabled = false;
            TXT_Payment.Enabled = false;
            TXT_Name.Enabled = false;
            TXT_TaslemDate.Enabled = false;
            TXT_TaslemPlace.Enabled = false;
            TXT_Payment.Enabled = false;
            TXT_TRNO.Enabled = false;
            TXT_Momayz.Enabled = false;
            TXT_Edara.Enabled = false;
            TXT_TalbNo.Enabled = false;
            TXT_HesabMward1.Enabled = false;
            TXT_HesabMward2.Enabled = false;
            TXT_Egmali.Enabled = false;
            TXT_BndMwazna.Enabled = false;
            TXT_AccNo.Enabled = false;
            TXT_MTaklif.Enabled = false;
            TXT_MResp.Enabled = false;
            TXT_Morakba.Enabled = false;
            TXT_Masrof.Enabled = false;
            TXT_Enfak.Enabled = false;
            TXT_Tasnif.Enabled = false;
            TXT_Mobashr.Enabled =false;
            BTN_Sigm1.Enabled = false;
            BTN_Sign2.Enabled = false;
            BTN_Sign3.Enabled = false;
            
        
        }

        public void EnableMalya()
        {
            TXT_AccNo.Enabled = true;
            TXT_MTaklif.Enabled =true;
            TXT_MResp.Enabled = true;
            TXT_Morakba.Enabled = true;
            TXT_Masrof.Enabled = true;
            TXT_Enfak.Enabled = true;
            TXT_Tasnif.Enabled=true;
            TXT_Mobashr.Enabled=true;
            Cmb_CType.Enabled = true;
        }
        private void cleargridview()
        {
            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد اضافة اضافة مخزنية جديدة؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                //btn_print.Enabled = false;

                MessageBox.Show("برجاء اختيار نوع الاضافة المخزنية و السنة المالية");
                EnableControls();
                Input_Reset();
                cleargridview();
                AddEditFlag = 2;
                EditBtn.Enabled = false;
              //  TXT_Edara.Text = Constants.NameEdara;
             //  BTN_ChooseTalb.Enabled = true;
                SaveBtn.Visible = true;

            }
            else
            {
                //do nothing
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
                    BTN_Print.Enabled = false;
                    AddEditFlag = 1;
                    Addbtn.Enabled = false;
                    TNO = Cmb_AmrNo.Text;
                    FY = Cmb_FY.Text;
                    FY2 = Cmb_FY2.Text;
                    MNO = TXT_EdafaNo.Text;
                    SaveBtn.Visible = true;
                    var button = (Button)sender;
                    if (button.Name == "EditBtn")
                    {
                        if (Constants.User_Type == "B" && Constants.UserTypeB == "Finance")
                        {
                            EnableMalya();
                            BTN_Sign1.Enabled = false;
                            BTN_Sign2.Enabled = false;
                            BTN_Sign3.Enabled = false;
                        }
                        else
                        {
                            EnableControls();
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign2.Enabled = true;
                            BTN_Sign3.Enabled = true;
                        }
                       
                    }
                    else if (button.Name == "EditBtn2")
                    {
                        if (Constants.User_Type == "B" && Constants.UserTypeB == "Finance")
                        {
                            EnableMalya();
                        }
                        else
                        {
                            //BTN_Sign1.Enabled = true;
                            BTN_Sign2.Enabled = true;
                            BTN_Sign3.Enabled = true;
                        }
                       
                    }
                }

            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
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

        private void Cmb_FY_SelectedIndexChanged(object sender, EventArgs e)
        {

            ///////////////////////////////////////
            Constants.opencon();
            Cmb_AmrNo.SelectedIndexChanged -= new EventHandler(Cmb_AmrNo_SelectedIndexChanged);
            Cmb_AmrNo.DataSource = null;
            Cmb_AmrNo.Items.Clear();
            //string cmdstring3 = "SELECT  Amrshraa_No from T_Awamershraa  where  Sign3 is not null and AmrSheraa_sanamalia='" + Cmb_FY.Text + "' order by  Amrshraa_No";
            string cmdstring3 = "select  Amrshraa_No from T_estlam where sign3 is not null and AmrSheraa_sanamalia='" + Cmb_FY.Text + "' and amrshraa_no not in(select Amrshraa_No from T_Edafa group by Amrshraa_No, AmrSheraa_sanamalia)group by Amrshraa_No,AmrSheraa_sanamalia";
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

            ////////////////////////////////////////////////////////


            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring = "";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
    
              //  cmdstring = "select (Amrshraa_No) from  T_Awamershraa where (Sign3 is not null) and AmrSheraa_sanamalia=@FY   order by  Amrshraa_No";

                 cmdstring = "select distinct(Amrshraa_No) from  T_Estlam   where (Sign3 is not null)  order by  Amrshraa_No";//will use cmdstring3


                cmd = new SqlCommand(cmdstring3, Constants.con);

                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
                DataTable dts = new DataTable();

                dts.Load(cmd.ExecuteReader());
                Cmb_AmrNo.DataSource = dts;
                Cmb_AmrNo.ValueMember = "Amrshraa_No";
                Cmb_AmrNo.DisplayMember = "Amrshraa_No";
                Cmb_AmrNo.SelectedIndex = -1;
                Cmb_AmrNo.SelectedIndexChanged += new EventHandler(Cmb_AmrNo_SelectedIndexChanged);
            



            ////////////////////////////////////////////////
            Constants.closecon();











            ////////////////////////////////////////////
             if (AddEditFlag == 2)
            {

                if (string.IsNullOrEmpty(TXT_TRNO.Text))
                {
                    MessageBox.Show("برجاء اختيار نوع الاضافة  اولا");
                    return;
                }
                Constants.opencon();
               
                 //get only finished amrsheraa
                string cmdstring4 = "SELECT  Amrshraa_No from T_Awamershraa  where (Sign3 is not null) and AmrSheraa_sanamalia='" + Cmb_FY.Text + "' order by  Amrshraa_No";
                SqlCommand cmd4 = new SqlCommand(cmdstring4, Constants.con);
                SqlDataReader dr4= cmd4.ExecuteReader();
                //---------------------------------
                if (dr4.HasRows == true)
                {
                    while (dr4.Read())
                    {
                        TalbColl.Add(dr4["Amrshraa_No"].ToString());

                    }
                }

                Constants.closecon();

            }
            //go and get talbTawreed_no for this FYear
            /*
            if (AddEditFlag == 2)//add
            {
                //call sp that get last num that eentered for this MM and this YYYY
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
                string cmdstring = "select max(Amrshraa_No) from  T_Awamershraa where AmrSheraa_sanamalia=@FY ";
                SqlCommand cmd = new SqlCommand(cmdstring, con);

                // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
                
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
                        TXT_AmrNo.Text = flag.ToString();//el rakm el new

                    }

                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    MessageBox.Show(sqlEx.ToString());
                    // flag = (int)cmd.Parameters["@Num"].Value;
                }
            }*/
        
        }
        public void SearchTalb(int x)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring = "select * from T_Awamershraa where Amrshraa_No=@TN and AmrSheraa_sanamalia=@FY";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
            if (x == 1)
            {
                cmd.Parameters.AddWithValue("@TN", Cmb_AmrNo.SelectedValue);
                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
            }
            else
            {
            //    cmd.Parameters.AddWithValue("@TN", Cmb_AmrNo2.Text);
             //   cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            }
            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);


            SqlDataReader dr = cmd.ExecuteReader();
           
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {

                    Cmb_FY.Text = dr["AmrSheraa_sanamalia"].ToString();
                   // Cmb_FY2.Text = dr["monaksa_sanamalia"].ToString();
                    Cmb_AmrNo.Text = dr["Amrshraa_No"].ToString(); 
                 //   TXT_EdafaNo.Text = dr["Monaksa_No"].ToString();
                    TXT_Momayz.Text = dr["Momayz"].ToString();
                   
                    TXT_Edara.Text = dr["NameEdara"].ToString();
                    TXT_Date.Text = dr["Date_amrshraa"].ToString();
                   // CMB_Sadr.Text = dr["Sadr_To"].ToString();
                    TXT_BndMwazna.Text = dr["Bnd_Mwazna"].ToString();
                    TXT_Payment.Text = dr["Payment_Method"].ToString();
                    TXT_TaslemDate.Text = dr["Date_Tslem"].ToString();
                    TXT_TaslemPlace.Text = dr["Mkan_Tslem"].ToString();
                    TXT_Name.Text = dr["Shick_Name"].ToString();
                    TXT_HesabMward1.Text = dr["Hesab_Mward"].ToString();
                    TXT_HesabMward2.Text = dr["Hesab_Mward"].ToString();
                    TXT_Egmali.Text = dr["Egmali"].ToString();

               /*     string s1 = dr["Sign1"].ToString();
                    string s2 = dr["Sign2"].ToString();
                    string s3 = dr["Sign3"].ToString();

                    //dr.Close();


                    if (s1 == "1")
                    {
                        string p = Constants.RetrieveSignature("1", "3");
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string

                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "1"]).Image = Image.FromFile(@p);
                            FlagSign1 = 1;

                        }
                    }
                    if (s2 == "1")
                    {
                        string p = Constants.RetrieveSignature("2", "3");
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string

                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "2"]).Image = Image.FromFile(@p);
                            FlagSign2 = 1;

                        }
                    }
                    if (s3 == "1")
                    {
                        string p = Constants.RetrieveSignature("3", "3");
                        if (p != "")
                        {
                            //   Pic_Sign1
                            //	"Pic_Sign1"	string

                            ((PictureBox)this.panel1.Controls["Pic_Sign" + "3"]).Image = Image.FromFile(@p);
                            FlagSign3 = 1;

                        }
                    }*/
                }
            }
            else
            {
                MessageBox.Show("من فضلك تاكد من رقم امر الشراء المراد اضافته");

            }
            dr.Close();


            //  string query1 = "SELECT  [TalbTwareed_No] ,[FYear] ,[Bnd_No],[RequestedQuan],[Unit],[BIAN_TSNIF] ,[STOCK_NO_ALL],[Quan] ,[ArrivalDate] FROM [T_TalbTawreed_Benod] where  [TalbTwareed_No]=@T and [FYear]=@F ";
            //  SqlCommand cmd1 = new SqlCommand(query1, Constants.con);
            //  cmd1.Parameters.AddWithValue("@T",Cmb_TalbNo2.Text);
            //  cmd1.Parameters.AddWithValue("@F", Cmb_FYear2.Text);


            // DT.Clear();بلثف
            // DT.Load(cmd1.ExecuteReader());
            // cleargridview();
            GetData(Convert.ToInt32(Cmb_AmrNo.SelectedValue), Cmb_FY.Text);
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
        private void TXT_AmrNo_KeyDown(object sender, KeyEventArgs e)
        {
          //  if (e.KeyCode == Keys.Enter && AddEditFlag == 2)
         //   {
                
             //   GetData(Convert.ToInt32(TXT_AmrNo.Text), Cmb_FY.Text);

        //    }
           if (e.KeyCode == Keys.Enter && AddEditFlag ==2)
            {
                cleargridview();
                SearchTalb(1);
            }
        }

        private void BTN_ChooseTalb_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Cmb_FY.Text))
            {
                MessageBox.Show("من فضلك اختار السنة المالية لامر الشراء");
                return;
            }
            if (string.IsNullOrEmpty(Cmb_FY2.Text))
            {
                MessageBox.Show("من فضلك اختار السنة المالية للمناقصة");
                return;
            }
            if (string.IsNullOrEmpty(Cmb_AmrNo.Text))
            {
                MessageBox.Show("من فضلك اختار رقم لامر الشراء");
                return;
            }
            if (string.IsNullOrEmpty(TXT_EdafaNo.Text))
            {
                MessageBox.Show("من فضلك اختار رقم المناقصة");
                return;
            }




            Amrsheraa_PopUp popup = new Amrsheraa_PopUp();
          // popup.Show();
       

           // Show testDialog as a modal dialog and determine if DialogResult = OK.
           if (popup.ShowDialog(this) == DialogResult.OK)
           {
               if (popup.dataGridView1.SelectedRows.Count > 0)
               {
                   foreach (DataGridViewRow row in popup.dataGridView1.SelectedRows)
                   {
                   //   table.ImportRow(((DataTable)popup.dataGridView1.DataSource).Rows[row.Index]);
                      //   {
                       r = dataGridView1.Rows.Count - 1;

                       rowflag = 1;
                       DataRow newRow = table.NewRow();

                       // Add the row to the rows collection.
                       //   table.Rows.Add(newRow);
                       table.Rows.InsertAt(newRow, r);

                       dataGridView1.DataSource = table;
                      dataGridView1.Rows[r].Cells[0].Value = Cmb_AmrNo.Text.ToString();
                      dataGridView1.Rows[r].Cells[1].Value = TXT_EdafaNo.Text.ToString();
                      dataGridView1.Rows[r].Cells[2].Value = Cmb_FY2.Text.ToString();
                      dataGridView1.Rows[r].Cells[3].Value = Cmb_FY.Text.ToString();

                      dataGridView1.Rows[r].Cells[4].Value = row.Cells[2].Value;
                      dataGridView1.Rows[r].Cells[5].Value = row.Cells[0].Value;
                      dataGridView1.Rows[r].Cells[6].Value = row.Cells[1].Value;
                      dataGridView1.Rows[r].Cells[7].Value= popup.TXT_Edara.Text.ToString();

                      dataGridView1.Rows[r].Cells[8].Value = popup.TXT_Edara.Text.ToString();
                      dataGridView1.Rows[r].Cells[9].Value = popup.TXT_BndMwazna.Text.ToString();
                      dataGridView1.Rows[r].Cells[10].Value = row.Cells[3].Value;
                      dataGridView1.Rows[r].Cells[11].Value = row.Cells[4].Value;
                      dataGridView1.Rows[r].Cells[12].Value =row.Cells[5].Value;
                      dataGridView1.Rows[r].Cells[14].Value =row.Cells[6].Value;
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

                       
                   }
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
            if (AddEditFlag == 2)
            {
                if (FlagSign1 != 1)
                {
                    MessageBox.Show("من فضلك تاكد من توقيع الاضافة المخزنية");
                    return;
                }

                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if(!row.IsNewRow)
                    {
                string cmdstring = "exec SP_InsertEdafa @p1,@p2,@p3,@p4,@p44,@p444,@p5,@p55,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p34 out";
                SqlCommand cmd = new SqlCommand(cmdstring, con);

                cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_EdafaNo.Text));
                cmd.Parameters.AddWithValue("@p3", Convert.ToInt32(Cmb_AmrNo.Text));
                cmd.Parameters.AddWithValue("@p2",(Cmb_FY2.Text));
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
                cmd.Parameters.AddWithValue("@p12",DBNull.Value);

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
                cmd.Parameters.AddWithValue("@p20",  DBNull.Value);//dareba


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
                    flag = (int)cmd.Parameters["@p34"].Value;
                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    MessageBox.Show(sqlEx.ToString());
                    flag = (int)cmd.Parameters["@p34"].Value;
                }
                
              
            }}
                if (executemsg == true && flag == 1)
                {
                    string st = "exec SP_DeleteEdaraAlarm @p2,@p3,@p4";
                    SqlCommand cmd1 = new SqlCommand(st, con);

                   // cmd1.Parameters.AddWithValue("@p1", row.Cells[7].Value);
           

                    cmd1.Parameters.AddWithValue("@p2", Convert.ToInt32(TXT_EdafaNo.Text));
          
                    cmd1.Parameters.AddWithValue("@p3", (Cmb_FY2.Text));
                    cmd1.Parameters.AddWithValue("@p4", (TXT_TRNO.Text));
                    cmd1.ExecuteNonQuery();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if(!row.IsNewRow)
                        {
                        st = "exec SP_SendEdaraAlarm @p1,@p11,@p111,@p2,@p3,@p33,@p4,@p5,@p6,@p7";
                        cmd1 = new SqlCommand(st, con);

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
                        SqlCommand cmd = new SqlCommand(cmdstring, con);

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

                    DisableControls();
                    // BTN_PrintPerm.Visible = true;
                    SaveBtn.Visible = false;
                    AddEditFlag = 0;
                    EditBtn.Enabled = true;
                }
                else if (executemsg == true && flag == 2)
                {
                    MessageBox.Show("تم إدخال رقم الاضافة المخزنية  من قبل  ! ");
                }
                con.Close();
            
            }
            else if (AddEditFlag == 1)
            {
              UpdateEdafa();
            }
        }
        public void UpdateEdafa()
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string cmdstring = "Exec SP_DeleteEdafa @TNO,@FY,@TRNO,@aot output";

            SqlCommand cmd = new SqlCommand(cmdstring, con);

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
               // MessageBox.Show("تم الحذف بنجاح");
           //   Input_Reset();
            }
         
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                      cmdstring = "exec SP_UpdateEdafa @fff,@p1old,@p2old,@p1,@p2,@p3,@p4,@p44,@p444,@p5,@p55,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p34 out,@p35";
                       cmd = new SqlCommand(cmdstring, con);
                       cmd.Parameters.AddWithValue("@fff", FlagSign3);
                        cmd.Parameters.AddWithValue("@p1old",MNO);
                        cmd.Parameters.AddWithValue("@p2old",FY2);
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
                            cmd.Parameters.AddWithValue("@p21",FlagEmpn4);

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
                            flag = (int)cmd.Parameters["@p34"].Value;
                        }
                        catch (SqlException sqlEx)
                        {
                            executemsg = false;
                            MessageBox.Show(sqlEx.ToString());
                            flag = (int)cmd.Parameters["@p34"].Value;
                        }

                     
                    }
                }
                if (FlagSign3 == 1)
                {

                    // InsertTrans();
                    // UpdateQuan();

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

                    if (FlagSign4 != 1  && Constants.UserTypeB !="Finance")
                    {
                        string st = "exec SP_DeleteEdaraAlarm @p2,@p3,@p4";
                        SqlCommand cmd1 = new SqlCommand(st, con);

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
                                cmd1 = new SqlCommand(st, con);

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





                    MessageBox.Show("تم التعديل بنجاح  ! ");

                    DisableControls();
                    // BTN_PrintPerm.Visible = true;
                    SaveBtn.Visible = false;
                    AddEditFlag = 0;
                    Addbtn.Enabled = true;
                }
                else if (executemsg == true && flag == 2)
                {
                    MessageBox.Show("تم إدخال رقم الاضافة المخزنية  من قبل  ! ");
                }
                con.Close();

        
     


        }
        private void BTN_Sign2_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 || FlagSign4 != 1)
            {
                MessageBox.Show("يرجى التاكد من التوقعات السابقة اولا");
                return;
            }
            Empn2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع المخازن", "");
           
            Sign2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع المخازن", "");
           
            if (Sign2 != "" && Empn2 !="")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
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
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }

        private void BTN_Sigm1_Click(object sender, EventArgs e)
        {
            Empn1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع مخزن الاستلام", "");
          
            Sign1= Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مخزن الاستلام", "");
          
            if (Sign1 != "" && Empn1!="")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("1", "5", Sign1, Empn1);
                if (result.Item3 == 1)
                {
                    Pic_Sign1.Image = Image.FromFile(@result.Item1);

                    FlagSign1 = result.Item2;
                    FlagEmpn1 = Empn1;
                }
                else
                {
                    FlagSign1= 0;
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

        private void BTN_Sign3_Click(object sender, EventArgs e)
        {
            if (FlagSign2 != 1 || FlagSign1 != 1 || FlagSign4 !=1)
            {
                MessageBox.Show("يرجى التاكد من التوقعات السابقة اولا");
                return;
            }
            Empn3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "اعتماد مدير عام م المخازن", "");
           
            Sign3= Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "اعتماد مدير عام م المخازن", "");
           
            if (Sign3 != "" && Empn3 !="")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
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
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }

        private void Pic_Sign3_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign2_Click(object sender, EventArgs e)
        {

        }

        private void BTN_Sign1_Click(object sender, EventArgs e)
        {

        }

        private void Pic_Sign1_Click(object sender, EventArgs e)
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

        }

        private void TXT_TalbNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void TXT_BndMwazna_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
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

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void Cmb_FY2_SelectedIndexChanged(object sender, EventArgs e)
        {
             if (AddEditFlag == 0)
            {
               
                Constants.opencon();
              
               TXT_EdafaNo.AutoCompleteMode = AutoCompleteMode.None;
                TXT_EdafaNo.AutoCompleteSource = AutoCompleteSource.None; ;
                //////////   string cmdstring3 = "SELECT [Edafa_No] from T_Edafa where Edafa_FY='" + Cmb_FY2.Text + "'";
                  string cmdstring3 = "SELECT [Edafa_No] from T_Edafa where Edafa_FY='" + Cmb_FY2.Text + "'and TR_NO='" +TXT_TRNO.Text + "'";
                SqlCommand cmd3 = new SqlCommand(cmdstring3, Constants.con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                //---------------------------------
                if (dr3.HasRows == true)
                {
                    while (dr3.Read())
                    {
                        EdafaColl.Add(dr3["Edafa_No"].ToString());

                    }
                }
              
                TXT_EdafaNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                TXT_EdafaNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                TXT_EdafaNo.AutoCompleteCustomSource = EdafaColl;
                Constants.closecon();

            }
            //go and get talbTawreed_no for this FYear
            if (AddEditFlag == 2)//add
            {
                //call sp that get last num that eentered for this MM and this YYYY
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (String.IsNullOrEmpty(TXT_TRNO.Text) == true)
                {
                    MessageBox.Show("يجب اختيار نوع الاضافة المخزنية");
                    return;
                }
                // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
                string cmdstring = "select  ( COALESCE(MAX( Edafa_No), 0))  from  T_Edafa where Edafa_FY=@FY  and TR_NO=@TRNO ";
                SqlCommand cmd = new SqlCommand(cmdstring, con);

                // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
                cmd.Parameters.AddWithValue("@FY", Cmb_FY2.Text);
                cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text);
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
                        TXT_EdafaNo.Text = flag.ToString();//el rakm el new

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

        private void TXT_AmrNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
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

        }

        private void Cmb_ِAmrNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchTalb(2);
        }

        private void Cmb_FYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring = "select (Amrshraa_No) from  T_Awamershraa where AmrSheraa_sanamalia=@FY  order by  Amrshraa_No";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
         //   cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
         ///   cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);


            DataTable dts = new DataTable();

            dts.Load(cmd.ExecuteReader());
         ///   Cmb_AmrNo2.DataSource = dts;
          //  Cmb_AmrNo2.ValueMember = "Amrshraa_No";
          //  Cmb_AmrNo2.DisplayMember = "Amrshraa_No";
          ///  Cmb_AmrNo2.SelectedIndex = -1;
           /// Cmb_AmrNo2.SelectedIndexChanged += new EventHandler(Cmb_ِAmrNo2_SelectedIndexChanged);
            Constants.closecon();
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

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
           
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

        private void TXT_AccNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void BTN_Sign4_Click(object sender, EventArgs e)
        {
            if (FlagSign1 != 1 )//|| FlagSign2!= 1 || FlagSign3 !=1)
            {
                MessageBox.Show("يرجى التاكد من التوقعات السابقة اولا");
                return;
            }
           Empn4 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع الادارة الطالبة", "");
          
            Sign4 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع الادارة الطالبة", "");
          
            if (Sign4 != "" && Empn4 !="")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
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
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }

        private void TXT_EdafaNo_KeyDown(object sender, KeyEventArgs e)
        {
        
            if (e.KeyCode == Keys.Enter && AddEditFlag == 0)
            {
                if (string.IsNullOrEmpty(TXT_TRNO.Text))
                {
                    MessageBox.Show("برجاء اختيار نوع الاضافة المخزنية");
                    return;
                }
                cleargridview();
                SearchEdafa(1);
            }
        }
        ////////////////////
        public void SearchEdafa(int x)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();
            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring = "select * from   T_Edafa where Edafa_No=@TN and Edafa_FY=@FY  and TR_NO=@TRNO";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
            if (x == 1)
            {
                cmd.Parameters.AddWithValue("@TN", TXT_EdafaNo.Text);
                cmd.Parameters.AddWithValue("@FY", Cmb_FY2.Text);
                cmd.Parameters.AddWithValue("@TRNO", TXT_TRNO.Text);
            }
            else
            {
                //    cmd.Parameters.AddWithValue("@TN", Cmb_AmrNo2.Text);
                //   cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            }
            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);


            SqlDataReader dr = cmd.ExecuteReader();
            string amrno="";
            string amrsana="";
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {

                    amrsana= dr["AmrSheraa_sanamalia"].ToString();
           
                  amrno = dr["Amrshraa_No"].ToString();
                  TXT_TRNO.Text = dr["TR_NO"].ToString();
                  if (TXT_TRNO.Text.ToString() == "")
                  {

                  }
                  else
                  {
                      Cmb_CType.SelectedValue = TXT_TRNO.Text.ToString();
                  }

                  string s1 = dr["Sign1"].ToString();
                  string s2 = dr["Sign2"].ToString();
                  string s3 = dr["Sign3"].ToString();
                  string s4 = dr["Sign4"].ToString();

                  //dr.Close();


                  if (s1 != "")
                  {
                      string p = Constants.RetrieveSignature("1", "5",s1);
                      if (p != "")
                      {
                          //   Pic_Sign1
                          //	"Pic_Sign1"	string

                          Ename1 = p.Split(':')[1];
                          wazifa1 = p.Split(':')[2];
                          pp = p.Split(':')[0];

                            ((PictureBox)this.tableLayoutPanel1.Controls["panel15"].Controls["Pic_Sign" + "1"]).Image = Image.FromFile(@pp);

                          FlagSign1 = 1;
                          FlagEmpn1 = s1;
                            ((PictureBox)this.tableLayoutPanel1.Controls["panel15"].Controls["Pic_Sign" + "1"]).BackColor = Color.Green;
                          toolTip1.SetToolTip(Pic_Sign1, Ename1 + Environment.NewLine + wazifa1);
                      }

                  }
                  else
                  {
                        ((PictureBox)this.tableLayoutPanel1.Controls["panel15"].Controls["Pic_Sign" + "1"]).BackColor = Color.Red;
                  }
                  if (s2 != "")
                  {
                      string p = Constants.RetrieveSignature("2", "5",s2);
                      if (p != "")
                      {
                          //   Pic_Sign1
                          //	"Pic_Sign1"	string

                          Ename2 = p.Split(':')[1];
                          wazifa2 = p.Split(':')[2];
                          pp = p.Split(':')[0];

                            ((PictureBox)this.tableLayoutPanel1.Controls["panel16"].Controls["Pic_Sign" + "2"]).Image = Image.FromFile(@pp);

                          FlagSign2 = 1;
                          FlagEmpn2 = s2;
                            ((PictureBox)this.tableLayoutPanel1.Controls["panel16"].Controls["Pic_Sign" + "2"]).BackColor = Color.Green;
                          toolTip1.SetToolTip(Pic_Sign2, Ename2 + Environment.NewLine + wazifa2);
                      }

                  }
                  else
                  {
                        ((PictureBox)this.tableLayoutPanel1.Controls["panel16"].Controls["Pic_Sign" + "2"]).BackColor = Color.Red;
                  }
                  if (s3 != "")
                  {
                      string p = Constants.RetrieveSignature("3", "5",s3);
                      if (p != "")
                      {
                          //   Pic_Sign1
                          //	"Pic_Sign1"	string
                          Ename3 = p.Split(':')[1];
                          wazifa3 = p.Split(':')[2];
                          pp = p.Split(':')[0];

                            ((PictureBox)this.tableLayoutPanel1.Controls["panel17"].Controls["Pic_Sign" + "3"]).Image = Image.FromFile(@pp);

                          FlagSign3 = 1;
                          FlagEmpn3 = s3;
                            ((PictureBox)this.tableLayoutPanel1.Controls["panel17"].Controls["Pic_Sign" + "3"]).BackColor = Color.Green;
                          toolTip1.SetToolTip(Pic_Sign3, Ename3 + Environment.NewLine + wazifa3);
                      }

                  }
                  else
                  {
                        ((PictureBox)this.tableLayoutPanel1.Controls["panel17"].Controls["Pic_Sign" + "3"]).BackColor = Color.Red;
                  }
                  if (s4 != "")
                  {
                      string p = Constants.RetrieveSignature("3", "1",s4);
                      if (p != "")
                      {
                          //   Pic_Sign1
                          //	"Pic_Sign1"	string
                          Ename4 = p.Split(':')[1];
                          wazifa4 = p.Split(':')[2];
                          pp = p.Split(':')[0];

                            ((PictureBox)this.tableLayoutPanel1.Controls["panel18"].Controls["Pic_Sign" + "4"]).Image = Image.FromFile(@pp);

                          FlagSign4 = 1;
                          FlagEmpn4 = s4;
                            ((PictureBox)this.tableLayoutPanel1.Controls["panel18"].Controls["Pic_Sign" + "4"]).BackColor = Color.Green;
                          toolTip1.SetToolTip(Pic_Sign4, Ename4 + Environment.NewLine + wazifa4);
                      }

                  }
                  else
                  {
                        ((PictureBox)this.tableLayoutPanel1.Controls["panel18"].Controls["Pic_Sign" + "4"]).BackColor = Color.Red;
                  }


                }
            }
            else
            {
                MessageBox.Show("من فضلك تاكد من رقم الاضافة المخزنية");
                return;

            }
            dr.Close();
            //////////////////////////////////

            cmdstring = "select * from  T_Awamershraa where  Amrshraa_No=@TN and AmrSheraa_sanamalia=@FY";
            cmd = new SqlCommand(cmdstring, Constants.con);
            if (x == 1)
            {
                cmd.Parameters.AddWithValue("@TN", amrno);
                cmd.Parameters.AddWithValue("@FY", amrsana);
            }
            else
            {
                //    cmd.Parameters.AddWithValue("@TN", Cmb_AmrNo2.Text);
                //   cmd.Parameters.AddWithValue("@FY", Cmb_FYear2.Text);
            }
            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);


            DataTable dtTalabTawreed = new DataTable();





            SqlConnection sqlConnction = new SqlConnection(Constants.constring);
            SqlDataAdapter daTalabTawreed = new SqlDataAdapter(@"select * from  T_Awamershraa where  Amrshraa_No="+ amrno + " and AmrSheraa_sanamalia='"+ amrsana + "'", sqlConnction);
            sqlConnction.Open();
            daTalabTawreed.Fill(dtTalabTawreed);
            sqlConnction.Close();



            dr = cmd.ExecuteReader();

            if (dtTalabTawreed.Rows.Count > 0)
            {
                DataRow row = dtTalabTawreed.Rows[0];
                //while (dr.Read())
                //{

                    Cmb_FY.Text = row["AmrSheraa_sanamalia"].ToString();
                    //   Cmb_FY2.Text = dr["monaksa_sanamalia"].ToString();
                    TXT_AmrNo.Text = row["Amrshraa_No"].ToString();
                    Cmb_AmrNo.Text = row["Amrshraa_No"].ToString();


                    //  TXT_EdafaNo.Text = dr["Monaksa_No"].ToString();
                    TXT_Momayz.Text = row["Momayz"].ToString();

                    TXT_Edara.Text = row["NameEdara"].ToString();
                    TXT_Date.Text = row["Date_amrshraa"].ToString();
                    // CMB_Sadr.Text = dr["Sadr_To"].ToString();
                    TXT_BndMwazna.Text = row["Bnd_Mwazna"].ToString();
                    TXT_Payment.Text = row["Payment_Method"].ToString();
                    TXT_TaslemDate.Text = row["Date_Tslem"].ToString();
                    TXT_TaslemPlace.Text = row["Mkan_Tslem"].ToString();
                    TXT_Name.Text = row["Shick_Name"].ToString();
                    TXT_HesabMward1.Text = row["Hesab_Mward"].ToString();
                    TXT_HesabMward2.Text = row["Hesab_Mward"].ToString();
                    TXT_Egmali.Text = row["Egmali"].ToString();

                    /*     string s1 = dr["Sign1"].ToString();
                         string s2 = dr["Sign2"].ToString();
                         string s3 = dr["Sign3"].ToString();

                         //dr.Close();


                         if (s1 == "1")
                         {
                             string p = Constants.RetrieveSignature("1", "3");
                             if (p != "")
                             {
                                 //   Pic_Sign1
                                 //	"Pic_Sign1"	string

                                 ((PictureBox)this.panel1.Controls["Pic_Sign" + "1"]).Image = Image.FromFile(@p);
                                 FlagSign1 = 1;

                             }
                         }
                         if (s2 == "1")
                         {
                             string p = Constants.RetrieveSignature("2", "3");
                             if (p != "")
                             {
                                 //   Pic_Sign1
                                 //	"Pic_Sign1"	string

                                 ((PictureBox)this.panel1.Controls["Pic_Sign" + "2"]).Image = Image.FromFile(@p);
                                 FlagSign2 = 1;

                             }
                         }
                         if (s3 == "1")
                         {
                             string p = Constants.RetrieveSignature("3", "3");
                             if (p != "")
                             {
                                 //   Pic_Sign1
                                 //	"Pic_Sign1"	string

                                 ((PictureBox)this.panel1.Controls["Pic_Sign" + "3"]).Image = Image.FromFile(@p);
                                 FlagSign3 = 1;

                             }
                         }*/

                    BTN_Print.Enabled = true;
                //}
            }
            else
            {
                MessageBox.Show("من فضلك تاكد من رقم الاضافة المخزنية");

                BTN_Print.Enabled = false;



            }
            dr.Close();


            //  string query1 = "SELECT  [TalbTwareed_No] ,[FYear] ,[Bnd_No],[RequestedQuan],[Unit],[BIAN_TSNIF] ,[STOCK_NO_ALL],[Quan] ,[ArrivalDate] FROM [T_TalbTawreed_Benod] where  [TalbTwareed_No]=@T and [FYear]=@F ";
            //  Sq

            //////////////////////////////////////
            GetData(Convert.ToInt32(Cmb_AmrNo.Text), Cmb_FY.Text);
            if (DT.Rows.Count == 0)
            {


            }
            else
            {


            }
            // searchbtn1 = false;
            //  DataGridViewReset();

            Constants.closecon();
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

            else
            { //No
                //----
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
                    Constants.MangerName = (string)cmd.Parameters["@aot"].Value;
                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    MessageBox.Show(sqlEx.ToString());
  Constants.MangerName =(string)cmd.Parameters["@aot"].Value;
                }
                Constants.closecon();
                //GET NAME MODER 3AM


                FReports F = new FReports();
                F.Show();

            }

            else
            { //No
                //----
            }
        }

        private void TXT_EdafaNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Constants.validatenumberkeypress(sender, e);
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
            else
            {

            }
        }
        public void SP_UpdateSignatures(int x, DateTime D1, DateTime? D2 = null)
        {
            string cmdstring = "Exec  SP_UpdateSignDates  @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
            SqlCommand cmd = new SqlCommand(cmdstring, con);

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
        private void Cmb_AmrNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public int CheckDirect70()
        {
            Constants.opencon();
            string cmdstring = "exec sp_CheckDirect70  @A,@F,@aot out";
            SqlCommand cmd = new SqlCommand(cmdstring, con);

            cmd.Parameters.AddWithValue("@A", Convert.ToInt32(Cmb_AmrNo.SelectedValue.ToString()));
            cmd.Parameters.AddWithValue("@F", Cmb_FY2.Text.ToString());
           
            cmd.Parameters.Add("@aot", SqlDbType.Int, 32);  //-------> output parameter
            cmd.Parameters["@aot"].Direction = ParameterDirection.Output;

            int flag=0;

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
                // MessageBox.Show("تم الحذف بنجاح");
                //   Input_Reset();
            }
            return flag;
         
        }
        private void Cmb_AmrNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (AddEditFlag == 2 && Cmb_AmrNo.SelectedValue.ToString() != "")
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
//
             //   GetData(Convert.ToInt32(Cmb_AmrNo.SelectedValue), Cmb_FY.Text);
                cleargridview();
                SearchTalb(1);

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
            if (Cmb_CType.SelectedValue.ToString() == "")
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

 

        private void label25_Click_1(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label26_Click_1(object sender, EventArgs e)
        {

        }

        private void TXT_TRNO_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("مميز متند ", TXT_TRNO);
        }



        //////////////////////////////////
    }
}
