namespace Lab_Basic_Command
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
			this.flpTables = new System.Windows.Forms.FlowLayoutPanel();
			this.cmsTableMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewBills = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewBillLog = new System.Windows.Forms.ToolStripMenuItem();
			this.btnAddTable = new System.Windows.Forms.Button();
			this.btnEditTable = new System.Windows.Forms.Button();
			this.btnDeleteTable = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.btnAccountManager = new System.Windows.Forms.Button();
			this.btnCategoryForm = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cmsTableMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// flpTables
			// 
			this.flpTables.ContextMenuStrip = this.cmsTableMenu;
			this.flpTables.Location = new System.Drawing.Point(6, 9);
			this.flpTables.Name = "flpTables";
			this.flpTables.Size = new System.Drawing.Size(422, 333);
			this.flpTables.TabIndex = 0;
			// 
			// cmsTableMenu
			// 
			this.cmsTableMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDelete,
            this.mnuViewBills,
            this.mnuViewBillLog});
			this.cmsTableMenu.Name = "cmsTableMenu";
			this.cmsTableMenu.Size = new System.Drawing.Size(203, 70);
			// 
			// mnuDelete
			// 
			this.mnuDelete.Name = "mnuDelete";
			this.mnuDelete.Size = new System.Drawing.Size(202, 22);
			this.mnuDelete.Text = "Xoá bàn";
			this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
			// 
			// mnuViewBills
			// 
			this.mnuViewBills.Name = "mnuViewBills";
			this.mnuViewBills.Size = new System.Drawing.Size(202, 22);
			this.mnuViewBills.Text = "Xem danh mục hoá đơn";
			this.mnuViewBills.Click += new System.EventHandler(this.mnuViewBills_Click);
			// 
			// mnuViewBillLog
			// 
			this.mnuViewBillLog.Name = "mnuViewBillLog";
			this.mnuViewBillLog.Size = new System.Drawing.Size(202, 22);
			this.mnuViewBillLog.Text = "Xem nhật ký hoá đơn";
			this.mnuViewBillLog.Click += new System.EventHandler(this.mnuViewBillLog_Click);
			// 
			// btnAddTable
			// 
			this.btnAddTable.Location = new System.Drawing.Point(443, 12);
			this.btnAddTable.Name = "btnAddTable";
			this.btnAddTable.Size = new System.Drawing.Size(75, 23);
			this.btnAddTable.TabIndex = 2;
			this.btnAddTable.Text = "Thêm bàn";
			this.btnAddTable.UseVisualStyleBackColor = true;
			this.btnAddTable.Click += new System.EventHandler(this.btnAddTable_Click);
			// 
			// btnEditTable
			// 
			this.btnEditTable.Location = new System.Drawing.Point(443, 56);
			this.btnEditTable.Name = "btnEditTable";
			this.btnEditTable.Size = new System.Drawing.Size(75, 23);
			this.btnEditTable.TabIndex = 3;
			this.btnEditTable.Text = "Cập nhật";
			this.btnEditTable.UseVisualStyleBackColor = true;
			this.btnEditTable.Click += new System.EventHandler(this.btnEditTable_Click);
			// 
			// btnDeleteTable
			// 
			this.btnDeleteTable.Location = new System.Drawing.Point(443, 100);
			this.btnDeleteTable.Name = "btnDeleteTable";
			this.btnDeleteTable.Size = new System.Drawing.Size(75, 23);
			this.btnDeleteTable.TabIndex = 4;
			this.btnDeleteTable.Text = "Xoá bàn";
			this.btnDeleteTable.UseVisualStyleBackColor = true;
			this.btnDeleteTable.Click += new System.EventHandler(this.btnDeleteTable_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(112, 364);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(10, 13);
			this.lblStatus.TabIndex = 5;
			this.lblStatus.Text = ".";
			// 
			// btnAccountManager
			// 
			this.btnAccountManager.Location = new System.Drawing.Point(443, 203);
			this.btnAccountManager.Name = "btnAccountManager";
			this.btnAccountManager.Size = new System.Drawing.Size(75, 23);
			this.btnAccountManager.TabIndex = 7;
			this.btnAccountManager.Text = "Tài khoản";
			this.btnAccountManager.UseVisualStyleBackColor = true;
			this.btnAccountManager.Click += new System.EventHandler(this.btnAccountManager_Click);
			// 
			// btnCategoryForm
			// 
			this.btnCategoryForm.Location = new System.Drawing.Point(443, 154);
			this.btnCategoryForm.Name = "btnCategoryForm";
			this.btnCategoryForm.Size = new System.Drawing.Size(75, 23);
			this.btnCategoryForm.TabIndex = 8;
			this.btnCategoryForm.Text = "Món ăn";
			this.btnCategoryForm.UseVisualStyleBackColor = true;
			this.btnCategoryForm.Click += new System.EventHandler(this.btnCategoryForm_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(38, 364);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Trạng thái:";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(542, 399);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCategoryForm);
			this.Controls.Add(this.btnAccountManager);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnDeleteTable);
			this.Controls.Add(this.btnEditTable);
			this.Controls.Add(this.btnAddTable);
			this.Controls.Add(this.flpTables);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.cmsTableMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flpTables;
		private System.Windows.Forms.ContextMenuStrip cmsTableMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuDelete;
		private System.Windows.Forms.ToolStripMenuItem mnuViewBills;
		private System.Windows.Forms.ToolStripMenuItem mnuViewBillLog;
		private System.Windows.Forms.Button btnAddTable;
		private System.Windows.Forms.Button btnEditTable;
		private System.Windows.Forms.Button btnDeleteTable;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Button btnAccountManager;
		private System.Windows.Forms.Button btnCategoryForm;
		private System.Windows.Forms.Label label1;
	}
}