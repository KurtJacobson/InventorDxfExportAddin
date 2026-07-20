using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InventorDxfExportAddin
{
    public partial class FormDxfSettings : Form
    {
        private readonly Dictionary<string, Button> _resetBtns = new Dictionary<string, Button>();

        public FormDxfSettings()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            LoadFromSettings();

            this.cbEnableBendLinesDown.CheckedChanged += (s, e) => ManageCheckGroupBox(cbEnableBendLinesDown, groupBox4);
            ManageCheckGroupBox(cbEnableBendLinesDown, groupBox4);

            if (SettingsManager.HasGlobalConfig)
                SetupOverrideUI();
        }

        private void LoadFromSettings()
        {
            var s = SettingsManager.Effective;

            tbOuterProfileLayer.Text                = s.OuterProfileLayer;
            cbOuterProfileLayerColor.SelectedColor  = SettingsManager.ParseColor(s.OuterProfileLayerColor);
            cbOuterProfileLineType.SelectedLineType = SettingsManager.ParseLineType(s.OuterProfileLineType);

            tbInnerProfilesLayer.Text                = s.InteriorProfilesLayer;
            cbInnerProfilesLayerColor.SelectedColor  = SettingsManager.ParseColor(s.InteriorProfilesLayerColor);
            cbInnerProfilesLineType.SelectedLineType = SettingsManager.ParseLineType(s.InteriorProfilesLineType);

            tbBendLineLayer.Text                = s.BendUpLayer;
            cbBendLinesLayerColor.SelectedColor = SettingsManager.ParseColor(s.BendUpLayerColor);
            cbBendLineType.SelectedLineType     = SettingsManager.ParseLineType(s.BendUpLineType);

            cbEnableBendLinesDown.Checked              = s.BendDownEnabled ?? true;
            tbBendLinesDownLayer.Text                  = s.BendDownLayer;
            cbBendLinesDownLayerColor.SelectedColor    = SettingsManager.ParseColor(s.BendDownLayerColor);
            cbBendDownLineType.SelectedLineType        = SettingsManager.ParseLineType(s.BendDownLineType);
        }

        // ── Override indicators ────────────────────────────────────────────────

        private void SetupOverrideUI()
        {
            AddOverrideColumn(tableLayoutPanel1, new[]
            {
                ("OuterProfileLayer",     (Control)tbOuterProfileLayer,      0),
                ("OuterProfileLayerColor",  cbOuterProfileLayerColor,        1),
                ("OuterProfileLineType",    cbOuterProfileLineType,           2),
            });

            AddOverrideColumn(tableLayoutPanel2, new[]
            {
                ("InteriorProfilesLayer",     (Control)tbInnerProfilesLayer,    0),
                ("InteriorProfilesLayerColor",  cbInnerProfilesLayerColor,      1),
                ("InteriorProfilesLineType",    cbInnerProfilesLineType,         2),
            });

            AddOverrideColumn(tableLayoutPanel3, new[]
            {
                ("BendUpLayer",     (Control)tbBendLineLayer,     0),
                ("BendUpLayerColor",  cbBendLinesLayerColor,      1),
                ("BendUpLineType",    cbBendLineType,              2),
            });

            AddOverrideColumn(tableLayoutPanel4, new[]
            {
                ("BendDownLayer",     (Control)tbBendLinesDownLayer,    0),
                ("BendDownLayerColor",  cbBendLinesDownLayerColor,      1),
                ("BendDownLineType",    cbBendDownLineType,               2),
            });
        }

        private void AddOverrideColumn(
            TableLayoutPanel tlp,
            (string key, Control control, int row)[] items)
        {
            tlp.ColumnCount = 3;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 26));

            foreach (var (key, control, row) in items)
            {
                bool overridden = SettingsManager.IsOverridden(key);
                if (overridden)
                    HighlightControl(control, true);

                var btn = new Button
                {
                    Text    = "↺",
                    Dock    = DockStyle.Fill,
                    Visible = overridden,
                    Margin  = new Padding(1),
                    Font    = new Font(Font.FontFamily, 8f),
                };

                string capturedKey = key;
                Control capturedControl = control;
                btn.Click += (s, e) =>
                {
                    SettingsManager.ResetToGlobal(capturedKey);
                    ReloadControl(capturedKey, capturedControl);
                    HighlightControl(capturedControl, false);
                    btn.Visible = false;
                };

                _resetBtns[key] = btn;
                tlp.Controls.Add(btn, 2, row);
            }
        }

        private static void HighlightControl(Control c, bool on)
            => c.BackColor = on ? Color.LightYellow : SystemColors.Window;

        private void ReloadControl(string key, Control control)
        {
            string strVal = SettingsManager.Effective.GetType()
                .GetProperty(key)?.GetValue(SettingsManager.Effective)?.ToString() ?? "";

            if (control is TextBox tb)
            {
                tb.Text = strVal;
            }
            else if (control is Custom_Controls.ColorComboBox ccb)
            {
                ccb.SelectedColor = SettingsManager.ParseColor(strVal);
            }
            else if (control is Custom_Controls.LineTypeComboBox ltb)
            {
                ltb.SelectedLineType = SettingsManager.ParseLineType(strVal);
            }
        }

        // ── Save ───────────────────────────────────────────────────────────────

        private void btnSave_Click(object sender, EventArgs e)
        {
            var form = new OrgSettings
            {
                OuterProfileLayer        = tbOuterProfileLayer.Text,
                OuterProfileLayerColor   = SettingsManager.ColorToString(cbOuterProfileLayerColor.SelectedColor),
                OuterProfileLineType     = SettingsManager.LineTypeToString(cbOuterProfileLineType.SelectedLineType),

                InteriorProfilesLayer      = tbInnerProfilesLayer.Text,
                InteriorProfilesLayerColor = SettingsManager.ColorToString(cbInnerProfilesLayerColor.SelectedColor),
                InteriorProfilesLineType   = SettingsManager.LineTypeToString(cbInnerProfilesLineType.SelectedLineType),

                BendUpLayer      = tbBendLineLayer.Text,
                BendUpLayerColor = SettingsManager.ColorToString(cbBendLinesLayerColor.SelectedColor),
                BendUpLineType   = SettingsManager.LineTypeToString(cbBendLineType.SelectedLineType),

                BendDownEnabled   = cbEnableBendLinesDown.Checked,
                BendDownLayer     = tbBendLinesDownLayer.Text,
                BendDownLayerColor = SettingsManager.ColorToString(cbBendLinesDownLayerColor.SelectedColor),
                BendDownLineType  = SettingsManager.LineTypeToString(cbBendDownLineType.SelectedLineType),
            };

            SettingsManager.SaveLayerSettings(form);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        // ── Bend-Down checkbox floating trick ──────────────────────────────────

        private void ManageCheckGroupBox(CheckBox chk, GroupBox grp)
        {
            if (chk.Parent == grp)
            {
                grp.Parent.Controls.Add(chk);
                chk.Location = new Point(chk.Left + grp.Left, chk.Top + grp.Top);
                chk.BringToFront();
            }
            grp.Enabled = chk.Checked;
        }
    }
}
