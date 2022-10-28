using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANRPC_Inventory
{
    public partial class Search : Form
    {
        Panel subTabContainer = new Panel();
        public Search()
        {
            InitializeComponent();
            HandleSubTabs(guna2GradientPanel1);
            subTabContainer.Visible = false;
        }

        private IconButton MakeTab(IconChar icon, int iconSize, string text,Font font ,Color defaultColor, Color defaultBackColor)
        {
            IconButton button = new IconButton();
        
            button.Text = text;
            button.IconChar = icon;
            button.IconColor = defaultColor;
            button.ForeColor = defaultColor;
            button.Font = font;
            button.ImageAlign = ContentAlignment.MiddleLeft;
            button.TextAlign = ContentAlignment.MiddleCenter;

            button.TextImageRelation = TextImageRelation.ImageBeforeText;
            button.IconFont = IconFont.Solid;
            button.IconSize = iconSize;
            button.Size = new Size(186, 52);

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.MouseDownBackColor = defaultBackColor;
            button.FlatAppearance.MouseOverBackColor = defaultBackColor;
            button.FlatAppearance.BorderSize = 0;

            button.Dock = DockStyle.Left;

            return button;
        }

        private void HandleSubTabs(Panel tabsContainer)
        {
            subTabContainer = new Panel();
            subTabContainer.Dock = DockStyle.None;
            //subTabContainer.Anchor = AnchorStyles.Top;

            for (int i = 0; i < 5; i++)
            {
                Font font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

                IconButton button = MakeTab(IconChar.Clipboard, 23, "   دورة مستندية", font, Color.FromArgb(155, 170, 192), Color.FromArgb(43, 19, 114));

                subTabContainer.Controls.Add(button);
            
            }
            subTabContainer.AutoSize = true;
            subTabContainer.Size = new Size(subTabContainer.Size.Width, 35);
            tabsContainer.Controls.Add(subTabContainer);
        }

        private void btnDocumentCycle_Click(object sender, EventArgs e)
        {
            //if (!subTabContainer.Visible)
            //{
            //    guna2GradientPanel1.Size = new Size(guna2GradientPanel1.Width, guna2GradientPanel1.Height + subTabContainer.Height + 10);

            //    panel2.Size = new Size(panel2.Width, panel2.Height + subTabContainer.Height + 10);


            //    subTabContainer.Location = new Point(btnDocumentCycle.Location.X+btnDocumentCycle.Size.Width, btnDocumentCycle.Location.Y + btnDocumentCycle.Size.Height + 5);

            //    subTabContainer.Visible = true;
            //}
        }

        private void btnTasnif_Click(object sender, EventArgs e)
        {
            //guna2GradientPanel1.Size = new Size(guna2GradientPanel1.Width, 52);
            //panel2.Size = new Size(panel2.Width, 69);

            //subTabContainer.Visible = false;

        }
    }
}
