CREATE TABLE [dbo].[SignUpForOpportunity]
(
	[Id]			  INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[UserId]          UNIQUEIDENTIFIER NOT NULL,
	[OpportunityId]   INT NOT NULL,


	CONSTRAINT [FK_SignUpForOpportunity_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUsers](Id),
	CONSTRAINT [FK_SignUpForOpportunity_Opportunities] FOREIGN KEY (OpportunityId) REFERENCES [Opportunities](Id)
)
