namespace _2312590_NNTDan_ChuDe05
{
    partial class OrderDetailsForm
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
            this.lvDSMatHang = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOrderID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvDSMatHang
            // 
            this.lvDSMatHang.HideSelection = false;
            this.lvDSMatHang.Location = new System.Drawing.Point(12, 35);
            this.lvDSMatHang.Name = "lvDSMatHang";
            this.lvDSMatHang.Size = new System.Drawing.Size(704, 263);
            this.lvDSMatHang.TabIndex = 0;
            this.lvDSMatHang.UseCompatibleStateImageBehavior = false;
            this.lvDSMatHang.SelectedIndexChanged += new System.EventHandler(this.lvDSMatHang_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Order ID: ";
            // 
            // lblOrderID
            // 
            this.lblOrderID.AutoSize = true;
            this.lblOrderID.Location = new System.Drawing.Point(71, 9);
            this.lblOrderID.Name = "lblOrderID";
            this.lblOrderID.Size = new System.Drawing.Size(16, 13);
            this.lblOrderID.TabIndex = 2;
            this.lblOrderID.Text = "...";
            this.lblOrderID.Click += new System.EventHandler(this.lblOrderID_Click);
            // 
            // OrderDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 331);
            this.Controls.Add(this.lblOrderID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvDSMatHang);
            this.Name = "OrderDetailsForm";
            this.Text = "OrderDetailsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvDSMatHang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOrderID;
    }
}