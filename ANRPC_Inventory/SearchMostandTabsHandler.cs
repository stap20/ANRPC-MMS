﻿using FontAwesome.Sharp;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANRPC_Inventory
{
    public partial class SearchMostandTabsHandler : Form
    {
        private IconButton currentActiveTab;
        private Panel tabsActiveBorder;
        private Form currentChildForm;


        private void prepareSubTabsActiveIndecator()
        {

            tabsActiveBorder = new Panel();
            tabsBar.Controls.Add(tabsActiveBorder);
            tabsActiveBorder.Visible = false;
        }



        public SearchMostandTabsHandler()
        {
            InitializeComponent();
            prepareSubTabsActiveIndecator();
            btnDocumentDetails.PerformClick();
        }

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(254, 254, 254);
            public static Color color2 = Color.FromArgb(2, 163, 123);
            public static Color color3 = Color.FromArgb(184, 224, 103);
        }

        private void openChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            //End
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            formWraper.Controls.Add(childForm);

            formWraper.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }

        private void DisableButton()
        {
            if (this.currentActiveTab != null)
            {

                currentActiveTab.BackColor = Color.Transparent;
                currentActiveTab.ForeColor = Color.FromArgb(239, 239, 255);
                currentActiveTab.IconColor = Color.FromArgb(239, 239, 255);
                currentActiveTab.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentActiveTab.ImageAlign = ContentAlignment.MiddleLeft;
                currentActiveTab.TextAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void ActivateButton(object senderBtn, Color color)
        {

            if (senderBtn != null)
            {
                DisableButton();
                //Button

                currentActiveTab = (IconButton)senderBtn;

                currentActiveTab.BackColor = RGBColors.color2;
                currentActiveTab.ForeColor = color;
                currentActiveTab.TextAlign = ContentAlignment.MiddleCenter;
                currentActiveTab.IconColor = color;
                currentActiveTab.ImageAlign = ContentAlignment.MiddleRight;

                //Buttom border button
                tabsActiveBorder.BackColor = RGBColors.color1;

                tabsActiveBorder.Size = new Size(currentActiveTab.Size.Width, 4);

                int activeBorderX, activeBorderY;

                activeBorderX = currentActiveTab.Location.X;
                activeBorderY = currentActiveTab.Location.Y + currentActiveTab.Size.Height - tabsActiveBorder.Size.Height;
                tabsActiveBorder.Location = new Point(activeBorderX, activeBorderY);

                tabsActiveBorder.Visible = true;
                tabsActiveBorder.BringToFront();
            }
        }

        private void SideBarBtnCLicked(object sender, EventArgs e, Color color, Form childForm = null)
        {
            ActivateButton(sender, color);

            if (childForm != null)
            {
                openChildForm(childForm);
            }


            formWraper.Visible = true;
        }

        private void btnDocumentDetails_Click(object sender, EventArgs e)
        {
            SideBarBtnCLicked(sender, e, RGBColors.color1, new TalbTawred("2022_2023", "8"));
        }

        private void btnDocumentTimeLine_Click(object sender, EventArgs e)
        {

            SideBarBtnCLicked(sender, e, RGBColors.color1, new TimeLineDrawerForm());
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }
    }
}