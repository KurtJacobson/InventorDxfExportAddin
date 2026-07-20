using Newtonsoft.Json;

namespace InventorDxfExportAddin
{
    /// <summary>
    /// Flat POCO for one layer of the three-tier settings hierarchy.
    /// Null means "not set at this layer" — merge falls through to the next.
    /// Stored as JSONC on disk; Newtonsoft handles // and /* */ comments transparently.
    /// </summary>
    internal class OrgSettings
    {
        // Outer Profile
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string OuterProfileLayer { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string OuterProfileLayerColor { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string OuterProfileLineType { get; set; }

        // Inner Profiles
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string InteriorProfilesLayer { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string InteriorProfilesLayerColor { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string InteriorProfilesLineType { get; set; }

        // Bend Lines (Up)
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BendUpLayer { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BendUpLayerColor { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BendUpLineType { get; set; }

        // Bend Lines (Down)
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? BendDownEnabled { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BendDownLayer { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BendDownLayerColor { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BendDownLineType { get; set; }

        // Export options
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ExportMode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ExportDirectory { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? PromptBeforeOverwrite { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TemplateBaseDirectory { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SubfolderTemplate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FilenameTemplate { get; set; }
    }
}
