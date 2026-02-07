---
name: mapperly-mapping
description: "Implement mapping patterns with Riok.Mapperly (source-generator), including EF Core queryable projections, user-implemented mappings, reference handling, and AutoMapper -> Mapperly migrations. Use when replacing AutoMapper profiles or building new Mapperly mappers and projections."
---

# Mapperly Mapping

## Overview

Implement consistent mapping with Mapperly (source-generated) for DTOs, domain models, and EF Core projections. Includes a migration path from AutoMapper.

## Workflow Decision Tree

1) If replacing AutoMapper profiles or `ProjectTo<T>` usage -> follow **AutoMapper -> Mapperly migration**.
2) If you need EF Core query projections -> follow **Queryable projections**.
3) If you need custom conversions (translations, dictionaries, nested transforms) -> follow **User-implemented mappings**.
4) If you need circular graph handling -> follow **Reference handling**.

## AutoMapper -> Mapperly migration

1. Inventory AutoMapper profiles and `ProjectTo<T>` usage.
2. Create mapper classes in `Infrastructure.*` (or the layer that references both source + target types).
3. Replace `ProjectTo<TDto>()` with generated projection methods (see assets).
4. Replace `Mapper.Map` calls with generated mapping methods or user-implemented mappings.
5. Remove AutoMapper package from the migrated project when the last mapping is replaced.

Use:
- `assets/snippets/StaticContentMapper.cs` for a projection + mapping template.
- `references/queryable-projections.md` for projection constraints.

## Queryable projections (EF Core)

1. Define a `[Mapper]` class with a `ProjectToXxx` method that takes `IQueryable<TSource>` and returns `IQueryable<TTarget>`.
2. Add a private partial mapping method with `[MapProperty]` to handle name mismatches and nested properties.
3. Use the generated projection in services: `query.ProjectToXxx().ToListAsync()`.

Notes:
- Projections have limitations (no reference handling, no factories, NRT disabled). See `references/queryable-projections.md`.
- If a `MapProperty` isn’t applied in projections, add an explicit mapping method (see `references/issues.md`).

## User-implemented mappings

Use user-implemented methods when:
- Mapping requires non-trivial transforms (e.g., translation dictionaries).
- You need to map to existing entities (`[MappingTarget]`).

Rules:
- The method signature must exactly match source/target types and nullability.
- Mapperly auto-discovers user-implemented mappings by default; use `[UserMapping]` to control discovery.

See `references/user-implemented-methods.md` for details and limitations.

## Reference handling

If you must map cyclic graphs:
- Set `[Mapper(UseReferenceHandling = true)]`.
- Ensure runtime assets are included (do not exclude runtime in the package reference).

See `references/reference-handling.md`.

## Output checklists

Before completing a migration slice:
- Ensure all `ProjectTo<T>` calls are replaced by Mapperly projections.
- Ensure all `Mapper.Map` usage in the slice is replaced by Mapperly-generated or user-implemented methods.
- Run targeted tests for the migrated slice.

## Assets

- `assets/snippets/MapperlyProjectionTemplate.cs` - minimal projection mapper.
- `assets/snippets/StaticContentMapper.cs` - example projection + custom mapping template.
- `assets/snippets/MigrationChecklist.md` - quick checklist.

## References

- `references/queryable-projections.md`
- `references/user-implemented-methods.md`
- `references/reference-handling.md`
- `references/issues.md`