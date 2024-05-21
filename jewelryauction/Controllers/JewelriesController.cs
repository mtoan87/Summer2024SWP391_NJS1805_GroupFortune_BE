using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JewelriesController : ControllerBase
    {
        private readonly JewelryAuctionContext _context;

        public JewelriesController(JewelryAuctionContext context)
        {
            _context = context;
        }

        // GET: api/Jewelries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jewelry>>> GetJewelries()
        {
          if (_context.Jewelries == null)
          {
              return NotFound();
          }
            return await _context.Jewelries.ToListAsync();
        }

        // GET: api/Jewelries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jewelry>> GetJewelry(int id)
        {
          if (_context.Jewelries == null)
          {
              return NotFound();
          }
            var jewelry = await _context.Jewelries.FindAsync(id);

            if (jewelry == null)
            {
                return NotFound();
            }

            return jewelry;
        }

        // PUT: api/Jewelries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJewelry(int id, Jewelry jewelry)
        {
            if (id != jewelry.JewelryId)
            {
                return BadRequest();
            }

            _context.Entry(jewelry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JewelryExists(id))
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

        // POST: api/Jewelries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Jewelry>> PostJewelry(Jewelry jewelry)
        {
          if (_context.Jewelries == null)
          {
              return Problem("Entity set 'JewelryAuctionContext.Jewelries'  is null.");
          }
            _context.Jewelries.Add(jewelry);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JewelryExists(jewelry.JewelryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJewelry", new { id = jewelry.JewelryId }, jewelry);
        }

        // DELETE: api/Jewelries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJewelry(int id)
        {
            if (_context.Jewelries == null)
            {
                return NotFound();
            }
            var jewelry = await _context.Jewelries.FindAsync(id);
            if (jewelry == null)
            {
                return NotFound();
            }

            _context.Jewelries.Remove(jewelry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JewelryExists(int id)
        {
            return (_context.Jewelries?.Any(e => e.JewelryId == id)).GetValueOrDefault();
        }
    }
}
