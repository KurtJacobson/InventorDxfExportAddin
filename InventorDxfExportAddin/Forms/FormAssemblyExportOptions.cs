using InventorDxfExportAddin.DxfExport;
using System.Drawing;
using System.Windows.Forms;

namespace InventorDxfExportAddin.Forms
{
    /// <summary>
    /// Pre-flight dialog for a batch assembly export.
    /// Shows the number of sheet metal parts found and asks once how to handle
    /// any DXF files that already exist at the target location.
    /// </summary>
    internal class FormAssemblyExportOptions : Form
    {
        private RadioButton _rbOverwrite;
        private RadioButton _rbSkip;

        public OverwritePolicy SelectedPolicy =>
            _rbOverwrite.Checked ? OverwritePolicy.OverwriteAll : OverwritePolicy.SkipExisting;

        public FormAssemblyExportOptions(int partCount)
        {
            Text            = "Export Assembly DXFs";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition   = FormStartPosition.CenterParent;
            MaximizeBox     = false;
            MinimizeBox     = false;
            ClientSize      = new Size(380, 210);
            Font            = new Font("Segoe UI", 9f);

            // ── Part count summary ──────────────────────────────────────────
            var lblSummary = new Label
            {
                Text      = $"Found {partCount} sheet metal part{(partCount == 1 ? "" : "s")} in the assembly.",
                Location  = new Point(16, 18),
                Size      = new Size(348, 20),
                Font      = new Font("Segoe UI", 9f, FontStyle.Bold),
            };

            // ── Overwrite section ───────────────────────────────────────────
            var lblOverwrite = new Label
            {
                Text     = "If a DXF already exists at the target location:",
                Location = new Point(16, 52),
                Size     = new Size(348, 18),
            };

            _rbOverwrite = new RadioButton
            {
                Text     = "Overwrite existing files",
                Location = new Point(28, 76),
                Size     = new Size(320, 20),
                Checked  = true,
            };

            _rbSkip = new RadioButton
            {
                Text     = "Skip — export new parts only",
                Location = new Point(28, 100),
                Size     = new Size(320, 20),
            };

            // ── Buttons ─────────────────────────────────────────────────────
            var btnExport = new Button
            {
                Text         = "Export",
                DialogResult = DialogResult.OK,
                Location     = new Point(195, 165),
                Size         = new Size(80, 26),
            };

            var btnCancel = new Button
            {
                Text         = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location     = new Point(284, 165),
                Size         = new Size(80, 26),
            };

            AcceptButton = btnExport;
            CancelButton = btnCancel;

            Controls.AddRange(new Control[]
            {
                lblSummary, lblOverwrite, _rbOverwrite, _rbSkip,
                btnExport, btnCancel,
            });
        }
    }
}
