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

namespace ANRPC_Inventory
{
    public partial class TimeLineDrawerForm : Form
    {
        List<TimeLineCircleDetails> list = new List<TimeLineCircleDetails>();
        private DataTable dtTalabTawreed = new DataTable();

        private void queryData()
        {
            string codeEdara = "850";
            int TalbTwareed_No = 8;
            string FYear = "2022_2023";
            string formNo = "1";
            SqlConnection sqlConnction = new SqlConnection(Constants.constring);
            SqlDataAdapter daTalabTawreed = new SqlDataAdapter(@"select *,cast(iif(Date2 is NULL ,0,1) as bit) as isDone, 
                                                                FORMAT([Date2], 'd MMM', 'en-US') as signDate, DATEDIFF(day, 
                                                                [Date1], [Date2]) AS Duration FROM T_SignaturesDates 
                                                                where TalbTwareed_No = " + TalbTwareed_No + " and FormNo=" + formNo + " AND FYear='" + FYear + "'", sqlConnction);


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

            return signatureDictionary[formType - 1][signType - 1];
        }

        private string getSignatureDescription(int formNo, int SignNo)
        {
            return getCurrentListOfSignaturesDescription(formNo, SignNo);
        }

        private TimeLineCircleDetails circleDetailsFiller(DataRow row, bool isLastDone)
        {
            TimeLineCircleDetails details = new TimeLineCircleDetails();

            int formNo, signNo;

            formNo = Convert.ToInt32(row["FormNo"]);
            signNo = Convert.ToInt32(row["SignatureNo"]);

            details.isDone = Convert.ToBoolean(row["isDone"]);
            details.mainText = new DrawedCircleText(Convert.ToString(row["signDate"]), new Font("Calibri", 16, FontStyle.Bold));
            details.circleDetailsText = new DrawedCircleText(getSignatureDescription(formNo, signNo), new Font("Calibri", 14, FontStyle.Bold));
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

        private void HandleTimeLineView()
        {
            //queryData
            queryData();
            for (int i = 0; i < dtTalabTawreed.Rows.Count; i++)
            {
                bool isLastDone = false;

                if (i + 1 < dtTalabTawreed.Rows.Count && Convert.ToBoolean(dtTalabTawreed.Rows[i]["isDone"]) && !Convert.ToBoolean(dtTalabTawreed.Rows[i + 1]["isDone"]))
                {
                    isLastDone = true;
                }

                TimeLineCircleDetails details = circleDetailsFiller(dtTalabTawreed.Rows[i], isLastDone);


                list.Add(details);
            }
        }

        public TimeLineDrawerForm()
        {
            InitializeComponent();
            HandleTimeLineView();
        }


        private void formWraper_Paint(object sender, PaintEventArgs e)
        {
            TimeLine timeLineGraph = new TimeLine(e, formWraper.Width, list);
            timeLineGraph.DarwSequance(offsetX: 60);
        }
    }
}
