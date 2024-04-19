using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParentCompanyModel;
using CsvHelper.Configuration;
using System.Globalization;
using Microsoft.Extensions.Hosting;
using CsvHelper;
using SubsidiariesServer.Data;
using Microsoft.AspNetCore.Identity;


namespace SubsidiariesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(ParentCompanySourceContext db, IHostEnvironment environment,
        UserManager<SubsdiariesUsers> userManager) : ControllerBase
    {
        [HttpPost("Users")]
        public async Task<ActionResult> SeedUsers()
        {
            (string name, string email) = ("user1", "comp584@csun.edu");
            SubsdiariesUsers user = new()
            {
                UserName = name,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            if (await userManager.FindByNameAsync(name) is not null)
            {
                user.UserName = "user2";
            }
            _ = await userManager.CreateAsync(user, "P@ssw0rd!")
                ?? throw new InvalidOperationException();
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            await db.SaveChangesAsync();

            return Ok();
        }
    }
}
