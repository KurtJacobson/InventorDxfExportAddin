using Inventor;
using InventorDxfExportAddin.Buttons;
using Microsoft.VisualBasic.Logging;
using Serilog;
using Serilog.Sinks.File;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Reflection;
using System.Windows.Forms;


internal class CaptureFilePathHook : FileLifecycleHooks
{
    public string? Path { get; private set; }
    public override Stream OnFileOpened(string path, Stream underlyingStream, Encoding encoding)
    {
        Path = path;
        return base.OnFileOpened(path, underlyingStream, encoding);
    }
}

namespace InventorDxfExportAddin
{

    public static class LogManager
    {
        public static ILogger Log { get; private set; }

        public static string addinPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string logFilePath = System.IO.Path.Combine(addinPath, "logs");

        public static string addinVersion = Assembly.GetExecutingAssembly()
                   .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                   ?.InformationalVersion;

        // Keep a reference to the hook so we can read it later
        private static CaptureFilePathHook _filePathHook;

        // Public accessor so other classes can read the current file path
        public static string CurrentLogFilePath => _filePathHook?.Path;

        static LogManager()
        {
            _filePathHook = new CaptureFilePathHook();

            Log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(
                    System.IO.Path.Combine(logFilePath, "event_log_.txt"),
                    rollingInterval: RollingInterval.Day,
                    hooks: _filePathHook)
                .CreateLogger();

            Log.Information("=========================== RELOAD ===========================");
            Log.Information($"Logging to: {_filePathHook.Path}");
            Log.Information($"AddIn Version: {addinVersion}");
        }
    }


    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [Guid("E640038B-84E1-4611-952F-CA2C15C294D1")]
    public class AddinServer : Inventor.ApplicationAddInServer
    {
        // The Inventor application instance
        public static Inventor.Application InventorApp;

        public static Guid AddinGuid = new("E640038B-84E1-4611-952F-CA2C15C294D1");

        List<InventorButton> _buttons;

        #region ApplicationAddInServer Members

        /// <summary>
        /// This method is called by Inventor when it loads the addin.
        /// The AddInSiteObject provides access to the Inventor Application object.
        /// The FirstTime flag indicates if the addin is loaded for the first time.
        /// </summary>
        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            InventorApp = addInSiteObject.Application;
            InventorApp.ApplicationEvents.OnApplicationOptionChange += UpdateButtons;

            try
            {
                // If the addin is loaded for the first time, initialize the UI components
                if (firstTime)
                {
                    LogManager.Log.Information($"Application Version: AutoDesk Inventor {InventorApp.SoftwareVersion.DisplayName}");

                    InitializeUIComponents();

                    LogManager.Log.Information("Successfully loaded InventorDxfExportAddin");
                }
            }
            catch (Exception ex)
            {
                LogManager.Log.Error("Could not load InventorDxfExportAddin");
                do
                {
                    LogManager.Log.Error("{0}\n{1}", ex.Message, ex.StackTrace);
                }
                while ((ex = ex.InnerException) != null);

                MessageBox.Show($"Could not load InventorDxfExportAddin.\nSee log file for more info.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Initializes the UI components of the addin.
        /// </summary>
        private void InitializeUIComponents()
        {
            _buttons = Assembly.GetAssembly(typeof(InventorButton)).GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(InventorButton)))
                .Select(Activator.CreateInstance)
                .Cast<InventorButton>()
                .Where(button => button.Enabled)
                .OrderBy(button => button.SequenceNumber)
                .ToList();

            _buttons.ForEach(b => b.Initialize());
        }

        /// <summary>
        /// Updates the buttons when the application options change.
        /// </summary>
        private void UpdateButtons(EventTimingEnum beforeOrAfter, NameValueMap context, out HandlingCodeEnum handlingCode)
        {
            if (beforeOrAfter == EventTimingEnum.kAfter)
            {
                _buttons.ForEach(b => b.Dispose());

                InitializeUIComponents();

                handlingCode = HandlingCodeEnum.kEventHandled;
            }

            handlingCode = HandlingCodeEnum.kEventNotHandled;
        }

        /// <summary>
        /// This method is called by Inventor when the AddIn is unloaded.
        /// The AddIn will be unloaded either manually by the user or
        /// when the Inventor session is terminated.
        /// </summary>
        public void Deactivate()
        {
            LogManager.Log.Information("AddIn unloaded/deactivated");

            InventorApp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// This method is now obsolete, you should use the
        /// ControlDefinition functionality for implementing commands.
        /// </summary>
        public void ExecuteCommand(int commandID)
        {
        }

        /// <summary>
        /// This property is provided to allow the AddIn to expose an API
        /// of its own to other programs. Typically, this  would be done by
        /// implementing the AddIn's API interface in a class and returning
        /// that class object through this property.
        /// </summary>
        public object Automation
        {
            get
            {
                return null;
            }
        }

        #endregion

    }
}