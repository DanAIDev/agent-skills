# Kubernetes to Fly.io Migration

This file exists only for migration tasks. It should not drive normal Fly.io operations.

## Mapping

- Deployment replicas -> `min_machines_running` + `fly scale count`
- Service/Ingress -> `http_service` in `fly.toml`
- ConfigMap/Secret -> env values + `fly secrets`

## Checklist

1. flatten app config into `fly.toml` + secrets.
2. build one production image per service.
3. migrate one service at a time.
4. validate readiness via Fly checks.
5. use `rolling` first, `immediate` only for urgent cutovers.
