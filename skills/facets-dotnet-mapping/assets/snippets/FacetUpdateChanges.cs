using System.Threading.Tasks;

public class UpdateUserRequest
{
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
}

[Facet(typeof(User), Include = new[] { "Email", "DisplayName" })]
public partial record UpdateUserDto;

public static class UserUpdater
{
    public static async Task ApplyUpdateAsync(MyDbContext db, User user, UpdateUserRequest request)
    {
        var dto = request.ToFacet<UpdateUserDto>();

        // Update and capture changes (pseudo-code; change object shape depends on Facet version)
        var changes = user.UpdateFromFacetWithChanges(dto, db);

        await db.SaveChangesAsync();
    }
}
