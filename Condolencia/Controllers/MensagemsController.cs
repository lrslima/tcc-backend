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
    public class MensagemsController : ControllerBase
    {
        private readonly CondolenciaContext _context;

        public MensagemsController(CondolenciaContext context)
        {
            _context = context;
        }

        // GET: api/Mensagems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mensagem>>> GetMensagem()
        {
            return await _context.Mensagem.ToListAsync();
        }

        // GET: api/Mensagems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mensagem>> GetMensagem(int id)
        {
            var mensagem = await _context.Mensagem.FindAsync(id);

            if (mensagem == null)
            {
                return NotFound();
            }

            return mensagem;
        }

        // PUT: api/Mensagems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMensagem(int id, Mensagem mensagem)
        {
            if (id != mensagem.Id)
            {
                return BadRequest();
            }

            _context.Entry(mensagem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MensagemExists(id))
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

        // POST: api/Mensagems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mensagem>> PostMensagem(Mensagem mensagem)
        {
            _context.Mensagem.Add(mensagem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMensagem", new { id = mensagem.Id }, mensagem);
        }

        // DELETE: api/Mensagems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMensagem(int id)
        {
            var mensagem = await _context.Mensagem.FindAsync(id);
            if (mensagem == null)
            {
                return NotFound();
            }

            _context.Mensagem.Remove(mensagem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MensagemExists(int id)
        {
            return _context.Mensagem.Any(e => e.Id == id);
        }
    }
}
