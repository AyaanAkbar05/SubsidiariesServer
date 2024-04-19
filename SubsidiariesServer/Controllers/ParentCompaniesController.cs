using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParentCompanyModel;

namespace SubsidiariesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentCompaniesController : ControllerBase
    {
        private readonly ParentCompanySourceContext _context;

        public ParentCompaniesController(ParentCompanySourceContext context)
        {
            _context = context;
        }

        // GET: api/ParentCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParentCompany>>> GetParentCompanies()
        {
            return await _context.ParentCompanies.ToListAsync();
        }

        // GET: api/ParentCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParentCompany>> GetParentCompany(int id)
        {
            var parentCompany = await _context.ParentCompanies.FindAsync(id);

            if (parentCompany == null)
            {
                return NotFound();
            }

            return parentCompany;
        }
        [HttpGet("ParentCompaniesSubsidiaries/{id}")]
        public async Task<ActionResult<IEnumerable<Subsidiary>>> GetCitiesByCountry(int id)
        {
            return await _context.Subsidiaries.Where(c => c.ParentCompanyId == id).ToListAsync();
        }
        // PUT: api/ParentCompanies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParentCompany(int id, ParentCompany parentCompany)
        {
            if (id != parentCompany.ParentCompanyId)
            {
                return BadRequest();
            }

            _context.Entry(parentCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentCompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ParentCompanies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParentCompany>> PostParentCompany(ParentCompany parentCompany)
        {
            _context.ParentCompanies.Add(parentCompany);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParentCompany", new { id = parentCompany.ParentCompanyId }, parentCompany);
        }

        // DELETE: api/ParentCompanies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParentCompany(int id)
        {
            var parentCompany = await _context.ParentCompanies.FindAsync(id);
            if (parentCompany == null)
            {
                return NotFound();
            }

            _context.ParentCompanies.Remove(parentCompany);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParentCompanyExists(int id)
        {
            return _context.ParentCompanies.Any(e => e.ParentCompanyId == id);
        }
    }
}
