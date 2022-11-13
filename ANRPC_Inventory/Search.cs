using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace ANRPC_Inventory
{
    public partial class Search : Form
    {
        private IconButton currentBtn;
        private Panel secondBarPanel = new Panel();
        private Panel BottomBorderBtn;
        private List<Tab> secondBarList = new List<Tab> ();

        private void prepareSideBarActiveIndecator()
        {
            BottomBorderBtn = new Panel();
            BottomBorderBtn.Size = new Size(8, 54);
            tabsBar.Controls.Add(BottomBorderBtn);
            BottomBorderBtn.Visible = false;
        }

        private IconButton getIndecatiorSection(string header)
        {
            IconButton indecator = new IconButton();
            indecator.Font = new Font("Calibri", 11, FontStyle.Bold);
            indecator.ForeColor = Color.FromArgb(155, 170, 192);
            indecator.Text = header;
            indecator.TextImageRelation = TextImageRelation.TextBeforeImage;
            indecator.IconChar = IconChar.AngleRight;
            indecator.IconSize = 18;
            indecator.IconColor = Color.FromArgb(155, 170, 192);
            indecator.IconFont = IconFont.Solid;
            indecator.Dock = DockStyle.Left;

            indecator.FlatAppearance.BorderSize = 0;
            indecator.FlatAppearance.MouseDownBackColor = Color.Transparent;
            indecator.FlatAppearance.MouseOverBackColor = Color.Transparent;
            indecator.FlatStyle = FlatStyle.Flat;

            indecator.AutoSize = true;

            return indecator;

        }


        private Panel getSecondBar(string current)
        {
            int secondBardHeight = 40;
            int spacing = 10;

            Panel secondBarContainer = new Panel();
            secondBarContainer.Dock = DockStyle.Top;
            secondBarContainer.BackColor = Color.Transparent;


            Guna2GradientPanel secondBar = new Guna2GradientPanel();
            secondBar.Dock = DockStyle.Top;
            secondBar.FillColor = Color.FromArgb(32, 15, 83);
            secondBar.FillColor2 = Color.FromArgb(32, 15, 83);
            secondBar.BorderRadius = 7;
            secondBar.Size = new Size(secondBar.Width, secondBardHeight);


            secondBarContainer.Size = new Size(secondBarContainer.Width, secondBardHeight + spacing);

            Panel tabsContainer = new Panel();
            tabsContainer.Dock = DockStyle.Fill;
            tabsContainer.BackColor = Color.Transparent;
            tabsContainer.AutoScroll = true;

            for (int i = secondBarList.Count - 1; i >= 0; i--)
            {
                tabsContainer.Controls.Add(secondBarList[i].getTab());
            }

            secondBar.Controls.Add(tabsContainer);
            secondBar.Controls.Add(getIndecatiorSection(current));
            secondBarContainer.Controls.Add(secondBar);


            return secondBarContainer;
        }


        private void prepareSecondBarTabsList()
        {
            Font font = new Font("Calibri", 11, FontStyle.Bold);
            Color color = Color.FromArgb(155, 170, 192);

            secondBarList.Add(new Tab(font, "   دورة مستندية", color, 32, IconFont.Solid, IconChar.ChartSimple,
                              DockStyle.Left, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e, RGBColors.color1); },
                              width: 172));

            secondBarList.Add(new Tab(font, "   2دورة مستندية", color, 25, IconFont.Solid, IconChar.ClockFour,
                            DockStyle.Left, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e, RGBColors.color1); },
                            width: 172));

            secondBarList.Add(new Tab(font, "   3دورة مستندية", color, 25, IconFont.Solid, IconChar.ClockFour,
                            DockStyle.Left, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e, RGBColors.color1); },
                            width: 172));

            secondBarList.Add(new Tab(font, "   4دورة مستندية", color, 25, IconFont.Solid, IconChar.ClockFour,
                            DockStyle.Left, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e, RGBColors.color1); },
                            width: 172));

            secondBarList.Add(new Tab(font, "   5دورة مستندية", color, 25, IconFont.Solid, IconChar.ClockFour,
                            DockStyle.Left, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e, RGBColors.color1); },
                            width: 172));
        }


        public Search()
        {
            InitializeComponent();


            prepareSecondBarTabsList();
            secondBarPanel = getSecondBar(btnDocumentCycle.Text);
            

            prepareSecondBar(panel1,secondBarPanel);

            secondBarPanel.Visible=false;
           // HandleSubTabs(guna2GradientPanel1);
            //subTabContainer.Visible = false;
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

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.ForeColor = Color.FromArgb(111, 139, 173);
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.FromArgb(111, 139, 173);
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
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
                BottomBorderBtn.BackColor = color;
                BottomBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                BottomBorderBtn.Visible = true;
                BottomBorderBtn.BringToFront();
                //iconAppBar
                //iconButton1.Visible = true;
                //iconButton1.IconChar = currentBtn.IconChar;
                //iconButton1.IconColor = color;
                //iconButton1.Text = currentBtn.Text;
                //iconButton1.ForeColor = color;

            }
        }



        private void SideBarBtnCLicked(object sender, EventArgs e, Color color, Form childForm = null)
        {
            ActivateButton(sender, color);

            //if (childForm != null)
            //{
            //    openChildForm(childForm);
            //}

            formWraper.Visible = true;
        }




        private void prepareSecondBar(Panel container, Panel secondBar)
        {
            
            container.Controls.Add(secondBar);
            secondBar.BringToFront();
            panel3.BringToFront();
            
        }


        private void btnDocumentCycle_Click(object sender, EventArgs e)
        {
            if (!secondBarPanel.Visible)
            {
                ActivateButton(sender, RGBColors.color3);
                secondBarPanel.Visible = true;
            }
        }

        private void btnTasnif_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            secondBarPanel.Visible = false;

        }
    }
}
