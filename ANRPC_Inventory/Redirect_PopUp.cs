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
    public partial class Redirect_PopUp : Form
    {
          public SqlConnection con;//sql conn for anrpc_sms db

        public DataTable DT = new DataTable();
        private BindingSource bindingsource1 = new BindingSource();
        private string TableQuery;
        private int AddEditFlag;
        public string BM;
        public string BM2;
        public string BM3;
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

        public Redirect_PopUp()
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
           

        }
        private void Getdata(string cmd)
        {
            dataadapter = new SqlDataAdapter(cmd,Constants.con);
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
      
            dataadapter.Fill(table);
         

          
          //  dataGridView1.Enabled = false;

        }
        private void GetData(int x, string y)
        {
           

        }
        private void TalbTawred_Load(object sender, EventArgs e)
        {
           // dataGridView1.Parent = panel1;
            //dataGridView1.Dock = DockStyle.Bottom;
           
           


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

      ///////////////////////////////////////////
              

           
    
        private void Addbtn_Click(object sender, EventArgs e)
        {
           




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
         
            
           // dataGridView1.Refresh();

        }
     

        private void TXT_TalbNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cleargridview();
              //  SearchTalb(1);
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
                BM =(DateTime.Now.ToShortDateString());
                BM2 = radioButton1.Text;
                BM3 = "605";
              
            }
            else
            {
                BM = "";
             
            }
          
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Input_Reset();
            if (radioButton2.Checked)
            {
                BM = (DateTime.Now.ToShortDateString());
                BM2 = radioButton2.Text;
                BM3 = "590";
            }
            else
            {
                BM = "";
               
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Input_Reset();
            if (radioButton3.Checked)
            {
                BM = (DateTime.Now.ToShortDateString());
                BM2 = radioButton3.Text;
                BM3 = "664";
               
            }
            else
            {
                BM = "";
               
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Input_Reset();
            if (radioButton4.Checked)
            {
                BM = (DateTime.Now.ToShortDateString());
                BM2 = radioButton4.Text;
                BM3 = "1275";
                
            }
            else
            {
                BM = "";
                
               
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Input_Reset();
            if (radioButton5.Checked)
            {
                BM = "5";
                BM2 = radioButton5.Text;
               
            }
            else
            {
                BM = "";
      
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            Input_Reset();
            if (radioButton6.Checked)
            {
                BM = "6";
                BM2 = radioButton6.Text;
              
            }
            else
            {
                BM = "";
               
               
            }


        }

        private void CMB_TalbNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cleargridview();
          //  SearchTalb(2);
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {

        }

        private void BTN_Redirect_Click(object sender, EventArgs e)
        {

        }
        }
    
}
