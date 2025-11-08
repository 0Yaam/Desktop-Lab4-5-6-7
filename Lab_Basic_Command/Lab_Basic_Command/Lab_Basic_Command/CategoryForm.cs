using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Basic_Command
{
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string connectionString = Configs.conn;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string sql = @"SELECT 
                                c.ID AS CategoryID,
                                c.Name AS CategoryName,
                                c.[Type] AS Type
                               FROM Category c
                               ORDER BY c.ID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        DisplayCategory(reader);
                    }
                }
            }
        }

        private void DisplayCategory(SqlDataReader reader)
        {
            lvCategory.Items.Clear();

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["CategoryID"].ToString());
                item.SubItems.Add(reader["CategoryName"].ToString());
                item.SubItems.Add(reader["Type"].ToString());
                lvCategory.Items.Add(item);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string connectionString = Configs.conn;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandText = "INSERT INTO Category(Name, [Type]) VALUES (@name, @type)";
                sqlCommand.Parameters.AddWithValue("@name", txtName.Text);
                sqlCommand.Parameters.AddWithValue("@type", txtType.Text);

                sqlConnection.Open();
                int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

                if (numOfRowsEffected == 1)
                {
                    MessageBox.Show("Thêm nhóm món ăn thành công");

                    btnLoad.PerformClick();
                    txtName.Text = "";
                    txtType.Text = "";
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra vui lòng thử lại");
                }
            }
        }

        private void lvCategory_Click(object sender, EventArgs e)
        {
            if (lvCategory.SelectedItems.Count == 0) return;

            ListViewItem item = lvCategory.SelectedItems[0];
            txtID.Text = item.Text;
            txtName.Text = item.SubItems[1].Text;
            txtType.Text = item.SubItems[2].Text == "0" ? "0" : item.SubItems[2].Text; // keep stored type value

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = Configs.conn;
            string query = "UPDATE Category SET Name = @Name, [Type] = @Type WHERE ID = @ID";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@Name", txtName.Text);
                sqlCommand.Parameters.AddWithValue("@Type", txtType.Text);
                sqlCommand.Parameters.AddWithValue("@ID", txtID.Text);

                sqlConnection.Open();
                int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

                if (numOfRowsEffected == 1 && lvCategory.SelectedItems.Count > 0)
                {
                    ListViewItem item = lvCategory.SelectedItems[0];
                    item.SubItems[1].Text = txtName.Text;
                    item.SubItems[2].Text = txtType.Text;

                    txtID.Text = "";
                    txtName.Text = "";
                    txtType.Text = "";

                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;

                    MessageBox.Show("Cập nhật nhóm món ăn thành công");
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn nhóm để xóa.");
                return;
            }

            string connectionString = Configs.conn;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandText = "DELETE FROM Category WHERE ID = @id";
                sqlCommand.Parameters.AddWithValue("@id", txtID.Text);

                sqlConnection.Open();
                int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

                if (numOfRowsEffected == 1)
                {
                    if (lvCategory.SelectedItems.Count > 0)
                    {
                        ListViewItem item = lvCategory.SelectedItems[0];
                        lvCategory.Items.Remove(item);
                    }

                    txtID.Text = "";
                    txtName.Text = "";
                    txtType.Text = "";

                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;

                    MessageBox.Show("Xóa nhóm món ăn thành công");
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại");
                }
            }
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (lvCategory.SelectedItems.Count > 0)
            {
                btnDelete.PerformClick();
            }
        }

        private void tsmViewFood_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                FoodForm foodForm = new FoodForm();
                foodForm.Show(this);
                foodForm.LoadFood(Convert.ToInt32(txtID.Text));
            }
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            btnLoad.PerformClick();
        }
    }
}
