using System;
using System.Windows.Forms;
using BusinessLogic;

namespace RestaurantManagementProject
{
    public partial class frmLogin : Form
    {
        private readonly AuthBL _auth = new AuthBL();

        public frmLogin()
        {
            InitializeComponent();
            this.AcceptButton = button1; 
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var user = txtTaiKhoan.Text?.Trim();
            var pass = txtMatKhau.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = _auth.TryLogin(user, pass);
            if (result == null)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng.", "Đăng nhập thất bại",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Điều hướng theo role
            this.Hide();
            if (result.IsAdmin)
            {
                using (var f = new FormAdmin())
                {
                    f.FormClosed += (s2, e2) => this.Close();
                    f.ShowDialog();
                }
            }
            else if (result.IsThuNgan)
            {
                using (var f = new FormDatBan(result.Account.AccountName))
                {
                    f.FormClosed += (s2, e2) => this.Close();
                    f.ShowDialog();
                }
            }
            else
            {
                this.Show();
                MessageBox.Show("Bạn không có quyền truy cập vào các màn hình chính (Admin/Thu ngân).",
                    "Không đủ quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e) { }
        private void txtTaiKhoan_TextChanged(object sender, EventArgs e) { }
    }
}
