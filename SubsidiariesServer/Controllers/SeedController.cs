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
using System.Diagnostics.Metrics;


namespace SubsidiariesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(ParentCompanySourceContext db, IHostEnvironment environment,
        UserManager<SubsdiariesUsers> userManager) : ControllerBase
    {
        private readonly string _pathName = Path.Combine(environment.ContentRootPath, "Data/subsidiaries.csv");

        [HttpPost("Subsdiary")]
        public async Task<ActionResult<Subsidiary>> SeedSubsidiary()
        {
            Dictionary<string,ParentCompany> parentcompanies = await db.ParentCompanies//.AsNoTracking()
            .ToDictionaryAsync(c => c.Name);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            int subsidiaryCount = 0;
            using (StreamReader reader = new(_pathName))
            using (CsvReader csv = new(reader, config))
            {
                IEnumerable<Subsidiaries>? records = csv.GetRecords<Subsidiaries>();
                foreach (Subsidiaries record in records)
                {
                    if (!parentcompanies.TryGetValue(record.ParentCompany, out ParentCompany? value))
                    {
                        Console.WriteLine($"Not found Parent Company for {record.Subsidiary}");
                        return NotFound(record);
                    }

                    if (!record.RevenueInBillions.HasValue)
                    {
                        Console.WriteLine($"Skipping {record.Subsidiary}");
                        continue;
                    }
                    Subsidiary subsidiary = new()
                    {
                        Name = record.Subsidiary,
                        Location = record.Location,
                        Revenue = (decimal)record.RevenueInBillions,
                        ParentCompanyId = value.ParentCompanyId
                    };
                    db.Subsidiaries.Add(subsidiary);
                    subsidiaryCount++;
                }
                await db.SaveChangesAsync();
            }
            return new JsonResult(subsidiaryCount);
        }

        [HttpPost("Parent Company")]
        public async Task<ActionResult<Subsidiary>> SeedParentCompany()
        {
            // create a lookup dictionary containing all the countries already existing 
            // into the Database (it will be empty on first run).
            Dictionary<string, ParentCompany> countriesByName = db.ParentCompanies
                .AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            List<Subsidiaries> records = csv.GetRecords<Subsidiaries>().ToList();
            foreach (Subsidiaries record in records)
            {
                if (countriesByName.ContainsKey(record.ParentCompany))
                {
                    continue;
                }

                ParentCompany parentcompany = new()
                {
                    Name = record.ParentCompany
                };
                await db.ParentCompanies.AddAsync(parentcompany);
                countriesByName.Add(record.ParentCompany, parentcompany);
            }

            await db.SaveChangesAsync();

            return new JsonResult(countriesByName.Count);
        }

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
