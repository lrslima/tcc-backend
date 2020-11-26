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

        [HttpGet("id")]
        [Produces("application/json")]
        public async Task<ActionResult<MensagemRegistrar>> GetMensagem(int id)
        {
            var mensagem = await _mensagemService.GetMensagem(id);

            if (mensagem == null)
            {
                return NotFound();
            }

            return mensagem;
        }

        [HttpPut("id")]
        [Produces("application/json")]
        public async Task<ActionResult<MensagemRegistrar>> PutMensagem(int id, [FromBody] MensagemModeradaViewModel mensagemModerada)
        {
            if (id == 0 || id < 0)
            {
                return BadRequest();
            }

            mensagemModerada.IdMensagem = id;
            var result = await _mensagemService.AlterarStatus(mensagemModerada);

            return result;
        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Mensagem>> PostMensagem([FromBody] MensagemRegistrar publicarMensagem)
        {
            var result = await _mensagemService.RegistrarMensagem(publicarMensagem);

            return result;
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
