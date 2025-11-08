using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic;
using DataAccess.OL;

namespace RestaurantManagementProject
{
    public partial class InHoaDon : Form
    {
        private readonly BillsBL _billsBL = new BillsBL();
        private readonly ReportingBL _reportBL = new ReportingBL();
        private readonly BanBL _banBL = new BanBL();

        private readonly int _billId;
        private readonly int _tableId;
        private readonly string _tableName;

        private List<OrderLineView> _lines = new List<OrderLineView>();
        private long _subTotal = 0;
        public bool DaHoanThanh { get; private set; } = false;

        public InHoaDon(int billId, int tableId, string tableName)
        {
            InitializeComponent();
            _billId = billId;
            _tableId = tableId;
            _tableName = tableName;

            InitListView();
            LoadDataAndRender();
        }

        private void InitListView()
        {
            lvHoaDon.View = View.Details;
            lvHoaDon.FullRowSelect = true;
            lvHoaDon.GridLines = true;
            lvHoaDon.CheckBoxes = false;
            lvHoaDon.Columns.Clear();

            lvHoaDon.Columns.Add("Tên món", 220);
            lvHoaDon.Columns.Add("ĐVT", 80);
            lvHoaDon.Columns.Add("Giá", 100, HorizontalAlignment.Right);
            lvHoaDon.Columns.Add("SL", 60, HorizontalAlignment.Center);
            lvHoaDon.Columns.Add("Thành tiền", 120, HorizontalAlignment.Right);
        }

        private void LoadDataAndRender()
        {
            lblBan.Text = _tableName;
            lblNgayIn.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            _lines = _reportBL.GetOrderLines(_billId);

            lvHoaDon.Items.Clear();
            _subTotal = 0;

            foreach (var d in _lines)
            {
                _subTotal += d.LineTotal;

                var it = new ListViewItem(d.FoodName);
                it.SubItems.Add(d.Unit);
                it.SubItems.Add(d.UnitPrice.ToString());
                it.SubItems.Add(d.Quantity.ToString());
                it.SubItems.Add(d.LineTotal.ToString("#,0"));
                lvHoaDon.Items.Add(it);
            }

            nudGiamGia.Minimum = 0;
            nudGiamGia.Maximum = Math.Max(_subTotal, 0);
            nudGiamGia.Value = 0;

            UpdateTongTien();
        }

        private void UpdateTongTien()
        {
            long giam = (long)nudGiamGia.Value;
            long total = _subTotal - giam;
            if (total < 0) total = 0;
            txtTongTien.Text = total.ToString("#,0");
        }

        private void nudGiamGia_ValueChanged(object sender, EventArgs e)
        {
            if (nudGiamGia.Value < 0) nudGiamGia.Value = 0;
            if (nudGiamGia.Value > _subTotal) nudGiamGia.Value = _subTotal;
            UpdateTongTien();
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            var bill = _billsBL.GetById(_billId);
            if (bill == null)
            {
                MessageBox.Show("Không tìm thấy hoá đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long giam = (long)nudGiamGia.Value;
            long total = _subTotal - giam;
            if (total < 0) total = 0;

            bill.Amount = (int)Math.Min(total, int.MaxValue);
            bill.Discount = (double)giam;
            bill.Status = true;
            bill.CheckoutDate = DateTime.Now;
            _billsBL.Update(bill);

            var ban = _banBL.GetAll().FirstOrDefault(x => x.ID == _tableId);
            if (ban != null)
            {
                ban.Status = 0;
                _banBL.Update(ban);
            }

            DaHoanThanh = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lvHoaDon_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtTongTien_TextChanged(object sender, EventArgs e) { }
    }
}
