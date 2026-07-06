using System.Diagnostics;
using System.Windows.Forms;

namespace InventorDxfExportAddin.Forms
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();

            this.lblInventorVersion.Text = $"{AddinServer.InventorApp.SoftwareVersion.DisplayName}";
            this.lblAddinPath.Text = LogManager.addinPath;
            this.lblAddinVersion.Text = LogManager.addinVersion;
            this.lblLogFilePath.Text = LogManager.CurrentLogFilePath;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close window
            this.Close();
        }

        private void btnShowLog_Click(object sender, EventArgs e)
        {
            LogManager.Log.Debug("About Dialog :: Open log file pressed");

            using Process fileopener = new Process();
            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = LogManager.CurrentLogFilePath;
            fileopener.Start();
        }

    }
}
