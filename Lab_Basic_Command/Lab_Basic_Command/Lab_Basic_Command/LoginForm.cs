using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Basic_Command
{
	public partial class LoginForm : Form
	{
		string connectionString = Configs.conn;
		public LoginForm()
		{
			InitializeComponent();
		}

		private void LoginForm_Load(object sender, EventArgs e)
		{

		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			string username = txtUserName.Text.Trim();
			string password = txtPassword.Text.Trim();

			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			{
				MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				try
				{
					conn.Open();

					string hashedPassword = password;

					string query = @"SELECT ID, GroupID FROM Account 
                             WHERE Username=@username AND Password=@password AND IsActive=1";
					SqlCommand cmd = new SqlCommand(query, conn);
					cmd.Parameters.AddWithValue("@username", username);
					cmd.Parameters.AddWithValue("@password", hashedPassword);

					SqlDataReader reader = cmd.ExecuteReader();
					if (reader.Read())
					{
						Session.AccountID = Convert.ToInt32(reader["ID"]);
						Session.Username = username;
						Session.IsAdmin = Convert.ToInt32(reader["GroupID"]) == 1; //1 = admin

						reader.Close();

						MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
						this.Hide();
						MainForm mainForm = new MainForm();
						mainForm.ShowDialog();
						this.Close();
					}
					else
					{
						MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi kết nối: " + ex.Message);
				}
			}
		}

		private string HashPassword(string password)
		{
			using (SHA256 sha = SHA256.Create())
			{
				byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
				return BitConverter.ToString(bytes).Replace("-", "").ToLower();
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

	}
}
