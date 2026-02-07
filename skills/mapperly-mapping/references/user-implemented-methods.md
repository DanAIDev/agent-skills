# User-implemented mapping methods

Source: https://mapperly.riok.app/docs/configuration/user-implemented-methods/

Use when:
- Mapperly cannot generate a mapping (custom transforms, dictionaries, translations)
- Mapping to existing entities with [MappingTarget]

Rules from docs:
- User-implemented mapping method signature must match source/target types exactly (including nullability).
- AutoUserMappings is enabled by default; disable it to require [UserMapping].
- Use [UserMapping(Default = true)] when multiple mappings exist for a type-pair.

Note:
- MapProperty can use Use = nameof(CustomMethod) to bind property mapping to a custom method.
