﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;
using YouKpiBackend.ModelsEntity;
using YouKpiBackend.ViewModels;
using YouKpiBackend.ViewModels.Store;

namespace YouKpiBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly YoukpiContext _ctx;
        public ProductsController(YoukpiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = _ctx.Produkty
                    .Include(p => p.ProduktCzesci)
                    .ThenInclude(p => p.Czesci)
                    .ThenInclude(p => p.CzesciRysunkiInfo)
                    .Include(p => p.ProduktCzesci)
                    .ThenInclude(p => p.Czesci)
                    .ThenInclude(p => p.Komponent)
                    .Include(p => p.ProduktyRysunkiInfo);

                foreach (var item in res) /// TODO: Zapetlona referencja... Naprawic to mozna dodajac warstwe viewmodeli
                {
                    foreach (var pc in item.ProduktCzesci)
                    {
                        pc.Czesci.ProduktCzesci = null;
                    }

                    var actualDwg = item.ProduktyRysunkiInfo.OrderBy(p => p.Id).LastOrDefault();
                    item.ProduktyRysunkiInfo.Clear();
                    item.ProduktyRysunkiInfo.Add(actualDwg);
                }

                var lst = await res.ToListAsync();

                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllSimpleView()
        {
            try
            {
                var res = await _ctx.Produkty.Select(x => new StoreElementSimpleViewModel(x.Id.ToString(), x.ProductName, x.Cena)).ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllSimple()
        {
            try
            {
                var res = await _ctx.Produkty.Select(p => new ProductSimpleViewModel()
                {
                    Id = p.Id,
                    Name = p.ProductName
                }).ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("[action]/{drawingInfoId}")]
        public async Task<IActionResult> GetDrawingContent(int drawingInfoId)
        {
            try
            {
                var dwgs = await _ctx.ProduktyRysunki.Where(p => p.ProduktyRysunkiInfo.Id == drawingInfoId).FirstOrDefaultAsync();
                var bytes = dwgs.Base64FileContent ?? new byte[0];

                return File(bytes, "application/octet-stream", "content");
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Produkty entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                entity.ProduktCzesci.ToList().ForEach(p =>
                {
                    p.Czesci = null;
                });

                //var iterator = 0;
                //var tempId = entity.Id;
                //while (_ctx.Produkty.Any(p => p.Id == tempId))
                //{
                //    iterator++;
                //    tempId = entity.Id + "_" + iterator;
                //};

                //entity.Id = tempId;
                var res = _ctx.Produkty.Add(entity);
                await _ctx.SaveChangesAsync();
                entity.Id = res.Entity.Id;
                return Created("", entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] Produkty entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var product = _ctx.Produkty.Include(p => p.ProduktCzesci).FirstOrDefault(p => p.Id == entity.Id);
                product.ProductName = entity.ProductName;
                product.TypWyrobuId = entity.TypWyrobuId;
                product.NumerRysNorma = entity.NumerRysNorma;
                product.Dn = entity.Dn;
                product.Pn = entity.Pn;
                product.Ansi = entity.Ansi;
                product.Wersja = entity.Wersja;
                product.Uszczelnienie = entity.Uszczelnienie;
                product.Cena = entity.Cena;
                product.Waluta = entity.Waluta;
                _ctx.Entry(product).State = EntityState.Modified;
                product.ProduktCzesci.ToList().ForEach(p =>
                {
                    _ctx.Entry(p).State = EntityState.Deleted;
                });
                entity.ProduktCzesci.ToList().ForEach(part =>
                {
                    part.Czesci = null;
                    part.ProduktId = product.Id;
                    product.ProduktCzesci.Add(part);
                });

                await _ctx.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFile(IFormCollection formFile)
        {
            try
            {
                var file = formFile.Files[0];
                var fileNameSplited = file.FileName.Split('.');
                int.TryParse(formFile["productId"], out int productId);

                var item = new ProduktyRysunkiInfo()
                {
                    Nazwa = file.FileName,
                    ProduktId = productId,
                    DataDodania = DateTime.Now,
                    ProduktyRysunki = new ProduktyRysunki()
                    {
                        Rozszerzenie = fileNameSplited.Last()
                    }
                };

                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var bytes = ms.ToArray();
                    item.ProduktyRysunki.Base64FileContent = bytes;
                }
                _ctx.ProduktyRysunkiInfo.Add(item);

                await _ctx.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = _ctx.Produkty.Include(p => p.ProduktCzesci).FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    product.ProduktCzesci.ToList().ForEach(p =>
                    {
                        _ctx.Entry(p).State = EntityState.Deleted;
                    });
                    _ctx.Produkty.Remove(product);
                    await _ctx.SaveChangesAsync();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
