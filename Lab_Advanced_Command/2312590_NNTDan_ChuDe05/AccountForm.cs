using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _2312590_NNTDan_ChuDe05
{
    public partial class AccountForm : Form
    {
        string connectionString = Configs.ConnectionString;

        public AccountForm()
        {
            InitializeComponent();
        }


        private void AccountForm_Load(object sender, EventArgs e)
        {

            LoadAccount();
        }

        private void LoadAccount()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "select AccountName, FullName, Email, Tell, DateCreated from Account";

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();

                conn.Open();

                adapter.Fill(table);

                conn.Close();
                conn.Dispose();

                dgvDanhSachTaiKhoan.DataSource = table;
            }
        }



        public void ThemMoi()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                foreach (DataGridViewRow row in dgvDanhSachTaiKhoan.Rows)
                {
                    string accName = row.Cells[0].Value.ToString();
                    string fullName = row.Cells[1].Value.ToString();
                    string email = row.Cells[2].Value.ToString();
                    string tell = row.Cells[3].Value.ToString();
                    string dateCreated = row.Cells[4].Value.ToString();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "exec Account_Insert " +accName +", "+fullName+", "+email+", "+tell+", "+dateCreated;
                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable("Account_Insert");
                    da.Fill(dt);

                    dgvDanhSachTaiKhoan.DataSource = dt;

                    conn.Close();
                    conn.Dispose();
                }
                }
                LoadAccount();
        }

        public void CapNhat()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                foreach (DataGridViewRow row in dgvDanhSachTaiKhoan.Rows)
                {
                    string accName = row.Cells[0].Value.ToString();
                    string fullName = row.Cells[1].Value.ToString();
                    string email = row.Cells[2].Value.ToString();
                    string tell = row.Cells[3].Value.ToString();
                    string dateCreated = row.Cells[4].Value.ToString();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "exec Account_Update " + accName + ", N'" + fullName + "', " + email + ", " + tell + ", " + dateCreated;
                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable("Account_Update");
                    da.Fill(dt);

                    dgvDanhSachTaiKhoan.DataSource = dt;

                    conn.Close();
                    conn.Dispose();
                }
            }
            LoadAccount();
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachTaiKhoan.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedrow = dgvDanhSachTaiKhoan.SelectedRows[0];
                DataRowView rowview = selectedrow.DataBoundItem as DataRowView;

                AddAccount accountForm = new AddAccount();
                accountForm.Show(this);
                accountForm.DisplayAccount(rowview);
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            AddAccount addAccount = new AddAccount();
            addAccount.ShowDialog();

        }

        private void cmsDanhSachVaiTro_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachTaiKhoan.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvDanhSachTaiKhoan.SelectedRows[0];
                DataRowView rowview = selectedRow.DataBoundItem as DataRowView;

                ListRole listrole = new ListRole();
                listrole.Show(this);
                //Lấy dữ liệu từ dòng được chọn
                listrole.DisplayRole(rowview);
            }

        }

        private void cmsNhatKy_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachTaiKhoan.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedrow = dgvDanhSachTaiKhoan.SelectedRows[0];
                DataRowView rowview = selectedrow.DataBoundItem as DataRowView;

                BillForm billForm = new BillForm();
                billForm.Show(this);
                billForm.DisplayBill(rowview);
            }
        }

        private void btnThongKeHoaDon_Click(object sender, EventArgs e)
        {
            
        }
    }
}
