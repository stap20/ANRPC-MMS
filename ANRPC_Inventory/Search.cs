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
        private List<Tab> secondBarList = new List<Tab> ();
        private SubTabsHandler subTabBar;


        private void prepareSideBarActiveIndecator()
        {
            //BottomBorderBtn = new Panel();
            //BottomBorderBtn.Size = new Size(8, 54);
            //tabsBar.Controls.Add(BottomBorderBtn);
            //BottomBorderBtn.Visible = false;
        }

        private void prepareSecondBarTabsList()
        {
            Font font = new Font("Calibri", 11, FontStyle.Bold);
            Color color = Color.FromArgb(155, 170, 192);

            secondBarList.Add(new Tab(font, "   دورة مستندية", color, 32, IconFont.Solid, IconChar.ChartSimple,
                              DockStyle.Left, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e); },
                              width: 172));

            secondBarList.Add(new Tab(font, "   2دورة مستندية", color, 25, IconFont.Solid, IconChar.ClockFour,
                            DockStyle.Left, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e); },
                            width: 172));

            secondBarList.Add(new Tab(font, "   3دورة مستندية", color, 25, IconFont.Solid, IconChar.ClockFour,
                            DockStyle.Left, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e); },
                            width: 172));

            secondBarList.Add(new Tab(font, "   4دورة مستندية", color, 25, IconFont.Solid, IconChar.ClockFour,
                            DockStyle.Left, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e); },
                            width: 172));

            secondBarList.Add(new Tab(font, "   5دورة مستندية", color, 25, IconFont.Solid, IconChar.ClockFour,
                            DockStyle.Left, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e); },
                            width: 172));
        }

        public Search()
        {
            InitializeComponent();


            prepareSecondBarTabsList();
            
            subTabBar = new SubTabsHandler(secondBarList,panel1);
            panel3.BringToFront();
            // HandleSubTabs(guna2GradientPanel1);
            //subTabContainer.Visible = false;
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
                //BottomBorderBtn.BackColor = color;
                //BottomBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                //BottomBorderBtn.Visible = true;
                //BottomBorderBtn.BringToFront();
                //iconAppBar
                //iconButton1.Visible = true;
                //iconButton1.IconChar = currentBtn.IconChar;
                //iconButton1.IconColor = color;
                //iconButton1.Text = currentBtn.Text;
                //iconButton1.ForeColor = color;

            }
        }

        private void SideBarBtnCLicked(object sender, EventArgs e, Form childForm = null)
        {

            //if (childForm != null)
            //{
            //    openChildForm(childForm);
            //}

            formWraper.Visible = true;
        }

        private void btnDocumentCycle_Click(object sender, EventArgs e)
        {
            if (!subTabBar.getSubTabsVisibleState())
            {
                subTabBar.setSubTabsVisible(true);
            }
        }

        private void btnTasnif_Click(object sender, EventArgs e)
        {
            subTabBar.setSubTabsVisible(false);
            //secondBarPanel.Visible = false;

        }
    }
}
