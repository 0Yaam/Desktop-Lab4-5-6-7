using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _2312590_NNTDan_ChuDe05
{
    public partial class FoodForm_Load : System.Windows.Forms.Form
    {
        string connectionString = Configs.ConnectionString;
        private DataTable foodTable;
        public FoodForm_Load()
        {
            InitializeComponent();
        }
        private void FoodForm_Load_Load(object sender, EventArgs e)
        {
            LoadCategory();
        }
        private void LoadCategory()
        {
            using (var conn = new SqlConnection(connectionString))
                using (var adapter = new SqlDataAdapter("select ID,Name from Category",conn))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cbbCategory.DisplayMember = "Name";
                cbbCategory.ValueMember = "ID";
                cbbCategory.DataSource = dt;
            }
        }

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbCategory.SelectedIndex == -1) return;

            using (SqlConnection conn = new SqlConnection(Configs.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("select * from Food where FoodCategoryID = @categoryId", conn))
            {
                cmd.Parameters.Add("@categoryId", SqlDbType.Int).Value = Convert.ToInt32(cbbCategory.SelectedValue); 
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                foodTable = new DataTable();
                conn.Open();
                adapter.Fill(foodTable);
                dgvFoodList.DataSource = foodTable;
                lblQuantity.Text = foodTable.Rows.Count.ToString();
                lblCatName.Text = cbbCategory.Text;
            }

        }

        private void cmsTinhSoLuongDaban_Click(object sender, EventArgs e)
        {
            if (dgvFoodList.CurrentRow == null) return;
            DataRowView row = dgvFoodList.CurrentRow.DataBoundItem as DataRowView;
            if (row == null) return;

            using (SqlConnection conn = new SqlConnection(Configs.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT COALESCE(SUM(Quantity),0) FROM BillDetails WHERE FoodID = @foodId", conn))
                {
                    cmd.Parameters.Add("@foodId", SqlDbType.Int).Value = Convert.ToInt32(row["ID"]);
                    conn.Open();
                    int sold = Convert.ToInt32(cmd.ExecuteScalar());
                    MessageBox.Show(
                        $"Tổng đã bán: {sold} {row["Unit"]} - {row["Name"]}",
                        "Thống kê", MessageBoxButtons.OK);
                }
            }
        }

        private void cmsThemMonAnMoi_Click(object sender, EventArgs e)
        {
            using (var frm = new FoodInfoForm())
            {
                var result = frm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ReloadFoodsForCurrentCategory();
                }
            }
        }


      
        private void cmsCapNhatMonAn_Click(object sender, EventArgs e)
        {
            if (dgvFoodList.SelectedRows.Count == 0) return;

            var rowView = dgvFoodList.SelectedRows[0].DataBoundItem as DataRowView;
            if (rowView == null) return;

            using (var frm = new FoodInfoForm())
            {
                frm.DisplayFoodInfo(rowView);    
                var result = frm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ReloadFoodsForCurrentCategory();
                }
            }
        }


        private void txtTimKiemTheoTen_TextChanged(object sender, EventArgs e)
        {

           if(foodTable == null)
            {
                return;
            
            }

           string filterExpression = $"Name LIKE '%{txtTimKiemTheoTen.Text}%'";
            string sortExpression = "Price DESC";
            DataViewRowState rowState = DataViewRowState.OriginalRows;

            DataView dv = new DataView(foodTable, filterExpression, sortExpression, rowState);
            dgvFoodList.DataSource = dv;
        }

        private void ReloadFoodsForCurrentCategory()
        {
            if (cbbCategory.SelectedValue == null) return;

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("select * from Food where FoodCategoryID = @categoryId", conn))
            {
                cmd.Parameters.Add("@categoryId", SqlDbType.Int).Value = Convert.ToInt32(cbbCategory.SelectedValue);
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    foodTable = new DataTable();
                    conn.Open();
                    adapter.Fill(foodTable);

                    dgvFoodList.DataSource = foodTable;
                    lblQuantity.Text = foodTable.Rows.Count.ToString();
                    lblCatName.Text = cbbCategory.Text;
                }
            }
        }

        private void btnThongKeHoaDon_Click(object sender, EventArgs e)
        {
            OrderForm frm = new OrderForm();
            frm.ShowDialog();
        }

        private void btnQuanLyTaiKhoan_Click(object sender, EventArgs e)
        {
            AccountForm accountForm = new AccountForm();
            accountForm.Show();
        }
    }
}



