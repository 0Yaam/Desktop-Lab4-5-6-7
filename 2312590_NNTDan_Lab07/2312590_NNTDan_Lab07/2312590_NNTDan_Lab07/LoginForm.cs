using _2312590_NNTDan_Lab07.Models;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace _2312590_NNTDan_Lab07
{
    public partial class LoginForm : Form
    {
        private readonly RestaurantContext _db = new RestaurantContext();

        public LoginForm()
        {
            InitializeComponent();
            ApplyVietnameseUi();
        }

        private void ApplyVietnameseUi()
        {
            // Force correct Unicode labels even if designer file encoding is wrong
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Text = "Đăng nhập";

            // These controls exist in designer
            label1.UseCompatibleTextRendering = false;
            label2.UseCompatibleTextRendering = false;
            btnLogin.UseCompatibleTextRendering = false;
            btnCancel.UseCompatibleTextRendering = false;

            label1.Text = "Tên đăng nhập";
            label2.Text = "Mật khẩu";
            btnLogin.Text = "Đăng nhập";
            btnCancel.Text = "Hủy";

            AcceptButton = btnLogin;
            CancelButton = btnCancel;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUser.Text.Trim();
            var password = txtPass.Text; // giữ nguyên để so sánh

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tìm tài khoản đang Active, nạp kèm Roles
            Account user = _db.Accounts
                .Include("Roles")
                .FirstOrDefault(a => a.UserName == username && a.Password == password && a.IsActive);

            if (user == null)
            {
                MessageBox.Show("Sai thông tin đăng nhập hoặc tài khoản không hoạt động", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AppSession.CurrentUser = user; // lưu session người dùng
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
