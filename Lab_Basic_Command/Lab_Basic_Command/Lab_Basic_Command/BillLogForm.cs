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
	public partial class BillLogForm : Form
	{
		private string connectionString = Configs.conn;

		public BillLogForm()
		{
			InitializeComponent();
		}

		private void BillLogForm_Load(object sender, EventArgs e)
		{
			LoadBillLog();
		}

		private void LoadBillLog()
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				SqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = @"
                    SELECT 
                        b.ID,
                        b.Account,
                        b.CreatedDate,
                        ISNULL(b.TotalAmount,0) AS TotalAmount,
                        ISNULL(b.Discount,0) AS Discount,
                        (ISNULL(b.TotalAmount,0) - ISNULL(b.Discount,0)) AS Final,
                        COUNT(bd.ID) AS ItemCount
                    FROM Bills b
                    LEFT JOIN BillDetails bd ON b.ID = bd.BillID
                    GROUP BY b.ID, b.Account, b.CreatedDate, b.TotalAmount, b.Discount
                    ORDER BY b.CreatedDate DESC";

				SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				adapter.Fill(dt);

				dgvBillLog.DataSource = dt;

				double total = 0, discount = 0, final = 0;
				foreach (DataRow row in dt.Rows)
				{
					total += Convert.ToDouble(row["TotalAmount"]);
					discount += Convert.ToDouble(row["Discount"]);
					final += Convert.ToDouble(row["Final"]);
				}

				lblCount.Text = $"Tổng số hóa đơn: {dt.Rows.Count}";
				lblTotal.Text = $"Tổng tiền (chưa giảm): {total:#,##0} VNĐ";
				lblDiscount.Text = $"Tổng giảm giá: {discount:#,##0} VNĐ";
				lblFinal.Text = $"Thực thu: {final:#,##0} VNĐ";
			}
		}

		private void btnReload_Click(object sender, EventArgs e)
		{
			LoadBillLog();
		}
	}
}
