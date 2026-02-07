using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// Query projection example with EF Core
public static class UserQueries
{
    public static Task<List<UserPublicDto>> GetActiveUsers(MyDbContext db)
    {
        return db.Users
            .Where(u => u.IsActive)
            .SelectFacet<UserPublicDto>()
            .ToListAsync();
    }
}
