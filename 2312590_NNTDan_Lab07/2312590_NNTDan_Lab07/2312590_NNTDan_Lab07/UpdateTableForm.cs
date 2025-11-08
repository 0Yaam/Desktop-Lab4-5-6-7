using _2312590_NNTDan_Lab07.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace _2312590_NNTDan_Lab07
{
    public partial class UpdateTableForm : Form
    {
        private readonly RestaurantContext _db = new RestaurantContext();
        private readonly int _tableId;

        public UpdateTableForm(int tableId = 0)
        {
            InitializeComponent();
            _tableId = tableId;
        }

        private void UpdateTableForm_Load(object sender, EventArgs e)
        {
            if (_tableId > 0)
            {
                DiningTable t = _db.Tables.Find(_tableId);
                if (t != null)
                {
                    txtId.Text = t.Id.ToString();
                    txtName.Text = t.Name;
                    chkActive.Checked = t.IsActive;
                    txtNotes.Text = t.Notes;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Tên bàn không được trống");
                return;
            }
            if (_tableId == 0)
            {
                var t = new DiningTable { Name = txtName.Text.Trim(), IsActive = chkActive.Checked, Notes = txtNotes.Text?.Trim() };
                _db.Tables.Add(t);
            }
            else
            {
                DiningTable t = _db.Tables.Find(_tableId);
                if (t == null)
                {
                    MessageBox.Show("Không tìm thấy bàn");
                    return;
                }
                t.Name = txtName.Text.Trim();
                t.IsActive = chkActive.Checked;
                t.Notes = txtNotes.Text?.Trim();
            }
            _db.SaveChanges();
            DialogResult = DialogResult.OK;
        }
    }
}
