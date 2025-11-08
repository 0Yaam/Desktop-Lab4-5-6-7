namespace RestaurantManagementProject
{
    partial class InHoaDon
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
            this.lvHoaDon = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.nudGiamGia = new System.Windows.Forms.NumericUpDown();
            this.btnHoanThanh = new System.Windows.Forms.Button();
            this.lblBan = new System.Windows.Forms.Label();
            this.lblNgayIn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiamGia)).BeginInit();
            this.SuspendLayout();
            // 
            // lvHoaDon
            // 
            this.lvHoaDon.HideSelection = false;
            this.lvHoaDon.Location = new System.Drawing.Point(12, 26);
            this.lvHoaDon.Name = "lvHoaDon";
            this.lvHoaDon.Size = new System.Drawing.Size(820, 305);
            this.lvHoaDon.TabIndex = 0;
            this.lvHoaDon.UseCompatibleStateImageBehavior = false;
            this.lvHoaDon.SelectedIndexChanged += new System.EventHandler(this.lvHoaDon_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(644, 351);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Giảm giá";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(644, 378);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tổng tiền";
            // 
            // txtTongTien
            // 
            this.txtTongTien.Location = new System.Drawing.Point(712, 374);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.Size = new System.Drawing.Size(120, 20);
            this.txtTongTien.TabIndex = 7;
            this.txtTongTien.TextChanged += new System.EventHandler(this.txtTongTien_TextChanged);
            // 
            // nudGiamGia
            // 
            this.nudGiamGia.Location = new System.Drawing.Point(712, 348);
            this.nudGiamGia.Name = "nudGiamGia";
            this.nudGiamGia.Size = new System.Drawing.Size(120, 20);
            this.nudGiamGia.TabIndex = 9;
            this.nudGiamGia.ValueChanged += new System.EventHandler(this.nudGiamGia_ValueChanged);
            // 
            // btnHoanThanh
            // 
            this.btnHoanThanh.Location = new System.Drawing.Point(757, 400);
            this.btnHoanThanh.Name = "btnHoanThanh";
            this.btnHoanThanh.Size = new System.Drawing.Size(75, 23);
            this.btnHoanThanh.TabIndex = 10;
            this.btnHoanThanh.Text = "Hoàn thành";
            this.btnHoanThanh.UseVisualStyleBackColor = true;
            this.btnHoanThanh.Click += new System.EventHandler(this.btnHoanThanh_Click);
            // 
            // lblBan
            // 
            this.lblBan.AutoSize = true;
            this.lblBan.Location = new System.Drawing.Point(12, 9);
            this.lblBan.Name = "lblBan";
            this.lblBan.Size = new System.Drawing.Size(26, 13);
            this.lblBan.TabIndex = 11;
            this.lblBan.Text = "Bàn";
            // 
            // lblNgayIn
            // 
            this.lblNgayIn.AutoSize = true;
            this.lblNgayIn.Location = new System.Drawing.Point(114, 9);
            this.lblNgayIn.Name = "lblNgayIn";
            this.lblNgayIn.Size = new System.Drawing.Size(43, 13);
            this.lblNgayIn.TabIndex = 12;
            this.lblNgayIn.Text = "Ngày in";
            // 
            // InHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 439);
            this.Controls.Add(this.lblNgayIn);
            this.Controls.Add(this.lblBan);
            this.Controls.Add(this.btnHoanThanh);
            this.Controls.Add(this.nudGiamGia);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvHoaDon);
            this.Name = "InHoaDon";
            this.Text = "InHoaDon";
            ((System.ComponentModel.ISupportInitialize)(this.nudGiamGia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvHoaDon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.NumericUpDown nudGiamGia;
        private System.Windows.Forms.Button btnHoanThanh;
        private System.Windows.Forms.Label lblBan;
        private System.Windows.Forms.Label lblNgayIn;
    }
}