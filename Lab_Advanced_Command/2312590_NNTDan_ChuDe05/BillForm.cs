using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2312590_NNTDan_ChuDe05
{
    public partial class BillForm : Form
    {
        string connectionString = Configs.ConnectionString;

        public BillForm()
        {
            InitializeComponent();
        }

        private void BillForm_Load(object sender, EventArgs e)
        {
            LoadBill();
            TongDTNgay();
            TongHoaDon();

        }
        public void LoadBill()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "exec pList @ngay = @day";
                cmd.Parameters.AddWithValue("@day", dtpNgay.Text);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();

                con.Open();
                adapter.Fill(table);
                con.Close();
                con.Dispose();
                dgvDanhSach.DataSource = table;
            }
        }

        private void dtpNgay_ValueChanged(object sender, EventArgs e)
        {
            LoadBill();
            TongDTNgay();
            TongHoaDon();

        }
        public void DisplayBill(DataRowView rowView)
        {
            try
            {
                txtName.Text = rowView["AccountName"].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void TongDTNgay()
        {
            Decimal tong = 0;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select sum(Amount + (Amount*Tax/100) - (Amount * Discount / 100)) from Bills where CheckoutDate = @ngay and Account = @name";
                cmd.Parameters.AddWithValue("@ngay", dtpNgay.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                    tong = Convert.ToDecimal(result);
                con.Close();
                con.Dispose();
            }
            lblSum.Text = tong.ToString();
        }

        private void TongHoaDon()
        {
            Decimal tong = 0;
            using (SqlConnection con = new SqlConnection(Configs.ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select count(ID) from Bills where CheckoutDate = @ngay and Account = @name";
                cmd.Parameters.AddWithValue("@ngay", dtpNgay.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                    tong = Convert.ToDecimal(result);
                con.Close();
                con.Dispose();
            }
            lblCount.Text = tong.ToString();
        }

    }
}
