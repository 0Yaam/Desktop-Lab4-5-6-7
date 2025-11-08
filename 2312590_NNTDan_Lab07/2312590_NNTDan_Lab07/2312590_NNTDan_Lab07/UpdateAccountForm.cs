using _2312590_NNTDan_Lab07.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace _2312590_NNTDan_Lab07
{
    public partial class UpdateAccountForm : Form
    {
        private readonly RestaurantContext _db = new RestaurantContext();
        private readonly int _accountId;

        public UpdateAccountForm(int accountId = 0)
        {
            InitializeComponent();
            _accountId = accountId;
        }

        private void UpdateAccountForm_Load(object sender, EventArgs e)
        {
            LoadRoles();
            if (_accountId > 0)
            {
                Account acc = _db.Accounts.Where(a => a.Id == _accountId).FirstOrDefault();
                if (acc != null)
                {
                    txtId.Text = acc.Id.ToString();
                    txtUserName.Text = acc.UserName;
                    txtDisplayName.Text = acc.DisplayName;
                    txtEmail.Text = acc.Email;
                    txtTel.Text = acc.Tel;
                    chkIsActive.Checked = acc.IsActive;
                    // select roles
                    var selected = new HashSet<int>(acc.Roles.Select(r => r.Id));
                    for (int i = 0; i < clbRoles.Items.Count; i++)
                    {
                        var item = (Role)clbRoles.Items[i];
                        clbRoles.SetItemChecked(i, selected.Contains(item.Id));
                    }
                }
            }
        }

        private void LoadRoles()
        {
            var roles = _db.Roles.OrderBy(r => r.Name).ToList();
            clbRoles.DataSource = roles;
            clbRoles.DisplayMember = "Name";
            clbRoles.ValueMember = "Id";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("UserName không được để trống");
                return;
            }
            var isNew = _accountId == 0;
            Account acc = isNew ? new Account() : _db.Accounts.Find(_accountId);

            if (!isNew && acc == null)
            {
                MessageBox.Show("Không tìm thấy tài khoản");
                return;
            }
            acc.UserName = txtUserName.Text.Trim();
            acc.DisplayName = txtDisplayName.Text?.Trim();
            acc.Email = txtEmail.Text?.Trim();
            acc.Tel = txtTel.Text?.Trim();
            acc.IsActive = chkIsActive.Checked;

            // roles
            var selectedRoleIds = clbRoles.CheckedItems.Cast<Role>().Select(r => r.Id).ToList();
            var roles = _db.Roles.Where(r => selectedRoleIds.Contains(r.Id)).ToList();
            acc.Roles = roles;

            if (isNew)
            {
                // default password for new account
                acc.Password = "123456";
                _db.Accounts.Add(acc);
            }
            _db.SaveChanges();
            DialogResult = DialogResult.OK;
        }
    }
}
