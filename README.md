# agent-skills

Public repository of reusable AI agent skills for personal use, team use, and community contributions.

## Goals

- Keep skills practical, focused, and easy to reuse.
- Make contribution flow lightweight for external contributors.
- Maintain compatibility with common agent ecosystems that use `SKILL.md`.

## Repository Structure

```text
skills/                  # Skills by category
docs/                    # Authoring and usage documentation
scripts/                 # Automation and validation scripts
.github/                 # Templates and CI workflows
```

## Quick Start

1. Clone the repo.
2. Create a new skill from template:
   `pwsh ./scripts/new-skill.ps1 -SkillName my-skill -Category general -Description "What it does and when to use it"`
3. Validate all skills:
   `pwsh ./scripts/validate.ps1`

## Current Skills

- `code-review/code-reviewer`
- `flyio-focused`

See `SKILLS.md` for the full index.

## Contributing

Contributions are welcome. Start with `CONTRIBUTING.md`.

## License

MIT. See `LICENSE`.
