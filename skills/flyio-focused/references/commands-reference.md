# Fly.io Commands Reference

This file is for command lookups. Use it when the task asks "how do I do X on Fly.io?".

Notes:

- `fly` and `flyctl` are equivalent in common usage.
- Prefer passing `--app <app>` explicitly in scripted/production commands.
- Prefer explicit deployment strategy (`rolling` or `immediate`) instead of implicit defaults.

## Table of Contents

1. Authentication and context
2. App lifecycle
3. Deployments and releases
4. Status, logs, and health checks
5. Scaling and regions
6. Secrets and configuration
7. Machines, SSH, and debugging
8. Volumes and persistence
9. Postgres and Redis
10. Domains, TLS, and networking
11. Common command sequences

## 1) Authentication and Context

### `fly auth login`

Use when:

- logging in on a new machine
- refreshing session credentials

Syntax:

```bash
fly auth login
```

### `fly auth whoami`

Use when:

- confirming active account before production changes

Syntax:

```bash
fly auth whoami
```

### `fly doctor`

Use when:

- CLI/network/environment checks fail unexpectedly

Syntax:

```bash
fly doctor
```

## 2) App Lifecycle

### `fly launch`

Use when:

- initializing a new app from local project

Syntax:

```bash
fly launch
fly launch --no-deploy
```

Key flags:

- `--no-deploy`: create config/app without first deploy

### `fly apps list`

Use when:

- discovering available apps in account/org

Syntax:

```bash
fly apps list
```

### `fly apps create`

Use when:

- creating app record before custom/manual setup

Syntax:

```bash
fly apps create <app>
```

### `fly apps destroy`

Use when:

- removing an app permanently

Syntax:

```bash
fly apps destroy <app>
```

Safety:

- destructive operation; verify target app and backups first

## 3) Deployments and Releases

### `fly deploy`

Use when:

- shipping a new version

Syntax:

```bash
fly deploy --app <app>
```

High-value flags:

- `--strategy rolling`: safer no-downtime rollout
- `--strategy immediate`: fastest replacement, may cause brief downtime
- `--remote-only`: build remotely
- `--local-only`: build locally (debugging build issues)
- `--no-cache`: force clean rebuild
- `--image <image>`: deploy prebuilt image
- `--config <path>`: deploy with specific config file

Examples:

```bash
fly deploy --strategy rolling --app my-app
fly deploy --strategy immediate --app my-app
fly deploy --no-cache --strategy rolling --app my-app
fly deploy --image registry.fly.io/my-app:v42 --strategy immediate --app my-app
```

### `fly releases`

Use when:

- inspecting release history before/after deploy
- choosing rollback target

Syntax:

```bash
fly releases --app <app>
```

### Rollback via previous image

Use when:

- current release is unhealthy and fastest restore is needed

Syntax:

```bash
fly deploy --image registry.fly.io/<app>:<previous-tag> --strategy immediate --app <app>
```

## 4) Status, Logs, and Health Checks

### `fly status`

Use when:

- quick operational state check

Syntax:

```bash
fly status --app <app>
```

### `fly logs`

Use when:

- runtime diagnosis
- post-deploy verification

Syntax:

```bash
fly logs --app <app>
```

Key flags:

- `--region <region>`
- `--instance <id>`
- `--json`

Examples:

```bash
fly logs --app my-app
fly logs --region iad --app my-app
fly logs --json --app my-app
```

### `fly checks list`

Use when:

- validating health-check status during rollouts

Syntax:

```bash
fly checks list --app <app>
```

### `fly metrics`

Use when:

- checking performance/resource behavior after scaling or deploy

Syntax:

```bash
fly metrics --app <app>
```

## 5) Scaling and Regions

### `fly scale count`

Use when:

- changing machine count quickly

Syntax:

```bash
fly scale count <n> --app <app>
```

### `fly scale vm`

Use when:

- changing VM class for CPU profile/performance

Syntax:

```bash
fly scale vm <vm-class> --app <app>
```

Example:

```bash
fly scale vm shared-cpu-2x --app my-app
```

### `fly scale memory`

Use when:

- memory pressure or OOM events occur

Syntax:

```bash
fly scale memory <mb> --app <app>
```

Example:

```bash
fly scale memory 1024 --app my-app
```

### `fly autoscale set`

Use when:

- enabling baseline + burst bounds

Syntax:

```bash
fly autoscale set min=<n> max=<n> --app <app>
```

### Region Commands

Use when:

- expanding/reducing regional footprint

Syntax:

```bash
fly regions list --app <app>
fly regions add <region> --app <app>
fly regions remove <region> --app <app>
```

Examples:

```bash
fly regions add iad --app my-app
fly regions remove lax --app my-app
```

## 6) Secrets and Configuration

### `fly secrets set`

Use when:

- setting runtime secrets

Syntax:

```bash
fly secrets set KEY=value --app <app>
```

Examples:

```bash
fly secrets set DATABASE_URL=... --app my-app
fly secrets set API_KEY=... JWT_SECRET=... --app my-app
```

### `fly secrets list`

Use when:

- auditing secret names presence (not values)

Syntax:

```bash
fly secrets list --app <app>
```

### `fly secrets unset`

Use when:

- removing obsolete or rotated secret keys

Syntax:

```bash
fly secrets unset KEY --app <app>
```

### `fly secrets import`

Use when:

- loading multiple env entries from file

Syntax:

```bash
fly secrets import < .env
```

## 7) Machines, SSH, and Debugging

### `fly vm status`

Use when:

- inspecting machine-level state/resources

Syntax:

```bash
fly vm status --app <app>
```

### `fly ssh console`

Use when:

- interactive machine debugging

Syntax:

```bash
fly ssh console --app <app>
fly ssh console -C "ps aux" --app <app>
```

### `fly proxy`

Use when:

- local-to-remote port forwarding for private services

Syntax:

```bash
fly proxy <local_port>[:<remote_port>] --app <app>
```

Example:

```bash
fly proxy 5432 --app my-db
```

## 8) Volumes and Persistence

### `fly volumes create`

Use when:

- provisioning persistent storage in a region

Syntax:

```bash
fly volumes create <name> --region <region> --size <gb> --app <app>
```

### `fly volumes list`

Use when:

- inventory and capacity checks

Syntax:

```bash
fly volumes list --app <app>
```

### `fly volumes snapshots`

Use when:

- snapshot and backup workflows

Syntax:

```bash
fly volumes snapshots list --app <app>
```

## 9) Postgres and Redis

### `fly postgres create`

Use when:

- creating Fly Postgres cluster

Syntax:

```bash
fly postgres create --name <db-app> --region <region>
```

### `fly postgres attach`

Use when:

- linking app to Fly Postgres and setting env vars

Syntax:

```bash
fly postgres attach <db-app> --app <app>
```

### `fly postgres connect`

Use when:

- SQL shell or query execution

Syntax:

```bash
fly postgres connect --app <db-app>
fly postgres connect --app <db-app> -c "SELECT version();"
```

### `fly postgres status` / `fly postgres logs`

Use when:

- DB health and incident debugging

Syntax:

```bash
fly postgres status --app <db-app>
fly postgres logs --app <db-app>
```

### `fly postgres backup`

Use when:

- creating and inspecting backups

Syntax:

```bash
fly postgres backup create --app <db-app>
fly postgres backup list --app <db-app>
```

### `fly postgres restore`

Use when:

- point-in-time or snapshot recovery

Syntax:

```bash
fly postgres restore --app <db-app> --snapshot <snapshot-id>
fly postgres restore --app <db-app> --timestamp "<iso8601>"
```

### Redis (Fly managed)

Use when:

- provisioning Redis and attaching to app

Syntax:

```bash
fly redis create --name <redis-name> --region <region>
fly redis attach <redis-name> --app <app>
```

## 10) Domains, TLS, and Networking

### `fly certs add`

Use when:

- enabling custom domain TLS

Syntax:

```bash
fly certs add <domain> --app <app>
```

### `fly certs show`

Use when:

- checking cert provisioning status

Syntax:

```bash
fly certs show <domain> --app <app>
```

### `fly ips list`

Use when:

- inspecting assigned public IPs

Syntax:

```bash
fly ips list --app <app>
```

### `fly wireguard`

Use when:

- private network admin access

Syntax:

```bash
fly wireguard create
fly wireguard list
fly wireguard remove <peer-name>
```

## 11) Common Command Sequences

### Standard Production Deploy (Rolling)

```bash
fly auth whoami
fly status --app my-app
fly checks list --app my-app
fly deploy --strategy rolling --app my-app
fly status --app my-app
fly logs --app my-app
```

### Emergency Fast Deploy (Immediate)

```bash
fly status --app my-app
fly deploy --strategy immediate --app my-app
fly logs --app my-app
fly releases --app my-app
```

### Incident Rollback

```bash
fly releases --app my-app
fly deploy --image registry.fly.io/my-app:<previous-tag> --strategy immediate --app my-app
fly status --app my-app
fly logs --app my-app
```

### Scale-Up for Traffic Spike

```bash
fly status --app my-app
fly scale count 4 --app my-app
fly scale vm shared-cpu-2x --app my-app
fly metrics --app my-app
```
