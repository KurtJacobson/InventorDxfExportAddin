using Inventor;
using InventorDxfExportAddin.DxfExport;
using InventorDxfExportAddin.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Application  = System.Windows.Forms.Application;
using Directory    = System.IO.Directory;
using Environment  = System.Environment;
using File         = System.IO.File;
using Path         = System.IO.Path;

namespace InventorDxfExportAddin
{
    internal class BatchPartResult
    {
        public SheetMetalPart Part         { get; set; }
        public ExportResult   ExportResult { get; set; }
    }

    internal static class AssemblyBatchExporter
    {
        public static void Run(AssemblyDocument asmDoc,
                               List<SheetMetalPart> parts,
                               OverwritePolicy policy)
        {
            var results   = new List<BatchPartResult>(parts.Count);
            bool cancelled = false;

            // ── Export loop ────────────────────────────────────────────────
            using (var progress = new FormBatchProgress(parts.Count))
            {
                progress.Show(InventorWindow.Instance);

                for (int i = 0; i < parts.Count; i++)
                {
                    Application.DoEvents();

                    if (progress.Cancelled)
                    {
                        cancelled = true;
                        break;
                    }

                    var part  = parts[i];
                    string label = !string.IsNullOrEmpty(part.PartNumber)
                        ? part.PartNumber
                        : Path.GetFileName(part.FullFileName);

                    progress.UpdateProgress(i + 1, label);
                    Application.DoEvents();

                    ExportResult result;

                    if (part.HasError || part.Document == null)
                    {
                        result = ExportResult.Fail(
                            part.LoadError ?? "Part document could not be accessed.");
                    }
                    else
                    {
                        var engine = new DxfExportEngine(part.Document)
                        {
                            SuppressDialogs = true,
                            OverwritePolicy = policy,
                        };
                        try
                        {
                            result = engine.ExportFlatDXF(part.Document);
                        }
                        catch (Exception ex)
                        {
                            result = ExportResult.Fail(ex.Message);
                            LogManager.Log.Error(
                                $"Batch export exception for '{part.FullFileName}': {ex.Message}\n{ex.StackTrace}");
                        }
                    }

                    results.Add(new BatchPartResult { Part = part, ExportResult = result });
                    LogManager.Log.Information(
                        $"Batch [{i + 1}/{parts.Count}] {label}: " +
                        (result.Success ? $"OK → {result.OutputPath}"
                        : result.Skipped ? $"skipped ({result.OutputPath})"
                        : $"FAILED — {result.ErrorMessage}"));
                }
            }

            // ── Report ─────────────────────────────────────────────────────
            string reportPath = WriteReport(asmDoc, parts, results, cancelled);

            int successes = 0, skips = 0, errors = 0;
            foreach (var r in results)
            {
                if (r.ExportResult.Success)       successes++;
                else if (r.ExportResult.Skipped)  skips++;
                else                              errors++;
            }

            string summary =
                cancelled ? $"Export cancelled after {results.Count} of {parts.Count} parts.\n\n" : "";
            summary += $"{successes} exported, {skips} skipped, {errors} failed.";

            if (reportPath != null)
            {
                summary += "\n\nOpen the results report?";
                var answer = MessageBox.Show(summary, "Export Assembly DXFs",
                    MessageBoxButtons.YesNo,
                    errors > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
                if (answer == DialogResult.Yes)
                    Process.Start(new ProcessStartInfo(reportPath) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show(summary, "Export Assembly DXFs",
                    MessageBoxButtons.OK,
                    errors > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }
        }

        // ── HTML report ────────────────────────────────────────────────────

        private static string WriteReport(AssemblyDocument asmDoc,
                                          List<SheetMetalPart> allParts,
                                          List<BatchPartResult> results,
                                          bool cancelled)
        {
            try
            {
                string dir = ReportDirectory(asmDoc);
                Directory.CreateDirectory(dir);

                string asmName = Path.GetFileNameWithoutExtension(asmDoc.DisplayName);
                string path    = Path.Combine(dir, $"{asmName}_DxfExportReport.html");

                File.WriteAllText(path, BuildHtml(asmDoc, allParts, results, cancelled),
                    Encoding.UTF8);

                LogManager.Log.Information($"Batch report written to: {path}");
                return path;
            }
            catch (Exception ex)
            {
                LogManager.Log.Error($"Could not write batch report: {ex.Message}");
                return null;
            }
        }

        private static string ReportDirectory(AssemblyDocument asmDoc)
        {
            string full = asmDoc.FullFileName;
            if (!string.IsNullOrEmpty(full))
                return Path.GetDirectoryName(full);

            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "InventorDxfExport", "Reports");
        }

        private static string BuildHtml(AssemblyDocument asmDoc,
                                        List<SheetMetalPart> allParts,
                                        List<BatchPartResult> results,
                                        bool cancelled)
        {
            int successes = 0, skips = 0, errors = 0;
            foreach (var r in results)
            {
                if (r.ExportResult.Success)       successes++;
                else if (r.ExportResult.Skipped)  skips++;
                else                              errors++;
            }
            int notReached = allParts.Count - results.Count;

            string asmName  = asmDoc.DisplayName;
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd  HH:mm");

            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\"><head><meta charset=\"utf-8\">");
            sb.AppendLine($"<title>DXF Export Report — {Esc(asmName)}</title>");
            sb.AppendLine("<style>");
            sb.AppendLine(@"
  *, *::before, *::after { box-sizing: border-box; margin: 0; padding: 0; }
  body   { font-family: 'Segoe UI', Arial, sans-serif; font-size: 13px;
           background: #f4f4f5; color: #1a1a1a; padding: 24px; }
  h1     { font-size: 18px; font-weight: 600; margin-bottom: 4px; }
  .meta  { color: #666; font-size: 12px; margin-bottom: 20px; }
  .stats { display: flex; gap: 12px; margin-bottom: 24px; flex-wrap: wrap; }
  .stat  { background: #fff; border: 1px solid #ddd; border-radius: 6px;
           padding: 10px 18px; min-width: 110px; text-align: center; }
  .stat .n  { font-size: 26px; font-weight: 700; line-height: 1; }
  .stat .lbl{ font-size: 11px; color: #666; margin-top: 3px; }
  .ok   .n  { color: #16a34a; }
  .skip .n  { color: #d97706; }
  .err  .n  { color: #dc2626; }
  .warn      { background: #fef3c7; border: 1px solid #f59e0b; border-radius: 6px;
               padding: 8px 14px; margin-bottom: 16px; font-size: 12px; color: #92400e; }
  table  { width: 100%; border-collapse: collapse; background: #fff;
           border: 1px solid #ddd; border-radius: 6px; overflow: hidden; }
  th     { background: #f0f0f1; font-weight: 600; font-size: 12px;
           padding: 8px 10px; text-align: left; border-bottom: 1px solid #ddd; }
  td     { padding: 7px 10px; border-bottom: 1px solid #eee; vertical-align: top; }
  tr:last-child td { border-bottom: none; }
  .tag   { display: inline-block; border-radius: 4px; padding: 1px 7px;
           font-size: 11px; font-weight: 600; white-space: nowrap; }
  .tag-ok   { background: #dcfce7; color: #166534; }
  .tag-skip { background: #fef9c3; color: #854d0e; }
  .tag-err  { background: #fee2e2; color: #991b1b; }
  .path  { font-family: 'Consolas', monospace; font-size: 11px; color: #555;
           word-break: break-all; }
  .err-msg { color: #dc2626; font-size: 12px; }
");
            sb.AppendLine("</style></head><body>");

            sb.AppendLine($"<h1>DXF Export Report — {Esc(asmName)}</h1>");
            sb.AppendLine($"<p class=\"meta\">Generated {Esc(dateTime)}</p>");

            // Summary stats
            sb.AppendLine("<div class=\"stats\">");
            sb.AppendLine($"<div class=\"stat\"><div class=\"n\">{allParts.Count}</div><div class=\"lbl\">Parts found</div></div>");
            sb.AppendLine($"<div class=\"stat ok\"><div class=\"n\">{successes}</div><div class=\"lbl\">Exported</div></div>");
            sb.AppendLine($"<div class=\"stat skip\"><div class=\"n\">{skips}</div><div class=\"lbl\">Skipped</div></div>");
            sb.AppendLine($"<div class=\"stat err\"><div class=\"n\">{errors + notReached}</div><div class=\"lbl\">Failed / not reached</div></div>");
            sb.AppendLine("</div>");

            if (cancelled)
                sb.AppendLine($"<div class=\"warn\">⚠ Export was cancelled after {results.Count} of {allParts.Count} parts. " +
                              $"{notReached} part(s) were not processed.</div>");

            // Results table
            sb.AppendLine("<table>");
            sb.AppendLine("<thead><tr>");
            sb.AppendLine("<th>Part Number</th><th>Material</th><th>Thickness</th>");
            sb.AppendLine("<th style=\"text-align:center\">Qty</th><th>Status</th><th>Output / Error</th>");
            sb.AppendLine("</tr></thead><tbody>");

            // Rows for processed parts
            foreach (var r in results)
            {
                var p = r.Part;
                var x = r.ExportResult;

                string tagClass, tagText, detail;
                if (x.Success)
                {
                    tagClass = "tag-ok";  tagText = "✓ Exported";
                    detail   = $"<span class=\"path\">{Esc(x.OutputPath)}</span>";
                }
                else if (x.Skipped)
                {
                    tagClass = "tag-skip"; tagText = "⏭ Skipped";
                    detail   = $"<span class=\"path\">{Esc(x.OutputPath)}</span>";
                }
                else
                {
                    tagClass = "tag-err"; tagText = "✗ Failed";
                    detail   = $"<span class=\"err-msg\">{Esc(x.ErrorMessage)}</span>";
                }

                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{Esc(p.PartNumber)}</td>");
                sb.AppendLine($"<td>{Esc(p.Material)}</td>");
                sb.AppendLine($"<td>{Esc(p.Thickness)}</td>");
                sb.AppendLine($"<td style=\"text-align:center\">{p.Quantity}</td>");
                sb.AppendLine($"<td><span class=\"tag {tagClass}\">{tagText}</span></td>");
                sb.AppendLine($"<td>{detail}</td>");
                sb.AppendLine("</tr>");
            }

            // Rows for parts not reached due to cancellation
            for (int i = results.Count; i < allParts.Count; i++)
            {
                var p = allParts[i];
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{Esc(p.PartNumber)}</td>");
                sb.AppendLine($"<td>{Esc(p.Material)}</td>");
                sb.AppendLine($"<td>{Esc(p.Thickness)}</td>");
                sb.AppendLine($"<td style=\"text-align:center\">{p.Quantity}</td>");
                sb.AppendLine($"<td><span class=\"tag tag-err\">⊘ Cancelled</span></td>");
                sb.AppendLine("<td></td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody></table>");
            sb.AppendLine("</body></html>");

            return sb.ToString();
        }

        private static string Esc(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            return s.Replace("&", "&amp;")
                    .Replace("<", "&lt;")
                    .Replace(">", "&gt;")
                    .Replace("\"", "&quot;");
        }
    }
}
