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
using System.Windows.Forms;


namespace ANRPC_Inventory.Resources
{
    public partial class FChemical : Form
    {
        public SqlConnection con;//sql conn for anrpc_sms db
        public int control_flag; // Flag For Control buttons
        //--------------------
        public Boolean executemsg;
        List<CurrencyInfo> currencies = new List<CurrencyInfo>();
        //--------------------
        AutoCompleteStringCollection sader_to = new AutoCompleteStringCollection(); // Sader_to
        public FChemical()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }


        //=========================================================
        //          Reset all the labels and values
        //=========================================================
        private void reset()
        {
            //Labels
            //-----
            L_TEARKEZ.Visible = false;
            L_TEARKEZ1.Visible = false;
            L_MONTH.Visible = false;
            L_YEAR.Visible = false;
            L_NOLON.Visible = false;
            L_NOLON1.Visible = false;
            L_DNOLON.Visible = false;
            L_DNOLON1.Visible = false;
            L_AMELEN.Visible = false;
            L_KHEDMA.Visible = false;
            L_Edaria.Visible = false;
            L_Edaria1.Visible = false;
            L_TAKAFOL.Visible = false;
            L_TAKAFOL1.Visible = false;
            L_HARBYA.Visible = false;
            L_KASRGNEH.Visible = false;
            L_DAREBA_EDARIA.Visible = false;
            L_DAREBA_NOLON.Visible = false;
            //----------------------------------
            //Values
            //------
            TXT_tarkez.Visible = false;
            CMB_Month.Visible = false;
            CMB_Year.Visible = false;
            TXT_NOLON.Visible = false;
            TXT_DNOLON.Visible = false;
            TXT_SANDOAMLEN.Visible = false;
            TXT_Tare2.Visible = false;
            TXT_Edaria.Visible = false;
            TXT_Takafol.Visible = false;
            checkBox_Harbya.Visible = false;
            checkBox_Kasr.Visible = false;
            //-------------------------------------
            TXT_NOLON_v.Visible = false;
            TXT_DNOLON_v.Visible = false;
            TXT_SANDOAMLEN_v.Visible = false;
            TXT_Tare2_v.Visible = false;
            TXT_Edaria_v.Visible = false;
            TXT_Takafol_v.Visible = false;
            TXT_DAREBA_EDARIA_v.Visible = false;
            TXT_DAREBA_NOLON_v.Visible = false;
            //---------------------------------------
        }

        //=========================================================
        private void reset_values()
        {
            TXT_AmrNo.Text = "";
            Cmb_FY.Text = "";
            CMB_Sadr.Text = "";
            TXT_MomayazMos.Text = "";
            TXT_TaslemDate.Text = "";
            TXT_TaslemPlace.Text = "";
            CMB_Edara.Text = "";
            TXT_BndMwazna.Text = "";
            TXT_Shik.Text = "";
            TXT_Pay.Text = "";
            TXT_TalbTawred.Text = "";
            TXT_Mowared.Text = "";
            TXT_Egmali.Text = "";
            TXT_EgmaliArabic.Text = "";
            //----------------------------------------
            TXT_BAND.Text = "";
            TXT_Quantity.Text = "";
            TXT_Unit.Text = "";
            TXT_tarkez.Text = "";
            CMB_Month.Text = "";
            CMB_Year.Text = "";
            TXT_NOLON.Text = "";
            TXT_DNOLON.Text = "";
            TXT_SANDOAMLEN.Text = "";
            TXT_Tare2.Text = "";
            TXT_Edaria.Text = "";
            TXT_Takafol.Text = "";
            checkBox_Harbya.Text = "";
            checkBox_Kasr.Text = "";
            TXT_Total_value.Text = "";
            //-------------------------------------
            TXT_NOLON_v.Text = "";
            TXT_DNOLON_v.Text = "";
            TXT_SANDOAMLEN_v.Text = "";
            TXT_Tare2_v.Text = "";
            TXT_Edaria_v.Text = "";
            TXT_Takafol_v.Text = "";
            TXT_DAREBA_EDARIA_v.Text = "";
            TXT_DAREBA_NOLON_v.Text = "";
            //---------------------------------------
        }

        //---------------------------------------------------------------


        // The labels Related to the Component
        //=====================================

        public void Component_ID()
        {
            switch (CMB_Component.SelectedValue)
            {
                case 11:
                    L_TEARKEZ.Visible = true;
                    TXT_tarkez.Visible = true;
                    L_TEARKEZ1.Visible = true;
                    L_DAREBA_EDARIA.Visible = true;
                    TXT_DAREBA_EDARIA_v.Visible = true;
                    break;

                //-------------------------
                case 12:
                    L_TEARKEZ.Visible = true;
                    TXT_tarkez.Visible = true;
                    L_TEARKEZ1.Visible = true;
                    //------------
                    L_NOLON.Visible = true;
                    L_NOLON1.Visible = true;
                    TXT_NOLON.Visible = true;
                    TXT_NOLON_v.Visible = true;
                    //------------
                    L_Edaria.Visible = true;
                    TXT_Edaria.Visible = true;
                    L_Edaria1.Visible = true;
                    TXT_Edaria_v.Visible = true;
                    //------------
                    L_DAREBA_EDARIA.Visible = true;
                    TXT_DAREBA_EDARIA_v.Visible = true;
                    //-----------
                    L_DAREBA_NOLON.Visible = true;
                    TXT_DAREBA_NOLON_v.Visible = true;
                    //-------------------------
                    break;

                case 13:
                    L_TEARKEZ.Visible = true;
                    TXT_tarkez.Visible = true;
                    L_TEARKEZ1.Visible = true;
                    //------------
                    L_NOLON.Visible = true;
                    L_NOLON1.Visible = true;
                    TXT_NOLON.Visible = true;
                    TXT_NOLON_v.Visible = true;
                    //------------
                    L_DAREBA_EDARIA.Visible = true;
                    TXT_DAREBA_EDARIA_v.Visible = true;
                    //-------------------------
                    break;

                case 21:
                    L_MONTH.Visible = true;
                    CMB_Month.Visible = true;
                    L_YEAR.Visible = true;
                    CMB_Year.Visible = true;
                    //------------
                    L_DAREBA_EDARIA.Visible = true;
                    TXT_DAREBA_EDARIA_v.Visible = true;
                    //----------------------------
                    break;
                case 31:
                    L_TAKAFOL.Visible = true;
                    TXT_Takafol.Visible = true;
                    L_TAKAFOL1.Visible = true;
                    TXT_Takafol_v.Visible = true;
                    //--------------
                    L_HARBYA.Visible = true;
                    checkBox_Harbya.Visible = true;
                    //----------------------------
                    break;

                case 41:
                    L_TEARKEZ.Visible = true;
                    TXT_tarkez.Visible = true;
                    L_TEARKEZ1.Visible = true;
                    //------------
                    L_NOLON.Visible = true;
                    L_NOLON1.Visible = true;
                    L_NOLON1.Text = "جنيه";
                    TXT_NOLON.Visible = true;
                    TXT_NOLON_v.Visible = true;
                    //------------
                    L_DNOLON.Visible = true;
                    TXT_DNOLON.Visible = true;
                    L_DNOLON1.Visible = true;
                    TXT_DNOLON_v.Visible = true;
                    //------------
                    L_KHEDMA.Visible = true;
                    TXT_Tare2.Visible = true;
                    TXT_Tare2_v.Visible = true;
                    //-------------
                    L_KASRGNEH.Visible = true;
                    L_DAREBA_EDARIA.Visible = true;
                    L_DAREBA_NOLON.Visible = true;
                    //-------------------------------
                    break;

                case 42:
                    L_TEARKEZ.Visible = true;
                    TXT_tarkez.Visible = true;
                    L_TEARKEZ1.Visible = true;
                    //------------
                    L_NOLON.Visible = true;
                    L_NOLON1.Visible = true;
                    L_NOLON1.Text = "جنيه";
                    TXT_NOLON.Visible = true;
                    TXT_NOLON_v.Visible = true;
                    //------------
                    L_DNOLON.Visible = true;
                    TXT_DNOLON.Visible = true;
                    L_DNOLON1.Visible = true;
                    TXT_DNOLON_v.Visible = true;
                    //------------
                    L_KHEDMA.Visible = true;
                    TXT_Tare2.Visible = true;
                    TXT_Tare2_v.Visible = true;
                    //-------------
                    L_KASRGNEH.Visible = true;
                    L_DAREBA_EDARIA.Visible = true;
                    L_DAREBA_NOLON.Visible = true;
                    //-----------------------------------------
                    break;

                case 43:
                    L_TEARKEZ.Visible = true;
                    TXT_tarkez.Visible = true;
                    L_TEARKEZ1.Visible = true;
                    //------------
                    L_NOLON.Visible = true;
                    L_NOLON1.Visible = true;
                    L_NOLON1.Text = "جنيه";
                    TXT_NOLON.Visible = true;
                    TXT_NOLON_v.Visible = true;
                    //------------
                    L_DNOLON.Visible = true;
                    TXT_DNOLON.Visible = true;
                    L_DNOLON1.Visible = true;
                    TXT_DNOLON_v.Visible = true;
                    //------------
                    L_KHEDMA.Visible = true;
                    TXT_Tare2.Visible = true;
                    TXT_Tare2_v.Visible = true;
                    //-------------
                    // L_KASRGNEH.Visible = true;
                    L_DAREBA_EDARIA.Visible = true;
                    L_DAREBA_NOLON.Visible = true;
                    break;

                case 44:
                    L_TEARKEZ.Visible = true;
                    TXT_tarkez.Visible = true;
                    L_TEARKEZ1.Visible = true;
                    //------------
                    L_DAREBA_EDARIA.Visible = true;
                    TXT_DAREBA_EDARIA_v.Visible = true;
                    //---------------------------------
                    break;

                case 51:
                    L_TEARKEZ.Visible = true;
                    TXT_tarkez.Visible = true;
                    L_TEARKEZ1.Visible = true;
                    //------------
                    L_AMELEN.Visible = true;
                    TXT_SANDOAMLEN.Visible = true;
                    TXT_SANDOAMLEN_v.Visible = true;
                    //------------
                    L_DAREBA_EDARIA.Visible = true;
                    TXT_DAREBA_EDARIA_v.Visible = true;
                    break;

                default:
                    break;
            }

        }
        //=========================================================


        private void calculate_fun(int Comp_id)
        {
            switch (Comp_id)
            {
                case 11:
                    TXT_Total_value.Text = (Convert.ToDouble(TXT_UnitPrice.Text) + Convert.ToDouble(TXT_Quantity.Text)).ToString();
                    TXT_DAREBA_EDARIA_v.Text = (Convert.ToDouble(TXT_Total_value.Text) * 0.14).ToString();
                    TXT_Egmali.Text = Math.Round((Convert.ToDouble(TXT_Total_value.Text) + Convert.ToDouble(TXT_DAREBA_EDARIA_v.Text)), 2).ToString();
                    break;
                //----------
                case 12:
                    TXT_Total_value.Text = (Convert.ToDouble(TXT_UnitPrice.Text) * Math.Round(((Convert.ToDouble(TXT_tarkez.Text) / 100) * Convert.ToDouble(TXT_Quantity.Text)), 4)).ToString();
                    TXT_NOLON_v.Text = Math.Round((Convert.ToDouble(TXT_NOLON.Text) * Convert.ToDouble(TXT_Quantity.Text)), 4).ToString();
                    TXT_Edaria_v.Text = (Convert.ToDouble(TXT_Edaria.Text) * Math.Round(((Convert.ToDouble(TXT_tarkez.Text) / 100) * Convert.ToDouble(TXT_Quantity.Text)), 4)).ToString();
                    TXT_DAREBA_NOLON_v.Text = (Convert.ToDouble(TXT_NOLON_v.Text) * 0.14).ToString();
                    TXT_DAREBA_EDARIA_v.Text = (Convert.ToDouble(TXT_Total_value.Text) * 0.14).ToString();
                    TXT_Egmali.Text = Math.Round(((Convert.ToDouble(TXT_Total_value.Text) + Convert.ToDouble(TXT_NOLON_v.Text) + Convert.ToDouble(TXT_Edaria_v.Text) + Convert.ToDouble(TXT_DAREBA_NOLON_v.Text) + Convert.ToDouble(TXT_DAREBA_EDARIA_v.Text))), 2).ToString();
                    break;
                //---------
                case 13:
                    double y = Math.Round(((Convert.ToDouble(TXT_tarkez.Text) / 100) / 0.3), 4);
                    TXT_Total_value.Text = Math.Round((Convert.ToDouble(TXT_Quantity.Text) * Convert.ToDouble(TXT_UnitPrice.Text) * y), 4).ToString();
                    TXT_NOLON_v.Text = TXT_NOLON.Text;
                    TXT_DAREBA_EDARIA_v.Text = (Convert.ToDouble(TXT_Total_value.Text) * 0.14).ToString();
                    TXT_DAREBA_NOLON_v.Text = (Convert.ToDouble(TXT_NOLON.Text) * 0.14).ToString();
                    TXT_Egmali.Text = Math.Round(((Convert.ToDouble(TXT_Total_value.Text) + Convert.ToDouble(TXT_NOLON_v.Text) + Convert.ToDouble(TXT_DAREBA_NOLON_v.Text) + Convert.ToDouble(TXT_DAREBA_EDARIA_v.Text))), 2).ToString();
                    break;
                //-------

                //==========================================================================================
                case 21:
                    TXT_Total_value.Text = Math.Round((Convert.ToDouble(TXT_Quantity.Text) * Convert.ToDouble(TXT_UnitPrice.Text)), 2).ToString();
                    TXT_DAREBA_EDARIA_v.Text = Math.Round((Convert.ToDouble(TXT_Total_value.Text) * 0.14), 2).ToString();
                    TXT_Egmali.Text = Math.Round((Convert.ToDouble(TXT_Total_value.Text) + Convert.ToDouble(TXT_DAREBA_EDARIA_v.Text)), 2).ToString();
                    break;
                //--------

                //==========================================================================================

                case 31:
                    TXT_Total_value.Text = Math.Round((Convert.ToDouble(TXT_Quantity.Text) * Convert.ToDouble(TXT_UnitPrice.Text)), 2).ToString();
                    TXT_Takafol_v.Text = Math.Round(((Convert.ToDouble(TXT_Takafol.Text) / 100) * Convert.ToDouble(TXT_Total_value.Text)), 2).ToString();
                    TXT_Egmali.Text = Math.Round((Convert.ToDouble(TXT_Total_value.Text) + Convert.ToDouble(TXT_Takafol_v.Text)), 2).ToString();

                    if (checkBox_Harbya.Checked == true)
                    {
                        TXT_Egmali.Text = (Convert.ToDouble(TXT_Egmali.Text) + 5).ToString();
                    }

                    break;
                //-------

                //==========================================================================================
                case 41:
                    double x = Math.Round(((Convert.ToDouble(TXT_tarkez.Text) / 100) / 0.98) * Convert.ToDouble(TXT_Quantity.Text), 3);
                    TXT_Total_value.Text = Math.Round(Convert.ToDouble(TXT_UnitPrice.Text) * x, 3).ToString();
                    TXT_DAREBA_EDARIA_v.Text = Math.Round((Convert.ToDouble(TXT_Total_value.Text) * 0.14), 3).ToString();
                    TXT_NOLON_v.Text = TXT_NOLON.Text;
                    TXT_DNOLON_v.Text = TXT_DNOLON.Text;
                    TXT_Tare2_v.Text = TXT_Tare2.Text;
                    TXT_DAREBA_NOLON_v.Text = Math.Round((Convert.ToDouble(TXT_NOLON_v.Text) * 0.14), 3).ToString();

                    TXT_Egmali.Text = Math.Round(Convert.ToDouble(TXT_Total_value.Text) + Convert.ToDouble(TXT_DAREBA_EDARIA_v.Text) + Convert.ToDouble(TXT_DNOLON_v.Text) + Convert.ToDouble(TXT_DAREBA_NOLON_v.Text) + Convert.ToDouble(TXT_Tare2_v.Text), 2).ToString();

                    if (checkBox_Kasr.Checked == true)
                    {
                        TXT_Egmali.Text = (Convert.ToInt32(TXT_Egmali.Text) + 1).ToString();
                    }
                    break;
                //-------
                case 42:
                    TXT_Total_value.Text = Math.Round((Convert.ToDouble(TXT_Quantity.Text) * Convert.ToDouble(TXT_UnitPrice.Text)), 3).ToString();
                    TXT_DAREBA_EDARIA_v.Text = Math.Round((Convert.ToDouble(TXT_Total_value.Text) * 0.14), 3).ToString();
                    TXT_NOLON_v.Text = TXT_NOLON.Text;
                    TXT_DNOLON_v.Text = TXT_DNOLON.Text;
                    TXT_Tare2_v.Text = TXT_Tare2.Text;
                    TXT_DAREBA_NOLON_v.Text = Math.Round((Convert.ToDouble(TXT_NOLON_v.Text) * 0.14), 3).ToString();

                    TXT_Egmali.Text = Math.Round(Convert.ToDouble(TXT_Total_value.Text) + Convert.ToDouble(TXT_DAREBA_EDARIA_v.Text) + Convert.ToDouble(TXT_DNOLON_v.Text) + Convert.ToDouble(TXT_DAREBA_NOLON_v.Text) + Convert.ToDouble(TXT_Tare2_v.Text), 2).ToString();

                    if (checkBox_Kasr.Checked == true)
                    {
                        TXT_Egmali.Text = (Convert.ToInt32(TXT_Egmali.Text) + 1).ToString();
                    }
                    break;
                //-------
                case 43:
                    TXT_Total_value.Text = Math.Round((Convert.ToDouble(TXT_Quantity.Text) * Convert.ToDouble(TXT_UnitPrice.Text)), 3).ToString();
                    TXT_DAREBA_EDARIA_v.Text = Math.Round((Convert.ToDouble(TXT_Total_value.Text) * 0.14), 3).ToString();
                    TXT_NOLON_v.Text = TXT_NOLON.Text;
                    TXT_DNOLON_v.Text = TXT_DNOLON.Text;
                    TXT_Tare2_v.Text = TXT_Tare2.Text;
                    TXT_DAREBA_NOLON_v.Text = Math.Round((Convert.ToDouble(TXT_NOLON_v.Text) * 0.14), 3).ToString();

                    TXT_Egmali.Text = Math.Round(Convert.ToDouble(TXT_Total_value.Text) + Convert.ToDouble(TXT_DAREBA_EDARIA_v.Text) + Convert.ToDouble(TXT_DNOLON_v.Text) + Convert.ToDouble(TXT_DAREBA_NOLON_v.Text) + Convert.ToDouble(TXT_Tare2_v.Text), 2).ToString();

                    if (checkBox_Kasr.Checked == true)
                    {
                        TXT_Egmali.Text = (Convert.ToInt32(TXT_Egmali.Text) + 1).ToString();
                    }

                    break;
                //-------
                case 44:
                    TXT_Total_value.Text = Math.Round((Convert.ToDouble(TXT_Quantity.Text) * Convert.ToDouble(TXT_UnitPrice.Text)), 2).ToString();
                    TXT_DAREBA_EDARIA_v.Text = Math.Round((Convert.ToDouble(TXT_Total_value.Text) * 0.14), 2).ToString();
                    TXT_Egmali.Text = Math.Round((Convert.ToDouble(TXT_Total_value.Text) + Convert.ToDouble(TXT_DAREBA_EDARIA_v.Text)), 2).ToString();
                    break;
                //-------

                //====================================================================
                case 51:
                    TXT_Total_value.Text = Math.Round((Convert.ToDouble(TXT_Quantity.Text) * Convert.ToDouble(TXT_UnitPrice.Text)), 2).ToString();
                    TXT_SANDOAMLEN_v.Text = (Convert.ToDouble(TXT_SANDOAMLEN.Text) * Convert.ToDouble(TXT_Quantity.Text)).ToString();
                    TXT_DAREBA_EDARIA_v.Text = Math.Round((Convert.ToDouble(TXT_Total_value.Text) * 0.14), 2).ToString();
                    TXT_Egmali.Text = Math.Round((Convert.ToDouble(TXT_Total_value.Text) + Convert.ToDouble(TXT_DAREBA_EDARIA_v.Text) + Convert.ToDouble(TXT_SANDOAMLEN_v.Text)), 2).ToString();

                    break;
                //-------
                default:
                    break;
            }
        }
        //=========================================
        private void FChemical_Load(object sender, EventArgs e)
        {
            //Reset Values
            //---------------
            reset();
            reset_values();
            control_flag = 0;
            //----------------
            con = new SqlConnection(Constants.constring);
            con.Open();

            // Sader_To
            //-----------
            string query = "SELECT Distinct [CompanyName],[CompanyID] FROM Compny_Master order by CompanyID  ";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dts = new DataTable();
            dts.Load(cmd.ExecuteReader());
            CMB_Sadr.DataSource = dts;
            CMB_Sadr.ValueMember = "CompanyID";
            CMB_Sadr.DisplayMember = "CompanyName";

            //-------------------------------------------
            //Edarat
            //-------
            string query1 = "SELECT [Edara_name] FROM Edara_Chemical   ";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            DataTable dts1 = new DataTable();
            dts1.Load(cmd1.ExecuteReader());
            CMB_Edara.DataSource = dts1;
            CMB_Edara.ValueMember = "Edara_name";
            CMB_Edara.DisplayMember = "Edara_name";
            con.Close();

            //-------------------------------------
            CMB_Edara.SelectedIndex = -1;
            CMB_Sadr.SelectedIndex = -1;
        }

        // Company Has been Choosen
        //---------------------------

        private void CMB_Sadr_SelectionChangeCommitted(object sender, EventArgs e)
        {
            con = new SqlConnection(Constants.constring);
            con.Open();

            // Components
            //-----------
            string query = "SELECT  [Component],[Compn_ID] FROM Compny_Master where [CompanyID] = @u  ";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dts = new DataTable();

            cmd.Parameters.AddWithValue("@u", Convert.ToInt32(CMB_Sadr.SelectedValue));
            dts.Load(cmd.ExecuteReader());
            CMB_Component.DataSource = dts;
            CMB_Component.ValueMember = "Compn_ID";
            CMB_Component.DisplayMember = "Component";
            con.Close();

            //-------------------------------------
            CMB_Component.SelectedIndex = -1;

            //---------------------------------------------------

            //Shik
            //*************
            TXT_Shik.Text = CMB_Sadr.Text;
            //*************
            //Makan_El_Taslem
            //*************
            if (Convert.ToInt32(CMB_Sadr.SelectedValue) == 5)
            { TXT_TaslemPlace.Text = "كفر الزيات"; }
            else { TXT_TaslemPlace.Text = "مخازن أنربك"; }
            //*************
            //Payment_Method
            //*************
            if (Convert.ToInt32(CMB_Sadr.SelectedValue) == 5 || Convert.ToInt32(CMB_Sadr.SelectedValue) == 3)
            { TXT_Pay.Text = "  %مقابل الإستلام100"; }
            else { TXT_Pay.Text = "  %بعد الإستلام و المطابقة100"; }
            //*************
            //Date_tasleem
            //*************
            TXT_TaslemDate.Text = "تورد حسب الطلب";
            //*************
            if (Convert.ToInt32(CMB_Sadr.SelectedValue) == 2)
            { TXT_TalbTawred.Text = "تعاقد"; }
            //*************

        }

        private void CMB_Component_SelectionChangeCommitted(object sender, EventArgs e)
        {

            reset();
            reset_values();
            Component_ID();
        }

        private void BTN_Add_Click(object sender, EventArgs e)
        {
            control_flag = 1;
            reset();
            reset_values();
        }

        private void BTN_Edit_Click(object sender, EventArgs e)
        {

            if ((MessageBox.Show("هل تريد تعديل امر الشراء ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_AmrNo.Text) || string.IsNullOrEmpty(Cmb_FY.Text))
                {
                    MessageBox.Show("يجب اختيار امر الشراء المراد تعديله");
                    return;
                }
                else
                {
                    //BTN_Print.Enabled = false;
                    //Addbtn.Enabled = false;
                    control_flag = 2;

                    // SaveBtn.Visible = true;
                    var button = (Button)sender;


                }
            }


        }

        private void BTN_Delete_Click(object sender, EventArgs e)
        {
            //Check for Amr_Shraa first
            //--------------------------
            if ((MessageBox.Show("هل تريد حذف امر الشراء ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(TXT_AmrNo.Text))
                {
                    MessageBox.Show("يجب اختيار امر الشراء  اولا");
                    return;
                }
                //------------------------
                Constants.opencon();
                string cmdstring = "Exec SP_DeleteChemical @TNO,@FY,@aot output";

                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_AmrNo.Text));
                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text.ToString());
                cmd.Parameters.Add("@aot", SqlDbType.Int, 32);  //-------> output parameter
                cmd.Parameters["@aot"].Direction = ParameterDirection.Output;

                int flag;

                try
                {
                    cmd.ExecuteNonQuery();
                    executemsg = true;
                    flag = (int)cmd.Parameters["@aot"].Value;
                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    MessageBox.Show(sqlEx.ToString());
                    flag = (int)cmd.Parameters["@aot"].Value;
                }
                if (executemsg == true && flag == 1)
                {
                    MessageBox.Show("تم الحذف بنجاح");
                    reset();
                    reset_values();
                }
                Constants.closecon();
            }
            control_flag = 3;
        }

        private void BTN_Save_Click(object sender, EventArgs e)
        {


            switch (control_flag)
            {
                case 1:  // Add New Record
                    break;
                //------------------------

                case 2:  // Edit On Record --> Must perform Search First

                    break;
                //-------------------------

                case 3:  // Delete Record --> Must Check on Amr Sheraa Number
                    break;
                //-------------------------


                default:  //Pressing save without select any control
                    break;

            }
        }

        private void BTN_Calc_Click(object sender, EventArgs e)
        {
            calculate_fun(Convert.ToInt32(CMB_Component.SelectedValue));
        }

        private void TXT_Egmali_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ToWord toWord = new ToWord(Convert.ToDecimal(TXT_Egmali.Text), currencies[0]);
                //   txt_englishword.Text = toWord.ConvertToEnglish();
                TXT_EgmaliArabic.Text = toWord.ConvertToArabic();
            }
            catch (Exception ex)
            {
                //   txt_englishword.Text = String.Empty;
                TXT_EgmaliArabic.Text = String.Empty;
            }
        }

        private void BTN_Print_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد طباعة تقرير امر الشراء؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_AmrNo.Text) || string.IsNullOrEmpty(Cmb_FY.Text))
                {
                    MessageBox.Show("يجب اختيار امر شراء المراد طباعتها اولا");
                    return;
                }
                else
                {

                    Constants.AmrSanaMalya = Cmb_FY.Text;
                    Constants.AmrNo = TXT_AmrNo.Text;
                    Constants.FormNo = 100;
                    FReports f = new FReports();
                    f.Show();
                }
            }
        }
    }
}
