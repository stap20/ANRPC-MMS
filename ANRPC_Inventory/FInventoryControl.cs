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
    public partial class FInventoryControl : Form
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
        public FInventoryControl()
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
             radioButton1.Checked = false;
             radioButton2.Checked = false;
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            //*******************************************
            // ******    AUTO COMPLETE
            //*******************************************
            string cmdstring = "";
          

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

        private void label8_Click(object sender, EventArgs e)
        {

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

  

    
        ///

      

    

    
    

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BTN_Print_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
            {
                MessageBox.Show("من فضلك اختار نوع التقرير");
                return;
            }
            else if(radioButton1.Checked==true)
            {
                Constants.FormNo = 10;
                FReports F = new FReports();
                F.Show();
            }
            else if (radioButton2.Checked == true)
            {
                Constants.FormNo = 11;
                FReports F = new FReports();
                F.Show();
            }
            else if (radioButton3.Checked == true)
            {
                Constants.FormNo = 12;
                FReports F = new FReports();
                F.Show();
            }
        }

    }
}
