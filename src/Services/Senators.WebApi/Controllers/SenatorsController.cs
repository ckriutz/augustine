using Augustine.Senators.WebApi.Contexts;
using Augustine.Senators.WebApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Augustine.Senators.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize]
    public class SenatorsController : ControllerBase
    {
        private readonly SenatorsContext _context;

        public SenatorsController(SenatorsContext context)
        {
            _context = context;
        }

        // GET: api/Senators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Senator>>> GetSenators()
        {
            if (_context.Senators.Count() == 0)
            {
                var senators = new List<Senator>
                {
                    new Senator
                    {
                        Name = "Kevin Kraus",
                        District = "Douglas County",
                        Party = "Independent",
                        PhoneNumber = "303-555-4444",
                        EmailAddress = "kkraus@microsoft.com",
                        TermStartDate = DateTime.Now.AddMonths(-6),
                        TermEndDate = DateTime.Now.AddMonths(6)
                    },
                    new Senator
                    {
                        Name = "Casey Kriutzfield",
                        District = "Augustine",
                        Party = "Independent",
                        PhoneNumber = "702-555-4444",
                        EmailAddress = "ckriutz@microsoft.com",
                        TermStartDate = DateTime.Now.AddMonths(-6),
                        TermEndDate = DateTime.Now.AddMonths(6)
                    }
                };

                await _context.Senators.AddRangeAsync(senators);
                await _context.SaveChangesAsync();
            }

            return await _context.Senators.ToListAsync();

        }

        // GET: api/Senators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Senator>> GetSenator(int id)
        {
            var senator = await _context.Senators.FindAsync(id);

            if (senator == null)
            {
                return NotFound();
            }

            return senator;
        }

        // PUT: api/Senators/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSenator(int id, Senator senator)
        {
            if (id != senator.Id)
            {
                return BadRequest();
            }

            _context.Entry(senator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SenatorExists(id))
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

        // POST: api/Senators
        [HttpPost]
        public async Task<ActionResult<Senator>> PostSenator(Senator senator)
        {
            _context.Senators.Add(senator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSenator", new { id = senator.Id }, senator);
        }

        // DELETE: api/Senators/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Senator>> DeleteSenator(int id)
        {
            var senator = await _context.Senators.FindAsync(id);
            if (senator == null)
            {
                return NotFound();
            }

            _context.Senators.Remove(senator);
            await _context.SaveChangesAsync();

            return senator;
        }

        private bool SenatorExists(int id)
        {
            return _context.Senators.Any(e => e.Id == id);
        }
    }
}
