using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Guna.UI2.WinForms;
using static System.Net.Mime.MediaTypeNames;

namespace ANRPC_Inventory
{
    public class SubTabsHandler
    {
        private IconButton currentActiveTab;
        private IconButton indecatorTitleSection;
        private Panel tabsActiveBorder;
        private Panel subTabPanel = new Panel();

        private List<Tab> tabsList;

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(249, 248, 113);
            public static Color color2 = Color.FromArgb(232, 213, 181);
            public static Color color3 = Color.FromArgb(184, 224, 103);
        }


        public SubTabsHandler(List<Tab> tabsList,Panel container)
        {

            foreach(Tab tab in tabsList)
            {
                IconButton iconButton = tab.getTab();

                iconButton.Click += new EventHandler((object sender,EventArgs e) =>
                {
                    ActivateButton(sender, RGBColors.color3);
                    (tab.getOnClickCallback())(sender,e);
                });
            }

            this.tabsList = tabsList;

            this.prepareSubTabsActiveIndecator();

            this.subTabPanel = getSubTabsPanel(40, 10);

            this.subTabPanel.Controls.Add(this.tabsActiveBorder);

            this.currentActiveTab = this.tabsList[0].getTab(); //default tab
           
            container.Controls.Add(this.subTabPanel);
            this.subTabPanel.BringToFront();
            this.subTabPanel.Visible = false;
        }

        private void prepareSubTabsActiveIndecator()
        {

            this.tabsActiveBorder = new Panel();
            this.subTabPanel.Controls.Add(this.tabsActiveBorder);
            this.tabsActiveBorder.Visible = false;
        }

        private IconButton getIndecatiorSection()
        {
            IconButton indecator = new IconButton();
            indecator.Font = new Font("Calibri", 11, FontStyle.Bold);
            indecator.ForeColor = Color.FromArgb(155, 170, 192);
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

        private Panel getSubTabsPanel(int height,int subTabs_spacing)
        {
            int secondBardHeight = height;
            int spacing = subTabs_spacing;

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

            for (int i = this.tabsList.Count - 1; i >= 0; i--)
            {
                tabsContainer.Controls.Add(this.tabsList[i].getTab());
            }

            secondBar.Controls.Add(tabsContainer);

            this.indecatorTitleSection = getIndecatiorSection();
            secondBar.Controls.Add(this.indecatorTitleSection);

            secondBarContainer.Controls.Add(secondBar);


            return secondBarContainer;
        }

        private void DisableButton()
        {
            if (this.currentActiveTab != null)
            {

                this.currentActiveTab.BackColor = Color.Transparent;
                this.currentActiveTab.ForeColor = Color.FromArgb(155, 170, 192);
                this.currentActiveTab.IconColor = Color.FromArgb(155, 170, 192);
                this.currentActiveTab.TextImageRelation = TextImageRelation.ImageBeforeText;
                this.currentActiveTab.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void ActivateButton(object senderBtn, Color color)
        {

            if (senderBtn != null)
            {

                DisableButton();
                //Button

                this.currentActiveTab = (IconButton)senderBtn;

                this.currentActiveTab.BackColor = Color.FromArgb(43, 19, 114);
                this.currentActiveTab.ForeColor = color;
                this.currentActiveTab.TextAlign = ContentAlignment.MiddleCenter;
                this.currentActiveTab.IconColor = color;
                this.currentActiveTab.ImageAlign = ContentAlignment.MiddleRight;

                //Buttom border button
                this.tabsActiveBorder.BackColor = color;

                this.indecatorTitleSection.Text = this.currentActiveTab.Text;

                this.tabsActiveBorder.Size = new Size(this.currentActiveTab.Size.Width, 4);

                int activeBorderX, activeBorderY;

                activeBorderX = this.currentActiveTab.Location.X + this.indecatorTitleSection.Size.Width;
                activeBorderY = this.currentActiveTab.Location.Y + this.currentActiveTab.Size.Height - this.tabsActiveBorder.Size.Height;
                this.tabsActiveBorder.Location = new Point(activeBorderX, activeBorderY);

                this.tabsActiveBorder.Visible = true;
                this.tabsActiveBorder.BringToFront();

                //iconAppBar
                //iconButton1.Visible = true;
                //iconButton1.IconChar = currentBtn.IconChar;
                //iconButton1.IconColor = color;
                //iconButton1.Text = currentBtn.Text;
                //iconButton1.ForeColor = color;

            }
        }


        public Panel getSubTabPanel()
        {
            return this.subTabPanel;
        }

        public bool getSubTabsVisibleState()
        {
            return this.subTabPanel.Visible;
        }

        public void setSubTabsVisible(bool state)
        {
            this.subTabPanel.Visible = state;

            if (state == true)
            {
                this.currentActiveTab.PerformClick();
            }
        }

    }
}
