/****** Object:  View [dbo].[vOtifReport]    Script Date: 18.06.2021 21:34:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vOtifReport]
AS
SELECT o.PlannedEnd AS plannedEndOffer, olp.ProcessId, p.NazwaProcesu, s.StepNum, s.Sekwencja, COALESCE (sow.ClosedOn, GETDATE()) AS ClosedOn, p.NazwaGrupyProcesu,
                      (SELECT COALESCE(SUM(Sekwencja), 0) AS Expr1

     FROM      dbo.Steps AS s1

     WHERE(StepNum > s.StepNum) AND(ProcessesID = olp.ProcessId)) AS stepShouldEndBeforeDays
FROM     dbo.Offer AS o INNER JOIN
                  dbo.OfferLines AS ol ON ol.OfferId = o.Id INNER JOIN
                  dbo.OfferLineProcess AS olp ON ol.ID = olp.OfferLineId INNER JOIN
                  dbo.Steps AS s ON olp.ProcessId = s.ProcessesID INNER JOIN
                  dbo.Process AS p ON s.ProcessesID = p.Id LEFT OUTER JOIN
                  dbo.StepOfferWykonanie AS sow ON sow.OfferLineID = ol.ID AND sow.Step = s.StepNum AND sow.ProcessID = p.Id
WHERE  (o.PlannedEnd < GETDATE())
GO


