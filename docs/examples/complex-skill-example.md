# Complex Skill Example

Use this shape when deterministic scripts and references are required.

```text
skills/data/csv-cleaner/
├── SKILL.md
├── scripts/clean_csv.py
└── references/schema-rules.md
```

`SKILL.md` should route:
- Basic requests to standard workflow.
- Edge cases to `references/schema-rules.md`.
- Repeatable cleanup to `scripts/clean_csv.py`.
