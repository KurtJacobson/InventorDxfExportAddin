using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventorDxfExportAddin.Custom_Controls
{

    public partial class ColorComboBox : ComboBox
    {

        private ColorDialog colorChooser = null;


        // Data for each color in the list.
        // AciIndex is set for standard DXF AutoCAD Color Index colors; null means custom.
        public class ColorInfo
        {
            public string Text { get; set; }
            public Color Color { get; set; }
            public byte? AciIndex { get; set; }

            public ColorInfo(string text, Color color, byte? aciIndex = null)
            {
                Text = text;
                Color = color;
                AciIndex = aciIndex;
            }
        }

        // Standard ACI colors with their exact AutoCAD RGB values
        public static readonly ColorInfo[] StandardColors =
        {
            new ColorInfo("1 - Red",        Color.FromArgb(255,   0,   0), 1),
            new ColorInfo("2 - Yellow",      Color.FromArgb(255, 255,   0), 2),
            new ColorInfo("3 - Green",       Color.FromArgb(  0, 255,   0), 3),
            new ColorInfo("4 - Cyan",        Color.FromArgb(  0, 255, 255), 4),
            new ColorInfo("5 - Blue",        Color.FromArgb(  0,   0, 255), 5),
            new ColorInfo("6 - Magenta",     Color.FromArgb(255,   0, 255), 6),
            new ColorInfo("7 - White/Black", Color.FromArgb(255, 255, 255), 7),
            new ColorInfo("8 - Dark Gray",   Color.FromArgb(128, 128, 128), 8),
            new ColorInfo("9 - Light Gray",  Color.FromArgb(192, 192, 192), 9),
        };

        // Returns the ACI index for a color if it matches a standard DXF color, otherwise null.
        public static byte? GetAciIndex(Color c)
        {
            int argb = c.ToArgb();
            foreach (var ci in StandardColors)
                if (ci.Color.ToArgb() == argb) return ci.AciIndex;
            return null;
        }

        public ColorComboBox()
        {
            InitializeComponent();

            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawFixed;
            DrawItem += OnDrawItem;
            SelectionChangeCommitted += OnSelectionChangeCommitted;

            PopulateColors();
        }

        public void PopulateColors()
        {
            Items.Clear();
            foreach (var ci in StandardColors)
                Items.Add(ci);
            Items.Add(new ColorInfo("Custom Color...", SystemColors.Window));
        }

        // Draw list item
        protected void OnDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                bool disabled = !Enabled;

                // Get this color
                ColorInfo color = (ColorInfo)Items[e.Index];

                // Fill background
                if (disabled)
                    e.Graphics.FillRectangle(SystemBrushes.Control, e.Bounds);
                else
                    e.DrawBackground();

                // Draw color box
                Rectangle rect = new Rectangle();
                rect.X = e.Bounds.X + 2;
                rect.Y = e.Bounds.Y + 2;
                rect.Width = 18;
                rect.Height = e.Bounds.Height - 5;

                if (disabled)
                {
                    e.Graphics.FillRectangle(SystemBrushes.Control, rect);
                    e.Graphics.DrawRectangle(SystemPens.GrayText, rect);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(color.Color), rect);
                    e.Graphics.DrawRectangle(SystemPens.WindowText, rect);
                }

                // Write color name
                Brush brush;
                if (disabled)
                    brush = SystemBrushes.GrayText;
                else if ((e.State & DrawItemState.Selected) != DrawItemState.None)
                    brush = SystemBrushes.HighlightText;
                else
                    brush = SystemBrushes.WindowText;

                e.Graphics.DrawString(color.Text, Font, brush,
                    e.Bounds.X + rect.X + rect.Width + 2,
                    e.Bounds.Y + ((e.Bounds.Height - Font.Height) / 2));

                // Draw the focus rectangle if appropriate
                if (!disabled && (e.State & DrawItemState.NoFocusRect) == DrawItemState.None)
                    e.DrawFocusRectangle();
            }
        }

        public new ColorInfo SelectedItem
        {
            get
            {
                return (ColorInfo)base.SelectedItem;
            }
            set
            {
                base.SelectedItem = value;
            }
        }

        public new string SelectedText
        {
            get
            {
                if (SelectedIndex >= 0)
                    return SelectedItem.Text;
                return String.Empty;
            }
            set
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (((ColorInfo)Items[i]).Text == value)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        public Color SelectedColor
        {
            get
            {
                if (SelectedIndex >= 0)
                    return SelectedItem.Color;
                return Color.White;
            }
            set
            {
                int argb = value.ToArgb();
                for (int i = 0; i < Items.Count; i++)
                {
                    if (((ColorInfo)Items[i]).Color.ToArgb() == argb)
                    {
                        SelectedIndex = i;
                        return;
                    }
                }

                // Not a standard color — insert as custom before the "Custom Color..." entry
                var colorName = $"Custom ({value.R}, {value.G}, {value.B})";
                Items.Insert(Items.Count - 1, new ColorInfo(colorName, value));
                SelectedIndex = Items.Count - 2;
            }
        }

        public new Color SelectedValue
        {
            get => SelectedColor;
            set => SelectedColor = value;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        private void OnSelectionChangeCommitted(object sender, EventArgs e)
        {

            if (SelectedItem.Text == "Custom Color...")
            {
                // Open ColorDialog to select custom color 
                this.colorChooser = new ColorDialog();
                if (this.colorChooser.ShowDialog() == DialogResult.OK)
                {
                    SelectedColor = this.colorChooser.Color;
                }
            }
        }

    }

}
