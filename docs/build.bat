@echo off
cd /d "%~dp0"

for /f "delims=" %%v in ('git describe --tags --always 2^>nul') do set VERSION=%%v
if "%VERSION%"=="" set VERSION=dev

echo Building manual.html (version: %VERSION%)...

pandoc manual.md -o manual.html ^
  --standalone ^
  --embed-resources ^
  --toc --toc-depth=3 ^
  --section-divs ^
  --css=manual.css ^
  --lua-filter=strip-image-alt.lua ^
  --lua-filter=html-layout.lua ^
  --metadata "date=Version %VERSION%"

if %ERRORLEVEL%==0 (
    echo Done. Opening manual.html...
    start manual.html
) else (
    echo Build failed.
    pause
)
