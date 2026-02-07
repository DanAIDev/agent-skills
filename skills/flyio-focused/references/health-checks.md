# Health Checks

## HTTP Check Example

```toml
[[http_service.checks]]
  interval = "10s"
  timeout = "2s"
  grace_period = "5s"
  method = "GET"
  path = "/health"
```

## Guidance

- implement lightweight health endpoint
- ensure dependencies needed for serving traffic are checked
- keep timeout low to detect failures early

Reliable checks are required for safe `rolling` deployments.
