using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParentCompanyModel;
using SubsidiariesServer.DTO;
using Microsoft.AspNetCore.Authorization;

namespace SubsidiariesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubsidiariesController(ParentCompanySourceContext context) : ControllerBase
    {
        

        // GET: api/Subsidiaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subsidiary>>> GetSubsidiaries()
        {
            return await context.Subsidiaries.ToListAsync();
        }
        [Authorize]
        [HttpGet("GetRevenue")]
        public async Task<ActionResult<IEnumerable<ParentCompanyRevenue>>> GetRevenue()
        {
            IQueryable<ParentCompanyRevenue> x = from c in context.ParentCompanies
                                              select new ParentCompanyRevenue
                                              {
                                                  Name = c.Name,
                                                  ParentCompanyId = c.ParentCompanyId,
                                                  RevenueInBillions = (int)c.Subsidiaries.Sum(t => t.Revenue)
                                              };
            return await x.ToListAsync();
        }
        [HttpGet("GetRevenue2")]
        public async Task<ActionResult<IEnumerable<ParentCompanyRevenue>>> GetRevenue2()
        {
            IQueryable<ParentCompanyRevenue> x = context.ParentCompanies.Select(c =>
                                              new ParentCompanyRevenue
                                              {
                                                  Name = c.Name,
                                                  ParentCompanyId = c.ParentCompanyId,
                                                  RevenueInBillions = (int)c.Subsidiaries.Sum(t => t.Revenue)
                                              });
            return await x.ToListAsync();
        }
    }
}
