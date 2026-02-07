---
name: facets-dotnet-mapping
description: Integrate the Facet .NET source-generator mapping library, define facets/DTOs and EF Core projections, and migrate from AutoMapper/Mapster/Mapperly or manual DTO mapping. Use when adding Facet to a project with no mapper or when replacing an existing mapper with Facet.
---

# Facets .NET Mapping

## Overview

Use this skill to add Facet to .NET solutions, define facets and projections, and migrate existing mapping code to generated facets.

## Workflow Decision Tree

Use the path that matches the task:

- Integrate Facet in a project with no mapper: follow "Integration Quick Start."
- Migrate from AutoMapper/Mapster/Mapperly or manual mapping: follow "Migration Workflow."
- Add EF Core projections or advanced mapping: follow "EF Core Workflow" and "Advanced Mapping."

Before implementation, read `references/overview.md`, `references/usage.md`, and `references/examples.md` to align on Facet concepts, APIs, and patterns.

## Integration Quick Start (No Existing Mapper)

1. Add the Facet packages you need (core plus optional Extensions/EF Core packages).
2. Define facets with `[Facet]` attributes for each DTO shape.
3. Replace manual DTO creation with generated facets and extension methods (`ToFacet`, `SelectFacet`, `SelectFacets`).
4. Build the solution to trigger source generation and address compile-time errors.

Use `references/overview.md` for package choices and `references/usage.md` for attribute patterns.

## Migration Workflow (AutoMapper/Mapster/Mapperly -> Facet)

1. Inventory existing mapping profiles, DTOs, and custom converters.
2. Create facets that match each DTO shape (use include/exclude and nested facets).
3. Replace mapping calls with Facet projections (`ToFacet`, `SelectFacet`, `SelectFacets`, `ToFacetsAsync`).
4. Implement custom mapping via Facet configuration interfaces where you used custom resolvers.
5. Remove old mapping registration and libraries after tests pass.

Use `references/migration.md` for a checklist and common pitfalls.

## EF Core Workflow

1. Add EF Core packages for Facet (`Facet.Extensions.EFCore` and optional mapping add-ons).
2. Use `SelectFacet<T>` in queries for server-side projection.
3. Use async helpers (`ToFacetsAsync`) for query execution.
4. Lean on Facet's generated projections to avoid manual includes unless necessary.

Read `references/efcore.md` for EF Core specifics and limitations.

## Advanced Mapping

1. Add mapping packages if you need custom logic or expression transforms.
2. Implement Facet mapping configuration interfaces for sync/async custom mapping.
3. Use attribute-based configuration (e.g., property rename/conditional mapping) where applicable.
4. Apply safeguards for circular references (depth limits and reference preservation).

Use `references/usage.md` for attribute hints and `references/overview.md` for feature inventory.

## Validation Checklist

1. Build and ensure generated facets compile cleanly.
2. Replace mapping tests with projection-focused tests.
3. Verify EF Core queries translate as expected in SQL.
4. Remove legacy mapper configs after verifying runtime behavior.

## Resources

- `references/overview.md` for packages, features, and terminology.
- `references/usage.md` for attribute patterns and mapping APIs.
- `references/efcore.md` for EF Core integration details.
- `references/migration.md` for migration steps and checks.
- `references/sources.md` for primary documentation links.
- `references/examples.md` for a catalog of examples and when to apply them.
- `assets/snippets/FacetBasics.cs` for a basic include/exclude and projection starter.
- `assets/snippets/FacetNestedCollections.cs` for nested facets and collections.
- `assets/snippets/FacetEfCoreProjection.cs` for query projection patterns.
- `assets/snippets/FacetUpdateChanges.cs` for update/patch with change tracking.
- `assets/snippets/FacetCustomMapperAsync.cs` for custom async mapping with DI.
- `assets/snippets/FacetCircularRefs.cs` for MaxDepth + PreserveReferences.
- `assets/snippets/FacetGenerateDtos.cs` for auto CRUD DTO generation.
