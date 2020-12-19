using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfluencePWA.Data;
using InfluencePWA.Data.Models;

namespace InfluencePWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrinciplesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PrinciplesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Principles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Principle>>> GetPrinciples()
        {
            return await _context.Principles.ToListAsync();
        }

        // GET: api/Principles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Principle>> GetPrinciple(int id)
        {
            var principle = await _context.Principles.FindAsync(id);

            if (principle == null)
            {
                return NotFound();
            }

            return principle;
        }

        // PUT: api/Principles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrinciple(int id, Principle principle)
        {
            if (id != principle.Id)
            {
                return BadRequest();
            }

            _context.Entry(principle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrincipleExists(id))
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

        // POST: api/Principles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Principle>> PostPrinciple(Principle principle)
        {
            _context.Principles.Add(principle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrinciple", new { id = principle.Id }, principle);
        }

        // DELETE: api/Principles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrinciple(int id)
        {
            var principle = await _context.Principles.FindAsync(id);
            if (principle == null)
            {
                return NotFound();
            }

            _context.Principles.Remove(principle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrincipleExists(int id)
        {
            return _context.Principles.Any(e => e.Id == id);
        }
    }
}
