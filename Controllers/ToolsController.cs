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
    public class ToolsController : ControllerBase
    {
        private readonly ToolManagerContext _context;

        public ToolsController(ToolManagerContext context)
        {
            _context = context;
        }

        // GET: api/Tools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tool>>> GetTools()
        {
            return await _context.Tools.ToListAsync();
        }

        // GET: api/Tools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tool>> GetTool(string id)
        {
            var tool = await _context.Tools.FindAsync(id);

            if (tool == null)
            {
                return NotFound();
            }

            return tool;
        }

        // PUT: api/Tools/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTool(string id, Tool tool)
        {
            if (id != tool.ToolId)
            {
                return BadRequest();
            }

            _context.Entry(tool).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToolExists(id))
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

        // POST: api/Tools
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tool>> PostTool(Tool tool)
        {
            _context.Tools.Add(tool);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ToolExists(tool.ToolId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTool", new { id = tool.ToolId }, tool);
        }

        // DELETE: api/Tools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTool(string id)
        {
            var tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                return NotFound();
            }

            _context.Tools.Remove(tool);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToolExists(string id)
        {
            return _context.Tools.Any(e => e.ToolId == id);
        }
    }
}
