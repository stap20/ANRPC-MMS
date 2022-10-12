namespace ANRPC_Inventory
{
    partial class FLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLogin));
            this.Password_label = new System.Windows.Forms.Label();
            this.user_txt = new System.Windows.Forms.ComboBox();
            this.password_txt = new System.Windows.Forms.TextBox();
            this.lgnBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MinimizeBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Password_label
            // 
            this.Password_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Password_label.AutoSize = true;
            this.Password_label.BackColor = System.Drawing.Color.Transparent;
            this.Password_label.Font = new System.Drawing.Font("Arabic Typesetting", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Password_label.ForeColor = System.Drawing.Color.Black;
            this.Password_label.Location = new System.Drawing.Point(232, 185);
            this.Password_label.Name = "Password_label";
            this.Password_label.Size = new System.Drawing.Size(123, 31);
            this.Password_label.TabIndex = 84;
            this.Password_label.Text = "خطأ فى رقم المرور*";
            // 
            // user_txt
            // 
            this.user_txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.user_txt.BackColor = System.Drawing.SystemColors.Control;
            this.user_txt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.user_txt.Font = new System.Drawing.Font("Arial", 13.3F);
            this.user_txt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.user_txt.Location = new System.Drawing.Point(45, 94);
            this.user_txt.Name = "user_txt";
            this.user_txt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.user_txt.Size = new System.Drawing.Size(310, 29);
            this.user_txt.TabIndex = 81;
            this.user_txt.Text = "User Name";
            this.user_txt.SelectedIndexChanged += new System.EventHandler(this.user_txt_SelectedIndexChanged);
            this.user_txt.Click += new System.EventHandler(this.user_txt_Click);
            this.user_txt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.user_txt_KeyPress);
            this.user_txt.Leave += new System.EventHandler(this.user_txt_Leave);
            // 
            // password_txt
            // 
            this.password_txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.password_txt.BackColor = System.Drawing.SystemColors.Control;
            this.password_txt.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_txt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.password_txt.Location = new System.Drawing.Point(45, 150);
            this.password_txt.Name = "password_txt";
            this.password_txt.Size = new System.Drawing.Size(310, 32);
            this.password_txt.TabIndex = 82;
            this.password_txt.Text = "Password";
            this.password_txt.Click += new System.EventHandler(this.password_txt_Click);
            this.password_txt.Leave += new System.EventHandler(this.password_txt_Leave);
            // 
            // lgnBtn
            // 
            this.lgnBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lgnBtn.BackColor = System.Drawing.Color.Red;
            this.lgnBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lgnBtn.BackgroundImage")));
            this.lgnBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lgnBtn.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.lgnBtn.FlatAppearance.BorderSize = 0;
            this.lgnBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.lgnBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lgnBtn.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lgnBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lgnBtn.Location = new System.Drawing.Point(45, 229);
            this.lgnBtn.Name = "lgnBtn";
            this.lgnBtn.Size = new System.Drawing.Size(310, 38);
            this.lgnBtn.TabIndex = 83;
            this.lgnBtn.Text = "دخول";
            this.lgnBtn.UseVisualStyleBackColor = false;
            this.lgnBtn.Click += new System.EventHandler(this.lgnBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.user_txt);
            this.panel1.Controls.Add(this.Password_label);
            this.panel1.Controls.Add(this.password_txt);
            this.panel1.Controls.Add(this.lgnBtn);
            this.panel1.Location = new System.Drawing.Point(532, 155);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 317);
            this.panel1.TabIndex = 85;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::ANRPC_Inventory.Properties.Resources.Anrpc;
            this.pictureBox1.Location = new System.Drawing.Point(142, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(110, 56);
            this.pictureBox1.TabIndex = 86;
            this.pictureBox1.TabStop = false;
            // 
            // MinimizeBtn
            // 
            this.MinimizeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizeBtn.BackColor = System.Drawing.Color.Transparent;
            this.MinimizeBtn.BackgroundImage = global::ANRPC_Inventory.Properties.Resources.minmize_btn___Copy;
            this.MinimizeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MinimizeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MinimizeBtn.FlatAppearance.BorderSize = 0;
            this.MinimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeBtn.Location = new System.Drawing.Point(1161, 3);
            this.MinimizeBtn.Name = "MinimizeBtn";
            this.MinimizeBtn.Size = new System.Drawing.Size(44, 36);
            this.MinimizeBtn.TabIndex = 89;
            this.MinimizeBtn.UseVisualStyleBackColor = false;
            this.MinimizeBtn.Click += new System.EventHandler(this.MinimizeBtn_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExitBtn.BackColor = System.Drawing.Color.Transparent;
            this.ExitBtn.BackgroundImage = global::ANRPC_Inventory.Properties.Resources.goExit___Copy1___Copy;
            this.ExitBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitBtn.FlatAppearance.BorderSize = 0;
            this.ExitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitBtn.Location = new System.Drawing.Point(1208, 3);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(44, 36);
            this.ExitBtn.TabIndex = 90;
            this.ExitBtn.UseVisualStyleBackColor = false;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // FLogin
            // 
            this.AcceptButton = this.lgnBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ANRPC_Inventory.Properties.Resources.bk3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1253, 573);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.MinimizeBtn);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FLogin";
            this.Text = "FLogin";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FLogin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Password_label;
        private System.Windows.Forms.ComboBox user_txt;
        private System.Windows.Forms.TextBox password_txt;
        private System.Windows.Forms.Button lgnBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button MinimizeBtn;
        private System.Windows.Forms.Button ExitBtn;
    }
}