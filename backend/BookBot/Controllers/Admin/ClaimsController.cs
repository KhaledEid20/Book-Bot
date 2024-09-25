using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookBot.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public ClaimsController(UserManager<IdentityUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddClaimToUser(string email, string claimName , string claimValue)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null) {
                return BadRequest();
            }
            var claim = new Claim(claimName ,  claimValue);
            var result = await _userManager.AddClaimAsync(user , claim);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = $"The claim {claimName} to the user {user.UserName}"
                });
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddClaimToRole(string RoleName , string ClaimName , string ClaimValue)
        {
            var role = await _roleManager.FindByNameAsync(RoleName);
            if (role == null) {
                return BadRequest();
            }
            var claim = new Claim(RoleName , ClaimValue);
            var result = await _roleManager.AddClaimAsync(role, claim);
            if (result.Succeeded) {
                return Ok(
                    new
                    {
                        result = $"The claim {ClaimName} added to the role {RoleName}"
                    });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}