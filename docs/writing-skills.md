# Writing Skills

## Design Principles

- Solve one workflow clearly.
- Prefer concise, actionable instructions.
- Make triggers explicit in frontmatter `description`.
- Add examples that mirror real user prompts.

## Recommended Process

1. Define 3-5 realistic prompts the skill should handle.
2. Identify reusable resources (`scripts`, `references`, `assets`).
3. Draft `SKILL.md` from the template.
4. Validate with `pwsh ./scripts/validate.ps1 -Strict`.
5. Test with real tasks and iterate.

## Naming

- Use kebab-case.
- Keep names short and specific.
- Keep category aligned to discoverability.

## Anti-Patterns

- Broad multi-purpose skills.
- Long theory-heavy `SKILL.md` files.
- Missing edge-case behavior.
- Hidden assumptions about tools or environment.
