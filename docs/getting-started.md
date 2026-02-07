# Getting Started

## Prerequisites

- PowerShell 7+
- Git

## Clone

```powershell
git clone https://github.com/<your-user>/agent-skills.git
cd agent-skills
```

## Create a New Skill

```powershell
pwsh ./scripts/new-skill.ps1 `
  -SkillName "my-skill" `
  -Category "general" `
  -Description "Explain what it does and when to use it"
```

## Validate

```powershell
pwsh ./scripts/validate.ps1 -Strict
```

## List Skills

```powershell
pwsh ./scripts/list-skills.ps1 -IncludeDescription
```

## Next

- Read `docs/writing-skills.md`
- Start from `skills/_template/SKILL.md`
