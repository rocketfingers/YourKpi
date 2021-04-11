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
                lst.AddRange(_mapper.Map<List<StoreElementViewModel>>(await _ctx.MagazynTowary.ToListAsync()));

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
                            await _ctx.SaveChangesAsync();
                            entity.Id = res1.Entity.Id;
                            return Created("", entity);
                        case 2:
                            var res2 = _ctx.MagazynProdukty.Add(_mapper.Map<MagazynProdukty>(entity));
                            await _ctx.SaveChangesAsync();
                            entity.Id = res2.Entity.Id;
                            return Created("", entity);
                        case 3:
                            var res3 = _ctx.MagazynKomponenty.Add(_mapper.Map<MagazynKomponenty>(entity));
                            entity.Id = res3.Entity.Id;
                            await _ctx.SaveChangesAsync();
                            return Created("", entity);
                        case 4:
                            var res4 = _ctx.MagazynTowary.Add(_mapper.Map<MagazynTowary>(entity));
                            entity.Id = res4.Entity.Id;
                            await _ctx.SaveChangesAsync();
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

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] StoreElementViewModel entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                switch (entity.Magazyn.Id)
                {
                    case 1:
                        var item = _ctx.MagazynCzesci.FirstOrDefault(p => p.Id == entity.Id);
                        if (item != null)
                        {
                            item.CenaJdnNetto = entity.CenaJdnNetto;
                            item.DataPrzyjecia = entity.DataPrzyjecia;
                            item.Element = null;
                            item.ElementId = entity.ElementId;
                            item.Ilosc = entity.Ilosc;
                            item.Jednostka = entity.Jednostka;
                            item.Kontrahent = null;
                            item.KontrahentId = entity.KontrahentId;
                            item.Lokacja = null;
                            item.LokacjaId = entity.LokacjaId;
                            item.NrFakturyId = entity.NrFakturyId;

                            await _ctx.SaveChangesAsync();
                        }
                        return NoContent();
                    case 2:
                        var item2 = _ctx.MagazynProdukty.FirstOrDefault(p => p.Id == entity.Id);
                        if (item2 != null)
                        {
                            item2.CenaJdnNetto = entity.CenaJdnNetto;
                            item2.DataPrzyjecia = entity.DataPrzyjecia;
                            item2.Element = null;
                            item2.ElementId = entity.ElementId;
                            item2.Ilosc = entity.Ilosc;
                            item2.Jednostka = entity.Jednostka;
                            item2.Kontrahent = null;
                            item2.KontrahentId = entity.KontrahentId;
                            item2.Lokacja = null;
                            item2.LokacjaId = entity.LokacjaId;
                            item2.NrFakturyId = entity.NrFakturyId;

                            await _ctx.SaveChangesAsync();
                        }
                        return NoContent();
                    case 3:
                        var item3 = _ctx.MagazynKomponenty.FirstOrDefault(p => p.Id == entity.Id);
                        if (item3 != null)
                        {
                            item3.CenaJdnNetto = entity.CenaJdnNetto;
                            item3.DataPrzyjecia = entity.DataPrzyjecia;
                            item3.Element = null;
                            int? elId = null;
                            if (int.TryParse(entity.ElementId, out int res))
                            {
                                elId = res;
                            }
                            item3.ElementId = elId;
                            item3.Ilosc = entity.Ilosc;
                            item3.Jednostka = entity.Jednostka;
                            item3.Kontrahent = null;
                            item3.KontrahentId = entity.KontrahentId;
                            item3.Lokacja = null;
                            item3.LokacjaId = entity.LokacjaId;
                            item3.NrFakturyId = entity.NrFakturyId;

                            await _ctx.SaveChangesAsync();
                        }
                        return NoContent();
                    case 4:
                        var item4 = _ctx.MagazynTowary.FirstOrDefault(p => p.Id == entity.Id);
                        if (item4 != null)
                        {
                            item4.CenaJdnNetto = entity.CenaJdnNetto;
                            item4.DataPrzyjecia = entity.DataPrzyjecia;
                            item4.Element = null;
                            item4.ElementId = entity.ElementId;
                            item4.Ilosc = entity.Ilosc;
                            item4.Jednostka = entity.Jednostka;
                            item4.Kontrahent = null;
                            item4.KontrahentId = entity.KontrahentId;
                            item4.Lokacja = null;
                            item4.LokacjaId = entity.LokacjaId;
                            item4.NrFakturyId = entity.NrFakturyId;

                            await _ctx.SaveChangesAsync();
                        }
                        return NoContent();
                    default:
                        throw new Exception("Nieparwidlowy magazyn!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

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
                        var item2 = _ctx.MagazynProdukty.FirstOrDefault(p => p.Id == id);
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

                    case 4:
                        var item4 = _ctx.MagazynTowary.FirstOrDefault(p => p.Id == id);
                        if (item4 != null)
                        {
                            _ctx.MagazynTowary.Remove(item4);
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
