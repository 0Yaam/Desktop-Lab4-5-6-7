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
	public partial class BillDetailsForm : Form
	{
		private int billID;
		public BillDetailsForm(int billID)
		{
			InitializeComponent();
			this.billID = billID;
		}

		private void BillDetailsForm_Load(object sender, EventArgs e)
		{
			string connectionString = Configs.conn;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
			
				SqlCommand cmd = connection.CreateCommand();
				cmd.CommandText = "SELECT ID, Account, CreatedDate, Discount, TotalAmount FROM Bills WHERE ID = @billID";
				cmd.Parameters.AddWithValue("@billID", billID);

				connection.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					lblBillID.Text = $"Mã hóa đơn: {reader["ID"]}";
					lblAccount.Text = $"Tài khoản: {reader["Account"]}";
					lblCreatedDate.Text = $"Ngày lập: {Convert.ToDateTime(reader["CreatedDate"]):dd/MM/yyyy}";
					lblTotal.Text = $"Tổng tiền: {Convert.ToDouble(reader["TotalAmount"]):#,##0} VNĐ";
					lblDiscount.Text = $"Giảm giá: {Convert.ToDouble(reader["Discount"]):#,##0} VNĐ";
					lblFinal.Text = $"Thực thu: {(Convert.ToDouble(reader["TotalAmount"]) - Convert.ToDouble(reader["Discount"])):#,##0} VNĐ";
				}
				reader.Close();

				cmd.CommandText = @"
                    SELECT f.Name AS [Tên món], bd.Quantity AS [Số lượng],
                           bd.UnitPrice AS [Đơn giá],
                           (bd.Quantity * bd.UnitPrice) AS [Thành tiền]
                    FROM BillDetails bd
                    JOIN Food f ON f.ID = bd.FoodID
                    WHERE bd.BillID = @billID";

				SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				adapter.Fill(dt);
				dgvBillDetails.DataSource = dt;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
