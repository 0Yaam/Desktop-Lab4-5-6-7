using _2312590_NNTDan_Lab07.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Windows.Forms;

namespace _2312590_NNTDan_Lab07
{
    public partial class FoodForm : Form
    {
        private readonly RestaurantContext _db = new RestaurantContext();

        public FoodForm()
        {
            InitializeComponent();
            txtKeyword.TextChanged += (s, e) => LoadFoods();
            cbbCategory.SelectedIndexChanged += (s, e) => LoadFoods();
        }

        private void FoodForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadFoods();
        }

        private void LoadCategories()
        {
            var categories = _db.Categories
                .OrderBy(x => x.Name)
                .Select(x => new { x.Id, x.Name })
                .ToList();
            categories.Insert(0, new
            {
                Id = 0,
                Name = "Tất cả"
            });

            cbbCategory.DisplayMember = "Name";
            cbbCategory.ValueMember = "Id";
            cbbCategory.DataSource = categories;
        }

        private void LoadFoods()
        {
            int categoryId = (cbbCategory.SelectedValue is int) ? (int)cbbCategory.SelectedValue : 0;
            string keyword = txtKeyword.Text?.Trim() ?? string.Empty;

            IQueryable<Food> query = _db.Foods.AsQueryable();

            if (categoryId > 0)
                query = query.Where(f => f.FoodCategoryId == categoryId);

            if (!string.IsNullOrEmpty(keyword))
            {
                string pattern = (keyword.Contains("%") || keyword.Contains("_") || keyword.Contains("["))
                    ? keyword
                    : "%" + keyword + "%";

                query = query.Where(f => SqlFunctions.PatIndex(pattern, f.Name) > 0);
            }

            var data = query
                .OrderBy(f => f.Name)
                .Select(f => new
                {
                    f.Id,
                    f.Name,
                    f.Unit,
                    CategoryName = f.Category.Name,
                    f.Price,
                    f.Notes
                })
                .ToList();

            dgvFoods.DataSource = data;
        }


        private int? GetSelectedFoodId()
        {
            if (dgvFoods.CurrentRow == null)
                return null;
            return (int?)dgvFoods.CurrentRow.Cells["Id"].Value;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadFoods();
        }

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFoods();
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            using (var dlg = new UpdateFoodForm())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LoadFoods();
                }
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            var id = GetSelectedFoodId();
            if (id == null)
                return;

            using (var dlg = new UpdateFoodForm(id.Value))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LoadFoods();
                }
            }
        }

        private void dgvFoods_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            var id = (int)dgvFoods.Rows[e.RowIndex].Cells["Id"].Value;
            using (var dlg = new UpdateFoodForm(id))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LoadFoods();
                }
            }
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (dgvFoods.SelectedRows.Count == 0)
                return;

            if (MessageBox.Show("Xóa các món đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var ids = new List<int>();
            foreach (DataGridViewRow row in dgvFoods.SelectedRows)
            {
                if (row.Cells["Id"].Value is int id)
                {
                    ids.Add(id);
                }
            }

            var foods = _db.Foods.Where(f => ids.Contains(f.Id)).ToList();
            _db.Foods.RemoveRange(foods);
            _db.SaveChanges();
            LoadFoods();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            using (var dlg = new UpdateCategoryForm())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LoadCategories();
                    LoadFoods();
                }
            }
        }
    }
}
