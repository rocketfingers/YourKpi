using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;
using YouKpiBackend.Helpers.Extensions;
using YouKpiBackend.ModelsEntity;
using YouKpiBackend.ViewModels.Store;

namespace YouKpiBackend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PartsController : ControllerBase
    {
        private readonly YoukpiContext _dbContext;

        public PartsController(YoukpiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = _dbContext.Czesci.Include(p => p.Komponent).Include(p => p.CzesciRysunkiInfo);
                foreach (var item in res) /// TODO: do servisu albo decyzji - czy usuwac stare pliki i jak brac najnowszy
                {
                    var actualDwg = item.CzesciRysunkiInfo.OrderBy(p => p.Id).LastOrDefault();
                    item.CzesciRysunkiInfo.Clear();
                    item.CzesciRysunkiInfo.Add(actualDwg);
                }
                return Ok(await res.ToListAsync());
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
                var res = await _dbContext.Czesci.Select(x => new StoreElementSimpleViewModel(x.Id, x.Nazwa, x.Komponent.Ilosc * x.Komponent.CenaJednostkowa)).ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllForOfferLineIdSimpleView(int offerLineId)
        {
            try
            {

                var res = await _dbContext.OfferLines
                    .Include(p => p.Product)
                    .ThenInclude(p => p.ProduktCzesci)
                    .ThenInclude(p => p.Czesci)
                    .Where(p => p.Id == (offerLineId))
                    .SelectMany(p => p.Product.ProduktCzesci.Select(x => x.Czesci)).ToListAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var encodedId = id.Replace("%2F", "");
                var res = await _dbContext.Czesci.Where(p => p.Id.Replace("/", "") == encodedId.RmNlTrim()).FirstOrDefaultAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Czesci part)
        {
            if (part == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                if (part.KomponentId != 0)
                {
                    await UpdateComponent(part.Komponent);
                    part.Komponent = null;
                }
                else
                {
                    part.Komponent.MagazynKomponenty = null;
                    part.Komponent.Czesci = null;
                }
                var res = _dbContext.Czesci.Add(part);

                await _dbContext.SaveChangesAsync();
                part.Id = res.Entity.Id;

                return Created("", part);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFile(IFormCollection formFile)
        {
            try
            {
                var file = formFile.Files[0];
                var fileNameSplited = file.FileName.Split('.');

                var item = new CzesciRysunkiInfo()
                {
                    Nazwa = file.FileName,
                    CzescId = formFile["partId"].ToString(),
                    DataDodania = DateTime.Now,
                    CzesciRysunki = new CzesciRysunki()
                    {
                        Rozszerzenie = fileNameSplited.Last()
                    }
                };

                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var bytes = ms.ToArray();
                    item.CzesciRysunki.Base64FileContent = bytes;
                }
                _dbContext.CzesciRysunkiInfo.Add(item);

                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{drawingInfoId}")]
        public async Task<IActionResult> GetDrawingContent(int drawingInfoId)
        {
            try
            {
                var dwgs = await _dbContext.CzesciRysunki.Where(p => p.CzesciRysunkiInfo.Id == drawingInfoId).FirstOrDefaultAsync();
                var bytes = dwgs.Base64FileContent ?? new byte[0];

                return File(bytes, "application/octet-stream", "content");
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }

        private async Task UpdateComponent(Komponenty entity)
        {
            if (entity != null)
            {

                var component = _dbContext.Komponenty.FirstOrDefault(p => p.Id == entity.Id);

                component.CenaJednostkowa = entity.CenaJednostkowa;
                component.GatunekPodst = entity.GatunekPodst;
                component.Ilosc = entity.Ilosc;
                component.Jednostka = entity.Jednostka;
                component.Nazwa = entity.Nazwa;
                component.ProcessId = entity.ProcessId;
                component.KomponentId = entity.KomponentId;
                component.Wymiar = entity.Wymiar;
                component.Ltid = entity.Ltid;


                await _dbContext.SaveChangesAsync();
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] Czesci entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                if (entity.KomponentId != 0)
                {
                    await UpdateComponent(entity.Komponent);
                }
                var part = _dbContext.Czesci.Include(p => p.Komponent).FirstOrDefault(p => p.Id == entity.Id);
                part.Nazwa = entity.Nazwa;
                part.GatPodstawowy = entity.GatPodstawowy;
                part.NumerRysNorma = entity.NumerRysNorma;
                part.KomponentId = entity.KomponentId;
                part.Tpz = entity.Tpz;
                part.Tj = entity.Tj;
                part.NumerProgramu = entity.NumerProgramu;
                part.CzasProgramisty = entity.CzasProgramisty;
                part.Wykonanie = entity.Wykonanie;

                //part.Komponent = entity.Komponent;
                //part.Komponent.MagazynKomponenty = null;
                //part.Komponent.Czesci = null;
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var partEntity = _dbContext.Czesci.FirstOrDefault(c => c.Id == id);
                if (partEntity != null)
                {
                    _dbContext.Czesci.Remove(partEntity);
                    await _dbContext.SaveChangesAsync();
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

