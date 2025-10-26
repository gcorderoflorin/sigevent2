using Microsoft.AspNetCore.Authorization;

namespace SIGEVENT2.Infrastructure.Auth;

public interface IAuthorizationPolicyRegistrar
{
    void RegisterPolicies(AuthorizationOptions options);
}