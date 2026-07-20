using System.Drawing;
using System.Windows.Forms;

namespace InventorDxfExportAddin.Forms
{
    /// <summary>
    /// Modeless progress window shown during a batch assembly export.
    /// The export loop calls <see cref="UpdateProgress"/> between parts and pumps
    /// the message queue with <see cref="Application.DoEvents"/> so this form stays live.
    /// </summary>
    internal class FormBatchProgress : Form
    {
        private readonly Label       _lblStatus;
        private readonly Label       _lblCount;
        private readonly ProgressBar _bar;
        private readonly int         _total;

        public bool Cancelled { get; private set; }

        public FormBatchProgress(int total)
        {
            _total = total;

            Text            = "Exporting Assembly DXFs…";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition   = FormStartPosition.CenterParent;
            MaximizeBox     = false;
            MinimizeBox     = false;
            ControlBox      = false;   // no X — Cancel button is the only exit
            ClientSize      = new Size(420, 130);
            Font            = new Font("Segoe UI", 9f);

            _lblStatus = new Label
            {
                Text     = "Starting…",
                Location = new Point(16, 16),
                Size     = new Size(388, 18),
                AutoEllipsis = true,
            };

            _bar = new ProgressBar
            {
                Location = new Point(16, 44),
                Size     = new Size(388, 20),
                Minimum  = 0,
                Maximum  = total,
                Value    = 0,
                Style    = ProgressBarStyle.Continuous,
            };

            _lblCount = new Label
            {
                Text      = $"0 of {total}",
                Location  = new Point(16, 72),
                Size      = new Size(388, 18),
                ForeColor = SystemColors.GrayText,
            };

            var btnCancel = new Button
            {
                Text     = "Cancel",
                Location = new Point(326, 96),
                Size     = new Size(78, 26),
            };
            btnCancel.Click += (s, e) =>
            {
                Cancelled        = true;
                btnCancel.Text   = "Cancelling…";
                btnCancel.Enabled = false;
            };

            Controls.AddRange(new Control[] { _lblStatus, _bar, _lblCount, btnCancel });
        }

        public void UpdateProgress(int current, string partLabel)
        {
            _lblStatus.Text = $"Exporting: {partLabel}";
            _lblCount.Text  = $"{current} of {_total}";
            _bar.Value      = current;
            Refresh();
        }
    }
}
