using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _2312590_NNTDan_ChuDe05
{
    public partial class frmThemMonAn : Form
    {
        private readonly string connectionString = Configs.ConnectionString;

        public int? NewCategoryId { get; private set; }

        public frmThemMonAn()
        {
            InitializeComponent();
        }

        private void btnThemMonAnMoi_Click(object sender, EventArgs e)
        {
            string name = txtTenMonAnMoi.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập tên nhóm món ăn.", "Thiếu dữ liệu");
                return;
            }

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand("Category_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var pId = cmd.Parameters.Add("@ID", SqlDbType.Int);
                    pId.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 1000).Value = name;

                    cmd.Parameters.Add("@Type", SqlDbType.Int).Value = 1;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["@ID"].Value != DBNull.Value)
                        NewCategoryId = Convert.ToInt32(cmd.Parameters["@ID"].Value);

                    MessageBox.Show("Thêm nhóm món ăn thành công!", "Thành công");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message, "Lỗi");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
