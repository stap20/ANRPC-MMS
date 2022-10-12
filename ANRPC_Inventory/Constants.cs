using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Data.SqlClient;

namespace ANRPC_Inventory
{
    public static class Constants
    {
        #region atrr - nour
        public static string constring = "server=172.18.8.48;database=ANRPC_Inventory_v2;user=TMS;password=P@ssw0rd;MultipleActiveResultSets=true";
        public static string constring2 = "server=172.18.8.48;database=ANRPC_Root;user=TMS;password=P@ssw0rd;MultipleActiveResultSets=true";
        public static string constring3 = "server=172.18.8.48;database=ANRPC_Inventory_foriegn_v2;user=TMS;password=P@ssw0rd;MultipleActiveResultSets=true";


        public static string warehouse_app_machine_directory = @"\\172.18.8.83\MaterialApp\Photos\";
        public static int VersionID = 2;
        public static int AuthFlag;
        public static int RedirectedFlag;
        public static string FlagRedirectEmpn = "";
        public static string MangerName;
        public static string CodeEdara;
        public static string NameEdara;
        public static string Ename;
        public static string Wazifa;
        public static string TransferNO;
        public static string TRFY;
        public static string TRNO;
        public static string User_Name;
        public static string User_Type;
        public static string Unit;
        public static string TasnifNo;
        public static string TasnifName;
        public static string Desc;
        public static string Quan;
        public static string RakmEdafa;
        public static string DateEdafa;
        public static string Date_E;
        public static string AmrNo;
        public static string AmrSanaMalya;
        public static string MwardName;
        public static string No_Tard;
        public static string No_Bnod;
        public static string Sanf;
        public static string Date_Amr;
        public static string Sign1;
        public static string Sign2;
        public static string Sign3;
        public static string Sign4;
        public static int FormNo;
        public static Form currentOpened;
        public static bool talbtawred_F;
        public static bool EznSarf_FF;
        public static bool Amrshera_F;
        public static int EdafaNo;
        public static string EdafaFY;

        public static int TalbNo;
        public static string TalbFY;
        public static string MowardName;

        public static int EznNo;
        public static string EznFY;
        public static string STockNoALL;
        public static bool AdminUserFlag;
        public static bool ReportsFlag;

        public static string UserTypeB;

        public static string STockname;
        public static string STockBian;


        public static string STockno;
        public static string stockmax;

        public static string STockmin;
        public static string Stocklocation;
        public static string Stockunit;
        public static bool EzonTahwel_FF;

        public static Boolean executemsg;
        public static SqlConnection con = new SqlConnection(Constants.constring);
        public static SqlConnection con3 = new SqlConnection(Constants.constring3);
        #endregion

        public static readonly Dictionary<string, string> SIGNATURE_TYPES = new Dictionary<string, string>
        {
            {"T-1","1" },
            {"T-2","2" },
            {"T-3","3" },
            {"T-4","8" },
            {"T-5","12" },
            {"T-6","4" },
            {"T-7","11" },
            {"T-8","9" },
            {"T-9","13" },
            {"T-10","7" },
            {"T-11","5" },
            {"T-12","6" },
        };

        #region meth- nour
        public static void EXIT_Btn()
        {
            //con.Close(); 
            if ((MessageBox.Show("هل تريد الخروج؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                Application.Exit(); // Close the Application 
            }
            else
            {
                //con.Open(); 
            }
        }
        public static void opencon()
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public static void opencon3()
        {
            if (con3 != null && con3.State == ConnectionState.Closed)
            {
                con3.Open();
            }
        }
        public static void closecon()
        {
            if (con != null && con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        public static string GetStock(string group)
        {
            Constants.opencon();
            string g;
            string cmdstring = "select Stock_NO from T_GTsnif where STOCK_NO_G=@G";
            SqlCommand cmd1 = new SqlCommand(cmdstring, Constants.con);

            cmd1.Parameters.AddWithValue("@G", group);

            var scalar = cmd1.ExecuteScalar();
            if (scalar != DBNull.Value && scalar != null) // Case where the DB value is null
            {
                g = scalar.ToString();
                executemsg = true;

                return g;
            }
            else
            {
                return "";
            }
            Constants.closecon();
        }
        public static int GetTalbStatus(string TT, string FY)
        {
            Constants.opencon();

            //string cmdstring = "select SignaturePicPath from [T_Signatures2] where SignatureCode=@code and [SignatureForm]=@F and Empn=@E ";
            string cmdstring = "exec SP_ReturnTalbStatus @TT,@FY,@flag out ";
            SqlCommand cmd1 = new SqlCommand(cmdstring, Constants.con);

            cmd1.Parameters.AddWithValue("@TT", TT);
            cmd1.Parameters.AddWithValue("@FY", FY);
            cmd1.Parameters.Add("@flag", SqlDbType.Int, 32);  //-------> output parameter
            cmd1.Parameters["@flag"].Direction = ParameterDirection.Output;

            int flag = 0;

            try
            {
                cmd1.ExecuteNonQuery();
                executemsg = true;
                flag = (int)cmd1.Parameters["@flag"].Value;

            }
            catch
            {

            }
            return flag;
            //  cmd1.Parameters.AddWithValue("@E", empn);
        }
        public static string RetrieveSignature(string code, string formNo, string empn)
        {
            Constants.opencon();
            string path;
            //string cmdstring = "select SignaturePicPath from [T_Signatures2] where SignatureCode=@code and [SignatureForm]=@F and Empn=@E ";
            string cmdstring = " select e.Wazifa,e.ENAME,s.SignaturePicPath from EmpMast e inner join T_Signatures2 s on e.empn=s.Empn where SignatureCode=@code and [SignatureForm]=@F and s.Empn=@E ";

            SqlCommand cmd1 = new SqlCommand(cmdstring, Constants.con);

            cmd1.Parameters.AddWithValue("@code", code);
            cmd1.Parameters.AddWithValue("@F", formNo);
            cmd1.Parameters.AddWithValue("@E", empn);
            /*var scalar = cmd1.ExecuteScalar();
            if (scalar != DBNull.Value &&  scalar !=null) // Case where the DB value is null
            {
                path = scalar.ToString();
                executemsg = true;

                return path;
            }
            else
            {
                return "";
            }*/
            SqlDataReader dr = cmd1.ExecuteReader();
            path = "";
            Ename = "";
            Wazifa = "";
            if (dr.HasRows == true)
            {

                while (dr.Read())
                {
                    // e.Wazifa,e.ENAME,s.SignaturePicPath
                    path = dr["SignaturePicPath"].ToString();
                    Ename = dr["ENAME"].ToString();
                    Wazifa = dr["Wazifa"].ToString();
                    path = path + ":" + Ename + ":" + Wazifa;
                    executemsg = true;


                }

                return path;
            }
            else
            {

                return "";
            }
            Constants.closecon();

        }
        public static Tuple<string, int, int, string, string> CheckSign(string code, string formNo, string pw, string empn)
        {
            int flag;
            int flagsign;
            string path;

            Constants.opencon();
            string cmdstring = "Exec SP_CheckSign @code,@F,@pw,@empn,@path out,@name out,@wazifa out,@flag out";

            SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);




            cmd.Parameters.AddWithValue("@code", code);
            cmd.Parameters.AddWithValue("@F", formNo);
            cmd.Parameters.AddWithValue("@pw", pw);
            cmd.Parameters.AddWithValue("@empn", empn);
            cmd.Parameters.Add("@path", SqlDbType.NVarChar, 100);  //-------> output parameter
            cmd.Parameters["@path"].Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 500);  //-------> output parameter
            cmd.Parameters["@name"].Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@wazifa", SqlDbType.NVarChar, 500);  //-------> output parameter
            cmd.Parameters["@wazifa"].Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@flag", SqlDbType.Int, 32);  //-------> output parameter
            cmd.Parameters["@flag"].Direction = ParameterDirection.Output;



            try
            {
                cmd.ExecuteNonQuery();
                executemsg = true;
                flag = (int)cmd.Parameters["@flag"].Value;
                if (cmd.Parameters["@path"].Value == DBNull.Value)
                {
                    path = "";
                    Ename = "";
                    Wazifa = "";
                }
                else
                {
                    path = (string)cmd.Parameters["@path"].Value;
                    Ename = (string)cmd.Parameters["@name"].Value;
                    Wazifa = (string)cmd.Parameters["@wazifa"].Value;

                }
                flagsign = 0;

            }
            catch (SqlException sqlEx)
            {
                executemsg = false;
                MessageBox.Show(sqlEx.ToString());
                flag = (int)cmd.Parameters["@flag"].Value;
                path = (string)cmd.Parameters["@path"].Value;
                flagsign = 0;

            }
            if (executemsg == true && flag == 1)
            {
                MessageBox.Show(path);
                //  Pic_Sign1.Image = Image.FromFile(@path);

                flagsign = 1;

            }
            else if (executemsg == true && flag == 2)
            {
                MessageBox.Show("رقم السرى غير صحيح");
                flagsign = 0;
            }

            var result = Tuple.Create<string, int, int, string, string>(path, flagsign, flag, Ename, Wazifa);

            return result;
            closecon();
        }
        public static void Minimize_Btn(Form f)
        {
            f.WindowState = FormWindowState.Minimized;
        }

        public static void validateTextboxNumbersonly(object sender)
        {
            TextBox tb = sender as TextBox;

            // GetData(Convert.ToInt32(TXT_TalbNo.Text), Cmb_FYear.Text);
            if (System.Text.RegularExpressions.Regex.IsMatch(tb.Text, "[^0-9]"))
            {
                MessageBox.Show("من فضلك ادخلل ارقام فقط");
                tb.Text = tb.Text.Remove(tb.Text.Length - 1);
                tb.Focus();
            }
        }

        public static void validatenumbersanddecimal(string s, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((s == "" && (e.KeyChar == '.')) || ((e.KeyChar == '.') && (s.IndexOf('.') > -1)))
            {
                e.Handled = true;
            }
        }

        //===============================================



        public static void validatenumberkeypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        #endregion
    }
}