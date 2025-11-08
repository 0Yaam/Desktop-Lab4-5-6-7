namespace _2312590_NNTDan_ChuDe05
{
    partial class frmThemMonAn
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
            this.btnThemMonAnMoi = new System.Windows.Forms.Button();
            this.txtTenMonAnMoi = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnThemMonAnMoi
            // 
            this.btnThemMonAnMoi.Location = new System.Drawing.Point(306, 12);
            this.btnThemMonAnMoi.Name = "btnThemMonAnMoi";
            this.btnThemMonAnMoi.Size = new System.Drawing.Size(75, 23);
            this.btnThemMonAnMoi.TabIndex = 0;
            this.btnThemMonAnMoi.Text = "button1";
            this.btnThemMonAnMoi.UseVisualStyleBackColor = true;
            this.btnThemMonAnMoi.Click += new System.EventHandler(this.btnThemMonAnMoi_Click);
            // 
            // txtTenMonAnMoi
            // 
            this.txtTenMonAnMoi.Location = new System.Drawing.Point(21, 15);
            this.txtTenMonAnMoi.Name = "txtTenMonAnMoi";
            this.txtTenMonAnMoi.Size = new System.Drawing.Size(252, 20);
            this.txtTenMonAnMoi.TabIndex = 1;
            // 
            // frmThemMonAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 59);
            this.Controls.Add(this.txtTenMonAnMoi);
            this.Controls.Add(this.btnThemMonAnMoi);
            this.Name = "frmThemMonAn";
            this.Text = "frmThemMonAn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnThemMonAnMoi;
        private System.Windows.Forms.TextBox txtTenMonAnMoi;
    }
}