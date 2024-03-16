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
    public class ToolAssembliesController : ControllerBase
    {
        private readonly ToolManagerContext _context;

        public ToolAssembliesController(ToolManagerContext context)
        {
            _context = context;
        }

        // GET: api/ToolAssemblies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToolAssembly>>> GetToolAssemblies()
        {
            return await _context.ToolAssemblies.ToListAsync();
        }

        // GET: api/ToolAssemblies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToolAssembly>> GetToolAssembly(string id)
        {
            var toolAssembly = await _context.ToolAssemblies.FindAsync(id);

            if (toolAssembly == null)
            {
                return NotFound();
            }

            return toolAssembly;
        }

        // PUT: api/ToolAssemblies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToolAssembly(string id, ToolAssembly toolAssembly)
        {
            if (id != toolAssembly.ToolAssemblyId)
            {
                return BadRequest();
            }

            _context.Entry(toolAssembly).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToolAssemblyExists(id))
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

        // POST: api/ToolAssemblies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToolAssembly>> PostToolAssembly(ToolAssembly toolAssembly)
        {
            _context.ToolAssemblies.Add(toolAssembly);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ToolAssemblyExists(toolAssembly.ToolAssemblyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetToolAssembly", new { id = toolAssembly.ToolAssemblyId }, toolAssembly);
        }

        // DELETE: api/ToolAssemblies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToolAssembly(string id)
        {
            var toolAssembly = await _context.ToolAssemblies.FindAsync(id);
            if (toolAssembly == null)
            {
                return NotFound();
            }

            _context.ToolAssemblies.Remove(toolAssembly);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToolAssemblyExists(string id)
        {
            return _context.ToolAssemblies.Any(e => e.ToolAssemblyId == id);
        }
    }
}
