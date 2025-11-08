using _2312590_NNTDan_Lab07.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace _2312590_NNTDan_Lab07
{
    public partial class MenuForm : Form
    {
        private readonly RestaurantContext _db = new RestaurantContext();
        private List<MenuItemRow> _currentData = new List<MenuItemRow>();

        // Print theming
        private readonly Color _accent = Color.FromArgb(242, 168, 74); // warm orange
        private readonly string _brand = "Nhà hàng";
        private readonly string _subtitle = DateTime.Now.Year.ToString();
        private readonly string _footerNote = "Trà đá miễn phí";

        // Thêm các biến màu sắc mới
        private readonly Color _cardBackground = Color.White;
        private readonly Color _cardBorder = Color.FromArgb(230, 230, 230);
        private readonly Color _nameColor = Color.FromArgb(51, 51, 51);
        private readonly Color _priceColor = Color.FromArgb(220, 53, 69); // Màu đỏ nổi bật
        private readonly Color _unitColor = Color.FromArgb(108, 117, 125);

        // UI helpers
        private readonly ToolTip _tooltip = new ToolTip();

        public MenuForm()
        {
            InitializeComponent();
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);

            // Thay đổi màu nền của FlowLayoutPanel
            flpMenu.BackColor = Color.FromArgb(248, 249, 250);
            flpMenu.FlowDirection = FlowDirection.LeftToRight;
            flpMenu.WrapContents = true;
            flpMenu.AutoScroll = true;
            flpMenu.Padding = new Padding(8);
            // ToolTip default timings
            _tooltip.AutoPopDelay = 8000;
            _tooltip.InitialDelay = 300;
            _tooltip.ReshowDelay = 100;
            _tooltip.ShowAlways = true;
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            // Ensure Vietnamese captions at runtime (avoid mojibake from designer encoding)
            if (lblType != null)
                lblType.Text = "Loại:";
            if (lblCategory != null)
                lblCategory.Text = "Danh mục:";
            if (lblKeyword != null)
                lblKeyword.Text = "Từ khóa:";
            if (btnFilter != null)
                btnFilter.Text = "Lọc";
            if (btnClose != null)
                btnClose.Text = "Đóng";
            this.Text = "Thực đơn";

            LoadTypeFilter();
            LoadCategories();
            LoadMenu();
        }

        private void LoadTypeFilter()
        {
            cbbTypeFilter.DisplayMember = "Text";
            cbbTypeFilter.ValueMember = "Value";
            cbbTypeFilter.Items.Clear();
            cbbTypeFilter.Items.Add(new
            {
                Text = "-- Tất cả --",
                Value = (CategoryType?)null
            });
            cbbTypeFilter.Items.Add(new
            {
                Text = "Đồ ăn",
                Value = (CategoryType?)CategoryType.Food
            });
            cbbTypeFilter.Items.Add(new
            {
                Text = "Thức uống",
                Value = (CategoryType?)CategoryType.Drink
            });
            cbbTypeFilter.SelectedIndex = 0;
        }

        private void LoadCategories()
        {
            var categories = _db.Categories
            .OrderBy(c => c.Name)
            .Select(c => new { c.Id, c.Name })
            .ToList();
            categories.Insert(0, new
            {
                Id = 0,
                Name = "-- Tất cả nhóm --"
            });

            cbbCategoryFilter.DisplayMember = "Name";
            cbbCategoryFilter.ValueMember = "Id";
            cbbCategoryFilter.DataSource = categories;
        }

        private void LoadMenu()
        {
            IQueryable<Food> q = _db.Foods.AsQueryable();

            // Type filter
            var typeItem = cbbTypeFilter.SelectedItem;
            if (typeItem != null)
            {
                PropertyInfo prop = typeItem.GetType().GetProperty("Value");
                var typeVal = (CategoryType?)prop?.GetValue(typeItem, null);
                if (typeVal != null)
                {
                    q = q.Where(f => f.Category.Type == typeVal);
                }
            }

            // Category filter
            int categoryId = 0;
            if (cbbCategoryFilter.SelectedValue is int id)
                categoryId = id;
            if (categoryId > 0)
            {
                q = q.Where(f => f.FoodCategoryId == categoryId);
            }

            // Keyword filter
            var keyword = (txtKeyword.Text ?? string.Empty).Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(f => f.Name.Contains(keyword) || f.Notes.Contains(keyword));
            }

            _currentData = q
            .OrderBy(f => f.Name)
            .Select(f => new MenuItemRow
            {
                Name = f.Name,
                Unit = f.Unit,
                Category = f.Category.Name,
                Price = f.Price,
                Notes = f.Notes
            })
            .ToList();

            RenderMenuCards();
        }

        private void RenderMenuCards()
        {
            flpMenu.SuspendLayout();
            flpMenu.Controls.Clear();
            foreach (var it in _currentData)
            {
                var panel = CreateCard(it);
                flpMenu.Controls.Add(panel);
            }
            flpMenu.ResumeLayout();
        }

        // Cập nhật phương thức CreateCard với bố cục đẹp hơn và vẽ tùy chỉnh
        private Control CreateCard(MenuItemRow it)
        {
            var card = new Panel
            {
                Height = 140,
                Width = CalcCardWidth(),
                Margin = new Padding(8),
                BackColor = Color.Transparent, // we'll paint background ourselves
                BorderStyle = BorderStyle.None,
                Tag = it
            };

            // enable double buffering (DoubleBuffered is protected) via reflection
            typeof(Panel).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(card, true, null);

            // Prepare child labels (transparent so custom paint shows through)
            int innerPad = 14;
            int avatarSize = 64;
            int w = card.Width - innerPad * 2 - avatarSize - 12; // room for avatar

            var lblName = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = _nameColor,
                Text = it.Name,
                Location = new Point(innerPad + avatarSize + 12, 18),
                Size = new Size(w, 28),
                TextAlign = ContentAlignment.MiddleLeft,
                UseCompatibleTextRendering = true,
                BackColor = Color.Transparent,
                Tag = "name"
            };

            var lblCategory = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = Color.DimGray,
                Text = it.Category,
                Location = new Point(innerPad + avatarSize + 12, 44),
                Size = new Size(w / 2, 20),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                UseCompatibleTextRendering = true,
                Tag = "category"
            };

            var lblUnit = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = _unitColor,
                Text = it.Unit,
                Location = new Point(innerPad + avatarSize + 12, 62),
                Size = new Size(w, 20),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                UseCompatibleTextRendering = true,
                Tag = "unit"
            };

            var lblPrice = new Label
            {
                AutoSize = false,
                Text = FormatPrice(it.Price),
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(card.Width - innerPad - 140, 18),
                Size = new Size(140, 36),
                ForeColor = _priceColor,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                UseCompatibleTextRendering = true,
                BackColor = Color.Transparent,
                Tag = "price"
            };

            // Hover and click behavior
            card.MouseEnter += (s, e) =>
            {
                card.Cursor = Cursors.Hand;
                card.Padding = new Padding(2);
                card.Invalidate();
            };
            card.MouseLeave += (s, e) =>
            {
                card.Cursor = Cursors.Default;
                card.Padding = Padding.Empty;
                card.Invalidate();
            };

            // Click shows details (non-blocking simple dialog)
            card.Click += (s, e) => ShowItemDetails(it);

            // also forward clicks from labels to show details directly (avoid PerformClick)
            foreach (Control c in new Control[] { lblName, lblCategory, lblUnit, lblPrice })
            {
                c.Click += (s, e) => ShowItemDetails(it);
            }

            // ToolTip
            if (!string.IsNullOrEmpty(it.Notes))
            {
                _tooltip.SetToolTip(card, it.Notes);
            }

            // Custom paint: shadow, gradient background, accent strip, avatar circle
            card.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(4, 6, card.Width - 8, card.Height - 12);

                // shadow
                using (var shadowBrush = new SolidBrush(Color.FromArgb(35, 0, 0, 0)))
                {
                    var shadowRect = new Rectangle(rect.X + 4, rect.Y + 6, rect.Width, rect.Height);
                    using (var pathShadow = GetRoundedRectPath(shadowRect, 10))
                        g.FillPath(shadowBrush, pathShadow);
                }

                // gradient background
                using (var brush = new LinearGradientBrush(rect, Color.FromArgb(255, 255, 255), Color.FromArgb(250, 250, 250), 90f))
                {
                    using (var path = GetRoundedRectPath(rect, 10))
                        g.FillPath(brush, path);
                }

                // left accent strip
                var stripRect = new Rectangle(rect.Left + 6, rect.Top + 6, 6, rect.Height - 12);
                using (var stripBrush = new SolidBrush(_accent))
                    g.FillRectangle(stripBrush, stripRect);

                // border
                using (var pen = new Pen(_cardBorder, 1))
                {
                    using (var path = GetRoundedRectPath(rect, 10))
                        g.DrawPath(pen, path);
                }

                // avatar circle with initial
                var avatarCenter = new Rectangle(rect.Left + 18, rect.Top + (rect.Height - avatarSize) / 2, avatarSize, avatarSize);
                using (var avatarBrush = new LinearGradientBrush(avatarCenter, Color.FromArgb(255, 255, 255), _accent, 45f))
                {
                    using (var path = new GraphicsPath())
                    {
                        path.AddEllipse(avatarCenter);
                        g.FillPath(avatarBrush, path);
                        using (var pen = new Pen(Color.FromArgb(200, Color.Black), 1))
                            g.DrawPath(pen, path);
                    }
                }

                // initial letter
                string initial = string.IsNullOrEmpty(it.Name) ? "?" : it.Name.Substring(0, 1).ToUpperInvariant();
                using (var f = new Font("Segoe UI", 22, FontStyle.Bold))
                using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                using (var brush = new SolidBrush(Color.White))
                {
                    g.DrawString(initial, f, brush, avatarCenter, sf);
                }
            };

            // Add controls after paint is set
            card.Controls.Add(lblName);
            card.Controls.Add(lblCategory);
            card.Controls.Add(lblUnit);
            card.Controls.Add(lblPrice);

            // Ensure child controls update hover visuals by duplicating the same effects
            foreach (Control ctl in card.Controls)
            {
                ctl.MouseEnter += (s, e) =>
                {
                    card.Cursor = Cursors.Hand;
                    card.Padding = new Padding(2);
                    card.Invalidate();
                };
                ctl.MouseLeave += (s, e) =>
                {
                    card.Cursor = Cursors.Default;
                    card.Padding = Padding.Empty;
                    card.Invalidate();
                };
            }

            return card;
        }

        // Thêm phương thức tạo đường dẫn bo góc
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int d = Math.Max(0, radius * 2);

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.X + rect.Width - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.X + rect.Width - d, rect.Y + rect.Height - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        private int CalcCardWidth()
        {
            // available client width excluding panel padding
            var containerW = Math.Max(0, flpMenu.ClientSize.Width - flpMenu.Padding.Left - flpMenu.Padding.Right);

            // desired card width (including desired spacing)
            const int desired = 320;
            const int hMargin = 16; // approx horizontal margin between cards

            // compute how many columns fit at desired width
            var columns = Math.Max(1, containerW / (desired + hMargin));

            if (columns > 1)
            {
                // compute width so cards do not stretch to full width when multiple columns are possible
                var available = containerW - (columns + 1) * 8; // keep same margin logic as before
                var width = available / columns;
                return Math.Max(260, Math.Min(desired, width));
            }

            // single column: allow it to use most of the space but leave some breathing room
            return Math.Max(260, Math.Min(desired, containerW - 16));
        }

        private void flpMenu_SizeChanged(object sender, EventArgs e)
        {
            // Re-layout cards on resize
            foreach (Panel p in flpMenu.Controls.OfType<Panel>())
            {
                p.Width = CalcCardWidth();

                int innerPad = 14;
                int avatarSize = 64;
                int w = p.Width - innerPad * 2 - avatarSize - 12;

                var lbls = p.Controls.OfType<Label>().ToDictionary(l => (string)l.Tag);
                if (lbls.TryGetValue("name", out var lblName))
                {
                    lblName.Left = innerPad + avatarSize + 12;
                    lblName.Width = w;
                }
                if (lbls.TryGetValue("category", out var lblCat))
                {
                    lblCat.Left = innerPad + avatarSize + 12;
                }
                if (lbls.TryGetValue("unit", out var lblUnit))
                {
                    lblUnit.Left = innerPad + avatarSize + 12;
                    lblUnit.Width = w;
                }
                if (lbls.TryGetValue("price", out var lblPrice))
                {
                    lblPrice.Left = p.Width - innerPad - 140;
                }

                p.Invalidate();
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadMenu();
        }



        // Styled two-column print like sample (no images)
        private void StyledPrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle content = e.MarginBounds;
            int padding = 24;
            int gutter = 24;

            // background panel
            using (var b = new SolidBrush(_accent))
            {
                e.Graphics.FillRectangle(b, content);
            }

            // header with extra spacing between MENU and brand
            var menuFont = new Font("Segoe UI", 38, FontStyle.Bold);
            var brandFont = new Font("Segoe UI", 20, FontStyle.Bold | FontStyle.Italic);
            var yearFont = new Font("Segoe UI", 14, FontStyle.Regular);
            var white = Brushes.White;

            int headerCenterX = content.Left + content.Width / 2;
            float y = content.Top + 24; // more top padding
            var sfCenter = new StringFormat { Alignment = StringAlignment.Center };
            e.Graphics.DrawString("MENU", menuFont, white, headerCenterX, y, sfCenter);
            y += 56; // more space
            e.Graphics.DrawString(_brand, brandFont, white, headerCenterX, y, sfCenter);
            y += 36; // more space
            e.Graphics.DrawString(_subtitle, yearFont, white, headerCenterX, y, sfCenter);
            y += 34;

            // inner white area for items
            Rectangle inner = new Rectangle(content.Left + padding, (int)y + 10, content.Width - padding * 2, content.Bottom - (int)y - 60);
            e.Graphics.FillRectangle(Brushes.White, inner);

            // columns
            int colWidth = (inner.Width - gutter) / 2;
            Rectangle colLeft = new Rectangle(inner.Left, inner.Top, colWidth, inner.Height);
            Rectangle colRight = new Rectangle(inner.Left + colWidth + gutter, inner.Top, colWidth, inner.Height);

            // group and draw
            var groups = _currentData
            .GroupBy(x => x.Category)
            .OrderBy(g => g.Key)
            .ToList();

            float yL = colLeft.Top + 10;
            float yR = colRight.Top + 10;
            var groupFont = new Font("Segoe UI", 12, FontStyle.Bold);
            var itemFont = new Font("Segoe UI", 10, FontStyle.Regular);
            var priceFont = new Font("Segoe UI", 10, FontStyle.Bold);
            var nameBrush = Brushes.Black;
            var priceBrush = Brushes.Black;
            int lineH = 22;

            // Local function (supports ref parameter) to draw a group
            void DrawGroup(string title, IEnumerable<MenuItemRow> rows, ref float yStart, Rectangle col)
            {
                e.Graphics.DrawString(title.ToUpperInvariant(), groupFont, nameBrush, col.Left + 10, yStart);
                yStart += lineH;
                foreach (var r in rows)
                {
                    if (yStart + lineH > col.Bottom)
                        return; // stop if overflow
                    // name centered
                    e.Graphics.DrawString(r.Name, itemFont, nameBrush, new RectangleF(col.Left, yStart, col.Width, lineH), new StringFormat { Alignment = StringAlignment.Center });
                    // right aligned price
                    string p = FormatPrice(r.Price);
                    e.Graphics.DrawString(p, priceFont, priceBrush, new RectangleF(col.Left, yStart, col.Width - 8, lineH), new StringFormat { Alignment = StringAlignment.Far });
                    yStart += lineH;
                    // unit centered italic
                    e.Graphics.DrawString(r.Unit, new Font("Segoe UI", 9, FontStyle.Italic), Brushes.DimGray,
                    new RectangleF(col.Left, yStart, col.Width, lineH - 2), new StringFormat { Alignment = StringAlignment.Center });
                    yStart += lineH - 2;
                }
                yStart += 10; // spacing after group
            }

            // fill left first then right (simple balance)
            foreach (var g in groups)
            {
                DrawGroup(g.Key, g, ref yL, colLeft);
            }
            foreach (var g in groups)
            {
                if (yL > colLeft.Bottom - 60)
                {
                    DrawGroup(g.Key, g, ref yR, colRight);
                }
            }

            // footer note
            var footerFont = new Font("Segoe UI", 11, FontStyle.Bold);
            e.Graphics.DrawString(_footerNote, footerFont, Brushes.Black, content.Left + content.Width / 2, content.Bottom - 40, sfCenter);

            e.HasMorePages = false;
        }

        private string FormatPrice(int price)
        {
            // Hiển thị dạng28K,45K ... nếu giá theo nghìn. Nếu không, hiển thị ###,### đ
            if (price % 1000 == 0)
            {
                return string.Format("{0}K", price / 1000);
            }
            return string.Format("{0:N0} đ", price);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Simple details — can be replaced with a dedicated form if desired
        private void ShowItemDetails(MenuItemRow it)
        {
            var msg = $"{it.Name}\n\nNhóm: {it.Category}\nĐơn vị: {it.Unit}\nGiá: {FormatPrice(it.Price)}";
            if (!string.IsNullOrEmpty(it.Notes))
                msg += $"\n\nGhi chú: {it.Notes}";
            MessageBox.Show(this, msg, "Chi tiết món", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private class MenuItemRow
        {
            public string Name
            {
                get; set;
            }
            public string Unit
            {
                get; set;
            }
            public string Category
            {
                get; set;
            }
            public int Price
            {
                get; set;
            }
            public string Notes
            {
                get; set;
            }
        }
    }


}
