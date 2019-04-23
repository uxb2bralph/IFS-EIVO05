
/****** Object:  Table [dbo].[DocumentSubscriptionQueue]    Script Date: 12/17/2014 14:46:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DocumentSubscriptionQueue](
	[DocID] [int] NOT NULL,
 CONSTRAINT [PK_DocumentSubscriptionQueue] PRIMARY KEY CLUSTERED 
(
	[DocID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[DocumentSubscriptionQueue]  WITH CHECK ADD  CONSTRAINT [FK_DocumentSubscriptionQueue_CDS_Document] FOREIGN KEY([DocID])
REFERENCES [dbo].[CDS_Document] ([DocID])
GO

ALTER TABLE [dbo].[DocumentSubscriptionQueue] CHECK CONSTRAINT [FK_DocumentSubscriptionQueue_CDS_Document]
GO


