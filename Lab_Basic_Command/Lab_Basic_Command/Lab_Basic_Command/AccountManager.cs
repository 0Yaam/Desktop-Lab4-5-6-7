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
    public partial class AccountManager : Form
    {
        private string connectionString = Configs.conn;
        private bool isLoading = false;

        public AccountManager()
        {
            InitializeComponent();
        }

        private void AccountManager_Load(object sender, EventArgs e)
        {
            LoadGroups();
            cboGroup.SelectedIndexChanged += cboGroup_SelectedIndexChanged;

            LoadAccounts();

            dgvAccounts.SelectionChanged += dgvAccounts_SelectionChanged;

            dgvAccounts.MouseDown += dgvAccounts_MouseDown;
        }

        private void LoadGroups()
        {
            isLoading = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
						SELECT 1 as ID, N'Quản trị viên' as Name
						UNION ALL
						SELECT 2 as ID, N'Nhân viên' as Name";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboGroup.DataSource = dt;
                    cboGroup.DisplayMember = "Name";
                    cboGroup.ValueMember = "ID";
                    cboGroup.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load nhóm: " + ex.Message);
            }
            finally
            {
                isLoading = false;
            }
        }

        private void LoadAccounts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT ID, Username,Password, DisplayName, GroupID, IsActive FROM Account WHERE 1=1";

                    if (chkActiveOnly.Checked)
                    {
                        cmd.CommandText += " AND IsActive = 1";
                    }

                    int selectedGroupId;
                    if (cboGroup.SelectedValue != null && int.TryParse(cboGroup.SelectedValue.ToString(), out selectedGroupId))
                    {
                        cmd.CommandText += " AND GroupID = @GroupID";
                        cmd.Parameters.AddWithValue("@GroupID", selectedGroupId);
                    }

                    cmd.CommandText += " ORDER BY ID";

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvAccounts.DataSource = dt;

                    if (dgvAccounts.Columns["ID"] != null) dgvAccounts.Columns["ID"].HeaderText = "Mã";
                    if (dgvAccounts.Columns["Username"] != null) dgvAccounts.Columns["Username"].HeaderText = "Tên đăng nhập";
                    if (dgvAccounts.Columns["Password"] != null) dgvAccounts.Columns["Password"].HeaderText = "Mật khẩu";
                    if (dgvAccounts.Columns["DisplayName"] != null) dgvAccounts.Columns["DisplayName"].HeaderText = "Tên hiển thị";
                    if (dgvAccounts.Columns["GroupID"] != null) dgvAccounts.Columns["GroupID"].HeaderText = "Nhóm";
                    if (dgvAccounts.Columns["IsActive"] != null) dgvAccounts.Columns["IsActive"].HeaderText = "Hoạt động";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load tài khoản: " + ex.Message);
            }
        }

        private void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            LoadAccounts();
        }

        private void dgvAccounts_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoading || dgvAccounts.CurrentRow == null) return;

            try
            {
                DataGridViewRow row = dgvAccounts.CurrentRow;

                txtUserName.Text = row.Cells["Username"].Value?.ToString() ?? "";
                txtDisplayName.Text = row.Cells["DisplayName"].Value?.ToString() ?? "";
                
                txtPassword.Text = row.Cells["Password"].Value?.ToString();

                int groupId = Convert.ToInt32(row.Cells["GroupID"].Value ?? 0);

                isLoading = true;
                try
                {
                    if (cboGroup.DataSource is DataTable dt && dt.AsEnumerable().Any(r => r.Field<int>("ID") == groupId))
                    {
                        cboGroup.SelectedValue = groupId;
                    }
                    else
                    {
                        cboGroup.SelectedIndex = -1;
                    }
                }
                finally
                {
                    isLoading = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load thông tin tài khoản: " + ex.Message);
            }
        }

        private void dgvAccounts_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var hit = dgvAccounts.HitTest(e.X, e.Y);
            if (hit.RowIndex >= 0)
            {
                dgvAccounts.ClearSelection();
                dgvAccounts.Rows[hit.RowIndex].Selected = true;
                if (dgvAccounts.Rows[hit.RowIndex].Cells.Count > 0)
                {
                    dgvAccounts.CurrentCell = dgvAccounts.Rows[hit.RowIndex].Cells[0];
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên hiển thị!");
                return;
            }
            if (cboGroup.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhóm!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO Account (Username, DisplayName, Password, GroupID, IsActive)
                        VALUES (@Username, @DisplayName, @Password, @GroupID, 1)";
                    cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
                    cmd.Parameters.AddWithValue("@DisplayName", txtDisplayName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", string.IsNullOrEmpty(txtPassword.Text) ? "123456" : txtPassword.Text);
                    cmd.Parameters.AddWithValue("@GroupID", Convert.ToInt32(cboGroup.SelectedValue));

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Thêm tài khoản thành công!");
                    ClearForm();
                    LoadAccounts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm tài khoản: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần cập nhật!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên hiển thị!");
                return;
            }
            if (cboGroup.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhóm!");
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgvAccounts.CurrentRow.Cells["ID"].Value);
                bool currentIsActive = Convert.ToBoolean(dgvAccounts.CurrentRow.Cells["IsActive"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        UPDATE Account
                        SET Username = @Username,
                            DisplayName = @DisplayName, 
                            GroupID = @GroupID,
                            IsActive = @IsActive";

                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        query += ", Password = @Password";
                    }

                    query += " WHERE ID = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
                        cmd.Parameters.AddWithValue("@DisplayName", txtDisplayName.Text.Trim());
                        cmd.Parameters.AddWithValue("@GroupID", Convert.ToInt32(cboGroup.SelectedValue));
                        cmd.Parameters.AddWithValue("@IsActive", currentIsActive ? 1 : 0); 
                        cmd.Parameters.AddWithValue("@ID", id);

                        if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                        {
                            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                        }

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật tài khoản thành công!");
                            LoadAccounts();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tài khoản để cập nhật!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật tài khoản: " + ex.Message);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần reset mật khẩu!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn reset mật khẩu về '123456'?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgvAccounts.CurrentRow.Cells["ID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "UPDATE Account SET Password = @Password WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@Password", "123456");
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã reset mật khẩu về '123456' thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tài khoản để reset mật khẩu!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi reset mật khẩu: " + ex.Message);
            }
        }

        private void cmsDelete_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn vô hiệu hóa tài khoản này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgvAccounts.CurrentRow.Cells["ID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        UPDATE AccountRole 
                        SET IsActive = 0 
                        WHERE AccountID = @ID;

                        UPDATE Account
                        SET IsActive = 0
                        WHERE ID = @ID;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Đã vô hiệu hóa tài khoản và vai trò liên quan!");
                            LoadAccounts();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tài khoản để vô hiệu hóa!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi vô hiệu hóa tài khoản: " + ex.Message);
            }
        }

        private void cmsViewRoles_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!");
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgvAccounts.CurrentRow.Cells["ID"].Value);
                RoleListForm roleForm = new RoleListForm(id);
                roleForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form vai trò: " + ex.Message);
            }
        }

        private void chkActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void ClearForm()
        {
            txtUserName.Text = "";
            txtDisplayName.Text = "";
            txtPassword.Text = "";
            cboGroup.SelectedIndex = -1;
        }
    }
}