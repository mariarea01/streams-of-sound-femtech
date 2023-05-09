CREATE TABLE [dbo].[ReasonToYeet]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ThisIsMyLastResort] NVARCHAR(MAX) NOT NULL,
	YeetedSlotId INT NOT NULL,
	[UserId] UNIQUEIDENTIFIER NOT NULL, 

	CONSTRAINT [FK_ReasonToYeet_InstrumentsSlots] FOREIGN KEY (YeetedSlotId) REFERENCES [InstrumentsSlots](Id),
	CONSTRAINT [FK_ReasonToYeet_AspNetUsers] FOREIGN KEY (UserId) REFERENCES [AspNetUsers](Id)
)
