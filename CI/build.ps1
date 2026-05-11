# ==============================================================
# version information
# ==============================================================
$Version = "1.0.0"

# ==============================================================
# logging
# ==============================================================
function Write-Log {
  param(
    [string]$Message,
    [string]$Level = "INFO"
  )

  $Timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
  Write-Host "$Timestamp [$Level] $Message"
}

# ==============================================================
# paths
# ==============================================================
$ScriptDir = $PSScriptRoot
$RepositoryRoot = Resolve-Path "$ScriptDir\.."

$Solution = "$RepositoryRoot\Hangman.sln"
$ProjectDir = "$RepositoryRoot\Hangman"

$ReferenceRoot = "$RepositoryRoot\lib\reference-assemblies"
$ReferenceAssemblyPath = "$ReferenceRoot\.NETFramework\v4.6.1"

$BuildOutputDir = "$ProjectDir\bin\Release"
$TargetDir = "$RepositoryRoot"

# ==============================================================
# version
# ==============================================================
if ($args -contains "--version") {
  Write-Log "build.ps1 $Version"
  exit 0
}

# ==============================================================
# build solution
# ==============================================================
Write-Log "Build solution: $Solution"

dotnet build "$Solution" `
  --configuration Release `
  /p:TargetFrameworkRootPath="$ReferenceRoot" `
  /p:FrameworkPathOverride="$ReferenceAssemblyPath"

if ($LASTEXITCODE -ne 0) {
  Write-Log "Build failed" "ERROR"
  exit $LASTEXITCODE
}

# ==============================================================
# copy build output to repository root
# ==============================================================
Write-Log "Copy build output"
Write-Log "Source: $BuildOutputDir"
Write-Log "Target: $TargetDir"

Copy-Item "$BuildOutputDir\*" "$TargetDir" -Recurse -Force

Write-Log "Build completed"
Write-Log "Build output copied to repository root"

exit 0