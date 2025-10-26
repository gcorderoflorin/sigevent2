using Microsoft.AspNetCore.Authorization;
using SIGEVENT2.Infrastructure.Auth;

namespace SIGEVENT2.Infrastructure.Membership.Auth;

public class MembershipAuthorizationPolicies : IAuthorizationPolicyRegistrar
{
    public void RegisterPolicies(AuthorizationOptions options)
    {
        // Example: only SuperAdmins can manage users
        options.AddPolicy("SuperAdminRole", policy =>
            policy.RequireRole("SuperAdmin"));
    }
}