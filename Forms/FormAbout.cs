using Serilog.Sinks.File;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorDxfExportAddin.Forms
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();

            // Make form window fixed size
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.lblInventorVersion.Text = $"{AddinServer.InventorApp.SoftwareVersion.DisplayName}";
            this.lblAddinPath.Text = LogManager.addinPath;
            this.lblAddinVersion.Text = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown";
            this.lblLogFilePath.Text = LogManager.CurrentLogFilePath;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close window
            this.Close();
        }

        private void btnShowLog_Click(object sender, EventArgs e)
        {
            var filePathHook = new CaptureFilePathHook();

            LogManager.Log.Debug("About Dialog :: Open log file pressed");

            using Process fileopener = new Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = LogManager.CurrentLogFilePath;
            fileopener.Start();
        }

        private void lblLogFilePath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var filePathHook = new CaptureFilePathHook();

            LogManager.Log.Debug("About Dialog :: Open log file link clicked");
            Process.Start(@"notepad.exe", LogManager.CurrentLogFilePath);
        }
    }
}
