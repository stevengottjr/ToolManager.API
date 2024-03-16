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
    public class HoldersController : ControllerBase
    {
        private readonly ToolManagerContext _context;

        public HoldersController(ToolManagerContext context)
        {
            _context = context;
        }

        // GET: api/Holders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Holder>>> GetHolders()
        {
            return await _context.Holders.ToListAsync();
        }

        // GET: api/Holders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Holder>> GetHolder(string id)
        {
            var holder = await _context.Holders.FindAsync(id);

            if (holder == null)
            {
                return NotFound();
            }

            return holder;
        }

        // PUT: api/Holders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHolder(string id, Holder holder)
        {
            if (id != holder.HolderId)
            {
                return BadRequest();
            }

            _context.Entry(holder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HolderExists(id))
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

        // POST: api/Holders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Holder>> PostHolder(Holder holder)
        {
            _context.Holders.Add(holder);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HolderExists(holder.HolderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHolder", new { id = holder.HolderId }, holder);
        }

        // DELETE: api/Holders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHolder(string id)
        {
            var holder = await _context.Holders.FindAsync(id);
            if (holder == null)
            {
                return NotFound();
            }

            _context.Holders.Remove(holder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HolderExists(string id)
        {
            return _context.Holders.Any(e => e.HolderId == id);
        }
    }
}
