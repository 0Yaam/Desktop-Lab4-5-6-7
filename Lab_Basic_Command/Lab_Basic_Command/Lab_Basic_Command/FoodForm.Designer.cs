namespace Lab_Basic_Command
{
    partial class FoodForm
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
			this.dgvFood = new System.Windows.Forms.DataGridView();
			this.FoodID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FoodName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FoodUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.GruopID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgvFood)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvFood
			// 
			this.dgvFood.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvFood.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvFood.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FoodID,
            this.FoodName,
            this.FoodUnit,
            this.GruopID,
            this.UnitPrice,
            this.Note});
			this.dgvFood.Location = new System.Drawing.Point(0, -1);
			this.dgvFood.Name = "dgvFood";
			this.dgvFood.Size = new System.Drawing.Size(645, 273);
			this.dgvFood.TabIndex = 0;
			// 
			// FoodID
			// 
			this.FoodID.HeaderText = "Mã món";
			this.FoodID.Name = "FoodID";
			// 
			// FoodName
			// 
			this.FoodName.HeaderText = "Tên món";
			this.FoodName.Name = "FoodName";
			// 
			// FoodUnit
			// 
			this.FoodUnit.HeaderText = "Đơn vị tính";
			this.FoodUnit.Name = "FoodUnit";
			// 
			// GruopID
			// 
			this.GruopID.HeaderText = "Mã nhóm";
			this.GruopID.Name = "GruopID";
			// 
			// UnitPrice
			// 
			this.UnitPrice.HeaderText = "Đơn giá";
			this.UnitPrice.Name = "UnitPrice";
			// 
			// Note
			// 
			this.Note.HeaderText = "Ghi chú";
			this.Note.Name = "Note";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(349, 291);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "Lưu";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(491, 291);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 2;
			this.btnDelete.Text = "Xoá";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// FoodForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(644, 335);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.dgvFood);
			this.Name = "FoodForm";
			this.Text = "FoodForm";
			this.Load += new System.EventHandler(this.FoodForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvFood)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFood;
        private System.Windows.Forms.DataGridViewTextBoxColumn FoodID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FoodName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FoodUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn GruopID;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnDelete;
	}
}