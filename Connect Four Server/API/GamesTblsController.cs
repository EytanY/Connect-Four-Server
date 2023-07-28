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
    public class GamesTblsController : ControllerBase
    {
        private readonly Connect_Four_ServerContext _context;

        public GamesTblsController(Connect_Four_ServerContext context)
        {
            _context = context;
        }

        // GET: api/GamesTbls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GamesTbl>>> GetGamesTbl()
        {
          if (_context.GamesTbl == null)
          {
              return NotFound();
          }
            return await _context.GamesTbl.ToListAsync();
        }

        // GET: api/GamesTbls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GamesTbl>> GetGamesTbl(int id)
        {
          if (_context.GamesTbl == null)
          {
              return NotFound();
          }
            var gamesTbl = await _context.GamesTbl.FindAsync(id);

            if (gamesTbl == null)
            {
                return NotFound();
            }

            return gamesTbl;
        }

        // PUT: api/GamesTbls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGamesTbl(int id, GamesTbl gamesTbl)
        {
            if (id != gamesTbl.Id)
            {
                return BadRequest();
            }

            _context.Entry(gamesTbl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamesTblExists(id))
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

        // POST: api/GamesTbls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GamesTbl>> PostGamesTbl(GamesTbl gamesTbl)
        {
          if (_context.GamesTbl == null)
          {
              return Problem("Entity set 'Connect_Four_ServerContext.GamesTbl'  is null.");
          }
            _context.GamesTbl.Add(gamesTbl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGamesTbl", new { id = gamesTbl.Id }, gamesTbl);
        }

        // DELETE: api/GamesTbls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGamesTbl(int id)
        {
            if (_context.GamesTbl == null)
            {
                return NotFound();
            }
            var gamesTbl = await _context.GamesTbl.FindAsync(id);
            if (gamesTbl == null)
            {
                return NotFound();
            }

            _context.GamesTbl.Remove(gamesTbl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GamesTblExists(int id)
        {
            return (_context.GamesTbl?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
