using ANRPC_Inventory.Resources;
using FontAwesome.Sharp;
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
    public partial class TabsHandler : Form
    {
        IDictionary<string, Func<Form>> formGetter = new Dictionary<string, Func<Form>>();
        private IconButton currentActiveTab;
        private Panel tabsActiveBorder;
        private Form currentChildForm;

        private string currentChildFormName;
        private void prepareTabsActiveIndecator()
        {
            tabsActiveBorder = new Panel();
            tabsBar.Controls.Add(tabsActiveBorder);
            tabsActiveBorder.Visible = false;
        }

        private void prepareFormGetter()
        {
            formGetter.Add("TalbTawred", () => { return new TalbTawred(); });
            formGetter.Add("TalbTawred_Foreign", () => { return new TalbTawred_Foreign(); });
            formGetter.Add("TalbTnfiz", () => { return new TalbTnfiz(); });
            formGetter.Add("TalbMoaera", () => { return new TalbMoaera(); });

            formGetter.Add("TalbEslah", () => { return new TalbEslah(); });
            formGetter.Add("EznSarf_F", () => { return new EznSarf_F(); });
            formGetter.Add("AmrSheraa", () => { return new AmrSheraa(); });
            formGetter.Add("AmrSheraa_Foreign", () => { return new AmrSheraa_Foreign(); });

            formGetter.Add("Estlam_F", () => { return new Estlam_F(); });
            formGetter.Add("Estlam_Foreign", () => { return new Estlam_Foreign(); });

            formGetter.Add("FEdafaMakhzania_F", () => { return new FEdafaMakhzania_F(); });
            formGetter.Add("FEdafaMakhzania_F_Foreign", () => { return new FEdafaMakhzania_F_Foreign(); });
            formGetter.Add("FTransfer_M", () => { return new FTransfer_M(); });
            formGetter.Add("FChemical", () => { return new FChemical(); });
        }

        public TabsHandler()
        {
            InitializeComponent();
            prepareTabsActiveIndecator();
        }

        public TabsHandler(string frmName,bool isOnlyConfirm = false)
        {
            InitializeComponent();
            prepareTabsActiveIndecator();
            prepareFormGetter();

            //btnAddEdit.PerformClick();

            this.currentChildFormName = frmName;

            if (isOnlyConfirm)
            {
                btnAddEdit.Visible = false;
            }
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

        private void TabBarBtnCLicked(object sender, EventArgs e, Color color, Form childForm = null)
        {
            ActivateButton(sender, color);

            if (childForm != null)
            {
                openChildForm(childForm);
            }


            formWraper.Visible = true;
        }


        private void btnAddEdit_Click(object sender, EventArgs e)
        {
            Constants.isConfirmForm = false;
            TabBarBtnCLicked(sender, e, RGBColors.color1, formGetter[currentChildFormName]());
        }

        private void btnFollowSignature_Click(object sender, EventArgs e)
        {
            Constants.isConfirmForm = true;
            TabBarBtnCLicked(sender, e, RGBColors.color1, formGetter[currentChildFormName]());
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
