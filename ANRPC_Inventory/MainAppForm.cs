using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using FontAwesome.Sharp;
using Guna.UI2.WinForms;

namespace ANRPC_Inventory
{
    public partial class MainAppForm : Form
    {
        //Fields
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        List<Tab> sideBarTabsList = new List<Tab>();

        public MainAppForm()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(8, 54);
            panelButtons.Controls.Add(leftBorderBtn);
            this.WindowState = FormWindowState.Maximized;
        }
        //Structs
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(249, 248, 113);
            public static Color color2 = Color.FromArgb(232, 213, 181);
            public static Color color3 = Color.FromArgb(184, 224, 103);
            public static Color color4 = Color.FromArgb(255, 180, 80);
            public static Color color5 = Color.FromArgb(247, 213, 101);
            public static Color color6 = Color.FromArgb(192, 57, 94);
            public static Color color7 = Color.FromArgb(236, 113, 82);
            
        }

        private void prepareSideBarTabsAction()
        {
            Font font = new Font("Calibri", 16, FontStyle.Bold);
            Color color = Color.FromArgb(111, 139, 173);
            Padding padd = new Padding(10, 0, 20, 0);
            FlatButtonAppearance appearance = new FlatButtonAppearance(Color.Transparent,0,Color.Red,Color.Green);


            sideBarTabsList.Add(new Tab(font, "    لوحة القيادة", color, 32, IconFont.Auto, IconChar.ChartSimple,
                                DockStyle.Top, (object sender, EventArgs e) => { MessageBox.Show("aaaa1"); },
                                height: 54, padding: padd));

            sideBarTabsList.Add(new Tab(font, "    طلب التوريد", color, 32, IconFont.Auto, IconChar.ClipboardList,
                                DockStyle.Top, (object sender, EventArgs e) => { MessageBox.Show("aaaa1"); },
                                height: 54, padding: padd));

            sideBarTabsList.Add(new Tab(font, "    إذن الصرف", color, 35, IconFont.Auto, IconChar.CartFlatbed,
                    DockStyle.Top, (object sender, EventArgs e) => { MessageBox.Show("aaaa1"); },
                    height: 54, padding: padd));

            sideBarTabsList.Add(new Tab(font, "    المطابقة الفنية", color, 32, IconFont.Auto, IconChar.ClipboardCheck,
                    DockStyle.Top, (object sender, EventArgs e) => { MessageBox.Show("aaaa1"); },
                    height: 54, padding: padd));

            sideBarTabsList.Add(new Tab(font, "    إذون التحويل", color, 32, IconFont.Auto, IconChar.DiagramPredecessor,
                    DockStyle.Top, (object sender, EventArgs e) => { MessageBox.Show("aaaa1"); },
                    height: 54, padding: padd));

            sideBarTabsList.Add(new Tab(font, "    البـــــحــــث", color, 32, IconFont.Auto, IconChar.Search,
                    DockStyle.Top, (object sender, EventArgs e) => { MessageBox.Show("aaaa1"); },
                    height: 54, padding: padd));
        }

        private void ActivateButton(object senderBtn, Color color)
        {
           
            if (senderBtn != null)
            {

                
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(43, 19, 114);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //iconAppBar
                iconButton1.Visible = true;
                iconButton1.IconChar = currentBtn.IconChar;
                iconButton1.IconColor = color;
                iconButton1.Text = currentBtn.Text;
                iconButton1.ForeColor = color;

            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = panelButtons.BackColor;
                currentBtn.ForeColor = Color.FromArgb(111, 139, 173);
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.FromArgb(111, 139, 173);
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            iconButton1.Visible = false;
            formwraper.Visible = false;
        }


        private void openChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            formwraper.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();

            currentChildForm = childForm;
        }


        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            
            conForm dashBoard = new conForm();
            openChildForm(dashBoard);

            formwraper.Visible = true;

        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);

            Search searchForm = new Search();
            openChildForm(searchForm);

            formwraper.Visible = true;
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);

            TalbTawred TalbTawreed = new TalbTawred();
            Constants.talbtawred_F = true; //--> panel7 --> Invisible - panel2 --> visible
            openChildForm(TalbTawreed);

            formwraper.Visible = true;
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);

            TalbTawred TalbTawreed = new TalbTawred();
            Constants.talbtawred_F = false; //--> panel7 --> Invisible - panel2 --> visible
            openChildForm(TalbTawreed);

            formwraper.Visible = true;
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);

            EznSarf_F EznSarf = new EznSarf_F();
            Constants.EznSarf_FF = true; //--> panel7 --> visible - panel2 --> Invisible
            openChildForm(EznSarf);

            formwraper.Visible = true;
        }

        private void btnFinancial_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);

            EznSarf_F EznSarf = new EznSarf_F();
            Constants.EznSarf_FF = false; //--> panel7 --> visible - panel2 --> Invisible
            openChildForm(EznSarf);

            formwraper.Visible = true;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color7);
            formwraper.Visible = false;
        }
        private void btnTahwel_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            formwraper.Visible = false;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);

            Search searchForm = new Search();
            openChildForm(searchForm);

            formwraper.Visible = true;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color7);
            formwraper.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_max_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btn_max.SendToBack();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnWindowReset_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnWindowReset.SendToBack();
        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
