# Networking

## Public Web Service

Use `[http_service]` for HTTP/HTTPS exposure.

## Internal Service Discovery

Use private DNS for app-to-app communication:

- `<app>.internal`

Prefer private networking for backend-to-backend calls.

## Quick Checks

```bash
fly ips list --app <app>
fly status --app <app>
```
