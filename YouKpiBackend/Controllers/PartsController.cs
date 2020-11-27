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
        public IActionResult Create([FromBody]Czesci part)
        {
            try
            {        
                _dbContext.Czesci.Add(part);
                _dbContext.SaveChangesAsync();
                return Created("", part);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody]Czesci part)
        {
            try { 
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromBody]Czesci part)
        {
            try
            {
                _dbContext.Czesci.Remove(part);
                _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

