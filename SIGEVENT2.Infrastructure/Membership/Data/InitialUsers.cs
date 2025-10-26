using SIGEVENT2.Application.Membership.Mapping;

namespace SIGEVENT2.Infrastructure.Membership.Data;

public static class InitialUsers
{
    public static readonly UserLoginDTO SuperAdmin = new()
    {
        Email = "superadmin@framework.dev",
        Username = "superadmin",
        Password = "Admin123!",
        FirstName = "Super",
        LastName = "Admin"
    };
}