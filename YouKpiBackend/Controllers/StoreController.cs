using AutoMapper;
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
                lst.AddRange(_mapper.Map<List<StoreElementViewModel>>(await _ctx.MagazynProdukty.ToListAsync()));
                lst.AddRange(_mapper.Map<List<StoreElementViewModel>>(await _ctx.MagazynKomponenty.ToListAsync()));

                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] StoreElementViewModel entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                if (entity.Magazyn.Id > 0)
                {
                    switch (entity.Magazyn.Id)
                    {
                        case 1:
                            var res1 = _ctx.MagazynCzesci.Add(_mapper.Map<MagazynCzesci>(entity));
                            _ctx.SaveChanges();
                            entity.Id = res1.Entity.Id;
                            return Created("", entity);
                        case 2:
                            var res2 = _ctx.MagazynProdukty.Add(_mapper.Map<MagazynProdukty>(entity));
                            _ctx.SaveChanges();
                            entity.Id = res2.Entity.Id;
                            return Created("", entity);
                        case 3:
                            var res3 = _ctx.MagazynKomponenty.Add(_mapper.Map<MagazynKomponenty>(entity));
                            entity.Id = res3.Entity.Id;
                            _ctx.SaveChanges();
                            return Created("", entity);
                        default:
                            throw new Exception("Nieparwidlowy magazyn!");
                    }
                }
                else
                {
                    throw new Exception("Nie wybrano magazynu!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

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

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(int id, int magazynId)
        {
            try
            {
                switch (magazynId)
                {
                    case 1:
                        var item = _ctx.MagazynCzesci.FirstOrDefault(p => p.Id == id);
                        if (item != null)
                        {
                            _ctx.MagazynCzesci.Remove(item);
                            await _ctx.SaveChangesAsync();
                        }
                        return Ok();

                    case 2:
                        var item2= _ctx.MagazynProdukty.FirstOrDefault(p => p.Id == id);
                        if (item2 != null)
                        {
                            _ctx.MagazynProdukty.Remove(item2);
                            await _ctx.SaveChangesAsync();
                        }
                        return Ok();

                    case 3:
                        var item3 = _ctx.MagazynKomponenty.FirstOrDefault(p => p.Id == id);
                        if (item3 != null)
                        {
                            _ctx.MagazynKomponenty.Remove(item3);
                            await _ctx.SaveChangesAsync();
                        }
                        return Ok();
                    default:
                        throw new Exception("Nieparwidlowy magazyn!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
