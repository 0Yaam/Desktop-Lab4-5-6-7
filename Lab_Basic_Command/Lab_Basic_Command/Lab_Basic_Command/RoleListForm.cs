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

namespace Lab_Basic_Command
{
	public partial class RoleListForm : Form
	{
		private int accountID;
		private string connectionString = Configs.conn;
		public RoleListForm(int accountID)
		{
			InitializeComponent();
			this.accountID = accountID;
		}

		private void RoleListForm_Load(object sender, EventArgs e)
		{
			LoadRoles();
		}

		private void LoadRoles()
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				SqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = @"
                    SELECT 
                        r.RoleName AS [Tên vai trò],
                        r.Description AS [Mô tả],
                        ar.IsActive AS [Đang hoạt động]
                    FROM AccountRole ar
                    JOIN Role r ON r.ID = ar.RoleID
                    WHERE ar.AccountID = @AccountID";

				cmd.Parameters.AddWithValue("@AccountID", accountID);

				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				da.Fill(dt);

				dgvRoles.DataSource = dt;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
