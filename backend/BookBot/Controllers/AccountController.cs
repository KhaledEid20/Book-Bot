using BookBot.Extensions;
using BookBot.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        private readonly HelperClass _helper;
        public AccountController(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            ILogger<AccountController> _logger,
            RoleManager<IdentityRole> _roleManager,
            HelperClass _helper
            )
        {
            this._userManager = userManager;
            this._configuration = configuration;
            this._logger = _logger;
            this._helper = _helper;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody]UserRegDto user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser appuser = new()
                {
                    Email = user.email,
                    UserName = user.name
                };
                var result = await _userManager.CreateAsync(appuser, user.password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appuser, "AppUser");
                    var token = await _helper.GenerteToken(appuser);
                    return Ok(token);
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return BadRequest();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                IdentityUser? user = await _userManager.FindByNameAsync(login.UserName);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, login.Password)) {
                        var token = await _helper.GenerteToken(user);
                        return Ok(token);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Name is invalid");
                }
            }
            return BadRequest();
        }
        
    }
}
