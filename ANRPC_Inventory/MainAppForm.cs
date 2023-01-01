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
        IDictionary<string ,List<APPLICATION_FORMS>> usersApplicationPrivilages = new Dictionary<string, List<APPLICATION_FORMS>>();


        enum APPLICATION_FORMS
        {
            TALB_TAWREED,
            TALB_TAWREED_FOREIGN,
            TALB_ESLAH,
            TALB_TANFIZ,
            TALB_MOAYRA,
            AMR_SHERAA,
            AMR_SHERAA_FOREIGN,
            AMR_SHERAA_KEMAWYAT,
            EZN_SARF,
            EDAFA_MAKHZANYA,
            EDAFA_MAKHZANYA_FOREIGN,
            MOTABAA_FANYA,
            MOTABAA_FANYA_FOREIGN,
            EZN_TAHWEEL,
            HARAKA,
            ESTLAM,
            ESTLAM_FOREIGN,
            SEARCH,
            NEW_TASNIF,
            DASHBOARD,
        }


        private void prepareUsersPrivilagesDict()
        {
            List<APPLICATION_FORMS> privilages;
            #region Edara
            privilages = new List<APPLICATION_FORMS>() {     
                APPLICATION_FORMS.TALB_TAWREED,
                APPLICATION_FORMS.TALB_TAWREED_FOREIGN,
                APPLICATION_FORMS.TALB_ESLAH,
                APPLICATION_FORMS.TALB_MOAYRA,
                APPLICATION_FORMS.TALB_TANFIZ,
                APPLICATION_FORMS.EZN_SARF,
                APPLICATION_FORMS.MOTABAA_FANYA,
                APPLICATION_FORMS.MOTABAA_FANYA_FOREIGN,
                APPLICATION_FORMS.EZN_TAHWEEL,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["Edara"] = privilages;
            #endregion

            #region Chairman
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.TALB_TAWREED,
                APPLICATION_FORMS.TALB_TAWREED_FOREIGN,
                APPLICATION_FORMS.TALB_ESLAH,
                APPLICATION_FORMS.TALB_MOAYRA,
                APPLICATION_FORMS.TALB_TANFIZ,
                APPLICATION_FORMS.AMR_SHERAA,
                APPLICATION_FORMS.AMR_SHERAA_KEMAWYAT,
                APPLICATION_FORMS.AMR_SHERAA_FOREIGN,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["Chairman"] = privilages;
            #endregion

            #region ViceChairman
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.TALB_TAWREED,
                APPLICATION_FORMS.TALB_TAWREED_FOREIGN,
                APPLICATION_FORMS.TALB_ESLAH,
                APPLICATION_FORMS.TALB_MOAYRA,
                APPLICATION_FORMS.TALB_TANFIZ,
                APPLICATION_FORMS.AMR_SHERAA,
                APPLICATION_FORMS.AMR_SHERAA_KEMAWYAT,
                APPLICATION_FORMS.AMR_SHERAA_FOREIGN,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["ViceChairman"] = privilages;
            #endregion

            #region TechnicalFollowUp
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.TALB_TAWREED,
                APPLICATION_FORMS.TALB_TAWREED_FOREIGN,
                APPLICATION_FORMS.TALB_ESLAH,
                APPLICATION_FORMS.TALB_MOAYRA,
                APPLICATION_FORMS.TALB_TANFIZ,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["TechnicalFollowUp"] = privilages;
            #endregion

            #region NewTasnif
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.TALB_TAWREED,
                APPLICATION_FORMS.TALB_TAWREED_FOREIGN,
                APPLICATION_FORMS.EZN_TAHWEEL,
                APPLICATION_FORMS.SEARCH,
                APPLICATION_FORMS.NEW_TASNIF,
            };


            usersApplicationPrivilages["NewTasnif"] = privilages;
            #endregion

            #region ChangeTasnif
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.TALB_TAWREED,
                APPLICATION_FORMS.TALB_TAWREED_FOREIGN,
                APPLICATION_FORMS.EZN_TAHWEEL,
                APPLICATION_FORMS.SEARCH,
                APPLICATION_FORMS.NEW_TASNIF,
            };


            usersApplicationPrivilages["ChangeTasnif"] = privilages;
            #endregion

            #region Mwazna
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.TALB_TAWREED,
                APPLICATION_FORMS.TALB_TAWREED_FOREIGN,
                APPLICATION_FORMS.TALB_ESLAH,
                APPLICATION_FORMS.TALB_MOAYRA,
                APPLICATION_FORMS.TALB_TANFIZ,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["Mwazna"] = privilages;
            #endregion

            #region Finance
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.EZN_SARF,
                APPLICATION_FORMS.EZN_TAHWEEL,
                APPLICATION_FORMS.AMR_SHERAA,
                APPLICATION_FORMS.AMR_SHERAA_KEMAWYAT,
                APPLICATION_FORMS.AMR_SHERAA_FOREIGN,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["Finance"] = privilages;
            #endregion

            #region Tkalif
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.EZN_SARF,
                APPLICATION_FORMS.EZN_TAHWEEL,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["Tkalif"] = privilages;
            #endregion

            #region Stock
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.AMR_SHERAA,
                APPLICATION_FORMS.AMR_SHERAA_KEMAWYAT,
                APPLICATION_FORMS.AMR_SHERAA_FOREIGN,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["Stock"] = privilages;
            #endregion

            #region GMInventory
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.TALB_TAWREED,
                APPLICATION_FORMS.TALB_TAWREED_FOREIGN,
                APPLICATION_FORMS.TALB_ESLAH,
                APPLICATION_FORMS.TALB_MOAYRA,
                APPLICATION_FORMS.TALB_TANFIZ,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["GMInventory"] = privilages;
            #endregion

            #region InventoryControl
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.TALB_TAWREED,
                APPLICATION_FORMS.TALB_TAWREED_FOREIGN,
                APPLICATION_FORMS.HARAKA,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["InventoryControl"] = privilages;
            #endregion

            #region Estlam
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.ESTLAM,
                APPLICATION_FORMS.ESTLAM_FOREIGN,
                APPLICATION_FORMS.EZN_TAHWEEL,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["Estlam"] = privilages;
            #endregion

            #region Edafa
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.EDAFA_MAKHZANYA,
                APPLICATION_FORMS.EDAFA_MAKHZANYA_FOREIGN,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["Edafa"] = privilages;
            #endregion

            #region Transfer1
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.EZN_TAHWEEL,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["Transfer1"] = privilages;
            #endregion

            #region Transfer2
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["Transfer2"] = privilages;
            #endregion

            #region Purchases
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.TALB_TAWREED,
                APPLICATION_FORMS.TALB_TAWREED_FOREIGN,
                APPLICATION_FORMS.TALB_ESLAH,
                APPLICATION_FORMS.TALB_MOAYRA,
                APPLICATION_FORMS.TALB_TANFIZ,

                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["Purchases"] = privilages;
            #endregion

            #region Sarf
            privilages = new List<APPLICATION_FORMS>() {
                APPLICATION_FORMS.EZN_SARF,
                APPLICATION_FORMS.SEARCH,
            };


            usersApplicationPrivilages["Sarf"] = privilages;
            #endregion
        }

        private Tab getTabByFormType(APPLICATION_FORMS form)
        {
            Tab tab = null;
            Font font = new Font("Calibri", 16, FontStyle.Bold);
            Color color = Color.FromArgb(227, 232, 234);
            Padding padd = new Padding(10, 0, 20, 0);
            Appearance appearance = new Appearance(0, Color.FromArgb(2, 163, 123), Color.FromArgb(2, 163, 123));

            if(form == APPLICATION_FORMS.DASHBOARD)
            {
                tab = new Tab(font, "  لوحة القيادة    ", color, 32, IconFont.Auto, IconChar.ChartSimple,
                                    DockStyle.Top, (object sender, EventArgs e) => { 
                                        SideBarBtnCLicked(sender, e, new conForm()); 
                                    },
                                    height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if (form == APPLICATION_FORMS.TALB_TAWREED)
            {
                tab = new Tab(font, "  طلب التوريد    ", color, 32, IconFont.Auto, IconChar.ClipboardList,
                                    DockStyle.Top, (object sender, EventArgs e) =>
                                    {
                                        bool isOnlyConfirm = Constants.User_Type == "A" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("TalbTawred", isOnlyConfirm));
                                    },
                                height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if (form == APPLICATION_FORMS.TALB_TAWREED_FOREIGN)
            {
                tab = new Tab(font, "  طلب التوريد الاجنبي   ", color, 32, IconFont.Auto, IconChar.ClipboardList,
                                    DockStyle.Top, (object sender, EventArgs e) =>
                                    {
                                        bool isOnlyConfirm = Constants.User_Type == "A" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("TalbTawred_Foreign", isOnlyConfirm));
                                    },
                                height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if(form == APPLICATION_FORMS.TALB_ESLAH)
            {
                tab = new Tab(font, "  طلب الاصلاح    ", color, 32, IconFont.Auto, IconChar.ClipboardList,
                                    DockStyle.Top, (object sender, EventArgs e) => {
                                        bool isOnlyConfirm = Constants.User_Type == "A" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("TalbEslah", isOnlyConfirm));
                                    },
                                height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if(form == APPLICATION_FORMS.TALB_TANFIZ)
            {
                tab = new Tab(font, "  طلب تنفيذ الأعمال    ", color, 32, IconFont.Auto, IconChar.ClipboardList,
                                    DockStyle.Top, (object sender, EventArgs e) => {
                                        bool isOnlyConfirm = Constants.User_Type == "A" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("TalbTnfiz", isOnlyConfirm));
                                    },
                                height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if(form == APPLICATION_FORMS.TALB_MOAYRA)
            {
                tab = new Tab(font, "  طلب المعايرة    ", color, 32, IconFont.Auto, IconChar.ClipboardList,
                                    DockStyle.Top, (object sender, EventArgs e) => {
                                        bool isOnlyConfirm = Constants.User_Type == "A" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("TalbMoaera", isOnlyConfirm));
                                    },
                                height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if(form == APPLICATION_FORMS.AMR_SHERAA)
            {
                tab = new Tab(font, "  أمر شراء    ", color, 35, IconFont.Auto, IconChar.MoneyCheckDollar,
                                    DockStyle.Top, (object sender, EventArgs e) => {
                                        bool isOnlyConfirm = Constants.User_Type == "B" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("AmrSheraa", isOnlyConfirm));
                                    },
                                    height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if (form == APPLICATION_FORMS.AMR_SHERAA_FOREIGN)
            {
                tab = new Tab(font, "  أمر شراء اجنبي   ", color, 35, IconFont.Auto, IconChar.MoneyCheckDollar,
                                    DockStyle.Top, (object sender, EventArgs e) => {
                                        bool isOnlyConfirm = Constants.User_Type == "B" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("AmrSheraa_Foreign", isOnlyConfirm));
                                    },
                                    height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if(form == APPLICATION_FORMS.AMR_SHERAA_KEMAWYAT)
            {
                tab = new Tab(font, " امر شراء الكيماويات ", color, 32, IconFont.Auto, IconChar.MoneyCheckDollar,
                                    DockStyle.Top, (object sender, EventArgs e) => {
                                        bool isOnlyConfirm = Constants.User_Type == "B" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("FChemical", isOnlyConfirm));
                                    },
                                    height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if(form == APPLICATION_FORMS.EZN_SARF)
            {
                tab = new Tab(font, "  إذن الصرف    ", color, 35, IconFont.Auto, IconChar.CartFlatbed,
                                    DockStyle.Top, (object sender, EventArgs e) => {
                                        bool isOnlyConfirm = Constants.User_Type == "A" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("EznSarf_F", isOnlyConfirm));
                                    },
                                    height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if(form == APPLICATION_FORMS.EDAFA_MAKHZANYA)
            {
                tab = new Tab(font, "  إضافة مخزنية    ", color, 32, IconFont.Auto, IconChar.ClipboardCheck,
                                    DockStyle.Top, (object sender, EventArgs e) => {
                                        bool isOnlyConfirm = Constants.User_Type == "B" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("FEdafaMakhzania_F", isOnlyConfirm));
                                    },
                                    height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if (form == APPLICATION_FORMS.EDAFA_MAKHZANYA_FOREIGN)
            {
                tab = new Tab(font, "  إضافة مخزنية اجنبي   ", color, 32, IconFont.Auto, IconChar.ClipboardCheck,
                                    DockStyle.Top, (object sender, EventArgs e) => {
                                        bool isOnlyConfirm = Constants.User_Type == "B" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("FEdafaMakhzania_F_Foreign", isOnlyConfirm));
                                    },
                                    height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if(form == APPLICATION_FORMS.MOTABAA_FANYA)
            {
                tab = new Tab(font, "  المطابقة الفنية    ", color, 32, IconFont.Auto, IconChar.ClipboardCheck,
                                DockStyle.Top, (object sender, EventArgs e) => {
                                    bool isOnlyConfirm = Constants.User_Type == "B" ? false : true;
                                    SideBarBtnCLicked(sender, e, new TabsHandler("FEdafaMakhzania_F", isOnlyConfirm));
                                },
                                height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if (form == APPLICATION_FORMS.MOTABAA_FANYA_FOREIGN)
            {
                tab = new Tab(font, "  المطابقة الفنية اجنبي   ", color, 32, IconFont.Auto, IconChar.ClipboardCheck,
                                DockStyle.Top, (object sender, EventArgs e) => {
                                    bool isOnlyConfirm = Constants.User_Type == "B" ? false : true;
                                    SideBarBtnCLicked(sender, e, new TabsHandler("FEdafaMakhzania_F_Foreign", isOnlyConfirm));
                                },
                                height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if(form == APPLICATION_FORMS.EZN_TAHWEEL)
            {
                tab = new Tab(font, "  إذون التحويل    ", color, 32, IconFont.Auto, IconChar.DiagramPredecessor,
                                DockStyle.Top, (object sender, EventArgs e) => {
                                    bool isOnlyConfirm = Constants.User_Type == "A" ? false : true;
                                    SideBarBtnCLicked(sender, e, new TabsHandler("FTransfer_M", isOnlyConfirm));
                                },
                                height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if(form == APPLICATION_FORMS.HARAKA)
            {
                tab = new Tab(font, "  حركة التصنيفات    ", color, 35, IconFont.Auto, IconChar.MoneyCheckDollar,
                                    DockStyle.Top, (object sender, EventArgs e) => {
                                        bool isOnlyConfirm = Constants.User_Type == "B" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("TasnifTrans", isOnlyConfirm));
                                    },
                                    height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if(form == APPLICATION_FORMS.ESTLAM)
            {
                tab = new Tab(font, "  إذن استلام    ", color, 32, IconFont.Auto, IconChar.ClipboardCheck,
                                    DockStyle.Top, (object sender, EventArgs e) => {
                                        bool isOnlyConfirm = Constants.User_Type == "B" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("Estlam_F", isOnlyConfirm));
                                    },
                                    height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if (form == APPLICATION_FORMS.ESTLAM_FOREIGN)
            {
                tab = new Tab(font, "  إذن استلام اجنبي   ", color, 32, IconFont.Auto, IconChar.ClipboardCheck,
                                    DockStyle.Top, (object sender, EventArgs e) => {
                                        bool isOnlyConfirm = Constants.User_Type == "B" ? false : true;
                                        SideBarBtnCLicked(sender, e, new TabsHandler("Estlam_Foreign", isOnlyConfirm));
                                    },
                                    height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if(form == APPLICATION_FORMS.SEARCH)
            {
                tab = new Tab(font, "  البـــــحــــث    ", color, 32, IconFont.Auto, IconChar.Search,
                                    DockStyle.Top, (object sender, EventArgs e) => { 
                                        SideBarBtnCLicked(sender, e, new Search()); 
                                    },
                                    height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            else if(form == APPLICATION_FORMS.NEW_TASNIF)
            {
                tab = new Tab(font, "  تصنيف جديد    ", color, 32, IconFont.Auto, IconChar.Search,
                                    DockStyle.Top, (object sender, EventArgs e) => { 
                                        SideBarBtnCLicked(sender, e, new Tasnif()); 
                                    },
                                    height: 54, padding: padd, appearance: appearance, isRL: true);
            }

            return tab;
        }

        private void prepareSideBarTabsAction(List<APPLICATION_FORMS> forms)
        {

            sideBarTabsList.Add(getTabByFormType(APPLICATION_FORMS.DASHBOARD));

            for(int i = 0; i < forms.Count; i++)
            {
                sideBarTabsList.Add(getTabByFormType(forms[i]));
            }
        }


        public MainAppForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            prepareUsersPrivilagesDict();
            prepareSideBarTabsAction(usersApplicationPrivilages[Constants.UserTypeB]);

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

        private void logOutBtn_Click(object sender, EventArgs e)
        {
            FLogin form = new FLogin();
            form.Show();
            this.Hide();
        }
    }
}
