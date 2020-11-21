using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Condolencia.Data;
using Condolencia.Models;

namespace Condolencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VitimasController : ControllerBase
    {
        private readonly CondolenciaContext _context;

        public VitimasController(CondolenciaContext context)
        {
            _context = context;
        }

        // GET: api/Vitimas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vitima>>> GetVitima()
        {
            return await _context.Vitima.ToListAsync();
        }

        // GET: api/Vitimas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vitima>> GetVitima(int id)
        {
            var vitima = await _context.Vitima.FindAsync(id);

            if (vitima == null)
            {
                return NotFound();
            }

            return vitima;
        }

        // PUT: api/Vitimas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVitima(int id, Vitima vitima)
        {
            if (id != vitima.Id)
            {
                return BadRequest();
            }

            _context.Entry(vitima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VitimaExists(id))
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

        // POST: api/Vitimas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vitima>> PostVitima(Vitima vitima)
        {
            _context.Vitima.Add(vitima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVitima", new { id = vitima.Id }, vitima);
        }

        // DELETE: api/Vitimas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVitima(int id)
        {
            var vitima = await _context.Vitima.FindAsync(id);
            if (vitima == null)
            {
                return NotFound();
            }

            _context.Vitima.Remove(vitima);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VitimaExists(int id)
        {
            return _context.Vitima.Any(e => e.Id == id);
        }
    }
}
