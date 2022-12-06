using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANRPC_Inventory
{
    public class CircleStyle
    {
        public Color circleBackColor { get; }

        public Color circleColor { get; }

        public CircleStyle(Color circleBackColor, Color circleColor)
        {
            this.circleBackColor = circleBackColor;
            this.circleColor = circleColor;
        }
    }

    public class CircleSymbol
    {
        public (int, int) symbolOffset { get; }

        public string symbol { get; }

        public Font Font { get; }

        public Color Color { get; }

        public CircleSymbol(string symbol, Font Font, Color Color, (int, int) symbolOffset)
        {
            this.symbol = symbol;
            this.Font = Font;
            this.Color = Color;
            this.symbolOffset = symbolOffset;
        }

    }

    public class DurationIndecatorSymbol
    {
        public (int, int) symbolOffset { get; }

        public string symbol { get; }

        public Font Font { get; }

        public Color Color { get; }

        public DurationIndecatorSymbol((int, int) symbolOffset, string symbol, Font font, Color color)
        {
            this.symbolOffset = symbolOffset;
            this.symbol = symbol;
            Font = font;
            Color = color;
        }
    }

    public class DurationIndecator
    {
        public Font Font { get; }

        public Color Color { get; }

        public Color backColor { get; }

        public string Text { get; }

        public (int, int) marginX { get; }
        public (int, int) marginY { get; }

        public DurationIndecatorSymbol symbol { set; get; }

        public DurationIndecator((int, int) marginX, (int, int) marginY, Color backColor,Font font, Color color, string text, DurationIndecatorSymbol symbol)
        {
            this.marginX = marginX;
            this.marginY = marginY;
            this.backColor = backColor;
            Font = font;
            Color = color;
            this.Text = text;
            this.symbol = symbol;
        }

    }

    internal class TimeLineCircleDetails
    {
        public DrawedCircleText mainText { get; set; }

        public DrawedCircleText circleDetailsText { get; set; }

        public CircleSymbol circleSymbol { get; set; }

        public CircleStyle circleStyle { get; set; }

        public DurationIndecator durationIndecator { get; set; }

        public int donePercent { get; set; }

        public bool isDone { get; set; }      
    }
}
