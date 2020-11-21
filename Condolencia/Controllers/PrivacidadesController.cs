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
    public class PrivacidadesController : ControllerBase
    {
        private readonly CondolenciaContext _context;

        public PrivacidadesController(CondolenciaContext context)
        {
            _context = context;
        }

        // GET: api/Privacidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Privacidade>>> GetPrivacidade()
        {
            return await _context.Privacidade.ToListAsync();
        }

        // GET: api/Privacidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Privacidade>> GetPrivacidade(int id)
        {
            var privacidade = await _context.Privacidade.FindAsync(id);

            if (privacidade == null)
            {
                return NotFound();
            }

            return privacidade;
        }

        // PUT: api/Privacidades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrivacidade(int id, Privacidade privacidade)
        {
            if (id != privacidade.Id)
            {
                return BadRequest();
            }

            _context.Entry(privacidade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrivacidadeExists(id))
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

        // POST: api/Privacidades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Privacidade>> PostPrivacidade(Privacidade privacidade)
        {
            _context.Privacidade.Add(privacidade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrivacidade", new { id = privacidade.Id }, privacidade);
        }

        // DELETE: api/Privacidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrivacidade(int id)
        {
            var privacidade = await _context.Privacidade.FindAsync(id);
            if (privacidade == null)
            {
                return NotFound();
            }

            _context.Privacidade.Remove(privacidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrivacidadeExists(int id)
        {
            return _context.Privacidade.Any(e => e.Id == id);
        }
    }
}
