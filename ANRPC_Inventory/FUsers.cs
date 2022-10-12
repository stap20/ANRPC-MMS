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
namespace ANRPC_Inventory
{
    public partial class FUsers : Form
    {
        //Class Variables
        //---------------
        public SqlConnection con;//sql conn for anrpc_sms db
        public SqlConnection con2;//sql conn for anrpc_root db
        public DataTable DT = new DataTable();
        public DataTable DTT = new DataTable();
        public SqlDataAdapter dataadapter;
        public DataSet ds = new DataSet();

        public int DTIndex = 0; //Initial Value is 0
        public int max_index = 0;
        public int operationIndex = 0;
        public int row;
        public int oldempn;
        public string oldusername;
        public string oldtype;
        public int DF = 0;

        int flag = 0;
        int flag2 = 0;
        int AddEditFlag; //flag that help me to know operation edit or add
        bool executemsg;

        AutoCompleteStringCollection EnamesColl = new AutoCompleteStringCollection();   //empnname 
        AutoCompleteStringCollection EMPNColl = new AutoCompleteStringCollection();   // empn
        AutoCompleteStringCollection UsersColl = new AutoCompleteStringCollection();   // empn


        public FUsers()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        //Control Tools
        //-------------
        private void ExitTool_Click(object sender, EventArgs e)
        {
            Constants.EXIT_Btn();
        }

        private void MinimizeTool_Click(object sender, EventArgs e)
        {
            Constants.Minimize_Btn(this);
        }

        private void BackTool_Click(object sender, EventArgs e)
        {
           // Constants.ba(this);
        }


        //Reset All Input Texts 
        //---------------------
        private void Input_Reset()
        {

            TXT_Password.Text = "";
            TXT_username.Text = "";
            Name_text.Text = "";
            EMPN_text.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            CHAdmin.Checked = false;
            checkBox12.Checked = false;
            checkBox13.Checked = false;
            checkBox14.Checked = false;
            checkBox15.Checked = false;
            checkBox16.Checked = false;
            Edara_cmb.SelectedIndex = -1;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
           
            
        }
        //---------------------------------------



        //DataGrid View Reset
        //-------------------
        public void DataGridViewReset()
        {
            /*
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DT;
            //dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            //---------------
            dataGridView1.Columns[0].HeaderText = "اسم المستخدم";
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[0].DataPropertyName= "Username";
            dataGridView1.Columns[0].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[0].Width = 60;


            dataGridView1.Columns[1].HeaderText ="رقم سرى ";
            dataGridView1.Columns[1].Width = 150;
             dataGridView1.Columns[1].DataPropertyName = "UPassword";
            dataGridView1.Columns[1].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[1].Width = 100;

            dataGridView1.Columns[2].HeaderText = "اسم الموظف ";
            dataGridView1.Columns[2].Width = 150;
             dataGridView1.Columns[2].DataPropertyName = "EmpName";
            dataGridView1.Columns[2].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[2].Visible = false;

            dataGridView1.Columns[3].HeaderText = "رقم القيد ";
            dataGridView1.Columns[3].Width = 60;
             dataGridView1.Columns[3].DataPropertyName = "Empn";
            dataGridView1.Columns[3].ContextMenuStrip = contextMenuStrip1;
            //dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[3].Visible = false;
            //---------------
            //---------------
            dataGridView1.Columns[4].HeaderText = "كود ادارة";
            dataGridView1.Columns[4].Width = 60;
         dataGridView1.Columns[4].DataPropertyName = "CodeEdara";
            dataGridView1.Columns[4].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[4].Visible = false;
            //---------------
           // ---------------
            dataGridView1.Columns[5].HeaderText = "اسم الادارة";
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[5].DataPropertyName = "NameEdara";
            dataGridView1.Columns[5].ContextMenuStrip = contextMenuStrip1;
            //---------------
            dataGridView1.Columns[6].HeaderText = "نوع المستخدم";
            dataGridView1.Columns[6].Width = 60;
            dataGridView1.Columns[6].DataPropertyName= "UserType";
            dataGridView1.Columns[6].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[6].Visible = false;
            //---------------

            ///////
            dataGridView1.Columns[7].HeaderText = "بحث التصنيفات";
            dataGridView1.Columns[7].Width = 60;
             dataGridView1.Columns[7].DataPropertyName = "F11";
            dataGridView1.Columns[7].ContextMenuStrip = contextMenuStrip1;
            //---------------

            ////////////
            dataGridView1.Columns[8].HeaderText = "حركة الصنف";
            dataGridView1.Columns[8].Width = 60;
            dataGridView1.Columns[8].DataPropertyName = "F12";
            dataGridView1.Columns[8].ContextMenuStrip = contextMenuStrip1;
            //---------------

            //---------------
            dataGridView1.Columns[9].HeaderText = "انشاء امر الشراء";
            dataGridView1.Columns[9].Width = 60;
           dataGridView1.Columns[9].DataPropertyName = "F13";
            dataGridView1.Columns[9].ContextMenuStrip = contextMenuStrip1;
            //---------------

            dataGridView1.Columns[10].HeaderText = "متابعة امر شراء ";
            dataGridView1.Columns[10].Width = 60;
           dataGridView1.Columns[10].DataPropertyName = "F14";
            dataGridView1.Columns[10].ContextMenuStrip = contextMenuStrip1;
            //---------------
            dataGridView1.Columns[11].HeaderText = "متابعة اذن الصرف";
            dataGridView1.Columns[11].Width = 60;
            dataGridView1.Columns[11].DataPropertyName = "F15";
            dataGridView1.Columns[11].ContextMenuStrip = contextMenuStrip1;

            //---------------
            dataGridView1.Columns[12].HeaderText = "متابعة طلب التوريد ";
            dataGridView1.Columns[12].Width = 60;
            dataGridView1.Columns[12].DataPropertyName = "F16";
            dataGridView1.Columns[12].ContextMenuStrip = contextMenuStrip1;

            //---------------
            //---------------
            dataGridView1.Columns[13].HeaderText = "الاضافة المخزنية ";
            dataGridView1.Columns[13].Width = 60;
            dataGridView1.Columns[13].DataPropertyName= "F17";
            dataGridView1.Columns[13].ContextMenuStrip = contextMenuStrip1;
            //---------------
            //---------------
            //---------------
            dataGridView1.Columns[14].HeaderText = "الاستلام";
            dataGridView1.Columns[14].Width = 60;
            dataGridView1.Columns[14].DataPropertyName= "F18";
            dataGridView1.Columns[14].ContextMenuStrip = contextMenuStrip1;
            
            dataGridView1.Columns[15].HeaderText = "إعداد مستخدمين";
            dataGridView1.Columns[15].Width = 60;
            dataGridView1.Columns[15].DataPropertyName = "F19";
            dataGridView1.Columns[15].ContextMenuStrip = contextMenuStrip1;
            //////////////////////////////////////////////

            

            dataGridView1.Columns[16].HeaderText = "Admin / User";
            dataGridView1.Columns[16].Width = 60;
            dataGridView1.Columns[16].DataPropertyName = "F20";
            dataGridView1.Columns[16].ContextMenuStrip = contextMenuStrip1;*/
            dataGridView1.DataSource = null;
           
           dataGridView1.Columns.Clear();
            dataGridView1.DataSource = DT;
            dataGridView1.Refresh();
            //---------------
            dataGridView1.Columns[0].HeaderText = "اسم المستخدم";
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[0].DataPropertyName = "UserName";
            dataGridView1.Columns[0].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[0].Width = 100;


            dataGridView1.Columns[1].HeaderText = "الرقم السرى";
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[1].DataPropertyName = "UPassword";
            dataGridView1.Columns[1].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[1].Width =50;

            dataGridView1.Columns[2].HeaderText = "رقم قيد";
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[2].DataPropertyName = "EmpName";
            dataGridView1.Columns[2].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[2].Visible = false;

            dataGridView1.Columns[3].HeaderText = "الاسم";
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[3].DataPropertyName = "Empn";
            dataGridView1.Columns[3].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[3].Visible = false;

            dataGridView1.Columns[4].HeaderText = "كود الادارة";
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[4].DataPropertyName = "CodeEdara";
            dataGridView1.Columns[4].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[4].Visible = false;

            dataGridView1.Columns[5].HeaderText = "اسم الادارة";
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[5].DataPropertyName = "NameEdara";
            dataGridView1.Columns[5].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[5].Width = 100;
            //
            dataGridView1.Columns[6].HeaderText = "نوع المستخدم";
            dataGridView1.Columns[6].Width = 60;
            dataGridView1.Columns[6].DataPropertyName = "UserType";
            dataGridView1.Columns[6].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[6].Visible = false;
            //---------------

            //---------------
            dataGridView1.Columns[7].HeaderText = "بحث التصنيفات";
            dataGridView1.Columns[7].Width = 50;
            dataGridView1.Columns[7].DataPropertyName = "F1";
            dataGridView1.Columns[7].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[7].Visible = false;

            //---------------
            //---------------
            dataGridView1.Columns[8].HeaderText = "انشاء اذن الصرف";
            dataGridView1.Columns[8].Width = 50;
            dataGridView1.Columns[8].DataPropertyName = "F2";
            dataGridView1.Columns[8].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[8].Visible = false;

            //---------------
            //---------------
            dataGridView1.Columns[9].HeaderText = "متابعة اذن صرف";
            dataGridView1.Columns[9].Width = 50;
            dataGridView1.Columns[9].DataPropertyName = "F3";
            dataGridView1.Columns[9].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[9].Visible = false;

            //---------------

            //---------------
            dataGridView1.Columns[10].HeaderText = "انشاء طلب التوريد";
            dataGridView1.Columns[10].Width = 50;
            dataGridView1.Columns[10].DataPropertyName = "F4";
            dataGridView1.Columns[10].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[10].Visible = false;

            //---------------

            dataGridView1.Columns[11].HeaderText = "متابعة طلب التوريد ";
            dataGridView1.Columns[11].Width = 50;
            dataGridView1.Columns[11].DataPropertyName = "F5";
            dataGridView1.Columns[11].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[11].Visible = false;

            //---------------
            dataGridView1.Columns[12].HeaderText = "مطابقة فنية";
            dataGridView1.Columns[12].Width = 50;
            dataGridView1.Columns[12].DataPropertyName = "F6";
            dataGridView1.Columns[12].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[12].Visible = false;


            //---------------
            dataGridView1.Columns[13].HeaderText = "طباعة تقرير";
            dataGridView1.Columns[13].Width = 50;
            dataGridView1.Columns[13].DataPropertyName = "F7";
            dataGridView1.Columns[13].ContextMenuStrip = contextMenuStrip1;
         //   dataGridView1.Columns[13].Visible = false;

            ///////////////



            ///////
            dataGridView1.Columns[14].HeaderText = "بحث التصنيفات";
            dataGridView1.Columns[14].Width = 50;
            dataGridView1.Columns[14].DataPropertyName = "F11";
            dataGridView1.Columns[14].ContextMenuStrip = contextMenuStrip1;
            //---------------

            ////////////
            dataGridView1.Columns[15].HeaderText = "حركة الصنف";
            dataGridView1.Columns[15].Width = 50;
            dataGridView1.Columns[15].DataPropertyName = "F12";
            dataGridView1.Columns[15].ContextMenuStrip = contextMenuStrip1;
            //---------------

            //---------------
            dataGridView1.Columns[16].HeaderText = "انشاء امر الشراء";
            dataGridView1.Columns[16].Width = 50;
            dataGridView1.Columns[16].DataPropertyName = "F13";
            dataGridView1.Columns[16].ContextMenuStrip = contextMenuStrip1;
            //---------------

            dataGridView1.Columns[17].HeaderText = "متابعة امر شراء ";
            dataGridView1.Columns[17].Width = 50;
            dataGridView1.Columns[17].DataPropertyName = "F14";
            dataGridView1.Columns[17].ContextMenuStrip = contextMenuStrip1;
            //---------------
            dataGridView1.Columns[18].HeaderText = "متابعة اذن الصرف";
            dataGridView1.Columns[18].Width = 50;
            dataGridView1.Columns[18].DataPropertyName = "F15";
            dataGridView1.Columns[18].ContextMenuStrip = contextMenuStrip1;


            //---------------
            dataGridView1.Columns[19].HeaderText = "متابعة طلب التوريد ";
            dataGridView1.Columns[19].Width = 50;
            dataGridView1.Columns[19].DataPropertyName = "F16";
            dataGridView1.Columns[19].ContextMenuStrip = contextMenuStrip1;

            //---------------
            //---------------
            dataGridView1.Columns[20].HeaderText = "الاضافة المخزنية ";
            dataGridView1.Columns[20].Width = 50;
            dataGridView1.Columns[20].DataPropertyName = "F17";
            dataGridView1.Columns[20].ContextMenuStrip = contextMenuStrip1;
            //---------------
            //---------------
            //---------------
            dataGridView1.Columns[21].HeaderText = "الاستلام";
            dataGridView1.Columns[21].Width = 50;
            dataGridView1.Columns[21].DataPropertyName = "F18";
            dataGridView1.Columns[21].ContextMenuStrip = contextMenuStrip1;

            dataGridView1.Columns[22].HeaderText = "إعداد مستخدمين";
            dataGridView1.Columns[22].Width = 50;
            dataGridView1.Columns[22].DataPropertyName = "F19";
            dataGridView1.Columns[22].ContextMenuStrip = contextMenuStrip1;
            //////////////////////////////////////////////



            dataGridView1.Columns[23].HeaderText = "Admin / User";
            dataGridView1.Columns[23].Width = 50;
            dataGridView1.Columns[23].DataPropertyName = "F20";
            dataGridView1.Columns[23].ContextMenuStrip = contextMenuStrip1;
            /////////////////

        }

        public void DataGridViewReset2()
        {

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DTT;
            //dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            //---------------
            dataGridView1.Columns[0].HeaderText = "اسم المستخدم";
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[0].DataPropertyName = "UserName";
            dataGridView1.Columns[0].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[0].Width = 150;


            dataGridView1.Columns[1].HeaderText =  "الرقم السرى";
            dataGridView1.Columns[1].Width =50;
            dataGridView1.Columns[1].DataPropertyName = "UPassword";
            dataGridView1.Columns[1].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[1].Width = 50;

            dataGridView1.Columns[2].HeaderText = "رقم قيد";
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[2].DataPropertyName = "EmpName";
            dataGridView1.Columns[2].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[2].Visible = false;

            dataGridView1.Columns[3].HeaderText = "الاسم";
            dataGridView1.Columns[3].Width = 150;
             dataGridView1.Columns[3].DataPropertyName = "Empn";
            dataGridView1.Columns[3].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[3].Visible = false;

            dataGridView1.Columns[4].HeaderText = "كود الادارة";
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[4].DataPropertyName= "CodeEdara";
            dataGridView1.Columns[4].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[4].Visible = false;

            dataGridView1.Columns[5].HeaderText = "اسم الادارة";
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[5].DataPropertyName= "NameEdara";
            dataGridView1.Columns[5].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[5].Width = 100;
            //
            dataGridView1.Columns[6].HeaderText = "نوع المستخدم";
            dataGridView1.Columns[6].Width = 60;
            dataGridView1.Columns[6].DataPropertyName = "UserType";
            dataGridView1.Columns[6].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[6].Visible = false;
            //---------------

            //---------------
            dataGridView1.Columns[7].HeaderText = "بحث التصنيفات";
            dataGridView1.Columns[7].Width = 60;
            dataGridView1.Columns[7].DataPropertyName = "F1";
            dataGridView1.Columns[7].ContextMenuStrip = contextMenuStrip1;
            //---------------
            //---------------
            dataGridView1.Columns[8].HeaderText = "انشاء اذن الصرف";
            dataGridView1.Columns[8].Width = 60;
            dataGridView1.Columns[8].DataPropertyName = "F2";
            dataGridView1.Columns[8].ContextMenuStrip = contextMenuStrip1;
            //---------------
            //---------------
            dataGridView1.Columns[9].HeaderText = "متابعة اذن صرف";
            dataGridView1.Columns[9].Width = 60;
           dataGridView1.Columns[9].DataPropertyName = "F3";
            dataGridView1.Columns[9].ContextMenuStrip = contextMenuStrip1;
            //---------------

            //---------------
            dataGridView1.Columns[10].HeaderText = "انشاء طلب التوريد";
            dataGridView1.Columns[10].Width = 60;
           dataGridView1.Columns[10].DataPropertyName = "F4";
            dataGridView1.Columns[10].ContextMenuStrip = contextMenuStrip1;
            //---------------

            dataGridView1.Columns[11].HeaderText = "متابعة طلب التوريد ";
            dataGridView1.Columns[11].Width = 60;
           dataGridView1.Columns[11].DataPropertyName = "F5";
            dataGridView1.Columns[11].ContextMenuStrip = contextMenuStrip1;
            //---------------
            dataGridView1.Columns[12].HeaderText = "مطابقة فنية";
            dataGridView1.Columns[12].Width = 60;
           dataGridView1.Columns[12].DataPropertyName = "F6";
            dataGridView1.Columns[12].ContextMenuStrip = contextMenuStrip1;

            //---------------
            dataGridView1.Columns[13].HeaderText = "طباعة تقرير";
            dataGridView1.Columns[13].Width = 60;
            dataGridView1.Columns[13].DataPropertyName = "F7";
            dataGridView1.Columns[13].ContextMenuStrip = contextMenuStrip1;

      ///////////////
         
          

            ///////
            dataGridView1.Columns[14].HeaderText = "بحث التصنيفات";
            dataGridView1.Columns[14].Width = 60;
            dataGridView1.Columns[14].DataPropertyName = "F11";
            dataGridView1.Columns[14].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[14].Visible = false;

            //---------------

            ////////////
            dataGridView1.Columns[15].HeaderText = "حركة الصنف";
            dataGridView1.Columns[15].Width = 60;
            dataGridView1.Columns[15].DataPropertyName = "F12";
            dataGridView1.Columns[15].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[15].Visible = false;

            //---------------

            //---------------
            dataGridView1.Columns[16].HeaderText = "انشاء امر الشراء";
            dataGridView1.Columns[16].Width = 60;
            dataGridView1.Columns[16].DataPropertyName = "F13";
            dataGridView1.Columns[16].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[16].Visible = false;

            //---------------

            dataGridView1.Columns[17].HeaderText = "متابعة امر شراء ";
            dataGridView1.Columns[17].Width = 60;
            dataGridView1.Columns[17].DataPropertyName = "F14";
            dataGridView1.Columns[17].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[17].Visible = false;

            //---------------
            dataGridView1.Columns[18].HeaderText = "متابعة اذن الصرف";
            dataGridView1.Columns[18].Width = 60;
            dataGridView1.Columns[18].DataPropertyName = "F15";
            dataGridView1.Columns[18].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[18].Visible = false;


            //---------------
            dataGridView1.Columns[19].HeaderText = "متابعة طلب التوريد ";
            dataGridView1.Columns[19].Width = 60;
            dataGridView1.Columns[19].DataPropertyName = "F16";
            dataGridView1.Columns[19].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[19].Visible = false;


            //---------------
            //---------------
            dataGridView1.Columns[20].HeaderText = "الاضافة المخزنية ";
            dataGridView1.Columns[20].Width = 60;
            dataGridView1.Columns[20].DataPropertyName = "F17";
            dataGridView1.Columns[20].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[20].Visible = false;

            //---------------
            //---------------
            //---------------
            dataGridView1.Columns[21].HeaderText = "الاستلام";
            dataGridView1.Columns[21].Width = 60;
            dataGridView1.Columns[21].DataPropertyName = "F18";
            dataGridView1.Columns[21].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[21].Visible = false;


            dataGridView1.Columns[22].HeaderText = "إعداد مستخدمين";
            dataGridView1.Columns[22].Width = 60;
            dataGridView1.Columns[22].DataPropertyName = "F19";
            dataGridView1.Columns[22].ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns[22].Visible = false;

            //////////////////////////////////////////////



            dataGridView1.Columns[23].HeaderText = "Admin / User";
            dataGridView1.Columns[23].Width = 60;
            dataGridView1.Columns[23].DataPropertyName = "F20";
            dataGridView1.Columns[23].ContextMenuStrip = contextMenuStrip1;
            /////////////////


        }
        private void FUsers_Load(object sender, EventArgs e)
        {
            //   Constants.DrawRectangle();
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[1]; // ArabicLanguage 
            con = new SqlConnection(Constants.constring);
           con2 = new SqlConnection(Constants.constring2);
            //-------------------------------------------

            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }  //--> OPEN CONNECTION

            if (con2 != null && con2.State == ConnectionState.Closed)
            {
                con2.Open();
            }  //--> OPEN CONNECTION



            //*******************************************
            // ******    AUTO COMPLETE
            //*******************************************

            //Name and EMPN
            //--------------

            string cmdstring = "select ENAME,EMPN from EmpMast";
            SqlCommand cmd = new SqlCommand(cmdstring, con2);
            SqlDataReader dr = cmd.ExecuteReader();
            //---------------------------------
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    EnamesColl.Add(dr["ENAME"].ToString());
                    EMPNColl.Add(dr["EMPN"].ToString());
                }
            }
            dr.Close();
            //////////////////////////
            cmdstring = "select UserName from UsersPrivilages";
             cmd = new SqlCommand(cmdstring, con);
             dr = cmd.ExecuteReader();
            //---------------------------------
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    UsersColl.Add(dr["UserName"].ToString());
                  //  EMPNColl.Add(dr["EMPN"].ToString());
                }
            }
            dr.Close();

            /////////////////////////
            string query = "SELECT CodeEdara , NameEdara FROM Edarat";
            cmd = new SqlCommand(query, con);
            DataTable dts = new DataTable();
            dts.Load(cmd.ExecuteReader());
            Edara_cmb.DataSource = dts;
            Edara_cmb.ValueMember = "CodeEdara";
            Edara_cmb.DisplayMember = "NameEdara";

            EMPN_text.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            EMPN_text.AutoCompleteSource = AutoCompleteSource.CustomSource;
            EMPN_text.AutoCompleteCustomSource = EMPNColl;

            Name_text.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Name_text.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Name_text.AutoCompleteCustomSource = EnamesColl;

           TXT_username.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
           TXT_username.AutoCompleteSource = AutoCompleteSource.CustomSource;
          TXT_username.AutoCompleteCustomSource = UsersColl;


            //EMPN_text
            loadgridview2();
            con.Close(); // --> CLOSE CONNECTION



        }

        private void Name_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (con != null && con.State == ConnectionState.Closed)
            {
                con2.Open();
            }
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {


                if (operationIndex == 1 || operationIndex == 2)//add
                {
                    string cmdstring = "select ENAME,EMPN from  EmpMast where ENAME=@a ";
                    SqlCommand cmd = new SqlCommand(cmdstring, con2);
                    cmd.Parameters.AddWithValue("@a", (Name_text.Text.ToString()));
                    SqlDataReader dr = cmd.ExecuteReader();
                    //---------------------------------
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            EMPN_text.Text = (dr["EMPN"].ToString());
                            Name_text.Text = (dr["ENAME"].ToString());

                        }
                    }
                    dr.Close();
                }
                else
                {


                    string query = "select  * from UsersPrivilages where UserName = @a ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@a", (TXT_username.Text.ToString()));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            if (dr["UserType"] == "A")
                            {

                                panel3.Visible = true;
                                panel4.Visible = false;

                                EMPN_text.Text = dr["EMPN"].ToString();
                                Name_text.Text = dr["EmpName"].ToString(); ;
                                TXT_Password.Text = dr["UPassword"].ToString();
                                TXT_username.Text = dr["UserName"].ToString();
                                radioButton1.Checked = true;
                                radioButton2.Checked = false;

                                checkBox1.Checked = (bool)dr["F1"];
                                checkBox2.Checked = (bool)dr["F2"];
                                checkBox3.Checked = (bool)dr["F3"];
                                checkBox4.Checked = (bool)dr["F4"];

                                checkBox6.Checked = (bool)dr["F5"];
                                checkBox7.Checked = (bool)dr["F6"];
                                checkBox8.Checked = (bool)dr["F7"];

                                CHAdmin.Checked = (bool)dr["Admin20"];
                            }
                            else
                            {
                                panel3.Visible = false;
                                panel4.Visible = true;
                                EMPN_text.Text = dr["EMPN"].ToString();
                                Name_text.Text = dr["EmpName"].ToString(); ;
                                TXT_Password.Text = dr["UPassword"].ToString();
                                TXT_username.Text = dr["UserName"].ToString();
                                radioButton1.Checked = true;
                                radioButton2.Checked = false;

                                checkBox14.Checked = (bool)dr["F11"];
                                checkBox13.Checked = (bool)dr["F12"];
                                checkBox12.Checked = (bool)dr["F13"];
                                checkBox11.Checked = (bool)dr["F14"];

                                checkBox10.Checked = (bool)dr["F15"];
                                checkBox15.Checked = (bool)dr["F16"];
                                checkBox9.Checked = (bool)dr["F17"];

                                checkBox5.Checked = (bool)dr["F18"];
                                checkBox16.Checked = (bool)dr["F19"];
                               
                                CHAdmin.Checked = (bool)dr["Admin20"];
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("لا يوجد مستخدم بذلك الأسم ");

                    }
                    dr.Close();

                    if (dr["UserType"].ToString() == "A")
                    {

                        loadgridview2();
                    }
                    else
                    {
                        loadgridview1();
                    }

                }

            }
            con.Close();
        }

        private void EMPN_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (con2 != null && con2.State == ConnectionState.Closed)
            {
                con2.Open();
            }
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {


                if (operationIndex == 1 || operationIndex == 2)//add
                {
                    string cmdstring = "select ENAME,EMPN from EmpMast where Empn=@a ";
                    SqlCommand cmd = new SqlCommand(cmdstring, con2);
                    cmd.Parameters.AddWithValue("@a", Convert.ToInt32(EMPN_text.Text));
                    SqlDataReader dr = cmd.ExecuteReader();
                    //---------------------------------
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            //  Name_text.Text=(dr["ENAME"].ToString());
                            EMPN_text.Text = (dr["EMPN"].ToString());
                            Name_text.Text = (dr["ENAME"].ToString());

                        }
                    }
                    dr.Close();
                }
                else
                {


                    string query = "select  UserName,UPassword,Empn,EmpName,CodeEdara,NameEdara,[F1],[F2],[F3],[F4],[F5],[F6],[F7],F8,F9,F10,F11,F20  from UsersPrivilages where UserName = @a ";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@a", Convert.ToInt32(EMPN_text.Text));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            // EMPN_text.Text=dr["EMPN"].ToString();
                            Name_text.Text = dr["EmpName"].ToString(); ;
                            TXT_Password.Text = dr["UPassword"].ToString();
                            TXT_username.Text = dr["UserName"].ToString();
                            checkBox1.Checked = (bool)dr["F1"];
                            checkBox2.Checked = (bool)dr["F2"];
                            checkBox3.Checked = (bool)dr["F3"];
                            checkBox4.Checked = (bool)dr["F4"];
                            checkBox5.Checked = (bool)dr["F5"];
                            checkBox6.Checked = (bool)dr["F6"];
                            checkBox7.Checked = (bool)dr["F7"];
                            checkBox8.Checked = (bool)dr["F8"];
                            checkBox9.Checked = (bool)dr["F9"];
                            checkBox10.Checked = (bool)dr["F10"];
                            CHAdmin.Checked = (bool)dr["AdminFlag"];
                            checkBox12.Checked = (bool)dr["F11"];
                        }
                    }
                    else
                    {
                        MessageBox.Show("لا يوجد مستخدم بذلك رقم القيد ");

                    }
                    dr.Close();


                    loadgridview1();

                }

            }
            con.Close();
        }





        private void contextMenuStrip1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // In Case of Edit 
            //----------------
            if (e.ClickedItem.Name == "EditTool")
            {

                // operationIndex = 2;
           
                if (dataGridView1.Rows[mouseLocation.RowIndex].Cells[6].Value.ToString() == "A")
                {
                    bindTextboxses1();
                }
                else if (dataGridView1.Rows[mouseLocation.RowIndex].Cells[6].Value.ToString() == "B")
                {
                    bindTextboxses2();
                }
               
                BTN_Edit.PerformClick();


                //----------------------------------------
            }



         //In case of Remove
            //-------------------
            else if (e.ClickedItem.Name == "RemoveTool")
            {
                DF = 1;
                DeleteRecord(DF);

            }

        }




        private void BTN_Save_Click(object sender, EventArgs e)
        {
            //validation
            //validation 
            if (string.IsNullOrWhiteSpace(EMPN_text.Text))
            {
               // MessageBox.Show("ادخل اسم الموظف");
              //  Name_text.Focus();
             //   return;
            }
            if (string.IsNullOrWhiteSpace(TXT_username.Text))
            {
                MessageBox.Show("ادخل اسم المستخدم");
                TXT_username.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(TXT_Password.Text))
            {
                MessageBox.Show("ادخل الرقم السرى");
                TXT_Password.Focus();
                return;
            }
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("من فضلك اختار نوع المستخدم");
                return;
            }
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //check if username or  empn already exist in user privialge msgbox appear this user is taken 
            if ((operationIndex == 2) && oldusername == TXT_username.Text.ToString())//user name doesnt change& edit
            {

            }
            else
            {
                flag = CHECK_username();
            }
            /*
            if ((operationIndex == 2) && oldempn == Convert.ToInt32(EMPN_text.Text))//user name doesnt change& edit
            {

            }
            else
            {
               // flag2 = CHECK_empn();
            }*/

         //   if (flag2 == 2)//found
         //   {
          //      MessageBox.Show("برجاء اختيار موظف اخر");
           //     return;
            //}
            if (flag == 2)//found
            {
                MessageBox.Show("برجاء اختيار اسم مستخدم اخر");
                return;
            }

            if (operationIndex == 2)//edit
            {
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                try
                {

                    SqlCommand strCmd = new SqlCommand("SP_UpdateUser", con);
                    strCmd.CommandType = CommandType.StoredProcedure;
                    strCmd.Parameters.AddWithValue("UserName", TXT_username.Text.ToString());
                    strCmd.Parameters.AddWithValue("UPassword", TXT_Password.Text.ToString());
                    strCmd.Parameters.AddWithValue("Empn", (EMPN_text.Text.ToString()));//type
                    strCmd.Parameters.AddWithValue("EmpName", Name_text.Text.ToString());//resp
                    strCmd.Parameters.AddWithValue("CodeEdara", (Edara_cmb.SelectedValue));//type
                    strCmd.Parameters.AddWithValue("NameEdara", Edara_cmb.Text.ToString());//resp
                    if (radioButton1.Checked == true)
                    {
                        strCmd.Parameters.AddWithValue("UserType","A");//resp
                    }
                    else if (radioButton2.Checked == true)
                    {
                        strCmd.Parameters.AddWithValue("UserType","B" );//resp
                    }
                    strCmd.Parameters.AddWithValue("F1", checkBox1.Checked);//edara
                    strCmd.Parameters.AddWithValue("F2", checkBox2.Checked);//reason
                    strCmd.Parameters.AddWithValue("F3", checkBox3.Checked);//time in 
                    strCmd.Parameters.AddWithValue("F4", checkBox4.Checked);//time out
                    strCmd.Parameters.AddWithValue("F5", checkBox6.Checked);//Notes
                    strCmd.Parameters.AddWithValue("F6", checkBox7.Checked);//Notes
                    strCmd.Parameters.AddWithValue("F7", checkBox8.Checked);
                  
                    strCmd.Parameters.AddWithValue("F11", checkBox14.Checked);//edara
                    strCmd.Parameters.AddWithValue("F12", checkBox13.Checked);//reason
                    strCmd.Parameters.AddWithValue("F13", checkBox12.Checked);//time in 
                    strCmd.Parameters.AddWithValue("F14", checkBox11.Checked);//time out
                    strCmd.Parameters.AddWithValue("F15", checkBox10.Checked);//Notes
                    strCmd.Parameters.AddWithValue("F16", checkBox15.Checked);//Notes
                    strCmd.Parameters.AddWithValue("F17", checkBox9.Checked);
                    strCmd.Parameters.AddWithValue("F18", checkBox5.Checked);
                    strCmd.Parameters.AddWithValue("F19", checkBox16.Checked);
                    strCmd.Parameters.AddWithValue("F20", CHAdmin.Checked);
                    strCmd.Parameters.AddWithValue("olduser", oldusername);
                    strCmd.Parameters.AddWithValue("oldtype", oldtype);
                    strCmd.ExecuteNonQuery();

                    executemsg = true;

                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    // Constants.SqlExceptionCatching(sqlEx);
                }


                if (executemsg == true)
                {
                    MessageBox.Show("تمت التعديل بنجاح");
                    if (radioButton1.Checked == true)
                    {
                        loadgridview2();
                    }
                    else
                    {
                        loadgridview1();
                    }
                  
                    BTN_Save.Visible = false;
                    Input_Reset();
                    operationIndex = 0;
                }
                con.Close();
            }


            else if (operationIndex == 1)//add
            {


                try
                {

                    SqlCommand strCmd = new SqlCommand("SP_InsertUser", con);
                    strCmd.CommandType = CommandType.StoredProcedure;
                    strCmd.Parameters.AddWithValue("UserName", TXT_username.Text.ToString());
                    strCmd.Parameters.AddWithValue("UPassword", TXT_Password.Text.ToString());
                    strCmd.Parameters.AddWithValue("Empn", (EMPN_text.Text.ToString()));//type
                    strCmd.Parameters.AddWithValue("EmpName", Name_text.Text.ToString());//resp
                    strCmd.Parameters.AddWithValue("CodeEdara",(Edara_cmb.SelectedValue));//type
                    strCmd.Parameters.AddWithValue("NameEdara", Edara_cmb.Text.ToString());//resp
                    if (radioButton1.Checked == true)
                    {
                        strCmd.Parameters.AddWithValue("UserType", "A");//resp
                    }
                    else if (radioButton2.Checked == true)
                    {
                        strCmd.Parameters.AddWithValue("UserType", "B");//resp
                    }
                    strCmd.Parameters.AddWithValue("F1", checkBox1.Checked);//edara
                    strCmd.Parameters.AddWithValue("F2", checkBox2.Checked);//reason
                    strCmd.Parameters.AddWithValue("F3", checkBox3.Checked);//time in 
                    strCmd.Parameters.AddWithValue("F4", checkBox4.Checked);//time out
                    strCmd.Parameters.AddWithValue("F5", checkBox6.Checked);//Notes
                    strCmd.Parameters.AddWithValue("F6", checkBox7.Checked);//Notes
                    strCmd.Parameters.AddWithValue("F7", checkBox8.Checked);

                    strCmd.Parameters.AddWithValue("F11", checkBox14.Checked);//edara
                    strCmd.Parameters.AddWithValue("F12", checkBox13.Checked);//reason
                    strCmd.Parameters.AddWithValue("F13", checkBox12.Checked);//time in 
                    strCmd.Parameters.AddWithValue("F14", checkBox11.Checked);//time out
                    strCmd.Parameters.AddWithValue("F15", checkBox10.Checked);//Notes
                    strCmd.Parameters.AddWithValue("F16", checkBox15.Checked);//Notes
                    strCmd.Parameters.AddWithValue("F17", checkBox9.Checked);
                    strCmd.Parameters.AddWithValue("F18", checkBox5.Checked);
                    strCmd.Parameters.AddWithValue("F19", checkBox16.Checked);
                    strCmd.Parameters.AddWithValue("F20", CHAdmin.Checked);
                   // strCmd.Parameters.AddWithValue("AdminFlag", CHAdmin.Checked);
                    strCmd.ExecuteNonQuery();

                    executemsg = true;

                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    //    Constants.SqlExceptionCatching(sqlEx);
                }


                if (executemsg == true)
                {
                    MessageBox.Show("تمت الإضافة بنجاح");
                    if (radioButton1.Checked == true)
                    {
                        loadgridview2();
                    }
                    else
                    {
                        loadgridview1();
                    }
                    BTN_Save.Visible = false;
                    Input_Reset();
                    operationIndex = 0;
                }
                con.Close();
            }



        }


        private void cleargridview()
        {

        }
        private void BTN_AddVistor_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد إضافة إدخال جديد ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                Input_Reset();
                operationIndex = 1;

                BTN_Save.Visible = true;
            }


        }

        private void BTN_Edit_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل تريد  التعديل ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {


                if ((TXT_username.Text) == "")
                {
                    MessageBox.Show("برجاء اختيار المستخدم المراد تعديل");
                    return;
                }
              //  oldempn = (TXT_username.Text);
                oldusername = TXT_username.Text.ToString();
                if (radioButton1.Checked == true)
                {
                    oldtype = "A";
                }
                else
                {
                    oldtype = "B";
                }
                
                // Input_Reset();
                operationIndex = 2;
                BTN_Save.Visible = true;
            }
        }

        private void Name_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            //allow only letters for name.text
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void IDNO_Text_KeyPress(object sender, KeyPressEventArgs e)
        {

            //allow only digit for IDNO
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void BTN_SearchMenu_Click(object sender, EventArgs e)
        {
            //Constants.FormNo = 9;
            //    FVistor_Search F = new FVistor_Search();
            //    F.ShowDialog();
        }

        public void DisablePanel3()
        {
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
            checkBox6.Enabled = false;
            checkBox7.Enabled = false;
            checkBox8.Enabled = false;
        }
       public void EnabledPanel3()
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox6.Enabled = true;
            checkBox7.Enabled = true;
            checkBox8.Enabled = true;
        }
       public void EnabledPanel4()
       {
           checkBox9.Enabled = true;
           checkBox10.Enabled = true;
           checkBox11.Enabled =true;
           checkBox12.Enabled =true;
           checkBox13.Enabled = true;
           checkBox14.Enabled = true;
           checkBox15.Enabled = true;
           checkBox16.Enabled = true;
           checkBox5.Enabled = true;
       }
       public void DisabledPanel4()
       {
           checkBox9.Enabled = false;
           checkBox10.Enabled = false;
           checkBox11.Enabled = false;
           checkBox12.Enabled = false;
           checkBox13.Enabled = false;
           checkBox14.Enabled = false;
           checkBox15.Enabled = false;
           checkBox16.Enabled = false;
           checkBox5.Enabled = false;
       }
        



        private void loadgridview1()
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
          
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
           // dataGridView1.Columns.Remove("F1");

            // string query1 = "SELECT [UserName],[UPassword],EmpName,Empn,CodeEdara,NameEdara,UserType ,[F11],[F12],[F13],[F14],[F15],[F16],[F17],F18,F19,F20 FROM [UsersPrivilages]  where UserType='B' ";

            string query1 = "SELECT  * FROM [UsersPrivilages]  where UserType='B' ";



            SqlCommand cmd1 = new SqlCommand(query1, con);
            DT.Clear();
            //this.dataGridView1.AutoGenerateColumns =false;
            this.dataGridView1.Columns.Clear();
            DT.Load(cmd1.ExecuteReader());
            DataGridViewReset();
            
            dataGridView1.Refresh();
           
        }

        private void loadgridview2()
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
         //   string query1 = "SELECT [UserName],[UPassword],EmpName,Empn,CodeEdara,NameEdara ,UserType ,[F1],[F2],[F3],[F4],[F5],[F6],[F7],F20 FROM [UsersPrivilages] where UserType='A' ";
            string query1 = "SELECT  * FROM [UsersPrivilages]  where UserType='A' ";

            SqlCommand cmd1 = new SqlCommand(query1, con);
            DTT.Clear();
            //dataGridView1.Columns.Remove("F1");
           // this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns.Clear();

            DTT.Load(cmd1.ExecuteReader());
            DataGridViewReset2();
            dataGridView1.Refresh();
        }

        private void TXT_username_KeyDown(object sender, KeyEventArgs e)
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (e.KeyCode == Keys.Enter)  // Search and get the data by the name 
            {

                string query = "SELECT * FROM [UsersPrivilages]  where UserName = @a ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@a", TXT_username.Text.ToString());
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        if (dr["UserType"].ToString() == "A")
                        {
                            radioButton1.Checked = true;
                            radioButton2.Checked = false;
                          DisabledPanel4();

                            loadgridview2();
                        }
                        else if (dr["UserType"].ToString() == "B")
                        {
                            radioButton1.Checked = false;
                            radioButton2.Checked = true;
                           DisablePanel3();

                            loadgridview1();
                        }

                        if (con != null && con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                       EMPN_text.Text = dr["EMPN"].ToString();
                        Name_text.Text = dr["EmpName"].ToString(); ;
                        TXT_Password.Text = dr["UPassword"].ToString();
                        Edara_cmb.SelectedValue = dr["CodeEdara"].ToString();
                        //  TXT_username.Text = dr["UserName"].ToString();
                        checkBox1.Checked = (bool)dr["F1"];
                        checkBox2.Checked = (bool)dr["F2"];
                        checkBox3.Checked = (bool)dr["F3"];
                        checkBox4.Checked = (bool)dr["F4"];
                       // checkBox5.Checked = (bool)dr["F10"];
                        checkBox6.Checked = (bool)dr["F5"];
                        checkBox7.Checked = (bool)dr["F6"];

                        checkBox8.Checked = (bool)dr["F7"];
                        checkBox14.Checked = (bool)dr["F11"];
                        checkBox13.Checked = (bool)dr["F12"];
                        checkBox12.Checked = (bool)dr["F13"];
                        checkBox11.Checked = (bool)dr["F14"];
                        checkBox10.Checked = (bool)dr["F15"];
                        checkBox15.Checked = (bool)dr["F16"];
                        checkBox9.Checked = (bool)dr["F17"];
                        checkBox5.Checked = (bool)dr["F18"];
                        checkBox16.Checked = (bool)dr["F19"];
                       
                        CHAdmin.Checked = (bool)dr["F20"];
                    }
                }
                else
                {

                    MessageBox.Show("لا يوجد مستخدم بهذا الاسم");
                }
                dr.Close();



            }
            con.Close();
        }

        private void EMPN_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }



        private int CHECK_username()
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }



            SqlCommand strCmd = new SqlCommand("SP_checkUSERNAMETAKEN", con);
            strCmd.CommandType = CommandType.StoredProcedure;
            strCmd.Parameters.AddWithValue("UserName", TXT_username.Text.ToString());
            //  strCmd.Parameters.AddWithValue("operationindex", operationIndex);
            strCmd.Parameters.Add("@flag", SqlDbType.Int, 32);  //-------> output parameter
            strCmd.Parameters["@flag"].Direction = ParameterDirection.Output;



            try
            {
                strCmd.ExecuteNonQuery();
                executemsg = true;
                flag = (int)strCmd.Parameters["@flag"].Value;
            }
            catch (SqlException sqlEx)
            {
                executemsg = false;
                MessageBox.Show(sqlEx.ToString());

            }
            return flag;


            con.Close();


        }
        ////////////////////////////////////////////
        private int CHECK_empn()
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }



            SqlCommand strCmd = new SqlCommand("SP_checkEmpnTAKEN", con);
            strCmd.CommandType = CommandType.StoredProcedure;
            strCmd.Parameters.AddWithValue("Empn", Convert.ToInt32(EMPN_text.Text));
            // strCmd.Parameters.AddWithValue("operationindex", operationIndex);
            strCmd.Parameters.Add("@flag", SqlDbType.Int, 32);  //-------> output parameter
            strCmd.Parameters["@flag"].Direction = ParameterDirection.Output;



            try
            {
                strCmd.ExecuteNonQuery();
                executemsg = true;
                flag2 = (int)strCmd.Parameters["@flag"].Value;
            }
            catch (SqlException sqlEx)
            {
                executemsg = false;
                MessageBox.Show(sqlEx.ToString());

            }
            return flag2;


            con.Close();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void DeleteRecord(int DF)
        {
            if ((MessageBox.Show("هل تريد الحذف ؟", "", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {

                try
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query1 = "Delete from UsersPrivilages Where UserName = @a";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    if (DF == 1)
                    {
                        cmd1.Parameters.AddWithValue("@a",(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                    }
                    else if (DF == 2)
                    {
                        cmd1.Parameters.AddWithValue("@a", (TXT_username.Text));
                    }
                    //.Parameters.AddWithValue("@d",Convert.ToDateTime( dataGridView1.CurrentRow.Cells[0].Value.ToString()).ToShortDateString());


                    cmd1.ExecuteReader();
                  //  DataGridViewReset();
                    dataGridView1.Refresh();
                    con.Close();
                    executemsg = true;
                }
                catch (SqlException sqlEx)
                {
                    executemsg = false;
                    //Constants.SqlExceptionCatching(sqlEx);
                }


                if (executemsg == true)
                {
                    MessageBox.Show("تم الحذف  .");
                    //------------------------------------

                    // rebinde datagridview 
                    //---------------------
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    loadgridview1();
                    con.Close();
                }

                else
                { //No
                    //----
                }

            }
        }

        private void BTN_delete_Click(object sender, EventArgs e)
        {

        }

        private void TXT_Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void EMPN_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TXT_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void Name_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void bindTextboxses1()
        {

            EMPN_text.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Name_text.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            TXT_username.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TXT_Password.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Edara_cmb.SelectedValue = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[6].Value.ToString()=="A")
            {
                radioButton1.Checked = true;
            }
            else if (dataGridView1.CurrentRow.Cells[6].Value.ToString() == "B")
            {
                radioButton2.Checked = true;
            }
            EnabledPanel3();
            checkBox1.Checked = (bool)dataGridView1.CurrentRow.Cells[7].Value;
            checkBox2.Checked = (bool)dataGridView1.CurrentRow.Cells[8].Value;
            checkBox3.Checked = (bool)dataGridView1.CurrentRow.Cells[9].Value;
            checkBox4.Checked = (bool)dataGridView1.CurrentRow.Cells[10].Value;
            //   checkBox5.Checked = (bool)dataGridView1.CurrentRow.Cells[9].Value;
            checkBox6.Checked = (bool)dataGridView1.CurrentRow.Cells[11].Value;
            
            checkBox7.Checked = (bool)dataGridView1.CurrentRow.Cells[12].Value;
            checkBox8.Checked = (bool)dataGridView1.CurrentRow.Cells[13].Value;
         
            CHAdmin.Checked = (bool)dataGridView1.CurrentRow.Cells[14].Value;
        }
        private void bindTextboxses2()
        {

            EMPN_text.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Name_text.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            TXT_username.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TXT_Password.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Edara_cmb.SelectedValue = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[6].Value.ToString() == "A")
            {
                radioButton1.Checked = true;
            }
            else if (dataGridView1.CurrentRow.Cells[6].Value.ToString() == "B")
            {
                radioButton2.Checked = true;
            }
            EnabledPanel4();
            checkBox14.Checked = (bool)dataGridView1.CurrentRow.Cells[7].Value;
            checkBox13.Checked = (bool)dataGridView1.CurrentRow.Cells[8].Value;
            checkBox12.Checked = (bool)dataGridView1.CurrentRow.Cells[9].Value;
            checkBox11.Checked = (bool)dataGridView1.CurrentRow.Cells[10].Value;
            //   checkBox5.Checked = (bool)dataGridView1.CurrentRow.Cells[9].Value;
            checkBox10.Checked = (bool)dataGridView1.CurrentRow.Cells[11].Value;

            checkBox15.Checked = (bool)dataGridView1.CurrentRow.Cells[12].Value;
            checkBox9.Checked = (bool)dataGridView1.CurrentRow.Cells[13].Value;

            checkBox5.Checked = (bool)dataGridView1.CurrentRow.Cells[14].Value;
            checkBox16.Checked = (bool)dataGridView1.CurrentRow.Cells[15].Value;

            CHAdmin.Checked = (bool)dataGridView1.CurrentRow.Cells[16].Value;
        }

        private void BTN_delete_Click_1(object sender, EventArgs e)
        {
            DeleteRecord(2);
            Input_Reset();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                panel3.Enabled = true;
                panel4.Enabled = false;
                EnabledPanel3();
                DisabledPanel4();  
                if (AddEditFlag == 0)
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    loadgridview2();
                    if (con != null && con.State == ConnectionState.Open)
                    {
                       // con.Close();
                    }
                }
                

            }
            else
            {
                panel3.Enabled =false;
             

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true && radioButton1.Checked==false)
            {
                panel4.Enabled = true;
                panel3.Enabled = false;
                EnabledPanel4();
                DisablePanel3();
                if (AddEditFlag == 0)
                {
                    if (con != null && con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    loadgridview1();
                    if (con != null && con.State == ConnectionState.Open)
                    {
                     //   con.Close();
                    }

                }
                

            }
            else
            {
                panel4.Enabled = false;


            }
        }
        private DataGridViewCellEventArgs mouseLocation;
        private void dataGridView1_MouseEnter(object sender, EventArgs e)
        {
           // mouseLocation = Location;
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs location)
        {
            mouseLocation = location;
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {

        }


    }
}
