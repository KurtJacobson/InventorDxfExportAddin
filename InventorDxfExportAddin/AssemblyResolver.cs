using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace InventorDxfExportAddin
{
    internal static class AssemblyResolver
    {
        [ModuleInitializer]
        internal static void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
        }

        private static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            string addinDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assemblyName = new AssemblyName(args.Name).Name;
            string assemblyPath = Path.Combine(addinDir, assemblyName + ".dll");
            return File.Exists(assemblyPath) ? Assembly.LoadFrom(assemblyPath) : null;
        }
    }
}

// C# 9 ModuleInitializerAttribute is not included in net48 — define it here so the
// compiler can emit the module .cctor without needing a newer runtime.
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal sealed class ModuleInitializerAttribute : Attribute { }
}
