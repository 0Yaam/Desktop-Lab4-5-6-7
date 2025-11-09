using _2312590_NNTDan_Lab07.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace _2312590_NNTDan_Lab07
{
    public partial class AccountForm : Form
    {
        private readonly RestaurantContext _db = new RestaurantContext();

        public AccountForm()
        {
            InitializeComponent();
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            LoadRolesIntoFilter();
            LoadAccounts();
            dgvAccounts.ReadOnly = false;
            dgvAccounts.Columns["Id"].ReadOnly = true;
        }

        private void LoadRolesIntoFilter()
        {
            var roles = _db.Roles.OrderBy(r => r.Name).Select(r => new { r.Id, r.Name }).ToList();
            roles.Insert(0, new
            {
                Id = 0,
                Name = "Tất cả"
            });
            cbbRoleFilter.DisplayMember = "Name";
            cbbRoleFilter.ValueMember = "Id";
            cbbRoleFilter.DataSource = roles;
        }

        private void LoadAccounts()
        {
            int roleId = (cbbRoleFilter.SelectedValue is int) ? (int)cbbRoleFilter.SelectedValue : 0;
            string keyword = txtKeyword.Text?.Trim() ?? string.Empty;
            IQueryable<Account> query = _db.Accounts.AsQueryable();
            if (roleId > 0)
            {
                query = query.Where(a => a.Roles.Any(r => r.Id == roleId));
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a => a.UserName.Contains(keyword) || a.DisplayName.Contains(keyword));
            }
            var data = query
                .OrderBy(a => a.UserName)
                .Select(a => new AccountDto { Id = a.Id, UserName = a.UserName, Password = a.Password, DisplayName = a.DisplayName, Email = a.Email, Tel = a.Tel, IsActive = a.IsActive })
                .ToList();
            dgvAccounts.DataSource = data;
        }

        private Account GetSelectedAccount()
        {
            if (dgvAccounts.CurrentRow == null)
                return null;
            var id = (int)dgvAccounts.CurrentRow.Cells["Id"].Value;
            return _db.Accounts.Find(id);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void cbbRoleFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new UpdateAccountForm())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LoadAccounts();
                }
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            var acc = GetSelectedAccount();
            if (acc == null)
            {
                MessageBox.Show("Chọn 1 tài khoản trước.", "Thông báo");
                return;
            }

            if (MessageBox.Show($"Reset mật khẩu của '{acc.UserName}' về 123?",
                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                acc.Password = "123";       
                                               
                _db.SaveChanges();

                LoadAccounts();                
                MessageBox.Show("Đã reset mật khẩu về 123.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi reset mật khẩu: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void ctxDeleteAccount_Click(object sender, EventArgs e)
        {
            var acc = GetSelectedAccount();
            if (acc == null)
            {
                MessageBox.Show("Chọn 1 tài khoản trước.", "Thông báo");
                return;
            }

            if (MessageBox.Show($"Xóa tài khoản '{acc.UserName}'?",
                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                // load Roles rồi clear để tránh lỗi FK ở bảng nối
                _db.Entry(acc).Collection(a => a.Roles).Load();
                acc.Roles.Clear();

                _db.Accounts.Remove(acc);
                _db.SaveChanges();

                LoadAccounts();
                MessageBox.Show("Đã xóa.", "Thông báo");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                MessageBox.Show(
                    "Không thể xóa do đang được tham chiếu ở nơi khác (ràng buộc dữ liệu).\n" +
                    "Bạn có thể đặt Inactive thay cho xóa.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ctxViewRoles_Click(object sender, EventArgs e)
        {
            Account acc = GetSelectedAccount();
            if (acc == null)
                return;
            using (var dlg = new AccountRolesForm(acc.Id))
            {
                dlg.ShowDialog(this);
            }
        }

        private void dgvAccounts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvAccounts.Rows[e.RowIndex];
            int id = (int)row.Cells["Id"].Value;
            var account = _db.Accounts.Find(id);
            if (account == null)
                return;
            string columnName = dgvAccounts.Columns[e.ColumnIndex].Name;
            switch (columnName)
            {
                case "UserName":
                    account.UserName = row.Cells["UserName"].Value?.ToString();
                    break;
                case "Password":
                    account.Password = row.Cells["Password"].Value?.ToString();
                    break;
                case "DisplayName":
                    account.DisplayName = row.Cells["DisplayName"].Value?.ToString();
                    break;
                case "Email":
                    account.Email = row.Cells["Email"].Value?.ToString();
                    break;
                case "Tel":
                    account.Tel = row.Cells["Tel"].Value?.ToString();
                    break;
                case "IsActive":
                    account.IsActive = (bool)row.Cells["IsActive"].Value;
                    break;
                default:
                    return; 
            }
            try
            {
                _db.SaveChanges();
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadAccounts();
            }
        }

    }
    public class AccountDto
    {
        public int Id
        {
            get; set;
        }
        public string UserName
        {
            get; set;
        }
        public string Password
        {
            get; set;
        }
        public string DisplayName
        {
            get; set;
        }
        public string Email
        {
            get; set;
        }
        public string Tel
        {
            get; set;
        }
        public bool IsActive
        {
            get; set;
        }
    }
}
