# Deploy Runbook

## Preflight

```bash
flyctl auth whoami
flyctl status --app <app>
flyctl checks list --app <app>
```

Verify:

- authenticated CLI
- correct target app/environment
- health checks defined and passing

## Deploy

Rolling:

```bash
fly deploy --strategy rolling --app <app>
```

Immediate:

```bash
fly deploy --strategy immediate --app <app>
```

## Verify

```bash
fly status --app <app>
fly logs --app <app>
fly releases --app <app>
```

Check:

- machines are healthy
- no startup crash loops
- latest release marked current

## Rollback

```bash
fly releases --app <app>
fly deploy --strategy immediate --image registry.fly.io/<app>:<previous-tag> --app <app>
```

Use immediate rollback for shortest recovery time.
