#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Loyalty.Data;
using Loyalty.Data.Entities;

namespace Loyalty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DemoController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Demo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefreshToken>>> GetRefreshTokens()
        {
            return await _context.RefreshTokens.Include(x => x.User).ToListAsync();
        }

        // GET: api/Demo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefreshToken>> GetRefreshToken(int id)
        {
            var refreshToken = await _context.RefreshTokens.FindAsync(id);

            if (refreshToken == null)
            {
                return NotFound();
            }

            return refreshToken;
        }

        // PUT: api/Demo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefreshToken(int id, RefreshToken refreshToken)
        {
            if (id != refreshToken.Id)
            {
                return BadRequest();
            }

            _context.Entry(refreshToken).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefreshTokenExists(id))
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

        // POST: api/Demo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RefreshToken>> PostRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefreshToken", new { id = refreshToken.Id }, refreshToken);
        }

        // DELETE: api/Demo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefreshToken(int id)
        {
            var refreshToken = await _context.RefreshTokens.FindAsync(id);
            if (refreshToken == null)
            {
                return NotFound();
            }

            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RefreshTokenExists(int id)
        {
            return _context.RefreshTokens.Any(e => e.Id == id);
        }
    }
}
