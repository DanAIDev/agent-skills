<#
.SYNOPSIS
Create a new skill scaffold from repository conventions.
#>

param(
    [Parameter(Mandatory = $true)]
    [string]$SkillName,

    [Parameter(Mandatory = $true)]
    [string]$Category,

    [Parameter(Mandatory = $true)]
    [string]$Description
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

function Convert-ToTitle {
    param([string]$Text)
    $spaced = $Text -replace "-", " "
    return (Get-Culture).TextInfo.ToTitleCase($spaced)
}

if ($SkillName -notmatch "^[a-z0-9]+(?:-[a-z0-9]+)*$") {
    Write-Error "SkillName must be kebab-case."
}

if ($Category -notmatch "^[a-z0-9]+(?:-[a-z0-9]+)*$") {
    Write-Error "Category must be kebab-case."
}

$skillPath = Join-Path -Path "skills/$Category" -ChildPath $SkillName
if (Test-Path -Path $skillPath) {
    Write-Error "Skill already exists: $skillPath"
}

New-Item -ItemType Directory -Force -Path "$skillPath/scripts" | Out-Null
New-Item -ItemType Directory -Force -Path "$skillPath/references" | Out-Null
New-Item -ItemType Directory -Force -Path "$skillPath/assets" | Out-Null

$title = Convert-ToTitle -Text $SkillName

$skillMd = @"
---
name: $SkillName
description: $Description
---

# $title

## Objective

Explain what outcome this skill should produce.

## Workflow

1. Define context and constraints.
2. Execute deterministic steps.
3. Validate output before returning.

## Examples

### Example 1

User prompt:
```
<add realistic prompt>
```

### Example 2

User prompt:
```
<add realistic prompt>
```

## Edge Cases

- Missing context
- Invalid input format
- Partial failures
"@

$skillMd | Set-Content -Encoding UTF8 -Path "$skillPath/SKILL.md"
Set-Content -Encoding UTF8 -Path "$skillPath/scripts/.gitkeep" -Value ""
Set-Content -Encoding UTF8 -Path "$skillPath/references/.gitkeep" -Value ""
Set-Content -Encoding UTF8 -Path "$skillPath/assets/.gitkeep" -Value ""

Write-Host "Created skill at $skillPath" -ForegroundColor Green
