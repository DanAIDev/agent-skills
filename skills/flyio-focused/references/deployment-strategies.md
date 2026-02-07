# Deployment Strategies

## Rolling

Use when uptime is required and health checks are reliable.

```bash
fly deploy --strategy rolling --app <app>
```

Behavior:

- replaces machines gradually
- respects health checks
- preferred for standard production deploys

## Immediate

Use for urgent cutover, emergency rollback, or accepted downtime windows.

```bash
fly deploy --strategy immediate --app <app>
```

Behavior:

- replaces machines quickly
- higher risk of brief service interruption
- useful when speed is more important than smooth transition

## Decision Rule

1. If downtime is not acceptable, use `rolling`.
2. If fastest replacement is required and downtime is acceptable, use `immediate`.
3. State chosen strategy and reason before running command.
