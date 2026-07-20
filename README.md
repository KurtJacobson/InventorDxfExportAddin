# Inventor DXF Export Addin

An Autodesk Inventor addin that exports sheet metal flat patterns to DXFs with bendlines, formatted for use with Schröder Folders.

[![User Manual](https://img.shields.io/badge/docs-User%20Manual-blue)](https://github.com/KurtJacobson/InventorDxfExportAddin/releases/latest/download/manual.pdf)

## Features

- Exports flat pattern DXF from sheet metal parts directly from the Flat Pattern ribbon tab
- Adds bend lines to the DXF with angle, inner radius, and K-factor as xData
- Embeds material name and sheet thickness as xData on the Outline layer
- Supports exporting from unsaved documents (e.g. STP files imported into Inventor)
- Configurable layer names, colors, and line types for outer profiles, inner profiles, bend up, and bend down lines
- **Organization-wide settings sync** — deploy a shared JSON config on a network share; users see a reset button next to any setting they have overridden locally

## Requirements

- Autodesk Inventor 2020 or later
- .NET Framework 4.8 (included with Windows 10 version 1903+ and all supported Windows 11 versions)

> **Tested with Inventor 2022, 2024, and 2026.**

## Installation

Run the `InventorDxfExportAddin-x.y.z.msi` installer. It installs the addin to:

```
%ProgramData%\Autodesk\ApplicationPlugins\InventorDxfExport\
```

Inventor automatically loads addins from this directory on startup — no manual registration required.

## Building from Source

**Requirements:** Visual Studio 2022 or later with the .NET desktop workload, or the .NET SDK with the `Microsoft.NETFramework.ReferenceAssemblies` package (included in the project).

```bash
# Debug build — deploys directly to Inventor's plugin folder for rapid iteration
dotnet build -c Debug

# Release build — outputs to bin\Release\net48\
dotnet build -c Release

# Build the MSI installer (requires Release build first)
dotnet build InventorDxfExportAddin.Setup -c Release
```

The versioned MSI will be output to:
```
InventorDxfExportAddin.Setup\bin\Release\InventorDxfExportAddin-x.y.z.msi
```

The version number is defined in `Directory.Build.props` at the solution root and flows through to the assembly, the MSI filename, and the installer package automatically.

## Usage

1. Open a sheet metal part in Inventor
2. Navigate to the **Flat Pattern** tab in the ribbon
3. Click **Export DXF** in the *Schröder DXF Export* panel
4. If the part has not been saved, a file dialog will prompt you to choose the DXF output location
5. The DXF is written to the chosen location with bend lines added

## Configuration

Layer names, colors, and export options are configured via the **DXF Settings** and **Export Options** buttons in the ribbon panel.

The `config/export.txt` file (located in the addin's install directory) controls additional Inventor flat pattern export parameters such as spline tolerance and polyline merging.

### Organization-wide settings

To push a shared baseline configuration to all users, place a copy of `docs/global_settings.jsonc` on a network share and add the following registry key to each machine (e.g. via Group Policy):

```
HKEY_LOCAL_MACHINE\SOFTWARE\InventorDxfExport
  GlobalConfigPath  REG_SZ  \\server\share\global_settings.jsonc
```

Users can still override any individual setting. A **↺** button appears next to settings that differ from the shared config, letting them revert to the org default at any time.

See [`docs/global_settings.jsonc`](docs/global_settings.jsonc) for the full list of available keys and their default values.

## Logging

Log files are written to the `logs/` subdirectory of the addin's install directory, rolling daily:

```
%ProgramData%\Autodesk\ApplicationPlugins\InventorDxfExport\logs\event_log_YYYYMMDD.txt
```
