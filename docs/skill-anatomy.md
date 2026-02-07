# Skill Anatomy

Each skill lives in:

```text
skills/<category>/<skill-name>/
├── SKILL.md          # Required
├── scripts/          # Optional deterministic helpers
├── references/       # Optional reference docs
└── assets/           # Optional output assets/templates
```

## SKILL.md Rules

- YAML frontmatter must include:
  - `name`
  - `description`
- Keep body concise and procedural.
- Put trigger intent in `description` (what + when).

## Resource Guidance

- `scripts/`: Use for repeatable logic.
- `references/`: Use for details that should not bloat `SKILL.md`.
- `assets/`: Use for templates or files used in generated output.
