using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SIGEVENT2.Core.Membership.Models;
using SIGEVENT2.Data;

namespace SIGEVENT2.Core.Membership.Data;

public class ModuleDataInitializer : IModuleDataInitializer
{
    private IServiceScope _serviceScope;
    private IDbContext _context;
    private RoleManager<IdentityRole<Guid>> _roleManager;
    private UserManager<User> _userManager;

    public async Task EnsureInitialData(IServiceScope serviceScope)
    {
        _serviceScope = serviceScope;

        _context = _serviceScope.ServiceProvider.GetService<IDbContext>();
        _roleManager = _serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole<Guid>>>();
        _userManager = _serviceScope.ServiceProvider.GetService<UserManager<User>>();

        await CreateRoles();
        await CreateInitialUsers();
    }

    private async Task CreateRoles()
    {
        var roles = InitialRoles.SystemRoles;
        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }

    private async Task CreateInitialUsers()
    {
        var superadmin = InitialUsers.SuperAdmin;
        var superadminUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == superadmin.Username);

        if (superadminUser == null)
        {
            superadminUser = new User
            {
                UserName = superadmin.Username,
                Email = superadmin.Email,
                EmailConfirmed = true,
                FirstName = superadmin.FirstName,
                LastName = superadmin.LastName
            };

            var result = await _userManager.CreateAsync(superadminUser, superadmin.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(superadminUser, InitialRoles.SuperAdmin);
            }
            else
            {
                Console.WriteLine($"⚠️ Failed to create superadmin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}