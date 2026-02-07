# SKILL.md Specification (Project Baseline)

## Required Frontmatter

```yaml
---
name: my-skill
description: Explain what this skill does and when to use it.
---
```

## Validation Rules

- `name`:
  - max 64 characters
  - lowercase letters, numbers, hyphens only
  - no leading/trailing hyphen
  - no consecutive hyphens
- `description`:
  - required
  - max 1024 characters
  - must include trigger context (when to use)

## Body Recommendations

- Start with short objective.
- Provide deterministic steps.
- Include examples and edge cases.
- Link to `references/` for detailed material.
