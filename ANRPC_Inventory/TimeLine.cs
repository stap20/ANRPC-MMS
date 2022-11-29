using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANRPC_Inventory
{
    internal class TimeLine
    {
        PaintEventArgs paintEvent;       
        int containerWidth;
        List<TimeLineCircleDetails> timeLineList = new List<TimeLineCircleDetails>();

        public TimeLine(PaintEventArgs paintEvent, int containerWidth, List<TimeLineCircleDetails> timeLineList)
        {
            this.paintEvent = paintEvent;
            this.containerWidth = containerWidth;
            this.timeLineList = timeLineList;
        }

        public void DarwSequance(int circleRaduis = 25, int offsetX = 40, int offsetY = 160, bool isRL = true)
        {

            int length, r, start_x, start_y, numberOfDrawedShapesCircles;
            r = circleRaduis;
            start_x = offsetX;
            start_y = offsetY;

            numberOfDrawedShapesCircles = this.timeLineList.Count - 1; //minus 1 because start dind't include in seq

            length = (this.containerWidth - 15 - (start_x * 2)) / numberOfDrawedShapesCircles;

            int circleDetailsPointer = isRL == true ? 0 : numberOfDrawedShapesCircles;


            for (int i = numberOfDrawedShapesCircles; i >= 0 ; i--)
            {
                TimeLineCircleDetails details = this.timeLineList[circleDetailsPointer];

                if (i == 0) //last circle
                {
                    TimeLineHelper.DrawCompletedCircle(this.paintEvent, start_x, start_y, r, details, details.isDone);
                }
                else
                {
                    TimeLineHelper.DrawShape(this.paintEvent, start_x + ((i - 1) * (length)), start_y, length, r, details, isRL: isRL);
                }

                circleDetailsPointer = isRL == true ? circleDetailsPointer+1 : i-1;  
            }
        }

        private static class TimeLineHelper
        {
            enum IndecatorBarType
            {
                NORMAL,
                MEDUIM,
                DANGER,

            }

            private static void DrawPoint(PaintEventArgs e, int x, int y, Color c)
            {
                e.Graphics.FillRectangle(new SolidBrush(c), x, y, 5, 5);
            }

            private static void DrawIndecatorSymbol(PaintEventArgs e,string indecatorSymbol, int center_x, int center_y, Color color)
            {
                // Create font and brush.
                PrivateFontCollection f = new PrivateFontCollection();
                f.AddFontFile("fa-solid-900.ttf");

                Font drawFont1 = new Font(f.Families[0], 18);

                SizeF s = e.Graphics.MeasureString(indecatorSymbol, drawFont1);

                int start_x, start_y;
                start_x = center_x - Convert.ToInt32(s.Width) / 2 + 1;
                start_y = center_y - Convert.ToInt32(s.Height) / 2 - 1;

                SolidBrush drawBrush1 = new SolidBrush(color);

                // Set format of string.
                StringFormat drawFormat1 = new StringFormat();

                // Draw string to screen.
                e.Graphics.DrawString(indecatorSymbol, drawFont1, drawBrush1, start_x, start_y, drawFormat1);

            }

            private static void DrawIndecatorBarSection(PaintEventArgs e, int x, int y, int length,Color color, IndecatorBarType type)
            {
                int start_pos_x, start_pos_y, end_pos_x, end_pos_y;

                start_pos_x = x;
                start_pos_y = y;
                end_pos_x = x + length;
                end_pos_y = y;

                //Draw_Line_Pending
                Pen bluepen = new Pen(color, 10);
                Point p3 = new Point(start_pos_x, start_pos_y);
                Point p4 = new Point(end_pos_x, end_pos_y);

                if (type == IndecatorBarType.NORMAL)
                {
                    bluepen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                }
                else if(type == IndecatorBarType.DANGER)
                {
                    bluepen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                }
                e.Graphics.DrawLine(bluepen, p3, p4);
            }

            private static void DrawDurationIndecator(PaintEventArgs e,int duration ,int start_x, int start_y, int lenght, TimeLineCircleDetails details)
            {
                Color normal_color,meduim_color,danger_color;

                normal_color = Color.FromArgb(80, 176, 46);
                meduim_color = Color.FromArgb(252, 216, 35);
                danger_color = Color.FromArgb(225, 26, 34);



                //string indecatorSymbol = "";
                //Color indecatorSymbolColor = new Color();
                //if(details.duration >= 0  && details.duration <= 3)
                //{
                //    indecatorSymbol = "";
                //    indecatorSymbolColor = Color.FromArgb(53, 178, 136);
                //}
                //else if(details.duration > 3 && details.duration <= 5)
                //{
                //    indecatorSymbol = "";
                //    indecatorSymbolColor = Color.FromArgb(255, 212, 59);
                //}
                //else if(details.duration >= 6)
                //{
                //    indecatorSymbol = "";
                //    indecatorSymbolColor = Color.FromArgb(235, 50, 35);
                //}

                //if (details.duration >= 0)
                //{
                //    DrawIndecatorSymbol(e, indecatorSymbol, center_x, center_y, indecatorSymbolColor);
                //}


                DrawIndecatorBarSection(e, start_x, start_y, lenght/3, normal_color, IndecatorBarType.NORMAL);
                DrawIndecatorBarSection(e, start_x+40, start_y, lenght / 3, meduim_color, IndecatorBarType.MEDUIM);
                DrawIndecatorBarSection(e, start_x+80, start_y, lenght / 3, danger_color, IndecatorBarType.DANGER);

            }

            private static void DrawLine(PaintEventArgs e, int x, int y, int length, TimeLineCircleDetails details, bool isActiveLine = false, bool isEndCurved = false,bool isRL=false)
            {
                Color line_color;

                if (isActiveLine == true)
                {
                    line_color = Color.FromArgb(53, 178, 136);
                }
                else
                {
                    line_color = Color.FromArgb(233, 241, 252);
                }


                int start_pos_x, start_pos_y, end_pos_x, end_pos_y;

                start_pos_x = x;
                start_pos_y = y;

                end_pos_x = x + length;
                end_pos_y = y;

                //Draw_Line_Pending
                Pen bluepen = new Pen(line_color, 20);
                Point p3 = new Point(start_pos_x, start_pos_y);
                Point p4 = new Point(end_pos_x, end_pos_y);

                if (isEndCurved == true)
                {
                    if (!isRL)
                    {
                        bluepen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    }
                    else
                    {
                        bluepen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    }
                }

                e.Graphics.DrawLine(bluepen, p3, p4);
            }

            private static void DrawCircle(PaintEventArgs e, int center_x, int center_y, int r, Color color)
            {
                int start_x, start_y, diameter;
                start_x = center_x - r;
                start_y = center_y - r;
                diameter = 2 * r;

                e.Graphics.FillEllipse(new SolidBrush(color), start_x, start_y, diameter, diameter);

            }

            private static void DrawSymbol(PaintEventArgs e, int center_x, int center_y, Color color)
            {

                String drawString1 = "";

                // Create font and brush.
                Font drawFont1 = new Font("Segoe UI Symbol", 13);

                SizeF s = e.Graphics.MeasureString(drawString1, drawFont1);

                int start_x, start_y;
                start_x = center_x - Convert.ToInt32(s.Width) / 2 + 1;
                start_y = center_y - Convert.ToInt32(s.Height) / 2 - 1;

                SolidBrush drawBrush1 = new SolidBrush(color);

                // Set format of string.
                StringFormat drawFormat1 = new StringFormat();

                // Draw string to screen.
                e.Graphics.DrawString(drawString1, drawFont1, drawBrush1, start_x, start_y, drawFormat1);

            }

            
            private static void DrawText(PaintEventArgs e, int center_x, int center_y, int r, Color color, int offsetY,  DrawedCircleText drawedText,bool isTitle)
            {
                SizeF s;

                if (!isTitle)
                {
                    s = e.Graphics.MeasureString(drawedText.Text, drawedText.Font,100);
                }
                else{
                    s = e.Graphics.MeasureString(drawedText.Text, drawedText.Font);
                }
              
                int start_x, start_y;

                start_x = center_x - Convert.ToInt32(s.Width) / 2 + 1;
                start_y = center_y + offsetY;

                SolidBrush drawBrush1 = new SolidBrush(color);

                // Set format of string.
                StringFormat drawFormat1 = new StringFormat();

                RectangleF textWrapper;
                textWrapper = new RectangleF(start_x, start_y, s.Width,s.Height);
                

                // Draw string to screen.
                e.Graphics.DrawString(drawedText.Text, drawedText.Font, drawBrush1,textWrapper, drawFormat1);
            }
            
            public static void DrawCompletedCircle(PaintEventArgs e, int center_x, int center_y, int r, TimeLineCircleDetails details, bool isActiveCircle = false)
            {
                Color color;
                Color textColor = Color.FromArgb(18, 18, 18); ;
                Color symbolColor;

                if (isActiveCircle == true)
                {
                    color = Color.FromArgb(53, 178, 136);
                    symbolColor = color;
                }
                else
                {
                    color = Color.FromArgb(233, 241, 252);
                    symbolColor = Color.FromArgb(188, 215, 246);
                }

                Color W = Color.FromArgb(255, 255, 255);

                DrawCircle(e, center_x, center_y, r, color);
                DrawCircle(e, center_x, center_y, r - 5, W);
                DrawSymbol(e, center_x, center_y, symbolColor);

                DrawText(e, center_x, center_y, r, color, -(Convert.ToInt32(r*2.5)), details.mainText,true);
                DrawText(e, center_x, center_y, r, textColor, (Convert.ToInt32(r*1.5)), details.circleDetailsText, false);
            }

            public static void DrawShape(PaintEventArgs e, int x, int y, int length, int r,TimeLineCircleDetails details,bool isRL = true)
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                int center_x, center_y;
                bool isActive = false;

                if(details.donePercent > 0)
                {
                    isActive = true;
                }

                if (details.donePercent > 0 && details.donePercent < 100)
                {
                    DrawLine(e, x, y, length + ((details.donePercent * (length - r)) / 100), details,isActive, true, isRL: isRL);

                   // DrawDurationIndecator(e, 5, x + r +20, y - 50,120, details);
                }
                else
                {
                    DrawLine(e, x, y, length, details,isActive,isRL: isRL);

                   // DrawDurationIndecator(e, 5, x + r + 10, y - 23,120, details);
                }

                center_x = x + length;
                center_y = y;

                DrawCompletedCircle(e, center_x, center_y, r, details,isActive);
            }


        }
    }
}
