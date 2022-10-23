using System.Windows.Forms;

namespace ANRPC_Inventory
{
    partial class MainAppForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainAppForm));
            this.container = new System.Windows.Forms.Panel();
            this.main = new System.Windows.Forms.Panel();
            this.formwraper = new System.Windows.Forms.Panel();
            this.appbar = new System.Windows.Forms.Panel();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.sidebar = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnSettings = new FontAwesome.Sharp.IconButton();
            this.btnReport = new FontAwesome.Sharp.IconButton();
            this.btnFinancial = new FontAwesome.Sharp.IconButton();
            this.btnStock = new FontAwesome.Sharp.IconButton();
            this.btnTransaction = new FontAwesome.Sharp.IconButton();
            this.btnOrder = new FontAwesome.Sharp.IconButton();
            this.btnCustomers = new FontAwesome.Sharp.IconButton();
            this.btnDashboard = new FontAwesome.Sharp.IconButton();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.container.SuspendLayout();
            this.main.SuspendLayout();
            this.appbar.SuspendLayout();
            this.sidebar.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // container
            // 
            this.container.Controls.Add(this.main);
            this.container.Controls.Add(this.appbar);
            this.container.Controls.Add(this.sidebar);
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(0, 0);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(929, 590);
            this.container.TabIndex = 0;
            // 
            // main
            // 
            this.main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(7)))), ((int)(((byte)(66)))));
            this.main.Controls.Add(this.formwraper);
            this.main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main.Location = new System.Drawing.Point(250, 60);
            this.main.Name = "main";
            this.main.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            this.main.Size = new System.Drawing.Size(679, 530);
            this.main.TabIndex = 1;
            // 
            // formwraper
            // 
            this.formwraper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formwraper.Location = new System.Drawing.Point(17, 0);
            this.formwraper.Name = "formwraper";
            this.formwraper.Padding = new System.Windows.Forms.Padding(0, 0, 17, 0);
            this.formwraper.Size = new System.Drawing.Size(662, 530);
            this.formwraper.TabIndex = 1;
            // 
            // appbar
            // 
            this.appbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(7)))), ((int)(((byte)(66)))));
            this.appbar.Controls.Add(this.iconButton1);
            this.appbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.appbar.Location = new System.Drawing.Point(250, 0);
            this.appbar.Name = "appbar";
            this.appbar.Padding = new System.Windows.Forms.Padding(5, 0, 0, 9);
            this.appbar.Size = new System.Drawing.Size(679, 60);
            this.appbar.TabIndex = 0;
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.Transparent;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.iconButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F, System.Drawing.FontStyle.Bold);
            this.iconButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(118)))), ((int)(((byte)(189)))));
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.iconButton1.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(133)))), ((int)(((byte)(222)))));
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 32;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.Location = new System.Drawing.Point(2, 3);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.iconButton1.Size = new System.Drawing.Size(250, 54);
            this.iconButton1.TabIndex = 1;
            this.iconButton1.Text = "   Dashboard";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = false;
            // 
            // sidebar
            // 
            this.sidebar.Controls.Add(this.panelButtons);
            this.sidebar.Controls.Add(this.panelLogo);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Location = new System.Drawing.Point(0, 0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(250, 590);
            this.sidebar.TabIndex = 0;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(16)))), ((int)(((byte)(90)))));
            this.panelButtons.Controls.Add(this.btnSettings);
            this.panelButtons.Controls.Add(this.btnReport);
            this.panelButtons.Controls.Add(this.btnFinancial);
            this.panelButtons.Controls.Add(this.btnStock);
            this.panelButtons.Controls.Add(this.btnTransaction);
            this.panelButtons.Controls.Add(this.btnOrder);
            this.panelButtons.Controls.Add(this.btnCustomers);
            this.panelButtons.Controls.Add(this.btnDashboard);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(0, 134);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(250, 456);
            this.panelButtons.TabIndex = 1;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F, System.Drawing.FontStyle.Bold);
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnSettings.IconChar = FontAwesome.Sharp.IconChar.Cog;
            this.btnSettings.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnSettings.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSettings.IconSize = 32;
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.Location = new System.Drawing.Point(0, 378);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnSettings.Size = new System.Drawing.Size(250, 54);
            this.btnSettings.TabIndex = 7;
            this.btnSettings.Text = "   Settings";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F, System.Drawing.FontStyle.Bold);
            this.btnReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnReport.IconChar = FontAwesome.Sharp.IconChar.PieChart;
            this.btnReport.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnReport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReport.IconSize = 32;
            this.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.Location = new System.Drawing.Point(0, 324);
            this.btnReport.Name = "btnReport";
            this.btnReport.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnReport.Size = new System.Drawing.Size(250, 54);
            this.btnReport.TabIndex = 6;
            this.btnReport.Text = "   Report";
            this.btnReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnFinancial
            // 
            this.btnFinancial.BackColor = System.Drawing.Color.Transparent;
            this.btnFinancial.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFinancial.FlatAppearance.BorderSize = 0;
            this.btnFinancial.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnFinancial.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnFinancial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinancial.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F, System.Drawing.FontStyle.Bold);
            this.btnFinancial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnFinancial.IconChar = FontAwesome.Sharp.IconChar.Wallet;
            this.btnFinancial.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnFinancial.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFinancial.IconSize = 32;
            this.btnFinancial.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFinancial.Location = new System.Drawing.Point(0, 270);
            this.btnFinancial.Name = "btnFinancial";
            this.btnFinancial.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnFinancial.Size = new System.Drawing.Size(250, 54);
            this.btnFinancial.TabIndex = 5;
            this.btnFinancial.Text = "   Financial";
            this.btnFinancial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFinancial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFinancial.UseVisualStyleBackColor = false;
            this.btnFinancial.Click += new System.EventHandler(this.btnFinancial_Click);
            // 
            // btnStock
            // 
            this.btnStock.BackColor = System.Drawing.Color.Transparent;
            this.btnStock.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStock.FlatAppearance.BorderSize = 0;
            this.btnStock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnStock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F, System.Drawing.FontStyle.Bold);
            this.btnStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnStock.IconChar = FontAwesome.Sharp.IconChar.Box;
            this.btnStock.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnStock.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnStock.IconSize = 32;
            this.btnStock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStock.Location = new System.Drawing.Point(0, 216);
            this.btnStock.Name = "btnStock";
            this.btnStock.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnStock.Size = new System.Drawing.Size(250, 54);
            this.btnStock.TabIndex = 4;
            this.btnStock.Text = "   Stock";
            this.btnStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStock.UseVisualStyleBackColor = false;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // btnTransaction
            // 
            this.btnTransaction.BackColor = System.Drawing.Color.Transparent;
            this.btnTransaction.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTransaction.FlatAppearance.BorderSize = 0;
            this.btnTransaction.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnTransaction.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F, System.Drawing.FontStyle.Bold);
            this.btnTransaction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnTransaction.IconChar = FontAwesome.Sharp.IconChar.ShoppingBag;
            this.btnTransaction.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnTransaction.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnTransaction.IconSize = 32;
            this.btnTransaction.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransaction.Location = new System.Drawing.Point(0, 162);
            this.btnTransaction.Name = "btnTransaction";
            this.btnTransaction.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnTransaction.Size = new System.Drawing.Size(250, 54);
            this.btnTransaction.TabIndex = 3;
            this.btnTransaction.Text = "   Transaction";
            this.btnTransaction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransaction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTransaction.UseVisualStyleBackColor = false;
            this.btnTransaction.Click += new System.EventHandler(this.btnTransaction_Click);
            // 
            // btnOrder
            // 
            this.btnOrder.BackColor = System.Drawing.Color.Transparent;
            this.btnOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOrder.FlatAppearance.BorderSize = 0;
            this.btnOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F, System.Drawing.FontStyle.Bold);
            this.btnOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnOrder.IconChar = FontAwesome.Sharp.IconChar.Truck;
            this.btnOrder.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnOrder.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnOrder.IconSize = 32;
            this.btnOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrder.Location = new System.Drawing.Point(0, 108);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnOrder.Size = new System.Drawing.Size(250, 54);
            this.btnOrder.TabIndex = 2;
            this.btnOrder.Text = "   Order";
            this.btnOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOrder.UseVisualStyleBackColor = false;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // btnCustomers
            // 
            this.btnCustomers.BackColor = System.Drawing.Color.Transparent;
            this.btnCustomers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCustomers.FlatAppearance.BorderSize = 0;
            this.btnCustomers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnCustomers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomers.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F, System.Drawing.FontStyle.Bold);
            this.btnCustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnCustomers.IconChar = FontAwesome.Sharp.IconChar.ArrowsSpin;
            this.btnCustomers.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnCustomers.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCustomers.IconSize = 32;
            this.btnCustomers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomers.Location = new System.Drawing.Point(0, 54);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnCustomers.Size = new System.Drawing.Size(250, 54);
            this.btnCustomers.TabIndex = 1;
            this.btnCustomers.Text = "   Cycle Details";
            this.btnCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCustomers.UseVisualStyleBackColor = false;
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.Transparent;
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F, System.Drawing.FontStyle.Bold);
            this.btnDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnDashboard.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.btnDashboard.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnDashboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDashboard.IconSize = 32;
            this.btnDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.Location = new System.Drawing.Point(0, 0);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnDashboard.Size = new System.Drawing.Size(250, 54);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "   Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(16)))), ((int)(((byte)(90)))));
            this.panelLogo.Controls.Add(this.pictureBox1);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(250, 134);
            this.panelLogo.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 134);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 590);
            this.Controls.Add(this.container);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.container.ResumeLayout(false);
            this.main.ResumeLayout(false);
            this.appbar.ResumeLayout(false);
            this.sidebar.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel container;
        private Panel main;
        private Panel formwraper;
        private Panel appbar;
        private Panel sidebar;
        private Panel panelButtons;
        private Panel panelLogo;
        private FontAwesome.Sharp.IconButton btnDashboard;
        private FontAwesome.Sharp.IconButton btnSettings;
        private FontAwesome.Sharp.IconButton btnReport;
        private FontAwesome.Sharp.IconButton btnFinancial;
        private FontAwesome.Sharp.IconButton btnStock;
        private FontAwesome.Sharp.IconButton btnTransaction;
        private FontAwesome.Sharp.IconButton btnOrder;
        private FontAwesome.Sharp.IconButton btnCustomers;
        private FontAwesome.Sharp.IconButton iconButton1;
        private PictureBox pictureBox1;
    }
}