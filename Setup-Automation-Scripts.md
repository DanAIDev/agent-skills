# AI Agent Skills Repository - Setup Automation Scripts

This document contains PowerShell scripts to automate repository initialization and maintenance tasks.

## Script 1: Initialize Repository Structure

**File: `initialize-repo.ps1`**

Usage: `.\initialize-repo.ps1 -RepoName "my-agent-skills" -AuthorName "Your Name"`

```powershell
param(
    [Parameter(Mandatory=$true)]
    [string]$RepoName,
    
    [Parameter(Mandatory=$true)]
    [string]$AuthorName,
    
    [string]$License = "MIT",
    [string]$Domain = "general"  # e.g., "data-engineering", "code-review"
)

Write-Host "🚀 Initializing AI Agent Skills Repository" -ForegroundColor Cyan

# Create root directory
if (Test-Path $RepoName) {
    Write-Host "❌ Directory '$RepoName' already exists" -ForegroundColor Red
    exit 1
}

New-Item -ItemType Directory -Name $RepoName | Out-Null
Push-Location $RepoName

# Create directory structure
Write-Host "📁 Creating directory structure..."
$dirs = @(
    "skills/_template/scripts",
    "skills/_template/references",
    "skills/_template/assets",
    ".github/ISSUE_TEMPLATE",
    ".github/workflows",
    "docs/examples",
    "scripts"
)

foreach ($dir in $dirs) {
    New-Item -ItemType Directory -Path $dir -Force | Out-Null
}

Write-Host "✅ Directory structure created"

# Create .gitignore
Write-Host "📝 Creating .gitignore..."
@"
# Python
__pycache__/
*.py[cod]
*`$py.class
*.so
.Python
build/
develop-eggs/
dist/
downloads/
eggs/
.eggs/
lib/
lib64/
parts/
sdist/
var/
wheels/
pip-wheel-metadata/
share/python-wheels/
*.egg-info/
.installed.cfg
*.egg
MANIFEST
venv/
ENV/

# Node
node_modules/
npm-debug.log
yarn-error.log

# IDE
.vscode/
.idea/
*.swp
*.swo
*~
.DS_Store

# Environment
.env
.env.local
.env.*.local
"@ | Out-File -Encoding UTF8 ".gitignore"

Write-Host "✅ .gitignore created"

# Create README.md
Write-Host "📝 Creating README.md..."
$readmeContent = @"
# $RepoName

AI agent skills for domain: **$Domain**

A curated collection of reusable, production-ready skills for AI coding assistants (Claude, Cursor, GitHub Copilot, etc.).

## 🎯 Purpose

This repository provides specialized agent skills that teach AI assistants how to solve specific problems in the **$Domain** domain.

## ✨ Features

- 🔧 **Easy to use**: Copy-paste skills to your project
- 📚 **Well documented**: Clear examples and edge cases
- ⚙️ **Customizable**: Adapt skills to your needs
- 🤝 **Community-friendly**: Easy contribution process
- 🎓 **Best practices**: Built with production lessons learned

## 🚀 Quick Start

### Installation

1. Clone this repository:
\`\`\`powershell
git clone https://github.com/yourusername/$RepoName.git
cd $RepoName
\`\`\`

2. Copy any skill to your AI assistant's skills directory:

**Cursor**:
\`\`\`powershell
Copy-Item -Path "skills\category\skill-name" -Destination `$env:APPDATA\.cursor\skills\skill-name -Recurse
\`\`\`

**GitHub Copilot**:
\`\`\`powershell
Copy-Item -Path "skills\category\skill-name" -Destination `$env:APPDATA\GitHub\Copilot\skills\skill-name -Recurse
\`\`\`

### Using a Skill

Tell your AI assistant to use the skill, or explicitly reference it:

\`\`\`
Use the [skill-name] skill to help me with [specific task]
\`\`\`

## 📚 Available Skills

| Skill | Category | Description |
|-------|----------|-------------|
| [To be added] | category | Brief description |

See [SKILLS.md](SKILLS.md) for complete list.

## 📖 Documentation

- [Getting Started](docs/getting-started.md) - Quick start guide
- [Skill Anatomy](docs/skill-anatomy.md) - How skills are structured
- [Writing Skills](docs/writing-skills.md) - Create your own skills
- [SKILL.md Specification](docs/SKILL.md-specification.md) - Full technical spec

## 🤝 Contributing

We welcome contributions! See [CONTRIBUTING.md](CONTRIBUTING.md) for:
- How to propose new skills
- Quality standards
- Submission process
- Code of conduct

## 📝 License

This project is licensed under the $License License. See [LICENSE](LICENSE) for details.

## 🙋 Support

- **Questions?** Open a [Discussion](../../discussions)
- **Found a bug?** Open an [Issue](../../issues)
- **Have an idea?** See [CONTRIBUTING.md](CONTRIBUTING.md)

---

**Created by**: $AuthorName  
**Repository**: github.com/yourusername/$RepoName  
**Status**: Active Development
"@

$readmeContent | Out-File -Encoding UTF8 "README.md"
Write-Host "✅ README.md created"

# Create CONTRIBUTING.md
Write-Host "📝 Creating CONTRIBUTING.md..."
$contributingContent = @"
# Contributing to $RepoName

Thank you for your interest in contributing! This guide explains how to add new skills or improve existing ones.

## 🎯 Getting Started

1. **Check existing skills** to avoid duplication
2. **Review** [docs/writing-skills.md](docs/writing-skills.md)
3. **Copy** `skills/_template/` as a starting point
4. **Follow** the SKILL.md specification
5. **Test** your skill with actual AI agents

## ✅ Skill Quality Standards

Your skill should:

- Have a **clear, single purpose**
- Include a well-written **SKILL.md** with YAML frontmatter
- Include **2-3 realistic usage examples**
- Handle **edge cases gracefully**
- Be **tested** with real use cases before submission
- **Not require** complex dependencies or setup

## 📤 Submission Process

### 1. Create a Branch

\`\`\`powershell
git checkout -b add/my-skill-name
\`\`\`

Name convention: `add/[skill-name]` for new skills, `fix/[skill-name]` for updates

### 2. Add Your Skill

Create skill in: `skills/[category]/[skill-name]/`

Structure:
\`\`\`
skills/[category]/[skill-name]/
├── SKILL.md (required)
├── scripts/ (optional)
│   └── [script-files]
├── references/ (optional)
│   └── [documentation-files]
└── assets/ (optional)
    └── [templates-and-resources]
\`\`\`

### 3. Update README

Add your skill to the skills table in main [README.md](README.md)

### 4. Submit Pull Request

Include in PR description:
- Skill name and purpose
- What problem it solves
- Example use cases
- Testing results

## 🎓 Learning From Examples

See `skills/_template/SKILL.md` for a complete template.

Reference examples:
- [docs/examples/](docs/examples/) - Breakdown of real skills

## 🏆 Skill Complexity Tiers

| Tier | Time | Description | Examples |
|------|------|-------------|----------|
| 1 | 1-2h | Simple instructions | Code reviewer, doc generator |
| 2 | 3-4h | Light scripting | Data transformer, validator |
| 3 | 5-8h | Moderate logic | Multi-step workflow |
| 4 | 8-16h | Complex features | API integration, state management |

Start with Tier 1 if first-time contributor!

## 📋 Pre-Submission Checklist

- [ ] SKILL.md has required frontmatter (name, description)
- [ ] Description is clear and includes "when to use" trigger
- [ ] Examples are tested and working
- [ ] Edge cases are documented
- [ ] No hardcoded values or secrets
- [ ] Skill serves a single, clear purpose
- [ ] README updated with skill entry
- [ ] No merge conflicts with main

## 🐛 Reporting Bugs

Use the [Bug Report](../../issues/new?template=bug-report.md) template.

Include:
- Which skill has the issue
- Steps to reproduce
- Expected vs. actual behavior
- Environment (Claude, Cursor, Copilot, etc.)

## 💡 Suggesting Skills

Use the [Skill Proposal](../../issues/new?template=skill-proposal.md) template.

Describe:
- What the skill does
- What problem it solves
- Example use cases
- Estimated complexity

## ❓ Questions?

- Check [Discussions](../../discussions) for Q&A
- Review [docs/](docs/) for detailed guides
- Look at existing skills for patterns

## 📖 Code of Conduct

This project follows a be-respectful, be-constructive philosophy:
- Be kind to fellow contributors
- Respect different opinions
- Focus on solving the problem
- No harassment or discrimination

## 🙏 Thank You

Thank you for contributing to make $RepoName better!

Questions? Open a [Discussion](../../discussions).
"@

$contributingContent | Out-File -Encoding UTF8 "CONTRIBUTING.md"
Write-Host "✅ CONTRIBUTING.md created"

# Create LICENSE
Write-Host "📝 Creating LICENSE..."
$licenseContent = @"
MIT License

Copyright (c) 2024 $AuthorName

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
"@

$licenseContent | Out-File -Encoding UTF8 "LICENSE"
Write-Host "✅ LICENSE created"

# Create CHANGELOG.md
Write-Host "📝 Creating CHANGELOG.md..."
$changelogContent = @"
# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/),
and this project adheres to [Semantic Versioning](https://semver.org/).

## [Unreleased]

### Added
- Initial repository setup
- Skill template structure
- Core documentation

## Types of Changes

- **Added**: for new features
- **Changed**: for changes in existing functionality
- **Deprecated**: for soon-to-be removed features
- **Removed**: for now removed features
- **Fixed**: for any bug fixes
- **Security**: in case of vulnerabilities
"@

$changelogContent | Out-File -Encoding UTF8 "CHANGELOG.md"
Write-Host "✅ CHANGELOG.md created"

# Create CODE_OF_CONDUCT.md
Write-Host "📝 Creating CODE_OF_CONDUCT.md..."
$conductContent = @"
# Code of Conduct

## Our Pledge

We are committed to providing a welcoming and inclusive environment for all contributors.

## Our Standards

Examples of behavior that creates a positive environment include:

- Using welcoming and inclusive language
- Being respectful of differing opinions, viewpoints, and experiences
- Giving and gracefully accepting constructive criticism
- Focusing on what is best for the community
- Showing empathy towards other community members

Examples of unacceptable behavior include:

- Harassment or intimidation of any kind
- Discrimination based on any characteristic
- Trolling or insulting comments
- Unwelcome sexual attention or advances
- Any other conduct which could reasonably be considered inappropriate

## Enforcement

Instances of abusive, harassing, or otherwise unacceptable behavior may be
reported by contacting the project maintainers. All reports will be reviewed
and investigated.
"@

$conductContent | Out-File -Encoding UTF8 "CODE_OF_CONDUCT.md"
Write-Host "✅ CODE_OF_CONDUCT.md created"

# Create issue templates
Write-Host "📝 Creating GitHub issue templates..."

$skillProposal = @"
---
name: Propose a New Skill
about: Suggest a new skill for this repository
title: '[SKILL] '
labels: enhancement
---

## Skill Idea

**Name**: (kebab-case, e.g., \`pdf-processor\`)

**Description**: What does this skill do? When would someone use it?

## Problem It Solves

What problem does this skill address?

## Example Use Cases

- Use case 1
- Use case 2

## Complexity Level

- [ ] Tier 1 (Very Simple - ~1-2 hours)
- [ ] Tier 2 (Simple - ~3-4 hours)
- [ ] Tier 3 (Moderate - ~5-8 hours)
- [ ] Tier 4 (Complex - ~8-16+ hours)

## Implementation

- [ ] I plan to implement this skill
- [ ] Looking for a co-maintainer
- [ ] Just an idea for now
"@

$skillProposal | Out-File -Encoding UTF8 ".github/ISSUE_TEMPLATE/skill-proposal.md"

$bugReport = @"
---
name: Bug Report
about: Report a problem with a skill
title: '[BUG] '
labels: bug
---

## Skill Name

Which skill has the issue?

## Steps to Reproduce

1. ...
2. ...

## Expected Behavior

What should happen?

## Actual Behavior

What actually happens?

## Environment

- AI Agent: (Claude / Cursor / Copilot / other)
- OS: Windows / Mac / Linux
- Agent Version: (if applicable)
"@

$bugReport | Out-File -Encoding UTF8 ".github/ISSUE_TEMPLATE/bug-report.md"

$featureRequest = @"
---
name: Feature Request
about: Request an improvement to a skill
title: '[FEATURE] '
labels: enhancement
---

## Skill Name

Which skill should be improved?

## Current Behavior

What does the skill currently do?

## Desired Behavior

What would you like it to do?

## Why Is This Useful?

Why is this improvement important?
"@

$featureRequest | Out-File -Encoding UTF8 ".github/ISSUE_TEMPLATE/feature-request.md"

Write-Host "✅ GitHub issue templates created"

# Create PR template
Write-Host "📝 Creating PR template..."
$prTemplate = @"
## Description

Describe your changes here.

## Type of Change

- [ ] New skill
- [ ] Skill improvement
- [ ] Documentation update
- [ ] Bug fix
- [ ] Maintenance

## Related Issue

Closes #(issue number, if applicable)

## How Has This Been Tested?

Describe how you tested this skill.

## Checklist

- [ ] SKILL.md frontmatter is complete (name, description)
- [ ] Examples are tested and working
- [ ] No secrets or hardcoded values
- [ ] Edge cases are documented
- [ ] README.md updated (if new skill)
- [ ] No merge conflicts
"@

$prTemplate | Out-File -Encoding UTF8 ".github/PULL_REQUEST_TEMPLATE.md"

Write-Host "✅ PR template created"

# Create template skill
Write-Host "📝 Creating skill template..."
$templateSkill = @"
---
name: template-skill
description: [Brief description - max 1024 chars. Explain WHAT it does and WHEN to use it.]
version: 1.0.0
author: $AuthorName
license: MIT
tags:
  - tag1
  - tag2
---

# [Skill Title]

## Overview

[2-3 sentence summary of what this skill does and why it's useful]

## When to Use This Skill

Use this skill when you need to:
- [Use case 1]
- [Use case 2]
- [Use case 3]

## Step-by-Step Instructions

### Step 1: [First Action]
[Detailed explanation and context]

### Step 2: [Second Action]
[Detailed explanation and context]

## Examples

### Example 1: [Common Use Case]
[Input and output example]

### Example 2: [Another Use Case]
[Input and output example]

## Common Patterns

[Describe 2-3 common implementation patterns or workflows]

## Edge Cases

- **Edge case 1**: [How to handle]
- **Edge case 2**: [How to handle]

## Troubleshooting

**Q: [Common question]**
A: [Answer]

## Related Skills

- [Link to related skill 1]
- [Link to related skill 2]

## Files in This Skill

- \`scripts/\` - Executable code (optional)
- \`references/\` - Additional documentation (optional)
- \`assets/\` - Templates and examples (optional)
"@

$templateSkill | Out-File -Encoding UTF8 "skills/_template/SKILL.md"

# Create template script placeholder
@"
# This directory contains scripts used by the skill.
# Examples: Python, PowerShell, JavaScript, Bash scripts
# Delete this file once you add actual scripts.
"@ | Out-File -Encoding UTF8 "skills/_template/scripts/README.md"

# Create template references placeholder
@"
# This directory contains reference documentation for the skill.
# Examples: Detailed guides, API references, additional examples
# Delete this file once you add actual references.
"@ | Out-File -Encoding UTF8 "skills/_template/references/README.md"

# Create template assets placeholder
@"
# This directory contains templates and resources used by the skill.
# Examples: Configuration templates, example files, sample data
# Delete this file once you add actual assets.
"@ | Out-File -Encoding UTF8 "skills/_template/assets/README.md"

Write-Host "✅ Skill template created"

# Create getting started docs
Write-Host "📝 Creating documentation..."
$gettingStarted = @"
# Getting Started

## Installation

Choose the installation method that matches your AI assistant:

### Cursor

\`\`\`powershell
# Option 1: Copy to Cursor skills directory
`$cursorSkillsPath = "`$env:APPDATA\.cursor\skills"
Copy-Item -Path "skills\category\skill-name" -Destination "`$cursorSkillsPath\skill-name" -Recurse

# Option 2: Restart Cursor and select skills to install
# Skills will be auto-discovered
\`\`\`

### GitHub Copilot

\`\`\`powershell
# Copy to Copilot skills directory
`$copilotPath = "`$env:APPDATA\GitHub\Copilot\skills"
Copy-Item -Path "skills\category\skill-name" -Destination "`$copilotPath\skill-name" -Recurse
\`\`\`

### Claude Code

1. Open Claude Code
2. Run: \`/plugin marketplace add https://github.com/yourusername/$RepoName\`
3. Select skills to install

## Using a Skill

### Method 1: Direct Reference

\`\`\`
Use the [skill-name] skill to [describe what you need]
\`\`\`

### Method 2: Implicit Trigger

The AI assistant will automatically use skills when relevant:

\`\`\`
Help me review this code for security issues
\`\`\`

(If you have a code-review skill, it will automatically activate)

## Creating Your Own Skill

See [docs/writing-skills.md](../writing-skills.md) for complete guide.

Quick version:
1. Copy \`skills/_template/\` 
2. Rename to your skill name
3. Edit SKILL.md
4. Test with your AI assistant
5. Submit PR if you want to share!

## Next Steps

- Review [docs/skill-anatomy.md](../skill-anatomy.md) to understand structure
- Look at example skills in \`skills/\` for patterns
- Check [CONTRIBUTING.md](../../CONTRIBUTING.md) if you want to contribute
"@

$gettingStarted | Out-File -Encoding UTF8 "docs/getting-started.md"

Write-Host "✅ Documentation created"

# Initialize git
Write-Host "🔧 Initializing git repository..."
git init | Out-Null
git config user.name "$AuthorName"

# Create .gitattributes for consistent line endings
@"
* text=auto
*.md text eol=lf
*.json text eol=lf
*.yaml text eol=lf
*.yml text eol=lf
*.ps1 text eol=crlf
*.py text eol=lf
"@ | Out-File -Encoding UTF8 ".gitattributes"

# Initial commit
git add . | Out-Null
git commit -m "chore: initial repository setup with template structure" | Out-Null
git branch -M main | Out-Null

Write-Host "✅ Git repository initialized"

Write-Host ""
Write-Host "✨ Repository successfully created!" -ForegroundColor Green
Write-Host ""
Write-Host "📋 Next steps:" -ForegroundColor Cyan
Write-Host "  1. Add remote: git remote add origin <your-github-url>"
Write-Host "  2. Create first skill by copying skills/_template/"
Write-Host "  3. Update README.md with your repo URL"
Write-Host "  4. Update docs/ with domain-specific information"
Write-Host "  5. git push -u origin main"
Write-Host ""
Write-Host "📚 Documentation:" -ForegroundColor Cyan
Write-Host "  - Getting started: docs/getting-started.md"
Write-Host "  - Contributing: CONTRIBUTING.md"
Write-Host "  - Skill template: skills/_template/SKILL.md"
Write-Host ""

Pop-Location
```

---

## Script 2: Validate Skills

**File: `scripts/validate.ps1`**

Usage: `.\scripts\validate.ps1`

```powershell
<#
.SYNOPSIS
Validates all SKILL.md files in the repository
.DESCRIPTION
Checks SKILL.md files for:
- Required YAML frontmatter fields (name, description)
- Proper YAML syntax
- Name follows kebab-case rules
- Description length constraints
#>

param(
    [switch]$Strict  # Fail on warnings
)

$errors = 0
$warnings = 0
$valid = 0

Write-Host "🔍 Validating skills..." -ForegroundColor Cyan

# Find all SKILL.md files
$skillFiles = Get-ChildItem -Path "skills" -Recurse -Filter "SKILL.md"

if ($skillFiles.Count -eq 0) {
    Write-Host "⚠️  No SKILL.md files found" -ForegroundColor Yellow
    return
}

foreach ($file in $skillFiles) {
    $relativePath = Resolve-Path -Path $file -Relative
    
    # Read file content
    $content = Get-Content -Path $file -Raw
    
    # Extract frontmatter
    if ($content -match "^---\n(.*?)\n---") {
        $frontmatter = $matches[1]
        $body = $content -replace "^---\n.*?\n---\n", ""
        
        # Parse YAML frontmatter (simple regex-based parsing)
        $nameMatch = $frontmatter | Select-String 'name:\s*(.+)'
        $descMatch = $frontmatter | Select-String 'description:\s*(.+)'
        
        $name = $nameMatch.Matches.Groups[1].Value.Trim() -replace '"', ""
        $description = $descMatch.Matches.Groups[1].Value.Trim() -replace '"', ""
        
        $fileErrors = 0
        
        # Validate name
        if ([string]::IsNullOrWhiteSpace($name)) {
            Write-Host "  ❌ $relativePath : Missing 'name' field" -ForegroundColor Red
            $fileErrors++
            $errors++
        } else {
            # Check kebab-case (lowercase letters, numbers, hyphens)
            if ($name -notmatch '^[a-z0-9]+(?:-[a-z0-9]+)*$') {
                Write-Host "  ⚠️  $relativePath : 'name' should be kebab-case: $name" -ForegroundColor Yellow
                $warnings++
            }
        }
        
        # Validate description
        if ([string]::IsNullOrWhiteSpace($description)) {
            Write-Host "  ❌ $relativePath : Missing 'description' field" -ForegroundColor Red
            $fileErrors++
            $errors++
        } else {
            if ($description.Length -gt 1024) {
                Write-Host "  ⚠️  $relativePath : 'description' exceeds 1024 characters" -ForegroundColor Yellow
                $warnings++
            }
        }
        
        # Check for body content
        if ([string]::IsNullOrWhiteSpace($body)) {
            Write-Host "  ⚠️  $relativePath : No instructions in body" -ForegroundColor Yellow
            $warnings++
        }
        
        if ($fileErrors -eq 0) {
            Write-Host "  ✅ $relativePath : Valid" -ForegroundColor Green
            $valid++
        }
    } else {
        Write-Host "  ❌ $relativePath : Missing YAML frontmatter (---)" -ForegroundColor Red
        $errors++
    }
}

Write-Host ""
Write-Host "📊 Validation Summary:" -ForegroundColor Cyan
Write-Host "  ✅ Valid: $valid"
Write-Host "  ⚠️  Warnings: $warnings"
Write-Host "  ❌ Errors: $errors"

if ($errors -gt 0) {
    Write-Host ""
    Write-Host "❌ Validation failed!" -ForegroundColor Red
    exit 1
}

if ($warnings -gt 0 -and $Strict) {
    Write-Host ""
    Write-Host "⚠️  Validation failed in strict mode" -ForegroundColor Yellow
    exit 1
}

Write-Host ""
Write-Host "✅ All skills valid!" -ForegroundColor Green
exit 0
```

---

## Script 3: List Skills

**File: `scripts/list-skills.ps1`**

Usage: `.\scripts\list-skills.ps1`

```powershell
<#
.SYNOPSIS
Lists all available skills in the repository
#>

param(
    [switch]$IncludeDescription,
    [switch]$Json
)

$skills = @()

# Find all SKILL.md files
$skillFiles = Get-ChildItem -Path "skills" -Recurse -Filter "SKILL.md" |
    Where-Object { $_.FullName -notmatch "_template" }

foreach ($file in $skillFiles) {
    # Extract skill directory name
    $skillPath = Split-Path -Path $file -Parent
    $skillName = Split-Path -Path $skillPath -Leaf
    $category = Split-Path -Path (Split-Path -Path $skillPath) -Leaf
    
    # Read frontmatter
    $content = Get-Content -Path $file -Raw
    if ($content -match "^---\n(.*?)\n---") {
        $frontmatter = $matches[1]
        
        $descMatch = $frontmatter | Select-String 'description:\s*(.+)'
        $description = $descMatch.Matches.Groups[1].Value.Trim() -replace '"', ""
        
        $skill = [PSCustomObject]@{
            Name        = $skillName
            Category    = $category
            Description = $description
            Path        = "skills/$category/$skillName"
        }
        
        $skills += $skill
    }
}

if ($Json) {
    $skills | ConvertTo-Json | Write-Host
} else {
    Write-Host "📚 Available Skills" -ForegroundColor Cyan
    Write-Host ""
    
    $groupedByCategory = $skills | Group-Object -Property Category
    
    foreach ($group in $groupedByCategory) {
        Write-Host "📂 $($group.Name)" -ForegroundColor Yellow
        foreach ($skill in $group.Group) {
            if ($IncludeDescription) {
                Write-Host "  • $($skill.Name)"
                Write-Host "    └─ $($skill.Description)"
            } else {
                Write-Host "  • $($skill.Name)"
            }
        }
        Write-Host ""
    }
    
    Write-Host "Total: $($skills.Count) skills" -ForegroundColor Green
}
```

---

## Script 4: Create New Skill

**File: `scripts/new-skill.ps1`**

Usage: `.\scripts\new-skill.ps1 -SkillName "my-awesome-skill" -Category "data" -Description "Does awesome things"`

```powershell
param(
    [Parameter(Mandatory=$true)]
    [string]$SkillName,
    
    [Parameter(Mandatory=$true)]
    [string]$Category,
    
    [Parameter(Mandatory=$true)]
    [string]$Description,
    
    [string]$Author = "Your Name",
    [string]$License = "MIT"
)

Write-Host "🚀 Creating new skill: $SkillName" -ForegroundColor Cyan

# Validate skill name (kebab-case)
if ($SkillName -notmatch '^[a-z0-9]+(?:-[a-z0-9]+)*$') {
    Write-Host "❌ Skill name must be kebab-case (lowercase, numbers, hyphens)" -ForegroundColor Red
    exit 1
}

# Create directory
$skillPath = "skills\$Category\$SkillName"

if (Test-Path $skillPath) {
    Write-Host "❌ Skill directory already exists: $skillPath" -ForegroundColor Red
    exit 1
}

New-Item -ItemType Directory -Path "$skillPath\scripts" -Force | Out-Null
New-Item -ItemType Directory -Path "$skillPath\references" -Force | Out-Null
New-Item -ItemType Directory -Path "$skillPath\assets" -Force | Out-Null

Write-Host "✅ Created directory: $skillPath"

# Create SKILL.md
$skillTemplate = @"
---
name: $SkillName
description: $Description
version: 1.0.0
author: $Author
license: $License
tags:
  - tag1
  - tag2
---

# $(Convert-TitleCase $SkillName)

## Overview

[2-3 sentence summary of what this skill does and why it's useful]

## When to Use This Skill

Use this skill when you need to:
- [Use case 1]
- [Use case 2]
- [Use case 3]

## Step-by-Step Instructions

### Step 1: [First Action]
[Detailed explanation and context]

### Step 2: [Second Action]
[Detailed explanation and context]

## Examples

### Example 1: [Common Use Case]
[Input and output example]

### Example 2: [Another Use Case]
[Input and output example]

## Common Patterns

[Describe 2-3 common implementation patterns or workflows]

## Edge Cases

- **Edge case 1**: [How to handle]
- **Edge case 2**: [How to handle]

## Troubleshooting

**Q: [Common question]**
A: [Answer]

## Related Skills

- [Link to related skill 1]
- [Link to related skill 2]

## Files in This Skill

- \`scripts/\` - Executable code (optional)
- \`references/\` - Additional documentation (optional)
- \`assets/\` - Templates and examples (optional)
"@

$skillTemplate | Out-File -Encoding UTF8 "$skillPath\SKILL.md"

# Create placeholder files
"# Add your scripts here" | Out-File -Encoding UTF8 "$skillPath\scripts\README.md"
"# Add reference documentation here" | Out-File -Encoding UTF8 "$skillPath\references\README.md"
"# Add templates and examples here" | Out-File -Encoding UTF8 "$skillPath\assets\README.md"

Write-Host "✅ Created SKILL.md with template"
Write-Host ""
Write-Host "📋 Next steps:" -ForegroundColor Cyan
Write-Host "  1. Edit: $skillPath\SKILL.md"
Write-Host "  2. Add examples and documentation"
Write-Host "  3. Add any scripts to: $skillPath\scripts\"
Write-Host "  4. Test with your AI assistant"
Write-Host "  5. Update main README.md to list the skill"
Write-Host "  6. Commit and push (or create PR)"
Write-Host ""

function Convert-TitleCase {
    param([string]$Text)
    $Text -replace '-', ' ' | Get-Culture | ForEach-Object {
        $_.TextInfo.ToTitleCase($_)
    }
}
```

---

## How to Use These Scripts

### Setup Initial Repository

```powershell
# Run from parent directory where you want the repo
.\initialize-repo.ps1 -RepoName "my-agent-skills" -AuthorName "Your Name"

# Go into the new directory
cd my-agent-skills
```

### Create a New Skill

```powershell
.\scripts\new-skill.ps1 `
  -SkillName "code-reviewer" `
  -Category "code-quality" `
  -Description "Reviews code for bugs, style issues, and security concerns"
```

### Validate All Skills

```powershell
# Basic validation
.\scripts\validate.ps1

# Strict mode (fail on warnings)
.\scripts\validate.ps1 -Strict
```

### List All Skills

```powershell
# Simple list
.\scripts\list-skills.ps1

# With descriptions
.\scripts\list-skills.ps1 -IncludeDescription

# JSON output
.\scripts\list-skills.ps1 -Json | ConvertFrom-Json
```

---

## Notes for Solo Development

These scripts are designed for efficient solo maintenance:

- **Validate** automatically with pre-commit hooks (optional setup)
- **Create** new skills in seconds, reducing friction
- **List** skills for documentation generation
- **Organize** by category as repository grows

Consider adding to `pre-commit.ps1`:

```powershell
# Pre-commit validation
Write-Host "Running skill validation..."
.\scripts\validate.ps1 -Strict

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Fix validation errors before committing"
    exit 1
}

Write-Host "✅ All validations passed"
```

---

Document Version: 1.0  
Last Updated: February 2, 2026
