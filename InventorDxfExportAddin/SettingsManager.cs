using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Reflection;
using Color     = System.Drawing.Color;
using Directory = System.IO.Directory;
using File      = System.IO.File;
using Path      = System.IO.Path;
using Environment = System.Environment;

namespace InventorDxfExportAddin
{
    /// <summary>
    /// Three-tier settings: global (network JSONC) → local user overrides (JSON) → built-in defaults.
    /// Registry key HKLM\SOFTWARE\InventorDxfExport\GlobalConfigPath points to the global file.
    /// Local overrides are stored in %APPDATA%\InventorDxfExport\user_settings.json.
    /// </summary>
    internal static class SettingsManager
    {
        private const string RegKey   = @"SOFTWARE\InventorDxfExport";
        private const string RegValue = "GlobalConfigPath";
        private const string AppFolder = "InventorDxfExport";
        private const string LocalFile  = "user_settings.json";

        private static readonly OrgSettings BuiltIn = new OrgSettings
        {
            OuterProfileLayer          = "Outline",
            OuterProfileLayerColor     = DxfColors.ToRgbString(DxfColors.Red),
            OuterProfileLineType       = "kDefaultLineType",
            InteriorProfilesLayer      = "InnerOutlines",
            InteriorProfilesLayerColor = DxfColors.ToRgbString(DxfColors.Green),
            InteriorProfilesLineType   = "kDefaultLineType",
            BendUpLayer                = "BendingLines",
            BendUpLayerColor           = DxfColors.ToRgbString(DxfColors.Blue),
            BendUpLineType             = "kDashedLineType",
            BendDownEnabled            = true,
            BendDownLayer              = "BendingLines",
            BendDownLayerColor         = DxfColors.ToRgbString(DxfColors.Magenta),
            BendDownLineType           = "kDashedLineType",
            ExportMode               = "NextToSourceFile",
            ExportDirectory          = "",
            PromptBeforeOverwrite    = true,
            TemplateBaseDirectory    = "",
            SubfolderTemplate        = @"{Material}\{Thickness}",
            FilenameTemplate         = "",
        };

        // Loaded layers — null Global means no org config is configured.
        public static OrgSettings Global    { get; private set; }
        public static OrgSettings Local     { get; private set; }
        public static OrgSettings Effective { get; private set; }

        public static bool HasGlobalConfig => Global != null;

        static SettingsManager() => Load();

        public static void Load()
        {
            Global    = LoadGlobal();
            Local     = LoadLocal();
            Effective = Merge(Local, Global, BuiltIn);
        }

        // ── Typed effective accessors ──────────────────────────────────────────

        public static string OuterProfileLayer        => Effective.OuterProfileLayer;
        public static Color  OuterProfileLayerColor   => ParseColor(Effective.OuterProfileLayerColor);
        public static Inventor.LineTypeEnum OuterProfileLineType => ParseLineType(Effective.OuterProfileLineType);

        public static string InteriorProfilesLayer      => Effective.InteriorProfilesLayer;
        public static Color  InteriorProfilesLayerColor => ParseColor(Effective.InteriorProfilesLayerColor);
        public static Inventor.LineTypeEnum InteriorProfilesLineType => ParseLineType(Effective.InteriorProfilesLineType);

        public static string BendUpLayer      => Effective.BendUpLayer;
        public static Color  BendUpLayerColor => ParseColor(Effective.BendUpLayerColor);
        public static Inventor.LineTypeEnum BendUpLineType => ParseLineType(Effective.BendUpLineType);

        public static bool   BendDownEnabled      => Effective.BendDownEnabled ?? true;
        public static string BendDownLayer        => Effective.BendDownLayer;
        public static Color  BendDownLayerColor   => ParseColor(Effective.BendDownLayerColor);
        public static Inventor.LineTypeEnum BendDownLineType => ParseLineType(Effective.BendDownLineType);

        public static string ExportMode            => Effective.ExportMode;
        public static string ExportDirectory       => Effective.ExportDirectory;
        public static bool   PromptBeforeOverwrite => Effective.PromptBeforeOverwrite ?? true;
        public static string TemplateBaseDirectory => Effective.TemplateBaseDirectory;
        public static string SubfolderTemplate     => Effective.SubfolderTemplate;
        public static string FilenameTemplate      => Effective.FilenameTemplate;

        // ── Override detection ─────────────────────────────────────────────────

        /// <summary>True when the local user file has a value for this property that differs from global.</summary>
        private static readonly System.Collections.Generic.HashSet<string> ColorPropNames
            = new System.Collections.Generic.HashSet<string>(StringComparer.Ordinal)
            {
                "OuterProfileLayerColor", "InteriorProfilesLayerColor",
                "BendUpLayerColor", "BendDownLayerColor"
            };

        public static bool IsOverridden(string propName)
        {
            if (!HasGlobalConfig) return false;
            PropertyInfo p = typeof(OrgSettings).GetProperty(propName);
            if (p == null) return false;
            string localStr  = p.GetValue(Local)?.ToString();
            string globalStr = p.GetValue(Global)?.ToString();
            if (localStr == null) return false;

            if (ColorPropNames.Contains(propName))
                return !ColorsEqual(localStr, globalStr);

            return !string.Equals(localStr, globalStr, StringComparison.OrdinalIgnoreCase);
        }

        private static bool ColorsEqual(string a, string b)
        {
            Color ca = ParseColor(a);
            Color cb = ParseColor(b ?? "");
            return ca.R == cb.R && ca.G == cb.G && ca.B == cb.B;
        }

        /// <summary>Returns the global value for a property as a string, or null if no global config.</summary>
        public static string GlobalStringValue(string propName)
        {
            if (Global == null) return null;
            return typeof(OrgSettings).GetProperty(propName)?.GetValue(Global)?.ToString();
        }

        // ── Mutations ──────────────────────────────────────────────────────────

        /// <summary>Remove a property from local overrides and recompute Effective.</summary>
        public static void ResetToGlobal(string propName)
        {
            PropertyInfo p = typeof(OrgSettings).GetProperty(propName);
            if (p == null) return;
            p.SetValue(Local, null);
            Effective = Merge(Local, Global, BuiltIn);
            SaveLocalFile();
        }

        /// <summary>
        /// Persist layer settings from the DXF Settings form.
        /// When a global config exists, only values that differ from global are kept in local.
        /// </summary>
        public static void SaveLayerSettings(OrgSettings form)
        {
            Local.OuterProfileLayer        = DiffStr(form.OuterProfileLayer,        Global?.OuterProfileLayer);
            Local.OuterProfileLayerColor   = DiffColor(form.OuterProfileLayerColor, Global?.OuterProfileLayerColor);
            Local.OuterProfileLineType     = DiffStr(form.OuterProfileLineType,     Global?.OuterProfileLineType);

            Local.InteriorProfilesLayer      = DiffStr(form.InteriorProfilesLayer,      Global?.InteriorProfilesLayer);
            Local.InteriorProfilesLayerColor = DiffColor(form.InteriorProfilesLayerColor, Global?.InteriorProfilesLayerColor);
            Local.InteriorProfilesLineType   = DiffStr(form.InteriorProfilesLineType,   Global?.InteriorProfilesLineType);

            Local.BendUpLayer      = DiffStr(form.BendUpLayer,      Global?.BendUpLayer);
            Local.BendUpLayerColor = DiffColor(form.BendUpLayerColor, Global?.BendUpLayerColor);
            Local.BendUpLineType   = DiffStr(form.BendUpLineType,   Global?.BendUpLineType);

            Local.BendDownEnabled    = DiffBool(form.BendDownEnabled,    Global?.BendDownEnabled);
            Local.BendDownLayer      = DiffStr(form.BendDownLayer,       Global?.BendDownLayer);
            Local.BendDownLayerColor = DiffColor(form.BendDownLayerColor, Global?.BendDownLayerColor);
            Local.BendDownLineType   = DiffStr(form.BendDownLineType,    Global?.BendDownLineType);

            Effective = Merge(Local, Global, BuiltIn);
            SaveLocalFile();
        }

        /// <summary>Persist export options from the Export Options form.</summary>
        public static void SaveExportOptions(OrgSettings form)
        {
            Local.ExportMode            = DiffStr(form.ExportMode,            Global?.ExportMode);
            Local.ExportDirectory       = DiffStr(form.ExportDirectory,       Global?.ExportDirectory);
            Local.PromptBeforeOverwrite = DiffBool(form.PromptBeforeOverwrite, Global?.PromptBeforeOverwrite);
            Local.TemplateBaseDirectory = DiffStr(form.TemplateBaseDirectory, Global?.TemplateBaseDirectory);
            Local.SubfolderTemplate     = DiffStr(form.SubfolderTemplate,     Global?.SubfolderTemplate);
            Local.FilenameTemplate      = DiffStr(form.FilenameTemplate,      Global?.FilenameTemplate);

            Effective = Merge(Local, Global, BuiltIn);
            SaveLocalFile();
        }

        // ── Color / LineType helpers (public for form use) ─────────────────────

        public static Color ParseColor(string value)
        {
            if (string.IsNullOrEmpty(value)) return DxfColors.White;
            // ACI names first — "Green" means ACI green (0,255,0), not .NET Color.Green (0,128,0)
            if (DxfColors.TryParse(value, out Color aciColor)) return aciColor;
            // RGB triple "R, G, B"
            var parts = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 3
                && int.TryParse(parts[0].Trim(), out int r)
                && int.TryParse(parts[1].Trim(), out int g)
                && int.TryParse(parts[2].Trim(), out int b))
                return Color.FromArgb(r, g, b);
            return DxfColors.White;
        }

        public static string ColorToString(Color c)
            => $"{c.R}, {c.G}, {c.B}";

        public static Inventor.LineTypeEnum ParseLineType(string value)
        {
            if (string.IsNullOrEmpty(value)) return Inventor.LineTypeEnum.kDefaultLineType;
            // Inventor enum names match exactly (e.g. "kDashedLineType")
            foreach (Inventor.LineTypeEnum lt in Enum.GetValues(typeof(Inventor.LineTypeEnum)))
                if (string.Equals(lt.ToString(), value, StringComparison.OrdinalIgnoreCase))
                    return lt;
            return Inventor.LineTypeEnum.kDefaultLineType;
        }

        public static string LineTypeToString(Inventor.LineTypeEnum lt) => lt.ToString();

        // ── Private helpers ────────────────────────────────────────────────────

        private static OrgSettings LoadGlobal()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(RegKey))
                {
                    if (key == null) return null;
                    string path = key.GetValue(RegValue) as string;
                    if (string.IsNullOrEmpty(path) || !File.Exists(path)) return null;
                    string json = File.ReadAllText(path);
                    // Newtonsoft ignores // and /* */ comments natively
                    return JsonConvert.DeserializeObject<OrgSettings>(json);
                }
            }
            catch { return null; }
        }

        private static OrgSettings LoadLocal()
        {
            string path = LocalConfigPath;
            if (!File.Exists(path))
                return MigrateFromDotNetSettings();

            try
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<OrgSettings>(json) ?? new OrgSettings();
            }
            catch { return new OrgSettings(); }
        }

        private static OrgSettings MigrateFromDotNetSettings()
        {
            // Carry existing user preferences forward on first run after upgrade.
            var s = Properties.DxfSettings.Default;
            var migrated = new OrgSettings
            {
                OuterProfileLayer        = s.OuterProfileLayer,
                OuterProfileLayerColor   = ColorToString(s.OuterProfileLayerColor),
                OuterProfileLineType     = LineTypeToString(s.OuterProfileLineType),
                InteriorProfilesLayer    = s.InteriorProfilesLayer,
                InteriorProfilesLayerColor = ColorToString(s.InteriorProfilesLayerColor),
                InteriorProfilesLineType = LineTypeToString(s.InteriorProfilesLineType),
                BendUpLayer              = s.BendUpLayer,
                BendUpLayerColor         = ColorToString(s.BendUpLayerColor),
                BendUpLineType           = LineTypeToString(s.BendUpLineType),
                BendDownEnabled          = s.BendDownEnabled,
                BendDownLayer            = s.BendDownLayer,
                BendDownLayerColor       = ColorToString(s.BendDownLayerColor),
                BendDownLineType         = LineTypeToString(s.BendDownLineType),
                ExportMode               = s.ExportMode,
                ExportDirectory          = s.ExportDirectory,
                PromptBeforeOverwrite    = s.PromptBeforeOverwrite,
                TemplateBaseDirectory    = s.TemplateBaseDirectory,
                SubfolderTemplate        = s.SubfolderTemplate,
                FilenameTemplate         = s.FilenameTemplate,
            };
            // Don't save yet — SaveLocalFile() is called when the user first opens a settings form.
            // This avoids creating the file on every addin load before the user has configured anything.
            return migrated;
        }

        private static OrgSettings Merge(OrgSettings local, OrgSettings global, OrgSettings builtIn)
        {
            var result = new OrgSettings();
            foreach (PropertyInfo p in typeof(OrgSettings).GetProperties())
            {
                object l = local  != null ? p.GetValue(local)  : null;
                object g = global != null ? p.GetValue(global) : null;
                object b = builtIn != null ? p.GetValue(builtIn) : null;
                p.SetValue(result, l ?? g ?? b);
            }
            return result;
        }

        private static void SaveLocalFile()
        {
            try
            {
                string dir  = Path.GetDirectoryName(LocalConfigPath);
                Directory.CreateDirectory(dir);
                string json = JsonConvert.SerializeObject(Local, Formatting.Indented);
                File.WriteAllText(LocalConfigPath, json);
            }
            catch { }
        }

        private static string LocalConfigPath
            => Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                AppFolder, LocalFile);

        private static string DiffStr(string formVal, string globalVal)
            => string.Equals(formVal, globalVal, StringComparison.OrdinalIgnoreCase) ? null : formVal;

        private static string DiffColor(string formVal, string globalVal)
            => ColorsEqual(formVal, globalVal) ? null : formVal;

        private static bool? DiffBool(bool? formVal, bool? globalVal)
            => formVal == globalVal ? (bool?)null : formVal;
    }
}
