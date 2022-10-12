using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ANRPC_Inventory
{
    public partial class FLogin : Form
    {
        SqlConnection con;

        public string username = "";
        public string password = "";

        public FLogin()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            CurrencyConverter3.init();
        }


        //Validations
        //--------------
        private void user_txt_Leave(object sender, EventArgs e)
        {
            if (user_txt.Text == "")
            {
                user_txt.Text = "User name";
            }
        }
        private void user_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void user_txt_Click(object sender, EventArgs e)
        {
            if (user_txt.Text == "User name")
            {
                user_txt.Text = "";
            }
        }
        //---------------------------
        private void password_txt_Leave(object sender, EventArgs e)
        {
            if (password_txt.Text == "")
            {
                password_txt.Text = "Password";
                password_txt.PasswordChar = '\0';
            }
        }

        private void password_txt_Click(object sender, EventArgs e)
        {
            if (password_txt.Text == "Password")
            {
                password_txt.Text = "";
                password_txt.PasswordChar = '●';
            }
        }

        //=====================================================

        private void FLogin_Load(object sender, EventArgs e)
        {
            Password_label.Visible = false;


            con = new SqlConnection(Constants.constring);
            con.Open();

            string query = "SELECT [UserName] FROM UsersPrivilages order by UserType desc,Username ";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dts = new DataTable();
            dts.Load(cmd.ExecuteReader());
            user_txt.DataSource = dts;
            user_txt.ValueMember = "UserName";
            user_txt.DisplayMember = "UserName";
            con.Close();
            user_txt.SelectedIndex = -1;
        }


        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Constants.EXIT_Btn();
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            Constants.Minimize_Btn(this);
        }

        private void lgnBtn_Click(object sender, EventArgs e)
        {

            username = "";
            password = "";

            con.Open();
            //---------------

            string cmdstring = "Select * from [UsersPrivilages] where UserName = @u order by UserType desc";
            SqlCommand cmd = new SqlCommand(cmdstring, con);
            cmd.Parameters.AddWithValue("@u", user_txt.SelectedValue.ToString());
            //-----------------------------------
            //Data Reader to read the values from Database 
            SqlDataReader dr = cmd.ExecuteReader();
            //-----------------------------------
            while (dr.Read())
            {
                username = dr["UserName"].ToString();
                password = dr["UPassword"].ToString();
                Constants.CodeEdara = dr["CodeEdara"].ToString();
                Constants.NameEdara = dr["NameEdara"].ToString();
                Constants.User_Type = dr["UserType"].ToString();
                if (Constants.User_Type == "B")
                {
                    Constants.UserTypeB =username.Substring(username.IndexOf("_") + 1);
                }

            }


            /////////////////////check version///////////////////
            string cmdstring2 = "Select * from T_Version where VersionFlag=1";
            SqlCommand cmd2 = new SqlCommand(cmdstring2, con);
          //  cmd.Parameters.AddWithValue("@u", user_txt.SelectedValue.ToString());
            //-----------------------------------
            //Data Reader to read the values from Database 
            SqlDataReader dr2 = cmd2.ExecuteReader();
            //-----------------------------------
            
            while (dr2.Read())
            {

                if (Constants.VersionID ==  Convert.ToInt32(dr2["VersionID"].ToString()))
                {
                   //do nothing 
                }
                else
                {
                    MessageBox.Show("نسخة البرنامج غير محدثة برجاء الرجوع لادارة نظم المعلومات لتحديث النسخة");
                    System.Environment.Exit(1);
                }

            }


            ////////////////end of check version//////////////////
            con.Close();


            //-------------------------------------------------
            //validation on username and password
            //------------------------------------

            // UserName_label.Visible = false;
            Password_label.Visible = false;

            if (password != password_txt.Text)
            {
                // Login Faild !
                //--------------

                password_txt.Focus();

                //-----------------
                //   UserName_label.Visible = false;
                Password_label.Visible = true;
            }
            else
            {
                // Login Successful 
                //-----------------
                Constants.User_Name = username;

       
                if(Constants.User_Type=="A")
                {
                FPublic AF = new FPublic();
                AF.Show();
                this.Hide();
                }
                else if(Constants.User_Type=="B"){
                Fmain AF = new Fmain();
                AF.Show();
                this.Hide();
                }
               
            }

        }

        private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Constants.FormNo = 7;
            FReports f = new FReports();
            f.Show();
        }

        private void user_txt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
