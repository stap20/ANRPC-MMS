using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace ANRPC_Inventory
{
    public partial class EznSarf : Form
    {
        SqlConnection con;

        public EznSarf()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        //--------------------------
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics surface = e.Graphics;
            Pen pen1 = new Pen(Color.Black, 2);
            surface.DrawLine(pen1, panel1.Location.X + 4, 4, panel1.Location.X + 4, panel1.Location.Y + panel1.Size.Height); // Left Line
            surface.DrawLine(pen1, panel1.Size.Width - 4, 4, panel1.Size.Width - 4, panel1.Location.Y + panel1.Size.Height); // Right Line
            //---------------------------
            surface.DrawLine(pen1, 4, 4, panel1.Location.X + panel1.Size.Width - 4, 4); // Top Line
            surface.DrawLine(pen1, 4, panel1.Size.Height - 1, panel1.Location.X + panel1.Size.Width - 4, panel1.Size.Height - 1); // Bottom Line
            surface.Dispose();
        }
        //====================================
        private void EznSarf_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(Constants.constring);
            
          /*  
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }  //--> OPEN CONNECTION

            //============
            //  string query = "SELECT  componentcode , componentname FROM [ANRPC_ProductionPlanning].[dbo].[PumpCompMaster] order by componentcode";
            string query = "SELECT  [ComponentCode],[ComponentName] FROM [ANRPC_ProductionPlanning].[dbo].[PumpCompMaster] where  [ComponentCode] Not IN (SELECT [ComponentCode] from PumpCompDetails WHERE PumpNo='" + PumpNo_Box.Text + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dts = new DataTable();
            dts.Load(cmd.ExecuteReader());
            component_box.DataSource = dts;
            component_box.ValueMember = "componentcode";
            component_box.DisplayMember = "componentname";
           * 
         
           */
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void AddNewbtn_Click(object sender, EventArgs e)
        {
            
        }

        private void Addbtn2_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void BTN_Sign3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
