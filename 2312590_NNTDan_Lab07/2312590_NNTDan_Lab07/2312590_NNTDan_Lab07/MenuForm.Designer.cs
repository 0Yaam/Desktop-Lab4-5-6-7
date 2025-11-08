namespace _2312590_NNTDan_Lab07
{
    partial class MenuForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbbTypeFilter;
        private System.Windows.Forms.ComboBox cbbCategoryFilter;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.FlowLayoutPanel flpMenu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cbbTypeFilter = new System.Windows.Forms.ComboBox();
            this.cbbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblType = new System.Windows.Forms.Label();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.flpMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCategory = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbbTypeFilter
            // 
            this.cbbTypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTypeFilter.Location = new System.Drawing.Point(60, 12);
            this.cbbTypeFilter.Name = "cbbTypeFilter";
            this.cbbTypeFilter.Size = new System.Drawing.Size(100, 23);
            this.cbbTypeFilter.TabIndex = 0;
            // 
            // cbbCategoryFilter
            // 
            this.cbbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCategoryFilter.Location = new System.Drawing.Point(235, 12);
            this.cbbCategoryFilter.Name = "cbbCategoryFilter";
            this.cbbCategoryFilter.Size = new System.Drawing.Size(140, 23);
            this.cbbCategoryFilter.TabIndex = 1;
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(455, 12);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(160, 23);
            this.txtKeyword.TabIndex = 2;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(625, 10);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(60, 23);
            this.btnFilter.TabIndex = 3;
            this.btnFilter.Text = "Lọc";
            this.btnFilter.UseCompatibleTextRendering = true;
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1021, 610);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Ðóng";
            this.btnClose.UseCompatibleTextRendering = true;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 15);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(27, 21);
            this.lblType.TabIndex = 9;
            this.lblType.Text = "Loại";
            this.lblType.UseCompatibleTextRendering = true;
            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(390, 15);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(0, 18);
            this.lblKeyword.TabIndex = 7;
            this.lblKeyword.UseCompatibleTextRendering = true;
            // 
            // flpMenu
            // 
            this.flpMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpMenu.AutoScroll = true;
            this.flpMenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMenu.Location = new System.Drawing.Point(12, 45);
            this.flpMenu.Name = "flpMenu";
            this.flpMenu.Size = new System.Drawing.Size(1084, 555);
            this.flpMenu.TabIndex = 4;
            this.flpMenu.WrapContents = false;
            this.flpMenu.SizeChanged += new System.EventHandler(this.flpMenu_SizeChanged);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(170, 15);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(60, 21);
            this.lblCategory.TabIndex = 8;
            this.lblCategory.Text = "Danh mục";
            this.lblCategory.UseCompatibleTextRendering = true;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 646);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.flpMenu);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.cbbCategoryFilter);
            this.Controls.Add(this.cbbTypeFilter);
            this.Controls.Add(this.lblKeyword);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblType);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Category";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblCategory;
    }
}
