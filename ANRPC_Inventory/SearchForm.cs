using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization;

namespace ANRPC_Inventory
{
    public partial class SearchForm : Form
    {
        IDictionary<MostndType, Mostand> MostandatData = new Dictionary<MostndType, Mostand>();

        public enum MostndType
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
            EZN_TAHWEEL,
        }


        private void prepareMostandatNames()
        {
            Mostand mostandData;

            #region TALB_TAWREED

            mostandData = new Mostand("طلب التوريد", "TalbTawred", 1);
            MostandatData[MostndType.TALB_TAWREED] = mostandData;
            #endregion

            #region TALB_TAWREED_FOREIGN

            mostandData = new Mostand("طلب التوريد الاجنبي", "TalbTawred_Foreign", 1,true);
            MostandatData[MostndType.TALB_TAWREED_FOREIGN] = mostandData;
            #endregion

            #region TALB_ESLAH

            mostandData = new Mostand("طلب الاصلاح", "TalbEslah", 8);
            MostandatData[MostndType.TALB_ESLAH] = mostandData;
            #endregion

            #region TALB_TANFIZ

            mostandData = new Mostand("طلب تنفيذ الأعمال", "TalbTnfiz", 10);
            MostandatData[MostndType.TALB_TANFIZ] = mostandData;
            #endregion

            #region TALB_MOAYRA

            mostandData = new Mostand("طلب المعايرة", "TalbMoaera", 9);
            MostandatData[MostndType.TALB_MOAYRA] = mostandData;
            #endregion

            #region AMR_SHERAA

            mostandData = new Mostand("أمر شراء", "AmrSheraa", 3);
            MostandatData[MostndType.AMR_SHERAA] = mostandData;
            #endregion

            #region AMR_SHERAA_FOREIGN

            mostandData = new Mostand("أمر شراء اجنبي", "AmrSheraa_Foreign", 3, true);
            MostandatData[MostndType.AMR_SHERAA_FOREIGN] = mostandData;
            #endregion

            #region AMR_SHERAA_KEMAWYAT

            mostandData = new Mostand("امر شراء الكيماويات", "FChemical", 12);
            MostandatData[MostndType.AMR_SHERAA_KEMAWYAT] = mostandData;
            #endregion

            #region EZN_SARF

            mostandData = new Mostand("إذن الصرف", "EznSarf_F", 2);
            MostandatData[MostndType.EZN_SARF] = mostandData;
            #endregion

            #region EDAFA_MAKHZANYA

            mostandData = new Mostand("إضافة مخزنية", "FEdafaMakhzania_F", 5);
            MostandatData[MostndType.EDAFA_MAKHZANYA] = mostandData;
            #endregion

            #region EDAFA_MAKHZANYA_FOREIGN

            mostandData = new Mostand("إضافة مخزنية اجنبي", "FEdafaMakhzania_F_Foreign", 5, true);
            MostandatData[MostndType.EDAFA_MAKHZANYA_FOREIGN] = mostandData;
            #endregion

            #region EZN_TAHWEEL

            mostandData = new Mostand("إذون التحويل", "FTransfer_M", 7);
            MostandatData[MostndType.EZN_TAHWEEL] = mostandData;
            #endregion
        }

        private void fillMostandNamesCombo()
        {
            List<KeyValuePair<MostndType, string>> source = MostandatData.Select(item => new KeyValuePair<MostndType, string>(item.Key, item.Value.displayName)).ToList();

            cmbMostandType.SelectedIndexChanged -= cmbMostandType_SelectedIndexChanged;

            cmbMostandType.DataSource = new BindingSource(source, null);
            cmbMostandType.DisplayMember = "Value";
            cmbMostandType.ValueMember = "Key";

            cmbMostandType.SelectedIndexChanged += cmbMostandType_SelectedIndexChanged;
        }

        private bool handleMostandTypes(MostndType type)
        {
            Constants.opencon();
            string cmdstring = "";

            if (type == MostndType.EZN_SARF)
            {
                cmdstring = "SELECT  [CCode],[CName] FROM [T_TransferTypes] where CType=2 and CFlag=1";
            }

            else if (type == MostndType.EDAFA_MAKHZANYA)
            {
                cmdstring = "SELECT  [CCode],[CName] FROM [T_TransferTypes] where CType=1 and CFlag=1";
            }

            else if (type == MostndType.EZN_TAHWEEL)
            {
                cmdstring = "SELECT [CCode],[CName] FROM[T_TransferTypes] where CType = 3 and CFlag = 1";
            }


            
            if (cmdstring != "")
            {
                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                //cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);
                DataTable dts = new DataTable();

                dts.Load(cmd.ExecuteReader());
                cmbMostandTypeInfo.DataSource = dts;
                cmbMostandTypeInfo.ValueMember = "CCode";
                cmbMostandTypeInfo.DisplayMember = "CName";
                cmbMostandTypeInfo.SelectedIndex = -1;

                return true;
            }

            return false;
        }

        private void handleMostandNumbers(MostndType type,bool isForeign = false)
        {
            if (isForeign)
            {
                Constants.openForeignCon();
            }
            else
            {
                Constants.opencon();
            }

            string cmdstring = "";

            if (type == MostndType.TALB_TAWREED)
            {
                cmdstring = "select (TalbTwareed_No) as mostand_number from  T_TalbTawreed where FYear = '" + cmbYear.Text + "' and CodeEdara = '" +Constants.CodeEdara + "'";
            }

            else if (type == MostndType.TALB_TAWREED_FOREIGN)
            {
                cmdstring = "select (TalbTwareed_No) as mostand_number from  T_TalbTawreed where FYear = '" + cmbYear.Text + "' and CodeEdara = '" + Constants.CodeEdara + "'";
            }

            else if (type == MostndType.TALB_ESLAH)
            {
                cmdstring = "select (Eslah_No) as mostand_number from T_TalbEslah where FYear = '" + cmbYear.Text + "' and CodeEdara = '" + Constants.CodeEdara + "'";
            }

            else if (type == MostndType.TALB_TANFIZ)
            {
                cmdstring = "select (Tanfiz_No) as mostand_number from  T_TalbTanfiz where FYear = '" + cmbYear.Text + "' and CodeEdara = '" + Constants.CodeEdara + "'";
            }

            else if (type == MostndType.TALB_MOAYRA)
            {
                cmdstring = "select (Moaera_No) as mostand_number from  T_TalbMoaera where FYear = '" + cmbYear.Text + "' and CodeEdara = '" + Constants.CodeEdara + "'";
            }

            else if (type == MostndType.AMR_SHERAA)
            {
                cmdstring = "select (Amrshraa_No) as mostand_number from  T_Awamershraa where AmrSheraa_sanamalia= '" + cmbYear.Text + "' and CodeEdara = '" + Constants.CodeEdara + "'";
            }

            else if (type == MostndType.AMR_SHERAA_FOREIGN)
            {
                cmdstring = "select (Amrshraa_No) as mostand_number from  T_Awamershraa where AmrSheraa_sanamalia= '" + cmbYear.Text + "' and CodeEdara = '" + Constants.CodeEdara + "'";
            }

            else if (type == MostndType.AMR_SHERAA_KEMAWYAT) // have issue here will see it later
            {
                cmdstring = "select (Amrshraa_No) as mostand_number from  T_Awamershraa where AmrSheraa_sanamalia= '" + cmbYear.Text + "' and CodeEdara = '" + Constants.CodeEdara + "'";
            }

            else if (type == MostndType.EZN_SARF)
            {
                cmdstring = "select(EznSarf_No) as mostand_number from T_EznSarf where FYear = '" + cmbYear.Text + "' and TR_NO = " + cmbMostandTypeInfo.SelectedValue.ToString() + " and CodeEdara = '" + Constants.CodeEdara + "'";
            }

            else if (type == MostndType.EDAFA_MAKHZANYA)
            {
                cmdstring = "SELECT [Edafa_No] as mostand_number from T_Edafa where Edafa_FY = '" + cmbYear.Text + "' and TR_NO = " + cmbMostandTypeInfo.SelectedValue.ToString() + " and CodeEdara = '"+ Constants.CodeEdara + "' group by Edafa_No order by  Edafa_No";
            }

            else if (type == MostndType.EDAFA_MAKHZANYA_FOREIGN)
            {
                cmdstring = "SELECT [Edafa_No] as mostand_number from T_Edafa where Edafa_FY = '" + cmbYear.Text + " and CodeEdara = '" + Constants.CodeEdara  + "' group by Edafa_No order by  Edafa_No";
            }

            else if (type == MostndType.EZN_TAHWEEL)
            {
                cmdstring = "select TransNo as mostand_number from T_EzonTahwel where FYear= '" + cmbYear.Text + "' and TR_NO = " + cmbMostandTypeInfo.SelectedValue.ToString() + " and CodeEdara = '" + Constants.CodeEdara + "'";
            }


            if(cmdstring == "")
            {
                return;
            }

            SqlCommand cmd = new SqlCommand(cmdstring, isForeign ? Constants.foreignCon : Constants.con);


            DataTable dts = new DataTable();

            dts.Load(cmd.ExecuteReader());
            cmbReqNo.DataSource = dts;
            cmbReqNo.ValueMember = "mostand_number";
            cmbReqNo.DisplayMember = "mostand_number";
            cmbReqNo.SelectedIndex = -1;

            if (isForeign)
            {
                Constants.closeForeignCon();
            }
            else
            {
                Constants.closecon();
            }          
        }

        public SearchForm()
        {
            InitializeComponent();

            prepareMostandatNames();
            fillMostandNamesCombo();

            HelperClass.comboBoxFiller(cmbYear, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);

            cmbMostandType.SelectedIndex = -1;
            cmbYear.SelectedIndex = -1;
        }

        private void showSearchTabHandler()
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;

            panel1.Hide();

            this.Controls.Add(panel);

            //End
            SearchMostandTabsHandler childForm = new SearchMostandTabsHandler();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel.Controls.Add(childForm);

            childForm.BringToFront();
            childForm.Show();
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            showSearchTabHandler();
        }

        private void cmbMostandType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbMostandType.SelectedIndex == -1)
            {
                return;  
            }

            MostndType type = (MostndType)cmbMostandType.SelectedValue;
            Mostand mostand = MostandatData[type];

            SelectedMostand.formName = mostand.formName;
            SelectedMostand.formNo = mostand.formNo;
            SelectedMostand.isForeign = mostand.isForeign;

            if (handleMostandTypes(type))
            {
                cmbMostandTypeInfo.Enabled = true;
            }
            else
            {
                cmbMostandTypeInfo.Enabled = false;
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostndType type = (MostndType)cmbMostandType.SelectedIndex;

            handleMostandNumbers(type,SelectedMostand.isForeign);

            SelectedMostand.mostandFinancialYear = cmbYear.Text;
        }

        private void cmbReqNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedMostand.mostandNumber = cmbReqNo.Text;
        }

        private void cmbMostandTypeInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMostandTypeInfo.SelectedIndex == -1)
            {
                return;
            }

            SelectedMostand.momayzMostand = cmbMostandTypeInfo.SelectedValue.ToString();
        }
    }
}
