using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Basic_Command
{
    public partial class MainForm : Form
    {
        string connectionString = Configs.conn;
        private Button selectedTableButton = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadTables();

            if (!Session.IsAdmin)
            {
                btnAddTable.Visible = false;
                btnEditTable.Visible = false;
                btnDeleteTable.Visible = false;
                btnCategoryForm.Visible = false;
                btnAccountManager.Visible = false;
            }
        }

        private void LoadTables()
        {
            flpTables.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ID, Name, Status FROM TableFood";

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Button btn = new Button();
                        btn.Width = 120;
                        btn.Height = 70;
                        btn.Text = reader["Name"].ToString() + Environment.NewLine + reader["Status"].ToString();
                        btn.Tag = Convert.ToInt32(reader["ID"]);
                        btn.BackColor = reader["Status"].ToString() == "Trống" ? Color.LightGreen : Color.LightCoral;
                        btn.ContextMenuStrip = cmsTableMenu;
                        btn.Click += Btn_Click;

                        flpTables.Controls.Add(btn);
                    }
                }
            }
        }

        // Changed: clicking a table no longer opens BillsForm.
        // Instead it selects/highlights the table and updates lblStatus.
        private void Btn_Click(object sender, EventArgs e)
        {
            Button tableButton = sender as Button;
            if (tableButton == null) return;

            // un-highlight previous selection
            if (selectedTableButton != null && !selectedTableButton.IsDisposed)
            {
                // restore color based on its status text
                string[] partsPrev = selectedTableButton.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                string statusPrev = partsPrev.Length > 1 ? partsPrev[1] : "";
                selectedTableButton.BackColor = statusPrev == "Trống" ? Color.LightGreen : Color.LightCoral;
            }

            // highlight current selection
            selectedTableButton = tableButton;
            selectedTableButton.BackColor = Color.Gold;

            int tableID = (int)tableButton.Tag;
            // Update status label with table information (no form shown)
            lblStatus.Text = $"Bàn đã chọn: {tableButton.Text.Replace(Environment.NewLine, " | ")} (ID: {tableID})";
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Find max numeric suffix for existing tables named "Bàn N"
                using (SqlCommand maxCmd = conn.CreateCommand())
                {
                    maxCmd.CommandText = "SELECT ISNULL(MAX(TRY_CAST(REPLACE(Name, N'Bàn ', '') AS INT)), 0) FROM TableFood WHERE Name LIKE N'Bàn %'";
                    object scalar = maxCmd.ExecuteScalar();
                    int max = 0;
                    if (scalar != null && scalar != DBNull.Value)
                    {
                        int.TryParse(scalar.ToString(), out max);
                    }
                    int next = max + 1;

                    using (SqlCommand insertCmd = conn.CreateCommand())
                    {
                        insertCmd.CommandText = "INSERT INTO TableFood (Name, Status) VALUES (@name, N'Trống')";
                        insertCmd.Parameters.AddWithValue("@name", $"Bàn {next}");
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }

            LoadTables();
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            if (flpTables.Controls.Count == 0) return;

            Button btn = (Button)flpTables.Controls[0];
            int id = (int)btn.Tag;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE TableFood SET Name = @name WHERE ID = @id";
                cmd.Parameters.AddWithValue("@name", "Bàn VIP");
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            LoadTables();
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            if (flpTables.Controls.Count == 0) return;

            Button btn = (Button)flpTables.Controls[0];
            int id = (int)btn.Tag;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM TableFood WHERE ID = @id";
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            LoadTables();
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            if (cmsTableMenu.SourceControl is Button btn)
            {
                int id = (int)btn.Tag;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM TableFood WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                LoadTables();
            }
        }

        private void mnuViewBills_Click(object sender, EventArgs e)
        {
            BillsForm form = new BillsForm();
            form.ShowDialog();
        }

        private void mnuViewBillLog_Click(object sender, EventArgs e)
        {
            BillLogForm form = new BillLogForm();
            form.ShowDialog();
        }

        private void btnCategoryForm_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm();
            categoryForm.ShowDialog();
        }

        private void btnAccountManager_Click(object sender, EventArgs e)
        {
            AccountManager accountForm = new AccountManager();
            accountForm.ShowDialog();
        }
    }
}