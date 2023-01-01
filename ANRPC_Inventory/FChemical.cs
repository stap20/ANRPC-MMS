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
        //------------------------------------------ Define Variables ---------------------------------
        #region Def Variables
        public SqlConnection con;//sql conn for anrpc_sms db
        private int AddEditFlag; // Flag For Control buttons
        AutoCompleteStringCollection TalbColl = new AutoCompleteStringCollection();

        private string ReportBayan1;
        private string ReportBayan2;

        public int comp_id;

        public string TNO;
        public string FY;

        //--------------------------

        public string Sign1;
        public string Sign2;
        public string Sign3;
        public string Sign4;
        public string Sign5;
        public string Sign6;
        public string Sign7;

        public string Empn1;
        public string Empn2;
        public string Empn3;
        public string Empn4;
        public string Empn5;
        public string Empn6;
        public string Empn7;

        public string FlagEmpn1;
        public string FlagEmpn2;
        public string FlagEmpn3;
        public string FlagEmpn4;
        public string FlagEmpn5;
        public string FlagEmpn6;
        public string FlagEmpn7;

        public int FlagSign1;
        public int FlagSign2;
        public int FlagSign3;
        public int FlagSign4;
        public int FlagSign5;
        public int FlagSign6;
        public int FlagSign7;

        public string wazifa1;
        public string wazifa2;
        public string wazifa3;
        public string wazifa4;
        public string wazifa5;
        public string wazifa6;
        public string wazifa7;
        public string wazifa8;
        public string wazifa9;
        public string wazifa10;
        public string wazifa11;

        public string Ename1;
        public string Ename2;
        public string Ename3;
        public string Ename4;
        public string Ename5;
        public string Ename6;
        public string Ename7;
        public string Ename8;
        public string Ename9;
        public string Ename10;
        public string Ename11;

        public string pp;


        //--------------------
        public Boolean executemsg;
        List<CurrencyInfo> currencies = new List<CurrencyInfo>();
        //--------------------
        AutoCompleteStringCollection sader_to = new AutoCompleteStringCollection(); // Sader_to
        #endregion

        #region myDefVariable
        enum VALIDATION_TYPES
        {
            ADD_AMRSHERAA_BNOD,
            ATTACH_FILE,
            SEARCH,
            CONFIRM_SEARCH,
            SAVE,

        }
        string curr_stock_no_all = "";
        int currentSignNumber = 0;
        bool isComeFromSearch = false;
        Dictionary<int, int> signatureOrder;
        #endregion

        //------------------------------------------ Helper ---------------------------------
        #region Helpers
        private void initiateSignatureOrder()
        {
            //Dictionary to get values of signature (sign1 or sign2 ...) according to thier order in table
            signatureOrder = new Dictionary<int, int>();
            signatureOrder.Add(1, 1);
            signatureOrder.Add(2, 2);
            signatureOrder.Add(3, 3);
            signatureOrder.Add(4, 4);
        }

        public void SP_InsertSignatures(int signNumber, int signOrder)
        {
            string cmdstring = "Exec  SP_InsertSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2,@SignOrder";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_AmrNo.Text));
            cmd.Parameters.AddWithValue("@TNO2", DBNull.Value);
            cmd.Parameters.AddWithValue("@FY", Cmb_FY2.Text.ToString());
            cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
            cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
            cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);
            cmd.Parameters.AddWithValue("@FN", 10);
            cmd.Parameters.AddWithValue("@SN", signNumber);
            cmd.Parameters.AddWithValue("@D1", DBNull.Value);
            cmd.Parameters.AddWithValue("@D2", DBNull.Value);
            cmd.Parameters.AddWithValue("@SignOrder", signOrder);
            cmd.ExecuteNonQuery();
        }

        private PictureBox CheckSignatures(Panel panel, int signNumber)
        {
            try
            {
                foreach (Control control in panel.Controls)
                {
                    if (control.GetType() == typeof(Panel))
                    {
                        PictureBox signControl = CheckSignatures((Panel)control, signNumber);

                        if (signControl != null)
                        {
                            return signControl;
                        }
                    }
                    else
                    {
                        if (control.Name == "Pic_Sign" + signNumber && ((PictureBox)control).Image == null)
                        {
                            return (PictureBox)control;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        private void errorProviderHandler(List<(ErrorProvider, Control, string)> errosList)
        {
            alertProvider.Clear();
            errorProvider.Clear();
            foreach (var error in errosList)
            {
                ////Txt_ReqQuan.Location = new Point(Txt_ReqQuan.Location.X + errorProvider.Icon.Width, Txt_ReqQuan.Location.Y);
                //error.Item2.Width = error.Item2.Width - error.Item1.Icon.Width;
                error.Item1.SetError(error.Item2, error.Item3);
            }
        }

        public void SP_UpdateSignatures(int x, DateTime D1, DateTime? D2 = null)
        {
            string cmdstring = "Exec  SP_UpdateSignDates @TNO,@TNO2,@FY,@CD,@CE,@NE,@FN,@SN,@D1,@D2";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TNO", Convert.ToInt32(TXT_AmrNo.Text));
            cmd.Parameters.AddWithValue("@TNO2", DBNull.Value);

            if (Cmb_FY2.Text.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text.ToString());
            }
            else
            {
                cmd.Parameters.AddWithValue("@FY", Cmb_FY2.Text.ToString());
            }

            cmd.Parameters.AddWithValue("@CD", Convert.ToDateTime(TXT_Date.Value.ToShortDateString()));
            cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);
            cmd.Parameters.AddWithValue("@NE", Constants.NameEdara);
            cmd.Parameters.AddWithValue("@FN", 10);
            cmd.Parameters.AddWithValue("@SN", x);
            cmd.Parameters.AddWithValue("@D1", D1);

            if (D2 == null)
            {
                cmd.Parameters.AddWithValue("@D2", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@D2", D2);
            }

            cmd.ExecuteNonQuery();
        }

        //Report Bayan Concatination
        private void Report_Bayan(int Comp_id)
        {
            switch (Comp_id)
            {
                case 11:

                    //-------------------------------------------------
                    ReportBayan1 = CMB_Component.Text + " " + L_TEARKEZ.Text + TXT_tarkez.Text + " " + L_TEARKEZ1.Text
                        + "\n" + L_DAREBA_EDARIA.Text + "\n" + L_NOTE.Text + " " + TXT_NOTE.Text;
                    ReportBayan2 = TXT_Total_value.Text + "\n" + TXT_DAREBA_EDARIA_v.Text;
                    //------------------------------------------------
                    break;

                //===================================================
                case 12:

                    //-------------------------------------------------
                    ReportBayan1 = CMB_Component.Text + " " + L_TEARKEZ.Text + " " + TXT_tarkez.Text + L_TEARKEZ1.Text
                        + "\n" + L_NOLON.Text + TXT_NOLON.Text + L_NOLON1.Text
                        + "\n" + L_Edaria.Text + TXT_Edaria.Text + L_Edaria1.Text
                        + "\n" + L_DAREBA_EDARIA.Text
                        + "\n" + L_DAREBA_NOLON.Text + "\n" + L_NOTE.Text + " " + TXT_NOTE.Text;


                    ReportBayan2 = TXT_Total_value.Text
                        + "\n" + TXT_NOLON_v.Text
                        + "\n" + TXT_Edaria_v.Text
                        + "\n" + TXT_DAREBA_EDARIA_v.Text
                        + "\n" + TXT_DAREBA_NOLON_v.Text;

                    //-------------------------------------------------

                    break;

                case 13:

                    //-------------------------------------------------
                    ReportBayan1 = CMB_Component.Text + " " + L_TEARKEZ.Text + " " + TXT_tarkez.Text + L_TEARKEZ1.Text
                        + "\n" + L_NOLON.Text + TXT_NOLON.Text + L_NOLON1.Text
                        + "\n" + L_DAREBA_EDARIA.Text + "\n" + L_NOTE.Text + " " + TXT_NOTE.Text;


                    ReportBayan2 = TXT_Total_value.Text
                        + "\n" + TXT_NOLON_v.Text
                        + "\n" + TXT_DAREBA_EDARIA_v.Text;
                    //-------------------------------------------------
                    break;

                case 21:
                    //-------------------------------------------------
                    ReportBayan1 = CMB_Component.Text + " " + L_MONTH.Text + " " + CMB_Month.Text + " " + L_YEAR.Text + " " + CMB_Year.Text
                        + "\n" + L_DAREBA_EDARIA.Text + "\n" + L_NOTE.Text + " " + TXT_NOTE.Text;


                    ReportBayan2 = TXT_Total_value.Text
                        + "\n" + TXT_DAREBA_EDARIA_v.Text;
                    //-------------------------------------------------

                    break;

                case 31:

                    //-------------------------------------------------
                    ReportBayan1 = CMB_Component.Text
                        + "\n" + L_TAKAFOL.Text + TXT_Takafol.Text + L_TAKAFOL1.Text;


                    ReportBayan2 = TXT_Total_value.Text
                        + "\n" + TXT_Takafol_v.Text;


                    if (checkBox_Harbya.Checked == true)
                    {
                        ReportBayan1 += "\n" + L_HARBYA.Text + "\n" + L_NOTE.Text + " " + TXT_NOTE.Text;
                        ReportBayan2 += "\n" + "5";
                    }
                    else
                    {
                        ReportBayan1 += "\n" + L_NOTE.Text + " " + TXT_NOTE.Text;
                    }
                    //-------------------------------------------------


                    break;

                case 41:

                    ReportBayan1 = CMB_Component.Text + " " + L_TEARKEZ.Text + " " + TXT_tarkez.Text + L_TEARKEZ1.Text
                        + "\n" + L_NOLON.Text + TXT_NOLON.Text + L_NOLON1.Text
                        + "\n" + L_DNOLON.Text + TXT_DNOLON.Text + L_DNOLON1.Text
                        + "\n" + L_KHEDMA.Text + TXT_Tare2.Text;



                    ReportBayan2 = TXT_Total_value.Text
                        + "\n" + TXT_NOLON_v.Text
                        + "\n" + TXT_DNOLON_v.Text
                        + "\n" + TXT_Tare2_v.Text
                        + "\n" + TXT_Edaria_v.Text
                        + "\n" + TXT_DAREBA_EDARIA_v.Text
                        + "\n" + TXT_DAREBA_NOLON_v.Text;

                    //-------------------------------------------------


                    if (checkBox_Kasr.Checked == true)
                    {
                        ReportBayan1 += "\n" + L_KASRGNEH.Text
                                  + "\n" + L_DAREBA_EDARIA.Text
                                  + "\n" + L_DAREBA_NOLON.Text + "\n" + L_NOTE.Text + " " + TXT_NOTE.Text;
                    }
                    else
                    {
                        ReportBayan1 += "\n" + L_DAREBA_EDARIA.Text
                            + "\n" + L_DAREBA_NOLON.Text + "\n" + L_NOTE.Text + " " + TXT_NOTE.Text;
                    }

                    break;
                //===============================================================
                case 42:

                    ReportBayan1 = CMB_Component.Text + " " + L_TEARKEZ.Text + " " + TXT_tarkez.Text + L_TEARKEZ1.Text
                        + "\n" + L_NOLON.Text + TXT_NOLON.Text + L_NOLON1.Text
                        + "\n" + L_DNOLON.Text + TXT_DNOLON.Text + L_DNOLON1.Text
                        + "\n" + L_KHEDMA.Text + TXT_Tare2.Text;


                    ReportBayan2 = TXT_Total_value.Text
                        + "\n" + TXT_NOLON_v.Text
                        + "\n" + TXT_DNOLON_v.Text
                        + "\n" + TXT_Tare2_v.Text
                        + "\n" + TXT_Edaria_v.Text
                        + "\n" + TXT_DAREBA_EDARIA_v.Text
                        + "\n" + TXT_DAREBA_NOLON_v.Text;

                    //-------------------------------------------------


                    if (checkBox_Kasr.Checked == true)
                    {
                        ReportBayan1 += System.Environment.NewLine + L_KASRGNEH.Text
                                  + "\n" + L_DAREBA_EDARIA.Text
                                  + "\n" + L_DAREBA_NOLON.Text + "\n" + L_NOTE.Text + " " + TXT_NOTE.Text;
                    }
                    else
                    {
                        ReportBayan1 += "\n" + L_DAREBA_EDARIA.Text
                             + "\n" + L_DAREBA_NOLON.Text + "\n" + L_NOTE.Text + " " + TXT_NOTE.Text;
                    }


                    break;
                //===============================================================

                case 43:
                    ReportBayan1 = CMB_Component.Text + " " + L_TEARKEZ.Text + " " + TXT_tarkez.Text + L_TEARKEZ1.Text
                        + "\n" + L_NOLON.Text + TXT_NOLON.Text + L_NOLON1.Text
                        + "\n" + L_DNOLON.Text + TXT_DNOLON.Text + L_DNOLON1.Text
                        + "\n" + L_KHEDMA.Text + TXT_Tare2.Text
                        + "\n" + L_DAREBA_EDARIA.Text
                        + "\n" + L_DAREBA_NOLON.Text + "\n" + L_NOTE.Text + " " + TXT_NOTE.Text;


                    ReportBayan2 = TXT_Total_value.Text
                        + "\n" + TXT_NOLON_v.Text
                        + "\n" + TXT_DNOLON_v.Text
                        + "\n" + TXT_Tare2_v.Text
                        + "\n" + TXT_Edaria_v.Text
                        + "\n" + TXT_DAREBA_EDARIA_v.Text
                        + "\n" + TXT_DAREBA_NOLON_v.Text;

                    break;
                //===============================================================
                case 44:

                    ReportBayan1 = CMB_Component.Text + " " + L_TEARKEZ.Text + " " + TXT_tarkez.Text + L_TEARKEZ1.Text
                        + "\n" + L_DAREBA_EDARIA.Text + "\n" + L_NOTE.Text + " " + TXT_NOTE.Text;
                    ;
                    ReportBayan2 = TXT_Total_value.Text
                        + "\n" + TXT_DAREBA_EDARIA_v.Text
;
                    break;
                //===============================================================

                case 51:

                    //-------------------------------------------------
                    ReportBayan1 = CMB_Component.Text + " " + L_TEARKEZ.Text + " " + TXT_tarkez.Text + L_TEARKEZ1.Text
                        + "\n" + L_AMELEN.Text + TXT_SANDOAMLEN.Text
                        + "\n" + L_DAREBA_EDARIA.Text + "\n" + L_NOTE.Text + " " + TXT_NOTE.Text;



                    ReportBayan2 = TXT_Total_value.Text
                        + "\n" + TXT_SANDOAMLEN_v.Text
                        + "\n" + TXT_DAREBA_EDARIA_v.Text;


                    //-------------------------------------------------

                    break;
                //===============================================================
                default:
                    ReportBayan1 = "";
                    ReportBayan2 = "";
                    break;

            }
        }

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

            //Convert To Arabic
            //---------------------

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

            //===========================================

            //----------------------------------------
            Report_Bayan(Comp_id);
            //----------------------------------------
        }

        // The labels Related to the Component
        //=====================================
        public void Component_ID()
        {
            switch (comp_id)
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

        public bool SearchAmrSheraa(string amrNo, string fyear)
        {
            Constants.opencon();

            string cmdstring;
            SqlCommand cmd;



            cmdstring = "select * from T_Awamershraa where Amrshraa_No=@TN and AmrSheraa_sanamalia=@FY and IsChemical = 1";
            cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@TN", amrNo);
            cmd.Parameters.AddWithValue("@FY", fyear);

            SqlDataReader dr = cmd.ExecuteReader();

            string talb_no = "";
            if (dr.HasRows == true)
            {
                try
                {
                    while (dr.Read())
                    {

                        TXT_AmrNo.Text = dr["Amrshraa_No"].ToString();

                        CMB_Edara.Text = dr["NameEdara"].ToString();
                        TXT_Date.Text = dr["Date_amrshraa"].ToString();
                        CMB_Sadr.Text = dr["Sadr_To"].ToString();
                        TXT_BndMwazna.Text = dr["Bnd_Mwazna"].ToString();
                        TXT_Pay.Text = dr["Payment_Method"].ToString();
                        TXT_TaslemDate.Text = dr["Date_Tslem"].ToString();
                        TXT_TaslemPlace.Text = dr["Mkan_Tslem"].ToString();
                        TXT_Shik.Text = dr["Shick_Name"].ToString();
                        TXT_Mowared.Text = dr["Hesab_Mward"].ToString();

                        Cmb_FY.Text = dr["AmrSheraa_sanamalia"].ToString();


                        string s1 = Convert.ToString(dr["Sign1"]);
                        string s2 = Convert.ToString(dr["Sign12"]);
                        string s3 = Convert.ToString(dr["Sign13"]);
                        string s4 = Convert.ToString(dr["Sign14"]);
                        string s5 = Convert.ToString(dr["Sign3"]);
                        string s6 = Convert.ToString(dr["Sign33"]);
                        string s7 = Convert.ToString(dr["Sign2"]);


                        talb_no = dr["Talb_Twred"].ToString();
                        if (s1 != "")
                        {
                            string p = Constants.RetrieveSignature("1", "10", s1);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename1 = p.Split(':')[1];
                                wazifa1 = p.Split(':')[2];
                                pp = p.Split(':')[0];

                                ((PictureBox)this.signatureTable.Controls["panel15"].Controls["Pic_Sign" + "1"]).Image = Image.FromFile(@pp);

                                FlagSign1 = 1;
                                FlagEmpn1 = s1;
                                ((PictureBox)this.signatureTable.Controls["panel15"].Controls["Pic_Sign" + "1"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign1, Ename1 + Environment.NewLine + wazifa1);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.signatureTable.Controls["panel15"].Controls["Pic_Sign" + "1"]).BackColor = Color.Red;
                        }
                        if (s2 != "")
                        {
                            string p = Constants.RetrieveSignature("2", "10", s2);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename2 = p.Split(':')[1];
                                wazifa2 = p.Split(':')[2];
                                pp = p.Split(':')[0];

                                ((PictureBox)this.signatureTable.Controls["panel16"].Controls["Pic_Sign" + "2"]).Image = Image.FromFile(@pp);

                                FlagSign2 = 1;
                                FlagEmpn2 = s2;
                                ((PictureBox)this.signatureTable.Controls["panel16"].Controls["Pic_Sign" + "2"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign2, Ename2 + Environment.NewLine + wazifa2);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.signatureTable.Controls["panel16"].Controls["Pic_Sign" + "2"]).BackColor = Color.Red;
                        }
                        if (s3 != "")
                        {
                            string p = Constants.RetrieveSignature("3", "10", s3);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename3 = p.Split(':')[1];
                                wazifa3 = p.Split(':')[2];
                                pp = p.Split(':')[0];

                                ((PictureBox)this.signatureTable.Controls["panel12"].Controls["Pic_Sign" + "3"]).Image = Image.FromFile(@pp);

                                FlagSign3 = 1;
                                FlagEmpn3 = s3;
                                ((PictureBox)this.signatureTable.Controls["panel12"].Controls["Pic_Sign" + "3"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign4, Ename3 + Environment.NewLine + wazifa3);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.signatureTable.Controls["panel12"].Controls["Pic_Sign" + "3"]).BackColor = Color.Red;
                        }
                        if (s4 != "")
                        {
                            string p = Constants.RetrieveSignature("4", "10", s4);
                            if (p != "")
                            {
                                //   Pic_Sign1
                                //	"Pic_Sign1"	string
                                Ename4 = p.Split(':')[1];
                                wazifa4 = p.Split(':')[2];
                                pp = p.Split(':')[0];

                                ((PictureBox)this.signatureTable.Controls["panel18"].Controls["Pic_Sign" + "4"]).Image = Image.FromFile(@pp);

                                FlagSign4 = 1;
                                FlagEmpn4 = s4;
                                ((PictureBox)this.signatureTable.Controls["panel18"].Controls["Pic_Sign" + "4"]).BackColor = Color.Green;
                                toolTip1.SetToolTip(Pic_Sign3, Ename4 + Environment.NewLine + wazifa4);
                            }

                        }
                        else
                        {
                            ((PictureBox)this.signatureTable.Controls["panel18"].Controls["Pic_Sign" + "4"]).BackColor = Color.Red;
                        }
                        
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    if (dr != null)
                    {
                        dr.Dispose();
                    }
                }
            }
            else
            {
                MessageBox.Show("من فضلك تاكد من رقم امر الشراء");
                reset();
                return false;
            }

            dr.Close();

            //============================================================


            // Reset the Component_ID ComboBox
            //--------------------------------
            //CMB_Component.DataSource = null; //reset 

            //string query = "SELECT  [Component],[Compn_ID] FROM Compny_Master where [CompanyID] = @u  ";
            //SqlCommand cmd2 = new SqlCommand(query, Constants.con);
            //DataTable dts = new DataTable();

            //cmd2.Parameters.AddWithValue("@u", Convert.ToInt32(CMB_Sadr.SelectedValue));
            //dts.Load(cmd2.ExecuteReader());
            //CMB_Component.DataSource = dts;
            //CMB_Component.ValueMember = "Compn_ID";
            //CMB_Component.DisplayMember = "Component";


            ////=============================================================

            string cmdstring1;
            SqlCommand cmd1;

            Constants.opencon();

            cmdstring1 = "select * from Awamershraa_Chemicals where Amrshraa_No=@TN and Sana_Malia=@FY";
            cmd1 = new SqlCommand(cmdstring1, Constants.con);
            cmd1.Parameters.AddWithValue("@TN", amrNo);
            cmd1.Parameters.AddWithValue("@FY", fyear);

            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.HasRows == true)
            {
                try
                {
                    while (dr1.Read())
                    {

                        TXT_BAND.Text = dr1["Band"].ToString();
                        TXT_Quantity.Text = dr1["Quantity"].ToString();
                        TXT_UnitPrice.Text = dr1["unit_price"].ToString();
                        TXT_Unit.Text = dr1["Unit"].ToString();
                        CMB_Component.Text = dr1["Component_Name"].ToString();
                        TXT_tarkez.Text = dr1["Concentrate"].ToString();
                        TXT_DAREBA_EDARIA_v.Text = dr1["dareba_edaria"].ToString();
                        TXT_NOLON.Text = dr1["Noloon_expenses"].ToString();
                        TXT_DNOLON.Text = dr1["damghet_nolon"].ToString();
                        CMB_Month.Text = dr1["Month_Name"].ToString();
                        CMB_Year.Text = dr1["year_n"].ToString();
                        TXT_Total_value.Text = dr1["total_value"].ToString();
                        TXT_Egmali.Text = dr1["total_after_taxes"].ToString();
                        TXT_Takafol.Text = dr1["sando_takafol"].ToString();
                        TXT_SANDOAMLEN.Text = dr1["sando_amleen"].ToString();
                        TXT_EgmaliArabic.Text = dr1["Total_Arabic"].ToString();
                        TXT_Tare2.Text = dr1["Khedmet_tare2"].ToString();
                        TXT_NOTE.Text = dr1["note"].ToString();
                        TXT_Momayaz.Text = dr1["Momayz"].ToString();
                        //-----------------------------------
                        comp_id = Convert.ToInt32(dr1["comp_id"]);
                        //-----------------------------------
                        CMB_Component.SelectedValue = comp_id;


                        checkBox_Harbya.Checked = Convert.ToBoolean(dr1["harby_Flag"]);
                        checkBox_Kasr.Checked = Convert.ToBoolean(dr1["harby_Flag"]);
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    if (dr1 != null)
                    {
                        dr1.Dispose();
                    }
                }
            }
            else
            {
                MessageBox.Show("من فضلك تاكد من رقم امر الشراء");
                reset();
                return false;
            }

            dr1.Close();


            Component_ID();

            Constants.closecon();

            TXT_TalbTawred.Text = talb_no;
            return true;
        }

        #endregion

        //------------------------------------------ State Handler ---------------------------------
        #region State Handler
        private void changePanelState(Panel panel, bool state)
        {
            try
            {
                foreach (Control control in panel.Controls)
                {
                    if (control.GetType() == typeof(Panel))
                    {
                        changePanelState((Panel)control, state);
                    }
                    else
                    {
                        control.Enabled = state;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void PrepareAddState()
        {

            //fyear sec
            changePanelState(panel9, false);
            Cmb_FY.Enabled = true;
            CMB_Sadr.Enabled = true;

            //sheek sec
            changePanelState(panel5, true);

            //mowazna value
            changePanelState(panel10, true);
            CMB_Edara.Enabled = false;
            TXT_BndMwazna.Enabled = false;

            //moward sec
            changePanelState(panel11, true);

            //data grid view simulator sec
            changePanelState(panel4, true);

            //btn Section
            //generalBtn
            BTN_Save.Enabled = true;
            BTN_Cancel.Enabled = true;
            browseBTN.Enabled = true;
            BTN_PDF.Enabled = true;

            BTN_Add.Enabled = false;
            BTN_Edit.Enabled = false;
            BTN_Search.Enabled = false;
            BTN_Print.Enabled = false;


            //signature btn
            changePanelState(signatureTable, false);
            BTN_Sign1.Enabled = true;

            Pic_Sign1.Image = null;
            FlagSign1 = 0;
            Pic_Sign1.BackColor = Color.Green;
            currentSignNumber = 1;
        }

        public void PrepareEditState()
        {
            PrepareAddState();
            BTN_Print.Enabled = true;

            Pic_Sign1.Image = null;
            Pic_Sign2.Image = null;
            FlagSign1 = 0;
            FlagSign2 = 0;
            Pic_Sign1.BackColor = Color.White;
            Pic_Sign2.BackColor = Color.White;
        }

        public void PrepareConfirmState()
        {
            DisableControls();
            BTN_Save2.Enabled = true;


            if (Constants.User_Type == "B")
            {
                if (Constants.UserTypeB == "Stock")
                {
                    if (FlagSign2 != 1 && FlagSign1 == 1)
                    {
                        BTN_Sign2.Enabled = true;

                        Pic_Sign2.BackColor = Color.Green;
                        currentSignNumber = 2;
                    }
                    else if (FlagSign3 != 1 && FlagSign2 == 1)
                    {
                        BTN_Sign3.Enabled = true;

                        Pic_Sign4.BackColor = Color.Green;
                        currentSignNumber = 3;
                    }
                    else if (FlagSign4 != 1 && FlagSign3 == 1)
                    {
                        BTN_Sign4.Enabled = true;

                        Pic_Sign3.BackColor = Color.Green;
                        currentSignNumber = 4;
                    }
                }
            }

            AddEditFlag = 1;
            TNO = TXT_AmrNo.Text;
            FY = Cmb_FY.Text;
        }

        public void prepareSearchState(bool isReset = true)
        {
            DisableControls();

            if (isReset)
            {
                Input_Reset();
            }

            if (!Constants.isConfirmForm)
            {
                Cmb_FY.Enabled = true;
                TXT_AmrNo.Enabled = true;
                BTN_Print.Enabled = true;
            }
        }

        public void reset()
        {
            prepareSearchState();
        }

        public void DisableControls()
        {
            //fyear sec
            changePanelState(panel9, false);

            //sheek sec
            changePanelState(panel5, false);

            //mowazna value
            changePanelState(panel10, false);

            //moward sec
            changePanelState(panel11, false);

            //data grid view simulator sec
            changePanelState(panel4, false);

            //btn Section
            //generalBtn
            BTN_Add.Enabled = true;
            BTN_Search.Enabled = true;
            BTN_Search_Motab3a.Enabled = true;
            BTN_Save.Enabled = false;
            BTN_Save2.Enabled = false;

            BTN_Edit.Enabled = false;
            BTN_Cancel.Enabled = false;
            EditBtn2.Enabled = false;
            BTN_Print.Enabled = false;
            BTN_Print2.Enabled = false;
            browseBTN.Enabled = false;
            BTN_PDF.Enabled = false;

            //signature btn
            changePanelState(signatureTable, false);
        }

        public void resetSignature()
        {
            //btn Section
            //signature btn
            Pic_Sign1.Image = null;
            FlagSign1 = 0;
            Pic_Sign1.BackColor = Color.White;

            Pic_Sign2.Image = null;
            FlagSign2 = 0;
            Pic_Sign2.BackColor = Color.White;

            Pic_Sign4.Image = null;
            FlagSign3 = 0;
            Pic_Sign4.BackColor = Color.White;

            Pic_Sign3.Image = null;
            FlagSign4 = 0;
            Pic_Sign3.BackColor = Color.White;
        }

        public void handleVisibleState()
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

            //DisableControls();
        }

        public void reset_bandValues()
        {
            //----------------------------------------
            TXT_Egmali.Text = "";
            TXT_EgmaliArabic.Text = "";
            //----------------------------------------
            TXT_BAND.Text = "1";
            TXT_BAND.Enabled = false;
            //----------------------------------------
            TXT_Quantity.Text = "";
            TXT_Unit.Text = "";
            TXT_UnitPrice.Text = "";
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

        public void Input_Reset()
        {
            //fyear sec
            TXT_AmrNo.Text = "";
            Cmb_FY.Text = "";
            Cmb_FY.SelectedIndex = -1;

            CMB_Sadr.Text = "";
            CMB_Sadr.SelectedIndex = -1;

            //shick sec value
            TXT_Date.Value = DateTime.Today;
            TXT_Momayaz.Text = "";
            TXT_Shik.Text = "";
            TXT_Pay.Text = "";


            //mowzna sec
            TXT_TaslemDate.Text = "";
            TXT_TaslemPlace.Text = "";
            TXT_BndMwazna.Text = "";

            CMB_Edara.Text = "";
            CMB_Edara.SelectedIndex = -1;

            //moward sec    
            TXT_Mowared.Text = "";
            TXT_Egmali.Text = "";

            TXT_TalbTawred.Text = "";
            TXT_TalbTawred.SelectedIndex = -1;

            //search sec
            Txt_AmrNo2.Text = "";
            Cmb_FY2.Text = "";
            Cmb_FY2.SelectedIndex = -1;

            reset_bandValues();
            handleVisibleState();

            resetSignature();

            AddEditFlag = 0;
        }
        #endregion

        //------------------------------------------ Logic Handler ---------------------------------
        #region Logic Handler
        private void AddLogic()
        {
            Constants.opencon();

            string cmdstring = "exec SP_InsertChemical @p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p32,@p33,@p34,@p35,@p36,@p37,@p38,@p39,@p40,@p41,@p42,@p43,@p44,@p45,@p46,@p47,@p48 out";
            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_AmrNo.Text));
            cmd.Parameters.AddWithValue("@p2", Cmb_FY.Text);
            cmd.Parameters.AddWithValue("@p3", (CMB_Sadr.Text));
            cmd.Parameters.AddWithValue("@p4", (Convert.ToDateTime(TXT_Date.Value.ToShortDateString())));
            cmd.Parameters.AddWithValue("@p5", (TXT_Momayaz.Text));
            cmd.Parameters.AddWithValue("@p6", (TXT_Shik.Text));
            cmd.Parameters.AddWithValue("@p7", (TXT_Pay.Text));
            cmd.Parameters.AddWithValue("@p8", (TXT_TaslemDate.Text));
            cmd.Parameters.AddWithValue("@p9", (TXT_TaslemPlace.Text));
            cmd.Parameters.AddWithValue("@p10", (CMB_Edara.Text));
            cmd.Parameters.AddWithValue("@p11", (TXT_BndMwazna.Text));
            cmd.Parameters.AddWithValue("@p12", (TXT_TalbTawred.Text));
            cmd.Parameters.AddWithValue("@p13", (TXT_Mowared.Text));
            cmd.Parameters.AddWithValue("@p14", (TXT_BAND.Text));
            cmd.Parameters.AddWithValue("@p15", (TXT_Quantity.Text));
            cmd.Parameters.AddWithValue("@p16", (TXT_UnitPrice.Text));
            cmd.Parameters.AddWithValue("@p17", (TXT_Unit.Text));
            cmd.Parameters.AddWithValue("@p18", (CMB_Component.Text));
            cmd.Parameters.AddWithValue("@p19", (TXT_tarkez.Text));
            cmd.Parameters.AddWithValue("@p20", (DBNull.Value));  //-------- Dareba
            cmd.Parameters.AddWithValue("@p21", (TXT_NOLON.Text));
            cmd.Parameters.AddWithValue("@p22", (TXT_Edaria.Text));
            cmd.Parameters.AddWithValue("@p23", (TXT_DAREBA_NOLON_v.Text));
            cmd.Parameters.AddWithValue("@p24", (TXT_DNOLON.Text));
            cmd.Parameters.AddWithValue("@p25", (TXT_DAREBA_EDARIA_v.Text));
            cmd.Parameters.AddWithValue("@p26", (checkBox_Kasr.Checked));
            cmd.Parameters.AddWithValue("@p27", (checkBox_Harbya.Checked));
            cmd.Parameters.AddWithValue("@p28", (CMB_Month.Text));
            cmd.Parameters.AddWithValue("@p29", (CMB_Year.Text));
            cmd.Parameters.AddWithValue("@p30", (TXT_Total_value.Text));
            //---------------------------
            if (TXT_Egmali.Text.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@p31", DBNull.Value);
            }

            else
            {
                cmd.Parameters.AddWithValue("@p31", Convert.ToDecimal(TXT_Egmali.Text));
            }
            //-------------------------
            cmd.Parameters.AddWithValue("@p32", (TXT_Takafol.Text));
            cmd.Parameters.AddWithValue("@p33", (TXT_NOTE.Text));
            cmd.Parameters.AddWithValue("@p34", (TXT_SANDOAMLEN.Text));
            cmd.Parameters.AddWithValue("@p35", (TXT_EgmaliArabic.Text));
            //=================================================
            cmd.Parameters.AddWithValue("@p36", ReportBayan1); //Report Bayan String1
            cmd.Parameters.AddWithValue("@p37", ReportBayan2); //Report Bayan String2
            //================================================

            cmd.Parameters.AddWithValue("@p38", (TXT_Tare2.Text));
            cmd.Parameters.AddWithValue("@p39", comp_id);




            cmd.Parameters.AddWithValue("@p40", FlagEmpn1);
            cmd.Parameters.AddWithValue("@p41", DBNull.Value);
            cmd.Parameters.AddWithValue("@p42", DBNull.Value);
            cmd.Parameters.AddWithValue("@p43", DBNull.Value);
            cmd.Parameters.AddWithValue("@p44", DBNull.Value);
            cmd.Parameters.AddWithValue("@p45", DBNull.Value);
            cmd.Parameters.AddWithValue("@p46", DBNull.Value);

            cmd.Parameters.AddWithValue("@p47", '1');




            cmd.Parameters.Add("@p48", SqlDbType.Int, 32);  //-------> output parameter
            cmd.Parameters["@p48"].Direction = ParameterDirection.Output;

            int flag;

            try
            {
                cmd.ExecuteNonQuery();
                executemsg = true;
            }
            catch (SqlException sqlEx)
            {
                executemsg = false;
                Console.WriteLine(sqlEx);
            }

            flag = (int)cmd.Parameters["@p48"].Value;

            if (executemsg == true && flag == 1)
            {

                SP_UpdateSignatures(1, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()));


                ///////////////////////////////////////////////////
                MessageBox.Show("تم الإضافة بنجاح  ! ");

                reset();
            }
            else if (executemsg == true && flag == 2)
            {
                MessageBox.Show("تم إدخال رقم امر الشراء  من قبل  ! ");
            }
            else if (executemsg == false)
            {
                MessageBox.Show("لم يتم إدخال إذن الصرف بنجاج!!");
            }

            Constants.closecon();
        }

        private void UpdateAmrsheraaChemicalSignatureCycle()
        {
            if (FlagSign2 == 1)
            {
                SP_UpdateSignatures(2, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                SP_UpdateSignatures(3, Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            }
            if (FlagSign3 == 1)
            {
                SP_UpdateSignatures(3, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            }
            if (FlagSign4 == 1)
            {
                SP_UpdateSignatures(4, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                SP_UpdateSignatures(5, Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            }
            if (FlagSign5 == 1)
            {
                SP_UpdateSignatures(5, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                SP_UpdateSignatures(7, Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            }
            if (FlagSign7 == 1)
            {
                SP_UpdateSignatures(7, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            }
            if (FlagSign6 == 1)
            {
                SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                // SP_UpdateSignatures(6, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortDateString()));
            }
        }

        public void UpdateAmrsheraaChemical()
        {
            Constants.opencon();

            string cmdstring = "Exec SP_UpdateChemical @TNOold,@FYold,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p32,@p33,@p34,@p35,@p36,@p37,@p38,@p39,@p40,@p41,@p42,@p43,@p44,@p45,@p46,@p47,@p48 out";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
            cmd.Parameters.AddWithValue("@TNOold", Convert.ToInt32(TNO));
            cmd.Parameters.AddWithValue("@FYold", FY);

            cmd.Parameters.AddWithValue("@p1", Convert.ToInt32(TXT_AmrNo.Text));
            cmd.Parameters.AddWithValue("@p2", Cmb_FY.Text);
            cmd.Parameters.AddWithValue("@p3", (CMB_Sadr.Text));
            cmd.Parameters.AddWithValue("@p4", (Convert.ToDateTime(TXT_Date.Value.ToShortDateString())));
            cmd.Parameters.AddWithValue("@p5", (TXT_Momayaz.Text));
            cmd.Parameters.AddWithValue("@p6", (TXT_Shik.Text));
            cmd.Parameters.AddWithValue("@p7", (TXT_Pay.Text));
            cmd.Parameters.AddWithValue("@p8", (TXT_TaslemDate.Text));
            cmd.Parameters.AddWithValue("@p9", (TXT_TaslemPlace.Text));
            cmd.Parameters.AddWithValue("@p10", (CMB_Edara.Text));
            cmd.Parameters.AddWithValue("@p11", (TXT_BndMwazna.Text));
            cmd.Parameters.AddWithValue("@p12", (TXT_TalbTawred.Text));
            cmd.Parameters.AddWithValue("@p13", (TXT_Mowared.Text));
            cmd.Parameters.AddWithValue("@p14", (TXT_BAND.Text));
            cmd.Parameters.AddWithValue("@p15", (TXT_Quantity.Text));
            cmd.Parameters.AddWithValue("@p16", (TXT_UnitPrice.Text));
            cmd.Parameters.AddWithValue("@p17", (TXT_Unit.Text));
            cmd.Parameters.AddWithValue("@p18", (CMB_Component.Text));
            cmd.Parameters.AddWithValue("@p19", (TXT_tarkez.Text));
            cmd.Parameters.AddWithValue("@p20", (DBNull.Value));  //-------- Dareba
            cmd.Parameters.AddWithValue("@p21", (TXT_NOLON.Text));
            cmd.Parameters.AddWithValue("@p22", (TXT_Edaria.Text));
            cmd.Parameters.AddWithValue("@p23", (TXT_DAREBA_NOLON_v.Text));
            cmd.Parameters.AddWithValue("@p24", (TXT_DNOLON.Text));
            cmd.Parameters.AddWithValue("@p25", (TXT_DAREBA_EDARIA_v.Text));
            cmd.Parameters.AddWithValue("@p26", (checkBox_Kasr.Checked));
            cmd.Parameters.AddWithValue("@p27", (checkBox_Harbya.Checked));
            cmd.Parameters.AddWithValue("@p28", (CMB_Month.Text));
            cmd.Parameters.AddWithValue("@p29", (CMB_Year.Text));
            cmd.Parameters.AddWithValue("@p30", (TXT_Total_value.Text));

            //---------------------------
            if (TXT_Egmali.Text.ToString() == "")
            {
                cmd.Parameters.AddWithValue("@p31", DBNull.Value);
            }

            else
            {
                cmd.Parameters.AddWithValue("@p31", Convert.ToDecimal(TXT_Egmali.Text));
            }
            //-------------------------
            cmd.Parameters.AddWithValue("@p32", (TXT_Takafol.Text));
            cmd.Parameters.AddWithValue("@p33", (TXT_NOTE.Text));
            cmd.Parameters.AddWithValue("@p34", (TXT_SANDOAMLEN.Text));
            cmd.Parameters.AddWithValue("@p35", (TXT_EgmaliArabic.Text));
            //=================================================
            cmd.Parameters.AddWithValue("@p36", ReportBayan1); //Report Bayan String1
            cmd.Parameters.AddWithValue("@p37", ReportBayan2); //Report Bayan String2
            //================================================

            cmd.Parameters.AddWithValue("@p38", (TXT_Tare2.Text));
            cmd.Parameters.AddWithValue("@p39", Convert.ToInt32(CMB_Component.SelectedValue));



            //Signs
            //--------
            //-----------------------
            if (FlagSign1 == 1)
            {
                cmd.Parameters.AddWithValue("@p40", FlagEmpn1);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p40", DBNull.Value);

            }
            //-----------------------
            if (FlagSign2 == 1)
            {
                cmd.Parameters.AddWithValue("@p41", FlagEmpn2);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p41", DBNull.Value);

            }
            //-----------------------
            if (FlagSign3 == 1)
            {
                cmd.Parameters.AddWithValue("@p42", FlagEmpn3);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p42", DBNull.Value);

            }
            //-----------------------
            if (FlagSign4 == 1)
            {
                cmd.Parameters.AddWithValue("@p43", FlagEmpn4);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p43", DBNull.Value);

            } //-----------------------
            if (FlagSign5 == 1)
            {
                cmd.Parameters.AddWithValue("@p44", FlagEmpn5);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p44", DBNull.Value);

            }
            //-----------------------
            if (FlagSign6 == 1)
            {
                cmd.Parameters.AddWithValue("@p45", FlagEmpn6);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p45", DBNull.Value);

            }
            //-----------------------
            if (FlagSign7 == 1)
            {
                cmd.Parameters.AddWithValue("@p46", FlagEmpn7);

            }
            else
            {
                cmd.Parameters.AddWithValue("@p46", DBNull.Value);

            }
            //-----------------------

            cmd.Parameters.AddWithValue("@p47", '1');
            cmd.Parameters.Add("@p48", SqlDbType.Int, 32);  //-------> output parameter
            cmd.Parameters["@p48"].Direction = ParameterDirection.Output;

            int flag;

            try
            {
                cmd.ExecuteNonQuery();
                executemsg = true;
            }
            catch (SqlException sqlEx)
            {
                executemsg = false;
                Console.WriteLine(sqlEx);
            }

            flag = (int)cmd.Parameters["@p48"].Value;

            if (executemsg == true && flag == 1)
            {

                UpdateAmrsheraaChemicalSignatureCycle();

                MessageBox.Show("تم التعديل بنجاح  ! ");

                reset();
            }
            else if (executemsg == true && flag == 3)
            {
                MessageBox.Show("تم إدخال رقم امر الشراء  من قبل  ! ");
            }
            else if (executemsg == false)
            {
                MessageBox.Show("لم يتم تعديل امر الشراء بنجاج!!");
            }

            Constants.closecon();
        }

        private void EditLogic()
        {
            UpdateAmrsheraaChemical();
        }

        private void DeleteLogic()
        {
            if ((MessageBox.Show("هل تريد حذف امر الشراء ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrWhiteSpace(TXT_AmrNo.Text))
                {
                    MessageBox.Show("يجب اختيار امر الشراء  اولا");
                    return;
                }
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
                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    Console.WriteLine(sqlEx);
                }

                flag = (int)cmd.Parameters["@aot"].Value;

                if (executemsg == true && flag == 1)
                {
                    MessageBox.Show("تم الحذف بنجاح");
                    reset();
                }
                Constants.closecon();
            }
        }
        #endregion

        //------------------------------------------ Validation Handler ---------------------------------
        #region Validation Handler
        private List<(ErrorProvider, Control, string)> ValidateAddBnodAmrSheraa()
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

            //#region Cmb_FYear
            //if (string.IsNullOrWhiteSpace(Cmb_FY.Text) || Cmb_FY.SelectedIndex == -1)
            //{
            //    errorsList.Add((errorProvider, Cmb_FY, "تاكد من  اختيار السنة المالية"));
            //}
            //#endregion

            return errorsList;
        }

        private List<(ErrorProvider, Control, string)> ValidateAttachFile()
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

            #region Cmb_FYear
            if (string.IsNullOrWhiteSpace(Cmb_FY.Text) || Cmb_FY.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_FY, "تاكد من  اختيار السنة المالية"));
            }
            #endregion

            #region TXT_EznNo
            if (string.IsNullOrWhiteSpace(TXT_AmrNo.Text))
            {
                errorsList.Add((errorProvider, TXT_AmrNo, "يجب اختيار رقم أمر الشراء"));
            }
            #endregion

            return errorsList;
        }

        private List<(ErrorProvider, Control, string)> ValidateSearch(bool isConfirm = false)
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

            if (isConfirm)
            {
                #region Cmb_FYear2
                if (string.IsNullOrWhiteSpace(Cmb_FY2.Text) || Cmb_FY2.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_FY2, "تاكد من  اختيار السنة المالية"));
                }
                #endregion

                #region Cmb_AmrNo2
                if (string.IsNullOrWhiteSpace(Txt_AmrNo2.Text))
                {
                    errorsList.Add((errorProvider, Txt_AmrNo2, "يجب ادخال رقم أمر الشراء"));
                }
                #endregion
            }
            else
            {
                #region Cmb_FYear
                if (string.IsNullOrWhiteSpace(Cmb_FY.Text) || Cmb_FY.SelectedIndex == -1)
                {
                    errorsList.Add((errorProvider, Cmb_FY, "تاكد من  اختيار السنة المالية"));
                }
                #endregion

                #region TXT_AmrNo
                if (string.IsNullOrWhiteSpace(TXT_AmrNo.Text))
                {
                    errorsList.Add((errorProvider, TXT_AmrNo, "يجب اختيار رقم أمر الشراء"));
                }
                #endregion
            }

            return errorsList;
        }

        private List<(ErrorProvider, Control, string)> ValidateSave()
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

            #region Cmb_FYear
            if (string.IsNullOrWhiteSpace(Cmb_FY.Text) || Cmb_FY.SelectedIndex == -1)
            {
                errorsList.Add((errorProvider, Cmb_FY, "تاكد من  اختيار السنة المالية"));
            }
            #endregion

            PictureBox signControl = CheckSignatures(signatureTable, currentSignNumber);
            if (signControl != null)
            {
                errorsList.Add((errorProvider, signControl, "تاكد من التوقيع"));
            }


            return errorsList;
        }

        private bool IsValidCase(VALIDATION_TYPES type)
        {
            List<(ErrorProvider, Control, string)> errorsList = new List<(ErrorProvider, Control, string)>();

            if (type == VALIDATION_TYPES.ADD_AMRSHERAA_BNOD)
            {
                errorsList = ValidateAddBnodAmrSheraa();
            }

            else if (type == VALIDATION_TYPES.ATTACH_FILE)
            {
                errorsList = ValidateAttachFile();
            }
            else if (type == VALIDATION_TYPES.SEARCH)
            {
                errorsList = ValidateSearch(false);
            }
            else if (type == VALIDATION_TYPES.CONFIRM_SEARCH)
            {
                errorsList = ValidateSearch(true);
            }
            else if (type == VALIDATION_TYPES.SAVE)
            {
                errorsList = ValidateSave();
            }


            errorProviderHandler(errorsList);

            if (errorsList.Count > 0)
            {
                return false;
            }

            return true;
        }
        #endregion

        private void init()
        {
            HelperClass.comboBoxFiller(Cmb_FY2, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
            HelperClass.comboBoxFiller(Cmb_FY, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);
            HelperClass.comboBoxFiller(Cmb_Fy_Talb, FinancialYearHandler.getFinancialYear(), "FinancialYear", "FinancialYear", this);


            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Egypt));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Syria));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Tunisia));
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Gold));

            //Reset Values
            //---------------
            reset();
            AddEditFlag = 0;

            if (Constants.isConfirmForm)
            {
                panel1.Visible = true;
                panel2.Visible = false;
                panel1.Dock = DockStyle.Top;
            }
            else
            {
                panel2.Visible = true;
                panel1.Visible = false;
                panel2.Dock = DockStyle.Top;
            }


            //----------------
            con = new SqlConnection(Constants.constring);
            con.Open();

            // Sader_To
            //-----------
            //  string query = "SELECT Distinct [CompanyName],[CompanyID] FROM Compny_Master order by CompanyID  ";
            // SqlCommand cmd = new SqlCommand(query, con);
            // DataTable dts = new DataTable();
            // dts.Load(cmd.ExecuteReader());
            // CMB_Sadr.DataSource = dts;
            // CMB_Sadr.ValueMember = "CompanyID";
            // CMB_Sadr.DisplayMember = "CompanyName";

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



            //-------------------------------------------
            //Talb Tawred
            //-------
            string query2 = "SELECT [TalbTwareed_No],[STOCK_NO_ALL] FROM T_TalbTawreed_Benod where IsChemical = 1  ";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            DataTable dts2 = new DataTable();
            dts2.Load(cmd2.ExecuteReader());
            TXT_TalbTawred.DataSource = dts2;
            TXT_TalbTawred.ValueMember = "STOCK_NO_ALL";
            TXT_TalbTawred.DisplayMember = "TalbTwareed_No";
            con.Close();

           

            //-------------------------------------
            CMB_Edara.SelectedIndex = -1;
            CMB_Sadr.SelectedIndex = -1;
            TXT_TalbTawred.SelectedIndex = -1;
        }

        public FChemical()
        {
            InitializeComponent();

            init();

            initiateSignatureOrder();
        }

        public FChemical(string x, string y)
        {
            InitializeComponent();
            Cmb_FY.Text = x;
            TXT_AmrNo.Text = y;


            panel1.Visible = false;
            panel2.Visible = false;

            isComeFromSearch = true;

        }

        private void FChemical_Load(object sender, EventArgs e)
        {
            if (isComeFromSearch)
            {
                BTN_Search_Click(BTN_Search, e);
            }
        }

        // Company Has been Choosen
        //---------------------------

        private void CMB_Sadr_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            con = new SqlConnection(Constants.constring);
            con.Open();

            // Components
            //-----------
            string query = "SELECT [Compn_ID] FROM Compny_Master where [CompanyID] = @u and [STOCK_NO_ALL] = @t ";
            SqlCommand cmd = new SqlCommand(query, con);
       

            cmd.Parameters.AddWithValue("@u", Convert.ToInt32(CMB_Sadr.SelectedValue));
            cmd.Parameters.AddWithValue("@t", curr_stock_no_all);


            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                try
                {
                    while (dr.Read())
                    {
                        comp_id = Convert.ToInt32(dr["Compn_ID"]); 

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    if (dr != null)
                    {
                        dr.Dispose();
                    }
                }
            }
            con.Close();

            //-------------------------------------
          

            //Shik
            //*************
            
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
            //else { TXT_TalbTawred.Text = ""; }
            //*************



            Component_ID();
            reset_bandValues();
          //  reset();
            //-------------------
            if (Convert.ToInt32(comp_id) == 21)
            {
                TXT_Unit.Text = "لتر";
            }
            else { TXT_Unit.Text = "طن"; }
            //=========================
        }

        private void BTN_Add_Click(object sender, EventArgs e)
        {

            if ((MessageBox.Show("هل تريد اضافة امر شراء جديد؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                reset();
                PrepareAddState();

                AddEditFlag = 2;

            }
        }
        //=======================================================================================================
        private void BTN_Edit_Click(object sender, EventArgs e)
        {

            if ((MessageBox.Show("هل تريد تعديل امر الشراء ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_AmrNo.Text) || string.IsNullOrEmpty(Cmb_FY.Text))
                {
                    MessageBox.Show("يجب اختيار امر الشراء المراد تعديله");
                    return;
                }

                PrepareEditState();
            }


        }
        //=======================================================================================================
        private void BTN_Delete_Click(object sender, EventArgs e)
        {
            DeleteLogic();
        }
        //=======================================================================================================
        private void BTN_Save_Click(object sender, EventArgs e)
        {

            if (!IsValidCase(VALIDATION_TYPES.SAVE))
            {
                return;
            }

            if (AddEditFlag == 2)
            {
                if (FlagSign1 != 1)
                {
                    MessageBox.Show("من فضلك تاكد من توقيع امر الشراء");
                    return;
                }

                AddLogic();

            }
            else if (AddEditFlag == 1)
            {
                EditLogic();
            }

            reset();
        }
        //=======================================================================================================
        private void BTN_Calc_Click(object sender, EventArgs e)
        {
            calculate_fun(comp_id);
        }
        //=================================================================
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
        //=======================================================================================================
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


        //---------- Get the Number of AmrShraa
        private void Cmb_FY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AddEditFlag == 0)
            {
                Constants.opencon();

                TXT_AmrNo.AutoCompleteMode = AutoCompleteMode.None;
                TXT_AmrNo.AutoCompleteSource = AutoCompleteSource.None; ;
                string cmdstring3 = "SELECT  Amrshraa_No from T_Awamershraa  where AmrSheraa_sanamalia='" + Cmb_FY.Text + "' order by  Amrshraa_No";
                SqlCommand cmd3 = new SqlCommand(cmdstring3, Constants.con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                //---------------------------------
                if (dr3.HasRows == true)
                {
                    while (dr3.Read())
                    {
                        TalbColl.Add(dr3["Amrshraa_No"].ToString());

                    }
                }

                TXT_AmrNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                TXT_AmrNo.AutoCompleteSource = AutoCompleteSource.CustomSource;
                TXT_AmrNo.AutoCompleteCustomSource = TalbColl;
                Constants.closecon();

            }
            //go and get talbTawreed_no for this FYear
            if (AddEditFlag == 2)//add
            {
                //call sp that get last num that eentered for this MM and this YYYY
                Constants.opencon();

                // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
                string cmdstring = "select ( COALESCE(MAX( Amrshraa_No), 0)) from  T_Awamershraa where AmrSheraa_sanamalia=@FY ";
                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

                // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
                cmd.Parameters.AddWithValue("@FY", Cmb_FY.Text);

                int flag;

                try
                {
                    Constants.opencon();
                    // cmd.ExecuteNonQuery();
                    var count = cmd.ExecuteScalar();
                    executemsg = true;
                    //  if (cmd.Parameters["@Num"].Value != null && cmd.Parameters["@Num"].Value != DBNull.Value)
                    if (count != null && count != DBNull.Value)
                    {
                        //  flag = (int)cmd.Parameters["@Num"].Value;

                        flag = (int)count;
                        flag = flag + 1;
                        TXT_AmrNo.Text = flag.ToString();//el rakm el new

                    }

                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    Console.WriteLine(sqlEx);
                    // flag = (int)cmd.Parameters["@Num"].Value;
                }
            }
        }

        //----------------------------------------------------------------------
        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            AddEditFlag = 0;
            reset();
        }

        private void BTN_Search_Click(object sender, EventArgs e)
        {
            if (isComeFromSearch == false && !IsValidCase(VALIDATION_TYPES.SEARCH))
            {
                return;
            }

            string amr_no = TXT_AmrNo.Text;
            string fyear = Cmb_FY.Text;

            reset();

            if (SearchAmrSheraa(amr_no, fyear))
            {
                prepareSearchState(false);

                if (FlagSign2 != 1 && FlagSign1 != 1)
                {
                    BTN_Edit.Enabled = true;
                }
                else
                {
                    BTN_Edit.Enabled = false;
                }
            }
        }

        private void BTN_Search_Motab3a_Click(object sender, EventArgs e)
        {
            if (!IsValidCase(VALIDATION_TYPES.CONFIRM_SEARCH))
            {
                return;
            }

            string amr_no = Txt_AmrNo2.Text;
            string fyear = Cmb_FY2.Text;

            reset();

            if (SearchAmrSheraa(amr_no, fyear))
            {
                prepareSearchState(false);

                EditBtn2.Enabled = true;
                BTN_Print2.Enabled = true;
            }

            TXT_AmrNo.Enabled = false;
            Cmb_FY.Enabled = false;
        }

        private void CMB_Sadr_SelectedIndexChanged(object sender, EventArgs e)
        {
            TXT_Shik.Text = CMB_Sadr.Text;
        }

        private void TXT_TalbTawred_SelectionChangeCommitted(object sender, EventArgs e)
        {

            con = new SqlConnection(Constants.constring);
            con.Open();

            // Components
            //-----------
            string query100 = "SELECT  BndMwazna,NameEdara FROM T_TalbTawreed where [TalbTwareed_No] = @u  ";
            SqlCommand cmd100 = new SqlCommand(query100, con);
            DataTable dts100 = new DataTable();

            cmd100.Parameters.AddWithValue("@u", TXT_TalbTawred.SelectedValue.ToString());
            dts100.Load(cmd100.ExecuteReader());

            TXT_BndMwazna.Text = dts100.Rows[0]["BndMwazna"].ToString();
            CMB_Edara.Text = dts100.Rows[0]["NameEdara"].ToString();


            string query0 = "SELECT  STOCK_NO_ALL FROM T_TalbTawreed_Benod where [TalbTwareed_No] = @u  ";
            SqlCommand cmd0 = new SqlCommand(query0, con);
            DataTable dts0 = new DataTable();

            cmd0.Parameters.AddWithValue("@u", TXT_TalbTawred.SelectedValue.ToString());
            dts0.Load(cmd0.ExecuteReader());

            curr_stock_no_all = dts0.Rows[0]["STOCK_NO_ALL"].ToString();


            string query = "SELECT  DISTINCT [Component] FROM Compny_Master where [STOCK_NO_ALL] = @u  ";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dts = new DataTable();

            cmd.Parameters.AddWithValue("@u", curr_stock_no_all);
            dts.Load(cmd.ExecuteReader());
            CMB_Component.DataSource = dts;
            CMB_Component.ValueMember = "Component";
            CMB_Component.DisplayMember = "Component";
           

            //-------------------------------------
            CMB_Component.SelectedIndex = 0;
            //---------------------------------------


            // Sader_To
            //-----------
             string query1 = "SELECT Distinct [CompanyName],[CompanyID] FROM Compny_Master where [STOCK_NO_ALL] = @u order by CompanyID  ";
             SqlCommand cmd1 = new SqlCommand(query1, con);
             DataTable dts1 = new DataTable();

             cmd1.Parameters.AddWithValue("@u", curr_stock_no_all);
             dts1.Load(cmd1.ExecuteReader());
             CMB_Sadr.DataSource = dts1;
             CMB_Sadr.ValueMember = "CompanyID";
             CMB_Sadr.DisplayMember = "CompanyName";

            con.Close();
            //-------------------------------------
            CMB_Sadr.SelectedIndex = -1;
            //---------------------------------------


        }

        private void EditBtn2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد تعديل امر الشراء ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(TXT_AmrNo.Text) || string.IsNullOrEmpty(Cmb_FY.Text))
                {
                    MessageBox.Show("يجب اختيار امر الشراء المراد تعديله");
                    return;
                }

                PrepareConfirmState();
            }
        }

        private void BTN_Save2_Click(object sender, EventArgs e)
        {
            if (AddEditFlag == 1)
            {
                EditLogic();
            }
        }

        private void BTN_Print2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Txt_AmrNo2.Text) || string.IsNullOrEmpty(Cmb_FY2.Text))
            {
                MessageBox.Show("يجب اختيار امر شراء المراد طباعتها اولا");
                return;
            }
            else
            {

                Constants.AmrSanaMalya = Cmb_FY2.Text;
                Constants.AmrNo = Txt_AmrNo2.Text;
                Constants.FormNo = 100;
                FReports f = new FReports();
                f.Show();
            }
        }

        //------------------------------------------ Signature Handler ---------------------------------
        #region Signature Handler
        private void BTN_Sign1_Click(object sender, EventArgs e)
        {

            Empn1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل  رقم القيد الخاص بك", "توقيع الاعدداد", "");

            Sign1 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع الاعدادس", "");

            if (Sign1 != "" && Empn1 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("1", "10", Sign1, Empn1);
                if (result.Item3 == 1)
                {
                    Pic_Sign1.Image = Image.FromFile(@result.Item1);

                    FlagSign1 = result.Item2;
                    FlagEmpn1 = Empn1;
                }
                else
                {
                    FlagSign1 = 0;
                    FlagEmpn1 = "";

                }
            }
        }

        private void BTN_Sign2_Click(object sender, EventArgs e)
        {
            Empn2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد سالخاص بك", "توقيع التصديق", "");

            Sign2 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع التصديق", "");

            if (Sign2 != "" && Empn2 != "")
            {
                Tuple<string, int, int, string, string> result = Constants.CheckSign("2", "10", Sign2, Empn2);
                if (result.Item3 == 1)
                {
                    Pic_Sign2.Image = Image.FromFile(@result.Item1);

                    FlagSign2 = result.Item2;
                    FlagEmpn2 = Empn2;
                }
                else
                {
                    FlagSign2 = 0;
                    FlagEmpn2 = "";
                }
            }
        }

        private void BTN_Sign3_Click(object sender, EventArgs e)
        {
            Empn3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد سالخاص بك", "توقيع مدير عام مساعد", "");

            Sign3 = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مدير عام مساعد", "");

            if (Sign3 != "" && Empn3 != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("3", "10", Sign3, Empn3);
                if (result.Item3 == 1)
                {
                    Pic_Sign3.Image = Image.FromFile(@result.Item1);

                    FlagSign3 = result.Item2;
                    FlagEmpn3 = Empn3;
                }
                else
                {
                    FlagSign3 = 0;
                    FlagEmpn3 = "";
                }
            }
        }

        private void BTN_Sign4_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void DeleteBtn2_Click(object sender, EventArgs e)
        {
            DeleteLogic();
        }

        private void Cmb_Fy_Talb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //call sp that get last num that eentered for this MM and this YYYY
            Constants.opencon();

            // string cmdstring = "Exec SP_getlast @TRNO,@MM,@YYYY,@Num output";
            string cmdstring = @"select T_TalbTawreed.TalbTwareed_No from T_TalbTawreed where IsChemical = 1 and Mohmat_Sign is not null and FYear = @FY";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);

            // cmd.Parameters.AddWithValue("@C1", row.Cells[0].Value);
            cmd.Parameters.AddWithValue("@FY", Cmb_Fy_Talb.Text);
            ///   cmd.Parameters.AddWithValue("@CE", Constants.CodeEdara);

            DataTable dts = new DataTable();

            dts.Load(cmd.ExecuteReader());
            TXT_TalbTawred.DataSource = dts;
            TXT_TalbTawred.ValueMember = "TalbTwareed_No";
            TXT_TalbTawred.DisplayMember = "TalbTwareed_No";
            TXT_TalbTawred.SelectedIndex = -1;
            Constants.closecon();
        }
    }
}
