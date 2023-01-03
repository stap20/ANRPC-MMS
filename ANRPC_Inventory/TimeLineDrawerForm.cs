using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;
using static ANRPC_Inventory.SearchForm;

namespace ANRPC_Inventory
{
    public partial class TimeLineDrawerForm : Form
    {
        Dictionary<(int,bool), Dictionary<int,string>> signatureDictionary = new Dictionary<(int, bool), Dictionary<int, string>>();
        List<TimeLineCircleDetails> list = new List<TimeLineCircleDetails>();
        private DataTable dtTalabTawreed = new DataTable();
        int totalDone, totalCircles;

        enum CircleType
        {
            NORMAL,
            START,
            LAST,
            LASTDONE,
            LASTINPROGRESS,
        }

        private void prepareSignatureDicts()
        {
            Dictionary<int, string> cycle;

            #region TALB_TAWREED
            cycle = new Dictionary<int, string>();
            cycle[1] = "إعداد الطلب";
            cycle[2] = "التصديق";
            cycle[3] = "إعتماد مدير عام الادارة الطالبة";
            cycle[4] = "مدير إدارة التصنيفات";
            cycle[5] = "مراقبة المخزون";
            cycle[6] = "الموازنة 1";
            cycle[7] = "الموازنة 2";
            cycle[8] = "مدير قطاع المشتريات";
            cycle[9] = "المتابعة الفنية";
            cycle[10] = "إعتماد رئيس مجلس الإدارة";
            cycle[11] = "مدير عام المهمات";

            signatureDictionary[(1,false)] = cycle;
            #endregion

            #region TALB_TAWREED_FOREIGN
            cycle = new Dictionary<int, string>();
            cycle[1] = "إعداد الطلب";
            cycle[2] = "التصديق";
            cycle[3] = "إعتماد مدير عام الادارة الطالبة";
            cycle[4] = "مدير إدارة التصنيفات";
            cycle[5] = "مراقبة المخزون";
            cycle[6] = "الموازنة 1";
            cycle[7] = "الموازنة 2";
            cycle[8] = "مدير قطاع المشتريات";
            cycle[9] = "المتابعة الفنية";
            cycle[10] = "إعتماد رئيس مجلس الإدارة";
            cycle[11] = "مدير عام المهمات";

            signatureDictionary[(1,true)] = cycle;
            #endregion

            #region TALB_ESLAH
            cycle = new Dictionary<int, string>();
            cycle[1] = "إعداد الطلب";
            cycle[2] = "التصديق";
            cycle[3] = "إعتماد مدير عام الادارة الطالبة";
            cycle[4] = "الموازنة 1";
            cycle[5] = "الموازنة 2";
            cycle[6] = "مدير قطاع المشتريات";
            cycle[7] = "المتابعة الفنية";
            cycle[8] = "إعتماد رئيس مجلس الإدارة";
            cycle[9] = "مدير عام المهمات";

            signatureDictionary[(8, false)] = cycle;
            #endregion

            #region TALB_TANFIZ
            cycle = new Dictionary<int, string>();
            cycle[1] = "إعداد الطلب";
            cycle[2] = "التصديق";
            cycle[3] = "إعتماد مدير عام الادارة الطالبة";
            cycle[4] = "الموازنة 1";
            cycle[5] = "الموازنة 2";
            cycle[6] = "مدير قطاع المشتريات";
            cycle[7] = "المتابعة الفنية";
            cycle[8] = "إعتماد رئيس مجلس الإدارة";
            cycle[9] = "مدير عام المهمات";

            signatureDictionary[(10, false)] = cycle;
            #endregion

            #region TALB_MOAYRA
            cycle = new Dictionary<int, string>();
            cycle[1] = "إعداد الطلب";
            cycle[2] = "التصديق";
            cycle[3] = "إعتماد مدير عام الادارة الطالبة";
            cycle[4] = "الموازنة 1";
            cycle[5] = "الموازنة 2";
            cycle[6] = "مدير قطاع المشتريات";
            cycle[7] = "المتابعة الفنية";
            cycle[8] = "إعتماد رئيس مجلس الإدارة";
            cycle[9] = "مدير عام المهمات";

            signatureDictionary[(9, false)] = cycle;
            #endregion

            #region AMR_SHERAA
            cycle = new Dictionary<int, string>();
            cycle[1] = "إعداد أمرالشراء";
            cycle[2] = "التصديق";
            cycle[3] = "مدير قطاع المشتريات";
            cycle[4] = "مدير عام المهمات";

            signatureDictionary[(3, false)] = cycle;
            #endregion

            #region AMR_SHERAA_FOREIGN
            cycle = new Dictionary<int, string>();
            cycle[1] = "إعداد أمرالشراء";
            cycle[2] = "التصديق";
            cycle[3] = "مدير قطاع المشتريات";
            cycle[4] = "مدير عام المهمات";

            signatureDictionary[(3, true)] = cycle;
            #endregion

            #region AMR_SHERAA_KEMAWYAT
            cycle = new Dictionary<int, string>();
            cycle[1] = "إعداد أمرالشراء";
            cycle[2] = "التصديق";
            cycle[3] = "مدير قطاع المشتريات";
            cycle[4] = "مدير عام المهمات";

            signatureDictionary[(12, false)] = cycle;
            #endregion

            #region EZN_SARF
            cycle = new Dictionary<int, string>();
            cycle[1] = "إعداد إذن الصرف";
            cycle[2] = "إعتماد مدير عام الادارة الطالبة";
            cycle[3] = "أمين المخزن";
            cycle[4] = "المستلم";

            signatureDictionary[(2, false)] = cycle;
            #endregion

            #region EDAFA_MAKHZANYA
            cycle = new Dictionary<int, string>();
            cycle[1] = "مخزن الاستلام";
            cycle[2] = "إعتماد مدير عام الادارة الطالبة";
            cycle[3] = "أمين المخزن";
            cycle[4] = "مدير قطاع المخازن";

            signatureDictionary[(5, false)] = cycle;
            #endregion

            #region EDAFA_MAKHZANYA_FOREIGN
            cycle = new Dictionary<int, string>();
            cycle[1] = "مخزن الاستلام";
            cycle[2] = "إعتماد مدير عام الادارة الطالبة";
            cycle[3] = "أمين المخزن";
            cycle[4] = "مدير قطاع المخازن";

            signatureDictionary[(5, true)] = cycle;
            #endregion

            #region EZN_TAHWEEL
            cycle = new Dictionary<int, string>();
            cycle[1] = "الراسل";
            cycle[2] = "إعتماد مدير عام الادارة الطالبة";
            cycle[3] = "مدير إدارة التصنيفات";
            cycle[4] = "مخزن الاستلام";
            cycle[5] = "مدير إدارة المخازن";
            cycle[6] = "مدير قطاع المخازن";

            signatureDictionary[(7, false)] = cycle;
            #endregion 
        }

        private void queryData()
        {
            string codeEdara = Constants.CodeEdara;
            string TalbTwareed_No = SelectedMostand.isForeign ? "'"+SelectedMostand.mostandNumber+"'" : SelectedMostand.mostandNumber;
            string FYear = SelectedMostand.mostandFinancialYear;
            string formNo = SelectedMostand.formNo.ToString();
            SqlConnection sqlConnction = new SqlConnection(SelectedMostand.isForeign ? Constants.constring3 : Constants.constring);
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
                                                                where TalbTwareed_No = " + TalbTwareed_No + " and FormNo=" + formNo + " AND FYear='" + FYear + "' order by SignOrder", sqlConnction);

            sqlConnction.Open();
            daTalabTawreed.Fill(dtTalabTawreed);


            sqlConnction.Close();
        }

        private string getSignatureDescription(int formNo, int signOrder)
        {
            return signatureDictionary[(formNo, SelectedMostand.isForeign)][signOrder];
        }

        private TimeLineCircleDetails circleDetailsFiller(DataRow row, CircleType type)
        {
            #region temp variables for selection
            int formNo, signNo;

            (int, int) symbolOffset;

            string symbol;

            Color mainTextColor, detailsTextColor, symbolColor, circleBackColor, circleColor;
            Font textFont, symbolFont;

            DurationIndecator indecator;
            #endregion

            TimeLineCircleDetails details = new TimeLineCircleDetails();

            symbol = "";

            formNo = Convert.ToInt32(row["FormNo"]);
            signNo = Convert.ToInt32(row["SignOrder"]);

            details.isDone = Convert.ToBoolean(row["isDone"]);
            details.donePercent = 0;

            int duration = Convert.ToInt32(row["Duration"]);

            indecator = null;
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



            if (duration > 3 && duration < 5)
            {
                Color backColor = Color.FromArgb(255, 193, 7);

                Font detailsFont = new Font("Calibri", (float)12, FontStyle.Bold);
                Color detailsColor = Color.FromArgb(255, 255, 255);

                DurationIndecatorSymbol symbolStyle = null;

                (int, int) marginX = (2, 2);
                (int, int) marginY = (1, 1);

                if (type == CircleType.LASTINPROGRESS) 
                {
                    string durationSymbol = "";
                    Font durationSymboltFont = new Font(f.Families[0], 11);

                    Color durationSymbolColor = Color.FromArgb(255, 255, 255);
                    (int, int) durationSymbolOffset = (0, 0);

                    symbolStyle = new DurationIndecatorSymbol(durationSymbolOffset, durationSymbol, durationSymboltFont, durationSymbolColor);
                }

                string indecateDuration = duration > 99 ? "99+ Days" : duration + " Days";
                indecator = new DurationIndecator(marginX, marginY, backColor, detailsFont, detailsColor, indecateDuration, symbolStyle);
            }
            else if (duration >= 5)
            {
                Color backColor = Color.FromArgb(220, 53, 69);

                Font detailsFont = new Font("Calibri", (float)12, FontStyle.Bold);
                Color detailsColor = Color.FromArgb(255, 255, 255);

                DurationIndecatorSymbol symbolStyle = null;

                (int, int) marginX = (2, 2);
                (int, int) marginY = (1, 1);

                if (type == CircleType.LASTINPROGRESS)
                {
                    string durationSymbol = "";
                    Font durationSymboltFont = new Font(f.Families[0], 13);

                    Color durationSymbolColor = Color.FromArgb(255, 255, 255);
                    (int, int) durationSymbolOffset = (0, 0);

                    symbolStyle = new DurationIndecatorSymbol(durationSymbolOffset, durationSymbol, durationSymboltFont, durationSymbolColor);
                }

                string indecateDuration = duration > 99 ? "99+ Days" : duration + " Days";
                indecator = new DurationIndecator(marginX, marginY, backColor, detailsFont, detailsColor, indecateDuration, symbolStyle);
            }

            details.mainText = new DrawedCircleText(Convert.ToString(row["signDate"]), textFont, mainTextColor);
            details.circleDetailsText = new DrawedCircleText(getSignatureDescription(formNo, signNo), textFont, detailsTextColor);
            details.circleSymbol = new CircleSymbol(symbol, symbolFont, symbolColor,symbolOffset);
            details.circleStyle = new CircleStyle(circleBackColor,circleColor);
            details.durationIndecator = indecator;

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
                else if (i > 0 && !Convert.ToBoolean(dtTalabTawreed.Rows[i]["isDone"]) && Convert.ToBoolean(dtTalabTawreed.Rows[i - 1]["isDone"]))
                {
                    type = CircleType.LASTINPROGRESS;
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
            prepareSignatureDicts();
            HandleTimeLineView();
        }


        private void formWraper_Paint(object sender, PaintEventArgs e)
        {
            TimeLine timeLineGraph = new TimeLine(e, formWraper.Width, list);
            timeLineGraph.DarwSequance(offsetX: 60,isRL:true);
        }
    }
}
