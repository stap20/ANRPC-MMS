using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Guna.UI2.WinForms;

namespace ANRPC_Inventory
{
    public partial class TimeLine_Form : Form
    {
        //Fields
        private IconButton currentBtn;
        private Panel BottomBorderBtn;
        private Form currentChildForm;

        List<TimeLineCircleDetails> list = new List<TimeLineCircleDetails>();



        private DataTable dtTalabTawreed = new DataTable();
        private void queryData()
        {
            string codeEdara = "850";
            int TalbTwareed_No =  8 ;
            string FYear = "2022_2023";
            string formNo = "1";
            SqlConnection sqlConnction = new SqlConnection(Constants.constring);
            SqlDataAdapter daTalabTawreed = new SqlDataAdapter(@"select *,cast(iif(Date2 is NULL ,0,1) as bit) as isDone, 
                                                                FORMAT([Date2], 'd MMM', 'en-US') as signDate, DATEDIFF(day, 
                                                                [Date1], [Date2]) AS Duration FROM T_SignaturesDates 
                                                                where TalbTwareed_No = " + TalbTwareed_No + " and FormNo=" + formNo + " AND FYear='" + FYear +"'", sqlConnction);


            sqlConnction.Open();
            daTalabTawreed.Fill(dtTalabTawreed);


            sqlConnction.Close();
        }


        private string getCurrentListOfSignaturesDescription(int formType, int signType)
        {
            Dictionary<int, List<string>> signatureDictionary = new Dictionary<int, List<string>>();

            signatureDictionary.Add(0, new List<string>());
            signatureDictionary.Add(1, new List<string>());

            signatureDictionary[0].Add("إعداد الطلب");
            signatureDictionary[0].Add("التصديق");
            signatureDictionary[0].Add("الإعتماد");
            signatureDictionary[0].Add("الموازنة 1");
            signatureDictionary[0].Add("مدير قطاع المشتريات");
            signatureDictionary[0].Add("مدير عام المهمات");
            signatureDictionary[0].Add("إعتماد رئيس مجلس الإدارة");
            signatureDictionary[0].Add("إدارة التصنيفات");
            signatureDictionary[0].Add("المتابعة الفنية");

            signatureDictionary[0].Add("");

            signatureDictionary[0].Add("الموازنة 2");
            signatureDictionary[0].Add("مراقبة المخزون");
            signatureDictionary[0].Add("");

            return signatureDictionary[formType-1][signType-1];
        }

        private string getSignatureDescription(int formNo, int SignNo)
        {
            return getCurrentListOfSignaturesDescription(formNo,SignNo);
        }

        private TimeLineCircleDetails circleDetailsFiller(DataRow row,bool isLastDone)
        {
            TimeLineCircleDetails details = new TimeLineCircleDetails();

            int formNo, signNo;

            formNo = Convert.ToInt32(row["FormNo"]);
            signNo = Convert.ToInt32(row["SignatureNo"]);

            details.isDone = Convert.ToBoolean(row["isDone"]);
            //details.mainText = new DrawedCircleText(Convert.ToString(row["signDate"]), new Font("Arial", 16, FontStyle.Bold));
            //details.circleDetailsText = new DrawedCircleText(getSignatureDescription(formNo, signNo), new Font("Arial", 10, FontStyle.Bold));
            details.donePercent = 0;

            if (details.isDone)
            {
                details.donePercent = 100;
            }

            if (isLastDone)
            {
                details.donePercent = 45;
            }

            return details;
        }

        private void prepareBeforeFormLoad()
        {
            BottomBorderBtn = new Panel();
            BottomBorderBtn.Size = new Size(143, 4);
            guna2GradientPanel1.Controls.Add(BottomBorderBtn);



            //queryData
            queryData();
            for (int i= 0;i < dtTalabTawreed.Rows.Count ;i++)
            {
                bool isLastDone = false;

                if (i+1 < dtTalabTawreed.Rows.Count && Convert.ToBoolean(dtTalabTawreed.Rows[i]["isDone"]) && !Convert.ToBoolean(dtTalabTawreed.Rows[i + 1]["isDone"]))
                {
                    isLastDone = true;
                }

                TimeLineCircleDetails details = circleDetailsFiller(dtTalabTawreed.Rows[i], isLastDone);


                list.Add(details);
            }
        }

        public TimeLine_Form()
        {       
            InitializeComponent();
            prepareBeforeFormLoad();
        }

        //Structs
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(226, 133, 222);
            public static Color color2 = Color.FromArgb(120, 77, 253);
            
        }
  
        private void ActivateButton(object senderBtn, Color color)
        {

            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(43, 19, 114);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Buttom border button
                BottomBorderBtn.BackColor = color;
                BottomBorderBtn.Location = new Point(currentBtn.Location.X,currentBtn.Location.Y+currentBtn.Size.Height - BottomBorderBtn.Size.Height);
                BottomBorderBtn.Visible = true;
                BottomBorderBtn.BringToFront();
            }
        }
 
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.Transparent;
                currentBtn.ForeColor = Color.FromArgb(155, 170, 192);
                //currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.FromArgb(155, 170, 192);
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                //currentBtn.FlatAppearance.MouseDownBackColor = Color.Black ;
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            Cycledetails_Form details = new Cycledetails_Form();
            openChildForm(details);

           // guna2GradientPanel6.Visible = true;

        }

        private void btnTimeline_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();

            }

            ActivateButton(sender, RGBColors.color2);
            //Label_hoba();
        }
















        private void guna2GradientPanel6_Paint(object sender, PaintEventArgs e)
        {
            TimeLine timeLineGraph = new TimeLine(e, guna2GradientPanel6.Width, list);
            timeLineGraph.DarwSequance(offsetX:60);
        }

        private void openChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            guna2GradientPanel6.Controls.Add(childForm);
            guna2GradientPanel6.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

            currentChildForm = childForm;
        }

        private void TimeLine_Form_Load(object sender, EventArgs e)
        {
            btnTimeline_Click(btnTimeline, e);
        }
 
        private void Label_hoba()
        {
            Label hoba = new Label();
            hoba.BackColor = System.Drawing.Color.Transparent;
            hoba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            hoba.Font = new System.Drawing.Font("Microsoft Sans Serif",15F);
            hoba.ForeColor = System.Drawing.Color.FromArgb(164, 163, 203);
            hoba.Location = new System.Drawing.Point(21, 15);
            hoba.Size = new System.Drawing.Size(53, 40);
            hoba.Padding = new System.Windows.Forms.Padding(0, 5, 5, 0);
            hoba.Dock = DockStyle.Top;
            hoba.Text = "TimeLine";

            guna2GradientPanel6.Controls.Add(hoba);
        }

    }
}
