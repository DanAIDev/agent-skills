# Facet overview

Facet is a .NET source-generator mapping library that produces DTOs and mapping/projection code at build time. It focuses on compile-time validation, predictable performance, and projection-friendly mappings.

## Core packages

- Facet: core attributes, generator, and mapping extensions.
- Facet.Extensions: optional helpers for additional mapping scenarios.
- Facet.Extensions.EFCore: EF Core query helpers such as projection helpers and async mapping.
- Facet.Extensions.EFCore.Mapping: EF Core-specific mapping extensions for advanced cases.
- Facet.Mapping.Expressions and Facet.Mapping: optional packages for expression-based or custom mapping logic.

## Key capabilities

- Attribute-driven DTO generation with `[Facet]` and inclusion/exclusion controls.
- Renaming and conditional mapping via attributes (e.g., MapFrom, MapWhen).
- Nested facets for complex graphs, including collections.
- Projection helpers for LINQ and EF Core (server-side mappings).
- Mapping hooks for custom behavior and after/before mapping logic.
- Update helpers that can apply patch-like changes and optionally track updates.
- Reference handling and depth limits for circular object graphs.

## Compatibility notes

- Current documentation indicates support for modern .NET versions (such as .NET 8, 9, and 10). Confirm target framework support before integrating into older runtimes.
- The author notes that the library is evolving; always verify current API details against the latest docs and release notes.

Use `references/sources.md` for the primary documentation links.
