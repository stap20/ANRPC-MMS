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

        public System.Drawing.Font Font { get;}

        public DrawedCircleText(string text, Font font)
        {
            Text = text;
            Font = font;
        }
    }
}
