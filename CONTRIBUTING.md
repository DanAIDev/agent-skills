# Contributing

## Scope

Contributions should add or improve reusable agent skills.

## Add a New Skill

1. Create a branch from `main`.
2. Run:
   `pwsh ./scripts/new-skill.ps1 -SkillName <kebab-name> -Category <category> -Description "<what + when>"`
3. Implement `SKILL.md` and optional resources (`scripts/`, `references/`, `assets/`).
4. Run validation:
   `pwsh ./scripts/validate.ps1 -Strict`
5. Update `SKILLS.md`.
6. Open a pull request.

## Skill Standards

- Single clear purpose.
- `SKILL.md` frontmatter must include:
  - `name`
  - `description` (must include when to use the skill)
- Prefer concise instructions and concrete examples.
- Include edge cases and failure handling guidance.

## Pull Request Checklist

- Skill works for at least one real use case.
- Validation passes.
- Documentation updated (`SKILLS.md`, docs if needed).
- No secrets or credentials committed.

## Code of Conduct

By participating, you agree to `CODE_OF_CONDUCT.md`.
