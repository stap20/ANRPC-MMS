﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.HtmlRenderer.Adapters.RGraphicsPath;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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

        public void DarwSequance(int circleRaduis = 25, int offsetX = 40, int offsetY = 160, bool isRL = false)
        {

            int length, r, start_x, start_y, numberOfDrawedShapesCircles;
            r = circleRaduis;
            start_x = offsetX;
            start_y = offsetY;

            numberOfDrawedShapesCircles = this.timeLineList.Count - 1; //minus 1 because start dind't include in seq

            length = (this.containerWidth - 15 - (start_x * 2)) / numberOfDrawedShapesCircles;

            for (int i = numberOfDrawedShapesCircles; i > 0 ; i--)
            {
                TimeLineCircleDetails details = this.timeLineList[i];

                if (isRL)
                {
                    TimeLineHelper.DrawShape(this.paintEvent, start_x + ((numberOfDrawedShapesCircles + 1 - i - 1) * (length)), start_y, length, r, details, isRL: isRL);
                }
                else
                {
                    TimeLineHelper.DrawShape(this.paintEvent, start_x + ((i - 1) * (length)), start_y, length, r, details, isRL: isRL);
                }

            }

            if (isRL)
            {
                TimeLineHelper.DrawCompletedCircle(this.paintEvent, this.containerWidth - start_x, start_y, r, this.timeLineList[0]);
            }
            else
            {
                TimeLineHelper.DrawCompletedCircle(this.paintEvent, start_x, start_y, r, this.timeLineList[0]);
            }

        }

        private static class TimeLineHelper
        {
            private static void DrawPoint(PaintEventArgs e, int x, int y, Color c)
            {
                e.Graphics.FillRectangle(new SolidBrush(c), x, y, 1, 1);
            }

            private static GraphicsPath MakeRoundedRect(RectangleF rect, float xradius, float yradius, bool round_ul, bool round_ur, bool round_lr, bool round_ll)
            {
                // Make a GraphicsPath to draw the rectangle.
                PointF point1, point2;
                GraphicsPath path = new GraphicsPath();

                //Top left corner
                if (round_ul)
                {
                    RectangleF corner = new RectangleF(
                        rect.X, rect.Y,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 180, 90);
                    point1 = new PointF(rect.X + xradius, rect.Y);
                }
                else point1 = new PointF(rect.X, rect.Y);

                //Top side
                if (round_ur)
                    point2 = new PointF(rect.Right - xradius, rect.Y);
                else
                    point2 = new PointF(rect.Right, rect.Y);
                path.AddLine(point1, point2);

                //Top right corner
                if (round_ur)
                {
                    RectangleF corner = new RectangleF(
                        rect.Right - 2 * xradius, rect.Y,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 270, 90);
                    point1 = new PointF(rect.Right, rect.Y + yradius);
                }
                else point1 = new PointF(rect.Right, rect.Y);

                //Right side
                if (round_lr)
                    point2 = new PointF(rect.Right, rect.Bottom - yradius);
                else
                    point2 = new PointF(rect.Right, rect.Bottom);
                path.AddLine(point1, point2);

                //Bottom right corner
                if (round_lr)
                {
                    RectangleF corner = new RectangleF(
                        rect.Right - 2 * xradius,
                        rect.Bottom - 2 * yradius,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 0, 90);
                    point1 = new PointF(rect.Right - xradius, rect.Bottom);
                }
                else point1 = new PointF(rect.Right, rect.Bottom);

                //Bottom side
                if (round_ll)
                    point2 = new PointF(rect.X + xradius, rect.Bottom);
                else
                    point2 = new PointF(rect.X, rect.Bottom);
                path.AddLine(point1, point2);

                //Bottom left corner
                if (round_ll)
                {
                    RectangleF corner = new RectangleF(
                        rect.X, rect.Bottom - 2 * yradius,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 90, 90);
                    point1 = new PointF(rect.X, rect.Bottom - yradius);
                }
                else point1 = new PointF(rect.X, rect.Bottom);

                //Left side
                if (round_ul)
                    point2 = new PointF(rect.X, rect.Y + yradius);
                else
                    point2 = new PointF(rect.X, rect.Y);
                path.AddLine(point1, point2);

                //Join with the start point.
                path.CloseFigure();

                return path;
            }

            public static void fillTriangle(PaintEventArgs e, Brush varbrush, Point[] points)
            {
                e.Graphics.FillPolygon(varbrush, points);
            }
            private static void DrawSymbol(PaintEventArgs e, int center_x, int center_y, TimeLineCircleDetails details)
            {
                Font font = details.circleSymbol.Font;
                string symbol = details.circleSymbol.symbol;
                Color color = details.circleSymbol.Color;

                // Create font and brush.
                SizeF s = e.Graphics.MeasureString(symbol, font);

                int start_x, start_y;
                start_x = center_x - (Convert.ToInt32(s.Width) / 2)+details.circleSymbol.symbolOffset.Item1;
                start_y = center_y - (Convert.ToInt32(s.Height) / 2)+ details.circleSymbol.symbolOffset.Item2;

                SolidBrush drawBrush1 = new SolidBrush(color);

                // Set format of string.
                StringFormat drawFormat1 = new StringFormat();

                // Draw string to screen.
                e.Graphics.DrawString(symbol, font, drawBrush1, start_x, start_y, drawFormat1);

            }

            private static void DurationIndecationSymbol(PaintEventArgs e, int start_x, int start_y, TimeLineCircleDetails details)
            {
                const float xradius = 1;
                const float yradius = 1;
                 
                RectangleF rect = new RectangleF(start_x, start_y, details.durationIndecator.width, details.durationIndecator.height);

                GraphicsPath path = MakeRoundedRect(rect, xradius, yradius, true, true, true, true);

                SolidBrush brush = new SolidBrush(details.durationIndecator.backColor);

                e.Graphics.FillPath(brush, path);

                e.Graphics.DrawPath(new Pen(details.durationIndecator.backColor, 5), path);



                int tr_x, tr_y,center_point,triangle_width,triangle_height;

                triangle_width = 16;
                triangle_height = 12;

                center_point = start_x + Convert.ToInt32(details.durationIndecator.width / 2);
                tr_x = center_point  - Convert.ToInt32(triangle_width);
                tr_y = start_y + details.durationIndecator.height;

                Point[] points = { new Point(tr_x, tr_y), new Point(tr_x + triangle_width, tr_y), new Point(center_point, tr_y + triangle_height) };

                SolidBrush varbrush = new SolidBrush(details.durationIndecator.backColor);

                fillTriangle(e, varbrush, points);
            }

            private static void DrawDurationIndecator(PaintEventArgs e ,int center_x, int center_y,TimeLineCircleDetails details)
            {
                int start_X, start_y;

                start_X = center_x - Convert.ToInt32(details.durationIndecator.width / 2);

                if (details.durationIndecator != null)
                {
                    DurationIndecationSymbol(e, center_x, center_y, details);   
                }
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
            
            private static void DrawText(PaintEventArgs e, int center_x, int center_y, int offsetY,  DrawedCircleText drawedText,bool isTitle)
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

                SolidBrush drawBrush1 = new SolidBrush(drawedText.Color);

                // Set format of string.
                StringFormat drawFormat1 = new StringFormat();

                RectangleF textWrapper;
                textWrapper = new RectangleF(start_x, start_y, s.Width,s.Height);
                

                // Draw string to screen.
                e.Graphics.DrawString(drawedText.Text, drawedText.Font, drawBrush1,textWrapper, drawFormat1);
            }
            
            public static void DrawCompletedCircle(PaintEventArgs e, int center_x, int center_y, int r, TimeLineCircleDetails details)
            {
                DrawCircle(e, center_x, center_y, r, details.circleStyle.circleBackColor);
                DrawCircle(e, center_x, center_y, r - 5, details.circleStyle.circleColor);

                DrawSymbol(e, center_x, center_y,details);

                DrawText(e, center_x, center_y, (Convert.ToInt32(r*1.5)), details.mainText,true);
                DrawText(e, center_x, center_y, (Convert.ToInt32(r*2.5)), details.circleDetailsText, false);
            }



            public static void DrawShape(PaintEventArgs e, int x, int y, int length, int r,TimeLineCircleDetails details,bool isRL = true)
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                int center_x, center_y;
                bool isActive = false;


                center_y = y;

                if (details.donePercent > 0)
                {
                    isActive = true;
                }

                if (isRL)
                {
                    center_x = x;
                }
                else
                {
                    center_x = x + length;
                }

                if (details.donePercent > 0 && details.donePercent < 100)
                {
                    int percentExtend = ((details.donePercent * (length - r)) / 100);

                    if (isRL)
                    {
                        DrawLine(e, x - percentExtend, y, length + percentExtend, details, isActive, true, isRL: isRL);
                    }
                    else
                    {
                        DrawLine(e, x, y, length + percentExtend, details, isActive, true, isRL: isRL);
                    }


                    DrawDurationIndecator(e, center_x, center_y, details);
                }
                else
                {
                    DrawLine(e, x, y, length, details,isActive,isRL: isRL);

                   // DrawDurationIndecator(e, 5, x + r + 10, y - 23,120, details);
                }

                DrawCompletedCircle(e, center_x, center_y, r, details);
            }
        }
    }
}
