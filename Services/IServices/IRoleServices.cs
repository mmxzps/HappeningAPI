using Microsoft.AspNetCore.Identity;

namespace EventVault.Services.IServices
{
    public interface IRoleServices
    {
        Task InitalizeRolesAsync();
        Task AssignRoleBasedOnUsernameAsync(IdentityUser user);
    }
}
