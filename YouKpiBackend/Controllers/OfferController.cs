using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;
using YouKpiBackend.ModelsEntity;

namespace YouKpiBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OfferController : Controller
    {
        private readonly YoukpiContext _ctx;
        public OfferController(YoukpiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _ctx.Offer.Include(o=>o.OfferLines).ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Offer entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var res = _ctx.Offer.Add(entity);
                _ctx.SaveChanges();

                return Created("", entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[HttpPut("[action]")]
        //public async Task<IActionResult> Update([FromBody] Produkty entity)
        //{
        //    if (entity == null)
        //    {
        //        return BadRequest("Bad model");
        //    }
        //    try
        //    {
        //        var product = _ctx.Produkty.Include(p => p.ProduktCzesci).FirstOrDefault(p => p.Id == entity.Id);
        //        product.TypWyrobuId = entity.TypWyrobuId;
        //        product.NumerRysNorma = entity.NumerRysNorma;
        //        product.Dn = entity.Dn;
        //        product.Pn = entity.Pn;
        //        product.Ansi = entity.Ansi;
        //        product.Wersja = entity.Wersja;
        //        product.Uszczelnienie = entity.Uszczelnienie;

        //        product.ProduktCzesci.ToList().ForEach(p =>
        //        {
        //            _ctx.Entry(p).State = EntityState.Deleted;
        //        });
        //        entity.ProduktCzesci.ToList().ForEach(part =>
        //        {
        //            part.ProduktyId = product.Id;
        //            product.ProduktCzesci.Add(part);
        //        });
        //        _ctx.SaveChanges();

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[HttpDelete("[action]")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    try
        //    {
        //        var product = _ctx.Produkty.Include(p => p.ProduktCzesci).FirstOrDefault(p => p.Id == id);
        //        if (product != null)
        //        {
        //            product.ProduktCzesci.ToList().ForEach(p =>
        //            {
        //                _ctx.Entry(p).State = EntityState.Deleted;
        //            });
        //            _ctx.Produkty.Remove(product);
        //            await _ctx.SaveChangesAsync();
        //        }
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}

