<#
.SYNOPSIS
Bootstrap a fresh agent-skills repository.
.DESCRIPTION
Creates folder structure and core files in a target path.
#>

param(
    [Parameter(Mandatory = $true)]
    [string]$TargetPath,

    [string]$RepoName = "agent-skills"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$root = Join-Path -Path $TargetPath -ChildPath $RepoName
if (Test-Path -Path $root) {
    Write-Error "Target already exists: $root"
}

$dirs = @(
    ".github/ISSUE_TEMPLATE",
    ".github/workflows",
    "docs/examples",
    "scripts",
    "skills/_template/scripts",
    "skills/_template/references",
    "skills/_template/assets"
)

New-Item -ItemType Directory -Force -Path $root | Out-Null
foreach ($d in $dirs) {
    New-Item -ItemType Directory -Force -Path (Join-Path $root $d) | Out-Null
}

@"
# $RepoName

Repository scaffold created by `scripts/initialize-repo.ps1`.
"@ | Set-Content -Encoding UTF8 -Path (Join-Path $root "README.md")

Write-Host "Repository scaffold created at $root" -ForegroundColor Green
