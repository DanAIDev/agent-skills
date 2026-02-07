# Deployment Strategies Examples

## Rolling

```bash
fly deploy --strategy rolling --app <app>
```

Use for normal production deployments.

## Immediate

```bash
fly deploy --strategy immediate --app <app>
```

Use for urgent replacement or accepted downtime windows.
