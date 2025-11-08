using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic;
using DataAccess.OL;

namespace RestaurantManagementProject
{
    public partial class frmFood : Form
    {
        public bool TablesChanged { get; private set; } = false;
        public bool FoodsChanged { get; private set; } = false;
        private List<Food> _viewFoods = new List<Food>();  

        private List<Category> listCategory = new List<Category>();
        private List<Food> listFood = new List<Food>();
        private Food foodCurrent = null;

        private bool _loadingCbb = false;

        public frmFood()
        {
            InitializeComponent();
            InitListViewColumns();
            LoadCategory();                
            LoadFoodDataToListView();     
        }

        private void frmFood_Load(object sender, EventArgs e)
        {
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtUnit.Clear();
            txtPrice.Clear();
            txtNotes.Clear();

            _loadingCbb = true;
            cbbCategory.SelectedIndex = -1;  
            _loadingCbb = false;

            foodCurrent = null;
        }

        private void InitListViewColumns()
        {
            if (lsvFood == null) return;

            lsvFood.Items.Clear();
            lsvFood.Columns.Clear();
            lsvFood.View = View.Details;
            lsvFood.FullRowSelect = true;
            lsvFood.GridLines = true;

            lsvFood.Columns.Add("STT", 60);
            lsvFood.Columns.Add("Tên món", 180);
            lsvFood.Columns.Add("ĐVT", 80);
            lsvFood.Columns.Add("Giá", 100, HorizontalAlignment.Right);
            lsvFood.Columns.Add("Nhóm", 150);
            lsvFood.Columns.Add("Ghi chú", 220);
        }

        private void LoadCategory()
        {
            _loadingCbb = true;

            var categoryBL = new CategoryBL();
            listCategory = categoryBL.GetAll();
            if (listCategory == null) listCategory = new List<Category>();

            cbbCategory.DisplayMember = "Name";
            cbbCategory.ValueMember = "ID";
            cbbCategory.DataSource = listCategory;

            cbbCategory.SelectedIndex = -1;

            _loadingCbb = false;
        }

        public void LoadFoodDataToListView(int categoryId = 0)
        {
            if (lsvFood == null) return;
            lsvFood.Items.Clear();

            var foodBL = new FoodBL();
            listFood = foodBL.GetAll() ?? new List<Food>();

            IEnumerable<Food> foods = listFood;
            if (categoryId != 0)
            {
                foods = listFood.Where(f =>
                {
                    int fid = 0;
                    try { fid = Convert.ToInt32(f.FoodCategoryID); } catch { fid = 0; }
                    return fid == categoryId;
                });
            }

            _viewFoods = foods.ToList();

            if (listCategory == null) listCategory = new List<Category>();
            if (listCategory.Count == 0)
            {
                var catBL = new CategoryBL();
                var cats = catBL.GetAll();
                if (cats != null) listCategory = cats;
            }
            var catMap = listCategory.ToDictionary(c => c.ID, c => c?.Name ?? "", comparer: EqualityComparer<int>.Default);

            int stt = 1;
            foreach (var food in _viewFoods)
            {
                if (food == null) continue;

                var item = lsvFood.Items.Add(stt.ToString());
                item.SubItems.Add(food.Name ?? "");
                item.SubItems.Add(food.Unit ?? "");
                item.SubItems.Add(food.Price.ToString());

                int fid = 0; try { fid = Convert.ToInt32(food.FoodCategoryID); } catch { fid = 0; }
                string catName;
                if (!catMap.TryGetValue(fid, out catName)) catName = "(Không có nhóm)";
                item.SubItems.Add(catName ?? "");
                item.SubItems.Add(food.Notes ?? "");

                item.Tag = food;  
                stt++;
            }
        }

        private void lsvFood_Click(object sender, EventArgs e)
        {
            if (lsvFood == null || lsvFood.SelectedItems.Count == 0) return;

            var li = lsvFood.SelectedItems[0];
            var f = li.Tag as Food;          
            if (f == null) return;
            foodCurrent = f;

            txtName.Text = f.Name ?? "";
            txtUnit.Text = f.Unit ?? "";
            txtPrice.Text = f.Price.ToString();
            txtNotes.Text = f.Notes ?? "";

            int catId = 0; try { catId = Convert.ToInt32(f.FoodCategoryID); } catch { catId = 0; }

            _loadingCbb = true;
            int foundIndex = -1;
            for (int i = 0; i < listCategory.Count; i++)
            {
                if (listCategory[i] != null && listCategory[i].ID == catId)
                {
                    foundIndex = i; break;
                }
            }
            cbbCategory.SelectedIndex = foundIndex;    
            _loadingCbb = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int result = InsertFood();
            if (result >= 0)
            {
                MessageBox.Show("Thêm dữ liệu thành công!");
                LoadFoodDataToListView();   
                FoodsChanged = true;
            }
            else
            {
                MessageBox.Show("Thêm dữ liệu không thành công. Vui lòng kiểm tra lại dữ liệu nhập!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (foodCurrent == null)
            {
                MessageBox.Show("Hãy chọn món cần xoá.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa món này?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var foodBL = new FoodBL();
                if (foodBL.Delete(foodCurrent) >= 0)
                {
                    MessageBox.Show("Xóa thực phẩm thành công!");
                    LoadFoodDataToListView();
                    FoodsChanged = true;
                }
                else
                {
                    MessageBox.Show("Xóa không thành công!");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int result = UpdateFood();
            if (result >= 0)
            {
                MessageBox.Show("Cập nhật dữ liệu thành công!");
                LoadFoodDataToListView();
                FoodsChanged = true;
            }
            else
            {
                MessageBox.Show("Cập nhật dữ liệu không thành công. Vui lòng kiểm tra lại dữ liệu nhập!");
            }
        }

        public int UpdateFood()
        {
            if (foodCurrent == null)
            {
                MessageBox.Show("Chưa chọn món để cập nhật.");
                return -1;
            }

            if (txtName.Text == "" || txtUnit.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Chưa nhập đủ dữ liệu cho các ô, vui lòng nhập lại!");
                return -1;
            }

            foodCurrent.Name = txtName.Text;
            foodCurrent.Unit = txtUnit.Text;
            foodCurrent.Notes = txtNotes.Text;

            int price = 0;
            try { price = int.Parse(txtPrice.Text); } catch { price = 0; }
            foodCurrent.Price = price;

            int catId = GetSelectedCategoryId();
            if (catId <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhóm món trong combobox.");
                return -1;
            }
            foodCurrent.FoodCategoryID = catId;

            var foodBL = new FoodBL();
            return foodBL.Update(foodCurrent);
        }

        public int InsertFood()
        {
            if (txtName.Text == "" || txtUnit.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Chưa nhập đủ dữ liệu cho các ô, vui lòng nhập lại!");
                return -1;
            }

            var food = new Food();
            food.ID = 0;
            food.Name = txtName.Text;
            food.Unit = txtUnit.Text;
            food.Notes = txtNotes.Text;

            int price = 0;
            try { price = int.Parse(txtPrice.Text); } catch { price = 0; }
            food.Price = price;

            int catId = GetSelectedCategoryId();
            if (catId <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhóm món trong combobox.");
                return -1;
            }
            food.FoodCategoryID = catId;

            var foodBL = new FoodBL();
            return foodBL.Insert(food);
        }

        private int GetSelectedCategoryId()
        {
            if (cbbCategory == null) return 0;
            if (cbbCategory.SelectedIndex < 0) return 0;

            var cat = cbbCategory.SelectedItem as Category;
            if (cat != null) return cat.ID;

            try
            {
                if (cbbCategory.SelectedValue != null)
                    return Convert.ToInt32(cbbCategory.SelectedValue);
            }
            catch { }

            return 0;
        }

        private void btnThemBan_Click(object sender, EventArgs e)
        {
            int soBan;
            if (!int.TryParse(txtThemBan.Text.Trim(), out soBan) || soBan <= 0)
            {
                MessageBox.Show("Số bàn phải là số nguyên > 0.", "Lỗi nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var banBL = new BanBL();
            int added = 0;

            for (int i = 0; i < soBan; i++)
            {
                var b = new Ban { ID = 0, Name = null, Status = 0, Capacity = 4 };
                int newId = banBL.Insert(b);

                if (newId > 0)
                {
                    b.ID = newId;
                    b.Name = "Bàn " + newId;
                    banBL.Update(b);
                    added++;
                }
            }

            MessageBox.Show(added > 0 ? "Đã thêm " + added + " bàn!" : "Không thêm được bàn nào!",
                "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtThemBan.Clear();
            this.TablesChanged = this.TablesChanged || (added > 0);
        }

        private void txtThemBan_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
