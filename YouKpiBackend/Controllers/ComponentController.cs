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
using YouKpiBackend.ViewModels;
using YouKpiBackend.ViewModels.Store;

namespace YouKpiBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ComponentController : Controller
    {
        private readonly YoukpiContext _ctx;
        public ComponentController(YoukpiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _ctx.Komponenty.Include(p => p.Czesci).ToListAsync();
                return Ok(res);
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
                var res = await _ctx.Komponenty.Select(x => new StoreElementSimpleViewModel(x.Id.ToString(), x.Nazwa, x.CenaJednostkowa)).ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUnassign()
        {
            try
            {
                var res = await _ctx.Komponenty.Include(p => p.Czesci).ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Komponenty entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var res = _ctx.Komponenty.Add(entity);
                await _ctx.SaveChangesAsync();

                return Created("", entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] Komponenty entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var component = _ctx.Komponenty.FirstOrDefault(p => p.Id == entity.Id);

                component.CenaJednostkowa = entity.CenaJednostkowa;
                component.GatunekPodst = entity.GatunekPodst;
                component.Ilosc = entity.Ilosc;
                component.Jednostka = entity.Jednostka;
                component.Nazwa = entity.Nazwa;
                component.ProcessId = entity.ProcessId;
                component.KomponentId = entity.KomponentId;
                component.Wymiar = entity.Wymiar;
                component.Ltid = entity.Ltid;

                await _ctx.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = _ctx.Komponenty.FirstOrDefault(p => p.Id == id);
                if (item != null)
                {
                    _ctx.Komponenty.Remove(item);
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
