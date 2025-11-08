using _2312590_NNTDan_Lab07.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace _2312590_NNTDan_Lab07
{
    public partial class TableSalesLogForm : Form
    {
        private readonly int _tableId;
        private readonly RestaurantContext _db = new RestaurantContext();

        public TableSalesLogForm(int tableId)
        {
            InitializeComponent();
            _tableId = tableId;
        }

        private void TableSalesLogForm_Load(object sender, EventArgs e)
        {
            DiningTable tbl = _db.Tables.Find(_tableId);
            if (tbl == null)
                return;
            Text = $"Nhật ký bán hàng - {tbl.Name}";
            var logs = _db.Bills.Where(b => b.TableId == _tableId)
               .Select(b => new
               {
                   b.Id,
                   Date = b.CheckIn,
                   StaffName = b.Staff.DisplayName,
                   b.DiscountPercent,
                   b.IsPaid,
                   Gross = b.Details.Sum(d => (int?)(d.Quantity * d.UnitPrice)) ?? 0,
                   Discount = ((b.Details.Sum(d => (int?)(d.Quantity * d.UnitPrice)) ?? 0) * b.DiscountPercent / 100),
                   Net = ((b.Details.Sum(d => (int?)(d.Quantity * d.UnitPrice)) ?? 0) - ((b.Details.Sum(d => (int?)(d.Quantity * d.UnitPrice)) ?? 0) * b.DiscountPercent / 100))
               })
                .OrderByDescending(x => x.Date)
                .ToList();
            dgvLog.DataSource = logs;
            lblSummary.Text = $"Id: {logs.Count} - Tổng: {logs.Sum(x => x.Gross):N0} - Giảm: {logs.Sum(x => x.Discount):N0} - Thực thu: {logs.Sum(x => x.Net):N0}";
        }
    }
}
