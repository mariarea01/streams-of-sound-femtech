CREATE TABLE [dbo].[InstrumentsSlots]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Instrument] Varchar(250) NOT NULL, 
    [StartTime] DATETIME NOT NULL, 
    [EndTime] DATETIME NOT NULL, 
    [OpportunityId] INT NULL,


    CONSTRAINT [FK_InstrumentSlots_Opportunities] FOREIGN KEY (OpportunityId) REFERENCES [Opportunities](Id)
)
