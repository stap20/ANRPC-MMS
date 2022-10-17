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
    public partial class PDF_PopUp : Form
    {
          public SqlConnection con;//sql conn for anrpc_sms db

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

        public PDF_PopUp()
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
            PDF_box.Items.Clear();
            PDF_box.ResetText();
            PDFListBox1.Items.Clear();
            PDFListBox1.ResetText();
            /*
            MessageBox.Show(CodeEdara.ToString());
            MessageBox.Show(Fyear.ToString());
            MessageBox.Show(TalbNo.ToString());
            */


            ///////////////////////////////////////////////////////
          //  ConstantPath = @"N:\PDF\";//////////////////change it to server path
            //ConstantPath = @"\\172.18.8.83\MaterialAPP\PDF\";//////////////////change it to server path


            //after change attach concept which add for all steps attachment
            //VariablePath =string.Concat(CodeEdara,@"\",Fyear,@"\",TalbNo);
            //WholePath = ConstantPath + VariablePath + @"\";

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
                    PDF_box.Items.Add(y);
                    PDFListBox1.Items.Add(x);
                }
            }

            else
            {
                MessageBox.Show("لا يوجد مرفقات", "خطأ");
            }

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

        private void pdf_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            // open.InitialDirectory = @"\\10.10.6.244\it_cloud\Customized_Programs\PDF";
            //   string[] paths = { @"\\Financial-app","pdf", TXT_MM.Text.ToString() + "_" + TXT_YYYY.Text.ToString() };
            //    string fullpath = Path.Combine(paths);
            //    open.InitialDirectory = fullpath;
            //  string folder1 = TXT_MM.Text.ToString() + "_" + TXT_YYYY.Text.ToString();
            // string folder2 = TXT_TransNo.Text.ToString();
            //open.InitialDirectory = @"\\Financial-app\pdf"; 
            open.InitialDirectory = @"\\warehouse-app";
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
            }
        }

        private void PDF_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PDF_box.SelectedIndex > -1)
            {

                //get index that i select in pdflistbox and send it to pdflistbox1 to get full path
                int ii = PDF_box.SelectedIndex;
                PDFListBox1.SelectedIndex = ii;
                if (PDF_box.Text == "")
                {
                    MessageBox.Show("  للعرضpdf لا يوجد ");

                }
                else
                {
                   axAcroPDF1.src = PDFListBox1.Text;

                }
                // pdf_path.Text = PDF1_box.Items[ii].ToString();
            }
        }

        private void delete_pdf_Click(object sender, EventArgs e)
        {
            if (PDF_box.Text == "")
            {
                MessageBox.Show("لا يوجد مرفق للحذف");
                return;
            }
            if ((MessageBox.Show("هل تريد حذف المرفق ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {

                int ii = PDF_box.SelectedIndex;
                string path = PDFListBox1.Text;
                PDFListBox1.Items.RemoveAt(ii);
                PDF_box.Items.RemoveAt(ii);
                File.Delete(path);
                PDF_box.Text = "";
               axAcroPDF1.src = null;
                axAcroPDF1.LoadFile("none");
         
                
                
                //   PDF1_box.Text = "";

                /*
                 int ii = PDFListBox.SelectedIndex;
                 PDFListBox1.Items.RemoveAt(ii);
                 PDFListBox.Items.RemoveAt(ii);*/

                //    pdf_path.Text = "";
            }
            else
            {

            }
        }

        private void OpenPDF_btn_Click(object sender, EventArgs e)
        {
            if (PDF_box.Text == "")
            {
                MessageBox.Show("  للعرضpdf لا يوجد ");

            }
            else
            {
                // Process.Start(@pdf_path.Text);
                System.Diagnostics.Process.Start(PDFListBox1.Text);
            }
        }

        private void PrintPdf_btn_Click(object sender, EventArgs e)
        {
            if (PDF_box.Text == "" || PDF_box.Text == null)
            {
                MessageBox.Show("لا يوجد مرفق لطباعة");
                return;
            }
            if ((MessageBox.Show("هل تريد طباعة المرفق ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                /*
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = false,
                    Verb = "print",
                    FileName = pdf_path.Text
                };
                p.Start();

            }
            else { }*/
                PrintDialog printDlg = new PrintDialog();
                PrintDocument printDoc = new PrintDocument();
                //   printDoc.DocumentName = pdf_path.Text;
                printDoc.DocumentName = PDFListBox1.Text;
                printDlg.Document = printDoc;
                printDlg.AllowSelection = true;
                printDlg.AllowSomePages = true;
                //Call ShowDialog  
                if (printDlg.ShowDialog() == DialogResult.OK) printDoc.Print();

            }
        }

        private void OpenPDF_btn2_Click(object sender, EventArgs e)
        {
            if (PDF_box.Text == "")
            {
                MessageBox.Show("  للعرضpdf لا يوجد ");

            }
            else
            {
           axAcroPDF1.src = PDFListBox1.Text;

            }
        }

        private void delete_pdf_MouseEnter(object sender, EventArgs e)
        {

        }

        private void delete_pdf_MouseLeave(object sender, EventArgs e)
        {

        }

        private void OpenPDF_btn_MouseEnter(object sender, EventArgs e)
        {
            label12.Visible = true;
        }

        private void OpenPDF_btn_MouseLeave(object sender, EventArgs e)
        {
            label12.Visible = false;
        }

        private void PrintPdf_btn_MouseLeave(object sender, EventArgs e)
        {
            label13.Visible = false;
        }

        private void PrintPdf_btn_MouseEnter(object sender, EventArgs e)
        {
            label13.Visible = true;
        }

        private void OpenPDF_btn2_MouseLeave(object sender, EventArgs e)
        {
            label14.Visible = false;
        }

        private void OpenPDF_btn2_MouseEnter(object sender, EventArgs e)
        {
            label14.Visible = true;
        }
        }
    
}
