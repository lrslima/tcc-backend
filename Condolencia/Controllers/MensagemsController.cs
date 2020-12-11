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

        // GET: api/Mensagems por status/5
        [HttpGet("status")]
        public async Task<ActionResult<List<MensagemRegistrar>>> GetMensagemByStatus(string status)
        {
            try
            {
                var result = await _mensagemService.GetMensagemByStatus(status);

                if (result == null)
                {
                    return NotFound(new { Mensagem = "Nenhuma condolência encontrada." });
                }

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpGet("QRcode")]
        public async Task<ActionResult<List<MensagemRegistrar>>> GetQrCode()
        {
            try
            {
                var result = await _mensagemService.GetQrCode();

                if (result == null)
                {
                    return NotFound(new { Mensagem = "Nenhuma condolência encontrada." });
                }

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpGet("id")]
        [Produces("application/json")]
        public async Task<ActionResult<MensagemRegistrar>> GetMensagem(int id)
        {
            try
            {
                var mensagem = await _mensagemService.GetMensagem(id);

                if (mensagem == null)
                {
                    return NotFound(new { Mensagem = "Nenhuma condolência encontrada." });
                }

                return mensagem;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }

        }

        [HttpPut("id")]
        [Produces("application/json")]
        public async Task<ActionResult<MensagemRegistrar>> PutMensagem(int id, [FromBody] MensagemModeradaViewModel mensagemModerada)
        {
            try
            {
                if (id == 0 || id < 0)
                {
                    return BadRequest(new { Mensagem = "Id informado não é válido." });
                }

                mensagemModerada.IdMensagem = id;
                var result = await _mensagemService.AlterarStatus(mensagemModerada);

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<MensagemRegistrar>> PostMensagem([FromBody] MensagemRegistrar publicarMensagem)
        {
            try
            {
                var result = await _mensagemService.RegistrarMensagem(publicarMensagem);

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<MensagemRegistrar>>> GetMensagens()
        {
            try
            {
                var result = await _mensagemService.GetAllMensagens();

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        // DELETE: api/Mensagems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMensagem(int id)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }

        }

        private bool MensagemExists(int id)
        {
            return _context.Mensagem.Any(e => e.Id == id);
        }
    }
}
