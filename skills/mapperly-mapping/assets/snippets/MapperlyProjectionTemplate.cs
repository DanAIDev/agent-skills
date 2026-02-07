// Minimal projection mapper
using Riok.Mapperly.Abstractions;

namespace YourProject.Infrastructure.Mappers;

[Mapper]
public static partial class ProjectionMapper
{
    public static partial IQueryable<TTarget> ProjectToTarget(this IQueryable<TSource> query);

    // Add MapProperty attributes here when names differ
    // [MapProperty(nameof(TSource.Foo), nameof(TTarget.Bar))]
    private static partial TTarget Map(TSource source);
}
