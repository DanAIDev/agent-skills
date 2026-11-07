# OM pipeline bootstrap — checklist

Ordered, verifiable. Each line is done when its check passes.

- [ ] `gh --version` >= 2.82.1 (else install release binary to `~/.local/bin`)
- [ ] `jq --version` present
- [ ] `gh auth status` green (PAT: `gh auth login --with-token < file`)
- [ ] Push path works: `git ls-remote origin` + remote's key is write-enabled
      (SSH deploy key ≠ gh auth — both needed)
- [ ] `npx skills add open-mercato/skills --skill '*' --agent <your-agent>`
      → the agent's skills dir has 74 dirs + `skills-lock.json` at repo root
- [ ] agent permission config written (Exec/tool allows for gh, git, jq, node,
      bash, pytest-runner — Devin template: `config-template-devin.json`)
- [ ] `/om-setup-agent-pipeline` run:
      `.ai/agentic.config.json`, `.ai/trackers/github.md`,
      `.ai/browsers/agent-browser.md`, `SDLC.md`, `AGENTS.md`,
      `CODE_REVIEW.md`, `BACKWARD_COMPATIBILITY.md`, `.ai/*/` dirs + `.gitkeep`,
      ~30 labels on the repo
- [ ] Validation gate verified locally (e.g. `uv run --with pytest -- pytest
      tests/ -x -q`) before trusting it in config
- [ ] Fine-grained PAT → `get-pr-checks` patched (see
      `get-pr-checks-override.md`); verify: op returns `SUCCESS` for a known
      green PR head
- [ ] `.ai/tmp/` and `.ai/qa/` runtime files gitignored
- [ ] Setup committed and pushed (`master` — worktrees branch off
      `origin/master`, so an unpushed setup commit breaks runs)
- [ ] Dry run: CI-workflow PR via `/om-auto-create-pr` → draft PR visible
      early → gate green → labels normalized → review pass → ready → merged
- [ ] Optional: branch protection on base (require PR + check green);
      `automerge.requireIndependentReview` if two-identity review wanted

## Decision quick-reference

- Labels taxonomy: yes (pipeline mechanics depend on it)
- `qaGate`: true when anything user-facing or money-adjacent ships
- `autofix.label`: rename from `cez-autofix` to taste
- Docs: generate all four — they're derived, not copied; review before commit
- Identity: fine-grained PAT default; bot account / App only when strict
  review or full Checks access is genuinely needed (see SKILL.md §2)
