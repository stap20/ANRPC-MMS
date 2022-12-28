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
    public partial class Warranty_PopUP : Form
    {
          public SqlConnection con;//sql conn for anrpc_sms db
          public bool w1;
          public bool w2;
          public int m1;
          public int m2;
        public DataTable DT = new DataTable();
        private BindingSource bindingsource1 = new BindingSource();
        private string TableQuery;
        private int AddEditFlag;
        public string BM;
        public string BM2;

        public string CodeEdara;
        public string Fyear;
        public string TalbNo;
        public string slash="";

        public string ConstantPath;
        public string VariablePath;
        public string WholePath;



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

        public Warranty_PopUP()
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
      
        private void GetData(int x, string y)
        {
            
        }
        private void TalbTawred_Load(object sender, EventArgs e)
        {
           
           

            ///////////////////////////////////////////////////////
          //  ConstantPath = @"N:\PDF\";//////////////////change it to server path
            ConstantPath = @"\\172.18.8.83\MaterialAPP\PDF\";//////////////////change it to server path
            VariablePath =string.Concat(CodeEdara,@"\",Fyear,@"\",TalbNo);


            ////////////////////////////////////////////////////
           // OpenFileDialog open = new OpenFileDialog();
            // open.InitialDirectory = @"\\10.10.6.244\it_cloud\Customized_Programs\PDF";
            //   string[] paths = { @"\\Financial-app","pdf", TXT_MM.Text.ToString() + "_" + TXT_YYYY.Text.ToString() };
            //    string fullpath = Path.Combine(paths);
            //    open.InitialDirectory = fullpath;
            //  string folder1 = TXT_MM.Text.ToString() + "_" + TXT_YYYY.Text.ToString();
            // string folder2 = TXT_TransNo.Text.ToString();
            //open.InitialDirectory = @"\\Financial-app\pdf"; 

            /*
            open.InitialDirectory = ConstantPath + VariablePath;
            open.Filter = "Pdf Files|*.pdf";
            if (open.ShowDialog() == DialogResult.OK)
            {

                // pdf_path.Text = open.FileName;
                FileStream fs = new FileStream(@open.FileName, FileMode.Open, FileAccess.Read, FileShare.None);

            }
            if (open.FileName != "")
            {
                PDF_box.Items.Add(Path.GetFileNameWithoutExtension(open.FileName));
                PDFListBox1.Items.Add(open.FileName);//save full path in PDFLISTBOX1
            }*/////////////////////
         //   string path = "C:\\MyFolde";
            WholePath = ConstantPath + VariablePath+@"\";

            if (Directory.Exists(WholePath))
            {
                string[] filePaths = Directory.GetFiles(WholePath, "*.pdf",
                                  SearchOption.AllDirectories);

                foreach (string x in filePaths)
                {
                    int startIndex = x.LastIndexOf(@"\");
                    int endIndex = x.Length - 1;
                    int length = endIndex - startIndex;
                    string y = x.Substring(x.LastIndexOf(@"\") + 1, length);
                  
                }
            }

            else
            {
                MessageBox.Show("لا يوجد مرفقات", "خطأ");
            }
            numericUpDown1.Value = m1;
            numericUpDown2.Value = m2;
                checkBox1.Checked=w1;
                checkBox2.Checked=w2;
               
            /*
            foreach (string dirFile in Directory.GetDirectories(WholePath))
            {
                foreach (string fileName in Directory.GetFiles(dirFile))
                {
                    PDF_box.Items.Add(fileName);
                     PDFListBox1.Items.Add(fileName);
                    // fileName  is the file name
                }
            }*/
                //CodeEdara + "\" +  Fyear +"\" +TalbNo+"\";
            //then loop 
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
             
            }
    
        private void Addbtn_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                //numericUpDown1.ReadOnly = false;
                w1 = checkBox1.Checked;
                m1 = Convert.ToInt32(numericUpDown1.Value);
            }
            else
            {
              //  numericUpDown1.ReadOnly = true;
                w1 = checkBox1.Checked;
                m1 = 0;
            }
            if (checkBox2.Checked == true)
            {
              
                w2 = checkBox2.Checked;
                m2 = Convert.ToInt32(numericUpDown2.Value);
            }
            else
            {
              //  numericUpDown1.ReadOnly = true;
                w2 = checkBox2.Checked;
                m2 = 0;
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
           
        }
        public void SearchTalb(int x)
          {
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
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {

        }

       

       
        

    
      
       
        private void delete_pdf_MouseEnter(object sender, EventArgs e)
        {

        }

        private void delete_pdf_MouseLeave(object sender, EventArgs e)
        {

        }

       
      

        private void OpenPDF_btn2_MouseLeave(object sender, EventArgs e)
        {
            label14.Visible = false;
        }

        private void OpenPDF_btn2_MouseEnter(object sender, EventArgs e)
        {
            label14.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                numericUpDown1.Visible = true;
                label2.Visible = true;
                numericUpDown1.ReadOnly = false;

                w1 = checkBox1.Checked;
            //    m1 =Convert.ToInt32( numericUpDown1.Value);
            }
            else
            {
                numericUpDown1.Visible = false;
                label2.Visible = false;
                numericUpDown1.ReadOnly = true;
                w1 = checkBox1.Checked;
                m1 = 0;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                numericUpDown2.Visible = true;
                label1.Visible = true;
                numericUpDown2.ReadOnly = false;
                w2 = checkBox2.Checked;
              //  m2 = Convert.ToInt32(numericUpDown2.Value);
            }
            else
            {
                numericUpDown2.Visible =false;
                label1.Visible = false;
                numericUpDown2.ReadOnly = true;
                w2 = checkBox2.Checked;
                m2 = 0;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
           // w1 = checkBox1.Checked;
            m1 = Convert.ToInt32(numericUpDown1.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
           // w2= checkBox2.Checked;
            m2 = Convert.ToInt32(numericUpDown2.Value);
        }
        }
    
}
