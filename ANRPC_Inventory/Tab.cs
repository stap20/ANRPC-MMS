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
    public class Appearance
    {
        public int borderSize;
        public Color mouseOverBackColor;
        public Color mouseDownBAckColor;

        public Appearance(int borderSize, Color mouseOver,Color mouseDown)
        {
            this.borderSize = borderSize;
            this.mouseOverBackColor = mouseOver;
            this.mouseDownBAckColor = mouseDown;
        }
    }
    public class Tab
    {
        private IconButton tabButton = new IconButton();
        private Action<object, EventArgs> onClickCallBack;

        public Tab(Font font,string text,Color color, int iconSize,IconFont iconFont,IconChar icon, DockStyle dockStyle,Action<object, EventArgs> onClickCallBack, int width = 0, int height = 0,Padding ? padding = null, Appearance appearance = null, bool isRL = false)
        {

            tabButton.Font = font;
            tabButton.Text = text;

            tabButton.ForeColor = color;
            tabButton.IconColor = color;

            tabButton.IconSize = iconSize;
            tabButton.IconChar = icon;
            tabButton.IconFont = iconFont;

            tabButton.FlatAppearance.BorderSize = 0;
            tabButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(2, 163, 123);
            tabButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(2, 163, 123);

            tabButton.AutoSize = true;

            tabButton.ImageAlign = ContentAlignment.MiddleLeft;

            this.onClickCallBack = onClickCallBack;

            tabButton.Click += new EventHandler(onClickCallBack);
            
            tabButton.FlatStyle = FlatStyle.Flat;            
            tabButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            tabButton.TextAlign = ContentAlignment.MiddleLeft;

            tabButton.Dock = dockStyle;



            if (height ==0 && width == 0)
            {
                tabButton.Size = new Size(width,height);
            }
            else if (height != 0 || width != 0)
            {
                if (height == 0)
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
                tabButton.FlatAppearance.BorderSize = appearance.borderSize;
                tabButton.FlatAppearance.MouseOverBackColor = appearance.mouseOverBackColor;
                tabButton.FlatAppearance.MouseDownBackColor = appearance.mouseDownBAckColor;
            }

            if (isRL)
            {
                tabButton.RightToLeft = RightToLeft.Yes;
            }

        }


        public Action<object, EventArgs> getOnClickCallback()
        {
            return this.onClickCallBack;
        }

        public IconButton getTab()
        {
            return tabButton;
        }
    }
}
