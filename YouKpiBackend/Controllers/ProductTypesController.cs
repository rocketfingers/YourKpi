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
    [Route("api/[controller]")]
    public class ProductTypesController : Controller
    {
        private readonly YoukpiContext _ctx;
        public ProductTypesController(YoukpiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _ctx.TypWyrobu.ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] TypWyrobu part)
        {
            if (part == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                _ctx.TypWyrobu.Add(part);
                await _ctx.SaveChangesAsync();

                return Created("", part);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] TypWyrobu entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                _ctx.Entry(entity).State = EntityState.Modified;
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
                var partEntity = _ctx.TypWyrobu.FirstOrDefault(c => c.Id == id);
                if (partEntity != null)
                {
                    _ctx.TypWyrobu.Remove(partEntity);
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
