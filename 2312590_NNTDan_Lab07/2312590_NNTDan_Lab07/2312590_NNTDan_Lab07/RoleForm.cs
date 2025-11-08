using _2312590_NNTDan_Lab07.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2312590_NNTDan_Lab07
{
    public partial class RoleForm : Form
    {
        private readonly RestaurantContext _db = new RestaurantContext();

        public RoleForm()
        {
            InitializeComponent();
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            LoadRoles();
        }

        private void LoadRoles()
        {
            var roles = _db.Roles
            .OrderBy(r => r.Name)
            .Select(r => new { r.Id, r.Name, r.Description, UserCount = r.Accounts.Count })
            .ToList();
            dgvRoles.DataSource = roles;
        }

        private void LoadUsersForSelectedRole()
        {
            if (dgvRoles.CurrentRow == null)
            {
                dgvUsers.DataSource = null;
                return;
            }
            var roleId = (int)dgvRoles.CurrentRow.Cells["Id"].Value;
            var users = _db.Accounts
            .Where(u => u.Roles.Any(r => r.Id == roleId))
            .OrderBy(u => u.UserName)
            .Select(u => new { u.Id, u.UserName, u.DisplayName, u.Email, u.IsActive })
            .ToList();
            dgvUsers.DataSource = users;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadRoles();
            LoadUsersForSelectedRole();
        }

        private void dgvRoles_SelectionChanged(object sender, EventArgs e)
        {
            LoadUsersForSelectedRole();
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            using (var dlg = new UpdateRoleForm())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LoadRoles();
                }
            }
        }

        private void btnEditRole_Click(object sender, EventArgs e)
        {
            if (dgvRoles.CurrentRow == null)
                return;
            var roleId = (int)dgvRoles.CurrentRow.Cells["Id"].Value;
            using (var dlg = new UpdateRoleForm(roleId))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LoadRoles();
                }
            }
        }
    }
}
