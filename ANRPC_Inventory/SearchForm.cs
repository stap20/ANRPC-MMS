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
        public enum MostndType
        {
            TalbTawreed,
            EznSarf,
            AmrSheraa,
            EdafaMakhaznya,
            EznTahwel,
            TalbEslah,
            TalbMoaayra,
            talbTanfizAamal,
        }

        private bool handleMostandTypes(MostndType type)
        {
            Constants.opencon();
            string cmdstring = "";

            if (type == MostndType.EznSarf)
            {
                cmdstring = "SELECT  [CCode],[CName] FROM [T_TransferTypes] where CType=2 and CFlag=1";
            }

            else if (type == MostndType.EdafaMakhaznya)
            {
                cmdstring = "SELECT  [CCode],[CName] FROM [T_TransferTypes] where CType=1 and CFlag=1";
            }

            else if (type == MostndType.EznTahwel)
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

        private void handleMostandNumber(MostndType type)
        {
            Constants.opencon();
            string cmdstring = "";

            if (type == MostndType.TalbTawreed)
            {
                cmdstring = "select (TalbTwareed_No) as mostand_number from  T_TalbTawreed where FYear = '" + cmbYear.Text + "'";
            }

            else if (type == MostndType.EznSarf)
            {
                cmdstring = "select(EznSarf_No) as mostand_number from T_EznSarf where FYear = '" + cmbYear.Text + "' and TR_NO = " + cmbMostandTypeInfo.SelectedValue.ToString();
            }

            else if (type == MostndType.AmrSheraa)
            {
                cmdstring = "select (Amrshraa_No) as mostand_number from  T_Awamershraa where AmrSheraa_sanamalia= '" + cmbYear.Text + "'";
            }

            else if (type == MostndType.EdafaMakhaznya)
            {
                cmdstring = "SELECT [Edafa_No] as mostand_number from T_Edafa where Edafa_FY = '" + cmbYear.Text + "' and TR_NO = " + cmbMostandTypeInfo.SelectedValue.ToString() + " group by Edafa_No order by  Edafa_No";
            }

            else if (type == MostndType.EznTahwel)
            {
                cmdstring = "select TransNo as mostand_number from T_EzonTahwel where FYear= '" + cmbYear.Text + "' and TR_NO = " + cmbMostandTypeInfo.SelectedValue.ToString();
            }

            else if (type == MostndType.TalbEslah)
            {
                cmdstring = "select (Eslah_No) as mostand_number from T_TalbEslah where FYear = '" + cmbYear.Text + "'";
            }

            else if (type == MostndType.TalbMoaayra)
            {
                cmdstring = "select (Moaera_No) as mostand_number from  T_TalbMoaera where FYear = '" + cmbYear.Text + "'";
            }

            else if (type == MostndType.talbTanfizAamal)
            {
                cmdstring = "select (Tanfiz_No) as mostand_number from  T_TalbTanfiz where FYear = '" + cmbYear.Text + "'";
            }

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);


            DataTable dts = new DataTable();

            dts.Load(cmd.ExecuteReader());
            cmbReqNo.DataSource = dts;
            cmbReqNo.ValueMember = "mostand_number";
            cmbReqNo.DisplayMember = "mostand_number";
            cmbReqNo.SelectedIndex = -1;

            Constants.closecon();

        }

        private string getFormName(MostndType type)
        {
            string formName = "";

            if (type == MostndType.TalbTawreed)
            {
                formName = "TalbTawred";
            }

            else if (type == MostndType.EznSarf)
            {
                formName = "EznSarf_F";
            }

            else if (type == MostndType.AmrSheraa)
            {
                formName = "AmrSheraa";
            }

            else if (type == MostndType.EdafaMakhaznya)
            {
                formName = "FEdafaMakhzania_F";
            }

            else if (type == MostndType.EznTahwel)
            {
                formName = "FTransfer_M";
            }

            else if (type == MostndType.TalbEslah)
            {
                formName = "TalbEslah";
            }

            else if (type == MostndType.TalbMoaayra)
            {
                formName = "TalbMoaera";
            }

            else if (type == MostndType.talbTanfizAamal)
            {
                formName = "TalbTnfiz";

            }
            
            return formName;
        }

        public SearchForm()
        {
            InitializeComponent();
        }

        private void Search_TalbTawreed_Load(object sender, EventArgs e)
        {
            HelperClass.comboBoxFiller(cmbYear, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
            HelperClass.comboBoxFiller(cmbMostandType, TransHandler.getTrans(), "TransName", "TransName", this);
            cmbReqNo.DrawMode = DrawMode.OwnerDrawFixed;
            cmbReqNo.DropDownClosed += Cmb_Edara_DropDownClosed;

        }

        private void Cmb_Edara_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(cmbReqNo);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchMostandTabsHandler frm = new SearchMostandTabsHandler();
            this.Close();
            frm.Show();
        }

        private void cmbMostandType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostndType type = (MostndType)cmbMostandType.SelectedIndex;

            if(handleMostandTypes(type))
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

            handleMostandTypes(type);
        }
    }
}
