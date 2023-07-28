using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Connect_Four_Server.Data;
using Connect_Four_Server.Models.Tables;

namespace Connect_Four_Server.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersTblsController : ControllerBase
    {
        private readonly Connect_Four_ServerContext _context;

        public PlayersTblsController(Connect_Four_ServerContext context)
        {
            _context = context;
        }

        // GET: api/PlayersTbls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayersTbl>>> GetPlayersTbl()
        {
          if (_context.PlayersTbl == null)
          {
              return NotFound();
          }
            return await _context.PlayersTbl.ToListAsync();
        }

        // GET: api/PlayersTbls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayersTbl>> GetPlayersTbl(int id)
        {
          if (_context.PlayersTbl == null)
          {
              return NotFound();
          }
            var playersTbl = await _context.PlayersTbl.FindAsync(id);

            if (playersTbl == null)
            {
                return NotFound();
            }

            return playersTbl;
        }

        // PUT: api/PlayersTbls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayersTbl(int id, PlayersTbl playersTbl)
        {
            if (id != playersTbl.Id)
            {
                return BadRequest();
            }

            _context.Entry(playersTbl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayersTblExists(id))
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

        // POST: api/PlayersTbls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayersTbl>> PostPlayersTbl(PlayersTbl playersTbl)
        {
          if (_context.PlayersTbl == null)
          {
              return Problem("Entity set 'Connect_Four_ServerContext.PlayersTbl'  is null.");
          }
            _context.PlayersTbl.Add(playersTbl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayersTbl", new { id = playersTbl.Id }, playersTbl);
        }

        // DELETE: api/PlayersTbls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayersTbl(int id)
        {
            if (_context.PlayersTbl == null)
            {
                return NotFound();
            }
            var playersTbl = await _context.PlayersTbl.FindAsync(id);
            if (playersTbl == null)
            {
                return NotFound();
            }

            _context.PlayersTbl.Remove(playersTbl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayersTblExists(int id)
        {
            return (_context.PlayersTbl?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
