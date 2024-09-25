using BookBot.Controllers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookBot.Helper
{
    public class HelperClass
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public HelperClass(UserManager<IdentityUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IConfiguration configuration
            )
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<string> GenerteToken(IdentityUser user)
        {
            var User = _userManager.FindByNameAsync(user.UserName);
            if (User == null) {
                return "The User does not exist";
            }     
            var claims = await GetClaims(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // create the token
            var token = new JwtSecurityToken(
                    claims: claims,
                    issuer: _configuration["JWT:issuer"],
                    audience: _configuration["JWT:audience"],
                    expires: DateTime.Now.AddSeconds(30),
                    signingCredentials: sc
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        public async Task<List<Claim>> GetClaims(IdentityUser user)
        {

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var UserClaim = await _userManager.GetClaimsAsync(user);
            claims.AddRange(UserClaim);

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                var RoleClaims = await _roleManager.FindByNameAsync(role);
                if(RoleClaims != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(RoleClaims);
                    foreach(var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            return claims;
        }
    }
}
