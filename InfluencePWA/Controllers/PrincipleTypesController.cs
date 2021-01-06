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
    public class PrincipleTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PrincipleTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PrincipleTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrincipleType>>> GetPrincipleTypes()
        {
            return await _context.PrincipleTypes.ToListAsync();
        }

        // GET: api/PrincipleTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrincipleType>> GetPrincipleType(int id)
        {
            var principleType = await _context.PrincipleTypes.FindAsync(id);

            if (principleType == null)
            {
                return NotFound();
            }

            return principleType;
        }

        // PUT: api/PrincipleTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrincipleType(int id, PrincipleType principleType)
        {
            if (id != principleType.Id)
            {
                return BadRequest();
            }

            _context.Entry(principleType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrincipleTypeExists(id))
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

        // POST: api/PrincipleTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrincipleType>> PostPrincipleType(PrincipleType principleType)
        {
            _context.PrincipleTypes.Add(principleType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrincipleType", new { id = principleType.Id }, principleType);
        }

        // DELETE: api/PrincipleTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrincipleType(int id)
        {
            var principleType = await _context.PrincipleTypes.FindAsync(id);
            if (principleType == null)
            {
                return NotFound();
            }

            _context.PrincipleTypes.Remove(principleType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrincipleTypeExists(int id)
        {
            return _context.PrincipleTypes.Any(e => e.Id == id);
        }
    }
}
