namespace ANRPC_Inventory
{
    partial class PDF_PopUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PDF_PopUp));
            this.Addbtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.OpenPDF_btn2 = new System.Windows.Forms.Button();
            this.PDFListBox1 = new System.Windows.Forms.ListBox();
            this.PDF_box = new System.Windows.Forms.ComboBox();
            this.OpenPDF_btn = new System.Windows.Forms.Button();
            this.PrintPdf_btn = new System.Windows.Forms.Button();
            this.delete_pdf = new System.Windows.Forms.Button();
            this.pdf_upload = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.axAcroPDF1 = new AxAcroPDFLib.AxAcroPDF();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).BeginInit();
            this.SuspendLayout();
            // 
            // Addbtn
            // 
            this.Addbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Addbtn.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Addbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Addbtn.FlatAppearance.BorderColor = System.Drawing.Color.DarkSalmon;
            this.Addbtn.FlatAppearance.BorderSize = 3;
            this.Addbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Addbtn.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Addbtn.ForeColor = System.Drawing.Color.Maroon;
            this.Addbtn.Location = new System.Drawing.Point(681, 345);
            this.Addbtn.Name = "Addbtn";
            this.Addbtn.Size = new System.Drawing.Size(115, 32);
            this.Addbtn.TabIndex = 17;
            this.Addbtn.Text = "إضافة";
            this.Addbtn.UseVisualStyleBackColor = false;
            this.Addbtn.Visible = false;
            this.Addbtn.Click += new System.EventHandler(this.Addbtn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Goldenrod;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.OpenPDF_btn2);
            this.panel2.Controls.Add(this.PDFListBox1);
            this.panel2.Controls.Add(this.PDF_box);
            this.panel2.Controls.Add(this.OpenPDF_btn);
            this.panel2.Controls.Add(this.PrintPdf_btn);
            this.panel2.Controls.Add(this.delete_pdf);
            this.panel2.Controls.Add(this.pdf_upload);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panel2.Size = new System.Drawing.Size(855, 153);
            this.panel2.TabIndex = 19;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(736, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 33);
            this.label1.TabIndex = 356;
            this.label1.Text = "المرفقات";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(201, 53);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 27);
            this.label14.TabIndex = 355;
            this.label14.Text = "عرض الملف";
            this.label14.Visible = false;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(271, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(110, 27);
            this.label13.TabIndex = 354;
            this.label13.Text = "طباعة الملف";
            this.label13.Visible = false;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(342, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 27);
            this.label12.TabIndex = 353;
            this.label12.Text = "فتح الملف";
            this.label12.Visible = false;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(406, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 27);
            this.label11.TabIndex = 352;
            this.label11.Text = "حذف";
            this.label11.Visible = false;
            // 
            // OpenPDF_btn2
            // 
            this.OpenPDF_btn2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenPDF_btn2.BackColor = System.Drawing.Color.Transparent;
            this.OpenPDF_btn2.FlatAppearance.BorderSize = 0;
            this.OpenPDF_btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenPDF_btn2.Font = new System.Drawing.Font("Arabic Typesetting", 24F);
            this.OpenPDF_btn2.Image = ((System.Drawing.Image)(resources.GetObject("OpenPDF_btn2.Image")));
            this.OpenPDF_btn2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OpenPDF_btn2.Location = new System.Drawing.Point(235, 84);
            this.OpenPDF_btn2.Name = "OpenPDF_btn2";
            this.OpenPDF_btn2.Size = new System.Drawing.Size(35, 52);
            this.OpenPDF_btn2.TabIndex = 351;
            this.OpenPDF_btn2.UseVisualStyleBackColor = false;
            this.OpenPDF_btn2.Click += new System.EventHandler(this.OpenPDF_btn2_Click);
            this.OpenPDF_btn2.MouseEnter += new System.EventHandler(this.OpenPDF_btn2_MouseEnter);
            this.OpenPDF_btn2.MouseLeave += new System.EventHandler(this.OpenPDF_btn2_MouseLeave);
            // 
            // PDFListBox1
            // 
            this.PDFListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PDFListBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PDFListBox1.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PDFListBox1.FormattingEnabled = true;
            this.PDFListBox1.ItemHeight = 23;
            this.PDFListBox1.Location = new System.Drawing.Point(21, 99);
            this.PDFListBox1.MultiColumn = true;
            this.PDFListBox1.Name = "PDFListBox1";
            this.PDFListBox1.Size = new System.Drawing.Size(183, 27);
            this.PDFListBox1.TabIndex = 350;
            this.PDFListBox1.Visible = false;
            // 
            // PDF_box
            // 
            this.PDF_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PDF_box.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PDF_box.FormattingEnabled = true;
            this.PDF_box.Location = new System.Drawing.Point(456, 94);
            this.PDF_box.Name = "PDF_box";
            this.PDF_box.Size = new System.Drawing.Size(258, 26);
            this.PDF_box.TabIndex = 349;
            this.PDF_box.SelectedIndexChanged += new System.EventHandler(this.PDF_box_SelectedIndexChanged);
            // 
            // OpenPDF_btn
            // 
            this.OpenPDF_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenPDF_btn.BackColor = System.Drawing.Color.Transparent;
            this.OpenPDF_btn.FlatAppearance.BorderSize = 0;
            this.OpenPDF_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenPDF_btn.Font = new System.Drawing.Font("Arabic Typesetting", 24F);
            this.OpenPDF_btn.Image = ((System.Drawing.Image)(resources.GetObject("OpenPDF_btn.Image")));
            this.OpenPDF_btn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OpenPDF_btn.Location = new System.Drawing.Point(359, 94);
            this.OpenPDF_btn.Name = "OpenPDF_btn";
            this.OpenPDF_btn.Size = new System.Drawing.Size(35, 41);
            this.OpenPDF_btn.TabIndex = 348;
            this.OpenPDF_btn.UseVisualStyleBackColor = false;
            this.OpenPDF_btn.Click += new System.EventHandler(this.OpenPDF_btn_Click);
            this.OpenPDF_btn.MouseEnter += new System.EventHandler(this.OpenPDF_btn_MouseEnter);
            this.OpenPDF_btn.MouseLeave += new System.EventHandler(this.OpenPDF_btn_MouseLeave);
            // 
            // PrintPdf_btn
            // 
            this.PrintPdf_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintPdf_btn.BackColor = System.Drawing.Color.Transparent;
            this.PrintPdf_btn.FlatAppearance.BorderSize = 0;
            this.PrintPdf_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrintPdf_btn.Font = new System.Drawing.Font("Arabic Typesetting", 24F);
            this.PrintPdf_btn.Image = ((System.Drawing.Image)(resources.GetObject("PrintPdf_btn.Image")));
            this.PrintPdf_btn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PrintPdf_btn.Location = new System.Drawing.Point(289, 90);
            this.PrintPdf_btn.Name = "PrintPdf_btn";
            this.PrintPdf_btn.Size = new System.Drawing.Size(56, 41);
            this.PrintPdf_btn.TabIndex = 347;
            this.PrintPdf_btn.UseVisualStyleBackColor = false;
            this.PrintPdf_btn.Click += new System.EventHandler(this.PrintPdf_btn_Click);
            this.PrintPdf_btn.MouseEnter += new System.EventHandler(this.PrintPdf_btn_MouseEnter);
            this.PrintPdf_btn.MouseLeave += new System.EventHandler(this.PrintPdf_btn_MouseLeave);
            // 
            // delete_pdf
            // 
            this.delete_pdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.delete_pdf.BackColor = System.Drawing.Color.Transparent;
            this.delete_pdf.FlatAppearance.BorderSize = 0;
            this.delete_pdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete_pdf.Font = new System.Drawing.Font("Arabic Typesetting", 24F);
            this.delete_pdf.Image = ((System.Drawing.Image)(resources.GetObject("delete_pdf.Image")));
            this.delete_pdf.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.delete_pdf.Location = new System.Drawing.Point(412, 84);
            this.delete_pdf.Name = "delete_pdf";
            this.delete_pdf.Size = new System.Drawing.Size(38, 47);
            this.delete_pdf.TabIndex = 346;
            this.delete_pdf.UseVisualStyleBackColor = false;
            this.delete_pdf.Visible = false;
            this.delete_pdf.Click += new System.EventHandler(this.delete_pdf_Click);
            this.delete_pdf.MouseEnter += new System.EventHandler(this.delete_pdf_MouseEnter);
            this.delete_pdf.MouseLeave += new System.EventHandler(this.delete_pdf_MouseLeave);
            // 
            // pdf_upload
            // 
            this.pdf_upload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pdf_upload.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pdf_upload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pdf_upload.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.pdf_upload.FlatAppearance.BorderSize = 2;
            this.pdf_upload.Font = new System.Drawing.Font("Arabic Typesetting", 24F);
            this.pdf_upload.ForeColor = System.Drawing.Color.DarkRed;
            this.pdf_upload.Image = ((System.Drawing.Image)(resources.GetObject("pdf_upload.Image")));
            this.pdf_upload.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.pdf_upload.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pdf_upload.Location = new System.Drawing.Point(720, 82);
            this.pdf_upload.Name = "pdf_upload";
            this.pdf_upload.Size = new System.Drawing.Size(123, 51);
            this.pdf_upload.TabIndex = 345;
            this.pdf_upload.Text = "PDF";
            this.pdf_upload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pdf_upload.UseVisualStyleBackColor = false;
            this.pdf_upload.Visible = false;
            this.pdf_upload.Click += new System.EventHandler(this.pdf_upload_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(149, 83);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 232;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.axAcroPDF1);
            this.panel1.Controls.Add(this.Addbtn);
            this.panel1.Location = new System.Drawing.Point(0, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(855, 436);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // axAcroPDF1
            // 
            this.axAcroPDF1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axAcroPDF1.Enabled = true;
            this.axAcroPDF1.Location = new System.Drawing.Point(0, 0);
            this.axAcroPDF1.Name = "axAcroPDF1";
            this.axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF1.OcxState")));
            this.axAcroPDF1.Size = new System.Drawing.Size(855, 436);
            this.axAcroPDF1.TabIndex = 20;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.BackColor = System.Drawing.Color.CornflowerBlue;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.FlatAppearance.BorderColor = System.Drawing.Color.DarkSalmon;
            this.CancelBtn.FlatAppearance.BorderSize = 3;
            this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelBtn.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.CancelBtn.ForeColor = System.Drawing.Color.Maroon;
            this.CancelBtn.Location = new System.Drawing.Point(372, 601);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(115, 35);
            this.CancelBtn.TabIndex = 19;
            this.CancelBtn.Text = "رجوع";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // PDF_PopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(855, 642);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PDF_PopUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TalbTawred";
            this.Load += new System.EventHandler(this.TalbTawred_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Addbtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button OpenPDF_btn2;
        private System.Windows.Forms.ListBox PDFListBox1;
        private System.Windows.Forms.ComboBox PDF_box;
        private System.Windows.Forms.Button OpenPDF_btn;
        private System.Windows.Forms.Button PrintPdf_btn;
        private System.Windows.Forms.Button delete_pdf;
        private System.Windows.Forms.Button pdf_upload;
        private AxAcroPDFLib.AxAcroPDF axAcroPDF1;
        private System.Windows.Forms.Label label1;
    }
}