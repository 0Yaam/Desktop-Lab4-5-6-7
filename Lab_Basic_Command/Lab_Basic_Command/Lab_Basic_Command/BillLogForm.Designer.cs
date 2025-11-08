namespace Lab_Basic_Command
{
	partial class BillLogForm
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
			this.dgvBillLog = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblCount = new System.Windows.Forms.Label();
			this.lblTotal = new System.Windows.Forms.Label();
			this.lblDiscount = new System.Windows.Forms.Label();
			this.lblFinal = new System.Windows.Forms.Label();
			this.btnReload = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgvBillLog)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvBillLog
			// 
			this.dgvBillLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvBillLog.Location = new System.Drawing.Point(4, 6);
			this.dgvBillLog.Name = "dgvBillLog";
			this.dgvBillLog.Size = new System.Drawing.Size(445, 182);
			this.dgvBillLog.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(32, 223);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Tổng số hoá đơn:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(32, 262);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(107, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Tổng tiền chưa giảm:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(32, 301);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Tổng giảm giá:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(32, 345);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Thực thu:";
			// 
			// lblCount
			// 
			this.lblCount.AutoSize = true;
			this.lblCount.Location = new System.Drawing.Point(166, 223);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(10, 13);
			this.lblCount.TabIndex = 5;
			this.lblCount.Text = ".";
			// 
			// lblTotal
			// 
			this.lblTotal.AutoSize = true;
			this.lblTotal.Location = new System.Drawing.Point(166, 262);
			this.lblTotal.Name = "lblTotal";
			this.lblTotal.Size = new System.Drawing.Size(10, 13);
			this.lblTotal.TabIndex = 6;
			this.lblTotal.Text = ".";
			// 
			// lblDiscount
			// 
			this.lblDiscount.AutoSize = true;
			this.lblDiscount.Location = new System.Drawing.Point(166, 301);
			this.lblDiscount.Name = "lblDiscount";
			this.lblDiscount.Size = new System.Drawing.Size(10, 13);
			this.lblDiscount.TabIndex = 7;
			this.lblDiscount.Text = ".";
			// 
			// lblFinal
			// 
			this.lblFinal.AutoSize = true;
			this.lblFinal.Location = new System.Drawing.Point(166, 345);
			this.lblFinal.Name = "lblFinal";
			this.lblFinal.Size = new System.Drawing.Size(10, 13);
			this.lblFinal.TabIndex = 8;
			this.lblFinal.Text = ".";
			// 
			// btnReload
			// 
			this.btnReload.Location = new System.Drawing.Point(313, 379);
			this.btnReload.Name = "btnReload";
			this.btnReload.Size = new System.Drawing.Size(110, 23);
			this.btnReload.TabIndex = 9;
			this.btnReload.Text = "Tải lại danh sách";
			this.btnReload.UseVisualStyleBackColor = true;
			this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
			// 
			// BillLogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(460, 410);
			this.Controls.Add(this.btnReload);
			this.Controls.Add(this.lblFinal);
			this.Controls.Add(this.lblDiscount);
			this.Controls.Add(this.lblTotal);
			this.Controls.Add(this.lblCount);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dgvBillLog);
			this.Name = "BillLogForm";
			this.Text = "BillLogForm";
			this.Load += new System.EventHandler(this.BillLogForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvBillLog)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvBillLog;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.Label lblTotal;
		private System.Windows.Forms.Label lblDiscount;
		private System.Windows.Forms.Label lblFinal;
		private System.Windows.Forms.Button btnReload;
	}
}