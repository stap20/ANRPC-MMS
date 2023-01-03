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
    public class SideBarHandler
    {
        private IconButton currentActiveTab;
        private Panel tabsActiveBorder;
        private Panel sideBarPanel = new Panel();
        private int containerWidth;
        private List<Tab> tabsList;

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(254, 254, 254);
            public static Color color2 = Color.FromArgb(2, 163, 123);
            public static Color color3 = Color.FromArgb(184, 224, 103);
        }


        public SideBarHandler(List<Tab> tabsList, Panel container)
        {

            containerWidth = container.Width;

            foreach (Tab tab in tabsList)
            {
                IconButton iconButton = tab.getTab();

                iconButton.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    ActivateButton(sender, RGBColors.color1);
                    tab.getOnClickCallback();
                });
            }

            this.tabsList = tabsList;

            this.prepareSubTabsActiveIndecator();

            this.sideBarPanel = getSideBarPanel();

            this.sideBarPanel.Controls.Add(this.tabsActiveBorder);

            this.currentActiveTab = this.tabsList[0].getTab(); //default tab

            this.currentActiveTab.PerformClick();

            container.Controls.Add(this.sideBarPanel);

            
            this.sideBarPanel.BringToFront();
        }

        private void prepareSubTabsActiveIndecator()
        {

            this.tabsActiveBorder = new Panel();
            this.sideBarPanel.Controls.Add(this.tabsActiveBorder);
            this.tabsActiveBorder.Visible = false;
        }

        private Panel getSideBarPanel()
        {
            Panel tabsContainer = new Panel();
            tabsContainer.Dock = DockStyle.Fill;
            tabsContainer.BackColor = Color.Transparent;
            tabsContainer.AutoScroll = true;

            for (int i = this.tabsList.Count - 1; i >= 0; i--)
            {
                tabsContainer.Controls.Add(this.tabsList[i].getTab());
            }


            return tabsContainer;
        }

        private void DisableButton()
        {
            if (this.currentActiveTab != null)
            {

                this.currentActiveTab.BackColor = Color.Transparent;
                this.currentActiveTab.ForeColor = Color.FromArgb(239, 239, 255);
                this.currentActiveTab.IconColor = Color.FromArgb(239, 239, 255);
                this.currentActiveTab.TextImageRelation = TextImageRelation.ImageBeforeText;
                this.currentActiveTab.ImageAlign = ContentAlignment.MiddleLeft;
                this.currentActiveTab.TextAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void ActivateButton(object senderBtn, Color color)
        {

            if (senderBtn != null)
            {

                DisableButton();
                //Button

                this.currentActiveTab = (IconButton)senderBtn;

                this.currentActiveTab.BackColor = RGBColors.color2;
                this.currentActiveTab.ForeColor = color;
                this.currentActiveTab.TextAlign = ContentAlignment.MiddleCenter;
                this.currentActiveTab.IconColor = color;
                this.currentActiveTab.TextImageRelation = TextImageRelation.TextBeforeImage;
                this.currentActiveTab.ImageAlign = ContentAlignment.MiddleLeft;

                //left border button
                this.tabsActiveBorder.BackColor = RGBColors.color1;


                this.tabsActiveBorder.Size = new Size(8, this.currentActiveTab.Size.Height);

                int activeBorderX, activeBorderY;


                activeBorderX = this.currentActiveTab.Location.X + containerWidth  - 8;
                activeBorderY = this.currentActiveTab.Location.Y;

                this.tabsActiveBorder.Location = new Point(activeBorderX, activeBorderY);

                this.tabsActiveBorder.Visible = true;
                this.tabsActiveBorder.BringToFront();

            }
        }


        public Panel getSubTabPanel()
        {
            return this.sideBarPanel;
        }

        public bool getSubTabsVisibleState()
        {
            return this.sideBarPanel.Visible;
        }

        public void setSubTabsVisible(bool state)
        {
            this.sideBarPanel.Visible = state;

            if (state == true)
            {
                this.currentActiveTab.PerformClick();
            }
        }

    }
}