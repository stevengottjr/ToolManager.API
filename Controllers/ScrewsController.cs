using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolManager.Data;
using ToolManager.Models;

namespace ToolManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrewsController : ControllerBase
    {
        private readonly ToolManagerContext _context;

        public ScrewsController(ToolManagerContext context)
        {
            _context = context;
        }

        // GET: api/Screws
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Screw>>> GetScrews()
        {
            return await _context.Screws.ToListAsync();
        }

        // GET: api/Screws/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Screw>> GetScrew(string id)
        {
            var screw = await _context.Screws.FindAsync(id);

            if (screw == null)
            {
                return NotFound();
            }

            return screw;
        }

        // PUT: api/Screws/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScrew(string id, Screw screw)
        {
            if (id != screw.ScrewId)
            {
                return BadRequest();
            }

            _context.Entry(screw).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScrewExists(id))
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

        // POST: api/Screws
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Screw>> PostScrew(Screw screw)
        {
            _context.Screws.Add(screw);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ScrewExists(screw.ScrewId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetScrew", new { id = screw.ScrewId }, screw);
        }

        // DELETE: api/Screws/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScrew(string id)
        {
            var screw = await _context.Screws.FindAsync(id);
            if (screw == null)
            {
                return NotFound();
            }

            _context.Screws.Remove(screw);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScrewExists(string id)
        {
            return _context.Screws.Any(e => e.ScrewId == id);
        }
    }
}
