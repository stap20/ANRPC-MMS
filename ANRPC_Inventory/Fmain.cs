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
    public partial class Fmain : Form
    {
        public int Count1 = 0;
        public Fmain()
        {
       
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        //----------------------
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Constants.EXIT_Btn();
        }
        //--------------
        private void backBtn_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            this.IsMdiContainer = false;
            tableLayoutPanel1.Visible = true;

            if (Constants.User_Name == "User1_Stock")
            {
                //button10.Visible = true;
                BTN_Redirect.Visible = true;
            }
            if (Constants.User_Name == "User1_Inventory")
            {
                button10.Visible = true;
                //  BTN_Redirect.Visible = true;
            }
            if (Constants.User_Name == "User1_InventoryControl")
            {
                button11.Visible = true;
            }
        }
        //----------------

        public void GetProblemsCount()
        {
            Constants.opencon();

            if (Constants.User_Type == "B" && Constants.UserTypeB == "NewTasnif")
            {
             //   string cmdstring = "select  sum(NewTasnifCount) as e from T_NewTasnifNotification where status=0";
                string cmdstring = "select  *   from T_NewTasnifNotification where status=0";

                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);
                              SqlDataReader dr = cmd.ExecuteReader();

                              if (dr.HasRows == true)
                              {
                                  while (dr.Read())
                                  {
                                      if (dr["NewTasnifCount"].ToString() == "0")
                                      {
                                          MessageBox.Show(" طلب توريد رقم" + dr["TalbNo"].ToString()+"يحتاج الى مراجعة");
                                          // label4.Text
                                      }
                                      else
                                      {


                                          MessageBox.Show(" هناك عدد " + dr["NewTasnifCount"].ToString() + " من التصنييفات الجديدة " + "فى طلب توريد رقم" + dr["TalbNo"].ToString());
                                          // label4.Text
                                      }
                                  }
                              }
              /*  if (cmd.ExecuteScalar() == DBNull.Value)
                {

                }
                else
                {


                    Count1 = (Int32)cmd.ExecuteScalar();
                    if (Count1 > 0)
                    {
                        MessageBox.Show(" هناك عدد " + Count1.ToString() + " من التصنييفات الجديدة "+"فى طلب توريد رقم");
                        // label4.Text = "عدد المطابقات الفنية المعلقة:" + Count1.ToString();
                    }
                    else if (Count1 == 0)
                    {
                        //   label4.Text = "عدد المطابقات الفنية المعلقة:0";
                    }
                }*/
                               string cmdstring5 = "select  count(transNo) as e from T_EzonTahwel where   ( Sign2 is not null)";
                             SqlCommand cmd5 = new SqlCommand(cmdstring5, Constants.con);


                              Count1 = (Int32)cmd5.ExecuteScalar();
                              if (Count1 > 0)
                              {
                                  MessageBox.Show("يوجد عدد " + Count1.ToString() + "اذون تحويل تحتاج متابعة من قبل ادارة التصنيفات");
                                  // label3.Text = label3.Text + " " + Count1.ToString();
                              }
                              else if (Count1 == 0)
                              {
                                  // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                              }
            }


            /////////////////////////////////////////////////////

            if (Constants.User_Type == "B" && Constants.UserTypeB == "Mwazna")
            {

                //  string cmdstring5 = "select Count(Amrshraa_No) as e from T_Awamershraa where  (Sign2 is null ) and  ( Sign1 is not null) ";
                string cmdstring2 = "select Count(TalbTwareed_No) as e from T_TalbTawreed where  (Confirm_Sign1 is not null)  and (Confirm_Sign2 is not null ) and (Sign8 is not null) and( Sign12 is null ) ";

                SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);


                Count1 = (Int32)cmd2.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + " طلبات توريد تحتاج الى متابعة من قبل ادارة مراقبة المخزون");
                    //  label2.Text = label2.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }
            }




            /////////


            /////////////////////////////////////////////////////

            if (Constants.User_Type == "B" && Constants.UserTypeB == "Transfer1")
            {

                //  string cmdstring5 = "select Count(Amrshraa_No) as e from T_Awamershraa where  (Sign2 is null ) and  ( Sign1 is not null) ";
                string cmdstring2 = "select count(transNo) as e from T_EzonTahwel where  (Sign3 is not null)   ";

                SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);


                Count1 = (Int32)cmd2.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + " اذون تحويل تحتاج متابعة من قبل مدير ادارة المخازن");
                    //  label2.Text = label2.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }
            }
            if (Constants.User_Type == "B" && Constants.UserTypeB == "Transfer2")
            {

                //  string cmdstring5 = "select Count(Amrshraa_No) as e from T_Awamershraa where  (Sign2 is null ) and  ( Sign1 is not null) ";
                string cmdstring2 = "select count(transNo) as e from T_EzonTahwel where  (Sign4 is not null)   ";

                SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);


                Count1 = (Int32)cmd2.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + " اذون تحويل تحتاج متابعة من قبل مدير قطاع المخازن");
                    //  label2.Text = label2.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }
            }








            /////////////////////////////////////////////////////////
            if (Constants.User_Type == "B" && Constants.UserTypeB == "TechnicalFollowUp")
            {
                string cmdstring = "select Count(TalbTwareed_No) as e from T_TalbTawreed where [Req_Signature] is not null and (Confirm_Sign1 is not null) and (Confirm_Sign2 is not null)  and Sign8 is not null and(Stock_Sign is not null) and Sign11 is not null and Sign9 is null";

                SqlCommand cmd = new SqlCommand(cmdstring, Constants.con);


                Count1 = (Int32)cmd.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + "  طلبات توريد تحتاج الى متابعة");
                    // label4.Text = "عدد المطابقات الفنية المعلقة:" + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    //   label4.Text = "عدد المطابقات الفنية المعلقة:0";
                }
            }
            if (Constants.User_Type == "B" && Constants.UserTypeB == "Edafa")
            {
                string cmdstring4 = "select count(distinct Edafa_No) as e from T_Edafa where (( Sign2 is null)) and   (Sign4 is not null) ";
                SqlCommand cmd4 = new SqlCommand(cmdstring4, Constants.con);


                Count1 = (Int32)cmd4.ExecuteScalar();
                if (Count1 > 0)
                {
                     MessageBox.Show("يوجد عدد " + Count1.ToString() + " يوجد اضافة مخزنية تمت المطابقة من قبل الادارة الطالبة");
                    // label3.Text = label3.Text + " " + Count1.ToString();


                     cmdstring4 = "select (Edafa_No)  from T_Edafa where (( Sign2 is null)) and   (Sign4 is not null) ";
                     cmd4 = new SqlCommand(cmdstring4, Constants.con);

                     SqlDataReader dr = cmd4.ExecuteReader();
                     //---------------------------------
                     if (dr.HasRows == true)
                     {
                         while (dr.Read())
                         {
                             MessageBox.Show("يوجد اضافة مخزنية رقم  " + dr["Edafa_No"].ToString() + " تمت المطابقة الفنية بنجاح");
                             // label3.Text = label3.Text + " " + Count1.ToString();

                         }
                     }
                     dr.Close();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }




                //////////////////////

                cmdstring4 = "select count(distinct Edafa_No) as e from T_Edafa where (( Sign3 is null)) and   (Sign2 is not null) ";
                 cmd4 = new SqlCommand(cmdstring4, Constants.con);


                Count1 = (Int32)cmd4.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + " يوجد اضافة مخزنية تحتاج توقيع مدير عام م المخازن");
                    // label3.Text = label3.Text + " " + Count1.ToString();

                    cmdstring4 = "select (Edafa_No)  from T_Edafa where (( Sign3 is null)) and   (Sign2 is not null) ";
                    cmd4 = new SqlCommand(cmdstring4, Constants.con);

                    SqlDataReader dr = cmd4.ExecuteReader();
                    //---------------------------------
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            MessageBox.Show("يوجد اضافة مخزنية رقم  " +dr["Edafa_No"].ToString()+ "  تحتاج توقيع مدير عام م المخازن");
                            // label3.Text = label3.Text + " " + Count1.ToString();

                        }
                    }
                    dr.Close();

                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }







                ///////////////////////
            }
            //////////////////////////////////////////////

            if (Constants.User_Type == "B" && Constants.UserTypeB == "Sarf")
            {


                string cmdstring3 = "select Count(EznSarf_No) as e from T_EznSarf where  (Sign3 is null ) and (Sign1 is not null) and ( Sign2 is not null) ";
                SqlCommand cmd3 = new SqlCommand(cmdstring3, Constants.con);


                Count1 = (Int32)cmd3.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + " طلبات اذن صرف تحتاج الى توقيع امين المخزن");
                    // label3.Text = label3.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }
            }
            if (Constants.User_Type == "B" && Constants.UserTypeB == "Stock")
            {


                string cmdstring2 = "select Count(TalbTwareed_No) as e from T_TalbTawreed where  (Confirm_Sign1 is not null)  and (Confirm_Sign2 is not null )and(Stock_Sign is null or Audit_Sign is null or Mohmat_Sign is null or CH_Sign is null) ";

                SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);


                Count1 = (Int32)cmd2.ExecuteScalar();
                if (Count1 > 0)
                {
                    //MessageBox.Show("يوجد عدد " + Count1.ToString() + " طلبات توريد تحتاج الى متابعة");
                    //  label2.Text = label2.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }




                ///////////////////////////////



                ///////////////////////////////////////////////

                string cmdstring4 = "select count(distinct Edafa_No) as e from T_Edafa where  (Sign3 is null ) and   (Sign4 is not null) ";
                SqlCommand cmd4 = new SqlCommand(cmdstring4, Constants.con);


                Count1 = (Int32)cmd4.ExecuteScalar();
                if (Count1 > 0)
                {
                   // MessageBox.Show("يوجد عدد " + Count1.ToString() + " يوجد اضافة مخزنية تمت المطابقة من قبل الادارة الطالبة");
                    // label3.Text = label3.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }
            }
            /////////////////////////////////////////////////////////////////////
            if (Constants.User_Type == "B" && Constants.UserTypeB == "Stock")
            {

                string cmdstring5 = "SELECT count(*) FROM [T_Estlam] where Sign1 is not null and (Sign2 is null or Sign3 is null)";
                SqlCommand cmd5 = new SqlCommand(cmdstring5, Constants.con);


                Count1 = (Int32)cmd5.ExecuteScalar();
                if (Count1 > 0)
                {
                   // MessageBox.Show("يوجد عدد " + Count1.ToString() + "طلبات استلام تحتاج الى توقيع مدير مخزن الاستلام /مدير عام م المخازن");
                    // label3.Text = label3.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }

                cmdstring5 = "select Count(Amrshraa_No) as e from T_Awamershraa where   ( Sign12 is  null) ";
                 cmd5 = new SqlCommand(cmdstring5, Constants.con);

                Count1 = (Int32)cmd5.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + "اوامر شراء تحتاج متابعة من قبل  المسئول عن تصديق اوامر الشراء فى المهمات");
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }


                //////////////////////////////////////////////
                cmdstring5 = "select Count(Amrshraa_No) as e from T_Awamershraa where   ( Sign12 is not  null) and ( Sign13 is  null)  ";
                cmd5 = new SqlCommand(cmdstring5, Constants.con);

                Count1 = (Int32)cmd5.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + "اوامر شراء تحتاج متابعة من قبل  مدير عام مساعد المهمات");
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }
                ///////////////////////////////////////////////////////////
                cmdstring5 = "select Count(Amrshraa_No) as e from T_Awamershraa where  Sign13 is not null and Sign14 is  null ";
                cmd5 = new SqlCommand(cmdstring5, Constants.con);

                Count1 = (Int32)cmd5.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + "اوامر شراء تحتاج متابعة من قبل  مدير عام المهمات");
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }
                ////////////////////////////////////////////////////
            }

            if (Constants.User_Type == "B" && Constants.UserTypeB == "Finance")
            {

                string cmdstring5 = "select Count(Amrshraa_No) as e from T_Awamershraa where   ( Sign3 is not null) ";
                SqlCommand cmd5 = new SqlCommand(cmdstring5, Constants.con);


                Count1 = (Int32)cmd5.ExecuteScalar();
                if (Count1 > 0)
                {
                   // MessageBox.Show("يوجد عدد " + Count1.ToString() + "اوامر شراء تحتاج متابعة من قبل الحسابات");
                    // label3.Text = label3.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }

                //////////////////////////////////////////////////
                 cmdstring5 = "select Count(Amrshraa_No) as e from T_Awamershraa where   ( Sign3 is not null)";
                 cmd5 = new SqlCommand(cmdstring5, Constants.con);


                Count1 = (Int32)cmd5.ExecuteScalar();
                if (Count1 > 0)
                {
                 //   MessageBox.Show("يوجد عدد " + Count1.ToString() + "اوامر شراء تحتاج متابعة من قبل الحسابات");
                    // label3.Text = label3.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }

                cmdstring5 = "select  count(transNo) as e from T_EzonTahwel where   ( Sign5 is not null)";
                cmd5 = new SqlCommand(cmdstring5, Constants.con);


                Count1 = (Int32)cmd5.ExecuteScalar();
                if (Count1 > 0)
                {
                      MessageBox.Show("يوجد عدد " + Count1.ToString() + "اذون تحويل تحتاج متابعة من قبل الحسابات");
                    // label3.Text = label3.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }
            }

            if (Constants.User_Type == "B" && Constants.UserTypeB == "Mwazna")
            {

              //  string cmdstring5 = "select Count(Amrshraa_No) as e from T_Awamershraa where  (Sign2 is null ) and  ( Sign1 is not null) ";
                string cmdstring2 = "select Count(TalbTwareed_No) as e from T_TalbTawreed where  (Confirm_Sign1 is not null)  and (Confirm_Sign2 is not null ) and (Sign8 is not null) and(Stock_Sign is null or Sign11 is null ) ";

                SqlCommand cmd2 = new SqlCommand(cmdstring2, Constants.con);


                Count1 = (Int32)cmd2.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + " طلبات توريد تحتاج الى متابعة من قبل ادارة الموازنة");
                    //  label2.Text = label2.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }
            }
            if (Constants.User_Type == "B" && Constants.UserTypeB == "Chairman")
            {

                string cmdstring5 = "select Count(TalbTwareed_No) as e from T_TalbTawreed where  (Confirm_Sign1 is not null)  and (Confirm_Sign2 is not null ) and (Sign8 is not null) and(Stock_Sign is not null) and( Sign11 is  not null ) and Sign9 is not null and(CH_Sign is null) ";

                
                SqlCommand cmd5 = new SqlCommand(cmdstring5, Constants.con);


                Count1 = (Int32)cmd5.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + "طلبات توريد تحتاج متابعة من رئيس مجلس الأدارة و العضو المنتدب");
                    // label3.Text = label3.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }

                //////////////////////////////////////////////////////////طلبات توريد///////////////////////////

                cmdstring5 = "select Count(Amrshraa_No) as e from T_Awamershraa where  (Sign33 is null ) and  ( Sign3 is not null)  ";
                 cmd5 = new SqlCommand(cmdstring5, Constants.con);


                Count1 = (Int32)cmd5.ExecuteScalar();
                if (Count1 > 0)
                {
                  //  MessageBox.Show("يوجد عدد " + Count1.ToString() + "اوامر شراء تحتاج متابعة من  رئيس مجلس الأدارة و العضو المنتدب");
                    // label3.Text = label3.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }





                //////////////////////////////////////////////////////////امر الشراء///////////////////////////



            }
            if (Constants.User_Type == "B" && Constants.UserTypeB == "ViceChairman")
            {


                /////////////////////////////////////////////////////////
                string cmdstring5 = "select Count(TalbTwareed_No) as e from T_TalbTawreed where  (Confirm_Sign1 is not null)  and (Confirm_Sign2 is not null ) and (Sign8 is not null) and(Stock_Sign is not null) and( Sign11 is  not null ) and (Sign13 is null) ";


                SqlCommand cmd5 = new SqlCommand(cmdstring5, Constants.con);


                Count1 = (Int32)cmd5.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + "طلبات توريد تحتاج متابعة من مساعد رئيس الشركة");
                    // label3.Text = label3.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }


                ////////////////////////////////////////////////////////
              cmdstring5 = "select Count(Amrshraa_No) as e from T_Awamershraa where  (Sign33 is null ) and  ( Sign3 is not null) ";
               cmd5 = new SqlCommand(cmdstring5, Constants.con);


                Count1 = (Int32)cmd5.ExecuteScalar();
                if (Count1 > 0)
                {
                   // MessageBox.Show("يوجد عدد " + Count1.ToString() + "اوامر شراء تحتاج متابعة من  مساعد رئيس الشركة");
                    // label3.Text = label3.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }

            }
            if (Constants.User_Type == "B" && Constants.UserTypeB == "Purchases")
            {
             

                string cmdstring5 = "select Count(TalbTwareed_No) as e from T_TalbTawreed where  (Confirm_Sign1 is not null)  and (Confirm_Sign2 is not null ) and (Sign8 is not null) and(Stock_Sign is not null) and( Sign11 is  not null ) and Sign9 is not null and(CH_Sign is not null) and Audit_Sign is null ";


                SqlCommand cmd5 = new SqlCommand(cmdstring5, Constants.con);


                Count1 = (Int32)cmd5.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + "طلبات توريد تحتاج متابعة من قبل قطاع المشتريات");
                    // label3.Text = label3.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }

                ////////////////


            }
            ///////////////////////////////




            if (Constants.User_Type == "B" && Constants.UserTypeB == "GMInventory")
            {


                string cmdstring5 = "select Count(TalbTwareed_No) as e from T_TalbTawreed where  (Confirm_Sign1 is not null)  and (Confirm_Sign2 is not null ) and (Sign8 is not null) and(Stock_Sign is not null) and( Sign11 is  not null ) and Sign9 is not null and(CH_Sign is not null) and Audit_Sign is not null  and Mohmat_Sign is null ";


                SqlCommand cmd5 = new SqlCommand(cmdstring5, Constants.con);


                Count1 = (Int32)cmd5.ExecuteScalar();
                if (Count1 > 0)
                {
                    MessageBox.Show("يوجد عدد " + Count1.ToString() + "طلبات توريد تحتاج متابعة من قبل مدير عام المهمات");
                    // label3.Text = label3.Text + " " + Count1.ToString();
                }
                else if (Count1 == 0)
                {
                    // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                }
            }
                ////////////////

                if (Constants.User_Type == "B" && Constants.UserTypeB == "Estlam")
                {


                  string   cmdstring5 = "select  ( Amrshraa_No),DATE,Sign3 ,SIGN2 from T_Estlam group by date,Amrshraa_No,AmrSheraa_sanamalia ,Sign2,Sign3 HAVING  Sign2 IS NULL";
                     SqlCommand cmd5 = new SqlCommand(cmdstring5, Constants.con);


                       SqlDataReader dr = cmd5.ExecuteReader();

                              if (dr.HasRows == true)
                              {
                                  while (dr.Read())
                                  {

                                      MessageBox.Show(" يوجد استلام لامر الشراء" + dr["Amrshraa_No"].ToString() + "بتاريخ " + dr["Date"].ToString() + " يحتاج الى مراجعة من مدير مخزن الاستلام");
                                          // label4.Text
                    
                                  }
                              }

                    //////////////////////////////////////
                            cmdstring5 = "select  ( Amrshraa_No),DATE,Sign3 ,SIGN2 from T_Estlam group by date,Amrshraa_No,AmrSheraa_sanamalia ,Sign2,Sign3 HAVING Sign3 IS NULL  and Sign2 is not null ";
                           cmd5 = new SqlCommand(cmdstring5, Constants.con);


                            dr = cmd5.ExecuteReader();

                              if (dr.HasRows == true)
                              {
                                  while (dr.Read())
                                  {

                                      MessageBox.Show(" يوجد استلام لامر الشراء" + dr["Amrshraa_No"].ToString() + "بتاريخ " + dr["Date"].ToString() + "يحتاج الى مراجعة من مدير عام مساعد المخازن ");
                                      // label4.Text

                                  }
                              }

                    ////////////////////////////////

                              cmdstring5 = "select  count(transNo) as e from T_EzonTahwel where   ( Sign3 is not null) and sign4 is null";
                            cmd5 = new SqlCommand(cmdstring5, Constants.con);


                              Count1 = (Int32)cmd5.ExecuteScalar();
                              if (Count1 > 0)
                              {
                                  MessageBox.Show("يوجد عدد " + Count1.ToString() + "اذون تحويل تحتاج متابعة من قبل مخزن الاستلام");
                                  // label3.Text = label3.Text + " " + Count1.ToString();
                              }
                              else if (Count1 == 0)
                              {
                                  // label2.Text = "عدد المطابقات الفنية المعلقة:0";
                              }


                    /////////////////////////////////////////////
                


            }
            ///////////////////////////////////////////////
            Constants.closecon();
            //----------------

        }
        private void FPublic_Load(object sender, EventArgs e)
        {
            GetProblemsCount();
            Constants.opencon();
            string query = "select * from UsersPrivilages where UserName = @a ";
            SqlCommand cmd = new SqlCommand(query, Constants.con);
            cmd.Parameters.AddWithValue("@a", Constants.User_Name);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {

                    //  TXT_username.Text = dr["UserName"].ToString();
                    button1.Enabled = (bool)dr["F11"];
                    button2.Enabled = (bool)dr["F12"];
                    button3.Enabled = (bool)dr["F13"];
                    button4.Enabled = (bool)dr["F14"];
                    button5.Enabled = (bool)dr["F15"];
                    button6.Enabled = (bool)dr["F16"];
                    button8.Enabled = (bool)dr["F17"];
                    button7.Enabled = (bool)dr["F18"];
                    button9.Enabled = (bool)dr["F19"];
                    button12.Enabled = (bool)dr["F21"];
                    button13.Enabled = (bool)dr["F22"];
                    Constants.AdminUserFlag = (bool)dr["F20"];
                }
            }
            Constants.closecon();

            if(Constants.User_Name=="User1_Stock"){
//button10.Visible = true;
                BTN_Redirect.Visible = true;
            }
            if (Constants.User_Name == "User1_Inventory")
            {
                button10.Visible = true;
              //  BTN_Redirect.Visible = true;
            }
            if (Constants.User_Name == "User1_InventoryControl")
            {
                button11.Visible = true;
            }

            //label1.Text = Constants.NameEdara;
        }
        //==============================================================
        // Dabt El tasnifat
        //--------------------
        private void button1_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            Tasnif F = new Tasnif();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
            //this.Close();
        }
        //-----------------------------
        // Beta2et 7arket sanf
        //--------------------
        private void button2_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            TasnifTrans F = new TasnifTrans();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
        }

        //------------------------------------------------
        // AmrSheraa
        //-------------
        private void button3_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            Constants.Amrshera_F = true;
            AmrSheraa F = new AmrSheraa();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
        }
        //------------------------------------------------
        // Motab3a  AmrSheraa
        //-------------
        private void button4_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            Constants.Amrshera_F = false;
            AmrSheraa F = new AmrSheraa();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
        }

        //----------------------------------------
        //Edafa Makhzania
        //-----------------
        private void button7_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            FEdafaMakhzania_F F = new FEdafaMakhzania_F();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
        }
        //-----------------------------------------
        // Estlam
        //----------
        private void button8_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            Constants.Estlam_F = true;
            Estlam_F F = new Estlam_F();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
        }
 
        //------------------------------------------
        // Talbat el Tawreed Lel Edara 
        //-----------------------------
        private void button9_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
           FUsers F = new FUsers();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
            //this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            Constants.EznSarf_FF = false;
            EznSarf_F F = new EznSarf_F();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            Constants.talbtawred_F = false;
            //----------------------
            TalbTawred F = new TalbTawred();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Constants.Minimize_Btn(this);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            InsertTasnifTrans F = new InsertTasnifTrans();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            InsertTasnifTrans F = new InsertTasnifTrans();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            FInventoryControl F = new FInventoryControl();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            BTN_Redirect.Visible = false;
        }

        private void BTN_Redirect_Click(object sender, EventArgs e)
        {
           string Empn = Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل رقم القيد الخاص بك", "توقيع مدير عام المهمات", "");

           string Sign= Microsoft.VisualBasic.Interaction.InputBox("من فضلك ادخل الرقم السرى الخاص بك", "توقيع مدير عام المهمات", "");
          int  FlagEmpn = 0;

            if (Sign != "" && Empn != "")
            {
                //  MessageBox.Show("done");
                // string result= Constants.CheckSign("1",Sign1);
                Tuple<string, int, int, string, string> result = Constants.CheckSign("1", "6", Sign, Empn);
                if (result.Item3 == 1)
                {
                    Constants.FlagRedirectEmpn = Empn;
                    if (Constants.currentOpened != null)
                    {
                        Constants.currentOpened.Close();
                    }
                    Constants.talbtawred_F = false;
                    //----------------------
                    TalbTawred F = new TalbTawred();
                    Constants.currentOpened = F;
                    Constants.RedirectedFlag = 1;
                    F.Show();
                    this.IsMdiContainer = true;
                    F.MdiParent = this;
                    F.Dock = DockStyle.Fill;
                    tableLayoutPanel1.Visible = false;
                    button11.Visible = false;
                    button10.Visible = false;
                    BTN_Redirect.Visible = false;
                    //Pic_Sign.Image = Image.FromFile(@result.Item1);

               //     FlagSign = result.Item2;
               //     FlagEmpn = Empn;
                }
                else
                {
                   //not found
                }
                // result.Item1;
                // result.Item2;


            }
            else
            {
                //cancel
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            Constants.Amrshera_F = true;
            FTransfer_M F = new FTransfer_M();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            Constants.EzonTahwel_FF = false;
            FTransfer_AA F = new FTransfer_AA();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            Constants.EzonTahwel_FF = true;
            FTransfer_AA F = new FTransfer_AA();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (Constants.currentOpened != null)
            {
                Constants.currentOpened.Close();
            }
            //----------------------
            Constants.Estlam_F = false;
            Estlam_F F = new Estlam_F();
            Constants.currentOpened = F;
            F.Show();
            this.IsMdiContainer = true;
            F.MdiParent = this;
            F.Dock = DockStyle.Fill;
            tableLayoutPanel1.Visible = false;
            button11.Visible = false;
            button10.Visible = false;
            BTN_Redirect.Visible = false;
        }
    }
}
