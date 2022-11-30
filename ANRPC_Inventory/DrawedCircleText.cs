using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANRPC_Inventory
{
    internal class DrawedCircleText
    {
        public string Text { get; }

        public Font Font { get;}

        public Color Color { get; }

        public DrawedCircleText(string text, Font font, Color color)
        {
            Text = text;
            Font = font;
            Color = color;
        }
    }
}
