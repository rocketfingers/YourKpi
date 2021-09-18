CREATE or alter VIEW [dbo].[vActivityHistory]
AS
SELECT p.Id AS PracownikId, p.Name AS PracownikName, pro.Id AS ProcessId, pro.NazwaProcesu AS ProcessName, pcs.CzasStart, ISNULL(pcs.CzasStop, GETDATE()) AS CzasStop, cl.ID AS ClientsId, cl.Name AS Client, c.Id AS PartId, 
                  c.Nazwa AS PartName, prod.ProductName AS WyrobId, ol.Quantity AS IloscCzesci
FROM     dbo.PracownikCzasStep AS pcs INNER JOIN
                  dbo.Process AS pro ON pcs.ProcessId = pro.Id INNER JOIN
                  dbo.Pracownik AS p ON pcs.PracownikId = p.Id LEFT OUTER JOIN
                  dbo.Czesci AS c ON pcs.CzescId = c.Id INNER JOIN
                  dbo.OfferLines AS ol ON ol.ID = pcs.OfferLinesID INNER JOIN
                  dbo.Produkty AS prod ON prod.ID = ol.ProductId INNER JOIN
                  dbo.Offer AS o ON o.Id = ol.ID INNER JOIN
                  dbo.Client AS cl ON cl.ID = o.ClientsId
WHERE  (pcs.CzasStart IS NOT NULL)

