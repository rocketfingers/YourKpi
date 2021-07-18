using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using YouKpiBackend.ModelsEntity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YouKpiBackend.DbContexts;
using System.Collections.Generic;

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

        private double availableScopeFroColumns = 440;
        private double col1Pos = 40;
        private double col2Pos = 180;
        private double col3Pos = 220;
        private double col4Pos = 250;
        private double col5Pos = 330;
        private double col6Pos = 400;
        private double col7Pos = 480;
        private int spaceBetweenColumns = 5;

        private string col1Name = "Id produktu";
        private string col2Name = "Ilość";
        private string col3Name = "W";
        private string col4Name = "Medium";
        private string col5Name = "Wypsażenie";
        private string col6Name = "Cena sprzedaży";
        private string col7Name = "Liczba procesów";


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
                CalculateColumnsPositions(res);
                DrawPageContent(ref page, res);
                document.Info.Title = "Oferta " + res.Name;
                document.Save(ms);
                document.Close();
                byteArr = ms.ToArray();
            }

            return byteArr != null ? byteArr : new byte[0];
        }

        private void CalculateColumnsPositions(ModelsEntity.Offer res)
        {
            var maxColCharNo = new Dictionary<int, int>();

            maxColCharNo.Add(1, res.OfferLines.Max(ol => ol.Product.ProductName.Count()));
            maxColCharNo.Add(2, res.OfferLines.Max(ol => ol.Quantity.ToString().Count()));
            maxColCharNo.Add(3, res.OfferLines.Max(ol => ol.W.Count()));
            maxColCharNo.Add(4, res.OfferLines.Max(ol => ol.Medium.Count()));
            maxColCharNo.Add(5, res.OfferLines.Max(ol => ol.AdditionalEquipment.Count()));
            maxColCharNo.Add(6, res.OfferLines.Max(ol => ol.SalesPrice.ToString().Count()));
            maxColCharNo.Add(7, res.OfferLines.Max(ol => ol.OfferLineProcess.Count()));

            maxColCharNo[1] = maxColCharNo[1] < col1Name.Length ? col1Name.Length : maxColCharNo[1];
            maxColCharNo[2] = maxColCharNo[2] < col2Name.Length ? col2Name.Length : maxColCharNo[2];
            maxColCharNo[3] = maxColCharNo[3] < col3Name.Length ? col3Name.Length : maxColCharNo[3];
            maxColCharNo[4] = maxColCharNo[4] < col4Name.Length ? col4Name.Length : maxColCharNo[4];
            maxColCharNo[5] = maxColCharNo[5] < col5Name.Length ? col5Name.Length : maxColCharNo[5];
            maxColCharNo[6] = maxColCharNo[6] < col6Name.Length ? col6Name.Length : maxColCharNo[6];
            maxColCharNo[7] = maxColCharNo[7] < col7Name.Length ? col7Name.Length : maxColCharNo[7];

            double allScopeInCharNo = 0;
            foreach (var item in maxColCharNo)
            {
                allScopeInCharNo += item.Value;
            }
            double sample = availableScopeFroColumns / allScopeInCharNo;
            if (allScopeInCharNo < 83)
            {
                sample = 6.2;
            }
            col1Pos = 40;
            col2Pos = maxColCharNo[1] * sample + col1Pos + spaceBetweenColumns;
            col3Pos = maxColCharNo[2] * sample + col2Pos + spaceBetweenColumns;
            col4Pos = maxColCharNo[3] * sample + col3Pos + spaceBetweenColumns + 10;
            col5Pos = maxColCharNo[4] * sample + col4Pos + spaceBetweenColumns;
            col6Pos = maxColCharNo[5] * sample + col5Pos + spaceBetweenColumns;
            col7Pos = maxColCharNo[6] * sample + col6Pos + spaceBetweenColumns;

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


            int height = marginTop + 40;
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
            //gfx.DrawLines(blackPen, framePoints);

            gfx.DrawImage(XImage.FromFile("Resources\\Chemitex logo - gradienty_m.jpg"), page.Width - marginRight - marginLeft - 160, marginTop , 160, 70);
            gfx.DrawString("Oferta", regularFont, XBrushes.Black, new XRect(NewLine(ref height, 0), pageSize), XStringFormats.TopCenter);
            gfx.DrawString(res.Name, boldFont, XBrushes.Black, new XRect(NewLine(ref height, 0), pageSize), XStringFormats.TopCenter);
            NewLine(ref height);

            var projIdHeight = height;
            gfx.DrawString("Projekt id:", regularFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.ProjectsId, boldFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);

            gfx.DrawString("Typ zamówienia:", regularFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.OrderType, boldFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);

            gfx.DrawString("Status:", regularFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.Status, boldFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);

            gfx.DrawString("Data oferty:", regularFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.OfferDate?.ToString("dd-MM-yyyy"), boldFont, XBrushes.Black, new XRect(NewLine(ref height), fieldSize), XStringFormats.TopLeft);

            height = projIdHeight;
            gfx.DrawString("Klient:", regularFont, XBrushes.Black, new XRect(NewLine(ref height, 400), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.Clients.Name, boldFont, XBrushes.Black, new XRect(NewLine(ref height, 400), fieldSize), XStringFormats.TopLeft);

            gfx.DrawString("Data zamówienia:", regularFont, XBrushes.Black, new XRect(NewLine(ref height, 400), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.OrderDate?.ToString("dd-MM-yyyy"), boldFont, XBrushes.Black, new XRect(NewLine(ref height, 400), fieldSize), XStringFormats.TopLeft);

            gfx.DrawString("Planowane zakończenie:", regularFont, XBrushes.Black, new XRect(NewLine(ref height, 400), fieldSize), XStringFormats.TopLeft);
            gfx.DrawString(res.PlannedEnd?.ToString("dd-MM-yyyy"), boldFont, XBrushes.Black, new XRect(NewLine(ref height, 400), fieldSize), XStringFormats.TopLeft);

            NewLine(ref height);
            NewLine(ref height);
            NewLine(ref height);


            gfx.DrawString("Pozycje oferty", regularFont, XBrushes.Black, new XRect(NewLine(ref height, 0), pageSize), XStringFormats.TopCenter);
            gfx.DrawLine(blackPen, NewLine(ref height), new XPoint(page.Width - marginLeft - marginRight, height));
            DrawOfferLineHeaderRow(ref gfx, ref height, boldTableFont, ref page);

            //var oldHeight = height;
            foreach (var ol in res.OfferLines)
            {
                gfx.DrawLine(blackPen, NewLine(ref height), new XPoint(page.Width - marginLeft - marginRight, height));
                DrawOfferLineRow(ref gfx, ref height, regularTableFont, ref page, ol);
            }

            //gfx.DrawLine(blackPen, new XPoint(col1Pos, oldHeight), new XPoint(col1Pos, height + interline));
            //gfx.DrawLine(blackPen, new XPoint(col2Pos, oldHeight), new XPoint(col2Pos, height + interline));
            //gfx.DrawLine(blackPen, new XPoint(col3Pos, oldHeight), new XPoint(col3Pos, height + interline));
            //gfx.DrawLine(blackPen, new XPoint(col4Pos, oldHeight), new XPoint(col4Pos, height + interline));
            //gfx.DrawLine(blackPen, new XPoint(col5Pos, oldHeight), new XPoint(col5Pos, height + interline));
            //gfx.DrawLine(blackPen, new XPoint(col6Pos, oldHeight), new XPoint(col6Pos, height + interline));
            //gfx.DrawLine(blackPen, new XPoint(col7Pos, oldHeight), new XPoint(col7Pos, height + interline));


        }



        private void DrawOfferLineHeaderRow(ref XGraphics gfx, ref int height, XFont font, ref PdfPage page)
        {
            var actualRow = NewLine(ref height, 0, tableLineHeight);
            gfx.DrawString(col1Name, font, XBrushes.Black, new XRect(NewColumn(actualRow, col1Pos), new XSize(col2Pos - col1Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString(col2Name, font, XBrushes.Black, new XRect(NewColumn(actualRow, col2Pos), new XSize(col3Pos - col2Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString(col3Name, font, XBrushes.Black, new XRect(NewColumn(actualRow, col3Pos), new XSize(col4Pos - col3Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString(col4Name, font, XBrushes.Black, new XRect(NewColumn(actualRow, col4Pos), new XSize(col5Pos - col4Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString(col5Name, font, XBrushes.Black, new XRect(NewColumn(actualRow, col5Pos), new XSize(col6Pos - col5Pos, tableLineHeight)), XStringFormats.TopLeft);
            //gfx.DrawString("Sale", font, XBrushes.Black, new XRect(NewColumn(actualRow, col6Pos), new XSize(col7Pos - col6Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString(col6Name, font, XBrushes.Black, new XRect(NewColumn(actualRow, col6Pos), new XSize(col7Pos - col6Pos, tableLineHeight)), XStringFormats.TopLeft);
            gfx.DrawString(col7Name, font, XBrushes.Black, new XRect(NewColumn(actualRow, col7Pos), new XSize(page.Width - col7Pos - marginRight, tableLineHeight)), XStringFormats.TopLeft);
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
