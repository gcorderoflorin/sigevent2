using Microsoft.AspNetCore.Authorization;

namespace SIGEVENT2.Core.Security.Auth;

public interface IAuthorizationPolicyRegistrar
{
    void RegisterPolicies(AuthorizationOptions options);
}