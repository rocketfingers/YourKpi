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
    public class ProcessController : Controller
    {
        private readonly YoukpiContext _ctx;
        public ProcessController(YoukpiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _ctx.Process.Include(p=> p.Steps).ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Process entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var res = _ctx.Process.Add(entity);
                await _ctx.SaveChangesAsync();

                return Created("", entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] Process entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var processEntity = _ctx.Process.Include(p => p.Steps).FirstOrDefault(c => c.Id == entity.Id);
                processEntity.Steps.ToList().ForEach(p =>
                {
                    _ctx.Entry(p).State = EntityState.Deleted;
                });

                processEntity.TypZlecenia = entity.TypZlecenia;
                processEntity.BusinessArea= entity.BusinessArea;
                processEntity.NazwaGrupyProcesu = entity.NazwaGrupyProcesu;
                processEntity.NazwaProcesu = entity.NazwaProcesu;
                processEntity.Steps = entity.Steps;

                await  _ctx.SaveChangesAsync();

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
                
                var processEntity = _ctx.Process.Include(p => p.Steps).FirstOrDefault (c => c.Id == id);
                processEntity.Steps.ToList().ForEach(p =>
                {
                    _ctx.Entry(p).State = EntityState.Deleted;
                });
                if (processEntity != null)
                {
                    _ctx.Process.Remove(processEntity);
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
