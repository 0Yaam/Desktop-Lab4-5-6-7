using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _2312590_NNTDan_ChuDe05
{
    public partial class OrderForm : Form
    {
        private readonly string connectionString = Configs.ConnectionString;
        private DataTable _orders;

        public OrderForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (dtpTuNgay != null) dtpTuNgay.Value = DateTime.Today;
            if (dtpDenNgay != null) dtpDenNgay.Value = DateTime.Today;
            LoadOrders(applyDateFilter: chkApDung.Checked);
        }


        private void LoadOrders(bool applyDateFilter)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = conn;

                if (applyDateFilter)
                {
                    DateTime fromDate = dtpTuNgay.Value.Date;
                    DateTime toExclusive = dtpDenNgay.Value.Date.AddDays(1);

                    cmd.CommandText = @"
                        SELECT 
                            b.ID,
                            b.Name,
                            b.TableID,
                            b.Amount,     
                            b.Discount,   
                            b.Tax,
                            b.Status,
                            b.CheckoutDate,
                            b.Account
                        FROM Bills b
                        WHERE b.Status = 1
                          AND b.CheckoutDate >= @fromDate
                          AND b.CheckoutDate <  @toExclusive
                        ORDER BY b.CheckoutDate ASC, b.ID ASC";
                    cmd.Parameters.Add("@fromDate", SqlDbType.SmallDateTime).Value = fromDate;
                    cmd.Parameters.Add("@toExclusive", SqlDbType.SmallDateTime).Value = toExclusive;
                }
                else
                {
                    cmd.CommandText = @"
                        SELECT 
                            b.ID,
                            b.Name,
                            b.TableID,
                            b.Amount,
                            b.Discount,
                            b.Tax,
                            b.Status,
                            b.CheckoutDate,
                            b.Account
                        FROM Bills b
                        WHERE b.Status = 1
                        ORDER BY b.CheckoutDate ASC, b.ID ASC";
                }

                using (var ad = new SqlDataAdapter(cmd))
                {
                    _orders = new DataTable();
                    ad.Fill(_orders);
                    dgvDSHD.DataSource = _orders;
                }
            }

            AutoSizeGridColumns();
            UpdateTotals();
        }

        private void AutoSizeGridColumns()
        {
            if (dgvDSHD.Columns.Count == 0) return;

            dgvDSHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            if (dgvDSHD.Columns.Contains("Name"))
                dgvDSHD.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        private void UpdateTotals()
        {
            decimal totalGross = 0m;     
            decimal totalDiscount = 0m; 
            decimal totalNet = 0m;    

            if (_orders != null)
            {
                foreach (DataRow r in _orders.Rows)
                {
                    decimal amount = ToDec(r["Amount"]);
                    decimal discount = ToDec(r["Discount"]);  
                    decimal discountMoney = (discount <= 1m) ? amount * discount : amount * (discount / 100m);

                    decimal net = amount - discountMoney; 

                    totalGross += amount;
                    totalDiscount += discountMoney;
                    totalNet += net;
                }
            }

            txtTienChuaGiamGia.Text = totalGross.ToString("N0");
            txtGiamGia.Text = totalDiscount.ToString("N0");
            txtTongDoanhThu.Text = totalNet.ToString("N0");
        }

        private decimal ToDec(object v)
        {
            if (v == null || v == DBNull.Value) return 0m;
            try { return Convert.ToDecimal(v); } catch { return 0m; }
        }


        private void chkApDung_CheckedChanged(object sender, EventArgs e)
        {
            LoadOrders(applyDateFilter: chkApDung.Checked);
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (chkApDung.Checked)
                LoadOrders(applyDateFilter: true);
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (chkApDung.Checked)
                LoadOrders(applyDateFilter: true);
        }


        private void dgvDSHD_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = ((DataRowView)dgvDSHD.Rows[e.RowIndex].DataBoundItem)?.Row;
            if (row == null) return;

            int billId = Convert.ToInt32(row["ID"]);
            using (var f = new OrderDetailsForm(billId))
            {
                f.ShowDialog(this);
            }
        }
    }
}
