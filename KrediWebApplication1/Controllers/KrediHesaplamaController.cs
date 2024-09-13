using KrediHesaplama; // KrediHesaplayici sınıfının bulunduğu namespace
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KrediWebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KrediHesaplamaController : ControllerBase
    {
        private readonly KrediHesaplayici _hesaplayici;

        public KrediHesaplamaController(KrediHesaplayici hesaplayici)
        {
            _hesaplayici = hesaplayici;
        }

        [HttpPost("hesapla")]
        public IActionResult Hesapla([FromBody] KrediHesaplamaDto krediDto)
        {
            try
            {
                // Kredi hesaplama işlemi
                var taksitler = _hesaplayici.TaksitleriHesapla(krediDto.KrediTutari, krediDto.TaksitSayisi);
                return Ok(taksitler);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class KrediHesaplamaDto
    {
        public decimal KrediTutari { get; set; }
        public int TaksitSayisi { get; set; }
    }
}
