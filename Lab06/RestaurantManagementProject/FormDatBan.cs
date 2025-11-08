using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic;
using DataAccess.OL;

namespace RestaurantManagementProject
{
    public partial class FormDatBan : Form
    {
        private readonly BanBL _banBL = new BanBL();
        private readonly FoodBL _foodBL = new FoodBL();
        private readonly CategoryBL _catBL = new CategoryBL();
        private readonly BillsBL _billsBL = new BillsBL();
        private readonly BillDetailsBL _detailsBL = new BillDetailsBL();

        private bool _loadingCbb = false;

        private List<Ban> _allTables = new List<Ban>();
        private List<Food> _allFoods = new List<Food>();
        private List<Category> _allCats = new List<Category>();

        private int _selectedTableId = 0;
        private Ban _selectedBan = null;

        private int _currentBillId = 0;           
        private string _currentUser = "system";  

        public FormDatBan()
        {
            InitializeComponent();

            InitFoodsGrid();        
            InitBillItemsListView();   

            LoadTablesToFLP();
            LoadCategoriesToCbb();
            LoadFoodsToGrid();        
            RefreshTongTien();
        }

        public FormDatBan(string accountName) : this()
        {
            if (!string.IsNullOrWhiteSpace(accountName))
            {
                _currentUser = accountName;
            }
        }

        private void InitFoodsGrid()
        {
            dgvDSMonAn.AutoGenerateColumns = false;
            dgvDSMonAn.AllowUserToAddRows = false;
            dgvDSMonAn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDSMonAn.MultiSelect = false;
            dgvDSMonAn.RowHeadersVisible = false;
            dgvDSMonAn.Columns.Clear();

            var colChk = new DataGridViewCheckBoxColumn { Name = "Chon", HeaderText = "", Width = 32 };

            var colName = new DataGridViewTextBoxColumn { Name = "TenMon", HeaderText = "Tên món", ReadOnly = true, Width = 200 };

            var colUnit = new DataGridViewTextBoxColumn { Name = "DVT", HeaderText = "ĐVT", ReadOnly = true, Width = 80 };

            var colPrice = new DataGridViewTextBoxColumn
            {
                Name = "Gia",
                HeaderText = "Giá",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            };

            var colNotes = new DataGridViewTextBoxColumn
            {
                Name = "GhiChu",
                HeaderText = "Ghi chú",
                ReadOnly = true,
                Width = 200
            };

            var colQty = new DataGridViewTextBoxColumn
            {
                Name = "SoLuong",
                HeaderText = "SL",
                Width = 60,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };

            var colId = new DataGridViewTextBoxColumn { Name = "FoodID", HeaderText = "FoodID", Visible = false };

            dgvDSMonAn.Columns.AddRange(colChk, colName, colUnit, colPrice, colNotes, colQty, colId);

            dgvDSMonAn.CellValidating += (s, e) =>
            {
                if (dgvDSMonAn.Columns[e.ColumnIndex].Name == "SoLuong")
                {
                    string value = string.Empty;
                    if (e.FormattedValue != null)
                    {
                        value = e.FormattedValue.ToString();
                        if (value != null) value = value.Trim();
                    }

                    int qtyParsed;
                    if (string.IsNullOrEmpty(value) || !int.TryParse(value, out qtyParsed) || qtyParsed < 1)
                    {
                        e.Cancel = true;
                        MessageBox.Show("Số lượng phải là số nguyên ≥ 1.", "Lỗi nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            };

            dgvDSMonAn.RowsAdded += (s, e) =>
            {
                for (int i = 0; i < e.RowCount; i++)
                {
                    var row = dgvDSMonAn.Rows[e.RowIndex + i];
                    if (row.Cells["SoLuong"].Value == null)
                    {
                        row.Cells["SoLuong"].Value = 1;
                    }
                }
            };
        }

        private void InitBillItemsListView()
        {
            lvMonAnTheoBan.View = View.Details;
            lvMonAnTheoBan.FullRowSelect = true;
            lvMonAnTheoBan.GridLines = true;
            lvMonAnTheoBan.CheckBoxes = false;
            lvMonAnTheoBan.Columns.Clear();

            lvMonAnTheoBan.Columns.Add("Tên món", 110);                                // index 0
            lvMonAnTheoBan.Columns.Add("ĐVT", 80);                                     // index 1
            lvMonAnTheoBan.Columns.Add("Giá", 100, HorizontalAlignment.Right);         // index 2
            lvMonAnTheoBan.Columns.Add("Ghi chú", 200);                                // index 3
            lvMonAnTheoBan.Columns.Add("Số lượng", 80, HorizontalAlignment.Center);    // index 4
        }

        private void LoadTablesToFLP()
        {
            flpDSBan.Controls.Clear();
            _allTables = _banBL.GetAll();

            foreach (var t in _allTables)
            {
                var btn = new Button();

                if (!string.IsNullOrEmpty(t.Name))
                {
                    btn.Text = t.Name;
                }
                else
                {
                    btn.Text = "Bàn " + t.ID;
                }

                btn.Tag = t;  
                btn.Width = 90;
                btn.Height = 60;
                btn.Margin = new Padding(6);

                btn.BackColor = (t.Status == 1)
                    ? Color.LightSalmon   
                    : Color.LightGreen;    

                btn.Click += OnTableButtonClick;
                flpDSBan.Controls.Add(btn);
            }
        }

        private void OnTableButtonClick(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            _selectedBan = btn.Tag as Ban;
            if (_selectedBan == null) return;

            _selectedTableId = _selectedBan.ID;

            if (!string.IsNullOrEmpty(_selectedBan.Name))
            {
                lblSoBan.Text = _selectedBan.Name.Trim();
            }
            else
            {
                lblSoBan.Text = "Bàn " + _selectedBan.ID;
            }

            var openBills = _billsBL.GetOpenByTable(_selectedTableId);
            var openBill = openBills.OrderByDescending(b => b.ID).FirstOrDefault();

            if (openBill != null)
            {
                _currentBillId = openBill.ID;
            }
            else
            {
                _currentBillId = 0;
            }

            LoadBillDetailsToListView();
            RefreshTongTien();
        }

        private void LoadBillDetailsToListView()
        {
            lvMonAnTheoBan.Items.Clear();
            if (_currentBillId == 0) return;

            var allDetails = _detailsBL.GetByBill(_currentBillId);
            if (_allFoods.Count == 0)
            {
                _allFoods = _foodBL.GetAll();
            }

            foreach (var d in allDetails)
            {
                var food = _allFoods.FirstOrDefault(f => f.ID == d.FoodID);
                if (food == null) continue;

                var it = new ListViewItem(food.Name);
                it.SubItems.Add(food.Unit);
                it.SubItems.Add(food.Price.ToString());

                string notes = "";
                if (food.Notes != null) notes = food.Notes;
                it.SubItems.Add(notes);

                it.SubItems.Add(d.Quantity.ToString());
                it.Tag = food.ID;
                lvMonAnTheoBan.Items.Add(it);
            }
        }

        private void LoadCategoriesToCbb()
        {
            _loadingCbb = true;

            _allCats = _catBL.GetAll();
            var src = new List<Category>();
            src.Add(new Category { ID = 0, Name = "Tất cả", Type = 0 }); 
            src.AddRange(_allCats);

            cbbCategory.DisplayMember = "Name";
            cbbCategory.ValueMember = "ID";
            cbbCategory.DataSource = src;

            cbbCategory.SelectedValue = 0;

            _loadingCbb = false;
        }

        private void LoadFoodsToGrid(int categoryId = 0)
        {
            dgvDSMonAn.Rows.Clear();
            _allFoods = _foodBL.GetAll();

            List<Food> foods;
            if (categoryId == 0)
            {
                foods = _allFoods;
            }
            else
            {
                foods = _allFoods.Where(f => f.FoodCategoryID == categoryId).ToList();
            }

            foreach (var f in foods)
            {
                string notes = "";
                if (f.Notes != null) notes = f.Notes;

                dgvDSMonAn.Rows.Add(false, f.Name, f.Unit, f.Price, notes, 1, f.ID);
            }
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (_selectedTableId == 0)
            {
                MessageBox.Show("Hãy chọn bàn trước.", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (_currentBillId == 0)
            {
                var newBill = new Bills
                {
                    ID = 0,
                    Name = "Bill_Ban_" + _selectedTableId + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    TableID = _selectedTableId,
                    Amount = 0,
                    Discount = null,
                    Tax = null,
                    Status = false,  
                    CheckoutDate = null,
                    Account = _currentUser
                };
                _currentBillId = _billsBL.Insert(newBill);

                if (_selectedBan != null)
                {
                    _selectedBan.Status = 1;
                    _banBL.Update(_selectedBan); 
                    LoadTablesToFLP(); 
                }
            }

            bool any = false;

            foreach (DataGridViewRow row in dgvDSMonAn.Rows)
            {
                bool chon = false;
                object chonVal = row.Cells["Chon"].Value;
                if (chonVal is bool)
                {
                    chon = (bool)chonVal;
                }
                if (!chon) continue;
                any = true;

                int foodId = Convert.ToInt32(row.Cells["FoodID"].Value);
                int price = Convert.ToInt32(row.Cells["Gia"].Value);
                int qty = 1;

                string cellQty = null;
                if (row.Cells["SoLuong"].Value != null)
                {
                    cellQty = row.Cells["SoLuong"].Value.ToString();
                }
                int qParsed;
                if (!string.IsNullOrWhiteSpace(cellQty) && int.TryParse(cellQty, out qParsed) && qParsed > 0)
                {
                    qty = qParsed;
                }

                var detail = new BillDetails
                {
                    ID = 0,
                    InvoiceID = _currentBillId,
                    FoodID = foodId,
                    Quantity = qty
                };
                _detailsBL.Insert(detail); // insert

                // Cập nhật UI (lvMonAnTheoBan)
                var food = _allFoods.FirstOrDefault(f => f.ID == foodId);
                if (food != null)
                {
                    string notes = "";
                    if (food.Notes != null) notes = food.Notes;

                    var line = new ListViewItem(food.Name);
                    line.SubItems.Add(food.Unit);
                    line.SubItems.Add(price.ToString());
                    line.SubItems.Add(notes);
                    line.SubItems.Add(qty.ToString());
                    line.Tag = food.ID;
                    lvMonAnTheoBan.Items.Add(line);
                }

                row.Cells["Chon"].Value = false;
                row.Cells["SoLuong"].Value = 1;
            }

            if (!any)
            {
                MessageBox.Show("Hãy tick chọn món cần thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            RefreshTongTien();
        }

        private void RefreshTongTien()
        {
            long total = 0;
            foreach (ListViewItem it in lvMonAnTheoBan.Items)
            {
                int price = 0;
                int qty = 0;

                int.TryParse(it.SubItems[2].Text, out price);
                int.TryParse(it.SubItems[4].Text, out qty);

                total += (long)price * qty;
            }
            txtTongTien.Text = total.ToString("#,0");
        }

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loadingCbb) return;
            if (cbbCategory.SelectedValue == null)
            {
                LoadFoodsToGrid(0);
                return;
            }

            if (cbbCategory.SelectedValue is System.Data.DataRowView)
            {
                return;  
            }

            int catId;
            try
            {
                catId = Convert.ToInt32(cbbCategory.SelectedValue);
            }
            catch
            {
                catId = 0;
            }

            LoadFoodsToGrid(catId);
        }



        private void btnResetCBB_Click(object sender, EventArgs e)
        {
            _loadingCbb = true;
            cbbCategory.SelectedValue = 0;
            _loadingCbb = false;
            LoadFoodsToGrid(0);
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (_selectedTableId == 0 || _currentBillId == 0)
            {
                MessageBox.Show("Chưa có bill đang mở cho bàn này.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string tableName = "Bàn " + _selectedTableId;
            if (_selectedBan != null && !string.IsNullOrEmpty(_selectedBan.Name))
            {
                tableName = _selectedBan.Name;
            }

            using (var f = new InHoaDon(_currentBillId, _selectedTableId, tableName))
            {
                var dialogResult = f.ShowDialog(this);

                if (f.DaHoanThanh)
                {
                    _currentBillId = 0;
                    lvMonAnTheoBan.Items.Clear();
                    RefreshTongTien();
                    LoadTablesToFLP();
                    MessageBox.Show("Thanh toán thành công.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void lvMonAnTheoBan_DoubleClick(object sender, EventArgs e)
        {
            if (lvMonAnTheoBan.SelectedItems.Count == 0 || _currentBillId == 0) return;

            var it = lvMonAnTheoBan.SelectedItems[0];
            if (it.Tag == null) return;

            int foodId = (int)it.Tag;

            int currentQty = int.Parse(it.SubItems[4].Text);
            int newQty = currentQty + 1;
            it.SubItems[4].Text = newQty.ToString();

            var detail = _detailsBL.GetByBill(_currentBillId)
                .Where(d => d.FoodID == foodId)
                .OrderByDescending(d => d.ID)
                .FirstOrDefault();

            if (detail != null)
            {
                detail.Quantity = newQty;
                _detailsBL.Update(detail);
            }

            RefreshTongTien();
        }

        private void cmsXoa_Click(object sender, EventArgs e)
        {
            if (lvMonAnTheoBan.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn dòng cần xoá.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xoá món đã chọn?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (_currentBillId == 0)
            {
                foreach (ListViewItem it in lvMonAnTheoBan.SelectedItems)
                {
                    lvMonAnTheoBan.Items.Remove(it);
                }

                RefreshTongTien();
                return;
            }

            var billDetails = _detailsBL.GetByBill(_currentBillId).ToList();

            foreach (ListViewItem it in lvMonAnTheoBan.SelectedItems)
            {
                if (it.Tag == null) continue;
                int foodId = (int)it.Tag;

                int qty = 1;
                int.TryParse(it.SubItems[4].Text, out qty);

                var toDelete = billDetails
                    .Where(d => d.FoodID == foodId && d.Quantity == qty)
                    .OrderByDescending(d => d.ID)
                    .FirstOrDefault();

                if (toDelete == null)
                {
                    toDelete = billDetails
                        .Where(d => d.FoodID == foodId)
                        .OrderByDescending(d => d.ID)
                        .FirstOrDefault();
                }

                if (toDelete != null)
                {
                    _detailsBL.Delete(new BillDetails
                    {
                        ID = toDelete.ID,
                        InvoiceID = toDelete.InvoiceID,
                        FoodID = toDelete.FoodID,
                        Quantity = toDelete.Quantity
                    });

                    billDetails.Remove(toDelete);
                }

                lvMonAnTheoBan.Items.Remove(it);
            }

            RefreshTongTien();
        }

        private void flpDSBan_Paint(object sender, PaintEventArgs e) { }
        private void lvMonAnTheoBan_SelectedIndexChanged(object sender, EventArgs e) { }
        private void lblSoBan_Click(object sender, EventArgs e) { }
        private void lvDSMonAn_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtTongTien_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void btnInHoaDon_Click(object sender, EventArgs e) { }

        private void dgvDSMonAn_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnHieuChinh_Click(object sender, EventArgs e)
        {
            using (var f = new frmFood())
            {
                var dialog = f.ShowDialog(this);

                if (dialog == DialogResult.OK)
                {
                    if (f.FoodsChanged)
                        LoadFoodsToGrid();     

                    if (f.TablesChanged)
                        LoadTablesToFLP();      
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn đăng xuất không?",
                             "Đăng xuất", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                var login = new frmLogin();
                login.Show();
            }
        }
    }
}
