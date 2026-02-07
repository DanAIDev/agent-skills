# Migration guide (AutoMapper/Mapster/Mapperly -> Facet)

## Pre-migration inventory

- List all mapping profiles, DTOs, custom resolvers, and after/before map hooks.
- Identify projection-heavy queries (EF Core, LINQ) and caching behavior.

## Migration steps

1. Create facets that mirror existing DTO shapes. Start with the most-used DTOs.
2. Replace mapping calls with Facet extensions (`ToFacet`, `SelectFacet`, `SelectFacets`, `ToFacetsAsync`).
3. Convert custom mapping logic to Facet configuration interfaces (sync or async) or attribute-based transforms.
4. Update tests to validate generated projections and DTO shapes.
5. Remove old mapper registrations and dependencies after validation.

## Common pitfalls

- Custom value resolvers may need explicit Facet mapping configuration.
- Projection and mapping method names may differ from previous libraries; centralize changes first.
- Large DTO graphs can be expensive; use nested facets and DTO variants to reduce payloads.

## Migration from manual mapping

- Keep DTOs only if they are public contracts; otherwise replace them with facets.
- Consolidate mapping logic into facets to avoid duplicate mapping layers.

Use `references/usage.md` for attribute patterns and `references/overview.md` for package names.
