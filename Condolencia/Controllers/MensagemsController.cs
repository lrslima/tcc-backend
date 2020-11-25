using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Condolencia.Data;
using Condolencia.Models;
using Condolencia.DTOs;
using Condolencia.Services;
using Condolencia.Interfaces;

namespace Condolencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemsController : ControllerBase
    {
        private readonly CondolenciaContext _context;
        private readonly IMensagemService _mensagemService;
        public MensagemsController(CondolenciaContext context, IMensagemService mensagemService)
        {
            _context = context;
            _mensagemService = mensagemService;
        }

        // GET: api/Mensagems/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Mensagem>> GetMensagem(int id)
        //{
        //    var mensagem = await _context.Mensagem.FindAsync(id);
        //
        //    if (mensagem == null)
        //    {
        //        return NotFound();
        //    }

        //    return mensagem;
        //}
        // GET: api/Mensagems/5
        
        [HttpGet("{id}")]
        public async Task<ActionResult<List<MensagemRegistrar>>> GetMensagem(int id)
        {
            var result = await _mensagemService.GetMensagem(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
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


        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<List<MensagemRegistrar>>> PostMensagem([FromBody] MensagemRegistrar publicarMensagem)
        {
            var result = await _mensagemService.RegistrarMensagem(publicarMensagem);
            var modelJson = await _mensagemService.GetMensagem(result.Id);
            return modelJson;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<MensagemRegistrar>>> GetMensagens()
        {
            var result = await _mensagemService.GetAllMensagens();

            return result;
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
