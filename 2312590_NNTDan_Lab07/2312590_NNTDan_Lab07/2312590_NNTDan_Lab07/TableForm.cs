using _2312590_NNTDan_Lab07.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace _2312590_NNTDan_Lab07
{
    public partial class TableForm : Form
    {
        private readonly RestaurantContext _db = new RestaurantContext();
        public TableForm()
        {
            InitializeComponent();
        }

        private void TableForm_Load(object sender, EventArgs e)
        {
            LoadTables();
        }

        private void LoadTables()
        {
            var data = _db.Tables
                .OrderBy(t => t.Name)
                .Select(t => new { t.Id, t.Name, t.IsActive, t.Notes })
                .ToList();
            dgvTables.DataSource = data;
        }

        private DiningTable GetSelectedTable()
        {
            if (dgvTables.CurrentRow == null)
                return null;
            var id = (int)dgvTables.CurrentRow.Cells["Id"].Value;
            return _db.Tables.Find(id);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadTables();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new UpdateTableForm())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    LoadTables();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DiningTable tbl = GetSelectedTable();
            if (tbl == null)
                return;
            using (var dlg = new UpdateTableForm(tbl.Id))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    LoadTables();
            }
        }



        private void mnuDelete_Click(object sender, EventArgs e)
        {
            var tbl = GetSelectedTable();
            if (tbl == null)
                return;

            // còn bill tham chiếu?
            bool hasBills = _db.Bills.Any(b => b.TableId == tbl.Id);
            if (hasBills)
            {
                if (MessageBox.Show(
                    "Bàn này đang được tham chiếu bởi hóa đơn (kể cả đã thanh toán). " +
                    "Không thể xóa do ràng buộc dữ liệu.\n\n" +
                    "Bạn có muốn đặt bàn này về trạng thái Inactive (ẩn) không?",
                    "Không thể xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    tbl.IsActive = false;
                    _db.SaveChanges();
                    LoadTables();
                    MessageBox.Show("Đã đặt bàn về Inactive.", "Thông báo");
                }
                return;
            }

            if (MessageBox.Show("Xóa bàn này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _db.Tables.Remove(tbl);
            _db.SaveChanges();
            LoadTables();
        }

        private void mnuViewBills_Click(object sender, EventArgs e)
        {
            DiningTable tbl = GetSelectedTable();
            if (tbl == null)
                return;
            using (var dlg = new TableBillsForm(tbl.Id))
            {
                dlg.ShowDialog(this);
            }
        }

        private void mnuViewSalesLog_Click(object sender, EventArgs e)
        {
            DiningTable tbl = GetSelectedTable();
            if (tbl == null)
                return;
            using (var dlg = new TableSalesLogForm(tbl.Id))
            {
                dlg.ShowDialog(this);
            }
        }

        private void btnViewCurrentBill_Click(object sender, EventArgs e)
        {
            DiningTable tbl = GetSelectedTable();
            if (tbl == null)
                return;
            Bill bill = _db.Bills
                .Where(b => b.TableId == tbl.Id && b.IsPaid == false)
                .OrderByDescending(b => b.CheckIn)
                .FirstOrDefault();
            if (bill == null)
            {
                MessageBox.Show("Bàn hiện không có hóa đơn mở", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (var dlg = new BillDetailsForm(bill.Id))
            {
                dlg.ShowDialog(this);
            }
        }

    }
}
