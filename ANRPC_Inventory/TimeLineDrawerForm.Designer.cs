namespace ANRPC_Inventory
{
    partial class TimeLineDrawerForm
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
            this.formWraper = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.SuspendLayout();
            // 
            // formWraper
            // 
            this.formWraper.BackColor = System.Drawing.Color.White;
            this.formWraper.BorderRadius = 7;
            this.formWraper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formWraper.FillColor = System.Drawing.Color.White;
            this.formWraper.FillColor2 = System.Drawing.Color.White;
            this.formWraper.Location = new System.Drawing.Point(5, 5);
            this.formWraper.Margin = new System.Windows.Forms.Padding(0, 0, 20, 15);
            this.formWraper.Name = "formWraper";
            this.formWraper.ShadowDecoration.Parent = this.formWraper;
            this.formWraper.Size = new System.Drawing.Size(790, 440);
            this.formWraper.TabIndex = 8;
            this.formWraper.Paint += new System.Windows.Forms.PaintEventHandler(this.formWraper_Paint);
            // 
            // TimeLineDrawerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.formWraper);
            this.Name = "TimeLineDrawerForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "TimeLineDrawerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel formWraper;
    }
}