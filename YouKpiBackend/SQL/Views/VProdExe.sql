CREATE or alter VIEW [dbo].[vProdExe]
AS

SELECT pro.Id + '-' + cast(ol.id AS varchar(50)) AS id, 'O' as typProcesu, ol.Id AS OfferLineID, o.Id AS OfferId, o.Name AS IdentyfikatorZamowienia, pro.id AS processId, ROW_NUMBER() OVER (PARTITION BY ol.OfferId, pro.id
ORDER BY ol.id) AS WierszOferty, c.Name AS Klient, c.NIp AS NumerKlienta, o.PlannedEnd, pro.NazwaProcesu, getdate() AS ZakonczenieData, DATEDIFF(day, GETDATE(), o.plannedEnd) AS Wynik, prod.ProductName AS IdentyfikatorWyrobu, 
prod.[TypWyrobuId] AS TypWyrobu, typ.dn AS DN, ol.[Quantity] AS IloscWyrobow, prod.wersja AS wersja, CzasSpedzony = isnull
    ((SELECT sum([LiczbaMinut])
      FROM      [dbo].[PracownikCzasStep]
      WHERE   [OfferLinesID] = ol.ID), 0), WykonaneStepy =
    (SELECT count(sow.id)
     FROM      [dbo].[StepOfferWykonanie] sow
     WHERE   sow.[OfferLineID] = ol.id AND sow.[Zakonczonie] = 1),
	 IloscStepow =
    (SELECT count(stps.id)
     FROM      [dbo].steps stps
     WHERE   stps.[ProcessesID] = pro.id)
FROM     Offer o LEFT JOIN
                  OfferLines ol ON o.Id = ol.OfferId LEFT JOIN
                  Client c ON c.ID = o.ClientsId LEFT JOIN
                  Produkty prod ON prod.Id = ol.ProductId LEFT JOIN
                  [dbo].[TypWyrobuIdDn] typ ON typ.[TypWyrobuId] = prod.[TypWyrobuId] AND typ.dn = prod.dn JOIN
				   OfferProcess op on op.OfferId = ol.OfferId join
                  Process pro ON pro.Id = op.ProcessId
WHERE  o.Status = 'otwarte' AND ol.ID IS NOT NULL
UNION ALL
SELECT olp.ProcessId + '-' + cast(ol.id AS varchar(50)) AS id, pro.TypZlecenia as typProcesu , ol.Id AS OfferLineID, o.Id AS OfferId, o.Name AS IdentyfikatorZamowienia, olp.ProcessId AS processId, ROW_NUMBER() OVER (PARTITION BY ol.OfferId, pro.id
ORDER BY ol.id) AS WierszOferty, c.Name AS Klient, c.NIp AS NumerKlienta, o.PlannedEnd, pro.NazwaProcesu, getdate() AS ZakonczenieData, DATEDIFF(day, GETDATE(), o.plannedEnd) AS Wynik, prod.ProductName AS IdentyfikatorWyrobu, 
prod.[TypWyrobuId] AS TypWyrobu, typ.dn AS DN, ol.[Quantity] AS IloscWyrobow, prod.wersja AS wersja, CzasSpedzony = isnull
    ((SELECT sum([LiczbaMinut])
      FROM      [dbo].[PracownikCzasStep]
      WHERE   [OfferLinesID] = ol.ID), 0), WykonaneStepy =
    (SELECT count(sow.id)
     FROM      [dbo].[StepOfferWykonanie] sow
     WHERE   sow.[OfferLineID] = ol.id AND sow.[Zakonczonie] = 1),
	 IloscStepow =
    (SELECT count(stps.id)
     FROM      [dbo].steps stps
     WHERE   stps.[ProcessesID] = pro.id)
FROM     Offer o LEFT JOIN
                  OfferLines ol ON o.Id = ol.OfferId LEFT JOIN
                  Client c ON c.ID = o.ClientsId LEFT JOIN
                  Produkty prod ON prod.Id = ol.ProductId LEFT JOIN
                  [dbo].[TypWyrobuIdDn] typ ON typ.[TypWyrobuId] = prod.[TypWyrobuId] AND typ.dn = prod.dn JOIN
                  OfferLineProcess olp ON olp.OfferLineId = ol.ID JOIN
                  Process pro ON pro.Id = olp.ProcessId
WHERE  o.Status != 'otwarte' AND ol.ID IS NOT NULL



