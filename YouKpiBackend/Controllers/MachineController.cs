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

namespace YouKpiBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MachineController : Controller
    {
        private readonly YoukpiContext _ctx;
        public MachineController(YoukpiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _ctx.Maszyny.ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Maszyny entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var res = _ctx.Maszyny.Add(entity);
                _ctx.SaveChanges();

                return Created("", entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] Maszyny entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var machine = _ctx.Maszyny.FirstOrDefault(p => p.Id == entity.Id);
                machine.MiejscePracy = entity.MiejscePracy;
                machine.Nazwa = entity.Nazwa;
                machine.Opis = entity.Opis;
                machine.OsX = entity.OsX;
                machine.OsY = entity.OsY;
                machine.OsZ = entity.OsZ;
                machine.Rodzaj = entity.Rodzaj;
                machine.DataUruchomienia = entity.DataUruchomienia;
                machine.Status = entity.Status;
                machine.Krytycznosc = entity.Krytycznosc;
                machine.TrybPracy = entity.TrybPracy;
                await _ctx.SaveChangesAsync();

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
                var item = _ctx.Maszyny.FirstOrDefault(p => p.Id == id);
                if (item != null)
                {
                    _ctx.Maszyny.Remove(item);
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
