<#
.SYNOPSIS
List all skills in the repository.
#>

param(
    [switch]$IncludeDescription,
    [switch]$Json
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

function Get-Meta {
    param([string]$Content)

    $m = [regex]::Match($Content, "(?s)^---\s*\r?\n(.*?)\r?\n---")
    if (-not $m.Success) { return @{} }

    $meta = @{}
    foreach ($line in ($m.Groups[1].Value -split "\r?\n")) {
        if ($line -match "^\s*([A-Za-z0-9_-]+)\s*:\s*(.*)\s*$") {
            $k = $Matches[1]
            $v = $Matches[2].Trim().Trim('"')
            $meta[$k] = $v
        }
    }
    return $meta
}

$skills = @()
$files = Get-ChildItem -Path "skills" -Recurse -File -Filter "SKILL.md" |
    Where-Object { $_.FullName -notmatch "\\_template\\" }

foreach ($file in $files) {
    $content = Get-Content -Raw -Path $file.FullName
    $meta = Get-Meta -Content $content

    $skillDir = Split-Path -Path $file.FullName -Parent
    $skillName = Split-Path -Path $skillDir -Leaf
    $categoryDir = Split-Path -Path $skillDir -Parent
    $category = Split-Path -Path $categoryDir -Leaf
    $path = "skills/$category/$skillName"

    $skills += [PSCustomObject]@{
        Name = $skillName
        Category = $category
        Description = ($meta["description"] | ForEach-Object { $_ })
        Path = $path
    }
}

if ($Json) {
    $skills | Sort-Object Category, Name | ConvertTo-Json -Depth 3
    exit 0
}

if ($skills.Count -eq 0) {
    Write-Host "No skills found."
    exit 0
}

$grouped = $skills | Sort-Object Category, Name | Group-Object -Property Category
foreach ($g in $grouped) {
    Write-Host $g.Name -ForegroundColor Yellow
    foreach ($skill in $g.Group) {
        if ($IncludeDescription -and -not [string]::IsNullOrWhiteSpace($skill.Description)) {
            Write-Host "  - $($skill.Name): $($skill.Description)"
        } else {
            Write-Host "  - $($skill.Name)"
        }
    }
}

Write-Host ""
Write-Host "Total skills: $($skills.Count)" -ForegroundColor Green
