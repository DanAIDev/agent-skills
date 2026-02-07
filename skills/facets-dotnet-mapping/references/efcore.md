# Facet with EF Core

## Packages

- Add `Facet.Extensions.EFCore` for projection helpers and async mapping support.
- Add `Facet.Extensions.EFCore.Mapping` when you need advanced EF Core mapping extensions.

## Projection workflow

1. Project queries using `SelectFacet<TFacet>()` on `IQueryable<T>`.
2. Use `ToFacetsAsync<TFacet>()` for async enumeration and materialization.
3. Prefer projection methods over manual `Include` unless you need non-projected navigation data.

## Navigation loading

Facet projections are designed to generate EF Core-friendly expressions that load required navigation properties when mapped through facets. This often removes the need for explicit `Include` calls, but validate SQL translation for complex cases.

## Guardrails

- Validate translation in integration tests (SQL Server provider can differ from in-memory behavior).
- Keep facets lean for query performance; avoid large, deeply nested graphs where possible.

Use `references/usage.md` for nested facets and projection API names.
