using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ParentCompanyModel;

namespace SubsidiariesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<SubsdiariesUsers> userManager, JwtHandler jwtHandler): ControllerBase
    {
        [HttpPost]
        public void Login()
        {

        }
    }
}
