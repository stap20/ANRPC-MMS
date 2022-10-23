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
using Guna.UI2.WinForms;

namespace ANRPC_Inventory
{
    public partial class TimeLine_Form : Form
    {
        //Fields
        private IconButton currentBtn;
        private Panel BottomBorderBtn;
        private Form currentChildForm;

        List<TimeLineCircleDetails> list = new List<TimeLineCircleDetails>();

        private void prepareBeforeFormLoad()
        {
            BottomBorderBtn = new Panel();
            BottomBorderBtn.Size = new Size(143, 4);
            guna2GradientPanel1.Controls.Add(BottomBorderBtn);


            for(int i = 0; i < 8; i++)
            {
                TimeLineCircleDetails details = new TimeLineCircleDetails();
                
                details.isDone = false;
                details.mainString = i.ToString();
                details.circleDetails = i.ToString() + "1";
                details.donePercent = 0;


                if (i < 3)
                {
                    details.isDone = true;
                    details.donePercent = 100;
                }

                if (i == 2)
                {
                    details.donePercent = 45;
                }

                list.Add(details);
            }
        }

        public TimeLine_Form()
        {       
            InitializeComponent();
            prepareBeforeFormLoad();
        }

        //Structs
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(226, 133, 222);
            public static Color color2 = Color.FromArgb(120, 77, 253);
            
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
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Buttom border button
                BottomBorderBtn.BackColor = color;
                BottomBorderBtn.Location = new Point(currentBtn.Location.X,currentBtn.Location.Y+currentBtn.Size.Height - BottomBorderBtn.Size.Height);
                BottomBorderBtn.Visible = true;
                BottomBorderBtn.BringToFront();
            }
        }
 
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.Transparent;
                currentBtn.ForeColor = Color.FromArgb(155, 170, 192);
                //currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.FromArgb(155, 170, 192);
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                //currentBtn.FlatAppearance.MouseDownBackColor = Color.Black ;
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            Cycledetails_Form details = new Cycledetails_Form();
            openChildForm(details);

           // guna2GradientPanel6.Visible = true;

        }

        private void btnTimeline_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();

            }

            ActivateButton(sender, RGBColors.color2);
            //Label_hoba();
        }
















        private void guna2GradientPanel6_Paint(object sender, PaintEventArgs e)
        {
            TimeLine timeLineGraph = new TimeLine(e, guna2GradientPanel6.Width, list);
            timeLineGraph.DarwSequance();
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
            guna2GradientPanel6.Controls.Add(childForm);
            guna2GradientPanel6.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

            currentChildForm = childForm;
        }

        private void TimeLine_Form_Load(object sender, EventArgs e)
        {
            btnTimeline_Click(btnTimeline, e);
        }
 
        private void Label_hoba()
        {
            Label hoba = new Label();
            hoba.BackColor = System.Drawing.Color.Transparent;
            hoba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            hoba.Font = new System.Drawing.Font("Microsoft Sans Serif",15F);
            hoba.ForeColor = System.Drawing.Color.FromArgb(164, 163, 203);
            hoba.Location = new System.Drawing.Point(21, 15);
            hoba.Size = new System.Drawing.Size(53, 40);
            hoba.Padding = new System.Windows.Forms.Padding(0, 5, 5, 0);
            hoba.Dock = DockStyle.Top;
            hoba.Text = "TimeLine";

            guna2GradientPanel6.Controls.Add(hoba);
        }

    }
}
