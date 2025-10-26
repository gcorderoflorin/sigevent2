using Microsoft.AspNetCore.Identity;

namespace SIGEVENT2.Infrastructure.Identity;

public class User : IdentityUser<Guid> // Guid key
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}