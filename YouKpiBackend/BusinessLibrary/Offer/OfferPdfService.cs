using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using YouKpiBackend.ModelsEntity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;

namespace YouKpiBackend.BusinessLibrary.Offer
{
    public class OfferPdfService
    {
        private readonly YoukpiContext _ctx;

        public OfferPdfService(YoukpiContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<byte[]> GeneratePdf(int id)
        {
            var res = await GetOfferById(id);
            var outputStream = new MemoryStream();
            byte[] byteArr;

            using (var ms = new MemoryStream())
            {
                var document = new PdfDocument();
                var page = document.AddPage();
                DrawPageContent(ref page, res);
                document.Save(ms);
                document.Close();
                byteArr = ms.ToArray();
            }

            return byteArr != null ? byteArr : new byte[0];
        }

        private Task<ModelsEntity.Offer> GetOfferById(int id)
        {
            var res = _ctx.Offer.Include(p => p.Clients)
                .Include(p => p.OfferProcess)
                .Include(p => p.OfferLines)
                .ThenInclude(p => p.OfferLineProcess)
                .Where(o => o.Id == id).FirstOrDefaultAsync();
            return res;
        }

        private void DrawPageContent(ref PdfPage page, ModelsEntity.Offer res)
        {
            var gfx = XGraphics.FromPdfPage(page);
            var x = 15;
            var font = new XFont("OpenSans", x, XFontStyle.Regular);
            gfx.DrawString("Oferta: " + res.Name, font, XBrushes.Black, new XRect(20, 20, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Projekt id: " + res.ProjectsId, font, XBrushes.Black, new XRect(20, 20 + x, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Typ zamówienia: " + res.OrderType, font, XBrushes.Black, new XRect(20, 20 + 2 * x, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Status: " + res.Status, font, XBrushes.Black, new XRect(20, 20 + 3 * x, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Data oferty: " + res.OfferDate, font, XBrushes.Black, new XRect(20, 20 + 4 * x, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Klient: " + res.ClientsId, font, XBrushes.Black, new XRect(20, 20 + 5 * x, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Data zamówienia: " + res.OrderDate, font, XBrushes.Black, new XRect(20, 20 + 6 * x, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Planowane zakończenie: " + res.PlannedEnd, font, XBrushes.Black, new XRect(20, 20 + 7 * x, page.Width, page.Height), XStringFormats.TopLeft);

        }
    }
}
