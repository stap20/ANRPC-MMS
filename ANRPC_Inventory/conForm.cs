using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Guna.UI2.WinForms;

namespace ANRPC_Inventory
{




    public partial class conForm : Form
    {
        int currentScrollValue = 0;
        
        public conForm()
        {        
            InitializeComponent();

            List<(NOTIFICATION_TYPES, PANEL_TYPES)> list = getNotificationList();
            generateNotificationControls(list);
        }



        private List<(NOTIFICATION_TYPES, PANEL_TYPES)> getNotificationList()
        {
            List<(NOTIFICATION_TYPES, PANEL_TYPES)> list = new List<(NOTIFICATION_TYPES, PANEL_TYPES)>();
            list.Add((NOTIFICATION_TYPES.SUCCESS, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.INFO, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.WARNING, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.SUCCESS, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.ERROR, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.WARNING, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.WARNING, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.SUCCESS, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.SUCCESS, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.INFO, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.INFO, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.ERROR, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.ERROR, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.ERROR, PANEL_TYPES.ROUNDED));
            list.Add((NOTIFICATION_TYPES.WARNING, PANEL_TYPES.ROUNDED));

            return list;
        }

        enum NOTIFICATION_TYPES
        {
            SUCCESS,
            INFO,
            WARNING,
            ERROR
        }


        enum PANEL_TYPES
        {
            ROUNDED,
            SHARP,
        }

















        private void ResponsiveEnd()
        {
            //MessageBox.Show(this.Width.ToString());
            //if (this.Width <= 450)
            //{
            //    tableLayoutPanel2.ColumnStyles[1].Width = 350;
            //}
            if (this.Width <= 555)
            {
                tableLayoutPanel2.ColumnCount = 1;
            }
            else if (this.Width <= 690)
            {
                tableLayoutPanel2.ColumnCount = 2;
                tableLayoutPanel3.ColumnCount = 1;
            }
            else
            {
                tableLayoutPanel2.ColumnCount = 4;
                tableLayoutPanel3.ColumnCount = 2;
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void wheelo(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (currentScrollValue + 20 < panel4.VerticalScroll.Maximum)
                {
                    currentScrollValue += 20;
                    panel4.VerticalScroll.Value = currentScrollValue;
                }
                else
                {
                    // If scroll position is above 280 set the position to 280 (MAX)
                    currentScrollValue = panel4.VerticalScroll.Maximum;
                    panel4.AutoScrollPosition = new Point(0, currentScrollValue);
                }
            }
            else
            {
                if (currentScrollValue - 20 > 0)
                {
                    currentScrollValue -= 20;
                    panel4.VerticalScroll.Value = currentScrollValue;
                }
                else
                {
                    // If scroll position is below 0 set the position to 0 (MIN)
                    currentScrollValue = 0;
                    panel4.AutoScrollPosition = new Point(0, currentScrollValue);
                }
            }
        }


        /**
                     Guna2Elipse guna2Elipse = new Guna2Elipse();
            Panel panel_Sidebar2 = new Panel();
            guna2Elipse.TargetControl = panel_Sidebar2;
            guna2Elipse.BorderRadius = 7;
         */


        /**
         
     
            IconPictureBox iconPictureBox_Sidebar1 = new IconPictureBox();
            iconPictureBox_Sidebar1.IconChar = FontAwesome.Sharp.IconChar.Trash;
            iconPictureBox_Sidebar1.BackColor = System.Drawing.Color.Transparent;
            iconPictureBox_Sidebar1.IconColor = System.Drawing.Color.FromArgb(255, 61, 87);
            iconPictureBox_Sidebar1.Location = new System.Drawing.Point(170, 18);
            iconPictureBox_Sidebar1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            iconPictureBox_Sidebar1.IconSize = 20;
            iconPictureBox_Sidebar1.MouseEnter += new EventHandler(Bin_MouseEnter);
            iconPictureBox_Sidebar1.MouseLeave += new EventHandler(Bin_MouseLeave);
            iconPictureBox_Sidebar1.Cursor = Cursors.Hand;


         */


        /**
         
         
            //Add_Label
            Label label_Sidebar = new Label();
            Label label_Sidebar1 = new Label();
            label_Sidebar.BackColor = System.Drawing.Color.Transparent;
            label_Sidebar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            label_Sidebar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            label_Sidebar.ForeColor = System.Drawing.Color.FromArgb(164, 163, 203);
            label_Sidebar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label_Sidebar.Location = new System.Drawing.Point(50, 6);
            label_Sidebar.Size = new System.Drawing.Size(71, 45);
            label_Sidebar.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            label_Sidebar.Text = "Deposit\nname entity";

            label_Sidebar1.BackColor = System.Drawing.Color.Transparent;
            label_Sidebar1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            label_Sidebar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label_Sidebar1.ForeColor = System.Drawing.Color.FromArgb(164, 163, 203);
            label_Sidebar1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label_Sidebar1.Location = new System.Drawing.Point(175, 6);
            label_Sidebar1.Size = new System.Drawing.Size(53, 40);
            label_Sidebar1.Padding = new System.Windows.Forms.Padding(0, 5, 5, 0);
            label_Sidebar1.Text = "$1526\n  date";

            //Add_button
         
         */


        private Panel getPanel(PANEL_TYPES type = PANEL_TYPES.SHARP, Color ? bkColor = null)
        {
            Panel panel = new Panel();
            panel.AutoSize = true;
            if(type == PANEL_TYPES.ROUNDED)
            {
                Guna2GradientPanel rpanel = new Guna2GradientPanel();
                rpanel.Dock = DockStyle.Fill;
                rpanel.BorderRadius = 7;
                rpanel.AutoSize = true;

                if (bkColor != null)
                {
                    rpanel.FillColor = (Color)bkColor;
                    rpanel.FillColor2 = (Color)bkColor;
                }
                panel.Controls.Add(rpanel);
            }
            else
            {
                if (bkColor != null)
                {
                    panel.BackColor = (Color)bkColor;
                }
            }
            
            return panel;
        }



        private ToastConfigration getToastConfigration(NOTIFICATION_TYPES type)
        {

            ToastConfigration toastConfig = new ToastConfigration();

            if (type == NOTIFICATION_TYPES.SUCCESS)
            {
                toastConfig.bodyColor = Color.FromArgb(0, 126, 80);

                toastConfig.icon = FontAwesome.Sharp.IconChar.CircleCheck;
                toastConfig.iconColor = Color.White;

                toastConfig.titleColor = Color.White;
                toastConfig.title = "Success";
            }
            else if (type == NOTIFICATION_TYPES.INFO)
            {
                toastConfig.bodyColor = Color.FromArgb(4, 105, 227);

                toastConfig.icon = FontAwesome.Sharp.IconChar.InfoCircle;
                toastConfig.iconColor = Color.White;

                toastConfig.titleColor = Color.White;
                toastConfig.title = "Info";
            }
            else if (type == NOTIFICATION_TYPES.WARNING)
            {
                toastConfig.bodyColor = Color.FromArgb(255, 167, 0);

                toastConfig.icon = FontAwesome.Sharp.IconChar.CircleExclamation;
                toastConfig.iconColor = Color.White;

                toastConfig.titleColor = Color.White;
                toastConfig.title = "Warning";
            }
            else if (type == NOTIFICATION_TYPES.ERROR)
            {
                toastConfig.bodyColor = Color.FromArgb(187, 2, 2);

                toastConfig.icon = FontAwesome.Sharp.IconChar.CircleXmark;
                toastConfig.iconColor = Color.White;

                toastConfig.titleColor = Color.White;
                toastConfig.title = "Error";
            }


            return toastConfig;
        }


        private Panel getToastBodyBone(PANEL_TYPES type, Color color)
        {
            Panel toastBody = getPanel(type, color);
            toastBody.Dock = System.Windows.Forms.DockStyle.Fill;
            //toastBody.Tag = 1;   to store index (stored any data)


            return toastBody;
        }

        private Panel getToastIcon(IconChar iconPic,Color color)
        {
            Panel iconPanel = new Panel();
            int iconSize = 40;
            IconPictureBox icon = new IconPictureBox();

            icon.IconChar = iconPic;

            icon.IconColor = color;
            icon.BackColor = Color.Transparent;
            icon.IconSize = iconSize;
            icon.UseGdi = true;
            icon.IconFont = FontAwesome.Sharp.IconFont.Solid;
            icon.SizeMode = PictureBoxSizeMode.CenterImage;
            icon.Dock = System.Windows.Forms.DockStyle.Fill;

            iconPanel.BackColor = Color.Transparent;
            iconPanel.Size = new Size(iconSize, iconPanel.Size.Height);
            iconPanel.Padding = new Padding(10, 0, 0, 0);
            iconPanel.Controls.Add(icon);
            iconPanel.Dock = System.Windows.Forms.DockStyle.Left;

            return iconPanel;
        }





       /// <summary>
       /// ///////////////////////////////////////////////////////////////////////////////////////////////////////
       /// </summary>
       /// <param name="iconPic"></param>
       /// <param name="color"></param>
       /// <returns></returns>
        private Panel getDismissToastIcon(IconChar iconPic, Color color)
        {
            Panel iconPanel = new Panel();
            int iconSize = 30;
            IconPictureBox icon = new IconPictureBox();

            icon.IconChar = FontAwesome.Sharp.IconChar.Trash;

            icon.IconColor = Color.FromArgb(211, 204, 196);
            icon.BackColor = Color.Transparent;
            icon.IconSize = iconSize;
            icon.UseGdi = true;
            icon.IconFont = FontAwesome.Sharp.IconFont.Solid;
            icon.SizeMode = PictureBoxSizeMode.CenterImage;
            icon.Dock = System.Windows.Forms.DockStyle.Fill;

            iconPanel.BackColor = Color.Transparent;
            iconPanel.Size = new Size(iconSize, iconPanel.Size.Height);
            iconPanel.Padding = new Padding(0, 0, 10, 0);
            iconPanel.Controls.Add(icon);
            iconPanel.Dock = System.Windows.Forms.DockStyle.Right;

            return iconPanel;
        }

        private Panel getToastTitle(string title, Color color)
        {
            Panel titlePanel = new Panel();
            titlePanel.Dock = DockStyle.Top;
            titlePanel.BackColor = Color.Transparent;

            Label label = new Label();
            label.Text = title;
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Font = new Font("Microsoft Sans Serif",10,FontStyle.Bold);
            label.ForeColor = color;
            label.BackColor = Color.Transparent;
            label.Dock = DockStyle.Fill; 
            

            titlePanel.Size = new Size(titlePanel.Size.Width, 32);
            titlePanel.Padding = new Padding(5, 5, 0, 0);

            titlePanel.Controls.Add(label);

            return titlePanel;
        }


        private Panel getToastDescription(string description, Color color)
        {
            Panel descriptionPanel = new Panel();
            descriptionPanel.Dock = DockStyle.Fill;
            descriptionPanel.AutoSize = true;
            descriptionPanel.BackColor = Color.Transparent;

            Label label = new Label();
            label.Text = description;
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            label.ForeColor = color;
            label.BackColor = Color.Transparent;
            label.Dock = DockStyle.Fill;
            label.AutoSize = true;
            label.MaximumSize = new Size(descriptionPanel.Size.Width-25, 0);

            descriptionPanel.Padding = new Padding(5,0,5,10);

            descriptionPanel.Controls.Add(label);

            return descriptionPanel;
        }


        private Panel getToastBody(NOTIFICATION_TYPES type, PANEL_TYPES bodyType)
        {
            ToastConfigration toastConfig = getToastConfigration(type);

            Panel toastBody = getToastBodyBone(bodyType, toastConfig.bodyColor);

            Panel toastIcon = getToastIcon(toastConfig.icon, toastConfig.iconColor);
            Panel toastDismissIcon = getDismissToastIcon(toastConfig.icon, toastConfig.iconColor);
            Panel toastTitle = getToastTitle(toastConfig.title,toastConfig.titleColor);
            Panel toastDescription = getToastDescription(@"You can access all files in this folder.", toastConfig.titleColor);

            if (bodyType == PANEL_TYPES.ROUNDED)
            {
                toastBody.Controls[0].Controls.Add(toastDescription);
                toastBody.Controls[0].Controls.Add(toastTitle);
                toastBody.Controls[0].Controls.Add(toastIcon);
                toastBody.Controls[0].Controls.Add(toastDismissIcon);
            }
            else
            {
                toastBody.Controls.Add(toastDescription);
                toastBody.Controls.Add(toastTitle);
                toastBody.Controls.Add(toastIcon);
                toastBody.Controls.Add(toastDismissIcon);
            }

            return toastBody;
        }
        
        
      


        private Panel MakeToast(NOTIFICATION_TYPES type,PANEL_TYPES bodyType)
        {
            const int MARGIN_BOTTOM = 10;

            Panel Toast = getPanel();

            //Toast.Size = new Size(Toast.Size.Width, HEIGHT);
            Toast.AutoSize = true;
            Toast.Dock = System.Windows.Forms.DockStyle.Top;

            Panel toastBody = getToastBody(type, bodyType);

            Panel margingBottom = getPanel();
            margingBottom.Dock = DockStyle.Bottom;

            margingBottom.MinimumSize= new Size(0, MARGIN_BOTTOM);
            margingBottom.MaximumSize = new Size(0, MARGIN_BOTTOM);

            

            Toast.Controls.Add(toastBody);
            Toast.Controls.Add(margingBottom);


            //panel_Sidebar2.MouseEnter += new EventHandler(NotificationMouseEnter);
            //panel_Sidebar2.MouseLeave += new EventHandler(NotificationMouseLeave);
            ///-0>>>>>>>>>>>>>space






            //panel_Sidebar2.Controls.Add(label_Sidebar1);
            //panel_Sidebar2.Controls.Add(label_Sidebar);
            //panel_Sidebar2.Controls.Add(iconPictureBox_Sidebar);
            //panel_Sidebar2.Controls.Add(iconPictureBox_Sidebar1);

            return Toast;
        }

        private void generateNotificationControls(List<(NOTIFICATION_TYPES, PANEL_TYPES)> list)
        {
            for (int i = list.Count-1; i >= 0; i--)
            {
                panel4.Controls.Add(MakeToast(list[i].Item1, list[i].Item2));    
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.Width <= 555)
            {
                this.MinimumSize = new Size(555, this.Height);
            }
            ResponsiveEnd();
        }











        private void NotificationMouseEnter(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            p.BackColor = Color.FromArgb(43, 19, 114);
        }
        private void iconPictureBox_Sidebar1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Done");
        }

        private void NotificationMouseLeave(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            p.BackColor = Color.FromArgb(35, 16, 90);
        }

        private void iconPictureBox3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Doneee");
        }



        private void Bin_MouseEnter(object sender, EventArgs e)
        {
            IconPictureBox i = (IconPictureBox)sender;
            i.IconColor = Color.Red;
        }

        private void Bin_MouseLeave(object sender, EventArgs e)
        {
            IconPictureBox i = (IconPictureBox)sender;
            i.IconColor = Color.FromArgb(255, 61, 87);
        }

        //panel5.BackColor = Color.FromArgb(143, 73, 253);


    }
}
