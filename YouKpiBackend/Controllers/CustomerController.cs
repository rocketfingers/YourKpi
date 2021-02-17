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
    public class CustomerController : Controller
    {
        private readonly YoukpiContext _ctx;

        public CustomerController(YoukpiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _ctx.Client.Select(p => new CustomerSimpleViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Nip = p.Nip
                }).ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllFull()
        {
            try
            {
                var res = await _ctx.Client.ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Client entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var res = _ctx.Client.Add(entity);
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
        public async Task<IActionResult> Update([FromBody] Client entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var client = _ctx.Client.FirstOrDefault(p => p.Id == entity.Id);
                client.Nip = entity.Nip;
                client.Name = entity.Name;
                client.KodPocztowy = entity.KodPocztowy;
                client.Miasto = entity.Miasto;
                client.Ulica = entity.Ulica;
                client.NrBudynku = entity.NrBudynku;
                client.NrTelefonu = entity.NrTelefonu;
                client.Email = entity.Email;

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
                var item = _ctx.Client.FirstOrDefault(p => p.Id == id);
                if (item != null)
                {
                    _ctx.Client.Remove(item);
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

