using System.Threading.Tasks;

public partial record UserPublicDto;

// Custom async mapping with DI (example interface from Facet docs)
public sealed class UserPublicMapper : IFacetMapConfigurationAsyncInstance<User, UserPublicDto>
{
    private readonly IAvatarService _avatarService;

    public UserPublicMapper(IAvatarService avatarService)
    {
        _avatarService = avatarService;
    }

    public async Task ConfigureMapping(IFacetMapBuilder<User, UserPublicDto> builder)
    {
        var defaultMap = builder.BuildDefaultMap();
        var avatarUrl = await _avatarService.GetAvatarUrlAsync(builder.Source.Email);

        builder.Map(dto =>
        {
            var mapped = defaultMap(dto);
            mapped.AvatarUrl = avatarUrl;
            return mapped;
        });
    }
}

// Usage (pseudo-code):
// var mapper = serviceProvider.GetRequiredService<UserPublicMapper>();
// var dto = await user.ToFacetAsync<User, UserPublicDto>(mapper);
