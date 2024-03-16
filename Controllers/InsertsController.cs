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
    public class InsertsController : ControllerBase
    {
        private readonly ToolManagerContext _context;

        public InsertsController(ToolManagerContext context)
        {
            _context = context;
        }

        // GET: api/Inserts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insert>>> GetInserts()
        {
            return await _context.Inserts.ToListAsync();
        }

        // GET: api/Inserts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Insert>> GetInsert(string id)
        {
            var insert = await _context.Inserts.FindAsync(id);

            if (insert == null)
            {
                return NotFound();
            }

            return insert;
        }

        // PUT: api/Inserts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsert(string id, Insert insert)
        {
            if (id != insert.InsertId)
            {
                return BadRequest();
            }

            _context.Entry(insert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsertExists(id))
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

        // POST: api/Inserts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Insert>> PostInsert(Insert insert)
        {
            _context.Inserts.Add(insert);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InsertExists(insert.InsertId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInsert", new { id = insert.InsertId }, insert);
        }

        // DELETE: api/Inserts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsert(string id)
        {
            var insert = await _context.Inserts.FindAsync(id);
            if (insert == null)
            {
                return NotFound();
            }

            _context.Inserts.Remove(insert);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InsertExists(string id)
        {
            return _context.Inserts.Any(e => e.InsertId == id);
        }
    }
}
