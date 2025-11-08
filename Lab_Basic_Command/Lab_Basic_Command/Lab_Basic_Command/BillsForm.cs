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
	public partial class BillsForm : Form
	{
		public BillsForm()
		{
			InitializeComponent();
		}

		private void btnView_Click(object sender, EventArgs e)
		{
			DateTime fromDate = dtpFromDate.Value;
			DateTime toDate = dtpToDate.Value;

			string connectionString = Configs.conn;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = connection.CreateCommand();
				cmd.CommandText = @"
            SELECT ID, Account, CreatedDate, Discount, TotalAmount 
            FROM Bills
            WHERE CreatedDate BETWEEN @from AND @to
            ORDER BY CreatedDate DESC";

				cmd.Parameters.AddWithValue("@from", fromDate);
				cmd.Parameters.AddWithValue("@to", toDate);

				SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				adapter.Fill(dt);

				dgvBills.DataSource = dt;

				double total = 0;
				double discount = 0;

				foreach (DataRow row in dt.Rows)
				{
					double billTotal = Convert.ToDouble(row["TotalAmount"]);
					double billDiscount = Convert.ToDouble(row["Discount"]);

					total += billTotal;
					discount += billDiscount;
				}

				double final = total - discount;

				lblTotal.Text = $"Tổng tiền (chưa giảm): {total:#,##0} VNĐ";
				lblDiscount.Text = $"Tổng giảm giá: {discount:#,##0} VNĐ";
				lblFinal.Text = $"Thực thu: {final:#,##0} VNĐ";
			}
		}

		private void dgvBills_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				int billID = Convert.ToInt32(dgvBills.Rows[e.RowIndex].Cells["ID"].Value);
				BillDetailsForm detailsForm = new BillDetailsForm(billID);
				detailsForm.ShowDialog();
			}
		}

		private void BillsForm_Load(object sender, EventArgs e)
		{
            dtpFromDate.Value = DateTime.Now.AddDays(-30);
            dtpToDate.Value = DateTime.Now;
        }

        private void btnGiam_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = dtpFromDate.Value.AddDays(-30);
        }

        private void btnTang_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = dtpFromDate.Value.AddDays(30);
        }
    }
}
