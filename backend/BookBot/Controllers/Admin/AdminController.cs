using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookBot.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRole(string RoleName)
        {
            var exist = await _roleManager.RoleExistsAsync(RoleName);
            if (!exist)
            {
                var role = await _roleManager.CreateAsync(new IdentityRole(RoleName));
                if (role.Succeeded)
                {
                    return Ok(new
                    {
                        result = "The Role just Added"
                    });
                }
                return BadRequest(new
                {
                    result = "The Role Can't be Added"
                });
            }
            return BadRequest(new
            {
                result = "The Role already Exist"
            });
        }

        [HttpGet("[action]")]

        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddUserToRole(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new
                {
                    result = "The User does not exist"
                });
            }
            var roleExist = await _roleManager.RoleExistsAsync(role);
            if (!roleExist)
            {
                return BadRequest(new
                {
                    result = "The Role does not exist"
                });
            }
            var add = await _userManager.AddToRoleAsync(user, role);
            return Ok(new
            {
                result = $"The Role {role} added to the user {user.UserName}"
            });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUserRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(
                    new
                    {
                        error = "The user does not exist"
                    });
            }
            var user1 = await _userManager.GetRolesAsync(user);
            return Ok(new
            {
                Roles = user1
            });
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RemoveUserFromRole(string email, string Role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new
                {
                    error = "The User does not exist"
                });
            }
            var role = await _roleManager.RoleExistsAsync(Role);
            if (role == null)
            {
                return BadRequest(new
                {
                    error = "The User does not exist"
                });
            }
            var Remove = await _userManager.RemoveFromRoleAsync(user , Role);
            return Ok(new
            {
                result = "The Role Just Deleted"
            });
        }
    }
}