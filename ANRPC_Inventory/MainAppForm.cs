using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace ANRPC_Inventory
{
    public partial class MainAppForm : Form
    {
        //Fields
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        public MainAppForm()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(8, 54);
            panelButtons.Controls.Add(leftBorderBtn);
        }
        //Structs
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(226, 133, 222);
            public static Color color2 = Color.FromArgb(120, 77, 253);
            public static Color color3 = Color.FromArgb(74, 218, 236);
            public static Color color4 = Color.FromArgb(251, 83, 155);
            public static Color color5 = Color.FromArgb(126, 130, 252);
            public static Color color6 = Color.FromArgb(255, 192, 71);
            public static Color color7 = Color.FromArgb(239, 108, 150);
            public static Color color8 = Color.FromArgb(120, 163, 252);
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
            formwraper.Tag = childForm;
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

            TimeLine_Form timeLine = new TimeLine_Form();
            openChildForm(timeLine);

            formwraper.Visible = true;
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            formwraper.Visible = false;
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            formwraper.Visible = false;
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            formwraper.Visible = false;
        }

        private void btnFinancial_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            formwraper.Visible = false;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color7);
            formwraper.Visible = false;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color8);
            formwraper.Visible = false;
        }
    }
}
