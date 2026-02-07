---
name: create-handoff
description: Write and store concise handoff documents in .ai/shared/handoffs to transfer work between sessions. Use when a user asks to create a handoff, end-of-session summary, or wants a /create_handoff-style command.
---

# Create Handoff

## Overview
Create a clear, concise handoff file that captures context, changes, and next steps so another agent can resume quickly.

## Workflow

### 1) Choose file path and name
- Save under `.ai/shared/handoffs` in the current repo.
- If a ticket exists, use a ticket folder; otherwise use `general`.
- Filename format:
  - With ticket: `YYYY-MM-DD_HH-mm-ss_ENG-XXXX_description.md`
  - Without ticket: `YYYY-MM-DD_HH-mm-ss_description.md`
- Use 24-hour time and local timezone. Keep `description` in kebab-case, ASCII.

### 2) Gather metadata
Capture:
- Current timestamp with timezone (ISO 8601)
- Git commit hash
- Git branch
- Repository name
- Author name (if available)

### 3) Write the handoff
Use the template below. Keep it thorough but concise. Prefer file path references (with optional line numbers) over large code blocks.

```markdown
---
date: [Current date and time with timezone in ISO format]
author: [Name or handle if available]
git_commit: [Current commit hash]
branch: [Current branch name]
repository: [Repository name]
topic: "[Short task name or feature]"
tags: [handoff, implementation, relevant-component-names]
status: in_progress|complete
last_updated: [Current date in YYYY-MM-DD format]
last_updated_by: [Author name]
type: handoff
---

# Handoff: [ENG-XXXX] [very concise description]

## Task(s)
- [Task 1] — [status: complete | in_progress | planned]
- [Task 2] — [status]

## Critical References
- [path/to/spec-or-plan.md]
- [path/to/architecture-note.md]

## Recent Changes
- [path/to/file.ext:line] — [what changed]
- [path/to/file.ext] — [what changed]

## Learnings
- [Key discovery or constraint] (file reference if relevant)
- [Pattern or gotcha]

## Artifacts
- [path/to/doc-or-plan.md]
- [path/to/branch-or-pr.md]

## Action Items & Next Steps
1. [Next action]
2. [Next action]

## Other Notes
- [Any extra context that does not fit above]
```

### 4) Confirm location and handoff command
After writing the file, respond with a short confirmation and the exact `/resume_handoff` command pointing at the new handoff path.

Example response:
```
Handoff created. Resume with:

/resume_handoff .ai/shared/handoffs/ENG-1234/2026-01-29_14-05-33_ENG-1234_fix-user-export.md
```

## Guidelines
- Be explicit about what is done vs. pending.
- List only the most critical references (2–3) to keep it scannable.
- Avoid large code blocks; prefer file path references.
- Include any risks, blockers, or assumptions in “Other Notes.”
