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
                return Ok(await _dbContext.Czesci.ToListAsync());
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
                _dbContext.Czesci.Add(part);
                await _dbContext.SaveChangesAsync();

                return Created("", part);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
                var part = _dbContext.Czesci.FirstOrDefault(p => p.Id == entity.Id);
                part.Nazwa = entity.Nazwa;
                part.GatPodstawowy = entity.GatPodstawowy;
                part.NumerRysNorma = entity.NumerRysNorma;

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

