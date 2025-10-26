using Microsoft.AspNetCore.Authorization;
using SIGEVENT2.Core.Security.Auth;

namespace SIGEVENT2.Core.Membership.Security.Auth;

public class MembershipAuthorizationPolicies : IAuthorizationPolicyRegistrar
{
    public void RegisterPolicies(AuthorizationOptions options)
    {
        // Example: only SuperAdmins can manage users
        options.AddPolicy("SuperAdminRole", policy =>
            policy.RequireRole("SuperAdmin"));
    }
}