using Inventor;
using InventorDxfExportAddin;
using Color = System.Drawing.Color;
using IxMilia;
using IxMilia.Dxf;
using IxMilia.Dxf.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorDxfExportAddin.DxfExport
{
    public class DxfExportEngine
    {
        private PartDocument partDoc = null;
        private string exportFileName = null;
        private string exportDirectory = null;

        public DxfExportEngine(PartDocument doc) 
        {
            this.partDoc = doc;
        }

        public string ExportFilename
        {
            get 
            {
                if (this.exportFileName == null)
                {
                   return System.IO.Path.GetFileNameWithoutExtension(partDoc.DisplayName) + ".dxf";
                }                
                return exportFileName; 
            }
            set { exportFileName = value; }
        }

        public string ExportDirectory
        {
            get
            {
                if (this.exportDirectory == null)
                {
                    var fullFileName = partDoc.FullFileName;
                    if (!string.IsNullOrEmpty(fullFileName))
                    {
                        var dir = System.IO.Path.GetDirectoryName(fullFileName);
                        System.IO.Directory.CreateDirectory(dir);
                        return dir;
                    }

                    // Document hasn't been saved as an IPT — prompt user for output location,
                    // defaulting to the source STP file's directory if we can find it.
                    using var dialog = new SaveFileDialog
                    {
                        Title = "Save DXF As",
                        Filter = "DXF Files (*.dxf)|*.dxf",
                        FileName = ExportFilename,
                        DefaultExt = "dxf",
                        InitialDirectory = GetSourceStpDirectory()
                    };

                    if (dialog.ShowDialog() != DialogResult.OK)
                        return null;

                    this.exportFileName = System.IO.Path.GetFileName(dialog.FileName);
                    this.exportDirectory = System.IO.Path.GetDirectoryName(dialog.FileName);
                    return this.exportDirectory;
                }
                return this.exportDirectory;
            }

            set
            { this.exportDirectory = value; }
        }

        /// <summary>
        /// Searches document property sets for a string value that points to a .stp/.step file
        /// on disk. Returns the full path, or null if not found.
        /// </summary>
        private string GetSourceStpPath()
        {
            try
            {
                foreach (PropertySet propSet in partDoc.PropertySets)
                {
                    foreach (Property prop in propSet)
                    {
                        if (prop.Value is string val &&
                            (val.EndsWith(".stp", StringComparison.OrdinalIgnoreCase) ||
                             val.EndsWith(".step", StringComparison.OrdinalIgnoreCase)) &&
                            System.IO.File.Exists(val))
                        {
                            LogManager.Log.Information($"Found source STP path in property '{prop.Name}': {val}");
                            return val;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.Log.Warning($"Could not read document properties for STP path: {ex.Message}");
            }

            return null;
        }

        private string GetSourceStpDirectory()
        {
            var stpPath = GetSourceStpPath();
            return stpPath != null
                ? System.IO.Path.GetDirectoryName(stpPath)
                : System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        }

        public string ExportFullPath
        {
            get {
                return System.IO.Path.Combine(ExportDirectory, ExportFilename); }
        }


        private static string colorToRgb(System.Drawing.Color color)
        {
            return String.Format("{0};{1};{2}", color.R, color.G, color.B);
        }

        public bool ExportFlatDXF(PartDocument oDoc)
        {
            LogManager.Log.Information("------ Begin DXF export ------");
            LogManager.Log.Information($"CAD Model Name: {oDoc.DisplayName}");

            // ExportDirectory may prompt user; if they cancel, abort silently
            if (ExportDirectory == null) return false;

            LogManager.Log.Information($"Export full path: {ExportFullPath}");

            SheetMetalComponentDefinition smCompDef;

            if (oDoc == null)
            {
                MessageBox.Show("Part document is null.", "Export error!");
                return false;
            }

            if (oDoc.DocumentType == DocumentTypeEnum.kPartDocumentObject)
            {
                Console.WriteLine("Is Inventor Part document");

                if (oDoc.SubType == "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}")
                {
                    Console.WriteLine("Is sheet metal component!");

                    // we MUST class as a sheet metal comp def
                    smCompDef = (SheetMetalComponentDefinition)oDoc.ComponentDefinition;
                }
                else
                {
                    MessageBox.Show("The selected item is not a valid Sheetmetal component.", "Export Error!");
                    return false;
                }

                // Unfold if we don't already have a flat pattern
                if (!smCompDef.HasFlatPattern)
                {
                    try
                    {
                        smCompDef.Unfold();
                    }
                    catch
                    {
                        MessageBox.Show("Inventor encountered an error while unfolding the part.\n"
                                      + "Please correct the issue with the model and try again.", "Unfold error!");
                        return false;
                    }
                }

                // set export length units
                UnitsOfMeasure originalUnits = oDoc.UnitsOfMeasure;
                oDoc.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kInchLengthUnits;

                FlatPattern fPatt = smCompDef.FlatPattern;

                string filePath = System.IO.Path.Combine(LogManager.addinPath, "config/export.txt");

                ExportConfigManager exportConfigManager = new();

                foreach (LineTypeEnum lineType in Enum.GetValues(typeof(LineTypeEnum)))
                {
                    try
                    {
                        string key = String.Format("LineTypeEnum.{0}", Enum.GetName(typeof(LineTypeEnum), lineType));
                        int value = (int)lineType;

                        LogManager.Log.Debug($"Key: {key}, Value: {value}");
                        exportConfigManager.AddValue(key, value.ToString());
                    }
                    catch { 
                    
                    }
                }

                exportConfigManager.LoadConfigFile(filePath);


                string sOut = "FLAT PATTERN DXF?AcadVersion=2007"
                            // Outer Profile Layer
                            + "&OuterProfileLayer=" + Properties.DxfSettings.Default.OuterProfileLayer
                            + "&OuterProfileLayerColor=" + colorToRgb(Properties.DxfSettings.Default.OuterProfileLayerColor)
                            + "&OuterProfileLineType=" + ((decimal)Properties.DxfSettings.Default.OuterProfileLineType)

                            // Interior Profile Layer
                            + "&InteriorProfilesLayer=" + Properties.DxfSettings.Default.InteriorProfilesLayer
                            + "&InteriorProfilesLayerColor=" + colorToRgb(Properties.DxfSettings.Default.InteriorProfilesLayerColor)
                            + "&InteriorProfilesLineType=" + ((decimal)Properties.DxfSettings.Default.InteriorProfilesLineType)

                            // Bend Up Layer
                            //+ "&BendUpLayer=not_used"
                            //+ "&BendUpLayerColor=0;0;255"
                            //+ "&BendUpLineType=" + ((decimal)LineTypeEnum.kDashedLineType)

                            // Bend Down Layer
                            //+ "&BendDownLayer=not_used"
                            //+ "&BendDownLayerColor=255;0;191"
                            //+ "&BendDownLineType=" + ((decimal)LineTypeEnum.kDashedLineType)

                            // Layers to hide
                            + "&InvisibleLayers=IV_TANGENT;IV_ARC_CENTERS;IV_BEND;IV_BEND_DOWN;IV_UNCONSUMED_SKETCHES;not_used"

                            // Other Export Settings
                            //+ "&MergeProfilesIntoPolyline=True"
                            //+ "&SimplifySplines=True"
                            //+ "&AdvancedLegacyExport=True"
                            //+ "&SplineTolerance=0.01"
                            ;

                DataIO oDataIO = oDoc.ComponentDefinition.DataIO;
                oDataIO.WriteDataToFile(sOut, ExportFullPath);

                LogManager.Log.Information($"Inventor export complete, adding bend lines");

                UnitsOfMeasure docUnits = oDoc.UnitsOfMeasure;
                string uString = docUnits.GetStringFromType(docUnits.LengthUnits);


                // read DXF and add bend lines
                var file = DxfFile.Load(ExportFullPath);
                file.Header.Version = DxfAcadVersion.R2007;
                file.ApplicationIds.Add(new DxfAppId("POS3000_V3_PRODUCT"));
                file.ApplicationIds.Add(new DxfAppId("POS3000_V3_BENDINGLINE"));

                // convert from internal units (CM) to document units
                double toDocUnits(double value)
                {
                    return docUnits.ConvertUnits(value, UnitsTypeEnum.kCentimeterLengthUnits, docUnits.LengthUnits);
                }

                // get sheet thickness from model
                var sheetThickness = toDocUnits((double)smCompDef.Thickness.Value);
                var materialName = smCompDef.Material.Name;

                // ToDo: Find a better way to get the Outline layer
                foreach (DxfLayer layer in file.Layers)
                {

                    if (layer.Name == Properties.DxfSettings.Default.OuterProfileLayer)
                    {
                        // add XData with product info
                        layer.XData["POS3000_V3_PRODUCT"] = new DxfXDataApplicationItemCollection
                        (
                            new DxfXDataString(String.Format("Thickness={0:F4}", sheetThickness)),
                            new DxfXDataString(String.Format("MaterialId=({0})", materialName))
                        );
                    }
                }

                bool bendDownEnabled = Properties.DxfSettings.Default.BendDownEnabled;
                var bUpLayer = Properties.DxfSettings.Default.BendUpLayer;
                var bDownLayer = bendDownEnabled
                    ? Properties.DxfSettings.Default.BendDownLayer
                    : bUpLayer;

                string lineTypeToDxfName(LineTypeEnum lt) =>
                    Custom_Controls.LineTypeComboBox.Styles
                        .FirstOrDefault(s => s.LineType == lt)?.DxfName ?? "CONTINUOUS";

                var bUpLineType = lineTypeToDxfName(Properties.DxfSettings.Default.BendUpLineType);
                var bDownLineType = bendDownEnabled
                    ? lineTypeToDxfName(Properties.DxfSettings.Default.BendDownLineType)
                    : bUpLineType;

                // Ensure a line type is defined in the DXF file, adding it if missing
                void ensureLineType(string dxfName)
                {
                    if (file.LineTypes.Any(lt => lt.Name == dxfName)) return;
                    var style = Custom_Controls.LineTypeComboBox.Styles.FirstOrDefault(s => s.DxfName == dxfName);
                    if (style == null) return;
                    var lt2 = new DxfLineType(dxfName);
                    foreach (var len in style.DxfElements)
                        lt2.Elements.Add(new DxfLineTypeElement { DashDotSpaceLength = len });
                    file.LineTypes.Add(lt2);
                }

                // layerCustomColors tracks layers that use a custom (non-ACI) color,
                // so entities on those layers get Color24Bit set explicitly.
                var layerCustomColors = new Dictionary<string, System.Drawing.Color>();

                void ensureLayer(string layerName, string lineTypeName, System.Drawing.Color color)
                {
                    ensureLineType(lineTypeName);
                    var layer = file.Layers.FirstOrDefault(l => l.Name == layerName);
                    if (layer == null)
                    {
                        layer = new DxfLayer(layerName);
                        file.Layers.Add(layer);
                    }
                    layer.LineTypeName = lineTypeName;
                    var aci = Custom_Controls.ColorComboBox.GetAciIndex(color);
                    if (aci.HasValue)
                        layer.Color = DxfColor.FromIndex(aci.Value);
                    else
                        layerCustomColors[layerName] = color; // entity-level 24-bit color applied per entity
                }

                var upColor = Properties.DxfSettings.Default.BendUpLayerColor;
                var downColor = bendDownEnabled
                    ? Properties.DxfSettings.Default.BendDownLayerColor
                    : upColor;

                ensureLayer(bUpLayer, bUpLineType, upColor);
                ensureLayer(bDownLayer, bDownLineType, downColor);
                ensureLineType("DIVIDE"); // always available for hems

                foreach (FlatBendResult oBend in fPatt.FlatBendResults)
                {
                    // we only want bends on the top face, so continue if on bottom face
                    if (oBend.IsOnBottomFace) continue;

                    var bAngle = oBend.Angle * 180 / Math.PI;
                    var bRadius = toDocUnits(oBend.InnerRadius);

                    var bLayer = bUpLayer;

                    // down bends have a negative bend angle and optionally a separate layer
                    if (oBend.IsDirectionUp)
                    {
                        bAngle *= -1.0;
                        bLayer = bDownLayer;
                    }

                    Inventor.Point startPoint = oBend.Edge.StartVertex.Point;
                    Inventor.Point stopPoint = oBend.Edge.StopVertex.Point;

                    // bend start and end points
                    var x1 = toDocUnits(startPoint.X);
                    var y1 = toDocUnits(startPoint.Y);
                    var x2 = toDocUnits(stopPoint.X);
                    var y2 = toDocUnits(stopPoint.Y);

                    // shorten bend lines
                    var d = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

                    // new starting point
                    var t = 0.1 / d;
                    x1 = (1 - t) * x1 + t * x2;
                    y1 = (1 - t) * y1 + t * y2;

                    // new stopping point
                    t = (d - 0.1) / d;
                    x2 = (1 - t) * x1 + t * x2;
                    y2 = (1 - t) * y1 + t * y2;

                    var bLine = new DxfLine(new DxfPoint(x1, y1, 0.0), new DxfPoint(x2, y2, 0.0));
                    bLine.Layer = bLayer;
                    // custom colors can't be set on the layer (DxfLayer only supports ACI), so set per-entity
                    if (layerCustomColors.TryGetValue(bLayer, out var customColor))
                        bLine.Color24Bit = customColor.ToArgb() & 0xFFFFFF;
                    // hems override the layer line type with DIVIDE
                    if (Math.Abs(bAngle) > 179)
                        bLine.LineTypeName = "DIVIDE";

                    // add XData with bend info
                    bLine.XData["POS3000_V3_BENDINGLINE"] = new DxfXDataApplicationItemCollection(
                        new DxfXDataString(String.Format("BendAngleDeg={0:F3}", bAngle)),
                        new DxfXDataString(String.Format("InnerRadius={0:F4}", bRadius)),
                        new DxfXDataString(String.Format("K_Factor={0:F3}", oBend.kFactor))
                    //new DxfXDataString(String.Format("BendShortening=-.5"))
                    );

                    // add bend line to DXF
                    file.Entities.Add(bLine);
                }

                file.Save(ExportFullPath);

                // restore original units of measure
                oDoc.UnitsOfMeasure.LengthUnits = originalUnits.LengthUnits;

                LogManager.Log.Information($"Bend lines added, export complete.");

                return true;
            }

            return false;
        }
    }

    public class ExportConfigManager
    {
        public string Template { get; set; }
        public Dictionary<string, string> Values { get; private set; } = new();

        public ExportConfigManager AddValue(string key, string value)
        {
            Values[key] = value;
            return this;
        }


        public string LoadConfigFile(string configFile)
        {
            try
            {
                //var lines = System.IO.File.ReadAllLines(configFile)
                //                .Where(line => !string.IsNullOrWhiteSpace(line) && !line.TrimStart().StartsWith("#"));


                //var lines = System.IO.File.ReadAllLines(configFile)
                //    .Select(line => line.Trim()) // Remove leading/trailing whitespace including newlines
                //    .Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                //    .ToList();

                var lines = System.IO.File.ReadAllLines(configFile);


                StringBuilder result = new StringBuilder();
                for (int i = 0; i < lines.Count(); i++)
                {
                    result.AppendLine($"{i + 1}: {lines[i]}");
                }
                LogManager.Log.Information($"\nRaw Config file:\n********\n{result}\n********");


                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    int lineNumber = i + 1;

                    if (line.Trim().StartsWith("#")) continue;  // Ignore comment lines
                    if (line.Trim() is "") continue;            // Ignore blank lines

                    try
                    {
                        LogManager.Log.Information($"Config Line {i+1}: {line}");

                        string renderedLine =
                            Regex.Replace(line, @"{{\s*(\w+)\s*}}", match =>
                            {
                                var key = match.Groups[1].Value;
                                if (Values != null && Values.TryGetValue(key, out var value)) return value;
                                else
                                {
                                    LogManager.Log.Error($"No value found for key '{key}' in config file line {i + 1}");
                                    return "<MISSING KEY>";
                                }
                            });

                        lines[i] = renderedLine;

                    }
                    catch (Exception ex)
                    {
                        {
                            LogManager.Log.Error($"Error in config file on line {i+1}: {ex.Message}\n{ex.StackTrace}");
                        }
                    }

                    //string inventorString = Render(string.Join("&", lines));
                    string inventorString = string.Join("&", lines);

                    LogManager.Log.Information($"Rendered inventor command:\n{inventorString}");
                    return inventorString;
                }
                return "";

            }
            catch (Exception ex)
            {
                LogManager.Log.Error($"Error processing config file: {ex.Message}\n{ex.StackTrace}");
                MessageBox.Show($"Error processing config file: {ex.Message}", "Config Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        } 


        public string Render(string template)
        {
            return Regex.Replace(template, @"{{\s*(\w+)\s*}}", match =>
            {

                var key = match.Groups[1].Value;

                LogManager.Log.Error($"Key: {key}");

                if (Values != null && Values.TryGetValue(key, out var value))
                {
                    return value;
                }
                else
                {
                    return "<MISSING KEY>";
                }

            });
        }
    }
}
