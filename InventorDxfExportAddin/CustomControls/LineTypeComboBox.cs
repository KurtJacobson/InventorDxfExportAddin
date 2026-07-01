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

            public float[] DashPattern { get; set; }

            public LineStyle(string typeName, LineTypeEnum lineType, float[] dashPattern)
            {
                TypeName = typeName;
                LineType = lineType;
                DashPattern = dashPattern;
            }
        }

        public static readonly List<LineStyle> Styles = new List<LineStyle>
        {
            new LineStyle("Continuous", LineTypeEnum.kContinuousLineType, new float[] { 100.0f, 0.01f }),
            new LineStyle("Dashed", LineTypeEnum.kDashedLineType, new float[] { 4.0f, 1.0f}),
            new LineStyle("Dotted", LineTypeEnum.kDottedLineType, new float[] { 1.0f, 1.0f}),
            new LineStyle("Hidden", LineTypeEnum.kDashedHiddenLineType, new float[] { 4.0f, 4.0f }),
            new LineStyle("Dash Dot", LineTypeEnum.kDashDottedLineType, new float[] { 4.0f, 1.0f, 1.0f, 1.0f }),
            new LineStyle("Dash Double Dot", LineTypeEnum.kDoubleDashedDottedLineType, new float[] { 4.0f, 1.0f, 1.0f, 1.0f , 1.0f, 1.0f }),
            new LineStyle("Dash Triple Dot", LineTypeEnum.kDashedTripleDottedLineType, new float[] { 4.0f, 1.0f, 1.0f, 1.0f , 1.0f, 1.0f, 1.0f, 1.0f }),
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
                // Get this line style
                LineStyle lineStyle = (LineStyle)Items[e.Index];

                // Fill background
                e.DrawBackground();

                // Draw line style
                Point p1 = new Point(e.Bounds.Left + 5, e.Bounds.Y + e.Bounds.Height / 2);
                Point p2 = new Point(e.Bounds.Left + 70, e.Bounds.Y + e.Bounds.Height / 2);

                using (Pen linePen = new Pen(e.ForeColor, 1))
                {
                    linePen.DashPattern = lineStyle.DashPattern;
                    e.Graphics.DrawLine(linePen, p1, p2);
                }

                // Write line style name
                Brush brush;
                if ((e.State & DrawItemState.Selected) != DrawItemState.None)
                    brush = SystemBrushes.HighlightText;
                else
                    brush = SystemBrushes.WindowText;

                e.Graphics.DrawString(lineStyle.TypeName, Font, brush,
                    e.Bounds.X + p2.X + 2,
                    e.Bounds.Y + ((e.Bounds.Height - Font.Height) / 2));

                // Draw the focus rectangle if appropriate
                if ((e.State & DrawItemState.NoFocusRect) == DrawItemState.None)
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
