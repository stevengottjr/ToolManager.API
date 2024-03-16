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
    public class MachineToolsController : ControllerBase
    {
        private readonly ToolManagerContext _context;

        public MachineToolsController(ToolManagerContext context)
        {
            _context = context;
        }

        // GET: api/MachineTools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineTool>>> GetMachineTools()
        {
            return await _context.MachineTools.ToListAsync();
        }

        // GET: api/MachineTools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MachineTool>> GetMachineTool(string id)
        {
            var machineTool = await _context.MachineTools.FindAsync(id);

            if (machineTool == null)
            {
                return NotFound();
            }

            return machineTool;
        }

        // PUT: api/MachineTools/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMachineTool(string id, MachineTool machineTool)
        {
            if (id != machineTool.MachineToolId)
            {
                return BadRequest();
            }

            _context.Entry(machineTool).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MachineToolExists(id))
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

        // POST: api/MachineTools
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MachineTool>> PostMachineTool(MachineTool machineTool)
        {
            _context.MachineTools.Add(machineTool);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MachineToolExists(machineTool.MachineToolId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMachineTool", new { id = machineTool.MachineToolId }, machineTool);
        }

        // DELETE: api/MachineTools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachineTool(string id)
        {
            var machineTool = await _context.MachineTools.FindAsync(id);
            if (machineTool == null)
            {
                return NotFound();
            }

            _context.MachineTools.Remove(machineTool);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MachineToolExists(string id)
        {
            return _context.MachineTools.Any(e => e.MachineToolId == id);
        }
    }
}
