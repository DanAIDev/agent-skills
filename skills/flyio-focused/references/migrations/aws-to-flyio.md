# AWS to Fly.io Migration

## Mapping

- containerized workload -> Fly app with `fly.toml`
- load balancer + TLS -> Fly proxy and automatic certs
- EC2/ECS runtime -> Fly Machines

## Checklist

1. containerize app with production Dockerfile.
2. create Fly app: `fly launch --no-deploy`.
3. set secrets: `fly secrets set ... --app <app>`.
4. configure health checks and ports in `fly.toml`.
5. deploy with `rolling` first cut.
6. verify and switch DNS.
