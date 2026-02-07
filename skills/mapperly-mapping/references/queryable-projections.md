# Queryable projections (EF Core)

Source: https://mapperly.riok.app/docs/configuration/queryable-projections/

Key points (from Mapperly docs):
- Mapperly supports IQueryable<T> projection methods on mapper classes.
- Projections use Expression trees and can be translated by EF Core.
- Only target members are selected in the generated query.

Projection limitations (from docs):
- Object factories not applied
- Constructors with unmatched optional parameters ignored
- ThrowOnPropertyMappingNullMismatch ignored
- AllowNullPropertyAssignment ignored
- Enum ByName mapping not supported
- Reference handling not supported
- Nullable reference types disabled
- Deep cloning not applied

Recommended pattern:
- Add a projection method returning IQueryable<TTarget>.
- Add a normal mapping method with MapProperty when names differ, and keep projection separate.
