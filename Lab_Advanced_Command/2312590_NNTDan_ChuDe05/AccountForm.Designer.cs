namespace _2312590_NNTDan_ChuDe05
{
    partial class AccountForm
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
            this.dgvDanhSachTaiKhoan = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsDanhSachVaiTro = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsNhatKy = new System.Windows.Forms.ToolStripMenuItem();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnThongKeHoaDon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachTaiKhoan)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDanhSachTaiKhoan
            // 
            this.dgvDanhSachTaiKhoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachTaiKhoan.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvDanhSachTaiKhoan.Location = new System.Drawing.Point(12, 48);
            this.dgvDanhSachTaiKhoan.Name = "dgvDanhSachTaiKhoan";
            this.dgvDanhSachTaiKhoan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachTaiKhoan.Size = new System.Drawing.Size(578, 371);
            this.dgvDanhSachTaiKhoan.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsDanhSachVaiTro,
            this.cmsNhatKy});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(213, 48);
            // 
            // cmsDanhSachVaiTro
            // 
            this.cmsDanhSachVaiTro.Name = "cmsDanhSachVaiTro";
            this.cmsDanhSachVaiTro.Size = new System.Drawing.Size(212, 22);
            this.cmsDanhSachVaiTro.Text = "Xem danh sách các vai trò";
            this.cmsDanhSachVaiTro.Click += new System.EventHandler(this.cmsDanhSachVaiTro_Click);
            // 
            // cmsNhatKy
            // 
            this.cmsNhatKy.Name = "cmsNhatKy";
            this.cmsNhatKy.Size = new System.Drawing.Size(212, 22);
            this.cmsNhatKy.Text = "Xem nhật ký hoạt động";
            this.cmsNhatKy.Click += new System.EventHandler(this.cmsNhatKy_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(96, 9);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 4;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(199, 9);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnThongKeHoaDon
            // 
            this.btnThongKeHoaDon.Location = new System.Drawing.Point(304, 9);
            this.btnThongKeHoaDon.Name = "btnThongKeHoaDon";
            this.btnThongKeHoaDon.Size = new System.Drawing.Size(117, 23);
            this.btnThongKeHoaDon.TabIndex = 5;
            this.btnThongKeHoaDon.Text = "Thong ke hoa don";
            this.btnThongKeHoaDon.UseVisualStyleBackColor = true;
            this.btnThongKeHoaDon.Click += new System.EventHandler(this.btnThongKeHoaDon_Click);
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 469);
            this.Controls.Add(this.btnThongKeHoaDon);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvDanhSachTaiKhoan);
            this.Name = "AccountForm";
            this.Text = "AccountForm";
            this.Load += new System.EventHandler(this.AccountForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachTaiKhoan)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDanhSachTaiKhoan;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmsDanhSachVaiTro;
        private System.Windows.Forms.ToolStripMenuItem cmsNhatKy;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnThongKeHoaDon;
    }
}