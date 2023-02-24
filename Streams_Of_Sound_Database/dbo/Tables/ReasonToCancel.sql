CREATE TABLE [dbo].[ReasonToCancel]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ReasonToCancel] NVARCHAR(MAX) NOT NULL,
	SignUpForOpportunityId INT NOT NULL,

	CONSTRAINT [FK_ReasonToCancel_SignUpForOpportunity] FOREIGN KEY (SignUpForOpportunityId) REFERENCES [SignUpForOpportunity](Id),
)
