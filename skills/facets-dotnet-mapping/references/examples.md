# Facet examples index

Use these examples as starting points. Adjust namespaces, entity names, and DTO shapes for your solution.

## Example list

- FacetBasics.cs: basic include/exclude and projection starter.
- FacetNestedCollections.cs: nested facets for collection and reference navigation.
- FacetEfCoreProjection.cs: IQueryable projection with SelectFacet and ToFacetsAsync.
- FacetUpdateChanges.cs: update/patch with change tracking.
- FacetCustomMapperAsync.cs: async custom mapping with DI.
- FacetCircularRefs.cs: MaxDepth and PreserveReferences usage.
- FacetGenerateDtos.cs: GenerateDtos attribute for CRUD DTO generation.

## Usage guidance

- Start with FacetBasics.cs if you are introducing Facet in a new project.
- Use FacetEfCoreProjection.cs for EF Core queries to avoid manual projection logic.
- Use FacetCustomMapperAsync.cs when you need non-trivial mapping logic (currency conversion, lookups, etc.).
- Use FacetUpdateChanges.cs to replace patch-like Update methods from other mappers.
- Use FacetCircularRefs.cs when a graph can loop (self references, parent/child cycles).

Use `references/sources.md` to verify API names if Facet changes.
