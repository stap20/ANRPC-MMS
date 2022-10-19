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
    public partial class Amrsheraa_PopUp : Form
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

        public Amrsheraa_PopUp()
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
            TXT_BndMwazna.Text = "";
            TXT_Edara.Text = "";
            CMB_TalbNo2.Text = "";
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

            dataGridView1.Columns["TalbTwareed_No2"].HeaderText = "رقم طلب التوريد";
            dataGridView1.Columns["TalbTwareed_No2"].ReadOnly = true;
            // dataGridView1.Columns["TalbTwareed_No"].Width = 60;
            dataGridView1.Columns["FYear"].HeaderText = "السنة المالية";
            dataGridView1.Columns["FYear"].ReadOnly = true;
            dataGridView1.Columns["Bnd_No"].HeaderText = "رقم البند";
            dataGridView1.Columns["Bnd_No"].ReadOnly = true;
            dataGridView1.Columns["RequestedQuan"].HeaderText = "الكمية";
            dataGridView1.Columns["RequestedQuan"].ReadOnly = true;
            dataGridView1.Columns["Unit"].HeaderText = "الوحدة";
            dataGridView1.Columns["Unit"].ReadOnly = true;
            dataGridView1.Columns["BIAN_TSNIF"].HeaderText = "بيان الموصفات";
            dataGridView1.Columns["BIAN_TSNIF"].ReadOnly = true;
            dataGridView1.Columns["STOCK_NO_ALL"].HeaderText = "الدليل الرقمى";
            dataGridView1.Columns["STOCK_NO_ALL"].ReadOnly = true;
            dataGridView1.Columns["Quan"].HeaderText = "رصيد المخزن";
            dataGridView1.Columns["Quan"].ReadOnly = true;
            dataGridView1.Columns["ArrivalDate"].HeaderText = "تاريخ وروده";
            dataGridView1.Columns["ArrivalDate"].ReadOnly= true;
            dataGridView1.AllowUserToAddRows = true;
          //  dataGridView1.Enabled = false;

        }
        private void GetData(int x, string y)
        {
            if (string.IsNullOrWhiteSpace(TXT_TalbNo.Text))
            {
                // MessageBox.Show("ادخل رقم التصريح");
                //  PermNo_text.Focus();
                return;
            }
            else
            {
                table.Clear();
                TableQuery = "SELECT  [TalbTwareed_No2] ,[FYear],[Bnd_No],[RequestedQuan],Unit,[BIAN_TSNIF] ,STOCK_NO_ALL,Quan,[ArrivalDate] FROM [T_TalbTawreed_Benod] Where TalbTwareed_No2= " + x + " and Fyear='" + y +"'" /*+ " and BuyMethod='" + BM + "'"*/;
                Getdata(TableQuery);
            }

        }
        private void TalbTawred_Load(object sender, EventArgs e)
        {
            // dataGridView1.Parent = panel1;
            //dataGridView1.Dock = DockStyle.Bottom;

            HelperClass.comboBoxFiller(CMB_FYear, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);

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
               CMB_TalbNo2.SelectedIndexChanged -= new EventHandler(CMB_TalbNo2_SelectedIndexChanged);


               TXT_TalbNo.AutoCompleteMode = AutoCompleteMode.None;
                TXT_TalbNo.AutoCompleteSource = AutoCompleteSource.None; ;
            //tlbat el tawred bs el 5alst hia el a2dr a3ml mnha amr sheraa
                string cmdstring3 = "SELECT [TalbTwareed_No2] from T_TalbTawreed where Mohmat_Sign is not null and TalbTwareed_No2 is not null and FYear='" + CMB_FYear.Text + "'" +" and BuyMethod='"+BM+"'";
                SqlCommand cmd3 = new SqlCommand(cmdstring3, Constants.con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                //---------------------------------
                if (dr3.HasRows == true)
                {
                    while (dr3.Read())
                    {
                        TalbColl.Add(dr3["TalbTwareed_No2"].ToString());
                     //   CMB_TalbNo2.d

                    }
                }
                dr3.Close();
                TXT_TalbNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                TXT_TalbNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                TXT_TalbNo.AutoCompleteCustomSource = TalbColl;
            ////////////////////////////////////////////////////////
               
                DataTable dts = new DataTable();
                dts.Load(cmd3.ExecuteReader());

                CMB_TalbNo2.DataSource = dts;
                CMB_TalbNo2.ValueMember = "TalbTwareed_No2";
                CMB_TalbNo2.DisplayMember = "TalbTwareed_No2" ;
                CMB_TalbNo2.SelectedIndex = -1;
                CMB_TalbNo2.SelectedIndexChanged += new EventHandler(CMB_TalbNo2_SelectedIndexChanged);


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
              string cmdstring = "select * from  T_TalbTawreed where TalbTwareed_No2=@TN and FYear=@FY";
              SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
              if (x == 1)
              {
                  cmd.Parameters.AddWithValue("@TN", TXT_TalbNo.Text);
                  cmd.Parameters.AddWithValue("@FY", CMB_FYear.Text);
              }
              else if (x == 2)
              {
                  cmd.Parameters.AddWithValue("@TN", CMB_TalbNo2.Text);
                  cmd.Parameters.AddWithValue("@FY", CMB_FYear.Text);
              }
            
              // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
            

              SqlDataReader dr = cmd.ExecuteReader();

              if (dr.HasRows == true)
              {
                  while (dr.Read())
                  {
                     CMB_FYear.Text = dr["FYear"].ToString();
                     TXT_TalbNo.Text = dr["TalbTwareed_No2"].ToString();
                     TXT_Edara.Text = dr["NameEdara"].ToString();
                     TXT_CodeEdara.Text = dr["CodeEdara"].ToString();
                      TXT_BndMwazna.Text=dr["BndMwazna"].ToString();
                       string s1=dr["Req_Signature"].ToString();
                      string s2=dr["Confirm_Sign1"].ToString();
                      string s3=dr["Confirm_Sign2"].ToString();
                      string s4=dr["Stock_Sign"].ToString();
                      string s5=dr["Audit_Sign"].ToString();
                      string s6=dr["Mohmat_Sign"].ToString();
                     string s7=dr["CH_Sign"].ToString();
                      //dr.Close();
                   
                
                     if (s1 != "")
                     {
                         string p = Constants.RetrieveSignature("1", "1",s1);
                         if (p != "")
                         {
                             //   Pic_Sign1
                             //	"Pic_Sign1"	string

                          //   ((PictureBox)this.panel1.Controls["Pic_Sign" + "1"]).Image = Image.FromFile(@p);
                             FlagSign1 = 1;
                             FlagEmpn1 = s1;

                         }
                     }
                     if (s2 != "")
                     {
                         string p = Constants.RetrieveSignature("2", "1",s2);
                         if (p != "")
                         {
                             //   Pic_Sign1
                             //	"Pic_Sign1"	string

                            // ((PictureBox)this.panel1.Controls["Pic_Sign" + "2"]).Image = Image.FromFile(@p);
                             FlagSign2= 1;
                             FlagEmpn2 = s2;

                         }
                     }
                     if (s3 != "")
                     {
                         string p = Constants.RetrieveSignature("3", "1",s3);
                         if (p != "")
                         {
                             //   Pic_Sign1
                             //	"Pic_Sign1"	string

                            // ((PictureBox)this.panel1.Controls["Pic_Sign" + "3"]).Image = Image.FromFile(@p);
                             FlagSign3 = 1;
                             FlagEmpn3 = s3;

                         }
                     }
                     if (s4 != "")
                     {
                         string p = Constants.RetrieveSignature("4", "1",s4);
                         if (p != "")
                         {
                             //   Pic_Sign1
                             //	"Pic_Sign1"	string

                     //        ((PictureBox)this.panel1.Controls["Pic_Sign" + "4"]).Image = Image.FromFile(@p);
                             FlagSign4 = 1;
                             FlagEmpn4 = s4;

                         }
                     }

                     if (s5 != "")
                     {
                         string p = Constants.RetrieveSignature("5", "1",s5);
                         if (p != "")
                         {
                             //   Pic_Sign1
                             //	"Pic_Sign1"	string

                     //        ((PictureBox)this.panel1.Controls["Pic_Sign" + "5"]).Image = Image.FromFile(@p);
                             FlagSign5 = 1;
                             FlagEmpn5 = s5;

                         }
     
                     
                  }      
                      if (s6 != "")
                     {
                         string p = Constants.RetrieveSignature("6", "1",s6);
                         if (p != "")
                         {
                             //   Pic_Sign1
                             //	"Pic_Sign1"	string

                           //  ((PictureBox)this.panel1.Controls["Pic_Sign" + "5"]).Image = Image.FromFile(@p);
                             FlagSign6 = 1;
                             FlagEmpn6 = s6;

                         }
     
                     
                  }
                      if (s7 != "")
                      {
                          string p = Constants.RetrieveSignature("7", "1",s7);
                          if (p != "")
                          {
                              //   Pic_Sign1
                              //	"Pic_Sign1"	string

                         //     ((PictureBox)this.panel1.Controls["Pic_Sign" + "7"]).Image = Image.FromFile(@p);
                              FlagSign7 = 1;
                              FlagEmpn7 = s7;

                          }

                      }
                  }
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
                  if (x == 1)
                  {
                      GetData(Convert.ToInt32(TXT_TalbNo.Text), CMB_FYear.Text);
                  }
                  else if (x == 2)
                  {
                      GetData(Convert.ToInt32(CMB_TalbNo2.Text), CMB_FYear.Text);
                  }
                 
              }
               
              else
              {
                  MessageBox.Show("من فضلك تاكد من رقم طلب التوريد");

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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Input_Reset();
            if (radioButton1.Checked)
            {
                BM = "1";
                BM2 = radioButton1.Text;
                CMB_TalbNo2.Enabled = true;
                CMB_FYear.Enabled = true;

            }
            else
            {
                BM = "";
                CMB_TalbNo2.Enabled =false;
                CMB_FYear.Enabled = false;
            }
          
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Input_Reset();
            if (radioButton2.Checked)
            {
                BM = "2";
                BM2 = radioButton2.Text;
                CMB_TalbNo2.Enabled = true;
                CMB_FYear.Enabled = true;
            }
            else
            {
                BM = "";
                CMB_TalbNo2.Enabled = false;
                CMB_FYear.Enabled = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Input_Reset();
            if (radioButton3.Checked)
            {
                BM = "3";
                BM2 = radioButton3.Text;
                CMB_TalbNo2.Enabled = true;
                CMB_FYear.Enabled = true;
            }
            else
            {
                BM = "";
                CMB_TalbNo2.Enabled =false;
                CMB_FYear.Enabled = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Input_Reset();
            if (radioButton4.Checked)
            {
                BM = "4";
                BM2 = radioButton4.Text;
                CMB_TalbNo2.Enabled = true;
                CMB_FYear.Enabled = true;
            }
            else
            {
                BM = "";
                CMB_TalbNo2.Enabled = false;
                CMB_FYear.Enabled = false;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Input_Reset();
            if (radioButton5.Checked)
            {
                BM = "5";
                BM2 = radioButton5.Text;
                CMB_TalbNo2.Enabled = true;
                CMB_FYear.Enabled = true;
            }
            else
            {
                BM = "";
                CMB_TalbNo2.Enabled = false;
                CMB_FYear.Enabled =false;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            Input_Reset();
            if (radioButton6.Checked)
            {
                BM = "6";
                BM2 = radioButton6.Text;
                CMB_TalbNo2.Enabled = true;
                CMB_FYear.Enabled = true;
            }
            else
            {
                BM = "";
                CMB_TalbNo2.Enabled = false;
                CMB_FYear.Enabled = false;
            }


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

        private void CMB_TalbNo2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    
}
