using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ANRPC_Inventory.Resources;
using FontAwesome.Sharp;

namespace ANRPC_Inventory
{
    public partial class MainAppForm : Form
    {
        //Fields
        private Form currentChildForm;
        List<Tab> sideBarTabsList = new List<Tab>();
        private SideBarHandler sideBarTabContainer;

        private void prepareSideBarTabsAction()
        {
            Font font = new Font("Calibri", 16, FontStyle.Bold);
            Color color = Color.FromArgb(227, 232, 234);
            Padding padd = new Padding(10, 0, 20, 0);
            Appearance appearance = new Appearance(0, Color.FromArgb(2, 163, 123), Color.FromArgb(2, 163, 123));

            sideBarTabsList.Add(new Tab(font, "  لوحة القيادة    ", color, 32, IconFont.Auto, IconChar.ChartSimple,
                                DockStyle.Top, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e, new conForm()); },
                                height: 54, padding: padd, appearance: appearance,isRL:true));

            sideBarTabsList.Add(new Tab(font, "  طلب التوريد    ", color, 32, IconFont.Auto, IconChar.ClipboardList,
                                DockStyle.Top, (object sender, EventArgs e) => { 
                                    bool isConfirm = Constants.User_Type == "A" ? false : true;
                                    SideBarBtnCLicked(sender, e, new TabsHandler(new TalbTawred(), isConfirm)); 
                                },
                            height: 54, padding: padd, appearance: appearance, isRL: true));

            sideBarTabsList.Add(new Tab(font, "  طلب تنفيذ الأعمال    ", color, 32, IconFont.Auto, IconChar.ClipboardList,
                                DockStyle.Top, (object sender, EventArgs e) => {
                                    bool isConfirm = Constants.User_Type == "A" ? false : true;
                                    SideBarBtnCLicked(sender, e, new TabsHandler(new TalbTnfiz(), isConfirm));
                                },
                            height: 54, padding: padd, appearance: appearance, isRL: true));


            sideBarTabsList.Add(new Tab(font, "  طلب المعايرة    ", color, 32, IconFont.Auto, IconChar.ClipboardList,
                                DockStyle.Top, (object sender, EventArgs e) => {
                                    bool isConfirm = Constants.User_Type == "A" ? false : true;
                                    SideBarBtnCLicked(sender, e, new TabsHandler(new TalbMoaera(), isConfirm));
                                },
                            height: 54, padding: padd, appearance: appearance, isRL: true));

            sideBarTabsList.Add(new Tab(font, "  طلب الاصلاح    ", color, 32, IconFont.Auto, IconChar.ClipboardList,
                                DockStyle.Top, (object sender, EventArgs e) => {
                                    bool isConfirm = Constants.User_Type == "A" ? false : true;
                                    SideBarBtnCLicked(sender, e, new TabsHandler(new TalbEslah(), isConfirm));
                                },
                            height: 54, padding: padd, appearance: appearance, isRL: true));


            sideBarTabsList.Add(new Tab(font, "  إذن الصرف    ", color, 35, IconFont.Auto, IconChar.CartFlatbed,
                    DockStyle.Top, (object sender, EventArgs e) => {
                        bool isConfirm = Constants.User_Type == "A" ? false : true;
                        SideBarBtnCLicked(sender, e, new TabsHandler(new EznSarf_F(), isConfirm)); 
                    },
                    height: 54, padding: padd, appearance: appearance, isRL: true));




            if (Constants.User_Type == "B")
            {
                sideBarTabsList.Add(new Tab(font, "  أمر شراء    ", color, 35, IconFont.Auto, IconChar.MoneyCheckDollar,
                      DockStyle.Top, (object sender, EventArgs e) => {
                          bool isConfirm = Constants.User_Type == "A" ? false : true;
                          SideBarBtnCLicked(sender, e, new TabsHandler(new AmrSheraa(), isConfirm));               
                      },
                      height: 54, padding: padd, appearance: appearance, isRL: true));
            }

            if (Constants.User_Type == "B")
            {
                sideBarTabsList.Add(new Tab(font, "  إضافة مخزنية    ", color, 32, IconFont.Auto, IconChar.ClipboardCheck,
                                DockStyle.Top, (object sender, EventArgs e) => {
                                    bool isConfirm = Constants.User_Type == "A" ? false : true;
                                    SideBarBtnCLicked(sender, e, new TabsHandler(new FEdafaMakhzania_F(), isConfirm)); 
                                },
                                height: 54, padding: padd, appearance: appearance, isRL: true));
            }
            else
            {
                sideBarTabsList.Add(new Tab(font, "  المطابقة الفنية    ", color, 32, IconFont.Auto, IconChar.ClipboardCheck,
                                DockStyle.Top, (object sender, EventArgs e) => {
                                    bool isConfirm = Constants.User_Type == "A" ? false : true;
                                    SideBarBtnCLicked(sender, e, new TabsHandler(new FEdafaMakhzania_F(), isConfirm)); 
                                },
                                height: 54, padding: padd, appearance: appearance, isRL: true));

            }



            sideBarTabsList.Add(new Tab(font, "  إذون التحويل    ", color, 32, IconFont.Auto, IconChar.DiagramPredecessor,
                    DockStyle.Top, (object sender, EventArgs e) => {
                        bool isConfirm = Constants.User_Type == "A" ? false : true;
                        SideBarBtnCLicked(sender, e, new TabsHandler(new FTransfer_M(), isConfirm)); 
                    },
                    height: 54, padding: padd, appearance: appearance, isRL: true));

            sideBarTabsList.Add(new Tab(font, "  البـــــحــــث    ", color, 32, IconFont.Auto, IconChar.Search,
                    DockStyle.Top, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e, new Search()); },
                    height: 54, padding: padd, appearance: appearance, isRL: true));


            sideBarTabsList.Add(new Tab(font, "  الكيماويات    ", color, 32, IconFont.Auto, IconChar.Search,
            DockStyle.Top, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e, new TabsHandler(new FChemical())); },
            height: 54, padding: padd, appearance: appearance, isRL: true));

            //sideBarTabsList.Add(new Tab(font, "  الإعدادت    ", color, 32, IconFont.Auto, IconChar.Cog,
            //    DockStyle.Top, (object sender, EventArgs e) => { SideBarBtnCLicked(sender, e, new conForm()); },
            //    height: 54, padding: padd, appearance: appearance, isRL: true));
        }


        public MainAppForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            prepareSideBarTabsAction();

            sideBarTabContainer = new SideBarHandler(sideBarTabsList, panelButtons);
        }

        private void handleAppBar(object senderBtn)
        {
            IconButton currentBtn = (IconButton)senderBtn;

            Color color = Color.FromArgb(0, 114, 86);
            Color textColor = Color.FromArgb(18, 18, 18);

            iconButton1.Visible = true;
            iconButton1.IconChar = currentBtn.IconChar;
            iconButton1.IconColor = color;
            iconButton1.Text = currentBtn.Text;
            iconButton1.ForeColor = textColor;
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

            formwraper.Controls.Add(childForm);

            formwraper.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }


        private void SideBarBtnCLicked(object sender, EventArgs e, Form childForm = null)
        {
            handleAppBar(sender);

            if (childForm != null)
            {
                openChildForm(childForm);
            }


            formwraper.Visible = true;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        } 

        private void btn_max_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btn_max.SendToBack();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnWindowReset_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnWindowReset.SendToBack();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
