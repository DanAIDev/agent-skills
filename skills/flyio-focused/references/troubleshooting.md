# Troubleshooting

## Triage Order

1. release status
2. machine health
3. logs
4. config mismatch
5. network reachability

## Commands

```bash
fly releases --app <app>
fly status --app <app>
fly logs --app <app>
fly checks list --app <app>
```

## Common Failures

- app not listening on `internal_port`
- missing secrets
- health check path mismatch
- image boot failure

If production is impacted, rollback first and debug second.
