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
using System.Xml.Linq;

namespace _2312590_NNTDan_ChuDe05
{
    public partial class ListRole : Form
    {
        string connectionString = Configs.ConnectionString;

        public TextBox TxtName
        {
            get { return txtName; }
        }

        public ListRole()
        {
            InitializeComponent();
        }

        private void ListRole_Load(object sender, EventArgs e)
        {
            LoadRole();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void DisplayRole(DataRowView rowview)
        {
            try
            {
                txtName.Text = rowview["AccountName"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị thông tin vai trò: " + ex.Message, "Lỗi");
            }
        }
        public void LoadRole()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select RoleName, Path, Notes from Rolee";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                conn.Open();
                adapter.Fill(table);
                dgvListRole.DataSource = table;
                CheckAssignedRoles();
                conn.Close();
                conn.Dispose();


            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddRole addRole = new AddRole();
            addRole.EnabledbtnUpdate();
            addRole.ShowDialog();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            AddRole roleForm = new AddRole();
            roleForm.EnabledbtnAdd();
            if (dgvListRole.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvListRole.SelectedRows[0];
                DataRowView rowview = selectedRow.DataBoundItem as DataRowView;


                roleForm.Show();
                roleForm.DisplayRole(rowview);
            }

        }


        private void CheckAssignedRoles()
        {
            string accountName = txtName.Text.Trim();
            if (string.IsNullOrEmpty(accountName)) return;

            List<string> assignedRoles = new List<string>();
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "SELECT RoleName FROM Rolee, RoleAccount WHERE RoleID = ID and AccountName = @AccountName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AccountName", accountName);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    assignedRoles.Add(reader["RoleName"].ToString());
                }
                conn.Close();
            }

            foreach (DataGridViewRow row in dgvListRole.Rows)
            {
                if (row.Cells["RoleName"].Value != null)
                {
                    string roleName = row.Cells["RoleName"].Value.ToString();
                    row.Cells["Check"].Value = assignedRoles.Contains(roleName);
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

            CheckAssignedRoles();
        }
    }
}