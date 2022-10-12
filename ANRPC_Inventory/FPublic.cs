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

namespace ANRPC_Inventory
{
    public partial class FPublic : Form
    {
        public int Count1;
        public int Count2;
        public string CR;
        public string CG;
        public string CY;
        public FPublic()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Constants.EXIT_Btn();
        }

        //
           // TalbTawred MF = new TalbTawred();
            //MF.Show();
            //MF.MdiParent = this;
            //MF.Dock = DockStyle.Fill; 
            //MF.Show();
            //this.Hide()

        private void BtnTalbTawred_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null) 
            {
                Constants.currentOpened.Close();  
            }
            //----------------------
            pictureBox2.Visible = false;
            Constants.talbtawred_F = true; //--> panel7 --> Invisible - panel2 --> visible
            TalbTawred F = new TalbTawred();
            Constants.currentOpened = F; 
            F.Show();
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;

        }
        //-------------------------------
        private void button4_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            pictureBox2.Visible = false;
            Constants.talbtawred_F = false; //--> panel7 --> visible - panel2 --> Invisible
            TalbTawred F = new TalbTawred();
            Constants.currentOpened = F;
            F.Show();
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
        }
        //---------------------------------------
        private void BtnEznsarf_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            pictureBox2.Visible = false;
            Constants.EznSarf_FF = true; //--> panel7 --> visible - panel2 --> Invisible
            EznSarf_F F = new EznSarf_F();
            Constants.currentOpened = F;
            F.Show();
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;

        }

        private void FPublic_Load(object sender, EventArgs e)
        {
            label5.Text = Constants.NameEdara;
            //this.Close();
            
            
            Constants.opencon();
            string query = "select * from UsersPrivilages where UserName = @a ";
            SqlCommand cmd = new SqlCommand(query,Constants.con);
            cmd.Parameters.AddWithValue("@a", Constants.User_Name);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {

                    //  TXT_username.Text = dr["UserName"].ToString();
                    BtnTasnif.Enabled = (bool)dr["F1"];
                    BtnTalbTawred.Enabled = (bool)dr["F2"];
                    button4.Enabled = (bool)dr["F3"];
                    BtnEznsarf.Enabled = (bool)dr["F4"];
                    button7.Enabled = (bool)dr["F5"];
                    button1.Enabled = (bool)dr["F6"];
                    Constants.ReportsFlag = (bool)dr["F7"]; 
                    Constants.AdminUserFlag = (bool)dr["F20"];
                }
            }
            GetProblemsCount();
            Constants.closecon();
        }

        private void BtnTasnif_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
           // Constants.TasnifTrans = true; //--> panel7 --> Invisible - panel2 --> visible
            pictureBox2.Visible = false;
            Tasnif F = new Tasnif();
            Constants.currentOpened = F;
            F.Show();
            F.MdiParent = this;

            F.Dock = DockStyle.Fill;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            pictureBox2.Visible = false;
            Constants.EznSarf_FF = false; //--> panel7 --> visible - panel2 --> Invisible
            EznSarf_F  F = new EznSarf_F();
            Constants.currentOpened = F;
            F.Show();
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            pictureBox2.Visible = false;
           // Constants.talbtawred_F = true; //--> panel7 --> Invisible - panel2 --> visible
           Search_TalbTawreed F = new Search_TalbTawreed();
            Constants.currentOpened = F;
            F.Show();
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;

        }
          public void GetProblemsCount()
        {
            Constants.opencon();


            string cmdstring = "select Count(distinct Edafa_No) as e from T_EdaraNotfication where  ( Sign4 is null) and EdaraName = N'" + Constants.NameEdara + "'";
          
              SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);


           Count1 = (Int32)cmd.ExecuteScalar();
           if (Count1 > 0 && button1.Enabled==true)
           {
               MessageBox.Show("يوجد عدد " + Count1.ToString() + "من المطابقات الفنية المعلقة");
             //  label4.Text = "عدد المطابقات الفنية المعلقة:"+Count1.ToString();
           }
           else if(Count1==0)
           {
             //  label4.Text = "عدد المطابقات الفنية المعلقة:0" ;
           }
              ///////////////////////
           cmdstring = "exec SP_CheckStatusMotabka @EN,@R out,@G out,@Y out";
           cmd = new SqlCommand(cmdstring, Constants.con);
           cmd.Parameters.AddWithValue("@EN",Constants.NameEdara);
           SqlParameter Red= new SqlParameter("@R", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Red);
           SqlParameter Green= new SqlParameter("@G", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Green);
              SqlParameter Yellow = new SqlParameter("@Y", SqlDbType.Int,32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Yellow);

           cmd.ExecuteNonQuery();
        
           CR = Red.Value.ToString();
           CG = Green.Value.ToString();  
           CY=Yellow.Value.ToString();
           label6.Text = CG;
           label7.Text = CR;
           label8.Text = CY;



              /////////////////////////////////////////////////////////




           ///////////////////////
           cmdstring = "exec SP_CheckStatusEznTahwel @EN,@R out,@G out,@Y out";
           cmd = new SqlCommand(cmdstring, Constants.con);
           cmd.Parameters.AddWithValue("@EN", Constants.NameEdara);
            Red = new SqlParameter("@R", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Red);
           Green = new SqlParameter("@G", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Green);
            Yellow = new SqlParameter("@Y", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Yellow);

           cmd.ExecuteNonQuery();

           CR = Red.Value.ToString();
           CG = Green.Value.ToString();
           CY = Yellow.Value.ToString();
           label22.Text = CG;
           label21.Text = CR;
           label20.Text = CY;



           /////////////////////////////////////////////////////////
           cmdstring = "exec SP_CheckStatusAmrSheraa @EN,@R out,@G out,@Y out";
            cmd = new SqlCommand(cmdstring, Constants.con);
           cmd.Parameters.AddWithValue("@EN", Constants.CodeEdara);
        Red = new SqlParameter("@R", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Red);
           Green = new SqlParameter("@G", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Green);
         Yellow = new SqlParameter("@Y", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Yellow);

           cmd.ExecuteNonQuery();

           CR = Red.Value.ToString();
           CG = Green.Value.ToString();
           CY = Yellow.Value.ToString();
           label11.Text = CG;
           label10.Text = CR;
           label9.Text = CY;

              /////////////////////////////////////////////////////////


           cmdstring = "exec SP_CheckStatusEznsarf @EN,@R out,@G out,@Y out";
           cmd = new SqlCommand(cmdstring, Constants.con);
           cmd.Parameters.AddWithValue("@EN", Constants.NameEdara);
           Red = new SqlParameter("@R", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Red);
           Green = new SqlParameter("@G", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Green);
           Yellow = new SqlParameter("@Y", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Yellow);

           cmd.ExecuteNonQuery();

           CR = Red.Value.ToString();
           CG = Green.Value.ToString();
           CY = Yellow.Value.ToString();
           label14.Text = CG;
           label13.Text = CR;
           label12.Text = CY;
              ///////////////////////////////////////////////////


           cmdstring = "exec SP_CheckStatusTalb @EN,@R out,@G out,@Y out";
           cmd = new SqlCommand(cmdstring, Constants.con);
           cmd.Parameters.AddWithValue("@EN", Constants.NameEdara);
           Red = new SqlParameter("@R", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Red);
           Green = new SqlParameter("@G", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Green);
           Yellow = new SqlParameter("@Y", SqlDbType.Int, 32) { Direction = ParameterDirection.Output };
           cmd.Parameters.Add(Yellow);

           cmd.ExecuteNonQuery();

           CR = Red.Value.ToString();
           CG = Green.Value.ToString();
           CY = Yellow.Value.ToString();
           label17.Text = CG;
           label16.Text = CR;
           label15.Text = CY;


           //  label4.Text= " <span style='color:red'>" + CR + "</span> km." + " <span style='color:green'>" + CG + "</span> km." +" <span style='color:yellow'>" + CY+ "</span> km.";;

         //  label4.Text = "عدد المطابقات الفنية المعلقة:" + CR.se

           //-----------------------------------
           //Data Reader to read the values from Database 

           //////////////////////////////////////////////

           string cmdstring2 = "select Count(TalbTwareed_No) as e from T_TalbTawreed where  (Confirm_Sign1 is null  and Confirm_Sign2 is null )and CodeEdara = " + Constants.CodeEdara;

           SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);


           Count1 = (Int32)cmd2.ExecuteScalar();
           if (Count1 >= 0 && button4.Enabled==true )
           {
               if (Count1 > 0)
               {


                   MessageBox.Show("يوجد عدد " + Count1.ToString() + " طلبات توريد تحتاج الى تصديق");
               }
            C1.Text = Count1.ToString();
           toolTip1 = new ToolTip();
            //The below are optional, of course,

           toolTip1.ToolTipIcon = ToolTipIcon.Info;
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(C1, "طلبات توريد تحتاج الى تصديق");
           }
           else if (Count1 == 0)
           {
             
           }




           ///////////////////////////////////////////////

           string cmdstring22 = "select Count(TalbTwareed_No) as e from T_TalbTawreed where  (Confirm_Sign1 is  not null  )and( Confirm_Sign2 is null )and CodeEdara = " + Constants.CodeEdara;

           SqlCommand cmd22 = new SqlCommand(cmdstring22, Constants.con);


           Count1 = (Int32)cmd22.ExecuteScalar();
           if (Count1 >=0 && button4.Enabled == true)
           {
               if (Count1 > 0)
               {
                   MessageBox.Show("يوجد عدد " + Count1.ToString() + " طلبات توريد تحتاج الى اعتماد");
               }
             C2.Text =Count1.ToString();

             toolTip1.ToolTipIcon = ToolTipIcon.Info;
             toolTip1.IsBalloon = true;
             toolTip1.ShowAlways = true;

             toolTip1.SetToolTip(C2, "طلبات توريد تحتاج الى اعتماد");
           }
           else if (Count1 == 0)
           {
               // label2.Text = "عدد المطابقات الفنية المعلقة:0";
           }







           ///////////////////////////////////////

           string cmdstring3 = "select Count(EznSarf_No) as e from T_EznSarf where  (Sign2 is null  ) and ( Sign1 is not null) and CodeEdara = " + Constants.CodeEdara;

           SqlCommand cmd3= new SqlCommand(cmdstring3, Constants.con);


           Count1 = (Int32)cmd3.ExecuteScalar();
           if (Count1 >=0&& button7.Enabled==true)
           {
               if (Count1 > 0)
               {
                   MessageBox.Show("يوجد عدد " + Count1.ToString() + " طلبات اذن صرف تحتاج الى توقيع اعتماد ");
               }
              C3.Text = Count1.ToString();

              toolTip1.ToolTipIcon = ToolTipIcon.Info;
              toolTip1.IsBalloon = true;
              toolTip1.ShowAlways = true;

              toolTip1.SetToolTip(C3, "اذون صرف تحتاج الى اعتماد");
           }
           else if (Count1 == 0)
           {
               // label2.Text = "عدد المطابقات الفنية المعلقة:0";
           }
              /////////////////

           cmdstring3 = "select Count(TransNo) as e from T_EzonTahwel where  (Sign2 is null  ) and ( Sign1 is not null) and FromEdaraCode = " + Constants.CodeEdara;

            cmd3 = new SqlCommand(cmdstring3, Constants.con);


           Count1 = (Int32)cmd3.ExecuteScalar();
           if (Count1 >= 0 && button7.Enabled == true)
           {
               if (Count1 > 0)
               {
                   MessageBox.Show("يوجد عدد " + Count1.ToString() + " طلبات اذن تحويل تحتاج الى توقيع اعتماد ");
               }
               C3.Text = Count1.ToString();

               toolTip1.ToolTipIcon = ToolTipIcon.Info;
               toolTip1.IsBalloon = true;
               toolTip1.ShowAlways = true;

               toolTip1.SetToolTip(C3, "اذون تحويل تحتاج الى اعتماد");
           }
           else if (Count1 == 0)
           {
               // label2.Text = "عدد المطابقات الفنية المعلقة:0";
           }


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

           string cmdstring4 = "select Count(EznSarf_No) as e from T_EznSarf where  ( Sign4 is null  ) and (Sign1 is not null) and (Sign2 is not null) and (Sign3 is not null) and CodeEdara = " + Constants.CodeEdara;

           SqlCommand cmd4 = new SqlCommand(cmdstring4, Constants.con);


           Count1 = (Int32)cmd4.ExecuteScalar();
           if (Count1 >= 0 && button7.Enabled == true)
           {
               if (Count1 > 0)
               {
                   MessageBox.Show("يوجد عدد " + Count1.ToString() + " طلبات اذن صرف تحتاج  توقيع المستلم");
               }
             C4.Text =  Count1.ToString();

             toolTip1.ToolTipIcon = ToolTipIcon.Info;
             toolTip1.IsBalloon = true;
             toolTip1.ShowAlways = true;

             toolTip1.SetToolTip(C4, "اذون صرف تحتاج الى استلام");
           }
           else if (Count1 == 0)
           {
               // label2.Text = "عدد المطابقات الفنية المعلقة:0";
           }
///////////////////////////////////////////////////////////

           string cmdstring5 = "select (TalbTwareed_No)  from T_TalbTawreed where  (Confirm_Sign1 is not null)  and ( Confirm_Sign2 is not null ) and(Sign8 is not null) and(Stock_Sign is not null) and( Sign11 is  not null ) and Sign9 is not null and(CH_Sign is  null) and Audit_Sign is  null  and Mohmat_Sign is null and Sign10 is not null and CodeEdara = " + Constants.CodeEdara;

 //where  (Confirm_Sign1 is not null)  and (Confirm_Sign2 is not null ) and (Sign8 is not null) and(Stock_Sign is not null) and( Sign11 is  not null ) and Sign9 is not null and(CH_Sign is not null) and Audit_Sign is not null  and Mohmat_Sign is null ";
          
              
              SqlCommand cmd5 = new SqlCommand(cmdstring5, Constants.con);


             SqlDataReader dr = cmd5.ExecuteReader();

             if (dr.HasRows == true)
             {
                 while (dr.Read())
                 {
                     MessageBox.Show("رقم طلب توريد " + dr["TalbTwareed_No"].ToString() + " تم رفضه من قبل العضو المنتدب ");

                 }
             }
              /////////////////////////
            cmdstring5 = "select Count(Amrshraa_No) as e from T_Awamershraa where CodeEdara=@E and   (Sign3 is null ) and  ( Sign14 is not null) ";
            cmd5 = new SqlCommand(cmdstring5, Constants.con);

            cmd5.Parameters.AddWithValue("@E", Constants.CodeEdara);
             Count1 = (Int32)cmd5.ExecuteScalar();
             if (Count1 > 0)
             {
                 MessageBox.Show("يوجد عدد " + Count1.ToString() + "اوامر شراء تحتاج متابعة من مدير عام الادارة الطالبة");
                 // label3.Text = label3.Text + " " + Count1.ToString();
             }
             else if (Count1 == 0)
             {
                 // label2.Text = "عدد المطابقات الفنية المعلقة:0";
             }
           ///////////////////////////
           Constants.closecon();
            //----------------
           
        }
     
        public void GetProblemsCount2()
        {
            Constants.opencon();

            string cmdstring = "select Count(distinct Edafa_No)) as e from T_EdaraNotfication where  EdaraName = N'" + Constants.NameEdara + "'";
          
            SqlCommand cmd3 = new SqlCommand(cmdstring, Constants.con);


            Count2 = (Int32)cmd3.ExecuteScalar();
            if (Count2 > Count1)
            {
                timer1.Enabled = false;
                MessageBox.Show("There is new task please check ");

                Count1 = Count2;
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = true;
            }
            Constants.closecon();


            //----------------

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetProblemsCount2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
        //    Constants.EznSarf_FF = true; //--> panel7 --> visible - panel2 --> Invisible
            pictureBox2.Visible = false;
          FEdafaMakhzania_F_Edara F = new FEdafaMakhzania_F_Edara();
            Constants.currentOpened = F;
            F.Show();
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            //    Constants.EznSarf_FF = true; //--> panel7 --> visible - panel2 --> Invisible
            pictureBox2.Visible = false;
            AmrSheraa F = new AmrSheraa();
            Constants.currentOpened = F;
            F.Show();
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            Constants.EzonTahwel_FF = true; //--> panel7 --> visible - panel2 --> Invisible
            pictureBox2.Visible = false;
            FTransfer_M F = new FTransfer_M();
            Constants.currentOpened = F;
            F.Show();
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            Constants.EzonTahwel_FF =false; //--> panel7 --> visible - panel2 --> Invisible
            pictureBox2.Visible = false;
            FTransfer_M F = new FTransfer_M();
            Constants.currentOpened = F;
            F.Show();
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
        }

        private void button12_Click(object sender, EventArgs e)
        {

            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            Constants.EzonTahwel_FF = false; //--> panel7 --> visible - panel2 --> Invisible
            pictureBox2.Visible = false;
            FTransfer_AA F = new FTransfer_AA();
            Constants.currentOpened = F;
            F.Show();
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
        }
    }
}
