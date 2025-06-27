using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore_identity_example.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RoleController(
        RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager
    )
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<IdentityResult>> CreateNewRole(string Name)
    {
        if (!string.IsNullOrEmpty(Name) && !await _roleManager.RoleExistsAsync(Name))
        {
            return await _roleManager.CreateAsync(new IdentityRole(Name));
        }
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult<IdentityResult>> DeleteRole(string id)
    {
        IdentityRole role = await _roleManager.FindByIdAsync(id);

        if (role != null)
        {
            return await _roleManager.DeleteAsync(role);
        }
        return NoContent();
    }

    [HttpGet("user-roles")]
    public async Task<IEnumerable<IdentityUser>> GetUserByRole(string roleName)
    {
        return await _userManager.GetUsersInRoleAsync(roleName);
    }

    [HttpDelete("user-roles")]
    public async Task<ActionResult<IdentityResult>> RemoveFromRole(string userId, string roleName)
    {
        IdentityUser user = await _userManager.FindByIdAsync(userId);

        if (await _userManager.IsInRoleAsync(user, roleName))
        {
            return await _userManager.RemoveFromRoleAsync(user, roleName);
        }

        return NoContent();
    }

    [HttpPost("user-roles")]
    public async Task<ActionResult<IdentityResult>> AssignRole(string userId, string roleName)
    {
        IdentityUser user = await _userManager.FindByIdAsync(userId);

        if (user != null && await _roleManager.RoleExistsAsync(roleName))
        {
            return await _userManager.AddToRoleAsync(user, roleName);
        }
        return NoContent();
    }
}
