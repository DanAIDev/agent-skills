# Mapperly Migration Checklist

- Inventory AutoMapper profiles and ProjectTo usage
- Add Mapperly package to target project
- Create mapper class(es) under Infrastructure or layer that references source+target types
- Replace ProjectTo<T> with ProjectToXxx generated projections
- Replace Mapper.Map with Mapperly-generated methods or user-implemented mappings
- Remove AutoMapper package and DI registration for the migrated slice
- Run targeted tests
