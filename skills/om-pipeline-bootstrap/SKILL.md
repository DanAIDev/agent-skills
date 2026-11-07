---
name: om-pipeline-bootstrap
description: Bring up the open-mercato om-* skill pipeline (74 GitHub PR-pipeline skills) in any repo, for any coding agent — install, gh auth decision, pipeline setup, PAT limitations and their fixes, first dry run. Use when setting up om skills, installing the open-mercato pipeline, or bootstrapping an agent PR pipeline.
---

# OM Pipeline Bootstrap

Repeatable bring-up of the open-mercato skills pipeline
(github.com/open-mercato/skills — 74 `om-*` skills: plan → implement → review →
QA gate → merge, chained by tracker ops) in a repository, for any agent.
Distilled from a real first setup — every caveat below cost real time.
Follow in order; skip what already exists. Verifiable companion:
`references/checklist.md`.

## 0. Prerequisites

- `gh` **>= 2.82.1** — distro packages lag years (Ubuntu apt ships ~2.4; older
  clients silently drop label edits via the Projects-classic GraphQL
  deprecation). Install the release binary, don't rely on apt.
- `jq`, `git`. For Python repos without a managed env: `uv`
  (`curl -LsSf https://astral.sh/uv/install.sh | sh`) — `uv run --with pytest`
  runs suites without pip/venv (uv provisions CPython itself).
- `gh auth status` green **and** git push verified separately — an SSH deploy
  key covers git only, never the API (see §2).

## 1. Install the skills

```bash
npx skills add open-mercato/skills --skill '*' --agent <your-agent>
```

- skills.sh supports ~22 agents; most modern targets install to the open
  `.agents/skills/` standard dir. `--agent devin` is a real target →
  `.devin/skills/`. `--agent '*'` or interactive selection also works.
- Install **all 74** — uninvoked skills cost nothing (body loads only on
  invocation, references lazily), and pruning breaks by-name chaining.
- The installer writes `skills-lock.json` — commit it; restore elsewhere with
  `npx skills experimental_install`, update with `npx skills update -p`.

## 2. GitHub auth — decide BEFORE running setup

Everything flows through `gh` (issues, PRs, labels, comments, reviews, CI).
Know the credential-class limits — these are hard GitHub rules, not config:

| | Fine-grained PAT | Classic PAT (`repo`) | GitHub App |
|---|---|---|---|
| Issues/PRs/labels/comments | yes | yes | yes |
| **Checks API (check-runs)** | **NO — documented platform gap** | yes | yes (Checks: read) |
| Actions runs (`gh run list`) | yes (Actions: read) | yes | yes |
| Lifetime | up to ~1y | none | 1h — needs a minting wrapper |

- **Self-approval is impossible for ANY single identity** — the author can't
  approve their own PR. Accepted upstream design: reviews land as verdict
  comments; that IS the intended end-state. A second reviewer identity is only
  needed when `automerge.requireIndependentReview` is enabled.
- Default: **fine-grained PAT scoped to the repo** (Contents RW, Issues RW,
  Pull requests RW, Actions R, Commit statuses R; Checks if offered).
  `gh auth login --with-token` writes `~/.config/gh/hosts.yml` — never commit.
- SSH deploy key must be **write-enabled** (pipeline pushes branches).
- Upgrade path if full Checks access is needed: dedicated bot account +
  classic PAT (a collaborator-scoped account makes `repo` scope effectively
  narrow) or a GitHub App (correct but adds the token-minting machinery).
- Another product's GitHub App (e.g. the Devin app) is **not** usable as a
  credential — app tokens require the owner's private key.

## 3. Agent specifics

- **Devin CLI**: `.devin/skills/` or `.agents/skills/` both load; add Exec
  allows to `.devin/config.json` (template in
  `references/config-template-devin.json`) or the run drowns in permission
  prompts. `Exec(git)` includes push — required for autonomous PR work.
- **Claude Code / Codex / others**: skills land in the agent's own dir via
  `--agent`; each has its own permissions/hooks mechanism — grant `gh`, `git`,
  `node`, `bash`, `jq` equivalent to the above.
- om skills invoke each other **by name** — works anywhere the agent can
  invoke installed skills autonomously.
- `om-harness` (multi-model worker/reviewer council) ships Claude-format
  `PreToolUse` hooks; portable to any agent with an equivalent hook event but
  optional — the core pipeline needs none of it.

## 4. Run `/om-setup-agent-pipeline`

Interviews, then writes `.ai/agentic.config.json`, `.ai/trackers/<t>.md`,
`.ai/browsers/<p>.md`, `SDLC.md`, `AGENTS.md`, `CODE_REVIEW.md`,
`BACKWARD_COMPATIBILITY.md`, ~30 GitHub labels, `.ai/{runs,specs,analysis,
scripts,qa}/` dirs.

- Validation commands are auto-detected; for env-less Python repos,
  `uv run --with pytest -- pytest tests/ -x -q` is a good gate. **Verify the
  command locally first.**
- `autofix.label` defaults to upstream's `cez-autofix` — rename freely.
- `qaGate: true` → `needs-qa` PRs park until `qa-approved`; risk-low/medium
  can self-QA under the documented exception, risk-high waits for a human.
- No CI → `ci.maxWaitMinutes` inert; merge gates reduce to labels + reviews +
  local validation. Dogfood CI first (§6).

## 5. Fine-grained PAT → patch `get-pr-checks`

`.ai/trackers/<tracker>.md` is the committed override point. With a
fine-grained PAT, replace the `gh pr checks` op with the Actions-runs
derivation in `references/get-pr-checks-override.md` — same
`{name,state,link}` contract; caveat: run-level granularity, Actions-only.

## 6. Dogfood: CI workflow as the first run

`/om-auto-create-pr "add a GitHub Actions workflow running the validation
gate"` — a `pull_request` workflow self-tests on its own PR. Watch for:

- `astral-sh/setup-uv` publishes **semver tags only** — `@v10` fails to
  resolve; pin a full version.
- Add `permissions: contents: read`.
- Merge hits the self-approval refusal; proceeds on human authorization.

## 7. (Optional) Gate third-party public-repo writes

To let the agent file issues/PRs upstream but never unattended, use an
enforced approval boundary — not a prompt convention:

- **Credential:** second CLASSIC PAT, `public_repo` scope only (cannot touch
  private repos). Store at `~/.config/gh-pat-public` (chmod 600); never
  `gh auth login` it — anything in the keyring is one `gh auth token` away
  from exfiltration.
- **Wrapper** (`~/.local/bin/gh-external`): loads the token internally,
  allowlists mutating ops (`issue create|comment`, `pr create|comment|
  review`, `repo fork`), resolves the target repo (`--repo`/remote), and
  refuses own-account or non-public targets and interactive flags.
- **Approval gate (agent-specific):** the agent's permission system must
  prompt on every `gh-external` call. Devin: `permissions.ask:
  Exec(gh-external)` plus `deny` rules for the token file, the
  wrapper/config/hook files, and `gh auth token|login|logout|refresh|
  switch`. Claude Code: equivalent `permissions.ask` / `deny` +
  `PreToolUse` hook. Any agent: whatever mechanism makes one command prefix
  always require human confirmation.
- **Smuggle guard:** a pre-exec hook hard-blocks `gh-external` anywhere
  except as the command's first word — `bash -c`, env prefixes, `&&`
  chains, and pipes would otherwise skip a prefix-matched approval — plus
  token-store references, `GH_TOKEN=` injection, decoder/pipe-to-shell
  patterns, and shell writes to enforcement files. (Devin reference
  implementation: `PreToolUse` hook matching `^(exec|write_to_process)$`.)
- Optional second gate: with a flag file
  (`~/.config/devin/gh-external-desktop-gate`, itself protected from agent
  writes) the hook pops `zenity --question` when a display is reachable —
  a human click that holds even in no-prompt permission modes. Off by
  default; pointless on remote-administered/headless machines.
- Residuals: bypass/no-prompt modes can skip `ask` prompts (hook blocks
  still fire); prefix rules can't see inside `bash -c` strings (the hook
  covers it); `--body-file` content isn't shown at the prompt — prefer
  inline `--body`.

## Caveats

- Artifacts: plans in `.ai/runs/`, worktrees under `.ai/tmp/` (gitignore),
  QA state in `.ai/qa/` (setup gitignores it).
- Commit and push the setup BEFORE running pipeline skills — worktrees branch
  off `origin/<base>`; an unpushed setup commit breaks runs.
- Git commit identity is never auto-set — `git -c user.name=... -c
  user.email=...` per commit or repo config.
- `gh` multi-account: `gh auth token --user <login>` selects per call —
  only needed for two-identity review setups.
- Writing files via `<<EOF` (unquoted heredoc) executes backticks as command
  substitution — quote the delimiter or use an editor.
