# Facet usage patterns

## Basic facet definition

- Define a facet for each DTO shape with the `[Facet]` attribute.
- Use include/exclude lists to keep the DTO surface minimal.
- Prefer separate facets for different use cases (list views, detail views, admin views).

## Include/exclude

- Use `[Facet(Of = typeof(SourceType), Include = new[] { "PropA", "PropB" })]` to whitelist properties.
- Use `[Facet(Of = typeof(SourceType), Exclude = new[] { "PropX" })]` to blacklist properties.

## Nested facets

- Define nested facet types first (bottom-up).
- Use `NestedFacets = new[] { typeof(NestedFacet) }` on the parent facet.
- Nested facets can cover single references and collections.

## Mapping and projection APIs

Common extension methods to look for:

- `ToFacet<TFacet>()` for in-memory mapping.
- `ToFacet<TSource, TFacet>()` for faster mapping with explicit source type.
- `SelectFacet<TFacet>()` for query projection.
- `SelectFacets<TFacet>()` for projecting an enumerable sequence.
- `ToFacetsAsync<TFacet>()` for async projection materialization.

## Update and patch helpers

- Use `UpdateFromFacet` to apply changes from a facet to an entity.
- Use `UpdateFromFacetWithChanges` when you need change tracking information.

## Custom mapping

- Use Facet mapping configuration interfaces for custom sync/async logic.
- Prefer attribute-based transforms for simple renames or conditional mapping.
- Keep custom mapping deterministic to preserve compile-time guarantees.

Use `references/overview.md` for package names and `references/efcore.md` for EF Core-specific guidance.

## Example catalog

See `references/examples.md` and `assets/snippets/*` for copy-paste patterns covering nested facets, EF Core projection, update helpers, custom mapping, circular references, and CRUD DTO generation.
