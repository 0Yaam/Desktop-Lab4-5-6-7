using _2312590_NNTDan_Lab07.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace _2312590_NNTDan_Lab07
{
    public partial class BillsForm : Form
    {
        private readonly RestaurantContext _db = new RestaurantContext();
        private const int WindowDays = 30;

        public BillsForm()
        {
            InitializeComponent();
        }

        private void BillsForm_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays(-WindowDays);
            dtpTo.Value = DateTime.Today;
            LoadBills();
        }


        private void LoadBills()
        {
            IQueryable<Bill> q = _db.Bills;

            // chỉ lọc khi checkbox bật
            if (chkLocTheoDtp.Checked)
            {
                // đảm bảo from <= to
                if (dtpFrom.Value.Date > dtpTo.Value.Date)
                    dtpTo.Value = dtpFrom.Value;

                DateTime from = dtpFrom.Value.Date;
                DateTime to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);

                q = q.Where(b => b.CheckIn >= from && b.CheckIn <= to);
            }
            // nếu checkbox tắt: KHÔNG lọc gì -> lấy all

            var data = q
                .Select(b => new
                {
                    b.Id,
                    Table = b.Table.Name,
                    b.CheckIn,
                    b.CheckOut,
                    b.DiscountPercent,
                    b.IsPaid,
                    Staff = b.Staff.DisplayName,
                    Gross = b.Details.Sum(d => (int?)(d.Quantity * d.UnitPrice)) ?? 0,
                    Discount = ((b.Details.Sum(d => (int?)(d.Quantity * d.UnitPrice)) ?? 0) * b.DiscountPercent) / 100,
                    Net = (b.Details.Sum(d => (int?)(d.Quantity * d.UnitPrice)) ?? 0)
                          - (((b.Details.Sum(d => (int?)(d.Quantity * d.UnitPrice)) ?? 0) * b.DiscountPercent) / 100)
                })
                .OrderByDescending(x => x.CheckIn)
                .ToList();

            dgvBills.DataSource = data;

            lblGross.Text = $"Tổng trước giảm: {data.Sum(x => x.Gross):N0}";
            lblDiscount.Text = $"Tổng giảm: {data.Sum(x => x.Discount):N0}";
            lblNet.Text = $"Thực thu: {data.Sum(x => x.Net):N0}";
        }

        // SHIFT tiện dụng: dịch cả from & to cùng số ngày
        private void ShiftWindow(int days)
        {
            dtpFrom.Value = dtpFrom.Value.AddDays(days);
            dtpTo.Value = dtpTo.Value.AddDays(days);
            LoadBills();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = dtpFrom.Value.AddDays(-WindowDays); // chỉ đổi From
            if (chkLocTheoDtp.Checked)
                LoadBills();             // chỉ reload khi đang lọc
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = dtpFrom.Value.AddDays(+WindowDays); // chỉ đổi From
            if (chkLocTheoDtp.Checked)
                LoadBills();             // chỉ reload khi đang lọc
        }



        private void dgvBills_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            var billId = (int)dgvBills.Rows[e.RowIndex].Cells["Id"].Value;
            using (var dlg = new BillDetailsForm(billId))
                dlg.ShowDialog(this);
        }

        // Lọc realtime khi đổi DTP / bật tắt checkbox
        // Realtime chỉ khi đang lọc
        private void chkLocTheoDtp_CheckedChanged(object sender, EventArgs e) => LoadBills();
        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            if (chkLocTheoDtp.Checked)
                LoadBills();
        }
        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            if (chkLocTheoDtp.Checked)
                LoadBills();
        }

    }
}
