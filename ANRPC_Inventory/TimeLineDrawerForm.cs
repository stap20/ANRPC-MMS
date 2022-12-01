using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
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
        int totalDone, totalCircles;

        enum CircleType
        {
            NORMAL,
            START,
            LAST,
            LASTDONE
        }

        private void queryData()
        {
            string codeEdara = "850";
            int TalbTwareed_No = 8;
            string FYear = "2022_2023";
            string formNo = "1";
            SqlConnection sqlConnction = new SqlConnection(Constants.constring);
            SqlDataAdapter daTalabTawreed = new SqlDataAdapter(@"select *, 
                                                                  cast(
                                                                    iif(Date2 is NULL, 0, 1) as bit
                                                                  ) as isDone, 
                                                                  FORMAT([Date2], 'd MMM', 'en-US') as signDate, 
                                                                  iif(
                                                                    [Date1] is not null 
                                                                    and [Date2] is not null, 
                                                                    DATEDIFF(day, [Date1], [Date2]), 
                                                                    iif(
                                                                      [Date1] is not null 
                                                                      and [Date2] is null, 
                                                                      DATEDIFF(day, [Date1], GETDATE()), 
                                                                      -1
                                                                    )
                                                                  ) AS Duration 
                                                                FROM 
                                                                  T_SignaturesDates 
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
            signatureDictionary[0].Add("مدير إدارة التصنيفات");
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

        private TimeLineCircleDetails circleDetailsFiller(DataRow row, CircleType type)
        {
            #region temp variables for selection
            int formNo, signNo;

            (int, int) symbolOffset;

            string symbol;

            Color mainTextColor, detailsTextColor, symbolColor, circleBackColor, circleColor;
            Font textFont, symbolFont;
            #endregion

            TimeLineCircleDetails details = new TimeLineCircleDetails();

            symbol = "";

            formNo = Convert.ToInt32(row["FormNo"]);
            signNo = Convert.ToInt32(row["SignatureNo"]);

            details.isDone = Convert.ToBoolean(row["isDone"]);
            details.donePercent = 0;

            details.duration = Convert.ToInt32(row["Duration"]);

            symbolOffset = (0, 0);
            circleColor = Color.FromArgb(255, 255, 255);
            PrivateFontCollection f = new PrivateFontCollection();
            f.AddFontFile("fa-solid-900.ttf");

            if (details.isDone)
            {
                details.donePercent = 100;

                circleBackColor = Color.FromArgb(53, 178, 136);

                mainTextColor = Color.FromArgb(53, 178, 136);
                detailsTextColor = Color.FromArgb(84, 84, 84);
                textFont = new Font("Calibri", 13, FontStyle.Bold);

                symbolFont = new Font(f.Families[0], 15);
                symbolColor = Color.FromArgb(53, 178, 136);
                totalDone++;
            }
            else
            {
                circleBackColor = Color.FromArgb(233, 241, 252);

                mainTextColor = Color.FromArgb(204, 204, 204);
                detailsTextColor = Color.FromArgb(204, 204, 204);
                textFont = new Font("Calibri", 13, FontStyle.Bold);

                symbolFont = new Font(f.Families[0], 15);
                symbolColor = Color.FromArgb(188, 215, 246);
            }


            if (type == CircleType.START)
            {
                symbol = "";
                symbolFont = new Font(f.Families[0], 13);
                symbolColor = Color.FromArgb(53, 178, 136);
                symbolOffset = (1, 1);
            }
            else if(type == CircleType.LAST)
            {
                symbol = Convert.ToString (Convert.ToInt32((totalDone/(totalCircles*1.0))*100)) + "%";
                symbolFont = new Font("Calibri", 11, FontStyle.Bold);
                symbolColor = Color.FromArgb(53, 178, 136);
                symbolOffset = (1, 1);
            }
            else if(type == CircleType.LASTDONE)
            {
                details.donePercent = 70;

                symbol = "";
                symbolColor = Color.FromArgb(53, 178, 136);
                symbolOffset = (1, -1);
            }
            else if(type == CircleType.NORMAL)
            {
                symbol = "";
                symbolOffset = (1, 1);
            }


            details.mainText = new DrawedCircleText(Convert.ToString(row["signDate"]), textFont, mainTextColor);
            details.circleDetailsText = new DrawedCircleText(getSignatureDescription(formNo, signNo), textFont, detailsTextColor);
            details.circleSymbol = new CircleSymbol(symbol, symbolFont, symbolColor,symbolOffset);
            details.circleStyle = new CircleStyle(circleBackColor,circleColor);

            return details;
        }

        private void HandleTimeLineView()
        {
            //queryData
            queryData();
            totalCircles = dtTalabTawreed.Rows.Count;
            totalDone = 0;
            for (int i = 0; i < dtTalabTawreed.Rows.Count; i++)
            {
                CircleType type;

                if (i == 0)
                {
                    type = CircleType.START;
                }
                else if(i+1 == dtTalabTawreed.Rows.Count)
                {
                    type = CircleType.LAST;
                }
                else if (i + 1 < dtTalabTawreed.Rows.Count && Convert.ToBoolean(dtTalabTawreed.Rows[i]["isDone"]) && !Convert.ToBoolean(dtTalabTawreed.Rows[i + 1]["isDone"]))
                {
                    type = CircleType.LASTDONE;
                }
                else
                {
                    type = CircleType.NORMAL;
                }

                TimeLineCircleDetails details = circleDetailsFiller(dtTalabTawreed.Rows[i], type);


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
            timeLineGraph.DarwSequance(offsetX: 60,isRL:true);
        }
    }
}
