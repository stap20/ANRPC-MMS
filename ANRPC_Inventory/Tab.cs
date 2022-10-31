using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ANRPC_Inventory
{
    public class Apperance
    {

    }
    public class Tab
    {
        private IconButton tabButton = new IconButton();

        public Tab(Font font,string text,Color color, int iconSize,IconFont iconFont,IconChar icon, DockStyle dockStyle,Action<object, EventArgs> onClickCallBack, int width = 0, int height = 0,Padding ? padding = null, FlatButtonAppearance appearance = null, bool isRL = false)
        {

            tabButton.Font = font;
            tabButton.Text = text;

            tabButton.ForeColor = color;
            tabButton.IconColor = color;

            tabButton.IconSize = iconSize;
            tabButton.IconChar = icon;
            tabButton.IconFont = iconFont;

            tabButton.FlatAppearance.BorderSize = 0;
            tabButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(43, 19, 114);
            tabButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(43, 19, 114);

            tabButton.AutoSize = true;

            tabButton.Click += new EventHandler(onClickCallBack);

            tabButton.FlatStyle = FlatStyle.Flat;            
            tabButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            tabButton.Dock = dockStyle;



            if (height ==0 && width == 0)
            {
                tabButton.Size = new Size(width,height);
            }
            else if (height != 0 || width != 0)
            {
                if(height == 0)
                {
                    tabButton.Size = new Size(width, tabButton.Size.Height);
                }
                else
                {
                    tabButton.Size = new Size(tabButton.Size.Width, height);
                }
            }
            else
            {
                tabButton.Size = new Size(width, height);
            }

            if(padding != null)
            {
                tabButton.Padding = (Padding)padding;
            }

            if (appearance != null)
            {
                tabButton.FlatAppearance.BorderSize = appearance.BorderSize;
                tabButton.FlatAppearance.MouseOverBackColor = appearance.MouseOverBackColor;
                tabButton.FlatAppearance.MouseDownBackColor = appearance.MouseDownBackColor;
            }

            if (isRL)
            {
                tabButton.RightToLeft = RightToLeft.Yes;
            }

        }



        public IconButton getTab()
        {
            return tabButton;
        }
    }
}
