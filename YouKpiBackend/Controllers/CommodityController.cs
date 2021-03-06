﻿using AutoMapper;
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
using YouKpiBackend.ViewModels.Store;

namespace YouKpiBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CommodityController : Controller
    {
        private readonly YoukpiContext _ctx;
        private readonly IMapper _mapper;

        public CommodityController(YoukpiContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllSimple()
        {
            try
            {
                var res = await _ctx.Towary.Select(p => new { Id = p.Id, Name = p.Nazwa }).ToListAsync();
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
                var res = await _ctx.Towary.Select(x => new StoreElementSimpleViewModel(x.Id, x.Nazwa, x.CenaSprzedazy)).ToListAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _ctx.Towary.ToListAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Towary entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var res = _ctx.Towary.Add(entity);
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
        public async Task<IActionResult> Update([FromBody] Towary entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var item = _ctx.Towary.FirstOrDefault(p => p.Id == entity.Id);

                item.Nazwa = entity.Nazwa;
                item.CenaSprzedazy = entity.CenaSprzedazy;
                item.CenaZakupu = entity.CenaZakupu;
                item.WalutaSprzedazy = entity.WalutaSprzedazy;
                item.WalutaZakupu = entity.WalutaZakupu;

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
                var item = _ctx.Towary.FirstOrDefault(p => p.Id == id);
                if (item != null)
                {
                    _ctx.Towary.Remove(item);
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
