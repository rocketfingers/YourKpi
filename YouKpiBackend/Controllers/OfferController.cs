using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.BusinessLibrary.Offer;
using YouKpiBackend.DbContexts;
using YouKpiBackend.ModelsEntity;
using YouKpiBackend.ViewModels;
using YouKpiBackend.ViewModels.Offer;

namespace YouKpiBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OfferController : Controller
    {
        private readonly YoukpiContext _ctx;
        private readonly OfferPdfService _offerPdfService;

        public OfferController(YoukpiContext ctx, OfferPdfService offerPdfService)
        {
            _ctx = ctx;
            _offerPdfService = offerPdfService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _ctx.Offer.Include(p => p.Clients)
                    .Include(p => p.OfferProcess)
                    .Include(p => p.OfferLines)
                    .ThenInclude(p => p.OfferLineProcess)
                    .Include(p => p.OfferLines)
                    .ThenInclude(p => p.Product)
                    .ToListAsync();

                //Ustawienie ceny w dniu oferty, jesli nie ustawila sie przy dodawaniu 
                res.ForEach(p =>
                {
                    p.OfferLines.ToList().ForEach(ol =>
                    {
                        ol.PriceInOfferDay = ol.Product.Cena;
                        if (!(ol.SalesPrice > 0) || ol.SalesPrice == null)
                        {
                            ol.SalesPrice = ol.Product.Cena;
                        }
                        ol.Product = null;
                    });
                });

                //var res = await _ctx.Offer.Include(p => p.Clients).Include(p => p.OfferLines).ThenInclude(p => p.Product).ToListAsync();
                return Ok(res.OrderByDescending(p => p.Id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetOfferAvailability(int id)
        {
            try
            {
                var offer = await _ctx.Offer.Where(o => o.Id == id).Include(p => p.Clients)
                    .Include(p => p.OfferLines)
                    .Include(p => p.OfferLines)
                    .ThenInclude(p => p.Product)
                    .ThenInclude(p => p.ProduktCzesci)
                    .FirstOrDefaultAsync();

                var availability = new AvailabilityOfferViewModel()
                {
                    AvailabilityProducts = new List<AvailabilityOfferLinesViewModel>()
                };

                foreach (var offerLine in offer.OfferLines)
                {
                    var availabilityParts = new List<AvailabilityPartsViewModel>();
                    var productParts = await _ctx.ProduktCzesci.Where(p => p.Id == offerLine.ProductId).Include(p => p.Czesci).ThenInclude(p => p.Komponent).ToListAsync();
                    foreach (var part in productParts)
                    {
                        var requiredPartsNo = part.IloscSztuk;
                        if (requiredPartsNo > 0)
                        {
                            requiredPartsNo = requiredPartsNo * offerLine.Quantity;
                        }
                        AvailabilityComponentViewModel availabilityComponent = null;
                        if (part.Czesci.Komponent != null)
                        {
                            var requiredComponentsNo = part.Czesci.Komponent.Ilosc;
                            if (requiredComponentsNo > 0)
                            {
                                requiredComponentsNo = requiredComponentsNo * requiredPartsNo * offerLine.Quantity;
                            }

                            var noOfAvailableComponents = _ctx.MagazynKomponenty.Where(p => p.ElementId == part.Czesci.KomponentId).FirstOrDefault()?.Ilosc;
                            var componentStatus = AvailabilityStatus.Lack;
                            if (noOfAvailableComponents == null)
                            { }
                            else if (noOfAvailableComponents >= part.Czesci.Komponent.Ilosc)
                            {
                                componentStatus = AvailabilityStatus.Enough;
                            }
                            else if (noOfAvailableComponents > 0)
                            {
                                componentStatus = AvailabilityStatus.NotEnouth;
                            }

                            availabilityComponent = new AvailabilityComponentViewModel()
                            {
                                Status = componentStatus,
                                AvailableComponents = noOfAvailableComponents,
                                Ilosc = requiredComponentsNo,
                                CenaJednostkowa = part.Czesci.Komponent.CenaJednostkowa,
                                GatunekPodst = part.Czesci.Komponent.GatunekPodst,
                                Id = part.Czesci.Komponent.Id,
                                Jednostka = part.Czesci.Komponent.Jednostka,
                                KomponentId = part.Czesci.Komponent.KomponentId,
                                Ltid = part.Czesci.Komponent.Ltid,
                                Nazwa = part.Czesci.Komponent.Nazwa,
                                Wymiar = part.Czesci.Komponent.Wymiar,
                                ProcessId = part.Czesci.Komponent.ProcessId
                            };
                        }

                        var noOfAvailableParts = _ctx.MagazynCzesci.Where(p => p.ElementId == part.CzesciId).FirstOrDefault()?.Ilosc;
                        var prartStatus = AvailabilityStatus.Lack;
                        if (noOfAvailableParts == null)
                        { }
                        else if (noOfAvailableParts >= requiredPartsNo)
                        {
                            prartStatus = AvailabilityStatus.Enough;
                        }
                        else if (noOfAvailableParts > 0)
                        {
                            prartStatus = AvailabilityStatus.NotEnouth;
                        }

                        availabilityParts.Add(new AvailabilityPartsViewModel()
                        {
                            Ilosc = requiredPartsNo,
                            AvailableParts = noOfAvailableParts,
                            GatPodstawowy = part.Czesci.GatPodstawowy,
                            Id = part.Czesci.Id,
                            KomponentId = part.Czesci.KomponentId,
                            Nazwa = part.Czesci.Nazwa,
                            NumerRysNorma = part.Czesci.NumerRysNorma,
                            Tj = part.Czesci.Tj,
                            Tpz = part.Czesci.Tpz,
                            Wymiary = part.Czesci.Wymiary,
                            Status = prartStatus,
                            Komponent = availabilityComponent
                        });
                    }

                    var noOfAvailableProducts = _ctx.MagazynProdukty.Where(p => p.ElementId == offerLine.ProductId).FirstOrDefault()?.Ilosc;
                    var productStatus = AvailabilityStatus.Lack;
                    if (noOfAvailableProducts == null)
                    { }
                    else if (noOfAvailableProducts >= offerLine.Quantity)
                    {
                        productStatus = AvailabilityStatus.Enough;
                    }
                    else if (noOfAvailableProducts > 0)
                    {
                        productStatus = AvailabilityStatus.NotEnouth;
                    }

                    availability.AvailabilityProducts.Add(
                        new AvailabilityOfferLinesViewModel()
                        {
                            Sale = offerLine.Sale,
                            SalesPrice = offerLine.SalesPrice,
                            AdditionalEquipment = offerLine.AdditionalEquipment,
                            AvailableProducts = noOfAvailableProducts,
                            Id = offerLine.Id,
                            Medium = offerLine.Medium,
                            OfferId = offerLine.OfferId,
                            Parts = availabilityParts,
                            PriceInOfferDay = offerLine.PriceInOfferDay,
                            ProductId = offerLine.Product.ProductId.ToString(),
                            Quantity = offerLine.Quantity,
                            W = offerLine.W,
                            Status = productStatus
                        }
                        );
                }

                return Ok(availability);
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
                entity.OfferLines.ToList().ForEach(p =>
                {
                    p.Product = null;
                    p.Offer = null;
                    p.OfferLineProcess.ToList().ForEach(ol =>
                    {
                        ol.OfferLine = null;
                        ol.Process = null;
                    });


                });

                entity.OfferProcess.ToList().ForEach(p =>
                {
                    p.Process = null;
                    p.Offer = null;
                });
                var res = _ctx.Add(entity);
                entity.Id = res.Entity.Id;
                await _ctx.SaveChangesAsync();

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
                var offer = await _ctx.Offer
                    .Include(p => p.OfferProcess)
                    .Include(p => p.OfferLines)
                    .ThenInclude(p => p.Product)
                    .Include(p => p.OfferLines)
                    .ThenInclude(p => p.OfferLineProcess)
                    .Include(p => p.Clients)
                    .FirstOrDefaultAsync(p => p.Id == entity.Id);

                //await UpdateOfferLines(entity.OfferLines.ToList(), offer);


                offer.OfferLines.ToList().ForEach(p =>
                {
                    p.OfferLineProcess.ToList().ForEach(olp =>
                    {
                        _ctx.Entry(olp).State = EntityState.Deleted;
                    });
                    _ctx.Entry(p).State = EntityState.Deleted;
                });
                offer.OfferProcess.ToList().ForEach(olp =>
                {
                    _ctx.Entry(olp).State = EntityState.Deleted;
                });

                foreach (var item in entity.OfferLines.ToList())
                {
                    item.OfferId = offer.Id;
                    item.Product = null;
                    offer.OfferLines.Add(item);
                }

                offer.Name = entity.Name;
                offer.OfferDate = entity.OfferDate;
                offer.Offerrer = entity.Offerrer;
                offer.OrderDate = entity.OrderDate;
                offer.ProjectsId = entity.ProjectsId;
                offer.Status = entity.Status;
                offer.OrderType = entity.OrderType;
                offer.ClientsId = entity.ClientsId;
                offer.PlannedEnd = entity.PlannedEnd;
                offer.OfferProcess = entity.OfferProcess;

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
                var offer = _ctx.Offer.Include(p => p.OfferProcess).Include(p => p.OfferLines).ThenInclude(p => p.OfferLineProcess).FirstOrDefault(p => p.Id == id);
                if (offer != null)
                {
                    offer.OfferLines.ToList().ForEach(p =>
                    {
                        foreach (var item in p.OfferLineProcess)
                        {
                            _ctx.Entry(item).State = EntityState.Deleted;
                        }

                        _ctx.Entry(p).State = EntityState.Deleted;
                    });

                    foreach (var op in offer.OfferProcess)
                    {
                        _ctx.Entry(op).State = EntityState.Deleted;
                    }
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
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetPdf(int id)
        {
            try
            {
                var bytes = await _offerPdfService.GeneratePdf(id);
                return File(bytes, "application/octet-stream", "faktura.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }
    }
}

