﻿using System.Windows.Forms;

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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.appbar = new System.Windows.Forms.Panel();
            this.btnMin = new FontAwesome.Sharp.IconButton();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.btnWindowReset = new FontAwesome.Sharp.IconButton();
            this.btn_max = new FontAwesome.Sharp.IconButton();
            this.sidebar = new System.Windows.Forms.Panel();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.btnRequestOrder = new FontAwesome.Sharp.IconButton();
            this.btnRequestDispatch = new FontAwesome.Sharp.IconButton();
            this.btnReport = new FontAwesome.Sharp.IconButton();
            this.btnTahwel = new FontAwesome.Sharp.IconButton();
            this.btnSearch = new FontAwesome.Sharp.IconButton();
            this.btnSettings = new FontAwesome.Sharp.IconButton();
            this.btnDashboard = new FontAwesome.Sharp.IconButton();
            this.container.SuspendLayout();
            this.main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.appbar.SuspendLayout();
            this.sidebar.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.guna2GradientPanel1.SuspendLayout();
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
            this.main.Padding = new System.Windows.Forms.Padding(17, 0, 17, 17);
            this.main.Size = new System.Drawing.Size(679, 530);
            this.main.TabIndex = 1;
            // 
            // formwraper
            // 
            this.formwraper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formwraper.Location = new System.Drawing.Point(17, 0);
            this.formwraper.Name = "formwraper";
            this.formwraper.Size = new System.Drawing.Size(645, 513);
            this.formwraper.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(232, 154);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // appbar
            // 
            this.appbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(7)))), ((int)(((byte)(66)))));
            this.appbar.Controls.Add(this.btnMin);
            this.appbar.Controls.Add(this.btnClose);
            this.appbar.Controls.Add(this.iconButton1);
            this.appbar.Controls.Add(this.btnWindowReset);
            this.appbar.Controls.Add(this.btn_max);
            this.appbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.appbar.Location = new System.Drawing.Point(250, 0);
            this.appbar.Name = "appbar";
            this.appbar.Padding = new System.Windows.Forms.Padding(5, 0, 0, 9);
            this.appbar.Size = new System.Drawing.Size(679, 60);
            this.appbar.TabIndex = 0;
            // 
            // btnMin
            // 
            this.btnMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btnMin.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            this.btnMin.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnMin.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMin.IconSize = 15;
            this.btnMin.Location = new System.Drawing.Point(569, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(55, 51);
            this.btnMin.TabIndex = 7;
            this.btnMin.UseVisualStyleBackColor = true;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Multiply;
            this.btnClose.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClose.IconSize = 20;
            this.btnClose.Location = new System.Drawing.Point(624, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(55, 51);
            this.btnClose.TabIndex = 4;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            // btnWindowReset
            // 
            this.btnWindowReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWindowReset.FlatAppearance.BorderSize = 0;
            this.btnWindowReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnWindowReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnWindowReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindowReset.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btnWindowReset.IconChar = FontAwesome.Sharp.IconChar.WindowRestore;
            this.btnWindowReset.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnWindowReset.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnWindowReset.IconSize = 15;
            this.btnWindowReset.Location = new System.Drawing.Point(454, 3);
            this.btnWindowReset.Name = "btnWindowReset";
            this.btnWindowReset.Size = new System.Drawing.Size(55, 43);
            this.btnWindowReset.TabIndex = 6;
            this.btnWindowReset.UseVisualStyleBackColor = true;
            this.btnWindowReset.Visible = false;
            this.btnWindowReset.Click += new System.EventHandler(this.btnWindowReset_Click);
            // 
            // btn_max
            // 
            this.btn_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_max.FlatAppearance.BorderSize = 0;
            this.btn_max.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btn_max.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btn_max.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_max.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btn_max.IconChar = FontAwesome.Sharp.IconChar.Square;
            this.btn_max.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btn_max.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_max.IconSize = 15;
            this.btn_max.Location = new System.Drawing.Point(458, 0);
            this.btn_max.Name = "btn_max";
            this.btn_max.Size = new System.Drawing.Size(55, 43);
            this.btn_max.TabIndex = 5;
            this.btn_max.UseVisualStyleBackColor = true;
            this.btn_max.Visible = false;
            this.btn_max.Click += new System.EventHandler(this.btn_max_Click);
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(7)))), ((int)(((byte)(66)))));
            this.sidebar.Controls.Add(this.guna2GradientPanel1);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Location = new System.Drawing.Point(0, 0);
            this.sidebar.Name = "sidebar";
            this.sidebar.Padding = new System.Windows.Forms.Padding(15, 17, 3, 17);
            this.sidebar.Size = new System.Drawing.Size(250, 590);
            this.sidebar.TabIndex = 0;
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(16)))), ((int)(((byte)(90)))));
            this.panelLogo.Controls.Add(this.pictureBox1);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 5);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(232, 154);
            this.panelLogo.TabIndex = 0;
            this.panelLogo.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLogo_Paint);
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(16)))), ((int)(((byte)(90)))));
            this.panelButtons.Controls.Add(this.btnSettings);
            this.panelButtons.Controls.Add(this.btnSearch);
            this.panelButtons.Controls.Add(this.btnTahwel);
            this.panelButtons.Controls.Add(this.btnReport);
            this.panelButtons.Controls.Add(this.btnRequestDispatch);
            this.panelButtons.Controls.Add(this.btnRequestOrder);
            this.panelButtons.Controls.Add(this.btnDashboard);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(0, 159);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(232, 392);
            this.panelButtons.TabIndex = 1;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BorderRadius = 7;
            this.guna2GradientPanel1.Controls.Add(this.panelButtons);
            this.guna2GradientPanel1.Controls.Add(this.panelLogo);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(16)))), ((int)(((byte)(90)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(16)))), ((int)(((byte)(90)))));
            this.guna2GradientPanel1.Location = new System.Drawing.Point(15, 17);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.guna2GradientPanel1.ShadowDecoration.Parent = this.guna2GradientPanel1;
            this.guna2GradientPanel1.Size = new System.Drawing.Size(232, 556);
            this.guna2GradientPanel1.TabIndex = 0;
            // 
            // btnRequestOrder
            // 
            this.btnRequestOrder.BackColor = System.Drawing.Color.Transparent;
            this.btnRequestOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRequestOrder.FlatAppearance.BorderSize = 0;
            this.btnRequestOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnRequestOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnRequestOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRequestOrder.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRequestOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnRequestOrder.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
            this.btnRequestOrder.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnRequestOrder.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRequestOrder.IconSize = 32;
            this.btnRequestOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRequestOrder.Location = new System.Drawing.Point(0, 54);
            this.btnRequestOrder.Name = "btnRequestOrder";
            this.btnRequestOrder.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnRequestOrder.Size = new System.Drawing.Size(232, 54);
            this.btnRequestOrder.TabIndex = 3;
            this.btnRequestOrder.Text = "    طلب التوريد";
            this.btnRequestOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRequestOrder.UseVisualStyleBackColor = false;
            this.btnRequestOrder.Click += new System.EventHandler(this.btnTransaction_Click);
            // 
            // btnRequestDispatch
            // 
            this.btnRequestDispatch.BackColor = System.Drawing.Color.Transparent;
            this.btnRequestDispatch.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRequestDispatch.FlatAppearance.BorderSize = 0;
            this.btnRequestDispatch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnRequestDispatch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnRequestDispatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRequestDispatch.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRequestDispatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnRequestDispatch.IconChar = FontAwesome.Sharp.IconChar.CartFlatbed;
            this.btnRequestDispatch.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnRequestDispatch.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRequestDispatch.IconSize = 35;
            this.btnRequestDispatch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRequestDispatch.Location = new System.Drawing.Point(0, 108);
            this.btnRequestDispatch.Name = "btnRequestDispatch";
            this.btnRequestDispatch.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnRequestDispatch.Size = new System.Drawing.Size(232, 54);
            this.btnRequestDispatch.TabIndex = 5;
            this.btnRequestDispatch.Text = "    إذن الصرف";
            this.btnRequestDispatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRequestDispatch.UseVisualStyleBackColor = false;
            this.btnRequestDispatch.Click += new System.EventHandler(this.btnFinancial_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnReport.IconChar = FontAwesome.Sharp.IconChar.ClipboardCheck;
            this.btnReport.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnReport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReport.IconSize = 32;
            this.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.Location = new System.Drawing.Point(0, 162);
            this.btnReport.Name = "btnReport";
            this.btnReport.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnReport.Size = new System.Drawing.Size(232, 54);
            this.btnReport.TabIndex = 6;
            this.btnReport.Text = "    المطابقة الفنية";
            this.btnReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnTahwel
            // 
            this.btnTahwel.BackColor = System.Drawing.Color.Transparent;
            this.btnTahwel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTahwel.FlatAppearance.BorderSize = 0;
            this.btnTahwel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnTahwel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnTahwel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTahwel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTahwel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnTahwel.IconChar = FontAwesome.Sharp.IconChar.DiagramPredecessor;
            this.btnTahwel.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnTahwel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnTahwel.IconSize = 32;
            this.btnTahwel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTahwel.Location = new System.Drawing.Point(0, 216);
            this.btnTahwel.Name = "btnTahwel";
            this.btnTahwel.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnTahwel.Size = new System.Drawing.Size(232, 54);
            this.btnTahwel.TabIndex = 8;
            this.btnTahwel.Text = "    إذون التحويل";
            this.btnTahwel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTahwel.UseVisualStyleBackColor = false;
            this.btnTahwel.Click += new System.EventHandler(this.btnTahwel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnSearch.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnSearch.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnSearch.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSearch.IconSize = 32;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(0, 270);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnSearch.Size = new System.Drawing.Size(232, 54);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "    البـــــحــــث";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnSettings.IconChar = FontAwesome.Sharp.IconChar.Cog;
            this.btnSettings.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnSettings.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSettings.IconSize = 32;
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.Location = new System.Drawing.Point(0, 324);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnSettings.Size = new System.Drawing.Size(232, 54);
            this.btnSettings.TabIndex = 7;
            this.btnSettings.Text = "    الإعدادت";
            this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.Transparent;
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(19)))), ((int)(((byte)(114)))));
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnDashboard.IconChar = FontAwesome.Sharp.IconChar.ChartSimple;
            this.btnDashboard.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(139)))), ((int)(((byte)(173)))));
            this.btnDashboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDashboard.IconSize = 32;
            this.btnDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.Location = new System.Drawing.Point(0, 0);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(0);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnDashboard.Size = new System.Drawing.Size(232, 54);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "    لوحة القيادة";
            this.btnDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // MainAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 590);
            this.Controls.Add(this.container);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainAppForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.container.ResumeLayout(false);
            this.main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.appbar.ResumeLayout(false);
            this.sidebar.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.guna2GradientPanel1.ResumeLayout(false);
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
        private FontAwesome.Sharp.IconButton iconButton1;
        private PictureBox pictureBox1;
        private FontAwesome.Sharp.IconButton btnMin;
        private FontAwesome.Sharp.IconButton btn_max;
        private FontAwesome.Sharp.IconButton btnClose;
        private FontAwesome.Sharp.IconButton btnWindowReset;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private FontAwesome.Sharp.IconButton btnSettings;
        private FontAwesome.Sharp.IconButton btnSearch;
        private FontAwesome.Sharp.IconButton btnTahwel;
        private FontAwesome.Sharp.IconButton btnReport;
        private FontAwesome.Sharp.IconButton btnRequestDispatch;
        private FontAwesome.Sharp.IconButton btnRequestOrder;
        private FontAwesome.Sharp.IconButton btnDashboard;
    }
}