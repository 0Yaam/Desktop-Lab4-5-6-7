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
    public partial class FoodInfoForm : Form
    {
        string connectionString = Configs.ConnectionString;
        public FoodInfoForm()
        {
            InitializeComponent();
        }

        private void FoodInfoForm_Load(object sender, EventArgs e)
        {
            InitValues();
        }

        private void InitValues()
        {
            using (var conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("select ID, Name from Category", conn))
            {
                DataSet ds = new DataSet();
                conn.Open();
                adapter.Fill(ds, "Category");

                cbbCatName.DataSource = ds.Tables["Category"];
                cbbCatName.DisplayMember = "Name";
                cbbCatName.ValueMember = "ID";
            }

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SqlConnection(Configs.ConnectionString))
                using (var cmd = new SqlCommand("Food_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var pId = cmd.Parameters.Add("@id", SqlDbType.Int);
                    pId.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 1000).Value =
                        string.IsNullOrWhiteSpace(txtName.Text) ? (object)DBNull.Value : txtName.Text.Trim();
                    cmd.Parameters.Add("@unit", SqlDbType.NVarChar, 100).Value =
                        string.IsNullOrWhiteSpace(txtUnit.Text) ? (object)DBNull.Value : txtUnit.Text.Trim();
                    cmd.Parameters.Add("@fcid", SqlDbType.Int).Value =
                        cbbCatName.SelectedValue == null ? (object)DBNull.Value : Convert.ToInt32(cbbCatName.SelectedValue);
                    cmd.Parameters.Add("@price", SqlDbType.Int).Value = Convert.ToInt32(nudPrice.Value);
                    cmd.Parameters.Add("@notes", SqlDbType.NVarChar, 3000).Value =
                        string.IsNullOrWhiteSpace(txtNotes.Text) ? (object)DBNull.Value : txtNotes.Text.Trim();

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Thêm món thành công", "OK");
                    this.DialogResult = DialogResult.OK;   
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message, "Error");
            }
        }

        public void DisplayFoodInfo(DataRowView rowView)
        {
            try
            {
                txtFoodID.Text = rowView["ID"].ToString();
                txtName.Text = rowView["Name"].ToString();
                txtUnit.Text = rowView["Unit"].ToString();
                txtNotes.Text = rowView["Notes"].ToString();

                decimal price = 0m;
                if (rowView["Price"] != DBNull.Value)
                {
                    price = Convert.ToDecimal(rowView["Price"]);
                }

                if (nudPrice != null)
                {
                    if (price < nudPrice.Minimum) price = nudPrice.Minimum;
                    if (price > nudPrice.Maximum) price = nudPrice.Maximum;
                    nudPrice.Value = price;
                }

                cbbCatName.SelectedIndex = -1;
                for (int index = 0; index < cbbCatName.Items.Count; index++)
                {
                    DataRowView cat = cbbCatName.Items[index] as DataRowView;
                    if (cat["ID"].ToString() == rowView["FoodCategoryID"].ToString())
                    {
                        cbbCatName.SelectedIndex = index;
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
                Close();
            }


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFoodID.Text))
                {
                    MessageBox.Show("FoodID missing", "Error");
                    return;
                }

                int? catId = null;
                if (cbbCatName.SelectedValue is DataRowView drv)
                {
                    catId = Convert.ToInt32(drv["ID"]);
                }
                else if (cbbCatName.SelectedValue != null && cbbCatName.SelectedValue != DBNull.Value)
                {
                    catId = Convert.ToInt32(cbbCatName.SelectedValue);
                }

                using (var conn = new SqlConnection(Configs.ConnectionString))
                using (var cmd = new SqlCommand("UpdateFood", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(txtFoodID.Text);
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 1000).Value =
                        string.IsNullOrWhiteSpace(txtName.Text) ? (object)DBNull.Value : txtName.Text.Trim();
                    cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 100).Value =
                        string.IsNullOrWhiteSpace(txtUnit.Text) ? (object)DBNull.Value : txtUnit.Text.Trim();

                    if (!catId.HasValue)
                    {
                        MessageBox.Show("Vui lòng chọn nhóm món (Category).", "Thiếu dữ liệu");
                        return;
                    }
                    cmd.Parameters.Add("@FoodCategoryID", SqlDbType.Int).Value = catId.Value;

                    cmd.Parameters.Add("@Price", SqlDbType.Int).Value = Convert.ToInt32(nudPrice.Value);
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 3000).Value =
                        string.IsNullOrWhiteSpace(txtNotes.Text) ? (object)DBNull.Value : txtNotes.Text.Trim();

                    conn.Open();
                    cmd.ExecuteNonQuery(); 

                    MessageBox.Show("Cập nhật món ăn thành công", "OK");
                    this.DialogResult = DialogResult.OK; 
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message, "Error");
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            using (var f = new frmThemMonAn())
            {
                var result = f.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    InitValues();

                    if (f.NewCategoryId.HasValue)
                    {
                        cbbCatName.SelectedValue = f.NewCategoryId.Value;
                    }
                }
            }
        }


    }
}
