using _2312590_NNTDan_Lab07.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace _2312590_NNTDan_Lab07
{
    public partial class UpdateRoleForm : Form
    {
        private readonly RestaurantContext _db = new RestaurantContext();
        private readonly int _roleId;

        public UpdateRoleForm(int roleId = 0)
        {
            InitializeComponent();
            _roleId = roleId;
        }

        private void UpdateRoleForm_Load(object sender, EventArgs e)
        {
            if (_roleId > 0)
            {
                Role role = _db.Roles.Find(_roleId);
                if (role != null)
                {
                    txtRoleId.Text = role.Id.ToString();
                    txtRoleName.Text = role.Name;
                    txtDescription.Text = role.Description;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var name = txtRoleName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên vai trò không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var desc = txtDescription.Text?.Trim();

            if (_roleId == 0)
            {
                var role = new Role { Name = name, Description = desc };
                _db.Roles.Add(role);
            }
            else
            {
                Role role = _db.Roles.Find(_roleId);
                if (role == null)
                {
                    MessageBox.Show("Không tìm thấy vai trò", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                role.Name = name;
                role.Description = desc;
            }
            _db.SaveChanges();
            DialogResult = DialogResult.OK;
        }
    }
}
