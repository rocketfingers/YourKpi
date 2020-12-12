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
    public class OfferController : Controller
    {
        private readonly YoukpiContext _ctx;
        public OfferController(YoukpiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _ctx.Offer.Include(p => p.Clients).Include(p => p.OfferLines).ToListAsync();
                //var res = await _ctx.Offer.Include(p => p.Clients).Include(p => p.OfferLines).ThenInclude(p => p.Product).ToListAsync();
                return Ok(res.OrderByDescending(p => p.Id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] Offer entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var res = _ctx.Offer.Add(entity);
                entity.Id = res.Entity.Id;
                _ctx.SaveChanges();

                return Created("", entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] Offer entity)
        {
            if (entity == null)
            {
                return BadRequest("Bad model");
            }
            try
            {
                var offer = _ctx.Offer.Include(p => p.OfferLines).FirstOrDefault(p => p.Id == entity.Id);
                offer.Name = entity.Name;
                offer.OfferDate = entity.OfferDate;
                offer.Offerrer = entity.Offerrer;
                offer.OrderDate = entity.OrderDate;
                offer.ProjectsId = entity.ProjectsId;
                offer.Status = entity.Status;
                offer.OrderType = entity.OrderType;
                offer.ClientsId = entity.ClientsId;
                offer.PlannedEnd = entity.PlannedEnd;

                //product.ProduktCzesci.ToList().ForEach(p =>
                //{
                //    _ctx.Entry(p).State = EntityState.Deleted;
                //});
                //entity.ProduktCzesci.ToList().ForEach(part =>
                //{
                //    part.ProduktyId = product.Id;
                //    product.ProduktCzesci.Add(part);
                //});
                _ctx.SaveChanges();

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
                var offer = _ctx.Offer.Include(p => p.OfferLines).FirstOrDefault(p => p.Id == id);
                if (offer != null)
                {
                    offer.OfferLines.ToList().ForEach(p =>
                    {
                        _ctx.Entry(p).State = EntityState.Deleted;
                    });
                    _ctx.Offer.Remove(offer);
                    await _ctx.SaveChangesAsync();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //private List<OfferViewModel> MapViewModel(List<Offer> offersEntity)
        //{
        //    var offersViewModels = new List<OfferViewModel>();

        //    foreach (var offer in offersEntity)
        //    {
        //        var offerViewModel = new OfferViewModel()
        //        {
        //            Status = offer.Status,
        //            ClientsId = offer.ClientsId,
        //            Id = offer.Id,
        //            Name = offer.Name,
        //            OfferDate = offer.OfferDate,
        //            Offerrer = offer.Offerrer,
        //            OrderDate = offer.OrderDate,
        //            PlannedEnd = offer.PlannedEnd
        //        };
        //        offersViewModels.Add(offerViewModel);
        //    }
        //    return offersViewModels;
        //}

    }
}

