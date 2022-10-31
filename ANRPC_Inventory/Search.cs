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
        List<Tab> secondBarList = new List<Tab> ();

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

        
        
        private void prepareSecondBarTabsList()
        {
            Font font = new Font("Calibri", 11, FontStyle.Bold);
            Color color = Color.FromArgb(155, 170, 192);
            secondBarList.Add(new Tab(font, "   دورة مستندية", color, 25, IconFont.Solid, IconChar.ClockFour, (object sender, EventArgs e) => { MessageBox.Show("aaaa1"); },172));
            secondBarList.Add(new Tab(font, "   2دورة مستندية", color, 25, IconFont.Solid, IconChar.ClockFour, (object sender, EventArgs e) => { MessageBox.Show("aaaa2"); }, 172));
            secondBarList.Add(new Tab(font, "   3دورة مستندية", color, 25, IconFont.Solid, IconChar.ClockFour, (object sender, EventArgs e) => { MessageBox.Show("aaaa3"); }, 172));
            secondBarList.Add(new Tab(font, "   4دورة مستندية", color, 25, IconFont.Solid, IconChar.ClockFour, (object sender, EventArgs e) => { MessageBox.Show("aaaa4"); }, 172));
            secondBarList.Add(new Tab(font, "   5دورة مستندية", color, 25, IconFont.Solid, IconChar.ClockFour, (object sender, EventArgs e) => { MessageBox.Show("aaaa5"); }, 172));
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
            tabsContainer.AutoScroll = true;

            for(int i = secondBarList.Count-1; i >=0; i--)
            {
                tabsContainer.Controls.Add(secondBarList[i].getTab());
            }

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
