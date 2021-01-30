using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;
using YouKpiBackend.ViewModels;

namespace YouKpiBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly YoukpiContext _ctx;
        private readonly IMapper _mapper;

        public StoreController(YoukpiContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lst = new List<StoreElementViewModel>();
                lst.AddRange(_mapper.Map<List<StoreElementViewModel>>(await _ctx.MagazynCzesci.ToListAsync()));
                lst.AddRange(_mapper.Map<List<StoreElementViewModel>>(await _ctx.MagazynKomponenty.ToListAsync()));
                lst.AddRange(_mapper.Map<List<StoreElementViewModel>>(await _ctx.MagazynKomponenty.ToListAsync()));
                
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> Create([FromBody] ReasonCodes entity)
        //{
        //    if (entity == null)
        //    {
        //        return BadRequest("Bad model");
        //    }
        //    try
        //    {
        //        var res = _ctx.ReasonCodes.Add(entity);
        //        _ctx.SaveChanges();
        //        entity.Id = res.Entity.Id;
        //        return Created("", entity);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[HttpPut("[action]")]
        //public async Task<IActionResult> Update([FromBody] ReasonCodes entity)
        //{
        //    if (entity == null)
        //    {
        //        return BadRequest("Bad model");
        //    }
        //    try
        //    {
        //        var reasonCode = _ctx.ReasonCodes.FirstOrDefault(p => p.Id == entity.Id);
        //        reasonCode.Opis = entity.Opis;
        //        reasonCode.IdDifferenceReasonCode = entity.IdDifferenceReasonCode;
        //        await _ctx.SaveChangesAsync();

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[HttpDelete("[action]")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var item = _ctx.ReasonCodes.FirstOrDefault(p => p.Id == id);
        //        if (item != null)
        //        {
        //            _ctx.ReasonCodes.Remove(item);
        //            await _ctx.SaveChangesAsync();
        //        }
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
