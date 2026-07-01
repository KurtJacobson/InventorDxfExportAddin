using Inventor;
using InventorDxfExportAddin.DxfExport;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace InventorDxfExportAddin.Custom_Controls
{

    public partial class LineTypeComboBox : ComboBox
    {
        public class LineStyle
        {
            public string TypeName { get; set; }
            public LineTypeEnum LineType { get; set; } = LineTypeEnum.kContinuousLineType;
            public string DxfName { get; set; }
            public float[] DashPattern { get; set; }
            public double[] DxfElements { get; set; }  // positive=dash, negative=gap, 0=dot

            public LineStyle(string typeName, LineTypeEnum lineType, string dxfName, float[] dashPattern, double[] dxfElements)
            {
                TypeName = typeName;
                LineType = lineType;
                DxfName = dxfName;
                DashPattern = dashPattern;
                DxfElements = dxfElements;
            }
        }

        public static readonly List<LineStyle> Styles = new List<LineStyle>
        {
            new LineStyle("Continuous",      LineTypeEnum.kContinuousLineType,         "CONTINUOUS", new float[] { 100.0f, 0.01f },                                    new double[] { }),
            new LineStyle("Dashed",          LineTypeEnum.kDashedLineType,             "DASHED",     new float[] { 4.0f, 1.0f },                                        new double[] { 0.5, -0.25 }),
            new LineStyle("Dotted",          LineTypeEnum.kDottedLineType,             "DOTTED",     new float[] { 1.0f, 1.0f },                                        new double[] { 0.0, -0.25 }),
            new LineStyle("Hidden",          LineTypeEnum.kDashedHiddenLineType,       "HIDDEN",     new float[] { 4.0f, 4.0f },                                        new double[] { 0.25, -0.125 }),
            new LineStyle("Dash Dot",        LineTypeEnum.kDashDottedLineType,         "DASHDOT",    new float[] { 4.0f, 1.0f, 1.0f, 1.0f },                           new double[] { 0.5, -0.25, 0.0, -0.25 }),
            new LineStyle("Dash Double Dot", LineTypeEnum.kDoubleDashedDottedLineType, "DASHDOT2",   new float[] { 4.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },              new double[] { 0.5, -0.25, 0.0, -0.25, 0.0, -0.25 }),
            new LineStyle("Dash Triple Dot", LineTypeEnum.kDashedTripleDottedLineType, "DIVIDE",     new float[] { 4.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f }, new double[] { 0.5, -0.25, 0.0, -0.25, 0.0, -0.25, 0.0, -0.25 }),
        };


        public LineTypeComboBox()
        {
            InitializeComponent();

            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawFixed;
            DrawItem += OnDrawItem;
            SelectionChangeCommitted += OnSelectionChangeCommitted;

            // Initialize options
            this.PopulateLineStyles();
        }

        // Populate control with line style options
        public void PopulateLineStyles()
        {
            Items.Clear();
            foreach (var style in Styles)
            {
                Items.Add(style);
            }
        }

        // Draw list item
        protected void OnDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                bool disabled = !Enabled;

                // Get this line style
                LineStyle lineStyle = (LineStyle)Items[e.Index];

                // Fill background
                if (disabled)
                    e.Graphics.FillRectangle(SystemBrushes.Control, e.Bounds);
                else
                    e.DrawBackground();

                // Draw line style
                Point p1 = new Point(e.Bounds.Left + 5, e.Bounds.Y + e.Bounds.Height / 2);
                Point p2 = new Point(e.Bounds.Left + 70, e.Bounds.Y + e.Bounds.Height / 2);

                System.Drawing.Color lineColor = disabled ? SystemColors.GrayText : e.ForeColor;
                using (Pen linePen = new Pen(lineColor, 1))
                {
                    linePen.DashPattern = lineStyle.DashPattern;
                    e.Graphics.DrawLine(linePen, p1, p2);
                }

                // Write line style name
                Brush brush;
                if (disabled)
                    brush = SystemBrushes.GrayText;
                else if ((e.State & DrawItemState.Selected) != DrawItemState.None)
                    brush = SystemBrushes.HighlightText;
                else
                    brush = SystemBrushes.WindowText;

                e.Graphics.DrawString(lineStyle.TypeName, Font, brush,
                    e.Bounds.X + p2.X + 2,
                    e.Bounds.Y + ((e.Bounds.Height - Font.Height) / 2));

                // Draw the focus rectangle if appropriate
                if (!disabled && (e.State & DrawItemState.NoFocusRect) == DrawItemState.None)
                    e.DrawFocusRectangle();
            }
        }

        public new LineStyle SelectedItem
        {
            get
            {
                return (LineStyle)base.SelectedItem;
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
                    return SelectedItem.TypeName;
                return "Continuous";
            }
            set
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (((LineStyle)Items[i]).TypeName == value)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        public new LineTypeEnum SelectedLineType
        {
            get
            {
                if (SelectedIndex >= 0)
                    return SelectedItem.LineType;
                return LineTypeEnum.kContinuousLineType;
            }
            set
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (((LineStyle)Items[i]).LineType == value)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        private void OnSelectionChangeCommitted(object sender, EventArgs e)
        {

            if (SelectedItem.TypeName == "Custom Style...")
            {
                // Custom line style not implemented
                return;
            }
        }
    }
}
