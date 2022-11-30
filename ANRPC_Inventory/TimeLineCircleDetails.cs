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
        public (int,int) symbolOffset { get; }

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

    internal class TimeLineCircleDetails
    {
        public DrawedCircleText mainText { get; set; }

        public DrawedCircleText circleDetailsText { get; set; }

        public CircleSymbol circleSymbol { get; set; }

        public CircleStyle circleStyle { get; set; }

        public int donePercent { get; set; }

        public bool isDone { get; set; }

        public int duration { get; set; }

    }
}
