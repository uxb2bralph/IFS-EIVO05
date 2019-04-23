SELECT  CDS_Document.DocID, CDS_Document.DocDate, InvoiceItem.InvoiceDate, CDS_Document.CurrentStep, center.DocumentFlowControl.LevelID, 
               InvoiceItem.TrackCode, InvoiceItem.No
FROM     CDS_Document INNER JOIN
               InvoiceItem ON CDS_Document.DocID = InvoiceItem.InvoiceID INNER JOIN
               center.DocumentFlowStep ON CDS_Document.DocID = center.DocumentFlowStep.DocID INNER JOIN
               center.DocumentFlowControl ON center.DocumentFlowStep.CurrentFlowStep = center.DocumentFlowControl.StepID AND 
               center.DocumentFlowStep.CurrentFlowStep = center.DocumentFlowControl.StepID
WHERE   (InvoiceItem.TrackCode = 'TE') AND (InvoiceItem.No IN ('75635061', '75635329', '75634623'))
go
UPDATE center.DocumentFlowStep
SET        CurrentFlowStep = center.DocumentFlowControl.PrevStep
FROM     CDS_Document INNER JOIN
               InvoiceItem ON CDS_Document.DocID = InvoiceItem.InvoiceID INNER JOIN
               InvoiceBuyer ON InvoiceItem.InvoiceID = InvoiceBuyer.InvoiceID INNER JOIN
               OrganizationStatus ON InvoiceBuyer.BuyerID = OrganizationStatus.CompanyID INNER JOIN
               center.DocumentFlowStep ON CDS_Document.DocID = center.DocumentFlowStep.DocID INNER JOIN
               center.DocumentFlowControl ON center.DocumentFlowStep.CurrentFlowStep = center.DocumentFlowControl.StepID AND 
               center.DocumentFlowStep.CurrentFlowStep = center.DocumentFlowControl.StepID AND 
               center.DocumentFlowStep.CurrentFlowStep = center.DocumentFlowControl.StepID AND 
               center.DocumentFlowStep.CurrentFlowStep = center.DocumentFlowControl.StepID
WHERE   (CDS_Document.DocDate >= '2017/5/12') AND (OrganizationStatus.Entrusting = 1) AND (CDS_Document.CurrentStep = 1305)
go
UPDATE CDS_Document
SET        CurrentStep = center.DocumentFlowControl.LevelID
FROM     CDS_Document INNER JOIN
               InvoiceItem ON CDS_Document.DocID = InvoiceItem.InvoiceID INNER JOIN
               InvoiceBuyer ON InvoiceItem.InvoiceID = InvoiceBuyer.InvoiceID INNER JOIN
               OrganizationStatus ON InvoiceBuyer.BuyerID = OrganizationStatus.CompanyID INNER JOIN
               center.DocumentFlowStep ON CDS_Document.DocID = center.DocumentFlowStep.DocID INNER JOIN
               center.DocumentFlowControl ON center.DocumentFlowStep.CurrentFlowStep = center.DocumentFlowControl.StepID AND 
               center.DocumentFlowStep.CurrentFlowStep = center.DocumentFlowControl.StepID AND 
               center.DocumentFlowStep.CurrentFlowStep = center.DocumentFlowControl.StepID AND 
               center.DocumentFlowStep.CurrentFlowStep = center.DocumentFlowControl.StepID
WHERE   (CDS_Document.DocDate >= '2017/5/12') AND (OrganizationStatus.Entrusting = 1)