---
name: flyio-focused
description: Deploy, operate, and troubleshoot applications on Fly.io using flyctl and fly.toml. Use when configuring services, choosing deployment strategy (rolling or immediate), managing secrets, scaling, or migrating an app to Fly.io.
---

# Fly.io Focused

## Objective

Execute Fly.io tasks with clear, low-noise runbooks and explicit deployment strategy decisions.

## Workflow

1. Confirm context:
   - app name
   - target environment (dev/staging/prod)
   - region
   - downtime tolerance
2. Validate baseline:
   - `flyctl auth whoami`
   - `flyctl status --app <app>`
   - `fly.toml` exists and matches service type
3. Select deployment strategy:
   - `rolling` for normal no-downtime changes
   - `immediate` for urgent cutover, rollback, or accepted downtime
4. Deploy with explicit command and strategy.
5. Verify health/logs/releases after deploy.
6. If issues occur, execute rollback and troubleshooting runbook.

## Deployment Strategy Rules

- Use `rolling` by default for production.
- Use `immediate` only when one of these is true:
  - urgent hotfix requires fastest replacement
  - rollback must happen immediately
  - maintenance window allows brief interruption
- Always explain strategy choice in one sentence before deployment.

## Response Format

1. Plan: one short runbook.
2. Commands: exact commands to run.
3. Verification: specific checks and expected signals.
4. Rollback: immediate rollback path.

## Reference Routing

Load only the file needed for the task:

- `references/index.md`: entrypoint and file map
- `references/commands-reference.md`: command catalog (syntax, flags, when to use, examples)
- `references/fly-toml-core.md`: core config and minimal patterns
- `references/deployment-strategies.md`: rolling vs immediate details
- `references/deploy-runbook.md`: preflight, deploy, verify, rollback
- `references/secrets.md`: secret lifecycle and env config
- `references/networking.md`: service exposure and internal comms
- `references/health-checks.md`: health checks and zero-downtime safety
- `references/scaling.md`: horizontal/vertical/regional scaling
- `references/troubleshooting.md`: incident diagnosis flow
- `references/migrations/*.md`: source-platform migration checklists

## Examples

Use files in `examples/` for concrete snippets and commands:

- `examples/minimal-web/`
- `examples/deploy-strategies/`

Do not inline long examples in this file.

## Command-First Rule

When user asks how to run Fly.io operations, consult `references/commands-reference.md` first and return:

1. command to run
2. key flags for that context
3. verification command
4. rollback/safe fallback when relevant
