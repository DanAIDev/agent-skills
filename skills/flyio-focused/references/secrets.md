# Secrets

## Set Secrets

```bash
fly secrets set KEY=value --app <app>
```

Multiple:

```bash
fly secrets set API_KEY=... JWT_SECRET=... --app <app>
```

## List and Unset

```bash
fly secrets list --app <app>
fly secrets unset KEY --app <app>
```

## Rules

- keep secrets out of `fly.toml`
- use `fly secrets` for sensitive values
- rotate on schedule and after incidents
