using _2312590_NNTDan_Lab07.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace _2312590_NNTDan_Lab07
{
    public partial class TableBillsForm : Form
    {
        private readonly int _tableId;
        private readonly RestaurantContext _db = new RestaurantContext();

        public TableBillsForm(int tableId)
        {
            InitializeComponent();
            _tableId = tableId;
        }

        private void TableBillsForm_Load(object sender, EventArgs e)
        {
            DiningTable tbl = _db.Tables.Find(_tableId);
            if (tbl == null)
                return;
            Text = $"Hóa don c?a {tbl.Name}";
            var bills = _db.Bills.Where(b => b.TableId == _tableId)
                                .OrderByDescending(b => b.CheckIn)
                                .ToList();
            lstDates.DisplayMember = nameof(Bill.CheckIn);
            lstDates.ValueMember = nameof(Bill.Id);
            lstDates.DataSource = bills;
        }

        private void lstDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDates.SelectedValue == null)
                return;
            int billId = (int)lstDates.SelectedValue;
            Bill bill = _db.Bills.Find(billId);
            if (bill == null)
                return;
            lblInfo.Text = $"Ngày: {bill.CheckIn:dd/MM/yyyy HH:mm} - Nhân viên: {bill.Staff?.DisplayName ?? string.Empty} - Gi?m: {bill.DiscountPercent}% - {(bill.IsPaid ? "Ðã thanh toán" : "Chua thanh toán")}";
            var items = _db.BillDetails.Where(d => d.BillId == billId)
                                    .Select(d => new { d.Id, FoodName = d.Food.Name, d.Quantity, d.UnitPrice, Total = d.Quantity * d.UnitPrice, d.Notes })
                                    .ToList();
            dgvItems.DataSource = items;
        }
    }
}