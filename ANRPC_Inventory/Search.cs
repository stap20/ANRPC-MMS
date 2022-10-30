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
        Panel secondBarPanel = new Panel();
        public Search()
        {
            InitializeComponent();

            secondBarPanel = getSecondBar(btnDocumentCycle.Text);
            prepareSecondBar(panel1,secondBarPanel);

            secondBarPanel.Visible=false;
           // HandleSubTabs(guna2GradientPanel1);
            //subTabContainer.Visible = false;
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

        private IconButton getTab(string text, IconChar icon, Action<object,EventArgs> onClickCallBack)
        {
            IconButton tab = new IconButton();
            tab.Font = new Font("Calibri", 11, FontStyle.Bold);
            tab.ForeColor = Color.FromArgb(155, 170, 192);
            tab.Text = text;

            tab.TextImageRelation = TextImageRelation.ImageBeforeText;
            tab.IconChar = icon;
            tab.IconSize = 25;
            tab.IconColor = Color.FromArgb(155, 170, 192);
            tab.IconFont = IconFont.Solid;
            tab.Dock = DockStyle.Left;

            tab.FlatAppearance.BorderSize = 0;
            tab.FlatAppearance.MouseDownBackColor = Color.FromArgb(43, 19, 114);
            tab.FlatAppearance.MouseOverBackColor = Color.FromArgb(43, 19, 114);
            tab.FlatStyle = FlatStyle.Flat;

            tab.AutoSize = true;

            tab.Click += new EventHandler(onClickCallBack);



            return tab;
        }
        private Panel getSecondBar(string current)
        {
            int secondBardHeight = 40;
            int spacing=10;

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



            tabsContainer.Controls.Add(getTab("   تصنيف", IconChar.Book, (object sender, EventArgs e) =>
            {
                MessageBox.Show("a7a4");
            }));
            tabsContainer.Controls.Add(getTab("   دورة مستندية", IconChar.ClockFour, (object sender, EventArgs e) =>
            {
                MessageBox.Show("a7a3");
            }));
            tabsContainer.Controls.Add(getTab("   تصنيف", IconChar.Book, (object sender, EventArgs e) =>
            {
                MessageBox.Show("a7a2");
            }));
            tabsContainer.Controls.Add(getTab("   دورة مستندية",IconChar.ClockFour, (object sender, EventArgs e) =>
            {
                MessageBox.Show("a7a");
            }));
            

            secondBar.Controls.Add(tabsContainer);
            secondBar.Controls.Add(getIndecatiorSection(current));
            secondBarContainer.Controls.Add(secondBar);
            

            return secondBarContainer;
        }
        
        private void prepareSecondBar(Panel container, Panel secondBar)
        {
            container.Controls.Add(secondBar);
            secondBar.BringToFront();
        }


        private void btnDocumentCycle_Click(object sender, EventArgs e)
        {
            if (!secondBarPanel.Visible)
            {
                secondBarPanel.Visible = true;
            }
        }

        private void btnTasnif_Click(object sender, EventArgs e)
        {
            secondBarPanel.Visible = false;

        }
    }
}
