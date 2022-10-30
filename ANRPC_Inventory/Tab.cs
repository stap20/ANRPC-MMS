using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
namespace ANRPC_Inventory
{
    public class Tab
    {
        //private Font font;
        //private string text;
        //private Color color;
        //private Size size;
        //private int iconSize;
        //private IconChar icon;
        //private Color hoverColor;

        private IconButton tabButton;

        public Tab(Font font,string text,Color color,Size size,int iconSize,IconChar icon,Color hoverColor)
        {

            tabButton.Font = font;
            tabButton.Text = text;
            tabButton.ForeColor = color;
            tabButton.IconColor = color;
            tabButton.Size = size;
            tabButton.IconSize = iconSize;
            tabButton.IconChar = icon;
            tabButton.FlatStyle = FlatStyle.Flat;

        }


    }
}
