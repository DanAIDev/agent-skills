# fly.toml Core

## Minimal Web Service

```toml
app = "my-app"
primary_region = "iad"

[build]
  dockerfile = "Dockerfile"

[http_service]
  internal_port = 8080
  force_https = true
  auto_stop_machines = true
  auto_start_machines = true
  min_machines_running = 1
```

## Key Fields

- `app`: globally unique app name.
- `primary_region`: preferred region for stateful workloads.
- `[http_service]`: public web service setup.
- `internal_port`: container listening port.

## Multi-Process Pattern

```toml
[processes]
  web = "npm run start"
  worker = "npm run worker"
```

Attach services to relevant processes only.
