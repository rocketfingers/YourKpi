using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
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
       // private readonly int x = 15;
        private readonly int interline = 15;
        private readonly int lineHeight = 20;
        private readonly int tableLineHeight = 10;
        private readonly int marginLeft = 20;
        private readonly int marginTop = 20;
        private readonly int marginRight = 20;
        private readonly int marginBottom = 20;
        private readonly int textPadding = 20;

        private readonly double col1Pos = 40;
        private readonly double col2Pos = 180;
        private readonly double col3Pos = 220;
        private readonly double col4Pos = 250;
        private readonly double col5Pos = 300;
        private readonly double col6Pos = 400;
        private readonly double col7Pos = 480;

        public OfferPdfService(YoukpiContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<byte[]> GeneratePdf(int id)
        {
            var res = await GetOfferById(id);
            byte[] byteArr;
            using (var ms = new MemoryStream())
            {
                var document = new PdfDocument();
                var page = document.AddPage();
                DrawPageContent(ref page, res);
                document.Info.Title = "Oferta " + res.Name;
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
                .Include(p => p.OfferLines)
                .ThenInclude(p => p.Product)
                .Where(o => o.Id == id).FirstOrDefaultAsync();
            return res;
        }

        private void DrawPageContent(ref PdfPage page, ModelsEntity.Offer res)
        {
            var gfx = XGraphics.FromPdfPage(page);
            var regularFont = new XFont("Times New Roman", 12, XFontStyle.Regular);
            var boldFont = new XFont("Times New Roman", 12, XFontStyle.Bold);
            var regularTableFont = new XFont("Times New Roman", 10, XFontStyle.Regular);
            var boldTableFont = new XFont("Times New Roman", 10, XFontStyle.Bold);


            int height = marginTop;
            var framePoints = new XPoint[5]
                {
                new XPoint(marginLeft,marginTop),
                new XPoint(page.Width-marginRight ,marginTop),
                new XPoint(page.Width-marginRight,page.Height-marginBottom),
                new XPoint(marginLeft,page.Height-marginBottom),
                new XPoint(marginLeft,marginTop),
                };
            var fieldSize = new XSize(page.Width - marginLeft - marginRight, lineHeight);
            var pageSize = new XSize(page.Width, page.Height);
            var blackPen = new XPen(XBrushes.Black);
            gfx.DrawLines(blackPen, framePoints);


            gfx.DrawString("Oferta", regularFont, XBrushes.Black, new XRect(NewLine(ref height, 0), pageSize), XStringFormats.TopCenter);
            gfx.DrawString(res.Name, boldFont, XBrushes.Black, new XRect(NewLine(ref height, 0), pageSize), XStringFormats.TopCenter);
            NewLine(ref height);
            gfx.DrawString("Projekt id:", regularFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.ProjectsId, boldFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);

            gfx.DrawString("Typ zamówienia:", regularFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.OrderType, boldFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);

            gfx.DrawString("Status:", regularFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.Status, boldFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);

            gfx.DrawString("Data oferty:", regularFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.OfferDate?.ToString("dd-MM-yyyy"), boldFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);

            gfx.DrawString("Klient:", regularFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.Clients.Name, boldFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);

            gfx.DrawString("Data zamówienia:", regularFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.OrderDate?.ToString("dd-MM-yyyy"), boldFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);

            gfx.DrawString("Planowane zakończenie:", regularFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.PlannedEnd?.ToString("dd-MM-yyyy"), boldFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);

            NewLine(ref height);

            gfx.DrawString("Pozycje oferty", regularFont, XBrushes.Black, new XRect(NewLine(ref height, 0), pageSize), XStringFormats.TopCenter);
            gfx.DrawLine(blackPen, NewLine(ref height), new XPoint(page.Width - marginLeft - marginRight, height));
            DrawOfferLineHeaderRow(ref gfx, ref height, boldTableFont, ref page);

            foreach (var ol in res.OfferLines)
            {
                gfx.DrawLine(blackPen, NewLine(ref height), new XPoint(page.Width - marginLeft - marginRight, height));
                DrawOfferLineRow(ref gfx, ref height, regularTableFont, ref page, ol);
            }
        }


        private void DrawOfferLineHeaderRow(ref XGraphics gfx, ref int height, XFont font, ref PdfPage page)
        {
            var actualRow = NewLine(ref height, 0, tableLineHeight);
            gfx.DrawString("Id produktu", font, XBrushes.Black, new XRect(NewColumn(actualRow, col1Pos), new XSize(col2Pos - col1Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString("Ilość", font, XBrushes.Black, new XRect(NewColumn(actualRow, col2Pos), new XSize(col3Pos - col2Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString("W", font, XBrushes.Black, new XRect(NewColumn(actualRow, col3Pos), new XSize(col4Pos - col3Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString("Medium", font, XBrushes.Black, new XRect(NewColumn(actualRow, col4Pos), new XSize(col5Pos - col4Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString("Wypsażenie", font, XBrushes.Black, new XRect(NewColumn(actualRow, col5Pos), new XSize(col6Pos - col5Pos, tableLineHeight)), XStringFormats.TopLeft);
            //gfx.DrawString("Sale", font, XBrushes.Black, new XRect(NewColumn(actualRow, col6Pos), new XSize(col7Pos - col6Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString("Cena sprzedaży", font, XBrushes.Black, new XRect(NewColumn(actualRow, col6Pos), new XSize(col7Pos - col6Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString("Liczba procesów", font, XBrushes.Black, new XRect(NewColumn(actualRow, col7Pos), new XSize(page.Width - col7Pos - marginRight, tableLineHeight)), XStringFormats.TopLeft);
        }


        private void DrawOfferLineRow(ref XGraphics gfx, ref int height, XFont regularFont, ref PdfPage page, OfferLines ol)
        {
            var actualRow = NewLine(ref height, 0, tableLineHeight);
            gfx.DrawString(ol.Product.ProductName.ToString(), regularFont, XBrushes.Black, new XRect(NewColumn(actualRow, col1Pos), new XSize(col2Pos - col1Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString(ol.Quantity.ToString(), regularFont, XBrushes.Black, new XRect(NewColumn(actualRow, col2Pos), new XSize(col3Pos - col2Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString(ol.W, regularFont, XBrushes.Black, new XRect(NewColumn(actualRow, col3Pos), new XSize(col4Pos - col3Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString(ol.Medium, regularFont, XBrushes.Black, new XRect(NewColumn(actualRow, col4Pos), new XSize(col5Pos - col4Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString(ol.AdditionalEquipment, regularFont, XBrushes.Black, new XRect(NewColumn(actualRow, col5Pos), new XSize(col6Pos - col5Pos, tableLineHeight)), XStringFormats.TopLeft);

            var salesPrice = "";
            if (ol.SalesPrice != null)
            {
                salesPrice = ol.SalesPrice.ToString();
            } 
            gfx.DrawString(salesPrice, regularFont, XBrushes.Black, new XRect(NewColumn(actualRow, col6Pos), new XSize(col7Pos - col6Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString(ol.OfferLineProcess.Count.ToString(), regularFont, XBrushes.Black, new XRect(NewColumn(actualRow, col7Pos), new XSize(page.Width - col7Pos - marginRight, tableLineHeight)), XStringFormats.TopLeft);
        }


        private XPoint NewColumn(XPoint actualRow, double colPos)
        {
            var newColWidth = new XPoint(actualRow.X + colPos, actualRow.Y);

            return newColWidth;
        }

        private XPoint NewLine(ref int height, int padding = -1, int tabLinHeight = -1)
        {
            if (padding < 0)
            {
                padding = marginLeft + textPadding;
            }

            if (tabLinHeight < 0)
            {
                height = height + interline;

            }
            else
            {
                height = height + tabLinHeight;
            }

            var point = new XPoint(padding, height);

            return point;
        }
    }
}
