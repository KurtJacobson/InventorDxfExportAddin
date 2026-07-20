using Inventor;
using System.Windows.Forms;

namespace InventorDxfExportAddin.Forms
{
    public partial class FormTokenPicker : Form
    {
        public string SelectedToken { get; private set; } = "";

        private static readonly (string token, string description)[] BuiltinTokens =
        {
            ("{PartNumber}",       "Part number iProperty"),
            ("{Description}",      "Description iProperty"),
            ("{Material}",         "Material iProperty"),
            ("{RevisionNumber}",   "Revision number iProperty"),
            ("{FileName}",         "Source file name (no extension)"),
            ("{Thickness}",        "Sheet thickness in document units"),
            ("{Thickness:mm:2}",   "Sheet thickness in mm, 2 decimal places"),
            ("{Thickness:in:3}",   "Sheet thickness in inches, 3 decimal places"),
            ("{Thickness:in:4}",   "Sheet thickness in inches, 4 decimal places"),
        };

        public FormTokenPicker(Document doc)
        {
            InitializeComponent();
            PopulateList(doc);
        }

        private void PopulateList(Document doc)
        {
            lvTokens.BeginUpdate();
            lvTokens.Groups.Clear();
            lvTokens.Items.Clear();

            var grpBuiltin = new ListViewGroup("Built-in Tokens", HorizontalAlignment.Left);
            lvTokens.Groups.Add(grpBuiltin);

            var liveValues = doc != null ? ResolveBuiltinValues(doc) : null;

            foreach (var (token, desc) in BuiltinTokens)
            {
                var item = new ListViewItem(token, grpBuiltin);
                item.SubItems.Add("Built-in");
                string liveVal = liveValues != null && liveValues.TryGetValue(token, out string v) ? v : "";
                item.SubItems.Add(liveVal.Length > 80 ? liveVal.Substring(0, 80) + "…" : liveVal);
                item.Tag = token;
                lvTokens.Items.Add(item);
            }

            if (doc != null)
                AddIProperties(doc);

            lvTokens.EndUpdate();
        }

        private static Dictionary<string, string> ResolveBuiltinValues(Document doc)
        {
            var raw = TemplateHelper.ResolveTokens(doc);

            // Map to the {Token} key format expected by the caller
            var vals = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var kv in raw)
            {
                // Thickness variants are already stored with brace keys; simple tokens are bare names
                string key = kv.Key.StartsWith("{") ? kv.Key : "{" + kv.Key + "}";
                vals[key] = kv.Value;
            }
            return vals;
        }

        private void AddIProperties(Document doc)
        {
            var sets = new[] { "Design Tracking Properties", "Inventor Summary Information" };

            // tokens already covered by built-ins — skip to avoid duplication
            var skip = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Part Number", "Description", "Material", "Revision Number"
            };

            foreach (string setName in sets)
            {
                PropertySet ps;
                try { ps = doc.PropertySets[setName]; }
                catch { continue; }

                var grp = new ListViewGroup(setName, HorizontalAlignment.Left);
                bool anyAdded = false;

                foreach (Property p in ps)
                {
                    if (skip.Contains(p.Name)) continue;

                    string valueStr;
                    try { valueStr = p.Value?.ToString() ?? ""; }
                    catch { valueStr = ""; }

                    if (string.IsNullOrWhiteSpace(valueStr)) continue;

                    // build a camelCase token name from the property name
                    string token = "{" + ToCamelToken(p.Name) + "}";

                    if (!anyAdded)
                    {
                        lvTokens.Groups.Add(grp);
                        anyAdded = true;
                    }

                    var item = new ListViewItem(token, grp);
                    item.SubItems.Add(p.Name);
                    item.SubItems.Add(valueStr.Length > 80 ? valueStr.Substring(0, 80) + "…" : valueStr);
                    item.Tag = token;
                    lvTokens.Items.Add(item);
                }
            }
        }

        private static string ToCamelToken(string name)
        {
            // "Sheet Metal Rule" → "SheetMetalRule"
            var parts = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Concat(parts.Select(p =>
                p.Length > 0 ? char.ToUpper(p[0]) + p.Substring(1) : p));
        }

        private void Commit()
        {
            if (lvTokens.SelectedItems.Count == 0) return;
            SelectedToken = lvTokens.SelectedItems[0].Tag as string ?? "";
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lvTokens_DoubleClick(object sender, EventArgs e) => Commit();

        private void lvTokens_SelectedIndexChanged(object sender, EventArgs e)
            => btnInsert.Enabled = lvTokens.SelectedItems.Count > 0;

        private void btnInsert_Click(object sender, EventArgs e) => Commit();
    }
}
