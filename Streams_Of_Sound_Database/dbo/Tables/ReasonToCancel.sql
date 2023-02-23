CREATE TABLE [dbo].[ReasonToCancel]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[ReasonToCancel] NVARCHAR(MAX),
	SignUpForOpportunityId INT NULL,

	CONSTRAINT [FK_ReasonToCancel_SignUpForOpportunity] FOREIGN KEY (SignUpForOpportunityId) REFERENCES [SignUpForOpportunity](Id),
)
