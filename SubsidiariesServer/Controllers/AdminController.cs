using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ParentCompanyModel;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using SubsidiariesServer.DTO;
using System.IdentityModel.Tokens.Jwt;

namespace SubsidiariesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<SubsdiariesUsers> userManager, JwtHandler jwtHandler) : ControllerBase
    {
        [HttpPost]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            SubsdiariesUsers? user = await userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null)
            {
                return Unauthorized("Bad username :P");
            }
            bool success = await userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!success)
            {
                return Unauthorized("Bad password :P");
            }
            JwtSecurityToken token = await jwtHandler.GetTokenAsync(user);
            string jwtstring = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new LoginResult
            {
                Success = true,
                Message = ":O",
                Token = jwtstring
            });
        }
    }
}
