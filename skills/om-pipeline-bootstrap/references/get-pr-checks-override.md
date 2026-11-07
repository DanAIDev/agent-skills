# get-pr-checks override for `.ai/trackers/github.md`

Apply when the automation authenticates with a **fine-grained PAT** — that token
class cannot call the Checks API (`gh pr checks`, `/check-runs` → 403; a
documented GitHub gap, not a missing grant). Replace the stock op:

```bash
gh pr checks {prNumber} --json name,state,link
```

with the Actions-runs derivation below — same `{name,state,link}` contract.
The unit becomes the workflow *run* (not the individual check job) and only
GitHub Actions checks are visible; third-party check providers are invisible
to this token class and must be disclosed as a gap when one is expected.
`get-required-checks` (branch protection) is unaffected.

```bash
#### get-pr-checks
`{prNumber}` → CI check runs with name, state, and link.

**Repo-local override:** fine-grained PATs cannot call the Checks API — this
op derives checks from the Actions runs API instead. Unit = workflow run;
Actions providers only.
```bash
HEAD_BRANCH=$(gh pr view {prNumber} --json headRefName --jq .headRefName)
HEAD_SHA=$(gh pr view {prNumber} --json headRefOid --jq .headRefOid)
gh run list --branch "$HEAD_BRANCH" --commit "$HEAD_SHA" --limit 50 \
  --json databaseId,workflowName,name,status,conclusion,url \
  --jq '
    group_by(.name) | map(sort_by(.databaseId) | last) | .[] |
    {name: (.workflowName // .name),
     state: (if .status != "completed" then "PENDING"
             elif .conclusion == "success" then "SUCCESS"
             elif (.conclusion == "failure" or .conclusion == "startup_failure") then "FAILURE"
             elif .conclusion == "timed_out" then "TIMED_OUT"
             elif .conclusion == "cancelled" then "CANCELLED"
             elif .conclusion == "skipped" then "SKIPPED"
             elif .conclusion == "action_required" then "ACTION_REQUIRED"
             else "NEUTRAL" end),
     link: .url}'
```
```

Notes:

- `gh run list` has a native `--commit <sha>` filter — use it; `gh`'s `--jq`
  does NOT accept `--arg`.
- Verified live on DanAIDev/stacker-agent: returns `tests: SUCCESS` for a
  green head (2026-11).
- If the credential is ever upgraded (GitHub App or classic PAT on a bot
  account), revert to the stock `gh pr checks` op — it's job-level and covers
  all check providers.
