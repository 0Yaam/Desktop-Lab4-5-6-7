namespace _2312590_NNTDan_ChuDe05
{
    partial class FoodForm_Load
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
            this.components = new System.ComponentModel.Container();
            this.cbbCategory = new System.Windows.Forms.ComboBox();
            this.dgvFoodList = new System.Windows.Forms.DataGridView();
            this.ctmFoodList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsTinhSoLuongDaban = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsThemMonAnMoi = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCapNhatMonAn = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCatName = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTimKiemTheoTen = new System.Windows.Forms.TextBox();
            this.btnThongKeHoaDon = new System.Windows.Forms.Button();
            this.btnQuanLyTaiKhoan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoodList)).BeginInit();
            this.ctmFoodList.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbbCategory
            // 
            this.cbbCategory.FormattingEnabled = true;
            this.cbbCategory.Location = new System.Drawing.Point(105, 12);
            this.cbbCategory.Name = "cbbCategory";
            this.cbbCategory.Size = new System.Drawing.Size(121, 21);
            this.cbbCategory.TabIndex = 0;
            this.cbbCategory.SelectedIndexChanged += new System.EventHandler(this.cbbCategory_SelectedIndexChanged);
            // 
            // dgvFoodList
            // 
            this.dgvFoodList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFoodList.ContextMenuStrip = this.ctmFoodList;
            this.dgvFoodList.Location = new System.Drawing.Point(12, 39);
            this.dgvFoodList.Name = "dgvFoodList";
            this.dgvFoodList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFoodList.Size = new System.Drawing.Size(622, 288);
            this.dgvFoodList.TabIndex = 1;
            // 
            // ctmFoodList
            // 
            this.ctmFoodList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsTinhSoLuongDaban,
            this.cmsThemMonAnMoi,
            this.cmsCapNhatMonAn});
            this.ctmFoodList.Name = "ctmFoodList";
            this.ctmFoodList.Size = new System.Drawing.Size(186, 70);
            // 
            // cmsTinhSoLuongDaban
            // 
            this.cmsTinhSoLuongDaban.Name = "cmsTinhSoLuongDaban";
            this.cmsTinhSoLuongDaban.Size = new System.Drawing.Size(185, 22);
            this.cmsTinhSoLuongDaban.Text = "Tính số lượng đã bán";
            this.cmsTinhSoLuongDaban.Click += new System.EventHandler(this.cmsTinhSoLuongDaban_Click);
            // 
            // cmsThemMonAnMoi
            // 
            this.cmsThemMonAnMoi.Name = "cmsThemMonAnMoi";
            this.cmsThemMonAnMoi.Size = new System.Drawing.Size(185, 22);
            this.cmsThemMonAnMoi.Text = "Thêm món ăn mới";
            this.cmsThemMonAnMoi.Click += new System.EventHandler(this.cmsThemMonAnMoi_Click);
            // 
            // cmsCapNhatMonAn
            // 
            this.cmsCapNhatMonAn.Name = "cmsCapNhatMonAn";
            this.cmsCapNhatMonAn.Size = new System.Drawing.Size(185, 22);
            this.cmsCapNhatMonAn.Text = "Cập nhật món ăn";
            this.cmsCapNhatMonAn.Click += new System.EventHandler(this.cmsCapNhatMonAn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 356);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // lblCatName
            // 
            this.lblCatName.AutoSize = true;
            this.lblCatName.Location = new System.Drawing.Point(309, 356);
            this.lblCatName.Name = "lblCatName";
            this.lblCatName.Size = new System.Drawing.Size(16, 13);
            this.lblCatName.TabIndex = 3;
            this.lblCatName.Text = "...";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(119, 356);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(16, 13);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(215, 356);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Chọn món ăn";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(325, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tìm kiến theo tên";
            // 
            // txtTimKiemTheoTen
            // 
            this.txtTimKiemTheoTen.Location = new System.Drawing.Point(430, 12);
            this.txtTimKiemTheoTen.Name = "txtTimKiemTheoTen";
            this.txtTimKiemTheoTen.Size = new System.Drawing.Size(175, 20);
            this.txtTimKiemTheoTen.TabIndex = 8;
            this.txtTimKiemTheoTen.TextChanged += new System.EventHandler(this.txtTimKiemTheoTen_TextChanged);
            // 
            // btnThongKeHoaDon
            // 
            this.btnThongKeHoaDon.Location = new System.Drawing.Point(521, 351);
            this.btnThongKeHoaDon.Name = "btnThongKeHoaDon";
            this.btnThongKeHoaDon.Size = new System.Drawing.Size(113, 23);
            this.btnThongKeHoaDon.TabIndex = 9;
            this.btnThongKeHoaDon.Text = "Thống kê hóa đơn";
            this.btnThongKeHoaDon.UseVisualStyleBackColor = true;
            this.btnThongKeHoaDon.Click += new System.EventHandler(this.btnThongKeHoaDon_Click);
            // 
            // btnQuanLyTaiKhoan
            // 
            this.btnQuanLyTaiKhoan.Location = new System.Drawing.Point(521, 397);
            this.btnQuanLyTaiKhoan.Name = "btnQuanLyTaiKhoan";
            this.btnQuanLyTaiKhoan.Size = new System.Drawing.Size(113, 23);
            this.btnQuanLyTaiKhoan.TabIndex = 10;
            this.btnQuanLyTaiKhoan.Text = "QL Tai khoan";
            this.btnQuanLyTaiKhoan.UseVisualStyleBackColor = true;
            this.btnQuanLyTaiKhoan.Click += new System.EventHandler(this.btnQuanLyTaiKhoan_Click);
            // 
            // FoodForm_Load
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnQuanLyTaiKhoan);
            this.Controls.Add(this.btnThongKeHoaDon);
            this.Controls.Add(this.txtTimKiemTheoTen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.lblCatName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvFoodList);
            this.Controls.Add(this.cbbCategory);
            this.Name = "FoodForm_Load";
            this.Text = "Danh sách món ăn";
            this.Load += new System.EventHandler(this.FoodForm_Load_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoodList)).EndInit();
            this.ctmFoodList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbCategory;
        private System.Windows.Forms.DataGridView dgvFoodList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCatName;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip ctmFoodList;
        private System.Windows.Forms.ToolStripMenuItem cmsTinhSoLuongDaban;
        private System.Windows.Forms.ToolStripMenuItem cmsThemMonAnMoi;
        private System.Windows.Forms.ToolStripMenuItem cmsCapNhatMonAn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTimKiemTheoTen;
        private System.Windows.Forms.Button btnThongKeHoaDon;
        private System.Windows.Forms.Button btnQuanLyTaiKhoan;
    }
}

