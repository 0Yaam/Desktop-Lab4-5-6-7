using System;
using System.Windows.Forms;
using BusinessLogic;

namespace _2312590_NNTDan_ChuDe05
{
    public partial class OrderDetailsForm : Form
    {
        private readonly int _billId;
        private readonly ReportingBL _reportBL = new ReportingBL();

        public OrderDetailsForm()
        {
            InitializeComponent();
        }

        public OrderDetailsForm(int billId) : this()
        {
            _billId = billId;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (lblOrderID != null) lblOrderID.Text = $"Hóa đơn #{_billId}";
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

            var lines = _reportBL.GetOrderLines(_billId);
            foreach (var ln in lines)
            {
                var item = new ListViewItem(ln.FoodName);
                item.SubItems.Add(ln.Unit);
                item.SubItems.Add(ln.UnitPrice.ToString("N0"));
                item.SubItems.Add(ln.Quantity.ToString("N0"));
                item.SubItems.Add(ln.LineTotal.ToString("N0"));
                lvDSMatHang.Items.Add(item);
            }

            lvDSMatHang.EndUpdate();
            AutoSizeLastColumn();
        }

        private void AutoSizeLastColumn()
        {
            if (lvDSMatHang.Columns.Count == 0) return;
            for (int i = 0; i < lvDSMatHang.Columns.Count; i++)
                lvDSMatHang.Columns[i].Width = -2;
        }

        private void lvDSMatHang_SelectedIndexChanged(object sender, EventArgs e) { }
        private void lblOrderID_Click(object sender, EventArgs e) { }
    }
}
