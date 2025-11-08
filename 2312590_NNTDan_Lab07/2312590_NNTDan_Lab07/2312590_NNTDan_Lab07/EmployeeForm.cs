using _2312590_NNTDan_Lab07.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace _2312590_NNTDan_Lab07
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();

            // Ensure Vietnamese characters render correctly
            Font = new Font("Segoe UI", NinePt(), FontStyle.Regular, GraphicsUnit.Point, 0);
            EnableGdiPlusTextRendering();
        }

        private float NinePt()
        {
            return 9F;
        }

        private void EnableGdiPlusTextRendering()
        {
            // Enable for static labels too
            if (lblUsername != null)
                lblUsername.UseCompatibleTextRendering = true;
            if (lblDisplayName != null)
                lblDisplayName.UseCompatibleTextRendering = true;
            if (lblEmail != null)
                lblEmail.UseCompatibleTextRendering = true;
            if (lblTel != null)
                lblTel.UseCompatibleTextRendering = true;
            if (lblGroup != null)
                lblGroup.UseCompatibleTextRendering = true;
            if (lblActive != null)
                lblActive.UseCompatibleTextRendering = true;
            if (lblRoles != null)
                lblRoles.UseCompatibleTextRendering = true;

            if (lblUsernameValue != null)
                lblUsernameValue.UseCompatibleTextRendering = true;
            if (lblDisplayNameValue != null)
                lblDisplayNameValue.UseCompatibleTextRendering = true;
            if (lblEmailValue != null)
                lblEmailValue.UseCompatibleTextRendering = true;
            if (lblTelValue != null)
                lblTelValue.UseCompatibleTextRendering = true;
            if (lblGroupValue != null)
                lblGroupValue.UseCompatibleTextRendering = true;
            if (lblActiveValue != null)
                lblActiveValue.UseCompatibleTextRendering = true;
            if (lblRolesValue != null)
                lblRolesValue.UseCompatibleTextRendering = true;
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            SetStaticLabels();
            ShowCurrentUser();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetStaticLabels()
        {
            // Override any mojibake from Designer by setting Unicode literals at runtime
            lblUsername.Text = "Tên đăng nhập:";
            lblDisplayName.Text = "Tên hiển thị:";
            lblEmail.Text = "Email:";
            lblTel.Text = "Điện thoại:";
            lblGroup.Text = "Nhóm:";
            lblActive.Text = "Trạng thái:";
            lblRoles.Text = "Vai trò:";
            Text = "Thông tin nhân viên";
        }

        private void ShowCurrentUser()
        {
            Account u = AppSession.CurrentUser;
            if (u == null)
            {
                lblUsernameValue.Text = "(chưa đăng nhập)";
                lblDisplayNameValue.Text = string.Empty;
                lblEmailValue.Text = string.Empty;
                lblTelValue.Text = string.Empty;
                lblGroupValue.Text = string.Empty;
                lblActiveValue.Text = string.Empty;
                lblRolesValue.Text = string.Empty;
                return;
            }

            lblUsernameValue.Text = u.UserName ?? string.Empty;
            lblDisplayNameValue.Text = u.DisplayName ?? string.Empty;
            lblEmailValue.Text = u.Email ?? string.Empty;
            lblTelValue.Text = u.Tel ?? string.Empty;
            lblGroupValue.Text = u.Group ?? string.Empty;
            lblActiveValue.Text = u.IsActive ? "Hoạt động" : "Ngừng";
            var roles = u.Roles != null ? string.Join(", ", u.Roles.Select(r => r.Name)) : string.Empty;
            lblRolesValue.Text = roles;
        }
    }
}
