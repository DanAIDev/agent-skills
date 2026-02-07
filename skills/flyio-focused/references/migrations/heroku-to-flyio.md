# Heroku to Fly.io Migration

## Mapping

- Procfile web/worker -> Fly `processes`
- config vars -> `fly secrets`
- dyno scaling -> `fly scale count`

## Checklist

1. export config vars and map secrets.
2. create Dockerfile matching app runtime.
3. create `fly.toml` with web/worker processes.
4. deploy to staging app on Fly.
5. validate health checks and background jobs.
6. cut over DNS and monitor logs.
