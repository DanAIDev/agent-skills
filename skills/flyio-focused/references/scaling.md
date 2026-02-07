# Scaling

## Horizontal

```bash
fly scale count 3 --app <app>
```

## Vertical

```bash
fly scale vm shared-cpu-2x --app <app>
fly scale memory 1024 --app <app>
```

## Regional

```bash
fly regions list --app <app>
fly regions set iad fra --app <app>
```

Scale based on observed CPU, memory, and request latency.
