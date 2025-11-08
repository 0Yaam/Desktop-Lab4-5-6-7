using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Basic_Command
{
    public partial class FoodForm : Form
    {
        public FoodForm()
        {
            InitializeComponent();
        }

        private void FoodForm_Load(object sender, EventArgs e)
        {

        }

        public void LoadFood(int categoryID)
        {
            string connectionString = Configs.conn;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT Name FROM Category WHERE ID = @id";
                    sqlCommand.Parameters.AddWithValue("@id", categoryID);

                    sqlConnection.Open();
                    string catName = sqlCommand.ExecuteScalar()?.ToString() ?? string.Empty;
                    this.Text = "Danh sách các món ăn thuộc nhóm: " + catName;

                    // reuse command for data adapter; ensure parameters present
                    sqlCommand.CommandText = "SELECT ID, Name, Unit, FoodCategoryID, Price, Notes FROM Food WHERE FoodCategoryID = @id";

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvFood.AutoGenerateColumns = false;
                    dgvFood.Columns.Clear();

                    dgvFood.Columns.Add("ID", "Mã món");
                    dgvFood.Columns["ID"].DataPropertyName = "ID";

                    dgvFood.Columns.Add("Name", "Tên món");
                    dgvFood.Columns["Name"].DataPropertyName = "Name";

                    dgvFood.Columns.Add("Unit", "Đơn vị tính");
                    dgvFood.Columns["Unit"].DataPropertyName = "Unit";

                    dgvFood.Columns.Add("FoodCategoryID", "Mã nhóm");
                    dgvFood.Columns["FoodCategoryID"].DataPropertyName = "FoodCategoryID";

                    dgvFood.Columns.Add("Price", "Đơn giá");
                    dgvFood.Columns["Price"].DataPropertyName = "Price";

                    dgvFood.Columns.Add("Notes", "Ghi chú");
                    dgvFood.Columns["Notes"].DataPropertyName = "Notes";

                    dgvFood.DataSource = dt;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = Configs.conn;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (DataGridViewRow row in dgvFood.Rows)
                {
                    if (row.IsNewRow) continue;

                    int id = row.Cells["ID"].Value == DBNull.Value || row.Cells["ID"].Value == null ? 0 : Convert.ToInt32(row.Cells["ID"].Value);
                    string name = row.Cells["Name"].Value?.ToString();
                    string unit = row.Cells["Unit"].Value?.ToString();
                    int categoryID = row.Cells["FoodCategoryID"].Value == DBNull.Value || row.Cells["FoodCategoryID"].Value == null ? 0 : Convert.ToInt32(row.Cells["FoodCategoryID"].Value);
                    double price = row.Cells["Price"].Value == DBNull.Value || row.Cells["Price"].Value == null ? 0 : Convert.ToDouble(row.Cells["Price"].Value);
                    string notes = row.Cells["Notes"].Value?.ToString();

                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        if (id == 0)
                        {
                            cmd.CommandText = @"INSERT INTO Food (Name, Unit, FoodCategoryID, Price, Notes)
                                    VALUES (@name, @unit, @categoryID, @price, @notes)";
                        }
                        else
                        {
                            cmd.CommandText = @"UPDATE Food
                                    SET Name = @name, Unit = @unit, FoodCategoryID = @categoryID,
                                        Price = @price, Notes = @notes
                                    WHERE ID = @id";
                            cmd.Parameters.AddWithValue("@id", id);
                        }

                        cmd.Parameters.AddWithValue("@name", (object)name ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@unit", (object)unit ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@categoryID", categoryID);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@notes", (object)notes ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đã lưu thay đổi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (dgvFood.Rows.Count > 0 && dgvFood.Rows[0].Cells["FoodCategoryID"].Value != null)
                {
                    LoadFood((int)dgvFood.Rows[0].Cells["FoodCategoryID"].Value);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvFood.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!");
                return;
            }

            var row = dgvFood.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["ID"].Value);

            if (MessageBox.Show("Bạn có chắc muốn xóa món này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string connectionString = Configs.conn;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM Food WHERE ID = @id";
                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.ExecuteNonQuery();
                    }
                }

                dgvFood.Rows.Remove(row);
                MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
