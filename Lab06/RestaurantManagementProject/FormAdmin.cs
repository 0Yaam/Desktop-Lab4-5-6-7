using _2312590_NNTDan_ChuDe05;
using BusinessLogic;
using DataAccess;
using DataAccess.OL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RestaurantManagementProject
{
    public partial class FormAdmin : Form
    {
        private readonly ReportingBL _reportBL = new ReportingBL();
        private readonly AccountBL _accountBL = new AccountBL();
        private readonly RoleeBL _roleBL = new RoleeBL();
        private readonly RoleAccountBL _roleAccBL = new RoleAccountBL();

        private List<Account> _accounts = new List<Account>();
        private List<Role> _roles = new List<Role>();
        private DataTable _orders; // datasource cho dgvDSHD


        public FormAdmin()
        {
            InitializeComponent();
            InitGrid();
            LoadRolesToCbb();
            LoadAccountsToGrid();
            dtpNgayTao.Value = DateTime.Now;
        }
        private void InitGrid()
        {
            dgvDSTK.AutoGenerateColumns = false;
            dgvDSTK.AllowUserToAddRows = false;
            dgvDSTK.MultiSelect = false;
            dgvDSTK.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDSTK.RowHeadersVisible = false;
            dgvDSTK.Columns.Clear();

            dgvDSTK.Columns.Add(new DataGridViewTextBoxColumn { Name = "AccountName", HeaderText = "Tài khoản", DataPropertyName = "AccountName", Width = 140 });
            dgvDSTK.Columns.Add(new DataGridViewTextBoxColumn { Name = "FullName", HeaderText = "Họ tên", DataPropertyName = "FullName", Width = 180 });
            dgvDSTK.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "Email", DataPropertyName = "Email", Width = 180 });
            dgvDSTK.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tell", HeaderText = "Điện thoại", DataPropertyName = "Tell", Width = 110 });
            dgvDSTK.Columns.Add(new DataGridViewTextBoxColumn { Name = "DateCreated", HeaderText = "Ngày tạo", DataPropertyName = "DateCreated", Width = 110 });

            dgvDSTK.Columns.Add(new DataGridViewTextBoxColumn { Name = "Roles", HeaderText = "Quyền (đang bật)", Width = 160 });
        }
        private void LoadRolesToCbb()
        {
            _roles = _roleBL.GetAll();
            cbbChucVu.DisplayMember = "RoleName";
            cbbChucVu.ValueMember = "ID";
            cbbChucVu.DataSource = _roles;
            cbbChucVu.SelectedIndex = -1;
        }

        private void LoadAccountsToGrid()
        {
            _accounts = _accountBL.GetAll();

            var roleAccAll = _roleAccBL.GetAll();

            var view = _accounts
                .Select(a =>
                {
                    var actives = roleAccAll
                        .Where(ra => ra.AccountName.Equals(a.AccountName, StringComparison.OrdinalIgnoreCase) && ra.Actived)
                        .Join(_roles, ra => ra.RoleID, r => r.ID, (ra, r) => r.RoleName ?? "")
                        .ToList();

                    return new
                    {
                        a.AccountName,
                        a.FullName,
                        a.Email,
                        Tell = a.Tell,
                        DateCreated = a.DateCreated?.ToString("dd/MM/yyyy"),
                        Roles = string.Join(", ", actives)
                    };
                })
                .ToList();

            dgvDSTK.DataSource = view;
            dgvDSTK.ClearSelection();
        }
        private Account ReadAccountFromForm()
        {
            return new Account
            {
                AccountName = txtTaiKhoan.Text.Trim(),
                Password = txtMatKhau.Text,       
                FullName = txtHoTen.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                Tell = string.IsNullOrWhiteSpace(txtTel.Text) ? null : txtTel.Text.Trim(),
                DateCreated = dtpNgayTao.Value
            };
        }

        private void FillFormFromGridRow()
        {
            if (dgvDSTK.CurrentRow == null) return;
            var username = dgvDSTK.CurrentRow.Cells["AccountName"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(username)) return;

            var acc = _accounts.FirstOrDefault(a => a.AccountName.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (acc == null) return;

            txtTaiKhoan.Text = acc.AccountName;
            txtMatKhau.Text = acc.Password;         
            txtHoTen.Text = acc.FullName;
            txtEmail.Text = acc.Email;
            txtTel.Text = acc.Tell;
            dtpNgayTao.Value = acc.DateCreated ?? DateTime.Now;

            var roleAcc = _roleAccBL.GetAll()
                .Where(ra => ra.AccountName.Equals(acc.AccountName, StringComparison.OrdinalIgnoreCase) && ra.Actived)
                .OrderBy(ra => ra.RoleID)
                .FirstOrDefault();

            if (roleAcc != null)
                cbbChucVu.SelectedValue = roleAcc.RoleID;
            else
                cbbChucVu.SelectedIndex = -1;
        }

        private void ClearForm()
        {
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
            txtHoTen.Clear();
            txtEmail.Clear();
            txtTel.Clear();
            dtpNgayTao.Value = DateTime.Now;
            cbbChucVu.SelectedIndex = -1;
            dgvDSTK.ClearSelection();
            txtTaiKhoan.Focus();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Tài khoản, Mật khẩu, Họ tên là bắt buộc.", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var acc = ReadAccountFromForm();

            // chặn trùng
            if (_accounts.Any(a => a.AccountName.Equals(acc.AccountName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Tài khoản đã tồn tại.", "Không thể thêm",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Insert account
            var ok = _accountBL.Insert(acc);
            if (ok <= 0)
            {
                MessageBox.Show("Thêm tài khoản thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbbChucVu.SelectedValue is int roleId)
            {
                var ra = new RoleAccount
                {
                    RoleID = roleId,
                    AccountName = acc.AccountName,
                    Actived = true,
                    Notes = null
                };
                _roleAccBL.Insert(ra);
            }

            LoadAccountsToGrid();
            ClearForm();
            MessageBox.Show("Đã thêm tài khoản.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text))
            {
                MessageBox.Show("Chọn tài khoản cần cập nhật.", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existed = _accounts.FirstOrDefault(a => a.AccountName.Equals(txtTaiKhoan.Text.Trim(), StringComparison.OrdinalIgnoreCase));
            if (existed == null)
            {
                MessageBox.Show("Tài khoản không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var acc = ReadAccountFromForm();

            var ok = _accountBL.Update(acc);
            if (ok <= 0)
            {
                MessageBox.Show("Cập nhật tài khoản thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var allRa = _roleAccBL.GetAll()
                .Where(ra => ra.AccountName.Equals(acc.AccountName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            int? selectedRoleId = cbbChucVu.SelectedValue as int?;
            foreach (var ra in allRa)
            {
                bool shouldActive = (selectedRoleId.HasValue && ra.RoleID == selectedRoleId.Value);
                if (ra.Actived != shouldActive)
                {
                    ra.Actived = shouldActive;
                    _roleAccBL.Update(ra);
                }
            }

            if (selectedRoleId.HasValue && !allRa.Any(x => x.RoleID == selectedRoleId.Value))
            {
                _roleAccBL.Insert(new RoleAccount
                {
                    RoleID = selectedRoleId.Value,
                    AccountName = acc.AccountName,
                    Actived = true,
                    Notes = null
                });
            }

            LoadAccountsToGrid();
            MessageBox.Show("Đã cập nhật tài khoản.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text))
            {
                MessageBox.Show("Chọn tài khoản cần xoá.", "Thiếu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var username = txtTaiKhoan.Text.Trim();

            if (MessageBox.Show($"Xoá tài khoản '{username}'?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // Xoá RoleAccount trước (tránh FK)
            var allRa = _roleAccBL.GetAll()
                .Where(ra => ra.AccountName.Equals(username, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var ra in allRa)
                _roleAccBL.Delete(ra);

            // Xoá Account
            var acc = _accounts.FirstOrDefault(a => a.AccountName.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (acc == null)
            {
                MessageBox.Show("Tài khoản không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var ok = _accountBL.Delete(acc);
            if (ok <= 0)
            {
                MessageBox.Show("Xoá tài khoản thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadAccountsToGrid();
            ClearForm();
            MessageBox.Show("Đã xoá tài khoản.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearForm();


        private void dgvDSTK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            dgvDSTK.CellClick += (s, ev) => FillFormFromGridRow();
            dgvDSTK.SelectionChanged += (s, ev) => FillFormFromGridRow();

            chkApDung.CheckedChanged += chkApDung_CheckedChanged;
            dtpTuNgay.ValueChanged += dtpTuNgay_ValueChanged;
            dtpDenNgay.ValueChanged += dtpDenNgay_ValueChanged;
            dgvDSHD.CellDoubleClick += dgvDSHD_CellDoubleClick;  

            if (dtpTuNgay != null) dtpTuNgay.Value = DateTime.Today;
            if (dtpDenNgay != null) dtpDenNgay.Value = DateTime.Today;

            LoadOrders(applyDateFilter: chkApDung.Checked);
        }
        private void LoadOrders(bool applyDateFilter)
        {
            DateTime? from = null;
            DateTime? toExclusive = null;

            if (applyDateFilter)
            {
                from = dtpTuNgay.Value.Date;
                toExclusive = dtpDenNgay.Value.Date.AddDays(1);
            }

            var list = _reportBL.GetPaidBills(from, toExclusive);

            dgvDSHD.DataSource = list;

            AutoSizeGridColumns();
            UpdateTotals(list);
        }


        private void AutoSizeGridColumns()
        {
            if (dgvDSHD.Columns.Count == 0) return;

            dgvDSHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            if (dgvDSHD.Columns.Contains("Name"))
                dgvDSHD.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        // Tổng hợp doanh thu
        private void UpdateTotals(IEnumerable<BusinessLogic.BillView> bills)
        {
            var totals = _reportBL.ComputeTotals(bills);

            // nếu control bạn tên khác, giữ logic fallback như cũ
            if (this.Controls.ContainsKey("txtTongTienChuaGiamGia"))
                this.Controls["txtTongTienChuaGiamGia"].Text = totals.TotalGross.ToString("N0");
            else
                txtTienChuaGiamGia.Text = totals.TotalGross.ToString("N0");

            txtGiamGia.Text = totals.TotalDiscount.ToString("N0");
            txtTongDoanhThu.Text = totals.TotalNet.ToString("N0");
        }

        private void UpdateTotals()
        {
            var data = dgvDSHD.DataSource as IEnumerable<BusinessLogic.BillView> ?? Enumerable.Empty<BusinessLogic.BillView>();
            UpdateTotals(data);
        }




        private decimal ToDec(object v)
        {
            if (v == null || v == DBNull.Value) return 0m;
            try { return Convert.ToDecimal(v); } catch { return 0m; }
        }


        private void chkApDung_CheckedChanged(object sender, EventArgs e)
        {
            LoadOrders(applyDateFilter: chkApDung.Checked);
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (chkApDung.Checked)
                LoadOrders(applyDateFilter: true);
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (chkApDung.Checked)
                LoadOrders(applyDateFilter: true);
        }

        private void dgvDSHD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvDSHD.Rows[e.RowIndex].DataBoundItem is BusinessLogic.BillView bv)
            {
                int billId = bv.ID;
                using (var f = new OrderDetailsForm(billId))
                {
                    f.ShowDialog(this);
                }
                return;
            }

            if (dgvDSHD.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
            {
                var row = drv.Row;
                if (row == null) return;

                int billId = Convert.ToInt32(row["ID"]);
                using (var f = new OrderDetailsForm(billId))
                {
                    f.ShowDialog(this);
                }
            }
        }
        private void txtTaiKhoan_TextChanged(object sender, EventArgs e) { }
        private void dtpNgayTao_ValueChanged(object sender, EventArgs e) { }
        private void dgvDSHD_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

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
