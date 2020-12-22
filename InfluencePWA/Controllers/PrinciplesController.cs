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
        // GET: api/Principles/?pageIndex=0&pageSize=10
        // GET: api/Principles/?pageIndex=0&pageSize=10&sortColumn=name&sortOrder=asc
        // GET: api/Principles/?pageIndex=0&pageSize=10&sortColumn=name&sortOrder=asc&filterColumn=name&filterQuery=york
        [HttpGet]
        public async Task<ActionResult<ApiResult<PrincipleDTO>>> GetPrinciples(
                int pageIndex = 0,
                int pageSize = 10,
                string sortColumn = null,
                string sortOrder = null,
                string filterColumn = null,
                string filterQuery = null)
        {
            return await ApiResult<PrincipleDTO>.CreateAsync(
                    _context.Principles
                        .Select(c => new PrincipleDTO()
                        {
                            Id = c.Id,
                            Law = c.Law,
                            Title = c.Title,
                            Description = c.Description,
                            PrincipleTypeId = c.PrincipleType.Id,
                            PrincipleTypeName = c.PrincipleType.Name
                        }),
                    pageIndex,
                    pageSize,
                    sortColumn,
                    sortOrder,
                    filterColumn,
                    filterQuery);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrinciple(int id, Principle principle)
        {
            if (id != principle.Id)
            {
                return BadRequest();
            }

            //var sourcePrinciple = _context.Principles.Where(i => i.Id == principle.Id).FirstOrDefault();
            //if (sourcePrinciple == null) return BadRequest();
            //sourcePrinciple.Name = principle.Name;
            //sourcePrinciple.Lat = principle.Lat;
            //sourcePrinciple.Lon = principle.Lon;

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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Principle>> PostPrinciple(Principle principle)
        {
            _context.Principles.Add(principle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrinciple", new { id = principle.Id }, principle);
        }

        // DELETE: api/Principles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Principle>> DeletePrinciple(int id)
        {
            var principle = await _context.Principles.FindAsync(id);
            if (principle == null)
            {
                return NotFound();
            }

            _context.Principles.Remove(principle);
            await _context.SaveChangesAsync();

            return principle;
        }

        private bool PrincipleExists(int id)
        {
            return _context.Principles.Any(e => e.Id == id);
        }

        [HttpPost]
        [Route("IsDupePrinciple")]
        public bool IsDupePrinciple(Principle principle)
        {
            return _context.Principles.Any(
                e => e.Law == principle.Law
                && e.Title == principle.Title
                && e.Description == principle.Description
                && e.PrincipleTypeId == principle.PrincipleTypeId
                && e.Id != principle.Id);
        }
    }
}
