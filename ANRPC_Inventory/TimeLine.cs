using System;
using System.Collections.Generic;
using System.Drawing;
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

        public void DarwSequance(int circleRaduis = 25, int offsetX = 40, int offsetY = 160)
        {

            int length, r, start_x, start_y, numberOfDrawedShapesCircles;
            r = circleRaduis;
            start_x = offsetX;
            start_y = offsetY;

            numberOfDrawedShapesCircles = this.timeLineList.Count - 1; //minus 1 because start dind't include in seq

            length = (this.containerWidth - 15 - (start_x * 2)) / numberOfDrawedShapesCircles;



            for (int i = numberOfDrawedShapesCircles; i > 0; i--)
            {

                TimeLineCircleDetails details = this.timeLineList[i];

                if (details.isDone)
                {
                   TimeLineHelper.DrawShape(this.paintEvent, start_x + ((i - 1) * (length)), start_y, length, r, details.donePercent, this.timeLineList[i]);
                }

                else
                {
                    TimeLineHelper.DrawShape(this.paintEvent, start_x + ((i - 1) * (length)), start_y, length, r,details.donePercent, this.timeLineList[i]);
                }
            }


            TimeLineHelper.DrawCompletedCircle(this.paintEvent, start_x, start_y, r, this.timeLineList[0], true);

        }

        private static class TimeLineHelper
        {
            public static void DrawPoint(PaintEventArgs e, int x, int y, Color c)
            {
                e.Graphics.FillRectangle(new SolidBrush(c), x, y, 5, 5);
            }

            public static void DrawLine(PaintEventArgs e, int x, int y, int length, bool isActiveLine = false, bool isEndCurved = false)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
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
                    bluepen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                }

                e.Graphics.DrawLine(bluepen, p3, p4);
            }

            public static void DrawCircle(PaintEventArgs e, int center_x, int center_y, int r, Color color)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                int start_x, start_y, diameter;
                start_x = center_x - r;
                start_y = center_y - r;
                diameter = 2 * r;

                e.Graphics.FillEllipse(new SolidBrush(color), start_x, start_y, diameter, diameter);

            }

            public static void DrawSymbol(PaintEventArgs e, int center_x, int center_y, Color color)
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
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

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

            public static void DrawShape(PaintEventArgs e, int x, int y, int length, int r, int SuccessSeqPercent,TimeLineCircleDetails details)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                int center_x, center_y;
                bool isActive = false;

                if(SuccessSeqPercent > 0)
                {
                    isActive = true;
                }

                if (SuccessSeqPercent < 100)
                {
                    DrawLine(e, x, y, length + ((SuccessSeqPercent * (length - r)) / 100), isActive, true);
                }
                else
                {
                    DrawLine(e, x, y, length, isActive);
                }

                center_x = x + length;
                center_y = y;

                DrawCompletedCircle(e, center_x, center_y, r, details,isActive);
            }


        }
    }
}
