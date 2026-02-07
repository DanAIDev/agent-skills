<#
.SYNOPSIS
Validate all SKILL.md files in this repository.
#>

param(
    [switch]$Strict
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$errors = 0
$warnings = 0
$valid = 0

function Get-FrontmatterMap {
    param([string]$Content)

    $m = [regex]::Match($Content, "(?s)^---\s*\r?\n(.*?)\r?\n---")
    if (-not $m.Success) {
        return $null
    }

    $map = @{}
    $lines = ($m.Groups[1].Value -split "\r?\n")
    foreach ($line in $lines) {
        if ($line -match "^\s*([A-Za-z0-9_-]+)\s*:\s*(.*)\s*$") {
            $key = $Matches[1]
            $value = $Matches[2].Trim()
            if ($value.StartsWith('"') -and $value.EndsWith('"')) {
                $value = $value.Trim('"')
            }
            $map[$key] = $value
        }
    }
    return $map
}

Write-Host "Validating skills..." -ForegroundColor Cyan

$skillFiles = Get-ChildItem -Path "skills" -Recurse -File -Filter "SKILL.md"

if ($skillFiles.Count -eq 0) {
    Write-Host "No SKILL.md files found." -ForegroundColor Yellow
    exit 0
}

foreach ($file in $skillFiles) {
    $path = $file.FullName.Replace((Get-Location).Path + [IO.Path]::DirectorySeparatorChar, "")
    $content = Get-Content -Raw -Path $file.FullName
    $meta = Get-FrontmatterMap -Content $content
    $fileErr = 0

    if ($null -eq $meta) {
        Write-Host "  [ERROR] ${path}: missing YAML frontmatter" -ForegroundColor Red
        $errors++
        continue
    }

    $name = ""
    $description = ""
    if ($meta.ContainsKey("name")) { $name = $meta["name"] }
    if ($meta.ContainsKey("description")) { $description = $meta["description"] }

    if ([string]::IsNullOrWhiteSpace($name)) {
        Write-Host "  [ERROR] ${path}: missing 'name'" -ForegroundColor Red
        $errors++; $fileErr++
    } elseif ($name.Length -gt 64 -or $name -notmatch "^[a-z0-9]+(?:-[a-z0-9]+)*$") {
        Write-Host "  [WARN]  ${path}: invalid 'name' format ($name)" -ForegroundColor Yellow
        $warnings++
    }

    if ([string]::IsNullOrWhiteSpace($description)) {
        Write-Host "  [ERROR] ${path}: missing 'description'" -ForegroundColor Red
        $errors++; $fileErr++
    } elseif ($description.Length -gt 1024) {
        Write-Host "  [WARN]  ${path}: description over 1024 chars" -ForegroundColor Yellow
        $warnings++
    }

    if ($fileErr -eq 0) {
        Write-Host "  [OK]    $path" -ForegroundColor Green
        $valid++
    }
}

Write-Host ""
Write-Host "Summary: valid=$valid warnings=$warnings errors=$errors" -ForegroundColor Cyan

if ($errors -gt 0) {
    exit 1
}

if ($Strict -and $warnings -gt 0) {
    exit 1
}

exit 0
