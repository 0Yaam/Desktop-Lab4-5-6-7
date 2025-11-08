namespace _2312590_NNTDan_Lab07
{
    partial class MainForm
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.btnAddFood = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnReloadFood = new System.Windows.Forms.Button();
            this.btnReloadCategory = new System.Windows.Forms.Button();
            this.tsmiThucAn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBan = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPay = new System.Windows.Forms.Button();
            this.btnPrintBill = new System.Windows.Forms.Button();
            this.dgvBillItems = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsXoaMonKhoiBan = new System.Windows.Forms.ToolStripMenuItem();
            this.lblSelectedTable = new System.Windows.Forms.Label();
            this.flpTables = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsNhapBan = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsChuyenBan = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lvwFood = new System.Windows.Forms.ListView();
            this.tvwCategory = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiQuanTri = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdmin_Roles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdmin_Accounts = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdmin_Foods = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdmin_Tables = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmThongTinNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddMonAn = new System.Windows.Forms.Button();
            this.lblTongTien = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillItems)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Location = new System.Drawing.Point(130, 26);
            this.btnAddCategory.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(29, 23);
            this.btnAddCategory.TabIndex = 21;
            this.btnAddCategory.Text = "+";
            this.toolTip1.SetToolTip(this.btnAddCategory, "Thêm danh mục mới");
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // btnAddFood
            // 
            this.btnAddFood.Location = new System.Drawing.Point(717, 26);
            this.btnAddFood.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddFood.Name = "btnAddFood";
            this.btnAddFood.Size = new System.Drawing.Size(29, 23);
            this.btnAddFood.TabIndex = 17;
            this.btnAddFood.Text = "+";
            this.toolTip1.SetToolTip(this.btnAddFood, "Thêm món ăn mới");
            this.btnAddFood.UseVisualStyleBackColor = true;
            this.btnAddFood.Click += new System.EventHandler(this.btnAddFood_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(683, 26);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(29, 23);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "-";
            this.toolTip1.SetToolTip(this.btnDelete, "Xóa món ăn được chọn");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnReloadFood
            // 
            this.btnReloadFood.Location = new System.Drawing.Point(649, 26);
            this.btnReloadFood.Margin = new System.Windows.Forms.Padding(2);
            this.btnReloadFood.Name = "btnReloadFood";
            this.btnReloadFood.Size = new System.Drawing.Size(29, 23);
            this.btnReloadFood.TabIndex = 20;
            this.btnReloadFood.Text = "R";
            this.toolTip1.SetToolTip(this.btnReloadFood, "Tải lại danh sách món ăn");
            this.btnReloadFood.UseVisualStyleBackColor = true;
            this.btnReloadFood.Click += new System.EventHandler(this.btnReloadFood_Click);
            // 
            // btnReloadCategory
            // 
            this.btnReloadCategory.Location = new System.Drawing.Point(97, 26);
            this.btnReloadCategory.Margin = new System.Windows.Forms.Padding(2);
            this.btnReloadCategory.Name = "btnReloadCategory";
            this.btnReloadCategory.Size = new System.Drawing.Size(29, 23);
            this.btnReloadCategory.TabIndex = 19;
            this.btnReloadCategory.Text = "R";
            this.toolTip1.SetToolTip(this.btnReloadCategory, "Tải lại danh mục");
            this.btnReloadCategory.UseVisualStyleBackColor = true;
            this.btnReloadCategory.Click += new System.EventHandler(this.btnReloadCategory_Click);
            // 
            // tsmiThucAn
            // 
            this.tsmiThucAn.Name = "tsmiThucAn";
            this.tsmiThucAn.Size = new System.Drawing.Size(75, 24);
            this.tsmiThucAn.Text = "Thức ăn";
            this.tsmiThucAn.Click += new System.EventHandler(this.tsmiThucAn_Click);
            // 
            // tsmiBan
            // 
            this.tsmiBan.Name = "tsmiBan";
            this.tsmiBan.Size = new System.Drawing.Size(48, 24);
            this.tsmiBan.Text = "Bàn";
            this.tsmiBan.Click += new System.EventHandler(this.tsmiBan_Click);
            // 
            // btnPay
            // 
            this.btnPay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPay.Location = new System.Drawing.Point(1051, 503);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(90, 30);
            this.btnPay.TabIndex = 28;
            this.btnPay.Text = "Thanh toán";
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // btnPrintBill
            // 
            this.btnPrintBill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrintBill.Location = new System.Drawing.Point(1051, 450);
            this.btnPrintBill.Name = "btnPrintBill";
            this.btnPrintBill.Size = new System.Drawing.Size(90, 30);
            this.btnPrintBill.TabIndex = 18;
            this.btnPrintBill.Text = "In hóa đơn";
            this.btnPrintBill.UseVisualStyleBackColor = true;
            this.btnPrintBill.Click += new System.EventHandler(this.btnPrintBill_Click);
            // 
            // dgvBillItems
            // 
            this.dgvBillItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBillItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBillItems.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvBillItems.Location = new System.Drawing.Point(170, 344);
            this.dgvBillItems.Name = "dgvBillItems";
            this.dgvBillItems.ReadOnly = true;
            this.dgvBillItems.RowHeadersWidth = 51;
            this.dgvBillItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBillItems.Size = new System.Drawing.Size(864, 189);
            this.dgvBillItems.TabIndex = 27;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsXoaMonKhoiBan});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(94, 26);
            // 
            // cmsXoaMonKhoiBan
            // 
            this.cmsXoaMonKhoiBan.Name = "cmsXoaMonKhoiBan";
            this.cmsXoaMonKhoiBan.Size = new System.Drawing.Size(93, 22);
            this.cmsXoaMonKhoiBan.Text = "xóa";
            this.cmsXoaMonKhoiBan.Click += new System.EventHandler(this.cmsXoaMonKhoiBan_Click);
            // 
            // lblSelectedTable
            // 
            this.lblSelectedTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSelectedTable.AutoSize = true;
            this.lblSelectedTable.Location = new System.Drawing.Point(169, 311);
            this.lblSelectedTable.Name = "lblSelectedTable";
            this.lblSelectedTable.Size = new System.Drawing.Size(41, 13);
            this.lblSelectedTable.TabIndex = 26;
            this.lblSelectedTable.Text = "Bàn: ...";
            // 
            // flpTables
            // 
            this.flpTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpTables.AutoScroll = true;
            this.flpTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpTables.ContextMenuStrip = this.contextMenuStrip2;
            this.flpTables.Location = new System.Drawing.Point(751, 53);
            this.flpTables.Name = "flpTables";
            this.flpTables.Size = new System.Drawing.Size(398, 253);
            this.flpTables.TabIndex = 25;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsNhapBan,
            this.cmsChuyenBan});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(139, 48);
            // 
            // cmsNhapBan
            // 
            this.cmsNhapBan.Name = "cmsNhapBan";
            this.cmsNhapBan.Size = new System.Drawing.Size(138, 22);
            this.cmsNhapBan.Text = "Nhập bàn";
            this.cmsNhapBan.Click += new System.EventHandler(this.cmsNhapBan_Click);
            // 
            // cmsChuyenBan
            // 
            this.cmsChuyenBan.Name = "cmsChuyenBan";
            this.cmsChuyenBan.Size = new System.Drawing.Size(138, 22);
            this.cmsChuyenBan.Text = "Chuyển bàn";
            this.cmsChuyenBan.Click += new System.EventHandler(this.cmsChuyenBan_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Thực đơn";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Danh mục";
            // 
            // lvwFood
            // 
            this.lvwFood.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwFood.CheckBoxes = true;
            this.lvwFood.FullRowSelect = true;
            this.lvwFood.GridLines = true;
            this.lvwFood.HideSelection = false;
            this.lvwFood.Location = new System.Drawing.Point(168, 53);
            this.lvwFood.Margin = new System.Windows.Forms.Padding(2);
            this.lvwFood.Name = "lvwFood";
            this.lvwFood.Size = new System.Drawing.Size(578, 253);
            this.lvwFood.TabIndex = 14;
            this.lvwFood.UseCompatibleStateImageBehavior = false;
            this.lvwFood.View = System.Windows.Forms.View.Details;
            // 
            // tvwCategory
            // 
            this.tvwCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvwCategory.Location = new System.Drawing.Point(6, 53);
            this.tvwCategory.Margin = new System.Windows.Forms.Padding(2);
            this.tvwCategory.Name = "tvwCategory";
            this.tvwCategory.Size = new System.Drawing.Size(153, 480);
            this.tvwCategory.TabIndex = 13;
            this.tvwCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwCategory_AfterSelect);
            this.tvwCategory.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwCategory_NodeMouseDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiQuanTri,
            this.tsmThongTinNhanVien,
            this.tsmiHoaDon,
            this.tsmiMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1161, 24);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiQuanTri
            // 
            this.tsmiQuanTri.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdmin_Roles,
            this.tsmiAdmin_Accounts,
            this.tsmiAdmin_Foods,
            this.tsmiAdmin_Tables});
            this.tsmiQuanTri.Name = "tsmiQuanTri";
            this.tsmiQuanTri.Size = new System.Drawing.Size(62, 20);
            this.tsmiQuanTri.Text = "Quản trị";
            // 
            // tsmiAdmin_Roles
            // 
            this.tsmiAdmin_Roles.Name = "tsmiAdmin_Roles";
            this.tsmiAdmin_Roles.Size = new System.Drawing.Size(124, 22);
            this.tsmiAdmin_Roles.Text = "Vai trò";
            this.tsmiAdmin_Roles.Click += new System.EventHandler(this.tsmiVaiTro_Click_1);
            // 
            // tsmiAdmin_Accounts
            // 
            this.tsmiAdmin_Accounts.Name = "tsmiAdmin_Accounts";
            this.tsmiAdmin_Accounts.Size = new System.Drawing.Size(124, 22);
            this.tsmiAdmin_Accounts.Text = "Tài khoản";
            this.tsmiAdmin_Accounts.Click += new System.EventHandler(this.tsmiAdmin_Accounts_Click);
            // 
            // tsmiAdmin_Foods
            // 
            this.tsmiAdmin_Foods.Name = "tsmiAdmin_Foods";
            this.tsmiAdmin_Foods.Size = new System.Drawing.Size(124, 22);
            this.tsmiAdmin_Foods.Text = "Thức ăn";
            this.tsmiAdmin_Foods.Click += new System.EventHandler(this.tsmiThucAn_Click);
            // 
            // tsmiAdmin_Tables
            // 
            this.tsmiAdmin_Tables.Name = "tsmiAdmin_Tables";
            this.tsmiAdmin_Tables.Size = new System.Drawing.Size(124, 22);
            this.tsmiAdmin_Tables.Text = "Bàn";
            this.tsmiAdmin_Tables.Click += new System.EventHandler(this.tsmiBan_Click);
            // 
            // tsmThongTinNhanVien
            // 
            this.tsmThongTinNhanVien.Name = "tsmThongTinNhanVien";
            this.tsmThongTinNhanVien.Size = new System.Drawing.Size(70, 20);
            this.tsmThongTinNhanVien.Text = "Thông tin";
            this.tsmThongTinNhanVien.Click += new System.EventHandler(this.tsmThongTinNhanVien_Click);
            // 
            // tsmiHoaDon
            // 
            this.tsmiHoaDon.Name = "tsmiHoaDon";
            this.tsmiHoaDon.Size = new System.Drawing.Size(65, 20);
            this.tsmiHoaDon.Text = "Hóa đơn";
            this.tsmiHoaDon.Click += new System.EventHandler(this.tsmiHoaDon_Click);
            // 
            // tsmiMenu
            // 
            this.tsmiMenu.Name = "tsmiMenu";
            this.tsmiMenu.Size = new System.Drawing.Size(50, 20);
            this.tsmiMenu.Text = "Menu";
            this.tsmiMenu.Click += new System.EventHandler(this.tsmiMenu_Click);
            // 
            // btnAddMonAn
            // 
            this.btnAddMonAn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddMonAn.Location = new System.Drawing.Point(681, 311);
            this.btnAddMonAn.Name = "btnAddMonAn";
            this.btnAddMonAn.Size = new System.Drawing.Size(65, 30);
            this.btnAddMonAn.TabIndex = 30;
            this.btnAddMonAn.Text = "Thêm";
            this.btnAddMonAn.UseVisualStyleBackColor = true;
            this.btnAddMonAn.Click += new System.EventHandler(this.btnAddMonAn_Click);
            // 
            // lblTongTien
            // 
            this.lblTongTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Location = new System.Drawing.Point(169, 560);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(55, 13);
            this.lblTongTien.TabIndex = 31;
            this.lblTongTien.Text = "Tổng tiền:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 589);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.btnAddMonAn);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.btnPrintBill);
            this.Controls.Add(this.dgvBillItems);
            this.Controls.Add(this.lblSelectedTable);
            this.Controls.Add(this.flpTables);
            this.Controls.Add(this.btnAddCategory);
            this.Controls.Add(this.btnAddFood);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnReloadFood);
            this.Controls.Add(this.btnReloadCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvwFood);
            this.Controls.Add(this.tvwCategory);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mainform";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillItems)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiThucAn;
        private System.Windows.Forms.ToolStripMenuItem tsmiBan;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.Button btnPrintBill;
        private System.Windows.Forms.DataGridView dgvBillItems;
        private System.Windows.Forms.Label lblSelectedTable;
        private System.Windows.Forms.FlowLayoutPanel flpTables;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Button btnAddFood;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnReloadFood;
        private System.Windows.Forms.Button btnReloadCategory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvwFood;
        private System.Windows.Forms.TreeView tvwCategory;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiQuanTri;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdmin_Roles;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdmin_Accounts;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdmin_Foods;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdmin_Tables;
        private System.Windows.Forms.ToolStripMenuItem tsmiHoaDon;
        private System.Windows.Forms.ToolStripMenuItem tsmThongTinNhanVien;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.Button btnAddMonAn;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmsXoaMonKhoiBan;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem cmsNhapBan;
        private System.Windows.Forms.ToolStripMenuItem cmsChuyenBan;
    }
}

