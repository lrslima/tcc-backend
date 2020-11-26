using Condolencia.Interfaces;
using Condolencia.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Condolencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {

        [HttpGet("IdCondolencia")]
        public IActionResult GetQrCode(int idCondolencia)
        {
            var image = QRCodeService.GenerateByteArray($"https://avarc.vercel.app/condolencia/" + idCondolencia);
            return File(image, "image/jpeg");
        }
    }
}
