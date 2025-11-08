using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _2312590_NNTDan_ChuDe05
{
    public partial class OrderDetailsForm : Form
    {
        private readonly string connectionString = Configs.ConnectionString;
        private readonly int _billId;

        public OrderDetailsForm()
        {
            InitializeComponent();
        }

        // Constructor dùng khi mở form từ OrderForm với billId
        public OrderDetailsForm(int billId) : this()
        {
            _billId = billId;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (lblOrderID != null)
                lblOrderID.Text = $"Hóa đơn #{_billId}";

            SetupListView();
            LoadOrderDetails();
        }

        private void SetupListView()
        {
            if (lvDSMatHang == null) return;

            lvDSMatHang.Clear();
            lvDSMatHang.View = View.Details;
            lvDSMatHang.FullRowSelect = true;
            lvDSMatHang.GridLines = true;
            lvDSMatHang.MultiSelect = false;

            lvDSMatHang.Columns.Add("Mặt hàng", 220);
            lvDSMatHang.Columns.Add("ĐVT", 80, HorizontalAlignment.Center);
            lvDSMatHang.Columns.Add("Đơn giá", 100, HorizontalAlignment.Right);
            lvDSMatHang.Columns.Add("Số lượng", 90, HorizontalAlignment.Right);
            lvDSMatHang.Columns.Add("Thành tiền", 120, HorizontalAlignment.Right);
        }

        private void LoadOrderDetails()
        {
            if (_billId <= 0) return;

            lvDSMatHang.BeginUpdate();
            lvDSMatHang.Items.Clear();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(@"
                SELECT 
                    f.Name      AS FoodName,
                    f.Unit      AS Unit,
                    f.Price     AS UnitPrice,
                    d.Quantity  AS Quantity,
                    (f.Price * d.Quantity) AS LineTotal
                FROM BillDetails d
                INNER JOIN Food f ON f.ID = d.FoodID
                WHERE d.InvoiceID = @id
                ORDER BY d.ID ASC;", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = _billId;
                conn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string foodName = rd["FoodName"]?.ToString() ?? "";
                        string unit = rd["Unit"]?.ToString() ?? "";
                        decimal unitPrice = SafeDec(rd["UnitPrice"]);
                        int quantity = SafeInt(rd["Quantity"]);
                        decimal lineTotal = SafeDec(rd["LineTotal"]);

                        var item = new ListViewItem(foodName);
                        item.SubItems.Add(unit);
                        item.SubItems.Add(unitPrice.ToString("N0"));
                        item.SubItems.Add(quantity.ToString("N0"));
                        item.SubItems.Add(lineTotal.ToString("N0"));
                        lvDSMatHang.Items.Add(item);
                    }
                }
            }

            lvDSMatHang.EndUpdate();
            AutoSizeLastColumn();
        }

        private void AutoSizeLastColumn()
        {
            if (lvDSMatHang.Columns.Count == 0) return;
            for (int i = 0; i < lvDSMatHang.Columns.Count - 1; i++)
                lvDSMatHang.Columns[i].Width = -2;  
            lvDSMatHang.Columns[lvDSMatHang.Columns.Count - 1].Width = -2;
        }

        private static decimal SafeDec(object v)
        {
            if (v == null || v == DBNull.Value) return 0m;
            try { return Convert.ToDecimal(v); } catch { return 0m; }
        }

        private static int SafeInt(object v)
        {
            if (v == null || v == DBNull.Value) return 0;
            try { return Convert.ToInt32(v); } catch { return 0; }
        }

        private void lvDSMatHang_SelectedIndexChanged(object sender, EventArgs e) { }
        private void lblOrderID_Click(object sender, EventArgs e) { }
    }
}
